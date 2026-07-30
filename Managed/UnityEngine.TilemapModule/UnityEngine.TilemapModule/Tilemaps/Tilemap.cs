using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Tilemaps
{
	// Token: 0x0200000B RID: 11
	[NativeHeader("Modules/Grid/Public/Grid.h")]
	[NativeHeader("Runtime/Graphics/SpriteFrame.h")]
	[NativeHeader("Modules/Tilemap/Public/TilemapTile.h")]
	[NativeHeader("Modules/Tilemap/Public/TilemapMarshalling.h")]
	[NativeType(Header = "Modules/Tilemap/Public/Tilemap.h")]
	[NativeHeader("Modules/Grid/Public/GridMarshalling.h")]
	[RequireComponent(typeof(Transform))]
	public sealed class Tilemap : GridLayout
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003A RID: 58
		public extern Grid layoutGrid
		{
			[NativeMethod(Name = "GetAttachedGrid")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002598 File Offset: 0x00000798
		public Vector3 GetCellCenterLocal(Vector3Int position)
		{
			return base.CellToLocalInterpolated(position + this.tileAnchor);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000025C4 File Offset: 0x000007C4
		public Vector3 GetCellCenterWorld(Vector3Int position)
		{
			return base.LocalToWorld(base.CellToLocalInterpolated(position + this.tileAnchor));
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000025F4 File Offset: 0x000007F4
		public BoundsInt cellBounds
		{
			get
			{
				return new BoundsInt(this.origin, this.size);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002618 File Offset: 0x00000818
		[NativeProperty("TilemapBoundsScripting")]
		public Bounds localBounds
		{
			get
			{
				Bounds bounds;
				this.get_localBounds_Injected(out bounds);
				return bounds;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002630 File Offset: 0x00000830
		[NativeProperty("TilemapFrameBoundsScripting")]
		internal Bounds localFrameBounds
		{
			get
			{
				Bounds bounds;
				this.get_localFrameBounds_Injected(out bounds);
				return bounds;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000040 RID: 64
		// (set) Token: 0x06000041 RID: 65
		public extern float animationFrameRate
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002648 File Offset: 0x00000848
		// (set) Token: 0x06000043 RID: 67 RVA: 0x0000265E File Offset: 0x0000085E
		public Color color
		{
			get
			{
				Color color;
				this.get_color_Injected(out color);
				return color;
			}
			set
			{
				this.set_color_Injected(ref value);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002668 File Offset: 0x00000868
		// (set) Token: 0x06000045 RID: 69 RVA: 0x0000267E File Offset: 0x0000087E
		public Vector3Int origin
		{
			get
			{
				Vector3Int vector3Int;
				this.get_origin_Injected(out vector3Int);
				return vector3Int;
			}
			set
			{
				this.set_origin_Injected(ref value);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002688 File Offset: 0x00000888
		// (set) Token: 0x06000047 RID: 71 RVA: 0x0000269E File Offset: 0x0000089E
		public Vector3Int size
		{
			get
			{
				Vector3Int vector3Int;
				this.get_size_Injected(out vector3Int);
				return vector3Int;
			}
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000026A8 File Offset: 0x000008A8
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000026BE File Offset: 0x000008BE
		[NativeProperty(Name = "TileAnchorScripting")]
		public Vector3 tileAnchor
		{
			get
			{
				Vector3 vector;
				this.get_tileAnchor_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_tileAnchor_Injected(ref value);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600004A RID: 74
		// (set) Token: 0x0600004B RID: 75
		public extern Tilemap.Orientation orientation
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000026C8 File Offset: 0x000008C8
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000026DE File Offset: 0x000008DE
		public Matrix4x4 orientationMatrix
		{
			[NativeMethod(Name = "GetTileOrientationMatrix")]
			get
			{
				Matrix4x4 matrix4x;
				this.get_orientationMatrix_Injected(out matrix4x);
				return matrix4x;
			}
			[NativeMethod(Name = "SetOrientationMatrix")]
			set
			{
				this.set_orientationMatrix_Injected(ref value);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000026E8 File Offset: 0x000008E8
		internal Object GetTileAsset(Vector3Int position)
		{
			return this.GetTileAsset_Injected(ref position);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000026F4 File Offset: 0x000008F4
		public TileBase GetTile(Vector3Int position)
		{
			return this.GetTileAsset(position) as TileBase;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002714 File Offset: 0x00000914
		public T GetTile<T>(Vector3Int position) where T : TileBase
		{
			return this.GetTileAsset(position) as T;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002737 File Offset: 0x00000937
		internal Object[] GetTileAssetsBlock(Vector3Int position, Vector3Int blockDimensions)
		{
			return this.GetTileAssetsBlock_Injected(ref position, ref blockDimensions);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002744 File Offset: 0x00000944
		public TileBase[] GetTilesBlock(BoundsInt bounds)
		{
			Object[] tileAssetsBlock = this.GetTileAssetsBlock(bounds.min, bounds.size);
			TileBase[] array = new TileBase[tileAssetsBlock.Length];
			for (int i = 0; i < tileAssetsBlock.Length; i++)
			{
				array[i] = (TileBase)tileAssetsBlock[i];
			}
			return array;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002796 File Offset: 0x00000996
		internal void SetTileAsset(Vector3Int position, Object tile)
		{
			this.SetTileAsset_Injected(ref position, tile);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x000027A1 File Offset: 0x000009A1
		public void SetTile(Vector3Int position, TileBase tile)
		{
			this.SetTileAsset(position, tile);
		}

		// Token: 0x06000055 RID: 85
		[MethodImpl(4096)]
		internal extern void SetTileAssets(Vector3Int[] positionArray, Object[] tileArray);

		// Token: 0x06000056 RID: 86 RVA: 0x000027B0 File Offset: 0x000009B0
		public void SetTiles(Vector3Int[] positionArray, TileBase[] tileArray)
		{
			this.SetTileAssets(positionArray, tileArray);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000027C9 File Offset: 0x000009C9
		[NativeMethod(Name = "SetTileAssetsBlock")]
		private void INTERNAL_CALL_SetTileAssetsBlock(Vector3Int position, Vector3Int blockDimensions, Object[] tileArray)
		{
			this.INTERNAL_CALL_SetTileAssetsBlock_Injected(ref position, ref blockDimensions, tileArray);
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000027D8 File Offset: 0x000009D8
		public void SetTilesBlock(BoundsInt position, TileBase[] tileArray)
		{
			this.INTERNAL_CALL_SetTileAssetsBlock(position.min, position.size, tileArray);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002800 File Offset: 0x00000A00
		public bool HasTile(Vector3Int position)
		{
			return this.GetTileAsset(position) != null;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000281F File Offset: 0x00000A1F
		[NativeMethod(Name = "RefreshTileAsset")]
		public void RefreshTile(Vector3Int position)
		{
			this.RefreshTile_Injected(ref position);
		}

		// Token: 0x0600005B RID: 91
		[NativeMethod(Name = "RefreshAllTileAssets")]
		[MethodImpl(4096)]
		public extern void RefreshAllTiles();

		// Token: 0x0600005C RID: 92
		[MethodImpl(4096)]
		internal extern void SwapTileAsset(Object changeTile, Object newTile);

		// Token: 0x0600005D RID: 93 RVA: 0x00002829 File Offset: 0x00000A29
		public void SwapTile(TileBase changeTile, TileBase newTile)
		{
			this.SwapTileAsset(changeTile, newTile);
		}

		// Token: 0x0600005E RID: 94
		[MethodImpl(4096)]
		internal extern bool ContainsTileAsset(Object tileAsset);

		// Token: 0x0600005F RID: 95 RVA: 0x00002838 File Offset: 0x00000A38
		public bool ContainsTile(TileBase tileAsset)
		{
			return this.ContainsTileAsset(tileAsset);
		}

		// Token: 0x06000060 RID: 96
		[MethodImpl(4096)]
		public extern int GetUsedTilesCount();

		// Token: 0x06000061 RID: 97 RVA: 0x00002854 File Offset: 0x00000A54
		public int GetUsedTilesNonAlloc(TileBase[] usedTiles)
		{
			return this.Internal_GetUsedTilesNonAlloc(usedTiles);
		}

		// Token: 0x06000062 RID: 98
		[FreeFunction(Name = "TilemapBindings::GetUsedTilesNonAlloc", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern int Internal_GetUsedTilesNonAlloc(Object[] usedTiles);

		// Token: 0x06000063 RID: 99 RVA: 0x0000286F File Offset: 0x00000A6F
		public Sprite GetSprite(Vector3Int position)
		{
			return this.GetSprite_Injected(ref position);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000287C File Offset: 0x00000A7C
		public Matrix4x4 GetTransformMatrix(Vector3Int position)
		{
			Matrix4x4 matrix4x;
			this.GetTransformMatrix_Injected(ref position, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002894 File Offset: 0x00000A94
		public void SetTransformMatrix(Vector3Int position, Matrix4x4 transform)
		{
			this.SetTransformMatrix_Injected(ref position, ref transform);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000028A0 File Offset: 0x00000AA0
		[NativeMethod(Name = "GetTileColor")]
		public Color GetColor(Vector3Int position)
		{
			Color color;
			this.GetColor_Injected(ref position, out color);
			return color;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000028B8 File Offset: 0x00000AB8
		[NativeMethod(Name = "SetTileColor")]
		public void SetColor(Vector3Int position, Color color)
		{
			this.SetColor_Injected(ref position, ref color);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000028C4 File Offset: 0x00000AC4
		public TileFlags GetTileFlags(Vector3Int position)
		{
			return this.GetTileFlags_Injected(ref position);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000028CE File Offset: 0x00000ACE
		public void SetTileFlags(Vector3Int position, TileFlags flags)
		{
			this.SetTileFlags_Injected(ref position, flags);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000028D9 File Offset: 0x00000AD9
		public void AddTileFlags(Vector3Int position, TileFlags flags)
		{
			this.AddTileFlags_Injected(ref position, flags);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000028E4 File Offset: 0x00000AE4
		public void RemoveTileFlags(Vector3Int position, TileFlags flags)
		{
			this.RemoveTileFlags_Injected(ref position, flags);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000028EF File Offset: 0x00000AEF
		[NativeMethod(Name = "GetTileInstantiatedObject")]
		public GameObject GetInstantiatedObject(Vector3Int position)
		{
			return this.GetInstantiatedObject_Injected(ref position);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000028F9 File Offset: 0x00000AF9
		[NativeMethod(Name = "GetTileObjectToInstantiate")]
		public GameObject GetObjectToInstantiate(Vector3Int position)
		{
			return this.GetObjectToInstantiate_Injected(ref position);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002903 File Offset: 0x00000B03
		[NativeMethod(Name = "SetTileColliderType")]
		public void SetColliderType(Vector3Int position, Tile.ColliderType colliderType)
		{
			this.SetColliderType_Injected(ref position, colliderType);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000290E File Offset: 0x00000B0E
		[NativeMethod(Name = "GetTileColliderType")]
		public Tile.ColliderType GetColliderType(Vector3Int position)
		{
			return this.GetColliderType_Injected(ref position);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002918 File Offset: 0x00000B18
		public void FloodFill(Vector3Int position, TileBase tile)
		{
			this.FloodFillTileAsset(position, tile);
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002924 File Offset: 0x00000B24
		[NativeMethod(Name = "FloodFill")]
		private void FloodFillTileAsset(Vector3Int position, Object tile)
		{
			this.FloodFillTileAsset_Injected(ref position, tile);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000292F File Offset: 0x00000B2F
		public void BoxFill(Vector3Int position, TileBase tile, int startX, int startY, int endX, int endY)
		{
			this.BoxFillTileAsset(position, tile, startX, startY, endX, endY);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002942 File Offset: 0x00000B42
		[NativeMethod(Name = "BoxFill")]
		private void BoxFillTileAsset(Vector3Int position, Object tile, int startX, int startY, int endX, int endY)
		{
			this.BoxFillTileAsset_Injected(ref position, tile, startX, startY, endX, endY);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002954 File Offset: 0x00000B54
		public void InsertCells(Vector3Int position, Vector3Int insertCells)
		{
			this.InsertCells(position, insertCells.x, insertCells.y, insertCells.z);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002974 File Offset: 0x00000B74
		public void InsertCells(Vector3Int position, int numColumns, int numRows, int numLayers)
		{
			this.InsertCells_Injected(ref position, numColumns, numRows, numLayers);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002982 File Offset: 0x00000B82
		public void DeleteCells(Vector3Int position, Vector3Int deleteCells)
		{
			this.DeleteCells(position, deleteCells.x, deleteCells.y, deleteCells.z);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000029A2 File Offset: 0x00000BA2
		public void DeleteCells(Vector3Int position, int numColumns, int numRows, int numLayers)
		{
			this.DeleteCells_Injected(ref position, numColumns, numRows, numLayers);
		}

		// Token: 0x06000078 RID: 120
		[MethodImpl(4096)]
		public extern void ClearAllTiles();

		// Token: 0x06000079 RID: 121
		[MethodImpl(4096)]
		public extern void ResizeBounds();

		// Token: 0x0600007A RID: 122
		[MethodImpl(4096)]
		public extern void CompressBounds();

		// Token: 0x0600007C RID: 124
		[MethodImpl(4096)]
		private extern void get_localBounds_Injected(out Bounds ret);

		// Token: 0x0600007D RID: 125
		[MethodImpl(4096)]
		private extern void get_localFrameBounds_Injected(out Bounds ret);

		// Token: 0x0600007E RID: 126
		[MethodImpl(4096)]
		private extern void get_color_Injected(out Color ret);

		// Token: 0x0600007F RID: 127
		[MethodImpl(4096)]
		private extern void set_color_Injected(ref Color value);

		// Token: 0x06000080 RID: 128
		[MethodImpl(4096)]
		private extern void get_origin_Injected(out Vector3Int ret);

		// Token: 0x06000081 RID: 129
		[MethodImpl(4096)]
		private extern void set_origin_Injected(ref Vector3Int value);

		// Token: 0x06000082 RID: 130
		[MethodImpl(4096)]
		private extern void get_size_Injected(out Vector3Int ret);

		// Token: 0x06000083 RID: 131
		[MethodImpl(4096)]
		private extern void set_size_Injected(ref Vector3Int value);

		// Token: 0x06000084 RID: 132
		[MethodImpl(4096)]
		private extern void get_tileAnchor_Injected(out Vector3 ret);

		// Token: 0x06000085 RID: 133
		[MethodImpl(4096)]
		private extern void set_tileAnchor_Injected(ref Vector3 value);

		// Token: 0x06000086 RID: 134
		[MethodImpl(4096)]
		private extern void get_orientationMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000087 RID: 135
		[MethodImpl(4096)]
		private extern void set_orientationMatrix_Injected(ref Matrix4x4 value);

		// Token: 0x06000088 RID: 136
		[MethodImpl(4096)]
		private extern Object GetTileAsset_Injected(ref Vector3Int position);

		// Token: 0x06000089 RID: 137
		[MethodImpl(4096)]
		private extern Object[] GetTileAssetsBlock_Injected(ref Vector3Int position, ref Vector3Int blockDimensions);

		// Token: 0x0600008A RID: 138
		[MethodImpl(4096)]
		private extern void SetTileAsset_Injected(ref Vector3Int position, Object tile);

		// Token: 0x0600008B RID: 139
		[MethodImpl(4096)]
		private extern void INTERNAL_CALL_SetTileAssetsBlock_Injected(ref Vector3Int position, ref Vector3Int blockDimensions, Object[] tileArray);

		// Token: 0x0600008C RID: 140
		[MethodImpl(4096)]
		private extern void RefreshTile_Injected(ref Vector3Int position);

		// Token: 0x0600008D RID: 141
		[MethodImpl(4096)]
		private extern Sprite GetSprite_Injected(ref Vector3Int position);

		// Token: 0x0600008E RID: 142
		[MethodImpl(4096)]
		private extern void GetTransformMatrix_Injected(ref Vector3Int position, out Matrix4x4 ret);

		// Token: 0x0600008F RID: 143
		[MethodImpl(4096)]
		private extern void SetTransformMatrix_Injected(ref Vector3Int position, ref Matrix4x4 transform);

		// Token: 0x06000090 RID: 144
		[MethodImpl(4096)]
		private extern void GetColor_Injected(ref Vector3Int position, out Color ret);

		// Token: 0x06000091 RID: 145
		[MethodImpl(4096)]
		private extern void SetColor_Injected(ref Vector3Int position, ref Color color);

		// Token: 0x06000092 RID: 146
		[MethodImpl(4096)]
		private extern TileFlags GetTileFlags_Injected(ref Vector3Int position);

		// Token: 0x06000093 RID: 147
		[MethodImpl(4096)]
		private extern void SetTileFlags_Injected(ref Vector3Int position, TileFlags flags);

		// Token: 0x06000094 RID: 148
		[MethodImpl(4096)]
		private extern void AddTileFlags_Injected(ref Vector3Int position, TileFlags flags);

		// Token: 0x06000095 RID: 149
		[MethodImpl(4096)]
		private extern void RemoveTileFlags_Injected(ref Vector3Int position, TileFlags flags);

		// Token: 0x06000096 RID: 150
		[MethodImpl(4096)]
		private extern GameObject GetInstantiatedObject_Injected(ref Vector3Int position);

		// Token: 0x06000097 RID: 151
		[MethodImpl(4096)]
		private extern GameObject GetObjectToInstantiate_Injected(ref Vector3Int position);

		// Token: 0x06000098 RID: 152
		[MethodImpl(4096)]
		private extern void SetColliderType_Injected(ref Vector3Int position, Tile.ColliderType colliderType);

		// Token: 0x06000099 RID: 153
		[MethodImpl(4096)]
		private extern Tile.ColliderType GetColliderType_Injected(ref Vector3Int position);

		// Token: 0x0600009A RID: 154
		[MethodImpl(4096)]
		private extern void FloodFillTileAsset_Injected(ref Vector3Int position, Object tile);

		// Token: 0x0600009B RID: 155
		[MethodImpl(4096)]
		private extern void BoxFillTileAsset_Injected(ref Vector3Int position, Object tile, int startX, int startY, int endX, int endY);

		// Token: 0x0600009C RID: 156
		[MethodImpl(4096)]
		private extern void InsertCells_Injected(ref Vector3Int position, int numColumns, int numRows, int numLayers);

		// Token: 0x0600009D RID: 157
		[MethodImpl(4096)]
		private extern void DeleteCells_Injected(ref Vector3Int position, int numColumns, int numRows, int numLayers);

		// Token: 0x0200000C RID: 12
		public enum Orientation
		{
			// Token: 0x04000020 RID: 32
			XY,
			// Token: 0x04000021 RID: 33
			XZ,
			// Token: 0x04000022 RID: 34
			YX,
			// Token: 0x04000023 RID: 35
			YZ,
			// Token: 0x04000024 RID: 36
			ZX,
			// Token: 0x04000025 RID: 37
			ZY,
			// Token: 0x04000026 RID: 38
			Custom
		}
	}
}
