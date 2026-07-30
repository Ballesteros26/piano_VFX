using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the type of a COM member.</summary>
	// Token: 0x020008D9 RID: 2265
	[ComVisible(true)]
	[Serializable]
	public enum ComMemberType
	{
		/// <summary>The member is a normal method.</summary>
		// Token: 0x04002CC7 RID: 11463
		Method,
		/// <summary>The member gets properties.</summary>
		// Token: 0x04002CC8 RID: 11464
		PropGet,
		/// <summary>The member sets properties.</summary>
		// Token: 0x04002CC9 RID: 11465
		PropSet
	}
}
