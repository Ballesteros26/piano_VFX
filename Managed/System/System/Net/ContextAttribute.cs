using System;

namespace System.Net
{
	// Token: 0x02000440 RID: 1088
	internal enum ContextAttribute
	{
		// Token: 0x04001CFA RID: 7418
		Sizes,
		// Token: 0x04001CFB RID: 7419
		Names,
		// Token: 0x04001CFC RID: 7420
		Lifespan,
		// Token: 0x04001CFD RID: 7421
		DceInfo,
		// Token: 0x04001CFE RID: 7422
		StreamSizes,
		// Token: 0x04001CFF RID: 7423
		Authority = 6,
		// Token: 0x04001D00 RID: 7424
		PackageInfo = 10,
		// Token: 0x04001D01 RID: 7425
		NegotiationInfo = 12,
		// Token: 0x04001D02 RID: 7426
		UniqueBindings = 25,
		// Token: 0x04001D03 RID: 7427
		EndpointBindings,
		// Token: 0x04001D04 RID: 7428
		ClientSpecifiedSpn,
		// Token: 0x04001D05 RID: 7429
		RemoteCertificate = 83,
		// Token: 0x04001D06 RID: 7430
		LocalCertificate,
		// Token: 0x04001D07 RID: 7431
		RootStore,
		// Token: 0x04001D08 RID: 7432
		IssuerListInfoEx = 89,
		// Token: 0x04001D09 RID: 7433
		ConnectionInfo,
		// Token: 0x04001D0A RID: 7434
		UiInfo = 104
	}
}
