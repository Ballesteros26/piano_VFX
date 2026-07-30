using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200001E RID: 30
	internal class Filter : AstNode
	{
		// Token: 0x060000BA RID: 186 RVA: 0x000038E6 File Offset: 0x00001AE6
		public Filter(AstNode input, AstNode condition)
		{
			this.input = input;
			this.condition = condition;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BB RID: 187 RVA: 0x000026AE File Offset: 0x000008AE
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Filter;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000226F File Offset: 0x0000046F
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000038FC File Offset: 0x00001AFC
		public AstNode Input
		{
			get
			{
				return this.input;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003904 File Offset: 0x00001B04
		public AstNode Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x04000080 RID: 128
		private AstNode input;

		// Token: 0x04000081 RID: 129
		private AstNode condition;
	}
}
