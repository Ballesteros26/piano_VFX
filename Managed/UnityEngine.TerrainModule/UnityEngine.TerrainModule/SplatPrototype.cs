using System;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	[UsedByNativeCode]
	[StructLayout(0)]
	public sealed class SplatPrototype
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002960 File Offset: 0x00000B60
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00002978 File Offset: 0x00000B78
		public Texture2D texture
		{
			get
			{
				return this.m_Texture;
			}
			set
			{
				this.m_Texture = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00002984 File Offset: 0x00000B84
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x0000299C File Offset: 0x00000B9C
		public Texture2D normalMap
		{
			get
			{
				return this.m_NormalMap;
			}
			set
			{
				this.m_NormalMap = value;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000029A8 File Offset: 0x00000BA8
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x000029C0 File Offset: 0x00000BC0
		public Vector2 tileSize
		{
			get
			{
				return this.m_TileSize;
			}
			set
			{
				this.m_TileSize = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000029E4 File Offset: 0x00000BE4
		public Vector2 tileOffset
		{
			get
			{
				return this.m_TileOffset;
			}
			set
			{
				this.m_TileOffset = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000029F0 File Offset: 0x00000BF0
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00002A28 File Offset: 0x00000C28
		public Color specular
		{
			get
			{
				return new Color(this.m_SpecularMetallic.x, this.m_SpecularMetallic.y, this.m_SpecularMetallic.z);
			}
			set
			{
				this.m_SpecularMetallic.x = value.r;
				this.m_SpecularMetallic.y = value.g;
				this.m_SpecularMetallic.z = value.b;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002A60 File Offset: 0x00000C60
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002A7D File Offset: 0x00000C7D
		public float metallic
		{
			get
			{
				return this.m_SpecularMetallic.w;
			}
			set
			{
				this.m_SpecularMetallic.w = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002A8C File Offset: 0x00000C8C
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00002AA4 File Offset: 0x00000CA4
		public float smoothness
		{
			get
			{
				return this.m_Smoothness;
			}
			set
			{
				this.m_Smoothness = value;
			}
		}

		// Token: 0x0400002B RID: 43
		internal Texture2D m_Texture;

		// Token: 0x0400002C RID: 44
		internal Texture2D m_NormalMap;

		// Token: 0x0400002D RID: 45
		internal Vector2 m_TileSize = new Vector2(15f, 15f);

		// Token: 0x0400002E RID: 46
		internal Vector2 m_TileOffset = new Vector2(0f, 0f);

		// Token: 0x0400002F RID: 47
		internal Vector4 m_SpecularMetallic = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x04000030 RID: 48
		internal float m_Smoothness = 0f;
	}
}
