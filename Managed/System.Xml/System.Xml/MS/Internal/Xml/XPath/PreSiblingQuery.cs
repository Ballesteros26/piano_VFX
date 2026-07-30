using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000038 RID: 56
	internal class PreSiblingQuery : CacheAxisQuery
	{
		// Token: 0x0600017B RID: 379 RVA: 0x000059BA File Offset: 0x00003BBA
		public PreSiblingQuery(Query qyInput, string name, string prefix, XPathNodeType typeTest)
			: base(qyInput, name, prefix, typeTest)
		{
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000059C7 File Offset: 0x00003BC7
		protected PreSiblingQuery(PreSiblingQuery other)
			: base(other)
		{
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00005BA8 File Offset: 0x00003DA8
		private bool NotVisited(XPathNavigator nav, List<XPathNavigator> parentStk)
		{
			XPathNavigator xpathNavigator = nav.Clone();
			xpathNavigator.MoveToParent();
			for (int i = 0; i < parentStk.Count; i++)
			{
				if (xpathNavigator.IsSamePosition(parentStk[i]))
				{
					return false;
				}
			}
			parentStk.Add(xpathNavigator);
			return true;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00005BF0 File Offset: 0x00003DF0
		public override object Evaluate(XPathNodeIterator context)
		{
			base.Evaluate(context);
			List<XPathNavigator> list = new List<XPathNavigator>();
			Stack<XPathNavigator> stack = new Stack<XPathNavigator>();
			while ((this.currentNode = this.qyInput.Advance()) != null)
			{
				stack.Push(this.currentNode.Clone());
			}
			while (stack.Count != 0)
			{
				XPathNavigator xpathNavigator = stack.Pop();
				if (xpathNavigator.NodeType != XPathNodeType.Attribute && xpathNavigator.NodeType != XPathNodeType.Namespace && this.NotVisited(xpathNavigator, list))
				{
					XPathNavigator xpathNavigator2 = xpathNavigator.Clone();
					if (xpathNavigator2.MoveToParent())
					{
						xpathNavigator2.MoveToFirstChild();
						while (!xpathNavigator2.IsSamePosition(xpathNavigator))
						{
							if (this.matches(xpathNavigator2))
							{
								base.Insert(this.outputBuffer, xpathNavigator2);
							}
							if (!xpathNavigator2.MoveToNext())
							{
								break;
							}
						}
					}
				}
			}
			return this;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00005CAF File Offset: 0x00003EAF
		public override XPathNodeIterator Clone()
		{
			return new PreSiblingQuery(this);
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00005CB7 File Offset: 0x00003EB7
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}
	}
}
