using System;

namespace System.Xml.Schema
{
	/// <summary>Represents the validity of an XML item validated by the <see cref="T:System.Xml.Schema.XmlSchemaValidator" /> class.</summary>
	// Token: 0x0200048F RID: 1167
	public enum XmlSchemaValidity
	{
		/// <summary>The validity of the XML item is not known.</summary>
		// Token: 0x04001E86 RID: 7814
		NotKnown,
		/// <summary>The XML item is valid.</summary>
		// Token: 0x04001E87 RID: 7815
		Valid,
		/// <summary>The XML item is invalid.</summary>
		// Token: 0x04001E88 RID: 7816
		Invalid
	}
}
