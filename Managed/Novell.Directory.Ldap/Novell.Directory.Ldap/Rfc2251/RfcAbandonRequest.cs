using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000053 RID: 83
	internal class RfcAbandonRequest : RfcMessageID, RfcRequest
	{
		// Token: 0x06000324 RID: 804 RVA: 0x00010355 File Offset: 0x0000E555
		public RfcAbandonRequest(int msgId)
			: base(msgId)
		{
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0001035E File Offset: 0x0000E55E
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, false, 16);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00010369 File Offset: 0x0000E569
		public RfcRequest dupRequest(string base_Renamed, string filter, bool reference)
		{
			throw new LdapException("NO_DUP_REQUEST", new object[] { "Abandon" }, 92, null);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00010386 File Offset: 0x0000E586
		public string getRequestDN()
		{
			return null;
		}
	}
}
