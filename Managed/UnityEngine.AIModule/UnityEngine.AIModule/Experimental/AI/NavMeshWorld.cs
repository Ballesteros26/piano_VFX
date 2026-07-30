using System;
using System.Runtime.CompilerServices;
using Unity.Jobs;
using UnityEngine.Bindings;

namespace UnityEngine.Experimental.AI
{
	// Token: 0x02000020 RID: 32
	[StaticAccessor("NavMeshWorldBindings", StaticAccessorType.DoubleColon)]
	public struct NavMeshWorld
	{
		// Token: 0x06000170 RID: 368 RVA: 0x000031C8 File Offset: 0x000013C8
		public bool IsValid()
		{
			return this.world != IntPtr.Zero;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000031EC File Offset: 0x000013EC
		public static NavMeshWorld GetDefaultWorld()
		{
			NavMeshWorld navMeshWorld;
			NavMeshWorld.GetDefaultWorld_Injected(out navMeshWorld);
			return navMeshWorld;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00003201 File Offset: 0x00001401
		private static void AddDependencyInternal(IntPtr navmesh, JobHandle handle)
		{
			NavMeshWorld.AddDependencyInternal_Injected(navmesh, ref handle);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000320B File Offset: 0x0000140B
		public void AddDependency(JobHandle job)
		{
			NavMeshWorld.AddDependencyInternal(this.world, job);
		}

		// Token: 0x06000174 RID: 372
		[MethodImpl(4096)]
		private static extern void GetDefaultWorld_Injected(out NavMeshWorld ret);

		// Token: 0x06000175 RID: 373
		[MethodImpl(4096)]
		private static extern void AddDependencyInternal_Injected(IntPtr navmesh, ref JobHandle handle);

		// Token: 0x04000072 RID: 114
		internal IntPtr world;
	}
}
