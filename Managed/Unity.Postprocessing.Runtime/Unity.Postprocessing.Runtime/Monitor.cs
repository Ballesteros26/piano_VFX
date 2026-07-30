using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200003B RID: 59
	public abstract class Monitor
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00008D06 File Offset: 0x00006F06
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00008D0E File Offset: 0x00006F0E
		public RenderTexture output { get; protected set; }

		// Token: 0x060000A7 RID: 167 RVA: 0x00008D17 File Offset: 0x00006F17
		public bool IsRequestedAndSupported(PostProcessRenderContext context)
		{
			return this.requested && SystemInfo.supportsComputeShaders && !RuntimeUtilities.isAndroidOpenGL && this.ShaderResourcesAvailable(context);
		}

		// Token: 0x060000A8 RID: 168
		internal abstract bool ShaderResourcesAvailable(PostProcessRenderContext context);

		// Token: 0x060000A9 RID: 169 RVA: 0x00008D38 File Offset: 0x00006F38
		internal virtual bool NeedsHalfRes()
		{
			return false;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00008D3C File Offset: 0x00006F3C
		protected void CheckOutput(int width, int height)
		{
			if (this.output == null || !this.output.IsCreated() || this.output.width != width || this.output.height != height)
			{
				RuntimeUtilities.Destroy(this.output);
				this.output = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32)
				{
					anisoLevel = 0,
					filterMode = FilterMode.Bilinear,
					wrapMode = TextureWrapMode.Clamp,
					useMipMap = false
				};
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002430 File Offset: 0x00000630
		internal virtual void OnEnable()
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00008DB6 File Offset: 0x00006FB6
		internal virtual void OnDisable()
		{
			RuntimeUtilities.Destroy(this.output);
		}

		// Token: 0x060000AD RID: 173
		internal abstract void Render(PostProcessRenderContext context);

		// Token: 0x040000EC RID: 236
		internal bool requested;
	}
}
