using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020005A2 RID: 1442
	internal class XslNodeEx : XslNode
	{
		// Token: 0x060038C8 RID: 14536 RVA: 0x0013F201 File Offset: 0x0013D401
		public XslNodeEx(XslNodeType t, QilName name, object arg, XsltInput.ContextInfo ctxInfo, XslVersion xslVer)
			: base(t, name, arg, xslVer)
		{
			this.ElemNameLi = ctxInfo.elemNameLi;
			this.EndTagLi = ctxInfo.endTagLi;
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x0013F066 File Offset: 0x0013D266
		public XslNodeEx(XslNodeType t, QilName name, object arg, XslVersion xslVer)
			: base(t, name, arg, xslVer)
		{
		}

		// Token: 0x04002516 RID: 9494
		public readonly ISourceLineInfo ElemNameLi;

		// Token: 0x04002517 RID: 9495
		public readonly ISourceLineInfo EndTagLi;
	}
}
