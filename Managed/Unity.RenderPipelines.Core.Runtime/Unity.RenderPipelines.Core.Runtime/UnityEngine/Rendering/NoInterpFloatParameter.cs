using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000076 RID: 118
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpFloatParameter : VolumeParameter<float>
	{
		// Token: 0x06000339 RID: 825 RVA: 0x0000D2BC File Offset: 0x0000B4BC
		public NoInterpFloatParameter(float value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
