using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Specifies one of two factors that determine the memory alignment of fields when a type is marshaled.</summary>
	// Token: 0x02000375 RID: 885
	[ComVisible(true)]
	[Serializable]
	public enum PackingSize
	{
		/// <summary>The packing size is not specified.</summary>
		// Token: 0x040015AD RID: 5549
		Unspecified,
		/// <summary>The packing size is 1 byte.</summary>
		// Token: 0x040015AE RID: 5550
		Size1,
		/// <summary>The packing size is 2 bytes.</summary>
		// Token: 0x040015AF RID: 5551
		Size2,
		/// <summary>The packing size is 4 bytes.</summary>
		// Token: 0x040015B0 RID: 5552
		Size4 = 4,
		/// <summary>The packing size is 8 bytes.</summary>
		// Token: 0x040015B1 RID: 5553
		Size8 = 8,
		/// <summary>The packing size is 16 bytes.</summary>
		// Token: 0x040015B2 RID: 5554
		Size16 = 16,
		/// <summary>The packing size is 32 bytes.</summary>
		// Token: 0x040015B3 RID: 5555
		Size32 = 32,
		/// <summary>The packing size is 64 bytes.</summary>
		// Token: 0x040015B4 RID: 5556
		Size64 = 64,
		/// <summary>The packing size is 128 bytes.</summary>
		// Token: 0x040015B5 RID: 5557
		Size128 = 128
	}
}
