using System;

namespace System.Web
{
	// Token: 0x0200004E RID: 78
	public interface ITlsTokenBindingInfo
	{
		// Token: 0x060003D1 RID: 977
		byte[] GetProvidedTokenBindingId();

		// Token: 0x060003D2 RID: 978
		byte[] GetReferredTokenBindingId();
	}
}
