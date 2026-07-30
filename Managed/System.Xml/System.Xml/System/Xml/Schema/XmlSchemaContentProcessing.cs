using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Provides information about the validation mode of any and anyAttribute element replacements.</summary>
	// Token: 0x02000449 RID: 1097
	public enum XmlSchemaContentProcessing
	{
		/// <summary>Document items are not validated.</summary>
		// Token: 0x04001D6C RID: 7532
		[XmlIgnore]
		None,
		/// <summary>Document items must consist of well-formed XML and are not validated by the schema.</summary>
		// Token: 0x04001D6D RID: 7533
		[XmlEnum("skip")]
		Skip,
		/// <summary>If the associated schema is found, the document items will be validated. No errors will be thrown otherwise.</summary>
		// Token: 0x04001D6E RID: 7534
		[XmlEnum("lax")]
		Lax,
		/// <summary>The schema processor must find a schema associated with the indicated namespace to validate the document items.</summary>
		// Token: 0x04001D6F RID: 7535
		[XmlEnum("strict")]
		Strict
	}
}
