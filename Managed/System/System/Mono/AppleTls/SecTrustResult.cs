using System;

namespace Mono.AppleTls
{
	// Token: 0x020000B0 RID: 176
	internal enum SecTrustResult
	{
		// Token: 0x04000AAD RID: 2733
		Invalid,
		// Token: 0x04000AAE RID: 2734
		Proceed,
		// Token: 0x04000AAF RID: 2735
		Confirm,
		// Token: 0x04000AB0 RID: 2736
		Deny,
		// Token: 0x04000AB1 RID: 2737
		Unspecified,
		// Token: 0x04000AB2 RID: 2738
		RecoverableTrustFailure,
		// Token: 0x04000AB3 RID: 2739
		FatalTrustFailure,
		// Token: 0x04000AB4 RID: 2740
		ResultOtherError
	}
}
