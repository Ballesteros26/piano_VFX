using System;

namespace Melanchall.DryWetMidi.Core
{
	// Token: 0x02000192 RID: 402
	public enum UnknownChannelEventPolicy
	{
		// Token: 0x0400093D RID: 2365
		Abort,
		// Token: 0x0400093E RID: 2366
		SkipStatusByte,
		// Token: 0x0400093F RID: 2367
		SkipStatusByteAndOneDataByte,
		// Token: 0x04000940 RID: 2368
		SkipStatusByteAndTwoDataBytes,
		// Token: 0x04000941 RID: 2369
		UseCallback
	}
}
