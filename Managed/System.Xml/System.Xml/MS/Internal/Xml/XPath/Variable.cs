using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000047 RID: 71
	internal class Variable : AstNode
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x000079AD File Offset: 0x00005BAD
		public Variable(string name, string prefix)
		{
			this.localname = name;
			this.prefix = prefix;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00006D07 File Offset: 0x00004F07
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Variable;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001FB RID: 507 RVA: 0x000038E3 File Offset: 0x00001AE3
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Any;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000079C3 File Offset: 0x00005BC3
		public string Localname
		{
			get
			{
				return this.localname;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000079CB File Offset: 0x00005BCB
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x0400010A RID: 266
		private string localname;

		// Token: 0x0400010B RID: 267
		private string prefix;
	}
}
