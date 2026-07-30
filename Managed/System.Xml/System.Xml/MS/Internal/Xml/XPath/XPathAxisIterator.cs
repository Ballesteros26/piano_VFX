using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200004C RID: 76
	internal abstract class XPathAxisIterator : XPathNodeIterator
	{
		// Token: 0x0600021D RID: 541 RVA: 0x00007E56 File Offset: 0x00006056
		public XPathAxisIterator(XPathNavigator nav, bool matchSelf)
		{
			this.nav = nav;
			this.matchSelf = matchSelf;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00007E73 File Offset: 0x00006073
		public XPathAxisIterator(XPathNavigator nav, XPathNodeType type, bool matchSelf)
			: this(nav, matchSelf)
		{
			this.type = type;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00007E84 File Offset: 0x00006084
		public XPathAxisIterator(XPathNavigator nav, string name, string namespaceURI, bool matchSelf)
			: this(nav, matchSelf)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (namespaceURI == null)
			{
				throw new ArgumentNullException("namespaceURI");
			}
			this.name = name;
			this.uri = namespaceURI;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00007EBC File Offset: 0x000060BC
		public XPathAxisIterator(XPathAxisIterator it)
		{
			this.nav = it.nav.Clone();
			this.type = it.type;
			this.name = it.name;
			this.uri = it.uri;
			this.position = it.position;
			this.matchSelf = it.matchSelf;
			this.first = it.first;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00007F2F File Offset: 0x0000612F
		public override XPathNavigator Current
		{
			get
			{
				return this.nav;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00007F37 File Offset: 0x00006137
		public override int CurrentPosition
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00007F40 File Offset: 0x00006140
		protected virtual bool Matches
		{
			get
			{
				if (this.name == null)
				{
					return this.type == this.nav.NodeType || this.type == XPathNodeType.All || (this.type == XPathNodeType.Text && (this.nav.NodeType == XPathNodeType.Whitespace || this.nav.NodeType == XPathNodeType.SignificantWhitespace));
				}
				return this.nav.NodeType == XPathNodeType.Element && (this.name.Length == 0 || this.name == this.nav.LocalName) && this.uri == this.nav.NamespaceURI;
			}
		}

		// Token: 0x04000110 RID: 272
		internal XPathNavigator nav;

		// Token: 0x04000111 RID: 273
		internal XPathNodeType type;

		// Token: 0x04000112 RID: 274
		internal string name;

		// Token: 0x04000113 RID: 275
		internal string uri;

		// Token: 0x04000114 RID: 276
		internal int position;

		// Token: 0x04000115 RID: 277
		internal bool matchSelf;

		// Token: 0x04000116 RID: 278
		internal bool first = true;
	}
}
