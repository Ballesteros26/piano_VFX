using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000026 RID: 38
	internal class Group : AstNode
	{
		// Token: 0x060000EE RID: 238 RVA: 0x0000449C File Offset: 0x0000269C
		public Group(AstNode groupNode)
		{
			this.groupNode = groupNode;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000EF RID: 239 RVA: 0x000038E3 File Offset: 0x00001AE3
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Group;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x0000226F File Offset: 0x0000046F
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000044AB File Offset: 0x000026AB
		public AstNode GroupNode
		{
			get
			{
				return this.groupNode;
			}
		}

		// Token: 0x040000AD RID: 173
		private AstNode groupNode;
	}
}
