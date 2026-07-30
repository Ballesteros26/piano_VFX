using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000032 RID: 50
	internal class Operand : AstNode
	{
		// Token: 0x0600015E RID: 350 RVA: 0x00005894 File Offset: 0x00003A94
		public Operand(string val)
		{
			this.type = XPathResultType.String;
			this.val = val;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x000058AA File Offset: 0x00003AAA
		public Operand(double val)
		{
			this.type = XPathResultType.Number;
			this.val = val;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x000058C5 File Offset: 0x00003AC5
		public Operand(bool val)
		{
			this.type = XPathResultType.Boolean;
			this.val = val;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000226F File Offset: 0x0000046F
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.ConstantOperand;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000058E0 File Offset: 0x00003AE0
		public override XPathResultType ReturnType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000163 RID: 355 RVA: 0x000058E8 File Offset: 0x00003AE8
		public object OperandValue
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x040000C2 RID: 194
		private XPathResultType type;

		// Token: 0x040000C3 RID: 195
		private object val;
	}
}
