using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000074 RID: 116
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpClampedIntParameter : VolumeParameter<int>
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000D1BA File Offset: 0x0000B3BA
		// (set) Token: 0x06000335 RID: 821 RVA: 0x0000D289 File Offset: 0x0000B489
		public override int value
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

		// Token: 0x06000336 RID: 822 RVA: 0x0000D2A3 File Offset: 0x0000B4A3
		public NoInterpClampedIntParameter(int value, int min, int max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040001B5 RID: 437
		public int min;

		// Token: 0x040001B6 RID: 438
		public int max;
	}
}
