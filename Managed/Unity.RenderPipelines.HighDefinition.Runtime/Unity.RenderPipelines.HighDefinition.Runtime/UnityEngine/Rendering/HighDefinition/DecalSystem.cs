using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000B0 RID: 176
	internal class DecalSystem
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x00034EE8 File Offset: 0x000330E8
		public static DecalSystem instance
		{
			get
			{
				if (DecalSystem.m_Instance == null)
				{
					DecalSystem.m_Instance = new DecalSystem();
				}
				return DecalSystem.m_Instance;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000692 RID: 1682 RVA: 0x00034F00 File Offset: 0x00033100
		public int DrawDistance
		{
			get
			{
				HDRenderPipelineAsset currentAsset = HDRenderPipeline.currentAsset;
				if (currentAsset != null)
				{
					return currentAsset.currentPlatformRenderPipelineSettings.decalSettings.drawDistance;
				}
				return 1000;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00034F34 File Offset: 0x00033134
		public bool perChannelMask
		{
			get
			{
				HDRenderPipelineAsset currentAsset = HDRenderPipeline.currentAsset;
				return currentAsset != null && currentAsset.currentPlatformRenderPipelineSettings.decalSettings.perChannelMask;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00034F62 File Offset: 0x00033162
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x00034F6A File Offset: 0x0003316A
		public Camera CurrentCamera
		{
			get
			{
				return this.m_Camera;
			}
			set
			{
				this.m_Camera = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00034F74 File Offset: 0x00033174
		public Texture2DAtlas Atlas
		{
			get
			{
				if (this.m_Atlas == null)
				{
					this.m_Atlas = new Texture2DAtlas(HDUtils.hdrpSettings.decalSettings.atlasWidth, HDUtils.hdrpSettings.decalSettings.atlasHeight, GraphicsFormat.R8G8B8A8_UNorm, FilterMode.Point, false, "", true);
				}
				return this.m_Atlas;
			}
		}

		// Token: 0x06000697 RID: 1687 RVA: 0x00034FC1 File Offset: 0x000331C1
		public static bool IsHDRenderPipelineDecal(Shader shader)
		{
			return shader.name == "HDRP/Decal";
		}

		// Token: 0x06000698 RID: 1688 RVA: 0x00034FD4 File Offset: 0x000331D4
		public static bool IsHDRenderPipelineDecal(Material material)
		{
			foreach (string text in DecalSystem.s_MaterialDecalPassNames)
			{
				if (material.FindPass(text) != -1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000699 RID: 1689 RVA: 0x00035008 File Offset: 0x00033208
		private void SetupMipStreamingSettings(Texture texture, bool allMips)
		{
			if (texture && texture.dimension == TextureDimension.Tex2D)
			{
				Texture2D texture2D = texture as Texture2D;
				if (texture2D)
				{
					if (allMips)
					{
						texture2D.requestedMipmapLevel = 0;
						return;
					}
					texture2D.ClearRequestedMipmapLevel();
				}
			}
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00035048 File Offset: 0x00033248
		private void SetupMipStreamingSettings(Material material, bool allMips)
		{
			if (material != null && DecalSystem.IsHDRenderPipelineDecal(material.shader))
			{
				this.SetupMipStreamingSettings(material.GetTexture("_BaseColorMap"), allMips);
				this.SetupMipStreamingSettings(material.GetTexture("_NormalMap"), allMips);
				this.SetupMipStreamingSettings(material.GetTexture("_MaskMap"), allMips);
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x000350A4 File Offset: 0x000332A4
		private DecalSystem.DecalHandle AddDecal(Matrix4x4 localToWorld, Quaternion rotation, Matrix4x4 sizeOffset, float drawDistance, float fadeScale, Vector4 uvScaleBias, bool affectsTransparency, Material material, int layerMask, float fadeFactor)
		{
			this.SetupMipStreamingSettings(material, true);
			DecalSystem.DecalSet decalSet = null;
			int num = ((material != null) ? material.GetInstanceID() : int.MaxValue);
			if (!this.m_DecalSets.TryGetValue(num, out decalSet))
			{
				decalSet = new DecalSystem.DecalSet(material);
				this.m_DecalSets.Add(num, decalSet);
			}
			return decalSet.AddDecal(localToWorld, rotation, sizeOffset, drawDistance, fadeScale, uvScaleBias, affectsTransparency, num, layerMask, fadeFactor);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00035110 File Offset: 0x00033310
		public DecalSystem.DecalHandle AddDecal(Vector3 position, Quaternion rotation, Vector3 scale, Matrix4x4 sizeOffset, float drawDistance, float fadeScale, Vector4 uvScaleBias, bool affectsTransparency, Material material, int layerMask, float fadeFactor)
		{
			return this.AddDecal(Matrix4x4.TRS(position, rotation, scale), rotation, sizeOffset, drawDistance, fadeScale, uvScaleBias, affectsTransparency, material, layerMask, fadeFactor);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0003513C File Offset: 0x0003333C
		public DecalSystem.DecalHandle AddDecal(Transform transform, Matrix4x4 sizeOffset, float drawDistance, float fadeScale, Vector4 uvScaleBias, bool affectsTransparency, Material material, int layerMask, float fadeFactor)
		{
			return this.AddDecal(transform.localToWorldMatrix, transform.rotation, sizeOffset, drawDistance, fadeScale, uvScaleBias, affectsTransparency, material, layerMask, fadeFactor);
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0003516C File Offset: 0x0003336C
		public void RemoveDecal(DecalSystem.DecalHandle handle)
		{
			if (!DecalSystem.DecalHandle.IsValid(handle))
			{
				return;
			}
			DecalSystem.DecalSet decalSet = null;
			int materialID = handle.m_MaterialID;
			if (this.m_DecalSets.TryGetValue(materialID, out decalSet))
			{
				decalSet.RemoveDecal(handle);
				if (decalSet.Count == 0)
				{
					this.SetupMipStreamingSettings(decalSet.KeyMaterial, false);
					this.m_DecalSets.Remove(materialID);
				}
			}
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000351C4 File Offset: 0x000333C4
		private void UpdateCachedData(Matrix4x4 localToWorld, Quaternion rotation, Matrix4x4 sizeOffset, float drawDistance, float fadeScale, Vector4 uvScaleBias, bool affectsTransparency, DecalSystem.DecalHandle handle, int layerMask, float fadeFactor)
		{
			if (!DecalSystem.DecalHandle.IsValid(handle))
			{
				return;
			}
			DecalSystem.DecalSet decalSet = null;
			int materialID = handle.m_MaterialID;
			if (this.m_DecalSets.TryGetValue(materialID, out decalSet))
			{
				decalSet.UpdateCachedData(localToWorld, rotation, sizeOffset, drawDistance, fadeScale, uvScaleBias, affectsTransparency, handle, layerMask, fadeFactor);
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0003520C File Offset: 0x0003340C
		public void UpdateCachedData(Vector3 position, Quaternion rotation, Matrix4x4 sizeOffset, float drawDistance, float fadeScale, Vector4 uvScaleBias, bool affectsTransparency, DecalSystem.DecalHandle handle, int layerMask, float fadeFactor)
		{
			this.UpdateCachedData(Matrix4x4.TRS(position, rotation, Vector3.one), rotation, sizeOffset, drawDistance, fadeScale, uvScaleBias, affectsTransparency, handle, layerMask, fadeFactor);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0003523C File Offset: 0x0003343C
		public void UpdateCachedData(Transform transform, Matrix4x4 sizeOffset, float drawDistance, float fadeScale, Vector4 uvScaleBias, bool affectsTransparency, DecalSystem.DecalHandle handle, int layerMask, float fadeFactor)
		{
			this.UpdateCachedData(Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one), transform.rotation, sizeOffset, drawDistance, fadeScale, uvScaleBias, affectsTransparency, handle, layerMask, fadeFactor);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0003527C File Offset: 0x0003347C
		public void BeginCull(DecalSystem.CullRequest request)
		{
			request.Clear();
			foreach (KeyValuePair<int, DecalSystem.DecalSet> keyValuePair in this.m_DecalSets)
			{
				keyValuePair.Value.BeginCull(request[keyValuePair.Key]);
			}
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x000352E8 File Offset: 0x000334E8
		private int QueryCullResults(DecalSystem.CullRequest decalCullRequest, DecalSystem.CullResult cullResults)
		{
			int num = 0;
			foreach (KeyValuePair<int, DecalSystem.DecalSet> keyValuePair in this.m_DecalSets)
			{
				num += keyValuePair.Value.QueryCullResults(decalCullRequest[keyValuePair.Key], cullResults[keyValuePair.Key]);
			}
			return num;
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x00035360 File Offset: 0x00033560
		public void EndCull(DecalSystem.CullRequest cullRequest, DecalSystem.CullResult cullResults)
		{
			DecalSystem.m_DecalsVisibleThisFrame = this.QueryCullResults(cullRequest, cullResults);
			foreach (KeyValuePair<int, DecalSystem.DecalSet> keyValuePair in this.m_DecalSets)
			{
				keyValuePair.Value.EndCull(cullRequest[keyValuePair.Key]);
			}
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x000353D4 File Offset: 0x000335D4
		public void RenderIntoDBuffer(CommandBuffer cmd)
		{
			if (DecalSystem.m_DecalMesh == null)
			{
				DecalSystem.m_DecalMesh = CoreUtils.CreateCubeMesh(DecalSystem.kMin, DecalSystem.kMax);
			}
			foreach (DecalSystem.DecalSet decalSet in this.m_DecalSetsRenderList)
			{
				decalSet.RenderIntoDBuffer(cmd);
			}
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x00035450 File Offset: 0x00033650
		public void RenderForwardEmissive(CommandBuffer cmd)
		{
			if (DecalSystem.m_DecalMesh == null)
			{
				DecalSystem.m_DecalMesh = CoreUtils.CreateCubeMesh(DecalSystem.kMin, DecalSystem.kMax);
			}
			foreach (DecalSystem.DecalSet decalSet in this.m_DecalSetsRenderList)
			{
				decalSet.RenderForwardEmissive(cmd);
			}
		}

		// Token: 0x060006A7 RID: 1703 RVA: 0x000354CC File Offset: 0x000336CC
		public void SetAtlas(CommandBuffer cmd)
		{
			cmd.SetGlobalTexture(HDShaderIDs._DecalAtlas2DID, this.Atlas.AtlasTexture);
		}

		// Token: 0x060006A8 RID: 1704 RVA: 0x000354E9 File Offset: 0x000336E9
		public void AddTexture(CommandBuffer cmd, DecalSystem.TextureScaleBias textureScaleBias)
		{
			if (textureScaleBias.m_Texture != null)
			{
				if (!this.Atlas.AddTexture(cmd, ref textureScaleBias.m_ScaleBias, textureScaleBias.m_Texture))
				{
					this.m_AllocationSuccess = false;
					return;
				}
			}
			else
			{
				textureScaleBias.m_ScaleBias = Vector4.zero;
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x00035528 File Offset: 0x00033728
		public void UpdateCachedMaterialData()
		{
			this.m_TextureList.Clear();
			foreach (KeyValuePair<int, DecalSystem.DecalSet> keyValuePair in this.m_DecalSets)
			{
				keyValuePair.Value.InitializeMaterialValues();
			}
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0003558C File Offset: 0x0003378C
		private void UpdateDecalDatasWithAtlasInfo()
		{
			for (int i = 0; i < DecalSystem.m_DecalDatasCount; i++)
			{
				DecalSystem.m_DecalDatas[i].diffuseScaleBias = DecalSystem.m_DiffuseTextureScaleBias[i].m_ScaleBias;
				DecalSystem.m_DecalDatas[i].normalScaleBias = DecalSystem.m_NormalTextureScaleBias[i].m_ScaleBias;
				DecalSystem.m_DecalDatas[i].maskScaleBias = DecalSystem.m_MaskTextureScaleBias[i].m_ScaleBias;
			}
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00035600 File Offset: 0x00033800
		public void UpdateTextureAtlas(CommandBuffer cmd)
		{
			this.m_AllocationSuccess = true;
			foreach (DecalSystem.TextureScaleBias textureScaleBias in this.m_TextureList)
			{
				this.AddTexture(cmd, textureScaleBias);
			}
			if (!this.m_AllocationSuccess)
			{
				this.m_TextureList.Sort();
				this.Atlas.ResetAllocator();
				this.m_AllocationSuccess = true;
				foreach (DecalSystem.TextureScaleBias textureScaleBias2 in this.m_TextureList)
				{
					this.AddTexture(cmd, textureScaleBias2);
				}
				if (!this.m_AllocationSuccess && this.m_PrevAllocationSuccess)
				{
					Debug.LogWarning("Decal texture atlas out of space, decals on transparent geometry might not render correctly, atlas size can be changed in HDRenderPipelineAsset");
				}
			}
			this.m_PrevAllocationSuccess = this.m_AllocationSuccess;
			this.UpdateDecalDatasWithAtlasInfo();
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x000356F0 File Offset: 0x000338F0
		public void CreateDrawData()
		{
			DecalSystem.m_DecalDatasCount = 0;
			if (DecalSystem.m_DecalsVisibleThisFrame > DecalSystem.m_DecalDatas.Length)
			{
				int num = (DecalSystem.m_DecalsVisibleThisFrame + 128 - 1) / 128 * 128;
				DecalSystem.m_DecalDatas = new DecalData[num];
				DecalSystem.m_Bounds = new SFiniteLightBound[num];
				DecalSystem.m_LightVolumes = new LightVolumeData[num];
				DecalSystem.m_DiffuseTextureScaleBias = new DecalSystem.TextureScaleBias[num];
				DecalSystem.m_NormalTextureScaleBias = new DecalSystem.TextureScaleBias[num];
				DecalSystem.m_MaskTextureScaleBias = new DecalSystem.TextureScaleBias[num];
				DecalSystem.m_BaseColor = new Vector4[num];
			}
			this.m_DecalSetsRenderList.Clear();
			foreach (KeyValuePair<int, DecalSystem.DecalSet> keyValuePair in this.m_DecalSets)
			{
				if (keyValuePair.Value.IsDrawn())
				{
					int num2 = 0;
					while (num2 < this.m_DecalSetsRenderList.Count && keyValuePair.Value.DrawOrder >= this.m_DecalSetsRenderList[num2].DrawOrder)
					{
						num2++;
					}
					this.m_DecalSetsRenderList.Insert(num2, keyValuePair.Value);
				}
			}
			foreach (DecalSystem.DecalSet decalSet in this.m_DecalSetsRenderList)
			{
				decalSet.CreateDrawData();
			}
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x00035858 File Offset: 0x00033A58
		public void Cleanup()
		{
			if (this.m_Atlas != null)
			{
				this.m_Atlas.Release();
			}
			CoreUtils.Destroy(DecalSystem.m_DecalMesh);
			DecalSystem.m_DecalMesh = null;
			this.m_Atlas = null;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x00035884 File Offset: 0x00033A84
		public void RenderDebugOverlay(HDCamera hdCamera, CommandBuffer cmd, DebugDisplaySettings debugDisplaySettings, ref float x, ref float y, float overlaySize, float width)
		{
			if (debugDisplaySettings.data.decalsDebugSettings.displayAtlas)
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DisplayDebugDecalsAtlas)))
				{
					cmd.SetViewport(new Rect(x, y, overlaySize, overlaySize));
					HDUtils.BlitQuad(cmd, this.Atlas.AtlasTexture, new Vector4(1f, 1f, 0f, 0f), new Vector4(1f, 1f, 0f, 0f), (int)debugDisplaySettings.data.decalsDebugSettings.mipLevel, true);
					HDUtils.NextOverlayCoord(ref x, ref y, overlaySize, overlaySize, hdCamera);
				}
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00035954 File Offset: 0x00033B54
		public void LoadCullResults(DecalSystem.CullResult cullResult)
		{
			using (Dictionary<int, DecalSystem.CullResult.Set>.Enumerator enumerator = cullResult.requests.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					Dictionary<int, DecalSystem.DecalSet> decalSets = this.m_DecalSets;
					KeyValuePair<int, DecalSystem.CullResult.Set> keyValuePair = enumerator.Current;
					DecalSystem.DecalSet decalSet;
					if (decalSets.TryGetValue(keyValuePair.Key, out decalSet))
					{
						DecalSystem.DecalSet decalSet2 = decalSet;
						Dictionary<int, DecalSystem.CullResult.Set> requests = cullResult.requests;
						keyValuePair = enumerator.Current;
						decalSet2.SetCullResult(requests[keyValuePair.Key]);
					}
				}
			}
		}

		// Token: 0x040006D0 RID: 1744
		public static readonly string[] s_MaterialDecalPassNames = Enum.GetNames(typeof(DecalSystem.MaterialDecalPass));

		// Token: 0x040006D1 RID: 1745
		public static readonly string[] s_MaterialSGDecalPassNames = Enum.GetNames(typeof(DecalSystem.MaterialSGDecalPass));

		// Token: 0x040006D2 RID: 1746
		public const int kInvalidIndex = -1;

		// Token: 0x040006D3 RID: 1747
		public const int kNullMaterialIndex = 2147483647;

		// Token: 0x040006D4 RID: 1748
		private static DecalSystem m_Instance;

		// Token: 0x040006D5 RID: 1749
		private const int kDefaultDrawDistance = 1000;

		// Token: 0x040006D6 RID: 1750
		private const int kDecalBlockSize = 128;

		// Token: 0x040006D7 RID: 1751
		private const int kDrawIndexedBatchSize = 250;

		// Token: 0x040006D8 RID: 1752
		private static Vector4 kMin = new Vector4(-0.5f, -0.5f, -0.5f, 1f);

		// Token: 0x040006D9 RID: 1753
		private static Vector4 kMax = new Vector4(0.5f, 0.5f, 0.5f, 1f);

		// Token: 0x040006DA RID: 1754
		public static Mesh m_DecalMesh = null;

		// Token: 0x040006DB RID: 1755
		public static DecalData[] m_DecalDatas = new DecalData[128];

		// Token: 0x040006DC RID: 1756
		public static SFiniteLightBound[] m_Bounds = new SFiniteLightBound[128];

		// Token: 0x040006DD RID: 1757
		public static LightVolumeData[] m_LightVolumes = new LightVolumeData[128];

		// Token: 0x040006DE RID: 1758
		public static DecalSystem.TextureScaleBias[] m_DiffuseTextureScaleBias = new DecalSystem.TextureScaleBias[128];

		// Token: 0x040006DF RID: 1759
		public static DecalSystem.TextureScaleBias[] m_NormalTextureScaleBias = new DecalSystem.TextureScaleBias[128];

		// Token: 0x040006E0 RID: 1760
		public static DecalSystem.TextureScaleBias[] m_MaskTextureScaleBias = new DecalSystem.TextureScaleBias[128];

		// Token: 0x040006E1 RID: 1761
		public static Vector4[] m_BaseColor = new Vector4[128];

		// Token: 0x040006E2 RID: 1762
		public static int m_DecalDatasCount = 0;

		// Token: 0x040006E3 RID: 1763
		public static float[] m_BoundingDistances = new float[1];

		// Token: 0x040006E4 RID: 1764
		private Dictionary<int, DecalSystem.DecalSet> m_DecalSets = new Dictionary<int, DecalSystem.DecalSet>();

		// Token: 0x040006E5 RID: 1765
		private List<DecalSystem.DecalSet> m_DecalSetsRenderList = new List<DecalSystem.DecalSet>();

		// Token: 0x040006E6 RID: 1766
		private Camera m_Camera;

		// Token: 0x040006E7 RID: 1767
		public static int m_DecalsVisibleThisFrame = 0;

		// Token: 0x040006E8 RID: 1768
		private Texture2DAtlas m_Atlas;

		// Token: 0x040006E9 RID: 1769
		public bool m_AllocationSuccess = true;

		// Token: 0x040006EA RID: 1770
		public bool m_PrevAllocationSuccess = true;

		// Token: 0x040006EB RID: 1771
		private List<DecalSystem.TextureScaleBias> m_TextureList = new List<DecalSystem.TextureScaleBias>();

		// Token: 0x0200022D RID: 557
		public enum MaterialDecalPass
		{
			// Token: 0x04001436 RID: 5174
			DBufferMesh_3RT,
			// Token: 0x04001437 RID: 5175
			DBufferProjector_M,
			// Token: 0x04001438 RID: 5176
			DBufferProjector_AO,
			// Token: 0x04001439 RID: 5177
			DBufferProjector_MAO,
			// Token: 0x0400143A RID: 5178
			DBufferProjector_S,
			// Token: 0x0400143B RID: 5179
			DBufferProjector_MS,
			// Token: 0x0400143C RID: 5180
			DBufferProjector_AOS,
			// Token: 0x0400143D RID: 5181
			DBufferProjector_MAOS,
			// Token: 0x0400143E RID: 5182
			DBufferMesh_M,
			// Token: 0x0400143F RID: 5183
			DBufferMesh_AO,
			// Token: 0x04001440 RID: 5184
			DBufferMesh_MAO,
			// Token: 0x04001441 RID: 5185
			DBufferMesh_S,
			// Token: 0x04001442 RID: 5186
			DBufferMesh_MS,
			// Token: 0x04001443 RID: 5187
			DBufferMesh_AOS,
			// Token: 0x04001444 RID: 5188
			DBufferMesh_MAOS,
			// Token: 0x04001445 RID: 5189
			Projector_Emissive,
			// Token: 0x04001446 RID: 5190
			Mesh_Emissive
		}

		// Token: 0x0200022E RID: 558
		public enum MaterialSGDecalPass
		{
			// Token: 0x04001448 RID: 5192
			ShaderGraph_DBufferProjector3RT,
			// Token: 0x04001449 RID: 5193
			ShaderGraph_DBufferProjector4RT,
			// Token: 0x0400144A RID: 5194
			ShaderGraph_ProjectorEmissive,
			// Token: 0x0400144B RID: 5195
			ShaderGraph_DBufferMesh3RT,
			// Token: 0x0400144C RID: 5196
			ShaderGraph_DBufferMesh4RT,
			// Token: 0x0400144D RID: 5197
			ShaderGraph_MeshEmissive
		}

		// Token: 0x0200022F RID: 559
		public class CullResult : IDisposable
		{
			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x06000C19 RID: 3097 RVA: 0x00057AC8 File Offset: 0x00055CC8
			public Dictionary<int, DecalSystem.CullResult.Set> requests
			{
				get
				{
					return this.m_Requests;
				}
			}

			// Token: 0x170001A8 RID: 424
			public DecalSystem.CullResult.Set this[int index]
			{
				get
				{
					DecalSystem.CullResult.Set set;
					if (!this.m_Requests.TryGetValue(index, out set))
					{
						set = GenericPool<DecalSystem.CullResult.Set>.Get();
						this.m_Requests.Add(index, set);
					}
					return set;
				}
			}

			// Token: 0x06000C1B RID: 3099 RVA: 0x00057B04 File Offset: 0x00055D04
			public void Clear()
			{
				foreach (KeyValuePair<int, DecalSystem.CullResult.Set> keyValuePair in this.m_Requests)
				{
					keyValuePair.Value.Clear();
					GenericPool<DecalSystem.CullResult.Set>.Release(keyValuePair.Value);
				}
				this.m_Requests.Clear();
			}

			// Token: 0x06000C1C RID: 3100 RVA: 0x00057B74 File Offset: 0x00055D74
			public void Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x06000C1D RID: 3101 RVA: 0x00057B7D File Offset: 0x00055D7D
			private void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.m_Requests.Clear();
					this.m_Requests = null;
				}
			}

			// Token: 0x0400144E RID: 5198
			private Dictionary<int, DecalSystem.CullResult.Set> m_Requests = new Dictionary<int, DecalSystem.CullResult.Set>();

			// Token: 0x020002AE RID: 686
			public class Set : IDisposable
			{
				// Token: 0x170001B7 RID: 439
				// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0005A72C File Offset: 0x0005892C
				public int numResults
				{
					get
					{
						return this.m_NumResults;
					}
				}

				// Token: 0x170001B8 RID: 440
				// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0005A734 File Offset: 0x00058934
				public int[] resultIndices
				{
					get
					{
						return this.m_ResultIndices;
					}
				}

				// Token: 0x06000CE0 RID: 3296 RVA: 0x0005A73C File Offset: 0x0005893C
				public void Dispose()
				{
					this.Dispose(true);
				}

				// Token: 0x06000CE1 RID: 3297 RVA: 0x0005A745 File Offset: 0x00058945
				private void Dispose(bool disposing)
				{
					if (disposing)
					{
						this.Clear();
						this.m_ResultIndices = null;
					}
				}

				// Token: 0x06000CE2 RID: 3298 RVA: 0x0005A757 File Offset: 0x00058957
				public void Clear()
				{
					this.m_NumResults = 0;
				}

				// Token: 0x06000CE3 RID: 3299 RVA: 0x0005A760 File Offset: 0x00058960
				public int QueryIndices(int maxLength, CullingGroup cullingGroup)
				{
					if (this.m_ResultIndices == null || this.m_ResultIndices.Length < maxLength)
					{
						Array.Resize<int>(ref this.m_ResultIndices, maxLength);
					}
					this.m_NumResults = cullingGroup.QueryIndices(true, this.m_ResultIndices, 0);
					return this.m_NumResults;
				}

				// Token: 0x04001733 RID: 5939
				private int m_NumResults;

				// Token: 0x04001734 RID: 5940
				private int[] m_ResultIndices;
			}
		}

		// Token: 0x02000230 RID: 560
		public class CullRequest : IDisposable
		{
			// Token: 0x170001A9 RID: 425
			public DecalSystem.CullRequest.Set this[int index]
			{
				get
				{
					DecalSystem.CullRequest.Set set;
					if (!this.m_Requests.TryGetValue(index, out set))
					{
						set = GenericPool<DecalSystem.CullRequest.Set>.Get();
						this.m_Requests.Add(index, set);
					}
					return set;
				}
			}

			// Token: 0x06000C20 RID: 3104 RVA: 0x00057BDC File Offset: 0x00055DDC
			public void Clear()
			{
				foreach (KeyValuePair<int, DecalSystem.CullRequest.Set> keyValuePair in this.m_Requests)
				{
					keyValuePair.Value.Clear();
					GenericPool<DecalSystem.CullRequest.Set>.Release(keyValuePair.Value);
				}
				this.m_Requests.Clear();
			}

			// Token: 0x06000C21 RID: 3105 RVA: 0x00057C4C File Offset: 0x00055E4C
			public void Dispose()
			{
				this.Dispose(true);
			}

			// Token: 0x06000C22 RID: 3106 RVA: 0x00057C55 File Offset: 0x00055E55
			private void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.m_Requests.Clear();
					this.m_Requests = null;
				}
			}

			// Token: 0x0400144F RID: 5199
			private Dictionary<int, DecalSystem.CullRequest.Set> m_Requests = new Dictionary<int, DecalSystem.CullRequest.Set>();

			// Token: 0x020002AF RID: 687
			public class Set : IDisposable
			{
				// Token: 0x170001B9 RID: 441
				// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0005A79B File Offset: 0x0005899B
				public CullingGroup cullingGroup
				{
					get
					{
						return this.m_CullingGroup;
					}
				}

				// Token: 0x06000CE6 RID: 3302 RVA: 0x0005A7A3 File Offset: 0x000589A3
				public void Dispose()
				{
					this.Dispose(true);
				}

				// Token: 0x06000CE7 RID: 3303 RVA: 0x0005A7AC File Offset: 0x000589AC
				private void Dispose(bool disposing)
				{
					if (disposing)
					{
						this.Clear();
					}
				}

				// Token: 0x06000CE8 RID: 3304 RVA: 0x0005A7B7 File Offset: 0x000589B7
				public void Clear()
				{
					this.m_NumRequest = 0;
					if (this.m_CullingGroup != null)
					{
						CullingGroupManager.instance.Free(this.m_CullingGroup);
					}
					this.m_CullingGroup = null;
				}

				// Token: 0x06000CE9 RID: 3305 RVA: 0x0005A7DF File Offset: 0x000589DF
				public void Initialize(int numRequests, CullingGroup cullingGroup)
				{
					this.m_NumRequest = numRequests;
					this.m_CullingGroup = cullingGroup;
				}

				// Token: 0x04001735 RID: 5941
				private int m_NumRequest;

				// Token: 0x04001736 RID: 5942
				private CullingGroup m_CullingGroup;
			}
		}

		// Token: 0x02000231 RID: 561
		public class DecalHandle
		{
			// Token: 0x06000C24 RID: 3108 RVA: 0x00057C7F File Offset: 0x00055E7F
			public DecalHandle(int index, int materialID)
			{
				this.m_MaterialID = materialID;
				this.m_Index = index;
			}

			// Token: 0x06000C25 RID: 3109 RVA: 0x00057C95 File Offset: 0x00055E95
			public static bool IsValid(DecalSystem.DecalHandle handle)
			{
				return handle != null && handle.m_Index != -1;
			}

			// Token: 0x04001450 RID: 5200
			public int m_MaterialID;

			// Token: 0x04001451 RID: 5201
			public int m_Index;
		}

		// Token: 0x02000232 RID: 562
		public class TextureScaleBias : IComparable
		{
			// Token: 0x06000C26 RID: 3110 RVA: 0x00057CA8 File Offset: 0x00055EA8
			public int CompareTo(object obj)
			{
				DecalSystem.TextureScaleBias textureScaleBias = obj as DecalSystem.TextureScaleBias;
				int num = this.m_Texture.width * this.m_Texture.height;
				int num2 = textureScaleBias.m_Texture.width * textureScaleBias.m_Texture.height;
				if (num > num2)
				{
					return -1;
				}
				if (num < num2)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x06000C27 RID: 3111 RVA: 0x00057CF9 File Offset: 0x00055EF9
			public void Initialize(Texture texture, Vector4 scaleBias)
			{
				this.m_Texture = texture;
				this.m_ScaleBias = scaleBias;
			}

			// Token: 0x04001452 RID: 5202
			public Texture m_Texture;

			// Token: 0x04001453 RID: 5203
			public Vector4 m_ScaleBias = Vector4.zero;
		}

		// Token: 0x02000233 RID: 563
		private class DecalSet
		{
			// Token: 0x06000C29 RID: 3113 RVA: 0x00057D1C File Offset: 0x00055F1C
			public void InitializeMaterialValues()
			{
				if (this.m_Material == null)
				{
					return;
				}
				bool perChannelMask = HDRenderPipeline.currentAsset.currentPlatformRenderPipelineSettings.decalSettings.perChannelMask;
				this.m_IsHDRenderPipelineDecal = DecalSystem.IsHDRenderPipelineDecal(this.m_Material);
				if (this.m_IsHDRenderPipelineDecal)
				{
					this.m_Diffuse.Initialize(this.m_Material.GetTexture("_BaseColorMap"), Vector4.zero);
					this.m_Normal.Initialize(this.m_Material.GetTexture("_NormalMap"), Vector4.zero);
					this.m_Mask.Initialize(this.m_Material.GetTexture("_MaskMap"), Vector4.zero);
					this.m_Blend = this.m_Material.GetFloat("_DecalBlend");
					this.m_AlbedoContribution = this.m_Material.GetFloat("_AlbedoMode");
					this.m_BaseColor = this.m_Material.GetVector("_BaseColor");
					this.m_BlendParams = new Vector3(this.m_Material.GetFloat("_NormalBlendSrc"), this.m_Material.GetFloat("_MaskBlendSrc"), this.m_Material.GetFloat("_MaskBlendMode"));
					this.m_RemappingAOS = new Vector4(this.m_Material.GetFloat("_AORemapMin"), this.m_Material.GetFloat("_AORemapMax"), this.m_Material.GetFloat("_SmoothnessRemapMin"), this.m_Material.GetFloat("_SmoothnessRemapMax"));
					this.m_ScalingMAB = new Vector4(this.m_Material.GetFloat("_MetallicScale"), 0f, this.m_Material.GetFloat("_DecalMaskMapBlueScale"), 0f);
					int num = (perChannelMask ? this.MaskBlendMode : 4);
					this.m_cachedProjectorPassValue = this.m_Material.FindPass(DecalSystem.s_MaterialDecalPassNames[num]);
					this.m_cachedProjectorEmissivePassValue = this.m_Material.FindPass(DecalSystem.s_MaterialDecalPassNames[15]);
					if (this.m_Material.GetFloat("_Emissive") != 1f)
					{
						this.m_cachedProjectorEmissivePassValue = -1;
						return;
					}
				}
				else
				{
					this.m_Blend = 1f;
					this.m_cachedProjectorPassValue = this.m_Material.FindPass(DecalSystem.s_MaterialSGDecalPassNames[perChannelMask ? 1 : 0]);
					this.m_cachedProjectorEmissivePassValue = this.m_Material.FindPass(DecalSystem.s_MaterialSGDecalPassNames[2]);
				}
			}

			// Token: 0x06000C2A RID: 3114 RVA: 0x00057F64 File Offset: 0x00056164
			public DecalSet(Material material)
			{
				this.m_Material = material;
				this.InitializeMaterialValues();
			}

			// Token: 0x06000C2B RID: 3115 RVA: 0x00058068 File Offset: 0x00056268
			private BoundingSphere GetDecalProjectBoundingSphere(Matrix4x4 decalToWorld)
			{
				Vector4 vector = default(Vector4);
				Vector4 vector2 = default(Vector4);
				vector = decalToWorld * DecalSystem.kMin;
				vector2 = decalToWorld * DecalSystem.kMax;
				return new BoundingSphere
				{
					position = (vector2 + vector) / 2f,
					radius = (vector2 - vector).magnitude / 2f
				};
			}

			// Token: 0x06000C2C RID: 3116 RVA: 0x000580E4 File Offset: 0x000562E4
			public void UpdateCachedData(Matrix4x4 localToWorld, Quaternion rotation, Matrix4x4 sizeOffset, float drawDistance, float fadeScale, Vector4 uvScaleBias, bool affectsTransparency, DecalSystem.DecalHandle handle, int layerMask, float fadeFactor)
			{
				int index = handle.m_Index;
				this.m_CachedDecalToWorld[index] = localToWorld * sizeOffset;
				Matrix4x4 matrix4x = Matrix4x4.Rotate(rotation);
				float m = matrix4x.m01;
				float m2 = matrix4x.m11;
				float m3 = matrix4x.m21;
				matrix4x.m01 = matrix4x.m02;
				matrix4x.m11 = matrix4x.m12;
				matrix4x.m21 = matrix4x.m22;
				matrix4x.m02 = m;
				matrix4x.m12 = m2;
				matrix4x.m22 = m3;
				this.m_CachedNormalToWorld[index] = matrix4x;
				this.m_CachedDrawDistances[index].x = ((drawDistance < (float)DecalSystem.instance.DrawDistance) ? drawDistance : ((float)DecalSystem.instance.DrawDistance));
				this.m_CachedDrawDistances[index].y = fadeScale;
				this.m_CachedUVScaleBias[index] = uvScaleBias;
				this.m_CachedAffectsTransparency[index] = affectsTransparency;
				this.m_CachedLayerMask[index] = layerMask;
				this.m_CachedFadeFactor[index] = fadeFactor;
				this.m_BoundingSpheres[index] = this.GetDecalProjectBoundingSphere(this.m_CachedDecalToWorld[index]);
			}

			// Token: 0x06000C2D RID: 3117 RVA: 0x00058204 File Offset: 0x00056404
			public void UpdateCachedData(Transform transform, Matrix4x4 sizeOffset, float drawDistance, float fadeScale, Vector4 uvScaleBias, bool affectsTransparency, DecalSystem.DecalHandle handle, int layerMask, float fadeFactor)
			{
				if (this.m_Material == null)
				{
					return;
				}
				this.UpdateCachedData(transform.localToWorldMatrix, transform.rotation, sizeOffset, drawDistance, fadeScale, uvScaleBias, affectsTransparency, handle, layerMask, fadeFactor);
			}

			// Token: 0x06000C2E RID: 3118 RVA: 0x00058240 File Offset: 0x00056440
			public DecalSystem.DecalHandle AddDecal(Matrix4x4 localToWorld, Quaternion rotation, Matrix4x4 sizeOffset, float drawDistance, float fadeScale, Vector4 uvScaleBias, bool affectsTransparency, int materialID, int layerMask, float fadeFactor)
			{
				if (this.m_DecalsCount == this.m_Handles.Length)
				{
					DecalSystem.DecalHandle[] array = new DecalSystem.DecalHandle[this.m_DecalsCount + 128];
					BoundingSphere[] array2 = new BoundingSphere[this.m_DecalsCount + 128];
					Matrix4x4[] array3 = new Matrix4x4[this.m_DecalsCount + 128];
					Matrix4x4[] array4 = new Matrix4x4[this.m_DecalsCount + 128];
					Vector2[] array5 = new Vector2[this.m_DecalsCount + 128];
					Vector4[] array6 = new Vector4[this.m_DecalsCount + 128];
					bool[] array7 = new bool[this.m_DecalsCount + 128];
					int[] array8 = new int[this.m_DecalsCount + 128];
					float[] array9 = new float[this.m_DecalsCount + 128];
					this.m_ResultIndices = new int[this.m_DecalsCount + 128];
					this.m_Handles.CopyTo(array, 0);
					this.m_BoundingSpheres.CopyTo(array2, 0);
					this.m_CachedDecalToWorld.CopyTo(array3, 0);
					this.m_CachedNormalToWorld.CopyTo(array4, 0);
					this.m_CachedDrawDistances.CopyTo(array5, 0);
					this.m_CachedUVScaleBias.CopyTo(array6, 0);
					this.m_CachedAffectsTransparency.CopyTo(array7, 0);
					this.m_CachedLayerMask.CopyTo(array8, 0);
					this.m_CachedFadeFactor.CopyTo(array9, 0);
					this.m_Handles = array;
					this.m_BoundingSpheres = array2;
					this.m_CachedDecalToWorld = array3;
					this.m_CachedNormalToWorld = array4;
					this.m_CachedDrawDistances = array5;
					this.m_CachedUVScaleBias = array6;
					this.m_CachedAffectsTransparency = array7;
					this.m_CachedLayerMask = array8;
					this.m_CachedFadeFactor = array9;
				}
				DecalSystem.DecalHandle decalHandle = new DecalSystem.DecalHandle(this.m_DecalsCount, materialID);
				this.m_Handles[this.m_DecalsCount] = decalHandle;
				this.UpdateCachedData(localToWorld, rotation, sizeOffset, drawDistance, fadeScale, uvScaleBias, affectsTransparency, decalHandle, layerMask, fadeFactor);
				this.m_DecalsCount++;
				return decalHandle;
			}

			// Token: 0x06000C2F RID: 3119 RVA: 0x00058420 File Offset: 0x00056620
			public void RemoveDecal(DecalSystem.DecalHandle handle)
			{
				int index = handle.m_Index;
				this.m_Handles[index] = this.m_Handles[this.m_DecalsCount - 1];
				this.m_Handles[index].m_Index = index;
				this.m_Handles[this.m_DecalsCount - 1] = null;
				this.m_BoundingSpheres[index] = this.m_BoundingSpheres[this.m_DecalsCount - 1];
				this.m_CachedDecalToWorld[index] = this.m_CachedDecalToWorld[this.m_DecalsCount - 1];
				this.m_CachedNormalToWorld[index] = this.m_CachedNormalToWorld[this.m_DecalsCount - 1];
				this.m_CachedDrawDistances[index] = this.m_CachedDrawDistances[this.m_DecalsCount - 1];
				this.m_CachedUVScaleBias[index] = this.m_CachedUVScaleBias[this.m_DecalsCount - 1];
				this.m_CachedAffectsTransparency[index] = this.m_CachedAffectsTransparency[this.m_DecalsCount - 1];
				this.m_CachedLayerMask[index] = this.m_CachedLayerMask[this.m_DecalsCount - 1];
				this.m_CachedFadeFactor[index] = this.m_CachedFadeFactor[this.m_DecalsCount - 1];
				this.m_DecalsCount--;
				handle.m_Index = -1;
			}

			// Token: 0x06000C30 RID: 3120 RVA: 0x00058560 File Offset: 0x00056760
			public void BeginCull(DecalSystem.CullRequest.Set cullRequest)
			{
				cullRequest.Clear();
				if (this.m_Material == null)
				{
					return;
				}
				if (cullRequest.cullingGroup != null)
				{
					Debug.LogError("Begin/EndCull() called out of sequence for decal projectors.");
				}
				DecalSystem.m_BoundingDistances[0] = (float)DecalSystem.instance.DrawDistance;
				this.m_NumResults = 0;
				CullingGroup cullingGroup = CullingGroupManager.instance.Alloc();
				cullingGroup.targetCamera = DecalSystem.instance.CurrentCamera;
				cullingGroup.SetDistanceReferencePoint(cullingGroup.targetCamera.transform.position);
				cullingGroup.SetBoundingDistances(DecalSystem.m_BoundingDistances);
				cullingGroup.SetBoundingSpheres(this.m_BoundingSpheres);
				cullingGroup.SetBoundingSphereCount(this.m_DecalsCount);
				cullRequest.Initialize(0, cullingGroup);
			}

			// Token: 0x06000C31 RID: 3121 RVA: 0x00058609 File Offset: 0x00056809
			public int QueryCullResults(DecalSystem.CullRequest.Set cullRequest, DecalSystem.CullResult.Set cullResult)
			{
				if (this.m_Material == null || cullRequest.cullingGroup == null)
				{
					return 0;
				}
				return cullResult.QueryIndices(this.m_Handles.Length, cullRequest.cullingGroup);
			}

			// Token: 0x06000C32 RID: 3122 RVA: 0x00058638 File Offset: 0x00056838
			private void GetDecalVolumeDataAndBound(Matrix4x4 decalToWorld, Matrix4x4 worldToView)
			{
				Vector4 vector = decalToWorld.GetColumn(0) * 0.5f;
				Vector4 vector2 = decalToWorld.GetColumn(1) * 0.5f;
				Vector4 vector3 = decalToWorld.GetColumn(2) * 0.5f;
				Vector4 column = decalToWorld.GetColumn(3);
				Vector3 vector4 = default(Vector3);
				vector4.x = vector.magnitude;
				vector4.y = vector2.magnitude;
				vector4.z = vector3.magnitude;
				Vector3 vector5 = worldToView.MultiplyVector(vector / vector4.x);
				Vector3 vector6 = worldToView.MultiplyVector(vector2 / vector4.y);
				Vector3 vector7 = worldToView.MultiplyVector(vector3 / vector4.z);
				Vector3 vector8 = worldToView.MultiplyPoint(column);
				DecalSystem.m_Bounds[DecalSystem.m_DecalDatasCount].center = vector8;
				DecalSystem.m_Bounds[DecalSystem.m_DecalDatasCount].boxAxisX = vector5 * vector4.x;
				DecalSystem.m_Bounds[DecalSystem.m_DecalDatasCount].boxAxisY = vector6 * vector4.y;
				DecalSystem.m_Bounds[DecalSystem.m_DecalDatasCount].boxAxisZ = vector7 * vector4.z;
				DecalSystem.m_Bounds[DecalSystem.m_DecalDatasCount].scaleXY.Set(1f, 1f);
				DecalSystem.m_Bounds[DecalSystem.m_DecalDatasCount].radius = vector4.magnitude;
				DecalSystem.m_LightVolumes[DecalSystem.m_DecalDatasCount].lightCategory = 3U;
				DecalSystem.m_LightVolumes[DecalSystem.m_DecalDatasCount].lightVolume = 2U;
				DecalSystem.m_LightVolumes[DecalSystem.m_DecalDatasCount].featureFlags = 32768U;
				DecalSystem.m_LightVolumes[DecalSystem.m_DecalDatasCount].lightPos = vector8;
				DecalSystem.m_LightVolumes[DecalSystem.m_DecalDatasCount].lightAxisX = vector5;
				DecalSystem.m_LightVolumes[DecalSystem.m_DecalDatasCount].lightAxisY = vector6;
				DecalSystem.m_LightVolumes[DecalSystem.m_DecalDatasCount].lightAxisZ = vector7;
				DecalSystem.m_LightVolumes[DecalSystem.m_DecalDatasCount].boxInnerDist = vector4 - HDRenderPipeline.k_BoxCullingExtentThreshold;
				DecalSystem.m_LightVolumes[DecalSystem.m_DecalDatasCount].boxInvRange.Set(1f / HDRenderPipeline.k_BoxCullingExtentThreshold.x, 1f / HDRenderPipeline.k_BoxCullingExtentThreshold.y, 1f / HDRenderPipeline.k_BoxCullingExtentThreshold.z);
			}

			// Token: 0x06000C33 RID: 3123 RVA: 0x000588D8 File Offset: 0x00056AD8
			private void AssignCurrentBatches(ref Matrix4x4[] decalToWorldBatch, ref Matrix4x4[] normalToWorldBatch, int batchCount)
			{
				if (this.m_DecalToWorld.Count == batchCount)
				{
					decalToWorldBatch = new Matrix4x4[250];
					this.m_DecalToWorld.Add(decalToWorldBatch);
					normalToWorldBatch = new Matrix4x4[250];
					this.m_NormalToWorld.Add(normalToWorldBatch);
					return;
				}
				decalToWorldBatch = this.m_DecalToWorld[batchCount];
				normalToWorldBatch = this.m_NormalToWorld[batchCount];
			}

			// Token: 0x06000C34 RID: 3124 RVA: 0x00058942 File Offset: 0x00056B42
			public bool IsDrawn()
			{
				return this.m_Material != null && this.m_NumResults > 0;
			}

			// Token: 0x06000C35 RID: 3125 RVA: 0x00058960 File Offset: 0x00056B60
			public void CreateDrawData()
			{
				int num = 0;
				int num2 = 0;
				this.m_InstanceCount = 0;
				Matrix4x4[] array = null;
				Matrix4x4[] array2 = null;
				bool flag = false;
				this.AssignCurrentBatches(ref array, ref array2, num2);
				Vector3 position = DecalSystem.instance.CurrentCamera.transform.position;
				Matrix4x4 matrix4x = HDRenderPipeline.WorldToCamera(DecalSystem.instance.CurrentCamera);
				bool perChannelMask = DecalSystem.instance.perChannelMask;
				for (int i = 0; i < this.m_NumResults; i++)
				{
					int num3 = this.m_ResultIndices[i];
					bool cullingMask = DecalSystem.instance.CurrentCamera.cullingMask != 0;
					int num4 = 1 << this.m_CachedLayerMask[num3];
					if (((cullingMask ? 1 : 0) & num4) != 0)
					{
						float magnitude = (position - this.m_BoundingSpheres[num3].position).magnitude;
						float num5 = this.m_CachedDrawDistances[num3].x + this.m_BoundingSpheres[num3].radius;
						if (magnitude < num5)
						{
							array[num] = this.m_CachedDecalToWorld[num3];
							array2[num] = this.m_CachedNormalToWorld[num3];
							float num6 = this.m_CachedFadeFactor[num3] * Mathf.Clamp((num5 - magnitude) / (num5 * (1f - this.m_CachedDrawDistances[num3].y)), 0f, 1f);
							array2[num].m03 = num6 * this.m_Blend;
							array2[num].m13 = this.m_AlbedoContribution;
							array2[num].SetRow(3, this.m_CachedUVScaleBias[num3]);
							if (this.m_CachedAffectsTransparency[num3])
							{
								DecalSystem.m_DecalDatas[DecalSystem.m_DecalDatasCount].worldToDecal = array[num].inverse;
								DecalSystem.m_DecalDatas[DecalSystem.m_DecalDatasCount].normalToWorld = array2[num];
								DecalSystem.m_DecalDatas[DecalSystem.m_DecalDatasCount].baseColor = this.m_BaseColor;
								DecalSystem.m_DecalDatas[DecalSystem.m_DecalDatasCount].blendParams = this.m_BlendParams;
								DecalSystem.m_DecalDatas[DecalSystem.m_DecalDatasCount].remappingAOS = this.m_RemappingAOS;
								DecalSystem.m_DecalDatas[DecalSystem.m_DecalDatasCount].scalingMAB = this.m_ScalingMAB;
								if (!perChannelMask)
								{
									DecalSystem.m_DecalDatas[DecalSystem.m_DecalDatasCount].blendParams.z = 4f;
								}
								DecalSystem.m_DiffuseTextureScaleBias[DecalSystem.m_DecalDatasCount] = this.m_Diffuse;
								DecalSystem.m_NormalTextureScaleBias[DecalSystem.m_DecalDatasCount] = this.m_Normal;
								DecalSystem.m_MaskTextureScaleBias[DecalSystem.m_DecalDatasCount] = this.m_Mask;
								this.GetDecalVolumeDataAndBound(array[num], matrix4x);
								DecalSystem.m_DecalDatasCount++;
								flag = true;
							}
							num++;
							this.m_InstanceCount++;
							if (num == 250)
							{
								num = 0;
								num2++;
								this.AssignCurrentBatches(ref array, ref array2, num2);
							}
						}
					}
				}
				if (flag)
				{
					this.AddToTextureList(ref DecalSystem.instance.m_TextureList);
				}
			}

			// Token: 0x06000C36 RID: 3126 RVA: 0x00058C66 File Offset: 0x00056E66
			public void EndCull(DecalSystem.CullRequest.Set request)
			{
				if (this.m_Material == null)
				{
					return;
				}
				if (request.cullingGroup == null)
				{
					Debug.LogError("Begin/EndCull() called out of sequence for decal projectors.");
					return;
				}
				request.Clear();
			}

			// Token: 0x06000C37 RID: 3127 RVA: 0x00058C90 File Offset: 0x00056E90
			public void AddToTextureList(ref List<DecalSystem.TextureScaleBias> textureList)
			{
				if (this.m_Diffuse.m_Texture != null)
				{
					textureList.Add(this.m_Diffuse);
				}
				if (this.m_Normal.m_Texture != null)
				{
					textureList.Add(this.m_Normal);
				}
				if (this.m_Mask.m_Texture != null)
				{
					textureList.Add(this.m_Mask);
				}
			}

			// Token: 0x06000C38 RID: 3128 RVA: 0x00058D00 File Offset: 0x00056F00
			public void RenderIntoDBuffer(CommandBuffer cmd)
			{
				if (this.m_Material == null || this.m_cachedProjectorPassValue == -1 || this.m_NumResults == 0)
				{
					return;
				}
				int i = 0;
				int num = this.m_InstanceCount;
				while (i < this.m_InstanceCount / 250)
				{
					this.m_PropertyBlock.SetMatrixArray(HDShaderIDs._NormalToWorldID, this.m_NormalToWorld[i]);
					cmd.DrawMeshInstanced(DecalSystem.m_DecalMesh, 0, this.m_Material, this.m_cachedProjectorPassValue, this.m_DecalToWorld[i], 250, this.m_PropertyBlock);
					num -= 250;
					i++;
				}
				if (num > 0)
				{
					this.m_PropertyBlock.SetMatrixArray(HDShaderIDs._NormalToWorldID, this.m_NormalToWorld[i]);
					cmd.DrawMeshInstanced(DecalSystem.m_DecalMesh, 0, this.m_Material, this.m_cachedProjectorPassValue, this.m_DecalToWorld[i], num, this.m_PropertyBlock);
				}
			}

			// Token: 0x06000C39 RID: 3129 RVA: 0x00058DEC File Offset: 0x00056FEC
			public void RenderForwardEmissive(CommandBuffer cmd)
			{
				if (this.m_Material == null || this.m_cachedProjectorEmissivePassValue == -1 || this.m_NumResults == 0)
				{
					return;
				}
				int i = 0;
				int num = this.m_InstanceCount;
				while (i < this.m_InstanceCount / 250)
				{
					this.m_PropertyBlock.SetMatrixArray(HDShaderIDs._NormalToWorldID, this.m_NormalToWorld[i]);
					cmd.DrawMeshInstanced(DecalSystem.m_DecalMesh, 0, this.m_Material, this.m_cachedProjectorEmissivePassValue, this.m_DecalToWorld[i], 250, this.m_PropertyBlock);
					num -= 250;
					i++;
				}
				if (num > 0)
				{
					this.m_PropertyBlock.SetMatrixArray(HDShaderIDs._NormalToWorldID, this.m_NormalToWorld[i]);
					cmd.DrawMeshInstanced(DecalSystem.m_DecalMesh, 0, this.m_Material, this.m_cachedProjectorEmissivePassValue, this.m_DecalToWorld[i], num, this.m_PropertyBlock);
				}
			}

			// Token: 0x170001AA RID: 426
			// (get) Token: 0x06000C3A RID: 3130 RVA: 0x00058ED5 File Offset: 0x000570D5
			public Material KeyMaterial
			{
				get
				{
					return this.m_Material;
				}
			}

			// Token: 0x170001AB RID: 427
			// (get) Token: 0x06000C3B RID: 3131 RVA: 0x00058EDD File Offset: 0x000570DD
			public int Count
			{
				get
				{
					return this.m_DecalsCount;
				}
			}

			// Token: 0x170001AC RID: 428
			// (get) Token: 0x06000C3C RID: 3132 RVA: 0x00058EE5 File Offset: 0x000570E5
			public int DrawOrder
			{
				get
				{
					return this.m_Material.GetInt("_DrawOrder");
				}
			}

			// Token: 0x170001AD RID: 429
			// (get) Token: 0x06000C3D RID: 3133 RVA: 0x00058EF7 File Offset: 0x000570F7
			public int MaskBlendMode
			{
				get
				{
					if (this.m_IsHDRenderPipelineDecal)
					{
						return (int)this.m_Material.GetFloat("_MaskBlendMode");
					}
					return 0;
				}
			}

			// Token: 0x06000C3E RID: 3134 RVA: 0x00058F14 File Offset: 0x00057114
			internal void SetCullResult(DecalSystem.CullResult.Set value)
			{
				this.m_NumResults = value.numResults;
				if (this.m_ResultIndices.Length < this.m_NumResults)
				{
					Array.Resize<int>(ref this.m_ResultIndices, this.m_NumResults);
				}
				Array.Copy(value.resultIndices, this.m_ResultIndices, this.m_NumResults);
			}

			// Token: 0x04001454 RID: 5204
			private List<Matrix4x4[]> m_DecalToWorld = new List<Matrix4x4[]>();

			// Token: 0x04001455 RID: 5205
			private List<Matrix4x4[]> m_NormalToWorld = new List<Matrix4x4[]>();

			// Token: 0x04001456 RID: 5206
			private BoundingSphere[] m_BoundingSpheres = new BoundingSphere[128];

			// Token: 0x04001457 RID: 5207
			private DecalSystem.DecalHandle[] m_Handles = new DecalSystem.DecalHandle[128];

			// Token: 0x04001458 RID: 5208
			private int[] m_ResultIndices = new int[128];

			// Token: 0x04001459 RID: 5209
			private int m_NumResults;

			// Token: 0x0400145A RID: 5210
			private int m_InstanceCount;

			// Token: 0x0400145B RID: 5211
			private int m_DecalsCount;

			// Token: 0x0400145C RID: 5212
			private Matrix4x4[] m_CachedDecalToWorld = new Matrix4x4[128];

			// Token: 0x0400145D RID: 5213
			private Matrix4x4[] m_CachedNormalToWorld = new Matrix4x4[128];

			// Token: 0x0400145E RID: 5214
			private Vector2[] m_CachedDrawDistances = new Vector2[128];

			// Token: 0x0400145F RID: 5215
			private Vector4[] m_CachedUVScaleBias = new Vector4[128];

			// Token: 0x04001460 RID: 5216
			private bool[] m_CachedAffectsTransparency = new bool[128];

			// Token: 0x04001461 RID: 5217
			private int[] m_CachedLayerMask = new int[128];

			// Token: 0x04001462 RID: 5218
			private float[] m_CachedFadeFactor = new float[128];

			// Token: 0x04001463 RID: 5219
			private Material m_Material;

			// Token: 0x04001464 RID: 5220
			private MaterialPropertyBlock m_PropertyBlock = new MaterialPropertyBlock();

			// Token: 0x04001465 RID: 5221
			private float m_Blend;

			// Token: 0x04001466 RID: 5222
			private float m_AlbedoContribution;

			// Token: 0x04001467 RID: 5223
			private Vector4 m_BaseColor;

			// Token: 0x04001468 RID: 5224
			private Vector4 m_RemappingAOS;

			// Token: 0x04001469 RID: 5225
			private Vector4 m_ScalingMAB;

			// Token: 0x0400146A RID: 5226
			private Vector3 m_BlendParams;

			// Token: 0x0400146B RID: 5227
			private bool m_IsHDRenderPipelineDecal;

			// Token: 0x0400146C RID: 5228
			private int m_cachedProjectorPassValue;

			// Token: 0x0400146D RID: 5229
			private int m_cachedProjectorEmissivePassValue;

			// Token: 0x0400146E RID: 5230
			private DecalSystem.TextureScaleBias m_Diffuse = new DecalSystem.TextureScaleBias();

			// Token: 0x0400146F RID: 5231
			private DecalSystem.TextureScaleBias m_Normal = new DecalSystem.TextureScaleBias();

			// Token: 0x04001470 RID: 5232
			private DecalSystem.TextureScaleBias m_Mask = new DecalSystem.TextureScaleBias();
		}
	}
}
