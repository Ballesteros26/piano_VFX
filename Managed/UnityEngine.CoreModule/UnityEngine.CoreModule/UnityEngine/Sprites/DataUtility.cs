using System;

namespace UnityEngine.Sprites
{
	// Token: 0x02000209 RID: 521
	public sealed class DataUtility
	{
		// Token: 0x06001731 RID: 5937 RVA: 0x00025AB8 File Offset: 0x00023CB8
		public static Vector4 GetInnerUV(Sprite sprite)
		{
			return sprite.GetInnerUVs();
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00025AD0 File Offset: 0x00023CD0
		public static Vector4 GetOuterUV(Sprite sprite)
		{
			return sprite.GetOuterUVs();
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00025AE8 File Offset: 0x00023CE8
		public static Vector4 GetPadding(Sprite sprite)
		{
			return sprite.GetPadding();
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00025B00 File Offset: 0x00023D00
		public static Vector2 GetMinSize(Sprite sprite)
		{
			Vector2 vector;
			vector.x = sprite.border.x + sprite.border.z;
			vector.y = sprite.border.y + sprite.border.w;
			return vector;
		}
	}
}
