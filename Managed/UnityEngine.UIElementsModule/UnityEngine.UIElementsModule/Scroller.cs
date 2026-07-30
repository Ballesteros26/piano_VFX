using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	// Token: 0x020000E6 RID: 230
	public class Scroller : VisualElement
	{
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x0600068D RID: 1677 RVA: 0x0001A978 File Offset: 0x00018B78
		// (remove) Token: 0x0600068E RID: 1678 RVA: 0x0001A9B0 File Offset: 0x00018BB0
		[field: DebuggerBrowsable(0)]
		public event Action<float> valueChanged;

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0001A9E5 File Offset: 0x00018BE5
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x0001A9ED File Offset: 0x00018BED
		public Slider slider { get; private set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0001A9F6 File Offset: 0x00018BF6
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x0001A9FE File Offset: 0x00018BFE
		public RepeatButton lowButton { get; private set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0001AA07 File Offset: 0x00018C07
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x0001AA0F File Offset: 0x00018C0F
		public RepeatButton highButton { get; private set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001AA18 File Offset: 0x00018C18
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x0001AA35 File Offset: 0x00018C35
		public float value
		{
			get
			{
				return this.slider.value;
			}
			set
			{
				this.slider.value = value;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0001AA48 File Offset: 0x00018C48
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x0001AA65 File Offset: 0x00018C65
		public float lowValue
		{
			get
			{
				return this.slider.lowValue;
			}
			set
			{
				this.slider.lowValue = value;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0001AA78 File Offset: 0x00018C78
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x0001AA95 File Offset: 0x00018C95
		public float highValue
		{
			get
			{
				return this.slider.highValue;
			}
			set
			{
				this.slider.highValue = value;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0001AAA8 File Offset: 0x00018CA8
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x0001AACC File Offset: 0x00018CCC
		public SliderDirection direction
		{
			get
			{
				return (base.resolvedStyle.flexDirection == FlexDirection.Row) ? SliderDirection.Horizontal : SliderDirection.Vertical;
			}
			set
			{
				this.slider.direction = value;
				bool flag = value == SliderDirection.Horizontal;
				if (flag)
				{
					base.style.flexDirection = FlexDirection.Row;
					base.AddToClassList(Scroller.horizontalVariantUssClassName);
					base.RemoveFromClassList(Scroller.verticalVariantUssClassName);
				}
				else
				{
					base.style.flexDirection = FlexDirection.Column;
					base.AddToClassList(Scroller.verticalVariantUssClassName);
					base.RemoveFromClassList(Scroller.horizontalVariantUssClassName);
				}
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x0001AB49 File Offset: 0x00018D49
		public Scroller()
			: this(0f, 0f, null, SliderDirection.Vertical)
		{
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001AB60 File Offset: 0x00018D60
		public Scroller(float lowValue, float highValue, Action<float> valueChanged, SliderDirection direction = SliderDirection.Vertical)
		{
			base.AddToClassList(Scroller.ussClassName);
			this.slider = new Slider(lowValue, highValue, direction, 20f)
			{
				name = "unity-slider",
				viewDataKey = "Slider"
			};
			this.slider.AddToClassList(Scroller.sliderUssClassName);
			this.slider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnSliderValueChange));
			this.lowButton = new RepeatButton(new Action(this.ScrollPageUp), 250L, 30L)
			{
				name = "unity-low-button"
			};
			this.lowButton.AddToClassList(Scroller.lowButtonUssClassName);
			base.Add(this.lowButton);
			this.highButton = new RepeatButton(new Action(this.ScrollPageDown), 250L, 30L)
			{
				name = "unity-high-button"
			};
			this.highButton.AddToClassList(Scroller.highButtonUssClassName);
			base.Add(this.highButton);
			base.Add(this.slider);
			this.direction = direction;
			this.valueChanged = valueChanged;
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001AC8A File Offset: 0x00018E8A
		public void Adjust(float factor)
		{
			base.SetEnabled(factor < 1f);
			this.slider.AdjustDragElement(factor);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001ACA9 File Offset: 0x00018EA9
		private void OnSliderValueChange(ChangeEvent<float> evt)
		{
			this.value = evt.newValue;
			Action<float> action = this.valueChanged;
			if (action != null)
			{
				action.Invoke(this.slider.value);
			}
			base.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001ACE2 File Offset: 0x00018EE2
		public void ScrollPageUp()
		{
			this.ScrollPageUp(1f);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001ACF1 File Offset: 0x00018EF1
		public void ScrollPageDown()
		{
			this.ScrollPageDown(1f);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001AD00 File Offset: 0x00018F00
		public void ScrollPageUp(float factor)
		{
			this.value -= factor * (this.slider.pageSize * ((this.slider.lowValue < this.slider.highValue) ? 1f : (-1f)));
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001AD50 File Offset: 0x00018F50
		public void ScrollPageDown(float factor)
		{
			this.value += factor * (this.slider.pageSize * ((this.slider.lowValue < this.slider.highValue) ? 1f : (-1f)));
		}

		// Token: 0x04000304 RID: 772
		internal const float kDefaultPageSize = 20f;

		// Token: 0x04000305 RID: 773
		public static readonly string ussClassName = "unity-scroller";

		// Token: 0x04000306 RID: 774
		public static readonly string horizontalVariantUssClassName = Scroller.ussClassName + "--horizontal";

		// Token: 0x04000307 RID: 775
		public static readonly string verticalVariantUssClassName = Scroller.ussClassName + "--vertical";

		// Token: 0x04000308 RID: 776
		public static readonly string sliderUssClassName = Scroller.ussClassName + "__slider";

		// Token: 0x04000309 RID: 777
		public static readonly string lowButtonUssClassName = Scroller.ussClassName + "__low-button";

		// Token: 0x0400030A RID: 778
		public static readonly string highButtonUssClassName = Scroller.ussClassName + "__high-button";

		// Token: 0x020000E7 RID: 231
		public new class UxmlFactory : UxmlFactory<Scroller, Scroller.UxmlTraits>
		{
		}

		// Token: 0x020000E8 RID: 232
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x17000181 RID: 385
			// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0001AE24 File Offset: 0x00019024
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x060006A8 RID: 1704 RVA: 0x0001AE44 File Offset: 0x00019044
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Scroller scroller = (Scroller)ve;
				scroller.slider.lowValue = this.m_LowValue.GetValueFromBag(bag, cc);
				scroller.slider.highValue = this.m_HighValue.GetValueFromBag(bag, cc);
				scroller.direction = this.m_Direction.GetValueFromBag(bag, cc);
				scroller.value = this.m_Value.GetValueFromBag(bag, cc);
			}

			// Token: 0x0400030B RID: 779
			private UxmlFloatAttributeDescription m_LowValue = new UxmlFloatAttributeDescription
			{
				name = "low-value",
				obsoleteNames = new string[] { "lowValue" }
			};

			// Token: 0x0400030C RID: 780
			private UxmlFloatAttributeDescription m_HighValue = new UxmlFloatAttributeDescription
			{
				name = "high-value",
				obsoleteNames = new string[] { "highValue" }
			};

			// Token: 0x0400030D RID: 781
			private UxmlEnumAttributeDescription<SliderDirection> m_Direction = new UxmlEnumAttributeDescription<SliderDirection>
			{
				name = "direction",
				defaultValue = SliderDirection.Vertical
			};

			// Token: 0x0400030E RID: 782
			private UxmlFloatAttributeDescription m_Value = new UxmlFloatAttributeDescription
			{
				name = "value"
			};
		}
	}
}
