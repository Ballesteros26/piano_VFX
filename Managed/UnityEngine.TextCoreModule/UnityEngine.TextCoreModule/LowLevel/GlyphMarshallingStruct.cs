using System;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000050 RID: 80
	[UsedByNativeCode]
	internal struct GlyphMarshallingStruct
	{
		// Token: 0x0600021C RID: 540 RVA: 0x0001B840 File Offset: 0x00019A40
		public GlyphMarshallingStruct(Glyph glyph)
		{
			this.index = glyph.index;
			this.metrics = glyph.metrics;
			this.glyphRect = glyph.glyphRect;
			this.scale = glyph.scale;
			this.atlasIndex = glyph.atlasIndex;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0001B87F File Offset: 0x00019A7F
		public GlyphMarshallingStruct(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			this.index = index;
			this.metrics = metrics;
			this.glyphRect = glyphRect;
			this.scale = scale;
			this.atlasIndex = atlasIndex;
		}

		// Token: 0x040003CB RID: 971
		public uint index;

		// Token: 0x040003CC RID: 972
		public GlyphMetrics metrics;

		// Token: 0x040003CD RID: 973
		public GlyphRect glyphRect;

		// Token: 0x040003CE RID: 974
		public float scale;

		// Token: 0x040003CF RID: 975
		public int atlasIndex;
	}
}
