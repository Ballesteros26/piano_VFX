using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines the attributes that can be associated with a property. These attribute values are defined in corhdr.h.</summary>
	// Token: 0x020002FC RID: 764
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum PropertyAttributes
	{
		/// <summary>Specifies that no attributes are associated with a property.</summary>
		// Token: 0x040012A1 RID: 4769
		None = 0,
		/// <summary>Specifies that the property is special, with the name describing how the property is special.</summary>
		// Token: 0x040012A2 RID: 4770
		SpecialName = 512,
		/// <summary>Specifies a flag reserved for runtime use only.</summary>
		// Token: 0x040012A3 RID: 4771
		ReservedMask = 62464,
		/// <summary>Specifies that the metadata internal APIs check the name encoding.</summary>
		// Token: 0x040012A4 RID: 4772
		RTSpecialName = 1024,
		/// <summary>Specifies that the property has a default value.</summary>
		// Token: 0x040012A5 RID: 4773
		HasDefault = 4096,
		/// <summary>Reserved.</summary>
		// Token: 0x040012A6 RID: 4774
		Reserved2 = 8192,
		/// <summary>Reserved.</summary>
		// Token: 0x040012A7 RID: 4775
		Reserved3 = 16384,
		/// <summary>Reserved.</summary>
		// Token: 0x040012A8 RID: 4776
		Reserved4 = 32768
	}
}
