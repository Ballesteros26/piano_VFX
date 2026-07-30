using System;
using System.Text;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200001D RID: 29
	public class LdapEntry : IComparable
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000077BE File Offset: 0x000059BE
		[CLSCompliant(false)]
		public virtual string DN
		{
			get
			{
				return this.dn;
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x000077C6 File Offset: 0x000059C6
		public LdapEntry()
			: this(null, null)
		{
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000077D0 File Offset: 0x000059D0
		public LdapEntry(string dn)
			: this(dn, null)
		{
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000077DA File Offset: 0x000059DA
		public LdapEntry(string dn, LdapAttributeSet attrs)
		{
			if (dn == null)
			{
				dn = "";
			}
			if (attrs == null)
			{
				attrs = new LdapAttributeSet();
			}
			this.dn = dn;
			this.attrs = attrs;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00007804 File Offset: 0x00005A04
		public virtual LdapAttribute getAttribute(string attrName)
		{
			return this.attrs.getAttribute(attrName);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00007812 File Offset: 0x00005A12
		public virtual LdapAttributeSet getAttributeSet()
		{
			return this.attrs;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000781A File Offset: 0x00005A1A
		public virtual LdapAttributeSet getAttributeSet(string subtype)
		{
			return this.attrs.getSubset(subtype);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00007828 File Offset: 0x00005A28
		public virtual int CompareTo(object entry)
		{
			return LdapDN.normalize(this.dn).CompareTo(LdapDN.normalize(((LdapEntry)entry).dn));
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000784C File Offset: 0x00005A4C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder("LdapEntry: ");
			if (this.dn != null)
			{
				stringBuilder.Append(this.dn + "; ");
			}
			if (this.attrs != null)
			{
				stringBuilder.Append(this.attrs.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040000B5 RID: 181
		protected internal string dn;

		// Token: 0x040000B6 RID: 182
		protected internal LdapAttributeSet attrs;
	}
}
