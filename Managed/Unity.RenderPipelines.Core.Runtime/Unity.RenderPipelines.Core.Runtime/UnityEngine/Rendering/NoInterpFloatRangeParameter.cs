using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200007E RID: 126
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpFloatRangeParameter : VolumeParameter<Vector2>
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000D3D7 File Offset: 0x0000B5D7
		// (set) Token: 0x06000351 RID: 849 RVA: 0x0000D483 File Offset: 0x0000B683
		public override Vector2 value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value.x = Mathf.Max(value.x, this.min);
				this.m_Value.y = Mathf.Min(value.y, this.max);
			}
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000D4BD File Offset: 0x0000B6BD
		public NoInterpFloatRangeParameter(Vector2 value, float min, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040001C1 RID: 449
		public float min;

		// Token: 0x040001C2 RID: 450
		public float max;
	}
}
