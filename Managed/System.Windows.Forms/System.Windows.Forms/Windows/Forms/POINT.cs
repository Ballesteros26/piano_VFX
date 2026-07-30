using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000451 RID: 1105
	internal struct POINT
	{
		// Token: 0x060048BA RID: 18618 RVA: 0x00119C50 File Offset: 0x00117E50
		internal POINT(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x00119C60 File Offset: 0x00117E60
		internal Point ToPoint()
		{
			return new Point(this.x, this.y);
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x00119C74 File Offset: 0x00117E74
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Point {",
				this.x.ToString(),
				", ",
				this.y.ToString(),
				"}"
			});
		}

		// Token: 0x04002414 RID: 9236
		internal int x;

		// Token: 0x04002415 RID: 9237
		internal int y;
	}
}
