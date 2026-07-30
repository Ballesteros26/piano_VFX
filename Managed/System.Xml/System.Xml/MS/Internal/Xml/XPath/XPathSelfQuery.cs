using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000056 RID: 86
	internal sealed class XPathSelfQuery : BaseAxisQuery
	{
		// Token: 0x06000283 RID: 643 RVA: 0x00002105 File Offset: 0x00000305
		public XPathSelfQuery(Query qyInput, string Name, string Prefix, XPathNodeType Type)
			: base(qyInput, Name, Prefix, Type)
		{
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000044BC File Offset: 0x000026BC
		private XPathSelfQuery(XPathSelfQuery other)
			: base(other)
		{
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00009E2C File Offset: 0x0000802C
		public override XPathNavigator Advance()
		{
			while ((this.currentNode = this.qyInput.Advance()) != null)
			{
				if (this.matches(this.currentNode))
				{
					this.position = 1;
					return this.currentNode;
				}
			}
			return null;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00009E6E File Offset: 0x0000806E
		public override XPathNodeIterator Clone()
		{
			return new XPathSelfQuery(this);
		}
	}
}
