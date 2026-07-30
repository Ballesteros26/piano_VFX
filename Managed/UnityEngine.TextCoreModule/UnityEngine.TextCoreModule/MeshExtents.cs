using System;

namespace UnityEngine.TextCore
{
	// Token: 0x02000030 RID: 48
	[Serializable]
	internal struct MeshExtents
	{
		// Token: 0x06000135 RID: 309 RVA: 0x00016A27 File Offset: 0x00014C27
		public MeshExtents(Vector2 min, Vector2 max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00016A38 File Offset: 0x00014C38
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Min (",
				this.min.x.ToString("f2"),
				", ",
				this.min.y.ToString("f2"),
				")   Max (",
				this.max.x.ToString("f2"),
				", ",
				this.max.y.ToString("f2"),
				")"
			});
		}

		// Token: 0x040002BF RID: 703
		public Vector2 min;

		// Token: 0x040002C0 RID: 704
		public Vector2 max;
	}
}
