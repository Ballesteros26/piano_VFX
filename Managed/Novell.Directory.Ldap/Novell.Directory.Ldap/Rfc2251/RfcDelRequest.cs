using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000064 RID: 100
	public class RfcDelRequest : RfcLdapDN, RfcRequest
	{
		// Token: 0x0600036A RID: 874 RVA: 0x0001098A File Offset: 0x0000EB8A
		public RfcDelRequest(string dn)
			: base(dn)
		{
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00010993 File Offset: 0x0000EB93
		[CLSCompliant(false)]
		public RfcDelRequest(sbyte[] dn)
			: base(dn)
		{
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001099C File Offset: 0x0000EB9C
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, false, 10);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000109A7 File Offset: 0x0000EBA7
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			if (base_Renamed == null)
			{
				return new RfcDelRequest(base.byteValue());
			}
			return new RfcDelRequest(base_Renamed);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000109BE File Offset: 0x0000EBBE
		public string getRequestDN()
		{
			return base.stringValue();
		}
	}
}
