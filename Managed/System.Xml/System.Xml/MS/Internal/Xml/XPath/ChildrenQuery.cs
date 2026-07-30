using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000012 RID: 18
	internal class ChildrenQuery : BaseAxisQuery
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00002D5F File Offset: 0x00000F5F
		public ChildrenQuery(Query qyInput, string name, string prefix, XPathNodeType type)
			: base(qyInput, name, prefix, type)
		{
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002D77 File Offset: 0x00000F77
		protected ChildrenQuery(ChildrenQuery other)
			: base(other)
		{
			this.iterator = Query.Clone(other.iterator);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00002D9C File Offset: 0x00000F9C
		public override void Reset()
		{
			this.iterator = XPathEmptyIterator.Instance;
			base.Reset();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public override XPathNavigator Advance()
		{
			while (!this.iterator.MoveNext())
			{
				XPathNavigator xpathNavigator = this.qyInput.Advance();
				if (xpathNavigator == null)
				{
					return null;
				}
				if (base.NameTest)
				{
					if (base.TypeTest == XPathNodeType.ProcessingInstruction)
					{
						this.iterator = new IteratorFilter(xpathNavigator.SelectChildren(base.TypeTest), base.Name);
					}
					else
					{
						this.iterator = xpathNavigator.SelectChildren(base.Name, base.Namespace);
					}
				}
				else
				{
					this.iterator = xpathNavigator.SelectChildren(base.TypeTest);
				}
				this.position = 0;
			}
			this.position++;
			this.currentNode = this.iterator.Current;
			return this.currentNode;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002E68 File Offset: 0x00001068
		public sealed override XPathNavigator MatchNode(XPathNavigator context)
		{
			if (context == null || !this.matches(context))
			{
				return null;
			}
			XPathNavigator xpathNavigator = context.Clone();
			if (xpathNavigator.NodeType != XPathNodeType.Attribute && xpathNavigator.MoveToParent())
			{
				return this.qyInput.MatchNode(xpathNavigator);
			}
			return null;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002EA9 File Offset: 0x000010A9
		public override XPathNodeIterator Clone()
		{
			return new ChildrenQuery(this);
		}

		// Token: 0x0400006E RID: 110
		private XPathNodeIterator iterator = XPathEmptyIterator.Instance;
	}
}
