using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore
{
	// Token: 0x0200000D RID: 13
	[UsedByNativeCode]
	[Serializable]
	[StructLayout(0)]
	public class Glyph
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00004EC8 File Offset: 0x000030C8
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00004EE0 File Offset: 0x000030E0
		public uint index
		{
			get
			{
				return this.m_Index;
			}
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004EEC File Offset: 0x000030EC
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00004F04 File Offset: 0x00003104
		public GlyphMetrics metrics
		{
			get
			{
				return this.m_Metrics;
			}
			set
			{
				this.m_Metrics = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00004F10 File Offset: 0x00003110
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00004F28 File Offset: 0x00003128
		public GlyphRect glyphRect
		{
			get
			{
				return this.m_GlyphRect;
			}
			set
			{
				this.m_GlyphRect = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004F34 File Offset: 0x00003134
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00004F4C File Offset: 0x0000314C
		public float scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				this.m_Scale = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00004F58 File Offset: 0x00003158
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00004F70 File Offset: 0x00003170
		public int atlasIndex
		{
			get
			{
				return this.m_AtlasIndex;
			}
			set
			{
				this.m_AtlasIndex = value;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004F7A File Offset: 0x0000317A
		public Glyph()
		{
			this.m_Index = 0U;
			this.m_Metrics = default(GlyphMetrics);
			this.m_GlyphRect = default(GlyphRect);
			this.m_Scale = 1f;
			this.m_AtlasIndex = 0;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004FB8 File Offset: 0x000031B8
		public Glyph(Glyph glyph)
		{
			this.m_Index = glyph.index;
			this.m_Metrics = glyph.metrics;
			this.m_GlyphRect = glyph.glyphRect;
			this.m_Scale = glyph.scale;
			this.m_AtlasIndex = glyph.atlasIndex;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000500C File Offset: 0x0000320C
		internal Glyph(GlyphMarshallingStruct glyphStruct)
		{
			this.m_Index = glyphStruct.index;
			this.m_Metrics = glyphStruct.metrics;
			this.m_GlyphRect = glyphStruct.glyphRect;
			this.m_Scale = glyphStruct.scale;
			this.m_AtlasIndex = glyphStruct.atlasIndex;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000505D File Offset: 0x0000325D
		public Glyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect)
		{
			this.m_Index = index;
			this.m_Metrics = metrics;
			this.m_GlyphRect = glyphRect;
			this.m_Scale = 1f;
			this.m_AtlasIndex = 0;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x0000508E File Offset: 0x0000328E
		public Glyph(uint index, GlyphMetrics metrics, GlyphRect glyphRect, float scale, int atlasIndex)
		{
			this.m_Index = index;
			this.m_Metrics = metrics;
			this.m_GlyphRect = glyphRect;
			this.m_Scale = scale;
			this.m_AtlasIndex = atlasIndex;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000050C0 File Offset: 0x000032C0
		public bool Compare(Glyph other)
		{
			return this.index == other.index && this.metrics == other.metrics && this.glyphRect == other.glyphRect && this.scale == other.scale && this.atlasIndex == other.atlasIndex;
		}

		// Token: 0x0400005E RID: 94
		[SerializeField]
		[NativeName("index")]
		private uint m_Index;

		// Token: 0x0400005F RID: 95
		[SerializeField]
		[NativeName("metrics")]
		private GlyphMetrics m_Metrics;

		// Token: 0x04000060 RID: 96
		[NativeName("glyphRect")]
		[SerializeField]
		private GlyphRect m_GlyphRect;

		// Token: 0x04000061 RID: 97
		[SerializeField]
		[NativeName("scale")]
		private float m_Scale;

		// Token: 0x04000062 RID: 98
		[SerializeField]
		[NativeName("atlasIndex")]
		private int m_AtlasIndex;
	}
}
