using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Describes the original settings of the <see cref="T:System.Runtime.InteropServices.TYPEFLAGS" /> in the COM type library from which the type was imported.</summary>
	// Token: 0x020008B7 RID: 2231
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum TypeLibTypeFlags
	{
		/// <summary>A type description that describes an Application object.</summary>
		// Token: 0x04002C0E RID: 11278
		FAppObject = 1,
		/// <summary>Instances of the type can be created by ITypeInfo::CreateInstance.</summary>
		// Token: 0x04002C0F RID: 11279
		FCanCreate = 2,
		/// <summary>The type is licensed.</summary>
		// Token: 0x04002C10 RID: 11280
		FLicensed = 4,
		/// <summary>The type is predefined. The client application should automatically create a single instance of the object that has this attribute. The name of the variable that points to the object is the same as the class name of the object.</summary>
		// Token: 0x04002C11 RID: 11281
		FPreDeclId = 8,
		/// <summary>The type should not be displayed to browsers.</summary>
		// Token: 0x04002C12 RID: 11282
		FHidden = 16,
		/// <summary>The type is a control from which other types will be derived, and should not be displayed to users.</summary>
		// Token: 0x04002C13 RID: 11283
		FControl = 32,
		/// <summary>The interface supplies both IDispatch and V-table binding.</summary>
		// Token: 0x04002C14 RID: 11284
		FDual = 64,
		/// <summary>The interface cannot add members at run time.</summary>
		// Token: 0x04002C15 RID: 11285
		FNonExtensible = 128,
		/// <summary>The types used in the interface are fully compatible with Automation, including vtable binding support.</summary>
		// Token: 0x04002C16 RID: 11286
		FOleAutomation = 256,
		/// <summary>This flag is intended for system-level types or types that type browsers should not display.</summary>
		// Token: 0x04002C17 RID: 11287
		FRestricted = 512,
		/// <summary>The class supports aggregation.</summary>
		// Token: 0x04002C18 RID: 11288
		FAggregatable = 1024,
		/// <summary>The object supports IConnectionPointWithDefault, and has default behaviors.</summary>
		// Token: 0x04002C19 RID: 11289
		FReplaceable = 2048,
		/// <summary>Indicates that the interface derives from IDispatch, either directly or indirectly.</summary>
		// Token: 0x04002C1A RID: 11290
		FDispatchable = 4096,
		/// <summary>Indicates base interfaces should be checked for name resolution before checking child interfaces. This is the reverse of the default behavior.</summary>
		// Token: 0x04002C1B RID: 11291
		FReverseBind = 8192
	}
}
