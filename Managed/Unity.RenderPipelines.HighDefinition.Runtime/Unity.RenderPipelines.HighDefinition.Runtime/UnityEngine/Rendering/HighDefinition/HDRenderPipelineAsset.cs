using System;
using System.Collections.Generic;
using UnityEngine.Serialization;
using Utilities;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000102 RID: 258
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@8.0/manual/HDRP-Asset.html")]
	public class HDRenderPipelineAsset : RenderPipelineAsset, IVersionable<HDRenderPipelineAsset.Version>
	{
		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0004207C File Offset: 0x0004027C
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x00042084 File Offset: 0x00040284
		HDRenderPipelineAsset.Version IVersionable<HDRenderPipelineAsset.Version>.version
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

		// Token: 0x06000847 RID: 2119 RVA: 0x00042090 File Offset: 0x00040290
		private void Awake()
		{
			HDRenderPipelineAsset.k_Migration.Migrate(this);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x000420AC File Offset: 0x000402AC
		private HDRenderPipelineAsset()
		{
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0004213F File Offset: 0x0004033F
		private void Reset()
		{
			this.OnValidate();
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x00042147 File Offset: 0x00040347
		protected override RenderPipeline CreatePipeline()
		{
			return new HDRenderPipeline(this, HDRenderPipeline.defaultAsset);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x00042154 File Offset: 0x00040354
		protected override void OnValidate()
		{
			if (GraphicsSettings.currentRenderPipeline == this)
			{
				base.OnValidate();
			}
			this.UpdateRenderingLayerNames();
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0004216F File Offset: 0x0004036F
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x00042177 File Offset: 0x00040377
		internal RenderPipelineResources renderPipelineResources
		{
			get
			{
				return this.m_RenderPipelineResources;
			}
			set
			{
				this.m_RenderPipelineResources = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00042180 File Offset: 0x00040380
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x00042188 File Offset: 0x00040388
		internal HDRenderPipelineRayTracingResources renderPipelineRayTracingResources
		{
			get
			{
				return this.m_RenderPipelineRayTracingResources;
			}
			set
			{
				this.m_RenderPipelineRayTracingResources = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00042191 File Offset: 0x00040391
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x00042199 File Offset: 0x00040399
		internal VolumeProfile defaultVolumeProfile
		{
			get
			{
				return this.m_DefaultVolumeProfile;
			}
			set
			{
				this.m_DefaultVolumeProfile = value;
			}
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x000421A2 File Offset: 0x000403A2
		internal ref FrameSettings GetDefaultFrameSettings(FrameSettingsRenderType type)
		{
			switch (type)
			{
			case FrameSettingsRenderType.Camera:
				return ref this.m_RenderingPathDefaultCameraFrameSettings;
			case FrameSettingsRenderType.CustomOrBakedReflection:
				return ref this.m_RenderingPathDefaultBakedOrCustomReflectionFrameSettings;
			case FrameSettingsRenderType.RealtimeReflection:
				return ref this.m_RenderingPathDefaultRealtimeReflectionFrameSettings;
			default:
				throw new ArgumentException("Unknown FrameSettingsRenderType");
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x000421D7 File Offset: 0x000403D7
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x000421DF File Offset: 0x000403DF
		internal bool frameSettingsHistory { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x000421E8 File Offset: 0x000403E8
		internal ReflectionSystemParameters reflectionSystemParameters
		{
			get
			{
				return new ReflectionSystemParameters
				{
					maxPlanarReflectionProbePerCamera = this.currentPlatformRenderPipelineSettings.lightLoopSettings.maxPlanarReflectionOnScreen,
					maxActivePlanarReflectionProbe = 512,
					planarReflectionProbeSize = 512,
					maxActiveReflectionProbe = 512,
					reflectionProbeSize = (int)this.currentPlatformRenderPipelineSettings.lightLoopSettings.reflectionCubemapSize
				};
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00042250 File Offset: 0x00040450
		public RenderPipelineSettings currentPlatformRenderPipelineSettings
		{
			get
			{
				return this.m_RenderPipelineSettings;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x00042258 File Offset: 0x00040458
		public MaterialQuality defaultMaterialQualityLevel
		{
			get
			{
				return this.m_DefaultMaterialQualityLevel;
			}
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00042260 File Offset: 0x00040460
		private void UpdateRenderingLayerNames()
		{
			this.m_RenderingLayerNames = new string[32];
			this.m_RenderingLayerNames[0] = this.m_RenderPipelineSettings.lightLayerName0;
			this.m_RenderingLayerNames[1] = this.m_RenderPipelineSettings.lightLayerName1;
			this.m_RenderingLayerNames[2] = this.m_RenderPipelineSettings.lightLayerName2;
			this.m_RenderingLayerNames[3] = this.m_RenderPipelineSettings.lightLayerName3;
			this.m_RenderingLayerNames[4] = this.m_RenderPipelineSettings.lightLayerName4;
			this.m_RenderingLayerNames[5] = this.m_RenderPipelineSettings.lightLayerName5;
			this.m_RenderingLayerNames[6] = this.m_RenderPipelineSettings.lightLayerName6;
			this.m_RenderingLayerNames[7] = this.m_RenderPipelineSettings.lightLayerName7;
			for (int i = 8; i < this.m_RenderingLayerNames.Length; i++)
			{
				this.m_RenderingLayerNames[i] = string.Format("Unused {0}", i);
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0004233D File Offset: 0x0004053D
		private string[] renderingLayerNames
		{
			get
			{
				if (this.m_RenderingLayerNames == null)
				{
					this.UpdateRenderingLayerNames();
				}
				return this.m_RenderingLayerNames;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x00042353 File Offset: 0x00040553
		public override string[] renderingLayerMaskNames
		{
			get
			{
				return this.renderingLayerNames;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x0004235C File Offset: 0x0004055C
		public string[] lightLayerNames
		{
			get
			{
				if (this.m_LightLayerNames == null)
				{
					this.m_LightLayerNames = new string[8];
				}
				for (int i = 0; i < 8; i++)
				{
					this.m_LightLayerNames[i] = this.renderingLayerNames[i];
				}
				return this.m_LightLayerNames;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x0004239F File Offset: 0x0004059F
		public override Shader defaultShader
		{
			get
			{
				RenderPipelineResources renderPipelineResources = this.m_RenderPipelineResources;
				if (renderPipelineResources == null)
				{
					return null;
				}
				return renderPipelineResources.shaders.defaultPS;
			}
		}

		// Token: 0x040009A0 RID: 2464
		private static readonly MigrationDescription<HDRenderPipelineAsset.Version, HDRenderPipelineAsset> k_Migration = MigrationDescription.New<HDRenderPipelineAsset.Version, HDRenderPipelineAsset>(new MigrationStep<HDRenderPipelineAsset.Version, HDRenderPipelineAsset>[]
		{
			MigrationStep.New<HDRenderPipelineAsset.Version, HDRenderPipelineAsset>(HDRenderPipelineAsset.Version.UpgradeFrameSettingsToStruct, delegate(HDRenderPipelineAsset data)
			{
				FrameSettingsOverrideMask frameSettingsOverrideMask = default(FrameSettingsOverrideMask);
				if (data.m_ObsoleteFrameSettings != null)
				{
					FrameSettings.MigrateFromClassVersion(ref data.m_ObsoleteFrameSettings, ref data.m_RenderingPathDefaultCameraFrameSettings, ref frameSettingsOverrideMask);
				}
				if (data.m_ObsoleteBakedOrCustomReflectionFrameSettings != null)
				{
					FrameSettings.MigrateFromClassVersion(ref data.m_ObsoleteBakedOrCustomReflectionFrameSettings, ref data.m_RenderingPathDefaultBakedOrCustomReflectionFrameSettings, ref frameSettingsOverrideMask);
				}
				if (data.m_ObsoleteRealtimeReflectionFrameSettings != null)
				{
					FrameSettings.MigrateFromClassVersion(ref data.m_ObsoleteRealtimeReflectionFrameSettings, ref data.m_RenderingPathDefaultRealtimeReflectionFrameSettings, ref frameSettingsOverrideMask);
				}
			}),
			MigrationStep.New<HDRenderPipelineAsset.Version, HDRenderPipelineAsset>(HDRenderPipelineAsset.Version.AddAfterPostProcessFrameSetting, delegate(HDRenderPipelineAsset data)
			{
				FrameSettings.MigrateToAfterPostprocess(ref data.m_RenderingPathDefaultCameraFrameSettings);
			}),
			MigrationStep.New<HDRenderPipelineAsset.Version, HDRenderPipelineAsset>(HDRenderPipelineAsset.Version.AddReflectionSettings, delegate(HDRenderPipelineAsset data)
			{
				FrameSettings.MigrateToDefaultReflectionSettings(ref data.m_RenderingPathDefaultCameraFrameSettings);
				FrameSettings.MigrateToNoReflectionSettings(ref data.m_RenderingPathDefaultBakedOrCustomReflectionFrameSettings);
				FrameSettings.MigrateToNoReflectionRealtimeSettings(ref data.m_RenderingPathDefaultRealtimeReflectionFrameSettings);
			}),
			MigrationStep.New<HDRenderPipelineAsset.Version, HDRenderPipelineAsset>(HDRenderPipelineAsset.Version.AddPostProcessFrameSettings, delegate(HDRenderPipelineAsset data)
			{
				FrameSettings.MigrateToPostProcess(ref data.m_RenderingPathDefaultCameraFrameSettings);
			}),
			MigrationStep.New<HDRenderPipelineAsset.Version, HDRenderPipelineAsset>(HDRenderPipelineAsset.Version.AddRayTracingFrameSettings, delegate(HDRenderPipelineAsset data)
			{
				FrameSettings.MigrateToRayTracing(ref data.m_RenderingPathDefaultCameraFrameSettings);
			}),
			MigrationStep.New<HDRenderPipelineAsset.Version, HDRenderPipelineAsset>(HDRenderPipelineAsset.Version.AddFrameSettingDirectSpecularLighting, delegate(HDRenderPipelineAsset data)
			{
				FrameSettings.MigrateToDirectSpecularLighting(ref data.m_RenderingPathDefaultCameraFrameSettings);
				FrameSettings.MigrateToNoDirectSpecularLighting(ref data.m_RenderingPathDefaultBakedOrCustomReflectionFrameSettings);
				FrameSettings.MigrateToDirectSpecularLighting(ref data.m_RenderingPathDefaultRealtimeReflectionFrameSettings);
			}),
			MigrationStep.New<HDRenderPipelineAsset.Version, HDRenderPipelineAsset>(HDRenderPipelineAsset.Version.AddCustomPostprocessAndCustomPass, delegate(HDRenderPipelineAsset data)
			{
				FrameSettings.MigrateToCustomPostprocessAndCustomPass(ref data.m_RenderingPathDefaultCameraFrameSettings);
			}),
			MigrationStep.New<HDRenderPipelineAsset.Version, HDRenderPipelineAsset>(HDRenderPipelineAsset.Version.ScalableSettingsRefactor, delegate(HDRenderPipelineAsset data)
			{
				data.m_RenderPipelineSettings.hdShadowInitParams.shadowResolutionArea.schemaId = ScalableSettingSchemaId.With4Levels;
				data.m_RenderPipelineSettings.hdShadowInitParams.shadowResolutionDirectional.schemaId = ScalableSettingSchemaId.With4Levels;
				data.m_RenderPipelineSettings.hdShadowInitParams.shadowResolutionPunctual.schemaId = ScalableSettingSchemaId.With4Levels;
			}),
			MigrationStep.New<HDRenderPipelineAsset.Version, HDRenderPipelineAsset>(HDRenderPipelineAsset.Version.ShadowFilteringVeryHighQualityRemoval, delegate(HDRenderPipelineAsset data)
			{
				ref HDShadowInitParameters ptr = ref data.m_RenderPipelineSettings.hdShadowInitParams;
				ptr.shadowFilteringQuality = ((ptr.shadowFilteringQuality > HDShadowFilteringQuality.High) ? HDShadowFilteringQuality.High : ptr.shadowFilteringQuality);
			}),
			MigrationStep.New<HDRenderPipelineAsset.Version, HDRenderPipelineAsset>(HDRenderPipelineAsset.Version.SeparateColorGradingAndTonemappingFrameSettings, delegate(HDRenderPipelineAsset data)
			{
				FrameSettings.MigrateToSeparateColorGradingAndTonemapping(ref data.m_RenderingPathDefaultCameraFrameSettings);
			}),
			MigrationStep.New<HDRenderPipelineAsset.Version, HDRenderPipelineAsset>(HDRenderPipelineAsset.Version.ReplaceTextureArraysByAtlasForCookieAndPlanar, delegate(HDRenderPipelineAsset data)
			{
				ref GlobalLightLoopSettings ptr2 = ref data.m_RenderPipelineSettings.lightLoopSettings;
				int num = (int)(ptr2.cookieAtlasSize * (CookieAtlasResolution)ptr2.cookieTexArraySize);
				int num2 = (int)(ptr2.planarReflectionAtlasSize * (PlanarReflectionAtlasResolution)ptr2.maxPlanarReflectionOnScreen);
				num = Mathf.ClosestPowerOfTwo(num);
				num2 = Mathf.ClosestPowerOfTwo(num2);
				num = Mathf.Clamp(num, 256, 8192);
				num2 = Mathf.Clamp(num2, 256, 8192);
				ptr2.cookieAtlasSize = (CookieAtlasResolution)num;
				ptr2.planarReflectionAtlasSize = (PlanarReflectionAtlasResolution)num2;
			})
		});

		// Token: 0x040009A1 RID: 2465
		[SerializeField]
		private HDRenderPipelineAsset.Version m_Version = MigrationDescription.LastVersion<HDRenderPipelineAsset.Version>();

		// Token: 0x040009A2 RID: 2466
		[SerializeField]
		[FormerlySerializedAs("serializedFrameSettings")]
		[FormerlySerializedAs("m_FrameSettings")]
		[Obsolete("For data migration")]
		private ObsoleteFrameSettings m_ObsoleteFrameSettings;

		// Token: 0x040009A3 RID: 2467
		[SerializeField]
		[FormerlySerializedAs("m_BakedOrCustomReflectionFrameSettings")]
		[Obsolete("For data migration")]
		private ObsoleteFrameSettings m_ObsoleteBakedOrCustomReflectionFrameSettings;

		// Token: 0x040009A4 RID: 2468
		[SerializeField]
		[FormerlySerializedAs("m_RealtimeReflectionFrameSettings")]
		[Obsolete("For data migration")]
		private ObsoleteFrameSettings m_ObsoleteRealtimeReflectionFrameSettings;

		// Token: 0x040009A5 RID: 2469
		[SerializeField]
		private RenderPipelineResources m_RenderPipelineResources;

		// Token: 0x040009A6 RID: 2470
		[SerializeField]
		private HDRenderPipelineRayTracingResources m_RenderPipelineRayTracingResources;

		// Token: 0x040009A7 RID: 2471
		[SerializeField]
		private VolumeProfile m_DefaultVolumeProfile;

		// Token: 0x040009A8 RID: 2472
		[SerializeField]
		private FrameSettings m_RenderingPathDefaultCameraFrameSettings = FrameSettings.NewDefaultCamera();

		// Token: 0x040009A9 RID: 2473
		[SerializeField]
		private FrameSettings m_RenderingPathDefaultBakedOrCustomReflectionFrameSettings = FrameSettings.NewDefaultCustomOrBakeReflectionProbe();

		// Token: 0x040009AA RID: 2474
		[SerializeField]
		private FrameSettings m_RenderingPathDefaultRealtimeReflectionFrameSettings = FrameSettings.NewDefaultRealtimeReflectionProbe();

		// Token: 0x040009AC RID: 2476
		[SerializeField]
		[FormerlySerializedAs("renderPipelineSettings")]
		private RenderPipelineSettings m_RenderPipelineSettings = RenderPipelineSettings.NewDefault();

		// Token: 0x040009AD RID: 2477
		[SerializeField]
		internal bool allowShaderVariantStripping = true;

		// Token: 0x040009AE RID: 2478
		[SerializeField]
		internal bool enableSRPBatcher = true;

		// Token: 0x040009AF RID: 2479
		[SerializeField]
		internal ShaderVariantLogLevel shaderVariantLogLevel;

		// Token: 0x040009B0 RID: 2480
		[FormerlySerializedAs("materialQualityLevels")]
		public MaterialQuality availableMaterialQualityLevels = (MaterialQuality)(-1);

		// Token: 0x040009B1 RID: 2481
		[SerializeField]
		[FormerlySerializedAs("m_CurrentMaterialQualityLevel")]
		private MaterialQuality m_DefaultMaterialQualityLevel = MaterialQuality.High;

		// Token: 0x040009B2 RID: 2482
		[SerializeField]
		[Obsolete("Use diffusionProfileSettingsList instead")]
		internal DiffusionProfileSettings diffusionProfileSettings;

		// Token: 0x040009B3 RID: 2483
		[SerializeField]
		internal DiffusionProfileSettings[] diffusionProfileSettingsList = new DiffusionProfileSettings[0];

		// Token: 0x040009B4 RID: 2484
		[NonSerialized]
		private string[] m_RenderingLayerNames;

		// Token: 0x040009B5 RID: 2485
		[NonSerialized]
		private string[] m_LightLayerNames;

		// Token: 0x040009B6 RID: 2486
		[SerializeField]
		internal List<string> beforeTransparentCustomPostProcesses = new List<string>();

		// Token: 0x040009B7 RID: 2487
		[SerializeField]
		internal List<string> beforePostProcessCustomPostProcesses = new List<string>();

		// Token: 0x040009B8 RID: 2488
		[SerializeField]
		internal List<string> afterPostProcessCustomPostProcesses = new List<string>();

		// Token: 0x02000269 RID: 617
		private enum Version
		{
			// Token: 0x040015D7 RID: 5591
			None,
			// Token: 0x040015D8 RID: 5592
			First,
			// Token: 0x040015D9 RID: 5593
			UpgradeFrameSettingsToStruct,
			// Token: 0x040015DA RID: 5594
			AddAfterPostProcessFrameSetting,
			// Token: 0x040015DB RID: 5595
			AddFrameSettingSpecularLighting = 5,
			// Token: 0x040015DC RID: 5596
			AddReflectionSettings,
			// Token: 0x040015DD RID: 5597
			AddPostProcessFrameSettings,
			// Token: 0x040015DE RID: 5598
			AddRayTracingFrameSettings,
			// Token: 0x040015DF RID: 5599
			AddFrameSettingDirectSpecularLighting,
			// Token: 0x040015E0 RID: 5600
			AddCustomPostprocessAndCustomPass,
			// Token: 0x040015E1 RID: 5601
			ScalableSettingsRefactor,
			// Token: 0x040015E2 RID: 5602
			ShadowFilteringVeryHighQualityRemoval,
			// Token: 0x040015E3 RID: 5603
			SeparateColorGradingAndTonemappingFrameSettings,
			// Token: 0x040015E4 RID: 5604
			ReplaceTextureArraysByAtlasForCookieAndPlanar
		}
	}
}
