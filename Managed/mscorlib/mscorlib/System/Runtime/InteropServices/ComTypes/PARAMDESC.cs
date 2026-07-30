using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains information about how to transfer a structure element, parameter, or function return value between processes.</summary>
	// Token: 0x02000991 RID: 2449
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct PARAMDESC
	{
		/// <summary>Represents a pointer to a value that is being passed between processes.</summary>
		// Token: 0x04002E95 RID: 11925
		public IntPtr lpVarValue;

		/// <summary>Represents bitmask values that describe the structure element, parameter, or return value.</summary>
		// Token: 0x04002E96 RID: 11926
		public PARAMFLAG wParamFlags;
	}
}
