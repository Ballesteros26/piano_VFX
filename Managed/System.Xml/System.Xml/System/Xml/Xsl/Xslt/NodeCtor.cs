using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x020005A0 RID: 1440
	internal class NodeCtor : XslNode
	{
		// Token: 0x060038C6 RID: 14534 RVA: 0x0013F1D2 File Offset: 0x0013D3D2
		public NodeCtor(XslNodeType nt, string nameAvt, string nsAvt, XslVersion xslVer)
			: base(nt, null, null, xslVer)
		{
			this.NameAvt = nameAvt;
			this.NsAvt = nsAvt;
		}

		// Token: 0x04002513 RID: 9491
		public readonly string NameAvt;

		// Token: 0x04002514 RID: 9492
		public readonly string NsAvt;
	}
}
