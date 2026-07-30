using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000BC RID: 188
	internal class IBLFilterGGX : IBLFilterBSDF
	{
		// Token: 0x060006E6 RID: 1766 RVA: 0x0003697D File Offset: 0x00034B7D
		public IBLFilterGGX(RenderPipelineResources renderPipelineResources, MipGenerator mipGenerator)
		{
			this.m_RenderPipelineResources = renderPipelineResources;
			this.m_MipGenerator = mipGenerator;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x000369BB File Offset: 0x00034BBB
		public override bool IsInitialized()
		{
			return this.m_GgxIblSampleData != null;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x000369CC File Offset: 0x00034BCC
		public override void Initialize(CommandBuffer cmd)
		{
			if (!this.m_ComputeGgxIblSampleDataCS)
			{
				this.m_ComputeGgxIblSampleDataCS = this.m_RenderPipelineResources.shaders.computeGgxIblSampleDataCS;
				this.m_ComputeGgxIblSampleDataKernel = this.m_ComputeGgxIblSampleDataCS.FindKernel("ComputeGgxIblSampleData");
			}
			if (!this.m_BuildProbabilityTablesCS)
			{
				this.m_BuildProbabilityTablesCS = this.m_RenderPipelineResources.shaders.buildProbabilityTablesCS;
				this.m_ConditionalDensitiesKernel = this.m_BuildProbabilityTablesCS.FindKernel("ComputeConditionalDensities");
				this.m_MarginalRowDensitiesKernel = this.m_BuildProbabilityTablesCS.FindKernel("ComputeMarginalRowDensities");
			}
			if (!this.m_convolveMaterial)
			{
				this.m_convolveMaterial = CoreUtils.CreateEngineMaterial(this.m_RenderPipelineResources.shaders.GGXConvolvePS);
			}
			if (!this.m_GgxIblSampleData)
			{
				this.m_GgxIblSampleData = new RenderTexture(this.m_GgxIblMaxSampleCount, 6, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
				this.m_GgxIblSampleData.useMipMap = false;
				this.m_GgxIblSampleData.autoGenerateMips = false;
				this.m_GgxIblSampleData.enableRandomWrite = true;
				this.m_GgxIblSampleData.filterMode = FilterMode.Point;
				this.m_GgxIblSampleData.name = CoreUtils.GetRenderTargetAutoName(this.m_GgxIblMaxSampleCount, 6, 1, RenderTextureFormat.ARGBHalf, "GGXIblSampleData", false, false, MSAASamples.None);
				this.m_GgxIblSampleData.hideFlags = HideFlags.HideAndDontSave;
				this.m_GgxIblSampleData.Create();
				this.InitializeGgxIblSampleData(cmd);
			}
			for (int i = 0; i < 6; i++)
			{
				Matrix4x4 matrix4x = Matrix4x4.LookAt(Vector3.zero, CoreUtils.lookAtList[i], CoreUtils.upVectorList[i]);
				this.m_faceWorldToViewMatrixMatrices[i] = matrix4x * Matrix4x4.Scale(new Vector3(1f, 1f, -1f));
			}
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00036B77 File Offset: 0x00034D77
		private void InitializeGgxIblSampleData(CommandBuffer cmd)
		{
			this.m_ComputeGgxIblSampleDataCS.SetTexture(this.m_ComputeGgxIblSampleDataKernel, "output", this.m_GgxIblSampleData);
			cmd.DispatchCompute(this.m_ComputeGgxIblSampleDataCS, this.m_ComputeGgxIblSampleDataKernel, 1, 1, 1);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00036BAA File Offset: 0x00034DAA
		public override void Cleanup()
		{
			CoreUtils.Destroy(this.m_convolveMaterial);
			CoreUtils.Destroy(this.m_GgxIblSampleData);
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00036BC4 File Offset: 0x00034DC4
		private void FilterCubemapCommon(CommandBuffer cmd, Texture source, RenderTexture target, Matrix4x4[] worldToViewMatrices)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.FilterCubemapGGX)))
			{
				if (1 + (int)Mathf.Log((float)source.width, 2f) < 7)
				{
					Debug.LogWarning("RenderCubemapGGXConvolution: Cubemap size is too small for GGX convolution, needs at least " + 7 + " mip levels");
				}
				else
				{
					for (int i = 0; i < 6; i++)
					{
						cmd.CopyTexture(source, i, 0, target, i, 0);
					}
					float num = 6f * (float)source.width * (float)source.width / 12.566371f;
					if (!this.m_GgxIblSampleData.IsCreated())
					{
						this.m_GgxIblSampleData.Create();
						this.InitializeGgxIblSampleData(cmd);
					}
					this.m_convolveMaterial.SetTexture("_GgxIblSamples", this.m_GgxIblSampleData);
					MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
					materialPropertyBlock.SetTexture("_MainTex", source);
					materialPropertyBlock.SetFloat("_InvOmegaP", num);
					for (int j = 1; j < 7; j++)
					{
						materialPropertyBlock.SetFloat("_Level", (float)j);
						for (int k = 0; k < 6; k++)
						{
							Vector4 vector = new Vector4((float)(source.width >> j), (float)(source.height >> j), 1f / (float)(source.width >> j), 1f / (float)(source.height >> j));
							Matrix4x4 matrix4x = HDUtils.ComputePixelCoordToWorldSpaceViewDirectionMatrix(1.5707964f, Vector2.zero, vector, worldToViewMatrices[k], true, -1f);
							materialPropertyBlock.SetMatrix(HDShaderIDs._PixelCoordToViewDirWS, matrix4x);
							CoreUtils.SetRenderTarget(cmd, target, ClearFlag.None, j, (CubemapFace)k, -1);
							CoreUtils.DrawFullScreen(cmd, this.m_convolveMaterial, materialPropertyBlock, 0);
						}
					}
				}
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00036DA4 File Offset: 0x00034FA4
		public override void FilterCubemapMIS(CommandBuffer cmd, Texture source, RenderTexture target, RenderTexture conditionalCdf, RenderTexture marginalRowCdf)
		{
			this.m_BuildProbabilityTablesCS.SetTexture(this.m_ConditionalDensitiesKernel, "envMap", source);
			this.m_BuildProbabilityTablesCS.SetTexture(this.m_ConditionalDensitiesKernel, "conditionalDensities", conditionalCdf);
			this.m_BuildProbabilityTablesCS.SetTexture(this.m_ConditionalDensitiesKernel, "marginalRowDensities", marginalRowCdf);
			this.m_BuildProbabilityTablesCS.SetTexture(this.m_MarginalRowDensitiesKernel, "marginalRowDensities", marginalRowCdf);
			int height = conditionalCdf.height;
			cmd.DispatchCompute(this.m_BuildProbabilityTablesCS, this.m_ConditionalDensitiesKernel, height, 1, 1);
			cmd.DispatchCompute(this.m_BuildProbabilityTablesCS, this.m_MarginalRowDensitiesKernel, 1, 1, 1);
			this.m_convolveMaterial.EnableKeyword("USE_MIS");
			this.m_convolveMaterial.SetTexture("_ConditionalDensities", conditionalCdf);
			this.m_convolveMaterial.SetTexture("_MarginalRowDensities", marginalRowCdf);
			this.FilterCubemapCommon(cmd, source, target, this.m_faceWorldToViewMatrixMatrices);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00036E85 File Offset: 0x00035085
		public override void FilterCubemap(CommandBuffer cmd, Texture source, RenderTexture target)
		{
			this.FilterCubemapCommon(cmd, source, target, this.m_faceWorldToViewMatrixMatrices);
		}

		// Token: 0x04000720 RID: 1824
		private RenderTexture m_GgxIblSampleData;

		// Token: 0x04000721 RID: 1825
		private int m_GgxIblMaxSampleCount = (TextureCache.isMobileBuildTarget ? 34 : 89);

		// Token: 0x04000722 RID: 1826
		private const int k_GgxIblMipCountMinusOne = 6;

		// Token: 0x04000723 RID: 1827
		private ComputeShader m_ComputeGgxIblSampleDataCS;

		// Token: 0x04000724 RID: 1828
		private int m_ComputeGgxIblSampleDataKernel = -1;

		// Token: 0x04000725 RID: 1829
		private ComputeShader m_BuildProbabilityTablesCS;

		// Token: 0x04000726 RID: 1830
		private int m_ConditionalDensitiesKernel = -1;

		// Token: 0x04000727 RID: 1831
		private int m_MarginalRowDensitiesKernel = -1;
	}
}
