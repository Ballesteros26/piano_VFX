using System;

namespace System.Net.Mail
{
	/// <summary>Specifies the level of access allowed to a Simple Mail Transport Protocol (SMTP) server.</summary>
	// Token: 0x02000583 RID: 1411
	public enum SmtpAccess
	{
		/// <summary>No access to an SMTP host.</summary>
		// Token: 0x04002484 RID: 9348
		None,
		/// <summary>Connection to an SMTP host on the default port (port 25).</summary>
		// Token: 0x04002485 RID: 9349
		Connect,
		/// <summary>Connection to an SMTP host on any port.</summary>
		// Token: 0x04002486 RID: 9350
		ConnectToUnrestrictedPort
	}
}
