using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore
{
	// Token: 0x0200000C RID: 12
	[UsedByNativeCode]
	[Serializable]
	public struct GlyphMetrics : IEquatable<GlyphMetrics>
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004CF8 File Offset: 0x00002EF8
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00004D10 File Offset: 0x00002F10
		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004D1C File Offset: 0x00002F1C
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00004D34 File Offset: 0x00002F34
		public float height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004D40 File Offset: 0x00002F40
		// (set) Token: 0x0600009C RID: 156 RVA: 0x00004D58 File Offset: 0x00002F58
		public float horizontalBearingX
		{
			get
			{
				return this.m_HorizontalBearingX;
			}
			set
			{
				this.m_HorizontalBearingX = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00004D64 File Offset: 0x00002F64
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00004D7C File Offset: 0x00002F7C
		public float horizontalBearingY
		{
			get
			{
				return this.m_HorizontalBearingY;
			}
			set
			{
				this.m_HorizontalBearingY = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004D88 File Offset: 0x00002F88
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00004DA0 File Offset: 0x00002FA0
		public float horizontalAdvance
		{
			get
			{
				return this.m_HorizontalAdvance;
			}
			set
			{
				this.m_HorizontalAdvance = value;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004DAA File Offset: 0x00002FAA
		public GlyphMetrics(float width, float height, float bearingX, float bearingY, float advance)
		{
			this.m_Width = width;
			this.m_Height = height;
			this.m_HorizontalBearingX = bearingX;
			this.m_HorizontalBearingY = bearingY;
			this.m_HorizontalAdvance = advance;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004DD4 File Offset: 0x00002FD4
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004DF8 File Offset: 0x00002FF8
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004E1C File Offset: 0x0000301C
		public bool Equals(GlyphMetrics other)
		{
			return base.Equals(other);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004E44 File Offset: 0x00003044
		public static bool operator ==(GlyphMetrics lhs, GlyphMetrics rhs)
		{
			return lhs.width == rhs.width && lhs.height == rhs.height && lhs.horizontalBearingX == rhs.horizontalBearingX && lhs.horizontalBearingY == rhs.horizontalBearingY && lhs.horizontalAdvance == rhs.horizontalAdvance;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004EAC File Offset: 0x000030AC
		public static bool operator !=(GlyphMetrics lhs, GlyphMetrics rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x04000059 RID: 89
		[NativeName("width")]
		[SerializeField]
		private float m_Width;

		// Token: 0x0400005A RID: 90
		[NativeName("height")]
		[SerializeField]
		private float m_Height;

		// Token: 0x0400005B RID: 91
		[NativeName("horizontalBearingX")]
		[SerializeField]
		private float m_HorizontalBearingX;

		// Token: 0x0400005C RID: 92
		[NativeName("horizontalBearingY")]
		[SerializeField]
		private float m_HorizontalBearingY;

		// Token: 0x0400005D RID: 93
		[NativeName("horizontalAdvance")]
		[SerializeField]
		private float m_HorizontalAdvance;
	}
}
