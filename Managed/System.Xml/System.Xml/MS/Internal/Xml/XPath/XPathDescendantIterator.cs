using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200004E RID: 78
	internal class XPathDescendantIterator : XPathAxisIterator
	{
		// Token: 0x06000229 RID: 553 RVA: 0x00007B2A File Offset: 0x00005D2A
		public XPathDescendantIterator(XPathNavigator nav, XPathNodeType type, bool matchSelf)
			: base(nav, type, matchSelf)
		{
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00007B35 File Offset: 0x00005D35
		public XPathDescendantIterator(XPathNavigator nav, string name, string namespaceURI, bool matchSelf)
			: base(nav, name, namespaceURI, matchSelf)
		{
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00008059 File Offset: 0x00006259
		public XPathDescendantIterator(XPathDescendantIterator it)
			: base(it)
		{
			this.level = it.level;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000806E File Offset: 0x0000626E
		public override XPathNodeIterator Clone()
		{
			return new XPathDescendantIterator(this);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00008078 File Offset: 0x00006278
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
			for (;;)
			{
				if (!this.nav.MoveToFirstChild())
				{
					while (this.level != 0)
					{
						if (this.nav.MoveToNext())
						{
							goto IL_0078;
						}
						this.nav.MoveToParent();
						this.level--;
					}
					break;
				}
				this.level++;
				IL_0078:
				if (this.Matches)
				{
					goto Block_7;
				}
			}
			return false;
			Block_7:
			this.position++;
			return true;
		}

		// Token: 0x04000117 RID: 279
		private int level;
	}
}
