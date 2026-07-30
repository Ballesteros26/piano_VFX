using System;

namespace Mono.Security.X509
{
	// Token: 0x02000053 RID: 83
	internal class PKCS5
	{
		// Token: 0x040004A5 RID: 1189
		public const string pbeWithMD2AndDESCBC = "1.2.840.113549.1.5.1";

		// Token: 0x040004A6 RID: 1190
		public const string pbeWithMD5AndDESCBC = "1.2.840.113549.1.5.3";

		// Token: 0x040004A7 RID: 1191
		public const string pbeWithMD2AndRC2CBC = "1.2.840.113549.1.5.4";

		// Token: 0x040004A8 RID: 1192
		public const string pbeWithMD5AndRC2CBC = "1.2.840.113549.1.5.6";

		// Token: 0x040004A9 RID: 1193
		public const string pbeWithSHA1AndDESCBC = "1.2.840.113549.1.5.10";

		// Token: 0x040004AA RID: 1194
		public const string pbeWithSHA1AndRC2CBC = "1.2.840.113549.1.5.11";
	}
}
