using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200005D RID: 93
	public class RfcAuthenticationChoice : Asn1Choice
	{
		// Token: 0x0600033D RID: 829 RVA: 0x000104EA File Offset: 0x0000E6EA
		public RfcAuthenticationChoice(Asn1Tagged choice)
			: base(choice)
		{
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000104F3 File Offset: 0x0000E6F3
		[CLSCompliant(false)]
		public RfcAuthenticationChoice(string mechanism, sbyte[] credentials)
			: base(new Asn1Tagged(new Asn1Identifier(2, true, 3), new RfcSaslCredentials(new RfcLdapString(mechanism), (credentials != null) ? new Asn1OctetString(credentials) : null), false))
		{
		}
	}
}
