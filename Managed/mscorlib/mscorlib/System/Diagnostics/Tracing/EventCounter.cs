using System;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000A91 RID: 2705
	public class EventCounter : IDisposable
	{
		// Token: 0x06006289 RID: 25225 RVA: 0x00141528 File Offset: 0x0013F728
		public EventCounter(string name, EventSource eventSource)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			this.InitializeBuffer();
			this._name = name;
			this._group = EventCounterGroup.GetEventCounterGroup(eventSource);
			this._group.Add(this);
			this._min = float.PositiveInfinity;
			this._max = float.NegativeInfinity;
		}

		// Token: 0x0600628A RID: 25226 RVA: 0x00141592 File Offset: 0x0013F792
		public void WriteMetric(float value)
		{
			this.Enqueue(value);
		}

		// Token: 0x0600628B RID: 25227 RVA: 0x0014159C File Offset: 0x0013F79C
		public void Dispose()
		{
			EventCounterGroup group = this._group;
			if (group != null)
			{
				group.Remove(this);
				this._group = null;
			}
		}

		// Token: 0x0600628C RID: 25228 RVA: 0x001415C4 File Offset: 0x0013F7C4
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"EventCounter '",
				this._name,
				"' Count ",
				this._count,
				" Mean ",
				((double)this._sum / (double)this._count).ToString("n3")
			});
		}

		// Token: 0x170011B2 RID: 4530
		// (get) Token: 0x0600628D RID: 25229 RVA: 0x0014162A File Offset: 0x0013F82A
		private object MyLock
		{
			get
			{
				return this._bufferedValues;
			}
		}

		// Token: 0x0600628E RID: 25230 RVA: 0x00141634 File Offset: 0x0013F834
		private void InitializeBuffer()
		{
			this._bufferedValues = new float[10];
			for (int i = 0; i < this._bufferedValues.Length; i++)
			{
				this._bufferedValues[i] = float.NegativeInfinity;
			}
		}

		// Token: 0x0600628F RID: 25231 RVA: 0x00141674 File Offset: 0x0013F874
		private void Enqueue(float value)
		{
			int num = this._bufferedValuesIndex;
			float num2;
			do
			{
				num2 = Interlocked.CompareExchange(ref this._bufferedValues[num], value, float.NegativeInfinity);
				num++;
				if (this._bufferedValues.Length <= num)
				{
					object myLock = this.MyLock;
					lock (myLock)
					{
						this.Flush();
					}
					num = 0;
				}
			}
			while (num2 != float.NegativeInfinity);
			this._bufferedValuesIndex = num;
		}

		// Token: 0x06006290 RID: 25232 RVA: 0x001416FC File Offset: 0x0013F8FC
		private void Flush()
		{
			for (int i = 0; i < this._bufferedValues.Length; i++)
			{
				float num = Interlocked.Exchange(ref this._bufferedValues[i], float.NegativeInfinity);
				if (num != float.NegativeInfinity)
				{
					this.OnMetricWritten(num);
				}
			}
			this._bufferedValuesIndex = 0;
		}

		// Token: 0x06006291 RID: 25233 RVA: 0x00141750 File Offset: 0x0013F950
		private void OnMetricWritten(float value)
		{
			this._sum += value;
			this._sumSquared += value * value;
			if (value > this._max)
			{
				this._max = value;
			}
			if (value < this._min)
			{
				this._min = value;
			}
			this._count++;
		}

		// Token: 0x06006292 RID: 25234 RVA: 0x001417AC File Offset: 0x0013F9AC
		internal EventCounterPayload GetEventCounterPayload()
		{
			object myLock = this.MyLock;
			EventCounterPayload eventCounterPayload2;
			lock (myLock)
			{
				this.Flush();
				EventCounterPayload eventCounterPayload = new EventCounterPayload();
				eventCounterPayload.Name = this._name;
				eventCounterPayload.Count = this._count;
				if (0 < this._count)
				{
					eventCounterPayload.Mean = this._sum / (float)this._count;
					eventCounterPayload.StandardDeviation = (float)Math.Sqrt((double)(this._sumSquared / (float)this._count - this._sum * this._sum / (float)this._count / (float)this._count));
				}
				else
				{
					eventCounterPayload.Mean = 0f;
					eventCounterPayload.StandardDeviation = 0f;
				}
				eventCounterPayload.Min = this._min;
				eventCounterPayload.Max = this._max;
				this.ResetStatistics();
				eventCounterPayload2 = eventCounterPayload;
			}
			return eventCounterPayload2;
		}

		// Token: 0x06006293 RID: 25235 RVA: 0x0014189C File Offset: 0x0013FA9C
		private void ResetStatistics()
		{
			this._count = 0;
			this._sum = 0f;
			this._sumSquared = 0f;
			this._min = float.PositiveInfinity;
			this._max = float.NegativeInfinity;
		}

		// Token: 0x04003111 RID: 12561
		private readonly string _name;

		// Token: 0x04003112 RID: 12562
		private EventCounterGroup _group;

		// Token: 0x04003113 RID: 12563
		private const int BufferedSize = 10;

		// Token: 0x04003114 RID: 12564
		private const float UnusedBufferSlotValue = float.NegativeInfinity;

		// Token: 0x04003115 RID: 12565
		private const int UnsetIndex = -1;

		// Token: 0x04003116 RID: 12566
		private volatile float[] _bufferedValues;

		// Token: 0x04003117 RID: 12567
		private volatile int _bufferedValuesIndex;

		// Token: 0x04003118 RID: 12568
		private int _count;

		// Token: 0x04003119 RID: 12569
		private float _sum;

		// Token: 0x0400311A RID: 12570
		private float _sumSquared;

		// Token: 0x0400311B RID: 12571
		private float _min;

		// Token: 0x0400311C RID: 12572
		private float _max;
	}
}
