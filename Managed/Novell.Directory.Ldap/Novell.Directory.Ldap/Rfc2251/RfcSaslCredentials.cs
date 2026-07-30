using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200007B RID: 123
	public class RfcSaslCredentials : Asn1Sequence
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x000124BC File Offset: 0x000106BC
		public RfcSaslCredentials(RfcLdapString mechanism)
			: this(mechanism, null)
		{
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000124C6 File Offset: 0x000106C6
		public RfcSaslCredentials(RfcLdapString mechanism, Asn1OctetString credentials)
			: base(2)
		{
			base.add(mechanism);
			if (credentials != null)
			{
				base.add(credentials);
			}
		}
	}
}
