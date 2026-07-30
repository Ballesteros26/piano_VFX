using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000009 RID: 9
	internal sealed class AttributeQuery : BaseAxisQuery
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002105 File Offset: 0x00000305
		public AttributeQuery(Query qyParent, string Name, string Prefix, XPathNodeType Type)
			: base(qyParent, Name, Prefix, Type)
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002112 File Offset: 0x00000312
		private AttributeQuery(AttributeQuery other)
			: base(other)
		{
			this.onAttribute = other.onAttribute;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002127 File Offset: 0x00000327
		public override void Reset()
		{
			this.onAttribute = false;
			base.Reset();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002138 File Offset: 0x00000338
		public override XPathNavigator Advance()
		{
			for (;;)
			{
				if (!this.onAttribute)
				{
					this.currentNode = this.qyInput.Advance();
					if (this.currentNode == null)
					{
						break;
					}
					this.position = 0;
					this.currentNode = this.currentNode.Clone();
					this.onAttribute = this.currentNode.MoveToFirstAttribute();
				}
				else
				{
					this.onAttribute = this.currentNode.MoveToNextAttribute();
				}
				if (this.onAttribute && this.matches(this.currentNode))
				{
					goto Block_3;
				}
			}
			return null;
			Block_3:
			this.position++;
			return this.currentNode;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021D0 File Offset: 0x000003D0
		public override XPathNavigator MatchNode(XPathNavigator context)
		{
			if (context != null && context.NodeType == XPathNodeType.Attribute && this.matches(context))
			{
				XPathNavigator xpathNavigator = context.Clone();
				if (xpathNavigator.MoveToParent())
				{
					return this.qyInput.MatchNode(xpathNavigator);
				}
			}
			return null;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000220F File Offset: 0x0000040F
		public override XPathNodeIterator Clone()
		{
			return new AttributeQuery(this);
		}

		// Token: 0x04000043 RID: 67
		private bool onAttribute;
	}
}
