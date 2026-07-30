using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000033 RID: 51
	[AddComponentMenu("UI/Scrollbar", 34)]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public class Scrollbar : Selectable, IBeginDragHandler, IEventSystemHandler, IDragHandler, IInitializePotentialDragHandler, ICanvasElement
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00011BBA File Offset: 0x0000FDBA
		// (set) Token: 0x0600036E RID: 878 RVA: 0x00011BC2 File Offset: 0x0000FDC2
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

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00011BDE File Offset: 0x0000FDDE
		// (set) Token: 0x06000370 RID: 880 RVA: 0x00011BE6 File Offset: 0x0000FDE6
		public Scrollbar.Direction direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Scrollbar.Direction>(ref this.m_Direction, value))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00011BFC File Offset: 0x0000FDFC
		protected Scrollbar()
		{
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00011C28 File Offset: 0x0000FE28
		// (set) Token: 0x06000373 RID: 883 RVA: 0x00011C61 File Offset: 0x0000FE61
		public float value
		{
			get
			{
				float num = this.m_Value;
				if (this.m_NumberOfSteps > 1)
				{
					num = Mathf.Round(num * (float)(this.m_NumberOfSteps - 1)) / (float)(this.m_NumberOfSteps - 1);
				}
				return num;
			}
			set
			{
				this.Set(value, true);
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00011C6B File Offset: 0x0000FE6B
		public virtual void SetValueWithoutNotify(float input)
		{
			this.Set(input, false);
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00011C75 File Offset: 0x0000FE75
		// (set) Token: 0x06000376 RID: 886 RVA: 0x00011C7D File Offset: 0x0000FE7D
		public float size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_Size, Mathf.Clamp01(value)))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00011C98 File Offset: 0x0000FE98
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00011CA0 File Offset: 0x0000FEA0
		public int numberOfSteps
		{
			get
			{
				return this.m_NumberOfSteps;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_NumberOfSteps, value))
				{
					this.Set(this.m_Value, true);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00011CC3 File Offset: 0x0000FEC3
		// (set) Token: 0x0600037A RID: 890 RVA: 0x00011CCB File Offset: 0x0000FECB
		public Scrollbar.ScrollEvent onValueChanged
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

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00011CD4 File Offset: 0x0000FED4
		private float stepSize
		{
			get
			{
				if (this.m_NumberOfSteps <= 1)
				{
					return 0.1f;
				}
				return 1f / (float)(this.m_NumberOfSteps - 1);
			}
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00011CF4 File Offset: 0x0000FEF4
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateCachedReferences();
			this.Set(this.m_Value, false);
			this.UpdateVisuals();
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00011D15 File Offset: 0x0000FF15
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00011D28 File Offset: 0x0000FF28
		protected virtual void Update()
		{
			if (this.m_DelayedUpdateVisuals)
			{
				this.m_DelayedUpdateVisuals = false;
				this.UpdateVisuals();
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00011D3F File Offset: 0x0000FF3F
		private void UpdateCachedReferences()
		{
			if (this.m_HandleRect && this.m_HandleRect.parent != null)
			{
				this.m_ContainerRect = this.m_HandleRect.parent.GetComponent<RectTransform>();
				return;
			}
			this.m_ContainerRect = null;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00011D7F File Offset: 0x0000FF7F
		private void Set(float input, bool sendCallback = true)
		{
			float value = this.m_Value;
			this.m_Value = input;
			if (value == this.value)
			{
				return;
			}
			this.UpdateVisuals();
			if (sendCallback)
			{
				UISystemProfilerApi.AddMarker("Scrollbar.value", this);
				this.m_OnValueChanged.Invoke(this.value);
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00011DBC File Offset: 0x0000FFBC
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateVisuals();
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00011DD3 File Offset: 0x0000FFD3
		private Scrollbar.Axis axis
		{
			get
			{
				if (this.m_Direction != Scrollbar.Direction.LeftToRight && this.m_Direction != Scrollbar.Direction.RightToLeft)
				{
					return Scrollbar.Axis.Vertical;
				}
				return Scrollbar.Axis.Horizontal;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00011DE9 File Offset: 0x0000FFE9
		private bool reverseValue
		{
			get
			{
				return this.m_Direction == Scrollbar.Direction.RightToLeft || this.m_Direction == Scrollbar.Direction.TopToBottom;
			}
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00011E00 File Offset: 0x00010000
		private void UpdateVisuals()
		{
			this.m_Tracker.Clear();
			if (this.m_ContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_HandleRect, DrivenTransformProperties.Anchors);
				Vector2 zero = Vector2.zero;
				Vector2 one = Vector2.one;
				float num = Mathf.Clamp01(this.value) * (1f - this.size);
				if (this.reverseValue)
				{
					zero[(int)this.axis] = 1f - num - this.size;
					one[(int)this.axis] = 1f - num;
				}
				else
				{
					zero[(int)this.axis] = num;
					one[(int)this.axis] = num + this.size;
				}
				this.m_HandleRect.anchorMin = zero;
				this.m_HandleRect.anchorMax = one;
			}
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00011EDC File Offset: 0x000100DC
		private void UpdateDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (this.m_ContainerRect == null)
			{
				return;
			}
			Vector2 zero = Vector2.zero;
			if (!MultipleDisplayUtilities.GetRelativeMousePositionForDrag(eventData, ref zero))
			{
				return;
			}
			Vector2 vector;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_ContainerRect, zero, eventData.pressEventCamera, out vector))
			{
				return;
			}
			Vector2 vector2 = vector - this.m_Offset - this.m_ContainerRect.rect.position - (this.m_HandleRect.rect.size - this.m_HandleRect.sizeDelta) * 0.5f;
			float num = ((this.axis == Scrollbar.Axis.Horizontal) ? this.m_ContainerRect.rect.width : this.m_ContainerRect.rect.height) * (1f - this.size);
			if (num <= 0f)
			{
				return;
			}
			this.DoUpdateDrag(vector2, num);
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00011FD4 File Offset: 0x000101D4
		private void DoUpdateDrag(Vector2 handleCorner, float remainingSize)
		{
			switch (this.m_Direction)
			{
			case Scrollbar.Direction.LeftToRight:
				this.Set(Mathf.Clamp01(handleCorner.x / remainingSize), true);
				return;
			case Scrollbar.Direction.RightToLeft:
				this.Set(Mathf.Clamp01(1f - handleCorner.x / remainingSize), true);
				return;
			case Scrollbar.Direction.BottomToTop:
				this.Set(Mathf.Clamp01(handleCorner.y / remainingSize), true);
				return;
			case Scrollbar.Direction.TopToBottom:
				this.Set(Mathf.Clamp01(1f - handleCorner.y / remainingSize), true);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001205E File Offset: 0x0001025E
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001207C File Offset: 0x0001027C
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.isPointerDownAndNotDragging = false;
			if (!this.MayDrag(eventData))
			{
				return;
			}
			if (this.m_ContainerRect == null)
			{
				return;
			}
			this.m_Offset = Vector2.zero;
			Vector2 vector;
			if (RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera) && RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, out vector))
			{
				this.m_Offset = vector - this.m_HandleRect.rect.center;
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00012111 File Offset: 0x00010311
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			if (this.m_ContainerRect != null)
			{
				this.UpdateDrag(eventData);
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00012132 File Offset: 0x00010332
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			base.OnPointerDown(eventData);
			this.isPointerDownAndNotDragging = true;
			this.m_PointerDownRepeat = base.StartCoroutine(this.ClickRepeat(eventData));
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001215F File Offset: 0x0001035F
		protected IEnumerator ClickRepeat(PointerEventData eventData)
		{
			while (this.isPointerDownAndNotDragging)
			{
				Vector2 vector;
				if (!RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.enterEventCamera) && RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.pointerPressRaycast.screenPosition, eventData.pressEventCamera, out vector))
				{
					float num = ((((this.axis == Scrollbar.Axis.Horizontal) ? vector.x : vector.y) < 0f) ? this.size : (-this.size));
					this.value += (this.reverseValue ? num : (-num));
				}
				yield return new WaitForEndOfFrame();
			}
			base.StopCoroutine(this.m_PointerDownRepeat);
			yield break;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00012175 File Offset: 0x00010375
		public override void OnPointerUp(PointerEventData eventData)
		{
			base.OnPointerUp(eventData);
			this.isPointerDownAndNotDragging = false;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00012188 File Offset: 0x00010388
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
				if (this.axis == Scrollbar.Axis.Horizontal && this.FindSelectableOnLeft() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value + this.stepSize) : (this.value - this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Up:
				if (this.axis == Scrollbar.Axis.Vertical && this.FindSelectableOnUp() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value - this.stepSize) : (this.value + this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Right:
				if (this.axis == Scrollbar.Axis.Horizontal && this.FindSelectableOnRight() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value - this.stepSize) : (this.value + this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			case MoveDirection.Down:
				if (this.axis == Scrollbar.Axis.Vertical && this.FindSelectableOnDown() == null)
				{
					this.Set(Mathf.Clamp01(this.reverseValue ? (this.value + this.stepSize) : (this.value - this.stepSize)), true);
					return;
				}
				base.OnMove(eventData);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00012308 File Offset: 0x00010508
		public override Selectable FindSelectableOnLeft()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00012338 File Offset: 0x00010538
		public override Selectable FindSelectableOnRight()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00012368 File Offset: 0x00010568
		public override Selectable FindSelectableOnUp()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00012398 File Offset: 0x00010598
		public override Selectable FindSelectableOnDown()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000123C7 File Offset: 0x000105C7
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000123D0 File Offset: 0x000105D0
		public void SetDirection(Scrollbar.Direction direction, bool includeRectLayouts)
		{
			Scrollbar.Axis axis = this.axis;
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

		// Token: 0x06000397 RID: 919 RVA: 0x00005DE4 File Offset: 0x00003FE4
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000131 RID: 305
		[SerializeField]
		private RectTransform m_HandleRect;

		// Token: 0x04000132 RID: 306
		[SerializeField]
		private Scrollbar.Direction m_Direction;

		// Token: 0x04000133 RID: 307
		[Range(0f, 1f)]
		[SerializeField]
		private float m_Value;

		// Token: 0x04000134 RID: 308
		[Range(0f, 1f)]
		[SerializeField]
		private float m_Size = 0.2f;

		// Token: 0x04000135 RID: 309
		[Range(0f, 11f)]
		[SerializeField]
		private int m_NumberOfSteps;

		// Token: 0x04000136 RID: 310
		[Space(6f)]
		[SerializeField]
		private Scrollbar.ScrollEvent m_OnValueChanged = new Scrollbar.ScrollEvent();

		// Token: 0x04000137 RID: 311
		private RectTransform m_ContainerRect;

		// Token: 0x04000138 RID: 312
		private Vector2 m_Offset = Vector2.zero;

		// Token: 0x04000139 RID: 313
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x0400013A RID: 314
		private Coroutine m_PointerDownRepeat;

		// Token: 0x0400013B RID: 315
		private bool isPointerDownAndNotDragging;

		// Token: 0x0400013C RID: 316
		private bool m_DelayedUpdateVisuals;

		// Token: 0x020000A2 RID: 162
		public enum Direction
		{
			// Token: 0x040002CC RID: 716
			LeftToRight,
			// Token: 0x040002CD RID: 717
			RightToLeft,
			// Token: 0x040002CE RID: 718
			BottomToTop,
			// Token: 0x040002CF RID: 719
			TopToBottom
		}

		// Token: 0x020000A3 RID: 163
		[Serializable]
		public class ScrollEvent : UnityEvent<float>
		{
		}

		// Token: 0x020000A4 RID: 164
		private enum Axis
		{
			// Token: 0x040002D1 RID: 721
			Horizontal,
			// Token: 0x040002D2 RID: 722
			Vertical
		}
	}
}
