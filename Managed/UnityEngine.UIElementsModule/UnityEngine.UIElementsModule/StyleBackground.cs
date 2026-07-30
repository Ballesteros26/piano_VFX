using System;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001AE RID: 430
	public struct StyleBackground : IStyleValue<Background>, IEquatable<StyleBackground>
	{
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x00032068 File Offset: 0x00030268
		// (set) Token: 0x06000CDC RID: 3292 RVA: 0x00032093 File Offset: 0x00030293
		public Background value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Background);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x000320A4 File Offset: 0x000302A4
		// (set) Token: 0x06000CDE RID: 3294 RVA: 0x000320BC File Offset: 0x000302BC
		public StyleKeyword keyword
		{
			get
			{
				return this.m_Keyword;
			}
			set
			{
				this.m_Keyword = value;
			}
		}

		// Token: 0x06000CDF RID: 3295 RVA: 0x000320C6 File Offset: 0x000302C6
		public StyleBackground(Background v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x000320D2 File Offset: 0x000302D2
		public StyleBackground(Texture2D v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		// Token: 0x06000CE1 RID: 3297 RVA: 0x000320DE File Offset: 0x000302DE
		public StyleBackground(VectorImage v)
		{
			this = new StyleBackground(v, StyleKeyword.Undefined);
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x000320EC File Offset: 0x000302EC
		public StyleBackground(StyleKeyword keyword)
		{
			this = new StyleBackground(default(Background), keyword);
		}

		// Token: 0x06000CE3 RID: 3299 RVA: 0x0003210B File Offset: 0x0003030B
		internal StyleBackground(Texture2D v, StyleKeyword keyword)
		{
			this = new StyleBackground(Background.FromTexture2D(v), keyword);
		}

		// Token: 0x06000CE4 RID: 3300 RVA: 0x0003211C File Offset: 0x0003031C
		internal StyleBackground(VectorImage v, StyleKeyword keyword)
		{
			this = new StyleBackground(Background.FromVectorImage(v), keyword);
		}

		// Token: 0x06000CE5 RID: 3301 RVA: 0x00032130 File Offset: 0x00030330
		internal StyleBackground(GCHandle gcHandle, StyleKeyword keyword)
		{
			this = new StyleBackground(gcHandle.IsAllocated ? Background.FromObject(gcHandle.Target) : default(Background), keyword);
		}

		// Token: 0x06000CE6 RID: 3302 RVA: 0x00032166 File Offset: 0x00030366
		internal StyleBackground(Background v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00032178 File Offset: 0x00030378
		public static bool operator ==(StyleBackground lhs, StyleBackground rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x000321AC File Offset: 0x000303AC
		public static bool operator !=(StyleBackground lhs, StyleBackground rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000CE9 RID: 3305 RVA: 0x000321C8 File Offset: 0x000303C8
		public static implicit operator StyleBackground(StyleKeyword keyword)
		{
			return new StyleBackground(keyword);
		}

		// Token: 0x06000CEA RID: 3306 RVA: 0x000321E0 File Offset: 0x000303E0
		public static implicit operator StyleBackground(Background v)
		{
			return new StyleBackground(v);
		}

		// Token: 0x06000CEB RID: 3307 RVA: 0x000321F8 File Offset: 0x000303F8
		public static implicit operator StyleBackground(Texture2D v)
		{
			return new StyleBackground(v);
		}

		// Token: 0x06000CEC RID: 3308 RVA: 0x00032210 File Offset: 0x00030410
		public bool Equals(StyleBackground other)
		{
			return other == this;
		}

		// Token: 0x06000CED RID: 3309 RVA: 0x00032230 File Offset: 0x00030430
		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleBackground);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				StyleBackground styleBackground = (StyleBackground)obj;
				flag2 = styleBackground == this;
			}
			return flag2;
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0003226C File Offset: 0x0003046C
		public override int GetHashCode()
		{
			int num = 917506989;
			num = num * -1521134295 + this.m_Keyword.GetHashCode();
			return num * -1521134295 + this.m_Value.GetHashCode();
		}

		// Token: 0x06000CEF RID: 3311 RVA: 0x000322BC File Offset: 0x000304BC
		public override string ToString()
		{
			return this.DebugString<Background>();
		}

		// Token: 0x04000531 RID: 1329
		private StyleKeyword m_Keyword;

		// Token: 0x04000532 RID: 1330
		private Background m_Value;
	}
}
