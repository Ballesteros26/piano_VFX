using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200002B RID: 43
	internal class DebugLightVolumes
	{
		// Token: 0x06000166 RID: 358 RVA: 0x00008F2C File Offset: 0x0000712C
		public void InitData(RenderPipelineResources renderPipelineResources)
		{
			this.m_DebugLightVolumeMaterial = CoreUtils.CreateEngineMaterial(renderPipelineResources.shaders.debugLightVolumePS);
			this.m_DebugLightVolumeCompute = renderPipelineResources.shaders.debugLightVolumeCS;
			this.m_DebugLightVolumeGradientKernel = this.m_DebugLightVolumeCompute.FindKernel("LightVolumeGradient");
			this.m_DebugLightVolumeColorsKernel = this.m_DebugLightVolumeCompute.FindKernel("LightVolumeColors");
			this.m_ColorGradientTexture = renderPipelineResources.textures.colorGradient;
			this.m_Blit = CoreUtils.CreateEngineMaterial(renderPipelineResources.shaders.blitPS);
			this.m_LightCountBuffer = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R32_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, false, false, true, false, 1, 0f, false, false, false, RenderTextureMemoryless.None, "LightVolumeCount");
			this.m_ColorAccumulationBuffer = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, false, false, true, false, 1, 0f, false, false, false, RenderTextureMemoryless.None, "LightVolumeColorAccumulation");
			this.m_DebugLightVolumesTexture = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R16G16B16A16_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, false, RenderTextureMemoryless.None, "LightVolumeDebugLightVolumesTexture");
			this.m_DepthBuffer = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R8_UNorm, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, false, false, true, false, 1, 0f, false, false, false, RenderTextureMemoryless.None, "LightVolumeDepth");
			this.m_RTIDs[0] = this.m_LightCountBuffer;
			this.m_RTIDs[1] = this.m_ColorAccumulationBuffer;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x000090A8 File Offset: 0x000072A8
		public void ReleaseData()
		{
			CoreUtils.Destroy(this.m_Blit);
			RTHandles.Release(this.m_DepthBuffer);
			RTHandles.Release(this.m_DebugLightVolumesTexture);
			RTHandles.Release(this.m_ColorAccumulationBuffer);
			RTHandles.Release(this.m_LightCountBuffer);
			CoreUtils.Destroy(this.m_DebugLightVolumeMaterial);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000090F8 File Offset: 0x000072F8
		public DebugLightVolumes.RenderLightVolumesParameters PrepareLightVolumeParameters(HDCamera hdCamera, LightingDebugSettings lightDebugSettings, CullingResults cullResults)
		{
			return new DebugLightVolumes.RenderLightVolumesParameters
			{
				hdCamera = hdCamera,
				cullResults = cullResults,
				debugLightVolumeMaterial = this.m_DebugLightVolumeMaterial,
				debugLightVolumeCS = this.m_DebugLightVolumeCompute,
				debugLightVolumeKernel = ((lightDebugSettings.lightVolumeDebugByCategory == LightVolumeDebug.ColorAndEdge) ? this.m_DebugLightVolumeColorsKernel : this.m_DebugLightVolumeGradientKernel),
				maxDebugLightCount = (int)lightDebugSettings.maxDebugLightCount,
				colorGradientTexture = this.m_ColorGradientTexture
			};
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00009170 File Offset: 0x00007370
		public static void RenderLightVolumes(CommandBuffer cmd, in DebugLightVolumes.RenderLightVolumesParameters parameters, RenderTargetIdentifier[] accumulationMRT, RTHandle lightCountBuffer, RTHandle colorAccumulationBuffer, RTHandle debugLightVolumesTexture, RTHandle depthBuffer, RTHandle destination, MaterialPropertyBlock mpb)
		{
			CoreUtils.SetRenderTarget(cmd, accumulationMRT, depthBuffer);
			CullingResults cullingResults = parameters.cullResults;
			int length = cullingResults.visibleLights.Length;
			for (int i = 0; i < length; i++)
			{
				cullingResults = parameters.cullResults;
				Light light = cullingResults.visibleLights[i].light;
				if (!(light == null))
				{
					HDAdditionalLightData component = light.GetComponent<HDAdditionalLightData>();
					if (!(component == null))
					{
						Matrix4x4 matrix4x = Matrix4x4.Translate(light.transform.position);
						switch (component.ComputeLightType(light))
						{
						case HDLightType.Spot:
							switch (component.spotLightShape)
							{
							case SpotLightShape.Cone:
							{
								float num = Mathf.Tan(light.spotAngle * 3.1415927f / 360f) * light.range;
								mpb.SetColor(DebugLightVolumes._ColorShaderID, new Color(1f, 0.5f, 0f, 1f));
								mpb.SetVector(DebugLightVolumes._RangeShaderID, new Vector3(num, num, light.range));
								mpb.SetVector(DebugLightVolumes._OffsetShaderID, new Vector3(0f, 0f, 0f));
								cmd.DrawMesh(DebugShapes.instance.RequestConeMesh(), light.gameObject.transform.localToWorldMatrix, parameters.debugLightVolumeMaterial, 0, 0, mpb);
								break;
							}
							case SpotLightShape.Pyramid:
							{
								float num2 = Mathf.Tan(light.spotAngle * 3.1415927f / 360f) * light.range;
								mpb.SetColor(DebugLightVolumes._ColorShaderID, new Color(1f, 0.5f, 0f, 1f));
								mpb.SetVector(DebugLightVolumes._RangeShaderID, new Vector3(component.aspectRatio * num2 * 2f, num2 * 2f, light.range));
								mpb.SetVector(DebugLightVolumes._OffsetShaderID, new Vector3(0f, 0f, 0f));
								cmd.DrawMesh(DebugShapes.instance.RequestPyramidMesh(), light.gameObject.transform.localToWorldMatrix, parameters.debugLightVolumeMaterial, 0, 0, mpb);
								break;
							}
							case SpotLightShape.Box:
								mpb.SetColor(DebugLightVolumes._ColorShaderID, new Color(1f, 0.5f, 0f, 1f));
								mpb.SetVector(DebugLightVolumes._RangeShaderID, new Vector3(component.shapeWidth, component.shapeHeight, light.range));
								mpb.SetVector(DebugLightVolumes._OffsetShaderID, new Vector3(0f, 0f, light.range / 2f));
								cmd.DrawMesh(DebugShapes.instance.RequestBoxMesh(), light.gameObject.transform.localToWorldMatrix, parameters.debugLightVolumeMaterial, 0, 0, mpb);
								break;
							}
							break;
						case HDLightType.Point:
							mpb.SetColor(DebugLightVolumes._ColorShaderID, new Color(0f, 0.5f, 0f, 1f));
							mpb.SetVector(DebugLightVolumes._OffsetShaderID, new Vector3(0f, 0f, 0f));
							cmd.DrawMesh(DebugShapes.instance.RequestSphereMesh(), matrix4x, parameters.debugLightVolumeMaterial, 0, 0, mpb);
							break;
						case HDLightType.Area:
						{
							AreaLightShape areaLightShape = component.areaLightShape;
							if (areaLightShape != AreaLightShape.Rectangle)
							{
								if (areaLightShape == AreaLightShape.Tube)
								{
									mpb.SetColor(DebugLightVolumes._ColorShaderID, new Color(1f, 0f, 0.5f, 1f));
									mpb.SetVector(DebugLightVolumes._OffsetShaderID, new Vector3(0f, 0f, 0f));
									cmd.DrawMesh(DebugShapes.instance.RequestSphereMesh(), matrix4x, parameters.debugLightVolumeMaterial, 0, 0, mpb);
								}
							}
							else
							{
								mpb.SetColor(DebugLightVolumes._ColorShaderID, new Color(0f, 1f, 1f, 1f));
								mpb.SetVector(DebugLightVolumes._OffsetShaderID, new Vector3(0f, 0f, 0f));
								cmd.DrawMesh(DebugShapes.instance.RequestSphereMesh(), matrix4x, parameters.debugLightVolumeMaterial, 0, 0, mpb);
							}
							break;
						}
						}
					}
				}
			}
			cullingResults = parameters.cullResults;
			int length2 = cullingResults.visibleReflectionProbes.Length;
			for (int j = 0; j < length2; j++)
			{
				cullingResults = parameters.cullResults;
				ReflectionProbe reflectionProbe = cullingResults.visibleReflectionProbes[j].reflectionProbe;
				HDAdditionalReflectionData component2 = reflectionProbe.GetComponent<HDAdditionalReflectionData>();
				if (component2)
				{
					MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
					Mesh mesh;
					if (component2.influenceVolume.shape == InfluenceShape.Sphere)
					{
						materialPropertyBlock.SetVector(DebugLightVolumes._RangeShaderID, new Vector3(component2.influenceVolume.sphereRadius, component2.influenceVolume.sphereRadius, component2.influenceVolume.sphereRadius));
						mesh = DebugShapes.instance.RequestSphereMesh();
					}
					else
					{
						materialPropertyBlock.SetVector(DebugLightVolumes._RangeShaderID, new Vector3(component2.influenceVolume.boxSize.x, component2.influenceVolume.boxSize.y, component2.influenceVolume.boxSize.z));
						mesh = DebugShapes.instance.RequestBoxMesh();
					}
					materialPropertyBlock.SetColor(DebugLightVolumes._ColorShaderID, new Color(1f, 1f, 0f, 1f));
					materialPropertyBlock.SetVector(DebugLightVolumes._OffsetShaderID, new Vector3(0f, 0f, 0f));
					Matrix4x4 matrix4x2 = Matrix4x4.Translate(reflectionProbe.transform.position);
					cmd.DrawMesh(mesh, matrix4x2, parameters.debugLightVolumeMaterial, 0, 0, materialPropertyBlock);
				}
			}
			cmd.SetComputeTextureParam(parameters.debugLightVolumeCS, parameters.debugLightVolumeKernel, DebugLightVolumes._DebugLightCountBufferShaderID, lightCountBuffer);
			cmd.SetComputeTextureParam(parameters.debugLightVolumeCS, parameters.debugLightVolumeKernel, DebugLightVolumes._DebugColorAccumulationBufferShaderID, colorAccumulationBuffer);
			cmd.SetComputeTextureParam(parameters.debugLightVolumeCS, parameters.debugLightVolumeKernel, DebugLightVolumes._DebugLightVolumesTextureShaderID, debugLightVolumesTexture);
			cmd.SetComputeTextureParam(parameters.debugLightVolumeCS, parameters.debugLightVolumeKernel, DebugLightVolumes._ColorGradientTextureShaderID, parameters.colorGradientTexture);
			cmd.SetComputeIntParam(parameters.debugLightVolumeCS, DebugLightVolumes._MaxDebugLightCountShaderID, parameters.maxDebugLightCount);
			int actualWidth = parameters.hdCamera.actualWidth;
			int actualHeight = parameters.hdCamera.actualHeight;
			int num3 = 8;
			int num4 = (actualWidth + (num3 - 1)) / num3;
			int num5 = (actualHeight + (num3 - 1)) / num3;
			cmd.DispatchCompute(parameters.debugLightVolumeCS, parameters.debugLightVolumeKernel, num4, num5, parameters.hdCamera.viewCount);
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			mpb.SetTexture(HDShaderIDs._BlitTexture, debugLightVolumesTexture);
			cmd.DrawProcedural(Matrix4x4.identity, parameters.debugLightVolumeMaterial, 1, MeshTopology.Triangles, 3, 1, mpb);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00009888 File Offset: 0x00007A88
		public void RenderLightVolumes(CommandBuffer cmd, HDCamera hdCamera, CullingResults cullResults, LightingDebugSettings lightDebugSettings, RTHandle finalRT)
		{
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DisplayLightVolume)))
			{
				CoreUtils.SetRenderTarget(cmd, this.m_ColorAccumulationBuffer, ClearFlag.Color, Color.black, 0, CubemapFace.Unknown, -1);
				CoreUtils.SetRenderTarget(cmd, this.m_LightCountBuffer, ClearFlag.Color, Color.black, 0, CubemapFace.Unknown, -1);
				CoreUtils.SetRenderTarget(cmd, this.m_DebugLightVolumesTexture, ClearFlag.Color, Color.black, 0, CubemapFace.Unknown, -1);
				DebugLightVolumes.RenderLightVolumesParameters renderLightVolumesParameters = this.PrepareLightVolumeParameters(hdCamera, lightDebugSettings, cullResults);
				DebugLightVolumes.RenderLightVolumes(cmd, in renderLightVolumesParameters, this.m_RTIDs, this.m_LightCountBuffer, this.m_ColorAccumulationBuffer, this.m_DebugLightVolumesTexture, this.m_DepthBuffer, finalRT, this.m_MaterialProperty);
			}
		}

		// Token: 0x040000DB RID: 219
		private RTHandle m_LightCountBuffer;

		// Token: 0x040000DC RID: 220
		private RTHandle m_ColorAccumulationBuffer;

		// Token: 0x040000DD RID: 221
		private RTHandle m_DebugLightVolumesTexture;

		// Token: 0x040000DE RID: 222
		private RTHandle m_DepthBuffer;

		// Token: 0x040000DF RID: 223
		private Material m_Blit;

		// Token: 0x040000E0 RID: 224
		private Material m_DebugLightVolumeMaterial;

		// Token: 0x040000E1 RID: 225
		private ComputeShader m_DebugLightVolumeCompute;

		// Token: 0x040000E2 RID: 226
		private int m_DebugLightVolumeGradientKernel;

		// Token: 0x040000E3 RID: 227
		private int m_DebugLightVolumeColorsKernel;

		// Token: 0x040000E4 RID: 228
		private Texture2D m_ColorGradientTexture;

		// Token: 0x040000E5 RID: 229
		public static readonly int _ColorShaderID = Shader.PropertyToID("_Color");

		// Token: 0x040000E6 RID: 230
		public static readonly int _OffsetShaderID = Shader.PropertyToID("_Offset");

		// Token: 0x040000E7 RID: 231
		public static readonly int _RangeShaderID = Shader.PropertyToID("_Range");

		// Token: 0x040000E8 RID: 232
		public static readonly int _DebugLightCountBufferShaderID = Shader.PropertyToID("_DebugLightCountBuffer");

		// Token: 0x040000E9 RID: 233
		public static readonly int _DebugColorAccumulationBufferShaderID = Shader.PropertyToID("_DebugColorAccumulationBuffer");

		// Token: 0x040000EA RID: 234
		public static readonly int _DebugLightVolumesTextureShaderID = Shader.PropertyToID("_DebugLightVolumesTexture");

		// Token: 0x040000EB RID: 235
		public static readonly int _ColorGradientTextureShaderID = Shader.PropertyToID("_ColorGradientTexture");

		// Token: 0x040000EC RID: 236
		public static readonly int _MaxDebugLightCountShaderID = Shader.PropertyToID("_MaxDebugLightCount");

		// Token: 0x040000ED RID: 237
		private RenderTargetIdentifier[] m_RTIDs = new RenderTargetIdentifier[2];

		// Token: 0x040000EE RID: 238
		private MaterialPropertyBlock m_MaterialProperty = new MaterialPropertyBlock();

		// Token: 0x02000196 RID: 406
		public struct RenderLightVolumesParameters
		{
			// Token: 0x04001104 RID: 4356
			public HDCamera hdCamera;

			// Token: 0x04001105 RID: 4357
			public CullingResults cullResults;

			// Token: 0x04001106 RID: 4358
			public Material debugLightVolumeMaterial;

			// Token: 0x04001107 RID: 4359
			public ComputeShader debugLightVolumeCS;

			// Token: 0x04001108 RID: 4360
			public int debugLightVolumeKernel;

			// Token: 0x04001109 RID: 4361
			public int maxDebugLightCount;

			// Token: 0x0400110A RID: 4362
			public Texture2D colorGradientTexture;
		}
	}
}
