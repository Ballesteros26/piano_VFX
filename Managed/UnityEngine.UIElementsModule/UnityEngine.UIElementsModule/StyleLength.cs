using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B6 RID: 438
	public struct StyleLength : IStyleValue<Length>, IEquatable<StyleLength>
	{
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x00034890 File Offset: 0x00032A90
		// (set) Token: 0x06000D5E RID: 3422 RVA: 0x000348BB File Offset: 0x00032ABB
		public Length value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Length);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x000348CC File Offset: 0x00032ACC
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x000348E4 File Offset: 0x00032AE4
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

		// Token: 0x06000D61 RID: 3425 RVA: 0x000348EE File Offset: 0x00032AEE
		public StyleLength(float v)
		{
			this = new StyleLength(new Length(v, LengthUnit.Pixel), StyleKeyword.Undefined);
		}

		// Token: 0x06000D62 RID: 3426 RVA: 0x00034900 File Offset: 0x00032B00
		public StyleLength(Length v)
		{
			this = new StyleLength(v, StyleKeyword.Undefined);
		}

		// Token: 0x06000D63 RID: 3427 RVA: 0x0003490C File Offset: 0x00032B0C
		public StyleLength(StyleKeyword keyword)
		{
			this = new StyleLength(default(Length), keyword);
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0003492B File Offset: 0x00032B2B
		internal StyleLength(Length v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06000D65 RID: 3429 RVA: 0x0003493C File Offset: 0x00032B3C
		public static bool operator ==(StyleLength lhs, StyleLength rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06000D66 RID: 3430 RVA: 0x00034970 File Offset: 0x00032B70
		public static bool operator !=(StyleLength lhs, StyleLength rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0003498C File Offset: 0x00032B8C
		public static implicit operator StyleLength(StyleKeyword keyword)
		{
			return new StyleLength(keyword);
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x000349A4 File Offset: 0x00032BA4
		public static implicit operator StyleLength(float v)
		{
			return new StyleLength(v);
		}

		// Token: 0x06000D69 RID: 3433 RVA: 0x000349BC File Offset: 0x00032BBC
		public static implicit operator StyleLength(Length v)
		{
			return new StyleLength(v);
		}

		// Token: 0x06000D6A RID: 3434 RVA: 0x000349D4 File Offset: 0x00032BD4
		public bool Equals(StyleLength other)
		{
			return other == this;
		}

		// Token: 0x06000D6B RID: 3435 RVA: 0x000349F4 File Offset: 0x00032BF4
		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleLength);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				StyleLength styleLength = (StyleLength)obj;
				flag2 = styleLength == this;
			}
			return flag2;
		}

		// Token: 0x06000D6C RID: 3436 RVA: 0x00034A30 File Offset: 0x00032C30
		public override int GetHashCode()
		{
			int num = -1977396678;
			num = num * -1521134295 + this.m_Keyword.GetHashCode();
			return num * -1521134295 + this.m_Value.GetHashCode();
		}

		// Token: 0x06000D6D RID: 3437 RVA: 0x00034A80 File Offset: 0x00032C80
		public override string ToString()
		{
			return this.DebugString<Length>();
		}

		// Token: 0x04000543 RID: 1347
		private StyleKeyword m_Keyword;

		// Token: 0x04000544 RID: 1348
		private Length m_Value;
	}
}
