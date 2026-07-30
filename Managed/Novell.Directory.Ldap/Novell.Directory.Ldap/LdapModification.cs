using System;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000028 RID: 40
	public class LdapModification
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000087E7 File Offset: 0x000069E7
		public virtual LdapAttribute Attribute
		{
			get
			{
				return this.attr;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001AE RID: 430 RVA: 0x000087EF File Offset: 0x000069EF
		public virtual int Op
		{
			get
			{
				return this.op;
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000087F7 File Offset: 0x000069F7
		public LdapModification(int op, LdapAttribute attr)
		{
			this.op = op;
			this.attr = attr;
		}

		// Token: 0x0400011F RID: 287
		private int op;

		// Token: 0x04000120 RID: 288
		private LdapAttribute attr;

		// Token: 0x04000121 RID: 289
		public const int ADD = 0;

		// Token: 0x04000122 RID: 290
		public const int DELETE = 1;

		// Token: 0x04000123 RID: 291
		public const int REPLACE = 2;
	}
}
