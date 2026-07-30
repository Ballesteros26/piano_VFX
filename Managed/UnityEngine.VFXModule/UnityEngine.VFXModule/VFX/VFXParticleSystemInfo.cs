using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.VFX
{
	// Token: 0x02000016 RID: 22
	[NativeHeader("Modules/VFX/Public/VFXSystem.h")]
	[UsedByNativeCode]
	public struct VFXParticleSystemInfo
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00002EB0 File Offset: 0x000010B0
		public VFXParticleSystemInfo(uint aliveCount, uint capacity, bool sleeping, Bounds bounds)
		{
			this.aliveCount = aliveCount;
			this.capacity = capacity;
			this.sleeping = sleeping;
			this.bounds = bounds;
		}

		// Token: 0x040000DC RID: 220
		public uint aliveCount;

		// Token: 0x040000DD RID: 221
		public uint capacity;

		// Token: 0x040000DE RID: 222
		public bool sleeping;

		// Token: 0x040000DF RID: 223
		public Bounds bounds;
	}
}
