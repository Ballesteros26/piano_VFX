using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore
{
	// Token: 0x0200000B RID: 11
	[UsedByNativeCode]
	[Serializable]
	public struct GlyphRect : IEquatable<GlyphRect>
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00004B00 File Offset: 0x00002D00
		// (set) Token: 0x06000087 RID: 135 RVA: 0x00004B18 File Offset: 0x00002D18
		public int x
		{
			get
			{
				return this.m_X;
			}
			set
			{
				this.m_X = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00004B24 File Offset: 0x00002D24
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00004B3C File Offset: 0x00002D3C
		public int y
		{
			get
			{
				return this.m_Y;
			}
			set
			{
				this.m_Y = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00004B48 File Offset: 0x00002D48
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00004B60 File Offset: 0x00002D60
		public int width
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

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004B6C File Offset: 0x00002D6C
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00004B84 File Offset: 0x00002D84
		public int height
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

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004B90 File Offset: 0x00002D90
		public static GlyphRect zero
		{
			get
			{
				return GlyphRect.s_ZeroGlyphRect;
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004BA7 File Offset: 0x00002DA7
		public GlyphRect(int x, int y, int width, int height)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004BC7 File Offset: 0x00002DC7
		public GlyphRect(Rect rect)
		{
			this.m_X = (int)rect.x;
			this.m_Y = (int)rect.y;
			this.m_Width = (int)rect.width;
			this.m_Height = (int)rect.height;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004C04 File Offset: 0x00002E04
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004C28 File Offset: 0x00002E28
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004C4C File Offset: 0x00002E4C
		public bool Equals(GlyphRect other)
		{
			return base.Equals(other);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004C74 File Offset: 0x00002E74
		public static bool operator ==(GlyphRect lhs, GlyphRect rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004CCC File Offset: 0x00002ECC
		public static bool operator !=(GlyphRect lhs, GlyphRect rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x04000054 RID: 84
		[NativeName("x")]
		[SerializeField]
		private int m_X;

		// Token: 0x04000055 RID: 85
		[SerializeField]
		[NativeName("y")]
		private int m_Y;

		// Token: 0x04000056 RID: 86
		[SerializeField]
		[NativeName("width")]
		private int m_Width;

		// Token: 0x04000057 RID: 87
		[SerializeField]
		[NativeName("height")]
		private int m_Height;

		// Token: 0x04000058 RID: 88
		private static readonly GlyphRect s_ZeroGlyphRect = new GlyphRect(0, 0, 0, 0);
	}
}
