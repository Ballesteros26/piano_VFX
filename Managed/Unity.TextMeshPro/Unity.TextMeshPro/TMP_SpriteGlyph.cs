using System;
using UnityEngine;
using UnityEngine.TextCore;

namespace TMPro
{
	// Token: 0x02000042 RID: 66
	[Serializable]
	public class TMP_SpriteGlyph : Glyph
	{
		// Token: 0x060002D1 RID: 721 RVA: 0x000115F2 File Offset: 0x0000F7F2
		public TMP_SpriteGlyph()
		{
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x000115FA File Offset: 0x0000F7FA
		public TMP_SpriteGlyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			base.index = index;
			base.metrics = metrics;
			base.glyphRect = glyphRect;
			base.scale = scale;
			base.atlasIndex = atlasIndex;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00011627 File Offset: 0x0000F827
		public TMP_SpriteGlyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex, Sprite sprite)
		{
			base.index = index;
			base.metrics = metrics;
			base.glyphRect = glyphRect;
			base.scale = scale;
			base.atlasIndex = atlasIndex;
			this.sprite = sprite;
		}

		// Token: 0x040002AB RID: 683
		public Sprite sprite;
	}
}
