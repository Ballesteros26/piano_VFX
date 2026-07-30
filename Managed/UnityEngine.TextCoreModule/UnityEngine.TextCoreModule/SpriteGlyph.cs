using System;

namespace UnityEngine.TextCore
{
	// Token: 0x02000022 RID: 34
	[Serializable]
	internal class SpriteGlyph : Glyph
	{
		// Token: 0x06000104 RID: 260 RVA: 0x00006E2F File Offset: 0x0000502F
		public SpriteGlyph()
		{
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00006E39 File Offset: 0x00005039
		public SpriteGlyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			base.index = index;
			base.metrics = metrics;
			base.glyphRect = glyphRect;
			base.scale = scale;
			base.atlasIndex = atlasIndex;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006E6D File Offset: 0x0000506D
		public SpriteGlyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex, Sprite sprite)
		{
			base.index = index;
			base.metrics = metrics;
			base.glyphRect = glyphRect;
			base.scale = scale;
			base.atlasIndex = atlasIndex;
			this.sprite = sprite;
		}

		// Token: 0x04000179 RID: 377
		public Sprite sprite;
	}
}
