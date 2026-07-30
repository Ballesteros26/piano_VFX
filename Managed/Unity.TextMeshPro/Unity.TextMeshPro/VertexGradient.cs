using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000064 RID: 100
	[Serializable]
	public struct VertexGradient
	{
		// Token: 0x060004D2 RID: 1234 RVA: 0x0002381C File Offset: 0x00021A1C
		public VertexGradient(Color color)
		{
			this.topLeft = color;
			this.topRight = color;
			this.bottomLeft = color;
			this.bottomRight = color;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0002383A File Offset: 0x00021A3A
		public VertexGradient(Color color0, Color color1, Color color2, Color color3)
		{
			this.topLeft = color0;
			this.topRight = color1;
			this.bottomLeft = color2;
			this.bottomRight = color3;
		}

		// Token: 0x0400046E RID: 1134
		public Color topLeft;

		// Token: 0x0400046F RID: 1135
		public Color topRight;

		// Token: 0x04000470 RID: 1136
		public Color bottomLeft;

		// Token: 0x04000471 RID: 1137
		public Color bottomRight;
	}
}
