using System;

namespace System.Xml.Schema
{
	/// <summary>Specifies schema validation options used by the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> and <see cref="T:System.Xml.XmlReader" /> classes.</summary>
	// Token: 0x0200048B RID: 1163
	[Flags]
	public enum XmlSchemaValidationFlags
	{
		/// <summary>Do not process identity constraints, inline schemas, schema location hints, or report schema validation warnings.</summary>
		// Token: 0x04001E3A RID: 7738
		None = 0,
		/// <summary>Process inline schemas encountered during validation.</summary>
		// Token: 0x04001E3B RID: 7739
		ProcessInlineSchema = 1,
		/// <summary>Process schema location hints (xsi:schemaLocation, xsi:noNamespaceSchemaLocation) encountered during validation.</summary>
		// Token: 0x04001E3C RID: 7740
		ProcessSchemaLocation = 2,
		/// <summary>Report schema validation warnings encountered during validation.</summary>
		// Token: 0x04001E3D RID: 7741
		ReportValidationWarnings = 4,
		/// <summary>Process identity constraints (xs:ID, xs:IDREF, xs:key, xs:keyref, xs:unique) encountered during validation.</summary>
		// Token: 0x04001E3E RID: 7742
		ProcessIdentityConstraints = 8,
		/// <summary>Allow xml:* attributes even if they are not defined in the schema. The attributes will be validated based on their data type.</summary>
		// Token: 0x04001E3F RID: 7743
		AllowXmlAttributes = 16
	}
}
