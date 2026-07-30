using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000DB RID: 219
	public class MinMaxSlider : BaseField<Vector2>
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000652 RID: 1618 RVA: 0x000197FF File Offset: 0x000179FF
		// (set) Token: 0x06000653 RID: 1619 RVA: 0x00019807 File Offset: 0x00017A07
		internal VisualElement dragElement { get; private set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000654 RID: 1620 RVA: 0x00019810 File Offset: 0x00017A10
		// (set) Token: 0x06000655 RID: 1621 RVA: 0x00019818 File Offset: 0x00017A18
		private VisualElement dragMinThumb { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000656 RID: 1622 RVA: 0x00019821 File Offset: 0x00017A21
		// (set) Token: 0x06000657 RID: 1623 RVA: 0x00019829 File Offset: 0x00017A29
		private VisualElement dragMaxThumb { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000658 RID: 1624 RVA: 0x00019832 File Offset: 0x00017A32
		// (set) Token: 0x06000659 RID: 1625 RVA: 0x0001983A File Offset: 0x00017A3A
		internal ClampedDragger<float> clampedDragger { get; private set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600065A RID: 1626 RVA: 0x00019844 File Offset: 0x00017A44
		// (set) Token: 0x0600065B RID: 1627 RVA: 0x00019861 File Offset: 0x00017A61
		public float minValue
		{
			get
			{
				return this.value.x;
			}
			set
			{
				base.value = this.ClampValues(new Vector2(value, base.rawValue.y));
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00019884 File Offset: 0x00017A84
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x000198A1 File Offset: 0x00017AA1
		public float maxValue
		{
			get
			{
				return this.value.y;
			}
			set
			{
				base.value = this.ClampValues(new Vector2(base.rawValue.x, value));
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x000198C4 File Offset: 0x00017AC4
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x000198DC File Offset: 0x00017ADC
		public override Vector2 value
		{
			get
			{
				return base.value;
			}
			set
			{
				base.value = this.ClampValues(value);
			}
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x000198ED File Offset: 0x00017AED
		public override void SetValueWithoutNotify(Vector2 newValue)
		{
			base.SetValueWithoutNotify(this.ClampValues(newValue));
			this.UpdateDragElementPosition();
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x00019908 File Offset: 0x00017B08
		public float range
		{
			get
			{
				return Math.Abs(this.highLimit - this.lowLimit);
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x0001992C File Offset: 0x00017B2C
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x00019944 File Offset: 0x00017B44
		public float lowLimit
		{
			get
			{
				return this.m_MinLimit;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_MinLimit, value);
				if (flag)
				{
					bool flag2 = value > this.m_MaxLimit;
					if (flag2)
					{
						throw new ArgumentException("lowLimit is greater than highLimit");
					}
					this.m_MinLimit = value;
					this.value = base.rawValue;
					this.UpdateDragElementPosition();
					bool flag3 = !string.IsNullOrEmpty(base.viewDataKey);
					if (flag3)
					{
						base.SaveViewData();
					}
				}
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x000199B4 File Offset: 0x00017BB4
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x000199CC File Offset: 0x00017BCC
		public float highLimit
		{
			get
			{
				return this.m_MaxLimit;
			}
			set
			{
				bool flag = !Mathf.Approximately(this.m_MaxLimit, value);
				if (flag)
				{
					bool flag2 = value < this.m_MinLimit;
					if (flag2)
					{
						throw new ArgumentException("highLimit is smaller than lowLimit");
					}
					this.m_MaxLimit = value;
					this.value = base.rawValue;
					this.UpdateDragElementPosition();
					bool flag3 = !string.IsNullOrEmpty(base.viewDataKey);
					if (flag3)
					{
						base.SaveViewData();
					}
				}
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x00019A3C File Offset: 0x00017C3C
		public MinMaxSlider()
			: this(null, 0f, 10f, float.MinValue, float.MaxValue)
		{
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x00019A5B File Offset: 0x00017C5B
		public MinMaxSlider(float minValue, float maxValue, float minLimit, float maxLimit)
			: this(null, minValue, maxValue, minLimit, maxLimit)
		{
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00019A6C File Offset: 0x00017C6C
		public MinMaxSlider(string label, float minValue = 0f, float maxValue = 10f, float minLimit = -3.4028235E+38f, float maxLimit = 3.4028235E+38f)
			: base(label, null)
		{
			this.lowLimit = minLimit;
			this.highLimit = maxLimit;
			this.minValue = minValue;
			this.maxValue = maxValue;
			base.AddToClassList(MinMaxSlider.ussClassName);
			base.labelElement.AddToClassList(MinMaxSlider.labelUssClassName);
			base.visualInput.AddToClassList(MinMaxSlider.inputUssClassName);
			base.pickingMode = PickingMode.Ignore;
			this.m_DragState = MinMaxSlider.DragState.NoThumb;
			base.visualInput.pickingMode = PickingMode.Position;
			VisualElement visualElement = new VisualElement
			{
				name = "unity-tracker"
			};
			visualElement.AddToClassList(MinMaxSlider.trackerUssClassName);
			base.visualInput.Add(visualElement);
			this.dragElement = new VisualElement
			{
				name = "unity-dragger"
			};
			this.dragElement.AddToClassList(MinMaxSlider.draggerUssClassName);
			this.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.UpdateDragElementPosition), TrickleDown.NoTrickleDown);
			base.visualInput.Add(this.dragElement);
			this.dragMinThumb = new VisualElement
			{
				name = "unity-thumb-min"
			};
			this.dragMaxThumb = new VisualElement
			{
				name = "unity-thumb-max"
			};
			this.dragMinThumb.AddToClassList(MinMaxSlider.minThumbUssClassName);
			this.dragMaxThumb.AddToClassList(MinMaxSlider.maxThumbUssClassName);
			this.dragElement.Add(this.dragMinThumb);
			this.dragElement.Add(this.dragMaxThumb);
			this.clampedDragger = new ClampedDragger<float>(null, new Action(this.SetSliderValueFromClick), new Action(this.SetSliderValueFromDrag));
			base.visualInput.AddManipulator(this.clampedDragger);
			this.m_MinLimit = minLimit;
			this.m_MaxLimit = maxLimit;
			base.rawValue = this.ClampValues(new Vector2(minValue, maxValue));
			this.UpdateDragElementPosition();
		}

		// Token: 0x06000669 RID: 1641 RVA: 0x00019C48 File Offset: 0x00017E48
		private Vector2 ClampValues(Vector2 valueToClamp)
		{
			bool flag = this.m_MinLimit > this.m_MaxLimit;
			if (flag)
			{
				this.m_MinLimit = this.m_MaxLimit;
			}
			Vector2 vector = default(Vector2);
			bool flag2 = valueToClamp.y > this.m_MaxLimit;
			if (flag2)
			{
				valueToClamp.y = this.m_MaxLimit;
			}
			vector.x = Mathf.Clamp(valueToClamp.x, this.m_MinLimit, valueToClamp.y);
			vector.y = Mathf.Clamp(valueToClamp.y, valueToClamp.x, this.m_MaxLimit);
			return vector;
		}

		// Token: 0x0600066A RID: 1642 RVA: 0x00019CE0 File Offset: 0x00017EE0
		private void UpdateDragElementPosition(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateDragElementPosition();
			}
		}

		// Token: 0x0600066B RID: 1643 RVA: 0x00019D20 File Offset: 0x00017F20
		private void UpdateDragElementPosition()
		{
			bool flag = base.panel == null;
			if (!flag)
			{
				float num = -this.dragElement.resolvedStyle.marginLeft - this.dragElement.resolvedStyle.marginRight;
				int num2 = this.dragElement.resolvedStyle.unitySliceLeft + this.dragElement.resolvedStyle.unitySliceRight;
				float num3 = Mathf.Round(this.SliderLerpUnclamped((float)this.dragElement.resolvedStyle.unitySliceLeft, base.visualInput.layout.width + num - (float)this.dragElement.resolvedStyle.unitySliceRight, this.SliderNormalizeValue(this.minValue, this.lowLimit, this.highLimit)) - (float)this.dragElement.resolvedStyle.unitySliceLeft);
				float num4 = Mathf.Round(this.SliderLerpUnclamped((float)this.dragElement.resolvedStyle.unitySliceLeft, base.visualInput.layout.width + num - (float)this.dragElement.resolvedStyle.unitySliceRight, this.SliderNormalizeValue(this.maxValue, this.lowLimit, this.highLimit)) + (float)this.dragElement.resolvedStyle.unitySliceRight);
				this.dragElement.style.width = Mathf.Max((float)num2, num4 - num3);
				this.dragElement.style.left = num3;
				this.m_DragMinThumbRect = new Rect(this.dragElement.resolvedStyle.left, this.dragElement.layout.yMin, (float)this.dragElement.resolvedStyle.unitySliceLeft, this.dragElement.resolvedStyle.height);
				this.m_DragMaxThumbRect = new Rect(this.dragElement.resolvedStyle.left + (this.dragElement.resolvedStyle.width - (float)this.dragElement.resolvedStyle.unitySliceRight), this.dragElement.layout.yMin, (float)this.dragElement.resolvedStyle.unitySliceRight, this.dragElement.resolvedStyle.height);
				this.dragMaxThumb.style.left = this.dragElement.resolvedStyle.width - (float)this.dragElement.resolvedStyle.unitySliceRight;
				this.dragMaxThumb.style.top = 0f;
				this.dragMinThumb.style.width = this.m_DragMinThumbRect.width;
				this.dragMinThumb.style.height = this.m_DragMinThumbRect.height;
				this.dragMinThumb.style.left = 0f;
				this.dragMinThumb.style.top = 0f;
				this.dragMaxThumb.style.width = this.m_DragMaxThumbRect.width;
				this.dragMaxThumb.style.height = this.m_DragMaxThumbRect.height;
			}
		}

		// Token: 0x0600066C RID: 1644 RVA: 0x0001A06C File Offset: 0x0001826C
		internal float SliderLerpUnclamped(float a, float b, float interpolant)
		{
			return Mathf.LerpUnclamped(a, b, interpolant);
		}

		// Token: 0x0600066D RID: 1645 RVA: 0x0001A088 File Offset: 0x00018288
		internal float SliderNormalizeValue(float currentValue, float lowerValue, float higherValue)
		{
			return (currentValue - lowerValue) / (higherValue - lowerValue);
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x0001A0A4 File Offset: 0x000182A4
		private float ComputeValueFromPosition(float positionToConvert)
		{
			float num = this.SliderNormalizeValue(positionToConvert, (float)this.dragElement.resolvedStyle.unitySliceLeft, base.visualInput.layout.width - (float)this.dragElement.resolvedStyle.unitySliceRight);
			return this.SliderLerpUnclamped(this.lowLimit, this.highLimit, num);
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001A110 File Offset: 0x00018310
		protected override void ExecuteDefaultAction(EventBase evt)
		{
			base.ExecuteDefaultAction(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<GeometryChangedEvent>.TypeId();
				if (flag2)
				{
					this.UpdateDragElementPosition((GeometryChangedEvent)evt);
				}
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x0001A154 File Offset: 0x00018354
		private void SetSliderValueFromDrag()
		{
			bool flag = this.clampedDragger.dragDirection != ClampedDragger<float>.DragDirection.Free;
			if (!flag)
			{
				float x = this.m_DragElementStartPos.x;
				float num = x + this.clampedDragger.delta.x;
				this.ComputeValueFromDraggingThumb(x, num);
			}
		}

		// Token: 0x06000671 RID: 1649 RVA: 0x0001A1A4 File Offset: 0x000183A4
		private void SetSliderValueFromClick()
		{
			bool flag = this.clampedDragger.dragDirection == ClampedDragger<float>.DragDirection.Free;
			if (!flag)
			{
				bool flag2 = this.m_DragMinThumbRect.Contains(this.clampedDragger.startMousePosition);
				if (flag2)
				{
					this.m_DragState = MinMaxSlider.DragState.MinThumb;
				}
				else
				{
					bool flag3 = this.m_DragMaxThumbRect.Contains(this.clampedDragger.startMousePosition);
					if (flag3)
					{
						this.m_DragState = MinMaxSlider.DragState.MaxThumb;
					}
					else
					{
						bool flag4 = this.dragElement.layout.Contains(this.clampedDragger.startMousePosition);
						if (flag4)
						{
							this.m_DragState = MinMaxSlider.DragState.MiddleThumb;
						}
						else
						{
							this.m_DragState = MinMaxSlider.DragState.NoThumb;
						}
					}
				}
				bool flag5 = this.m_DragState == MinMaxSlider.DragState.NoThumb;
				if (flag5)
				{
					this.m_DragElementStartPos = new Vector2(this.clampedDragger.startMousePosition.x, this.dragElement.resolvedStyle.top);
					this.clampedDragger.dragDirection = ClampedDragger<float>.DragDirection.Free;
					this.ComputeValueDragStateNoThumb((float)this.dragElement.resolvedStyle.unitySliceLeft, base.visualInput.layout.width - (float)this.dragElement.resolvedStyle.unitySliceRight, this.m_DragElementStartPos.x);
					this.m_DragState = MinMaxSlider.DragState.MiddleThumb;
					this.m_ValueStartPos = this.value;
				}
				else
				{
					this.m_ValueStartPos = this.value;
					this.clampedDragger.dragDirection = ClampedDragger<float>.DragDirection.Free;
					this.m_DragElementStartPos = this.clampedDragger.startMousePosition;
				}
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001A320 File Offset: 0x00018520
		private void ComputeValueDragStateNoThumb(float lowLimitPosition, float highLimitPosition, float dragElementPos)
		{
			bool flag = dragElementPos < lowLimitPosition;
			float num;
			if (flag)
			{
				num = this.lowLimit;
			}
			else
			{
				bool flag2 = dragElementPos > highLimitPosition;
				if (flag2)
				{
					num = this.highLimit;
				}
				else
				{
					num = this.ComputeValueFromPosition(dragElementPos);
				}
			}
			float num2 = this.maxValue - this.minValue;
			float num3 = num - num2;
			float num4 = num;
			bool flag3 = num3 < this.lowLimit;
			if (flag3)
			{
				num3 = this.lowLimit;
				num4 = num3 + num2;
			}
			this.value = new Vector2(num3, num4);
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001A3A0 File Offset: 0x000185A0
		private void ComputeValueFromDraggingThumb(float dragElementStartPos, float dragElementEndPos)
		{
			float num = this.ComputeValueFromPosition(dragElementStartPos);
			float num2 = this.ComputeValueFromPosition(dragElementEndPos);
			float num3 = num2 - num;
			switch (this.m_DragState)
			{
			case MinMaxSlider.DragState.MinThumb:
			{
				float num4 = this.m_ValueStartPos.x + num3;
				bool flag = num4 > this.maxValue;
				if (flag)
				{
					num4 = this.maxValue;
				}
				else
				{
					bool flag2 = num4 < this.lowLimit;
					if (flag2)
					{
						num4 = this.lowLimit;
					}
				}
				this.value = new Vector2(num4, this.maxValue);
				break;
			}
			case MinMaxSlider.DragState.MiddleThumb:
			{
				Vector2 value = this.value;
				value.x = this.m_ValueStartPos.x + num3;
				value.y = this.m_ValueStartPos.y + num3;
				float num5 = this.m_ValueStartPos.y - this.m_ValueStartPos.x;
				bool flag3 = value.x < this.lowLimit;
				if (flag3)
				{
					value.x = this.lowLimit;
					value.y = this.lowLimit + num5;
				}
				else
				{
					bool flag4 = value.y > this.highLimit;
					if (flag4)
					{
						value.y = this.highLimit;
						value.x = this.highLimit - num5;
					}
				}
				this.value = value;
				break;
			}
			case MinMaxSlider.DragState.MaxThumb:
			{
				float num6 = this.m_ValueStartPos.y + num3;
				bool flag5 = num6 < this.minValue;
				if (flag5)
				{
					num6 = this.minValue;
				}
				else
				{
					bool flag6 = num6 > this.highLimit;
					if (flag6)
					{
						num6 = this.highLimit;
					}
				}
				this.value = new Vector2(this.minValue, num6);
				break;
			}
			}
		}

		// Token: 0x040002DD RID: 733
		private Vector2 m_DragElementStartPos;

		// Token: 0x040002DE RID: 734
		private Vector2 m_ValueStartPos;

		// Token: 0x040002DF RID: 735
		private Rect m_DragMinThumbRect;

		// Token: 0x040002E0 RID: 736
		private Rect m_DragMaxThumbRect;

		// Token: 0x040002E1 RID: 737
		private MinMaxSlider.DragState m_DragState;

		// Token: 0x040002E2 RID: 738
		private float m_MinLimit;

		// Token: 0x040002E3 RID: 739
		private float m_MaxLimit;

		// Token: 0x040002E4 RID: 740
		internal const float kDefaultHighValue = 10f;

		// Token: 0x040002E5 RID: 741
		public new static readonly string ussClassName = "unity-min-max-slider";

		// Token: 0x040002E6 RID: 742
		public new static readonly string labelUssClassName = MinMaxSlider.ussClassName + "__label";

		// Token: 0x040002E7 RID: 743
		public new static readonly string inputUssClassName = MinMaxSlider.ussClassName + "__input";

		// Token: 0x040002E8 RID: 744
		public static readonly string trackerUssClassName = MinMaxSlider.ussClassName + "__tracker";

		// Token: 0x040002E9 RID: 745
		public static readonly string draggerUssClassName = MinMaxSlider.ussClassName + "__dragger";

		// Token: 0x040002EA RID: 746
		public static readonly string minThumbUssClassName = MinMaxSlider.ussClassName + "__min-thumb";

		// Token: 0x040002EB RID: 747
		public static readonly string maxThumbUssClassName = MinMaxSlider.ussClassName + "__max-thumb";

		// Token: 0x020000DC RID: 220
		public new class UxmlFactory : UxmlFactory<MinMaxSlider, MinMaxSlider.UxmlTraits>
		{
		}

		// Token: 0x020000DD RID: 221
		public new class UxmlTraits : BaseField<Vector2>.UxmlTraits
		{
			// Token: 0x06000676 RID: 1654 RVA: 0x0001A5F4 File Offset: 0x000187F4
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				MinMaxSlider minMaxSlider = (MinMaxSlider)ve;
				minMaxSlider.minValue = this.m_MinValue.GetValueFromBag(bag, cc);
				minMaxSlider.maxValue = this.m_MaxValue.GetValueFromBag(bag, cc);
				minMaxSlider.lowLimit = this.m_LowLimit.GetValueFromBag(bag, cc);
				minMaxSlider.highLimit = this.m_HighLimit.GetValueFromBag(bag, cc);
			}

			// Token: 0x040002EC RID: 748
			private UxmlFloatAttributeDescription m_MinValue = new UxmlFloatAttributeDescription
			{
				name = "min-value",
				defaultValue = 0f
			};

			// Token: 0x040002ED RID: 749
			private UxmlFloatAttributeDescription m_MaxValue = new UxmlFloatAttributeDescription
			{
				name = "max-value",
				defaultValue = 10f
			};

			// Token: 0x040002EE RID: 750
			private UxmlFloatAttributeDescription m_LowLimit = new UxmlFloatAttributeDescription
			{
				name = "low-limit",
				defaultValue = float.MinValue
			};

			// Token: 0x040002EF RID: 751
			private UxmlFloatAttributeDescription m_HighLimit = new UxmlFloatAttributeDescription
			{
				name = "high-limit",
				defaultValue = float.MaxValue
			};
		}

		// Token: 0x020000DE RID: 222
		private enum DragState
		{
			// Token: 0x040002F1 RID: 753
			NoThumb,
			// Token: 0x040002F2 RID: 754
			MinThumb,
			// Token: 0x040002F3 RID: 755
			MiddleThumb,
			// Token: 0x040002F4 RID: 756
			MaxThumb
		}
	}
}
