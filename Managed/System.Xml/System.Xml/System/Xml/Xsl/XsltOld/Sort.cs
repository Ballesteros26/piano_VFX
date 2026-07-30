using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004F2 RID: 1266
	internal class Sort
	{
		// Token: 0x06003375 RID: 13173 RVA: 0x001261DD File Offset: 0x001243DD
		public Sort(int sortkey, string xmllang, XmlDataType datatype, XmlSortOrder xmlorder, XmlCaseOrder xmlcaseorder)
		{
			this.select = sortkey;
			this.lang = xmllang;
			this.dataType = datatype;
			this.order = xmlorder;
			this.caseOrder = xmlcaseorder;
		}

		// Token: 0x0400213B RID: 8507
		internal int select;

		// Token: 0x0400213C RID: 8508
		internal string lang;

		// Token: 0x0400213D RID: 8509
		internal XmlDataType dataType;

		// Token: 0x0400213E RID: 8510
		internal XmlSortOrder order;

		// Token: 0x0400213F RID: 8511
		internal XmlCaseOrder caseOrder;
	}
}
