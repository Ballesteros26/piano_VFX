using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies various options to use when generating .NET Framework types for use with an XML Web Service.</summary>
	// Token: 0x020002C7 RID: 711
	[Flags]
	public enum CodeGenerationOptions
	{
		/// <summary>Represents primitive types by fields and primitive types by <see cref="N:System" /> namespace types.</summary>
		// Token: 0x0400158B RID: 5515
		[XmlIgnore]
		None = 0,
		/// <summary>Represents primitive types by properties.</summary>
		// Token: 0x0400158C RID: 5516
		[XmlEnum("properties")]
		GenerateProperties = 1,
		/// <summary>Creates events for the asynchronous invocation of Web methods.</summary>
		// Token: 0x0400158D RID: 5517
		[XmlEnum("newAsync")]
		GenerateNewAsync = 2,
		/// <summary>Creates Begin and End methods for the asynchronous invocation of Web methods.</summary>
		// Token: 0x0400158E RID: 5518
		[XmlEnum("oldAsync")]
		GenerateOldAsync = 4,
		/// <summary>Generates explicitly ordered serialization code as specified through the Order property of the <see cref="T:System.Xml.Serialization.XmlAnyElementAttribute" />, <see cref="T:System.Xml.Serialization.XmlArrayAttribute" />, and <see cref="T:System.Xml.Serialization.XmlElementAttribute" /> attributes. </summary>
		// Token: 0x0400158F RID: 5519
		[XmlEnum("order")]
		GenerateOrder = 8,
		/// <summary>Enables data binding.</summary>
		// Token: 0x04001590 RID: 5520
		[XmlEnum("enableDataBinding")]
		EnableDataBinding = 16
	}
}
