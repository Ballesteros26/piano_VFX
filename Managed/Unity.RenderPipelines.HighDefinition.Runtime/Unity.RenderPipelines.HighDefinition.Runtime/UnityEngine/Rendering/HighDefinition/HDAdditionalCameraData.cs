using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000F6 RID: 246
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@8.0/manual/HDRP-Camera.html")]
	[DisallowMultipleComponent]
	[ExecuteAlways]
	[RequireComponent(typeof(Camera))]
	public class HDAdditionalCameraData : MonoBehaviour, IVersionable<HDAdditionalCameraData.Version>, IFrameSettingsHistoryContainer, IDebugData
	{
		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0003F1E7 File Offset: 0x0003D3E7
		// (set) Token: 0x060007B6 RID: 1974 RVA: 0x0003F1EF File Offset: 0x0003D3EF
		HDAdditionalCameraData.Version IVersionable<HDAdditionalCameraData.Version>.version
		{
			get
			{
				return this.m_Version;
			}
			set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0003F1F8 File Offset: 0x0003D3F8
		private void Awake()
		{
			HDAdditionalCameraData.k_Migration.Migrate(this);
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060007B8 RID: 1976 RVA: 0x0003F214 File Offset: 0x0003D414
		// (remove) Token: 0x060007B9 RID: 1977 RVA: 0x0003F24C File Offset: 0x0003D44C
		public event Action<ScriptableRenderContext, HDCamera> customRender;

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0003F281 File Offset: 0x0003D481
		public bool hasCustomRender
		{
			get
			{
				return this.customRender != null;
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060007BB RID: 1979 RVA: 0x0003F28C File Offset: 0x0003D48C
		// (remove) Token: 0x060007BC RID: 1980 RVA: 0x0003F2C4 File Offset: 0x0003D4C4
		public event HDAdditionalCameraData.RequestAccessDelegate requestGraphicsBuffer;

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0003F2F9 File Offset: 0x0003D4F9
		public ref FrameSettings renderingPathCustomFrameSettings
		{
			get
			{
				return ref this.m_RenderingPathCustomFrameSettings;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x0003F301 File Offset: 0x0003D501
		bool IFrameSettingsHistoryContainer.hasCustomFrameSettings
		{
			get
			{
				return this.customRenderingSettings;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0003F309 File Offset: 0x0003D509
		FrameSettingsOverrideMask IFrameSettingsHistoryContainer.frameSettingsMask
		{
			get
			{
				return this.renderingPathCustomFrameSettingsOverrideMask;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0003F311 File Offset: 0x0003D511
		FrameSettings IFrameSettingsHistoryContainer.frameSettings
		{
			get
			{
				return this.m_RenderingPathCustomFrameSettings;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0003F319 File Offset: 0x0003D519
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x0003F321 File Offset: 0x0003D521
		FrameSettingsHistory IFrameSettingsHistoryContainer.frameSettingsHistory
		{
			get
			{
				return this.m_RenderingPathHistory;
			}
			set
			{
				this.m_RenderingPathHistory = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0003F32A File Offset: 0x0003D52A
		string IFrameSettingsHistoryContainer.panelName
		{
			get
			{
				return this.m_CameraRegisterName;
			}
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0003F332 File Offset: 0x0003D532
		Action IDebugData.GetReset()
		{
			return delegate
			{
				this.m_RenderingPathHistory.TriggerReset();
			};
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0003F340 File Offset: 0x0003D540
		public void SetAOVRequests(AOVRequestDataCollection aovRequests)
		{
			this.m_AOVRequestDataCollection = aovRequests;
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0003F34C File Offset: 0x0003D54C
		public IEnumerable<AOVRequestData> aovRequests
		{
			get
			{
				AOVRequestDataCollection aovrequestDataCollection;
				if ((aovrequestDataCollection = this.m_AOVRequestDataCollection) == null)
				{
					aovrequestDataCollection = (this.m_AOVRequestDataCollection = new AOVRequestDataCollection(null));
				}
				return aovrequestDataCollection;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x0003F372 File Offset: 0x0003D572
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x0003F37A File Offset: 0x0003D57A
		internal bool isEditorCameraPreview { get; set; }

		// Token: 0x060007C9 RID: 1993 RVA: 0x0003F384 File Offset: 0x0003D584
		public unsafe void CopyTo(HDAdditionalCameraData data)
		{
			data.clearColorMode = this.clearColorMode;
			data.backgroundColorHDR = this.backgroundColorHDR;
			data.clearDepth = this.clearDepth;
			data.customRenderingSettings = this.customRenderingSettings;
			data.volumeLayerMask = this.volumeLayerMask;
			data.volumeAnchorOverride = this.volumeAnchorOverride;
			data.antialiasing = this.antialiasing;
			data.dithering = this.dithering;
			this.physicalParameters.CopyTo(data.physicalParameters);
			*data.renderingPathCustomFrameSettings = *this.renderingPathCustomFrameSettings;
			data.renderingPathCustomFrameSettingsOverrideMask = this.renderingPathCustomFrameSettingsOverrideMask;
			data.defaultFrameSettings = this.defaultFrameSettings;
			data.probeCustomFixedExposure = this.probeCustomFixedExposure;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0003F43C File Offset: 0x0003D63C
		public Matrix4x4 GetNonObliqueProjection(Camera camera)
		{
			return this.nonObliqueProjectionGetter(camera);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0003F44A File Offset: 0x0003D64A
		private void RegisterDebug()
		{
			if (!this.m_IsDebugRegistered)
			{
				this.m_CameraRegisterName = base.name;
				if (this.m_Camera.cameraType != CameraType.Preview && this.m_Camera.cameraType != CameraType.Reflection)
				{
					DebugDisplaySettings.RegisterCamera(this);
				}
				this.m_IsDebugRegistered = true;
			}
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0003F48A File Offset: 0x0003D68A
		private void UnRegisterDebug()
		{
			if (this.m_IsDebugRegistered)
			{
				if (this.m_Camera.cameraType != CameraType.Preview)
				{
					Camera camera = this.m_Camera;
					if (camera == null || camera.cameraType != CameraType.Reflection)
					{
						DebugDisplaySettings.UnRegisterCamera(this);
					}
				}
				this.m_IsDebugRegistered = false;
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0003F4CA File Offset: 0x0003D6CA
		private void OnEnable()
		{
			this.m_Camera = base.GetComponent<Camera>();
			if (this.m_Camera == null)
			{
				return;
			}
			this.m_Camera.allowMSAA = false;
			this.m_Camera.allowHDR = false;
			this.RegisterDebug();
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0003F505 File Offset: 0x0003D705
		private void UpdateDebugCameraName()
		{
			this.profilingSampler = new ProfilingSampler(HDUtils.ComputeCameraName(base.name));
			if (base.name != this.m_CameraRegisterName)
			{
				this.UnRegisterDebug();
				this.RegisterDebug();
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0003F53C File Offset: 0x0003D73C
		private void OnDisable()
		{
			this.UnRegisterDebug();
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0003F544 File Offset: 0x0003D744
		internal static void InitDefaultHDAdditionalCameraData(HDAdditionalCameraData cameraData)
		{
			Camera component = cameraData.gameObject.GetComponent<Camera>();
			cameraData.clearDepth = component.clearFlags != CameraClearFlags.Nothing;
			if (component.clearFlags == CameraClearFlags.Skybox)
			{
				cameraData.clearColorMode = HDAdditionalCameraData.ClearColorMode.Sky;
				return;
			}
			if (component.clearFlags == CameraClearFlags.Color)
			{
				cameraData.clearColorMode = HDAdditionalCameraData.ClearColorMode.Color;
				return;
			}
			cameraData.clearColorMode = HDAdditionalCameraData.ClearColorMode.None;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x0003F598 File Offset: 0x0003D798
		internal void ExecuteCustomRender(ScriptableRenderContext renderContext, HDCamera hdCamera)
		{
			if (this.customRender != null)
			{
				this.customRender(renderContext, hdCamera);
			}
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0003F5B0 File Offset: 0x0003D7B0
		internal HDAdditionalCameraData.BufferAccessType GetBufferAccess()
		{
			HDAdditionalCameraData.BufferAccess bufferAccess = default(HDAdditionalCameraData.BufferAccess);
			HDAdditionalCameraData.RequestAccessDelegate requestAccessDelegate = this.requestGraphicsBuffer;
			if (requestAccessDelegate != null)
			{
				requestAccessDelegate(ref bufferAccess);
			}
			return bufferAccess.bufferAccess;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x0003F5E0 File Offset: 0x0003D7E0
		public RTHandle GetGraphicsBuffer(HDAdditionalCameraData.BufferAccessType type)
		{
			HDCamera orCreate = HDCamera.GetOrCreate(this.m_Camera, 0);
			if ((type & HDAdditionalCameraData.BufferAccessType.Color) != (HDAdditionalCameraData.BufferAccessType)0)
			{
				return orCreate.GetCurrentFrameRT(0);
			}
			if ((type & HDAdditionalCameraData.BufferAccessType.Depth) != (HDAdditionalCameraData.BufferAccessType)0)
			{
				return orCreate.GetCurrentFrameRT(6);
			}
			if ((type & HDAdditionalCameraData.BufferAccessType.Normal) != (HDAdditionalCameraData.BufferAccessType)0)
			{
				return orCreate.GetCurrentFrameRT(5);
			}
			return null;
		}

		// Token: 0x0400085A RID: 2138
		[SerializeField]
		[FormerlySerializedAs("version")]
		private HDAdditionalCameraData.Version m_Version = MigrationDescription.LastVersion<HDAdditionalCameraData.Version>();

		// Token: 0x0400085B RID: 2139
		private static readonly MigrationDescription<HDAdditionalCameraData.Version, HDAdditionalCameraData> k_Migration = MigrationDescription.New<HDAdditionalCameraData.Version, HDAdditionalCameraData>(new MigrationStep<HDAdditionalCameraData.Version, HDAdditionalCameraData>[]
		{
			MigrationStep.New<HDAdditionalCameraData.Version, HDAdditionalCameraData>(HDAdditionalCameraData.Version.SeparatePassThrough, delegate(HDAdditionalCameraData data)
			{
				switch (data.m_ObsoleteRenderingPath)
				{
				case 0:
					data.fullscreenPassthrough = false;
					data.customRenderingSettings = false;
					return;
				case 1:
					data.fullscreenPassthrough = false;
					data.customRenderingSettings = true;
					return;
				case 2:
					data.fullscreenPassthrough = true;
					data.customRenderingSettings = false;
					return;
				default:
					return;
				}
			}),
			MigrationStep.New<HDAdditionalCameraData.Version, HDAdditionalCameraData>(HDAdditionalCameraData.Version.UpgradingFrameSettingsToStruct, delegate(HDAdditionalCameraData data)
			{
				if (data.m_ObsoleteFrameSettings != null)
				{
					FrameSettings.MigrateFromClassVersion(ref data.m_ObsoleteFrameSettings, data.renderingPathCustomFrameSettings, ref data.renderingPathCustomFrameSettingsOverrideMask);
				}
			}),
			MigrationStep.New<HDAdditionalCameraData.Version, HDAdditionalCameraData>(HDAdditionalCameraData.Version.AddAfterPostProcessFrameSetting, delegate(HDAdditionalCameraData data)
			{
				FrameSettings.MigrateToAfterPostprocess(data.renderingPathCustomFrameSettings);
			}),
			MigrationStep.New<HDAdditionalCameraData.Version, HDAdditionalCameraData>(HDAdditionalCameraData.Version.AddReflectionSettings, delegate(HDAdditionalCameraData data)
			{
				FrameSettings.MigrateToDefaultReflectionSettings(data.renderingPathCustomFrameSettings);
			}),
			MigrationStep.New<HDAdditionalCameraData.Version, HDAdditionalCameraData>(HDAdditionalCameraData.Version.AddCustomPostprocessAndCustomPass, delegate(HDAdditionalCameraData data)
			{
				FrameSettings.MigrateToCustomPostprocessAndCustomPass(data.renderingPathCustomFrameSettings);
			})
		});

		// Token: 0x0400085C RID: 2140
		[SerializeField]
		[FormerlySerializedAs("renderingPath")]
		[Obsolete("For Data Migration")]
		private int m_ObsoleteRenderingPath;

		// Token: 0x0400085D RID: 2141
		[SerializeField]
		[FormerlySerializedAs("serializedFrameSettings")]
		[FormerlySerializedAs("m_FrameSettings")]
		private ObsoleteFrameSettings m_ObsoleteFrameSettings;

		// Token: 0x0400085E RID: 2142
		private Camera m_Camera;

		// Token: 0x0400085F RID: 2143
		public HDAdditionalCameraData.ClearColorMode clearColorMode;

		// Token: 0x04000860 RID: 2144
		[ColorUsage(true, true)]
		public Color backgroundColorHDR = new Color(0.025f, 0.07f, 0.19f, 0f);

		// Token: 0x04000861 RID: 2145
		public bool clearDepth = true;

		// Token: 0x04000862 RID: 2146
		[Tooltip("LayerMask HDRP uses for Volume interpolation for this Camera.")]
		public LayerMask volumeLayerMask = 1;

		// Token: 0x04000863 RID: 2147
		public Transform volumeAnchorOverride;

		// Token: 0x04000864 RID: 2148
		public HDAdditionalCameraData.AntialiasingMode antialiasing;

		// Token: 0x04000865 RID: 2149
		public HDAdditionalCameraData.SMAAQualityLevel SMAAQuality = HDAdditionalCameraData.SMAAQualityLevel.High;

		// Token: 0x04000866 RID: 2150
		public bool dithering;

		// Token: 0x04000867 RID: 2151
		public bool stopNaNs;

		// Token: 0x04000868 RID: 2152
		[Range(0f, 2f)]
		public float taaSharpenStrength = 0.6f;

		// Token: 0x04000869 RID: 2153
		public HDPhysicalCamera physicalParameters = new HDPhysicalCamera();

		// Token: 0x0400086A RID: 2154
		public HDAdditionalCameraData.FlipYMode flipYMode;

		// Token: 0x0400086B RID: 2155
		[Tooltip("Skips rendering settings to directly render in fullscreen (Useful for video).")]
		public bool fullscreenPassthrough;

		// Token: 0x0400086C RID: 2156
		[Tooltip("Allows dynamic resolution on buffers linked to this camera.")]
		public bool allowDynamicResolution;

		// Token: 0x0400086D RID: 2157
		[Tooltip("Allows you to override the default settings for this camera.")]
		public bool customRenderingSettings;

		// Token: 0x0400086E RID: 2158
		public bool invertFaceCulling;

		// Token: 0x0400086F RID: 2159
		public LayerMask probeLayerMask = -1;

		// Token: 0x04000870 RID: 2160
		public bool hasPersistentHistory;

		// Token: 0x04000873 RID: 2163
		internal float probeCustomFixedExposure = 1f;

		// Token: 0x04000874 RID: 2164
		[SerializeField]
		[FormerlySerializedAs("renderingPathCustomFrameSettings")]
		private FrameSettings m_RenderingPathCustomFrameSettings = FrameSettings.NewDefaultCamera();

		// Token: 0x04000875 RID: 2165
		public FrameSettingsOverrideMask renderingPathCustomFrameSettingsOverrideMask;

		// Token: 0x04000876 RID: 2166
		public FrameSettingsRenderType defaultFrameSettings;

		// Token: 0x04000877 RID: 2167
		private FrameSettingsHistory m_RenderingPathHistory = new FrameSettingsHistory
		{
			defaultType = FrameSettingsRenderType.Camera
		};

		// Token: 0x04000878 RID: 2168
		internal ProfilingSampler profilingSampler;

		// Token: 0x04000879 RID: 2169
		private AOVRequestDataCollection m_AOVRequestDataCollection = new AOVRequestDataCollection(null);

		// Token: 0x0400087A RID: 2170
		private bool m_IsDebugRegistered;

		// Token: 0x0400087B RID: 2171
		private string m_CameraRegisterName;

		// Token: 0x0400087D RID: 2173
		public HDAdditionalCameraData.NonObliqueProjectionGetter nonObliqueProjectionGetter = new HDAdditionalCameraData.NonObliqueProjectionGetter(GeometryUtils.CalculateProjectionMatrix);

		// Token: 0x02000258 RID: 600
		protected enum Version
		{
			// Token: 0x04001596 RID: 5526
			None,
			// Token: 0x04001597 RID: 5527
			First,
			// Token: 0x04001598 RID: 5528
			SeparatePassThrough,
			// Token: 0x04001599 RID: 5529
			UpgradingFrameSettingsToStruct,
			// Token: 0x0400159A RID: 5530
			AddAfterPostProcessFrameSetting,
			// Token: 0x0400159B RID: 5531
			AddFrameSettingSpecularLighting,
			// Token: 0x0400159C RID: 5532
			AddReflectionSettings,
			// Token: 0x0400159D RID: 5533
			AddCustomPostprocessAndCustomPass
		}

		// Token: 0x02000259 RID: 601
		public enum FlipYMode
		{
			// Token: 0x0400159F RID: 5535
			Automatic,
			// Token: 0x040015A0 RID: 5536
			ForceFlipY
		}

		// Token: 0x0200025A RID: 602
		[Flags]
		public enum BufferAccessType
		{
			// Token: 0x040015A2 RID: 5538
			Depth = 1,
			// Token: 0x040015A3 RID: 5539
			Normal = 2,
			// Token: 0x040015A4 RID: 5540
			Color = 4
		}

		// Token: 0x0200025B RID: 603
		public struct BufferAccess
		{
			// Token: 0x06000C4C RID: 3148 RVA: 0x0005925A File Offset: 0x0005745A
			internal void Reset()
			{
				this.bufferAccess = (HDAdditionalCameraData.BufferAccessType)0;
			}

			// Token: 0x06000C4D RID: 3149 RVA: 0x00059263 File Offset: 0x00057463
			public void RequestAccess(HDAdditionalCameraData.BufferAccessType flags)
			{
				this.bufferAccess |= flags;
			}

			// Token: 0x040015A5 RID: 5541
			internal HDAdditionalCameraData.BufferAccessType bufferAccess;
		}

		// Token: 0x0200025C RID: 604
		// (Invoke) Token: 0x06000C4F RID: 3151
		public delegate Matrix4x4 NonObliqueProjectionGetter(Camera camera);

		// Token: 0x0200025D RID: 605
		public enum ClearColorMode
		{
			// Token: 0x040015A7 RID: 5543
			Sky,
			// Token: 0x040015A8 RID: 5544
			Color,
			// Token: 0x040015A9 RID: 5545
			None
		}

		// Token: 0x0200025E RID: 606
		public enum AntialiasingMode
		{
			// Token: 0x040015AB RID: 5547
			None,
			// Token: 0x040015AC RID: 5548
			FastApproximateAntialiasing,
			// Token: 0x040015AD RID: 5549
			TemporalAntialiasing,
			// Token: 0x040015AE RID: 5550
			SubpixelMorphologicalAntiAliasing
		}

		// Token: 0x0200025F RID: 607
		public enum SMAAQualityLevel
		{
			// Token: 0x040015B0 RID: 5552
			Low,
			// Token: 0x040015B1 RID: 5553
			Medium,
			// Token: 0x040015B2 RID: 5554
			High
		}

		// Token: 0x02000260 RID: 608
		// (Invoke) Token: 0x06000C53 RID: 3155
		public delegate void RequestAccessDelegate(ref HDAdditionalCameraData.BufferAccess bufferAccess);
	}
}
