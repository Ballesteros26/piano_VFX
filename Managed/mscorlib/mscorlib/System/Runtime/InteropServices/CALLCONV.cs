using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.CALLCONV" /> instead.</summary>
	// Token: 0x02000906 RID: 2310
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.CALLCONV instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum CALLCONV
	{
		/// <summary>Indicates that the Cdecl calling convention is used for a method.</summary>
		// Token: 0x04002D5D RID: 11613
		CC_CDECL = 1,
		/// <summary>Indicates that the Mscpascal calling convention is used for a method.</summary>
		// Token: 0x04002D5E RID: 11614
		CC_MSCPASCAL,
		/// <summary>Indicates that the Pascal calling convention is used for a method.</summary>
		// Token: 0x04002D5F RID: 11615
		CC_PASCAL = 2,
		/// <summary>Indicates that the Macpascal calling convention is used for a method.</summary>
		// Token: 0x04002D60 RID: 11616
		CC_MACPASCAL,
		/// <summary>Indicates that the Stdcall calling convention is used for a method.</summary>
		// Token: 0x04002D61 RID: 11617
		CC_STDCALL,
		/// <summary>This value is reserved for future use.</summary>
		// Token: 0x04002D62 RID: 11618
		CC_RESERVED,
		/// <summary>Indicates that the Syscall calling convention is used for a method.</summary>
		// Token: 0x04002D63 RID: 11619
		CC_SYSCALL,
		/// <summary>Indicates that the Mpwcdecl calling convention is used for a method.</summary>
		// Token: 0x04002D64 RID: 11620
		CC_MPWCDECL,
		/// <summary>Indicates that the Mpwpascal calling convention is used for a method.</summary>
		// Token: 0x04002D65 RID: 11621
		CC_MPWPASCAL,
		/// <summary>Indicates the end of the <see cref="T:System.Runtime.InteropServices.CALLCONV" /> enumeration.</summary>
		// Token: 0x04002D66 RID: 11622
		CC_MAX
	}
}
