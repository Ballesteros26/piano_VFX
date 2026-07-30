using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000036 RID: 54
	[AddComponentMenu("UI/Slider", 33)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class Slider : Selectable, IDragHandler, IEventSystemHandler, IInitializePotentialDragHandler, ICanvasElement
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x000130A9 File Offset: 0x000112A9
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x000130B1 File Offset: 0x000112B1
		public RectTransform fillRect
		{
			get
			{
				return this.m_FillRect;
			}
			set
			{
				if (SetPropertyUtility.SetClass<RectTransform>(ref this.m_FillRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003DA RID: 986 RVA: 0x000130CD File Offset: 0x000112CD
		// (set) Token: 0x060003DB RID: 987 RVA: 0x000130D5 File Offset: 0x000112D5
		public RectTransform handleRect
		{
			get
			{
				return this.m_HandleRect;
			}
			set
			{
				if (SetPropertyUtility.SetClass<RectTransform>(ref this.m_HandleRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003DC RID: 988 RVA: 0x000130F1 File Offset: 0x000112F1
		// (set) Token: 0x060003DD RID: 989 RVA: 0x000130F9 File Offset: 0x000112F9
		public Slider.Direction direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Slider.Direction>(ref this.m_Direction, value))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0001310F File Offset: 0x0001130F
		// (set) Token: 0x060003DF RID: 991 RVA: 0x00013117 File Offset: 0x00011317
		public float minValue
		{
			get
			{
				return this.m_MinValue;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinValue, value))
				{
					this.Set(this.m_Value, true);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0001313A File Offset: 0x0001133A
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x00013142 File Offset: 0x00011342
		public float maxValue
		{
			get
			{
				return this.m_MaxValue;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MaxValue, value))
				{
					this.Set(this.m_Value, true);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00013165 File Offset: 0x00011365
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x0001316D File Offset: 0x0001136D
		public bool wholeNumbers
		{
			get
			{
				return this.m_WholeNumbers;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_WholeNumbers, value))
				{
					this.Set(this.m_Value, true);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00013190 File Offset: 0x00011390
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x000131AC File Offset: 0x000113AC
		public virtual float value
		{
			get
			{
				if (!this.wholeNumbers)
				{
					return this.m_Value;
				}
				return Mathf.Round(this.m_Value);
			}
			set
			{
				this.Set(value, true);
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x000131B6 File Offset: 0x000113B6
		public virtual void SetValueWithoutNotify(float input)
		{
			this.Set(input, false);
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x000131C0 File Offset: 0x000113C0
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x000131F2 File Offset: 0x000113F2
		public float normalizedValue
		{
			get
			{
				if (Mathf.Approximately(this.minValue, this.maxValue))
				{
					return 0f;
				}
				return Mathf.InverseLerp(this.minValue, this.maxValue, this.value);
			}
			set
			{
				this.value = Mathf.Lerp(this.minValue, this.maxValue, value);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0001320C File Offset: 0x0001140C
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x00013214 File Offset: 0x00011414
		public Slider.SliderEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				this.m_OnValueChanged = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0001321D File Offset: 0x0001141D
		private float stepSize
		{
			get
			{
				if (!this.wholeNumbers)
				{
					return (this.maxValue - this.minValue) * 0.1f;
				}
				return 1f;
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00013240 File Offset: 0x00011440
		protected Slider()
		{
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00013269 File Offset: 0x00011469
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateCachedReferences();
			this.Set(this.m_Value, false);
			this.UpdateVisuals();
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0001328A File Offset: 0x0001148A
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0001329D File Offset: 0x0001149D
		protected virtual void Update()
		{
			if (this.m_DelayedUpdateVisuals)
			{
				this.m_DelayedUpdateVisuals = false;
				this.Set(this.m_Value, false);
				this.UpdateVisuals();
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000132C4 File Offset: 0x000114C4
		protected override void OnDidApplyAnimationProperties()
		{
			this.m_Value = this.ClampValue(this.m_Value);
			float num = this.normalizedValue;
			if (this.m_FillContainerRect != null)
			{
				if (this.m_FillImage != null && this.m_FillImage.type == Image.Type.Filled)
				{
					num = this.m_FillImage.fillAmount;
				}
				else
				{
					num = (this.reverseValue ? (1f - this.m_FillRect.anchorMin[(int)this.axis]) : this.m_FillRect.anchorMax[(int)this.axis]);
				}
			}
			else if (this.m_HandleContainerRect != null)
			{
				num = (this.reverseValue ? (1f - this.m_HandleRect.anchorMin[(int)this.axis]) : this.m_HandleRect.anchorMin[(int)this.axis]);
			}
			this.UpdateVisuals();
			if (num != this.normalizedValue)
			{
				UISystemProfilerApi.AddMarker("Slider.value", this);
				this.onValueChanged.Invoke(this.m_Value);
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000133E8 File Offset: 0x000115E8
		private void UpdateCachedReferences()
		{
			if (this.m_FillRect && this.m_FillRect != (RectTransform)base.transform)
			{
				this.m_FillTransform = this.m_FillRect.transform;
				this.m_FillImage = this.m_FillRect.GetComponent<Image>();
				if (this.m_FillTransform.parent != null)
				{
					this.m_FillContainerRect = this.m_FillTransform.parent.GetComponent<RectTransform>();
				}
			}
			else
			{
				this.m_FillRect = null;
				this.m_FillContainerRect = null;
				this.m_FillImage = null;
			}
			if (this.m_HandleRect && this.m_HandleRect != (RectTransform)base.transform)
			{
				this.m_HandleTransform = this.m_HandleRect.transform;
				if (this.m_HandleTransform.parent != null)
				{
					this.m_HandleContainerRect = this.m_HandleTransform.parent.GetComponent<RectTransform>();
					return;
				}
			}
			else
			{
				this.m_HandleRect = null;
				this.m_HandleContainerRect = null;
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000134EC File Offset: 0x000116EC
		private float ClampValue(float input)
		{
			float num = Mathf.Clamp(input, this.minValue, this.maxValue);
			if (this.wholeNumbers)
			{
				num = Mathf.Round(num);
			}
			return num;
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001351C File Offset: 0x0001171C
		protected virtual void Set(float input, bool sendCallback = true)
		{
			float num = this.ClampValue(input);
			if (this.m_Value == num)
			{
				return;
			}
			this.m_Value = num;
			this.UpdateVisuals();
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("Slider.value", this);
				this.m_OnValueChanged.Invoke(num);
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00013562 File Offset: 0x00011762
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateVisuals();
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00013579 File Offset: 0x00011779
		private Slider.Axis axis
		{
			get
			{
				if (this.m_Direction != Slider.Direction.LeftToRight && this.m_Direction != Slider.Direction.RightToLeft)
				{
					return Slider.Axis.Vertical;
				}
				return Slider.Axis.Horizontal;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0001358F File Offset: 0x0001178F
		private bool reverseValue
		{
			get
			{
				return this.m_Direction == Slider.Direction.RightToLeft || this.m_Direction == Slider.Direction.TopToBottom;
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000135A8 File Offset: 0x000117A8
		private void UpdateVisuals()
		{
			this.m_Tracker.Clear();
			if (this.m_FillContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_FillRect, DrivenTransformProperties.Anchors);
				Vector2 zero = Vector2.zero;
				Vector2 one = Vector2.one;
				if (this.m_FillImage != null && this.m_FillImage.type == Image.Type.Filled)
				{
					this.m_FillImage.fillAmount = this.normalizedValue;
				}
				else if (this.reverseValue)
				{
					zero[(int)this.axis] = 1f - this.normalizedValue;
				}
				else
				{
					one[(int)this.axis] = this.normalizedValue;
				}
				this.m_FillRect.anchorMin = zero;
				this.m_FillRect.anchorMax = one;
			}
			if (this.m_HandleContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_HandleRect, DrivenTransformProperties.Anchors);
				Vector2 zero2 = Vector2.zero;
				Vector2 one2 = Vector2.one;
				zero2[(int)this.axis] = (one2[(int)this.axis] = (this.reverseValue ? (1f - this.normalizedValue) : this.normalizedValue));
				this.m_HandleRect.anchorMin = zero2;
				this.m_HandleRect.anchorMax = one2;
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000136F8 File Offset: 0x000118F8
		private void UpdateDrag(PointerEventData eventData, Camera cam)
		{
			RectTransform rectTransform = this.m_HandleContainerRect ?? this.m_FillContainerRect;
			if (rectTransform != null && rectTransform.rect.size[(int)this.axis] > 0f)
			{
				Vector2 zero = Vector2.zero;
				if (!MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref zero))
				{
					return;
				}
				Vector2 vector;
				if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, zero, cam, out vector))
				{
					return;
				}
				vector -= rectTransform.rect.position;
				float num = Mathf.Clamp01((vector - this.m_Offset)[(int)this.axis] / rectTransform.rect.size[(int)this.axis]);
				this.normalizedValue = (this.reverseValue ? (1f - num) : num);
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001205E File Offset: 0x0001025E
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000137D8 File Offset: 0x000119D8
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			base.OnPointerDown(eventData);
			this.m_Offset = Vector2.zero;
			if (this.m_HandleContainerRect != null && RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera))
			{
				Vector2 vector;
				if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, out vector))
				{
					this.m_Offset = vector;
					return;
				}
			}
			else
			{
				this.UpdateDrag(eventData, eventData.pressEventCamera);
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00013862 File Offset: 0x00011A62
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.UpdateDrag(eventData, eventData.pressEventCamera);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001387C File Offset: 0x00011A7C
		public override void OnMove(AxisEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				base.OnMove(eventData);
				return;
			}
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				if (this.axis == Slider.Axis.Horizontal && this.FindSelectableOnLeft() == null)
				{
					this.Set(this.reverseValue ? (this.value + this.stepSize) : (this.value - this.stepSize), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Up:
				if (this.axis == Slider.Axis.Vertical && this.FindSelectableOnUp() == null)
				{
					this.Set(this.reverseValue ? (this.value - this.stepSize) : (this.value + this.stepSize), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Right:
				if (this.axis == Slider.Axis.Horizontal && this.FindSelectableOnRight() == null)
				{
					this.Set(this.reverseValue ? (this.value - this.stepSize) : (this.value + this.stepSize), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Down:
				if (this.axis == Slider.Axis.Vertical && this.FindSelectableOnDown() == null)
				{
					this.Set(this.reverseValue ? (this.value + this.stepSize) : (this.value - this.stepSize), true);
					return;
				}
				base.OnMove(eventData);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000139E8 File Offset: 0x00011BE8
		public override Selectable FindSelectableOnLeft()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00013A18 File Offset: 0x00011C18
		public override Selectable FindSelectableOnRight()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00013A48 File Offset: 0x00011C48
		public override Selectable FindSelectableOnUp()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00013A78 File Offset: 0x00011C78
		public override Selectable FindSelectableOnDown()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000123C7 File Offset: 0x000105C7
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00013AA8 File Offset: 0x00011CA8
		public void SetDirection(Slider.Direction direction, bool includeRectLayouts)
		{
			Slider.Axis axis = this.axis;
			bool reverseValue = this.reverseValue;
			this.direction = direction;
			if (!includeRectLayouts)
			{
				return;
			}
			if (this.axis != axis)
			{
				RectTransformUtility.FlipLayoutAxes(base.transform as RectTransform, true, true);
			}
			if (this.reverseValue != reverseValue)
			{
				RectTransformUtility.FlipLayoutOnAxis(base.transform as RectTransform, (int)this.axis, true, true);
			}
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00005DE4 File Offset: 0x00003FE4
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x0400014C RID: 332
		[SerializeField]
		private RectTransform m_FillRect;

		// Token: 0x0400014D RID: 333
		[SerializeField]
		private RectTransform m_HandleRect;

		// Token: 0x0400014E RID: 334
		[Space]
		[SerializeField]
		private Slider.Direction m_Direction;

		// Token: 0x0400014F RID: 335
		[SerializeField]
		private float m_MinValue;

		// Token: 0x04000150 RID: 336
		[SerializeField]
		private float m_MaxValue = 1f;

		// Token: 0x04000151 RID: 337
		[SerializeField]
		private bool m_WholeNumbers;

		// Token: 0x04000152 RID: 338
		[SerializeField]
		protected float m_Value;

		// Token: 0x04000153 RID: 339
		[Space]
		[SerializeField]
		private Slider.SliderEvent m_OnValueChanged = new Slider.SliderEvent();

		// Token: 0x04000154 RID: 340
		private Image m_FillImage;

		// Token: 0x04000155 RID: 341
		private Transform m_FillTransform;

		// Token: 0x04000156 RID: 342
		private RectTransform m_FillContainerRect;

		// Token: 0x04000157 RID: 343
		private Transform m_HandleTransform;

		// Token: 0x04000158 RID: 344
		private RectTransform m_HandleContainerRect;

		// Token: 0x04000159 RID: 345
		private Vector2 m_Offset = Vector2.zero;

		// Token: 0x0400015A RID: 346
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x0400015B RID: 347
		private bool m_DelayedUpdateVisuals;

		// Token: 0x020000A8 RID: 168
		public enum Direction
		{
			// Token: 0x040002E3 RID: 739
			LeftToRight,
			// Token: 0x040002E4 RID: 740
			RightToLeft,
			// Token: 0x040002E5 RID: 741
			BottomToTop,
			// Token: 0x040002E6 RID: 742
			TopToBottom
		}

		// Token: 0x020000A9 RID: 169
		[Serializable]
		public class SliderEvent : UnityEvent<float>
		{
		}

		// Token: 0x020000AA RID: 170
		private enum Axis
		{
			// Token: 0x040002E8 RID: 744
			Horizontal,
			// Token: 0x040002E9 RID: 745
			Vertical
		}
	}
}
