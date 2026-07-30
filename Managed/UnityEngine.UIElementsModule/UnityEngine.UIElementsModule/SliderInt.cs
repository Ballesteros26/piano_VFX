using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000F2 RID: 242
	public class SliderInt : BaseSlider<int>
	{
		// Token: 0x060006F3 RID: 1779 RVA: 0x0001CE50 File Offset: 0x0001B050
		public SliderInt()
			: this(null, 0, 10, SliderDirection.Horizontal, 0f)
		{
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001CE64 File Offset: 0x0001B064
		public SliderInt(int start, int end, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f)
			: this(null, start, end, direction, pageSize)
		{
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001CE74 File Offset: 0x0001B074
		public SliderInt(string label, int start = 0, int end = 10, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f)
			: base(label, start, end, direction, pageSize)
		{
			base.AddToClassList(SliderInt.ussClassName);
			base.labelElement.AddToClassList(SliderInt.labelUssClassName);
			base.visualInput.AddToClassList(SliderInt.inputUssClassName);
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0001CEB4 File Offset: 0x0001B0B4
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x0001CECC File Offset: 0x0001B0CC
		public override float pageSize
		{
			get
			{
				return base.pageSize;
			}
			set
			{
				base.pageSize = (float)Mathf.RoundToInt(value);
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001CEE0 File Offset: 0x0001B0E0
		internal override int SliderLerpUnclamped(int a, int b, float interpolant)
		{
			return Mathf.RoundToInt(Mathf.LerpUnclamped((float)a, (float)b, interpolant));
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001CF04 File Offset: 0x0001B104
		internal override float SliderNormalizeValue(int currentValue, int lowerValue, int higherValue)
		{
			return ((float)currentValue - (float)lowerValue) / ((float)higherValue - (float)lowerValue);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001CF24 File Offset: 0x0001B124
		internal override int SliderRange()
		{
			return Math.Abs(base.highValue - base.lowValue);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001CF48 File Offset: 0x0001B148
		internal override void ComputeValueAndDirectionFromClick(float sliderLength, float dragElementLength, float dragElementPos, float dragElementLastPos)
		{
			bool flag = Mathf.Approximately(this.pageSize, 0f);
			if (flag)
			{
				base.ComputeValueAndDirectionFromClick(sliderLength, dragElementLength, dragElementPos, dragElementLastPos);
			}
			else
			{
				float num = sliderLength - dragElementLength;
				bool flag2 = Mathf.Abs(num) < Mathf.Epsilon;
				if (!flag2)
				{
					int num2 = (int)this.pageSize;
					bool flag3 = base.lowValue > base.highValue;
					if (flag3)
					{
						num2 = -num2;
					}
					bool flag4 = dragElementLastPos < dragElementPos && base.clampedDragger.dragDirection != ClampedDragger<int>.DragDirection.LowToHigh;
					if (flag4)
					{
						base.clampedDragger.dragDirection = ClampedDragger<int>.DragDirection.HighToLow;
						this.value -= num2;
					}
					else
					{
						bool flag5 = dragElementLastPos > dragElementPos + dragElementLength && base.clampedDragger.dragDirection != ClampedDragger<int>.DragDirection.HighToLow;
						if (flag5)
						{
							base.clampedDragger.dragDirection = ClampedDragger<int>.DragDirection.LowToHigh;
							this.value += num2;
						}
					}
				}
			}
		}

		// Token: 0x04000347 RID: 839
		internal const int kDefaultHighValue = 10;

		// Token: 0x04000348 RID: 840
		public new static readonly string ussClassName = "unity-slider-int";

		// Token: 0x04000349 RID: 841
		public new static readonly string labelUssClassName = SliderInt.ussClassName + "__label";

		// Token: 0x0400034A RID: 842
		public new static readonly string inputUssClassName = SliderInt.ussClassName + "__input";

		// Token: 0x020000F3 RID: 243
		public new class UxmlFactory : UxmlFactory<SliderInt, SliderInt.UxmlTraits>
		{
		}

		// Token: 0x020000F4 RID: 244
		public new class UxmlTraits : BaseFieldTraits<int, UxmlIntAttributeDescription>
		{
			// Token: 0x060006FE RID: 1790 RVA: 0x0001D078 File Offset: 0x0001B278
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				SliderInt sliderInt = (SliderInt)ve;
				sliderInt.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				sliderInt.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				sliderInt.direction = this.m_Direction.GetValueFromBag(bag, cc);
				sliderInt.pageSize = (float)this.m_PageSize.GetValueFromBag(bag, cc);
				base.Init(ve, bag, cc);
			}

			// Token: 0x0400034B RID: 843
			private UxmlIntAttributeDescription m_LowValue = new UxmlIntAttributeDescription
			{
				name = "low-value"
			};

			// Token: 0x0400034C RID: 844
			private UxmlIntAttributeDescription m_HighValue = new UxmlIntAttributeDescription
			{
				name = "high-value",
				defaultValue = 10
			};

			// Token: 0x0400034D RID: 845
			private UxmlIntAttributeDescription m_PageSize = new UxmlIntAttributeDescription
			{
				name = "page-size",
				defaultValue = 0
			};

			// Token: 0x0400034E RID: 846
			private UxmlEnumAttributeDescription<SliderDirection> m_Direction = new UxmlEnumAttributeDescription<SliderDirection>
			{
				name = "direction",
				defaultValue = SliderDirection.Horizontal
			};
		}
	}
}
