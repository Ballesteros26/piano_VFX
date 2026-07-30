using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006E RID: 110
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpIntParameter : VolumeParameter<int>
	{
		// Token: 0x06000324 RID: 804 RVA: 0x0000D19E File Offset: 0x0000B39E
		public NoInterpIntParameter(int value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
