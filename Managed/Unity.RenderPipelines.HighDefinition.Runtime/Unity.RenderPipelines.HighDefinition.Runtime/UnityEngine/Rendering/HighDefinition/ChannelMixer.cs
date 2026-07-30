using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000CC RID: 204
	[VolumeComponentMenu("Post-processing/Channel Mixer")]
	[Serializable]
	public sealed class ChannelMixer : VolumeComponent, IPostProcessComponent
	{
		// Token: 0x0600073F RID: 1855 RVA: 0x00037DF4 File Offset: 0x00035FF4
		public bool IsActive()
		{
			return this.redOutRedIn.value != 100f || this.redOutGreenIn.value != 0f || this.redOutBlueIn.value != 0f || this.greenOutRedIn.value != 0f || this.greenOutGreenIn.value != 100f || this.greenOutBlueIn.value != 0f || this.blueOutRedIn.value != 0f || this.blueOutGreenIn.value != 0f || this.blueOutBlueIn.value != 100f;
		}

		// Token: 0x04000765 RID: 1893
		[Tooltip("Controls the influence of the red channel in the output red channel.")]
		public ClampedFloatParameter redOutRedIn = new ClampedFloatParameter(100f, -200f, 200f, false);

		// Token: 0x04000766 RID: 1894
		[Tooltip("Controls the influence of the green channel in the output red channel.")]
		public ClampedFloatParameter redOutGreenIn = new ClampedFloatParameter(0f, -200f, 200f, false);

		// Token: 0x04000767 RID: 1895
		[Tooltip("Controls the influence of the blue channel in the output red channel.")]
		public ClampedFloatParameter redOutBlueIn = new ClampedFloatParameter(0f, -200f, 200f, false);

		// Token: 0x04000768 RID: 1896
		[Tooltip("Controls the influence of the red channel in the output green channel.")]
		public ClampedFloatParameter greenOutRedIn = new ClampedFloatParameter(0f, -200f, 200f, false);

		// Token: 0x04000769 RID: 1897
		[Tooltip("Controls the influence of the green channel in the output green channel.")]
		public ClampedFloatParameter greenOutGreenIn = new ClampedFloatParameter(100f, -200f, 200f, false);

		// Token: 0x0400076A RID: 1898
		[Tooltip("Controls the influence of the blue channel in the output green channel.")]
		public ClampedFloatParameter greenOutBlueIn = new ClampedFloatParameter(0f, -200f, 200f, false);

		// Token: 0x0400076B RID: 1899
		[Tooltip("Controls the influence of the red channel in the output blue channel.")]
		public ClampedFloatParameter blueOutRedIn = new ClampedFloatParameter(0f, -200f, 200f, false);

		// Token: 0x0400076C RID: 1900
		[Tooltip("Controls the influence of the green channel in the output blue channel.")]
		public ClampedFloatParameter blueOutGreenIn = new ClampedFloatParameter(0f, -200f, 200f, false);

		// Token: 0x0400076D RID: 1901
		[Tooltip("Controls the influence of the blue channel in the output blue channel.")]
		public ClampedFloatParameter blueOutBlueIn = new ClampedFloatParameter(100f, -200f, 200f, false);

		// Token: 0x0400076E RID: 1902
		[SerializeField]
		private int m_SelectedChannel;
	}
}
