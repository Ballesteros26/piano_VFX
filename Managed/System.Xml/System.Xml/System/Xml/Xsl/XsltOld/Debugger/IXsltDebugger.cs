using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld.Debugger
{
	// Token: 0x02000566 RID: 1382
	internal interface IXsltDebugger
	{
		// Token: 0x0600373B RID: 14139
		string GetBuiltInTemplatesUri();

		// Token: 0x0600373C RID: 14140
		void OnInstructionCompile(XPathNavigator styleSheetNavigator);

		// Token: 0x0600373D RID: 14141
		void OnInstructionExecute(IXsltProcessor xsltProcessor);
	}
}
