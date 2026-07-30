using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200004D RID: 77
	internal class XPathChildIterator : XPathAxisIterator
	{
		// Token: 0x06000224 RID: 548 RVA: 0x00007FE9 File Offset: 0x000061E9
		public XPathChildIterator(XPathNavigator nav, XPathNodeType type)
			: base(nav, type, false)
		{
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00007FF4 File Offset: 0x000061F4
		public XPathChildIterator(XPathNavigator nav, string name, string namespaceURI)
			: base(nav, name, namespaceURI, false)
		{
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00007B42 File Offset: 0x00005D42
		public XPathChildIterator(XPathChildIterator it)
			: base(it)
		{
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00008000 File Offset: 0x00006200
		public override XPathNodeIterator Clone()
		{
			return new XPathChildIterator(this);
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00008008 File Offset: 0x00006208
		public override bool MoveNext()
		{
			while (this.first ? this.nav.MoveToFirstChild() : this.nav.MoveToNext())
			{
				this.first = false;
				if (this.Matches)
				{
					this.position++;
					return true;
				}
			}
			return false;
		}
	}
}
