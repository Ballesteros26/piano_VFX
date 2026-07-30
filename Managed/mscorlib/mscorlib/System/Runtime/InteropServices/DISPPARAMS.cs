using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.DISPPARAMS" /> instead.</summary>
	// Token: 0x02000902 RID: 2306
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.DISPPARAMS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct DISPPARAMS
	{
		/// <summary>Represents a reference to the array of arguments.</summary>
		// Token: 0x04002D45 RID: 11589
		public IntPtr rgvarg;

		/// <summary>Represents the dispatch IDs of named arguments.</summary>
		// Token: 0x04002D46 RID: 11590
		public IntPtr rgdispidNamedArgs;

		/// <summary>Represents the count of arguments.</summary>
		// Token: 0x04002D47 RID: 11591
		public int cArgs;

		/// <summary>Represents the count of named arguments </summary>
		// Token: 0x04002D48 RID: 11592
		public int cNamedArgs;
	}
}
