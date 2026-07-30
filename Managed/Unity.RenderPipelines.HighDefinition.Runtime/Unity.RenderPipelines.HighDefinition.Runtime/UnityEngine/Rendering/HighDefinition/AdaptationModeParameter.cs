using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000DD RID: 221
	[Serializable]
	public sealed class AdaptationModeParameter : VolumeParameter<AdaptationMode>
	{
		// Token: 0x06000760 RID: 1888 RVA: 0x00038802 File Offset: 0x00036A02
		public AdaptationModeParameter(AdaptationMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
