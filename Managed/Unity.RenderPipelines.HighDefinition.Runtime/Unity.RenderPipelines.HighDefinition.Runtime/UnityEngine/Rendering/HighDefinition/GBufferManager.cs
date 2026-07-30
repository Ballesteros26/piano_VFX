using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000BA RID: 186
	internal class GBufferManager : MRTBufferManager
	{
		// Token: 0x060006E0 RID: 1760 RVA: 0x00036678 File Offset: 0x00034878
		public GBufferManager(HDRenderPipelineAsset asset, RenderPipelineMaterial deferredMaterial)
			: base(deferredMaterial.GetMaterialGBufferCount(asset))
		{
			for (int i = 0; i < this.m_BufferCount; i++)
			{
				this.m_RTIDsArray[i] = new RenderTargetIdentifier[i + 1];
			}
			this.m_DeferredMaterial = deferredMaterial;
			this.m_asset = asset;
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x000366DC File Offset: 0x000348DC
		public override void CreateBuffers()
		{
			GraphicsFormat[] array;
			bool[] array2;
			this.m_DeferredMaterial.GetMaterialGBufferDescription(this.m_asset, out array, out this.m_GBufferUsage, out array2);
			for (int i = 0; i < this.m_BufferCount; i++)
			{
				RTHandle[] rts = this.m_RTs;
				int num = i;
				Vector2 one = Vector2.one;
				int slices = TextureXR.slices;
				DepthBits depthBits = DepthBits.None;
				GraphicsFormat graphicsFormat = array[i];
				FilterMode filterMode = FilterMode.Point;
				TextureWrapMode textureWrapMode = TextureWrapMode.Repeat;
				TextureDimension dimension = TextureXR.dimension;
				string text = string.Format("GBuffer{0}", i);
				rts[num] = RTHandles.Alloc(one, slices, depthBits, graphicsFormat, filterMode, textureWrapMode, dimension, array2[i], false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, text);
				this.m_RTIDs[i] = this.m_RTs[i].nameID;
				this.m_TextureShaderIDs[i] = HDShaderIDs._GBufferTexture[i];
				if (this.m_GBufferUsage[i] == GBufferUsage.ShadowMask)
				{
					this.m_ShadowMaskIndex = i;
				}
				else if (this.m_GBufferUsage[i] == GBufferUsage.LightLayers)
				{
					this.m_LightLayers = i;
				}
			}
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000367B0 File Offset: 0x000349B0
		public override void BindBufferAsTextures(CommandBuffer cmd)
		{
			for (int i = 0; i < this.m_BufferCount; i++)
			{
				cmd.SetGlobalTexture(this.m_TextureShaderIDs[i], this.m_RTs[i]);
			}
			if (this.m_ShadowMaskIndex >= 0)
			{
				cmd.SetGlobalTexture(HDShaderIDs._ShadowMaskTexture, this.m_RTs[this.m_ShadowMaskIndex]);
			}
			if (this.m_LightLayers >= 0)
			{
				cmd.SetGlobalTexture(HDShaderIDs._LightLayersTexture, this.m_RTs[this.m_LightLayers]);
				return;
			}
			cmd.SetGlobalTexture(HDShaderIDs._LightLayersTexture, TextureXR.GetWhiteTexture());
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0003684C File Offset: 0x00034A4C
		public RenderTargetIdentifier[] GetBuffersRTI(FrameSettings frameSettings)
		{
			int num = 0;
			for (int i = 0; i < this.m_BufferCount; i++)
			{
				if ((this.m_GBufferUsage[i] != GBufferUsage.ShadowMask || frameSettings.IsEnabled(FrameSettingsField.Shadowmask)) && (this.m_GBufferUsage[i] != GBufferUsage.LightLayers || frameSettings.IsEnabled(FrameSettingsField.LightLayers)))
				{
					num++;
				}
			}
			RenderTargetIdentifier[] array = this.m_RTIDsArray[num - 1];
			num = 0;
			for (int j = 0; j < this.m_BufferCount; j++)
			{
				if ((this.m_GBufferUsage[j] != GBufferUsage.ShadowMask || frameSettings.IsEnabled(FrameSettingsField.Shadowmask)) && (this.m_GBufferUsage[j] != GBufferUsage.LightLayers || frameSettings.IsEnabled(FrameSettingsField.LightLayers)))
				{
					array[num] = this.m_RTs[j].nameID;
					num++;
				}
			}
			return array;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00036900 File Offset: 0x00034B00
		public RTHandle GetNormalBuffer(int index)
		{
			int num = 0;
			for (int i = 0; i < this.m_BufferCount; i++)
			{
				if (this.m_GBufferUsage[i] == GBufferUsage.Normal)
				{
					if (num == index)
					{
						return this.m_RTs[i];
					}
					num++;
				}
			}
			return null;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00036940 File Offset: 0x00034B40
		public RTHandle GetSubsurfaceScatteringBuffer(int index)
		{
			int num = 0;
			for (int i = 0; i < this.m_BufferCount; i++)
			{
				if (this.m_GBufferUsage[i] == GBufferUsage.SubsurfaceScattering)
				{
					if (num == index)
					{
						return this.m_RTs[i];
					}
					num++;
				}
			}
			return null;
		}

		// Token: 0x04000717 RID: 1815
		private RenderPipelineMaterial m_DeferredMaterial;

		// Token: 0x04000718 RID: 1816
		protected GBufferUsage[] m_GBufferUsage;

		// Token: 0x04000719 RID: 1817
		protected int m_ShadowMaskIndex = -1;

		// Token: 0x0400071A RID: 1818
		protected int m_LightLayers = -1;

		// Token: 0x0400071B RID: 1819
		protected HDRenderPipelineAsset m_asset;

		// Token: 0x0400071C RID: 1820
		protected RenderTargetIdentifier[][] m_RTIDsArray = new RenderTargetIdentifier[8][];
	}
}
