using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains a pointer to a bound-to <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> structure, <see cref="T:System.Runtime.InteropServices.VARDESC" /> structure, or an ITypeComp interface.</summary>
	// Token: 0x02000987 RID: 2439
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
	public struct BINDPTR
	{
		/// <summary>Represents a pointer to a <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> structure.</summary>
		// Token: 0x04002E43 RID: 11843
		[FieldOffset(0)]
		public IntPtr lpfuncdesc;

		/// <summary>Represents a pointer to a <see cref="T:System.Runtime.InteropServices.VARDESC" /> structure.</summary>
		// Token: 0x04002E44 RID: 11844
		[FieldOffset(0)]
		public IntPtr lpvardesc;

		/// <summary>Represents a pointer to an <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeComp" /> interface.</summary>
		// Token: 0x04002E45 RID: 11845
		[FieldOffset(0)]
		public IntPtr lptcomp;
	}
}
