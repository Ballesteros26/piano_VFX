using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.U2D
{
	// Token: 0x020003B3 RID: 947
	[NativeHeader("Runtime/2D/Renderer/SpriteRendererGroup.h")]
	[RequiredByNativeCode]
	[StructLayout(0)]
	internal class SpriteRendererGroup
	{
		// Token: 0x0600215F RID: 8543 RVA: 0x00038038 File Offset: 0x00036238
		public static void AddRenderers(NativeArray<SpriteIntermediateRendererInfo> renderers)
		{
			SpriteRendererGroup.AddRenderers(renderers.GetUnsafeReadOnlyPtr<SpriteIntermediateRendererInfo>(), renderers.Length);
		}

		// Token: 0x06002160 RID: 8544
		[MethodImpl(4096)]
		private unsafe static extern void AddRenderers(void* renderers, int count);

		// Token: 0x06002161 RID: 8545
		[MethodImpl(4096)]
		public static extern void Clear();
	}
}
