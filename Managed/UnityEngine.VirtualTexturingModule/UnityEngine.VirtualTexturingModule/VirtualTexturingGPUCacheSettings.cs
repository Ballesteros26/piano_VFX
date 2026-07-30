using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x02000009 RID: 9
	[NativeHeader("Modules/VirtualTexturing/Public/VirtualTexturingSettings.h")]
	[UsedByNativeCode]
	[Serializable]
	public struct VirtualTexturingGPUCacheSettings
	{
		// Token: 0x04000011 RID: 17
		public uint sizeInMegaBytes;

		// Token: 0x04000012 RID: 18
		public VirtualTexturingGPUCacheSizeOverride[] sizeOverrides;
	}
}
