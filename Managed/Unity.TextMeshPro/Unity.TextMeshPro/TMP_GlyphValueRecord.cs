using System;
using UnityEngine;
using UnityEngine.TextCore.LowLevel;

namespace TMPro
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public struct TMP_GlyphValueRecord
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00007E14 File Offset: 0x00006014
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00007E1C File Offset: 0x0000601C
		public float xPlacement
		{
			get
			{
				return this.m_XPlacement;
			}
			set
			{
				this.m_XPlacement = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00007E25 File Offset: 0x00006025
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00007E2D File Offset: 0x0000602D
		public float yPlacement
		{
			get
			{
				return this.m_YPlacement;
			}
			set
			{
				this.m_YPlacement = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00007E36 File Offset: 0x00006036
		// (set) Token: 0x06000134 RID: 308 RVA: 0x00007E3E File Offset: 0x0000603E
		public float xAdvance
		{
			get
			{
				return this.m_XAdvance;
			}
			set
			{
				this.m_XAdvance = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00007E47 File Offset: 0x00006047
		// (set) Token: 0x06000136 RID: 310 RVA: 0x00007E4F File Offset: 0x0000604F
		public float yAdvance
		{
			get
			{
				return this.m_YAdvance;
			}
			set
			{
				this.m_YAdvance = value;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00007E58 File Offset: 0x00006058
		public TMP_GlyphValueRecord(float xPlacement, float yPlacement, float xAdvance, float yAdvance)
		{
			this.m_XPlacement = xPlacement;
			this.m_YPlacement = yPlacement;
			this.m_XAdvance = xAdvance;
			this.m_YAdvance = yAdvance;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00007E77 File Offset: 0x00006077
		internal TMP_GlyphValueRecord(GlyphValueRecord_Legacy valueRecord)
		{
			this.m_XPlacement = valueRecord.xPlacement;
			this.m_YPlacement = valueRecord.yPlacement;
			this.m_XAdvance = valueRecord.xAdvance;
			this.m_YAdvance = valueRecord.yAdvance;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00007EA9 File Offset: 0x000060A9
		internal TMP_GlyphValueRecord(GlyphValueRecord valueRecord)
		{
			this.m_XPlacement = valueRecord.xPlacement;
			this.m_YPlacement = valueRecord.yPlacement;
			this.m_XAdvance = valueRecord.xAdvance;
			this.m_YAdvance = valueRecord.yAdvance;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00007EE0 File Offset: 0x000060E0
		public static TMP_GlyphValueRecord operator +(TMP_GlyphValueRecord a, TMP_GlyphValueRecord b)
		{
			TMP_GlyphValueRecord tmp_GlyphValueRecord;
			tmp_GlyphValueRecord.m_XPlacement = a.xPlacement + b.xPlacement;
			tmp_GlyphValueRecord.m_YPlacement = a.yPlacement + b.yPlacement;
			tmp_GlyphValueRecord.m_XAdvance = a.xAdvance + b.xAdvance;
			tmp_GlyphValueRecord.m_YAdvance = a.yAdvance + b.yAdvance;
			return tmp_GlyphValueRecord;
		}

		// Token: 0x040000F7 RID: 247
		[SerializeField]
		internal float m_XPlacement;

		// Token: 0x040000F8 RID: 248
		[SerializeField]
		internal float m_YPlacement;

		// Token: 0x040000F9 RID: 249
		[SerializeField]
		internal float m_XAdvance;

		// Token: 0x040000FA RID: 250
		[SerializeField]
		internal float m_YAdvance;
	}
}
