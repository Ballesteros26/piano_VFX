using System;

namespace System.Net
{
	// Token: 0x02000443 RID: 1091
	internal enum BufferType
	{
		// Token: 0x04001D13 RID: 7443
		Empty,
		// Token: 0x04001D14 RID: 7444
		Data,
		// Token: 0x04001D15 RID: 7445
		Token,
		// Token: 0x04001D16 RID: 7446
		Parameters,
		// Token: 0x04001D17 RID: 7447
		Missing,
		// Token: 0x04001D18 RID: 7448
		Extra,
		// Token: 0x04001D19 RID: 7449
		Trailer,
		// Token: 0x04001D1A RID: 7450
		Header,
		// Token: 0x04001D1B RID: 7451
		Padding = 9,
		// Token: 0x04001D1C RID: 7452
		Stream,
		// Token: 0x04001D1D RID: 7453
		ChannelBindings = 14,
		// Token: 0x04001D1E RID: 7454
		TargetHost = 16,
		// Token: 0x04001D1F RID: 7455
		ReadOnlyFlag = -2147483648,
		// Token: 0x04001D20 RID: 7456
		ReadOnlyWithChecksum = 268435456
	}
}
