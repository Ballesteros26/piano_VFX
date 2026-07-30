using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000046 RID: 70
	[UsedByNativeCode]
	[Serializable]
	internal struct GlyphValueRecord
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x0001A79C File Offset: 0x0001899C
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x0001A7B4 File Offset: 0x000189B4
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

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0001A7C0 File Offset: 0x000189C0
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x0001A7D8 File Offset: 0x000189D8
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

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x0001A7E4 File Offset: 0x000189E4
		// (set) Token: 0x060001C9 RID: 457 RVA: 0x0001A7FC File Offset: 0x000189FC
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

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001CA RID: 458 RVA: 0x0001A808 File Offset: 0x00018A08
		// (set) Token: 0x060001CB RID: 459 RVA: 0x0001A820 File Offset: 0x00018A20
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

		// Token: 0x060001CC RID: 460 RVA: 0x0001A82A File Offset: 0x00018A2A
		public GlyphValueRecord(float xPlacement, float yPlacement, float xAdvance, float yAdvance)
		{
			this.m_XPlacement = xPlacement;
			this.m_YPlacement = yPlacement;
			this.m_XAdvance = xAdvance;
			this.m_YAdvance = yAdvance;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0001A84C File Offset: 0x00018A4C
		public static GlyphValueRecord operator +(GlyphValueRecord a, GlyphValueRecord b)
		{
			GlyphValueRecord glyphValueRecord;
			glyphValueRecord.m_XPlacement = a.xPlacement + b.xPlacement;
			glyphValueRecord.m_YPlacement = a.yPlacement + b.yPlacement;
			glyphValueRecord.m_XAdvance = a.xAdvance + b.xAdvance;
			glyphValueRecord.m_YAdvance = a.yAdvance + b.yAdvance;
			return glyphValueRecord;
		}

		// Token: 0x04000381 RID: 897
		[SerializeField]
		[NativeName("xPlacement")]
		private float m_XPlacement;

		// Token: 0x04000382 RID: 898
		[SerializeField]
		[NativeName("yPlacement")]
		private float m_YPlacement;

		// Token: 0x04000383 RID: 899
		[SerializeField]
		[NativeName("xAdvance")]
		private float m_XAdvance;

		// Token: 0x04000384 RID: 900
		[SerializeField]
		[NativeName("yAdvance")]
		private float m_YAdvance;
	}
}
