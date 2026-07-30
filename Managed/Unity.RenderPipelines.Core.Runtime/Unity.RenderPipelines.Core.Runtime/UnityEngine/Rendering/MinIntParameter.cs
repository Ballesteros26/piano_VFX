using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200006F RID: 111
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class MinIntParameter : IntParameter
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000D1BA File Offset: 0x0000B3BA
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000D1C2 File Offset: 0x0000B3C2
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

		// Token: 0x06000327 RID: 807 RVA: 0x0000D1D6 File Offset: 0x0000B3D6
		public MinIntParameter(int value, int min, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
		}

		// Token: 0x040001AF RID: 431
		public int min;
	}
}
