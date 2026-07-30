using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005C4 RID: 1476
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct ContentIterator
	{
		// Token: 0x06003AC4 RID: 15044 RVA: 0x0014C1BC File Offset: 0x0014A3BC
		public void Create(XPathNavigator context)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.needFirst = true;
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x0014C1D7 File Offset: 0x0014A3D7
		public bool MoveNext()
		{
			if (this.needFirst)
			{
				this.needFirst = !this.navCurrent.MoveToFirstChild();
				return !this.needFirst;
			}
			return this.navCurrent.MoveToNext();
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06003AC6 RID: 15046 RVA: 0x0014C20A File Offset: 0x0014A40A
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x04002651 RID: 9809
		private XPathNavigator navCurrent;

		// Token: 0x04002652 RID: 9810
		private bool needFirst;
	}
}
