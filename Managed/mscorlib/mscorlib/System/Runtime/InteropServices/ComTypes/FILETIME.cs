using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Represents the number of 100-nanosecond intervals since January 1, 1601. This structure is a 64-bit value.</summary>
	// Token: 0x0200097F RID: 2431
	public struct FILETIME
	{
		/// <summary>Specifies the low 32 bits of the FILETIME.</summary>
		// Token: 0x04002E2F RID: 11823
		public int dwLowDateTime;

		/// <summary>Specifies the high 32 bits of the FILETIME.</summary>
		// Token: 0x04002E30 RID: 11824
		public int dwHighDateTime;
	}
}
