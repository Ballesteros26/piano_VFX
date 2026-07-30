using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000183 RID: 387
	public enum InvalidChannelEventParameterValuePolicy : byte
	{
		// Token: 0x040008FE RID: 2302
		Abort,
		// Token: 0x040008FF RID: 2303
		ReadValid,
		// Token: 0x04000900 RID: 2304
		SnapToLimits
	}
}
