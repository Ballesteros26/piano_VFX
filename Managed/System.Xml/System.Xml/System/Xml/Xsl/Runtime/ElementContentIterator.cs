using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005C5 RID: 1477
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct ElementContentIterator
	{
		// Token: 0x06003AC7 RID: 15047 RVA: 0x0014C212 File Offset: 0x0014A412
		public void Create(XPathNavigator context, string localName, string ns)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.localName = localName;
			this.ns = ns;
			this.needFirst = true;
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x0014C23C File Offset: 0x0014A43C
		public bool MoveNext()
		{
			if (this.needFirst)
			{
				this.needFirst = !this.navCurrent.MoveToChild(this.localName, this.ns);
				return !this.needFirst;
			}
			return this.navCurrent.MoveToNext(this.localName, this.ns);
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06003AC9 RID: 15049 RVA: 0x0014C292 File Offset: 0x0014A492
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x04002653 RID: 9811
		private string localName;

		// Token: 0x04002654 RID: 9812
		private string ns;

		// Token: 0x04002655 RID: 9813
		private XPathNavigator navCurrent;

		// Token: 0x04002656 RID: 9814
		private bool needFirst;
	}
}
