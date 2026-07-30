using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace System.Xml.Xsl.XPath
{
	// Token: 0x020005B6 RID: 1462
	internal interface IXPathBuilder<Node>
	{
		// Token: 0x06003A23 RID: 14883
		void StartBuild();

		// Token: 0x06003A24 RID: 14884
		Node EndBuild(Node result);

		// Token: 0x06003A25 RID: 14885
		Node String(string value);

		// Token: 0x06003A26 RID: 14886
		Node Number(double value);

		// Token: 0x06003A27 RID: 14887
		Node Operator(XPathOperator op, Node left, Node right);

		// Token: 0x06003A28 RID: 14888
		Node Axis(XPathAxis xpathAxis, XPathNodeType nodeType, string prefix, string name);

		// Token: 0x06003A29 RID: 14889
		Node JoinStep(Node left, Node right);

		// Token: 0x06003A2A RID: 14890
		Node Predicate(Node node, Node condition, bool reverseStep);

		// Token: 0x06003A2B RID: 14891
		Node Variable(string prefix, string name);

		// Token: 0x06003A2C RID: 14892
		Node Function(string prefix, string name, IList<Node> args);
	}
}
