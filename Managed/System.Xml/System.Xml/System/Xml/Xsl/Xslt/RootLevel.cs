using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x02000568 RID: 1384
	internal class RootLevel : StylesheetLevel
	{
		// Token: 0x0600373E RID: 14142 RVA: 0x001342BB File Offset: 0x001324BB
		public RootLevel(Stylesheet principal)
		{
			this.Imports = new Stylesheet[] { principal };
		}
	}
}
