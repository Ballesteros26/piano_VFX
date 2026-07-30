using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B0 RID: 432
	public struct StyleCursor : IStyleValue<Cursor>, IEquatable<StyleCursor>
	{
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x000324F8 File Offset: 0x000306F8
		// (set) Token: 0x06000D02 RID: 3330 RVA: 0x00032523 File Offset: 0x00030723
		public Cursor value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : default(Cursor);
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00032534 File Offset: 0x00030734
		// (set) Token: 0x06000D04 RID: 3332 RVA: 0x0003254C File Offset: 0x0003074C
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

		// Token: 0x06000D05 RID: 3333 RVA: 0x00032556 File Offset: 0x00030756
		public StyleCursor(Cursor v)
		{
			this = new StyleCursor(v, StyleKeyword.Undefined);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x00032564 File Offset: 0x00030764
		public StyleCursor(StyleKeyword keyword)
		{
			this = new StyleCursor(default(Cursor), keyword);
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x00032583 File Offset: 0x00030783
		internal StyleCursor(Cursor v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x00032594 File Offset: 0x00030794
		public static bool operator ==(StyleCursor lhs, StyleCursor rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x000325C8 File Offset: 0x000307C8
		public static bool operator !=(StyleCursor lhs, StyleCursor rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x000325E4 File Offset: 0x000307E4
		public static implicit operator StyleCursor(StyleKeyword keyword)
		{
			return new StyleCursor(keyword);
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x000325FC File Offset: 0x000307FC
		public static implicit operator StyleCursor(Cursor v)
		{
			return new StyleCursor(v);
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x00032614 File Offset: 0x00030814
		public bool Equals(StyleCursor other)
		{
			return other == this;
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x00032634 File Offset: 0x00030834
		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleCursor);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				StyleCursor styleCursor = (StyleCursor)obj;
				flag2 = styleCursor == this;
			}
			return flag2;
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x00032670 File Offset: 0x00030870
		public override int GetHashCode()
		{
			int num = 917506989;
			num = num * -1521134295 + this.m_Keyword.GetHashCode();
			return num * -1521134295 + this.m_Value.GetHashCode();
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x000326C0 File Offset: 0x000308C0
		public override string ToString()
		{
			return this.DebugString<Cursor>();
		}

		// Token: 0x04000535 RID: 1333
		private StyleKeyword m_Keyword;

		// Token: 0x04000536 RID: 1334
		private Cursor m_Value;
	}
}
