using System;
using System.Globalization;

namespace System.Xml.XmlConfiguration
{
	// Token: 0x020004B7 RID: 1207
	internal static class XmlConfigurationString
	{
		// Token: 0x04002024 RID: 8228
		internal const string XmlReaderSectionName = "xmlReader";

		// Token: 0x04002025 RID: 8229
		internal const string XsltSectionName = "xslt";

		// Token: 0x04002026 RID: 8230
		internal const string ProhibitDefaultResolverName = "prohibitDefaultResolver";

		// Token: 0x04002027 RID: 8231
		internal const string LimitXPathComplexityName = "limitXPathComplexity";

		// Token: 0x04002028 RID: 8232
		internal const string EnableMemberAccessForXslCompiledTransformName = "enableMemberAccessForXslCompiledTransform";

		// Token: 0x04002029 RID: 8233
		internal const string CollapseWhiteSpaceIntoEmptyStringName = "CollapseWhiteSpaceIntoEmptyString";

		// Token: 0x0400202A RID: 8234
		internal const string XmlConfigurationSectionName = "system.xml";

		// Token: 0x0400202B RID: 8235
		internal static string XmlReaderSectionPath = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", "system.xml", "xmlReader");

		// Token: 0x0400202C RID: 8236
		internal static string XsltSectionPath = string.Format(CultureInfo.InvariantCulture, "{0}/{1}", "system.xml", "xslt");
	}
}
