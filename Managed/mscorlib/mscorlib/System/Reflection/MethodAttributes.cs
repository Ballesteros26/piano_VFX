using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies flags for method attributes. These flags are defined in the corhdr.h file.</summary>
	// Token: 0x020002F0 RID: 752
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum MethodAttributes
	{
		/// <summary>Retrieves accessibility information.</summary>
		// Token: 0x0400124E RID: 4686
		MemberAccessMask = 7,
		/// <summary>Indicates that the member cannot be referenced.</summary>
		// Token: 0x0400124F RID: 4687
		PrivateScope = 0,
		/// <summary>Indicates that the method is accessible only to the current class.</summary>
		// Token: 0x04001250 RID: 4688
		Private = 1,
		/// <summary>Indicates that the method is accessible to members of this type and its derived types that are in this assembly only.</summary>
		// Token: 0x04001251 RID: 4689
		FamANDAssem = 2,
		/// <summary>Indicates that the method is accessible to any class of this assembly.</summary>
		// Token: 0x04001252 RID: 4690
		Assembly = 3,
		/// <summary>Indicates that the method is accessible only to members of this class and its derived classes.</summary>
		// Token: 0x04001253 RID: 4691
		Family = 4,
		/// <summary>Indicates that the method is accessible to derived classes anywhere, as well as to any class in the assembly.</summary>
		// Token: 0x04001254 RID: 4692
		FamORAssem = 5,
		/// <summary>Indicates that the method is accessible to any object for which this object is in scope.</summary>
		// Token: 0x04001255 RID: 4693
		Public = 6,
		/// <summary>Indicates that the method is defined on the type; otherwise, it is defined per instance.</summary>
		// Token: 0x04001256 RID: 4694
		Static = 16,
		/// <summary>Indicates that the method cannot be overridden.</summary>
		// Token: 0x04001257 RID: 4695
		Final = 32,
		/// <summary>Indicates that the method is virtual.</summary>
		// Token: 0x04001258 RID: 4696
		Virtual = 64,
		/// <summary>Indicates that the method hides by name and signature; otherwise, by name only.</summary>
		// Token: 0x04001259 RID: 4697
		HideBySig = 128,
		/// <summary>Indicates that the method can only be overridden when it is also accessible.</summary>
		// Token: 0x0400125A RID: 4698
		CheckAccessOnOverride = 512,
		/// <summary>Retrieves vtable attributes.</summary>
		// Token: 0x0400125B RID: 4699
		VtableLayoutMask = 256,
		/// <summary>Indicates that the method will reuse an existing slot in the vtable. This is the default behavior.</summary>
		// Token: 0x0400125C RID: 4700
		ReuseSlot = 0,
		/// <summary>Indicates that the method always gets a new slot in the vtable.</summary>
		// Token: 0x0400125D RID: 4701
		NewSlot = 256,
		/// <summary>Indicates that the class does not provide an implementation of this method.</summary>
		// Token: 0x0400125E RID: 4702
		Abstract = 1024,
		/// <summary>Indicates that the method is special. The name describes how this method is special.</summary>
		// Token: 0x0400125F RID: 4703
		SpecialName = 2048,
		/// <summary>Indicates that the method implementation is forwarded through PInvoke (Platform Invocation Services).</summary>
		// Token: 0x04001260 RID: 4704
		PinvokeImpl = 8192,
		/// <summary>Indicates that the managed method is exported by thunk to unmanaged code.</summary>
		// Token: 0x04001261 RID: 4705
		UnmanagedExport = 8,
		/// <summary>Indicates that the common language runtime checks the name encoding.</summary>
		// Token: 0x04001262 RID: 4706
		RTSpecialName = 4096,
		/// <summary>Indicates a reserved flag for runtime use only.</summary>
		// Token: 0x04001263 RID: 4707
		ReservedMask = 53248,
		/// <summary>Indicates that the method has security associated with it. Reserved flag for runtime use only.</summary>
		// Token: 0x04001264 RID: 4708
		HasSecurity = 16384,
		/// <summary>Indicates that the method calls another method containing security code. Reserved flag for runtime use only.</summary>
		// Token: 0x04001265 RID: 4709
		RequireSecObject = 32768
	}
}
