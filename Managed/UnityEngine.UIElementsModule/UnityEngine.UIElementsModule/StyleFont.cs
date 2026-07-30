using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020001B4 RID: 436
	public struct StyleFont : IStyleValue<Font>, IEquatable<StyleFont>
	{
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000D3E RID: 3390 RVA: 0x000344D8 File Offset: 0x000326D8
		// (set) Token: 0x06000D3F RID: 3391 RVA: 0x000344FB File Offset: 0x000326FB
		public Font value
		{
			get
			{
				return (this.m_Keyword == StyleKeyword.Undefined) ? this.m_Value : null;
			}
			set
			{
				this.m_Value = value;
				this.m_Keyword = StyleKeyword.Undefined;
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000D40 RID: 3392 RVA: 0x0003450C File Offset: 0x0003270C
		// (set) Token: 0x06000D41 RID: 3393 RVA: 0x00034524 File Offset: 0x00032724
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

		// Token: 0x06000D42 RID: 3394 RVA: 0x0003452E File Offset: 0x0003272E
		public StyleFont(Font v)
		{
			this = new StyleFont(v, StyleKeyword.Undefined);
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0003453A File Offset: 0x0003273A
		public StyleFont(StyleKeyword keyword)
		{
			this = new StyleFont(null, keyword);
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x00034546 File Offset: 0x00032746
		internal StyleFont(GCHandle gcHandle, StyleKeyword keyword)
		{
			this = new StyleFont(gcHandle.IsAllocated ? (gcHandle.Target as Font) : null, keyword);
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x00034569 File Offset: 0x00032769
		internal StyleFont(Font v, StyleKeyword keyword)
		{
			this.m_Keyword = keyword;
			this.m_Value = v;
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0003457C File Offset: 0x0003277C
		public static bool operator ==(StyleFont lhs, StyleFont rhs)
		{
			return lhs.m_Keyword == rhs.m_Keyword && lhs.m_Value == rhs.m_Value;
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x000345B0 File Offset: 0x000327B0
		public static bool operator !=(StyleFont lhs, StyleFont rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x000345CC File Offset: 0x000327CC
		public static implicit operator StyleFont(StyleKeyword keyword)
		{
			return new StyleFont(keyword);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x000345E4 File Offset: 0x000327E4
		public static implicit operator StyleFont(Font v)
		{
			return new StyleFont(v);
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x000345FC File Offset: 0x000327FC
		public bool Equals(StyleFont other)
		{
			return other == this;
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0003461C File Offset: 0x0003281C
		public override bool Equals(object obj)
		{
			bool flag = !(obj is StyleFont);
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				StyleFont styleFont = (StyleFont)obj;
				flag2 = styleFont == this;
			}
			return flag2;
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00034658 File Offset: 0x00032858
		public override int GetHashCode()
		{
			int num = 917506989;
			num = num * -1521134295 + this.m_Keyword.GetHashCode();
			return num * -1521134295 + EqualityComparer<Font>.Default.GetHashCode(this.m_Value);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x000346A4 File Offset: 0x000328A4
		public override string ToString()
		{
			return this.DebugString<Font>();
		}

		// Token: 0x0400053F RID: 1343
		private StyleKeyword m_Keyword;

		// Token: 0x04000540 RID: 1344
		private Font m_Value;
	}
}
