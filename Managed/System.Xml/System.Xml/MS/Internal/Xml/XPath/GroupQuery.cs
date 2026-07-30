using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000027 RID: 39
	internal sealed class GroupQuery : BaseAxisQuery
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x000044B3 File Offset: 0x000026B3
		public GroupQuery(Query qy)
			: base(qy)
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000044BC File Offset: 0x000026BC
		private GroupQuery(GroupQuery other)
			: base(other)
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000044C5 File Offset: 0x000026C5
		public override XPathNavigator Advance()
		{
			this.currentNode = this.qyInput.Advance();
			if (this.currentNode != null)
			{
				this.position++;
			}
			return this.currentNode;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000044F4 File Offset: 0x000026F4
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			return this.qyInput.Evaluate(nodeIterator);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004502 File Offset: 0x00002702
		public override XPathNodeIterator Clone()
		{
			return new GroupQuery(this);
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000450A File Offset: 0x0000270A
		public override XPathResultType StaticType
		{
			get
			{
				return this.qyInput.StaticType;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00003242 File Offset: 0x00001442
		public override QueryProps Properties
		{
			get
			{
				return QueryProps.Position;
			}
		}
	}
}
