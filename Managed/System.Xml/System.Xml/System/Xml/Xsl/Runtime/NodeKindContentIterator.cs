using System;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005C6 RID: 1478
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct NodeKindContentIterator
	{
		// Token: 0x06003ACA RID: 15050 RVA: 0x0014C29A File Offset: 0x0014A49A
		public void Create(XPathNavigator context, XPathNodeType nodeType)
		{
			this.navCurrent = XmlQueryRuntime.SyncToNavigator(this.navCurrent, context);
			this.nodeType = nodeType;
			this.needFirst = true;
		}

		// Token: 0x06003ACB RID: 15051 RVA: 0x0014C2BC File Offset: 0x0014A4BC
		public bool MoveNext()
		{
			if (this.needFirst)
			{
				this.needFirst = !this.navCurrent.MoveToChild(this.nodeType);
				return !this.needFirst;
			}
			return this.navCurrent.MoveToNext(this.nodeType);
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06003ACC RID: 15052 RVA: 0x0014C2FB File Offset: 0x0014A4FB
		public XPathNavigator Current
		{
			get
			{
				return this.navCurrent;
			}
		}

		// Token: 0x04002657 RID: 9815
		private XPathNodeType nodeType;

		// Token: 0x04002658 RID: 9816
		private XPathNavigator navCurrent;

		// Token: 0x04002659 RID: 9817
		private bool needFirst;
	}
}
