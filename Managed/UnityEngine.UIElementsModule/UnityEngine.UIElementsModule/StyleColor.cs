using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001AF RID: 431
	public struct StyleColor : IStyleValue<Color>, IEquatable<StyleColor>
	{
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x000322E0 File Offset: 0x000304E0
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x00032307 File Offset: 0x00030507
		public Color value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : Color.clear;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x00032318 File Offset: 0x00030518
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x00032330 File Offset: 0x00030530
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

		// Token: 0x06000CF4 RID: 3316 RVA: 0x0003233A File Offset: 0x0003053A
		public StyleColor(Color v)
		{
			this = new StyleColor(v, StyleKeyword.Undefined);
		}

		// Token: 0x06000CF5 RID: 3317 RVA: 0x00032346 File Offset: 0x00030546
		public StyleColor(StyleKeyword keyword)
		{
			this = new StyleColor(Color.clear, keyword);
		}

		// Token: 0x06000CF6 RID: 3318 RVA: 0x00032356 File Offset: 0x00030556
		internal StyleColor(Color v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x00032368 File Offset: 0x00030568
		public static bool operator ==(StyleColor lhs, StyleColor rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06000CF8 RID: 3320 RVA: 0x0003239C File Offset: 0x0003059C
		public static bool operator !=(StyleColor lhs, StyleColor rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000CF9 RID: 3321 RVA: 0x000323B8 File Offset: 0x000305B8
		public static bool operator ==(StyleColor lhs, Color rhs)
		{
			StyleColor styleColor = new StyleColor(rhs);
			return lhs == styleColor;
		}

		// Token: 0x06000CFA RID: 3322 RVA: 0x000323DC File Offset: 0x000305DC
		public static bool operator !=(StyleColor lhs, Color rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000CFB RID: 3323 RVA: 0x000323F8 File Offset: 0x000305F8
		public static implicit operator StyleColor(StyleKeyword keyword)
		{
			return new StyleColor(keyword);
		}

		// Token: 0x06000CFC RID: 3324 RVA: 0x00032410 File Offset: 0x00030610
		public static implicit operator StyleColor(Color v)
		{
			return new StyleColor(v);
		}

		// Token: 0x06000CFD RID: 3325 RVA: 0x00032428 File Offset: 0x00030628
		public bool Equals(StyleColor other)
		{
			return other == this;
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x00032448 File Offset: 0x00030648
		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleColor);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				StyleColor styleColor = (StyleColor)obj;
				flag2 = styleColor == this;
			}
			return flag2;
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x00032484 File Offset: 0x00030684
		public override int GetHashCode()
		{
			int num = 917506989;
			num = num * -1521134295 + this.m_Keyword.GetHashCode();
			return num * -1521134295 + this.m_Value.GetHashCode();
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x000324D4 File Offset: 0x000306D4
		public override string ToString()
		{
			return this.DebugString<Color>();
		}

		// Token: 0x04000533 RID: 1331
		private StyleKeyword m_Keyword;

		// Token: 0x04000534 RID: 1332
		private Color m_Value;
	}
}
