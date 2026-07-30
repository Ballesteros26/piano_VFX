using System;
using System.Collections;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x0200053B RID: 1339
	internal struct DocumentKeyList
	{
		// Token: 0x0600362C RID: 13868 RVA: 0x0012F7C8 File Offset: 0x0012D9C8
		public DocumentKeyList(XPathNavigator rootNav, Hashtable keyTable)
		{
			this.rootNav = rootNav;
			this.keyTable = keyTable;
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x0600362D RID: 13869 RVA: 0x0012F7D8 File Offset: 0x0012D9D8
		public XPathNavigator RootNav
		{
			get
			{
				return this.rootNav;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x0600362E RID: 13870 RVA: 0x0012F7E0 File Offset: 0x0012D9E0
		public Hashtable KeyTable
		{
			get
			{
				return this.keyTable;
			}
		}

		// Token: 0x0400227E RID: 8830
		private XPathNavigator rootNav;

		// Token: 0x0400227F RID: 8831
		private Hashtable keyTable;
	}
}
