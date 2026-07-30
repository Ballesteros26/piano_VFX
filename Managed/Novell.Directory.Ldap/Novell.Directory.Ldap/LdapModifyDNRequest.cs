using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000029 RID: 41
	public class LdapModifyDNRequest : LdapMessage
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0000880D File Offset: 0x00006A0D
		public virtual string DN
		{
			get
			{
				return this.Asn1Object.RequestDN;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x0000881A File Offset: 0x00006A1A
		public virtual string NewRDN
		{
			get
			{
				return ((RfcRelativeLdapDN)((RfcModifyDNRequest)this.Asn1Object.getRequest()).toArray()[1]).stringValue();
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000883D File Offset: 0x00006A3D
		public virtual bool DeleteOldRDN
		{
			get
			{
				return ((Asn1Boolean)((RfcModifyDNRequest)this.Asn1Object.getRequest()).toArray()[2]).booleanValue();
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00008860 File Offset: 0x00006A60
		public virtual string ParentDN
		{
			get
			{
				RfcModifyDNRequest rfcModifyDNRequest = (RfcModifyDNRequest)this.Asn1Object.getRequest();
				Asn1Object[] array = rfcModifyDNRequest.toArray();
				if (array.Length < 4 || array[3] == null)
				{
					return null;
				}
				return ((RfcLdapDN)rfcModifyDNRequest.toArray()[3]).stringValue();
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000088A4 File Offset: 0x00006AA4
		public LdapModifyDNRequest(string dn, string newRdn, string newParentdn, bool deleteOldRdn, LdapControl[] cont)
			: base(12, new RfcModifyDNRequest(new RfcLdapDN(dn), new RfcRelativeLdapDN(newRdn), new Asn1Boolean(deleteOldRdn), (newParentdn != null) ? new RfcLdapSuperDN(newParentdn) : null), cont)
		{
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000088D4 File Offset: 0x00006AD4
		public override string ToString()
		{
			return this.Asn1Object.ToString();
		}
	}
}
