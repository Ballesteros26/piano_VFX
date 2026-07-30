using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005EE RID: 1518
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct XPathPrecedingIterator
	{
		// Token: 0x06003B53 RID: 15187 RVA: 0x0014DD18 File Offset: 0x0014BF18
		public void Create(XPathNavigator context, XmlNavigatorFilter filter)
		{
			XPathPrecedingDocOrderIterator xpathPrecedingDocOrderIterator = default(XPathPrecedingDocOrderIterator);
			xpathPrecedingDocOrderIterator.Create(context, filter);
			this.stack.Reset();
			while (xpathPrecedingDocOrderIterator.MoveNext())
			{
				XPathNavigator xpathNavigator = xpathPrecedingDocOrderIterator.Current;
				this.stack.Push(xpathNavigator.Clone());
			}
		}

		// Token: 0x06003B54 RID: 15188 RVA: 0x0014DD63 File Offset: 0x0014BF63
		public bool MoveNext()
		{
			if (this.stack.IsEmpty)
			{
				return false;
			}
			this.navCurrent = this.stack.Pop();
			return true;
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06003B55 RID: 15189 RVA: 0x0014DD86 File Offset: 0x0014BF86
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x0400270E RID: 9998
		private XmlNavigatorStack stack;

		// Token: 0x0400270F RID: 9999
		private XPathNavigator navCurrent;
	}
}
