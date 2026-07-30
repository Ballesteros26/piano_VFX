using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000098 RID: 152
	[VolumeComponentMenu("Shadowing/Shadows")]
	[Serializable]
	public class HDShadowSettings : VolumeComponent
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00032034 File Offset: 0x00030234
		public float[] cascadeShadowSplits
		{
			get
			{
				this.m_CascadeShadowSplits[0] = this.cascadeShadowSplit0.value;
				this.m_CascadeShadowSplits[1] = this.cascadeShadowSplit1.value;
				this.m_CascadeShadowSplits[2] = this.cascadeShadowSplit2.value;
				return this.m_CascadeShadowSplits;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x00032080 File Offset: 0x00030280
		public float[] cascadeShadowBorders
		{
			get
			{
				this.m_CascadeShadowBorders[0] = this.cascadeShadowBorder0.value;
				this.m_CascadeShadowBorders[1] = this.cascadeShadowBorder1.value;
				this.m_CascadeShadowBorders[2] = this.cascadeShadowBorder2.value;
				this.m_CascadeShadowBorders[3] = this.cascadeShadowBorder3.value;
				if (!HDRenderPipeline.s_UseCascadeBorders)
				{
					this.m_CascadeShadowBorders[this.cascadeShadowSplitCount.value - 1] = 0.2f;
				}
				return this.m_CascadeShadowBorders;
			}
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x00032100 File Offset: 0x00030300
		private HDShadowSettings()
		{
			base.displayName = "Shadows";
			this.cascadeShadowSplit0.Init(this.cascadeShadowSplitCount, 2, this.maxShadowDistance, null, this.cascadeShadowSplit1);
			this.cascadeShadowSplit1.Init(this.cascadeShadowSplitCount, 3, this.maxShadowDistance, this.cascadeShadowSplit0, this.cascadeShadowSplit2);
			this.cascadeShadowSplit2.Init(this.cascadeShadowSplitCount, 4, this.maxShadowDistance, this.cascadeShadowSplit1, null);
			this.cascadeShadowBorder0.Init(this.cascadeShadowSplitCount, 1, this.maxShadowDistance, null, this.cascadeShadowSplit0);
			this.cascadeShadowBorder1.Init(this.cascadeShadowSplitCount, 2, this.maxShadowDistance, this.cascadeShadowSplit0, this.cascadeShadowSplit1);
			this.cascadeShadowBorder2.Init(this.cascadeShadowSplitCount, 3, this.maxShadowDistance, this.cascadeShadowSplit1, this.cascadeShadowSplit2);
			this.cascadeShadowBorder3.Init(this.cascadeShadowSplitCount, 4, this.maxShadowDistance, this.cascadeShadowSplit2, null);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x000322DC File Offset: 0x000304DC
		internal void InitNormalized(bool normalized)
		{
			this.cascadeShadowSplit0.normalized = normalized;
			this.cascadeShadowSplit1.normalized = normalized;
			this.cascadeShadowSplit2.normalized = normalized;
			this.cascadeShadowBorder0.normalized = normalized;
			this.cascadeShadowBorder1.normalized = normalized;
			this.cascadeShadowBorder2.normalized = normalized;
			this.cascadeShadowBorder3.normalized = normalized;
		}

		// Token: 0x0400063F RID: 1599
		private float[] m_CascadeShadowSplits = new float[3];

		// Token: 0x04000640 RID: 1600
		private float[] m_CascadeShadowBorders = new float[4];

		// Token: 0x04000641 RID: 1601
		[Tooltip("Sets the maximum distance HDRP renders shadows for all Light types.")]
		public NoInterpMinFloatParameter maxShadowDistance = new NoInterpMinFloatParameter(500f, 0f, false);

		// Token: 0x04000642 RID: 1602
		[Tooltip("Multiplier for thick transmission.")]
		public ClampedFloatParameter directionalTransmissionMultiplier = new ClampedFloatParameter(1f, 0f, 1f, false);

		// Token: 0x04000643 RID: 1603
		[Tooltip("Controls the number of cascades HDRP uses for cascaded shadow maps.")]
		public NoInterpClampedIntParameter cascadeShadowSplitCount = new NoInterpClampedIntParameter(4, 1, 4, false);

		// Token: 0x04000644 RID: 1604
		[Tooltip("Sets the position of the first cascade split as a percentage of Max Distance.")]
		public CascadePartitionSplitParameter cascadeShadowSplit0 = new CascadePartitionSplitParameter(0.05f, false, false);

		// Token: 0x04000645 RID: 1605
		[Tooltip("Sets the position of the second cascade split as a percentage of Max Distance.")]
		public CascadePartitionSplitParameter cascadeShadowSplit1 = new CascadePartitionSplitParameter(0.15f, false, false);

		// Token: 0x04000646 RID: 1606
		[Tooltip("Position of the third cascade split as a percentage of Max Distance.")]
		public CascadePartitionSplitParameter cascadeShadowSplit2 = new CascadePartitionSplitParameter(0.3f, false, false);

		// Token: 0x04000647 RID: 1607
		[Tooltip("Sets the border size between the first and second cascade split.")]
		public CascadeEndBorderParameter cascadeShadowBorder0 = new CascadeEndBorderParameter(0f, false, false);

		// Token: 0x04000648 RID: 1608
		[Tooltip("Sets the border size between the second and third cascade split.")]
		public CascadeEndBorderParameter cascadeShadowBorder1 = new CascadeEndBorderParameter(0f, false, false);

		// Token: 0x04000649 RID: 1609
		[Tooltip("Sets the border size between the third and last cascade split.")]
		public CascadeEndBorderParameter cascadeShadowBorder2 = new CascadeEndBorderParameter(0f, false, false);

		// Token: 0x0400064A RID: 1610
		[Tooltip("Sets the border size at the end of the last cascade split.")]
		public CascadeEndBorderParameter cascadeShadowBorder3 = new CascadeEndBorderParameter(0f, false, false);
	}
}
