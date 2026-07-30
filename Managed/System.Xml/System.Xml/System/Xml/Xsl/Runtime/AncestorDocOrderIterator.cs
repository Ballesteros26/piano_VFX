using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005E9 RID: 1513
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct AncestorDocOrderIterator
	{
		// Token: 0x06003B44 RID: 15172 RVA: 0x0014D9E0 File Offset: 0x0014BBE0
		public void Create(XPathNavigator context, XmlNavigatorFilter filter, bool orSelf)
		{
			AncestorIterator ancestorIterator = default(AncestorIterator);
			ancestorIterator.Create(context, filter, orSelf);
			this.stack.Reset();
			while (ancestorIterator.MoveNext())
			{
				XPathNavigator xpathNavigator = ancestorIterator.Current;
				this.stack.Push(xpathNavigator.Clone());
			}
		}

		// Token: 0x06003B45 RID: 15173 RVA: 0x0014DA2C File Offset: 0x0014BC2C
		public bool MoveNext()
		{
			if (this.stack.IsEmpty)
			{
				return false;
			}
			this.navCurrent = this.stack.Pop();
			return true;
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06003B46 RID: 15174 RVA: 0x0014DA4F File Offset: 0x0014BC4F
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x040026FD RID: 9981
		private XmlNavigatorStack stack;

		// Token: 0x040026FE RID: 9982
		private XPathNavigator navCurrent;
	}
}
