using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000060 RID: 96
	public class RfcCompareRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00010730 File Offset: 0x0000E930
		public virtual RfcAttributeValueAssertion AttributeValueAssertion
		{
			get
			{
				return (RfcAttributeValueAssertion)base.get_Renamed(1);
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001073E File Offset: 0x0000E93E
		public RfcCompareRequest(RfcLdapDN entry, RfcAttributeValueAssertion ava)
			: base(2)
		{
			base.add(entry);
			base.add(ava);
			if (ava.AssertionValue == null)
			{
				throw new ArgumentException("compare: Attribute must have an assertion value");
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00010768 File Offset: 0x0000E968
		internal RfcCompareRequest(Asn1Object[] origRequest, string base_Renamed)
			: base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(0, new RfcLdapDN(base_Renamed));
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00010784 File Offset: 0x0000E984
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 14);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0001078F File Offset: 0x0000E98F
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcCompareRequest(base.toArray(), base_Renamed);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0001079D File Offset: 0x0000E99D
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(0)).stringValue();
		}
	}
}
