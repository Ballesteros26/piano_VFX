using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.LIBFLAGS" /> instead.</summary>
	// Token: 0x0200091D RID: 2333
	[Obsolete]
	[Flags]
	[Serializable]
	public enum LIBFLAGS : short
	{
		/// <summary>The type library is restricted, and should not be displayed to users.</summary>
		// Token: 0x04002DA8 RID: 11688
		LIBFLAG_FRESTRICTED = 1,
		/// <summary>The type library describes controls, and should not be displayed in type browsers intended for nonvisual objects.</summary>
		// Token: 0x04002DA9 RID: 11689
		LIBFLAG_FCONTROL = 2,
		/// <summary>The type library should not be displayed to users, although its use is not restricted. Should be used by controls. Hosts should create a new type library that wraps the control with extended properties.</summary>
		// Token: 0x04002DAA RID: 11690
		LIBFLAG_FHIDDEN = 4,
		/// <summary>The type library exists in a persisted form on disk.</summary>
		// Token: 0x04002DAB RID: 11691
		LIBFLAG_FHASDISKIMAGE = 8
	}
}
