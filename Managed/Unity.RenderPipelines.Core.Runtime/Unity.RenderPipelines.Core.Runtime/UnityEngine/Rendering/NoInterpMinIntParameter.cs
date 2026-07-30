using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000070 RID: 112
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpMinIntParameter : VolumeParameter<int>
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000D1BA File Offset: 0x0000B3BA
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0000D1E7 File Offset: 0x0000B3E7
		public override int value
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

		// Token: 0x0600032A RID: 810 RVA: 0x0000D1FB File Offset: 0x0000B3FB
		public NoInterpMinIntParameter(int value, int min, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
		}

		// Token: 0x040001B0 RID: 432
		public int min;
	}
}
