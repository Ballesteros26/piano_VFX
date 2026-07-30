using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Identifies the constants that define the properties of a function.</summary>
	// Token: 0x0200099D RID: 2461
	[Flags]
	[Serializable]
	public enum FUNCFLAGS : short
	{
		/// <summary>The function should not be accessible from macro languages. This flag is intended for system-level functions or functions that type browsers should not display.</summary>
		// Token: 0x04002ECE RID: 11982
		FUNCFLAG_FRESTRICTED = 1,
		/// <summary>The function returns an object that is a source of events.</summary>
		// Token: 0x04002ECF RID: 11983
		FUNCFLAG_FSOURCE = 2,
		/// <summary>The function that supports data binding.</summary>
		// Token: 0x04002ED0 RID: 11984
		FUNCFLAG_FBINDABLE = 4,
		/// <summary>When set, any call to a method that sets the property results first in a call to IPropertyNotifySink::OnRequestEdit. The implementation of OnRequestEdit determines if the call is allowed to set the property.</summary>
		// Token: 0x04002ED1 RID: 11985
		FUNCFLAG_FREQUESTEDIT = 8,
		/// <summary>The function that is displayed to the user as bindable. <see cref="F:System.Runtime.InteropServices.FUNCFLAGS.FUNCFLAG_FBINDABLE" /> must also be set.</summary>
		// Token: 0x04002ED2 RID: 11986
		FUNCFLAG_FDISPLAYBIND = 16,
		/// <summary>The function that best represents the object. Only one function in a type can have this attribute.</summary>
		// Token: 0x04002ED3 RID: 11987
		FUNCFLAG_FDEFAULTBIND = 32,
		/// <summary>The function should not be displayed to the user, although it exists and is bindable.</summary>
		// Token: 0x04002ED4 RID: 11988
		FUNCFLAG_FHIDDEN = 64,
		/// <summary>The function supports GetLastError. If an error occurs during the function, the caller can call GetLastError to retrieve the error code.</summary>
		// Token: 0x04002ED5 RID: 11989
		FUNCFLAG_FUSESGETLASTERROR = 128,
		/// <summary>Permits an optimization in which the compiler looks for a member named "xyz" on the type of "abc". If such a member is found, and is flagged as an accessor function for an element of the default collection, a call is generated to that member function. Permitted on members in dispinterfaces and interfaces; not permitted on modules.</summary>
		// Token: 0x04002ED6 RID: 11990
		FUNCFLAG_FDEFAULTCOLLELEM = 256,
		/// <summary>The type information member is the default member for display in the user interface.</summary>
		// Token: 0x04002ED7 RID: 11991
		FUNCFLAG_FUIDEFAULT = 512,
		/// <summary>The property appears in an object browser, but not in a properties browser.</summary>
		// Token: 0x04002ED8 RID: 11992
		FUNCFLAG_FNONBROWSABLE = 1024,
		/// <summary>Tags the interface as having default behaviors.</summary>
		// Token: 0x04002ED9 RID: 11993
		FUNCFLAG_FREPLACEABLE = 2048,
		/// <summary>Mapped as individual bindable properties.</summary>
		// Token: 0x04002EDA RID: 11994
		FUNCFLAG_FIMMEDIATEBIND = 4096
	}
}
