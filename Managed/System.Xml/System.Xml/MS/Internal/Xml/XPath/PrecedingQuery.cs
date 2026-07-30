using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000037 RID: 55
	internal sealed class PrecedingQuery : BaseAxisQuery
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00005A25 File Offset: 0x00003C25
		public PrecedingQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest)
			: base(qyInput, name, prefix, typeTest)
		{
			this.ancestorStk = new ClonableStack<XPathNavigator>();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00005A3D File Offset: 0x00003C3D
		private PrecedingQuery(PrecedingQuery other)
			: base(other)
		{
			this.workIterator = Query.Clone(other.workIterator);
			this.ancestorStk = other.ancestorStk.Clone();
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00005A68 File Offset: 0x00003C68
		public override void Reset()
		{
			this.workIterator = null;
			this.ancestorStk.Clear();
			base.Reset();
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00005A84 File Offset: 0x00003C84
		public override XPathNavigator Advance()
		{
			if (this.workIterator == null)
			{
				XPathNavigator xpathNavigator = this.qyInput.Advance();
				if (xpathNavigator == null)
				{
					return null;
				}
				XPathNavigator xpathNavigator2 = xpathNavigator.Clone();
				do
				{
					xpathNavigator2.MoveTo(xpathNavigator);
				}
				while ((xpathNavigator = this.qyInput.Advance()) != null);
				if (xpathNavigator2.NodeType == XPathNodeType.Attribute || xpathNavigator2.NodeType == XPathNodeType.Namespace)
				{
					xpathNavigator2.MoveToParent();
				}
				do
				{
					this.ancestorStk.Push(xpathNavigator2.Clone());
				}
				while (xpathNavigator2.MoveToParent());
				this.workIterator = xpathNavigator2.SelectDescendants(XPathNodeType.All, true);
			}
			while (this.workIterator.MoveNext())
			{
				this.currentNode = this.workIterator.Current;
				if (this.currentNode.IsSamePosition(this.ancestorStk.Peek()))
				{
					this.ancestorStk.Pop();
					if (this.ancestorStk.Count == 0)
					{
						this.currentNode = null;
						this.workIterator = null;
						return null;
					}
				}
				else if (this.matches(this.currentNode))
				{
					this.position++;
					return this.currentNode;
				}
			}
			return null;
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00005B94 File Offset: 0x00003D94
		public override XPathNodeIterator Clone()
		{
			return new PrecedingQuery(this);
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00005B9C File Offset: 0x00003D9C
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}

		// Token: 0x040000D9 RID: 217
		private XPathNodeIterator workIterator;

		// Token: 0x040000DA RID: 218
		private ClonableStack<XPathNavigator> ancestorStk;
	}
}
