using System;

namespace System.Xml.Xsl.XsltOld.Debugger
{
	// Token: 0x02000565 RID: 1381
	internal interface IXsltProcessor
	{
		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06003739 RID: 14137
		int StackDepth { get; }

		// Token: 0x0600373A RID: 14138
		IStackFrame GetStackFrame(int depth);
	}
}
