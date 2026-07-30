using System;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000008 RID: 8
	[RequiredByNativeCode]
	[Serializable]
	public class Tile : TileBase
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000023B4 File Offset: 0x000005B4
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000023CC File Offset: 0x000005CC
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000023D8 File Offset: 0x000005D8
		// (set) Token: 0x06000028 RID: 40 RVA: 0x000023F0 File Offset: 0x000005F0
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000023FC File Offset: 0x000005FC
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002414 File Offset: 0x00000614
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

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002420 File Offset: 0x00000620
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002438 File Offset: 0x00000638
		public GameObject gameObject
		{
			get
			{
				return this.m_InstancedGameObject;
			}
			set
			{
				this.m_InstancedGameObject = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002444 File Offset: 0x00000644
		// (set) Token: 0x0600002E RID: 46 RVA: 0x0000245C File Offset: 0x0000065C
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

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002468 File Offset: 0x00000668
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002480 File Offset: 0x00000680
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

		// Token: 0x06000031 RID: 49 RVA: 0x0000248C File Offset: 0x0000068C
		public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
		{
			tileData.sprite = this.m_Sprite;
			tileData.color = this.m_Color;
			tileData.transform = this.m_Transform;
			tileData.gameObject = this.m_InstancedGameObject;
			tileData.flags = this.m_Flags;
			tileData.colliderType = this.m_ColliderType;
		}

		// Token: 0x04000015 RID: 21
		[SerializeField]
		private Sprite m_Sprite;

		// Token: 0x04000016 RID: 22
		[SerializeField]
		private Color m_Color = Color.white;

		// Token: 0x04000017 RID: 23
		[SerializeField]
		private Matrix4x4 m_Transform = Matrix4x4.identity;

		// Token: 0x04000018 RID: 24
		[SerializeField]
		private GameObject m_InstancedGameObject;

		// Token: 0x04000019 RID: 25
		[SerializeField]
		private TileFlags m_Flags = TileFlags.LockColor;

		// Token: 0x0400001A RID: 26
		[SerializeField]
		private Tile.ColliderType m_ColliderType = Tile.ColliderType.Sprite;

		// Token: 0x02000009 RID: 9
		public enum ColliderType
		{
			// Token: 0x0400001C RID: 28
			None,
			// Token: 0x0400001D RID: 29
			Sprite,
			// Token: 0x0400001E RID: 30
			Grid
		}
	}
}
