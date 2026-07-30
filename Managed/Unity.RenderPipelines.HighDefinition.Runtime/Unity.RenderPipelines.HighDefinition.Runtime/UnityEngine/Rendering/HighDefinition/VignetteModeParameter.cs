using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000EC RID: 236
	[Serializable]
	public sealed class VignetteModeParameter : VolumeParameter<VignetteMode>
	{
		// Token: 0x06000778 RID: 1912 RVA: 0x000390E1 File Offset: 0x000372E1
		public VignetteModeParameter(VignetteMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
