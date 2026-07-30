using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000DC RID: 220
	[Serializable]
	public sealed class LuminanceSourceParameter : VolumeParameter<LuminanceSource>
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x000387F8 File Offset: 0x000369F8
		public LuminanceSourceParameter(LuminanceSource value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
