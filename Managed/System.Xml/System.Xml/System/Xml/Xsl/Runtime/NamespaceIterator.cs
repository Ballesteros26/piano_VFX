using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005C8 RID: 1480
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct NamespaceIterator
	{
		// Token: 0x06003AD0 RID: 15056 RVA: 0x0014C35C File Offset: 0x0014A55C
		public void Create(XPathNavigator context)
		{
			this.navStack.Reset();
			if (context.MoveToFirstNamespace(XPathNamespaceScope.All))
			{
				do
				{
					if (context.LocalName.Length != 0 || context.Value.Length != 0)
					{
						this.navStack.Push(context.Clone());
					}
				}
				while (context.MoveToNextNamespace(XPathNamespaceScope.All));
				context.MoveToParent();
			}
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x0014C3B8 File Offset: 0x0014A5B8
		public bool MoveNext()
		{
			if (this.navStack.IsEmpty)
			{
				return false;
			}
			this.navCurrent = this.navStack.Pop();
			return true;
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06003AD2 RID: 15058 RVA: 0x0014C3DB File Offset: 0x0014A5DB
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x0400265C RID: 9820
		private XPathNavigator navCurrent;

		// Token: 0x0400265D RID: 9821
		private XmlNavigatorStack navStack;
	}
}
