using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000019 RID: 25
	internal class DescendantQuery : DescendantBaseQuery
	{
		// Token: 0x06000095 RID: 149 RVA: 0x0000339E File Offset: 0x0000159E
		internal DescendantQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type, bool matchSelf, bool abbrAxis)
			: base(qyParent, Name, Prefix, Type, matchSelf, abbrAxis)
		{
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000033AF File Offset: 0x000015AF
		public DescendantQuery(DescendantQuery other)
			: base(other)
		{
			this.nodeIterator = Query.Clone(other.nodeIterator);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000033C9 File Offset: 0x000015C9
		public override void Reset()
		{
			this.nodeIterator = null;
			base.Reset();
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000033D8 File Offset: 0x000015D8
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (this.nodeIterator == null)
				{
					this.position = 0;
					XPathNavigator xpathNavigator = this.qyInput.Advance();
					if (xpathNavigator == null)
					{
						break;
					}
					if (base.NameTest)
					{
						if (base.TypeTest == XPathNodeType.ProcessingInstruction)
						{
							this.nodeIterator = new IteratorFilter(xpathNavigator.SelectDescendants(base.TypeTest, this.matchSelf), base.Name);
						}
						else
						{
							this.nodeIterator = xpathNavigator.SelectDescendants(base.Name, base.Namespace, this.matchSelf);
						}
					}
					else
					{
						this.nodeIterator = xpathNavigator.SelectDescendants(base.TypeTest, this.matchSelf);
					}
				}
				if (this.nodeIterator.MoveNext())
				{
					goto Block_4;
				}
				this.nodeIterator = null;
			}
			return null;
			Block_4:
			this.position++;
			this.currentNode = this.nodeIterator.Current;
			return this.currentNode;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000034B4 File Offset: 0x000016B4
		public override XPathNodeIterator Clone()
		{
			return new DescendantQuery(this);
		}

		// Token: 0x0400007A RID: 122
		private XPathNodeIterator nodeIterator;
	}
}
