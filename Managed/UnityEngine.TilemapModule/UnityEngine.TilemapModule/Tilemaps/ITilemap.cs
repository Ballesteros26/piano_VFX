using System;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x02000007 RID: 7
	[RequiredByNativeCode]
	public class ITilemap
	{
		// Token: 0x06000016 RID: 22 RVA: 0x0000220A File Offset: 0x0000040A
		internal ITilemap()
		{
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002214 File Offset: 0x00000414
		internal void SetTilemapInstance(Tilemap tilemap)
		{
			this.m_Tilemap = tilemap;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002220 File Offset: 0x00000420
		public Vector3Int origin
		{
			get
			{
				return this.m_Tilemap.origin;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002240 File Offset: 0x00000440
		public Vector3Int size
		{
			get
			{
				return this.m_Tilemap.size;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002260 File Offset: 0x00000460
		public Bounds localBounds
		{
			get
			{
				return this.m_Tilemap.localBounds;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002280 File Offset: 0x00000480
		public BoundsInt cellBounds
		{
			get
			{
				return this.m_Tilemap.cellBounds;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000022A0 File Offset: 0x000004A0
		public virtual Sprite GetSprite(Vector3Int position)
		{
			return this.m_Tilemap.GetSprite(position);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000022C0 File Offset: 0x000004C0
		public virtual Color GetColor(Vector3Int position)
		{
			return this.m_Tilemap.GetColor(position);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000022E0 File Offset: 0x000004E0
		public virtual Matrix4x4 GetTransformMatrix(Vector3Int position)
		{
			return this.m_Tilemap.GetTransformMatrix(position);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002300 File Offset: 0x00000500
		public virtual TileFlags GetTileFlags(Vector3Int position)
		{
			return this.m_Tilemap.GetTileFlags(position);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002320 File Offset: 0x00000520
		public virtual TileBase GetTile(Vector3Int position)
		{
			return this.m_Tilemap.GetTile(position);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002340 File Offset: 0x00000540
		public virtual T GetTile<T>(Vector3Int position) where T : TileBase
		{
			return this.m_Tilemap.GetTile<T>(position);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000235E File Offset: 0x0000055E
		public void RefreshTile(Vector3Int position)
		{
			this.m_Tilemap.RefreshTile(position);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002370 File Offset: 0x00000570
		public T GetComponent<T>()
		{
			return this.m_Tilemap.GetComponent<T>();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002390 File Offset: 0x00000590
		[RequiredByNativeCode]
		private static ITilemap CreateInstance()
		{
			ITilemap.s_Instance = new ITilemap();
			return ITilemap.s_Instance;
		}

		// Token: 0x04000013 RID: 19
		internal static ITilemap s_Instance;

		// Token: 0x04000014 RID: 20
		internal Tilemap m_Tilemap;
	}
}
