using System;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000596 RID: 1430
	internal abstract class ProtoTemplate : XslNode
	{
		// Token: 0x060038B7 RID: 14519 RVA: 0x0013EEDD File Offset: 0x0013D0DD
		public ProtoTemplate(XslNodeType nt, QilName name, XslVersion xslVer)
			: base(nt, name, null, xslVer)
		{
		}

		// Token: 0x060038B8 RID: 14520
		public abstract string GetDebugName();

		// Token: 0x040024F2 RID: 9458
		public QilFunction Function;
	}
}
