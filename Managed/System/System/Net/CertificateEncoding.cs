using System;

namespace System.Net
{
	// Token: 0x0200044A RID: 1098
	internal enum CertificateEncoding
	{
		// Token: 0x04001D48 RID: 7496
		Zero,
		// Token: 0x04001D49 RID: 7497
		X509AsnEncoding,
		// Token: 0x04001D4A RID: 7498
		X509NdrEncoding,
		// Token: 0x04001D4B RID: 7499
		Pkcs7AsnEncoding = 65536,
		// Token: 0x04001D4C RID: 7500
		Pkcs7NdrEncoding = 131072,
		// Token: 0x04001D4D RID: 7501
		AnyAsnEncoding = 65537
	}
}
