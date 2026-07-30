using System;

namespace System.Net
{
	/// <summary>Specifies protocols for authentication.</summary>
	// Token: 0x02000419 RID: 1049
	[Flags]
	public enum AuthenticationSchemes
	{
		/// <summary>No authentication is allowed. A client requesting an <see cref="T:System.Net.HttpListener" /> object with this flag set will always receive a 403 Forbidden status. Use this flag when a resource should never be served to a client.</summary>
		// Token: 0x04001BBE RID: 7102
		None = 0,
		/// <summary>Specifies digest authentication.</summary>
		// Token: 0x04001BBF RID: 7103
		Digest = 1,
		/// <summary>Negotiates with the client to determine the authentication scheme. If both client and server support Kerberos, it is used; otherwise, NTLM is used.</summary>
		// Token: 0x04001BC0 RID: 7104
		Negotiate = 2,
		/// <summary>Specifies NTLM authentication.</summary>
		// Token: 0x04001BC1 RID: 7105
		Ntlm = 4,
		/// <summary>Specifies basic authentication. </summary>
		// Token: 0x04001BC2 RID: 7106
		Basic = 8,
		/// <summary>Specifies anonymous authentication.</summary>
		// Token: 0x04001BC3 RID: 7107
		Anonymous = 32768,
		/// <summary>Specifies Windows authentication.</summary>
		// Token: 0x04001BC4 RID: 7108
		IntegratedWindowsAuthentication = 6
	}
}
