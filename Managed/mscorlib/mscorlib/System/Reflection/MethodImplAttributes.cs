using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies flags for the attributes of a method implementation.</summary>
	// Token: 0x020002F4 RID: 756
	[ComVisible(true)]
	[Serializable]
	public enum MethodImplAttributes
	{
		/// <summary>Specifies flags about code type.</summary>
		// Token: 0x0400127A RID: 4730
		CodeTypeMask = 3,
		/// <summary>Specifies that the method implementation is in Microsoft intermediate language (MSIL).</summary>
		// Token: 0x0400127B RID: 4731
		IL = 0,
		/// <summary>Specifies that the method implementation is native.</summary>
		// Token: 0x0400127C RID: 4732
		Native,
		/// <summary>Specifies that the method implementation is in Optimized Intermediate Language (OPTIL).</summary>
		// Token: 0x0400127D RID: 4733
		OPTIL,
		/// <summary>Specifies that the method implementation is provided by the runtime.</summary>
		// Token: 0x0400127E RID: 4734
		Runtime,
		/// <summary>Specifies whether the method is implemented in managed or unmanaged code.</summary>
		// Token: 0x0400127F RID: 4735
		ManagedMask,
		/// <summary>Specifies that the method is implemented in unmanaged code.</summary>
		// Token: 0x04001280 RID: 4736
		Unmanaged = 4,
		/// <summary>Specifies that the method is implemented in managed code. </summary>
		// Token: 0x04001281 RID: 4737
		Managed = 0,
		/// <summary>Specifies that the method is not defined.</summary>
		// Token: 0x04001282 RID: 4738
		ForwardRef = 16,
		/// <summary>Specifies that the method signature is exported exactly as declared.</summary>
		// Token: 0x04001283 RID: 4739
		PreserveSig = 128,
		/// <summary>Specifies an internal call.</summary>
		// Token: 0x04001284 RID: 4740
		InternalCall = 4096,
		/// <summary>Specifies that the method is single-threaded through the body. Static methods (Shared in Visual Basic) lock on the type, whereas instance methods lock on the instance. You can also use the C# lock statement or the Visual Basic SyncLock statement for this purpose. </summary>
		// Token: 0x04001285 RID: 4741
		Synchronized = 32,
		/// <summary>Specifies that the method cannot be inlined.</summary>
		// Token: 0x04001286 RID: 4742
		NoInlining = 8,
		/// <summary>Specifies that the method should be inlined wherever possible.</summary>
		// Token: 0x04001287 RID: 4743
		[ComVisible(false)]
		AggressiveInlining = 256,
		/// <summary>Specifies that the method is not optimized by the just-in-time (JIT) compiler or by native code generation (see Ngen.exe) when debugging possible code generation problems.</summary>
		// Token: 0x04001288 RID: 4744
		NoOptimization = 64,
		/// <summary>Specifies a range check value.</summary>
		// Token: 0x04001289 RID: 4745
		MaxMethodImplVal = 65535
	}
}
