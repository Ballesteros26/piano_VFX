using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000075 RID: 117
	public class RfcModifyRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0001241A File Offset: 0x0001061A
		public virtual Asn1SequenceOf Modifications
		{
			get
			{
				return (Asn1SequenceOf)base.get_Renamed(1);
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00012428 File Offset: 0x00010628
		public RfcModifyRequest(RfcLdapDN object_Renamed, Asn1SequenceOf modification)
			: base(2)
		{
			base.add(object_Renamed);
			base.add(modification);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0001243F File Offset: 0x0001063F
		internal RfcModifyRequest(Asn1Object[] origRequest, string base_Renamed)
			: base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(0, new RfcLdapDN(base_Renamed));
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001245B File Offset: 0x0001065B
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(1, true, 6);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00012465 File Offset: 0x00010665
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcModifyRequest(base.toArray(), base_Renamed);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00012473 File Offset: 0x00010673
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(0)).stringValue();
		}
	}
}
