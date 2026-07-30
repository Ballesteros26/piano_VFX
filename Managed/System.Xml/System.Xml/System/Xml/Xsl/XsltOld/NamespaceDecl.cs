using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000525 RID: 1317
	internal class NamespaceDecl
	{
		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x060034FF RID: 13567 RVA: 0x0012B270 File Offset: 0x00129470
		internal string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06003500 RID: 13568 RVA: 0x0012B278 File Offset: 0x00129478
		internal string Uri
		{
			get
			{
				return this.nsUri;
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06003501 RID: 13569 RVA: 0x0012B280 File Offset: 0x00129480
		internal string PrevDefaultNsUri
		{
			get
			{
				return this.prevDefaultNsUri;
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06003502 RID: 13570 RVA: 0x0012B288 File Offset: 0x00129488
		internal NamespaceDecl Next
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x0012B290 File Offset: 0x00129490
		internal NamespaceDecl(string prefix, string nsUri, string prevDefaultNsUri, NamespaceDecl next)
		{
			this.Init(prefix, nsUri, prevDefaultNsUri, next);
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x0012B2A3 File Offset: 0x001294A3
		internal void Init(string prefix, string nsUri, string prevDefaultNsUri, NamespaceDecl next)
		{
			this.prefix = prefix;
			this.nsUri = nsUri;
			this.prevDefaultNsUri = prevDefaultNsUri;
			this.next = next;
		}

		// Token: 0x040021D2 RID: 8658
		private string prefix;

		// Token: 0x040021D3 RID: 8659
		private string nsUri;

		// Token: 0x040021D4 RID: 8660
		private string prevDefaultNsUri;

		// Token: 0x040021D5 RID: 8661
		private NamespaceDecl next;
	}
}
