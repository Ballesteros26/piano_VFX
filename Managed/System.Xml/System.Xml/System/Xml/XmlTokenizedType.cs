using System;

namespace System.Xml
{
	/// <summary>Represents the XML type for the string. This allows the string to be read as a particular XML type, for example a CDATA section type.</summary>
	// Token: 0x02000282 RID: 642
	public enum XmlTokenizedType
	{
		/// <summary>CDATA type.</summary>
		// Token: 0x04000FC7 RID: 4039
		CDATA,
		/// <summary>ID type.</summary>
		// Token: 0x04000FC8 RID: 4040
		ID,
		/// <summary>IDREF type.</summary>
		// Token: 0x04000FC9 RID: 4041
		IDREF,
		/// <summary>IDREFS type.</summary>
		// Token: 0x04000FCA RID: 4042
		IDREFS,
		/// <summary>ENTITY type.</summary>
		// Token: 0x04000FCB RID: 4043
		ENTITY,
		/// <summary>ENTITIES type.</summary>
		// Token: 0x04000FCC RID: 4044
		ENTITIES,
		/// <summary>NMTOKEN type.</summary>
		// Token: 0x04000FCD RID: 4045
		NMTOKEN,
		/// <summary>NMTOKENS type.</summary>
		// Token: 0x04000FCE RID: 4046
		NMTOKENS,
		/// <summary>NOTATION type.</summary>
		// Token: 0x04000FCF RID: 4047
		NOTATION,
		/// <summary>ENUMERATION type.</summary>
		// Token: 0x04000FD0 RID: 4048
		ENUMERATION,
		/// <summary>QName type.</summary>
		// Token: 0x04000FD1 RID: 4049
		QName,
		/// <summary>NCName type.</summary>
		// Token: 0x04000FD2 RID: 4050
		NCName,
		/// <summary>No type.</summary>
		// Token: 0x04000FD3 RID: 4051
		None
	}
}
