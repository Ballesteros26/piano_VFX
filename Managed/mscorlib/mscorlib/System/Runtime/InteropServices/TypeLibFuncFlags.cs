using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the original settings of the FUNCFLAGS in the COM type library from where this method was imported.</summary>
	// Token: 0x020008B8 RID: 2232
	[ComVisible(true)]
	[Flags]
	[Serializable]
	public enum TypeLibFuncFlags
	{
		/// <summary>This flag is intended for system-level functions or functions that type browsers should not display.</summary>
		// Token: 0x04002C1D RID: 11293
		FRestricted = 1,
		/// <summary>The function returns an object that is a source of events.</summary>
		// Token: 0x04002C1E RID: 11294
		FSource = 2,
		/// <summary>The function that supports data binding.</summary>
		// Token: 0x04002C1F RID: 11295
		FBindable = 4,
		/// <summary>When set, any call to a method that sets the property results first in a call to IPropertyNotifySink::OnRequestEdit.</summary>
		// Token: 0x04002C20 RID: 11296
		FRequestEdit = 8,
		/// <summary>The function that is displayed to the user as bindable. <see cref="F:System.Runtime.InteropServices.TypeLibFuncFlags.FBindable" /> must also be set.</summary>
		// Token: 0x04002C21 RID: 11297
		FDisplayBind = 16,
		/// <summary>The function that best represents the object. Only one function in a type information can have this attribute.</summary>
		// Token: 0x04002C22 RID: 11298
		FDefaultBind = 32,
		/// <summary>The function should not be displayed to the user, although it exists and is bindable.</summary>
		// Token: 0x04002C23 RID: 11299
		FHidden = 64,
		/// <summary>The function supports GetLastError.</summary>
		// Token: 0x04002C24 RID: 11300
		FUsesGetLastError = 128,
		/// <summary>Permits an optimization in which the compiler looks for a member named "xyz" on the type "abc". If such a member is found and is flagged as an accessor function for an element of the default collection, then a call is generated to that member function.</summary>
		// Token: 0x04002C25 RID: 11301
		FDefaultCollelem = 256,
		/// <summary>The type information member is the default member for display in the user interface.</summary>
		// Token: 0x04002C26 RID: 11302
		FUiDefault = 512,
		/// <summary>The property appears in an object browser, but not in a properties browser.</summary>
		// Token: 0x04002C27 RID: 11303
		FNonBrowsable = 1024,
		/// <summary>Tags the interface as having default behaviors.</summary>
		// Token: 0x04002C28 RID: 11304
		FReplaceable = 2048,
		/// <summary>The function is mapped as individual bindable properties.</summary>
		// Token: 0x04002C29 RID: 11305
		FImmediateBind = 4096
	}
}
