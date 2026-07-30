using System;

namespace System.Xml.Schema
{
	/// <summary>Defines the post-schema-validation infoset of a validated XML node.</summary>
	// Token: 0x02000401 RID: 1025
	public interface IXmlSchemaInfo
	{
		/// <summary>Gets the <see cref="T:System.Xml.Schema.XmlSchemaValidity" /> value of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaValidity" /> value of this validated XML node.</returns>
		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x060027B8 RID: 10168
		XmlSchemaValidity Validity { get; }

		/// <summary>Gets a value indicating if this validated XML node was set as the result of a default being applied during XML Schema Definition Language (XSD) schema validation.</summary>
		/// <returns>true if this validated XML node was set as the result of a default being applied during schema validation; otherwise, false.</returns>
		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x060027B9 RID: 10169
		bool IsDefault { get; }

		/// <summary>Gets a value indicating if the value for this validated XML node is nil.</summary>
		/// <returns>true if the value for this validated XML node is nil; otherwise, false.</returns>
		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x060027BA RID: 10170
		bool IsNil { get; }

		/// <summary>Gets the dynamic schema type for this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaSimpleType" /> object that represents the dynamic schema type for this validated XML node.</returns>
		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x060027BB RID: 10171
		XmlSchemaSimpleType MemberType { get; }

		/// <summary>Gets the static XML Schema Definition Language (XSD) schema type of this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaType" /> of this validated XML node.</returns>
		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x060027BC RID: 10172
		XmlSchemaType SchemaType { get; }

		/// <summary>Gets the compiled <see cref="T:System.Xml.Schema.XmlSchemaElement" /> that corresponds to this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaElement" /> that corresponds to this validated XML node.</returns>
		// Token: 0x17000854 RID: 2132
		// (get) Token: 0x060027BD RID: 10173
		XmlSchemaElement SchemaElement { get; }

		/// <summary>Gets the compiled <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> that corresponds to this validated XML node.</summary>
		/// <returns>An <see cref="T:System.Xml.Schema.XmlSchemaAttribute" /> that corresponds to this validated XML node.</returns>
		// Token: 0x17000855 RID: 2133
		// (get) Token: 0x060027BE RID: 10174
		XmlSchemaAttribute SchemaAttribute { get; }
	}
}
