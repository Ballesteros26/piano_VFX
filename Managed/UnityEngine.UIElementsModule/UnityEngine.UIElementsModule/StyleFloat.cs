using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B3 RID: 435
	public struct StyleFloat : IStyleValue<float>, IEquatable<StyleFloat>
	{
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x00034308 File Offset: 0x00032508
		// (set) Token: 0x06000D30 RID: 3376 RVA: 0x0003432F File Offset: 0x0003252F
		public float value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : 0f;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x00034340 File Offset: 0x00032540
		// (set) Token: 0x06000D32 RID: 3378 RVA: 0x00034358 File Offset: 0x00032558
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

		// Token: 0x06000D33 RID: 3379 RVA: 0x00034362 File Offset: 0x00032562
		public StyleFloat(float v)
		{
			this = new StyleFloat(v, StyleKeyword.Undefined);
		}

		// Token: 0x06000D34 RID: 3380 RVA: 0x0003436E File Offset: 0x0003256E
		public StyleFloat(StyleKeyword keyword)
		{
			this = new StyleFloat(0f, keyword);
		}

		// Token: 0x06000D35 RID: 3381 RVA: 0x0003437E File Offset: 0x0003257E
		internal StyleFloat(float v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x00034390 File Offset: 0x00032590
		public static bool operator ==(StyleFloat lhs, StyleFloat rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x000343C4 File Offset: 0x000325C4
		public static bool operator !=(StyleFloat lhs, StyleFloat rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x000343E0 File Offset: 0x000325E0
		public static implicit operator StyleFloat(StyleKeyword keyword)
		{
			return new StyleFloat(keyword);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x000343F8 File Offset: 0x000325F8
		public static implicit operator StyleFloat(float v)
		{
			return new StyleFloat(v);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x00034410 File Offset: 0x00032610
		public bool Equals(StyleFloat other)
		{
			return other == this;
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x00034430 File Offset: 0x00032630
		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleFloat);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				StyleFloat styleFloat = (StyleFloat)obj;
				flag2 = styleFloat == this;
			}
			return flag2;
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0003446C File Offset: 0x0003266C
		public override int GetHashCode()
		{
			int num = 917506989;
			num = num * -1521134295 + this.m_Keyword.GetHashCode();
			return num * -1521134295 + this.m_Value.GetHashCode();
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x000344B4 File Offset: 0x000326B4
		public override string ToString()
		{
			return this.DebugString<float>();
		}

		// Token: 0x0400053D RID: 1341
		private StyleKeyword m_Keyword;

		// Token: 0x0400053E RID: 1342
		private float m_Value;
	}
}
