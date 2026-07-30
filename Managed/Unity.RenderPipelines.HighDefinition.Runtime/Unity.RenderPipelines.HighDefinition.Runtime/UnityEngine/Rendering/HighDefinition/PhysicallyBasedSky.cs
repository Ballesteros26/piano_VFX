using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200015B RID: 347
	[VolumeComponentMenu("Sky/Physically Based Sky")]
	[SkyUniqueID(4)]
	public class PhysicallyBasedSky : SkySettings
	{
		// Token: 0x06000A2D RID: 2605 RVA: 0x0000AFC2 File Offset: 0x000091C2
		internal static float ScaleHeightFromLayerDepth(float d)
		{
			return d * 0.144765f;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0004F716 File Offset: 0x0004D916
		internal static float LayerDepthFromScaleHeight(float H)
		{
			return H / 0.144765f;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0004F720 File Offset: 0x0004D920
		internal static float ExtinctionFromZenithOpacityAndScaleHeight(float alpha, float H)
		{
			float num = Mathf.Min(alpha, 0.999999f);
			return -Mathf.Log(1f - num, 2.7182817f) / H;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0004F750 File Offset: 0x0004D950
		internal static float ZenithOpacityFromExtinctionAndScaleHeight(float ext, float H)
		{
			float num = ext * H;
			return 1f - Mathf.Exp(-num);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0004F76E File Offset: 0x0004D96E
		internal float GetAirScaleHeight()
		{
			if (this.earthPreset.value)
			{
				return 8000f;
			}
			return PhysicallyBasedSky.ScaleHeightFromLayerDepth(this.airMaximumAltitude.value);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0004F793 File Offset: 0x0004D993
		internal float GetPlanetaryRadius()
		{
			if (this.earthPreset.value)
			{
				return 6378100f;
			}
			return this.planetaryRadius.value;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0004F7B4 File Offset: 0x0004D9B4
		internal Vector3 GetPlanetCenterPosition(Vector3 camPosWS)
		{
			if (this.sphericalMode.value)
			{
				return this.planetCenterPosition.value;
			}
			float num = this.GetPlanetaryRadius();
			float value = this.seaLevel.value;
			return new Vector3(camPosWS.x, -num + value, camPosWS.z);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0004F804 File Offset: 0x0004DA04
		internal Vector3 GetAirExtinctionCoefficient()
		{
			Vector3 vector = default(Vector3);
			if (this.earthPreset.value)
			{
				vector.x = 5.8E-06f;
				vector.y = 1.35E-05f;
				vector.z = 3.3099997E-05f;
			}
			else
			{
				vector.x = PhysicallyBasedSky.ExtinctionFromZenithOpacityAndScaleHeight(this.airDensityR.value, this.GetAirScaleHeight());
				vector.y = PhysicallyBasedSky.ExtinctionFromZenithOpacityAndScaleHeight(this.airDensityG.value, this.GetAirScaleHeight());
				vector.z = PhysicallyBasedSky.ExtinctionFromZenithOpacityAndScaleHeight(this.airDensityB.value, this.GetAirScaleHeight());
			}
			return vector;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0004F8A4 File Offset: 0x0004DAA4
		internal Vector3 GetAirAlbedo()
		{
			Vector3 vector = default(Vector3);
			if (this.earthPreset.value)
			{
				vector.x = 0.9f;
				vector.y = 0.9f;
				vector.z = 1f;
			}
			else
			{
				vector.x = this.airTint.value.r;
				vector.y = this.airTint.value.g;
				vector.z = this.airTint.value.b;
			}
			return vector;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0004F934 File Offset: 0x0004DB34
		internal Vector3 GetAirScatteringCoefficient()
		{
			Vector3 airExtinctionCoefficient = this.GetAirExtinctionCoefficient();
			Vector3 airAlbedo = this.GetAirAlbedo();
			return new Vector3(airExtinctionCoefficient.x * airAlbedo.x, airExtinctionCoefficient.y * airAlbedo.y, airExtinctionCoefficient.z * airAlbedo.z);
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0004F97B File Offset: 0x0004DB7B
		internal float GetAerosolScaleHeight()
		{
			return PhysicallyBasedSky.ScaleHeightFromLayerDepth(this.aerosolMaximumAltitude.value);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0004F98D File Offset: 0x0004DB8D
		internal float GetAerosolExtinctionCoefficient()
		{
			return PhysicallyBasedSky.ExtinctionFromZenithOpacityAndScaleHeight(this.aerosolDensity.value, this.GetAerosolScaleHeight());
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0004F9A8 File Offset: 0x0004DBA8
		internal Vector3 GetAerosolScatteringCoefficient()
		{
			float aerosolExtinctionCoefficient = this.GetAerosolExtinctionCoefficient();
			return new Vector3(aerosolExtinctionCoefficient * this.aerosolTint.value.r, aerosolExtinctionCoefficient * this.aerosolTint.value.g, aerosolExtinctionCoefficient * this.aerosolTint.value.b);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0004F9F8 File Offset: 0x0004DBF8
		private PhysicallyBasedSky()
		{
			base.displayName = "Physically Based Sky";
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0004FCE0 File Offset: 0x0004DEE0
		internal int GetPrecomputationHashCode()
		{
			return ((((((((((((base.GetHashCode() * 23 + this.earthPreset.GetHashCode()) * 23 + this.planetaryRadius.GetHashCode()) * 23 + this.groundTint.GetHashCode()) * 23 + this.airMaximumAltitude.GetHashCode()) * 23 + this.airDensityR.GetHashCode()) * 23 + this.airDensityG.GetHashCode()) * 23 + this.airDensityB.GetHashCode()) * 23 + this.airTint.GetHashCode()) * 23 + this.aerosolMaximumAltitude.GetHashCode()) * 23 + this.aerosolDensity.GetHashCode()) * 23 + this.aerosolTint.GetHashCode()) * 23 + this.aerosolAnisotropy.GetHashCode()) * 23 + this.numberOfBounces.GetHashCode();
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0004FDB8 File Offset: 0x0004DFB8
		public override int GetHashCode()
		{
			int num = this.GetPrecomputationHashCode();
			num = num * 23 + this.sphericalMode.GetHashCode();
			num = num * 23 + this.seaLevel.GetHashCode();
			num = num * 23 + this.planetCenterPosition.GetHashCode();
			num = num * 23 + this.planetRotation.GetHashCode();
			if (this.groundColorTexture.value != null)
			{
				num = num * 23 + this.groundColorTexture.GetHashCode();
			}
			if (this.groundEmissionTexture.value != null)
			{
				num = num * 23 + this.groundEmissionTexture.GetHashCode();
			}
			num = num * 23 + this.groundEmissionMultiplier.GetHashCode();
			num = num * 23 + this.spaceRotation.GetHashCode();
			if (this.spaceEmissionTexture.value != null)
			{
				num = num * 23 + this.spaceEmissionTexture.GetHashCode();
			}
			num = num * 23 + this.spaceEmissionMultiplier.GetHashCode();
			num = num * 23 + this.colorSaturation.GetHashCode();
			num = num * 23 + this.alphaSaturation.GetHashCode();
			num = num * 23 + this.alphaMultiplier.GetHashCode();
			num = num * 23 + this.horizonTint.GetHashCode();
			num = num * 23 + this.zenithTint.GetHashCode();
			return num * 23 + this.horizonZenithShift.GetHashCode();
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0004FF16 File Offset: 0x0004E116
		public override Type GetSkyRendererType()
		{
			return typeof(PhysicallyBasedSkyRenderer);
		}

		// Token: 0x04000F7B RID: 3963
		private const float k_DefaultEarthRadius = 6378100f;

		// Token: 0x04000F7C RID: 3964
		private const float k_DefaultAirScatteringR = 5.8E-06f;

		// Token: 0x04000F7D RID: 3965
		private const float k_DefaultAirScatteringG = 1.35E-05f;

		// Token: 0x04000F7E RID: 3966
		private const float k_DefaultAirScatteringB = 3.3099997E-05f;

		// Token: 0x04000F7F RID: 3967
		private const float k_DefaultAirScaleHeight = 8000f;

		// Token: 0x04000F80 RID: 3968
		private const float k_DefaultAirAlbedoR = 0.9f;

		// Token: 0x04000F81 RID: 3969
		private const float k_DefaultAirAlbedoG = 0.9f;

		// Token: 0x04000F82 RID: 3970
		private const float k_DefaultAirAlbedoB = 1f;

		// Token: 0x04000F83 RID: 3971
		[Tooltip("Simplifies the interface by using parameters suitable to simulate Earth.")]
		public BoolParameter earthPreset = new BoolParameter(true, false);

		// Token: 0x04000F84 RID: 3972
		[Tooltip("Allows to specify the location of the planet. If disabled, the planet is always below the camera in the world-space X-Z plane.")]
		public BoolParameter sphericalMode = new BoolParameter(true, false);

		// Token: 0x04000F85 RID: 3973
		[Tooltip("World-space Y coordinate of the sea level of the planet. Units: meters.")]
		public FloatParameter seaLevel = new FloatParameter(0f, false);

		// Token: 0x04000F86 RID: 3974
		[Tooltip("Radius of the planet (distance from the center of the planet to the sea level). Units: meters.")]
		public MinFloatParameter planetaryRadius = new MinFloatParameter(6378100f, 0f, false);

		// Token: 0x04000F87 RID: 3975
		[Tooltip("Position of the center of the planet in the world space. Units: meters. Does not affect the precomputation.")]
		public Vector3Parameter planetCenterPosition = new Vector3Parameter(new Vector3(0f, -6378100f, 0f), false);

		// Token: 0x04000F88 RID: 3976
		[Tooltip("Opacity (per color channel) of air as measured by an observer on the ground looking towards the zenith.")]
		public ClampedFloatParameter airDensityR = new ClampedFloatParameter(PhysicallyBasedSky.ZenithOpacityFromExtinctionAndScaleHeight(5.8E-06f, 8000f), 0f, 1f, false);

		// Token: 0x04000F89 RID: 3977
		[Tooltip("Opacity (per color channel) of air as measured by an observer on the ground looking towards the zenith.")]
		public ClampedFloatParameter airDensityG = new ClampedFloatParameter(PhysicallyBasedSky.ZenithOpacityFromExtinctionAndScaleHeight(1.35E-05f, 8000f), 0f, 1f, false);

		// Token: 0x04000F8A RID: 3978
		[Tooltip("Opacity (per color channel) of air as measured by an observer on the ground looking towards the zenith.")]
		public ClampedFloatParameter airDensityB = new ClampedFloatParameter(PhysicallyBasedSky.ZenithOpacityFromExtinctionAndScaleHeight(3.3099997E-05f, 8000f), 0f, 1f, false);

		// Token: 0x04000F8B RID: 3979
		[Tooltip("Single scattering albedo of air molecules (per color channel). The value of 0 results in absorbing molecules, and the value of 1 results in scattering ones.")]
		public ColorParameter airTint = new ColorParameter(new Color(0.9f, 0.9f, 1f), false, false, true, false);

		// Token: 0x04000F8C RID: 3980
		[Tooltip("Depth of the atmospheric layer (from the sea level) composed of air particles. Controls the rate of height-based density falloff. Units: meters.")]
		public MinFloatParameter airMaximumAltitude = new MinFloatParameter(PhysicallyBasedSky.LayerDepthFromScaleHeight(8000f), 0f, false);

		// Token: 0x04000F8D RID: 3981
		[Tooltip("Opacity of aerosols as measured by an observer on the ground looking towards the zenith.")]
		public ClampedFloatParameter aerosolDensity = new ClampedFloatParameter(PhysicallyBasedSky.ZenithOpacityFromExtinctionAndScaleHeight(1E-05f, 1200f), 0f, 1f, false);

		// Token: 0x04000F8E RID: 3982
		[Tooltip("Single scattering albedo of aerosol molecules (per color channel). The value of 0 results in absorbing molecules, and the value of 1 results in scattering ones.")]
		public ColorParameter aerosolTint = new ColorParameter(new Color(0.9f, 0.9f, 0.9f), false, false, true, false);

		// Token: 0x04000F8F RID: 3983
		[Tooltip("Depth of the atmospheric layer (from the sea level) composed of aerosol particles. Controls the rate of height-based density falloff. Units: meters.")]
		public MinFloatParameter aerosolMaximumAltitude = new MinFloatParameter(PhysicallyBasedSky.LayerDepthFromScaleHeight(1200f), 0f, false);

		// Token: 0x04000F90 RID: 3984
		[Tooltip("Positive values for forward scattering, 0 for isotropic scattering. negative values for backward scattering.")]
		public ClampedFloatParameter aerosolAnisotropy = new ClampedFloatParameter(0f, -1f, 1f, false);

		// Token: 0x04000F91 RID: 3985
		[Tooltip("Number of scattering events.")]
		public ClampedIntParameter numberOfBounces = new ClampedIntParameter(8, 1, 10, false);

		// Token: 0x04000F92 RID: 3986
		[Tooltip("Ground tint.")]
		public ColorParameter groundTint = new ColorParameter(new Color(0.4f, 0.25f, 0.15f), false, false, false, false);

		// Token: 0x04000F93 RID: 3987
		[Tooltip("Ground color texture. Does not affect the precomputation.")]
		public CubemapParameter groundColorTexture = new CubemapParameter(null, false);

		// Token: 0x04000F94 RID: 3988
		[Tooltip("Ground emission texture. Does not affect the precomputation.")]
		public CubemapParameter groundEmissionTexture = new CubemapParameter(null, false);

		// Token: 0x04000F95 RID: 3989
		[Tooltip("Ground emission multiplier. Does not affect the precomputation.")]
		public MinFloatParameter groundEmissionMultiplier = new MinFloatParameter(1f, 0f, false);

		// Token: 0x04000F96 RID: 3990
		[Tooltip("Rotation of the planet. Does not affect the precomputation.")]
		public Vector3Parameter planetRotation = new Vector3Parameter(Vector3.zero, false);

		// Token: 0x04000F97 RID: 3991
		[Tooltip("Space emission texture. Does not affect the precomputation.")]
		public CubemapParameter spaceEmissionTexture = new CubemapParameter(null, false);

		// Token: 0x04000F98 RID: 3992
		[Tooltip("Space emission multiplier. Does not affect the precomputation.")]
		public MinFloatParameter spaceEmissionMultiplier = new MinFloatParameter(1f, 0f, false);

		// Token: 0x04000F99 RID: 3993
		[Tooltip("Rotation of space. Does not affect the precomputation.")]
		public Vector3Parameter spaceRotation = new Vector3Parameter(Vector3.zero, false);

		// Token: 0x04000F9A RID: 3994
		[Tooltip("Color saturation. Does not affect the precomputation.")]
		public ClampedFloatParameter colorSaturation = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x04000F9B RID: 3995
		[Tooltip("Opacity saturation. Does not affect the precomputation.")]
		public ClampedFloatParameter alphaSaturation = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x04000F9C RID: 3996
		[Tooltip("Opacity multiplier. Does not affect the precomputation.")]
		public ClampedFloatParameter alphaMultiplier = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x04000F9D RID: 3997
		[Tooltip("Horizon tint. Does not affect the precomputation.")]
		public ColorParameter horizonTint = new ColorParameter(Color.white, false, false, false, false);

		// Token: 0x04000F9E RID: 3998
		[Tooltip("Zenith tint. Does not affect the precomputation.")]
		public ColorParameter zenithTint = new ColorParameter(Color.white, false, false, false, false);

		// Token: 0x04000F9F RID: 3999
		[Tooltip("Horizon-zenith shift. Does not affect the precomputation.")]
		public ClampedFloatParameter horizonZenithShift = new ClampedFloatParameter(0f, -1f, 1f, false);
	}
}
