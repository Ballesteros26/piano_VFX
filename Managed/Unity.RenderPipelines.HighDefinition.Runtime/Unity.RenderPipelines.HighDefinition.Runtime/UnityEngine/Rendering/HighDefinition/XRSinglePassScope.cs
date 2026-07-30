using System;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000101 RID: 257
	internal struct XRSinglePassScope : IDisposable
	{
		// Token: 0x06000842 RID: 2114 RVA: 0x0004202F File Offset: 0x0004022F
		public XRSinglePassScope(RenderGraph renderGraph, HDCamera hdCamera)
		{
			this.m_RenderGraph = renderGraph;
			this.m_HDCamera = hdCamera;
			this.m_Disposed = false;
			HDRenderPipeline.StartXRSinglePass(renderGraph, hdCamera);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0004204D File Offset: 0x0004024D
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x00042056 File Offset: 0x00040256
		private void Dispose(bool disposing)
		{
			if (this.m_Disposed)
			{
				return;
			}
			if (disposing)
			{
				HDRenderPipeline.StopXRSinglePass(this.m_RenderGraph, this.m_HDCamera);
			}
			this.m_Disposed = true;
		}

		// Token: 0x0400099D RID: 2461
		private readonly RenderGraph m_RenderGraph;

		// Token: 0x0400099E RID: 2462
		private readonly HDCamera m_HDCamera;

		// Token: 0x0400099F RID: 2463
		private bool m_Disposed;
	}
}
