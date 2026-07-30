using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000122 RID: 290
	public struct AOVRequestData
	{
		// Token: 0x060008BD RID: 2237 RVA: 0x00048544 File Offset: 0x00046744
		public static AOVRequestData NewDefault()
		{
			return new AOVRequestData
			{
				m_Settings = AOVRequest.NewDefault(),
				m_RequestedAOVBuffers = new AOVBuffers[0],
				m_Callback = null
			};
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0004857B File Offset: 0x0004677B
		public bool isValid
		{
			get
			{
				return this.m_RequestedAOVBuffers != null && this.m_Callback != null;
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x00048590 File Offset: 0x00046790
		public AOVRequestData(AOVRequest settings, AOVRequestBufferAllocator bufferAllocator, List<GameObject> lightFilter, AOVBuffers[] requestedAOVBuffers, FramePassCallback callback)
		{
			this.m_Settings = settings;
			this.m_BufferAllocator = bufferAllocator;
			this.m_RequestedAOVBuffers = requestedAOVBuffers;
			this.m_LightFilter = lightFilter;
			this.m_Callback = callback;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x000485B8 File Offset: 0x000467B8
		public void AllocateTargetTexturesIfRequired(ref List<RTHandle> textures)
		{
			if (!this.isValid || textures == null)
			{
				return;
			}
			textures.Clear();
			foreach (AOVBuffers aovbuffers in this.m_RequestedAOVBuffers)
			{
				textures.Add(this.m_BufferAllocator(aovbuffers));
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x00048608 File Offset: 0x00046808
		internal void PushCameraTexture(CommandBuffer cmd, AOVBuffers aovBufferId, HDCamera camera, RTHandle source, List<RTHandle> targets)
		{
			if (!this.isValid)
			{
				return;
			}
			int num = Array.IndexOf<AOVBuffers>(this.m_RequestedAOVBuffers, aovBufferId);
			if (num == -1)
			{
				return;
			}
			HDUtils.BlitCameraTexture(cmd, source, targets[num], 0f, false);
		}

		// Token: 0x060008C2 RID: 2242 RVA: 0x00048648 File Offset: 0x00046848
		internal void PushCameraTexture(RenderGraph renderGraph, AOVBuffers aovBufferId, HDCamera camera, RenderGraphResource source, List<RTHandle> targets)
		{
			if (!this.isValid)
			{
				return;
			}
			int num = Array.IndexOf<AOVBuffers>(this.m_RequestedAOVBuffers, aovBufferId);
			if (num == -1)
			{
				return;
			}
			AOVRequestData.PushCameraTexturePassData pushCameraTexturePassData;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<AOVRequestData.PushCameraTexturePassData>("Push AOV Camera Texture", out pushCameraTexturePassData, null))
			{
				pushCameraTexturePassData.requestIndex = num;
				pushCameraTexturePassData.source = renderGraphBuilder.ReadTexture(in source);
				pushCameraTexturePassData.targets = targets;
				renderGraphBuilder.SetRenderFunc<AOVRequestData.PushCameraTexturePassData>(delegate(AOVRequestData.PushCameraTexturePassData data, RenderGraphContext ctx)
				{
					HDUtils.BlitCameraTexture(ctx.cmd, ctx.resources.GetTexture(in data.source), data.targets[data.requestIndex], 0f, false);
				});
			}
		}

		// Token: 0x060008C3 RID: 2243 RVA: 0x000486E4 File Offset: 0x000468E4
		public void Execute(CommandBuffer cmd, List<RTHandle> framePassTextures, RenderOutputProperties outputProperties)
		{
			if (!this.isValid)
			{
				return;
			}
			this.m_Callback(cmd, framePassTextures, outputProperties);
		}

		// Token: 0x060008C4 RID: 2244 RVA: 0x000486FD File Offset: 0x000468FD
		public void SetupDebugData(ref DebugDisplaySettings debugDisplaySettings)
		{
			if (!this.isValid)
			{
				return;
			}
			debugDisplaySettings = new DebugDisplaySettings();
			this.m_Settings.FillDebugData(debugDisplaySettings);
		}

		// Token: 0x060008C5 RID: 2245 RVA: 0x0004871C File Offset: 0x0004691C
		public bool IsLightEnabled(GameObject gameObject)
		{
			return this.m_LightFilter == null || this.m_LightFilter.Contains(gameObject);
		}

		// Token: 0x04000D95 RID: 3477
		[Obsolete("Since 2019.3, use AOVRequestData.NewDefault() instead.")]
		public static readonly AOVRequestData @default = default(AOVRequestData);

		// Token: 0x04000D96 RID: 3478
		public static readonly AOVRequestData defaultAOVRequestDataNonAlloc = AOVRequestData.NewDefault();

		// Token: 0x04000D97 RID: 3479
		private AOVRequest m_Settings;

		// Token: 0x04000D98 RID: 3480
		private AOVBuffers[] m_RequestedAOVBuffers;

		// Token: 0x04000D99 RID: 3481
		private FramePassCallback m_Callback;

		// Token: 0x04000D9A RID: 3482
		private readonly AOVRequestBufferAllocator m_BufferAllocator;

		// Token: 0x04000D9B RID: 3483
		private List<GameObject> m_LightFilter;

		// Token: 0x0200026F RID: 623
		private class PushCameraTexturePassData
		{
			// Token: 0x04001612 RID: 5650
			public int requestIndex;

			// Token: 0x04001613 RID: 5651
			public RenderGraphResource source;

			// Token: 0x04001614 RID: 5652
			public List<RTHandle> targets;
		}
	}
}
