using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000099 RID: 153
	[Serializable]
	public class CascadePartitionSplitParameter : VolumeParameter<float>
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x0003233D File Offset: 0x0003053D
		internal float min
		{
			get
			{
				CascadePartitionSplitParameter cascadePartitionSplitParameter = this.previous;
				if (cascadePartitionSplitParameter == null)
				{
					return 0f;
				}
				return cascadePartitionSplitParameter.value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00032354 File Offset: 0x00030554
		internal float max
		{
			get
			{
				if (this.cascadeCounts.value <= this.minCascadeToAppears || this.next == null)
				{
					return 1f;
				}
				return this.next.value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x00032382 File Offset: 0x00030582
		internal float representationDistance
		{
			get
			{
				return this.maxDistance.value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0003238F File Offset: 0x0003058F
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x00032397 File Offset: 0x00030597
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

		// Token: 0x060005F5 RID: 1525 RVA: 0x000323B1 File Offset: 0x000305B1
		public CascadePartitionSplitParameter(float value, bool normalized = false, bool overrideState = false)
			: base(value, overrideState)
		{
			this.normalized = normalized;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x000323C2 File Offset: 0x000305C2
		internal void Init(NoInterpClampedIntParameter cascadeCounts, int minCascadeToAppears, NoInterpMinFloatParameter maxDistance, CascadePartitionSplitParameter previous, CascadePartitionSplitParameter next)
		{
			this.maxDistance = maxDistance;
			this.previous = previous;
			this.next = next;
			this.cascadeCounts = cascadeCounts;
			this.minCascadeToAppears = minCascadeToAppears;
		}

		// Token: 0x0400064B RID: 1611
		[NonSerialized]
		private NoInterpMinFloatParameter maxDistance;

		// Token: 0x0400064C RID: 1612
		internal bool normalized;

		// Token: 0x0400064D RID: 1613
		[NonSerialized]
		private CascadePartitionSplitParameter previous;

		// Token: 0x0400064E RID: 1614
		[NonSerialized]
		private CascadePartitionSplitParameter next;

		// Token: 0x0400064F RID: 1615
		[NonSerialized]
		private NoInterpClampedIntParameter cascadeCounts;

		// Token: 0x04000650 RID: 1616
		private int minCascadeToAppears;
	}
}
