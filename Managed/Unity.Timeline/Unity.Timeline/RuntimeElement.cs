using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000021 RID: 33
	internal abstract class RuntimeElement : IInterval
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060001FD RID: 509
		public abstract long intervalStart { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060001FE RID: 510
		public abstract long intervalEnd { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00007A41 File Offset: 0x00005C41
		// (set) Token: 0x06000200 RID: 512 RVA: 0x00007A49 File Offset: 0x00005C49
		public int intervalBit { get; set; }

		// Token: 0x1700009E RID: 158
		// (set) Token: 0x06000201 RID: 513
		public abstract bool enable { set; }

		// Token: 0x06000202 RID: 514
		public abstract void EvaluateAt(double localTime, FrameData frameData);

		// Token: 0x06000203 RID: 515 RVA: 0x000028DC File Offset: 0x00000ADC
		public virtual void Reset()
		{
		}
	}
}
