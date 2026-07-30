using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x0200000B RID: 11
	[NativeHeader("Modules/VirtualTexturing/Public/VirtualTexturingSettings.h")]
	[UsedByNativeCode]
	[NativeAsStruct]
	[Serializable]
	[StructLayout(0)]
	public class VirtualTexturingSettings
	{
		// Token: 0x04000014 RID: 20
		public VirtualTexturingGPUCacheSettings gpuCache;

		// Token: 0x04000015 RID: 21
		public VirtualTexturingCPUCacheSettings cpuCache;
	}
}
