using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000193 RID: 403
	public enum UnknownChunkIdPolicy : byte
	{
		// Token: 0x04000943 RID: 2371
		ReadAsUnknownChunk,
		// Token: 0x04000944 RID: 2372
		Skip,
		// Token: 0x04000945 RID: 2373
		Abort
	}
}
