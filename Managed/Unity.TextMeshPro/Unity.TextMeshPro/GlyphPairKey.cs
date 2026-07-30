using System;

namespace TMPro
{
	// Token: 0x02000029 RID: 41
	public struct GlyphPairKey
	{
		// Token: 0x06000149 RID: 329 RVA: 0x0000801C File Offset: 0x0000621C
		public GlyphPairKey(uint firstGlyphIndex, uint secondGlyphIndex)
		{
			this.firstGlyphIndex = firstGlyphIndex;
			this.secondGlyphIndex = secondGlyphIndex;
			this.key = (secondGlyphIndex << 16) | firstGlyphIndex;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00008038 File Offset: 0x00006238
		internal GlyphPairKey(TMP_GlyphPairAdjustmentRecord record)
		{
			this.firstGlyphIndex = record.firstAdjustmentRecord.glyphIndex;
			this.secondGlyphIndex = record.secondAdjustmentRecord.glyphIndex;
			this.key = (this.secondGlyphIndex << 16) | this.firstGlyphIndex;
		}

		// Token: 0x04000100 RID: 256
		public uint firstGlyphIndex;

		// Token: 0x04000101 RID: 257
		public uint secondGlyphIndex;

		// Token: 0x04000102 RID: 258
		public uint key;
	}
}
