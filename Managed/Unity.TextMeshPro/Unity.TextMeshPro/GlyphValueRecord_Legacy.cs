using System;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x0200001F RID: 31
	[Serializable]
	public struct GlyphValueRecord_Legacy
	{
		// Token: 0x0600010A RID: 266 RVA: 0x00007520 File Offset: 0x00005720
		internal GlyphValueRecord_Legacy(GlyphValueRecord valueRecord)
		{
			this.xPlacement = valueRecord.xPlacement;
			this.yPlacement = valueRecord.yPlacement;
			this.xAdvance = valueRecord.xAdvance;
			this.yAdvance = valueRecord.yAdvance;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00007558 File Offset: 0x00005758
		public static GlyphValueRecord_Legacy operator +(GlyphValueRecord_Legacy a, GlyphValueRecord_Legacy b)
		{
			GlyphValueRecord_Legacy glyphValueRecord_Legacy;
			glyphValueRecord_Legacy.xPlacement = a.xPlacement + b.xPlacement;
			glyphValueRecord_Legacy.yPlacement = a.yPlacement + b.yPlacement;
			glyphValueRecord_Legacy.xAdvance = a.xAdvance + b.xAdvance;
			glyphValueRecord_Legacy.yAdvance = a.yAdvance + b.yAdvance;
			return glyphValueRecord_Legacy;
		}

		// Token: 0x040000E1 RID: 225
		public float xPlacement;

		// Token: 0x040000E2 RID: 226
		public float yPlacement;

		// Token: 0x040000E3 RID: 227
		public float xAdvance;

		// Token: 0x040000E4 RID: 228
		public float yAdvance;
	}
}
