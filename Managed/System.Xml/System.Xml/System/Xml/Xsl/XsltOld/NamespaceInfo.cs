using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020004F6 RID: 1270
	internal class NamespaceInfo
	{
		// Token: 0x060033E3 RID: 13283 RVA: 0x00127779 File Offset: 0x00125979
		internal NamespaceInfo(string prefix, string nameSpace, int stylesheetId)
		{
			this.prefix = prefix;
			this.nameSpace = nameSpace;
			this.stylesheetId = stylesheetId;
		}

		// Token: 0x04002161 RID: 8545
		internal string prefix;

		// Token: 0x04002162 RID: 8546
		internal string nameSpace;

		// Token: 0x04002163 RID: 8547
		internal int stylesheetId;
	}
}
