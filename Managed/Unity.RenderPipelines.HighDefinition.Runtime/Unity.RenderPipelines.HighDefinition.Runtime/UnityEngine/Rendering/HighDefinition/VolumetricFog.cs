using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000043 RID: 67
	[VolumeComponentDeprecated]
	internal class VolumetricFog : AtmosphericScattering
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00002646 File Offset: 0x00000846
		internal override void PushShaderParameters(HDCamera hdCamera, CommandBuffer cmd)
		{
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000B458 File Offset: 0x00009658
		private VolumetricFog()
		{
			base.displayName = "Volumetric Fog (Deprecated)";
		}

		// Token: 0x040001B4 RID: 436
		public ColorParameter albedo = new ColorParameter(Color.white, false);

		// Token: 0x040001B5 RID: 437
		public MinFloatParameter meanFreePath = new MinFloatParameter(1000000f, 1f, false);

		// Token: 0x040001B6 RID: 438
		public FloatParameter baseHeight = new FloatParameter(0f, false);

		// Token: 0x040001B7 RID: 439
		public FloatParameter maximumHeight = new FloatParameter(10f, false);

		// Token: 0x040001B8 RID: 440
		public ClampedFloatParameter anisotropy = new ClampedFloatParameter(0f, -1f, 1f, false);

		// Token: 0x040001B9 RID: 441
		public ClampedFloatParameter globalLightProbeDimmer = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x040001BA RID: 442
		public BoolParameter enableDistantFog = new BoolParameter(false, false);
	}
}
