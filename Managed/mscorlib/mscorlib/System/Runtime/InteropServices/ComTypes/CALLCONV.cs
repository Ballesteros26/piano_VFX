using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Identifies the calling convention used by a method described in a METHODDATA Data Type structure.</summary>
	// Token: 0x0200099C RID: 2460
	[Serializable]
	public enum CALLCONV
	{
		/// <summary>Indicates that the C declaration (CDECL) calling convention is used for a method. </summary>
		// Token: 0x04002EC3 RID: 11971
		CC_CDECL = 1,
		/// <summary>Indicates that the MSC Pascal (MSCPASCAL) calling convention is used for a method.</summary>
		// Token: 0x04002EC4 RID: 11972
		CC_MSCPASCAL,
		/// <summary>Indicates that the Pascal calling convention is used for a method.</summary>
		// Token: 0x04002EC5 RID: 11973
		CC_PASCAL = 2,
		/// <summary>Indicates that the Macintosh Pascal (MACPASCAL) calling convention is used for a method.</summary>
		// Token: 0x04002EC6 RID: 11974
		CC_MACPASCAL,
		/// <summary>Indicates that the standard calling convention (STDCALL) is used for a method.</summary>
		// Token: 0x04002EC7 RID: 11975
		CC_STDCALL,
		/// <summary>This value is reserved for future use.</summary>
		// Token: 0x04002EC8 RID: 11976
		CC_RESERVED,
		/// <summary>Indicates that the standard SYSCALL calling convention is used for a method.</summary>
		// Token: 0x04002EC9 RID: 11977
		CC_SYSCALL,
		/// <summary>Indicates that the Macintosh Programmers' Workbench (MPW) CDECL calling convention is used for a method.</summary>
		// Token: 0x04002ECA RID: 11978
		CC_MPWCDECL,
		/// <summary>Indicates that the Macintosh Programmers' Workbench (MPW) PASCAL calling convention is used for a method.</summary>
		// Token: 0x04002ECB RID: 11979
		CC_MPWPASCAL,
		/// <summary>Indicates the end of the <see cref="T:System.Runtime.InteropServices.ComTypes.CALLCONV" /> enumeration.</summary>
		// Token: 0x04002ECC RID: 11980
		CC_MAX
	}
}
