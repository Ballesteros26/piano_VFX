using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200056B RID: 1387
	internal class Output
	{
		// Token: 0x0600375A RID: 14170 RVA: 0x00134A50 File Offset: 0x00132C50
		public Output()
		{
			this.Settings = new XmlWriterSettings();
			this.Settings.OutputMethod = XmlOutputMethod.AutoDetect;
			this.Settings.AutoXmlDeclaration = true;
			this.Settings.ConformanceLevel = ConformanceLevel.Auto;
			this.Settings.MergeCDataSections = true;
		}

		// Token: 0x04002373 RID: 9075
		public XmlWriterSettings Settings;

		// Token: 0x04002374 RID: 9076
		public string Version;

		// Token: 0x04002375 RID: 9077
		public string Encoding;

		// Token: 0x04002376 RID: 9078
		public XmlQualifiedName Method;

		// Token: 0x04002377 RID: 9079
		public const int NeverDeclaredPrec = -2147483648;

		// Token: 0x04002378 RID: 9080
		public int MethodPrec = int.MinValue;

		// Token: 0x04002379 RID: 9081
		public int VersionPrec = int.MinValue;

		// Token: 0x0400237A RID: 9082
		public int EncodingPrec = int.MinValue;

		// Token: 0x0400237B RID: 9083
		public int OmitXmlDeclarationPrec = int.MinValue;

		// Token: 0x0400237C RID: 9084
		public int StandalonePrec = int.MinValue;

		// Token: 0x0400237D RID: 9085
		public int DocTypePublicPrec = int.MinValue;

		// Token: 0x0400237E RID: 9086
		public int DocTypeSystemPrec = int.MinValue;

		// Token: 0x0400237F RID: 9087
		public int IndentPrec = int.MinValue;

		// Token: 0x04002380 RID: 9088
		public int MediaTypePrec = int.MinValue;
	}
}
