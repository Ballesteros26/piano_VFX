using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.TYPEKIND" /> instead.</summary>
	// Token: 0x020008F4 RID: 2292
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEKIND instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum TYPEKIND
	{
		/// <summary>A set of enumerators.</summary>
		// Token: 0x04002CE8 RID: 11496
		TKIND_ENUM,
		/// <summary>A structure with no methods.</summary>
		// Token: 0x04002CE9 RID: 11497
		TKIND_RECORD,
		/// <summary>A module that can only have static functions and data (for example, a DLL).</summary>
		// Token: 0x04002CEA RID: 11498
		TKIND_MODULE,
		/// <summary>A type that has virtual functions, all of which are pure.</summary>
		// Token: 0x04002CEB RID: 11499
		TKIND_INTERFACE,
		/// <summary>A set of methods and properties that are accessible through IDispatch::Invoke. By default, dual interfaces return TKIND_DISPATCH.</summary>
		// Token: 0x04002CEC RID: 11500
		TKIND_DISPATCH,
		/// <summary>A set of implemented components interfaces.</summary>
		// Token: 0x04002CED RID: 11501
		TKIND_COCLASS,
		/// <summary>A type that is an alias for another type.</summary>
		// Token: 0x04002CEE RID: 11502
		TKIND_ALIAS,
		/// <summary>A union of all members that have an offset of zero.</summary>
		// Token: 0x04002CEF RID: 11503
		TKIND_UNION,
		/// <summary>End of enumeration marker.</summary>
		// Token: 0x04002CF0 RID: 11504
		TKIND_MAX
	}
}
