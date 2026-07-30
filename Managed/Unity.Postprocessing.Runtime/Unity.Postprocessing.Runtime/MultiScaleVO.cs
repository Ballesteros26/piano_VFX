using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200002A RID: 42
	[Preserve]
	[Serializable]
	internal sealed class MultiScaleVO : IAmbientOcclusionMethod
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00005E58 File Offset: 0x00004058
		public MultiScaleVO(AmbientOcclusion settings)
		{
			this.m_Settings = settings;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00004DDA File Offset: 0x00002FDA
		public DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00005F77 File Offset: 0x00004177
		public void SetResources(PostProcessResources resources)
		{
			this.m_Resources = resources;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00005F80 File Offset: 0x00004180
		private void Alloc(CommandBuffer cmd, int id, MultiScaleVO.MipLevel size, RenderTextureFormat format, bool uav)
		{
			cmd.GetTemporaryRT(id, new RenderTextureDescriptor
			{
				width = this.m_ScaledWidths[(int)size],
				height = this.m_ScaledHeights[(int)size],
				colorFormat = format,
				depthBufferBits = 0,
				volumeDepth = 1,
				autoGenerateMips = false,
				msaaSamples = 1,
				enableRandomWrite = uav,
				dimension = TextureDimension.Tex2D,
				sRGB = false
			}, FilterMode.Point);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00006000 File Offset: 0x00004200
		private void AllocArray(CommandBuffer cmd, int id, MultiScaleVO.MipLevel size, RenderTextureFormat format, bool uav)
		{
			cmd.GetTemporaryRT(id, new RenderTextureDescriptor
			{
				width = this.m_ScaledWidths[(int)size],
				height = this.m_ScaledHeights[(int)size],
				colorFormat = format,
				depthBufferBits = 0,
				volumeDepth = 16,
				autoGenerateMips = false,
				msaaSamples = 1,
				enableRandomWrite = uav,
				dimension = TextureDimension.Tex2DArray,
				sRGB = false
			}, FilterMode.Point);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00006081 File Offset: 0x00004281
		private void Release(CommandBuffer cmd, int id)
		{
			cmd.ReleaseTemporaryRT(id);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000608C File Offset: 0x0000428C
		private Vector4 CalculateZBufferParams(Camera camera)
		{
			float num = camera.farClipPlane / camera.nearClipPlane;
			if (SystemInfo.usesReversedZBuffer)
			{
				return new Vector4(num - 1f, 1f, 0f, 0f);
			}
			return new Vector4(1f - num, num, 0f, 0f);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000060E4 File Offset: 0x000042E4
		private float CalculateTanHalfFovHeight(Camera camera)
		{
			return 1f / camera.projectionMatrix[0, 0];
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00006107 File Offset: 0x00004307
		private Vector2 GetSize(MultiScaleVO.MipLevel mip)
		{
			return new Vector2((float)this.m_ScaledWidths[(int)mip], (float)this.m_ScaledHeights[(int)mip]);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00006120 File Offset: 0x00004320
		private Vector3 GetSizeArray(MultiScaleVO.MipLevel mip)
		{
			return new Vector3((float)this.m_ScaledWidths[(int)mip], (float)this.m_ScaledHeights[(int)mip], 16f);
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00006140 File Offset: 0x00004340
		public void GenerateAOMap(CommandBuffer cmd, Camera camera, RenderTargetIdentifier destination, RenderTargetIdentifier? depthMap, bool invert, bool isMSAA)
		{
			this.m_ScaledWidths[0] = camera.scaledPixelWidth * (RuntimeUtilities.isSinglePassStereoEnabled ? 2 : 1);
			this.m_ScaledHeights[0] = camera.scaledPixelHeight;
			for (int i = 1; i < 7; i++)
			{
				int num = 1 << i;
				this.m_ScaledWidths[i] = (this.m_ScaledWidths[0] + (num - 1)) / num;
				this.m_ScaledHeights[i] = (this.m_ScaledHeights[0] + (num - 1)) / num;
			}
			this.PushAllocCommands(cmd, isMSAA);
			this.PushDownsampleCommands(cmd, camera, depthMap, isMSAA);
			float num2 = this.CalculateTanHalfFovHeight(camera);
			this.PushRenderCommands(cmd, ShaderIDs.TiledDepth1, ShaderIDs.Occlusion1, this.GetSizeArray(MultiScaleVO.MipLevel.L3), num2, isMSAA);
			this.PushRenderCommands(cmd, ShaderIDs.TiledDepth2, ShaderIDs.Occlusion2, this.GetSizeArray(MultiScaleVO.MipLevel.L4), num2, isMSAA);
			this.PushRenderCommands(cmd, ShaderIDs.TiledDepth3, ShaderIDs.Occlusion3, this.GetSizeArray(MultiScaleVO.MipLevel.L5), num2, isMSAA);
			this.PushRenderCommands(cmd, ShaderIDs.TiledDepth4, ShaderIDs.Occlusion4, this.GetSizeArray(MultiScaleVO.MipLevel.L6), num2, isMSAA);
			this.PushUpsampleCommands(cmd, ShaderIDs.LowDepth4, ShaderIDs.Occlusion4, ShaderIDs.LowDepth3, new int?(ShaderIDs.Occlusion3), ShaderIDs.Combined3, this.GetSize(MultiScaleVO.MipLevel.L4), this.GetSize(MultiScaleVO.MipLevel.L3), isMSAA, false);
			this.PushUpsampleCommands(cmd, ShaderIDs.LowDepth3, ShaderIDs.Combined3, ShaderIDs.LowDepth2, new int?(ShaderIDs.Occlusion2), ShaderIDs.Combined2, this.GetSize(MultiScaleVO.MipLevel.L3), this.GetSize(MultiScaleVO.MipLevel.L2), isMSAA, false);
			this.PushUpsampleCommands(cmd, ShaderIDs.LowDepth2, ShaderIDs.Combined2, ShaderIDs.LowDepth1, new int?(ShaderIDs.Occlusion1), ShaderIDs.Combined1, this.GetSize(MultiScaleVO.MipLevel.L2), this.GetSize(MultiScaleVO.MipLevel.L1), isMSAA, false);
			this.PushUpsampleCommands(cmd, ShaderIDs.LowDepth1, ShaderIDs.Combined1, ShaderIDs.LinearDepth, null, destination, this.GetSize(MultiScaleVO.MipLevel.L1), this.GetSize(MultiScaleVO.MipLevel.Original), isMSAA, invert);
			this.PushReleaseCommands(cmd);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000633C File Offset: 0x0000453C
		private void PushAllocCommands(CommandBuffer cmd, bool isMSAA)
		{
			if (isMSAA)
			{
				this.Alloc(cmd, ShaderIDs.LinearDepth, MultiScaleVO.MipLevel.Original, RenderTextureFormat.RGHalf, true);
				this.Alloc(cmd, ShaderIDs.LowDepth1, MultiScaleVO.MipLevel.L1, RenderTextureFormat.RGFloat, true);
				this.Alloc(cmd, ShaderIDs.LowDepth2, MultiScaleVO.MipLevel.L2, RenderTextureFormat.RGFloat, true);
				this.Alloc(cmd, ShaderIDs.LowDepth3, MultiScaleVO.MipLevel.L3, RenderTextureFormat.RGFloat, true);
				this.Alloc(cmd, ShaderIDs.LowDepth4, MultiScaleVO.MipLevel.L4, RenderTextureFormat.RGFloat, true);
				this.AllocArray(cmd, ShaderIDs.TiledDepth1, MultiScaleVO.MipLevel.L3, RenderTextureFormat.RGHalf, true);
				this.AllocArray(cmd, ShaderIDs.TiledDepth2, MultiScaleVO.MipLevel.L4, RenderTextureFormat.RGHalf, true);
				this.AllocArray(cmd, ShaderIDs.TiledDepth3, MultiScaleVO.MipLevel.L5, RenderTextureFormat.RGHalf, true);
				this.AllocArray(cmd, ShaderIDs.TiledDepth4, MultiScaleVO.MipLevel.L6, RenderTextureFormat.RGHalf, true);
				this.Alloc(cmd, ShaderIDs.Occlusion1, MultiScaleVO.MipLevel.L1, RenderTextureFormat.RG16, true);
				this.Alloc(cmd, ShaderIDs.Occlusion2, MultiScaleVO.MipLevel.L2, RenderTextureFormat.RG16, true);
				this.Alloc(cmd, ShaderIDs.Occlusion3, MultiScaleVO.MipLevel.L3, RenderTextureFormat.RG16, true);
				this.Alloc(cmd, ShaderIDs.Occlusion4, MultiScaleVO.MipLevel.L4, RenderTextureFormat.RG16, true);
				this.Alloc(cmd, ShaderIDs.Combined1, MultiScaleVO.MipLevel.L1, RenderTextureFormat.RG16, true);
				this.Alloc(cmd, ShaderIDs.Combined2, MultiScaleVO.MipLevel.L2, RenderTextureFormat.RG16, true);
				this.Alloc(cmd, ShaderIDs.Combined3, MultiScaleVO.MipLevel.L3, RenderTextureFormat.RG16, true);
				return;
			}
			this.Alloc(cmd, ShaderIDs.LinearDepth, MultiScaleVO.MipLevel.Original, RenderTextureFormat.RHalf, true);
			this.Alloc(cmd, ShaderIDs.LowDepth1, MultiScaleVO.MipLevel.L1, RenderTextureFormat.RFloat, true);
			this.Alloc(cmd, ShaderIDs.LowDepth2, MultiScaleVO.MipLevel.L2, RenderTextureFormat.RFloat, true);
			this.Alloc(cmd, ShaderIDs.LowDepth3, MultiScaleVO.MipLevel.L3, RenderTextureFormat.RFloat, true);
			this.Alloc(cmd, ShaderIDs.LowDepth4, MultiScaleVO.MipLevel.L4, RenderTextureFormat.RFloat, true);
			this.AllocArray(cmd, ShaderIDs.TiledDepth1, MultiScaleVO.MipLevel.L3, RenderTextureFormat.RHalf, true);
			this.AllocArray(cmd, ShaderIDs.TiledDepth2, MultiScaleVO.MipLevel.L4, RenderTextureFormat.RHalf, true);
			this.AllocArray(cmd, ShaderIDs.TiledDepth3, MultiScaleVO.MipLevel.L5, RenderTextureFormat.RHalf, true);
			this.AllocArray(cmd, ShaderIDs.TiledDepth4, MultiScaleVO.MipLevel.L6, RenderTextureFormat.RHalf, true);
			this.Alloc(cmd, ShaderIDs.Occlusion1, MultiScaleVO.MipLevel.L1, RenderTextureFormat.R8, true);
			this.Alloc(cmd, ShaderIDs.Occlusion2, MultiScaleVO.MipLevel.L2, RenderTextureFormat.R8, true);
			this.Alloc(cmd, ShaderIDs.Occlusion3, MultiScaleVO.MipLevel.L3, RenderTextureFormat.R8, true);
			this.Alloc(cmd, ShaderIDs.Occlusion4, MultiScaleVO.MipLevel.L4, RenderTextureFormat.R8, true);
			this.Alloc(cmd, ShaderIDs.Combined1, MultiScaleVO.MipLevel.L1, RenderTextureFormat.R8, true);
			this.Alloc(cmd, ShaderIDs.Combined2, MultiScaleVO.MipLevel.L2, RenderTextureFormat.R8, true);
			this.Alloc(cmd, ShaderIDs.Combined3, MultiScaleVO.MipLevel.L3, RenderTextureFormat.R8, true);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00006550 File Offset: 0x00004750
		private void PushDownsampleCommands(CommandBuffer cmd, Camera camera, RenderTargetIdentifier? depthMap, bool isMSAA)
		{
			bool flag = false;
			RenderTargetIdentifier renderTargetIdentifier;
			if (depthMap != null)
			{
				renderTargetIdentifier = depthMap.Value;
			}
			else if (!RuntimeUtilities.IsResolvedDepthAvailable(camera))
			{
				this.Alloc(cmd, ShaderIDs.DepthCopy, MultiScaleVO.MipLevel.Original, RenderTextureFormat.RFloat, false);
				renderTargetIdentifier = new RenderTargetIdentifier(ShaderIDs.DepthCopy);
				cmd.BlitFullscreenTriangle(BuiltinRenderTextureType.None, renderTargetIdentifier, this.m_PropertySheet, 0, false, null);
				flag = true;
			}
			else
			{
				renderTargetIdentifier = BuiltinRenderTextureType.ResolvedDepth;
			}
			ComputeShader computeShader = this.m_Resources.computeShaders.multiScaleAODownsample1;
			int num = computeShader.FindKernel(isMSAA ? "MultiScaleVODownsample1_MSAA" : "MultiScaleVODownsample1");
			cmd.SetComputeTextureParam(computeShader, num, "LinearZ", ShaderIDs.LinearDepth);
			cmd.SetComputeTextureParam(computeShader, num, "DS2x", ShaderIDs.LowDepth1);
			cmd.SetComputeTextureParam(computeShader, num, "DS4x", ShaderIDs.LowDepth2);
			cmd.SetComputeTextureParam(computeShader, num, "DS2xAtlas", ShaderIDs.TiledDepth1);
			cmd.SetComputeTextureParam(computeShader, num, "DS4xAtlas", ShaderIDs.TiledDepth2);
			cmd.SetComputeVectorParam(computeShader, "ZBufferParams", this.CalculateZBufferParams(camera));
			cmd.SetComputeTextureParam(computeShader, num, "Depth", renderTargetIdentifier);
			cmd.DispatchCompute(computeShader, num, this.m_ScaledWidths[4], this.m_ScaledHeights[4], 1);
			if (flag)
			{
				this.Release(cmd, ShaderIDs.DepthCopy);
			}
			computeShader = this.m_Resources.computeShaders.multiScaleAODownsample2;
			num = (isMSAA ? computeShader.FindKernel("MultiScaleVODownsample2_MSAA") : computeShader.FindKernel("MultiScaleVODownsample2"));
			cmd.SetComputeTextureParam(computeShader, num, "DS4x", ShaderIDs.LowDepth2);
			cmd.SetComputeTextureParam(computeShader, num, "DS8x", ShaderIDs.LowDepth3);
			cmd.SetComputeTextureParam(computeShader, num, "DS16x", ShaderIDs.LowDepth4);
			cmd.SetComputeTextureParam(computeShader, num, "DS8xAtlas", ShaderIDs.TiledDepth3);
			cmd.SetComputeTextureParam(computeShader, num, "DS16xAtlas", ShaderIDs.TiledDepth4);
			cmd.DispatchCompute(computeShader, num, this.m_ScaledWidths[6], this.m_ScaledHeights[6], 1);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00006760 File Offset: 0x00004960
		private void PushRenderCommands(CommandBuffer cmd, int source, int destination, Vector3 sourceSize, float tanHalfFovH, bool isMSAA)
		{
			float num = 2f * tanHalfFovH * 10f / sourceSize.x;
			if (RuntimeUtilities.isSinglePassStereoEnabled)
			{
				num *= 2f;
			}
			float num2 = 1f / num;
			for (int i = 0; i < 12; i++)
			{
				this.m_InvThicknessTable[i] = num2 / this.m_SampleThickness[i];
			}
			this.m_SampleWeightTable[0] = 4f * this.m_SampleThickness[0];
			this.m_SampleWeightTable[1] = 4f * this.m_SampleThickness[1];
			this.m_SampleWeightTable[2] = 4f * this.m_SampleThickness[2];
			this.m_SampleWeightTable[3] = 4f * this.m_SampleThickness[3];
			this.m_SampleWeightTable[4] = 4f * this.m_SampleThickness[4];
			this.m_SampleWeightTable[5] = 8f * this.m_SampleThickness[5];
			this.m_SampleWeightTable[6] = 8f * this.m_SampleThickness[6];
			this.m_SampleWeightTable[7] = 8f * this.m_SampleThickness[7];
			this.m_SampleWeightTable[8] = 4f * this.m_SampleThickness[8];
			this.m_SampleWeightTable[9] = 8f * this.m_SampleThickness[9];
			this.m_SampleWeightTable[10] = 8f * this.m_SampleThickness[10];
			this.m_SampleWeightTable[11] = 4f * this.m_SampleThickness[11];
			this.m_SampleWeightTable[0] = 0f;
			this.m_SampleWeightTable[2] = 0f;
			this.m_SampleWeightTable[5] = 0f;
			this.m_SampleWeightTable[7] = 0f;
			this.m_SampleWeightTable[9] = 0f;
			float num3 = 0f;
			foreach (float num4 in this.m_SampleWeightTable)
			{
				num3 += num4;
			}
			for (int k = 0; k < this.m_SampleWeightTable.Length; k++)
			{
				this.m_SampleWeightTable[k] /= num3;
			}
			ComputeShader multiScaleAORender = this.m_Resources.computeShaders.multiScaleAORender;
			int num5 = (isMSAA ? multiScaleAORender.FindKernel("MultiScaleVORender_MSAA_interleaved") : multiScaleAORender.FindKernel("MultiScaleVORender_interleaved"));
			cmd.SetComputeFloatParams(multiScaleAORender, "gInvThicknessTable", this.m_InvThicknessTable);
			cmd.SetComputeFloatParams(multiScaleAORender, "gSampleWeightTable", this.m_SampleWeightTable);
			cmd.SetComputeVectorParam(multiScaleAORender, "gInvSliceDimension", new Vector2(1f / sourceSize.x, 1f / sourceSize.y));
			cmd.SetComputeVectorParam(multiScaleAORender, "AdditionalParams", new Vector2(-1f / this.m_Settings.thicknessModifier.value, this.m_Settings.intensity.value));
			cmd.SetComputeTextureParam(multiScaleAORender, num5, "DepthTex", source);
			cmd.SetComputeTextureParam(multiScaleAORender, num5, "Occlusion", destination);
			uint num6;
			uint num7;
			uint num8;
			multiScaleAORender.GetKernelThreadGroupSizes(num5, out num6, out num7, out num8);
			cmd.DispatchCompute(multiScaleAORender, num5, ((int)sourceSize.x + (int)num6 - 1) / (int)num6, ((int)sourceSize.y + (int)num7 - 1) / (int)num7, ((int)sourceSize.z + (int)num8 - 1) / (int)num8);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00006A94 File Offset: 0x00004C94
		private void PushUpsampleCommands(CommandBuffer cmd, int lowResDepth, int interleavedAO, int highResDepth, int? highResAO, RenderTargetIdentifier dest, Vector3 lowResDepthSize, Vector2 highResDepthSize, bool isMSAA, bool invert = false)
		{
			ComputeShader multiScaleAOUpsample = this.m_Resources.computeShaders.multiScaleAOUpsample;
			int num;
			if (!isMSAA)
			{
				num = multiScaleAOUpsample.FindKernel((highResAO == null) ? (invert ? "MultiScaleVOUpSample_invert" : "MultiScaleVOUpSample") : "MultiScaleVOUpSample_blendout");
			}
			else
			{
				num = multiScaleAOUpsample.FindKernel((highResAO == null) ? (invert ? "MultiScaleVOUpSample_MSAA_invert" : "MultiScaleVOUpSample_MSAA") : "MultiScaleVOUpSample_MSAA_blendout");
			}
			float num2 = 1920f / lowResDepthSize.x;
			float num3 = 1f - Mathf.Pow(10f, this.m_Settings.blurTolerance.value) * num2;
			num3 *= num3;
			float num4 = Mathf.Pow(10f, this.m_Settings.upsampleTolerance.value);
			float num5 = 1f / (Mathf.Pow(10f, this.m_Settings.noiseFilterTolerance.value) + num4);
			cmd.SetComputeVectorParam(multiScaleAOUpsample, "InvLowResolution", new Vector2(1f / lowResDepthSize.x, 1f / lowResDepthSize.y));
			cmd.SetComputeVectorParam(multiScaleAOUpsample, "InvHighResolution", new Vector2(1f / highResDepthSize.x, 1f / highResDepthSize.y));
			cmd.SetComputeVectorParam(multiScaleAOUpsample, "AdditionalParams", new Vector4(num5, num2, num3, num4));
			cmd.SetComputeTextureParam(multiScaleAOUpsample, num, "LoResDB", lowResDepth);
			cmd.SetComputeTextureParam(multiScaleAOUpsample, num, "HiResDB", highResDepth);
			cmd.SetComputeTextureParam(multiScaleAOUpsample, num, "LoResAO1", interleavedAO);
			if (highResAO != null)
			{
				cmd.SetComputeTextureParam(multiScaleAOUpsample, num, "HiResAO", highResAO.Value);
			}
			cmd.SetComputeTextureParam(multiScaleAOUpsample, num, "AoResult", dest);
			int num6 = ((int)highResDepthSize.x + 17) / 16;
			int num7 = ((int)highResDepthSize.y + 17) / 16;
			cmd.DispatchCompute(multiScaleAOUpsample, num, num6, num7, 1);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00006C90 File Offset: 0x00004E90
		private void PushReleaseCommands(CommandBuffer cmd)
		{
			this.Release(cmd, ShaderIDs.LinearDepth);
			this.Release(cmd, ShaderIDs.LowDepth1);
			this.Release(cmd, ShaderIDs.LowDepth2);
			this.Release(cmd, ShaderIDs.LowDepth3);
			this.Release(cmd, ShaderIDs.LowDepth4);
			this.Release(cmd, ShaderIDs.TiledDepth1);
			this.Release(cmd, ShaderIDs.TiledDepth2);
			this.Release(cmd, ShaderIDs.TiledDepth3);
			this.Release(cmd, ShaderIDs.TiledDepth4);
			this.Release(cmd, ShaderIDs.Occlusion1);
			this.Release(cmd, ShaderIDs.Occlusion2);
			this.Release(cmd, ShaderIDs.Occlusion3);
			this.Release(cmd, ShaderIDs.Occlusion4);
			this.Release(cmd, ShaderIDs.Combined1);
			this.Release(cmd, ShaderIDs.Combined2);
			this.Release(cmd, ShaderIDs.Combined3);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00006D60 File Offset: 0x00004F60
		private void PreparePropertySheet(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.m_Resources.shaders.multiScaleAO);
			propertySheet.ClearKeywords();
			propertySheet.properties.SetVector(ShaderIDs.AOColor, Color.white - this.m_Settings.color.value);
			this.m_PropertySheet = propertySheet;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00006DC8 File Offset: 0x00004FC8
		private void CheckAOTexture(PostProcessRenderContext context)
		{
			if (this.m_AmbientOnlyAO == null || !this.m_AmbientOnlyAO.IsCreated() || this.m_AmbientOnlyAO.width != context.width || this.m_AmbientOnlyAO.height != context.height || this.m_AmbientOnlyAO.useDynamicScale != context.camera.allowDynamicResolution)
			{
				RuntimeUtilities.Destroy(this.m_AmbientOnlyAO);
				this.m_AmbientOnlyAO = new RenderTexture(context.width, context.height, 0, RenderTextureFormat.R8, RenderTextureReadWrite.Linear)
				{
					hideFlags = HideFlags.DontSave,
					filterMode = FilterMode.Point,
					enableRandomWrite = true,
					useDynamicScale = context.camera.allowDynamicResolution
				};
				this.m_AmbientOnlyAO.Create();
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00006E97 File Offset: 0x00005097
		private void PushDebug(PostProcessRenderContext context)
		{
			if (context.IsDebugOverlayEnabled(DebugOverlay.AmbientOcclusion))
			{
				context.PushDebugOverlay(context.command, this.m_AmbientOnlyAO, this.m_PropertySheet, 3);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00006EC0 File Offset: 0x000050C0
		public void RenderAfterOpaque(PostProcessRenderContext context)
		{
			CommandBuffer command = context.command;
			command.BeginSample("Ambient Occlusion");
			this.SetResources(context.resources);
			this.PreparePropertySheet(context);
			this.CheckAOTexture(context);
			if (context.camera.actualRenderingPath == RenderingPath.Forward && RenderSettings.fog)
			{
				this.m_PropertySheet.EnableKeyword("APPLY_FORWARD_FOG");
				this.m_PropertySheet.properties.SetVector(ShaderIDs.FogParams, new Vector3(RenderSettings.fogDensity, RenderSettings.fogStartDistance, RenderSettings.fogEndDistance));
			}
			this.GenerateAOMap(command, context.camera, this.m_AmbientOnlyAO, null, false, false);
			this.PushDebug(context);
			command.SetGlobalTexture(ShaderIDs.MSVOcclusionTexture, this.m_AmbientOnlyAO);
			command.BlitFullscreenTriangle(BuiltinRenderTextureType.None, BuiltinRenderTextureType.CameraTarget, this.m_PropertySheet, 2, RenderBufferLoadAction.Load, null);
			command.EndSample("Ambient Occlusion");
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00006FBC File Offset: 0x000051BC
		public void RenderAmbientOnly(PostProcessRenderContext context)
		{
			CommandBuffer command = context.command;
			command.BeginSample("Ambient Occlusion Render");
			this.SetResources(context.resources);
			this.PreparePropertySheet(context);
			this.CheckAOTexture(context);
			this.GenerateAOMap(command, context.camera, this.m_AmbientOnlyAO, null, false, false);
			this.PushDebug(context);
			command.EndSample("Ambient Occlusion Render");
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000702C File Offset: 0x0000522C
		public void CompositeAmbientOnly(PostProcessRenderContext context)
		{
			CommandBuffer command = context.command;
			command.BeginSample("Ambient Occlusion Composite");
			command.SetGlobalTexture(ShaderIDs.MSVOcclusionTexture, this.m_AmbientOnlyAO);
			command.BlitFullscreenTriangle(BuiltinRenderTextureType.None, this.m_MRT, BuiltinRenderTextureType.CameraTarget, this.m_PropertySheet, 1, false, null);
			command.EndSample("Ambient Occlusion Composite");
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00007093 File Offset: 0x00005293
		public void Release()
		{
			RuntimeUtilities.Destroy(this.m_AmbientOnlyAO);
			this.m_AmbientOnlyAO = null;
		}

		// Token: 0x0400009A RID: 154
		private readonly float[] m_SampleThickness = new float[]
		{
			Mathf.Sqrt(0.96f),
			Mathf.Sqrt(0.84f),
			Mathf.Sqrt(0.64f),
			Mathf.Sqrt(0.35999995f),
			Mathf.Sqrt(0.91999996f),
			Mathf.Sqrt(0.79999995f),
			Mathf.Sqrt(0.59999996f),
			Mathf.Sqrt(0.31999993f),
			Mathf.Sqrt(0.67999995f),
			Mathf.Sqrt(0.47999996f),
			Mathf.Sqrt(0.19999993f),
			Mathf.Sqrt(0.27999997f)
		};

		// Token: 0x0400009B RID: 155
		private readonly float[] m_InvThicknessTable = new float[12];

		// Token: 0x0400009C RID: 156
		private readonly float[] m_SampleWeightTable = new float[12];

		// Token: 0x0400009D RID: 157
		private readonly int[] m_ScaledWidths = new int[7];

		// Token: 0x0400009E RID: 158
		private readonly int[] m_ScaledHeights = new int[7];

		// Token: 0x0400009F RID: 159
		private AmbientOcclusion m_Settings;

		// Token: 0x040000A0 RID: 160
		private PropertySheet m_PropertySheet;

		// Token: 0x040000A1 RID: 161
		private PostProcessResources m_Resources;

		// Token: 0x040000A2 RID: 162
		private RenderTexture m_AmbientOnlyAO;

		// Token: 0x040000A3 RID: 163
		private readonly RenderTargetIdentifier[] m_MRT = new RenderTargetIdentifier[]
		{
			BuiltinRenderTextureType.GBuffer0,
			BuiltinRenderTextureType.CameraTarget
		};

		// Token: 0x0200006E RID: 110
		internal enum MipLevel
		{
			// Token: 0x04000270 RID: 624
			Original,
			// Token: 0x04000271 RID: 625
			L1,
			// Token: 0x04000272 RID: 626
			L2,
			// Token: 0x04000273 RID: 627
			L3,
			// Token: 0x04000274 RID: 628
			L4,
			// Token: 0x04000275 RID: 629
			L5,
			// Token: 0x04000276 RID: 630
			L6
		}

		// Token: 0x0200006F RID: 111
		private enum Pass
		{
			// Token: 0x04000278 RID: 632
			DepthCopy,
			// Token: 0x04000279 RID: 633
			CompositionDeferred,
			// Token: 0x0400027A RID: 634
			CompositionForward,
			// Token: 0x0400027B RID: 635
			DebugOverlay
		}
	}
}
