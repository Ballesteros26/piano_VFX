using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Rendering.VirtualTexturing.Procedural
{
	// Token: 0x02000019 RID: 25
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	[StaticAccessor("VirtualTexturing::Procedural", StaticAccessorType.DoubleColon)]
	public static class TextureStackRequestLayerUtil
	{
		// Token: 0x0600004D RID: 77 RVA: 0x00002A9B File Offset: 0x00000C9B
		public static int GetWidth(this GPUTextureStackRequestLayerParameters layer)
		{
			return TextureStackRequestLayerUtil.GetWidth_Injected(ref layer);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public static int GetHeight(this GPUTextureStackRequestLayerParameters layer)
		{
			return TextureStackRequestLayerUtil.GetHeight_Injected(ref layer);
		}

		// Token: 0x0600004F RID: 79
		[MethodImpl(4096)]
		private static extern int GetWidth_Injected(ref GPUTextureStackRequestLayerParameters layer);

		// Token: 0x06000050 RID: 80
		[MethodImpl(4096)]
		private static extern int GetHeight_Injected(ref GPUTextureStackRequestLayerParameters layer);
	}
}
