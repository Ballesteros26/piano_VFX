using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Specifies various types of data and functions.</summary>
	// Token: 0x02000989 RID: 2441
	[Serializable]
	public enum TYPEKIND
	{
		/// <summary>A set of enumerators.</summary>
		// Token: 0x04002E47 RID: 11847
		TKIND_ENUM,
		/// <summary>A structure with no methods.</summary>
		// Token: 0x04002E48 RID: 11848
		TKIND_RECORD,
		/// <summary>A module that can have only static functions and data (for example, a DLL).</summary>
		// Token: 0x04002E49 RID: 11849
		TKIND_MODULE,
		/// <summary>A type that has virtual functions, all of which are pure.</summary>
		// Token: 0x04002E4A RID: 11850
		TKIND_INTERFACE,
		/// <summary>A set of methods and properties that are accessible through IDispatch::Invoke. By default, dual interfaces return TKIND_DISPATCH.</summary>
		// Token: 0x04002E4B RID: 11851
		TKIND_DISPATCH,
		/// <summary>A set of implemented components interfaces.</summary>
		// Token: 0x04002E4C RID: 11852
		TKIND_COCLASS,
		/// <summary>A type that is an alias for another type.</summary>
		// Token: 0x04002E4D RID: 11853
		TKIND_ALIAS,
		/// <summary>A union of all members that have an offset of zero.</summary>
		// Token: 0x04002E4E RID: 11854
		TKIND_UNION,
		/// <summary>End-of-enumeration marker.</summary>
		// Token: 0x04002E4F RID: 11855
		TKIND_MAX
	}
}
