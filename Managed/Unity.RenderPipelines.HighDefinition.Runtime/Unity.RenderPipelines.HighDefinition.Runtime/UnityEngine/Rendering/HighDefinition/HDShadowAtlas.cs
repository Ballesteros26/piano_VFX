using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200008D RID: 141
	internal class HDShadowAtlas
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0002F2D8 File Offset: 0x0002D4D8
		public RTHandle renderTarget
		{
			get
			{
				return this.m_Atlas;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0002F2E0 File Offset: 0x0002D4E0
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x0002F2E8 File Offset: 0x0002D4E8
		public int width { get; private set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0002F2F1 File Offset: 0x0002D4F1
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0002F2F9 File Offset: 0x0002D4F9
		public int height { get; private set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0002F302 File Offset: 0x0002D502
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x0002F30A File Offset: 0x0002D50A
		public int frameOfCacheValidity { get; private set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0002F313 File Offset: 0x0002D513
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x0002F31B File Offset: 0x0002D51B
		public int atlasShapeID { get; private set; }

		// Token: 0x060005A5 RID: 1445 RVA: 0x0002F324 File Offset: 0x0002D524
		public HDShadowAtlas(RenderPipelineResources renderPipelineResources, int width, int height, int atlasShaderID, int atlasSizeShaderID, Material clearMaterial, int maxShadowRequests, HDShadowAtlas.BlurAlgorithm blurAlgorithm = HDShadowAtlas.BlurAlgorithm.None, FilterMode filterMode = FilterMode.Bilinear, DepthBits depthBufferBits = DepthBits.Depth16, RenderTextureFormat format = RenderTextureFormat.Shadowmap, string name = "", int momentAtlasShaderID = 0)
		{
			this.width = width;
			this.height = height;
			this.m_FilterMode = filterMode;
			this.m_DepthBufferBits = depthBufferBits;
			this.m_Format = format;
			this.m_Name = name;
			this.m_AtlasShaderID = atlasShaderID;
			this.m_MomentAtlasShaderID = momentAtlasShaderID;
			this.m_AtlasSizeShaderID = atlasSizeShaderID;
			this.m_ClearMaterial = clearMaterial;
			this.m_BlurAlgorithm = blurAlgorithm;
			this.m_RenderPipelineResources = renderPipelineResources;
			this.m_SortedRequestsCache = new HDShadowResolutionRequest[Mathf.CeilToInt((float)maxShadowRequests * 1.5f)];
			this.m_CachedResolutionRequests = new HDShadowResolutionRequest[maxShadowRequests];
			for (int i = 0; i < maxShadowRequests; i++)
			{
				this.m_CachedResolutionRequests[i] = new HDShadowResolutionRequest();
			}
			this.AllocateRenderTexture();
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0002F408 File Offset: 0x0002D608
		private void AllocateRenderTexture()
		{
			if (this.m_Atlas != null)
			{
				this.m_Atlas.Release();
			}
			int width = this.width;
			int height = this.height;
			int num = 1;
			FilterMode filterMode = this.m_FilterMode;
			this.m_Atlas = RTHandles.Alloc(width, height, num, this.m_DepthBufferBits, GraphicsFormat.R8G8B8A8_SRGB, filterMode, TextureWrapMode.Repeat, TextureDimension.Tex2D, false, false, true, true, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, this.m_Name);
			if (this.m_BlurAlgorithm == HDShadowAtlas.BlurAlgorithm.IM)
			{
				string text = this.m_Name + "Moment";
				this.m_AtlasMoments = new RTHandle[1];
				this.m_AtlasMoments[0] = RTHandles.Alloc(this.width, this.height, 1, DepthBits.None, GraphicsFormat.R32G32B32A32_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex2D, true, false, true, false, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, text);
				string text2 = this.m_Name + "IntermediateSummedArea";
				this.m_IntermediateSummedAreaTexture = RTHandles.Alloc(this.width, this.height, 1, DepthBits.None, GraphicsFormat.R32G32B32A32_SInt, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex2D, true, false, true, false, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, text2);
				string text3 = this.m_Name + "SummedAreaFinal";
				this.m_SummedAreaTexture = RTHandles.Alloc(this.width, this.height, 1, DepthBits.None, GraphicsFormat.R32G32B32A32_SInt, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex2D, true, false, true, false, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, text3);
				return;
			}
			if (this.m_BlurAlgorithm == HDShadowAtlas.BlurAlgorithm.EVSM)
			{
				string[] array = new string[]
				{
					this.m_Name + "Moment",
					this.m_Name + "MomentCopy"
				};
				this.m_AtlasMoments = new RTHandle[2];
				for (int i = 0; i < 2; i++)
				{
					this.m_AtlasMoments[i] = RTHandles.Alloc(this.width / 2, this.height / 2, 1, DepthBits.None, GraphicsFormat.R32G32_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureDimension.Tex2D, true, true, false, false, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, array[i]);
				}
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0002F5C8 File Offset: 0x0002D7C8
		public void BindResources(CommandBuffer cmd)
		{
			cmd.SetGlobalTexture(this.m_AtlasShaderID, this.m_Atlas);
			if (this.m_BlurAlgorithm == HDShadowAtlas.BlurAlgorithm.EVSM)
			{
				cmd.SetGlobalTexture(this.m_MomentAtlasShaderID, this.m_AtlasMoments[0]);
			}
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0002F603 File Offset: 0x0002D803
		public void UpdateSize(Vector2Int size)
		{
			if (this.m_Atlas == null || this.m_Atlas.referenceSize != size)
			{
				this.width = size.x;
				this.height = size.y;
				this.AllocateRenderTexture();
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0002F640 File Offset: 0x0002D840
		internal void ReserveResolution(HDShadowResolutionRequest shadowRequest)
		{
			this.m_ShadowResolutionRequests.Add(shadowRequest);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0002F64E File Offset: 0x0002D84E
		internal void AddShadowRequest(HDShadowRequest shadowRequest)
		{
			this.m_ShadowRequests.Add(shadowRequest);
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0002F65C File Offset: 0x0002D85C
		public void UpdateDebugSettings(LightingDebugSettings lightingDebugSettings)
		{
			this.m_LightingDebugSettings = lightingDebugSettings;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0002F668 File Offset: 0x0002D868
		private void InsertionSort(HDShadowResolutionRequest[] array, int startIndex, int lastIndex)
		{
			for (int i = startIndex + 1; i < lastIndex; i++)
			{
				HDShadowResolutionRequest hdshadowResolutionRequest = array[i];
				int num = i - 1;
				while (num >= 0 && (hdshadowResolutionRequest.resolution.x > array[num].resolution.x || hdshadowResolutionRequest.resolution.y > array[num].resolution.y))
				{
					array[num + 1] = array[num];
					num--;
				}
				array[num + 1] = hdshadowResolutionRequest;
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0002F6D7 File Offset: 0x0002D8D7
		internal HDShadowResolutionRequest GetCachedRequest(int cachedIndex)
		{
			if (cachedIndex < 0 || cachedIndex >= this.m_ListOfCachedShadowRequests.Count)
			{
				return null;
			}
			return this.m_ListOfCachedShadowRequests[cachedIndex];
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0002F6F9 File Offset: 0x0002D8F9
		internal bool HasResizedThisFrame()
		{
			return this.m_HasResizedAtlas;
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0002F704 File Offset: 0x0002D904
		internal void MarkCulledShadowMapAsEmptySlots()
		{
			for (int i = 0; i < this.m_ListOfCachedShadowRequests.Count; i++)
			{
				if (this.frameCounter - this.m_ListOfCachedShadowRequests[i].lastFrameActive > 0)
				{
					this.m_ListOfCachedShadowRequests[i].emptyRequest = true;
				}
			}
			this.frameCounter++;
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0002F762 File Offset: 0x0002D962
		internal void PruneDeadCachedLightSlots()
		{
			this.m_ListOfCachedShadowRequests.RemoveAll((HDShadowResolutionRequest x) => x.emptyRequest);
			this.frameOfCacheValidity = 0;
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0002F798 File Offset: 0x0002D998
		internal void MarkCachedShadowSlotAsEmpty(int lightID)
		{
			List<HDShadowResolutionRequest> list = this.m_ListOfCachedShadowRequests.FindAll((HDShadowResolutionRequest x) => x.lightID == lightID);
			for (int i = 0; i < list.Count; i++)
			{
				list[i].emptyRequest = true;
			}
		}

		// Token: 0x060005B2 RID: 1458 RVA: 0x0002F7E8 File Offset: 0x0002D9E8
		internal int RegisterCachedLight(HDShadowResolutionRequest request)
		{
			int frameOfCacheValidity = this.frameOfCacheValidity;
			this.frameOfCacheValidity = frameOfCacheValidity + 1;
			int num = -1;
			for (int i = 0; i < this.m_ListOfCachedShadowRequests.Count; i++)
			{
				if (!this.m_ListOfCachedShadowRequests[i].emptyRequest && this.m_ListOfCachedShadowRequests[i].lightID == request.lightID && this.m_ListOfCachedShadowRequests[i].indexInLight == request.indexInLight)
				{
					num = i;
					break;
				}
			}
			if (num == -1)
			{
				float width = request.atlasViewport.width;
				request.lastFrameActive = this.frameCounter;
				int num2 = -1;
				for (int j = 0; j < this.m_ListOfCachedShadowRequests.Count; j++)
				{
					HDShadowResolutionRequest hdshadowResolutionRequest = this.m_ListOfCachedShadowRequests[j];
					if (hdshadowResolutionRequest.emptyRequest && request.atlasViewport.width <= hdshadowResolutionRequest.atlasViewport.width && hdshadowResolutionRequest.atlasViewport.width - request.atlasViewport.width <= hdshadowResolutionRequest.atlasViewport.width * 0.1f)
					{
						num2 = j;
						break;
					}
				}
				if (num2 >= 0)
				{
					this.m_ListOfCachedShadowRequests[num2] = request;
					return num2;
				}
				this.m_CachedResolutionRequestsCounter = 0;
				for (int k = 0; k < this.m_ListOfCachedShadowRequests.Count; k++)
				{
					int cachedResolutionRequestsCounter = this.m_CachedResolutionRequestsCounter;
					this.m_CachedResolutionRequests[cachedResolutionRequestsCounter] = this.m_ListOfCachedShadowRequests[k].ShallowCopy();
					this.m_ListOfCachedShadowRequests[k] = this.m_CachedResolutionRequests[cachedResolutionRequestsCounter];
					this.m_CachedResolutionRequestsCounter++;
				}
				this.m_CachedResolutionRequests[this.m_CachedResolutionRequestsCounter] = request.ShallowCopy();
				this.m_ListOfCachedShadowRequests.Add(this.m_CachedResolutionRequests[this.m_CachedResolutionRequestsCounter]);
				this.m_CachedResolutionRequestsCounter++;
				this.InsertionSort(this.m_ListOfCachedShadowRequests.ToArray(), 0, this.m_ListOfCachedShadowRequests.Count);
				this.frameOfCacheValidity = 0;
				for (int l = 0; l < this.m_ListOfCachedShadowRequests.Count; l++)
				{
					if (this.m_ListOfCachedShadowRequests[l].lightID == request.lightID && this.m_ListOfCachedShadowRequests[l].indexInLight == request.indexInLight)
					{
						return l;
					}
				}
			}
			else if (this.m_ListOfCachedShadowRequests[num].emptyRequest)
			{
				this.m_ListOfCachedShadowRequests[num].emptyRequest = false;
			}
			this.m_ListOfCachedShadowRequests[num].lastFrameActive = this.frameCounter;
			return num;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0002FA6C File Offset: 0x0002DC6C
		private bool AtlasLayout(bool allowResize, HDShadowResolutionRequest[] fullShadowList, int requestsCount, bool enteredWithPrunedCachedList = false)
		{
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = (float)this.width;
			float num5 = (float)this.height;
			this.m_RcpScaleFactor = 1f;
			int i = 0;
			while (i < requestsCount)
			{
				HDShadowResolutionRequest hdshadowResolutionRequest = fullShadowList[i];
				Rect rect = new Rect(Vector2.zero, hdshadowResolutionRequest.resolution);
				num3 = Mathf.Max(num3, rect.height);
				if (num + rect.width > num4)
				{
					num = 0f;
					num2 += num3;
					num3 = rect.height;
				}
				if (num2 + num3 > num5)
				{
					if (!enteredWithPrunedCachedList)
					{
						this.PruneDeadCachedLightSlots();
						int num6 = 0;
						for (int j = 0; j < requestsCount; j++)
						{
							if (!fullShadowList[j].emptyRequest)
							{
								this.m_SortedRequestsCache[num6++] = fullShadowList[j];
							}
						}
						return this.AtlasLayout(allowResize, this.m_SortedRequestsCache, num6, true);
					}
					this.frameOfCacheValidity = 0;
					this.m_ListOfCachedShadowRequests.Clear();
					this.m_CachedResolutionRequestsCounter = 0;
					if (allowResize)
					{
						this.LayoutResize();
						this.m_HasResizedAtlas = true;
						return true;
					}
					return false;
				}
				else
				{
					rect.x = num;
					rect.y = num2;
					hdshadowResolutionRequest.atlasViewport = rect;
					hdshadowResolutionRequest.resolution = rect.size;
					num += rect.width;
					i++;
				}
			}
			this.m_HasResizedAtlas = false;
			return true;
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0002FBBC File Offset: 0x0002DDBC
		internal bool Layout(bool allowResize = true)
		{
			if (this.m_ShadowResolutionRequests != null)
			{
				int count = this.m_ShadowResolutionRequests.Count;
			}
			int i;
			for (i = 0; i < this.m_ListOfCachedShadowRequests.Count; i++)
			{
				this.m_SortedRequestsCache[i] = this.m_ListOfCachedShadowRequests[i];
			}
			int num = i;
			for (int j = 0; j < this.m_ShadowResolutionRequests.Count; j++)
			{
				if (!this.m_ShadowResolutionRequests[j].hasBeenStoredInCachedList)
				{
					this.m_SortedRequestsCache[i++] = this.m_ShadowResolutionRequests[j];
				}
			}
			this.InsertionSort(this.m_SortedRequestsCache, num, i);
			return this.AtlasLayout(allowResize, this.m_SortedRequestsCache, i, false);
		}

		// Token: 0x060005B5 RID: 1461 RVA: 0x0002FC68 File Offset: 0x0002DE68
		private void LayoutResize()
		{
			int i = 0;
			float num = 0f;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			while (i < this.m_ShadowResolutionRequests.Count)
			{
				float num5 = 0f;
				float num6 = num4;
				do
				{
					Rect rect = new Rect(Vector2.zero, this.m_ShadowResolutionRequests[i].resolution);
					rect.x = num4;
					rect.y = num5;
					num5 += rect.height;
					num2 = Mathf.Max(num2, num5);
					num6 = Mathf.Max(num6, num4 + rect.width);
					this.m_ShadowResolutionRequests[i].atlasViewport = rect;
					i++;
				}
				while (num5 < num3 && i < this.m_ShadowResolutionRequests.Count);
				num3 = Mathf.Max(num3, num2);
				num4 = num6;
				if (i < this.m_ShadowResolutionRequests.Count)
				{
					float num7 = 0f;
					float num8 = num3;
					do
					{
						Rect rect2 = new Rect(Vector2.zero, this.m_ShadowResolutionRequests[i].resolution);
						rect2.x = num7;
						rect2.y = num3;
						num7 += rect2.width;
						num = Mathf.Max(num, num7);
						num8 = Mathf.Max(num8, num3 + rect2.height);
						this.m_ShadowResolutionRequests[i].atlasViewport = rect2;
						i++;
					}
					while (num7 < num4 && i < this.m_ShadowResolutionRequests.Count);
					num4 = Mathf.Max(num4, num);
					num3 = num8;
				}
			}
			float num9 = Math.Max(num4, num3);
			Vector4 vector = new Vector4((float)this.width / num9, (float)this.height / num9, (float)this.width / num9, (float)this.height / num9);
			this.m_RcpScaleFactor = Mathf.Min(vector.x, vector.y);
			foreach (HDShadowResolutionRequest hdshadowResolutionRequest in this.m_ShadowResolutionRequests)
			{
				Vector4 vector2 = Vector4.Scale(new Vector4(hdshadowResolutionRequest.atlasViewport.x, hdshadowResolutionRequest.atlasViewport.y, hdshadowResolutionRequest.atlasViewport.width, hdshadowResolutionRequest.atlasViewport.height), vector);
				hdshadowResolutionRequest.atlasViewport = new Rect(vector2.x, vector2.y, vector2.z, vector2.w);
				hdshadowResolutionRequest.resolution = hdshadowResolutionRequest.atlasViewport.size;
			}
			int atlasShapeID = this.atlasShapeID;
			this.atlasShapeID = atlasShapeID + 1;
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x0002FF10 File Offset: 0x0002E110
		public void RenderShadows(CullingResults cullResults, FrameSettings frameSettings, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			if (this.m_ShadowRequests.Count == 0)
			{
				return;
			}
			ShadowDrawingSettings shadowDrawingSettings = new ShadowDrawingSettings(cullResults, 0);
			shadowDrawingSettings.useRenderingLayerMaskTest = frameSettings.IsEnabled(FrameSettingsField.LightLayers);
			HDShadowAtlas.RenderShadowsParameters renderShadowsParameters = this.PrepareRenderShadowsParameters();
			HDShadowAtlas.RenderShadows(renderShadowsParameters, this.m_Atlas, shadowDrawingSettings, renderContext, cmd);
			if (renderShadowsParameters.blurAlgorithm == HDShadowAtlas.BlurAlgorithm.IM)
			{
				HDShadowAtlas.IMBlurMoment(renderShadowsParameters, this.m_Atlas, this.m_AtlasMoments[0], this.m_IntermediateSummedAreaTexture, this.m_SummedAreaTexture, cmd);
				return;
			}
			if (renderShadowsParameters.blurAlgorithm == HDShadowAtlas.BlurAlgorithm.EVSM)
			{
				HDShadowAtlas.EVSMBlurMoments(renderShadowsParameters, this.m_Atlas, this.m_AtlasMoments, cmd);
			}
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x0002FFA4 File Offset: 0x0002E1A4
		private HDShadowAtlas.RenderShadowsParameters PrepareRenderShadowsParameters()
		{
			return new HDShadowAtlas.RenderShadowsParameters
			{
				shadowRequests = this.m_ShadowRequests,
				clearMaterial = this.m_ClearMaterial,
				debugClearAtlas = this.m_LightingDebugSettings.clearShadowAtlas,
				atlasShaderID = this.m_AtlasShaderID,
				atlasSizeShaderID = this.m_AtlasSizeShaderID,
				blurAlgorithm = this.m_BlurAlgorithm,
				evsmShadowBlurMomentsCS = this.m_RenderPipelineResources.shaders.evsmBlurCS,
				momentAtlasShaderID = this.m_MomentAtlasShaderID,
				imShadowBlurMomentsCS = this.m_RenderPipelineResources.shaders.momentShadowsCS
			};
		}

		// Token: 0x060005B8 RID: 1464 RVA: 0x00030048 File Offset: 0x0002E248
		private static void RenderShadows(HDShadowAtlas.RenderShadowsParameters parameters, RTHandle atlasRenderTexture, ShadowDrawingSettings shadowDrawSettings, ScriptableRenderContext renderContext, CommandBuffer cmd)
		{
			cmd.SetRenderTarget(atlasRenderTexture, RenderBufferLoadAction.DontCare, RenderBufferStoreAction.Store);
			cmd.SetGlobalVector(parameters.atlasSizeShaderID, new Vector4((float)atlasRenderTexture.rt.width, (float)atlasRenderTexture.rt.height, 1f / (float)atlasRenderTexture.rt.width, 1f / (float)atlasRenderTexture.rt.height));
			if (parameters.debugClearAtlas)
			{
				CoreUtils.DrawFullScreen(cmd, parameters.clearMaterial, null, 0);
			}
			foreach (HDShadowRequest hdshadowRequest in parameters.shadowRequests)
			{
				if (!hdshadowRequest.shouldUseCachedShadow)
				{
					cmd.SetGlobalDepthBias(1f, hdshadowRequest.slopeBias);
					cmd.SetViewport(hdshadowRequest.atlasViewport);
					cmd.SetGlobalFloat(HDShaderIDs._ZClip, hdshadowRequest.zClip ? 1f : 0f);
					CoreUtils.DrawFullScreen(cmd, parameters.clearMaterial, null, 0);
					shadowDrawSettings.lightIndex = hdshadowRequest.lightIndex;
					shadowDrawSettings.splitData = hdshadowRequest.splitData;
					Matrix4x4 matrix4x = hdshadowRequest.deviceProjectionYFlip * hdshadowRequest.view;
					cmd.SetGlobalMatrix(HDShaderIDs._ViewMatrix, hdshadowRequest.view);
					cmd.SetGlobalMatrix(HDShaderIDs._InvViewMatrix, hdshadowRequest.view.inverse);
					cmd.SetGlobalMatrix(HDShaderIDs._ProjMatrix, hdshadowRequest.deviceProjectionYFlip);
					cmd.SetGlobalMatrix(HDShaderIDs._InvProjMatrix, hdshadowRequest.deviceProjectionYFlip.inverse);
					cmd.SetGlobalMatrix(HDShaderIDs._ViewProjMatrix, matrix4x);
					cmd.SetGlobalMatrix(HDShaderIDs._InvViewProjMatrix, matrix4x.inverse);
					cmd.SetGlobalVectorArray(HDShaderIDs._ShadowClipPlanes, hdshadowRequest.frustumPlanes);
					renderContext.ExecuteCommandBuffer(cmd);
					cmd.Clear();
					renderContext.DrawShadows(ref shadowDrawSettings);
				}
			}
			cmd.SetGlobalFloat(HDShaderIDs._ZClip, 1f);
			cmd.SetGlobalDepthBias(0f, 0f);
		}

		// Token: 0x060005B9 RID: 1465 RVA: 0x00030260 File Offset: 0x0002E460
		public bool HasBlurredEVSM()
		{
			return this.m_BlurAlgorithm == HDShadowAtlas.BlurAlgorithm.EVSM && this.m_AtlasMoments[0] != null;
		}

		// Token: 0x060005BA RID: 1466 RVA: 0x00030278 File Offset: 0x0002E478
		private unsafe static void EVSMBlurMoments(HDShadowAtlas.RenderShadowsParameters parameters, RTHandle atlasRenderTexture, RTHandle[] momentAtlasRenderTextures, CommandBuffer cmd)
		{
			HDShadowAtlas.<>c__DisplayClass66_0 CS$<>8__locals1;
			CS$<>8__locals1.momentAtlasRenderTextures = momentAtlasRenderTextures;
			ComputeShader evsmShadowBlurMomentsCS = parameters.evsmShadowBlurMomentsCS;
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderEVSMShadowMaps)))
			{
				int num = evsmShadowBlurMomentsCS.FindKernel("ConvertAndBlur");
				int num2 = evsmShadowBlurMomentsCS.FindKernel("Blur");
				int num3 = evsmShadowBlurMomentsCS.FindKernel("CopyMoments");
				cmd.SetComputeTextureParam(evsmShadowBlurMomentsCS, num, HDShaderIDs._DepthTexture, atlasRenderTexture);
				cmd.SetComputeVectorArrayParam(evsmShadowBlurMomentsCS, HDShaderIDs._BlurWeightsStorage, HDShadowAtlas.evsmBlurWeights);
				int* ptr;
				int num4;
				checked
				{
					ptr = stackalloc int[unchecked((UIntPtr)parameters.shadowRequests.Count) * 4];
					num4 = 0;
				}
				foreach (HDShadowRequest hdshadowRequest in parameters.shadowRequests)
				{
					using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderEVSMShadowMapsBlur)))
					{
						int num5 = Mathf.CeilToInt(hdshadowRequest.atlasViewport.width * 0.5f);
						int num6 = Mathf.CeilToInt(hdshadowRequest.atlasViewport.height * 0.5f);
						Vector2 vector = new Vector2(hdshadowRequest.atlasViewport.min.x * 0.5f, hdshadowRequest.atlasViewport.min.y * 0.5f);
						cmd.SetComputeTextureParam(evsmShadowBlurMomentsCS, num, HDShaderIDs._OutputTexture, CS$<>8__locals1.momentAtlasRenderTextures[0]);
						cmd.SetComputeVectorParam(evsmShadowBlurMomentsCS, HDShaderIDs._SrcRect, new Vector4(hdshadowRequest.atlasViewport.min.x, hdshadowRequest.atlasViewport.min.y, hdshadowRequest.atlasViewport.width, hdshadowRequest.atlasViewport.height));
						cmd.SetComputeVectorParam(evsmShadowBlurMomentsCS, HDShaderIDs._DstRect, new Vector4(vector.x, vector.y, 1f / (float)atlasRenderTexture.rt.width, 1f / (float)atlasRenderTexture.rt.height));
						cmd.SetComputeFloatParam(evsmShadowBlurMomentsCS, HDShaderIDs._EVSMExponent, hdshadowRequest.evsmParams.x);
						int num7 = (num5 + 7) / 8;
						int num8 = (num6 + 7) / 8;
						cmd.DispatchCompute(evsmShadowBlurMomentsCS, num, num7, num8, 1);
						HDShadowAtlas.<>c__DisplayClass66_1 CS$<>8__locals2;
						CS$<>8__locals2.currentAtlasMomentSurface = 0;
						cmd.SetComputeVectorParam(evsmShadowBlurMomentsCS, HDShaderIDs._SrcRect, new Vector4(vector.x, vector.y, (float)num5, (float)num6));
						int num9 = 0;
						while ((float)num9 < hdshadowRequest.evsmParams.w)
						{
							CS$<>8__locals2.currentAtlasMomentSurface = (CS$<>8__locals2.currentAtlasMomentSurface + 1) & 1;
							cmd.SetComputeTextureParam(evsmShadowBlurMomentsCS, num2, HDShaderIDs._InputTexture, HDShadowAtlas.<EVSMBlurMoments>g__GetMomentRTCopy|66_1(ref CS$<>8__locals1, ref CS$<>8__locals2));
							cmd.SetComputeTextureParam(evsmShadowBlurMomentsCS, num2, HDShaderIDs._OutputTexture, HDShadowAtlas.<EVSMBlurMoments>g__GetMomentRT|66_0(ref CS$<>8__locals1, ref CS$<>8__locals2));
							cmd.DispatchCompute(evsmShadowBlurMomentsCS, num2, num7, num8, 1);
							num9++;
						}
						ptr[(IntPtr)(num4++) * 4] = CS$<>8__locals2.currentAtlasMomentSurface;
					}
				}
				for (int i = 0; i < parameters.shadowRequests.Count; i++)
				{
					if (ptr[i] != 0)
					{
						using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderEVSMShadowMapsCopyToAtlas)))
						{
							HDShadowRequest hdshadowRequest2 = parameters.shadowRequests[i];
							int num10 = Mathf.CeilToInt(hdshadowRequest2.atlasViewport.width * 0.5f);
							int num11 = Mathf.CeilToInt(hdshadowRequest2.atlasViewport.height * 0.5f);
							cmd.SetComputeVectorParam(evsmShadowBlurMomentsCS, HDShaderIDs._SrcRect, new Vector4(hdshadowRequest2.atlasViewport.min.x * 0.5f, hdshadowRequest2.atlasViewport.min.y * 0.5f, (float)num10, (float)num11));
							cmd.SetComputeTextureParam(evsmShadowBlurMomentsCS, num3, HDShaderIDs._InputTexture, CS$<>8__locals1.momentAtlasRenderTextures[1]);
							cmd.SetComputeTextureParam(evsmShadowBlurMomentsCS, num3, HDShaderIDs._OutputTexture, CS$<>8__locals1.momentAtlasRenderTextures[0]);
							int num12 = (num10 + 7) / 8;
							int num13 = (num11 + 7) / 8;
							cmd.DispatchCompute(evsmShadowBlurMomentsCS, num3, num12, num13, 1);
						}
					}
				}
			}
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000306F0 File Offset: 0x0002E8F0
		private static void IMBlurMoment(HDShadowAtlas.RenderShadowsParameters parameters, RTHandle atlas, RTHandle atlasMoment, RTHandle intermediateSummedAreaTexture, RTHandle summedAreaTexture, CommandBuffer cmd)
		{
			ComputeShader imShadowBlurMomentsCS = parameters.imShadowBlurMomentsCS;
			if (imShadowBlurMomentsCS == null)
			{
				return;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderMomentShadowMaps)))
			{
				int num = imShadowBlurMomentsCS.FindKernel("ComputeMomentShadows");
				int num2 = imShadowBlurMomentsCS.FindKernel("MomentSummedAreaTableHorizontal");
				int num3 = imShadowBlurMomentsCS.FindKernel("MomentSummedAreaTableVertical");
				CoreUtils.SetRenderTarget(cmd, atlasMoment, ClearFlag.Color, Color.black, 0, CubemapFace.Unknown, -1);
				CoreUtils.SetRenderTarget(cmd, intermediateSummedAreaTexture, ClearFlag.Color, Color.black, 0, CubemapFace.Unknown, -1);
				CoreUtils.SetRenderTarget(cmd, summedAreaTexture, ClearFlag.Color, Color.black, 0, CubemapFace.Unknown, -1);
				foreach (HDShadowRequest hdshadowRequest in parameters.shadowRequests)
				{
					cmd.SetComputeTextureParam(imShadowBlurMomentsCS, num, HDShaderIDs._ShadowmapAtlas, atlas);
					cmd.SetComputeTextureParam(imShadowBlurMomentsCS, num, HDShaderIDs._MomentShadowAtlas, atlasMoment);
					cmd.SetComputeVectorParam(imShadowBlurMomentsCS, HDShaderIDs._MomentShadowmapSlotST, new Vector4(hdshadowRequest.atlasViewport.width, hdshadowRequest.atlasViewport.height, hdshadowRequest.atlasViewport.min.x, hdshadowRequest.atlasViewport.min.y));
					int num4 = Math.Max((int)hdshadowRequest.atlasViewport.width / 8, 1);
					int num5 = Math.Max((int)hdshadowRequest.atlasViewport.height / 8, 1);
					cmd.DispatchCompute(imShadowBlurMomentsCS, num, num4, num5, 1);
					cmd.SetComputeTextureParam(imShadowBlurMomentsCS, num2, HDShaderIDs._SummedAreaTableInputFloat, atlasMoment);
					cmd.SetComputeTextureParam(imShadowBlurMomentsCS, num2, HDShaderIDs._SummedAreaTableOutputInt, intermediateSummedAreaTexture);
					cmd.SetComputeFloatParam(imShadowBlurMomentsCS, HDShaderIDs._IMSKernelSize, hdshadowRequest.kernelSize);
					cmd.SetComputeVectorParam(imShadowBlurMomentsCS, HDShaderIDs._MomentShadowmapSize, new Vector2((float)atlasMoment.referenceSize.x, (float)atlasMoment.referenceSize.y));
					int num6 = Math.Max((int)hdshadowRequest.atlasViewport.width / 64, 1);
					cmd.DispatchCompute(imShadowBlurMomentsCS, num2, num6, 1, 1);
					cmd.SetComputeTextureParam(imShadowBlurMomentsCS, num3, HDShaderIDs._SummedAreaTableInputInt, intermediateSummedAreaTexture);
					cmd.SetComputeTextureParam(imShadowBlurMomentsCS, num3, HDShaderIDs._SummedAreaTableOutputInt, summedAreaTexture);
					cmd.SetComputeVectorParam(imShadowBlurMomentsCS, HDShaderIDs._MomentShadowmapSize, new Vector2((float)atlasMoment.referenceSize.x, (float)atlasMoment.referenceSize.y));
					cmd.SetComputeFloatParam(imShadowBlurMomentsCS, HDShaderIDs._IMSKernelSize, hdshadowRequest.kernelSize);
					int num7 = Math.Max((int)hdshadowRequest.atlasViewport.height / 64, 1);
					cmd.DispatchCompute(imShadowBlurMomentsCS, num3, num7, 1, 1);
					cmd.SetGlobalTexture(HDShaderIDs._SummedAreaTableInputInt, summedAreaTexture);
				}
			}
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000309E8 File Offset: 0x0002EBE8
		public void DisplayAtlas(RTHandle atlasTexture, CommandBuffer cmd, Material debugMaterial, Rect atlasViewport, float screenX, float screenY, float screenSizeX, float screenSizeY, float minValue, float maxValue, MaterialPropertyBlock mpb)
		{
			if (atlasTexture == null)
			{
				return;
			}
			Vector4 vector = new Vector4(minValue, 1f / (maxValue - minValue));
			float num = 1f / (float)this.width;
			float num2 = 1f / (float)this.height;
			Vector4 vector2 = Vector4.Scale(new Vector4(num, num2, num, num2), new Vector4(atlasViewport.width, atlasViewport.height, atlasViewport.x, atlasViewport.y));
			mpb.SetTexture("_AtlasTexture", atlasTexture);
			mpb.SetVector("_TextureScaleBias", vector2);
			mpb.SetVector("_ValidRange", vector);
			mpb.SetFloat("_RcpGlobalScaleFactor", this.m_RcpScaleFactor);
			cmd.SetViewport(new Rect(screenX, screenY, screenSizeX, screenSizeY));
			cmd.DrawProcedural(Matrix4x4.identity, debugMaterial, debugMaterial.FindPass("RegularShadow"), MeshTopology.Triangles, 3, 1, mpb);
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00030AC6 File Offset: 0x0002ECC6
		public void Clear()
		{
			this.m_ShadowResolutionRequests.Clear();
			this.m_ShadowRequests.Clear();
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00030AE0 File Offset: 0x0002ECE0
		public void Release()
		{
			if (this.m_Atlas != null)
			{
				RTHandles.Release(this.m_Atlas);
			}
			if (this.m_AtlasMoments != null && this.m_AtlasMoments.Length != 0)
			{
				for (int i = 0; i < this.m_AtlasMoments.Length; i++)
				{
					if (this.m_AtlasMoments[i] != null)
					{
						RTHandles.Release(this.m_AtlasMoments[i]);
						this.m_AtlasMoments[i] = null;
					}
				}
			}
			if (this.m_IntermediateSummedAreaTexture != null)
			{
				RTHandles.Release(this.m_IntermediateSummedAreaTexture);
				this.m_IntermediateSummedAreaTexture = null;
			}
			if (this.m_SummedAreaTexture != null)
			{
				RTHandles.Release(this.m_SummedAreaTexture);
				this.m_SummedAreaTexture = null;
			}
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00030B78 File Offset: 0x0002ED78
		private RenderGraphMutableResource AllocateMomentAtlas(RenderGraph renderGraph, string name, int shaderID = 0)
		{
			return renderGraph.CreateTexture(new TextureDesc(this.width / 2, this.height / 2, false, false)
			{
				colorFormat = GraphicsFormat.R32G32_SFloat,
				useMipMap = true,
				autoGenerateMips = false,
				name = name,
				enableRandomWrite = true
			}, shaderID);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x00030BD0 File Offset: 0x0002EDD0
		internal RenderGraphResource RenderShadows(RenderGraph renderGraph, CullingResults cullResults, FrameSettings frameSettings, string shadowPassName)
		{
			RenderGraphResource renderGraphResource = default(RenderGraphResource);
			if (this.m_ShadowRequests.Count == 0)
			{
				return renderGraphResource;
			}
			HDShadowAtlas.RenderShadowsPassData renderShadowsPassData;
			RenderGraphResource renderGraphResource2;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<HDShadowAtlas.RenderShadowsPassData>(shadowPassName, out renderShadowsPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderShadowMaps)))
			{
				renderShadowsPassData.parameters = this.PrepareRenderShadowsParameters();
				renderShadowsPassData.shadowDrawSettings = new ShadowDrawingSettings(cullResults, 0);
				renderShadowsPassData.shadowDrawSettings.useRenderingLayerMaskTest = frameSettings.IsEnabled(FrameSettingsField.LightLayers);
				HDShadowAtlas.RenderShadowsPassData renderShadowsPassData2 = renderShadowsPassData;
				RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(this.width, this.height, false, false)
				{
					filterMode = this.m_FilterMode,
					depthBufferBits = this.m_DepthBufferBits,
					isShadowMap = true,
					name = this.m_Name,
					clearBuffer = renderShadowsPassData.parameters.debugClearAtlas
				}, renderShadowsPassData.parameters.atlasShaderID);
				renderShadowsPassData2.atlasTexture = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				renderGraphResource = renderShadowsPassData.atlasTexture;
				if (renderShadowsPassData.parameters.blurAlgorithm == HDShadowAtlas.BlurAlgorithm.EVSM)
				{
					HDShadowAtlas.RenderShadowsPassData renderShadowsPassData3 = renderShadowsPassData;
					renderGraphMutableResource = this.AllocateMomentAtlas(renderGraph, string.Format("{0}Moment", this.m_Name), renderShadowsPassData.parameters.momentAtlasShaderID);
					renderShadowsPassData3.momentAtlasTexture1 = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
					HDShadowAtlas.RenderShadowsPassData renderShadowsPassData4 = renderShadowsPassData;
					renderGraphMutableResource = this.AllocateMomentAtlas(renderGraph, string.Format("{0}MomentCopy", this.m_Name), 0);
					renderShadowsPassData4.momentAtlasTexture2 = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
					renderGraphResource = renderShadowsPassData.momentAtlasTexture1;
				}
				else if (renderShadowsPassData.parameters.blurAlgorithm == HDShadowAtlas.BlurAlgorithm.IM)
				{
					HDShadowAtlas.RenderShadowsPassData renderShadowsPassData5 = renderShadowsPassData;
					renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(this.width, this.height, false, false)
					{
						colorFormat = GraphicsFormat.R32G32B32A32_SFloat,
						name = string.Format("{0}Moment", this.m_Name),
						enableRandomWrite = true
					}, renderShadowsPassData.parameters.momentAtlasShaderID);
					renderShadowsPassData5.momentAtlasTexture1 = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
					HDShadowAtlas.RenderShadowsPassData renderShadowsPassData6 = renderShadowsPassData;
					renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(this.width, this.height, false, false)
					{
						colorFormat = GraphicsFormat.R32G32B32A32_SInt,
						name = string.Format("{0}IntermediateSummedArea", this.m_Name),
						enableRandomWrite = true
					}, renderShadowsPassData.parameters.momentAtlasShaderID);
					renderShadowsPassData6.intermediateSummedAreaTexture = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
					HDShadowAtlas.RenderShadowsPassData renderShadowsPassData7 = renderShadowsPassData;
					renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(this.width, this.height, false, false)
					{
						colorFormat = GraphicsFormat.R32G32B32A32_SInt,
						name = string.Format("{0}SummedArea", this.m_Name),
						enableRandomWrite = true
					}, renderShadowsPassData.parameters.momentAtlasShaderID);
					renderShadowsPassData7.summedAreaTexture = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
					renderGraphResource = renderShadowsPassData.momentAtlasTexture1;
				}
				renderGraphBuilder.SetRenderFunc<HDShadowAtlas.RenderShadowsPassData>(delegate(HDShadowAtlas.RenderShadowsPassData data, RenderGraphContext context)
				{
					RenderGraphResourceRegistry resources = context.resources;
					RenderGraphResource renderGraphResource3 = data.atlasTexture;
					RTHandle texture = resources.GetTexture(in renderGraphResource3);
					HDShadowAtlas.RenderShadows(data.parameters, texture, data.shadowDrawSettings, context.renderContext, context.cmd);
					if (data.parameters.blurAlgorithm == HDShadowAtlas.BlurAlgorithm.EVSM)
					{
						RTHandle[] tempArray = context.renderGraphPool.GetTempArray<RTHandle>(2);
						RTHandle[] array = tempArray;
						int num = 0;
						RenderGraphResourceRegistry resources2 = context.resources;
						renderGraphResource3 = data.momentAtlasTexture1;
						array[num] = resources2.GetTexture(in renderGraphResource3);
						RTHandle[] array2 = tempArray;
						int num2 = 1;
						RenderGraphResourceRegistry resources3 = context.resources;
						renderGraphResource3 = data.momentAtlasTexture2;
						array2[num2] = resources3.GetTexture(in renderGraphResource3);
						HDShadowAtlas.EVSMBlurMoments(data.parameters, texture, tempArray, context.cmd);
						return;
					}
					if (data.parameters.blurAlgorithm == HDShadowAtlas.BlurAlgorithm.IM)
					{
						RenderGraphResourceRegistry resources4 = context.resources;
						renderGraphResource3 = data.momentAtlasTexture1;
						RTHandle texture2 = resources4.GetTexture(in renderGraphResource3);
						RenderGraphResourceRegistry resources5 = context.resources;
						renderGraphResource3 = data.intermediateSummedAreaTexture;
						RTHandle texture3 = resources5.GetTexture(in renderGraphResource3);
						RenderGraphResourceRegistry resources6 = context.resources;
						renderGraphResource3 = data.summedAreaTexture;
						RTHandle texture4 = resources6.GetTexture(in renderGraphResource3);
						HDShadowAtlas.IMBlurMoment(data.parameters, texture, texture2, texture3, texture4, context.cmd);
					}
				});
				renderGraphResource2 = renderGraphResource;
			}
			return renderGraphResource2;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00030F1C File Offset: 0x0002F11C
		[CompilerGenerated]
		internal static RTHandle <EVSMBlurMoments>g__GetMomentRT|66_0(ref HDShadowAtlas.<>c__DisplayClass66_0 A_0, ref HDShadowAtlas.<>c__DisplayClass66_1 A_1)
		{
			return A_0.momentAtlasRenderTextures[A_1.currentAtlasMomentSurface];
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00030F2B File Offset: 0x0002F12B
		[CompilerGenerated]
		internal static RTHandle <EVSMBlurMoments>g__GetMomentRTCopy|66_1(ref HDShadowAtlas.<>c__DisplayClass66_0 A_0, ref HDShadowAtlas.<>c__DisplayClass66_1 A_1)
		{
			return A_0.momentAtlasRenderTextures[(A_1.currentAtlasMomentSurface + 1) & 1];
		}

		// Token: 0x040005BD RID: 1469
		private readonly List<HDShadowResolutionRequest> m_ShadowResolutionRequests = new List<HDShadowResolutionRequest>();

		// Token: 0x040005BE RID: 1470
		private readonly List<HDShadowRequest> m_ShadowRequests = new List<HDShadowRequest>();

		// Token: 0x040005BF RID: 1471
		private readonly List<HDShadowResolutionRequest> m_ListOfCachedShadowRequests = new List<HDShadowResolutionRequest>();

		// Token: 0x040005C2 RID: 1474
		private RTHandle m_Atlas;

		// Token: 0x040005C3 RID: 1475
		private Material m_ClearMaterial;

		// Token: 0x040005C4 RID: 1476
		private LightingDebugSettings m_LightingDebugSettings;

		// Token: 0x040005C5 RID: 1477
		private float m_RcpScaleFactor = 1f;

		// Token: 0x040005C6 RID: 1478
		private FilterMode m_FilterMode;

		// Token: 0x040005C7 RID: 1479
		private DepthBits m_DepthBufferBits;

		// Token: 0x040005C8 RID: 1480
		private RenderTextureFormat m_Format;

		// Token: 0x040005C9 RID: 1481
		private string m_Name;

		// Token: 0x040005CA RID: 1482
		private int m_AtlasSizeShaderID;

		// Token: 0x040005CB RID: 1483
		private int m_AtlasShaderID;

		// Token: 0x040005CC RID: 1484
		private int m_MomentAtlasShaderID;

		// Token: 0x040005CD RID: 1485
		private RenderPipelineResources m_RenderPipelineResources;

		// Token: 0x040005CE RID: 1486
		private HDShadowAtlas.BlurAlgorithm m_BlurAlgorithm;

		// Token: 0x040005CF RID: 1487
		private RTHandle[] m_AtlasMoments;

		// Token: 0x040005D0 RID: 1488
		private RTHandle m_IntermediateSummedAreaTexture;

		// Token: 0x040005D1 RID: 1489
		private RTHandle m_SummedAreaTexture;

		// Token: 0x040005D2 RID: 1490
		private HDShadowResolutionRequest[] m_SortedRequestsCache;

		// Token: 0x040005D5 RID: 1493
		private HDShadowResolutionRequest[] m_CachedResolutionRequests;

		// Token: 0x040005D6 RID: 1494
		private int m_CachedResolutionRequestsCounter;

		// Token: 0x040005D7 RID: 1495
		private bool m_HasResizedAtlas;

		// Token: 0x040005D8 RID: 1496
		private int frameCounter;

		// Token: 0x040005D9 RID: 1497
		private static readonly Vector4[] evsmBlurWeights = new Vector4[]
		{
			new Vector4(0.1531703f, 0.1448929f, 0.1226492f, 0.0929025f),
			new Vector4(0.06297021f, 0f, 0f, 0f)
		};

		// Token: 0x02000214 RID: 532
		public enum BlurAlgorithm
		{
			// Token: 0x040013C7 RID: 5063
			None,
			// Token: 0x040013C8 RID: 5064
			EVSM,
			// Token: 0x040013C9 RID: 5065
			IM
		}

		// Token: 0x02000215 RID: 533
		private struct RenderShadowsParameters
		{
			// Token: 0x040013CA RID: 5066
			public List<HDShadowRequest> shadowRequests;

			// Token: 0x040013CB RID: 5067
			public Material clearMaterial;

			// Token: 0x040013CC RID: 5068
			public bool debugClearAtlas;

			// Token: 0x040013CD RID: 5069
			public int atlasShaderID;

			// Token: 0x040013CE RID: 5070
			public int atlasSizeShaderID;

			// Token: 0x040013CF RID: 5071
			public HDShadowAtlas.BlurAlgorithm blurAlgorithm;

			// Token: 0x040013D0 RID: 5072
			public ComputeShader evsmShadowBlurMomentsCS;

			// Token: 0x040013D1 RID: 5073
			public int momentAtlasShaderID;

			// Token: 0x040013D2 RID: 5074
			public ComputeShader imShadowBlurMomentsCS;
		}

		// Token: 0x02000216 RID: 534
		private class RenderShadowsPassData
		{
			// Token: 0x040013D3 RID: 5075
			public RenderGraphMutableResource atlasTexture;

			// Token: 0x040013D4 RID: 5076
			public RenderGraphMutableResource momentAtlasTexture1;

			// Token: 0x040013D5 RID: 5077
			public RenderGraphMutableResource momentAtlasTexture2;

			// Token: 0x040013D6 RID: 5078
			public RenderGraphMutableResource intermediateSummedAreaTexture;

			// Token: 0x040013D7 RID: 5079
			public RenderGraphMutableResource summedAreaTexture;

			// Token: 0x040013D8 RID: 5080
			public HDShadowAtlas.RenderShadowsParameters parameters;

			// Token: 0x040013D9 RID: 5081
			public ShadowDrawingSettings shadowDrawSettings;
		}
	}
}
