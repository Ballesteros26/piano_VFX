using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000073 RID: 115
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class ClampedIntParameter : IntParameter
	{
		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000D1BA File Offset: 0x0000B3BA
		// (set) Token: 0x06000332 RID: 818 RVA: 0x0000D256 File Offset: 0x0000B456
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

		// Token: 0x06000333 RID: 819 RVA: 0x0000D270 File Offset: 0x0000B470
		public ClampedIntParameter(int value, int min, int max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040001B3 RID: 435
		public int min;

		// Token: 0x040001B4 RID: 436
		public int max;
	}
}
