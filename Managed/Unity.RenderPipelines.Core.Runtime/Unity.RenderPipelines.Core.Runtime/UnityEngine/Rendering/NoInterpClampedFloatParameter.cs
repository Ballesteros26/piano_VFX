using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200007C RID: 124
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpClampedFloatParameter : VolumeParameter<float>
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000D2D5 File Offset: 0x0000B4D5
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000D3A4 File Offset: 0x0000B5A4
		public override float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Clamp(value, this.min, this.max);
			}
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000D3BE File Offset: 0x0000B5BE
		public NoInterpClampedFloatParameter(float value, float min, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040001BD RID: 445
		public float min;

		// Token: 0x040001BE RID: 446
		public float max;
	}
}
