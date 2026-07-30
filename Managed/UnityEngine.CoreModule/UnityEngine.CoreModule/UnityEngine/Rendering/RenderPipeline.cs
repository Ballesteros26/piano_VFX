using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200036F RID: 879
	public abstract class RenderPipeline
	{
		// Token: 0x06001E1B RID: 7707
		protected abstract void Render(ScriptableRenderContext context, Camera[] cameras);

		// Token: 0x06001E1C RID: 7708 RVA: 0x000332C9 File Offset: 0x000314C9
		protected static void BeginFrameRendering(ScriptableRenderContext context, Camera[] cameras)
		{
			RenderPipelineManager.BeginFrameRendering(context, cameras);
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x000332D4 File Offset: 0x000314D4
		protected static void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			RenderPipelineManager.BeginCameraRendering(context, camera);
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x000332DF File Offset: 0x000314DF
		protected static void EndFrameRendering(ScriptableRenderContext context, Camera[] cameras)
		{
			RenderPipelineManager.EndFrameRendering(context, cameras);
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x000332EA File Offset: 0x000314EA
		protected static void EndCameraRendering(ScriptableRenderContext context, Camera camera)
		{
			RenderPipelineManager.EndCameraRendering(context, camera);
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x000332F8 File Offset: 0x000314F8
		internal void InternalRender(ScriptableRenderContext context, Camera[] cameras)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				throw new ObjectDisposedException(string.Format("{0} has been disposed. Do not call Render on disposed a RenderPipeline.", this));
			}
			this.Render(context, cameras);
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x0003332A File Offset: 0x0003152A
		// (set) Token: 0x06001E22 RID: 7714 RVA: 0x00033332 File Offset: 0x00031532
		public bool disposed { get; private set; }

		// Token: 0x06001E23 RID: 7715 RVA: 0x0003333B File Offset: 0x0003153B
		internal void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
			this.disposed = true;
		}

		// Token: 0x06001E24 RID: 7716 RVA: 0x00002EC3 File Offset: 0x000010C3
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
