using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200019B RID: 411
	public struct Vertex
	{
		// Token: 0x040004D1 RID: 1233
		public static readonly float nearZ = 0f;

		// Token: 0x040004D2 RID: 1234
		public Vector3 position;

		// Token: 0x040004D3 RID: 1235
		public Color32 tint;

		// Token: 0x040004D4 RID: 1236
		public Vector2 uv;

		// Token: 0x040004D5 RID: 1237
		internal Color32 xformClipPages;

		// Token: 0x040004D6 RID: 1238
		internal Color32 idsFlags;

		// Token: 0x040004D7 RID: 1239
		internal Color32 opacityPageSVGSettingIndex;
	}
}
