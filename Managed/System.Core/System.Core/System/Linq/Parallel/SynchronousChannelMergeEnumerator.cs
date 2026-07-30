using System;

namespace System.Linq.Parallel
{
	// Token: 0x02000124 RID: 292
	internal sealed class SynchronousChannelMergeEnumerator<T> : MergeEnumerator<T>
	{
		// Token: 0x06000987 RID: 2439 RVA: 0x0001E9D3 File Offset: 0x0001CBD3
		internal SynchronousChannelMergeEnumerator(QueryTaskGroupState taskGroupState, SynchronousChannel<T>[] channels)
			: base(taskGroupState)
		{
			this._channels = channels;
			this._channelIndex = -1;
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x0001E9EA File Offset: 0x0001CBEA
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

		// Token: 0x06000989 RID: 2441 RVA: 0x0001EA18 File Offset: 0x0001CC18
		public override bool MoveNext()
		{
			if (this._channelIndex == -1)
			{
				this._channelIndex = 0;
			}
			while (this._channelIndex != this._channels.Length)
			{
				SynchronousChannel<T> synchronousChannel = this._channels[this._channelIndex];
				if (synchronousChannel.Count != 0)
				{
					this._currentElement = synchronousChannel.Dequeue();
					return true;
				}
				this._channelIndex++;
			}
			return false;
		}

		// Token: 0x04000590 RID: 1424
		private SynchronousChannel<T>[] _channels;

		// Token: 0x04000591 RID: 1425
		private int _channelIndex;

		// Token: 0x04000592 RID: 1426
		private T _currentElement;
	}
}
