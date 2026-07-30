using System;
using System.Collections.Generic;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x0200000C RID: 12
	public class RenderGraph
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000026FB File Offset: 0x000008FB
		public bool enabled
		{
			get
			{
				return this.m_DebugParameters.enableRenderGraph;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002708 File Offset: 0x00000908
		public RTHandleProperties rtHandleProperties
		{
			get
			{
				return this.m_Resources.GetRTHandleProperties();
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002718 File Offset: 0x00000918
		public RenderGraph(bool supportMSAA, MSAASamples initialSampleCount)
		{
			this.m_Resources = new RenderGraphResourceRegistry(supportMSAA, initialSampleCount, this.m_DebugParameters, this.m_Logger);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000277B File Offset: 0x0000097B
		public void Cleanup()
		{
			this.m_Resources.Cleanup();
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002788 File Offset: 0x00000988
		public void RegisterDebug()
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002788 File Offset: 0x00000988
		public void UnRegisterDebug()
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x0000278A File Offset: 0x0000098A
		public RenderGraphMutableResource ImportTexture(RTHandle rt, int shaderProperty = 0)
		{
			return this.m_Resources.ImportTexture(rt, shaderProperty);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002799 File Offset: 0x00000999
		public RenderGraphMutableResource CreateTexture(TextureDesc desc, int shaderProperty = 0)
		{
			if (this.m_DebugParameters.tagResourceNamesWithRG)
			{
				desc.name = string.Format("{0}_RenderGraph", desc.name);
			}
			return this.m_Resources.CreateTexture(in desc, shaderProperty);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x000027D0 File Offset: 0x000009D0
		public RenderGraphMutableResource CreateTexture(in RenderGraphResource texture, int shaderProperty = 0)
		{
			TextureDesc textureResourceDesc = this.m_Resources.GetTextureResourceDesc(texture);
			if (this.m_DebugParameters.tagResourceNamesWithRG)
			{
				textureResourceDesc.name = string.Format("{0}_RenderGraph", textureResourceDesc.name);
			}
			return this.m_Resources.CreateTexture(in textureResourceDesc, shaderProperty);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002824 File Offset: 0x00000A24
		public TextureDesc GetTextureDesc(in RenderGraphResource texture)
		{
			RenderGraphResource renderGraphResource = texture;
			if (renderGraphResource.type != RenderGraphResourceType.Texture)
			{
				throw new ArgumentException("Trying to retrieve a TextureDesc from a resource that is not a texture.");
			}
			return this.m_Resources.GetTextureResourceDesc(texture);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000285E File Offset: 0x00000A5E
		public RenderGraphResource CreateRendererList(in RendererListDesc desc)
		{
			return this.m_Resources.CreateRendererList(in desc);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000286C File Offset: 0x00000A6C
		public RenderGraphBuilder AddRenderPass<PassData>(string passName, out PassData passData, ProfilingSampler sampler = null) where PassData : class, new()
		{
			RenderGraph.RenderPass<PassData> renderPass = this.m_RenderGraphPool.Get<RenderGraph.RenderPass<PassData>>();
			renderPass.Clear();
			renderPass.index = this.m_RenderPasses.Count;
			renderPass.data = this.m_RenderGraphPool.Get<PassData>();
			renderPass.name = passName;
			renderPass.customSampler = sampler;
			passData = renderPass.data;
			this.m_RenderPasses.Add(renderPass);
			return new RenderGraphBuilder(renderPass, this.m_Resources);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028E0 File Offset: 0x00000AE0
		public void Execute(ScriptableRenderContext renderContext, CommandBuffer cmd, in RenderGraphExecuteParams parameters)
		{
			this.m_Logger.Initialize();
			this.m_Resources.SetRTHandleReferenceSize(parameters.renderingWidth, parameters.renderingHeight, parameters.msaaSamples);
			this.LogFrameInformation(parameters.renderingWidth, parameters.renderingHeight);
			for (int i = 0; i < this.m_RenderPasses.Count; i++)
			{
				RenderGraph.RenderPass renderPass = this.m_RenderPasses[i];
				this.m_RendererLists.AddRange(renderPass.usedRendererListList);
			}
			this.m_Resources.CreateRendererLists(this.m_RendererLists);
			this.LogRendererListsCreation();
			RenderGraphContext renderGraphContext = default(RenderGraphContext);
			renderGraphContext.cmd = cmd;
			renderGraphContext.renderContext = renderContext;
			renderGraphContext.renderGraphPool = this.m_RenderGraphPool;
			renderGraphContext.resources = this.m_Resources;
			try
			{
				for (int j = 0; j < this.m_RenderPasses.Count; j++)
				{
					RenderGraph.RenderPass renderPass2 = this.m_RenderPasses[j];
					if (!renderPass2.HasRenderFunc())
					{
						throw new InvalidOperationException(string.Format("RenderPass {0} was not provided with an execute function.", renderPass2.name));
					}
					using (new ProfilingScope(cmd, renderPass2.customSampler))
					{
						this.LogRenderPassBegin(in renderPass2);
						using (new RenderGraphLogIndent(this.m_Logger, 1))
						{
							this.PreRenderPassExecute(j, in renderPass2, renderGraphContext);
							renderPass2.Execute(renderGraphContext);
							this.PostRenderPassExecute(j, in renderPass2, renderGraphContext);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("Render Graph Execution error");
				Debug.LogException(ex);
			}
			finally
			{
				this.ClearRenderPasses();
				this.m_Resources.Clear();
				this.m_RendererLists.Clear();
				if (this.m_DebugParameters.logFrameInformation || this.m_DebugParameters.logResources)
				{
					Debug.Log(this.m_Logger.GetLog());
				}
				this.m_DebugParameters.logFrameInformation = false;
				this.m_DebugParameters.logResources = false;
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002AF8 File Offset: 0x00000CF8
		private RenderGraph()
		{
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002B38 File Offset: 0x00000D38
		private void PreRenderPassSetRenderTargets(in RenderGraph.RenderPass pass, RenderGraphContext rgContext)
		{
			if (pass.depthBuffer.IsValid() || pass.colorBufferMaxIndex != -1)
			{
				RenderTargetIdentifier[] tempArray = rgContext.renderGraphPool.GetTempArray<RenderTargetIdentifier>(pass.colorBufferMaxIndex + 1);
				RenderGraphMutableResource[] colorBuffers = pass.colorBuffers;
				if (pass.colorBufferMaxIndex > 0)
				{
					for (int i = 0; i <= pass.colorBufferMaxIndex; i++)
					{
						if (!colorBuffers[i].IsValid())
						{
							throw new InvalidOperationException("MRT setup is invalid. Some indices are not used.");
						}
						RenderTargetIdentifier[] array = tempArray;
						int num = i;
						RenderGraphResourceRegistry resources = this.m_Resources;
						RenderGraphResource renderGraphResource = colorBuffers[i];
						array[num] = resources.GetTexture(in renderGraphResource);
					}
					if (pass.depthBuffer.IsValid())
					{
						CommandBuffer cmd = rgContext.cmd;
						RenderTargetIdentifier[] array2 = tempArray;
						RenderGraphResourceRegistry resources2 = this.m_Resources;
						RenderGraphResource renderGraphResource = pass.depthBuffer;
						CoreUtils.SetRenderTarget(cmd, array2, resources2.GetTexture(in renderGraphResource));
						return;
					}
					throw new InvalidOperationException("Setting MRTs without a depth buffer is not supported.");
				}
				else if (pass.depthBuffer.IsValid())
				{
					RenderGraphResource renderGraphResource;
					if (pass.colorBufferMaxIndex > -1)
					{
						CommandBuffer cmd2 = rgContext.cmd;
						RenderGraphResourceRegistry resources3 = this.m_Resources;
						renderGraphResource = pass.colorBuffers[0];
						RTHandle texture = resources3.GetTexture(in renderGraphResource);
						RenderGraphResourceRegistry resources4 = this.m_Resources;
						RenderGraphResource renderGraphResource2 = pass.depthBuffer;
						CoreUtils.SetRenderTarget(cmd2, texture, resources4.GetTexture(in renderGraphResource2), 0, CubemapFace.Unknown, -1);
						return;
					}
					CommandBuffer cmd3 = rgContext.cmd;
					RenderGraphResourceRegistry resources5 = this.m_Resources;
					renderGraphResource = pass.depthBuffer;
					CoreUtils.SetRenderTarget(cmd3, resources5.GetTexture(in renderGraphResource), ClearFlag.None, 0, CubemapFace.Unknown, -1);
					return;
				}
				else
				{
					CommandBuffer cmd4 = rgContext.cmd;
					RenderGraphResourceRegistry resources6 = this.m_Resources;
					RenderGraphResource renderGraphResource = pass.colorBuffers[0];
					CoreUtils.SetRenderTarget(cmd4, resources6.GetTexture(in renderGraphResource), ClearFlag.None, 0, CubemapFace.Unknown, -1);
				}
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002CE7 File Offset: 0x00000EE7
		private void PreRenderPassExecute(int passIndex, in RenderGraph.RenderPass pass, RenderGraphContext rgContext)
		{
			this.m_Resources.CreateAndClearTexturesForPass(rgContext, pass.index, pass.resourceWriteList);
			this.PreRenderPassSetRenderTargets(in pass, rgContext);
			this.m_Resources.PreRenderPassSetGlobalTextures(rgContext, pass.resourceReadList);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002D20 File Offset: 0x00000F20
		private void PostRenderPassExecute(int passIndex, in RenderGraph.RenderPass pass, RenderGraphContext rgContext)
		{
			if (this.m_DebugParameters.unbindGlobalTextures)
			{
				this.m_Resources.PostRenderPassUnbindGlobalTextures(rgContext, pass.resourceReadList);
			}
			this.m_RenderGraphPool.ReleaseAllTempAlloc();
			this.m_Resources.ReleaseTexturesForPass(rgContext, pass.index, pass.resourceReadList, pass.resourceWriteList);
			pass.Release(rgContext);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002D81 File Offset: 0x00000F81
		private void ClearRenderPasses()
		{
			this.m_RenderPasses.Clear();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002D90 File Offset: 0x00000F90
		private void LogFrameInformation(int renderingWidth, int renderingHeight)
		{
			if (this.m_DebugParameters.logFrameInformation)
			{
				this.m_Logger.LogLine("==== Staring frame at resolution ({0}x{1}) ====", new object[] { renderingWidth, renderingHeight });
				this.m_Logger.LogLine("Number of passes declared: {0}", new object[] { this.m_RenderPasses.Count });
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002DFB File Offset: 0x00000FFB
		private void LogRendererListsCreation()
		{
			if (this.m_DebugParameters.logFrameInformation)
			{
				this.m_Logger.LogLine("Number of renderer lists created: {0}", new object[] { this.m_RendererLists.Count });
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002E33 File Offset: 0x00001033
		private void LogRenderPassBegin(in RenderGraph.RenderPass pass)
		{
			if (this.m_DebugParameters.logFrameInformation)
			{
				this.m_Logger.LogLine("Executing pass \"{0}\" (index: {1})", new object[] { pass.name, pass.index });
			}
		}

		// Token: 0x0400002E RID: 46
		public static readonly int kMaxMRTCount = 8;

		// Token: 0x0400002F RID: 47
		private RenderGraphResourceRegistry m_Resources;

		// Token: 0x04000030 RID: 48
		private RenderGraphObjectPool m_RenderGraphPool = new RenderGraphObjectPool();

		// Token: 0x04000031 RID: 49
		private List<RenderGraph.RenderPass> m_RenderPasses = new List<RenderGraph.RenderPass>();

		// Token: 0x04000032 RID: 50
		private List<RenderGraphResource> m_RendererLists = new List<RenderGraphResource>();

		// Token: 0x04000033 RID: 51
		private RenderGraphDebugParams m_DebugParameters = new RenderGraphDebugParams();

		// Token: 0x04000034 RID: 52
		private RenderGraphLogger m_Logger = new RenderGraphLogger();

		// Token: 0x020000B0 RID: 176
		internal abstract class RenderPass
		{
			// Token: 0x0600047A RID: 1146 RVA: 0x00010FBD File Offset: 0x0000F1BD
			internal RenderFunc<PassData> GetExecuteDelegate<PassData>() where PassData : class, new()
			{
				return ((RenderGraph.RenderPass<PassData>)this).renderFunc;
			}

			// Token: 0x0600047B RID: 1147
			internal abstract void Execute(RenderGraphContext renderGraphContext);

			// Token: 0x0600047C RID: 1148
			internal abstract void Release(RenderGraphContext renderGraphContext);

			// Token: 0x0600047D RID: 1149
			internal abstract bool HasRenderFunc();

			// Token: 0x17000098 RID: 152
			// (get) Token: 0x0600047E RID: 1150 RVA: 0x00010FCA File Offset: 0x0000F1CA
			internal RenderGraphMutableResource depthBuffer
			{
				get
				{
					return this.m_DepthBuffer;
				}
			}

			// Token: 0x17000099 RID: 153
			// (get) Token: 0x0600047F RID: 1151 RVA: 0x00010FD2 File Offset: 0x0000F1D2
			internal RenderGraphMutableResource[] colorBuffers
			{
				get
				{
					return this.m_ColorBuffers;
				}
			}

			// Token: 0x1700009A RID: 154
			// (get) Token: 0x06000480 RID: 1152 RVA: 0x00010FDA File Offset: 0x0000F1DA
			internal int colorBufferMaxIndex
			{
				get
				{
					return this.m_MaxColorBufferIndex;
				}
			}

			// Token: 0x06000481 RID: 1153 RVA: 0x00010FE4 File Offset: 0x0000F1E4
			internal void Clear()
			{
				this.name = "";
				this.index = -1;
				this.customSampler = null;
				this.resourceReadList.Clear();
				this.resourceWriteList.Clear();
				this.usedRendererListList.Clear();
				this.enableAsyncCompute = false;
				this.m_MaxColorBufferIndex = -1;
				this.m_DepthBuffer = default(RenderGraphMutableResource);
				for (int i = 0; i < RenderGraph.kMaxMRTCount; i++)
				{
					this.m_ColorBuffers[i] = default(RenderGraphMutableResource);
				}
			}

			// Token: 0x06000482 RID: 1154 RVA: 0x00011067 File Offset: 0x0000F267
			internal void SetColorBuffer(in RenderGraphMutableResource resource, int index)
			{
				this.m_MaxColorBufferIndex = Math.Max(this.m_MaxColorBufferIndex, index);
				this.m_ColorBuffers[index] = resource;
				this.resourceWriteList.Add(resource);
			}

			// Token: 0x06000483 RID: 1155 RVA: 0x0001109E File Offset: 0x0000F29E
			internal void SetDepthBuffer(in RenderGraphMutableResource resource, DepthAccess flags)
			{
				this.m_DepthBuffer = resource;
				if ((flags | DepthAccess.Read) != (DepthAccess)0)
				{
					this.resourceReadList.Add(resource);
				}
				if ((flags | DepthAccess.Write) != (DepthAccess)0)
				{
					this.resourceWriteList.Add(resource);
				}
			}

			// Token: 0x04000249 RID: 585
			internal string name;

			// Token: 0x0400024A RID: 586
			internal int index;

			// Token: 0x0400024B RID: 587
			internal ProfilingSampler customSampler;

			// Token: 0x0400024C RID: 588
			internal List<RenderGraphResource> resourceReadList = new List<RenderGraphResource>();

			// Token: 0x0400024D RID: 589
			internal List<RenderGraphMutableResource> resourceWriteList = new List<RenderGraphMutableResource>();

			// Token: 0x0400024E RID: 590
			internal List<RenderGraphResource> usedRendererListList = new List<RenderGraphResource>();

			// Token: 0x0400024F RID: 591
			internal bool enableAsyncCompute;

			// Token: 0x04000250 RID: 592
			protected RenderGraphMutableResource[] m_ColorBuffers = new RenderGraphMutableResource[RenderGraph.kMaxMRTCount];

			// Token: 0x04000251 RID: 593
			protected RenderGraphMutableResource m_DepthBuffer;

			// Token: 0x04000252 RID: 594
			protected int m_MaxColorBufferIndex = -1;
		}

		// Token: 0x020000B1 RID: 177
		internal sealed class RenderPass<PassData> : RenderGraph.RenderPass where PassData : class, new()
		{
			// Token: 0x06000485 RID: 1157 RVA: 0x0001111D File Offset: 0x0000F31D
			internal override void Execute(RenderGraphContext renderGraphContext)
			{
				base.GetExecuteDelegate<PassData>()(this.data, renderGraphContext);
			}

			// Token: 0x06000486 RID: 1158 RVA: 0x00011131 File Offset: 0x0000F331
			internal override void Release(RenderGraphContext renderGraphContext)
			{
				base.Clear();
				renderGraphContext.renderGraphPool.Release<PassData>(this.data);
				this.data = default(PassData);
				this.renderFunc = null;
				renderGraphContext.renderGraphPool.Release<RenderGraph.RenderPass<PassData>>(this);
			}

			// Token: 0x06000487 RID: 1159 RVA: 0x00011169 File Offset: 0x0000F369
			internal override bool HasRenderFunc()
			{
				return this.renderFunc != null;
			}

			// Token: 0x04000253 RID: 595
			internal PassData data;

			// Token: 0x04000254 RID: 596
			internal RenderFunc<PassData> renderFunc;
		}
	}
}
