using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000095 RID: 149
	internal enum DnsRCode : ushort
	{
		// Token: 0x0400088A RID: 2186
		NoError,
		// Token: 0x0400088B RID: 2187
		FormErr,
		// Token: 0x0400088C RID: 2188
		ServFail,
		// Token: 0x0400088D RID: 2189
		NXDomain,
		// Token: 0x0400088E RID: 2190
		NotImp,
		// Token: 0x0400088F RID: 2191
		Refused,
		// Token: 0x04000890 RID: 2192
		YXDomain,
		// Token: 0x04000891 RID: 2193
		YXRRSet,
		// Token: 0x04000892 RID: 2194
		NXRRSet,
		// Token: 0x04000893 RID: 2195
		NotAuth,
		// Token: 0x04000894 RID: 2196
		NotZone,
		// Token: 0x04000895 RID: 2197
		BadVers = 16,
		// Token: 0x04000896 RID: 2198
		BadSig = 16,
		// Token: 0x04000897 RID: 2199
		BadKey,
		// Token: 0x04000898 RID: 2200
		BadTime,
		// Token: 0x04000899 RID: 2201
		BadMode,
		// Token: 0x0400089A RID: 2202
		BadName,
		// Token: 0x0400089B RID: 2203
		BadAlg,
		// Token: 0x0400089C RID: 2204
		BadTrunc
	}
}
