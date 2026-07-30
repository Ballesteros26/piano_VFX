using System;

namespace System.Xml.Xsl
{
	// Token: 0x020004CA RID: 1226
	internal class XmlQualifiedNameTest : XmlQualifiedName
	{
		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x060031B1 RID: 12721 RVA: 0x00120819 File Offset: 0x0011EA19
		public static XmlQualifiedNameTest Wildcard
		{
			get
			{
				return XmlQualifiedNameTest.wc;
			}
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x00120820 File Offset: 0x0011EA20
		private XmlQualifiedNameTest(string name, string ns, bool exclude)
			: base(name, ns)
		{
			this.exclude = exclude;
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x00120831 File Offset: 0x0011EA31
		public static XmlQualifiedNameTest New(string name, string ns)
		{
			if (ns == null && name == null)
			{
				return XmlQualifiedNameTest.Wildcard;
			}
			return new XmlQualifiedNameTest((name == null) ? "*" : name, (ns == null) ? "*" : ns, false);
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x060031B4 RID: 12724 RVA: 0x0012085B File Offset: 0x0011EA5B
		public bool IsWildcard
		{
			get
			{
				return this == XmlQualifiedNameTest.Wildcard;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x060031B5 RID: 12725 RVA: 0x00120865 File Offset: 0x0011EA65
		public bool IsNameWildcard
		{
			get
			{
				return base.Name == "*";
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x060031B6 RID: 12726 RVA: 0x00120874 File Offset: 0x0011EA74
		public bool IsNamespaceWildcard
		{
			get
			{
				return base.Namespace == "*";
			}
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x00120883 File Offset: 0x0011EA83
		private bool IsNameSubsetOf(XmlQualifiedNameTest other)
		{
			return other.IsNameWildcard || base.Name == other.Name;
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x001208A0 File Offset: 0x0011EAA0
		private bool IsNamespaceSubsetOf(XmlQualifiedNameTest other)
		{
			return other.IsNamespaceWildcard || (this.exclude == other.exclude && base.Namespace == other.Namespace) || (other.exclude && !this.exclude && base.Namespace != other.Namespace);
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x001208FB File Offset: 0x0011EAFB
		public bool IsSubsetOf(XmlQualifiedNameTest other)
		{
			return this.IsNameSubsetOf(other) && this.IsNamespaceSubsetOf(other);
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x0012090F File Offset: 0x0011EB0F
		public bool HasIntersection(XmlQualifiedNameTest other)
		{
			return (this.IsNamespaceSubsetOf(other) || other.IsNamespaceSubsetOf(this)) && (this.IsNameSubsetOf(other) || other.IsNameSubsetOf(this));
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x00120938 File Offset: 0x0011EB38
		public override string ToString()
		{
			if (this == XmlQualifiedNameTest.Wildcard)
			{
				return "*";
			}
			if (base.Namespace.Length == 0)
			{
				return base.Name;
			}
			if (base.Namespace == "*")
			{
				return "*:" + base.Name;
			}
			if (this.exclude)
			{
				return "{~" + base.Namespace + "}:" + base.Name;
			}
			return "{" + base.Namespace + "}:" + base.Name;
		}

		// Token: 0x04002068 RID: 8296
		private bool exclude;

		// Token: 0x04002069 RID: 8297
		private const string wildcard = "*";

		// Token: 0x0400206A RID: 8298
		private static XmlQualifiedNameTest wc = XmlQualifiedNameTest.New("*", "*");
	}
}
