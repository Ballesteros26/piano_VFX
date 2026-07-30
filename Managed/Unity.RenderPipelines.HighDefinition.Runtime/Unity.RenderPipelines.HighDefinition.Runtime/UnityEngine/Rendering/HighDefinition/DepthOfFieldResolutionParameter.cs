using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000D4 RID: 212
	[Serializable]
	public sealed class DepthOfFieldResolutionParameter : VolumeParameter<DepthOfFieldResolution>
	{
		// Token: 0x0600075A RID: 1882 RVA: 0x000386FB File Offset: 0x000368FB
		public DepthOfFieldResolutionParameter(DepthOfFieldResolution value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
