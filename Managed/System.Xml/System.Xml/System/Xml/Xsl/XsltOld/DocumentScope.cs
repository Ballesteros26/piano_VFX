using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000519 RID: 1305
	internal class DocumentScope
	{
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x060034AD RID: 13485 RVA: 0x00129DDF File Offset: 0x00127FDF
		internal NamespaceDecl Scopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x060034AE RID: 13486 RVA: 0x00129DE7 File Offset: 0x00127FE7
		internal NamespaceDecl AddNamespace(string prefix, string uri, string prevDefaultNsUri)
		{
			this.scopes = new NamespaceDecl(prefix, uri, prevDefaultNsUri, this.scopes);
			return this.scopes;
		}

		// Token: 0x060034AF RID: 13487 RVA: 0x00129E04 File Offset: 0x00128004
		internal string ResolveAtom(string prefix)
		{
			for (NamespaceDecl next = this.scopes; next != null; next = next.Next)
			{
				if (Ref.Equal(next.Prefix, prefix))
				{
					return next.Uri;
				}
			}
			return null;
		}

		// Token: 0x060034B0 RID: 13488 RVA: 0x00129E3C File Offset: 0x0012803C
		internal string ResolveNonAtom(string prefix)
		{
			for (NamespaceDecl next = this.scopes; next != null; next = next.Next)
			{
				if (next.Prefix == prefix)
				{
					return next.Uri;
				}
			}
			return null;
		}

		// Token: 0x040021A6 RID: 8614
		protected NamespaceDecl scopes;
	}
}
