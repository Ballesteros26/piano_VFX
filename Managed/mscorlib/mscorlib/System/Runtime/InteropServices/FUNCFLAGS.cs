using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.FUNCFLAGS" /> instead. </summary>
	// Token: 0x02000907 RID: 2311
	[Flags]
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.FUNCFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum FUNCFLAGS : short
	{
		/// <summary>The function should not be accessible from macro languages. This flag is intended for system-level functions or functions that type browsers should not display.</summary>
		// Token: 0x04002D68 RID: 11624
		FUNCFLAG_FRESTRICTED = 1,
		/// <summary>The function returns an object that is a source of events.</summary>
		// Token: 0x04002D69 RID: 11625
		FUNCFLAG_FSOURCE = 2,
		/// <summary>The function that supports data binding.</summary>
		// Token: 0x04002D6A RID: 11626
		FUNCFLAG_FBINDABLE = 4,
		/// <summary>When set, any call to a method that sets the property results first in a call to IPropertyNotifySink::OnRequestEdit. The implementation of OnRequestEdit determines if the call is allowed to set the property.</summary>
		// Token: 0x04002D6B RID: 11627
		FUNCFLAG_FREQUESTEDIT = 8,
		/// <summary>The function that is displayed to the user as bindable. <see cref="F:System.Runtime.InteropServices.FUNCFLAGS.FUNCFLAG_FBINDABLE" /> must also be set.</summary>
		// Token: 0x04002D6C RID: 11628
		FUNCFLAG_FDISPLAYBIND = 16,
		/// <summary>The function that best represents the object. Only one function in a type information can have this attribute.</summary>
		// Token: 0x04002D6D RID: 11629
		FUNCFLAG_FDEFAULTBIND = 32,
		/// <summary>The function should not be displayed to the user, although it exists and is bindable.</summary>
		// Token: 0x04002D6E RID: 11630
		FUNCFLAG_FHIDDEN = 64,
		/// <summary>The function supports GetLastError. If an error occurs during the function, the caller can call GetLastError to retrieve the error code.</summary>
		// Token: 0x04002D6F RID: 11631
		FUNCFLAG_FUSESGETLASTERROR = 128,
		/// <summary>Permits an optimization in which the compiler looks for a member named "xyz" on the type of "abc". If such a member is found, and is flagged as an accessor function for an element of the default collection, a call is generated to that member function. Permitted on members in dispinterfaces and interfaces; not permitted on modules.</summary>
		// Token: 0x04002D70 RID: 11632
		FUNCFLAG_FDEFAULTCOLLELEM = 256,
		/// <summary>The type information member is the default member for display in the user interface.</summary>
		// Token: 0x04002D71 RID: 11633
		FUNCFLAG_FUIDEFAULT = 512,
		/// <summary>The property appears in an object browser, but not in a properties browser.</summary>
		// Token: 0x04002D72 RID: 11634
		FUNCFLAG_FNONBROWSABLE = 1024,
		/// <summary>Tags the interface as having default behaviors.</summary>
		// Token: 0x04002D73 RID: 11635
		FUNCFLAG_FREPLACEABLE = 2048,
		/// <summary>Mapped as individual bindable properties.</summary>
		// Token: 0x04002D74 RID: 11636
		FUNCFLAG_FIMMEDIATEBIND = 4096
	}
}
