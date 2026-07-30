using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.TerrainAPI
{
	// Token: 0x02000026 RID: 38
	public static class TerrainUtility
	{
		// Token: 0x060001EF RID: 495 RVA: 0x000065E4 File Offset: 0x000047E4
		internal static bool HasValidTerrains()
		{
			return Terrain.activeTerrains != null && Terrain.activeTerrains.Length != 0;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0000660C File Offset: 0x0000480C
		internal static void ClearConnectivity()
		{
			foreach (Terrain terrain in Terrain.activeTerrains)
			{
				terrain.SetNeighbors(null, null, null, null);
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00006640 File Offset: 0x00004840
		internal static TerrainUtility.TerrainGroups CollectTerrains(bool onlyAutoConnectedTerrains = true)
		{
			bool flag = !TerrainUtility.HasValidTerrains();
			TerrainUtility.TerrainGroups terrainGroups;
			if (flag)
			{
				terrainGroups = null;
			}
			else
			{
				TerrainUtility.TerrainGroups terrainGroups2 = new TerrainUtility.TerrainGroups();
				Terrain[] activeTerrains = Terrain.activeTerrains;
				for (int i = 0; i < activeTerrains.Length; i++)
				{
					Terrain t = activeTerrains[i];
					bool flag2 = onlyAutoConnectedTerrains && !t.allowAutoConnect;
					if (!flag2)
					{
						bool flag3 = !terrainGroups2.ContainsKey(t.groupingID);
						if (flag3)
						{
							TerrainUtility.TerrainMap terrainMap = TerrainUtility.TerrainMap.CreateFromPlacement(t, (Terrain x) => x.groupingID == t.groupingID && (!onlyAutoConnectedTerrains || x.allowAutoConnect), true);
							bool flag4 = terrainMap != null;
							if (flag4)
							{
								terrainGroups2.Add(t.groupingID, terrainMap);
							}
						}
					}
				}
				terrainGroups = ((terrainGroups2.Count != 0) ? terrainGroups2 : null);
			}
			return terrainGroups;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006744 File Offset: 0x00004944
		[RequiredByNativeCode]
		public static void AutoConnect()
		{
			bool flag = !TerrainUtility.HasValidTerrains();
			if (!flag)
			{
				TerrainUtility.ClearConnectivity();
				TerrainUtility.TerrainGroups terrainGroups = TerrainUtility.CollectTerrains(true);
				bool flag2 = terrainGroups == null;
				if (!flag2)
				{
					foreach (KeyValuePair<int, TerrainUtility.TerrainMap> keyValuePair in terrainGroups)
					{
						TerrainUtility.TerrainMap value = keyValuePair.Value;
						foreach (KeyValuePair<TerrainUtility.TerrainMap.TileCoord, Terrain> keyValuePair2 in value.m_terrainTiles)
						{
							TerrainUtility.TerrainMap.TileCoord key = keyValuePair2.Key;
							Terrain terrain = value.GetTerrain(key.tileX, key.tileZ);
							Terrain terrain2 = value.GetTerrain(key.tileX - 1, key.tileZ);
							Terrain terrain3 = value.GetTerrain(key.tileX + 1, key.tileZ);
							Terrain terrain4 = value.GetTerrain(key.tileX, key.tileZ + 1);
							Terrain terrain5 = value.GetTerrain(key.tileX, key.tileZ - 1);
							terrain.SetNeighbors(terrain2, terrain4, terrain3, terrain5);
						}
					}
				}
			}
		}

		// Token: 0x02000027 RID: 39
		public class TerrainMap
		{
			// Token: 0x060001F3 RID: 499 RVA: 0x000068A8 File Offset: 0x00004AA8
			public Terrain GetTerrain(int tileX, int tileZ)
			{
				Terrain terrain = null;
				this.m_terrainTiles.TryGetValue(new TerrainUtility.TerrainMap.TileCoord(tileX, tileZ), ref terrain);
				return terrain;
			}

			// Token: 0x060001F4 RID: 500 RVA: 0x000068D4 File Offset: 0x00004AD4
			public static TerrainUtility.TerrainMap CreateFromConnectedNeighbors(Terrain originTerrain, TerrainUtility.TerrainMap.TerrainFilter filter = null, bool fullValidation = true)
			{
				bool flag = originTerrain == null;
				TerrainUtility.TerrainMap terrainMap;
				if (flag)
				{
					terrainMap = null;
				}
				else
				{
					bool flag2 = originTerrain.terrainData == null;
					if (flag2)
					{
						terrainMap = null;
					}
					else
					{
						TerrainUtility.TerrainMap terrainMap2 = new TerrainUtility.TerrainMap();
						Queue<TerrainUtility.TerrainMap.QueueElement> queue = new Queue<TerrainUtility.TerrainMap.QueueElement>();
						queue.Enqueue(new TerrainUtility.TerrainMap.QueueElement(0, 0, originTerrain));
						int num = Terrain.activeTerrains.Length;
						while (queue.Count > 0)
						{
							TerrainUtility.TerrainMap.QueueElement queueElement = queue.Dequeue();
							bool flag3 = filter == null || filter(queueElement.terrain);
							if (flag3)
							{
								bool flag4 = terrainMap2.TryToAddTerrain(queueElement.tileX, queueElement.tileZ, queueElement.terrain);
								if (flag4)
								{
									bool flag5 = terrainMap2.m_terrainTiles.Count > num;
									if (flag5)
									{
										break;
									}
									bool flag6 = queueElement.terrain.leftNeighbor != null;
									if (flag6)
									{
										queue.Enqueue(new TerrainUtility.TerrainMap.QueueElement(queueElement.tileX - 1, queueElement.tileZ, queueElement.terrain.leftNeighbor));
									}
									bool flag7 = queueElement.terrain.bottomNeighbor != null;
									if (flag7)
									{
										queue.Enqueue(new TerrainUtility.TerrainMap.QueueElement(queueElement.tileX, queueElement.tileZ - 1, queueElement.terrain.bottomNeighbor));
									}
									bool flag8 = queueElement.terrain.rightNeighbor != null;
									if (flag8)
									{
										queue.Enqueue(new TerrainUtility.TerrainMap.QueueElement(queueElement.tileX + 1, queueElement.tileZ, queueElement.terrain.rightNeighbor));
									}
									bool flag9 = queueElement.terrain.topNeighbor != null;
									if (flag9)
									{
										queue.Enqueue(new TerrainUtility.TerrainMap.QueueElement(queueElement.tileX, queueElement.tileZ + 1, queueElement.terrain.topNeighbor));
									}
								}
							}
						}
						if (fullValidation)
						{
							terrainMap2.Validate();
						}
						terrainMap = terrainMap2;
					}
				}
				return terrainMap;
			}

			// Token: 0x060001F5 RID: 501 RVA: 0x00006AC8 File Offset: 0x00004CC8
			public static TerrainUtility.TerrainMap CreateFromPlacement(Terrain originTerrain, TerrainUtility.TerrainMap.TerrainFilter filter = null, bool fullValidation = true)
			{
				bool flag = Terrain.activeTerrains == null || Terrain.activeTerrains.Length == 0 || originTerrain == null;
				TerrainUtility.TerrainMap terrainMap;
				if (flag)
				{
					terrainMap = null;
				}
				else
				{
					bool flag2 = originTerrain.terrainData == null;
					if (flag2)
					{
						terrainMap = null;
					}
					else
					{
						int groupID = originTerrain.groupingID;
						float x3 = originTerrain.transform.position.x;
						float z = originTerrain.transform.position.z;
						float x2 = originTerrain.terrainData.size.x;
						float z2 = originTerrain.terrainData.size.z;
						bool flag3 = filter == null;
						if (flag3)
						{
							filter = (Terrain x) => x.groupingID == groupID;
						}
						terrainMap = TerrainUtility.TerrainMap.CreateFromPlacement(new Vector2(x3, z), new Vector2(x2, z2), filter, fullValidation);
					}
				}
				return terrainMap;
			}

			// Token: 0x060001F6 RID: 502 RVA: 0x00006BA4 File Offset: 0x00004DA4
			public static TerrainUtility.TerrainMap CreateFromPlacement(Vector2 gridOrigin, Vector2 gridSize, TerrainUtility.TerrainMap.TerrainFilter filter = null, bool fullValidation = true)
			{
				bool flag = Terrain.activeTerrains == null || Terrain.activeTerrains.Length == 0;
				TerrainUtility.TerrainMap terrainMap;
				if (flag)
				{
					terrainMap = null;
				}
				else
				{
					TerrainUtility.TerrainMap terrainMap2 = new TerrainUtility.TerrainMap();
					float num = 1f / gridSize.x;
					float num2 = 1f / gridSize.y;
					foreach (Terrain terrain in Terrain.activeTerrains)
					{
						bool flag2 = terrain.terrainData == null;
						if (!flag2)
						{
							bool flag3 = filter == null || filter(terrain);
							if (flag3)
							{
								Vector3 position = terrain.transform.position;
								int num3 = Mathf.RoundToInt((position.x - gridOrigin.x) * num);
								int num4 = Mathf.RoundToInt((position.z - gridOrigin.y) * num2);
								terrainMap2.TryToAddTerrain(num3, num4, terrain);
							}
						}
					}
					if (fullValidation)
					{
						terrainMap2.Validate();
					}
					terrainMap = ((terrainMap2.m_terrainTiles.Count > 0) ? terrainMap2 : null);
				}
				return terrainMap;
			}

			// Token: 0x060001F7 RID: 503 RVA: 0x00006CB5 File Offset: 0x00004EB5
			public TerrainMap()
			{
				this.m_errorCode = TerrainUtility.TerrainMap.ErrorCode.OK;
				this.m_terrainTiles = new Dictionary<TerrainUtility.TerrainMap.TileCoord, Terrain>();
			}

			// Token: 0x060001F8 RID: 504 RVA: 0x00006CD4 File Offset: 0x00004ED4
			private void AddTerrainInternal(int x, int z, Terrain terrain)
			{
				bool flag = this.m_terrainTiles.Count == 0;
				if (flag)
				{
					this.m_patchSize = terrain.terrainData.size;
				}
				else
				{
					bool flag2 = terrain.terrainData.size != this.m_patchSize;
					if (flag2)
					{
						this.m_errorCode |= TerrainUtility.TerrainMap.ErrorCode.SizeMismatch;
					}
				}
				this.m_terrainTiles.Add(new TerrainUtility.TerrainMap.TileCoord(x, z), terrain);
			}

			// Token: 0x060001F9 RID: 505 RVA: 0x00006D48 File Offset: 0x00004F48
			private bool TryToAddTerrain(int tileX, int tileZ, Terrain terrain)
			{
				bool flag = false;
				bool flag2 = terrain != null;
				if (flag2)
				{
					Terrain terrain2 = this.GetTerrain(tileX, tileZ);
					bool flag3 = terrain2 != null;
					if (flag3)
					{
						bool flag4 = terrain2 != terrain;
						if (flag4)
						{
							this.m_errorCode |= TerrainUtility.TerrainMap.ErrorCode.Overlapping;
						}
					}
					else
					{
						this.AddTerrainInternal(tileX, tileZ, terrain);
						flag = true;
					}
				}
				return flag;
			}

			// Token: 0x060001FA RID: 506 RVA: 0x00006DB0 File Offset: 0x00004FB0
			private void ValidateTerrain(int tileX, int tileZ)
			{
				Terrain terrain = this.GetTerrain(tileX, tileZ);
				bool flag = terrain != null;
				if (flag)
				{
					Terrain terrain2 = this.GetTerrain(tileX - 1, tileZ);
					Terrain terrain3 = this.GetTerrain(tileX + 1, tileZ);
					Terrain terrain4 = this.GetTerrain(tileX, tileZ + 1);
					Terrain terrain5 = this.GetTerrain(tileX, tileZ - 1);
					bool flag2 = terrain2;
					if (flag2)
					{
						bool flag3 = !Mathf.Approximately(terrain.transform.position.x, terrain2.transform.position.x + terrain2.terrainData.size.x) || !Mathf.Approximately(terrain.transform.position.z, terrain2.transform.position.z);
						if (flag3)
						{
							this.m_errorCode |= TerrainUtility.TerrainMap.ErrorCode.EdgeAlignmentMismatch;
						}
					}
					bool flag4 = terrain3;
					if (flag4)
					{
						bool flag5 = !Mathf.Approximately(terrain.transform.position.x + terrain.terrainData.size.x, terrain3.transform.position.x) || !Mathf.Approximately(terrain.transform.position.z, terrain3.transform.position.z);
						if (flag5)
						{
							this.m_errorCode |= TerrainUtility.TerrainMap.ErrorCode.EdgeAlignmentMismatch;
						}
					}
					bool flag6 = terrain4;
					if (flag6)
					{
						bool flag7 = !Mathf.Approximately(terrain.transform.position.x, terrain4.transform.position.x) || !Mathf.Approximately(terrain.transform.position.z + terrain.terrainData.size.z, terrain4.transform.position.z);
						if (flag7)
						{
							this.m_errorCode |= TerrainUtility.TerrainMap.ErrorCode.EdgeAlignmentMismatch;
						}
					}
					bool flag8 = terrain5;
					if (flag8)
					{
						bool flag9 = !Mathf.Approximately(terrain.transform.position.x, terrain5.transform.position.x) || !Mathf.Approximately(terrain.transform.position.z, terrain5.transform.position.z + terrain5.terrainData.size.z);
						if (flag9)
						{
							this.m_errorCode |= TerrainUtility.TerrainMap.ErrorCode.EdgeAlignmentMismatch;
						}
					}
				}
			}

			// Token: 0x060001FB RID: 507 RVA: 0x0000702C File Offset: 0x0000522C
			private TerrainUtility.TerrainMap.ErrorCode Validate()
			{
				foreach (TerrainUtility.TerrainMap.TileCoord tileCoord in this.m_terrainTiles.Keys)
				{
					this.ValidateTerrain(tileCoord.tileX, tileCoord.tileZ);
				}
				return this.m_errorCode;
			}

			// Token: 0x040000A4 RID: 164
			private Vector3 m_patchSize;

			// Token: 0x040000A5 RID: 165
			public TerrainUtility.TerrainMap.ErrorCode m_errorCode;

			// Token: 0x040000A6 RID: 166
			public Dictionary<TerrainUtility.TerrainMap.TileCoord, Terrain> m_terrainTiles;

			// Token: 0x02000028 RID: 40
			// (Invoke) Token: 0x060001FD RID: 509
			public delegate bool TerrainFilter(Terrain terrain);

			// Token: 0x02000029 RID: 41
			private struct QueueElement
			{
				// Token: 0x06000200 RID: 512 RVA: 0x000070A0 File Offset: 0x000052A0
				public QueueElement(int tileX, int tileZ, Terrain terrain)
				{
					this.tileX = tileX;
					this.tileZ = tileZ;
					this.terrain = terrain;
				}

				// Token: 0x040000A7 RID: 167
				public readonly int tileX;

				// Token: 0x040000A8 RID: 168
				public readonly int tileZ;

				// Token: 0x040000A9 RID: 169
				public readonly Terrain terrain;
			}

			// Token: 0x0200002A RID: 42
			public struct TileCoord
			{
				// Token: 0x06000201 RID: 513 RVA: 0x000070B8 File Offset: 0x000052B8
				public TileCoord(int tileX, int tileZ)
				{
					this.tileX = tileX;
					this.tileZ = tileZ;
				}

				// Token: 0x040000AA RID: 170
				public readonly int tileX;

				// Token: 0x040000AB RID: 171
				public readonly int tileZ;
			}

			// Token: 0x0200002B RID: 43
			public enum ErrorCode
			{
				// Token: 0x040000AD RID: 173
				OK,
				// Token: 0x040000AE RID: 174
				Overlapping,
				// Token: 0x040000AF RID: 175
				SizeMismatch = 4,
				// Token: 0x040000B0 RID: 176
				EdgeAlignmentMismatch = 8
			}
		}

		// Token: 0x0200002D RID: 45
		public class TerrainGroups : Dictionary<int, TerrainUtility.TerrainMap>
		{
		}
	}
}
