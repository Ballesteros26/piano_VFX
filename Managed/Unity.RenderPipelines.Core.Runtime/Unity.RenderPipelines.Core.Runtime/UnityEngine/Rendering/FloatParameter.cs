using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000075 RID: 117
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class FloatParameter : VolumeParameter<float>
	{
		// Token: 0x06000337 RID: 823 RVA: 0x0000D2BC File Offset: 0x0000B4BC
		public FloatParameter(float value, bool overrideState = false)
			: base(value, overrideState)
		{
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000D2C6 File Offset: 0x0000B4C6
		public sealed override void Interp(float from, float to, float t)
		{
			this.m_Value = from + (to - from) * t;
		}
	}
}
