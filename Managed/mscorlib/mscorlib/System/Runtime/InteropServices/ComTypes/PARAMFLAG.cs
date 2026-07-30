using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Describes how to transfer a structure element, parameter, or function return value between processes.</summary>
	// Token: 0x02000990 RID: 2448
	[Flags]
	[Serializable]
	public enum PARAMFLAG : short
	{
		/// <summary>Does not specify whether the parameter passes or receives information.</summary>
		// Token: 0x04002E8D RID: 11917
		PARAMFLAG_NONE = 0,
		/// <summary>The parameter passes information from the caller to the callee.</summary>
		// Token: 0x04002E8E RID: 11918
		PARAMFLAG_FIN = 1,
		/// <summary>The parameter returns information from the callee to the caller.</summary>
		// Token: 0x04002E8F RID: 11919
		PARAMFLAG_FOUT = 2,
		/// <summary>The parameter is the local identifier of a client application.</summary>
		// Token: 0x04002E90 RID: 11920
		PARAMFLAG_FLCID = 4,
		/// <summary>The parameter is the return value of the member.</summary>
		// Token: 0x04002E91 RID: 11921
		PARAMFLAG_FRETVAL = 8,
		/// <summary>The parameter is optional.</summary>
		// Token: 0x04002E92 RID: 11922
		PARAMFLAG_FOPT = 16,
		/// <summary>The parameter has default behaviors defined.</summary>
		// Token: 0x04002E93 RID: 11923
		PARAMFLAG_FHASDEFAULT = 32,
		/// <summary>The parameter has custom data.</summary>
		// Token: 0x04002E94 RID: 11924
		PARAMFLAG_FHASCUSTDATA = 64
	}
}
