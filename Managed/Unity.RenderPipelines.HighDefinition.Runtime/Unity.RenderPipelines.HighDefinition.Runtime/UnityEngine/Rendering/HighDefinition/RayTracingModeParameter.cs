using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000118 RID: 280
	[Serializable]
	public sealed class RayTracingModeParameter : VolumeParameter<RayTracingMode>
	{
		// Token: 0x060008A5 RID: 2213 RVA: 0x000482F8 File Offset: 0x000464F8
		public RayTracingModeParameter(RayTracingMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
