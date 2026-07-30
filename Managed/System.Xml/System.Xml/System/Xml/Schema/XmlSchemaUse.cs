using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	/// <summary>Indicator of how the attribute is used.</summary>
	// Token: 0x02000488 RID: 1160
	public enum XmlSchemaUse
	{
		/// <summary>Attribute use not specified.</summary>
		// Token: 0x04001E34 RID: 7732
		[XmlIgnore]
		None,
		/// <summary>Attribute is optional.</summary>
		// Token: 0x04001E35 RID: 7733
		[XmlEnum("optional")]
		Optional,
		/// <summary>Attribute cannot be used.</summary>
		// Token: 0x04001E36 RID: 7734
		[XmlEnum("prohibited")]
		Prohibited,
		/// <summary>Attribute must appear once.</summary>
		// Token: 0x04001E37 RID: 7735
		[XmlEnum("required")]
		Required
	}
}
