using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.BINDPTR" /> instead.</summary>
	// Token: 0x0200090D RID: 2317
	[Obsolete]
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
	public struct BINDPTR
	{
		/// <summary>Represents a pointer to a <see cref="T:System.Runtime.InteropServices.FUNCDESC" /> structure.</summary>
		// Token: 0x04002D85 RID: 11653
		[FieldOffset(0)]
		public IntPtr lpfuncdesc;

		/// <summary>Represents a pointer to a <see cref="F:System.Runtime.InteropServices.BINDPTR.lptcomp" /> interface.</summary>
		// Token: 0x04002D86 RID: 11654
		[FieldOffset(0)]
		public IntPtr lptcomp;

		/// <summary>Represents a pointer to a <see cref="T:System.Runtime.InteropServices.VARDESC" /> structure.</summary>
		// Token: 0x04002D87 RID: 11655
		[FieldOffset(0)]
		public IntPtr lpvardesc;
	}
}
