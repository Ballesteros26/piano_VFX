using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x02000077 RID: 119
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class MinFloatParameter : FloatParameter
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000D2D5 File Offset: 0x0000B4D5
		// (set) Token: 0x0600033B RID: 827 RVA: 0x0000D2DD File Offset: 0x0000B4DD
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

		// Token: 0x0600033C RID: 828 RVA: 0x0000D2F1 File Offset: 0x0000B4F1
		public MinFloatParameter(float value, float min, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
		}

		// Token: 0x040001B7 RID: 439
		public float min;
	}
}
