using System;

namespace System.Web.Security
{
	/// <summary>Describes how information in a cookie is protected.</summary>
	// Token: 0x020004BA RID: 1210
	public enum CookieProtection
	{
		/// <summary>Do not protect information in the cookie. Information in the cookie is stored in clear text and not validated when sent back to the server.</summary>
		// Token: 0x04001DB4 RID: 7604
		None,
		/// <summary>Ensure that the information in the cookie has not been altered before being sent back to the server.</summary>
		// Token: 0x04001DB5 RID: 7605
		Validation,
		/// <summary>Encrypt the information in the cookie.</summary>
		// Token: 0x04001DB6 RID: 7606
		Encryption,
		/// <summary>Use both <see cref="F:System.Web.Security.CookieProtection.Validation" /> and <see cref="F:System.Web.Security.CookieProtection.Encryption" /> to protect the information in the cookie.</summary>
		// Token: 0x04001DB7 RID: 7607
		All
	}
}
