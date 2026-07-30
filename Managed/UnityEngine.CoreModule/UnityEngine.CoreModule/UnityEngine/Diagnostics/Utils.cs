using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Diagnostics
{
	// Token: 0x020003B0 RID: 944
	[NativeHeader("Runtime/Export/Diagnostics/DiagnosticsUtils.bindings.h")]
	public static class Utils
	{
		// Token: 0x06002159 RID: 8537
		[FreeFunction("DiagnosticsUtils_Bindings::ForceCrash", ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern void ForceCrash(ForcedCrashCategory crashCategory);

		// Token: 0x0600215A RID: 8538
		[FreeFunction("DiagnosticsUtils_Bindings::NativeAssert")]
		[MethodImpl(4096)]
		public static extern void NativeAssert(string message);

		// Token: 0x0600215B RID: 8539
		[FreeFunction("DiagnosticsUtils_Bindings::NativeError")]
		[MethodImpl(4096)]
		public static extern void NativeError(string message);

		// Token: 0x0600215C RID: 8540
		[FreeFunction("DiagnosticsUtils_Bindings::NativeWarning")]
		[MethodImpl(4096)]
		public static extern void NativeWarning(string message);
	}
}
