using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000033 RID: 51
	[Preserve]
	[Serializable]
	public sealed class TemporalAntialiasing
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00007FFC File Offset: 0x000061FC
		// (set) Token: 0x06000089 RID: 137 RVA: 0x00008004 File Offset: 0x00006204
		public Vector2 jitter { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600008A RID: 138 RVA: 0x0000800D File Offset: 0x0000620D
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00008015 File Offset: 0x00006215
		public int sampleIndex { get; private set; }

		// Token: 0x0600008C RID: 140 RVA: 0x0000801E File Offset: 0x0000621E
		public bool IsSupported()
		{
			return SystemInfo.supportedRenderTargetCount >= 2 && SystemInfo.supportsMotionVectors && SystemInfo.graphicsDeviceType != GraphicsDeviceType.OpenGLES2;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00005ABD File Offset: 0x00003CBD
		internal DepthTextureMode GetCameraFlags()
		{
			return DepthTextureMode.Depth | DepthTextureMode.MotionVectors;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000803C File Offset: 0x0000623C
		internal void ResetHistory()
		{
			this.m_ResetHistory = true;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00008048 File Offset: 0x00006248
		private Vector2 GenerateRandomOffset()
		{
			Vector2 vector = new Vector2(HaltonSeq.Get((this.sampleIndex & 1023) + 1, 2) - 0.5f, HaltonSeq.Get((this.sampleIndex & 1023) + 1, 3) - 0.5f);
			int num = this.sampleIndex + 1;
			this.sampleIndex = num;
			if (num >= 8)
			{
				this.sampleIndex = 0;
			}
			return vector;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000080AC File Offset: 0x000062AC
		public Matrix4x4 GetJitteredProjectionMatrix(Camera camera)
		{
			this.jitter = this.GenerateRandomOffset();
			this.jitter *= this.jitterSpread;
			Matrix4x4 matrix4x;
			if (this.jitteredMatrixFunc != null)
			{
				matrix4x = this.jitteredMatrixFunc(camera, this.jitter);
			}
			else
			{
				matrix4x = (camera.orthographic ? RuntimeUtilities.GetJitteredOrthographicProjectionMatrix(camera, this.jitter) : RuntimeUtilities.GetJitteredPerspectiveProjectionMatrix(camera, this.jitter));
			}
			this.jitter = new Vector2(this.jitter.x / (float)camera.pixelWidth, this.jitter.y / (float)camera.pixelHeight);
			return matrix4x;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00008150 File Offset: 0x00006350
		public void ConfigureJitteredProjectionMatrix(PostProcessRenderContext context)
		{
			Camera camera = context.camera;
			camera.nonJitteredProjectionMatrix = camera.projectionMatrix;
			camera.projectionMatrix = this.GetJitteredProjectionMatrix(camera);
			camera.useJitteredProjectionMatrixForTransparentRendering = false;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00008184 File Offset: 0x00006384
		public void ConfigureStereoJitteredProjectionMatrices(PostProcessRenderContext context)
		{
			Camera camera = context.camera;
			this.jitter = this.GenerateRandomOffset();
			this.jitter *= this.jitterSpread;
			for (Camera.StereoscopicEye stereoscopicEye = Camera.StereoscopicEye.Left; stereoscopicEye <= Camera.StereoscopicEye.Right; stereoscopicEye++)
			{
				context.camera.CopyStereoDeviceProjectionMatrixToNonJittered(stereoscopicEye);
				Matrix4x4 stereoNonJitteredProjectionMatrix = context.camera.GetStereoNonJitteredProjectionMatrix(stereoscopicEye);
				Matrix4x4 matrix4x = RuntimeUtilities.GenerateJitteredProjectionMatrixFromOriginal(context, stereoNonJitteredProjectionMatrix, this.jitter);
				context.camera.SetStereoProjectionMatrix(stereoscopicEye, matrix4x);
			}
			this.jitter = new Vector2(this.jitter.x / (float)context.screenWidth, this.jitter.y / (float)context.screenHeight);
			camera.useJitteredProjectionMatrixForTransparentRendering = false;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00008234 File Offset: 0x00006434
		private void GenerateHistoryName(RenderTexture rt, int id, PostProcessRenderContext context)
		{
			rt.name = "Temporal Anti-aliasing History id #" + id;
			if (context.stereoActive)
			{
				rt.name = rt.name + " for eye " + context.xrActiveEye;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00008280 File Offset: 0x00006480
		private RenderTexture CheckHistory(int id, PostProcessRenderContext context)
		{
			int xrActiveEye = context.xrActiveEye;
			if (this.m_HistoryTextures[xrActiveEye] == null)
			{
				this.m_HistoryTextures[xrActiveEye] = new RenderTexture[2];
			}
			RenderTexture renderTexture = this.m_HistoryTextures[xrActiveEye][id];
			if (this.m_ResetHistory || renderTexture == null || !renderTexture.IsCreated())
			{
				RenderTexture.ReleaseTemporary(renderTexture);
				renderTexture = context.GetScreenSpaceTemporaryRT(0, context.sourceFormat, RenderTextureReadWrite.Default, 0, 0);
				this.GenerateHistoryName(renderTexture, id, context);
				renderTexture.filterMode = FilterMode.Bilinear;
				this.m_HistoryTextures[xrActiveEye][id] = renderTexture;
				context.command.BlitFullscreenTriangle(context.source, renderTexture, false, null);
			}
			else if (renderTexture.width != context.width || renderTexture.height != context.height)
			{
				RenderTexture screenSpaceTemporaryRT = context.GetScreenSpaceTemporaryRT(0, context.sourceFormat, RenderTextureReadWrite.Default, 0, 0);
				this.GenerateHistoryName(screenSpaceTemporaryRT, id, context);
				screenSpaceTemporaryRT.filterMode = FilterMode.Bilinear;
				this.m_HistoryTextures[xrActiveEye][id] = screenSpaceTemporaryRT;
				context.command.BlitFullscreenTriangle(renderTexture, screenSpaceTemporaryRT, false, null);
				RenderTexture.ReleaseTemporary(renderTexture);
			}
			return this.m_HistoryTextures[xrActiveEye][id];
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000083A0 File Offset: 0x000065A0
		internal void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.temporalAntialiasing);
			CommandBuffer command = context.command;
			command.BeginSample("TemporalAntialiasing");
			int num = this.m_HistoryPingPong[context.xrActiveEye];
			RenderTexture renderTexture = this.CheckHistory(++num % 2, context);
			RenderTexture renderTexture2 = this.CheckHistory(++num % 2, context);
			this.m_HistoryPingPong[context.xrActiveEye] = (num + 1) % 2;
			propertySheet.properties.SetVector(ShaderIDs.Jitter, this.jitter);
			propertySheet.properties.SetFloat(ShaderIDs.Sharpness, this.sharpness);
			propertySheet.properties.SetVector(ShaderIDs.FinalBlendParameters, new Vector4(this.stationaryBlending, this.motionBlending, 6000f, 0f));
			propertySheet.properties.SetTexture(ShaderIDs.HistoryTex, renderTexture);
			int num2 = (context.camera.orthographic ? 1 : 0);
			this.m_Mrt[0] = context.destination;
			this.m_Mrt[1] = renderTexture2;
			command.BlitFullscreenTriangle(context.source, this.m_Mrt, context.source, propertySheet, num2, false, null);
			command.EndSample("TemporalAntialiasing");
			this.m_ResetHistory = false;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000084F8 File Offset: 0x000066F8
		internal void Release()
		{
			if (this.m_HistoryTextures != null)
			{
				for (int i = 0; i < this.m_HistoryTextures.Length; i++)
				{
					if (this.m_HistoryTextures[i] != null)
					{
						for (int j = 0; j < this.m_HistoryTextures[i].Length; j++)
						{
							RenderTexture.ReleaseTemporary(this.m_HistoryTextures[i][j]);
							this.m_HistoryTextures[i][j] = null;
						}
						this.m_HistoryTextures[i] = null;
					}
				}
			}
			this.sampleIndex = 0;
			this.m_HistoryPingPong[0] = 0;
			this.m_HistoryPingPong[1] = 0;
			this.ResetHistory();
		}

		// Token: 0x040000C2 RID: 194
		[Tooltip("The diameter (in texels) inside which jitter samples are spread. Smaller values result in crisper but more aliased output, while larger values result in more stable, but blurrier, output.")]
		[Range(0.1f, 1f)]
		public float jitterSpread = 0.75f;

		// Token: 0x040000C3 RID: 195
		[Tooltip("Controls the amount of sharpening applied to the color buffer. High values may introduce dark-border artifacts.")]
		[Range(0f, 3f)]
		public float sharpness = 0.25f;

		// Token: 0x040000C4 RID: 196
		[Tooltip("The blend coefficient for a stationary fragment. Controls the percentage of history sample blended into the final color.")]
		[Range(0f, 0.99f)]
		public float stationaryBlending = 0.95f;

		// Token: 0x040000C5 RID: 197
		[Tooltip("The blend coefficient for a fragment with significant motion. Controls the percentage of history sample blended into the final color.")]
		[Range(0f, 0.99f)]
		public float motionBlending = 0.85f;

		// Token: 0x040000C6 RID: 198
		public Func<Camera, Vector2, Matrix4x4> jitteredMatrixFunc;

		// Token: 0x040000C8 RID: 200
		private readonly RenderTargetIdentifier[] m_Mrt = new RenderTargetIdentifier[2];

		// Token: 0x040000C9 RID: 201
		private bool m_ResetHistory = true;

		// Token: 0x040000CA RID: 202
		private const int k_SampleCount = 8;

		// Token: 0x040000CC RID: 204
		private const int k_NumEyes = 2;

		// Token: 0x040000CD RID: 205
		private const int k_NumHistoryTextures = 2;

		// Token: 0x040000CE RID: 206
		private readonly RenderTexture[][] m_HistoryTextures = new RenderTexture[2][];

		// Token: 0x040000CF RID: 207
		private readonly int[] m_HistoryPingPong = new int[2];

		// Token: 0x02000075 RID: 117
		private enum Pass
		{
			// Token: 0x04000296 RID: 662
			SolverDilate,
			// Token: 0x04000297 RID: 663
			SolverNoDilate
		}
	}
}
