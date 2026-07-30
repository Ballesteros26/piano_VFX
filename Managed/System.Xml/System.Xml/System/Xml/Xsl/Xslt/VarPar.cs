using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200059A RID: 1434
	internal class VarPar : XslNode
	{
		// Token: 0x060038BF RID: 14527 RVA: 0x0013F066 File Offset: 0x0013D266
		public VarPar(XslNodeType nt, QilName name, string select, XslVersion xslVer)
			: base(nt, name, select, xslVer)
		{
		}

		// Token: 0x040024FD RID: 9469
		public XslFlags DefValueFlags;

		// Token: 0x040024FE RID: 9470
		public QilNode Value;
	}
}
