using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering
{
	// Token: 0x02000312 RID: 786
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	public class PIX
	{
		// Token: 0x06001AEB RID: 6891
		[FreeFunction("PIX::BeginGPUCapture")]
		[MethodImpl(4096)]
		public static extern void BeginGPUCapture();

		// Token: 0x06001AEC RID: 6892
		[FreeFunction("PIX::EndGPUCapture")]
		[MethodImpl(4096)]
		public static extern void EndGPUCapture();

		// Token: 0x06001AED RID: 6893
		[FreeFunction("PIX::IsAttached")]
		[MethodImpl(4096)]
		public static extern bool IsAttached();
	}
}
