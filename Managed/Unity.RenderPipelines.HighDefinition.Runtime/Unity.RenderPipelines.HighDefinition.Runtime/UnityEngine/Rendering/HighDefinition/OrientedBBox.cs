using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000025 RID: 37
	[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
	internal struct OrientedBBox
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00004B22 File Offset: 0x00002D22
		public Vector3 forward
		{
			get
			{
				return Vector3.Cross(this.up, this.right);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004B38 File Offset: 0x00002D38
		public OrientedBBox(Matrix4x4 trs)
		{
			Vector3 vector = trs.GetColumn(0);
			Vector3 vector2 = trs.GetColumn(1);
			Vector3 vector3 = trs.GetColumn(2);
			this.center = trs.GetColumn(3);
			this.right = vector * (1f / vector.magnitude);
			this.up = vector2 * (1f / vector2.magnitude);
			this.extentX = 0.5f * vector.magnitude;
			this.extentY = 0.5f * vector2.magnitude;
			this.extentZ = 0.5f * vector3.magnitude;
		}

		// Token: 0x0400009A RID: 154
		public Vector3 right;

		// Token: 0x0400009B RID: 155
		public float extentX;

		// Token: 0x0400009C RID: 156
		public Vector3 up;

		// Token: 0x0400009D RID: 157
		public float extentY;

		// Token: 0x0400009E RID: 158
		public Vector3 center;

		// Token: 0x0400009F RID: 159
		public float extentZ;
	}
}
