using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000012 RID: 18
	[RequiredByNativeCode]
	[NativeType(Header = "Modules/Tilemap/TilemapScripting.h")]
	public struct TileData
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00002A08 File Offset: 0x00000C08
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00002A20 File Offset: 0x00000C20
		public Sprite sprite
		{
			get
			{
				return this.m_Sprite;
			}
			set
			{
				this.m_Sprite = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00002A2C File Offset: 0x00000C2C
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00002A44 File Offset: 0x00000C44
		public Color color
		{
			get
			{
				return this.m_Color;
			}
			set
			{
				this.m_Color = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002A50 File Offset: 0x00000C50
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00002A68 File Offset: 0x00000C68
		public Matrix4x4 transform
		{
			get
			{
				return this.m_Transform;
			}
			set
			{
				this.m_Transform = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002A74 File Offset: 0x00000C74
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00002A8C File Offset: 0x00000C8C
		public GameObject gameObject
		{
			get
			{
				return this.m_GameObject;
			}
			set
			{
				this.m_GameObject = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00002A98 File Offset: 0x00000C98
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00002AB0 File Offset: 0x00000CB0
		public TileFlags flags
		{
			get
			{
				return this.m_Flags;
			}
			set
			{
				this.m_Flags = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00002ABC File Offset: 0x00000CBC
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public Tile.ColliderType colliderType
		{
			get
			{
				return this.m_ColliderType;
			}
			set
			{
				this.m_ColliderType = value;
			}
		}

		// Token: 0x04000038 RID: 56
		private Sprite m_Sprite;

		// Token: 0x04000039 RID: 57
		private Color m_Color;

		// Token: 0x0400003A RID: 58
		private Matrix4x4 m_Transform;

		// Token: 0x0400003B RID: 59
		private GameObject m_GameObject;

		// Token: 0x0400003C RID: 60
		private TileFlags m_Flags;

		// Token: 0x0400003D RID: 61
		private Tile.ColliderType m_ColliderType;
	}
}
