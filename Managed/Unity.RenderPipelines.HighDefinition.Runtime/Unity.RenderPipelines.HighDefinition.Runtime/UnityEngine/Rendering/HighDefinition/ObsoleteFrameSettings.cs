using System;
using System.Diagnostics;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000133 RID: 307
	[DebuggerDisplay("FrameSettings overriding {overrides.ToString(\"X\")}")]
	[Obsolete("For data migration")]
	[Serializable]
	internal class ObsoleteFrameSettings
	{
		// Token: 0x04000E38 RID: 3640
		public ObsoleteFrameSettingsOverrides overrides;

		// Token: 0x04000E39 RID: 3641
		public bool enableShadow;

		// Token: 0x04000E3A RID: 3642
		public bool enableContactShadows;

		// Token: 0x04000E3B RID: 3643
		public bool enableShadowMask;

		// Token: 0x04000E3C RID: 3644
		public bool enableSSR;

		// Token: 0x04000E3D RID: 3645
		public bool enableSSAO;

		// Token: 0x04000E3E RID: 3646
		public bool enableSubsurfaceScattering;

		// Token: 0x04000E3F RID: 3647
		public bool enableTransmission;

		// Token: 0x04000E40 RID: 3648
		public bool enableAtmosphericScattering;

		// Token: 0x04000E41 RID: 3649
		public bool enableVolumetrics;

		// Token: 0x04000E42 RID: 3650
		public bool enableReprojectionForVolumetrics;

		// Token: 0x04000E43 RID: 3651
		public bool enableLightLayers;

		// Token: 0x04000E44 RID: 3652
		public bool enableExposureControl = true;

		// Token: 0x04000E45 RID: 3653
		public float diffuseGlobalDimmer;

		// Token: 0x04000E46 RID: 3654
		public float specularGlobalDimmer;

		// Token: 0x04000E47 RID: 3655
		public ObsoleteLitShaderMode shaderLitMode;

		// Token: 0x04000E48 RID: 3656
		public bool enableDepthPrepassWithDeferredRendering;

		// Token: 0x04000E49 RID: 3657
		public bool enableTransparentPrepass;

		// Token: 0x04000E4A RID: 3658
		public bool enableMotionVectors;

		// Token: 0x04000E4B RID: 3659
		public bool enableObjectMotionVectors;

		// Token: 0x04000E4C RID: 3660
		[FormerlySerializedAs("enableDBuffer")]
		public bool enableDecals;

		// Token: 0x04000E4D RID: 3661
		public bool enableRoughRefraction;

		// Token: 0x04000E4E RID: 3662
		public bool enableTransparentPostpass;

		// Token: 0x04000E4F RID: 3663
		public bool enableDistortion;

		// Token: 0x04000E50 RID: 3664
		public bool enablePostprocess;

		// Token: 0x04000E51 RID: 3665
		public bool enableOpaqueObjects;

		// Token: 0x04000E52 RID: 3666
		public bool enableTransparentObjects;

		// Token: 0x04000E53 RID: 3667
		public bool enableRealtimePlanarReflection;

		// Token: 0x04000E54 RID: 3668
		public bool enableMSAA;

		// Token: 0x04000E55 RID: 3669
		public bool enableAsyncCompute;

		// Token: 0x04000E56 RID: 3670
		public bool runLightListAsync;

		// Token: 0x04000E57 RID: 3671
		public bool runSSRAsync;

		// Token: 0x04000E58 RID: 3672
		public bool runSSAOAsync;

		// Token: 0x04000E59 RID: 3673
		public bool runContactShadowsAsync;

		// Token: 0x04000E5A RID: 3674
		public bool runVolumeVoxelizationAsync;

		// Token: 0x04000E5B RID: 3675
		public ObsoleteLightLoopSettings lightLoopSettings;
	}
}
