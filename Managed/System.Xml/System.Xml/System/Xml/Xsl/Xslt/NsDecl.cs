using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000594 RID: 1428
	internal class NsDecl
	{
		// Token: 0x060038AC RID: 14508 RVA: 0x0013EDF0 File Offset: 0x0013CFF0
		public NsDecl(NsDecl prev, string prefix, string nsUri)
		{
			this.Prev = prev;
			this.Prefix = prefix;
			this.NsUri = nsUri;
		}

		// Token: 0x040024E6 RID: 9446
		public readonly NsDecl Prev;

		// Token: 0x040024E7 RID: 9447
		public readonly string Prefix;

		// Token: 0x040024E8 RID: 9448
		public readonly string NsUri;
	}
}
