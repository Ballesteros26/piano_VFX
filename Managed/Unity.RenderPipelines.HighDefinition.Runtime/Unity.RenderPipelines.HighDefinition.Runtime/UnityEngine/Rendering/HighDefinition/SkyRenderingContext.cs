using System;
using Unity.Collections;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000163 RID: 355
	internal class SkyRenderingContext
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x000523E0 File Offset: 0x000505E0
		public SphericalHarmonicsL2 ambientProbe
		{
			get
			{
				return this.m_AmbientProbe;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x000523E8 File Offset: 0x000505E8
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x000523F0 File Offset: 0x000505F0
		public ComputeBuffer ambientProbeResult { get; private set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x000523F9 File Offset: 0x000505F9
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x00052401 File Offset: 0x00050601
		public RTHandle skyboxCubemapRT { get; private set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x0005240A File Offset: 0x0005060A
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x00052412 File Offset: 0x00050612
		public CubemapArray skyboxBSDFCubemapArray { get; private set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0005241B File Offset: 0x0005061B
		// (set) Token: 0x06000A8B RID: 2699 RVA: 0x00052423 File Offset: 0x00050623
		public bool supportsConvolution { get; private set; }

		// Token: 0x06000A8C RID: 2700 RVA: 0x0005242C File Offset: 0x0005062C
		public SkyRenderingContext(int resolution, int bsdfCount, bool supportsConvolution, SphericalHarmonicsL2 ambientProbe)
		{
			this.m_AmbientProbe = ambientProbe;
			this.supportsConvolution = supportsConvolution;
			this.ambientProbeResult = new ComputeBuffer(27, 4);
			this.skyboxCubemapRT = RTHandles.Alloc(resolution, resolution, 1, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Trilinear, TextureWrapMode.Repeat, TextureDimension.Cube, false, true, false, false, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, "SkyboxCubemap");
			if (supportsConvolution)
			{
				this.skyboxBSDFCubemapArray = new CubemapArray(resolution, bsdfCount, TextureFormat.RGBAHalf, true)
				{
					hideFlags = HideFlags.HideAndDontSave,
					wrapMode = TextureWrapMode.Repeat,
					wrapModeV = TextureWrapMode.Clamp,
					filterMode = FilterMode.Trilinear,
					anisoLevel = 0,
					name = "SkyboxCubemapConvolution"
				};
			}
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x000524C5 File Offset: 0x000506C5
		public void Cleanup()
		{
			RTHandles.Release(this.skyboxCubemapRT);
			if (this.skyboxBSDFCubemapArray != null)
			{
				CoreUtils.Destroy(this.skyboxBSDFCubemapArray);
			}
			this.ambientProbeResult.Release();
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x000524F6 File Offset: 0x000506F6
		public void ClearAmbientProbe()
		{
			this.m_AmbientProbe = default(SphericalHarmonicsL2);
		}

		// Token: 0x06000A8F RID: 2703 RVA: 0x00052504 File Offset: 0x00050704
		public void UpdateAmbientProbe(in SphericalHarmonicsL2 probe)
		{
			this.m_AmbientProbe = probe;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x00052514 File Offset: 0x00050714
		public void OnComputeAmbientProbeDone(AsyncGPUReadbackRequest request)
		{
			if (!request.hasError)
			{
				NativeArray<float> data = request.GetData<float>(0);
				for (int i = 0; i < 3; i++)
				{
					for (int j = 0; j < 9; j++)
					{
						this.m_AmbientProbe[i, j] = data[i * 9 + j];
					}
				}
			}
		}

		// Token: 0x04000FE0 RID: 4064
		private SphericalHarmonicsL2 m_AmbientProbe;
	}
}
