using System;

namespace UnityEngine
{
	// Token: 0x02000006 RID: 6
	[Flags]
	public enum MeshColliderCookingOptions
	{
		// Token: 0x0400001B RID: 27
		None = 0,
		// Token: 0x0400001C RID: 28
		[Obsolete("No longer used because the problem this was trying to solve is gone since Unity 2018.3", true)]
		InflateConvexMesh = 1,
		// Token: 0x0400001D RID: 29
		CookForFasterSimulation = 2,
		// Token: 0x0400001E RID: 30
		EnableMeshCleaning = 4,
		// Token: 0x0400001F RID: 31
		WeldColocatedVertices = 8,
		// Token: 0x04000020 RID: 32
		UseFastMidphase = 16
	}
}
