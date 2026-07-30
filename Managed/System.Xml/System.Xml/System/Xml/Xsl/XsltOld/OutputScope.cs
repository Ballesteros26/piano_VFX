using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200052E RID: 1326
	internal class OutputScope : DocumentScope
	{
		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x0600354D RID: 13645 RVA: 0x0012C7CA File Offset: 0x0012A9CA
		internal string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x0600354E RID: 13646 RVA: 0x0012C7D2 File Offset: 0x0012A9D2
		internal string Namespace
		{
			get
			{
				return this.nsUri;
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x0600354F RID: 13647 RVA: 0x0012C7DA File Offset: 0x0012A9DA
		// (set) Token: 0x06003550 RID: 13648 RVA: 0x0012C7E2 File Offset: 0x0012A9E2
		internal string Prefix
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

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06003551 RID: 13649 RVA: 0x0012C7EB File Offset: 0x0012A9EB
		// (set) Token: 0x06003552 RID: 13650 RVA: 0x0012C7F3 File Offset: 0x0012A9F3
		internal XmlSpace Space
		{
			get
			{
				return this.space;
			}
			set
			{
				this.space = value;
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06003553 RID: 13651 RVA: 0x0012C7FC File Offset: 0x0012A9FC
		// (set) Token: 0x06003554 RID: 13652 RVA: 0x0012C804 File Offset: 0x0012AA04
		internal string Lang
		{
			get
			{
				return this.lang;
			}
			set
			{
				this.lang = value;
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06003555 RID: 13653 RVA: 0x0012C80D File Offset: 0x0012AA0D
		// (set) Token: 0x06003556 RID: 13654 RVA: 0x0012C815 File Offset: 0x0012AA15
		internal bool Mixed
		{
			get
			{
				return this.mixed;
			}
			set
			{
				this.mixed = value;
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06003557 RID: 13655 RVA: 0x0012C81E File Offset: 0x0012AA1E
		// (set) Token: 0x06003558 RID: 13656 RVA: 0x0012C826 File Offset: 0x0012AA26
		internal bool ToCData
		{
			get
			{
				return this.toCData;
			}
			set
			{
				this.toCData = value;
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06003559 RID: 13657 RVA: 0x0012C82F File Offset: 0x0012AA2F
		// (set) Token: 0x0600355A RID: 13658 RVA: 0x0012C837 File Offset: 0x0012AA37
		internal HtmlElementProps HtmlElementProps
		{
			get
			{
				return this.htmlElementProps;
			}
			set
			{
				this.htmlElementProps = value;
			}
		}

		// Token: 0x0600355B RID: 13659 RVA: 0x0012C840 File Offset: 0x0012AA40
		internal OutputScope()
		{
			this.Init(string.Empty, string.Empty, string.Empty, XmlSpace.None, string.Empty, false);
		}

		// Token: 0x0600355C RID: 13660 RVA: 0x0012C864 File Offset: 0x0012AA64
		internal void Init(string name, string nspace, string prefix, XmlSpace space, string lang, bool mixed)
		{
			this.scopes = null;
			this.name = name;
			this.nsUri = nspace;
			this.prefix = prefix;
			this.space = space;
			this.lang = lang;
			this.mixed = mixed;
			this.toCData = false;
			this.htmlElementProps = null;
		}

		// Token: 0x0600355D RID: 13661 RVA: 0x0012C8B4 File Offset: 0x0012AAB4
		internal bool FindPrefix(string urn, out string prefix)
		{
			for (NamespaceDecl namespaceDecl = this.scopes; namespaceDecl != null; namespaceDecl = namespaceDecl.Next)
			{
				if (Ref.Equal(namespaceDecl.Uri, urn) && namespaceDecl.Prefix != null && namespaceDecl.Prefix.Length > 0)
				{
					prefix = namespaceDecl.Prefix;
					return true;
				}
			}
			prefix = string.Empty;
			return false;
		}

		// Token: 0x0400220D RID: 8717
		private string name;

		// Token: 0x0400220E RID: 8718
		private string nsUri;

		// Token: 0x0400220F RID: 8719
		private string prefix;

		// Token: 0x04002210 RID: 8720
		private XmlSpace space;

		// Token: 0x04002211 RID: 8721
		private string lang;

		// Token: 0x04002212 RID: 8722
		private bool mixed;

		// Token: 0x04002213 RID: 8723
		private bool toCData;

		// Token: 0x04002214 RID: 8724
		private HtmlElementProps htmlElementProps;
	}
}
