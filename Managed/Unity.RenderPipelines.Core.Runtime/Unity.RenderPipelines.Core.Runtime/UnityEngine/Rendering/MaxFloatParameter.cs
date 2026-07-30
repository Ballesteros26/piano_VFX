using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000079 RID: 121
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class MaxFloatParameter : FloatParameter
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000340 RID: 832 RVA: 0x0000D2D5 File Offset: 0x0000B4D5
		// (set) Token: 0x06000341 RID: 833 RVA: 0x0000D327 File Offset: 0x0000B527
		public override float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Min(value, this.max);
			}
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000D33B File Offset: 0x0000B53B
		public MaxFloatParameter(float value, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.max = max;
		}

		// Token: 0x040001B9 RID: 441
		public float max;
	}
}
