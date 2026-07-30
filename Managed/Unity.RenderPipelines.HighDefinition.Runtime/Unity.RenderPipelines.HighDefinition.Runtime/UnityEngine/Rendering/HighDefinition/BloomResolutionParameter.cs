using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000CB RID: 203
	[Serializable]
	public sealed class BloomResolutionParameter : VolumeParameter<BloomResolution>
	{
		// Token: 0x0600073E RID: 1854 RVA: 0x00037DE9 File Offset: 0x00035FE9
		public BloomResolutionParameter(BloomResolution value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
