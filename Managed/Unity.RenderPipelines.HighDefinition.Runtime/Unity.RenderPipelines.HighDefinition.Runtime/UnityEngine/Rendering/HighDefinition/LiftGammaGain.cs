using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000E2 RID: 226
	[VolumeComponentMenu("Post-processing/Lift, Gamma, Gain")]
	[Serializable]
	public sealed class LiftGammaGain : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x0003899C File Offset: 0x00036B9C
		public bool IsActive()
		{
			Vector4 vector = new Vector4(1f, 1f, 1f, 0f);
			return this.lift != vector || this.gamma != vector || this.gain != vector;
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000389F0 File Offset: 0x00036BF0
		private LiftGammaGain()
		{
			base.displayName = "Lift, Gamma, Gain";
		}

		// Token: 0x040007C3 RID: 1987
		[Tooltip("Controls the dark tones of the render.")]
		public Vector4Parameter lift = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f), false);

		// Token: 0x040007C4 RID: 1988
		[Tooltip("Controls the mid-range tones of the render with a power function.")]
		public Vector4Parameter gamma = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f), false);

		// Token: 0x040007C5 RID: 1989
		[Tooltip("Controls the highlights of the render.")]
		public Vector4Parameter gain = new Vector4Parameter(new Vector4(1f, 1f, 1f, 0f), false);
	}
}
