using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000048 RID: 72
	[VolumeComponentMenu("Lighting/Indirect Lighting Controller")]
	[Serializable]
	internal class IndirectLightingController : VolumeComponent
	{
		// Token: 0x040001CB RID: 459
		public MinFloatParameter indirectSpecularIntensity = new MinFloatParameter(1f, 0f, false);

		// Token: 0x040001CC RID: 460
		public MinFloatParameter indirectDiffuseIntensity = new MinFloatParameter(1f, 0f, false);
	}
}
