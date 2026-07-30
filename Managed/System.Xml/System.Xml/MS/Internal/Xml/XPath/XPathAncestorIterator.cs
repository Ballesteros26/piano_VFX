using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000049 RID: 73
	internal class XPathAncestorIterator : XPathAxisIterator
	{
		// Token: 0x06000205 RID: 517 RVA: 0x00007B2A File Offset: 0x00005D2A
		public XPathAncestorIterator(XPathNavigator nav, XPathNodeType type, bool matchSelf)
			: base(nav, type, matchSelf)
		{
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00007B35 File Offset: 0x00005D35
		public XPathAncestorIterator(XPathNavigator nav, string name, string namespaceURI, bool matchSelf)
			: base(nav, name, namespaceURI, matchSelf)
		{
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00007B42 File Offset: 0x00005D42
		public XPathAncestorIterator(XPathAncestorIterator other)
			: base(other)
		{
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00007B4C File Offset: 0x00005D4C
		public override bool MoveNext()
		{
			if (this.first)
			{
				this.first = false;
				if (this.matchSelf && this.Matches)
				{
					this.position = 1;
					return true;
				}
			}
			while (this.nav.MoveToParent())
			{
				if (this.Matches)
				{
					this.position++;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00007BA7 File Offset: 0x00005DA7
		public override XPathNodeIterator Clone()
		{
			return new XPathAncestorIterator(this);
		}
	}
}
