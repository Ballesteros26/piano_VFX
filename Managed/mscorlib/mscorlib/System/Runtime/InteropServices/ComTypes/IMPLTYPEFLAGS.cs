using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Defines the attributes of an implemented or inherited interface of a type.</summary>
	// Token: 0x0200098B RID: 2443
	[Flags]
	[Serializable]
	public enum IMPLTYPEFLAGS
	{
		/// <summary>The interface or dispinterface represents the default for the source or sink.</summary>
		// Token: 0x04002E61 RID: 11873
		IMPLTYPEFLAG_FDEFAULT = 1,
		/// <summary>This member of a coclass is called rather than implemented.</summary>
		// Token: 0x04002E62 RID: 11874
		IMPLTYPEFLAG_FSOURCE = 2,
		/// <summary>The member should not be displayed or programmable by users.</summary>
		// Token: 0x04002E63 RID: 11875
		IMPLTYPEFLAG_FRESTRICTED = 4,
		/// <summary>Sinks receive events through the virtual function table (VTBL).</summary>
		// Token: 0x04002E64 RID: 11876
		IMPLTYPEFLAG_FDEFAULTVTABLE = 8
	}
}
