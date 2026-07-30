using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace TMPro
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	public class KerningPair
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000075B6 File Offset: 0x000057B6
		// (set) Token: 0x0600010D RID: 269 RVA: 0x000075BE File Offset: 0x000057BE
		public uint firstGlyph
		{
			get
			{
				return this.m_FirstGlyph;
			}
			set
			{
				this.m_FirstGlyph = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000075C7 File Offset: 0x000057C7
		public GlyphValueRecord_Legacy firstGlyphAdjustments
		{
			get
			{
				return this.m_FirstGlyphAdjustments;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000075CF File Offset: 0x000057CF
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000075D7 File Offset: 0x000057D7
		public uint secondGlyph
		{
			get
			{
				return this.m_SecondGlyph;
			}
			set
			{
				this.m_SecondGlyph = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000075E0 File Offset: 0x000057E0
		public GlyphValueRecord_Legacy secondGlyphAdjustments
		{
			get
			{
				return this.m_SecondGlyphAdjustments;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000075E8 File Offset: 0x000057E8
		public bool ignoreSpacingAdjustments
		{
			get
			{
				return this.m_IgnoreSpacingAdjustments;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000075F0 File Offset: 0x000057F0
		public KerningPair()
		{
			this.m_FirstGlyph = 0U;
			this.m_FirstGlyphAdjustments = default(GlyphValueRecord_Legacy);
			this.m_SecondGlyph = 0U;
			this.m_SecondGlyphAdjustments = default(GlyphValueRecord_Legacy);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000761E File Offset: 0x0000581E
		public KerningPair(uint left, uint right, float offset)
		{
			this.firstGlyph = left;
			this.m_SecondGlyph = right;
			this.xOffset = offset;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000763B File Offset: 0x0000583B
		public KerningPair(uint firstGlyph, GlyphValueRecord_Legacy firstGlyphAdjustments, uint secondGlyph, GlyphValueRecord_Legacy secondGlyphAdjustments)
		{
			this.m_FirstGlyph = firstGlyph;
			this.m_FirstGlyphAdjustments = firstGlyphAdjustments;
			this.m_SecondGlyph = secondGlyph;
			this.m_SecondGlyphAdjustments = secondGlyphAdjustments;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00007660 File Offset: 0x00005860
		internal void ConvertLegacyKerningData()
		{
			this.m_FirstGlyphAdjustments.xAdvance = this.xOffset;
		}

		// Token: 0x040000E5 RID: 229
		[FormerlySerializedAs("AscII_Left")]
		[SerializeField]
		private uint m_FirstGlyph;

		// Token: 0x040000E6 RID: 230
		[SerializeField]
		private GlyphValueRecord_Legacy m_FirstGlyphAdjustments;

		// Token: 0x040000E7 RID: 231
		[FormerlySerializedAs("AscII_Right")]
		[SerializeField]
		private uint m_SecondGlyph;

		// Token: 0x040000E8 RID: 232
		[SerializeField]
		private GlyphValueRecord_Legacy m_SecondGlyphAdjustments;

		// Token: 0x040000E9 RID: 233
		[FormerlySerializedAs("XadvanceOffset")]
		public float xOffset;

		// Token: 0x040000EA RID: 234
		internal static KerningPair empty = new KerningPair(0U, default(GlyphValueRecord_Legacy), 0U, default(GlyphValueRecord_Legacy));

		// Token: 0x040000EB RID: 235
		[SerializeField]
		private bool m_IgnoreSpacingAdjustments;
	}
}
