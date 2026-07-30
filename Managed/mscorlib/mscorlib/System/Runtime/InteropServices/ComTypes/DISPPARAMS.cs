using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Contains the arguments passed to a method or property by IDispatch::Invoke.</summary>
	// Token: 0x02000998 RID: 2456
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct DISPPARAMS
	{
		/// <summary>Represents a reference to the array of arguments.</summary>
		// Token: 0x04002EAA RID: 11946
		public IntPtr rgvarg;

		/// <summary>Represents the dispatch IDs of named arguments.</summary>
		// Token: 0x04002EAB RID: 11947
		public IntPtr rgdispidNamedArgs;

		/// <summary>Represents the count of arguments.</summary>
		// Token: 0x04002EAC RID: 11948
		public int cArgs;

		/// <summary>Represents the count of named arguments </summary>
		// Token: 0x04002EAD RID: 11949
		public int cNamedArgs;
	}
}
