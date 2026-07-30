using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000078 RID: 120
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpMinFloatParameter : VolumeParameter<float>
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000D2D5 File Offset: 0x0000B4D5
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000D302 File Offset: 0x0000B502
		public override float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Max(value, this.min);
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000D316 File Offset: 0x0000B516
		public NoInterpMinFloatParameter(float value, float min, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
		}

		// Token: 0x040001B8 RID: 440
		public float min;
	}
}
