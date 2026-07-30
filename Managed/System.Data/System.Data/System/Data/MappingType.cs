using System;

namespace System.Data
{
	/// <summary>Specifies how a <see cref="T:System.Data.DataColumn" /> is mapped.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000D3 RID: 211
	public enum MappingType
	{
		/// <summary>The column is mapped to an XML element.</summary>
		// Token: 0x040007D9 RID: 2009
		Element = 1,
		/// <summary>The column is mapped to an XML attribute.</summary>
		// Token: 0x040007DA RID: 2010
		Attribute,
		/// <summary>The column is mapped to an <see cref="T:System.Xml.XmlText" /> node.</summary>
		// Token: 0x040007DB RID: 2011
		SimpleContent,
		/// <summary>The column is mapped to an internal structure.</summary>
		// Token: 0x040007DC RID: 2012
		Hidden
	}
}
