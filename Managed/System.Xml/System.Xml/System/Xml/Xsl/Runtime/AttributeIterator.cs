using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005C7 RID: 1479
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct AttributeIterator
	{
		// Token: 0x06003ACD RID: 15053 RVA: 0x0014C303 File Offset: 0x0014A503
		public void Create(XPathNavigator context)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.needFirst = true;
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x0014C31E File Offset: 0x0014A51E
		public bool MoveNext()
		{
			if (this.needFirst)
			{
				this.needFirst = !this.navCurrent.MoveToFirstAttribute();
				return !this.needFirst;
			}
			return this.navCurrent.MoveToNextAttribute();
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06003ACF RID: 15055 RVA: 0x0014C351 File Offset: 0x0014A551
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x0400265A RID: 9818
		private XPathNavigator navCurrent;

		// Token: 0x0400265B RID: 9819
		private bool needFirst;
	}
}
