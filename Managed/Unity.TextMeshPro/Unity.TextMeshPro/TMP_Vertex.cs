using System;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200000B RID: 11
	public struct TMP_Vertex
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000027B3 File Offset: 0x000009B3
		public static TMP_Vertex zero
		{
			get
			{
				return TMP_Vertex.k_Zero;
			}
		}

		// Token: 0x0400001D RID: 29
		public Vector3 position;

		// Token: 0x0400001E RID: 30
		public Vector2 uv;

		// Token: 0x0400001F RID: 31
		public Vector2 uv2;

		// Token: 0x04000020 RID: 32
		public Vector2 uv4;

		// Token: 0x04000021 RID: 33
		public Color32 color;

		// Token: 0x04000022 RID: 34
		private static readonly TMP_Vertex k_Zero;
	}
}
