using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000072 RID: 114
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpMaxIntParameter : VolumeParameter<int>
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000D1BA File Offset: 0x0000B3BA
		// (set) Token: 0x0600032F RID: 815 RVA: 0x0000D231 File Offset: 0x0000B431
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

		// Token: 0x06000330 RID: 816 RVA: 0x0000D245 File Offset: 0x0000B445
		public NoInterpMaxIntParameter(int value, int max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.max = max;
		}

		// Token: 0x040001B2 RID: 434
		public int max;
	}
}
