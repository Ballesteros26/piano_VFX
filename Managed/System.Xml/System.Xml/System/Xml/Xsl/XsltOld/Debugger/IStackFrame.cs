using System;
using System.Xml.XPath;

namespace System.Xml.Xsl.XsltOld.Debugger
{
	// Token: 0x02000564 RID: 1380
	internal interface IStackFrame
	{
		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06003734 RID: 14132
		XPathNavigator Instruction { get; }

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06003735 RID: 14133
		XPathNodeIterator NodeSet { get; }

		// Token: 0x06003736 RID: 14134
		int GetVariablesCount();

		// Token: 0x06003737 RID: 14135
		XPathNavigator GetVariable(int varIndex);

		// Token: 0x06003738 RID: 14136
		object GetVariableValue(int varIndex);
	}
}
