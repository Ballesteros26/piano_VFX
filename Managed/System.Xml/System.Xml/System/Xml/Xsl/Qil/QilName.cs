using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000638 RID: 1592
	internal class QilName : QilLiteral
	{
		// Token: 0x06003EBB RID: 16059 RVA: 0x00157BCD File Offset: 0x00155DCD
		public QilName(QilNodeType nodeType, string local, string uri, string prefix)
			: base(nodeType, null)
		{
			this.LocalName = local;
			this.NamespaceUri = uri;
			this.Prefix = prefix;
			base.Value = this;
		}

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06003EBC RID: 16060 RVA: 0x00157BF4 File Offset: 0x00155DF4
		// (set) Token: 0x06003EBD RID: 16061 RVA: 0x00157BFC File Offset: 0x00155DFC
		public string LocalName
		{
			get
			{
				return this.local;
			}
			set
			{
				this.local = value;
			}
		}

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06003EBE RID: 16062 RVA: 0x00157C05 File Offset: 0x00155E05
		// (set) Token: 0x06003EBF RID: 16063 RVA: 0x00157C0D File Offset: 0x00155E0D
		public string NamespaceUri
		{
			get
			{
				return this.uri;
			}
			set
			{
				this.uri = value;
			}
		}

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06003EC0 RID: 16064 RVA: 0x00157C16 File Offset: 0x00155E16
		// (set) Token: 0x06003EC1 RID: 16065 RVA: 0x00157C1E File Offset: 0x00155E1E
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
			set
			{
				this.prefix = value;
			}
		}

		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06003EC2 RID: 16066 RVA: 0x00157C27 File Offset: 0x00155E27
		public string QualifiedName
		{
			get
			{
				if (this.prefix.Length == 0)
				{
					return this.local;
				}
				return this.prefix + ":" + this.local;
			}
		}

		// Token: 0x06003EC3 RID: 16067 RVA: 0x00157C53 File Offset: 0x00155E53
		public override int GetHashCode()
		{
			return this.local.GetHashCode();
		}

		// Token: 0x06003EC4 RID: 16068 RVA: 0x00157C60 File Offset: 0x00155E60
		public override bool Equals(object other)
		{
			QilName qilName = other as QilName;
			return !(qilName == null) && this.local == qilName.local && this.uri == qilName.uri;
		}

		// Token: 0x06003EC5 RID: 16069 RVA: 0x00157CA5 File Offset: 0x00155EA5
		public static bool operator ==(QilName a, QilName b)
		{
			return a == b || (a != null && b != null && a.local == b.local && a.uri == b.uri);
		}

		// Token: 0x06003EC6 RID: 16070 RVA: 0x00157CDB File Offset: 0x00155EDB
		public static bool operator !=(QilName a, QilName b)
		{
			return !(a == b);
		}

		// Token: 0x06003EC7 RID: 16071 RVA: 0x00157CE8 File Offset: 0x00155EE8
		public override string ToString()
		{
			if (this.prefix.Length != 0)
			{
				return string.Concat(new string[] { "{", this.uri, "}", this.prefix, ":", this.local });
			}
			if (this.uri.Length == 0)
			{
				return this.local;
			}
			return "{" + this.uri + "}" + this.local;
		}

		// Token: 0x0400284A RID: 10314
		private string local;

		// Token: 0x0400284B RID: 10315
		private string uri;

		// Token: 0x0400284C RID: 10316
		private string prefix;
	}
}
