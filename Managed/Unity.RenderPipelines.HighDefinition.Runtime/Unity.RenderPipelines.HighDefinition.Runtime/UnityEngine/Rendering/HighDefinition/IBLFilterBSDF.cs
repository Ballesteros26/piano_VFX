using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000BE RID: 190
	internal abstract class IBLFilterBSDF
	{
		// Token: 0x060006F3 RID: 1779
		public abstract bool IsInitialized();

		// Token: 0x060006F4 RID: 1780
		public abstract void Initialize(CommandBuffer cmd);

		// Token: 0x060006F5 RID: 1781
		public abstract void Cleanup();

		// Token: 0x060006F6 RID: 1782
		public abstract void FilterCubemap(CommandBuffer cmd, Texture source, RenderTexture target);

		// Token: 0x060006F7 RID: 1783 RVA: 0x00036EEB File Offset: 0x000350EB
		public void FilterPlanarTexture(CommandBuffer cmd, RenderTexture source, RenderTexture target)
		{
			this.m_MipGenerator.RenderColorGaussianPyramid(cmd, new Vector2Int(source.width, source.height), source, target);
		}

		// Token: 0x060006F8 RID: 1784
		public abstract void FilterCubemapMIS(CommandBuffer cmd, Texture source, RenderTexture target, RenderTexture conditionalCdf, RenderTexture marginalRowCdf);

		// Token: 0x04000728 RID: 1832
		protected Material m_convolveMaterial;

		// Token: 0x04000729 RID: 1833
		protected Matrix4x4[] m_faceWorldToViewMatrixMatrices = new Matrix4x4[6];

		// Token: 0x0400072A RID: 1834
		protected RenderPipelineResources m_RenderPipelineResources;

		// Token: 0x0400072B RID: 1835
		protected MipGenerator m_MipGenerator;
	}
}
