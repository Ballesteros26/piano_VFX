using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005EF RID: 1519
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct XPathPrecedingDocOrderIterator
	{
		// Token: 0x06003B56 RID: 15190 RVA: 0x0014DD8E File Offset: 0x0014BF8E
		public void Create(XPathNavigator input, XmlNavigatorFilter filter)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, input);
			this.filter = filter;
			this.PushAncestors();
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x0014DDB0 File Offset: 0x0014BFB0
		public bool MoveNext()
		{
			if (!this.navStack.IsEmpty)
			{
				while (!this.filter.MoveToFollowing(this.navCurrent, this.navStack.Peek()))
				{
					this.navCurrent.MoveTo(this.navStack.Pop());
					if (this.navStack.IsEmpty)
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06003B58 RID: 15192 RVA: 0x0014DE0F File Offset: 0x0014C00F
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x0014DE17 File Offset: 0x0014C017
		private void PushAncestors()
		{
			this.navStack.Reset();
			do
			{
				this.navStack.Push(this.navCurrent.Clone());
			}
			while (this.navCurrent.MoveToParent());
			this.navStack.Pop();
		}

		// Token: 0x04002710 RID: 10000
		private XmlNavigatorFilter filter;

		// Token: 0x04002711 RID: 10001
		private XPathNavigator navCurrent;

		// Token: 0x04002712 RID: 10002
		private XmlNavigatorStack navStack;
	}
}
