using System;
using System.Xml.Serialization;

namespace System.Web.Services.Description
{
	/// <summary>Specifies whether the message parts are encoded as abstract type definitions or concrete schema definitions.</summary>
	// Token: 0x02000123 RID: 291
	public enum SoapBindingUse
	{
		/// <summary>Specifies an empty string ("") value for the corresponding XML use attribute.</summary>
		// Token: 0x04000539 RID: 1337
		[XmlIgnore]
		Default,
		/// <summary>The message parts are encoded using given encoding rules.</summary>
		// Token: 0x0400053A RID: 1338
		[XmlEnum("encoded")]
		Encoded,
		/// <summary>The message parts represent a concrete schema.</summary>
		// Token: 0x0400053B RID: 1339
		[XmlEnum("literal")]
		Literal
	}
}
