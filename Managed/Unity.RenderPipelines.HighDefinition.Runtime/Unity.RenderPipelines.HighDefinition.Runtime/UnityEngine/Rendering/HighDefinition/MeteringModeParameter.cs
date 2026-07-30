using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000DB RID: 219
	[Serializable]
	public sealed class MeteringModeParameter : VolumeParameter<MeteringMode>
	{
		// Token: 0x0600075E RID: 1886 RVA: 0x000387EE File Offset: 0x000369EE
		public MeteringModeParameter(MeteringMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
