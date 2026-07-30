using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000040 RID: 64
	internal class Root : AstNode
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x00006B15 File Offset: 0x00004D15
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Root;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x0000226F File Offset: 0x0000046F
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}
	}
}
