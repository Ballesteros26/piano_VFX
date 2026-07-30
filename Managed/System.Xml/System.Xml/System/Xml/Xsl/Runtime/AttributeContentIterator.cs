using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005C9 RID: 1481
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct AttributeContentIterator
	{
		// Token: 0x06003AD3 RID: 15059 RVA: 0x0014C3E3 File Offset: 0x0014A5E3
		public void Create(XPathNavigator context)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.needFirst = true;
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x0014C3FE File Offset: 0x0014A5FE
		public bool MoveNext()
		{
			if (this.needFirst)
			{
				this.needFirst = !XmlNavNeverFilter.MoveToFirstAttributeContent(this.navCurrent);
				return !this.needFirst;
			}
			return XmlNavNeverFilter.MoveToNextAttributeContent(this.navCurrent);
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06003AD5 RID: 15061 RVA: 0x0014C431 File Offset: 0x0014A631
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x0400265E RID: 9822
		private XPathNavigator navCurrent;

		// Token: 0x0400265F RID: 9823
		private bool needFirst;
	}
}
