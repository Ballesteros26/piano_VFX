using System;

namespace System.Web.Configuration
{
	/// <summary>Specifies the authentication mode to use in a Web application.</summary>
	// Token: 0x0200055D RID: 1373
	public enum AuthenticationMode
	{
		/// <summary>Specifies no authentication.</summary>
		// Token: 0x04002000 RID: 8192
		None,
		/// <summary>Specifies Windows as the authentication mode. This mode applies when using the Internet Information Services (IIS) authentication methods Basic, Digest, Integrated Windows (NTLM/Kerberos), or certificates.</summary>
		// Token: 0x04002001 RID: 8193
		Windows,
		/// <summary>Specifies Microsoft Passport as the authentication mode.</summary>
		// Token: 0x04002002 RID: 8194
		[Obsolete("This field is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
		Passport,
		/// <summary>Specifies ASP.NET Forms-based authentication as the authentication mode.</summary>
		// Token: 0x04002003 RID: 8195
		Forms
	}
}
