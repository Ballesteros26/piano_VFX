using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x0200007D RID: 125
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class FloatRangeParameter : VolumeParameter<Vector2>
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000D3D7 File Offset: 0x0000B5D7
		// (set) Token: 0x0600034D RID: 845 RVA: 0x0000D3DF File Offset: 0x0000B5DF
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

		// Token: 0x0600034E RID: 846 RVA: 0x0000D419 File Offset: 0x0000B619
		public FloatRangeParameter(Vector2 value, float min, float max, bool overrideState = false)
			: base(value, overrideState)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000D434 File Offset: 0x0000B634
		public override void Interp(Vector2 from, Vector2 to, float t)
		{
			this.m_Value.x = from.x + (to.x - from.x) * t;
			this.m_Value.y = from.y + (to.y - from.y) * t;
		}

		// Token: 0x040001BF RID: 447
		public float min;

		// Token: 0x040001C0 RID: 448
		public float max;
	}
}
