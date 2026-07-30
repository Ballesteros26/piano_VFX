using System;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Experimental.Rendering.RenderGraphModule;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000087 RID: 135
	internal class AmbientOcclusionSystem
	{
		// Token: 0x06000570 RID: 1392 RVA: 0x0002D98C File Offset: 0x0002BB8C
		private RenderGraphMutableResource CreateAmbientOcclusionTexture(RenderGraph renderGraph)
		{
			return renderGraph.CreateTexture(new TextureDesc(Vector2.one, true, true)
			{
				enableRandomWrite = true,
				colorFormat = GraphicsFormat.R8_UNorm,
				name = "Ambient Occlusion"
			}, HDShaderIDs._AmbientOcclusionTexture);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0002D9D0 File Offset: 0x0002BBD0
		public RenderGraphResource Render(RenderGraph renderGraph, HDCamera hdCamera, RenderGraphResource depthPyramid, RenderGraphResource motionVectors, int frameCount)
		{
			AmbientOcclusion component = hdCamera.volumeStack.GetComponent<AmbientOcclusion>();
			RenderGraphResource renderGraphResource2;
			if (this.IsActive(hdCamera, component))
			{
				this.EnsureRTSize(component, hdCamera);
				AmbientOcclusionSystem.RenderAOParameters renderAOParameters = this.PrepareRenderAOParameters(hdCamera, renderGraph.rtHandleProperties, frameCount);
				RenderGraphMutableResource renderGraphMutableResource = renderGraph.ImportTexture(hdCamera.GetCurrentFrameRT(7), 0);
				RenderGraphMutableResource renderGraphMutableResource2 = renderGraph.ImportTexture(hdCamera.GetPreviousFrameRT(7), 0);
				RenderGraphResource renderGraphResource = this.RenderAO(renderGraph, in renderAOParameters, depthPyramid);
				renderGraphResource2 = this.DenoiseAO(renderGraph, in renderAOParameters, motionVectors, renderGraphResource, renderGraphMutableResource, renderGraphMutableResource2);
			}
			else
			{
				renderGraphResource2 = renderGraph.ImportTexture(TextureXR.GetBlackTexture(), HDShaderIDs._AmbientOcclusionTexture);
			}
			return renderGraphResource2;
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x0002DA60 File Offset: 0x0002BC60
		private RenderGraphResource RenderAO(RenderGraph renderGraph, in AmbientOcclusionSystem.RenderAOParameters parameters, RenderGraphResource depthPyramid)
		{
			AmbientOcclusionSystem.RenderAOPassData renderAOPassData;
			RenderGraphResource renderGraphResource;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<AmbientOcclusionSystem.RenderAOPassData>("GTAO Horizon search and integration", out renderAOPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.HorizonSSAO)))
			{
				renderGraphBuilder.EnableAsyncCompute(parameters.runAsync);
				float num = (parameters.fullResolution ? 1f : 0.5f);
				renderAOPassData.parameters = parameters;
				AmbientOcclusionSystem.RenderAOPassData renderAOPassData2 = renderAOPassData;
				RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one * num, true, true)
				{
					colorFormat = GraphicsFormat.R32_UInt,
					enableRandomWrite = true,
					name = "AO Packed data"
				}, 0);
				renderAOPassData2.packedData = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				renderAOPassData.depthPyramid = renderGraphBuilder.ReadTexture(in depthPyramid);
				renderGraphBuilder.SetRenderFunc<AmbientOcclusionSystem.RenderAOPassData>(delegate(AmbientOcclusionSystem.RenderAOPassData data, RenderGraphContext ctx)
				{
					RenderGraphResourceRegistry resources = ctx.resources;
					RenderGraphResource renderGraphResource2 = data.packedData;
					AmbientOcclusionSystem.RenderAO(in data.parameters, resources.GetTexture(in renderGraphResource2), this.m_Resources, ctx.cmd);
				});
				renderGraphResource = renderAOPassData.packedData;
			}
			return renderGraphResource;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x0002DB4C File Offset: 0x0002BD4C
		private RenderGraphResource DenoiseAO(RenderGraph renderGraph, in AmbientOcclusionSystem.RenderAOParameters parameters, RenderGraphResource motionVectors, RenderGraphResource aoPackedData, RenderGraphMutableResource currentHistory, RenderGraphMutableResource outputHistory)
		{
			AmbientOcclusionSystem.DenoiseAOPassData denoiseAOPassData;
			RenderGraphResource renderGraphResource2;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<AmbientOcclusionSystem.DenoiseAOPassData>("Denoise GTAO", out denoiseAOPassData, null))
			{
				renderGraphBuilder.EnableAsyncCompute(parameters.runAsync);
				float num = (parameters.fullResolution ? 1f : 0.5f);
				denoiseAOPassData.parameters = parameters;
				denoiseAOPassData.packedData = renderGraphBuilder.ReadTexture(in aoPackedData);
				denoiseAOPassData.motionVectors = renderGraphBuilder.ReadTexture(in motionVectors);
				AmbientOcclusionSystem.DenoiseAOPassData denoiseAOPassData2 = denoiseAOPassData;
				RenderGraphMutableResource renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one * num, true, true)
				{
					colorFormat = GraphicsFormat.R32_UInt,
					enableRandomWrite = true,
					name = "AO Packed blurred data"
				}, 0);
				denoiseAOPassData2.packedDataBlurred = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				AmbientOcclusionSystem.DenoiseAOPassData denoiseAOPassData3 = denoiseAOPassData;
				RenderGraphResource renderGraphResource = currentHistory;
				denoiseAOPassData3.currentHistory = renderGraphBuilder.ReadTexture(in renderGraphResource);
				denoiseAOPassData.outputHistory = renderGraphBuilder.WriteTexture(in outputHistory);
				bool fullResolution = parameters.fullResolution;
				if (parameters.fullResolution)
				{
					AmbientOcclusionSystem.DenoiseAOPassData denoiseAOPassData4 = denoiseAOPassData;
					renderGraphMutableResource = this.CreateAmbientOcclusionTexture(renderGraph);
					denoiseAOPassData4.denoiseOutput = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				}
				else
				{
					AmbientOcclusionSystem.DenoiseAOPassData denoiseAOPassData5 = denoiseAOPassData;
					renderGraphMutableResource = renderGraph.CreateTexture(new TextureDesc(Vector2.one * 0.5f, true, true)
					{
						enableRandomWrite = true,
						colorFormat = GraphicsFormat.R32_UInt,
						name = "Final Half Res AO Packed"
					}, 0);
					denoiseAOPassData5.denoiseOutput = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				}
				renderGraphResource2 = denoiseAOPassData.denoiseOutput;
				renderGraphBuilder.SetRenderFunc<AmbientOcclusionSystem.DenoiseAOPassData>(delegate(AmbientOcclusionSystem.DenoiseAOPassData data, RenderGraphContext ctx)
				{
					RenderGraphResourceRegistry resources = ctx.resources;
					RTHandle texture = resources.GetTexture(in data.packedData);
					RenderGraphResourceRegistry renderGraphResourceRegistry = resources;
					RenderGraphResource renderGraphResource3 = data.packedDataBlurred;
					RTHandle texture2 = renderGraphResourceRegistry.GetTexture(in renderGraphResource3);
					RTHandle texture3 = resources.GetTexture(in data.currentHistory);
					RenderGraphResourceRegistry renderGraphResourceRegistry2 = resources;
					RenderGraphResource renderGraphResource4 = data.outputHistory;
					RTHandle texture4 = renderGraphResourceRegistry2.GetTexture(in renderGraphResource4);
					RenderGraphResourceRegistry renderGraphResourceRegistry3 = resources;
					RenderGraphResource renderGraphResource5 = data.denoiseOutput;
					AmbientOcclusionSystem.DenoiseAO(in data.parameters, texture, texture2, texture3, texture4, renderGraphResourceRegistry3.GetTexture(in renderGraphResource5), ctx.cmd);
				});
				if (parameters.fullResolution)
				{
					return denoiseAOPassData.denoiseOutput;
				}
			}
			return this.UpsampleAO(renderGraph, in parameters, renderGraphResource2);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x0002DD20 File Offset: 0x0002BF20
		private RenderGraphResource UpsampleAO(RenderGraph renderGraph, in AmbientOcclusionSystem.RenderAOParameters parameters, RenderGraphResource input)
		{
			AmbientOcclusionSystem.UpsampleAOPassData upsampleAOPassData;
			RenderGraphResource renderGraphResource;
			using (RenderGraphBuilder renderGraphBuilder = renderGraph.AddRenderPass<AmbientOcclusionSystem.UpsampleAOPassData>("Upsample GTAO", out upsampleAOPassData, ProfilingSampler.Get<HDProfileId>(HDProfileId.UpSampleSSAO)))
			{
				renderGraphBuilder.EnableAsyncCompute(parameters.runAsync);
				upsampleAOPassData.parameters = parameters;
				upsampleAOPassData.input = renderGraphBuilder.ReadTexture(in input);
				AmbientOcclusionSystem.UpsampleAOPassData upsampleAOPassData2 = upsampleAOPassData;
				RenderGraphMutableResource renderGraphMutableResource = this.CreateAmbientOcclusionTexture(renderGraph);
				upsampleAOPassData2.output = renderGraphBuilder.WriteTexture(in renderGraphMutableResource);
				renderGraphBuilder.SetRenderFunc<AmbientOcclusionSystem.UpsampleAOPassData>(delegate(AmbientOcclusionSystem.UpsampleAOPassData data, RenderGraphContext ctx)
				{
					RTHandle texture = ctx.resources.GetTexture(in data.input);
					RenderGraphResourceRegistry resources = ctx.resources;
					RenderGraphResource renderGraphResource2 = data.output;
					AmbientOcclusionSystem.UpsampleAO(in data.parameters, texture, resources.GetTexture(in renderGraphResource2), ctx.cmd);
				});
				renderGraphResource = upsampleAOPassData.output;
			}
			return renderGraphResource;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x0002DDD4 File Offset: 0x0002BFD4
		private void ReleaseRT()
		{
			RTHandles.Release(this.m_AmbientOcclusionTex);
			RTHandles.Release(this.m_PackedDataTex);
			RTHandles.Release(this.m_PackedDataBlurred);
			RTHandles.Release(this.m_FinalHalfRes);
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0002DE04 File Offset: 0x0002C004
		private void AllocRT(float scaleFactor)
		{
			this.m_AmbientOcclusionTex = RTHandles.Alloc(Vector2.one, TextureXR.slices, DepthBits.None, GraphicsFormat.R8_UNorm, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "Ambient Occlusion");
			this.m_PackedDataTex = RTHandles.Alloc(Vector2.one * scaleFactor, TextureXR.slices, DepthBits.None, GraphicsFormat.R32_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "AO Packed data");
			this.m_PackedDataBlurred = RTHandles.Alloc(Vector2.one * scaleFactor, TextureXR.slices, DepthBits.None, GraphicsFormat.R32_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "AO Packed blurred data");
			this.m_FinalHalfRes = RTHandles.Alloc(Vector2.one * 0.5f, TextureXR.slices, DepthBits.None, GraphicsFormat.R32_SFloat, FilterMode.Point, TextureWrapMode.Repeat, TextureXR.dimension, true, false, true, false, 1, 0f, false, false, true, RenderTextureMemoryless.None, "Final Half Res AO Packed");
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x0002DEF0 File Offset: 0x0002C0F0
		private void EnsureRTSize(AmbientOcclusion settings, HDCamera hdCamera)
		{
			float num = (this.m_RunningFullRes ? 1f : 0.5f);
			if (settings.fullResolution != this.m_RunningFullRes)
			{
				this.ReleaseRT();
				this.m_RunningFullRes = settings.fullResolution;
				num = (this.m_RunningFullRes ? 1f : 0.5f);
				this.AllocRT(num);
			}
			hdCamera.AllocateAmbientOcclusionHistoryBuffer(num);
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x0002DF55 File Offset: 0x0002C155
		internal AmbientOcclusionSystem(HDRenderPipelineAsset hdAsset, RenderPipelineResources defaultResources)
		{
			this.m_Settings = hdAsset.currentPlatformRenderPipelineSettings;
			this.m_Resources = defaultResources;
			if (!hdAsset.currentPlatformRenderPipelineSettings.supportSSAO)
			{
				return;
			}
			this.AllocRT(0.5f);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x0002DF94 File Offset: 0x0002C194
		internal void Cleanup()
		{
			if (HDRenderPipeline.GatherRayTracingSupport(this.m_Settings))
			{
				this.m_RaytracingAmbientOcclusion.Release();
			}
			this.ReleaseRT();
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x0002DFB4 File Offset: 0x0002C1B4
		internal void InitRaytracing(HDRenderPipeline renderPipeline)
		{
			this.m_RaytracingAmbientOcclusion.Init(renderPipeline);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x0002DFC4 File Offset: 0x0002C1C4
		internal bool IsActive(HDCamera camera, AmbientOcclusion settings)
		{
			return camera.frameSettings.IsEnabled(FrameSettingsField.SSAO) && settings.intensity.value > 0f;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x0002DFF8 File Offset: 0x0002C1F8
		internal void Render(CommandBuffer cmd, HDCamera camera, ScriptableRenderContext renderContext, int frameCount)
		{
			AmbientOcclusion component = camera.volumeStack.GetComponent<AmbientOcclusion>();
			if (!this.IsActive(camera, component))
			{
				this.PostDispatchWork(cmd, camera);
				return;
			}
			if (camera.frameSettings.IsEnabled(FrameSettingsField.RayTracing) && component.rayTracing.value)
			{
				this.m_RaytracingAmbientOcclusion.RenderAO(camera, cmd, this.m_AmbientOcclusionTex, renderContext, frameCount);
				return;
			}
			this.Dispatch(cmd, camera, frameCount);
			this.PostDispatchWork(cmd, camera);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x0002E06C File Offset: 0x0002C26C
		private AmbientOcclusionSystem.RenderAOParameters PrepareRenderAOParameters(HDCamera camera, RTHandleProperties rtHandleProperties, int frameCount)
		{
			AmbientOcclusionSystem.RenderAOParameters renderAOParameters = default(AmbientOcclusionSystem.RenderAOParameters);
			AmbientOcclusion component = camera.volumeStack.GetComponent<AmbientOcclusion>();
			renderAOParameters.fullResolution = component.fullResolution;
			if (renderAOParameters.fullResolution)
			{
				renderAOParameters.runningRes = new Vector2((float)camera.actualWidth, (float)camera.actualHeight);
				renderAOParameters.aoBufferInfo = new Vector4((float)camera.actualWidth, (float)camera.actualHeight, 1f / (float)camera.actualWidth, 1f / (float)camera.actualHeight);
			}
			else
			{
				renderAOParameters.runningRes = new Vector2((float)camera.actualWidth, (float)camera.actualHeight) * 0.5f;
				renderAOParameters.aoBufferInfo = new Vector4((float)camera.actualWidth * 0.5f, (float)camera.actualHeight * 0.5f, 2f / (float)camera.actualWidth, 2f / (float)camera.actualHeight);
			}
			float num = -camera.mainViewConstants.projMatrix[1, 1];
			float num2 = renderAOParameters.runningRes.y / renderAOParameters.runningRes.x;
			renderAOParameters.aoParams0 = new Vector4(renderAOParameters.fullResolution ? 0f : 1f, renderAOParameters.runningRes.y * num * 0.25f, component.radius.value, (float)component.stepCount);
			renderAOParameters.aoParams1 = new Vector4(component.intensity.value, 1f / (component.radius.value * component.radius.value), (float)(frameCount / 6 % 4), (float)(frameCount % 6));
			renderAOParameters.toViewSpaceProj = new Vector4(2f / (num * num2 * renderAOParameters.runningRes.x), 2f / (num * renderAOParameters.runningRes.y), 1f / (num * num2), 1f / num);
			float num3 = renderAOParameters.runningRes.x * renderAOParameters.runningRes.y / 518400f;
			float num4 = Mathf.Max(16f, (float)component.maximumRadiusInPixels * Mathf.Sqrt(num3));
			renderAOParameters.aoParams2 = new Vector4(this.m_HistoryInfo.x, this.m_HistoryInfo.y, 1f / ((float)component.stepCount + 1f), num4);
			float num5 = (this.m_RunningFullRes ? 1f : 0.5f);
			float num6 = 1f - component.blurSharpness.value;
			float num7 = 0.25f;
			float num8 = -2.5f;
			num6 = num8 + num6 * (num7 - num8);
			float num9 = 1f - Mathf.Pow(10f, num6) * num5;
			num9 *= num9;
			float num10 = Mathf.Pow(10f, -7f);
			float num11 = 1f / (Mathf.Pow(10f, 0f) + num10);
			renderAOParameters.aoParams3 = new Vector4(num9, num10, num11, num5);
			float num12 = 1f - component.ghostingReduction.value;
			num12 = 0.25f + num12 * 4.75f;
			renderAOParameters.aoParams4 = new Vector4((float)component.directionCount, num12, 0.25f, 0f);
			HDUtils.PackedMipChainInfo depthBufferMipChainInfo = (RenderPipelineManager.currentPipeline as HDRenderPipeline).sharedRTManager.GetDepthBufferMipChainInfo();
			renderAOParameters.firstAndSecondMipOffsets = new Vector4((float)depthBufferMipChainInfo.mipLevelOffsets[1].x, (float)depthBufferMipChainInfo.mipLevelOffsets[1].y, (float)depthBufferMipChainInfo.mipLevelOffsets[2].x, (float)depthBufferMipChainInfo.mipLevelOffsets[2].y);
			renderAOParameters.bilateralUpsample = component.bilateralUpsample;
			renderAOParameters.gtaoCS = this.m_Resources.shaders.GTAOCS;
			renderAOParameters.temporalAccumulation = component.temporalAccumulation.value;
			if (renderAOParameters.temporalAccumulation)
			{
				if (renderAOParameters.fullResolution)
				{
					renderAOParameters.gtaoKernel = renderAOParameters.gtaoCS.FindKernel("GTAOMain_FullRes_Temporal");
				}
				else
				{
					renderAOParameters.gtaoKernel = renderAOParameters.gtaoCS.FindKernel("GTAOMain_HalfRes_Temporal");
				}
			}
			else if (renderAOParameters.fullResolution)
			{
				renderAOParameters.gtaoKernel = renderAOParameters.gtaoCS.FindKernel("GTAOMain_FullRes");
			}
			else
			{
				renderAOParameters.gtaoKernel = renderAOParameters.gtaoCS.FindKernel("GTAOMain_HalfRes");
			}
			renderAOParameters.upsampleAndBlurAOCS = this.m_Resources.shaders.GTAOBlurAndUpsample;
			renderAOParameters.denoiseAOCS = this.m_Resources.shaders.GTAODenoiseCS;
			renderAOParameters.denoiseKernelSpatial = renderAOParameters.denoiseAOCS.FindKernel(renderAOParameters.temporalAccumulation ? "GTAODenoise_Spatial_To_Temporal" : "GTAODenoise_Spatial");
			renderAOParameters.denoiseKernelTemporal = renderAOParameters.denoiseAOCS.FindKernel(renderAOParameters.fullResolution ? "GTAODenoise_Temporal_FullRes" : "GTAODenoise_Temporal");
			renderAOParameters.denoiseKernelCopyHistory = renderAOParameters.denoiseAOCS.FindKernel("GTAODenoise_CopyHistory");
			renderAOParameters.upsampleAndBlurKernel = renderAOParameters.upsampleAndBlurAOCS.FindKernel("BlurUpsample");
			renderAOParameters.upsampleAOKernel = renderAOParameters.upsampleAndBlurAOCS.FindKernel(component.bilateralUpsample ? "BilateralUpsampling" : "BoxUpsampling");
			renderAOParameters.outputWidth = camera.actualWidth;
			renderAOParameters.outputHeight = camera.actualHeight;
			renderAOParameters.viewCount = camera.viewCount;
			renderAOParameters.historyReady = this.m_HistoryReady;
			this.m_HistoryReady = true;
			renderAOParameters.runAsync = camera.frameSettings.SSAORunsAsync();
			renderAOParameters.motionVectorDisabled = !camera.frameSettings.IsEnabled(FrameSettingsField.MotionVectors);
			return renderAOParameters;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x0002E5F8 File Offset: 0x0002C7F8
		private static void RenderAO(in AmbientOcclusionSystem.RenderAOParameters parameters, RTHandle packedDataTexture, RenderPipelineResources resources, CommandBuffer cmd)
		{
			cmd.SetComputeVectorParam(parameters.gtaoCS, HDShaderIDs._AOBufferSize, parameters.aoBufferInfo);
			cmd.SetComputeVectorParam(parameters.gtaoCS, HDShaderIDs._AODepthToViewParams, parameters.toViewSpaceProj);
			cmd.SetComputeVectorParam(parameters.gtaoCS, HDShaderIDs._AOParams0, parameters.aoParams0);
			cmd.SetComputeVectorParam(parameters.gtaoCS, HDShaderIDs._AOParams1, parameters.aoParams1);
			cmd.SetComputeVectorParam(parameters.gtaoCS, HDShaderIDs._AOParams2, parameters.aoParams2);
			cmd.SetComputeVectorParam(parameters.gtaoCS, HDShaderIDs._AOParams4, parameters.aoParams4);
			cmd.SetComputeVectorParam(parameters.gtaoCS, HDShaderIDs._FirstTwoDepthMipOffsets, parameters.firstAndSecondMipOffsets);
			cmd.SetComputeTextureParam(parameters.gtaoCS, parameters.gtaoKernel, HDShaderIDs._AOPackedData, packedDataTexture);
			int num = ((int)parameters.runningRes.x + 7) / 8;
			int num2 = ((int)parameters.runningRes.y + 7) / 8;
			cmd.DispatchCompute(parameters.gtaoCS, parameters.gtaoKernel, num, num2, parameters.viewCount);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x0002E700 File Offset: 0x0002C900
		private static void DenoiseAO(in AmbientOcclusionSystem.RenderAOParameters parameters, RTHandle packedDataTex, RTHandle packedDataBlurredTex, RTHandle packedHistoryTex, RTHandle packedHistoryOutputTex, RTHandle aoOutputTex, CommandBuffer cmd)
		{
			int num = ((int)parameters.runningRes.x + 7) / 8;
			int num2 = ((int)parameters.runningRes.y + 7) / 8;
			if (parameters.temporalAccumulation || parameters.fullResolution)
			{
				ComputeShader denoiseAOCS = parameters.denoiseAOCS;
				cmd.SetComputeVectorParam(parameters.denoiseAOCS, HDShaderIDs._AOParams1, parameters.aoParams1);
				cmd.SetComputeVectorParam(parameters.denoiseAOCS, HDShaderIDs._AOParams2, parameters.aoParams2);
				cmd.SetComputeVectorParam(parameters.denoiseAOCS, HDShaderIDs._AOParams3, parameters.aoParams3);
				cmd.SetComputeVectorParam(parameters.denoiseAOCS, HDShaderIDs._AOParams4, parameters.aoParams4);
				cmd.SetComputeVectorParam(parameters.denoiseAOCS, HDShaderIDs._AOBufferSize, parameters.aoBufferInfo);
				cmd.SetComputeTextureParam(denoiseAOCS, parameters.denoiseKernelSpatial, HDShaderIDs._AOPackedData, packedDataTex);
				if (parameters.temporalAccumulation)
				{
					cmd.SetComputeTextureParam(denoiseAOCS, parameters.denoiseKernelSpatial, HDShaderIDs._AOPackedBlurred, packedDataBlurredTex);
				}
				else
				{
					cmd.SetComputeTextureParam(denoiseAOCS, parameters.denoiseKernelSpatial, HDShaderIDs._OcclusionTexture, aoOutputTex);
				}
				cmd.DispatchCompute(denoiseAOCS, parameters.denoiseKernelSpatial, num, num2, parameters.viewCount);
			}
			if (parameters.temporalAccumulation)
			{
				if (!parameters.historyReady)
				{
					cmd.SetComputeTextureParam(parameters.denoiseAOCS, parameters.denoiseKernelCopyHistory, HDShaderIDs._InputTexture, packedDataTex);
					cmd.SetComputeTextureParam(parameters.denoiseAOCS, parameters.denoiseKernelCopyHistory, HDShaderIDs._OutputTexture, packedHistoryTex);
					cmd.DispatchCompute(parameters.denoiseAOCS, parameters.denoiseKernelCopyHistory, num, num2, parameters.viewCount);
				}
				cmd.SetComputeTextureParam(parameters.denoiseAOCS, parameters.denoiseKernelTemporal, HDShaderIDs._AOPackedData, packedDataTex);
				cmd.SetComputeTextureParam(parameters.denoiseAOCS, parameters.denoiseKernelTemporal, HDShaderIDs._AOPackedBlurred, packedDataBlurredTex);
				cmd.SetComputeTextureParam(parameters.denoiseAOCS, parameters.denoiseKernelTemporal, HDShaderIDs._AOPackedHistory, packedHistoryTex);
				cmd.SetComputeTextureParam(parameters.denoiseAOCS, parameters.denoiseKernelTemporal, HDShaderIDs._AOOutputHistory, packedHistoryOutputTex);
				cmd.SetComputeTextureParam(parameters.denoiseAOCS, parameters.denoiseKernelTemporal, HDShaderIDs._OcclusionTexture, aoOutputTex);
				cmd.DispatchCompute(parameters.denoiseAOCS, parameters.denoiseKernelTemporal, num, num2, parameters.viewCount);
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x0002E94C File Offset: 0x0002CB4C
		private static void UpsampleAO(in AmbientOcclusionSystem.RenderAOParameters parameters, RTHandle input, RTHandle output, CommandBuffer cmd)
		{
			bool flag = !parameters.temporalAccumulation;
			cmd.SetComputeVectorParam(parameters.upsampleAndBlurAOCS, HDShaderIDs._AOBufferSize, parameters.aoBufferInfo);
			cmd.SetComputeVectorParam(parameters.upsampleAndBlurAOCS, HDShaderIDs._AOParams1, parameters.aoParams1);
			cmd.SetComputeVectorParam(parameters.upsampleAndBlurAOCS, HDShaderIDs._AOParams3, parameters.aoParams3);
			if (flag)
			{
				cmd.SetComputeTextureParam(parameters.upsampleAndBlurAOCS, parameters.upsampleAndBlurKernel, HDShaderIDs._AOPackedData, input);
				cmd.SetComputeTextureParam(parameters.upsampleAndBlurAOCS, parameters.upsampleAndBlurKernel, HDShaderIDs._OcclusionTexture, output);
				int num = ((int)parameters.runningRes.x + 7) / 8;
				int num2 = ((int)parameters.runningRes.y + 7) / 8;
				cmd.DispatchCompute(parameters.upsampleAndBlurAOCS, parameters.upsampleAndBlurKernel, num, num2, parameters.viewCount);
				return;
			}
			cmd.SetComputeTextureParam(parameters.upsampleAndBlurAOCS, parameters.upsampleAOKernel, HDShaderIDs._AOPackedData, input);
			cmd.SetComputeTextureParam(parameters.upsampleAndBlurAOCS, parameters.upsampleAOKernel, HDShaderIDs._OcclusionTexture, output);
			int num3 = ((int)parameters.runningRes.x + 7) / 8;
			int num4 = ((int)parameters.runningRes.y + 7) / 8;
			cmd.DispatchCompute(parameters.upsampleAndBlurAOCS, parameters.upsampleAOKernel, num3, num4, parameters.viewCount);
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x0002EA98 File Offset: 0x0002CC98
		internal void Dispatch(CommandBuffer cmd, HDCamera camera, int frameCount)
		{
			AmbientOcclusion component = camera.volumeStack.GetComponent<AmbientOcclusion>();
			if (this.IsActive(camera, component))
			{
				using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.RenderSSAO)))
				{
					this.EnsureRTSize(component, camera);
					RTHandle currentFrameRT = camera.GetCurrentFrameRT(7);
					RTHandle previousFrameRT = camera.GetPreviousFrameRT(7);
					AmbientOcclusionSystem.RenderAOParameters renderAOParameters = this.PrepareRenderAOParameters(camera, RTHandles.rtHandleProperties, frameCount);
					using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.HorizonSSAO)))
					{
						AmbientOcclusionSystem.RenderAO(in renderAOParameters, this.m_PackedDataTex, this.m_Resources, cmd);
					}
					using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.DenoiseSSAO)))
					{
						RTHandle rthandle = (this.m_RunningFullRes ? this.m_AmbientOcclusionTex : this.m_FinalHalfRes);
						AmbientOcclusionSystem.DenoiseAO(in renderAOParameters, this.m_PackedDataTex, this.m_PackedDataBlurred, currentFrameRT, previousFrameRT, rthandle, cmd);
						this.m_HistoryInfo = renderAOParameters.aoBufferInfo;
					}
					if (!this.m_RunningFullRes)
					{
						using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.UpSampleSSAO)))
						{
							AmbientOcclusionSystem.UpsampleAO(in renderAOParameters, component.temporalAccumulation.value ? this.m_FinalHalfRes : this.m_PackedDataTex, this.m_AmbientOcclusionTex, cmd);
						}
					}
				}
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x0002EC44 File Offset: 0x0002CE44
		internal void PushGlobalParameters(HDCamera hdCamera, CommandBuffer cmd)
		{
			AmbientOcclusion component = hdCamera.volumeStack.GetComponent<AmbientOcclusion>();
			if (this.IsActive(hdCamera, component))
			{
				cmd.SetGlobalVector(HDShaderIDs._AmbientOcclusionParam, new Vector4(0f, 0f, 0f, component.directLightingStrength.value));
				return;
			}
			cmd.SetGlobalVector(HDShaderIDs._AmbientOcclusionParam, Vector4.zero);
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x0002ECA4 File Offset: 0x0002CEA4
		internal void PostDispatchWork(CommandBuffer cmd, HDCamera camera)
		{
			AmbientOcclusion component = camera.volumeStack.GetComponent<AmbientOcclusion>();
			RTHandle rthandle = (this.IsActive(camera, component) ? this.m_AmbientOcclusionTex : TextureXR.GetBlackTexture());
			cmd.SetGlobalTexture(HDShaderIDs._AmbientOcclusionTexture, rthandle);
			(RenderPipelineManager.currentPipeline as HDRenderPipeline).PushFullScreenDebugTexture(camera, cmd, rthandle, FullScreenDebugMode.SSAO);
		}

		// Token: 0x04000580 RID: 1408
		private RenderPipelineResources m_Resources;

		// Token: 0x04000581 RID: 1409
		private RenderPipelineSettings m_Settings;

		// Token: 0x04000582 RID: 1410
		private bool m_HistoryReady;

		// Token: 0x04000583 RID: 1411
		private RTHandle m_PackedDataTex;

		// Token: 0x04000584 RID: 1412
		private RTHandle m_PackedDataBlurred;

		// Token: 0x04000585 RID: 1413
		private RTHandle m_AmbientOcclusionTex;

		// Token: 0x04000586 RID: 1414
		private RTHandle m_FinalHalfRes;

		// Token: 0x04000587 RID: 1415
		private bool m_RunningFullRes;

		// Token: 0x04000588 RID: 1416
		private Vector4 m_HistoryInfo;

		// Token: 0x04000589 RID: 1417
		private readonly HDRaytracingAmbientOcclusion m_RaytracingAmbientOcclusion = new HDRaytracingAmbientOcclusion();

		// Token: 0x0200020E RID: 526
		private class RenderAOPassData
		{
			// Token: 0x04001396 RID: 5014
			public AmbientOcclusionSystem.RenderAOParameters parameters;

			// Token: 0x04001397 RID: 5015
			public RenderGraphMutableResource packedData;

			// Token: 0x04001398 RID: 5016
			public RenderGraphResource depthPyramid;
		}

		// Token: 0x0200020F RID: 527
		private class DenoiseAOPassData
		{
			// Token: 0x04001399 RID: 5017
			public AmbientOcclusionSystem.RenderAOParameters parameters;

			// Token: 0x0400139A RID: 5018
			public RenderGraphResource packedData;

			// Token: 0x0400139B RID: 5019
			public RenderGraphMutableResource packedDataBlurred;

			// Token: 0x0400139C RID: 5020
			public RenderGraphResource currentHistory;

			// Token: 0x0400139D RID: 5021
			public RenderGraphMutableResource outputHistory;

			// Token: 0x0400139E RID: 5022
			public RenderGraphMutableResource denoiseOutput;

			// Token: 0x0400139F RID: 5023
			public RenderGraphResource motionVectors;
		}

		// Token: 0x02000210 RID: 528
		private class UpsampleAOPassData
		{
			// Token: 0x040013A0 RID: 5024
			public AmbientOcclusionSystem.RenderAOParameters parameters;

			// Token: 0x040013A1 RID: 5025
			public RenderGraphResource input;

			// Token: 0x040013A2 RID: 5026
			public RenderGraphMutableResource output;
		}

		// Token: 0x02000211 RID: 529
		private struct RenderAOParameters
		{
			// Token: 0x040013A3 RID: 5027
			public ComputeShader gtaoCS;

			// Token: 0x040013A4 RID: 5028
			public int gtaoKernel;

			// Token: 0x040013A5 RID: 5029
			public ComputeShader denoiseAOCS;

			// Token: 0x040013A6 RID: 5030
			public int denoiseKernelSpatial;

			// Token: 0x040013A7 RID: 5031
			public int denoiseKernelTemporal;

			// Token: 0x040013A8 RID: 5032
			public int denoiseKernelCopyHistory;

			// Token: 0x040013A9 RID: 5033
			public ComputeShader upsampleAndBlurAOCS;

			// Token: 0x040013AA RID: 5034
			public int upsampleAndBlurKernel;

			// Token: 0x040013AB RID: 5035
			public int upsampleAOKernel;

			// Token: 0x040013AC RID: 5036
			public Vector4 aoParams0;

			// Token: 0x040013AD RID: 5037
			public Vector4 aoParams1;

			// Token: 0x040013AE RID: 5038
			public Vector4 aoParams2;

			// Token: 0x040013AF RID: 5039
			public Vector4 aoParams3;

			// Token: 0x040013B0 RID: 5040
			public Vector4 aoParams4;

			// Token: 0x040013B1 RID: 5041
			public Vector4 firstAndSecondMipOffsets;

			// Token: 0x040013B2 RID: 5042
			public Vector4 aoBufferInfo;

			// Token: 0x040013B3 RID: 5043
			public Vector4 toViewSpaceProj;

			// Token: 0x040013B4 RID: 5044
			public Vector2 runningRes;

			// Token: 0x040013B5 RID: 5045
			public int viewCount;

			// Token: 0x040013B6 RID: 5046
			public bool historyReady;

			// Token: 0x040013B7 RID: 5047
			public int outputWidth;

			// Token: 0x040013B8 RID: 5048
			public int outputHeight;

			// Token: 0x040013B9 RID: 5049
			public bool fullResolution;

			// Token: 0x040013BA RID: 5050
			public bool runAsync;

			// Token: 0x040013BB RID: 5051
			public bool motionVectorDisabled;

			// Token: 0x040013BC RID: 5052
			public bool temporalAccumulation;

			// Token: 0x040013BD RID: 5053
			public bool bilateralUpsample;
		}
	}
}
