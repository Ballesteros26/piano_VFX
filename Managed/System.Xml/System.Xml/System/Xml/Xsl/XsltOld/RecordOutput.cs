using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000539 RID: 1337
	internal interface RecordOutput
	{
		// Token: 0x06003623 RID: 13859
		Processor.OutputResult RecordDone(RecordBuilder record);

		// Token: 0x06003624 RID: 13860
		void TheEnd();
	}
}
