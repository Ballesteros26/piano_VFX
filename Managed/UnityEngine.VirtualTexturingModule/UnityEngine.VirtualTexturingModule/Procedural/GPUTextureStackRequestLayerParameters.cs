using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.VirtualTexturing.Procedural
{
	// Token: 0x02000010 RID: 16
	[UsedByNativeCode]
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	public struct GPUTextureStackRequestLayerParameters
	{
		// Token: 0x04000023 RID: 35
		public int destX;

		// Token: 0x04000024 RID: 36
		public int destY;

		// Token: 0x04000025 RID: 37
		public RenderTargetIdentifier dest;
	}
}
