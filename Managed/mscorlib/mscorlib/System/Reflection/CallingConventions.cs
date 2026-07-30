using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines the valid calling conventions for a method.</summary>
	// Token: 0x020002DA RID: 730
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum CallingConventions
	{
		/// <summary>Specifies the default calling convention as determined by the common language runtime. Use this calling convention for static methods. For instance or virtual methods use HasThis.</summary>
		// Token: 0x0400119C RID: 4508
		Standard = 1,
		/// <summary>Specifies the calling convention for methods with variable arguments.</summary>
		// Token: 0x0400119D RID: 4509
		VarArgs = 2,
		/// <summary>Specifies that either the Standard or the VarArgs calling convention may be used.</summary>
		// Token: 0x0400119E RID: 4510
		Any = 3,
		/// <summary>Specifies an instance or virtual method (not a static method). At run-time, the called method is passed a pointer to the target object as its first argument (the this pointer). The signature stored in metadata does not include the type of this first argument, because the method is known and its owner class can be discovered from metadata.</summary>
		// Token: 0x0400119F RID: 4511
		HasThis = 32,
		/// <summary>Specifies that the signature is a function-pointer signature, representing a call to an instance or virtual method (not a static method). If ExplicitThis is set, HasThis must also be set. The first argument passed to the called method is still a this pointer, but the type of the first argument is now unknown. Therefore, a token that describes the type (or class) of the this pointer is explicitly stored into its metadata signature.</summary>
		// Token: 0x040011A0 RID: 4512
		ExplicitThis = 64
	}
}
