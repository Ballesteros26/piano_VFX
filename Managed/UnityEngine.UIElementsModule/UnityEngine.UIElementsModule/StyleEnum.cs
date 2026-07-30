using System;
using Unity.Collections.LowLevel.Unsafe;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B2 RID: 434
	public struct StyleEnum<T> : IStyleValue<T>, IEquatable<StyleEnum<T>> where T : struct, IConvertible
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x0003411C File Offset: 0x0003231C
		// (set) Token: 0x06000D21 RID: 3361 RVA: 0x00034147 File Offset: 0x00032347
		public T value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(T);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00034158 File Offset: 0x00032358
		// (set) Token: 0x06000D23 RID: 3363 RVA: 0x00034170 File Offset: 0x00032370
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

		// Token: 0x06000D24 RID: 3364 RVA: 0x0003417A File Offset: 0x0003237A
		public StyleEnum(T v)
		{
			this = new StyleEnum<T>(v, StyleKeyword.Undefined);
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00034188 File Offset: 0x00032388
		public StyleEnum(StyleKeyword keyword)
		{
			this = new StyleEnum<T>(default(T), keyword);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000341A7 File Offset: 0x000323A7
		internal StyleEnum(T v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x000341B8 File Offset: 0x000323B8
		public static bool operator ==(StyleEnum<T> lhs, StyleEnum<T> rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && UnsafeUtility.EnumEquals<T>(lhs.m_Value, rhs.m_Value);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x000341EC File Offset: 0x000323EC
		public static bool operator !=(StyleEnum<T> lhs, StyleEnum<T> rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x00034208 File Offset: 0x00032408
		public static implicit operator StyleEnum<T>(StyleKeyword keyword)
		{
			return new StyleEnum<T>(keyword);
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x00034220 File Offset: 0x00032420
		public static implicit operator StyleEnum<T>(T v)
		{
			return new StyleEnum<T>(v);
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x00034238 File Offset: 0x00032438
		public bool Equals(StyleEnum<T> other)
		{
			return other == this;
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x00034258 File Offset: 0x00032458
		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleEnum<T>);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				StyleEnum<T> styleEnum = (StyleEnum<T>)obj;
				flag2 = styleEnum == this;
			}
			return flag2;
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x00034294 File Offset: 0x00032494
		public override int GetHashCode()
		{
			int num = 917506989;
			num = num * -1521134295 + this.m_Keyword.GetHashCode();
			return num * -1521134295 + this.m_Value.GetHashCode();
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x000342E4 File Offset: 0x000324E4
		public override string ToString()
		{
			return this.DebugString<T>();
		}

		// Token: 0x0400053B RID: 1339
		private StyleKeyword m_Keyword;

		// Token: 0x0400053C RID: 1340
		private T m_Value;
	}
}
