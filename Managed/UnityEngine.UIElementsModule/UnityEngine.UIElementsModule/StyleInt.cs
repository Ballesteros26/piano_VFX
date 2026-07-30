using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B5 RID: 437
	public struct StyleInt : IStyleValue<int>, IEquatable<StyleInt>
	{
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x000346C8 File Offset: 0x000328C8
		// (set) Token: 0x06000D4F RID: 3407 RVA: 0x000346EB File Offset: 0x000328EB
		public int value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : 0;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x000346FC File Offset: 0x000328FC
		// (set) Token: 0x06000D51 RID: 3409 RVA: 0x00034714 File Offset: 0x00032914
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

		// Token: 0x06000D52 RID: 3410 RVA: 0x0003471E File Offset: 0x0003291E
		public StyleInt(int v)
		{
			this = new StyleInt(v, StyleKeyword.Undefined);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0003472A File Offset: 0x0003292A
		public StyleInt(StyleKeyword keyword)
		{
			this = new StyleInt(0, keyword);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x00034736 File Offset: 0x00032936
		internal StyleInt(int v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x00034748 File Offset: 0x00032948
		public static bool operator ==(StyleInt lhs, StyleInt rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0003477C File Offset: 0x0003297C
		public static bool operator !=(StyleInt lhs, StyleInt rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x00034798 File Offset: 0x00032998
		public static implicit operator StyleInt(StyleKeyword keyword)
		{
			return new StyleInt(keyword);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x000347B0 File Offset: 0x000329B0
		public static implicit operator StyleInt(int v)
		{
			return new StyleInt(v);
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x000347C8 File Offset: 0x000329C8
		public bool Equals(StyleInt other)
		{
			return other == this;
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x000347E8 File Offset: 0x000329E8
		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleInt);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				StyleInt styleInt = (StyleInt)obj;
				flag2 = styleInt == this;
			}
			return flag2;
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x00034824 File Offset: 0x00032A24
		public override int GetHashCode()
		{
			int num = 917506989;
			num = num * -1521134295 + this.m_Keyword.GetHashCode();
			return num * -1521134295 + this.m_Value.GetHashCode();
		}

		// Token: 0x06000D5C RID: 3420 RVA: 0x0003486C File Offset: 0x00032A6C
		public override string ToString()
		{
			return this.DebugString<int>();
		}

		// Token: 0x04000541 RID: 1345
		private StyleKeyword m_Keyword;

		// Token: 0x04000542 RID: 1346
		private int m_Value;
	}
}
