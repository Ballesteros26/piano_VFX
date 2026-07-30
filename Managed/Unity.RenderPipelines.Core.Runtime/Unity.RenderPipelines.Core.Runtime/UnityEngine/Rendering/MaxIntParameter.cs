using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000071 RID: 113
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class MaxIntParameter : IntParameter
	{
		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000D1BA File Offset: 0x0000B3BA
		// (set) Token: 0x0600032C RID: 812 RVA: 0x0000D20C File Offset: 0x0000B40C
		public override int value
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

		// Token: 0x0600032D RID: 813 RVA: 0x0000D220 File Offset: 0x0000B420
		public MaxIntParameter(int value, int max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.max = max;
		}

		// Token: 0x040001B1 RID: 433
		public int max;
	}
}
