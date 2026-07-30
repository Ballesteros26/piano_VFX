using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000081 RID: 129
	public class RfcUnbindRequest : Asn1Null, RfcRequest
	{
		// Token: 0x060003F5 RID: 1013 RVA: 0x00012641 File Offset: 0x00010841
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, false, 2);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001264B File Offset: 0x0001084B
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			throw new LdapException("NO_DUP_REQUEST", new object[] { "unbind" }, 92, null);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00012668 File Offset: 0x00010868
		public string getRequestDN()
		{
			return null;
		}
	}
}
