using System;

namespace Melanchall.DryWetMidi.Composing
{
	// Token: 0x020001B4 RID: 436
	internal abstract class PatternAction
	{
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x00022F68 File Offset: 0x00021168
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x00022F70 File Offset: 0x00021170
		public PatternActionState State { get; set; }

		// Token: 0x06000A70 RID: 2672
		public abstract PatternActionResult Invoke(long time, PatternContext context);

		// Token: 0x06000A71 RID: 2673
		public abstract PatternAction Clone();
	}
}
