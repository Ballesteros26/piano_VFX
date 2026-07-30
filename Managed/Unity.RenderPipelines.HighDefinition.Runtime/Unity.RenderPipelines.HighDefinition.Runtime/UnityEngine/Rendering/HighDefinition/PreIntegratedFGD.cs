using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000C0 RID: 192
	internal class PreIntegratedFGD
	{
		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x0003700F File Offset: 0x0003520F
		public static PreIntegratedFGD instance
		{
			get
			{
				if (PreIntegratedFGD.s_Instance == null)
				{
					PreIntegratedFGD.s_Instance = new PreIntegratedFGD();
				}
				return PreIntegratedFGD.s_Instance;
			}
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x00037028 File Offset: 0x00035228
		private PreIntegratedFGD()
		{
			for (int i = 0; i < 2; i++)
			{
				this.m_isInit[i] = false;
				this.m_refCounting[i] = 0;
			}
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0003708C File Offset: 0x0003528C
		public void Build(PreIntegratedFGD.FGDIndex index)
		{
			if (this.m_refCounting[(int)index] == 0)
			{
				HDRenderPipelineAsset defaultAsset = HDRenderPipeline.defaultAsset;
				int num = 64;
				if (index != PreIntegratedFGD.FGDIndex.FGD_GGXAndDisneyDiffuse)
				{
					if (index == PreIntegratedFGD.FGDIndex.FGD_CharlieAndFabricLambert)
					{
						this.m_PreIntegratedFGDMaterial[(int)index] = CoreUtils.CreateEngineMaterial(defaultAsset.renderPipelineResources.shaders.preIntegratedFGD_CharlieFabricLambertPS);
						this.m_PreIntegratedFGD[(int)index] = new RenderTexture(num, num, 0, RenderTextureFormat.ARGB2101010, RenderTextureReadWrite.Linear);
						this.m_PreIntegratedFGD[(int)index].hideFlags = HideFlags.HideAndDontSave;
						this.m_PreIntegratedFGD[(int)index].filterMode = FilterMode.Bilinear;
						this.m_PreIntegratedFGD[(int)index].wrapMode = TextureWrapMode.Clamp;
						this.m_PreIntegratedFGD[(int)index].name = CoreUtils.GetRenderTargetAutoName(num, num, 1, RenderTextureFormat.ARGB2101010, "preIntegratedFGD_CharlieFabricLambert", false, false, MSAASamples.None);
						this.m_PreIntegratedFGD[(int)index].Create();
					}
				}
				else
				{
					this.m_PreIntegratedFGDMaterial[(int)index] = CoreUtils.CreateEngineMaterial(defaultAsset.renderPipelineResources.shaders.preIntegratedFGD_GGXDisneyDiffusePS);
					this.m_PreIntegratedFGD[(int)index] = new RenderTexture(num, num, 0, RenderTextureFormat.ARGB2101010, RenderTextureReadWrite.Linear);
					this.m_PreIntegratedFGD[(int)index].hideFlags = HideFlags.HideAndDontSave;
					this.m_PreIntegratedFGD[(int)index].filterMode = FilterMode.Bilinear;
					this.m_PreIntegratedFGD[(int)index].wrapMode = TextureWrapMode.Clamp;
					this.m_PreIntegratedFGD[(int)index].name = CoreUtils.GetRenderTargetAutoName(num, num, 1, RenderTextureFormat.ARGB2101010, "preIntegratedFGD_GGXDisneyDiffuse", false, false, MSAASamples.None);
					this.m_PreIntegratedFGD[(int)index].Create();
				}
				this.m_isInit[(int)index] = false;
			}
			this.m_refCounting[(int)index]++;
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x000371EC File Offset: 0x000353EC
		public void RenderInit(PreIntegratedFGD.FGDIndex index, CommandBuffer cmd)
		{
			if (this.m_isInit[(int)index] && this.m_PreIntegratedFGD[(int)index].IsCreated())
			{
				return;
			}
			CoreUtils.DrawFullScreen(cmd, this.m_PreIntegratedFGDMaterial[(int)index], new RenderTargetIdentifier(this.m_PreIntegratedFGD[(int)index]), null, 0);
			this.m_isInit[(int)index] = true;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00037239 File Offset: 0x00035439
		public void Cleanup(PreIntegratedFGD.FGDIndex index)
		{
			this.m_refCounting[(int)index]--;
			if (this.m_refCounting[(int)index] == 0)
			{
				CoreUtils.Destroy(this.m_PreIntegratedFGDMaterial[(int)index]);
				CoreUtils.Destroy(this.m_PreIntegratedFGD[(int)index]);
				this.m_isInit[(int)index] = false;
			}
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x00037279 File Offset: 0x00035479
		public void Bind(CommandBuffer cmd, PreIntegratedFGD.FGDIndex index)
		{
			if (index == PreIntegratedFGD.FGDIndex.FGD_GGXAndDisneyDiffuse)
			{
				cmd.SetGlobalTexture(HDShaderIDs._PreIntegratedFGD_GGXDisneyDiffuse, this.m_PreIntegratedFGD[(int)index]);
				return;
			}
			if (index != PreIntegratedFGD.FGDIndex.FGD_CharlieAndFabricLambert)
			{
				return;
			}
			cmd.SetGlobalTexture(HDShaderIDs._PreIntegratedFGD_CharlieAndFabric, this.m_PreIntegratedFGD[(int)index]);
		}

		// Token: 0x0400072C RID: 1836
		private static PreIntegratedFGD s_Instance;

		// Token: 0x0400072D RID: 1837
		private bool[] m_isInit = new bool[2];

		// Token: 0x0400072E RID: 1838
		private int[] m_refCounting = new int[2];

		// Token: 0x0400072F RID: 1839
		private Material[] m_PreIntegratedFGDMaterial = new Material[2];

		// Token: 0x04000730 RID: 1840
		private RenderTexture[] m_PreIntegratedFGD = new RenderTexture[2];

		// Token: 0x02000244 RID: 580
		[GenerateHLSL(PackingRules.Exact, true, false, false, 1, false, false)]
		public enum FGDTexture
		{
			// Token: 0x04001516 RID: 5398
			Resolution = 64
		}

		// Token: 0x02000245 RID: 581
		public enum FGDIndex
		{
			// Token: 0x04001518 RID: 5400
			FGD_GGXAndDisneyDiffuse,
			// Token: 0x04001519 RID: 5401
			FGD_CharlieAndFabricLambert,
			// Token: 0x0400151A RID: 5402
			Count
		}
	}
}
