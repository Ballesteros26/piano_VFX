using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.BIND_OPTS" /> instead.</summary>
	// Token: 0x0200090E RID: 2318
	[Obsolete]
	public struct BIND_OPTS
	{
		/// <summary>Specifies the size of the BIND_OPTS structure in bytes.</summary>
		// Token: 0x04002D88 RID: 11656
		public int cbStruct;

		/// <summary>Controls aspects of moniker binding operations.</summary>
		// Token: 0x04002D89 RID: 11657
		public int grfFlags;

		/// <summary>Flags that should be used when opening the file that contains the object identified by the moniker.</summary>
		// Token: 0x04002D8A RID: 11658
		public int grfMode;

		/// <summary>Indicates the amount of time (clock time in milliseconds, as returned by the GetTickCount function) the caller specified to complete the binding operation.</summary>
		// Token: 0x04002D8B RID: 11659
		public int dwTickCountDeadline;
	}
}
