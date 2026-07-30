using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200016A RID: 362
	public abstract class SkySettings : VolumeComponent
	{
		// Token: 0x06000A95 RID: 2709 RVA: 0x00052594 File Offset: 0x00050794
		public override int GetHashCode()
		{
			return (((((13 * 23 + this.rotation.GetHashCode()) * 23 + this.exposure.GetHashCode()) * 23 + this.multiplier.GetHashCode()) * 23 + this.desiredLuxValue.GetHashCode()) * 23 + this.skyIntensityMode.GetHashCode()) * 23 + this.includeSunInBaking.GetHashCode();
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x000525FD File Offset: 0x000507FD
		internal static int GetUniqueID<T>()
		{
			return SkySettings.GetUniqueID(typeof(T));
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00052610 File Offset: 0x00050810
		internal static int GetUniqueID(Type type)
		{
			int num;
			if (!SkySettings.skyUniqueIDs.TryGetValue(type, out num))
			{
				object[] customAttributes = type.GetCustomAttributes(typeof(SkyUniqueID), false);
				num = ((customAttributes.Length == 0) ? (-1) : ((SkyUniqueID)customAttributes[0]).uniqueID);
				SkySettings.skyUniqueIDs[type] = num;
			}
			return num;
		}

		// Token: 0x06000A98 RID: 2712
		public abstract Type GetSkyRendererType();

		// Token: 0x04000FEF RID: 4079
		[Tooltip("Sets the rotation of the sky.")]
		public ClampedFloatParameter rotation = new ClampedFloatParameter(0f, 0f, 360f, false);

		// Token: 0x04000FF0 RID: 4080
		[Tooltip("Specifies the intensity mode HDRP uses for the sky.")]
		public SkyIntensityParameter skyIntensityMode = new SkyIntensityParameter(SkyIntensityMode.Exposure, false);

		// Token: 0x04000FF1 RID: 4081
		[Tooltip("Sets the exposure of the sky in EV.")]
		public FloatParameter exposure = new FloatParameter(0f, false);

		// Token: 0x04000FF2 RID: 4082
		[Tooltip("Sets the intensity multiplier for the sky.")]
		public MinFloatParameter multiplier = new MinFloatParameter(1f, 0f, false);

		// Token: 0x04000FF3 RID: 4083
		[Tooltip("Informative helper that displays the relative intensity (in Lux) for the current HDR texture set in HDRI Sky.")]
		public MinFloatParameter upperHemisphereLuxValue = new MinFloatParameter(1f, 0f, false);

		// Token: 0x04000FF4 RID: 4084
		[Tooltip("Informative helper that displays Show the color of Shadow.")]
		public Vector3Parameter upperHemisphereLuxColor = new Vector3Parameter(new Vector3(0f, 0f, 0f), false);

		// Token: 0x04000FF5 RID: 4085
		[Tooltip("Sets the absolute intensity (in Lux) of the current HDR texture set in HDRI Sky. Functions as a Lux intensity multiplier for the sky.")]
		public FloatParameter desiredLuxValue = new FloatParameter(20000f, false);

		// Token: 0x04000FF6 RID: 4086
		[Tooltip("Specifies when HDRP updates the environment lighting. When set to OnDemand, use HDRenderPipeline.RequestSkyEnvironmentUpdate() to request an update.")]
		public EnvUpdateParameter updateMode = new EnvUpdateParameter(EnvironmentUpdateMode.OnChanged, false);

		// Token: 0x04000FF7 RID: 4087
		[Tooltip("Sets the period, in seconds, at which HDRP updates the environment ligting (0 means HDRP updates it every frame).")]
		public MinFloatParameter updatePeriod = new MinFloatParameter(0f, 0f, false);

		// Token: 0x04000FF8 RID: 4088
		[Tooltip("When enabled, HDRP uses the Sun Disk in baked lighting.")]
		public BoolParameter includeSunInBaking = new BoolParameter(false, false);

		// Token: 0x04000FF9 RID: 4089
		private static Dictionary<Type, int> skyUniqueIDs = new Dictionary<Type, int>();
	}
}
