using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200007A RID: 122
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpMaxFloatParameter : VolumeParameter<float>
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000D2D5 File Offset: 0x0000B4D5
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0000D34C File Offset: 0x0000B54C
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

		// Token: 0x06000345 RID: 837 RVA: 0x0000D360 File Offset: 0x0000B560
		public NoInterpMaxFloatParameter(float value, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.max = max;
		}

		// Token: 0x040001BA RID: 442
		public float max;
	}
}
