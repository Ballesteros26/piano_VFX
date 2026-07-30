using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	/// <summary>Defines the details of how a method is implemented.</summary>
	// Token: 0x0200088A RID: 2186
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum MethodImplOptions
	{
		/// <summary>The method is implemented in unmanaged code.</summary>
		// Token: 0x04002BC4 RID: 11204
		Unmanaged = 4,
		/// <summary>The method is declared, but its implementation is provided elsewhere.</summary>
		// Token: 0x04002BC5 RID: 11205
		ForwardRef = 16,
		/// <summary>The method signature is exported exactly as declared.</summary>
		// Token: 0x04002BC6 RID: 11206
		PreserveSig = 128,
		/// <summary>The call is internal, that is, it calls a method that is implemented within the common language runtime.</summary>
		// Token: 0x04002BC7 RID: 11207
		InternalCall = 4096,
		/// <summary>The method can be executed by only one thread at a time. Static methods lock on the type, whereas instance methods lock on the instance. Only one thread can execute in any of the instance functions, and only one thread can execute in any of a class's static functions.</summary>
		// Token: 0x04002BC8 RID: 11208
		Synchronized = 32,
		/// <summary>The method cannot be inlined. Inlining is an optimization by which a method call is replaced with the method body.</summary>
		// Token: 0x04002BC9 RID: 11209
		NoInlining = 8,
		/// <summary>The method should be inlined if possible.</summary>
		// Token: 0x04002BCA RID: 11210
		[ComVisible(false)]
		AggressiveInlining = 256,
		/// <summary>The method is not optimized by the just-in-time (JIT) compiler or by native code generation (see Ngen.exe) when debugging possible code generation problems.</summary>
		// Token: 0x04002BCB RID: 11211
		NoOptimization = 64
	}
}
