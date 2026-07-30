using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000073 RID: 115
	public class RfcModifyDNRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x0001236A File Offset: 0x0001056A
		public RfcModifyDNRequest(RfcLdapDN entry, RfcRelativeLdapDN newrdn, Asn1Boolean deleteoldrdn)
			: this(entry, newrdn, deleteoldrdn, null)
		{
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00012376 File Offset: 0x00010576
		public RfcModifyDNRequest(RfcLdapDN entry, RfcRelativeLdapDN newrdn, Asn1Boolean deleteoldrdn, RfcLdapSuperDN newSuperior)
			: base(4)
		{
			base.add(entry);
			base.add(newrdn);
			base.add(deleteoldrdn);
			if (newSuperior != null)
			{
				newSuperior.setIdentifier(new Asn1Identifier(2, false, 0));
				base.add(newSuperior);
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000123AF File Offset: 0x000105AF
		internal RfcModifyDNRequest(Asn1Object[] origRequest, string base_Renamed)
			: base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(0, new RfcLdapDN(base_Renamed));
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x000123CB File Offset: 0x000105CB
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 12);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000123D6 File Offset: 0x000105D6
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcModifyDNRequest(base.toArray(), base_Renamed);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x000123E4 File Offset: 0x000105E4
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(0)).stringValue();
		}
	}
}
