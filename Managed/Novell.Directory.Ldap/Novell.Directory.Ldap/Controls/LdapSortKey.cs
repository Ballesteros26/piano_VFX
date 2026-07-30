using System;

namespace Novell.Directory.Ldap.Controls
{
	// Token: 0x020000CA RID: 202
	public class LdapSortKey
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x0001687C File Offset: 0x00014A7C
		public virtual string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00016884 File Offset: 0x00014A84
		public virtual bool Reverse
		{
			get
			{
				return this.reverse;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x0001688C File Offset: 0x00014A8C
		public virtual string MatchRule
		{
			get
			{
				return this.matchRule;
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00016894 File Offset: 0x00014A94
		public LdapSortKey(string keyDescription)
		{
			this.matchRule = null;
			this.reverse = false;
			string text = keyDescription;
			if (text[0] == '-')
			{
				text = text.Substring(1);
				this.reverse = true;
			}
			int num = text.IndexOf(":");
			if (num != -1)
			{
				this.key = text.Substring(0, num);
				this.matchRule = text.Substring(num + 1);
				return;
			}
			this.key = text;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x00016906 File Offset: 0x00014B06
		public LdapSortKey(string key, bool reverse)
			: this(key, reverse, null)
		{
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00016911 File Offset: 0x00014B11
		public LdapSortKey(string key, bool reverse, string matchRule)
		{
			this.key = key;
			this.reverse = reverse;
			this.matchRule = matchRule;
		}

		// Token: 0x04000485 RID: 1157
		private string key;

		// Token: 0x04000486 RID: 1158
		private bool reverse;

		// Token: 0x04000487 RID: 1159
		private string matchRule;
	}
}
