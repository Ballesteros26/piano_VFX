using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000E9 RID: 233
	[Serializable]
	public sealed class TonemappingModeParameter : VolumeParameter<TonemappingMode>
	{
		// Token: 0x06000775 RID: 1909 RVA: 0x00038F9C File Offset: 0x0003719C
		public TonemappingModeParameter(TonemappingMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
