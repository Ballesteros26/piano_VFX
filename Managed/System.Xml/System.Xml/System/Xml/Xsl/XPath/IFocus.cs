using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.XPath
{
	// Token: 0x020005B4 RID: 1460
	internal interface IFocus
	{
		// Token: 0x06003A1C RID: 14876
		QilNode GetCurrent();

		// Token: 0x06003A1D RID: 14877
		QilNode GetPosition();

		// Token: 0x06003A1E RID: 14878
		QilNode GetLast();
	}
}
