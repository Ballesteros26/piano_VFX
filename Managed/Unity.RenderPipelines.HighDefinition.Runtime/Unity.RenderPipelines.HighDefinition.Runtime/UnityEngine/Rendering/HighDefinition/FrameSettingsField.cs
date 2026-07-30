using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000138 RID: 312
	public enum FrameSettingsField
	{
		// Token: 0x04000E70 RID: 3696
		None = -1,
		// Token: 0x04000E71 RID: 3697
		[FrameSettingsField(0, FrameSettingsField.LitShaderMode, null, "Specifies the Lit Shader Mode for Cameras using these Frame Settings use to render the Scene (Depends on \"Lit Shader Mode\" in current HDRP Asset).", FrameSettingsFieldAttribute.DisplayType.BoolAsEnumPopup, typeof(LitShaderMode), null, null, 0)]
		LitShaderMode,
		// Token: 0x04000E72 RID: 3698
		[FrameSettingsField(0, FrameSettingsField.None, "Depth Prepass within Deferred", "When enabled, HDRP processes a depth prepass for Cameras using these Frame Settings. Set Lit Shader Mode to Deferred to access this option.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.LitShaderMode }, null, -1)]
		DepthPrepassWithDeferredRendering,
		// Token: 0x04000E73 RID: 3699
		[FrameSettingsField(0, FrameSettingsField.None, "Clear GBuffers", "When enabled, HDRP clear GBuffers for Cameras using these Frame Settings. Set Lit Shader Mode to Deferred to access this option.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.LitShaderMode }, null, 2)]
		ClearGBuffers = 5,
		// Token: 0x04000E74 RID: 3700
		[FrameSettingsField(0, FrameSettingsField.None, "MSAA within Forward", "When enabled, Cameras using these Frame Settings calculate MSAA when they render the Scene. Set Lit Shader Mode to Forward to access this option.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, new FrameSettingsField[] { FrameSettingsField.LitShaderMode }, 3)]
		MSAA = 31,
		// Token: 0x04000E75 RID: 3701
		[FrameSettingsField(0, FrameSettingsField.OpaqueObjects, null, "When enabled, Cameras using these Frame Settings render opaque GameObjects.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, 4)]
		OpaqueObjects = 2,
		// Token: 0x04000E76 RID: 3702
		[FrameSettingsField(0, FrameSettingsField.TransparentObjects, null, "When enabled, Cameras using these Frame Settings render Transparent GameObjects.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, 5)]
		TransparentObjects,
		// Token: 0x04000E77 RID: 3703
		[FrameSettingsField(0, FrameSettingsField.Decals, null, "When enabled, HDRP processes a decal render pass for Cameras using these Frame Settings.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, 6)]
		Decals = 12,
		// Token: 0x04000E78 RID: 3704
		[FrameSettingsField(0, FrameSettingsField.TransparentPrepass, null, "When enabled, HDRP processes a transparent prepass for Cameras using these Frame Settings.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, 7)]
		TransparentPrepass = 8,
		// Token: 0x04000E79 RID: 3705
		[FrameSettingsField(0, FrameSettingsField.TransparentPostpass, null, "When enabled, HDRP processes a transparent postpass for Cameras using these Frame Settings.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, 8)]
		TransparentPostpass,
		// Token: 0x04000E7A RID: 3706
		[FrameSettingsField(0, FrameSettingsField.None, "Low Resolution Transparent", "When enabled, HDRP processes a transparent pass in a lower resolution for Cameras using these Frame Settings.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, 9)]
		LowResTransparent = 18,
		// Token: 0x04000E7B RID: 3707
		[FrameSettingsField(0, FrameSettingsField.None, "Ray Tracing", "When enabled, HDRP updates ray tracing for Cameras using these Frame Settings (Depends on \"Realtime RayTracing\" in current HDRP Asset).", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, 10)]
		RayTracing = 92,
		// Token: 0x04000E7C RID: 3708
		[FrameSettingsField(0, FrameSettingsField.CustomPass, null, "When enabled, HDRP renders custom passes contained in CustomPassVolume components.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, 11)]
		CustomPass = 6,
		// Token: 0x04000E7D RID: 3709
		[FrameSettingsField(0, FrameSettingsField.MotionVectors, null, "When enabled, HDRP processes a motion vector pass for Cameras using these Frame Settings (Depends on \"Motion Vectors\" in current HDRP Asset).", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, 12)]
		MotionVectors = 10,
		// Token: 0x04000E7E RID: 3710
		[FrameSettingsField(0, FrameSettingsField.None, "Opaque Object Motion", "When enabled, HDRP processes an object motion vector pass for Cameras using these Frame Settings.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.MotionVectors }, null, 13)]
		ObjectMotionVectors,
		// Token: 0x04000E7F RID: 3711
		[FrameSettingsField(0, FrameSettingsField.None, "Transparent Object Motion", "When enabled, transparent GameObjects use Motion Vectors. You must also enable TransparentWritesVelocity for Materials that you want to use motion vectors with.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.MotionVectors }, null, 14)]
		TransparentsWriteMotionVector = 16,
		// Token: 0x04000E80 RID: 3712
		[FrameSettingsField(0, FrameSettingsField.Refraction, null, "When enabled, HDRP processes a refraction render pass for Cameras using these Frame Settings. This add a resolve of ColorBuffer after the drawing of opaque materials to be use for Refraction effect during transparent pass.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, 15)]
		Refraction = 13,
		// Token: 0x04000E81 RID: 3713
		[Obsolete]
		RoughRefraction = 13,
		// Token: 0x04000E82 RID: 3714
		[FrameSettingsField(0, FrameSettingsField.Distortion, null, "When enabled, HDRP processes a distortion render pass for Cameras using these Frame Settings (Depends on \"Distortion\" in current HDRP Asset).", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, -1)]
		Distortion,
		// Token: 0x04000E83 RID: 3715
		[FrameSettingsField(0, FrameSettingsField.None, "Post-process", "When enabled, HDRP processes a post-processing render pass for Cameras using these Frame Settings.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, -1)]
		Postprocess,
		// Token: 0x04000E84 RID: 3716
		[FrameSettingsField(0, FrameSettingsField.None, "Custom Post-process", "When enabled, HDRP render user written post processes.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.Postprocess }, null, 17)]
		CustomPostProcess = 39,
		// Token: 0x04000E85 RID: 3717
		[FrameSettingsField(0, FrameSettingsField.None, "Stop NaN", "When enabled, HDRP replace NaN values with black pixels for Cameras using these Frame Settings.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.Postprocess }, null, 17)]
		StopNaN = 80,
		// Token: 0x04000E86 RID: 3718
		[FrameSettingsField(0, FrameSettingsField.DepthOfField, null, "When enabled, HDRP adds depth of field to Cameras affected by a Volume containing the Depth Of Field override.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.Postprocess }, null, 17)]
		DepthOfField,
		// Token: 0x04000E87 RID: 3719
		[FrameSettingsField(0, FrameSettingsField.MotionBlur, null, "When enabled, HDRP adds motion blur to Cameras affected by a Volume containing the Blur override.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.Postprocess }, null, 17)]
		MotionBlur,
		// Token: 0x04000E88 RID: 3720
		[FrameSettingsField(0, FrameSettingsField.PaniniProjection, null, "When enabled, HDRP adds panini projection to Cameras affected by a Volume containing the Panini Projection override.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.Postprocess }, null, 17)]
		PaniniProjection,
		// Token: 0x04000E89 RID: 3721
		[FrameSettingsField(0, FrameSettingsField.Bloom, null, "When enabled, HDRP adds bloom to Cameras affected by a Volume containing the Bloom override.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.Postprocess }, null, 17)]
		Bloom,
		// Token: 0x04000E8A RID: 3722
		[FrameSettingsField(0, FrameSettingsField.LensDistortion, null, "When enabled, HDRP adds lens distortion to Cameras affected by a Volume containing the Lens Distortion override.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.Postprocess }, null, 17)]
		LensDistortion,
		// Token: 0x04000E8B RID: 3723
		[FrameSettingsField(0, FrameSettingsField.ChromaticAberration, null, "When enabled, HDRP adds chromatic aberration to Cameras affected by a Volume containing the Chromatic Aberration override.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.Postprocess }, null, 17)]
		ChromaticAberration,
		// Token: 0x04000E8C RID: 3724
		[FrameSettingsField(0, FrameSettingsField.Vignette, null, "When enabled, HDRP adds vignette to Cameras affected by a Volume containing the Vignette override.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.Postprocess }, null, 17)]
		Vignette,
		// Token: 0x04000E8D RID: 3725
		[FrameSettingsField(0, FrameSettingsField.ColorGrading, null, "When enabled, HDRP processes color grading for Cameras using these Frame Settings.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.Postprocess }, null, 17)]
		ColorGrading,
		// Token: 0x04000E8E RID: 3726
		[FrameSettingsField(0, FrameSettingsField.Tonemapping, null, "When enabled, HDRP processes tonemapping for Cameras using these Frame Settings.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.Postprocess }, null, 17)]
		Tonemapping = 93,
		// Token: 0x04000E8F RID: 3727
		[FrameSettingsField(0, FrameSettingsField.FilmGrain, null, "When enabled, HDRP adds film grain to Cameras affected by a Volume containing the Film Grain override.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.Postprocess }, null, 18)]
		FilmGrain = 89,
		// Token: 0x04000E90 RID: 3728
		[FrameSettingsField(0, FrameSettingsField.Dithering, null, "When enabled, HDRP processes dithering for Cameras using these Frame Settings.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.Postprocess }, null, 18)]
		Dithering,
		// Token: 0x04000E91 RID: 3729
		[FrameSettingsField(0, FrameSettingsField.None, "Anti-aliasing", "When enabled, HDRP processes anti-aliasing for camera using these Frame Settings.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.Postprocess }, null, 18)]
		Antialiasing,
		// Token: 0x04000E92 RID: 3730
		[FrameSettingsField(0, FrameSettingsField.None, "After Post-process", "When enabled, HDRP processes a post-processing render pass for Cameras using these Frame Settings.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, 19)]
		AfterPostprocess = 17,
		// Token: 0x04000E93 RID: 3731
		[FrameSettingsField(0, FrameSettingsField.None, "Depth Test", "When enabled, Cameras that don't use TAA process a depth test for Materials in the AfterPostProcess rendering pass.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.AfterPostprocess }, null, 20)]
		ZTestAfterPostProcessTAA = 19,
		// Token: 0x04000E94 RID: 3732
		[FrameSettingsField(0, FrameSettingsField.LODBiasMode, null, "Specifies the Level Of Detail Mode for Cameras using these Frame Settings use to render the Scene. Scale will allow to add a scale factor while Override will allow to set a specific value.", FrameSettingsFieldAttribute.DisplayType.Others, typeof(LODBiasMode), null, null, 100)]
		LODBiasMode = 60,
		// Token: 0x04000E95 RID: 3733
		[FrameSettingsField(0, FrameSettingsField.LODBias, null, "Sets the Level Of Detail Bias or the Scale on it.", FrameSettingsFieldAttribute.DisplayType.Others, null, new FrameSettingsField[] { FrameSettingsField.LODBiasMode }, null, -1)]
		LODBias,
		// Token: 0x04000E96 RID: 3734
		[FrameSettingsField(0, FrameSettingsField.None, "Quality Level", "The quality level to use when fetching the value from the quality settings.", FrameSettingsFieldAttribute.DisplayType.Others, null, new FrameSettingsField[] { FrameSettingsField.LODBiasMode }, null, 100)]
		LODBiasQualityLevel = 64,
		// Token: 0x04000E97 RID: 3735
		[FrameSettingsField(0, FrameSettingsField.MaximumLODLevelMode, null, "Specifies the Maximum Level Of Detail Mode for Cameras using these Frame Settings to use to render the Scene. Offset allows you to add an offset factor while Override allows you to set a specific value.", FrameSettingsFieldAttribute.DisplayType.Others, typeof(MaximumLODLevelMode), null, null, -1)]
		MaximumLODLevelMode = 62,
		// Token: 0x04000E98 RID: 3736
		[FrameSettingsField(0, FrameSettingsField.MaximumLODLevel, null, "Sets the Maximum Level Of Detail Level or the Offset on it.", FrameSettingsFieldAttribute.DisplayType.Others, null, new FrameSettingsField[] { FrameSettingsField.MaximumLODLevelMode }, null, -1)]
		MaximumLODLevel,
		// Token: 0x04000E99 RID: 3737
		[FrameSettingsField(0, FrameSettingsField.None, "Quality Level", "The quality level to use when fetching the value from the quality settings.", FrameSettingsFieldAttribute.DisplayType.Others, null, new FrameSettingsField[] { FrameSettingsField.MaximumLODLevelMode }, null, 102)]
		MaximumLODLevelQualityLevel = 65,
		// Token: 0x04000E9A RID: 3738
		[FrameSettingsField(0, FrameSettingsField.MaterialQualityLevel, null, "The material quality level to use.", FrameSettingsFieldAttribute.DisplayType.Others, null, null, null, -1)]
		MaterialQualityLevel,
		// Token: 0x04000E9B RID: 3739
		[FrameSettingsField(1, FrameSettingsField.ShadowMaps, null, "When enabled, Cameras using these Frame Settings render shadows.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, 1)]
		ShadowMaps = 20,
		// Token: 0x04000E9C RID: 3740
		[FrameSettingsField(1, FrameSettingsField.ContactShadows, null, "When enabled, Cameras using these Frame Settings render Contact Shadows", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, -1)]
		ContactShadows,
		// Token: 0x04000E9D RID: 3741
		[FrameSettingsField(1, FrameSettingsField.ScreenSpaceShadows, null, "When enabled, Cameras using these Frame Settings render Screen Space Shadows (Depends on \"Screen Space Shadows\" in current HDRP Asset).", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, 23)]
		ScreenSpaceShadows = 34,
		// Token: 0x04000E9E RID: 3742
		[FrameSettingsField(1, FrameSettingsField.Shadowmask, null, "When enabled, Cameras using these Frame Settings render shadows from Shadow Masks (Depends on \"Shadowmask\" in current HDRP Asset).", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, 24)]
		Shadowmask = 22,
		// Token: 0x04000E9F RID: 3743
		[FrameSettingsField(1, FrameSettingsField.None, "Screen Space Reflection", "When enabled, Cameras using these Frame Settings calculate Screen Space Reflections (Depends on \"Screen Space Reflection\" in current HDRP Asset).", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, -1)]
		SSR,
		// Token: 0x04000EA0 RID: 3744
		[FrameSettingsField(1, FrameSettingsField.None, "Screen Space Ambient Occlusion", "When enabled, Cameras using these Frame Settings calculate Screen Space Ambient Occlusion (Depends on \"Screen Space Ambient Occlusion\" in current HDRP Asset).", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, -1)]
		SSAO,
		// Token: 0x04000EA1 RID: 3745
		[FrameSettingsField(1, FrameSettingsField.SubsurfaceScattering, null, "When enabled, Cameras using these Frame Settings render subsurface scattering (SSS) effects for GameObjects that use a SSS Material (Depends on \"Subsurface Scattering\" in current HDRP Asset).", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, -1)]
		SubsurfaceScattering,
		// Token: 0x04000EA2 RID: 3746
		[FrameSettingsField(1, FrameSettingsField.Transmission, null, "When enabled, Cameras using these Frame Settings render subsurface scattering (SSS) Materials with an added transmission effect (only if you enable Transmission on the SSS Material in the Material's Inspector).", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, -1)]
		Transmission,
		// Token: 0x04000EA3 RID: 3747
		[FrameSettingsField(1, FrameSettingsField.None, "Fog", "When enabled, Cameras using these Frame Settings render fog effects.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, -1)]
		AtmosphericScattering,
		// Token: 0x04000EA4 RID: 3748
		[FrameSettingsField(1, FrameSettingsField.Volumetrics, null, "When enabled, Cameras using these Frame Settings render volumetric effects such as volumetric fog and lighting (Depends on \"Volumetrics\" in current HDRP Asset).", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.AtmosphericScattering }, null, -1)]
		Volumetrics,
		// Token: 0x04000EA5 RID: 3749
		[FrameSettingsField(1, FrameSettingsField.None, "Reprojection", "When enabled, Cameras using these Frame Settings use several previous frames to calculate volumetric effects which increases their overall quality at run time.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[]
		{
			FrameSettingsField.AtmosphericScattering,
			FrameSettingsField.Volumetrics
		}, null, -1)]
		ReprojectionForVolumetrics,
		// Token: 0x04000EA6 RID: 3750
		[FrameSettingsField(1, FrameSettingsField.LightLayers, null, "When enabled, Cameras that use these Frame Settings make use of LightLayers (Depends on \"Light Layers\" in current HDRP Asset).", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, -1)]
		LightLayers,
		// Token: 0x04000EA7 RID: 3751
		[FrameSettingsField(1, FrameSettingsField.ExposureControl, null, "When enabled, Cameras that use these Frame Settings use exposure values defined in relevant components.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, 33)]
		ExposureControl = 32,
		// Token: 0x04000EA8 RID: 3752
		[FrameSettingsField(1, FrameSettingsField.ReflectionProbe, null, "When enabled, Cameras that use these Frame Settings calculate reflection from Reflection Probes.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, -1)]
		ReflectionProbe,
		// Token: 0x04000EA9 RID: 3753
		[FrameSettingsField(1, FrameSettingsField.None, "Planar Reflection Probe", "When enabled, Cameras that use these Frame Settings calculate reflection from Planar Reflection Probes.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, 36)]
		PlanarProbe = 35,
		// Token: 0x04000EAA RID: 3754
		[FrameSettingsField(1, FrameSettingsField.None, "Metallic Indirect Fallback", "When enabled, Cameras that use these Frame Settings render Materials with base color as diffuse. This is a useful Frame Setting to use for real-time Reflection Probes because it renders metals as diffuse Materials to stop them appearing black when Unity can't calculate several bounces of specular lighting.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, -1)]
		ReplaceDiffuseForIndirect,
		// Token: 0x04000EAB RID: 3755
		[FrameSettingsField(1, FrameSettingsField.SkyReflection, null, "When enabled, the Sky affects specular lighting for Cameras that use these Frame Settings.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, -1)]
		SkyReflection,
		// Token: 0x04000EAC RID: 3756
		[FrameSettingsField(1, FrameSettingsField.DirectSpecularLighting, null, "When enabled, Cameras that use these Frame Settings render Direct Specular lighting. This is a useful Frame Setting to use for baked Reflection Probes to remove view dependent lighting.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, -1)]
		DirectSpecularLighting,
		// Token: 0x04000EAD RID: 3757
		[FrameSettingsField(2, FrameSettingsField.None, "Asynchronous Execution", "When enabled, HDRP executes certain Compute Shader commands in parallel. This only has an effect if the target platform supports async compute.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, -1)]
		AsyncCompute = 40,
		// Token: 0x04000EAE RID: 3758
		[FrameSettingsField(2, FrameSettingsField.None, "Light List", "When enabled, HDRP builds the Light List asynchronously.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.AsyncCompute }, null, -1)]
		LightListAsync,
		// Token: 0x04000EAF RID: 3759
		[FrameSettingsField(2, FrameSettingsField.None, "SS Reflection", "When enabled, HDRP calculates screen space reflection asynchronously.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.AsyncCompute }, null, -1)]
		SSRAsync,
		// Token: 0x04000EB0 RID: 3760
		[FrameSettingsField(2, FrameSettingsField.None, "SS Ambient Occlusion", "When enabled, HDRP calculates screen space ambient occlusion asynchronously.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.AsyncCompute }, null, -1)]
		SSAOAsync,
		// Token: 0x04000EB1 RID: 3761
		ContactShadowsAsync,
		// Token: 0x04000EB2 RID: 3762
		[FrameSettingsField(2, FrameSettingsField.None, "Volume Voxelizations", "When enabled, HDRP calculates volumetric voxelization asynchronously.", FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.AsyncCompute }, null, -1)]
		VolumeVoxelizationsAsync,
		// Token: 0x04000EB3 RID: 3763
		[FrameSettingsField(3, FrameSettingsField.FPTLForForwardOpaque, null, null, FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, -1)]
		FPTLForForwardOpaque = 120,
		// Token: 0x04000EB4 RID: 3764
		[FrameSettingsField(3, FrameSettingsField.BigTilePrepass, null, null, FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, -1)]
		BigTilePrepass,
		// Token: 0x04000EB5 RID: 3765
		[FrameSettingsField(3, FrameSettingsField.DeferredTile, null, null, FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, null, null, -1)]
		DeferredTile,
		// Token: 0x04000EB6 RID: 3766
		[FrameSettingsField(3, FrameSettingsField.ComputeLightEvaluation, null, null, FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.DeferredTile }, null, -1)]
		ComputeLightEvaluation,
		// Token: 0x04000EB7 RID: 3767
		[FrameSettingsField(3, FrameSettingsField.ComputeLightVariants, null, null, FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.DeferredTile }, null, -1)]
		ComputeLightVariants,
		// Token: 0x04000EB8 RID: 3768
		[FrameSettingsField(3, FrameSettingsField.ComputeMaterialVariants, null, null, FrameSettingsFieldAttribute.DisplayType.BoolAsCheckbox, null, new FrameSettingsField[] { FrameSettingsField.DeferredTile }, null, -1)]
		ComputeMaterialVariants
	}
}
