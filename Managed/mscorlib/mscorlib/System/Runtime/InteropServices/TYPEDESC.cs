using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.TYPEDESC" /> instead.</summary>
	// Token: 0x020008FD RID: 2301
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEDESC
	{
		/// <summary>If the variable is VT_SAFEARRAY or VT_PTR, the <see cref="F:System.Runtime.InteropServices.TYPEDESC.lpValue" /> field contains a pointer to a TYPEDESC that specifies the element type.</summary>
		// Token: 0x04002D38 RID: 11576
		public IntPtr lpValue;

		/// <summary>Indicates the variant type for the item described by this TYPEDESC.</summary>
		// Token: 0x04002D39 RID: 11577
		public short vt;
	}
}
