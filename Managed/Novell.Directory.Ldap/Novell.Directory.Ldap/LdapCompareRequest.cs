using System;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000014 RID: 20
	public class LdapCompareRequest : LdapMessage
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004DAB File Offset: 0x00002FAB
		public virtual string AttributeDescription
		{
			get
			{
				return ((RfcCompareRequest)this.Asn1Object.getRequest()).AttributeValueAssertion.AttributeDescription;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00004DC7 File Offset: 0x00002FC7
		[CLSCompliant(false)]
		public virtual sbyte[] AssertionValue
		{
			get
			{
				return ((RfcCompareRequest)this.Asn1Object.getRequest()).AttributeValueAssertion.AssertionValue;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004DE3 File Offset: 0x00002FE3
		public virtual string DN
		{
			get
			{
				return this.Asn1Object.RequestDN;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004DF0 File Offset: 0x00002FF0
		[CLSCompliant(false)]
		public LdapCompareRequest(string dn, string name, sbyte[] value_Renamed, LdapControl[] cont)
			: base(14, new RfcCompareRequest(new RfcLdapDN(dn), new RfcAttributeValueAssertion(new RfcAttributeDescription(name), new RfcAssertionValue(value_Renamed))), cont)
		{
		}
	}
}
