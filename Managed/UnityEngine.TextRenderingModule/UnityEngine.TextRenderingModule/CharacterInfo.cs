using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	[UsedByNativeCode]
	public struct CharacterInfo
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000029A8 File Offset: 0x00000BA8
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000029C8 File Offset: 0x00000BC8
		public int advance
		{
			get
			{
				return (int)Math.Round((double)this.width, 1);
			}
			set
			{
				this.width = (float)value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000029D4 File Offset: 0x00000BD4
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000029F2 File Offset: 0x00000BF2
		public int glyphWidth
		{
			get
			{
				return (int)this.vert.width;
			}
			set
			{
				this.vert.width = (float)value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002A04 File Offset: 0x00000C04
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002A24 File Offset: 0x00000C24
		public int glyphHeight
		{
			get
			{
				return (int)(-(int)this.vert.height);
			}
			set
			{
				float height = this.vert.height;
				this.vert.height = (float)(-(float)value);
				this.vert.y = this.vert.y + (height - this.vert.height);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002A70 File Offset: 0x00000C70
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002A8E File Offset: 0x00000C8E
		public int bearing
		{
			get
			{
				return (int)this.vert.x;
			}
			set
			{
				this.vert.x = (float)value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002AA0 File Offset: 0x00000CA0
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002ACA File Offset: 0x00000CCA
		public int minY
		{
			get
			{
				return (int)(this.vert.y + this.vert.height);
			}
			set
			{
				this.vert.height = (float)value - this.vert.y;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002AE8 File Offset: 0x00000CE8
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002B08 File Offset: 0x00000D08
		public int maxY
		{
			get
			{
				return (int)this.vert.y;
			}
			set
			{
				float y = this.vert.y;
				this.vert.y = (float)value;
				this.vert.height = this.vert.height + (y - this.vert.y);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002B50 File Offset: 0x00000D50
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002B70 File Offset: 0x00000D70
		public int minX
		{
			get
			{
				return (int)this.vert.x;
			}
			set
			{
				float x = this.vert.x;
				this.vert.x = (float)value;
				this.vert.width = this.vert.width + (x - this.vert.x);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002BB8 File Offset: 0x00000DB8
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002BE2 File Offset: 0x00000DE2
		public int maxX
		{
			get
			{
				return (int)(this.vert.x + this.vert.width);
			}
			set
			{
				this.vert.width = (float)value - this.vert.x;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002C00 File Offset: 0x00000E00
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002C30 File Offset: 0x00000E30
		internal Vector2 uvBottomLeftUnFlipped
		{
			get
			{
				return new Vector2(this.uv.x, this.uv.y);
			}
			set
			{
				Vector2 uvTopRightUnFlipped = this.uvTopRightUnFlipped;
				this.uv.x = value.x;
				this.uv.y = value.y;
				this.uv.width = uvTopRightUnFlipped.x - this.uv.x;
				this.uv.height = uvTopRightUnFlipped.y - this.uv.y;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002CA8 File Offset: 0x00000EA8
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002CE4 File Offset: 0x00000EE4
		internal Vector2 uvBottomRightUnFlipped
		{
			get
			{
				return new Vector2(this.uv.x + this.uv.width, this.uv.y);
			}
			set
			{
				Vector2 uvTopRightUnFlipped = this.uvTopRightUnFlipped;
				this.uv.width = value.x - this.uv.x;
				this.uv.y = value.y;
				this.uv.height = uvTopRightUnFlipped.y - this.uv.y;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002D48 File Offset: 0x00000F48
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002D8D File Offset: 0x00000F8D
		internal Vector2 uvTopRightUnFlipped
		{
			get
			{
				return new Vector2(this.uv.x + this.uv.width, this.uv.y + this.uv.height);
			}
			set
			{
				this.uv.width = value.x - this.uv.x;
				this.uv.height = value.y - this.uv.y;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002DCC File Offset: 0x00000FCC
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002E08 File Offset: 0x00001008
		internal Vector2 uvTopLeftUnFlipped
		{
			get
			{
				return new Vector2(this.uv.x, this.uv.y + this.uv.height);
			}
			set
			{
				Vector2 uvTopRightUnFlipped = this.uvTopRightUnFlipped;
				this.uv.x = value.x;
				this.uv.height = value.y - this.uv.y;
				this.uv.width = uvTopRightUnFlipped.x - this.uv.x;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002E6C File Offset: 0x0000106C
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002E84 File Offset: 0x00001084
		public Vector2 uvBottomLeft
		{
			get
			{
				return this.uvBottomLeftUnFlipped;
			}
			set
			{
				this.uvBottomLeftUnFlipped = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002E90 File Offset: 0x00001090
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002EB8 File Offset: 0x000010B8
		public Vector2 uvBottomRight
		{
			get
			{
				return this.flipped ? this.uvTopLeftUnFlipped : this.uvBottomRightUnFlipped;
			}
			set
			{
				bool flag = this.flipped;
				if (flag)
				{
					this.uvTopLeftUnFlipped = value;
				}
				else
				{
					this.uvBottomRightUnFlipped = value;
				}
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002EE4 File Offset: 0x000010E4
		// (set) Token: 0x0600007A RID: 122 RVA: 0x00002EFC File Offset: 0x000010FC
		public Vector2 uvTopRight
		{
			get
			{
				return this.uvTopRightUnFlipped;
			}
			set
			{
				this.uvTopRightUnFlipped = value;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002F08 File Offset: 0x00001108
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002F30 File Offset: 0x00001130
		public Vector2 uvTopLeft
		{
			get
			{
				return this.flipped ? this.uvBottomRightUnFlipped : this.uvTopLeftUnFlipped;
			}
			set
			{
				bool flag = this.flipped;
				if (flag)
				{
					this.uvBottomRightUnFlipped = value;
				}
				else
				{
					this.uvTopLeftUnFlipped = value;
				}
			}
		}

		// Token: 0x0400003C RID: 60
		public int index;

		// Token: 0x0400003D RID: 61
		[Obsolete("CharacterInfo.uv is deprecated. Use uvBottomLeft, uvBottomRight, uvTopRight or uvTopLeft instead.")]
		public Rect uv;

		// Token: 0x0400003E RID: 62
		[Obsolete("CharacterInfo.vert is deprecated. Use minX, maxX, minY, maxY instead.")]
		public Rect vert;

		// Token: 0x0400003F RID: 63
		[NativeName("advance")]
		[Obsolete("CharacterInfo.width is deprecated. Use advance instead.")]
		public float width;

		// Token: 0x04000040 RID: 64
		public int size;

		// Token: 0x04000041 RID: 65
		public FontStyle style;

		// Token: 0x04000042 RID: 66
		[Obsolete("CharacterInfo.flipped is deprecated. Use uvBottomLeft, uvBottomRight, uvTopRight or uvTopLeft instead, which will be correct regardless of orientation.")]
		public bool flipped;
	}
}
