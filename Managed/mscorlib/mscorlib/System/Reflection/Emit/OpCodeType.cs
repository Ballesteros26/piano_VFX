using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Describes the types of the Microsoft intermediate language (MSIL) instructions.</summary>
	// Token: 0x02000371 RID: 881
	[ComVisible(true)]
	[Serializable]
	public enum OpCodeType
	{
		/// <summary>This enumerator value is reserved and should not be used.</summary>
		// Token: 0x040014AD RID: 5293
		[Obsolete("This API has been deprecated.")]
		Annotation,
		/// <summary>These are Microsoft intermediate language (MSIL) instructions that are used as a synonym for other MSIL instructions. For example, ldarg.0 represents the ldarg instruction with an argument of 0.</summary>
		// Token: 0x040014AE RID: 5294
		Macro,
		/// <summary>Describes a reserved Microsoft intermediate language (MSIL) instruction.</summary>
		// Token: 0x040014AF RID: 5295
		Nternal,
		/// <summary>Describes a Microsoft intermediate language (MSIL) instruction that applies to objects.</summary>
		// Token: 0x040014B0 RID: 5296
		Objmodel,
		/// <summary>Describes a prefix instruction that modifies the behavior of the following instruction.</summary>
		// Token: 0x040014B1 RID: 5297
		Prefix,
		/// <summary>Describes a built-in instruction.</summary>
		// Token: 0x040014B2 RID: 5298
		Primitive
	}
}
