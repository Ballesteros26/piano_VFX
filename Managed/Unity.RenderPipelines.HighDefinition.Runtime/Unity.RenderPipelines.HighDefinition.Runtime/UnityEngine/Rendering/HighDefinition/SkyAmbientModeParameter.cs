using System;
using System.Diagnostics;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000170 RID: 368
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public sealed class SkyAmbientModeParameter : VolumeParameter<SkyAmbientMode>
	{
		// Token: 0x06000AB0 RID: 2736 RVA: 0x00052B95 File Offset: 0x00050D95
		public SkyAmbientModeParameter(SkyAmbientMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
