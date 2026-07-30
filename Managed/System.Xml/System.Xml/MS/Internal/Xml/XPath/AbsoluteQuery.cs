using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000006 RID: 6
	internal sealed class AbsoluteQuery : ContextQuery
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000020AE File Offset: 0x000002AE
		public AbsoluteQuery()
		{
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020B6 File Offset: 0x000002B6
		private AbsoluteQuery(AbsoluteQuery other)
			: base(other)
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020BF File Offset: 0x000002BF
		public override object Evaluate(XPathNodeIterator context)
		{
			this.contextNode = context.Current.Clone();
			this.contextNode.MoveToRoot();
			this.count = 0;
			return this;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020E5 File Offset: 0x000002E5
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			if (context != null && context.NodeType == XPathNodeType.Root)
			{
				return context;
			}
			return null;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020F5 File Offset: 0x000002F5
		public override XPathNodeIterator Clone()
		{
			return new AbsoluteQuery(this);
		}
	}
}
