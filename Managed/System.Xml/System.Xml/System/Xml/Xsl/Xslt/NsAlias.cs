using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200056E RID: 1390
	internal class NsAlias
	{
		// Token: 0x0600375F RID: 14175 RVA: 0x00134B5B File Offset: 0x00132D5B
		public NsAlias(string resultNsUri, string resultPrefix, int importPrecedence)
		{
			this.ResultNsUri = resultNsUri;
			this.ResultPrefix = resultPrefix;
			this.ImportPrecedence = importPrecedence;
		}

		// Token: 0x04002386 RID: 9094
		public readonly string ResultNsUri;

		// Token: 0x04002387 RID: 9095
		public readonly string ResultPrefix;

		// Token: 0x04002388 RID: 9096
		public readonly int ImportPrecedence;
	}
}
