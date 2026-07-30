using System;

namespace Melanchall.DryWetMidi.Tools
{
	// Token: 0x0200004C RID: 76
	public enum QuantizingBeyondFixedEndPolicy
	{
		// Token: 0x040000E4 RID: 228
		CollapseAndFix,
		// Token: 0x040000E5 RID: 229
		CollapseAndMove,
		// Token: 0x040000E6 RID: 230
		SwapEnds,
		// Token: 0x040000E7 RID: 231
		Skip,
		// Token: 0x040000E8 RID: 232
		Abort
	}
}
