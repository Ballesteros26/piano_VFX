using System;
using UnityEngine.Scripting;

namespace UnityEngine.Tilemaps
{
	// Token: 0x0200000A RID: 10
	[RequiredByNativeCode]
	public abstract class TileBase : ScriptableObject
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002515 File Offset: 0x00000715
		[RequiredByNativeCode]
		public virtual void RefreshTile(Vector3Int position, ITilemap tilemap)
		{
			tilemap.RefreshTile(position);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002101 File Offset: 0x00000301
		[RequiredByNativeCode]
		public virtual void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
		{
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002520 File Offset: 0x00000720
		private TileData GetTileDataNoRef(Vector3Int position, ITilemap tilemap)
		{
			TileData tileData = default(TileData);
			this.GetTileData(position, tilemap, ref tileData);
			return tileData;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002548 File Offset: 0x00000748
		[RequiredByNativeCode]
		public virtual bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
		{
			return false;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000255C File Offset: 0x0000075C
		private TileAnimationData GetTileAnimationDataNoRef(Vector3Int position, ITilemap tilemap)
		{
			TileAnimationData tileAnimationData = default(TileAnimationData);
			this.GetTileAnimationData(position, tilemap, ref tileAnimationData);
			return tileAnimationData;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002584 File Offset: 0x00000784
		[RequiredByNativeCode]
		public virtual bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
		{
			return false;
		}
	}
}
