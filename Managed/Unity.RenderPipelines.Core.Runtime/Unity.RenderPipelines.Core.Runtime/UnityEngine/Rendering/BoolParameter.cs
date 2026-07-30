using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006B RID: 107
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class BoolParameter : VolumeParameter<bool>
	{
		// Token: 0x06000320 RID: 800 RVA: 0x0000D18A File Offset: 0x0000B38A
		public BoolParameter(bool value, bool overrideState = false)
			: base(value, overrideState)
		{
		}
	}
}
