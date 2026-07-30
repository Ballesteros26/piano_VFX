using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200016D RID: 365
	[VolumeComponentMenu("Visual Environment")]
	[Serializable]
	public sealed class VisualEnvironment : VolumeComponent
	{
		// Token: 0x04001005 RID: 4101
		public IntParameter skyType = new IntParameter(0, false);

		// Token: 0x04001006 RID: 4102
		public SkyAmbientModeParameter skyAmbientMode = new SkyAmbientModeParameter(SkyAmbientMode.Static, false);

		// Token: 0x04001007 RID: 4103
		[SerializeField]
		internal FogTypeParameter fogType = new FogTypeParameter(FogType.None, false);
	}
}
