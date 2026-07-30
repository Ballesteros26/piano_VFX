using System;

namespace UnityEngine.Timeline
{
	// Token: 0x02000020 RID: 32
	internal abstract class RuntimeClipBase : RuntimeElement
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060001F8 RID: 504
		public abstract double start { get; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001F9 RID: 505
		public abstract double duration { get; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00007A18 File Offset: 0x00005C18
		public override long intervalStart
		{
			get
			{
				return DiscreteTime.GetNearestTick(this.start);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060001FB RID: 507 RVA: 0x00007A25 File Offset: 0x00005C25
		public override long intervalEnd
		{
			get
			{
				return DiscreteTime.GetNearestTick(this.start + this.duration);
			}
		}
	}
}
