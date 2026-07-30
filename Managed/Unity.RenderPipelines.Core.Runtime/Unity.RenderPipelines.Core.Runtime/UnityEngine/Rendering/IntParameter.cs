using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006D RID: 109
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class IntParameter : VolumeParameter<int>
	{
		// Token: 0x06000322 RID: 802 RVA: 0x0000D19E File Offset: 0x0000B39E
		public IntParameter(int value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000D1A8 File Offset: 0x0000B3A8
		public sealed override void Interp(int from, int to, float t)
		{
			this.m_Value = (int)((float)from + (float)(to - from) * t);
		}
	}
}
