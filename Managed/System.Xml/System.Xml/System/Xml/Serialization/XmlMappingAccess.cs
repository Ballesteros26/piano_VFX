using System;

namespace System.Xml.Serialization
{
	/// <summary>Specifies whether a mapping is read, write, or both.</summary>
	// Token: 0x02000335 RID: 821
	[Flags]
	public enum XmlMappingAccess
	{
		/// <summary>Both read and write methods are generated.</summary>
		// Token: 0x0400173E RID: 5950
		None = 0,
		/// <summary>Read methods are generated.</summary>
		// Token: 0x0400173F RID: 5951
		Read = 1,
		/// <summary>Write methods are generated.</summary>
		// Token: 0x04001740 RID: 5952
		Write = 2
	}
}
