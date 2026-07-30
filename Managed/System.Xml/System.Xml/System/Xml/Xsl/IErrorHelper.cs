using System;

namespace System.Xml.Xsl
{
	// Token: 0x020004D5 RID: 1237
	internal interface IErrorHelper
	{
		// Token: 0x0600325F RID: 12895
		void ReportError(string res, params string[] args);

		// Token: 0x06003260 RID: 12896
		void ReportWarning(string res, params string[] args);
	}
}
