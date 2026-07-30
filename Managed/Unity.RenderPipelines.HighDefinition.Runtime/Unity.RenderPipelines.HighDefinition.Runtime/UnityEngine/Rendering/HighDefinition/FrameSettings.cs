using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Utilities;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000134 RID: 308
	[DebuggerDisplay("{bitDatas.humanizedData}")]
	[DebuggerTypeProxy(typeof(FrameSettings.FrameSettingsDebugView))]
	[Serializable]
	public struct FrameSettings
	{
		// Token: 0x06000916 RID: 2326 RVA: 0x00049FF4 File Offset: 0x000481F4
		internal static void MigrateFromClassVersion(ref ObsoleteFrameSettings oldFrameSettingsFormat, ref FrameSettings newFrameSettingsFormat, ref FrameSettingsOverrideMask newFrameSettingsOverrideMask)
		{
			if (oldFrameSettingsFormat == null)
			{
				return;
			}
			ObsoleteLitShaderMode shaderLitMode = oldFrameSettingsFormat.shaderLitMode;
			if (shaderLitMode != ObsoleteLitShaderMode.Forward)
			{
				if (shaderLitMode != ObsoleteLitShaderMode.Deferred)
				{
					throw new ArgumentException("Unknown ObsoleteLitShaderMode");
				}
				newFrameSettingsFormat.litShaderMode = LitShaderMode.Deferred;
			}
			else
			{
				newFrameSettingsFormat.litShaderMode = LitShaderMode.Forward;
			}
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.ShadowMaps, oldFrameSettingsFormat.enableShadow);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.ContactShadows, oldFrameSettingsFormat.enableContactShadows);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.Shadowmask, oldFrameSettingsFormat.enableShadowMask);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.SSR, oldFrameSettingsFormat.enableSSR);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.SSAO, oldFrameSettingsFormat.enableSSAO);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.SubsurfaceScattering, oldFrameSettingsFormat.enableSubsurfaceScattering);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.Transmission, oldFrameSettingsFormat.enableTransmission);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.AtmosphericScattering, oldFrameSettingsFormat.enableAtmosphericScattering);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.Volumetrics, oldFrameSettingsFormat.enableVolumetrics);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.ReprojectionForVolumetrics, oldFrameSettingsFormat.enableReprojectionForVolumetrics);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.LightLayers, oldFrameSettingsFormat.enableLightLayers);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.DepthPrepassWithDeferredRendering, oldFrameSettingsFormat.enableDepthPrepassWithDeferredRendering);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.TransparentPrepass, oldFrameSettingsFormat.enableTransparentPrepass);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.MotionVectors, oldFrameSettingsFormat.enableMotionVectors);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.ObjectMotionVectors, oldFrameSettingsFormat.enableObjectMotionVectors);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.Decals, oldFrameSettingsFormat.enableDecals);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.Refraction, oldFrameSettingsFormat.enableRoughRefraction);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.TransparentPostpass, oldFrameSettingsFormat.enableTransparentPostpass);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.Distortion, oldFrameSettingsFormat.enableDistortion);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.Postprocess, oldFrameSettingsFormat.enablePostprocess);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.OpaqueObjects, oldFrameSettingsFormat.enableOpaqueObjects);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.TransparentObjects, oldFrameSettingsFormat.enableTransparentObjects);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.MSAA, oldFrameSettingsFormat.enableMSAA);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.ExposureControl, oldFrameSettingsFormat.enableExposureControl);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.AsyncCompute, oldFrameSettingsFormat.enableAsyncCompute);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.LightListAsync, oldFrameSettingsFormat.runLightListAsync);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.SSRAsync, oldFrameSettingsFormat.runSSRAsync);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.SSAOAsync, oldFrameSettingsFormat.runSSAOAsync);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.ContactShadowsAsync, oldFrameSettingsFormat.runContactShadowsAsync);
			newFrameSettingsFormat.SetEnabled(FrameSettingsField.VolumeVoxelizationsAsync, oldFrameSettingsFormat.runVolumeVoxelizationAsync);
			if (oldFrameSettingsFormat.lightLoopSettings != null)
			{
				newFrameSettingsFormat.SetEnabled(FrameSettingsField.DeferredTile, oldFrameSettingsFormat.lightLoopSettings.enableDeferredTileAndCluster);
				newFrameSettingsFormat.SetEnabled(FrameSettingsField.ComputeLightEvaluation, oldFrameSettingsFormat.lightLoopSettings.enableComputeLightEvaluation);
				newFrameSettingsFormat.SetEnabled(FrameSettingsField.ComputeLightVariants, oldFrameSettingsFormat.lightLoopSettings.enableComputeLightVariants);
				newFrameSettingsFormat.SetEnabled(FrameSettingsField.ComputeMaterialVariants, oldFrameSettingsFormat.lightLoopSettings.enableComputeMaterialVariants);
				newFrameSettingsFormat.SetEnabled(FrameSettingsField.FPTLForForwardOpaque, oldFrameSettingsFormat.lightLoopSettings.enableFptlForForwardOpaque);
				newFrameSettingsFormat.SetEnabled(FrameSettingsField.BigTilePrepass, oldFrameSettingsFormat.lightLoopSettings.enableBigTilePrepass);
			}
			newFrameSettingsOverrideMask.mask = default(BitArray128);
			foreach (object obj in Enum.GetValues(typeof(ObsoleteFrameSettingsOverrides)))
			{
				ObsoleteFrameSettingsOverrides obsoleteFrameSettingsOverrides = (ObsoleteFrameSettingsOverrides)obj;
				if ((obsoleteFrameSettingsOverrides & oldFrameSettingsFormat.overrides) > (ObsoleteFrameSettingsOverrides)0)
				{
					if (obsoleteFrameSettingsOverrides <= ObsoleteFrameSettingsOverrides.TransparentPostpass)
					{
						if (obsoleteFrameSettingsOverrides <= ObsoleteFrameSettingsOverrides.AtmosphericScaterring)
						{
							if (obsoleteFrameSettingsOverrides <= ObsoleteFrameSettingsOverrides.SSR)
							{
								if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.VolumeVoxelizationsAsync)
								{
									newFrameSettingsOverrideMask.mask[45U] = true;
									continue;
								}
								switch (obsoleteFrameSettingsOverrides)
								{
								case ObsoleteFrameSettingsOverrides.Shadow:
									newFrameSettingsOverrideMask.mask[20U] = true;
									continue;
								case ObsoleteFrameSettingsOverrides.ContactShadow:
									newFrameSettingsOverrideMask.mask[21U] = true;
									continue;
								case ObsoleteFrameSettingsOverrides.Shadow | ObsoleteFrameSettingsOverrides.ContactShadow:
									break;
								case ObsoleteFrameSettingsOverrides.ShadowMask:
									newFrameSettingsOverrideMask.mask[22U] = true;
									continue;
								default:
									if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.SSR)
									{
										newFrameSettingsOverrideMask.mask[23U] = true;
										continue;
									}
									break;
								}
							}
							else if (obsoleteFrameSettingsOverrides <= ObsoleteFrameSettingsOverrides.SubsurfaceScattering)
							{
								if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.SSAO)
								{
									newFrameSettingsOverrideMask.mask[24U] = true;
									continue;
								}
								if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.SubsurfaceScattering)
								{
									newFrameSettingsOverrideMask.mask[25U] = true;
									continue;
								}
							}
							else
							{
								if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.Transmission)
								{
									newFrameSettingsOverrideMask.mask[26U] = true;
									continue;
								}
								if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.AtmosphericScaterring)
								{
									newFrameSettingsOverrideMask.mask[27U] = true;
									continue;
								}
							}
						}
						else if (obsoleteFrameSettingsOverrides <= ObsoleteFrameSettingsOverrides.LightLayers)
						{
							if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.Volumetrics)
							{
								newFrameSettingsOverrideMask.mask[28U] = true;
								continue;
							}
							if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.ReprojectionForVolumetrics)
							{
								newFrameSettingsOverrideMask.mask[29U] = true;
								continue;
							}
							if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.LightLayers)
							{
								newFrameSettingsOverrideMask.mask[30U] = true;
								continue;
							}
						}
						else if (obsoleteFrameSettingsOverrides <= ObsoleteFrameSettingsOverrides.ExposureControl)
						{
							if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.MSAA)
							{
								newFrameSettingsOverrideMask.mask[31U] = true;
								continue;
							}
							if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.ExposureControl)
							{
								newFrameSettingsOverrideMask.mask[32U] = true;
								continue;
							}
						}
						else
						{
							if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.TransparentPrepass)
							{
								newFrameSettingsOverrideMask.mask[8U] = true;
								continue;
							}
							if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.TransparentPostpass)
							{
								newFrameSettingsOverrideMask.mask[9U] = true;
								continue;
							}
						}
					}
					else if (obsoleteFrameSettingsOverrides <= ObsoleteFrameSettingsOverrides.ShaderLitMode)
					{
						if (obsoleteFrameSettingsOverrides <= ObsoleteFrameSettingsOverrides.Decals)
						{
							if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.MotionVectors)
							{
								newFrameSettingsOverrideMask.mask[10U] = true;
								continue;
							}
							if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.ObjectMotionVectors)
							{
								newFrameSettingsOverrideMask.mask[11U] = true;
								continue;
							}
							if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.Decals)
							{
								newFrameSettingsOverrideMask.mask[12U] = true;
								continue;
							}
						}
						else if (obsoleteFrameSettingsOverrides <= ObsoleteFrameSettingsOverrides.Distortion)
						{
							if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.RoughRefraction)
							{
								newFrameSettingsOverrideMask.mask[13U] = true;
								continue;
							}
							if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.Distortion)
							{
								newFrameSettingsOverrideMask.mask[14U] = true;
								continue;
							}
						}
						else
						{
							if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.Postprocess)
							{
								newFrameSettingsOverrideMask.mask[15U] = true;
								continue;
							}
							if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.ShaderLitMode)
							{
								newFrameSettingsOverrideMask.mask[0U] = true;
								continue;
							}
						}
					}
					else if (obsoleteFrameSettingsOverrides <= ObsoleteFrameSettingsOverrides.TransparentObjects)
					{
						if (obsoleteFrameSettingsOverrides <= ObsoleteFrameSettingsOverrides.AsyncCompute)
						{
							if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.DepthPrepassWithDeferredRendering)
							{
								newFrameSettingsOverrideMask.mask[1U] = true;
								continue;
							}
							if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.AsyncCompute)
							{
								newFrameSettingsOverrideMask.mask[40U] = true;
								continue;
							}
						}
						else
						{
							if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.OpaqueObjects)
							{
								newFrameSettingsOverrideMask.mask[2U] = true;
								continue;
							}
							if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.TransparentObjects)
							{
								newFrameSettingsOverrideMask.mask[3U] = true;
								continue;
							}
						}
					}
					else if (obsoleteFrameSettingsOverrides <= ObsoleteFrameSettingsOverrides.SSRAsync)
					{
						if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.LightListAsync)
						{
							newFrameSettingsOverrideMask.mask[41U] = true;
							continue;
						}
						if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.SSRAsync)
						{
							newFrameSettingsOverrideMask.mask[42U] = true;
							continue;
						}
					}
					else
					{
						if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.SSAOAsync)
						{
							newFrameSettingsOverrideMask.mask[43U] = true;
							continue;
						}
						if (obsoleteFrameSettingsOverrides == ObsoleteFrameSettingsOverrides.ContactShadowsAsync)
						{
							newFrameSettingsOverrideMask.mask[44U] = true;
							continue;
						}
					}
					throw new ArgumentException("Unknown ObsoleteFrameSettingsOverride, was " + obsoleteFrameSettingsOverrides);
				}
			}
			if (oldFrameSettingsFormat.lightLoopSettings != null)
			{
				foreach (object obj2 in Enum.GetValues(typeof(ObsoleteLightLoopSettingsOverrides)))
				{
					ObsoleteLightLoopSettingsOverrides obsoleteLightLoopSettingsOverrides = (ObsoleteLightLoopSettingsOverrides)obj2;
					if ((obsoleteLightLoopSettingsOverrides & oldFrameSettingsFormat.lightLoopSettings.overrides) > (ObsoleteLightLoopSettingsOverrides)0)
					{
						if (obsoleteLightLoopSettingsOverrides <= ObsoleteLightLoopSettingsOverrides.ComputeLightVariants)
						{
							switch (obsoleteLightLoopSettingsOverrides)
							{
							case ObsoleteLightLoopSettingsOverrides.FptlForForwardOpaque:
								newFrameSettingsOverrideMask.mask[120U] = true;
								continue;
							case ObsoleteLightLoopSettingsOverrides.BigTilePrepass:
								newFrameSettingsOverrideMask.mask[121U] = true;
								continue;
							case ObsoleteLightLoopSettingsOverrides.FptlForForwardOpaque | ObsoleteLightLoopSettingsOverrides.BigTilePrepass:
								break;
							case ObsoleteLightLoopSettingsOverrides.ComputeLightEvaluation:
								newFrameSettingsOverrideMask.mask[123U] = true;
								continue;
							default:
								if (obsoleteLightLoopSettingsOverrides == ObsoleteLightLoopSettingsOverrides.ComputeLightVariants)
								{
									newFrameSettingsOverrideMask.mask[124U] = true;
									continue;
								}
								break;
							}
						}
						else
						{
							if (obsoleteLightLoopSettingsOverrides == ObsoleteLightLoopSettingsOverrides.ComputeMaterialVariants)
							{
								newFrameSettingsOverrideMask.mask[125U] = true;
								continue;
							}
							if (obsoleteLightLoopSettingsOverrides == ObsoleteLightLoopSettingsOverrides.TileAndCluster)
							{
								newFrameSettingsOverrideMask.mask[122U] = true;
								continue;
							}
						}
						throw new ArgumentException("Unknown ObsoleteLightLoopSettingsOverrides");
					}
				}
			}
			oldFrameSettingsFormat = null;
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0004A830 File Offset: 0x00048A30
		internal static void MigrateToCustomPostprocessAndCustomPass(ref FrameSettings cameraFrameSettings)
		{
			cameraFrameSettings.SetEnabled(FrameSettingsField.CustomPass, true);
			cameraFrameSettings.SetEnabled(FrameSettingsField.CustomPostProcess, true);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x0004A843 File Offset: 0x00048A43
		internal static void MigrateToAfterPostprocess(ref FrameSettings cameraFrameSettings)
		{
			cameraFrameSettings.SetEnabled(FrameSettingsField.AfterPostprocess, true);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x0004A84E File Offset: 0x00048A4E
		internal static void MigrateToDefaultReflectionSettings(ref FrameSettings cameraFrameSettings)
		{
			cameraFrameSettings.SetEnabled(FrameSettingsField.ReflectionProbe, true);
			cameraFrameSettings.SetEnabled(FrameSettingsField.PlanarProbe, true);
			cameraFrameSettings.SetEnabled(FrameSettingsField.ReplaceDiffuseForIndirect, false);
			cameraFrameSettings.SetEnabled(FrameSettingsField.SkyReflection, true);
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x0004A874 File Offset: 0x00048A74
		internal static void MigrateToNoReflectionRealtimeSettings(ref FrameSettings cameraFrameSettings)
		{
			cameraFrameSettings.SetEnabled(FrameSettingsField.ReflectionProbe, true);
			cameraFrameSettings.SetEnabled(FrameSettingsField.PlanarProbe, false);
			cameraFrameSettings.SetEnabled(FrameSettingsField.ReplaceDiffuseForIndirect, false);
			cameraFrameSettings.SetEnabled(FrameSettingsField.SkyReflection, true);
		}

		// Token: 0x0600091B RID: 2331 RVA: 0x0004A89A File Offset: 0x00048A9A
		internal static void MigrateToNoReflectionSettings(ref FrameSettings cameraFrameSettings)
		{
			cameraFrameSettings.SetEnabled(FrameSettingsField.ReflectionProbe, false);
			cameraFrameSettings.SetEnabled(FrameSettingsField.PlanarProbe, false);
			cameraFrameSettings.SetEnabled(FrameSettingsField.ReplaceDiffuseForIndirect, true);
			cameraFrameSettings.SetEnabled(FrameSettingsField.SkyReflection, false);
		}

		// Token: 0x0600091C RID: 2332 RVA: 0x0004A8C0 File Offset: 0x00048AC0
		internal static void MigrateToPostProcess(ref FrameSettings cameraFrameSettings)
		{
			cameraFrameSettings.SetEnabled(FrameSettingsField.StopNaN, true);
			cameraFrameSettings.SetEnabled(FrameSettingsField.DepthOfField, true);
			cameraFrameSettings.SetEnabled(FrameSettingsField.MotionBlur, true);
			cameraFrameSettings.SetEnabled(FrameSettingsField.PaniniProjection, true);
			cameraFrameSettings.SetEnabled(FrameSettingsField.Bloom, true);
			cameraFrameSettings.SetEnabled(FrameSettingsField.LensDistortion, true);
			cameraFrameSettings.SetEnabled(FrameSettingsField.ChromaticAberration, true);
			cameraFrameSettings.SetEnabled(FrameSettingsField.Vignette, true);
			cameraFrameSettings.SetEnabled(FrameSettingsField.ColorGrading, true);
			cameraFrameSettings.SetEnabled(FrameSettingsField.FilmGrain, true);
			cameraFrameSettings.SetEnabled(FrameSettingsField.Dithering, true);
			cameraFrameSettings.SetEnabled(FrameSettingsField.Antialiasing, true);
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0004A939 File Offset: 0x00048B39
		internal static void MigrateToDirectSpecularLighting(ref FrameSettings cameraFrameSettings)
		{
			cameraFrameSettings.SetEnabled(FrameSettingsField.DirectSpecularLighting, true);
		}

		// Token: 0x0600091E RID: 2334 RVA: 0x0004A944 File Offset: 0x00048B44
		internal static void MigrateToNoDirectSpecularLighting(ref FrameSettings cameraFrameSettings)
		{
			cameraFrameSettings.SetEnabled(FrameSettingsField.DirectSpecularLighting, false);
		}

		// Token: 0x0600091F RID: 2335 RVA: 0x0004A94F File Offset: 0x00048B4F
		internal static void MigrateToRayTracing(ref FrameSettings cameraFrameSettings)
		{
			cameraFrameSettings.SetEnabled(FrameSettingsField.RayTracing, true);
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x0004A95A File Offset: 0x00048B5A
		internal static void MigrateToSeparateColorGradingAndTonemapping(ref FrameSettings cameraFrameSettings)
		{
			cameraFrameSettings.SetEnabled(FrameSettingsField.Tonemapping, true);
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x0004A968 File Offset: 0x00048B68
		internal static FrameSettings NewDefaultCamera()
		{
			return new FrameSettings
			{
				bitDatas = new BitArray128(new uint[]
				{
					20U, 21U, 22U, 34U, 23U, 24U, 25U, 26U, 27U, 28U,
					29U, 30U, 32U, 0U, 8U, 9U, 6U, 10U, 11U, 12U,
					13U, 14U, 15U, 39U, 80U, 81U, 82U, 83U, 84U, 85U,
					86U, 87U, 88U, 93U, 89U, 90U, 91U, 17U, 18U, 19U,
					2U, 3U, 40U, 41U, 42U, 42U, 43U, 44U, 45U, 122U,
					123U, 124U, 125U, 120U, 121U, 16U, 33U, 35U, 37U, 38U,
					92U
				}),
				lodBias = 1f
			};
		}

		// Token: 0x06000922 RID: 2338 RVA: 0x0004A9A8 File Offset: 0x00048BA8
		internal static FrameSettings NewDefaultRealtimeReflectionProbe()
		{
			return new FrameSettings
			{
				bitDatas = new BitArray128(new uint[]
				{
					20U, 25U, 26U, 28U, 29U, 30U, 0U, 8U, 9U, 6U,
					10U, 11U, 12U, 2U, 3U, 40U, 41U, 42U, 42U, 43U,
					44U, 45U, 122U, 123U, 124U, 125U, 120U, 121U, 33U, 92U,
					38U
				}),
				lodBias = 1f
			};
		}

		// Token: 0x06000923 RID: 2339 RVA: 0x0004A9E8 File Offset: 0x00048BE8
		internal static FrameSettings NewDefaultCustomOrBakeReflectionProbe()
		{
			return new FrameSettings
			{
				bitDatas = new BitArray128(new uint[]
				{
					20U, 21U, 22U, 24U, 25U, 26U, 27U, 28U, 29U, 30U,
					0U, 8U, 9U, 6U, 12U, 13U, 14U, 2U, 3U, 40U,
					41U, 43U, 44U, 45U, 122U, 123U, 124U, 125U, 120U, 121U,
					36U
				}),
				lodBias = 1f
			};
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x0004AA28 File Offset: 0x00048C28
		// (set) Token: 0x06000925 RID: 2341 RVA: 0x0004AA3B File Offset: 0x00048C3B
		public LitShaderMode litShaderMode
		{
			get
			{
				if (!this.bitDatas[0U])
				{
					return LitShaderMode.Forward;
				}
				return LitShaderMode.Deferred;
			}
			set
			{
				this.bitDatas[0U] = value == LitShaderMode.Deferred;
			}
		}

		// Token: 0x06000926 RID: 2342 RVA: 0x0004AA4D File Offset: 0x00048C4D
		public bool IsEnabled(FrameSettingsField field)
		{
			return this.bitDatas[(uint)field];
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0004AA5B File Offset: 0x00048C5B
		public void SetEnabled(FrameSettingsField field, bool value)
		{
			this.bitDatas[(uint)field] = value;
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x0004AA6C File Offset: 0x00048C6C
		public float GetResolvedLODBias(HDRenderPipelineAsset hdrp)
		{
			FloatScalableSetting floatScalableSetting = hdrp.currentPlatformRenderPipelineSettings.lodBias;
			switch (this.lodBiasMode)
			{
			case LODBiasMode.FromQualitySettings:
				return floatScalableSetting[this.lodBiasQualityLevel];
			case LODBiasMode.ScaleQualitySettings:
				return this.lodBias * floatScalableSetting[this.lodBiasQualityLevel];
			case LODBiasMode.OverrideQualitySettings:
				return this.lodBias;
			default:
				throw new ArgumentOutOfRangeException("lodBiasMode");
			}
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0004AAD4 File Offset: 0x00048CD4
		public int GetResolvedMaximumLODLevel(HDRenderPipelineAsset hdrp)
		{
			IntScalableSetting intScalableSetting = hdrp.currentPlatformRenderPipelineSettings.maximumLODLevel;
			switch (this.maximumLODLevelMode)
			{
			case MaximumLODLevelMode.FromQualitySettings:
				return intScalableSetting[this.maximumLODLevelQualityLevel];
			case MaximumLODLevelMode.OffsetQualitySettings:
				return intScalableSetting[this.maximumLODLevelQualityLevel] + this.maximumLODLevel;
			case MaximumLODLevelMode.OverrideQualitySettings:
				return this.maximumLODLevel;
			default:
				throw new ArgumentOutOfRangeException("maximumLODLevelMode");
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0004AB3A File Offset: 0x00048D3A
		internal bool fptl
		{
			get
			{
				return this.litShaderMode == LitShaderMode.Deferred || this.bitDatas[120U];
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0004AB54 File Offset: 0x00048D54
		internal float specularGlobalDimmer
		{
			get
			{
				if (!this.bitDatas[38U])
				{
					return 0f;
				}
				return 1f;
			}
		}

		// Token: 0x0600092C RID: 2348 RVA: 0x0004AB70 File Offset: 0x00048D70
		internal bool BuildLightListRunsAsync()
		{
			return SystemInfo.supportsAsyncCompute && this.bitDatas[40U] && this.bitDatas[41U];
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0004AB97 File Offset: 0x00048D97
		internal bool SSRRunsAsync()
		{
			return SystemInfo.supportsAsyncCompute && this.bitDatas[40U] && this.bitDatas[42U];
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0004ABBE File Offset: 0x00048DBE
		internal bool SSAORunsAsync()
		{
			return SystemInfo.supportsAsyncCompute && this.bitDatas[40U] && this.bitDatas[43U];
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0004ABE5 File Offset: 0x00048DE5
		internal bool ContactShadowsRunAsync()
		{
			return SystemInfo.supportsAsyncCompute && this.bitDatas[40U] && false;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0004AC00 File Offset: 0x00048E00
		internal bool VolumeVoxelizationRunsAsync()
		{
			return SystemInfo.supportsAsyncCompute && this.bitDatas[40U] && this.bitDatas[45U];
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0004AC28 File Offset: 0x00048E28
		internal static void Override(ref FrameSettings overriddenFrameSettings, FrameSettings overridingFrameSettings, FrameSettingsOverrideMask frameSettingsOverideMask)
		{
			overriddenFrameSettings.bitDatas = (overridingFrameSettings.bitDatas & frameSettingsOverideMask.mask) | (~frameSettingsOverideMask.mask & overriddenFrameSettings.bitDatas);
			if (frameSettingsOverideMask.mask[61U])
			{
				overriddenFrameSettings.lodBias = overridingFrameSettings.lodBias;
			}
			if (frameSettingsOverideMask.mask[60U])
			{
				overriddenFrameSettings.lodBiasMode = overridingFrameSettings.lodBiasMode;
			}
			if (frameSettingsOverideMask.mask[64U])
			{
				overriddenFrameSettings.lodBiasQualityLevel = overridingFrameSettings.lodBiasQualityLevel;
			}
			if (frameSettingsOverideMask.mask[63U])
			{
				overriddenFrameSettings.maximumLODLevel = overridingFrameSettings.maximumLODLevel;
			}
			if (frameSettingsOverideMask.mask[62U])
			{
				overriddenFrameSettings.maximumLODLevelMode = overridingFrameSettings.maximumLODLevelMode;
			}
			if (frameSettingsOverideMask.mask[65U])
			{
				overriddenFrameSettings.maximumLODLevelQualityLevel = overridingFrameSettings.maximumLODLevelQualityLevel;
			}
			if (frameSettingsOverideMask.mask[66U])
			{
				overriddenFrameSettings.materialQuality = overridingFrameSettings.materialQuality;
			}
		}

		// Token: 0x06000932 RID: 2354 RVA: 0x0004AD2C File Offset: 0x00048F2C
		internal static void Sanitize(ref FrameSettings sanitizedFrameSettings, Camera camera, RenderPipelineSettings renderPipelineSettings)
		{
			bool flag = camera.cameraType == CameraType.Reflection;
			bool flag2 = HDUtils.IsRegularPreviewCamera(camera);
			bool flag3 = CoreUtils.IsSceneViewFogEnabled(camera);
			switch (renderPipelineSettings.supportedLitShaderMode)
			{
			case RenderPipelineSettings.SupportedLitShaderMode.ForwardOnly:
				sanitizedFrameSettings.litShaderMode = LitShaderMode.Forward;
				break;
			case RenderPipelineSettings.SupportedLitShaderMode.DeferredOnly:
				sanitizedFrameSettings.litShaderMode = LitShaderMode.Deferred;
				break;
			}
			ref BitArray128 ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[20U] = ptr[20U] & !flag2;
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[22U] = ptr[22U] & (renderPipelineSettings.supportShadowMask && !flag2);
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[21U] = ptr[21U] & !flag2;
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[34U] = ptr[34U] & renderPipelineSettings.hdShadowInitParams.supportScreenSpaceShadows;
			ptr = ref sanitizedFrameSettings.bitDatas;
			bool flag4 = (ptr[92U] = ptr[92U] & HDRenderPipeline.GatherRayTracingSupport(renderPipelineSettings));
			ptr = ref sanitizedFrameSettings.bitDatas;
			bool flag5 = (ptr[31U] = ptr[31U] & (renderPipelineSettings.supportMSAA && sanitizedFrameSettings.litShaderMode == LitShaderMode.Forward));
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[23U] = ptr[23U] & (renderPipelineSettings.supportSSR && !flag5 && !flag2);
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[13U] = ptr[13U] & !flag2;
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[24U] = ptr[24U] & (renderPipelineSettings.supportSSAO && !flag2);
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[25U] = ptr[25U] & renderPipelineSettings.supportSubsurfaceScattering;
			ptr = ref sanitizedFrameSettings.bitDatas;
			bool flag6 = (ptr[27U] = ptr[27U] & (flag3 && !flag2));
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[28U] = ptr[28U] & (renderPipelineSettings.supportVolumetrics && flag6);
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[29U] = ptr[29U] & !flag2;
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[30U] = ptr[30U] & (renderPipelineSettings.supportLightLayers && !flag2);
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[32U] = ptr[32U] & (!flag && !flag2);
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[15U] = ptr[15U] & (!flag && !flag2);
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[8U] = ptr[8U] & (renderPipelineSettings.supportTransparentDepthPrepass && !flag2);
			ptr = ref sanitizedFrameSettings.bitDatas;
			bool flag7 = (ptr[10U] = ptr[10U] & (renderPipelineSettings.supportMotionVectors && !flag2));
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[11U] = ptr[11U] & (flag7 && !flag2);
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[12U] = ptr[12U] & (renderPipelineSettings.supportDecals && !flag2);
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[9U] = ptr[9U] & (renderPipelineSettings.supportTransparentDepthPostpass && !flag2);
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[14U] = ptr[14U] & (renderPipelineSettings.supportDistortion && !flag5 && !flag2);
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[18U] = ptr[18U] & renderPipelineSettings.lowresTransparentSettings.enabled;
			ptr = ref sanitizedFrameSettings.bitDatas;
			bool flag8 = (ptr[40U] = ptr[40U] & SystemInfo.supportsAsyncCompute);
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[41U] = ptr[41U] && flag8;
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[42U] = ptr[42U] & (flag8 && !flag4);
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[43U] = ptr[43U] & (flag8 && !flag4);
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[44U] = ptr[44U] & (flag8 && !flag4);
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[45U] = ptr[45U] && flag8;
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[6U] = ptr[6U] & renderPipelineSettings.supportCustomPass;
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[6U] = ptr[6U] & (camera.cameraType != CameraType.Preview);
			ptr = ref sanitizedFrameSettings.bitDatas;
			ptr[120U] = ptr[120U] & !flag5;
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0004B246 File Offset: 0x00049446
		internal static void AggregateFrameSettings(ref FrameSettings aggregatedFrameSettings, Camera camera, HDAdditionalCameraData additionalData, HDRenderPipelineAsset hdrpAsset, HDRenderPipelineAsset defaultHdrpAsset)
		{
			FrameSettings.AggregateFrameSettings(ref aggregatedFrameSettings, camera, additionalData, defaultHdrpAsset.GetDefaultFrameSettings((additionalData != null) ? additionalData.defaultFrameSettings : FrameSettingsRenderType.Camera), hdrpAsset.currentPlatformRenderPipelineSettings);
		}

		// Token: 0x06000934 RID: 2356 RVA: 0x0004B269 File Offset: 0x00049469
		internal unsafe static void AggregateFrameSettings(ref FrameSettings aggregatedFrameSettings, Camera camera, HDAdditionalCameraData additionalData, ref FrameSettings defaultFrameSettings, RenderPipelineSettings supportedFeatures)
		{
			aggregatedFrameSettings = defaultFrameSettings;
			if (additionalData && additionalData.customRenderingSettings)
			{
				FrameSettings.Override(ref aggregatedFrameSettings, *additionalData.renderingPathCustomFrameSettings, additionalData.renderingPathCustomFrameSettingsOverrideMask);
			}
			FrameSettings.Sanitize(ref aggregatedFrameSettings, camera, supportedFeatures);
		}

		// Token: 0x06000935 RID: 2357 RVA: 0x0004B2A8 File Offset: 0x000494A8
		public static bool operator ==(FrameSettings a, FrameSettings b)
		{
			return a.bitDatas == b.bitDatas && a.lodBias == b.lodBias && a.lodBiasMode == b.lodBiasMode && a.lodBiasQualityLevel == b.lodBiasQualityLevel && a.maximumLODLevel == b.maximumLODLevel && a.maximumLODLevelMode == b.maximumLODLevelMode && a.maximumLODLevelQualityLevel == b.maximumLODLevelQualityLevel && a.materialQuality == b.materialQuality;
		}

		// Token: 0x06000936 RID: 2358 RVA: 0x0004B32C File Offset: 0x0004952C
		public static bool operator !=(FrameSettings a, FrameSettings b)
		{
			return a.bitDatas != b.bitDatas || a.lodBias != b.lodBias || a.lodBiasMode != b.lodBiasMode || a.lodBiasQualityLevel != b.lodBiasQualityLevel || a.maximumLODLevel != b.maximumLODLevel || a.maximumLODLevelMode != b.maximumLODLevelMode || a.maximumLODLevelQualityLevel != b.maximumLODLevelQualityLevel || a.materialQuality != b.materialQuality;
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x0004B3B4 File Offset: 0x000495B4
		public override bool Equals(object obj)
		{
			return obj is FrameSettings && this.bitDatas.Equals(((FrameSettings)obj).bitDatas) && this.lodBias.Equals(((FrameSettings)obj).lodBias) && this.lodBiasMode.Equals(((FrameSettings)obj).lodBiasMode) && this.lodBiasQualityLevel.Equals(((FrameSettings)obj).lodBiasQualityLevel) && this.maximumLODLevel.Equals(((FrameSettings)obj).maximumLODLevel) && this.maximumLODLevelMode.Equals(((FrameSettings)obj).maximumLODLevelMode) && this.maximumLODLevelQualityLevel.Equals(((FrameSettings)obj).maximumLODLevelQualityLevel) && this.materialQuality.Equals(((FrameSettings)obj).materialQuality);
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x0004B4C4 File Offset: 0x000496C4
		public override int GetHashCode()
		{
			return (((((((1474027755 * -1521134295 + this.bitDatas.GetHashCode()) * -1521134295 + this.lodBias.GetHashCode()) * -1521134295 + this.lodBiasMode.GetHashCode()) * -1521134295 + this.lodBiasQualityLevel.GetHashCode()) * -1521134295 + this.maximumLODLevel.GetHashCode()) * -1521134295 + this.maximumLODLevelMode.GetHashCode()) * -1521134295 + this.maximumLODLevelQualityLevel.GetHashCode()) * -1521134295 + this.materialQuality.GetHashCode();
		}

		// Token: 0x04000E5C RID: 3676
		[SerializeField]
		private BitArray128 bitDatas;

		// Token: 0x04000E5D RID: 3677
		[SerializeField]
		public float lodBias;

		// Token: 0x04000E5E RID: 3678
		[SerializeField]
		public LODBiasMode lodBiasMode;

		// Token: 0x04000E5F RID: 3679
		[SerializeField]
		public int lodBiasQualityLevel;

		// Token: 0x04000E60 RID: 3680
		[SerializeField]
		public int maximumLODLevel;

		// Token: 0x04000E61 RID: 3681
		[SerializeField]
		public MaximumLODLevelMode maximumLODLevelMode;

		// Token: 0x04000E62 RID: 3682
		[SerializeField]
		public int maximumLODLevelQualityLevel;

		// Token: 0x04000E63 RID: 3683
		public MaterialQuality materialQuality;

		// Token: 0x02000280 RID: 640
		[DebuggerDisplay("{m_Value}", Name = "{m_Label,nq}")]
		internal class DebuggerEntry
		{
			// Token: 0x06000C98 RID: 3224 RVA: 0x000598FF File Offset: 0x00057AFF
			public DebuggerEntry(string label, object value)
			{
				this.m_Label = label;
				this.m_Value = value;
			}

			// Token: 0x040016BD RID: 5821
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string m_Label;

			// Token: 0x040016BE RID: 5822
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private object m_Value;
		}

		// Token: 0x02000281 RID: 641
		[DebuggerDisplay("", Name = "{m_GroupName,nq}")]
		internal class DebuggerGroup
		{
			// Token: 0x06000C99 RID: 3225 RVA: 0x00059915 File Offset: 0x00057B15
			public DebuggerGroup(string groupName, FrameSettings.DebuggerEntry[] entries)
			{
				this.m_GroupName = groupName;
				this.m_Entries = entries;
			}

			// Token: 0x040016BF RID: 5823
			[DebuggerBrowsable(DebuggerBrowsableState.Never)]
			private string m_GroupName;

			// Token: 0x040016C0 RID: 5824
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public FrameSettings.DebuggerEntry[] m_Entries;
		}

		// Token: 0x02000282 RID: 642
		internal class FrameSettingsDebugView
		{
			// Token: 0x06000C9A RID: 3226 RVA: 0x0005992B File Offset: 0x00057B2B
			public FrameSettingsDebugView(FrameSettings frameSettings)
			{
				this.m_FrameSettings = frameSettings;
			}

			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x06000C9B RID: 3227 RVA: 0x0005993C File Offset: 0x00057B3C
			[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
			public FrameSettings.DebuggerGroup[] Keys
			{
				get
				{
					int length = Enum.GetValues(typeof(FrameSettingsField)).Length;
					Dictionary<FrameSettingsField, FrameSettingsFieldAttribute> dictionary = new Dictionary<FrameSettingsField, FrameSettingsFieldAttribute>();
					List<FrameSettings.DebuggerGroup> list = new List<FrameSettings.DebuggerGroup>();
					Type typeFromHandle = typeof(FrameSettingsField);
					List<FrameSettingsField> list2 = new List<FrameSettingsField>();
					foreach (object obj in Enum.GetValues(typeFromHandle))
					{
						FrameSettingsField frameSettingsField = (FrameSettingsField)obj;
						dictionary[frameSettingsField] = typeFromHandle.GetField(Enum.GetName(typeFromHandle, frameSettingsField)).GetCustomAttribute<FrameSettingsFieldAttribute>();
						if (dictionary[frameSettingsField] == null)
						{
							list2.Add(frameSettingsField);
						}
					}
					using (IEnumerator<int> enumerator2 = (from a in dictionary.Values
						where a != null
						select a.@group).Distinct<int>().GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							int groupIndex = enumerator2.Current;
							List<FrameSettings.DebuggerGroup> list3 = list;
							string text = FrameSettingsHistory.foldoutNames[groupIndex];
							FrameSettings.DebuggerEntry[] array;
							if (dictionary == null)
							{
								array = null;
							}
							else
							{
								IEnumerable<KeyValuePair<FrameSettingsField, FrameSettingsFieldAttribute>> enumerable = dictionary.Where(delegate(KeyValuePair<FrameSettingsField, FrameSettingsFieldAttribute> pair)
								{
									FrameSettingsFieldAttribute value = pair.Value;
									return value != null && value.group == groupIndex;
								});
								if (enumerable == null)
								{
									array = null;
								}
								else
								{
									array = (from pair in enumerable
										orderby pair.Value.orderInGroup
										select pair into kvp
										select new FrameSettings.DebuggerEntry(Enum.GetName(typeof(FrameSettingsField), kvp.Key), this.m_FrameSettings.bitDatas[(uint)kvp.Key])).ToArray<FrameSettings.DebuggerEntry>();
								}
							}
							list3.Add(new FrameSettings.DebuggerGroup(text, array));
						}
					}
					List<FrameSettings.DebuggerGroup> list4 = list;
					string text2 = "Bits without attribute";
					IEnumerable<FrameSettingsField> enumerable2 = list2.Where((FrameSettingsField fs) => fs != FrameSettingsField.None);
					list4.Add(new FrameSettings.DebuggerGroup(text2, (enumerable2 != null) ? enumerable2.Select((FrameSettingsField fs) => new FrameSettings.DebuggerEntry(Enum.GetName(typeof(FrameSettingsField), fs), this.m_FrameSettings.bitDatas[(uint)fs])).ToArray<FrameSettings.DebuggerEntry>() : null));
					list.Add(new FrameSettings.DebuggerGroup("Non Bit data", new FrameSettings.DebuggerEntry[]
					{
						new FrameSettings.DebuggerEntry("lodBias", this.m_FrameSettings.lodBias),
						new FrameSettings.DebuggerEntry("lodBiasMode", this.m_FrameSettings.lodBiasMode),
						new FrameSettings.DebuggerEntry("lodBiasQualityLevel", this.m_FrameSettings.lodBiasQualityLevel),
						new FrameSettings.DebuggerEntry("maximumLODLevel", this.m_FrameSettings.maximumLODLevel),
						new FrameSettings.DebuggerEntry("maximumLODLevelMode", this.m_FrameSettings.maximumLODLevelMode),
						new FrameSettings.DebuggerEntry("maximumLODLevelQualityLevel", this.m_FrameSettings.maximumLODLevelQualityLevel),
						new FrameSettings.DebuggerEntry("materialQuality", this.m_FrameSettings.materialQuality)
					}));
					return list.ToArray();
				}
			}

			// Token: 0x040016C1 RID: 5825
			private const int numberOfNonBitValues = 2;

			// Token: 0x040016C2 RID: 5826
			private FrameSettings m_FrameSettings;
		}
	}
}
