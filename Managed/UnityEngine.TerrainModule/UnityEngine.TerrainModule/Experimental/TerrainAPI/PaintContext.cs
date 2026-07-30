using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnityEngine.Experimental.TerrainAPI
{
	// Token: 0x02000014 RID: 20
	public class PaintContext
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000431C File Offset: 0x0000251C
		public Terrain originTerrain { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00004324 File Offset: 0x00002524
		public RectInt pixelRect { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000432C File Offset: 0x0000252C
		public int targetTextureWidth { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00004334 File Offset: 0x00002534
		public int targetTextureHeight { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000433C File Offset: 0x0000253C
		public Vector2 pixelSize { get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00004344 File Offset: 0x00002544
		// (set) Token: 0x0600017D RID: 381 RVA: 0x0000434C File Offset: 0x0000254C
		public RenderTexture sourceRenderTexture { get; private set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00004355 File Offset: 0x00002555
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0000435D File Offset: 0x0000255D
		public RenderTexture destinationRenderTexture { get; private set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00004366 File Offset: 0x00002566
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0000436E File Offset: 0x0000256E
		public RenderTexture oldRenderTexture { get; private set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00004378 File Offset: 0x00002578
		public int terrainCount
		{
			get
			{
				return this.m_TerrainTiles.Count;
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00004398 File Offset: 0x00002598
		public Terrain GetTerrain(int terrainIndex)
		{
			return this.m_TerrainTiles[terrainIndex].terrain;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000043BC File Offset: 0x000025BC
		public RectInt GetClippedPixelRectInTerrainPixels(int terrainIndex)
		{
			return this.m_TerrainTiles[terrainIndex].clippedTerrainPixels;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x000043E0 File Offset: 0x000025E0
		public RectInt GetClippedPixelRectInRenderTexturePixels(int terrainIndex)
		{
			return this.m_TerrainTiles[terrainIndex].clippedPCPixels;
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00004403 File Offset: 0x00002603
		public float heightWorldSpaceMin
		{
			get
			{
				return this.m_HeightWorldSpaceMin;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000440B File Offset: 0x0000260B
		public float heightWorldSpaceSize
		{
			get
			{
				return this.m_HeightWorldSpaceMax - this.m_HeightWorldSpaceMin;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0000441A File Offset: 0x0000261A
		public static float kNormalizedHeightScale
		{
			get
			{
				return 0.4999771f;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000189 RID: 393 RVA: 0x00004424 File Offset: 0x00002624
		// (remove) Token: 0x0600018A RID: 394 RVA: 0x00004458 File Offset: 0x00002658
		[field: DebuggerBrowsable(0)]
		internal static event Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> onTerrainTileBeforePaint;

		// Token: 0x0600018B RID: 395 RVA: 0x0000448C File Offset: 0x0000268C
		public PaintContext(Terrain terrain, RectInt pixelRect, int targetTextureWidth, int targetTextureHeight, bool texelPadding = true)
		{
			this.originTerrain = terrain;
			this.pixelRect = pixelRect;
			this.targetTextureWidth = targetTextureWidth;
			this.targetTextureHeight = targetTextureHeight;
			TerrainData terrainData = terrain.terrainData;
			this.pixelSize = new Vector2(terrainData.size.x / ((float)targetTextureWidth - (texelPadding ? 1f : 0f)), terrainData.size.z / ((float)targetTextureHeight - (texelPadding ? 1f : 0f)));
			this.FindTerrainTilesUnlimited(texelPadding);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00004518 File Offset: 0x00002718
		public static PaintContext CreateFromBounds(Terrain terrain, Rect boundsInTerrainSpace, int inputTextureWidth, int inputTextureHeight, int extraBorderPixels = 0, bool texelPadding = true)
		{
			return new PaintContext(terrain, TerrainPaintUtility.CalcPixelRectFromBounds(terrain, boundsInTerrainSpace, inputTextureWidth, inputTextureHeight, extraBorderPixels, texelPadding), inputTextureWidth, inputTextureHeight, texelPadding);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00004544 File Offset: 0x00002744
		private void FindTerrainTilesUnlimited(bool texelPadding)
		{
			float minX = this.originTerrain.transform.position.x + this.pixelSize.x * (float)this.pixelRect.xMin;
			float minZ = this.originTerrain.transform.position.z + this.pixelSize.y * (float)this.pixelRect.yMin;
			float maxX = this.originTerrain.transform.position.x + this.pixelSize.x * (float)(this.pixelRect.xMax - 1);
			float maxZ = this.originTerrain.transform.position.z + this.pixelSize.y * (float)(this.pixelRect.yMax - 1);
			this.m_HeightWorldSpaceMin = this.originTerrain.GetPosition().y;
			this.m_HeightWorldSpaceMax = this.m_HeightWorldSpaceMin + this.originTerrain.terrainData.size.y;
			TerrainUtility.TerrainMap.TerrainFilter terrainFilter = delegate(Terrain t)
			{
				float x = t.transform.position.x;
				float z = t.transform.position.z;
				float num3 = t.transform.position.x + t.terrainData.size.x;
				float num4 = t.transform.position.z + t.terrainData.size.z;
				return x <= maxX && num3 >= minX && z <= maxZ && num4 >= minZ;
			};
			TerrainUtility.TerrainMap terrainMap = TerrainUtility.TerrainMap.CreateFromConnectedNeighbors(this.originTerrain, terrainFilter, false);
			this.m_TerrainTiles = new List<PaintContext.TerrainTile>();
			bool flag = terrainMap != null;
			if (flag)
			{
				foreach (KeyValuePair<TerrainUtility.TerrainMap.TileCoord, Terrain> keyValuePair in terrainMap.m_terrainTiles)
				{
					TerrainUtility.TerrainMap.TileCoord key = keyValuePair.Key;
					Terrain value = keyValuePair.Value;
					int num = key.tileX * (this.targetTextureWidth - (texelPadding ? 1 : 0));
					int num2 = key.tileZ * (this.targetTextureHeight - (texelPadding ? 1 : 0));
					RectInt rectInt = new RectInt(num, num2, this.targetTextureWidth, this.targetTextureHeight);
					bool flag2 = this.pixelRect.Overlaps(rectInt);
					if (flag2)
					{
						this.m_TerrainTiles.Add(PaintContext.TerrainTile.Make(value, num, num2, this.pixelRect, this.targetTextureWidth, this.targetTextureHeight));
						this.m_HeightWorldSpaceMin = Mathf.Min(this.m_HeightWorldSpaceMin, value.GetPosition().y);
						this.m_HeightWorldSpaceMax = Mathf.Max(this.m_HeightWorldSpaceMax, value.GetPosition().y + value.terrainData.size.y);
					}
				}
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x000047E4 File Offset: 0x000029E4
		public void CreateRenderTargets(RenderTextureFormat colorFormat)
		{
			this.sourceRenderTexture = RenderTexture.GetTemporary(this.pixelRect.width, this.pixelRect.height, 0, colorFormat, RenderTextureReadWrite.Linear);
			this.destinationRenderTexture = RenderTexture.GetTemporary(this.pixelRect.width, this.pixelRect.height, 0, colorFormat, RenderTextureReadWrite.Linear);
			this.sourceRenderTexture.wrapMode = TextureWrapMode.Clamp;
			this.sourceRenderTexture.filterMode = FilterMode.Point;
			this.oldRenderTexture = RenderTexture.active;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00004870 File Offset: 0x00002A70
		public void Cleanup(bool restoreRenderTexture = true)
		{
			if (restoreRenderTexture)
			{
				RenderTexture.active = this.oldRenderTexture;
			}
			RenderTexture.ReleaseTemporary(this.sourceRenderTexture);
			RenderTexture.ReleaseTemporary(this.destinationRenderTexture);
			this.sourceRenderTexture = null;
			this.destinationRenderTexture = null;
			this.oldRenderTexture = null;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000048C0 File Offset: 0x00002AC0
		private void GatherInternal(Func<PaintContext.ITerrainInfo, Texture> terrainToTexture, Color defaultColor, string operationName, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			bool flag = blitMaterial == null;
			if (flag)
			{
				blitMaterial = TerrainPaintUtility.GetBlitMaterial();
			}
			RenderTexture.active = this.sourceRenderTexture;
			GL.Clear(false, true, defaultColor);
			GL.PushMatrix();
			GL.LoadPixelMatrix(0f, (float)this.pixelRect.width, 0f, (float)this.pixelRect.height);
			for (int i = 0; i < this.m_TerrainTiles.Count; i++)
			{
				PaintContext.TerrainTile terrainTile = this.m_TerrainTiles[i];
				bool flag2 = !terrainTile.gatherEnable;
				if (!flag2)
				{
					Texture texture = terrainToTexture.Invoke(terrainTile);
					bool flag3 = texture == null || !terrainTile.gatherEnable;
					if (!flag3)
					{
						bool flag4 = texture.width != this.targetTextureWidth || texture.height != this.targetTextureHeight;
						if (flag4)
						{
							Debug.LogWarning(operationName + " requires the same resolution texture for all Terrains - mismatched Terrains are ignored.", terrainTile.terrain);
						}
						else
						{
							if (beforeBlit != null)
							{
								beforeBlit.Invoke(terrainTile);
							}
							bool flag5 = !terrainTile.gatherEnable;
							if (!flag5)
							{
								FilterMode filterMode = texture.filterMode;
								texture.filterMode = FilterMode.Point;
								blitMaterial.SetTexture("_MainTex", texture);
								blitMaterial.SetPass(blitPass);
								TerrainPaintUtility.DrawQuad(terrainTile.clippedPCPixels, terrainTile.clippedTerrainPixels, texture);
								texture.filterMode = filterMode;
								if (afterBlit != null)
								{
									afterBlit.Invoke(terrainTile);
								}
							}
						}
					}
				}
			}
			GL.PopMatrix();
			RenderTexture.active = this.oldRenderTexture;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00004A68 File Offset: 0x00002C68
		private void ScatterInternal(Func<PaintContext.ITerrainInfo, RenderTexture> terrainToRT, string operationName, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			RenderTexture active = RenderTexture.active;
			bool flag = blitMaterial == null;
			if (flag)
			{
				blitMaterial = TerrainPaintUtility.GetBlitMaterial();
			}
			for (int i = 0; i < this.m_TerrainTiles.Count; i++)
			{
				PaintContext.TerrainTile terrainTile = this.m_TerrainTiles[i];
				bool flag2 = !terrainTile.scatterEnable;
				if (!flag2)
				{
					RenderTexture renderTexture = terrainToRT.Invoke(terrainTile);
					bool flag3 = renderTexture == null || !terrainTile.scatterEnable;
					if (!flag3)
					{
						bool flag4 = renderTexture.width != this.targetTextureWidth || renderTexture.height != this.targetTextureHeight;
						if (flag4)
						{
							Debug.LogWarning(operationName + " requires the same resolution for all Terrains - mismatched Terrains are ignored.", terrainTile.terrain);
						}
						else
						{
							if (beforeBlit != null)
							{
								beforeBlit.Invoke(terrainTile);
							}
							bool flag5 = !terrainTile.scatterEnable;
							if (!flag5)
							{
								RenderTexture.active = renderTexture;
								GL.PushMatrix();
								GL.LoadPixelMatrix(0f, (float)renderTexture.width, 0f, (float)renderTexture.height);
								FilterMode filterMode = this.destinationRenderTexture.filterMode;
								this.destinationRenderTexture.filterMode = FilterMode.Point;
								blitMaterial.SetTexture("_MainTex", this.destinationRenderTexture);
								blitMaterial.SetPass(blitPass);
								TerrainPaintUtility.DrawQuad(terrainTile.clippedTerrainPixels, terrainTile.clippedPCPixels, this.destinationRenderTexture);
								this.destinationRenderTexture.filterMode = filterMode;
								GL.PopMatrix();
								if (afterBlit != null)
								{
									afterBlit.Invoke(terrainTile);
								}
							}
						}
					}
				}
			}
			RenderTexture.active = active;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00004C10 File Offset: 0x00002E10
		public void Gather(Func<PaintContext.ITerrainInfo, Texture> terrainSource, Color defaultColor, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			bool flag = terrainSource != null;
			if (flag)
			{
				this.GatherInternal(terrainSource, defaultColor, "PaintContext.Gather", blitMaterial, blitPass, beforeBlit, afterBlit);
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00004C3C File Offset: 0x00002E3C
		public void Scatter(Func<PaintContext.ITerrainInfo, RenderTexture> terrainDest, Material blitMaterial = null, int blitPass = 0, Action<PaintContext.ITerrainInfo> beforeBlit = null, Action<PaintContext.ITerrainInfo> afterBlit = null)
		{
			bool flag = terrainDest != null;
			if (flag)
			{
				this.ScatterInternal(terrainDest, "PaintContext.Scatter", blitMaterial, blitPass, beforeBlit, afterBlit);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004C68 File Offset: 0x00002E68
		public void GatherHeightmap()
		{
			Material blitMaterial = TerrainPaintUtility.GetHeightBlitMaterial();
			blitMaterial.SetFloat("_Height_Offset", 0f);
			blitMaterial.SetFloat("_Height_Scale", 1f);
			this.GatherInternal((PaintContext.ITerrainInfo t) => t.terrain.terrainData.heightmapTexture, new Color(0f, 0f, 0f, 0f), "PaintContext.GatherHeightmap", blitMaterial, 0, delegate(PaintContext.ITerrainInfo t)
			{
				blitMaterial.SetFloat("_Height_Offset", (t.terrain.GetPosition().y - this.heightWorldSpaceMin) / this.heightWorldSpaceSize * PaintContext.kNormalizedHeightScale);
				blitMaterial.SetFloat("_Height_Scale", t.terrain.terrainData.size.y / this.heightWorldSpaceSize);
			}, null);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00004D14 File Offset: 0x00002F14
		public void ScatterHeightmap(string editorUndoName)
		{
			Material blitMaterial = TerrainPaintUtility.GetHeightBlitMaterial();
			blitMaterial.SetFloat("_Height_Offset", 0f);
			blitMaterial.SetFloat("_Height_Scale", 1f);
			this.ScatterInternal((PaintContext.ITerrainInfo t) => t.terrain.terrainData.heightmapTexture, "PaintContext.ScatterHeightmap", blitMaterial, 0, delegate(PaintContext.ITerrainInfo t)
			{
				Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action = PaintContext.onTerrainTileBeforePaint;
				if (action != null)
				{
					action.Invoke(t, PaintContext.ToolAction.PaintHeightmap, editorUndoName);
				}
				blitMaterial.SetFloat("_Height_Offset", (this.heightWorldSpaceMin - t.terrain.GetPosition().y) / t.terrain.terrainData.size.y * PaintContext.kNormalizedHeightScale);
				blitMaterial.SetFloat("_Height_Scale", this.heightWorldSpaceSize / t.terrain.terrainData.size.y);
			}, delegate(PaintContext.ITerrainInfo t)
			{
				t.terrain.terrainData.DirtyHeightmapRegion(t.clippedTerrainPixels, t.terrain.drawInstanced ? TerrainHeightmapSyncControl.None : TerrainHeightmapSyncControl.HeightOnly);
				PaintContext.OnTerrainPainted(t, PaintContext.ToolAction.PaintHeightmap);
			});
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00004DCC File Offset: 0x00002FCC
		public void GatherHoles()
		{
			this.GatherInternal((PaintContext.ITerrainInfo t) => t.terrain.terrainData.holesTexture, new Color(0f, 0f, 0f, 0f), "PaintContext.GatherHoles", null, 0, null, null);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00004E24 File Offset: 0x00003024
		public void ScatterHoles(string editorUndoName)
		{
			this.ScatterInternal(delegate(PaintContext.ITerrainInfo t)
			{
				Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action = PaintContext.onTerrainTileBeforePaint;
				if (action != null)
				{
					action.Invoke(t, PaintContext.ToolAction.PaintHoles, editorUndoName);
				}
				t.terrain.terrainData.CopyActiveRenderTextureToTexture(TerrainData.HolesTextureName, 0, t.clippedPCPixels, t.clippedTerrainPixels.min, true);
				PaintContext.OnTerrainPainted(t, PaintContext.ToolAction.PaintHoles);
				return null;
			}, "PaintContext.ScatterHoles", null, 0, null, null);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00004E5C File Offset: 0x0000305C
		public void GatherNormals()
		{
			this.GatherInternal((PaintContext.ITerrainInfo t) => t.terrain.normalmapTexture, new Color(0.5f, 0.5f, 0.5f, 0.5f), "PaintContext.GatherNormals", null, 0, null, null);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00004EB4 File Offset: 0x000030B4
		private PaintContext.SplatmapUserData GetTerrainLayerUserData(PaintContext.ITerrainInfo context, TerrainLayer terrainLayer = null, bool addLayerIfDoesntExist = false)
		{
			PaintContext.SplatmapUserData splatmapUserData = context.userData as PaintContext.SplatmapUserData;
			bool flag = splatmapUserData != null;
			if (flag)
			{
				bool flag2 = terrainLayer == null || terrainLayer == splatmapUserData.terrainLayer;
				if (flag2)
				{
					return splatmapUserData;
				}
				splatmapUserData = null;
			}
			bool flag3 = splatmapUserData == null;
			if (flag3)
			{
				int num = -1;
				bool flag4 = terrainLayer != null;
				if (flag4)
				{
					num = TerrainPaintUtility.FindTerrainLayerIndex(context.terrain, terrainLayer);
					bool flag5 = num == -1 && addLayerIfDoesntExist;
					if (flag5)
					{
						num = TerrainPaintUtility.AddTerrainLayer(context.terrain, terrainLayer);
					}
				}
				bool flag6 = num != -1;
				if (flag6)
				{
					splatmapUserData = new PaintContext.SplatmapUserData();
					splatmapUserData.terrainLayer = terrainLayer;
					splatmapUserData.terrainLayerIndex = num;
					splatmapUserData.mapIndex = num >> 2;
					splatmapUserData.channelIndex = num & 3;
				}
				context.userData = splatmapUserData;
			}
			return splatmapUserData;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00004F90 File Offset: 0x00003190
		public void GatherAlphamap(TerrainLayer inputLayer, bool addLayerIfDoesntExist = true)
		{
			bool flag = inputLayer == null;
			if (!flag)
			{
				Material copyTerrainLayerMaterial = TerrainPaintUtility.GetCopyTerrainLayerMaterial();
				Vector4[] layerMasks = new Vector4[]
				{
					new Vector4(1f, 0f, 0f, 0f),
					new Vector4(0f, 1f, 0f, 0f),
					new Vector4(0f, 0f, 1f, 0f),
					new Vector4(0f, 0f, 0f, 1f)
				};
				this.GatherInternal(delegate(PaintContext.ITerrainInfo t)
				{
					PaintContext.SplatmapUserData terrainLayerUserData = this.GetTerrainLayerUserData(t, inputLayer, addLayerIfDoesntExist);
					bool flag2 = terrainLayerUserData != null;
					Texture texture;
					if (flag2)
					{
						texture = TerrainPaintUtility.GetTerrainAlphaMapChecked(t.terrain, terrainLayerUserData.mapIndex);
					}
					else
					{
						texture = null;
					}
					return texture;
				}, new Color(0f, 0f, 0f, 0f), "PaintContext.GatherAlphamap", copyTerrainLayerMaterial, 0, delegate(PaintContext.ITerrainInfo t)
				{
					PaintContext.SplatmapUserData terrainLayerUserData2 = this.GetTerrainLayerUserData(t, null, false);
					copyTerrainLayerMaterial.SetVector("_LayerMask", layerMasks[terrainLayerUserData2.channelIndex]);
				}, null);
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000050AC File Offset: 0x000032AC
		public void ScatterAlphamap(string editorUndoName)
		{
			Vector4[] layerMasks = new Vector4[]
			{
				new Vector4(1f, 0f, 0f, 0f),
				new Vector4(0f, 1f, 0f, 0f),
				new Vector4(0f, 0f, 1f, 0f),
				new Vector4(0f, 0f, 0f, 1f)
			};
			Material copyTerrainLayerMaterial = TerrainPaintUtility.GetCopyTerrainLayerMaterial();
			RenderTexture tempTarget = RenderTexture.GetTemporary(new RenderTextureDescriptor(this.destinationRenderTexture.width, this.destinationRenderTexture.height, RenderTextureFormat.ARGB32)
			{
				sRGB = false,
				useMipMap = false,
				autoGenerateMips = false
			});
			this.ScatterInternal(delegate(PaintContext.ITerrainInfo t)
			{
				PaintContext.SplatmapUserData terrainLayerUserData = this.GetTerrainLayerUserData(t, null, false);
				bool flag = terrainLayerUserData != null;
				if (flag)
				{
					Action<PaintContext.ITerrainInfo, PaintContext.ToolAction, string> action = PaintContext.onTerrainTileBeforePaint;
					if (action != null)
					{
						action.Invoke(t, PaintContext.ToolAction.PaintTexture, editorUndoName);
					}
					int mapIndex = terrainLayerUserData.mapIndex;
					int channelIndex = terrainLayerUserData.channelIndex;
					Texture2D texture2D = t.terrain.terrainData.alphamapTextures[mapIndex];
					this.destinationRenderTexture.filterMode = FilterMode.Point;
					this.sourceRenderTexture.filterMode = FilterMode.Point;
					for (int i = 0; i <= t.terrain.terrainData.alphamapTextureCount; i++)
					{
						bool flag2 = i == mapIndex;
						if (!flag2)
						{
							int num = ((i == t.terrain.terrainData.alphamapTextureCount) ? mapIndex : i);
							Texture2D texture2D2 = t.terrain.terrainData.alphamapTextures[num];
							bool flag3 = texture2D2.width != this.targetTextureWidth || texture2D2.height != this.targetTextureHeight;
							if (flag3)
							{
								Debug.LogWarning("PaintContext alphamap operations must use the same resolution for all Terrains - mismatched Terrains are ignored.", t.terrain);
							}
							else
							{
								RenderTexture.active = tempTarget;
								GL.PushMatrix();
								GL.LoadPixelMatrix(0f, (float)tempTarget.width, 0f, (float)tempTarget.height);
								copyTerrainLayerMaterial.SetTexture("_MainTex", this.destinationRenderTexture);
								copyTerrainLayerMaterial.SetTexture("_OldAlphaMapTexture", this.sourceRenderTexture);
								copyTerrainLayerMaterial.SetTexture("_OriginalTargetAlphaMap", texture2D);
								copyTerrainLayerMaterial.SetTexture("_AlphaMapTexture", texture2D2);
								copyTerrainLayerMaterial.SetVector("_LayerMask", (num == mapIndex) ? layerMasks[channelIndex] : Vector4.zero);
								copyTerrainLayerMaterial.SetVector("_OriginalTargetAlphaMask", layerMasks[channelIndex]);
								copyTerrainLayerMaterial.SetPass(1);
								TerrainPaintUtility.DrawQuad2(t.clippedPCPixels, t.clippedPCPixels, this.destinationRenderTexture, t.clippedTerrainPixels, texture2D2);
								GL.PopMatrix();
								t.terrain.terrainData.CopyActiveRenderTextureToTexture(TerrainData.AlphamapTextureName, num, t.clippedPCPixels, t.clippedTerrainPixels.min, true);
							}
						}
					}
					RenderTexture.active = null;
					PaintContext.OnTerrainPainted(t, PaintContext.ToolAction.PaintTexture);
				}
				return null;
			}, "PaintContext.ScatterAlphamap", copyTerrainLayerMaterial, 0, null, null);
			RenderTexture.ReleaseTemporary(tempTarget);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000051D8 File Offset: 0x000033D8
		private static void OnTerrainPainted(PaintContext.ITerrainInfo tile, PaintContext.ToolAction action)
		{
			for (int i = 0; i < PaintContext.s_PaintedTerrain.Count; i++)
			{
				bool flag = tile.terrain == PaintContext.s_PaintedTerrain[i].terrain;
				if (flag)
				{
					PaintContext.PaintedTerrain paintedTerrain = PaintContext.s_PaintedTerrain[i];
					paintedTerrain.action |= action;
					PaintContext.s_PaintedTerrain[i] = paintedTerrain;
					return;
				}
			}
			PaintContext.s_PaintedTerrain.Add(new PaintContext.PaintedTerrain
			{
				terrain = tile.terrain,
				action = action
			});
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00005274 File Offset: 0x00003474
		public static void ApplyDelayedActions()
		{
			for (int i = 0; i < PaintContext.s_PaintedTerrain.Count; i++)
			{
				PaintContext.PaintedTerrain paintedTerrain = PaintContext.s_PaintedTerrain[i];
				TerrainData terrainData = paintedTerrain.terrain.terrainData;
				bool flag = terrainData == null;
				if (!flag)
				{
					bool flag2 = (paintedTerrain.action & PaintContext.ToolAction.PaintHeightmap) > PaintContext.ToolAction.None;
					if (flag2)
					{
						terrainData.SyncHeightmap();
					}
					bool flag3 = (paintedTerrain.action & PaintContext.ToolAction.PaintHoles) > PaintContext.ToolAction.None;
					if (flag3)
					{
						terrainData.SyncTexture(TerrainData.HolesTextureName);
					}
					bool flag4 = (paintedTerrain.action & PaintContext.ToolAction.PaintTexture) > PaintContext.ToolAction.None;
					if (flag4)
					{
						terrainData.SetBaseMapDirty();
						terrainData.SyncTexture(TerrainData.AlphamapTextureName);
					}
					paintedTerrain.terrain.editorRenderFlags = TerrainRenderFlags.all;
				}
			}
			PaintContext.s_PaintedTerrain.Clear();
		}

		// Token: 0x04000066 RID: 102
		private List<PaintContext.TerrainTile> m_TerrainTiles;

		// Token: 0x04000067 RID: 103
		private float m_HeightWorldSpaceMin;

		// Token: 0x04000068 RID: 104
		private float m_HeightWorldSpaceMax;

		// Token: 0x0400006A RID: 106
		private static List<PaintContext.PaintedTerrain> s_PaintedTerrain = new List<PaintContext.PaintedTerrain>();

		// Token: 0x02000015 RID: 21
		public interface ITerrainInfo
		{
			// Token: 0x17000096 RID: 150
			// (get) Token: 0x0600019F RID: 415
			Terrain terrain { get; }

			// Token: 0x17000097 RID: 151
			// (get) Token: 0x060001A0 RID: 416
			RectInt clippedTerrainPixels { get; }

			// Token: 0x17000098 RID: 152
			// (get) Token: 0x060001A1 RID: 417
			RectInt clippedPCPixels { get; }

			// Token: 0x17000099 RID: 153
			// (get) Token: 0x060001A2 RID: 418
			// (set) Token: 0x060001A3 RID: 419
			bool gatherEnable { get; set; }

			// Token: 0x1700009A RID: 154
			// (get) Token: 0x060001A4 RID: 420
			// (set) Token: 0x060001A5 RID: 421
			bool scatterEnable { get; set; }

			// Token: 0x1700009B RID: 155
			// (get) Token: 0x060001A6 RID: 422
			// (set) Token: 0x060001A7 RID: 423
			object userData { get; set; }
		}

		// Token: 0x02000016 RID: 22
		private class TerrainTile : PaintContext.ITerrainInfo
		{
			// Token: 0x1700009C RID: 156
			// (get) Token: 0x060001A8 RID: 424 RVA: 0x00005350 File Offset: 0x00003550
			Terrain PaintContext.ITerrainInfo.terrain
			{
				get
				{
					return this.terrain;
				}
			}

			// Token: 0x1700009D RID: 157
			// (get) Token: 0x060001A9 RID: 425 RVA: 0x00005368 File Offset: 0x00003568
			RectInt PaintContext.ITerrainInfo.clippedTerrainPixels
			{
				get
				{
					return this.clippedTerrainPixels;
				}
			}

			// Token: 0x1700009E RID: 158
			// (get) Token: 0x060001AA RID: 426 RVA: 0x00005380 File Offset: 0x00003580
			RectInt PaintContext.ITerrainInfo.clippedPCPixels
			{
				get
				{
					return this.clippedPCPixels;
				}
			}

			// Token: 0x1700009F RID: 159
			// (get) Token: 0x060001AB RID: 427 RVA: 0x00005398 File Offset: 0x00003598
			// (set) Token: 0x060001AC RID: 428 RVA: 0x000053B0 File Offset: 0x000035B0
			bool PaintContext.ITerrainInfo.gatherEnable
			{
				get
				{
					return this.gatherEnable;
				}
				set
				{
					this.gatherEnable = value;
				}
			}

			// Token: 0x170000A0 RID: 160
			// (get) Token: 0x060001AD RID: 429 RVA: 0x000053BC File Offset: 0x000035BC
			// (set) Token: 0x060001AE RID: 430 RVA: 0x000053D4 File Offset: 0x000035D4
			bool PaintContext.ITerrainInfo.scatterEnable
			{
				get
				{
					return this.scatterEnable;
				}
				set
				{
					this.scatterEnable = value;
				}
			}

			// Token: 0x170000A1 RID: 161
			// (get) Token: 0x060001AF RID: 431 RVA: 0x000053E0 File Offset: 0x000035E0
			// (set) Token: 0x060001B0 RID: 432 RVA: 0x000053F8 File Offset: 0x000035F8
			object PaintContext.ITerrainInfo.userData
			{
				get
				{
					return this.userData;
				}
				set
				{
					this.userData = value;
				}
			}

			// Token: 0x060001B1 RID: 433 RVA: 0x00005404 File Offset: 0x00003604
			public static PaintContext.TerrainTile Make(Terrain terrain, int tileOriginPixelsX, int tileOriginPixelsY, RectInt pixelRect, int targetTextureWidth, int targetTextureHeight)
			{
				PaintContext.TerrainTile terrainTile = new PaintContext.TerrainTile
				{
					terrain = terrain,
					gatherEnable = true,
					scatterEnable = true,
					tileOriginPixels = new Vector2Int(tileOriginPixelsX, tileOriginPixelsY),
					clippedTerrainPixels = new RectInt
					{
						x = Mathf.Max(0, pixelRect.x - tileOriginPixelsX),
						y = Mathf.Max(0, pixelRect.y - tileOriginPixelsY),
						xMax = Mathf.Min(targetTextureWidth, pixelRect.xMax - tileOriginPixelsX),
						yMax = Mathf.Min(targetTextureHeight, pixelRect.yMax - tileOriginPixelsY)
					}
				};
				terrainTile.clippedPCPixels = new RectInt(terrainTile.clippedTerrainPixels.x + terrainTile.tileOriginPixels.x - pixelRect.x, terrainTile.clippedTerrainPixels.y + terrainTile.tileOriginPixels.y - pixelRect.y, terrainTile.clippedTerrainPixels.width, terrainTile.clippedTerrainPixels.height);
				bool flag = terrainTile.clippedTerrainPixels.width == 0 || terrainTile.clippedTerrainPixels.height == 0;
				if (flag)
				{
					terrainTile.gatherEnable = false;
					terrainTile.scatterEnable = false;
					Debug.LogError("PaintContext.ClipTerrainTiles found 0 content rect");
				}
				return terrainTile;
			}

			// Token: 0x0400006B RID: 107
			public Terrain terrain;

			// Token: 0x0400006C RID: 108
			public Vector2Int tileOriginPixels;

			// Token: 0x0400006D RID: 109
			public RectInt clippedTerrainPixels;

			// Token: 0x0400006E RID: 110
			public RectInt clippedPCPixels;

			// Token: 0x0400006F RID: 111
			public object userData;

			// Token: 0x04000070 RID: 112
			public bool gatherEnable;

			// Token: 0x04000071 RID: 113
			public bool scatterEnable;
		}

		// Token: 0x02000017 RID: 23
		private class SplatmapUserData
		{
			// Token: 0x04000072 RID: 114
			public TerrainLayer terrainLayer;

			// Token: 0x04000073 RID: 115
			public int terrainLayerIndex;

			// Token: 0x04000074 RID: 116
			public int mapIndex;

			// Token: 0x04000075 RID: 117
			public int channelIndex;
		}

		// Token: 0x02000018 RID: 24
		[Flags]
		internal enum ToolAction
		{
			// Token: 0x04000077 RID: 119
			None = 0,
			// Token: 0x04000078 RID: 120
			PaintHeightmap = 1,
			// Token: 0x04000079 RID: 121
			PaintTexture = 2,
			// Token: 0x0400007A RID: 122
			PaintHoles = 4
		}

		// Token: 0x02000019 RID: 25
		private struct PaintedTerrain
		{
			// Token: 0x0400007B RID: 123
			public Terrain terrain;

			// Token: 0x0400007C RID: 124
			public PaintContext.ToolAction action;
		}
	}
}
