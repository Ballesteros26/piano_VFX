using System;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200001C RID: 28
	public class LdapDeleteRequest : LdapMessage
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00007793 File Offset: 0x00005993
		public virtual string DN
		{
			get
			{
				return this.Asn1Object.RequestDN;
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x000077A0 File Offset: 0x000059A0
		public LdapDeleteRequest(string dn, LdapControl[] cont)
			: base(10, new RfcDelRequest(dn), cont)
		{
		}

		// Token: 0x06000153 RID: 339 RVA: 0x000077B1 File Offset: 0x000059B1
		public override string ToString()
		{
			return this.Asn1Object.ToString();
		}
	}
}
