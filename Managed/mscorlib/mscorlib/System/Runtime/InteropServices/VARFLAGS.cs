using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.VARFLAGS" /> instead.</summary>
	// Token: 0x02000908 RID: 2312
	[Flags]
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.VARFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Serializable]
	public enum VARFLAGS : short
	{
		/// <summary>Assignment to the variable should not be allowed.</summary>
		// Token: 0x04002D76 RID: 11638
		VARFLAG_FREADONLY = 1,
		/// <summary>The variable returns an object that is a source of events.</summary>
		// Token: 0x04002D77 RID: 11639
		VARFLAG_FSOURCE = 2,
		/// <summary>The variable supports data binding.</summary>
		// Token: 0x04002D78 RID: 11640
		VARFLAG_FBINDABLE = 4,
		/// <summary>When set, any attempt to directly change the property results in a call to IPropertyNotifySink::OnRequestEdit. The implementation of OnRequestEdit determines if the change is accepted.</summary>
		// Token: 0x04002D79 RID: 11641
		VARFLAG_FREQUESTEDIT = 8,
		/// <summary>The variable is displayed to the user as bindable. <see cref="F:System.Runtime.InteropServices.VARFLAGS.VARFLAG_FBINDABLE" /> must also be set.</summary>
		// Token: 0x04002D7A RID: 11642
		VARFLAG_FDISPLAYBIND = 16,
		/// <summary>The variable is the single property that best represents the object. Only one variable in type information can have this attribute.</summary>
		// Token: 0x04002D7B RID: 11643
		VARFLAG_FDEFAULTBIND = 32,
		/// <summary>The variable should not be displayed to the user in a browser, although it exists and is bindable.</summary>
		// Token: 0x04002D7C RID: 11644
		VARFLAG_FHIDDEN = 64,
		/// <summary>The variable should not be accessible from macro languages. This flag is intended for system-level variables or variables that you do not want type browsers to display.</summary>
		// Token: 0x04002D7D RID: 11645
		VARFLAG_FRESTRICTED = 128,
		/// <summary>Permits an optimization in which the compiler looks for a member named "xyz" on the type of "abc". If such a member is found and is flagged as an accessor function for an element of the default collection, then a call is generated to that member function. Permitted on members in dispinterfaces and interfaces; not permitted on modules.</summary>
		// Token: 0x04002D7E RID: 11646
		VARFLAG_FDEFAULTCOLLELEM = 256,
		/// <summary>The variable is the default display in the user interface.</summary>
		// Token: 0x04002D7F RID: 11647
		VARFLAG_FUIDEFAULT = 512,
		/// <summary>The variable appears in an object browser, but not in a properties browser.</summary>
		// Token: 0x04002D80 RID: 11648
		VARFLAG_FNONBROWSABLE = 1024,
		/// <summary>Tags the interface as having default behaviors.</summary>
		// Token: 0x04002D81 RID: 11649
		VARFLAG_FREPLACEABLE = 2048,
		/// <summary>The variable is mapped as individual bindable properties.</summary>
		// Token: 0x04002D82 RID: 11650
		VARFLAG_FIMMEDIATEBIND = 4096
	}
}
