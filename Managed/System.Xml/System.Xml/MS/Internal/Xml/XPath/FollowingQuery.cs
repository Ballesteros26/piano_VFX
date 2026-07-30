using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000020 RID: 32
	internal sealed class FollowingQuery : BaseAxisQuery
	{
		// Token: 0x060000CA RID: 202 RVA: 0x00002105 File Offset: 0x00000305
		public FollowingQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest)
			: base(qyInput, name, prefix, typeTest)
		{
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003CBC File Offset: 0x00001EBC
		private FollowingQuery(FollowingQuery other)
			: base(other)
		{
			this.input = Query.Clone(other.input);
			this.iterator = Query.Clone(other.iterator);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003CE7 File Offset: 0x00001EE7
		public override void Reset()
		{
			this.iterator = null;
			base.Reset();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003CF8 File Offset: 0x00001EF8
		public override XPathNavigator Advance()
		{
			if (this.iterator == null)
			{
				this.input = this.qyInput.Advance();
				if (this.input == null)
				{
					return null;
				}
				XPathNavigator xpathNavigator;
				do
				{
					xpathNavigator = this.input.Clone();
					this.input = this.qyInput.Advance();
				}
				while (xpathNavigator.IsDescendant(this.input));
				this.input = xpathNavigator;
				this.iterator = XPathEmptyIterator.Instance;
			}
			while (!this.iterator.MoveNext())
			{
				bool flag;
				if (this.input.NodeType == XPathNodeType.Attribute || this.input.NodeType == XPathNodeType.Namespace)
				{
					this.input.MoveToParent();
					flag = false;
				}
				else
				{
					while (!this.input.MoveToNext())
					{
						if (!this.input.MoveToParent())
						{
							return null;
						}
					}
					flag = true;
				}
				if (base.NameTest)
				{
					this.iterator = this.input.SelectDescendants(base.Name, base.Namespace, flag);
				}
				else
				{
					this.iterator = this.input.SelectDescendants(base.TypeTest, flag);
				}
			}
			this.position++;
			this.currentNode = this.iterator.Current;
			return this.currentNode;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003E2C File Offset: 0x0000202C
		public override XPathNodeIterator Clone()
		{
			return new FollowingQuery(this);
		}

		// Token: 0x04000084 RID: 132
		private XPathNavigator input;

		// Token: 0x04000085 RID: 133
		private XPathNodeIterator iterator;
	}
}
