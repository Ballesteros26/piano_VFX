using System;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200005E RID: 94
	public class RfcBindRequest : Asn1Sequence, RfcRequest
	{
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00010520 File Offset: 0x0000E720
		// (set) Token: 0x06000340 RID: 832 RVA: 0x0001052E File Offset: 0x0000E72E
		public virtual Asn1Integer Version
		{
			get
			{
				return (Asn1Integer)base.get_Renamed(0);
			}
			set
			{
				base.set_Renamed(0, value);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00010538 File Offset: 0x0000E738
		// (set) Token: 0x06000342 RID: 834 RVA: 0x00010546 File Offset: 0x0000E746
		public virtual RfcLdapDN Name
		{
			get
			{
				return (RfcLdapDN)base.get_Renamed(1);
			}
			set
			{
				base.set_Renamed(1, value);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00010550 File Offset: 0x0000E750
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0001055E File Offset: 0x0000E75E
		public virtual RfcAuthenticationChoice AuthenticationChoice
		{
			get
			{
				return (RfcAuthenticationChoice)base.get_Renamed(2);
			}
			set
			{
				base.set_Renamed(2, value);
			}
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00010568 File Offset: 0x0000E768
		public RfcBindRequest(Asn1Integer version, RfcLdapDN name, RfcAuthenticationChoice auth)
			: base(3)
		{
			base.add(version);
			base.add(name);
			base.add(auth);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00010586 File Offset: 0x0000E786
		[CLSCompliant(false)]
		public RfcBindRequest(int version, string dn, string mechanism, sbyte[] credentials)
			: this(new Asn1Integer(version), new RfcLdapDN(dn), new RfcAuthenticationChoice(mechanism, credentials))
		{
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000105A2 File Offset: 0x0000E7A2
		internal RfcBindRequest(Asn1Object[] origRequest, string base_Renamed)
			: base(origRequest, origRequest.Length)
		{
			if (base_Renamed != null)
			{
				base.set_Renamed(1, new RfcLdapDN(base_Renamed));
			}
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000105BE File Offset: 0x0000E7BE
		public override Asn1Identifier getIdentifier()
		{
			return RfcBindRequest.ID;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000105C5 File Offset: 0x0000E7C5
		public RfcRequest dupRequest(string base_Renamed, string filter, bool request)
		{
			return new RfcBindRequest(base.toArray(), base_Renamed);
		}

		// Token: 0x0600034A RID: 842 RVA: 0x000105D3 File Offset: 0x0000E7D3
		public string getRequestDN()
		{
			return ((RfcLdapDN)base.get_Renamed(1)).stringValue();
		}

		// Token: 0x0400022A RID: 554
		private static readonly Asn1Identifier ID = new Asn1Identifier(1, true, 0);
	}
}
