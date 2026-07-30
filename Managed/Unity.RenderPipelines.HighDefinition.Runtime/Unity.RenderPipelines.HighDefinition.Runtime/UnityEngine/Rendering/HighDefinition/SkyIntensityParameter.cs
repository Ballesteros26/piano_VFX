using System;
using System.Diagnostics;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000169 RID: 361
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public sealed class SkyIntensityParameter : VolumeParameter<SkyIntensityMode>
	{
		// Token: 0x06000A94 RID: 2708 RVA: 0x00052589 File Offset: 0x00050789
		public SkyIntensityParameter(SkyIntensityMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
