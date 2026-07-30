using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200007B RID: 123
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class ClampedFloatParameter : FloatParameter
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000D2D5 File Offset: 0x0000B4D5
		// (set) Token: 0x06000347 RID: 839 RVA: 0x0000D371 File Offset: 0x0000B571
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

		// Token: 0x06000348 RID: 840 RVA: 0x0000D38B File Offset: 0x0000B58B
		public ClampedFloatParameter(float value, float min, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040001BB RID: 443
		public float min;

		// Token: 0x040001BC RID: 444
		public float max;
	}
}
