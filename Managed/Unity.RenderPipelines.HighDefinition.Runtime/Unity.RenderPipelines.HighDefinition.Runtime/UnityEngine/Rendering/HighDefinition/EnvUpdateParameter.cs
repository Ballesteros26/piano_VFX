using System;
using System.Diagnostics;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000165 RID: 357
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public sealed class EnvUpdateParameter : VolumeParameter<EnvironmentUpdateMode>
	{
		// Token: 0x06000A92 RID: 2706 RVA: 0x00052575 File Offset: 0x00050775
		public EnvUpdateParameter(EnvironmentUpdateMode value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
