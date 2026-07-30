using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.PARAMFLAG" /> instead.</summary>
	// Token: 0x020008FB RID: 2299
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.PARAMFLAG instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum PARAMFLAG : short
	{
		/// <summary>Whether the parameter passes or receives information is unspecified.</summary>
		// Token: 0x04002D2E RID: 11566
		PARAMFLAG_NONE = 0,
		/// <summary>The parameter passes information from the caller to the callee.</summary>
		// Token: 0x04002D2F RID: 11567
		PARAMFLAG_FIN = 1,
		/// <summary>The parameter returns information from the callee to the caller.</summary>
		// Token: 0x04002D30 RID: 11568
		PARAMFLAG_FOUT = 2,
		/// <summary>The parameter is the local identifier of a client application.</summary>
		// Token: 0x04002D31 RID: 11569
		PARAMFLAG_FLCID = 4,
		/// <summary>The parameter is the return value of the member.</summary>
		// Token: 0x04002D32 RID: 11570
		PARAMFLAG_FRETVAL = 8,
		/// <summary>The parameter is optional.</summary>
		// Token: 0x04002D33 RID: 11571
		PARAMFLAG_FOPT = 16,
		/// <summary>The parameter has default behaviors defined.</summary>
		// Token: 0x04002D34 RID: 11572
		PARAMFLAG_FHASDEFAULT = 32,
		/// <summary>The parameter has custom data.</summary>
		// Token: 0x04002D35 RID: 11573
		PARAMFLAG_FHASCUSTDATA = 64
	}
}
