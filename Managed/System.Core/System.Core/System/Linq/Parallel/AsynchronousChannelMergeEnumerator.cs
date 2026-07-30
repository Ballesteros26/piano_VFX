using System;

namespace System.Linq.Parallel
{
	// Token: 0x02000119 RID: 281
	internal sealed class AsynchronousChannelMergeEnumerator<T> : MergeEnumerator<T>
	{
		// Token: 0x06000959 RID: 2393 RVA: 0x0001DDB4 File Offset: 0x0001BFB4
		internal AsynchronousChannelMergeEnumerator(QueryTaskGroupState taskGroupState, AsynchronousChannel<T>[] channels, IntValueEvent consumerEvent)
			: base(taskGroupState)
		{
			this._channels = channels;
			this._channelIndex = -1;
			this._done = new bool[this._channels.Length];
			this._consumerEvent = consumerEvent;
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0001DDE5 File Offset: 0x0001BFE5
		public override T Current
		{
			get
			{
				if (this._channelIndex == -1 || this._channelIndex == this._channels.Length)
				{
					throw new InvalidOperationException("Enumeration has not started. MoveNext must be called to initiate enumeration.");
				}
				return this._currentElement;
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0001DE14 File Offset: 0x0001C014
		public override bool MoveNext()
		{
			int num = this._channelIndex;
			if (num == -1)
			{
				num = (this._channelIndex = 0);
			}
			if (num == this._channels.Length)
			{
				return false;
			}
			if (!this._done[num] && this._channels[num].TryDequeue(ref this._currentElement))
			{
				this._channelIndex = (num + 1) % this._channels.Length;
				return true;
			}
			return this.MoveNextSlowPath();
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x0001DE7C File Offset: 0x0001C07C
		private bool MoveNextSlowPath()
		{
			int num = 0;
			int num2 = this._channelIndex;
			int num3;
			while ((num3 = this._channelIndex) != this._channels.Length)
			{
				AsynchronousChannel<T> asynchronousChannel = this._channels[num3];
				bool flag = this._done[num3];
				if (!flag && asynchronousChannel.TryDequeue(ref this._currentElement))
				{
					this._channelIndex = (num3 + 1) % this._channels.Length;
					return true;
				}
				if (!flag && asynchronousChannel.IsDone)
				{
					if (!asynchronousChannel.IsChunkBufferEmpty)
					{
						asynchronousChannel.TryDequeue(ref this._currentElement);
						return true;
					}
					this._done[num3] = true;
					flag = true;
					asynchronousChannel.Dispose();
				}
				if (flag && ++num == this._channels.Length)
				{
					this._channelIndex = this._channels.Length;
					break;
				}
				num3 = (this._channelIndex = (num3 + 1) % this._channels.Length);
				if (num3 == num2)
				{
					try
					{
						num = 0;
						for (int i = 0; i < this._channels.Length; i++)
						{
							bool flag2 = false;
							if (!this._done[i] && this._channels[i].TryDequeue(ref this._currentElement, ref flag2))
							{
								return true;
							}
							if (flag2)
							{
								if (!this._done[i])
								{
									this._done[i] = true;
								}
								if (++num == this._channels.Length)
								{
									num3 = (this._channelIndex = this._channels.Length);
									break;
								}
							}
						}
						if (num3 == this._channels.Length)
						{
							break;
						}
						this._consumerEvent.Wait();
						num3 = (this._channelIndex = this._consumerEvent.Value);
						this._consumerEvent.Reset();
						num2 = num3;
						num = 0;
					}
					finally
					{
						for (int j = 0; j < this._channels.Length; j++)
						{
							if (!this._done[j])
							{
								this._channels[j].DoneWithDequeueWait();
							}
						}
					}
					continue;
				}
			}
			this._taskGroupState.QueryEnd(false);
			return false;
		}

		// Token: 0x0600095D RID: 2397 RVA: 0x0001E06C File Offset: 0x0001C26C
		public override void Dispose()
		{
			if (this._consumerEvent != null)
			{
				base.Dispose();
				this._consumerEvent.Dispose();
				this._consumerEvent = null;
			}
		}

		// Token: 0x04000569 RID: 1385
		private AsynchronousChannel<T>[] _channels;

		// Token: 0x0400056A RID: 1386
		private IntValueEvent _consumerEvent;

		// Token: 0x0400056B RID: 1387
		private bool[] _done;

		// Token: 0x0400056C RID: 1388
		private int _channelIndex;

		// Token: 0x0400056D RID: 1389
		private T _currentElement;
	}
}
