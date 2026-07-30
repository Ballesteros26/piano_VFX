using System;

namespace UnityEngine.TextCore
{
	// Token: 0x02000014 RID: 20
	internal struct Extents
	{
		// Token: 0x060000CF RID: 207 RVA: 0x0000540B File Offset: 0x0000360B
		public Extents(Vector2 min, Vector2 max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000541C File Offset: 0x0000361C
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

		// Token: 0x04000073 RID: 115
		public Vector2 min;

		// Token: 0x04000074 RID: 116
		public Vector2 max;
	}
}
