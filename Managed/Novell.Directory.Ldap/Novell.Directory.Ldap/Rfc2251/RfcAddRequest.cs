using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000054 RID: 84
	public class RfcAddRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000328 RID: 808 RVA: 0x00010389 File Offset: 0x0000E589
		public virtual RfcAttributeList Attributes
		{
			get
			{
				return (RfcAttributeList)base.get_Renamed(1);
			}
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00010397 File Offset: 0x0000E597
		public RfcAddRequest(RfcLdapDN entry, RfcAttributeList attributes)
			: base(2)
		{
			base.add(entry);
			base.add(attributes);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x000103AE File Offset: 0x0000E5AE
		internal RfcAddRequest(Asn1Object[] origRequest, string base_Renamed)
			: base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(0, new RfcLdapDN(base_Renamed));
			}
		}

		// Token: 0x0600032B RID: 811 RVA: 0x000103CA File Offset: 0x0000E5CA
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 8);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000103D4 File Offset: 0x0000E5D4
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcAddRequest(base.toArray(), base_Renamed);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000103E2 File Offset: 0x0000E5E2
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(0)).stringValue();
		}
	}
}
