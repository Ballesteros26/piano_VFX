using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200002B RID: 43
	[Preserve]
	[Serializable]
	internal sealed class ScalableAO : IAmbientOcclusionMethod
	{
		// Token: 0x06000074 RID: 116 RVA: 0x000070A8 File Offset: 0x000052A8
		public ScalableAO(AmbientOcclusion settings)
		{
			this.m_Settings = settings;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00007100 File Offset: 0x00005300
		public DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth | DepthTextureMode.DepthNormals;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00007104 File Offset: 0x00005304
		private void DoLazyInitialization(PostProcessRenderContext context)
		{
			this.m_PropertySheet = context.propertySheets.Get(context.resources.shaders.scalableAO);
			bool flag = false;
			if (this.m_Result == null || !this.m_Result.IsCreated())
			{
				this.m_Result = context.GetScreenSpaceTemporaryRT(0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear, 0, 0);
				this.m_Result.hideFlags = HideFlags.DontSave;
				this.m_Result.filterMode = FilterMode.Bilinear;
				flag = true;
			}
			else if (this.m_Result.width != context.width || this.m_Result.height != context.height)
			{
				this.m_Result.Release();
				this.m_Result.width = context.width;
				this.m_Result.height = context.height;
				flag = true;
			}
			if (flag)
			{
				this.m_Result.Create();
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000071E4 File Offset: 0x000053E4
		private void Render(PostProcessRenderContext context, CommandBuffer cmd, int occlusionSource)
		{
			this.DoLazyInitialization(context);
			this.m_Settings.radius.value = Mathf.Max(this.m_Settings.radius.value, 0.0001f);
			bool flag = this.m_Settings.quality.value < AmbientOcclusionQuality.High;
			float value = this.m_Settings.intensity.value;
			float value2 = this.m_Settings.radius.value;
			float num = (flag ? 0.5f : 1f);
			float num2 = (float)this.m_SampleCount[(int)this.m_Settings.quality.value];
			PropertySheet propertySheet = this.m_PropertySheet;
			propertySheet.ClearKeywords();
			propertySheet.properties.SetVector(ShaderIDs.AOParams, new Vector4(value, value2, num, num2));
			propertySheet.properties.SetVector(ShaderIDs.AOColor, Color.white - this.m_Settings.color.value);
			if (context.camera.actualRenderingPath == RenderingPath.Forward && RenderSettings.fog)
			{
				propertySheet.EnableKeyword("APPLY_FORWARD_FOG");
				propertySheet.properties.SetVector(ShaderIDs.FogParams, new Vector3(RenderSettings.fogDensity, RenderSettings.fogStartDistance, RenderSettings.fogEndDistance));
			}
			int num3 = (flag ? 2 : 1);
			int occlusionTexture = ShaderIDs.OcclusionTexture1;
			int num4 = context.width / num3;
			int num5 = context.height / num3;
			context.GetScreenSpaceTemporaryRT(cmd, occlusionTexture, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear, FilterMode.Bilinear, num4, num5);
			cmd.BlitFullscreenTriangle(BuiltinRenderTextureType.None, occlusionTexture, propertySheet, occlusionSource, false, null);
			int occlusionTexture2 = ShaderIDs.OcclusionTexture2;
			context.GetScreenSpaceTemporaryRT(cmd, occlusionTexture2, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear, FilterMode.Bilinear, 0, 0);
			cmd.BlitFullscreenTriangle(occlusionTexture, occlusionTexture2, propertySheet, 2 + occlusionSource, false, null);
			cmd.ReleaseTemporaryRT(occlusionTexture);
			cmd.BlitFullscreenTriangle(occlusionTexture2, this.m_Result, propertySheet, 4, false, null);
			cmd.ReleaseTemporaryRT(occlusionTexture2);
			if (context.IsDebugOverlayEnabled(DebugOverlay.AmbientOcclusion))
			{
				context.PushDebugOverlay(cmd, this.m_Result, propertySheet, 7);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000740C File Offset: 0x0000560C
		public void RenderAfterOpaque(PostProcessRenderContext context)
		{
			CommandBuffer command = context.command;
			command.BeginSample("Ambient Occlusion");
			this.Render(context, command, 0);
			command.SetGlobalTexture(ShaderIDs.SAOcclusionTexture, this.m_Result);
			command.BlitFullscreenTriangle(BuiltinRenderTextureType.None, BuiltinRenderTextureType.CameraTarget, this.m_PropertySheet, 5, RenderBufferLoadAction.Load, null);
			command.EndSample("Ambient Occlusion");
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00007478 File Offset: 0x00005678
		public void RenderAmbientOnly(PostProcessRenderContext context)
		{
			CommandBuffer command = context.command;
			command.BeginSample("Ambient Occlusion Render");
			this.Render(context, command, 1);
			command.EndSample("Ambient Occlusion Render");
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000074AC File Offset: 0x000056AC
		public void CompositeAmbientOnly(PostProcessRenderContext context)
		{
			CommandBuffer command = context.command;
			command.BeginSample("Ambient Occlusion Composite");
			command.SetGlobalTexture(ShaderIDs.SAOcclusionTexture, this.m_Result);
			command.BlitFullscreenTriangle(BuiltinRenderTextureType.None, this.m_MRT, BuiltinRenderTextureType.CameraTarget, this.m_PropertySheet, 6, false, null);
			command.EndSample("Ambient Occlusion Composite");
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00007513 File Offset: 0x00005713
		public void Release()
		{
			RuntimeUtilities.Destroy(this.m_Result);
			this.m_Result = null;
		}

		// Token: 0x040000A4 RID: 164
		private RenderTexture m_Result;

		// Token: 0x040000A5 RID: 165
		private PropertySheet m_PropertySheet;

		// Token: 0x040000A6 RID: 166
		private AmbientOcclusion m_Settings;

		// Token: 0x040000A7 RID: 167
		private readonly RenderTargetIdentifier[] m_MRT = new RenderTargetIdentifier[]
		{
			BuiltinRenderTextureType.GBuffer0,
			BuiltinRenderTextureType.CameraTarget
		};

		// Token: 0x040000A8 RID: 168
		private readonly int[] m_SampleCount = new int[] { 4, 6, 10, 8, 12 };

		// Token: 0x02000070 RID: 112
		private enum Pass
		{
			// Token: 0x0400027D RID: 637
			OcclusionEstimationForward,
			// Token: 0x0400027E RID: 638
			OcclusionEstimationDeferred,
			// Token: 0x0400027F RID: 639
			HorizontalBlurForward,
			// Token: 0x04000280 RID: 640
			HorizontalBlurDeferred,
			// Token: 0x04000281 RID: 641
			VerticalBlur,
			// Token: 0x04000282 RID: 642
			CompositionForward,
			// Token: 0x04000283 RID: 643
			CompositionDeferred,
			// Token: 0x04000284 RID: 644
			DebugOverlay
		}
	}
}
