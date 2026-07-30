using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000069 RID: 105
	public struct Extents
	{
		// Token: 0x060004D8 RID: 1240 RVA: 0x0002397D File Offset: 0x00021B7D
		public Extents(Vector2 min, Vector2 max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00023990 File Offset: 0x00021B90
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

		// Token: 0x04000485 RID: 1157
		internal static Extents zero = new Extents(Vector2.zero, Vector2.zero);

		// Token: 0x04000486 RID: 1158
		internal static Extents uninitialized = new Extents(new Vector2(32767f, 32767f), new Vector2(-32767f, -32767f));

		// Token: 0x04000487 RID: 1159
		public Vector2 min;

		// Token: 0x04000488 RID: 1160
		public Vector2 max;
	}
}
