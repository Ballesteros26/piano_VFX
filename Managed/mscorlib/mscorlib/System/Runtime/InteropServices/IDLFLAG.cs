using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.IDLFLAG" /> instead. </summary>
	// Token: 0x020008F9 RID: 2297
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IDLFLAG instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum IDLFLAG : short
	{
		/// <summary>Whether the parameter passes or receives information is unspecified.</summary>
		// Token: 0x04002D26 RID: 11558
		IDLFLAG_NONE = 0,
		/// <summary>The parameter passes information from the caller to the callee.</summary>
		// Token: 0x04002D27 RID: 11559
		IDLFLAG_FIN = 1,
		/// <summary>The parameter returns information from the callee to the caller.</summary>
		// Token: 0x04002D28 RID: 11560
		IDLFLAG_FOUT = 2,
		/// <summary>The parameter is the local identifier of a client application.</summary>
		// Token: 0x04002D29 RID: 11561
		IDLFLAG_FLCID = 4,
		/// <summary>The parameter is the return value of the member.</summary>
		// Token: 0x04002D2A RID: 11562
		IDLFLAG_FRETVAL = 8
	}
}
