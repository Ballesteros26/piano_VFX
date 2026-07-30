using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000D3 RID: 211
	[Serializable]
	public sealed class DepthOfFieldModeParameter : VolumeParameter<DepthOfFieldMode>
	{
		// Token: 0x06000759 RID: 1881 RVA: 0x000386F1 File Offset: 0x000368F1
		public DepthOfFieldModeParameter(DepthOfFieldMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
