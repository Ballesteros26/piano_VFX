using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Stores the parameters that are used during a moniker binding operation.</summary>
	// Token: 0x02000972 RID: 2418
	public struct BIND_OPTS
	{
		/// <summary>Specifies the size, in bytes, of the BIND_OPTS structure.</summary>
		// Token: 0x04002E29 RID: 11817
		public int cbStruct;

		/// <summary>Controls aspects of moniker binding operations.</summary>
		// Token: 0x04002E2A RID: 11818
		public int grfFlags;

		/// <summary>Represents flags that should be used when opening the file that contains the object identified by the moniker.</summary>
		// Token: 0x04002E2B RID: 11819
		public int grfMode;

		/// <summary>Indicates the amount of time (clock time in milliseconds, as returned by the GetTickCount function) that the caller specified to complete the binding operation.</summary>
		// Token: 0x04002E2C RID: 11820
		public int dwTickCountDeadline;
	}
}
