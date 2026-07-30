using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200059B RID: 1435
	internal class Sort : XslNode
	{
		// Token: 0x060038C0 RID: 14528 RVA: 0x0013F073 File Offset: 0x0013D273
		public Sort(string select, string lang, string dataType, string order, string caseOrder, XslVersion xslVer)
			: base(XslNodeType.Sort, null, select, xslVer)
		{
			this.Lang = lang;
			this.DataType = dataType;
			this.Order = order;
			this.CaseOrder = caseOrder;
		}

		// Token: 0x040024FF RID: 9471
		public readonly string Lang;

		// Token: 0x04002500 RID: 9472
		public readonly string DataType;

		// Token: 0x04002501 RID: 9473
		public readonly string Order;

		// Token: 0x04002502 RID: 9474
		public readonly string CaseOrder;
	}
}
