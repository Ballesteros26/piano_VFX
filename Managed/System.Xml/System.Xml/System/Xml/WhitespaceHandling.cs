using System;

namespace System.Xml
{
	/// <summary>Specifies how white space is handled.</summary>
	// Token: 0x020000C1 RID: 193
	public enum WhitespaceHandling
	{
		/// <summary>Return Whitespace and SignificantWhitespace nodes. This is the default.</summary>
		// Token: 0x040003D8 RID: 984
		All,
		/// <summary>Return SignificantWhitespace nodes only.</summary>
		// Token: 0x040003D9 RID: 985
		Significant,
		/// <summary>Return no Whitespace and no SignificantWhitespace nodes.</summary>
		// Token: 0x040003DA RID: 986
		None
	}
}
