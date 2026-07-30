using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS" /> instead.</summary>
	// Token: 0x020008F6 RID: 2294
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.IMPLTYPEFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum IMPLTYPEFLAGS
	{
		/// <summary>The interface or dispinterface represents the default for the source or sink.</summary>
		// Token: 0x04002D02 RID: 11522
		IMPLTYPEFLAG_FDEFAULT = 1,
		/// <summary>This member of a coclass is called rather than implemented.</summary>
		// Token: 0x04002D03 RID: 11523
		IMPLTYPEFLAG_FSOURCE = 2,
		/// <summary>The member should not be displayed or programmable by users.</summary>
		// Token: 0x04002D04 RID: 11524
		IMPLTYPEFLAG_FRESTRICTED = 4,
		/// <summary>Sinks receive events through the virtual function table (VTBL).</summary>
		// Token: 0x04002D05 RID: 11525
		IMPLTYPEFLAG_FDEFAULTVTABLE = 8
	}
}
