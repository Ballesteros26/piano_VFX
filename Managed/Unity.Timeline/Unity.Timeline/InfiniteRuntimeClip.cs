using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200001B RID: 27
	internal class InfiniteRuntimeClip : RuntimeElement
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x0000719A File Offset: 0x0000539A
		public InfiniteRuntimeClip(Playable playable)
		{
			this.m_Playable = playable;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000071A9 File Offset: 0x000053A9
		public override long intervalStart
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000071AD File Offset: 0x000053AD
		public override long intervalEnd
		{
			get
			{
				return InfiniteRuntimeClip.kIntervalEnd;
			}
		}

		// Token: 0x1700008D RID: 141
		// (set) Token: 0x060001DB RID: 475 RVA: 0x000071B4 File Offset: 0x000053B4
		public override bool enable
		{
			set
			{
				if (value)
				{
					this.m_Playable.Play<Playable>();
					return;
				}
				this.m_Playable.Pause<Playable>();
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000071D0 File Offset: 0x000053D0
		public override void EvaluateAt(double localTime, FrameData frameData)
		{
			this.m_Playable.SetTime(localTime);
		}

		// Token: 0x040000AC RID: 172
		private Playable m_Playable;

		// Token: 0x040000AD RID: 173
		private static readonly long kIntervalEnd = DiscreteTime.GetNearestTick(TimelineClip.kMaxTimeValue);
	}
}
