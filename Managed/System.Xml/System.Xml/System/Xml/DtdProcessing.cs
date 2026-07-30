using System;

namespace System.Xml
{
	/// <summary>Specifies the options for processing DTDs. The <see cref="T:System.Xml.DtdProcessing" /> enumeration is used by <see cref="T:System.Xml.XmlReaderSettings" />.</summary>
	// Token: 0x02000090 RID: 144
	public enum DtdProcessing
	{
		/// <summary>Specifies that when a DTD is encountered, an <see cref="T:System.Xml.XmlException" /> is thrown with a message that states that DTDs are prohibited. This is the default behavior.</summary>
		// Token: 0x04000312 RID: 786
		Prohibit,
		/// <summary>Causes the DOCTYPE element to be ignored. No DTD processing occurs. </summary>
		// Token: 0x04000313 RID: 787
		Ignore,
		/// <summary>Used for parsing DTDs.</summary>
		// Token: 0x04000314 RID: 788
		Parse
	}
}
