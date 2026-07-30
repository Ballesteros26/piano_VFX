using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000044 RID: 68
	[VolumeComponentMenu("Material/Diffusion Profile Override")]
	[Serializable]
	internal sealed class DiffusionProfileOverride : VolumeComponent
	{
		// Token: 0x040001BB RID: 443
		[Tooltip("List of diffusion profiles used inside the volume.")]
		[SerializeField]
		internal DiffusionProfileSettingsParameter diffusionProfiles = new DiffusionProfileSettingsParameter(null, true);
	}
}
