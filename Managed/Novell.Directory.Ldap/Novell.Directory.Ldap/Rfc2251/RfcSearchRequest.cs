using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200007C RID: 124
	public class RfcSearchRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x060003E5 RID: 997 RVA: 0x000124E0 File Offset: 0x000106E0
		public RfcSearchRequest(RfcLdapDN baseObject, Asn1Enumerated scope, Asn1Enumerated derefAliases, Asn1Integer sizeLimit, Asn1Integer timeLimit, Asn1Boolean typesOnly, RfcFilter filter, RfcAttributeDescriptionList attributes)
			: base(8)
		{
			base.add(baseObject);
			base.add(scope);
			base.add(derefAliases);
			base.add(sizeLimit);
			base.add(timeLimit);
			base.add(typesOnly);
			base.add(filter);
			base.add(attributes);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00012534 File Offset: 0x00010734
		internal RfcSearchRequest(Asn1Object[] origRequest, string base_Renamed, string filter, bool request)
			: base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(0, new RfcLdapDN(base_Renamed));
			}
			if (request && ((Asn1Enumerated)origRequest[1]).intValue() == 1)
			{
				base.set_Renamed(1, new Asn1Enumerated(0));
			}
			if (filter != null)
			{
				base.set_Renamed(6, new RfcFilter(filter));
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0001258C File Offset: 0x0001078C
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 3);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00012596 File Offset: 0x00010796
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcSearchRequest(base.toArray(), base_Renamed, filter, request);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000125A6 File Offset: 0x000107A6
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(0)).stringValue();
		}
	}
}
