using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.FILETIME" /> instead.</summary>
	// Token: 0x02000914 RID: 2324
	[Obsolete]
	public struct FILETIME
	{
		/// <summary>Specifies the low 32 bits of the FILETIME.</summary>
		// Token: 0x04002D9B RID: 11675
		public int dwLowDateTime;

		/// <summary>Specifies the high 32 bits of the FILETIME.</summary>
		// Token: 0x04002D9C RID: 11676
		public int dwHighDateTime;
	}
}
