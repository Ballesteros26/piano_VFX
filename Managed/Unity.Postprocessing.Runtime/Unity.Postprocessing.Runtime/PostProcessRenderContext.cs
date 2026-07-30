using System;
using System.Collections.Generic;
using UnityEngine.XR;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000057 RID: 87
	public sealed class PostProcessRenderContext
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000CE6E File Offset: 0x0000B06E
		// (set) Token: 0x06000166 RID: 358 RVA: 0x0000CE78 File Offset: 0x0000B078
		public Camera camera
		{
			get
			{
				return this.m_Camera;
			}
			set
			{
				this.m_Camera = value;
				if (this.m_Camera.stereoEnabled)
				{
					RenderTextureDescriptor eyeTextureDesc = XRSettings.eyeTextureDesc;
					this.stereoRenderingMode = PostProcessRenderContext.StereoRenderingMode.SinglePass;
					this.numberOfEyes = 1;
					if (XRSettings.stereoRenderingMode == XRSettings.StereoRenderingMode.MultiPass)
					{
						this.stereoRenderingMode = PostProcessRenderContext.StereoRenderingMode.MultiPass;
					}
					if (eyeTextureDesc.dimension == TextureDimension.Tex2DArray)
					{
						this.stereoRenderingMode = PostProcessRenderContext.StereoRenderingMode.SinglePassInstanced;
					}
					if (this.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePassInstanced)
					{
						this.numberOfEyes = 2;
					}
					if (this.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePass)
					{
						this.numberOfEyes = 2;
						eyeTextureDesc.width /= 2;
						eyeTextureDesc.vrUsage = VRTextureUsage.None;
					}
					this.width = eyeTextureDesc.width;
					this.height = eyeTextureDesc.height;
					this.m_sourceDescriptor = eyeTextureDesc;
					if (this.m_Camera.stereoActiveEye == Camera.MonoOrStereoscopicEye.Right)
					{
						this.xrActiveEye = 1;
					}
					this.screenWidth = XRSettings.eyeTextureWidth;
					this.screenHeight = XRSettings.eyeTextureHeight;
					if (this.stereoRenderingMode == PostProcessRenderContext.StereoRenderingMode.SinglePass)
					{
						this.screenWidth /= 2;
					}
					this.stereoActive = true;
					return;
				}
				this.width = this.m_Camera.pixelWidth;
				this.height = this.m_Camera.pixelHeight;
				this.m_sourceDescriptor.width = this.width;
				this.m_sourceDescriptor.height = this.height;
				this.screenWidth = this.width;
				this.screenHeight = this.height;
				this.stereoActive = false;
				this.numberOfEyes = 1;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000CFDB File Offset: 0x0000B1DB
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000CFE3 File Offset: 0x0000B1E3
		public CommandBuffer command { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000CFEC File Offset: 0x0000B1EC
		// (set) Token: 0x0600016A RID: 362 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
		public RenderTargetIdentifier source { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600016B RID: 363 RVA: 0x0000CFFD File Offset: 0x0000B1FD
		// (set) Token: 0x0600016C RID: 364 RVA: 0x0000D005 File Offset: 0x0000B205
		public RenderTargetIdentifier destination { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600016D RID: 365 RVA: 0x0000D00E File Offset: 0x0000B20E
		// (set) Token: 0x0600016E RID: 366 RVA: 0x0000D016 File Offset: 0x0000B216
		public RenderTextureFormat sourceFormat { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000D01F File Offset: 0x0000B21F
		// (set) Token: 0x06000170 RID: 368 RVA: 0x0000D027 File Offset: 0x0000B227
		public bool flip { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000171 RID: 369 RVA: 0x0000D030 File Offset: 0x0000B230
		// (set) Token: 0x06000172 RID: 370 RVA: 0x0000D038 File Offset: 0x0000B238
		public PostProcessResources resources { get; internal set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000173 RID: 371 RVA: 0x0000D041 File Offset: 0x0000B241
		// (set) Token: 0x06000174 RID: 372 RVA: 0x0000D049 File Offset: 0x0000B249
		public PropertySheetFactory propertySheets { get; internal set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000175 RID: 373 RVA: 0x0000D052 File Offset: 0x0000B252
		// (set) Token: 0x06000176 RID: 374 RVA: 0x0000D05A File Offset: 0x0000B25A
		public Dictionary<string, object> userData { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000177 RID: 375 RVA: 0x0000D063 File Offset: 0x0000B263
		// (set) Token: 0x06000178 RID: 376 RVA: 0x0000D06B File Offset: 0x0000B26B
		public PostProcessDebugLayer debugLayer { get; internal set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000179 RID: 377 RVA: 0x0000D074 File Offset: 0x0000B274
		// (set) Token: 0x0600017A RID: 378 RVA: 0x0000D07C File Offset: 0x0000B27C
		public int width { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600017B RID: 379 RVA: 0x0000D085 File Offset: 0x0000B285
		// (set) Token: 0x0600017C RID: 380 RVA: 0x0000D08D File Offset: 0x0000B28D
		public int height { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000D096 File Offset: 0x0000B296
		// (set) Token: 0x0600017E RID: 382 RVA: 0x0000D09E File Offset: 0x0000B29E
		public bool stereoActive { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000D0A7 File Offset: 0x0000B2A7
		// (set) Token: 0x06000180 RID: 384 RVA: 0x0000D0AF File Offset: 0x0000B2AF
		public int xrActiveEye { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000D0B8 File Offset: 0x0000B2B8
		// (set) Token: 0x06000182 RID: 386 RVA: 0x0000D0C0 File Offset: 0x0000B2C0
		public int numberOfEyes { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000183 RID: 387 RVA: 0x0000D0C9 File Offset: 0x0000B2C9
		// (set) Token: 0x06000184 RID: 388 RVA: 0x0000D0D1 File Offset: 0x0000B2D1
		public PostProcessRenderContext.StereoRenderingMode stereoRenderingMode { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000D0DA File Offset: 0x0000B2DA
		// (set) Token: 0x06000186 RID: 390 RVA: 0x0000D0E2 File Offset: 0x0000B2E2
		public int screenWidth { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000187 RID: 391 RVA: 0x0000D0EB File Offset: 0x0000B2EB
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000D0F3 File Offset: 0x0000B2F3
		public int screenHeight { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000189 RID: 393 RVA: 0x0000D0FC File Offset: 0x0000B2FC
		// (set) Token: 0x0600018A RID: 394 RVA: 0x0000D104 File Offset: 0x0000B304
		public bool isSceneView { get; internal set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000D10D File Offset: 0x0000B30D
		// (set) Token: 0x0600018C RID: 396 RVA: 0x0000D115 File Offset: 0x0000B315
		public PostProcessLayer.Antialiasing antialiasing { get; internal set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000D11E File Offset: 0x0000B31E
		// (set) Token: 0x0600018E RID: 398 RVA: 0x0000D126 File Offset: 0x0000B326
		public TemporalAntialiasing temporalAntialiasing { get; internal set; }

		// Token: 0x0600018F RID: 399 RVA: 0x0000D130 File Offset: 0x0000B330
		public void Reset()
		{
			this.m_Camera = null;
			this.width = 0;
			this.height = 0;
			this.m_sourceDescriptor = new RenderTextureDescriptor(0, 0);
			this.physicalCamera = false;
			this.stereoActive = false;
			this.xrActiveEye = 0;
			this.screenWidth = 0;
			this.screenHeight = 0;
			this.command = null;
			this.source = 0;
			this.destination = 0;
			this.sourceFormat = RenderTextureFormat.ARGB32;
			this.flip = false;
			this.resources = null;
			this.propertySheets = null;
			this.debugLayer = null;
			this.isSceneView = false;
			this.antialiasing = PostProcessLayer.Antialiasing.None;
			this.temporalAntialiasing = null;
			this.uberSheet = null;
			this.autoExposureTexture = null;
			this.logLut = null;
			this.autoExposure = null;
			this.bloomBufferNameID = -1;
			if (this.userData == null)
			{
				this.userData = new Dictionary<string, object>();
			}
			this.userData.Clear();
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000D21A File Offset: 0x0000B41A
		public bool IsTemporalAntialiasingActive()
		{
			return this.antialiasing == PostProcessLayer.Antialiasing.TemporalAntialiasing && !this.isSceneView && this.temporalAntialiasing.IsSupported();
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000D23A File Offset: 0x0000B43A
		public bool IsDebugOverlayEnabled(DebugOverlay overlay)
		{
			return this.debugLayer.debugOverlay == overlay;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000D24A File Offset: 0x0000B44A
		public void PushDebugOverlay(CommandBuffer cmd, RenderTargetIdentifier source, PropertySheet sheet, int pass)
		{
			this.debugLayer.PushDebugOverlay(cmd, source, sheet, pass);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000D25C File Offset: 0x0000B45C
		internal RenderTextureDescriptor GetDescriptor(int depthBufferBits = 0, RenderTextureFormat colorFormat = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default)
		{
			RenderTextureDescriptor renderTextureDescriptor = new RenderTextureDescriptor(this.m_sourceDescriptor.width, this.m_sourceDescriptor.height, this.m_sourceDescriptor.colorFormat, depthBufferBits);
			renderTextureDescriptor.dimension = this.m_sourceDescriptor.dimension;
			renderTextureDescriptor.volumeDepth = this.m_sourceDescriptor.volumeDepth;
			renderTextureDescriptor.vrUsage = this.m_sourceDescriptor.vrUsage;
			renderTextureDescriptor.msaaSamples = this.m_sourceDescriptor.msaaSamples;
			renderTextureDescriptor.memoryless = this.m_sourceDescriptor.memoryless;
			renderTextureDescriptor.useMipMap = this.m_sourceDescriptor.useMipMap;
			renderTextureDescriptor.autoGenerateMips = this.m_sourceDescriptor.autoGenerateMips;
			renderTextureDescriptor.enableRandomWrite = this.m_sourceDescriptor.enableRandomWrite;
			renderTextureDescriptor.shadowSamplingMode = this.m_sourceDescriptor.shadowSamplingMode;
			if (this.m_Camera.allowDynamicResolution)
			{
				renderTextureDescriptor.useDynamicScale = true;
			}
			if (colorFormat != RenderTextureFormat.Default)
			{
				renderTextureDescriptor.colorFormat = colorFormat;
			}
			if (readWrite == RenderTextureReadWrite.sRGB)
			{
				renderTextureDescriptor.sRGB = true;
			}
			else if (readWrite == RenderTextureReadWrite.Linear)
			{
				renderTextureDescriptor.sRGB = false;
			}
			else if (readWrite == RenderTextureReadWrite.Default)
			{
				renderTextureDescriptor.sRGB = QualitySettings.activeColorSpace > ColorSpace.Gamma;
			}
			return renderTextureDescriptor;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000D384 File Offset: 0x0000B584
		public void GetScreenSpaceTemporaryRT(CommandBuffer cmd, int nameID, int depthBufferBits = 0, RenderTextureFormat colorFormat = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default, FilterMode filter = FilterMode.Bilinear, int widthOverride = 0, int heightOverride = 0)
		{
			RenderTextureDescriptor descriptor = this.GetDescriptor(depthBufferBits, colorFormat, readWrite);
			if (widthOverride > 0)
			{
				descriptor.width = widthOverride;
			}
			if (heightOverride > 0)
			{
				descriptor.height = heightOverride;
			}
			if (this.stereoActive && descriptor.dimension == TextureDimension.Tex2DArray)
			{
				descriptor.dimension = TextureDimension.Tex2D;
			}
			cmd.GetTemporaryRT(nameID, descriptor, filter);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000D3E0 File Offset: 0x0000B5E0
		public RenderTexture GetScreenSpaceTemporaryRT(int depthBufferBits = 0, RenderTextureFormat colorFormat = RenderTextureFormat.Default, RenderTextureReadWrite readWrite = RenderTextureReadWrite.Default, int widthOverride = 0, int heightOverride = 0)
		{
			RenderTextureDescriptor descriptor = this.GetDescriptor(depthBufferBits, colorFormat, readWrite);
			if (widthOverride > 0)
			{
				descriptor.width = widthOverride;
			}
			if (heightOverride > 0)
			{
				descriptor.height = heightOverride;
			}
			return RenderTexture.GetTemporary(descriptor);
		}

		// Token: 0x04000160 RID: 352
		private Camera m_Camera;

		// Token: 0x04000175 RID: 373
		internal PropertySheet uberSheet;

		// Token: 0x04000176 RID: 374
		internal Texture autoExposureTexture;

		// Token: 0x04000177 RID: 375
		internal LogHistogram logHistogram;

		// Token: 0x04000178 RID: 376
		internal Texture logLut;

		// Token: 0x04000179 RID: 377
		internal AutoExposure autoExposure;

		// Token: 0x0400017A RID: 378
		internal int bloomBufferNameID;

		// Token: 0x0400017B RID: 379
		internal bool physicalCamera;

		// Token: 0x0400017C RID: 380
		private RenderTextureDescriptor m_sourceDescriptor;

		// Token: 0x02000082 RID: 130
		public enum StereoRenderingMode
		{
			// Token: 0x040002B8 RID: 696
			MultiPass,
			// Token: 0x040002B9 RID: 697
			SinglePass,
			// Token: 0x040002BA RID: 698
			SinglePassInstanced,
			// Token: 0x040002BB RID: 699
			SinglePassMultiview
		}
	}
}
