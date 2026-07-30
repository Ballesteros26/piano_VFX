using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000EF RID: 239
	public class Slider : BaseSlider<float>
	{
		// Token: 0x060006E9 RID: 1769 RVA: 0x0001CC48 File Offset: 0x0001AE48
		public Slider()
			: this(null, 0f, 10f, SliderDirection.Horizontal, 0f)
		{
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001CC63 File Offset: 0x0001AE63
		public Slider(float start, float end, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f)
			: this(null, start, end, direction, pageSize)
		{
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001CC73 File Offset: 0x0001AE73
		public Slider(string label, float start = 0f, float end = 10f, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f)
			: base(label, start, end, direction, pageSize)
		{
			base.AddToClassList(Slider.ussClassName);
			base.labelElement.AddToClassList(Slider.labelUssClassName);
			base.visualInput.AddToClassList(Slider.inputUssClassName);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001CCB4 File Offset: 0x0001AEB4
		internal override float SliderLerpUnclamped(float a, float b, float interpolant)
		{
			return Mathf.LerpUnclamped(a, b, interpolant);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001CCD0 File Offset: 0x0001AED0
		internal override float SliderNormalizeValue(float currentValue, float lowerValue, float higherValue)
		{
			return (currentValue - lowerValue) / (higherValue - lowerValue);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001CCEC File Offset: 0x0001AEEC
		internal override float SliderRange()
		{
			return Math.Abs(base.highValue - base.lowValue);
		}

		// Token: 0x0400033F RID: 831
		internal const float kDefaultHighValue = 10f;

		// Token: 0x04000340 RID: 832
		public new static readonly string ussClassName = "unity-slider";

		// Token: 0x04000341 RID: 833
		public new static readonly string labelUssClassName = Slider.ussClassName + "__label";

		// Token: 0x04000342 RID: 834
		public new static readonly string inputUssClassName = Slider.ussClassName + "__input";

		// Token: 0x020000F0 RID: 240
		public new class UxmlFactory : UxmlFactory<Slider, Slider.UxmlTraits>
		{
		}

		// Token: 0x020000F1 RID: 241
		public new class UxmlTraits : BaseFieldTraits<float, UxmlFloatAttributeDescription>
		{
			// Token: 0x060006F1 RID: 1777 RVA: 0x0001CD50 File Offset: 0x0001AF50
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				Slider slider = (Slider)ve;
				slider.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				slider.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				slider.direction = this.m_Direction.GetValueFromBag(bag, cc);
				slider.pageSize = this.m_PageSize.GetValueFromBag(bag, cc);
				base.Init(ve, bag, cc);
			}

			// Token: 0x04000343 RID: 835
			private UxmlFloatAttributeDescription m_LowValue = new UxmlFloatAttributeDescription
			{
				name = "low-value"
			};

			// Token: 0x04000344 RID: 836
			private UxmlFloatAttributeDescription m_HighValue = new UxmlFloatAttributeDescription
			{
				name = "high-value",
				defaultValue = 10f
			};

			// Token: 0x04000345 RID: 837
			private UxmlFloatAttributeDescription m_PageSize = new UxmlFloatAttributeDescription
			{
				name = "page-size",
				defaultValue = 0f
			};

			// Token: 0x04000346 RID: 838
			private UxmlEnumAttributeDescription<SliderDirection> m_Direction = new UxmlEnumAttributeDescription<SliderDirection>
			{
				name = "direction",
				defaultValue = SliderDirection.Horizontal
			};
		}
	}
}
