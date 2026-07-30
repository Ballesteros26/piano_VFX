using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Specifies the order and structure of the child elements of a type.</summary>
	// Token: 0x02000448 RID: 1096
	public abstract class XmlSchemaContentModel : XmlSchemaAnnotated
	{
		/// <summary>Gets or sets the content of the type.</summary>
		/// <returns>Provides the content of the type.</returns>
		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06002B9C RID: 11164
		// (set) Token: 0x06002B9D RID: 11165
		[XmlIgnore]
		public abstract XmlSchemaContent Content { get; set; }
	}
}
