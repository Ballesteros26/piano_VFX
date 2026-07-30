using System;

namespace TMPro
{
	// Token: 0x0200000C RID: 12
	public struct TMP_Offset
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000027BC File Offset: 0x000009BC
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000027C4 File Offset: 0x000009C4
		public float left
		{
			get
			{
				return this.m_Left;
			}
			set
			{
				this.m_Left = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000027CD File Offset: 0x000009CD
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000027D5 File Offset: 0x000009D5
		public float right
		{
			get
			{
				return this.m_Right;
			}
			set
			{
				this.m_Right = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000027DE File Offset: 0x000009DE
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000027E6 File Offset: 0x000009E6
		public float top
		{
			get
			{
				return this.m_Top;
			}
			set
			{
				this.m_Top = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000027EF File Offset: 0x000009EF
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000027F7 File Offset: 0x000009F7
		public float bottom
		{
			get
			{
				return this.m_Bottom;
			}
			set
			{
				this.m_Bottom = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000027BC File Offset: 0x000009BC
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002800 File Offset: 0x00000A00
		public float horizontal
		{
			get
			{
				return this.m_Left;
			}
			set
			{
				this.m_Left = value;
				this.m_Right = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000027DE File Offset: 0x000009DE
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002810 File Offset: 0x00000A10
		public float vertical
		{
			get
			{
				return this.m_Top;
			}
			set
			{
				this.m_Top = value;
				this.m_Bottom = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002820 File Offset: 0x00000A20
		public static TMP_Offset zero
		{
			get
			{
				return TMP_Offset.k_ZeroOffset;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002827 File Offset: 0x00000A27
		public TMP_Offset(float left, float right, float top, float bottom)
		{
			this.m_Left = left;
			this.m_Right = right;
			this.m_Top = top;
			this.m_Bottom = bottom;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002846 File Offset: 0x00000A46
		public TMP_Offset(float horizontal, float vertical)
		{
			this.m_Left = horizontal;
			this.m_Right = horizontal;
			this.m_Top = vertical;
			this.m_Bottom = vertical;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002864 File Offset: 0x00000A64
		public static bool operator ==(TMP_Offset lhs, TMP_Offset rhs)
		{
			return lhs.m_Left == rhs.m_Left && lhs.m_Right == rhs.m_Right && lhs.m_Top == rhs.m_Top && lhs.m_Bottom == rhs.m_Bottom;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000028A0 File Offset: 0x00000AA0
		public static bool operator !=(TMP_Offset lhs, TMP_Offset rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000028AC File Offset: 0x00000AAC
		public static TMP_Offset operator *(TMP_Offset a, float b)
		{
			return new TMP_Offset(a.m_Left * b, a.m_Right * b, a.m_Top * b, a.m_Bottom * b);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000028D3 File Offset: 0x00000AD3
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000028E5 File Offset: 0x00000AE5
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000028F8 File Offset: 0x00000AF8
		public bool Equals(TMP_Offset other)
		{
			return base.Equals(other);
		}

		// Token: 0x04000023 RID: 35
		private float m_Left;

		// Token: 0x04000024 RID: 36
		private float m_Right;

		// Token: 0x04000025 RID: 37
		private float m_Top;

		// Token: 0x04000026 RID: 38
		private float m_Bottom;

		// Token: 0x04000027 RID: 39
		private static readonly TMP_Offset k_ZeroOffset = new TMP_Offset(0f, 0f, 0f, 0f);
	}
}
