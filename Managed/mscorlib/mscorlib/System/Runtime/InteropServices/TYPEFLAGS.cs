using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Use <see cref="T:System.Runtime.InteropServices.ComTypes.TYPEFLAGS" /> instead.</summary>
	// Token: 0x020008F5 RID: 2293
	[Obsolete("Use System.Runtime.InteropServices.ComTypes.TYPEFLAGS instead. http://go.microsoft.com/fwlink/?linkid=14202", false)]
	[Flags]
	[Serializable]
	public enum TYPEFLAGS : short
	{
		/// <summary>A type description that describes an Application object.</summary>
		// Token: 0x04002CF2 RID: 11506
		TYPEFLAG_FAPPOBJECT = 1,
		/// <summary>Instances of the type can be created by ITypeInfo::CreateInstance.</summary>
		// Token: 0x04002CF3 RID: 11507
		TYPEFLAG_FCANCREATE = 2,
		/// <summary>The type is licensed.</summary>
		// Token: 0x04002CF4 RID: 11508
		TYPEFLAG_FLICENSED = 4,
		/// <summary>The type is predefined. The client application should automatically create a single instance of the object that has this attribute. The name of the variable that points to the object is the same as the class name of the object.</summary>
		// Token: 0x04002CF5 RID: 11509
		TYPEFLAG_FPREDECLID = 8,
		/// <summary>The type should not be displayed to browsers.</summary>
		// Token: 0x04002CF6 RID: 11510
		TYPEFLAG_FHIDDEN = 16,
		/// <summary>The type is a control from which other types will be derived, and should not be displayed to users.</summary>
		// Token: 0x04002CF7 RID: 11511
		TYPEFLAG_FCONTROL = 32,
		/// <summary>The interface supplies both IDispatch and VTBL binding.</summary>
		// Token: 0x04002CF8 RID: 11512
		TYPEFLAG_FDUAL = 64,
		/// <summary>The interface cannot add members at run time.</summary>
		// Token: 0x04002CF9 RID: 11513
		TYPEFLAG_FNONEXTENSIBLE = 128,
		/// <summary>The types used in the interface are fully compatible with Automation, including VTBL binding support. Setting dual on an interface sets this flag in addition to <see cref="F:System.Runtime.InteropServices.TYPEFLAGS.TYPEFLAG_FDUAL" />. Not allowed on dispinterfaces.</summary>
		// Token: 0x04002CFA RID: 11514
		TYPEFLAG_FOLEAUTOMATION = 256,
		/// <summary>Should not be accessible from macro languages. This flag is intended for system-level types or types that type browsers should not display.</summary>
		// Token: 0x04002CFB RID: 11515
		TYPEFLAG_FRESTRICTED = 512,
		/// <summary>The class supports aggregation.</summary>
		// Token: 0x04002CFC RID: 11516
		TYPEFLAG_FAGGREGATABLE = 1024,
		/// <summary>The object supports IConnectionPointWithDefault, and has default behaviors.</summary>
		// Token: 0x04002CFD RID: 11517
		TYPEFLAG_FREPLACEABLE = 2048,
		/// <summary>Indicates that the interface derives from IDispatch, either directly or indirectly. This flag is computed, there is no Object Description Language for the flag.</summary>
		// Token: 0x04002CFE RID: 11518
		TYPEFLAG_FDISPATCHABLE = 4096,
		/// <summary>Indicates base interfaces should be checked for name resolution before checking children, the reverse of the default behavior.</summary>
		// Token: 0x04002CFF RID: 11519
		TYPEFLAG_FREVERSEBIND = 8192,
		/// <summary>Indicates that the interface will be using a proxy/stub dynamic link library. This flag specifies that the type library proxy should not be unregistered when the type library is unregistered.</summary>
		// Token: 0x04002D00 RID: 11520
		TYPEFLAG_FPROXY = 16384
	}
}
