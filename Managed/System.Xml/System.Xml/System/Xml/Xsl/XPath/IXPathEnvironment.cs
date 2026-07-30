using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.XPath
{
	// Token: 0x020005B5 RID: 1461
	internal interface IXPathEnvironment : IFocus
	{
		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06003A1F RID: 14879
		XPathQilFactory Factory { get; }

		// Token: 0x06003A20 RID: 14880
		QilNode ResolveVariable(string prefix, string name);

		// Token: 0x06003A21 RID: 14881
		QilNode ResolveFunction(string prefix, string name, IList<QilNode> args, IFocus env);

		// Token: 0x06003A22 RID: 14882
		string ResolvePrefix(string prefix);
	}
}
