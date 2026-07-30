using System;

namespace TMPro
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	public class TMP_Glyph : TMP_TextElement_Legacy
	{
		// Token: 0x06000106 RID: 262 RVA: 0x000073E8 File Offset: 0x000055E8
		public static TMP_Glyph Clone(TMP_Glyph source)
		{
			return new TMP_Glyph
			{
				id = source.id,
				x = source.x,
				y = source.y,
				width = source.width,
				height = source.height,
				xOffset = source.xOffset,
				yOffset = source.yOffset,
				xAdvance = source.xAdvance,
				scale = source.scale
			};
		}
	}
}
