using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.PARAMDESC" /> instead.</summary>
	// Token: 0x020008FC RID: 2300
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.PARAMDESC instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct PARAMDESC
	{
		/// <summary>Represents a pointer to a value that is being passed between processes.</summary>
		// Token: 0x04002D36 RID: 11574
		public IntPtr lpVarValue;

		/// <summary>Represents bitmask values that describe the structure element, parameter, or return value.</summary>
		// Token: 0x04002D37 RID: 11575
		public PARAMFLAG wParamFlags;
	}
}
