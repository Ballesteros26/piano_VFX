using System;

namespace System.Web.Security.Cryptography
{
	// Token: 0x020004D6 RID: 1238
	internal interface ICryptoService
	{
		// Token: 0x06003843 RID: 14403
		byte[] Protect(byte[] clearData);

		// Token: 0x06003844 RID: 14404
		byte[] Unprotect(byte[] protectedData);
	}
}
