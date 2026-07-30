using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000239 RID: 569
	internal class MeshHandle : PoolItem
	{
		// Token: 0x040007BC RID: 1980
		internal Alloc allocVerts;

		// Token: 0x040007BD RID: 1981
		internal Alloc allocIndices;

		// Token: 0x040007BE RID: 1982
		internal uint triangleCount;

		// Token: 0x040007BF RID: 1983
		internal Page allocPage;

		// Token: 0x040007C0 RID: 1984
		internal uint allocTime;

		// Token: 0x040007C1 RID: 1985
		internal uint updateAllocID;
	}
}
