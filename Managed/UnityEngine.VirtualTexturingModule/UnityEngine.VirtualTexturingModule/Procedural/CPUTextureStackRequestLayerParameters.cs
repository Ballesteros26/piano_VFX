using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.VirtualTexturing.Procedural
{
	// Token: 0x02000011 RID: 17
	[NativeHeader("Modules/VirtualTexturing/ScriptBindings/VirtualTexturing.bindings.h")]
	[UsedByNativeCode]
	public struct CPUTextureStackRequestLayerParameters
	{
		// Token: 0x04000026 RID: 38
		public int scanlineSize;

		// Token: 0x04000027 RID: 39
		public int dataSize;

		// Token: 0x04000028 RID: 40
		[NativeDisableUnsafePtrRestriction]
		public unsafe void* data;
	}
}
