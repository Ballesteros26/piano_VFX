using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000B8 RID: 184
	internal class IBLFilterCharlie : IBLFilterBSDF
	{
		// Token: 0x060006D9 RID: 1753 RVA: 0x000363ED File Offset: 0x000345ED
		public IBLFilterCharlie(RenderPipelineResources renderPipelineResources, MipGenerator mipGenerator)
		{
			this.m_RenderPipelineResources = renderPipelineResources;
			this.m_MipGenerator = mipGenerator;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00036403 File Offset: 0x00034603
		public override bool IsInitialized()
		{
			return this.m_convolveMaterial != null;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00036414 File Offset: 0x00034614
		public override void Initialize(CommandBuffer cmd)
		{
			if (!this.m_convolveMaterial)
			{
				this.m_convolveMaterial = CoreUtils.CreateEngineMaterial(this.m_RenderPipelineResources.shaders.charlieConvolvePS);
			}
			for (int i = 0; i < 6; i++)
			{
				Matrix4x4 matrix4x = Matrix4x4.LookAt(Vector3.zero, CoreUtils.lookAtList[i], CoreUtils.upVectorList[i]);
				this.m_faceWorldToViewMatrixMatrices[i] = matrix4x * Matrix4x4.Scale(new Vector3(1f, 1f, -1f));
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x000364A1 File Offset: 0x000346A1
		public override void Cleanup()
		{
			CoreUtils.Destroy(this.m_convolveMaterial);
			this.m_convolveMaterial = null;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000364B8 File Offset: 0x000346B8
		private void FilterCubemapCommon(CommandBuffer cmd, Texture source, RenderTexture target, Matrix4x4[] worldToViewMatrices)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.FilterCubemapCharlie)))
			{
				if (1 + (int)Mathf.Log((float)source.width, 2f) < 7)
				{
					Debug.LogWarning("RenderCubemapCharlieConvolution: Cubemap size is too small for Charlie convolution, needs at least " + 7 + " mip levels");
				}
				else
				{
					float num = 6f * (float)source.width * (float)source.width / 12.566371f;
					for (int i = 0; i < 6; i++)
					{
						cmd.CopyTexture(source, i, 0, target, i, 0);
					}
					MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
					materialPropertyBlock.SetTexture("_MainTex", source);
					materialPropertyBlock.SetFloat("_InvOmegaP", num);
					for (int j = 0; j < 7; j++)
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

		// Token: 0x060006DE RID: 1758 RVA: 0x00036664 File Offset: 0x00034864
		public override void FilterCubemap(CommandBuffer cmd, Texture source, RenderTexture target)
		{
			this.FilterCubemapCommon(cmd, source, target, this.m_faceWorldToViewMatrixMatrices);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00002646 File Offset: 0x00000846
		public override void FilterCubemapMIS(CommandBuffer cmd, Texture source, RenderTexture target, RenderTexture conditionalCdf, RenderTexture marginalRowCdf)
		{
		}
	}
}
