using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000010 RID: 16
	public class LdapAuthProvider
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004A5F File Offset: 0x00002C5F
		public virtual string DN
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004A67 File Offset: 0x00002C67
		[CLSCompliant(false)]
		public virtual sbyte[] Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004A6F File Offset: 0x00002C6F
		[CLSCompliant(false)]
		public LdapAuthProvider(string dn, sbyte[] password)
		{
			this.dn = dn;
			this.password = password;
		}

		// Token: 0x0400006E RID: 110
		private string dn;

		// Token: 0x0400006F RID: 111
		private sbyte[] password;
	}
}
