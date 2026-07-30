using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000159 RID: 345
	[VolumeComponentMenu("Sky/HDRI Sky")]
	[SkyUniqueID(1)]
	public class HDRISky : SkySettings
	{
		// Token: 0x06000A20 RID: 2592 RVA: 0x0004F060 File Offset: 0x0004D260
		public override int GetHashCode()
		{
			int num = base.GetHashCode();
			num = ((this.hdriSky.value != null) ? (num * 23 + this.hdriSky.GetHashCode()) : num);
			num = num * 23 + this.enableBackplate.GetHashCode();
			num = num * 23 + this.backplateType.GetHashCode();
			num = num * 23 + this.groundLevel.GetHashCode();
			num = num * 23 + this.scale.GetHashCode();
			num = num * 23 + this.projectionDistance.GetHashCode();
			num = num * 23 + this.plateRotation.GetHashCode();
			num = num * 23 + this.plateTexRotation.GetHashCode();
			num = num * 23 + this.plateTexOffset.GetHashCode();
			num = num * 23 + this.blendAmount.GetHashCode();
			num = num * 23 + this.shadowTint.GetHashCode();
			num = num * 23 + this.pointLightShadow.GetHashCode();
			num = num * 23 + this.dirLightShadow.GetHashCode();
			return num * 23 + this.rectLightShadow.GetHashCode();
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0004F179 File Offset: 0x0004D379
		public override Type GetSkyRendererType()
		{
			return typeof(HDRISkyRenderer);
		}

		// Token: 0x04000F65 RID: 3941
		[Tooltip("Specify the cubemap HDRP uses to render the sky.")]
		public CubemapParameter hdriSky = new CubemapParameter(null, false);

		// Token: 0x04000F66 RID: 3942
		[Tooltip("Enable or disable the backplate.")]
		public BoolParameter enableBackplate = new BoolParameter(false, false);

		// Token: 0x04000F67 RID: 3943
		[Tooltip("Backplate type.")]
		public BackplateTypeParameter backplateType = new BackplateTypeParameter(BackplateType.Disc, false);

		// Token: 0x04000F68 RID: 3944
		[Tooltip("Define the ground level of the Backplate.")]
		public FloatParameter groundLevel = new FloatParameter(0f, false);

		// Token: 0x04000F69 RID: 3945
		[Tooltip("Extent of the Backplate (if circle only the X value is considered).")]
		public Vector2Parameter scale = new Vector2Parameter(Vector2.one * 32f, false);

		// Token: 0x04000F6A RID: 3946
		[Tooltip("Backplate's projection distance to varying the cubemap projection on the plate.")]
		public MinFloatParameter projectionDistance = new MinFloatParameter(16f, 1E-07f, false);

		// Token: 0x04000F6B RID: 3947
		[Tooltip("Backplate rotation parameter for the geometry.")]
		public ClampedFloatParameter plateRotation = new ClampedFloatParameter(0f, 0f, 360f, false);

		// Token: 0x04000F6C RID: 3948
		[Tooltip("Backplate rotation parameter for the projected texture.")]
		public ClampedFloatParameter plateTexRotation = new ClampedFloatParameter(0f, 0f, 360f, false);

		// Token: 0x04000F6D RID: 3949
		[Tooltip("Backplate projection offset on the plane.")]
		public Vector2Parameter plateTexOffset = new Vector2Parameter(Vector2.zero, false);

		// Token: 0x04000F6E RID: 3950
		[Tooltip("Backplate blend parameter to blend the edge of the backplate with the background.")]
		public ClampedFloatParameter blendAmount = new ClampedFloatParameter(0f, 0f, 100f, false);

		// Token: 0x04000F6F RID: 3951
		[Tooltip("Backplate Shadow Tint projected on the plane.")]
		public ColorParameter shadowTint = new ColorParameter(Color.grey, false);

		// Token: 0x04000F70 RID: 3952
		[Tooltip("Allow backplate to receive shadow from point light.")]
		public BoolParameter pointLightShadow = new BoolParameter(false, false);

		// Token: 0x04000F71 RID: 3953
		[Tooltip("Allow backplate to receive shadow from directional light.")]
		public BoolParameter dirLightShadow = new BoolParameter(false, false);

		// Token: 0x04000F72 RID: 3954
		[Tooltip("Allow backplate to receive shadow from Area light.")]
		public BoolParameter rectLightShadow = new BoolParameter(false, false);
	}
}
