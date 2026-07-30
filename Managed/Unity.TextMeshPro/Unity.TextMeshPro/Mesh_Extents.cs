using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200006A RID: 106
	[Serializable]
	public struct Mesh_Extents
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x00023A6F File Offset: 0x00021C6F
		public Mesh_Extents(Vector2 min, Vector2 max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x00023A80 File Offset: 0x00021C80
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

		// Token: 0x04000489 RID: 1161
		public Vector2 min;

		// Token: 0x0400048A RID: 1162
		public Vector2 max;
	}
}
