using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200009A RID: 154
	[Serializable]
	public class CascadeEndBorderParameter : VolumeParameter<float>
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x000323EC File Offset: 0x000305EC
		internal float representationDistance
		{
			get
			{
				float num = ((this.cascadeCounts.value > this.minCascadeToAppears && this.max != null) ? this.max.value : 1f);
				CascadePartitionSplitParameter cascadePartitionSplitParameter = this.min;
				return (num - ((cascadePartitionSplitParameter != null) ? cascadePartitionSplitParameter.value : 0f)) * this.maxDistance.value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x0003238F File Offset: 0x0003058F
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x00032449 File Offset: 0x00030649
		public override float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Clamp01(value);
			}
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00032457 File Offset: 0x00030657
		public CascadeEndBorderParameter(float value, bool normalized = false, bool overrideState = false)
			: base(value, overrideState)
		{
			this.normalized = normalized;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00032468 File Offset: 0x00030668
		internal void Init(NoInterpClampedIntParameter cascadeCounts, int minCascadeToAppears, NoInterpMinFloatParameter maxDistance, CascadePartitionSplitParameter min, CascadePartitionSplitParameter max)
		{
			this.maxDistance = maxDistance;
			this.min = min;
			this.max = max;
			this.cascadeCounts = cascadeCounts;
			this.minCascadeToAppears = minCascadeToAppears;
		}

		// Token: 0x04000651 RID: 1617
		internal bool normalized;

		// Token: 0x04000652 RID: 1618
		[NonSerialized]
		private CascadePartitionSplitParameter min;

		// Token: 0x04000653 RID: 1619
		[NonSerialized]
		private CascadePartitionSplitParameter max;

		// Token: 0x04000654 RID: 1620
		[NonSerialized]
		private NoInterpMinFloatParameter maxDistance;

		// Token: 0x04000655 RID: 1621
		[NonSerialized]
		private NoInterpClampedIntParameter cascadeCounts;

		// Token: 0x04000656 RID: 1622
		private int minCascadeToAppears;
	}
}
