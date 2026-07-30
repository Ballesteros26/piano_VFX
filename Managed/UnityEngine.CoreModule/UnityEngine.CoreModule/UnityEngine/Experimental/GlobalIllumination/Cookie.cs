using System;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x020003C0 RID: 960
	public struct Cookie
	{
		// Token: 0x0600216D RID: 8557 RVA: 0x000382AC File Offset: 0x000364AC
		public static Cookie Defaults()
		{
			Cookie cookie;
			cookie.instanceID = 0;
			cookie.scale = 1f;
			cookie.sizes = new Vector2(1f, 1f);
			return cookie;
		}

		// Token: 0x04000C2D RID: 3117
		public int instanceID;

		// Token: 0x04000C2E RID: 3118
		public float scale;

		// Token: 0x04000C2F RID: 3119
		public Vector2 sizes;
	}
}
