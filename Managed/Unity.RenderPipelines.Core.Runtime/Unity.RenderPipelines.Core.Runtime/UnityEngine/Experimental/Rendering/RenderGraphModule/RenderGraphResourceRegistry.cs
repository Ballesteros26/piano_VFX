using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000017 RID: 23
	public class RenderGraphResourceRegistry
	{
		// Token: 0x06000073 RID: 115 RVA: 0x000036D8 File Offset: 0x000018D8
		public RTHandle GetTexture(in RenderGraphResource handle)
		{
			DynamicArray<RenderGraphResourceRegistry.TextureResource> textureResources = this.m_TextureResources;
			RenderGraphResource renderGraphResource = handle;
			return textureResources[renderGraphResource.handle].rt;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003704 File Offset: 0x00001904
		public RendererList GetRendererList(in RenderGraphResource handle)
		{
			DynamicArray<RenderGraphResourceRegistry.RendererListResource> rendererListResources = this.m_RendererListResources;
			RenderGraphResource renderGraphResource = handle;
			return rendererListResources[renderGraphResource.handle].rendererList;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000372F File Offset: 0x0000192F
		private RenderGraphResourceRegistry()
		{
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003770 File Offset: 0x00001970
		internal RenderGraphResourceRegistry(bool supportMSAA, MSAASamples initialSampleCount, RenderGraphDebugParams renderGraphDebug, RenderGraphLogger logger)
		{
			this.m_RTHandleSystem.Initialize(1, 1, supportMSAA, initialSampleCount);
			this.m_RenderGraphDebug = renderGraphDebug;
			this.m_Logger = logger;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000037D8 File Offset: 0x000019D8
		internal void SetRTHandleReferenceSize(int width, int height, MSAASamples msaaSamples)
		{
			this.m_RTHandleSystem.SetReferenceSize(width, height, msaaSamples);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000037E8 File Offset: 0x000019E8
		internal RTHandleProperties GetRTHandleProperties()
		{
			return this.m_RTHandleSystem.rtHandleProperties;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000037F8 File Offset: 0x000019F8
		internal RenderGraphMutableResource ImportTexture(RTHandle rt, int shaderProperty = 0)
		{
			DynamicArray<RenderGraphResourceRegistry.TextureResource> textureResources = this.m_TextureResources;
			RenderGraphResourceRegistry.TextureResource textureResource = new RenderGraphResourceRegistry.TextureResource(rt, shaderProperty);
			return new RenderGraphMutableResource(textureResources.Add(in textureResource), RenderGraphResourceType.Texture);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003820 File Offset: 0x00001A20
		internal RenderGraphMutableResource CreateTexture(in TextureDesc desc, int shaderProperty = 0)
		{
			this.ValidateTextureDesc(in desc);
			DynamicArray<RenderGraphResourceRegistry.TextureResource> textureResources = this.m_TextureResources;
			RenderGraphResourceRegistry.TextureResource textureResource = new RenderGraphResourceRegistry.TextureResource(in desc, shaderProperty);
			return new RenderGraphMutableResource(textureResources.Add(in textureResource), RenderGraphResourceType.Texture);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003850 File Offset: 0x00001A50
		internal void UpdateTextureFirstWrite(RenderGraphResource tex, int passIndex)
		{
			ref RenderGraphResourceRegistry.TextureResource textureResource = ref this.GetTextureResource(tex);
			textureResource.firstWritePassIndex = Math.Min(passIndex, textureResource.firstWritePassIndex);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003878 File Offset: 0x00001A78
		internal void UpdateTextureLastRead(RenderGraphResource tex, int passIndex)
		{
			ref RenderGraphResourceRegistry.TextureResource textureResource = ref this.GetTextureResource(tex);
			textureResource.lastReadPassIndex = Math.Max(passIndex, textureResource.lastReadPassIndex);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000389F File Offset: 0x00001A9F
		private ref RenderGraphResourceRegistry.TextureResource GetTextureResource(RenderGraphResource res)
		{
			return this.m_TextureResources[res.handle];
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000038B3 File Offset: 0x00001AB3
		internal TextureDesc GetTextureResourceDesc(RenderGraphResource res)
		{
			return this.m_TextureResources[res.handle].desc;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000038CC File Offset: 0x00001ACC
		internal RenderGraphResource CreateRendererList(in RendererListDesc desc)
		{
			this.ValidateRendererListDesc(in desc);
			DynamicArray<RenderGraphResourceRegistry.RendererListResource> rendererListResources = this.m_RendererListResources;
			RenderGraphResourceRegistry.RendererListResource rendererListResource = new RenderGraphResourceRegistry.RendererListResource(in desc);
			return new RenderGraphResource(rendererListResources.Add(in rendererListResource), RenderGraphResourceType.RendererList);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000038FC File Offset: 0x00001AFC
		internal void CreateAndClearTexturesForPass(RenderGraphContext rgContext, int passIndex, List<RenderGraphMutableResource> textures)
		{
			foreach (RenderGraphMutableResource renderGraphMutableResource in textures)
			{
				ref RenderGraphResourceRegistry.TextureResource textureResource = ref this.GetTextureResource(renderGraphMutableResource);
				if (!textureResource.imported && textureResource.firstWritePassIndex == passIndex)
				{
					this.CreateTextureForPass(ref textureResource);
					if (textureResource.desc.clearBuffer || this.m_RenderGraphDebug.clearRenderTargetsAtCreation)
					{
						bool flag = this.m_RenderGraphDebug.clearRenderTargetsAtCreation && !textureResource.desc.clearBuffer;
						using (new ProfilingScope(rgContext.cmd, ProfilingSampler.Get<RenderGraphProfileId>(RenderGraphProfileId.RenderGraphClear)))
						{
							ClearFlag clearFlag = ((textureResource.desc.depthBufferBits != DepthBits.None) ? ClearFlag.Depth : ClearFlag.Color);
							Color color = (flag ? Color.magenta : textureResource.desc.clearColor);
							CoreUtils.SetRenderTarget(rgContext.cmd, textureResource.rt, clearFlag, color, 0, CubemapFace.Unknown, -1);
						}
					}
					this.LogTextureCreation(textureResource.rt, textureResource.desc.clearBuffer || this.m_RenderGraphDebug.clearRenderTargetsAtCreation);
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003A64 File Offset: 0x00001C64
		private void CreateTextureForPass(ref RenderGraphResourceRegistry.TextureResource resource)
		{
			TextureDesc desc = resource.desc;
			int hashCode = desc.GetHashCode();
			if (resource.rt != null)
			{
				throw new InvalidOperationException(string.Format("Trying to create an already created texture ({0}). Texture was probably declared for writing more than once.", resource.desc.name));
			}
			resource.rt = null;
			if (!this.TryGetRenderTarget(hashCode, out resource.rt))
			{
				switch (desc.sizeMode)
				{
				case TextureSizeMode.Explicit:
					resource.rt = this.m_RTHandleSystem.Alloc(desc.width, desc.height, desc.slices, desc.depthBufferBits, desc.colorFormat, desc.filterMode, desc.wrapMode, desc.dimension, desc.enableRandomWrite, desc.useMipMap, desc.autoGenerateMips, desc.isShadowMap, desc.anisoLevel, desc.mipMapBias, desc.msaaSamples, desc.bindTextureMS, desc.useDynamicScale, desc.memoryless, desc.name);
					break;
				case TextureSizeMode.Scale:
					resource.rt = this.m_RTHandleSystem.Alloc(desc.scale, desc.slices, desc.depthBufferBits, desc.colorFormat, desc.filterMode, desc.wrapMode, desc.dimension, desc.enableRandomWrite, desc.useMipMap, desc.autoGenerateMips, desc.isShadowMap, desc.anisoLevel, desc.mipMapBias, desc.enableMSAA, desc.bindTextureMS, desc.useDynamicScale, desc.memoryless, desc.name);
					break;
				case TextureSizeMode.Functor:
					resource.rt = this.m_RTHandleSystem.Alloc(desc.func, desc.slices, desc.depthBufferBits, desc.colorFormat, desc.filterMode, desc.wrapMode, desc.dimension, desc.enableRandomWrite, desc.useMipMap, desc.autoGenerateMips, desc.isShadowMap, desc.anisoLevel, desc.mipMapBias, desc.enableMSAA, desc.bindTextureMS, desc.useDynamicScale, desc.memoryless, desc.name);
					break;
				}
			}
			resource.cachedHash = hashCode;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003C6C File Offset: 0x00001E6C
		private unsafe void SetGlobalTextures(RenderGraphContext rgContext, List<RenderGraphResource> textures, bool bindDummyTexture)
		{
			foreach (RenderGraphResource renderGraphResource in textures)
			{
				RenderGraphResourceRegistry.TextureResource textureResource = *this.GetTextureResource(renderGraphResource);
				if (textureResource.shaderProperty != 0)
				{
					if (textureResource.rt == null)
					{
						throw new InvalidOperationException(string.Format("Trying to set Global Texture parameter for \"{0}\" which was never created.\nCheck that at least one write operation happens before reading it.", textureResource.desc.name));
					}
					rgContext.cmd.SetGlobalTexture(textureResource.shaderProperty, bindDummyTexture ? TextureXR.GetMagentaTexture() : textureResource.rt);
				}
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003D14 File Offset: 0x00001F14
		internal void PreRenderPassSetGlobalTextures(RenderGraphContext rgContext, List<RenderGraphResource> textures)
		{
			this.SetGlobalTextures(rgContext, textures, false);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003D1F File Offset: 0x00001F1F
		internal void PostRenderPassUnbindGlobalTextures(RenderGraphContext rgContext, List<RenderGraphResource> textures)
		{
			this.SetGlobalTextures(rgContext, textures, true);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003D2C File Offset: 0x00001F2C
		internal void ReleaseTexturesForPass(RenderGraphContext rgContext, int passIndex, List<RenderGraphResource> readTextures, List<RenderGraphMutableResource> writtenTextures)
		{
			foreach (RenderGraphResource renderGraphResource in readTextures)
			{
				ref RenderGraphResourceRegistry.TextureResource textureResource = ref this.GetTextureResource(renderGraphResource);
				if (!textureResource.imported && textureResource.lastReadPassIndex == passIndex)
				{
					if (this.m_RenderGraphDebug.clearRenderTargetsAtRelease)
					{
						using (new ProfilingScope(rgContext.cmd, ProfilingSampler.Get<RenderGraphProfileId>(RenderGraphProfileId.RenderGraphClearDebug)))
						{
							ClearFlag clearFlag = ((textureResource.desc.depthBufferBits != DepthBits.None) ? ClearFlag.Depth : ClearFlag.Color);
							CoreUtils.SetRenderTarget(rgContext.cmd, this.GetTexture(in renderGraphResource), clearFlag, Color.magenta, 0, CubemapFace.Unknown, -1);
						}
					}
					this.ReleaseTextureForPass(renderGraphResource);
				}
			}
			foreach (RenderGraphMutableResource renderGraphMutableResource in writtenTextures)
			{
				ref RenderGraphResourceRegistry.TextureResource textureResource2 = ref this.GetTextureResource(renderGraphMutableResource);
				if (!textureResource2.imported && textureResource2.lastReadPassIndex <= passIndex)
				{
					this.ReleaseTextureForPass(renderGraphMutableResource);
				}
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003E6C File Offset: 0x0000206C
		private void ReleaseTextureForPass(RenderGraphResource res)
		{
			ref RenderGraphResourceRegistry.TextureResource ptr = ref this.m_TextureResources[res.handle];
			if (ptr.rt != null)
			{
				this.LogTextureRelease(ptr.rt);
				this.ReleaseTextureResource(ptr.cachedHash, ptr.rt);
				ptr.cachedHash = -1;
				ptr.rt = null;
				ptr.wasReleased = true;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003EC8 File Offset: 0x000020C8
		private void ReleaseTextureResource(int hash, RTHandle rt)
		{
			Stack<RTHandle> stack;
			if (!this.m_TexturePool.TryGetValue(hash, out stack))
			{
				stack = new Stack<RTHandle>();
				this.m_TexturePool.Add(hash, stack);
			}
			stack.Push(rt);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002788 File Offset: 0x00000988
		private void ValidateTextureDesc(in TextureDesc desc)
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002788 File Offset: 0x00000988
		private void ValidateRendererListDesc(in RendererListDesc desc)
		{
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003F00 File Offset: 0x00002100
		private bool TryGetRenderTarget(int hashCode, out RTHandle rt)
		{
			Stack<RTHandle> stack;
			if (this.m_TexturePool.TryGetValue(hashCode, out stack) && stack.Count > 0)
			{
				rt = stack.Pop();
				return true;
			}
			rt = null;
			return false;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003F34 File Offset: 0x00002134
		internal void CreateRendererLists(List<RenderGraphResource> rendererLists)
		{
			foreach (RenderGraphResource renderGraphResource in rendererLists)
			{
				ref RenderGraphResourceRegistry.RendererListResource ptr = ref this.m_RendererListResources[renderGraphResource.handle];
				RendererList rendererList = RendererList.Create(in ptr.desc);
				ptr.rendererList = rendererList;
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003FA0 File Offset: 0x000021A0
		internal void Clear()
		{
			this.LogResources();
			this.m_TextureResources.Clear();
			this.m_RendererListResources.Clear();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003FC0 File Offset: 0x000021C0
		internal void Cleanup()
		{
			foreach (KeyValuePair<int, Stack<RTHandle>> keyValuePair in this.m_TexturePool)
			{
				foreach (RTHandle rthandle in keyValuePair.Value)
				{
					this.m_RTHandleSystem.Release(rthandle);
				}
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004054 File Offset: 0x00002254
		private void LogTextureCreation(RTHandle rt, bool cleared)
		{
			if (this.m_RenderGraphDebug.logFrameInformation)
			{
				this.m_Logger.LogLine("Created Texture: {0} (Cleared: {1})", new object[]
				{
					rt.rt.name,
					cleared
				});
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004090 File Offset: 0x00002290
		private void LogTextureRelease(RTHandle rt)
		{
			if (this.m_RenderGraphDebug.logFrameInformation)
			{
				this.m_Logger.LogLine("Released Texture: {0}", new object[] { rt.rt.name });
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000040C4 File Offset: 0x000022C4
		private void LogResources()
		{
			if (this.m_RenderGraphDebug.logResources)
			{
				this.m_Logger.LogLine("==== Allocated Resources ====\n", Array.Empty<object>());
				List<string> list = new List<string>();
				foreach (KeyValuePair<int, Stack<RTHandle>> keyValuePair in this.m_TexturePool)
				{
					foreach (RTHandle rthandle in keyValuePair.Value)
					{
						list.Add(rthandle.rt.name);
					}
				}
				list.Sort();
				int num = 0;
				foreach (string text in list)
				{
					this.m_Logger.LogLine("[{0}] {1}", new object[]
					{
						num++,
						text
					});
				}
			}
		}

		// Token: 0x04000069 RID: 105
		private static readonly ShaderTagId s_EmptyName = new ShaderTagId("");

		// Token: 0x0400006A RID: 106
		private DynamicArray<RenderGraphResourceRegistry.TextureResource> m_TextureResources = new DynamicArray<RenderGraphResourceRegistry.TextureResource>();

		// Token: 0x0400006B RID: 107
		private Dictionary<int, Stack<RTHandle>> m_TexturePool = new Dictionary<int, Stack<RTHandle>>();

		// Token: 0x0400006C RID: 108
		private DynamicArray<RenderGraphResourceRegistry.RendererListResource> m_RendererListResources = new DynamicArray<RenderGraphResourceRegistry.RendererListResource>();

		// Token: 0x0400006D RID: 109
		private RTHandleSystem m_RTHandleSystem = new RTHandleSystem();

		// Token: 0x0400006E RID: 110
		private RenderGraphDebugParams m_RenderGraphDebug;

		// Token: 0x0400006F RID: 111
		private RenderGraphLogger m_Logger;

		// Token: 0x04000070 RID: 112
		private List<ValueTuple<int, RTHandle>> m_AllocatedTextures = new List<ValueTuple<int, RTHandle>>();

		// Token: 0x020000B3 RID: 179
		internal struct TextureResource
		{
			// Token: 0x0600048E RID: 1166 RVA: 0x000111D5 File Offset: 0x0000F3D5
			internal TextureResource(RTHandle rt, int shaderProperty)
			{
				this = default(RenderGraphResourceRegistry.TextureResource);
				this.Reset();
				this.rt = rt;
				this.imported = true;
				this.shaderProperty = shaderProperty;
			}

			// Token: 0x0600048F RID: 1167 RVA: 0x000111F9 File Offset: 0x0000F3F9
			internal TextureResource(in TextureDesc desc, int shaderProperty)
			{
				this = default(RenderGraphResourceRegistry.TextureResource);
				this.Reset();
				this.desc = desc;
				this.shaderProperty = shaderProperty;
			}

			// Token: 0x06000490 RID: 1168 RVA: 0x0001121B File Offset: 0x0000F41B
			private void Reset()
			{
				this.imported = false;
				this.rt = null;
				this.cachedHash = -1;
				this.firstWritePassIndex = int.MaxValue;
				this.lastReadPassIndex = -1;
				this.wasReleased = false;
			}

			// Token: 0x04000257 RID: 599
			public TextureDesc desc;

			// Token: 0x04000258 RID: 600
			public bool imported;

			// Token: 0x04000259 RID: 601
			public RTHandle rt;

			// Token: 0x0400025A RID: 602
			public int cachedHash;

			// Token: 0x0400025B RID: 603
			public int firstWritePassIndex;

			// Token: 0x0400025C RID: 604
			public int lastReadPassIndex;

			// Token: 0x0400025D RID: 605
			public int shaderProperty;

			// Token: 0x0400025E RID: 606
			public bool wasReleased;
		}

		// Token: 0x020000B4 RID: 180
		internal struct RendererListResource
		{
			// Token: 0x06000491 RID: 1169 RVA: 0x0001124B File Offset: 0x0000F44B
			internal RendererListResource(in RendererListDesc desc)
			{
				this.desc = desc;
				this.rendererList = default(RendererList);
			}

			// Token: 0x0400025F RID: 607
			public RendererListDesc desc;

			// Token: 0x04000260 RID: 608
			public RendererList rendererList;
		}
	}
}
