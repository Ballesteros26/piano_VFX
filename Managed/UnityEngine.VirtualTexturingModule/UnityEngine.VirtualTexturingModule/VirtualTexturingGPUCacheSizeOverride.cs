using System;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.VirtualTexturing
{
	// Token: 0x02000008 RID: 8
	[UsedByNativeCode]
	[NativeHeader("Modules/VirtualTexturing/Public/VirtualTexturingSettings.h")]
	[Serializable]
	public struct VirtualTexturingGPUCacheSizeOverride
	{
		// Token: 0x0400000E RID: 14
		public VirtualTexturingCacheUsage usage;

		// Token: 0x0400000F RID: 15
		public GraphicsFormat format;

		// Token: 0x04000010 RID: 16
		public uint sizeInMegaBytes;
	}
}
