using System;

namespace System.Web.Security
{
	/// <summary>Specifies the connection protection options supported by the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> class.</summary>
	// Token: 0x020004B4 RID: 1204
	public enum ActiveDirectoryConnectionProtection
	{
		/// <summary>No transport layer security is used. Explicit credentials for the Active Directory connection must be provided in the configuration file.</summary>
		// Token: 0x04001DAA RID: 7594
		None,
		/// <summary>An SSL connection is used to connect to the Active Directory server.</summary>
		// Token: 0x04001DAB RID: 7595
		Ssl,
		/// <summary>The connection to the Active Directory server is secured by digitally signing and encrypting each packet sent to the server. </summary>
		// Token: 0x04001DAC RID: 7596
		SignAndSeal
	}
}
