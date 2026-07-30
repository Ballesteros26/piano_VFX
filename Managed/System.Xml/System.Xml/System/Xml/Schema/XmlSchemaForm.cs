using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Indicates if attributes or elements need to be qualified with a namespace prefix.</summary>
	// Token: 0x02000460 RID: 1120
	public enum XmlSchemaForm
	{
		/// <summary>Element and attribute form is not specified in the schema.</summary>
		// Token: 0x04001DB6 RID: 7606
		[XmlIgnore]
		None,
		/// <summary>Elements and attributes must be qualified with a namespace prefix.</summary>
		// Token: 0x04001DB7 RID: 7607
		[XmlEnum("qualified")]
		Qualified,
		/// <summary>Elements and attributes are not required to be qualified with a namespace prefix.</summary>
		// Token: 0x04001DB8 RID: 7608
		[XmlEnum("unqualified")]
		Unqualified
	}
}
