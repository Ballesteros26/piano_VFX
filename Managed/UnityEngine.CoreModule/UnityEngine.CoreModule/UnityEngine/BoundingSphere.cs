using System;

namespace UnityEngine
{
	// Token: 0x020000B3 RID: 179
	public struct BoundingSphere
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x000061A2 File Offset: 0x000043A2
		public BoundingSphere(Vector3 pos, float rad)
		{
			this.position = pos;
			this.radius = rad;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000061B3 File Offset: 0x000043B3
		public BoundingSphere(Vector4 packedSphere)
		{
			this.position = new Vector3(packedSphere.x, packedSphere.y, packedSphere.z);
			this.radius = packedSphere.w;
		}

		// Token: 0x04000211 RID: 529
		public Vector3 position;

		// Token: 0x04000212 RID: 530
		public float radius;
	}
}
