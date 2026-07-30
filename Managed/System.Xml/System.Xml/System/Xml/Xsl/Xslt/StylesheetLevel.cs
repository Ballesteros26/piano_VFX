using System;
using System.Collections.Generic;
using System.Xml.Xsl.Qil;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200058B RID: 1419
	internal class StylesheetLevel
	{
		// Token: 0x040024A2 RID: 9378
		public Stylesheet[] Imports;

		// Token: 0x040024A3 RID: 9379
		public Dictionary<QilName, XslFlags> ModeFlags = new Dictionary<QilName, XslFlags>();

		// Token: 0x040024A4 RID: 9380
		public Dictionary<QilName, List<QilFunction>> ApplyFunctions = new Dictionary<QilName, List<QilFunction>>();
	}
}
