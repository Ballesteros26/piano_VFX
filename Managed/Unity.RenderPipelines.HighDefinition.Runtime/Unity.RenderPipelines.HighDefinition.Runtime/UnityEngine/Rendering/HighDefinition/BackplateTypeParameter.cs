using System;
using System.Diagnostics;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000168 RID: 360
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public sealed class BackplateTypeParameter : VolumeParameter<BackplateType>
	{
		// Token: 0x06000A93 RID: 2707 RVA: 0x0005257F File Offset: 0x0005077F
		public BackplateTypeParameter(BackplateType value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
