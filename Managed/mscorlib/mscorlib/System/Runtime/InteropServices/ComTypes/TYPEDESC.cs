using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Describes the type of a variable, return type of a function, or the type of a function parameter.</summary>
	// Token: 0x02000992 RID: 2450
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEDESC
	{
		/// <summary>If the variable is VT_SAFEARRAY or VT_PTR, the lpValue field contains a pointer to a TYPEDESC that specifies the element type.</summary>
		// Token: 0x04002E97 RID: 11927
		public IntPtr lpValue;

		/// <summary>Indicates the variant type for the item described by this TYPEDESC.</summary>
		// Token: 0x04002E98 RID: 11928
		public short vt;
	}
}
