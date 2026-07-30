using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200009C RID: 156
	[VolumeComponentMenu("Shadowing/Micro Shadows")]
	[Serializable]
	public class MicroShadowing : VolumeComponent
	{
		// Token: 0x0600060F RID: 1551 RVA: 0x00033067 File Offset: 0x00031267
		private MicroShadowing()
		{
			base.displayName = "Micro Shadows";
		}

		// Token: 0x0400065B RID: 1627
		public BoolParameter enable = new BoolParameter(false, false);

		// Token: 0x0400065C RID: 1628
		public ClampedFloatParameter opacity = new ClampedFloatParameter(1f, 0f, 1f, false);
	}
}
