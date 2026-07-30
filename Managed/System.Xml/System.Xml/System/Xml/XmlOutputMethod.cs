using System;

namespace System.Xml
{
	/// <summary>Specifies the method used to serialize the <see cref="T:System.Xml.XmlWriter" /> output. </summary>
	// Token: 0x020001E3 RID: 483
	public enum XmlOutputMethod
	{
		/// <summary>Serialize according to the XML 1.0 rules.</summary>
		// Token: 0x04000C30 RID: 3120
		Xml,
		/// <summary>Serialize according to the HTML rules specified by XSLT.</summary>
		// Token: 0x04000C31 RID: 3121
		Html,
		/// <summary>Serialize text blocks only.</summary>
		// Token: 0x04000C32 RID: 3122
		Text,
		/// <summary>Use the XSLT rules to choose between the <see cref="F:System.Xml.XmlOutputMethod.Xml" /> and <see cref="F:System.Xml.XmlOutputMethod.Html" /> output methods at runtime.</summary>
		// Token: 0x04000C33 RID: 3123
		AutoDetect
	}
}
