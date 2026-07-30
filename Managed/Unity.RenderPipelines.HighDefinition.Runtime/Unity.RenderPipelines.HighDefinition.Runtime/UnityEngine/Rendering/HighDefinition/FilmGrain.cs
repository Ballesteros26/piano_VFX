using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000DF RID: 223
	[VolumeComponentMenu("Post-processing/Film Grain")]
	[Serializable]
	public sealed class FilmGrain : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x06000761 RID: 1889 RVA: 0x0003880C File Offset: 0x00036A0C
		public bool IsActive()
		{
			return this.intensity.value > 0f && (this.type.value != FilmGrainLookup.Custom || this.texture.value != null);
		}

		// Token: 0x040007BA RID: 1978
		[Tooltip("Specifies the type of grain to use. Select a preset or select \"Custom\" to provide your own Texture.")]
		public FilmGrainLookupParameter type = new FilmGrainLookupParameter(FilmGrainLookup.Thin1, false);

		// Token: 0x040007BB RID: 1979
		[Tooltip("Controls the strength of the film grain effect.")]
		public ClampedFloatParameter intensity = new ClampedFloatParameter(0f, 0f, 1f, false);

		// Token: 0x040007BC RID: 1980
		[Tooltip("Controls the noisiness response curve. The higher you set this value, the less noise there is in brighter areas.")]
		public ClampedFloatParameter response = new ClampedFloatParameter(0.8f, 0f, 1f, false);

		// Token: 0x040007BD RID: 1981
		[Tooltip("Specifies a tileable Texture to use for the grain. The neutral value for this Texture is 0.5 which means that HDRP does not apply grain at this value.")]
		public NoInterpTextureParameter texture = new NoInterpTextureParameter(null, false);
	}
}
