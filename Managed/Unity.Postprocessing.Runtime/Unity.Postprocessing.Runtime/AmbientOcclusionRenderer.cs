using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200000E RID: 14
	[Preserve]
	internal sealed class AmbientOcclusionRenderer : PostProcessEffectRenderer<AmbientOcclusion>
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002355 File Offset: 0x00000555
		public override void Init()
		{
			if (this.m_Methods == null)
			{
				this.m_Methods = new IAmbientOcclusionMethod[]
				{
					new ScalableAO(base.settings),
					new MultiScaleVO(base.settings)
				};
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002388 File Offset: 0x00000588
		public bool IsAmbientOnly(PostProcessRenderContext context)
		{
			Camera camera = context.camera;
			return base.settings.ambientOnly.value && camera.actualRenderingPath == RenderingPath.DeferredShading && camera.allowHDR;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023BF File Offset: 0x000005BF
		public IAmbientOcclusionMethod Get()
		{
			return this.m_Methods[(int)base.settings.mode.value];
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023D8 File Offset: 0x000005D8
		public override DepthTextureMode GetCameraFlags()
		{
			return this.Get().GetCameraFlags();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000023E8 File Offset: 0x000005E8
		public override void Release()
		{
			IAmbientOcclusionMethod[] methods = this.m_Methods;
			for (int i = 0; i < methods.Length; i++)
			{
				methods[i].Release();
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002412 File Offset: 0x00000612
		public ScalableAO GetScalableAO()
		{
			return (ScalableAO)this.m_Methods[0];
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002421 File Offset: 0x00000621
		public MultiScaleVO GetMultiScaleVO()
		{
			return (MultiScaleVO)this.m_Methods[1];
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002430 File Offset: 0x00000630
		public override void Render(PostProcessRenderContext context)
		{
		}

		// Token: 0x04000020 RID: 32
		private IAmbientOcclusionMethod[] m_Methods;
	}
}
