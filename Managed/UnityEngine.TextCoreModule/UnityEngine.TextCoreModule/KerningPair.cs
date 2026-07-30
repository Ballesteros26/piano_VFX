using System;
using UnityEngine.Serialization;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	internal class KerningPair
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00005144 File Offset: 0x00003344
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x0000515C File Offset: 0x0000335C
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

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00005168 File Offset: 0x00003368
		public GlyphValueRecord firstGlyphAdjustments
		{
			get
			{
				return this.m_FirstGlyphAdjustments;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00005180 File Offset: 0x00003380
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00005198 File Offset: 0x00003398
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000051A4 File Offset: 0x000033A4
		public GlyphValueRecord secondGlyphAdjustments
		{
			get
			{
				return this.m_SecondGlyphAdjustments;
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000051BC File Offset: 0x000033BC
		public KerningPair()
		{
			this.m_FirstGlyph = 0U;
			this.m_FirstGlyphAdjustments = default(GlyphValueRecord);
			this.m_SecondGlyph = 0U;
			this.m_SecondGlyphAdjustments = default(GlyphValueRecord);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000051EC File Offset: 0x000033EC
		public KerningPair(uint left, uint right, float offset)
		{
			this.firstGlyph = left;
			this.m_SecondGlyph = right;
			this.xOffset = offset;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000520C File Offset: 0x0000340C
		public KerningPair(uint firstGlyph, GlyphValueRecord firstGlyphAdjustments, uint secondGlyph, GlyphValueRecord secondGlyphAdjustments)
		{
			this.m_FirstGlyph = firstGlyph;
			this.m_FirstGlyphAdjustments = firstGlyphAdjustments;
			this.m_SecondGlyph = secondGlyph;
			this.m_SecondGlyphAdjustments = secondGlyphAdjustments;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005233 File Offset: 0x00003433
		internal void ConvertLegacyKerningData()
		{
			this.m_FirstGlyphAdjustments.xAdvance = this.xOffset;
		}

		// Token: 0x04000066 RID: 102
		[FormerlySerializedAs("AscII_Left")]
		[SerializeField]
		private uint m_FirstGlyph;

		// Token: 0x04000067 RID: 103
		[SerializeField]
		private GlyphValueRecord m_FirstGlyphAdjustments;

		// Token: 0x04000068 RID: 104
		[FormerlySerializedAs("AscII_Right")]
		[SerializeField]
		private uint m_SecondGlyph;

		// Token: 0x04000069 RID: 105
		[SerializeField]
		private GlyphValueRecord m_SecondGlyphAdjustments;

		// Token: 0x0400006A RID: 106
		[FormerlySerializedAs("XadvanceOffset")]
		public float xOffset;
	}
}
