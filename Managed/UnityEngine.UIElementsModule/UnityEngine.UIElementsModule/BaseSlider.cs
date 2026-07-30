using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020000BB RID: 187
	public abstract class BaseSlider<TValueType> : BaseField<TValueType> where TValueType : IComparable<TValueType>
	{
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x000149E0 File Offset: 0x00012BE0
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x000149E8 File Offset: 0x00012BE8
		internal VisualElement dragElement { get; private set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x000149F1 File Offset: 0x00012BF1
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x000149F9 File Offset: 0x00012BF9
		internal VisualElement dragBorderElement { get; private set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00014A04 File Offset: 0x00012C04
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x00014A1C File Offset: 0x00012C1C
		public TValueType lowValue
		{
			get
			{
				return this.m_LowValue;
			}
			set
			{
				bool flag = !EqualityComparer<TValueType>.Default.Equals(this.m_LowValue, value);
				if (flag)
				{
					this.m_LowValue = value;
					this.ClampValue();
					this.UpdateDragElementPosition();
					base.SaveViewData();
				}
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00014A60 File Offset: 0x00012C60
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x00014A78 File Offset: 0x00012C78
		public TValueType highValue
		{
			get
			{
				return this.m_HighValue;
			}
			set
			{
				bool flag = !EqualityComparer<TValueType>.Default.Equals(this.m_HighValue, value);
				if (flag)
				{
					this.m_HighValue = value;
					this.ClampValue();
					this.UpdateDragElementPosition();
					base.SaveViewData();
				}
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00014ABC File Offset: 0x00012CBC
		public TValueType range
		{
			get
			{
				return this.SliderRange();
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00014AD4 File Offset: 0x00012CD4
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x00014AEC File Offset: 0x00012CEC
		public virtual float pageSize
		{
			get
			{
				return this.m_PageSize;
			}
			set
			{
				this.m_PageSize = value;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00014AF6 File Offset: 0x00012CF6
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x00014AFE File Offset: 0x00012CFE
		internal bool clamped { get; set; } = true;

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00014B07 File Offset: 0x00012D07
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x00014B0F File Offset: 0x00012D0F
		internal ClampedDragger<TValueType> clampedDragger { get; private set; }

		// Token: 0x06000575 RID: 1397 RVA: 0x00014B18 File Offset: 0x00012D18
		private TValueType Clamp(TValueType value, TValueType lowBound, TValueType highBound)
		{
			TValueType tvalueType = value;
			bool flag = lowBound.CompareTo(value) > 0;
			if (flag)
			{
				tvalueType = lowBound;
			}
			else
			{
				bool flag2 = highBound.CompareTo(value) < 0;
				if (flag2)
				{
					tvalueType = highBound;
				}
			}
			return tvalueType;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00014B64 File Offset: 0x00012D64
		private TValueType GetClampedValue(TValueType newValue)
		{
			TValueType tvalueType = this.lowValue;
			TValueType tvalueType2 = this.highValue;
			bool flag = tvalueType.CompareTo(tvalueType2) > 0;
			if (flag)
			{
				TValueType tvalueType3 = tvalueType;
				tvalueType = tvalueType2;
				tvalueType2 = tvalueType3;
			}
			return this.Clamp(newValue, tvalueType, tvalueType2);
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00014BAC File Offset: 0x00012DAC
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x00014BC4 File Offset: 0x00012DC4
		public override TValueType value
		{
			get
			{
				return base.value;
			}
			set
			{
				TValueType tvalueType = (this.clamped ? this.GetClampedValue(value) : value);
				base.value = tvalueType;
			}
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00014BF0 File Offset: 0x00012DF0
		public override void SetValueWithoutNotify(TValueType newValue)
		{
			TValueType tvalueType = (this.clamped ? this.GetClampedValue(newValue) : newValue);
			base.SetValueWithoutNotify(tvalueType);
			this.UpdateDragElementPosition();
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00014C20 File Offset: 0x00012E20
		// (set) Token: 0x0600057B RID: 1403 RVA: 0x00014C38 File Offset: 0x00012E38
		public SliderDirection direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				this.m_Direction = value;
				bool flag = this.m_Direction == SliderDirection.Horizontal;
				if (flag)
				{
					base.RemoveFromClassList(BaseSlider<TValueType>.verticalVariantUssClassName);
					base.AddToClassList(BaseSlider<TValueType>.horizontalVariantUssClassName);
				}
				else
				{
					base.RemoveFromClassList(BaseSlider<TValueType>.horizontalVariantUssClassName);
					base.AddToClassList(BaseSlider<TValueType>.verticalVariantUssClassName);
				}
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00014C90 File Offset: 0x00012E90
		internal BaseSlider(string label, TValueType start, TValueType end, SliderDirection direction = SliderDirection.Horizontal, float pageSize = 0f)
			: base(label, null)
		{
			base.AddToClassList(BaseSlider<TValueType>.ussClassName);
			base.labelElement.AddToClassList(BaseSlider<TValueType>.labelUssClassName);
			base.visualInput.AddToClassList(BaseSlider<TValueType>.inputUssClassName);
			this.direction = direction;
			this.pageSize = pageSize;
			this.lowValue = start;
			this.highValue = end;
			base.pickingMode = PickingMode.Ignore;
			base.visualInput.pickingMode = PickingMode.Position;
			VisualElement visualElement = new VisualElement
			{
				name = "unity-tracker"
			};
			visualElement.AddToClassList(BaseSlider<TValueType>.trackerUssClassName);
			base.visualInput.Add(visualElement);
			this.dragBorderElement = new VisualElement
			{
				name = "unity-dragger-border"
			};
			this.dragBorderElement.AddToClassList(BaseSlider<TValueType>.draggerBorderUssClassName);
			base.visualInput.Add(this.dragBorderElement);
			this.dragElement = new VisualElement
			{
				name = "unity-dragger"
			};
			this.dragElement.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.UpdateDragElementPosition), TrickleDown.NoTrickleDown);
			this.dragElement.AddToClassList(BaseSlider<TValueType>.draggerUssClassName);
			base.visualInput.Add(this.dragElement);
			this.clampedDragger = new ClampedDragger<TValueType>(this, new Action(this.SetSliderValueFromClick), new Action(this.SetSliderValueFromDrag));
			base.visualInput.AddManipulator(this.clampedDragger);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00014E04 File Offset: 0x00013004
		private void ClampValue()
		{
			this.value = base.rawValue;
		}

		// Token: 0x0600057E RID: 1406
		internal abstract TValueType SliderLerpUnclamped(TValueType a, TValueType b, float interpolant);

		// Token: 0x0600057F RID: 1407
		internal abstract float SliderNormalizeValue(TValueType currentValue, TValueType lowerValue, TValueType higherValue);

		// Token: 0x06000580 RID: 1408
		internal abstract TValueType SliderRange();

		// Token: 0x06000581 RID: 1409 RVA: 0x00014E14 File Offset: 0x00013014
		private void SetSliderValueFromDrag()
		{
			bool flag = this.clampedDragger.dragDirection != ClampedDragger<TValueType>.DragDirection.Free;
			if (!flag)
			{
				Vector2 delta = this.clampedDragger.delta;
				bool flag2 = this.direction == SliderDirection.Horizontal;
				if (flag2)
				{
					this.ComputeValueAndDirectionFromDrag(base.visualInput.resolvedStyle.width, this.dragElement.resolvedStyle.width, this.m_DragElementStartPos.x + delta.x);
				}
				else
				{
					this.ComputeValueAndDirectionFromDrag(base.visualInput.resolvedStyle.height, this.dragElement.resolvedStyle.height, this.m_DragElementStartPos.y + delta.y);
				}
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00014ECC File Offset: 0x000130CC
		private void ComputeValueAndDirectionFromDrag(float sliderLength, float dragElementLength, float dragElementPos)
		{
			float num = sliderLength - dragElementLength;
			bool flag = Mathf.Abs(num) < Mathf.Epsilon;
			if (!flag)
			{
				float num2 = Mathf.Max(0f, Mathf.Min(dragElementPos, num)) / num;
				this.value = this.SliderLerpUnclamped(this.lowValue, this.highValue, num2);
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00014F20 File Offset: 0x00013120
		private void SetSliderValueFromClick()
		{
			bool flag = this.clampedDragger.dragDirection == ClampedDragger<TValueType>.DragDirection.Free;
			if (!flag)
			{
				bool flag2 = this.clampedDragger.dragDirection == ClampedDragger<TValueType>.DragDirection.None;
				if (flag2)
				{
					bool flag3 = Mathf.Approximately(this.pageSize, 0f);
					if (flag3)
					{
						float num = ((this.direction == SliderDirection.Horizontal) ? (this.clampedDragger.startMousePosition.x - this.dragElement.resolvedStyle.width / 2f) : this.dragElement.transform.position.x);
						float num2 = ((this.direction == SliderDirection.Horizontal) ? this.dragElement.transform.position.y : (this.clampedDragger.startMousePosition.y - this.dragElement.resolvedStyle.height / 2f));
						Vector3 vector = new Vector3(num, num2, 0f);
						this.dragElement.transform.position = vector;
						this.dragBorderElement.transform.position = vector;
						this.m_DragElementStartPos = new Rect(num, num2, this.dragElement.resolvedStyle.width, this.dragElement.resolvedStyle.height);
						this.clampedDragger.dragDirection = ClampedDragger<TValueType>.DragDirection.Free;
						bool flag4 = this.direction == SliderDirection.Horizontal;
						if (flag4)
						{
							this.ComputeValueAndDirectionFromDrag(base.visualInput.resolvedStyle.width, this.dragElement.resolvedStyle.width, this.m_DragElementStartPos.x);
						}
						else
						{
							this.ComputeValueAndDirectionFromDrag(base.visualInput.resolvedStyle.height, this.dragElement.resolvedStyle.height, this.m_DragElementStartPos.y);
						}
						return;
					}
					this.m_DragElementStartPos = new Rect(this.dragElement.transform.position.x, this.dragElement.transform.position.y, this.dragElement.resolvedStyle.width, this.dragElement.resolvedStyle.height);
				}
				bool flag5 = this.direction == SliderDirection.Horizontal;
				if (flag5)
				{
					this.ComputeValueAndDirectionFromClick(base.visualInput.resolvedStyle.width, this.dragElement.resolvedStyle.width, this.dragElement.transform.position.x, this.clampedDragger.lastMousePosition.x);
				}
				else
				{
					this.ComputeValueAndDirectionFromClick(base.visualInput.resolvedStyle.height, this.dragElement.resolvedStyle.height, this.dragElement.transform.position.y, this.clampedDragger.lastMousePosition.y);
				}
			}
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x000151EC File Offset: 0x000133EC
		internal virtual void ComputeValueAndDirectionFromClick(float sliderLength, float dragElementLength, float dragElementPos, float dragElementLastPos)
		{
			float num = sliderLength - dragElementLength;
			bool flag = Mathf.Abs(num) < Mathf.Epsilon;
			if (!flag)
			{
				bool flag2 = dragElementLastPos < dragElementPos && this.clampedDragger.dragDirection != ClampedDragger<TValueType>.DragDirection.LowToHigh;
				if (flag2)
				{
					this.clampedDragger.dragDirection = ClampedDragger<TValueType>.DragDirection.HighToLow;
					float num2 = Mathf.Max(0f, Mathf.Min(dragElementPos - this.pageSize, num)) / num;
					this.value = this.SliderLerpUnclamped(this.lowValue, this.highValue, num2);
				}
				else
				{
					bool flag3 = dragElementLastPos > dragElementPos + dragElementLength && this.clampedDragger.dragDirection != ClampedDragger<TValueType>.DragDirection.HighToLow;
					if (flag3)
					{
						this.clampedDragger.dragDirection = ClampedDragger<TValueType>.DragDirection.LowToHigh;
						float num3 = Mathf.Max(0f, Mathf.Min(dragElementPos + this.pageSize, num)) / num;
						this.value = this.SliderLerpUnclamped(this.lowValue, this.highValue, num3);
					}
				}
			}
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x000152E0 File Offset: 0x000134E0
		public void AdjustDragElement(float factor)
		{
			bool flag = factor < 1f;
			this.dragElement.visible = flag;
			bool flag2 = flag;
			if (flag2)
			{
				IStyle style = this.dragElement.style;
				this.dragElement.visible = true;
				bool flag3 = this.direction == SliderDirection.Horizontal;
				if (flag3)
				{
					float num = ((base.resolvedStyle.minWidth == StyleKeyword.Auto) ? 0f : base.resolvedStyle.minWidth.value);
					style.width = Mathf.Round(Mathf.Max(base.visualInput.layout.width * factor, num));
				}
				else
				{
					float num2 = ((base.resolvedStyle.minHeight == StyleKeyword.Auto) ? 0f : base.resolvedStyle.minHeight.value);
					style.height = Mathf.Round(Mathf.Max(base.visualInput.layout.height * factor, num2));
				}
			}
			this.dragBorderElement.visible = this.dragElement.visible;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00015418 File Offset: 0x00013618
		private void UpdateDragElementPosition(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				this.UpdateDragElementPosition();
			}
		}

		// Token: 0x06000587 RID: 1415 RVA: 0x00015455 File Offset: 0x00013655
		internal override void OnViewDataReady()
		{
			base.OnViewDataReady();
			this.UpdateDragElementPosition();
		}

		// Token: 0x06000588 RID: 1416 RVA: 0x00015468 File Offset: 0x00013668
		private bool SameValues(float a, float b, float epsilon)
		{
			return Mathf.Abs(b - a) < epsilon;
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x00015488 File Offset: 0x00013688
		private void UpdateDragElementPosition()
		{
			bool flag = base.panel == null;
			if (!flag)
			{
				float num = this.SliderNormalizeValue(this.value, this.lowValue, this.highValue);
				float num2 = base.scaledPixelsPerPoint * 0.5f;
				bool flag2 = this.direction == SliderDirection.Horizontal;
				if (flag2)
				{
					float width = this.dragElement.resolvedStyle.width;
					float num3 = -this.dragElement.resolvedStyle.marginLeft - this.dragElement.resolvedStyle.marginRight;
					float num4 = base.visualInput.layout.width - width + num3;
					float num5 = num * num4;
					bool flag3 = float.IsNaN(num5);
					if (!flag3)
					{
						float x = this.dragElement.transform.position.x;
						bool flag4 = !this.SameValues(x, num5, num2);
						if (flag4)
						{
							Vector3 vector = new Vector3(num5, 0f, 0f);
							this.dragElement.transform.position = vector;
							this.dragBorderElement.transform.position = vector;
						}
					}
				}
				else
				{
					float height = this.dragElement.resolvedStyle.height;
					float num6 = base.visualInput.resolvedStyle.height - height;
					float num7 = num * num6;
					bool flag5 = float.IsNaN(num7);
					if (!flag5)
					{
						float y = this.dragElement.transform.position.y;
						bool flag6 = !this.SameValues(y, num7, num2);
						if (flag6)
						{
							Vector3 vector2 = new Vector3(0f, num7, 0f);
							this.dragElement.transform.position = vector2;
							this.dragBorderElement.transform.position = vector2;
						}
					}
				}
			}
		}

		// Token: 0x0600058A RID: 1418 RVA: 0x0001565C File Offset: 0x0001385C
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

		// Token: 0x04000257 RID: 599
		[SerializeField]
		private TValueType m_LowValue;

		// Token: 0x04000258 RID: 600
		[SerializeField]
		private TValueType m_HighValue;

		// Token: 0x04000259 RID: 601
		private float m_PageSize;

		// Token: 0x0400025C RID: 604
		private Rect m_DragElementStartPos;

		// Token: 0x0400025D RID: 605
		private SliderDirection m_Direction;

		// Token: 0x0400025E RID: 606
		internal const float kDefaultPageSize = 0f;

		// Token: 0x0400025F RID: 607
		public new static readonly string ussClassName = "unity-base-slider";

		// Token: 0x04000260 RID: 608
		public new static readonly string labelUssClassName = BaseSlider<TValueType>.ussClassName + "__label";

		// Token: 0x04000261 RID: 609
		public new static readonly string inputUssClassName = BaseSlider<TValueType>.ussClassName + "__input";

		// Token: 0x04000262 RID: 610
		public static readonly string horizontalVariantUssClassName = BaseSlider<TValueType>.ussClassName + "--horizontal";

		// Token: 0x04000263 RID: 611
		public static readonly string verticalVariantUssClassName = BaseSlider<TValueType>.ussClassName + "--vertical";

		// Token: 0x04000264 RID: 612
		public static readonly string trackerUssClassName = BaseSlider<TValueType>.ussClassName + "__tracker";

		// Token: 0x04000265 RID: 613
		public static readonly string draggerUssClassName = BaseSlider<TValueType>.ussClassName + "__dragger";

		// Token: 0x04000266 RID: 614
		public static readonly string draggerBorderUssClassName = BaseSlider<TValueType>.ussClassName + "__dragger-border";
	}
}
