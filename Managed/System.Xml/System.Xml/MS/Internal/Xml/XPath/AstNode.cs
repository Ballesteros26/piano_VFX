using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000007 RID: 7
	internal abstract class AstNode
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000E RID: 14
		public abstract AstNode.AstType Type { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15
		public abstract XPathResultType ReturnType { get; }

		// Token: 0x02000008 RID: 8
		public enum AstType
		{
			// Token: 0x0400003A RID: 58
			Axis,
			// Token: 0x0400003B RID: 59
			Operator,
			// Token: 0x0400003C RID: 60
			Filter,
			// Token: 0x0400003D RID: 61
			ConstantOperand,
			// Token: 0x0400003E RID: 62
			Function,
			// Token: 0x0400003F RID: 63
			Group,
			// Token: 0x04000040 RID: 64
			Root,
			// Token: 0x04000041 RID: 65
			Variable,
			// Token: 0x04000042 RID: 66
			Error
		}
	}
}
