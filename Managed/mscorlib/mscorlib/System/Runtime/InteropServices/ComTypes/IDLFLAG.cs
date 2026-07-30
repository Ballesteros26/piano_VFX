using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Describes how to transfer a structure element, parameter, or function return value between processes.</summary>
	// Token: 0x0200098E RID: 2446
	[Flags]
	[Serializable]
	public enum IDLFLAG : short
	{
		/// <summary>Does not specify whether the parameter passes or receives information.</summary>
		// Token: 0x04002E85 RID: 11909
		IDLFLAG_NONE = 0,
		/// <summary>The parameter passes information from the caller to the callee.</summary>
		// Token: 0x04002E86 RID: 11910
		IDLFLAG_FIN = 1,
		/// <summary>The parameter returns information from the callee to the caller.</summary>
		// Token: 0x04002E87 RID: 11911
		IDLFLAG_FOUT = 2,
		/// <summary>The parameter is the local identifier of a client application.</summary>
		// Token: 0x04002E88 RID: 11912
		IDLFLAG_FLCID = 4,
		/// <summary>The parameter is the return value of the member.</summary>
		// Token: 0x04002E89 RID: 11913
		IDLFLAG_FRETVAL = 8
	}
}
