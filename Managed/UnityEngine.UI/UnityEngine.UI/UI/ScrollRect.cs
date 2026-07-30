using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000032 RID: 50
	[AddComponentMenu("UI/Scroll Rect", 37)]
	[SelectionBase]
	[ExecuteAlways]
	[DisallowMultipleComponent]
	[RequireComponent(typeof(RectTransform))]
	public class ScrollRect : UIBehaviour, IInitializePotentialDragHandler, IEventSystemHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IScrollHandler, ICanvasElement, ILayoutElement, ILayoutGroup, ILayoutController
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00010014 File Offset: 0x0000E214
		// (set) Token: 0x06000313 RID: 787 RVA: 0x0001001C File Offset: 0x0000E21C
		public RectTransform content
		{
			get
			{
				return this.m_Content;
			}
			set
			{
				this.m_Content = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000314 RID: 788 RVA: 0x00010025 File Offset: 0x0000E225
		// (set) Token: 0x06000315 RID: 789 RVA: 0x0001002D File Offset: 0x0000E22D
		public bool horizontal
		{
			get
			{
				return this.m_Horizontal;
			}
			set
			{
				this.m_Horizontal = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00010036 File Offset: 0x0000E236
		// (set) Token: 0x06000317 RID: 791 RVA: 0x0001003E File Offset: 0x0000E23E
		public bool vertical
		{
			get
			{
				return this.m_Vertical;
			}
			set
			{
				this.m_Vertical = value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00010047 File Offset: 0x0000E247
		// (set) Token: 0x06000319 RID: 793 RVA: 0x0001004F File Offset: 0x0000E24F
		public ScrollRect.MovementType movementType
		{
			get
			{
				return this.m_MovementType;
			}
			set
			{
				this.m_MovementType = value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00010058 File Offset: 0x0000E258
		// (set) Token: 0x0600031B RID: 795 RVA: 0x00010060 File Offset: 0x0000E260
		public float elasticity
		{
			get
			{
				return this.m_Elasticity;
			}
			set
			{
				this.m_Elasticity = value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00010069 File Offset: 0x0000E269
		// (set) Token: 0x0600031D RID: 797 RVA: 0x00010071 File Offset: 0x0000E271
		public bool inertia
		{
			get
			{
				return this.m_Inertia;
			}
			set
			{
				this.m_Inertia = value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0001007A File Offset: 0x0000E27A
		// (set) Token: 0x0600031F RID: 799 RVA: 0x00010082 File Offset: 0x0000E282
		public float decelerationRate
		{
			get
			{
				return this.m_DecelerationRate;
			}
			set
			{
				this.m_DecelerationRate = value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0001008B File Offset: 0x0000E28B
		// (set) Token: 0x06000321 RID: 801 RVA: 0x00010093 File Offset: 0x0000E293
		public float scrollSensitivity
		{
			get
			{
				return this.m_ScrollSensitivity;
			}
			set
			{
				this.m_ScrollSensitivity = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000322 RID: 802 RVA: 0x0001009C File Offset: 0x0000E29C
		// (set) Token: 0x06000323 RID: 803 RVA: 0x000100A4 File Offset: 0x0000E2A4
		public RectTransform viewport
		{
			get
			{
				return this.m_Viewport;
			}
			set
			{
				this.m_Viewport = value;
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000324 RID: 804 RVA: 0x000100B3 File Offset: 0x0000E2B3
		// (set) Token: 0x06000325 RID: 805 RVA: 0x000100BC File Offset: 0x0000E2BC
		public Scrollbar horizontalScrollbar
		{
			get
			{
				return this.m_HorizontalScrollbar;
			}
			set
			{
				if (this.m_HorizontalScrollbar)
				{
					this.m_HorizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
				}
				this.m_HorizontalScrollbar = value;
				if (this.m_HorizontalScrollbar)
				{
					this.m_HorizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
				}
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00010128 File Offset: 0x0000E328
		// (set) Token: 0x06000327 RID: 807 RVA: 0x00010130 File Offset: 0x0000E330
		public Scrollbar verticalScrollbar
		{
			get
			{
				return this.m_VerticalScrollbar;
			}
			set
			{
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
				}
				this.m_VerticalScrollbar = value;
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
				}
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0001019C File Offset: 0x0000E39C
		// (set) Token: 0x06000329 RID: 809 RVA: 0x000101A4 File Offset: 0x0000E3A4
		public ScrollRect.ScrollbarVisibility horizontalScrollbarVisibility
		{
			get
			{
				return this.m_HorizontalScrollbarVisibility;
			}
			set
			{
				this.m_HorizontalScrollbarVisibility = value;
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600032A RID: 810 RVA: 0x000101B3 File Offset: 0x0000E3B3
		// (set) Token: 0x0600032B RID: 811 RVA: 0x000101BB File Offset: 0x0000E3BB
		public ScrollRect.ScrollbarVisibility verticalScrollbarVisibility
		{
			get
			{
				return this.m_VerticalScrollbarVisibility;
			}
			set
			{
				this.m_VerticalScrollbarVisibility = value;
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600032C RID: 812 RVA: 0x000101CA File Offset: 0x0000E3CA
		// (set) Token: 0x0600032D RID: 813 RVA: 0x000101D2 File Offset: 0x0000E3D2
		public float horizontalScrollbarSpacing
		{
			get
			{
				return this.m_HorizontalScrollbarSpacing;
			}
			set
			{
				this.m_HorizontalScrollbarSpacing = value;
				this.SetDirty();
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600032E RID: 814 RVA: 0x000101E1 File Offset: 0x0000E3E1
		// (set) Token: 0x0600032F RID: 815 RVA: 0x000101E9 File Offset: 0x0000E3E9
		public float verticalScrollbarSpacing
		{
			get
			{
				return this.m_VerticalScrollbarSpacing;
			}
			set
			{
				this.m_VerticalScrollbarSpacing = value;
				this.SetDirty();
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000330 RID: 816 RVA: 0x000101F8 File Offset: 0x0000E3F8
		// (set) Token: 0x06000331 RID: 817 RVA: 0x00010200 File Offset: 0x0000E400
		public ScrollRect.ScrollRectEvent onValueChanged
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

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0001020C File Offset: 0x0000E40C
		protected RectTransform viewRect
		{
			get
			{
				if (this.m_ViewRect == null)
				{
					this.m_ViewRect = this.m_Viewport;
				}
				if (this.m_ViewRect == null)
				{
					this.m_ViewRect = (RectTransform)base.transform;
				}
				return this.m_ViewRect;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00010258 File Offset: 0x0000E458
		// (set) Token: 0x06000334 RID: 820 RVA: 0x00010260 File Offset: 0x0000E460
		public Vector2 velocity
		{
			get
			{
				return this.m_Velocity;
			}
			set
			{
				this.m_Velocity = value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00010269 File Offset: 0x0000E469
		private RectTransform rectTransform
		{
			get
			{
				if (this.m_Rect == null)
				{
					this.m_Rect = base.GetComponent<RectTransform>();
				}
				return this.m_Rect;
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0001028C File Offset: 0x0000E48C
		protected ScrollRect()
		{
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00010314 File Offset: 0x0000E514
		public virtual void Rebuild(CanvasUpdate executing)
		{
			if (executing == CanvasUpdate.Prelayout)
			{
				this.UpdateCachedData();
			}
			if (executing == CanvasUpdate.PostLayout)
			{
				this.UpdateBounds();
				this.UpdateScrollbars(Vector2.zero);
				this.UpdatePrevData();
				this.m_HasRebuiltLayout = true;
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x0600033A RID: 826 RVA: 0x00010344 File Offset: 0x0000E544
		private void UpdateCachedData()
		{
			Transform transform = base.transform;
			this.m_HorizontalScrollbarRect = ((this.m_HorizontalScrollbar == null) ? null : (this.m_HorizontalScrollbar.transform as RectTransform));
			this.m_VerticalScrollbarRect = ((this.m_VerticalScrollbar == null) ? null : (this.m_VerticalScrollbar.transform as RectTransform));
			bool flag = this.viewRect.parent == transform;
			bool flag2 = !this.m_HorizontalScrollbarRect || this.m_HorizontalScrollbarRect.parent == transform;
			bool flag3 = !this.m_VerticalScrollbarRect || this.m_VerticalScrollbarRect.parent == transform;
			bool flag4 = flag && flag2 && flag3;
			this.m_HSliderExpand = flag4 && this.m_HorizontalScrollbarRect && this.horizontalScrollbarVisibility == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			this.m_VSliderExpand = flag4 && this.m_VerticalScrollbarRect && this.verticalScrollbarVisibility == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;
			this.m_HSliderHeight = ((this.m_HorizontalScrollbarRect == null) ? 0f : this.m_HorizontalScrollbarRect.rect.height);
			this.m_VSliderWidth = ((this.m_VerticalScrollbarRect == null) ? 0f : this.m_VerticalScrollbarRect.rect.width);
		}

		// Token: 0x0600033B RID: 827 RVA: 0x000104A4 File Offset: 0x0000E6A4
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
			this.SetDirty();
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00010518 File Offset: 0x0000E718
		protected override void OnDisable()
		{
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			this.m_Dragging = false;
			this.m_Scrolling = false;
			this.m_HasRebuiltLayout = false;
			this.m_Tracker.Clear();
			this.m_Velocity = Vector2.zero;
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x000105B9 File Offset: 0x0000E7B9
		public override bool IsActive()
		{
			return base.IsActive() && this.m_Content != null;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000105D1 File Offset: 0x0000E7D1
		private void EnsureLayoutHasRebuilt()
		{
			if (!this.m_HasRebuiltLayout && !CanvasUpdateRegistry.IsRebuildingLayout())
			{
				Canvas.ForceUpdateCanvases();
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000105E7 File Offset: 0x0000E7E7
		public virtual void StopMovement()
		{
			this.m_Velocity = Vector2.zero;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000105F4 File Offset: 0x0000E7F4
		public virtual void OnScroll(PointerEventData data)
		{
			if (!this.IsActive())
			{
				return;
			}
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			Vector2 scrollDelta = data.scrollDelta;
			scrollDelta.y *= -1f;
			if (this.vertical && !this.horizontal)
			{
				if (Mathf.Abs(scrollDelta.x) > Mathf.Abs(scrollDelta.y))
				{
					scrollDelta.y = scrollDelta.x;
				}
				scrollDelta.x = 0f;
			}
			if (this.horizontal && !this.vertical)
			{
				if (Mathf.Abs(scrollDelta.y) > Mathf.Abs(scrollDelta.x))
				{
					scrollDelta.x = scrollDelta.y;
				}
				scrollDelta.y = 0f;
			}
			if (data.IsScrolling())
			{
				this.m_Scrolling = true;
			}
			Vector2 vector = this.m_Content.anchoredPosition;
			vector += scrollDelta * this.m_ScrollSensitivity;
			if (this.m_MovementType == ScrollRect.MovementType.Clamped)
			{
				vector += this.CalculateOffset(vector - this.m_Content.anchoredPosition);
			}
			this.SetContentAnchoredPosition(vector);
			this.UpdateBounds();
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00010711 File Offset: 0x0000E911
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.m_Velocity = Vector2.zero;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00010728 File Offset: 0x0000E928
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateBounds();
			this.m_PointerStartLocalCursor = Vector2.zero;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.position, eventData.pressEventCamera, out this.m_PointerStartLocalCursor);
			this.m_ContentStartPosition = this.m_Content.anchoredPosition;
			this.m_Dragging = true;
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001078E File Offset: 0x0000E98E
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.m_Dragging = false;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000107A0 File Offset: 0x0000E9A0
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.m_Dragging)
			{
				return;
			}
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (!this.IsActive())
			{
				return;
			}
			Vector2 vector;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.position, eventData.pressEventCamera, out vector))
			{
				return;
			}
			this.UpdateBounds();
			Vector2 vector2 = vector - this.m_PointerStartLocalCursor;
			Vector2 vector3 = this.m_ContentStartPosition + vector2;
			Vector2 vector4 = this.CalculateOffset(vector3 - this.m_Content.anchoredPosition);
			vector3 += vector4;
			if (this.m_MovementType == ScrollRect.MovementType.Elastic)
			{
				if (vector4.x != 0f)
				{
					vector3.x -= ScrollRect.RubberDelta(vector4.x, this.m_ViewBounds.size.x);
				}
				if (vector4.y != 0f)
				{
					vector3.y -= ScrollRect.RubberDelta(vector4.y, this.m_ViewBounds.size.y);
				}
			}
			this.SetContentAnchoredPosition(vector3);
		}

		// Token: 0x06000345 RID: 837 RVA: 0x000108A0 File Offset: 0x0000EAA0
		protected virtual void SetContentAnchoredPosition(Vector2 position)
		{
			if (!this.m_Horizontal)
			{
				position.x = this.m_Content.anchoredPosition.x;
			}
			if (!this.m_Vertical)
			{
				position.y = this.m_Content.anchoredPosition.y;
			}
			if (position != this.m_Content.anchoredPosition)
			{
				this.m_Content.anchoredPosition = position;
				this.UpdateBounds();
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00010910 File Offset: 0x0000EB10
		protected virtual void LateUpdate()
		{
			if (!this.m_Content)
			{
				return;
			}
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			Vector2 vector = this.CalculateOffset(Vector2.zero);
			if (!this.m_Dragging && (vector != Vector2.zero || this.m_Velocity != Vector2.zero))
			{
				Vector2 vector2 = this.m_Content.anchoredPosition;
				for (int i = 0; i < 2; i++)
				{
					if (this.m_MovementType == ScrollRect.MovementType.Elastic && vector[i] != 0f)
					{
						float num = this.m_Velocity[i];
						float num2 = this.m_Elasticity;
						if (this.m_Scrolling)
						{
							num2 *= 3f;
						}
						vector2[i] = Mathf.SmoothDamp(this.m_Content.anchoredPosition[i], this.m_Content.anchoredPosition[i] + vector[i], ref num, num2, float.PositiveInfinity, unscaledDeltaTime);
						if (Mathf.Abs(num) < 1f)
						{
							num = 0f;
						}
						this.m_Velocity[i] = num;
					}
					else if (this.m_Inertia)
					{
						ref Vector2 ptr = ref this.m_Velocity;
						int num3 = i;
						ptr[num3] *= Mathf.Pow(this.m_DecelerationRate, unscaledDeltaTime);
						if (Mathf.Abs(this.m_Velocity[i]) < 1f)
						{
							this.m_Velocity[i] = 0f;
						}
						ptr = ref vector2;
						num3 = i;
						ptr[num3] += this.m_Velocity[i] * unscaledDeltaTime;
					}
					else
					{
						this.m_Velocity[i] = 0f;
					}
				}
				if (this.m_MovementType == ScrollRect.MovementType.Clamped)
				{
					vector = this.CalculateOffset(vector2 - this.m_Content.anchoredPosition);
					vector2 += vector;
				}
				this.SetContentAnchoredPosition(vector2);
			}
			if (this.m_Dragging && this.m_Inertia)
			{
				Vector3 vector3 = (this.m_Content.anchoredPosition - this.m_PrevPosition) / unscaledDeltaTime;
				this.m_Velocity = Vector3.Lerp(this.m_Velocity, vector3, unscaledDeltaTime * 10f);
			}
			if (this.m_ViewBounds != this.m_PrevViewBounds || this.m_ContentBounds != this.m_PrevContentBounds || this.m_Content.anchoredPosition != this.m_PrevPosition)
			{
				this.UpdateScrollbars(vector);
				UISystemProfilerApi.AddMarker("ScrollRect.value", this);
				this.m_OnValueChanged.Invoke(this.normalizedPosition);
				this.UpdatePrevData();
			}
			this.UpdateScrollbarVisibility();
			this.m_Scrolling = false;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00010BE0 File Offset: 0x0000EDE0
		protected void UpdatePrevData()
		{
			if (this.m_Content == null)
			{
				this.m_PrevPosition = Vector2.zero;
			}
			else
			{
				this.m_PrevPosition = this.m_Content.anchoredPosition;
			}
			this.m_PrevViewBounds = this.m_ViewBounds;
			this.m_PrevContentBounds = this.m_ContentBounds;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00010C34 File Offset: 0x0000EE34
		private void UpdateScrollbars(Vector2 offset)
		{
			if (this.m_HorizontalScrollbar)
			{
				if (this.m_ContentBounds.size.x > 0f)
				{
					this.m_HorizontalScrollbar.size = Mathf.Clamp01((this.m_ViewBounds.size.x - Mathf.Abs(offset.x)) / this.m_ContentBounds.size.x);
				}
				else
				{
					this.m_HorizontalScrollbar.size = 1f;
				}
				this.m_HorizontalScrollbar.value = this.horizontalNormalizedPosition;
			}
			if (this.m_VerticalScrollbar)
			{
				if (this.m_ContentBounds.size.y > 0f)
				{
					this.m_VerticalScrollbar.size = Mathf.Clamp01((this.m_ViewBounds.size.y - Mathf.Abs(offset.y)) / this.m_ContentBounds.size.y);
				}
				else
				{
					this.m_VerticalScrollbar.size = 1f;
				}
				this.m_VerticalScrollbar.value = this.verticalNormalizedPosition;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00010D49 File Offset: 0x0000EF49
		// (set) Token: 0x0600034A RID: 842 RVA: 0x00010D5C File Offset: 0x0000EF5C
		public Vector2 normalizedPosition
		{
			get
			{
				return new Vector2(this.horizontalNormalizedPosition, this.verticalNormalizedPosition);
			}
			set
			{
				this.SetNormalizedPosition(value.x, 0);
				this.SetNormalizedPosition(value.y, 1);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00010D78 File Offset: 0x0000EF78
		// (set) Token: 0x0600034C RID: 844 RVA: 0x00010E3F File Offset: 0x0000F03F
		public float horizontalNormalizedPosition
		{
			get
			{
				this.UpdateBounds();
				if (this.m_ContentBounds.size.x <= this.m_ViewBounds.size.x || Mathf.Approximately(this.m_ContentBounds.size.x, this.m_ViewBounds.size.x))
				{
					return (float)((this.m_ViewBounds.min.x > this.m_ContentBounds.min.x) ? 1 : 0);
				}
				return (this.m_ViewBounds.min.x - this.m_ContentBounds.min.x) / (this.m_ContentBounds.size.x - this.m_ViewBounds.size.x);
			}
			set
			{
				this.SetNormalizedPosition(value, 0);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00010E4C File Offset: 0x0000F04C
		// (set) Token: 0x0600034E RID: 846 RVA: 0x00010F13 File Offset: 0x0000F113
		public float verticalNormalizedPosition
		{
			get
			{
				this.UpdateBounds();
				if (this.m_ContentBounds.size.y <= this.m_ViewBounds.size.y || Mathf.Approximately(this.m_ContentBounds.size.y, this.m_ViewBounds.size.y))
				{
					return (float)((this.m_ViewBounds.min.y > this.m_ContentBounds.min.y) ? 1 : 0);
				}
				return (this.m_ViewBounds.min.y - this.m_ContentBounds.min.y) / (this.m_ContentBounds.size.y - this.m_ViewBounds.size.y);
			}
			set
			{
				this.SetNormalizedPosition(value, 1);
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00010E3F File Offset: 0x0000F03F
		private void SetHorizontalNormalizedPosition(float value)
		{
			this.SetNormalizedPosition(value, 0);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00010F13 File Offset: 0x0000F113
		private void SetVerticalNormalizedPosition(float value)
		{
			this.SetNormalizedPosition(value, 1);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00010F20 File Offset: 0x0000F120
		protected virtual void SetNormalizedPosition(float value, int axis)
		{
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			float num = this.m_ContentBounds.size[axis] - this.m_ViewBounds.size[axis];
			float num2 = this.m_ViewBounds.min[axis] - value * num;
			float num3 = this.m_Content.localPosition[axis] + num2 - this.m_ContentBounds.min[axis];
			Vector3 localPosition = this.m_Content.localPosition;
			if (Mathf.Abs(localPosition[axis] - num3) > 0.01f)
			{
				localPosition[axis] = num3;
				this.m_Content.localPosition = localPosition;
				this.m_Velocity[axis] = 0f;
				this.UpdateBounds();
			}
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00010FFB File Offset: 0x0000F1FB
		private static float RubberDelta(float overStretching, float viewSize)
		{
			return (1f - 1f / (Mathf.Abs(overStretching) * 0.55f / viewSize + 1f)) * viewSize * Mathf.Sign(overStretching);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00011026 File Offset: 0x0000F226
		protected override void OnRectTransformDimensionsChange()
		{
			this.SetDirty();
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0001102E File Offset: 0x0000F22E
		private bool hScrollingNeeded
		{
			get
			{
				return !Application.isPlaying || this.m_ContentBounds.size.x > this.m_ViewBounds.size.x + 0.01f;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00011061 File Offset: 0x0000F261
		private bool vScrollingNeeded
		{
			get
			{
				return !Application.isPlaying || this.m_ContentBounds.size.y > this.m_ViewBounds.size.y + 0.01f;
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00008C4E File Offset: 0x00006E4E
		public virtual float minWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00008C4E File Offset: 0x00006E4E
		public virtual float preferredWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00008C4E File Offset: 0x00006E4E
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600035B RID: 859 RVA: 0x00008C4E File Offset: 0x00006E4E
		public virtual float minHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00008C4E File Offset: 0x00006E4E
		public virtual float preferredHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600035D RID: 861 RVA: 0x00008C4E File Offset: 0x00006E4E
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00011094 File Offset: 0x0000F294
		public virtual int layoutPriority
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00011098 File Offset: 0x0000F298
		public virtual void SetLayoutHorizontal()
		{
			this.m_Tracker.Clear();
			if (this.m_HSliderExpand || this.m_VSliderExpand)
			{
				this.m_Tracker.Add(this, this.viewRect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
				this.viewRect.anchorMin = Vector2.zero;
				this.viewRect.anchorMax = Vector2.one;
				this.viewRect.sizeDelta = Vector2.zero;
				this.viewRect.anchoredPosition = Vector2.zero;
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.content);
				this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
				this.m_ContentBounds = this.GetBounds();
			}
			if (this.m_VSliderExpand && this.vScrollingNeeded)
			{
				this.viewRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.viewRect.sizeDelta.y);
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.content);
				this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
				this.m_ContentBounds = this.GetBounds();
			}
			if (this.m_HSliderExpand && this.hScrollingNeeded)
			{
				this.viewRect.sizeDelta = new Vector2(this.viewRect.sizeDelta.x, -(this.m_HSliderHeight + this.m_HorizontalScrollbarSpacing));
				this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
				this.m_ContentBounds = this.GetBounds();
			}
			if (this.m_VSliderExpand && this.vScrollingNeeded && this.viewRect.sizeDelta.x == 0f && this.viewRect.sizeDelta.y < 0f)
			{
				this.viewRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.viewRect.sizeDelta.y);
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000112F4 File Offset: 0x0000F4F4
		public virtual void SetLayoutVertical()
		{
			this.UpdateScrollbarLayout();
			this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
			this.m_ContentBounds = this.GetBounds();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001134E File Offset: 0x0000F54E
		private void UpdateScrollbarVisibility()
		{
			ScrollRect.UpdateOneScrollbarVisibility(this.vScrollingNeeded, this.m_Vertical, this.m_VerticalScrollbarVisibility, this.m_VerticalScrollbar);
			ScrollRect.UpdateOneScrollbarVisibility(this.hScrollingNeeded, this.m_Horizontal, this.m_HorizontalScrollbarVisibility, this.m_HorizontalScrollbar);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x0001138C File Offset: 0x0000F58C
		private static void UpdateOneScrollbarVisibility(bool xScrollingNeeded, bool xAxisEnabled, ScrollRect.ScrollbarVisibility scrollbarVisibility, Scrollbar scrollbar)
		{
			if (scrollbar)
			{
				if (scrollbarVisibility == ScrollRect.ScrollbarVisibility.Permanent)
				{
					if (scrollbar.gameObject.activeSelf != xAxisEnabled)
					{
						scrollbar.gameObject.SetActive(xAxisEnabled);
						return;
					}
				}
				else if (scrollbar.gameObject.activeSelf != xScrollingNeeded)
				{
					scrollbar.gameObject.SetActive(xScrollingNeeded);
				}
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000113DC File Offset: 0x0000F5DC
		private void UpdateScrollbarLayout()
		{
			if (this.m_VSliderExpand && this.m_HorizontalScrollbar)
			{
				this.m_Tracker.Add(this, this.m_HorizontalScrollbarRect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.SizeDeltaX);
				this.m_HorizontalScrollbarRect.anchorMin = new Vector2(0f, this.m_HorizontalScrollbarRect.anchorMin.y);
				this.m_HorizontalScrollbarRect.anchorMax = new Vector2(1f, this.m_HorizontalScrollbarRect.anchorMax.y);
				this.m_HorizontalScrollbarRect.anchoredPosition = new Vector2(0f, this.m_HorizontalScrollbarRect.anchoredPosition.y);
				if (this.vScrollingNeeded)
				{
					this.m_HorizontalScrollbarRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.m_HorizontalScrollbarRect.sizeDelta.y);
				}
				else
				{
					this.m_HorizontalScrollbarRect.sizeDelta = new Vector2(0f, this.m_HorizontalScrollbarRect.sizeDelta.y);
				}
			}
			if (this.m_HSliderExpand && this.m_VerticalScrollbar)
			{
				this.m_Tracker.Add(this, this.m_VerticalScrollbarRect, DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaY);
				this.m_VerticalScrollbarRect.anchorMin = new Vector2(this.m_VerticalScrollbarRect.anchorMin.x, 0f);
				this.m_VerticalScrollbarRect.anchorMax = new Vector2(this.m_VerticalScrollbarRect.anchorMax.x, 1f);
				this.m_VerticalScrollbarRect.anchoredPosition = new Vector2(this.m_VerticalScrollbarRect.anchoredPosition.x, 0f);
				if (this.hScrollingNeeded)
				{
					this.m_VerticalScrollbarRect.sizeDelta = new Vector2(this.m_VerticalScrollbarRect.sizeDelta.x, -(this.m_HSliderHeight + this.m_HorizontalScrollbarSpacing));
					return;
				}
				this.m_VerticalScrollbarRect.sizeDelta = new Vector2(this.m_VerticalScrollbarRect.sizeDelta.x, 0f);
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000115E4 File Offset: 0x0000F7E4
		protected void UpdateBounds()
		{
			this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
			this.m_ContentBounds = this.GetBounds();
			if (this.m_Content == null)
			{
				return;
			}
			Vector3 size = this.m_ContentBounds.size;
			Vector3 vector = this.m_ContentBounds.center;
			Vector2 pivot = this.m_Content.pivot;
			ScrollRect.AdjustBounds(ref this.m_ViewBounds, ref pivot, ref size, ref vector);
			this.m_ContentBounds.size = size;
			this.m_ContentBounds.center = vector;
			if (this.movementType == ScrollRect.MovementType.Clamped)
			{
				Vector2 zero = Vector2.zero;
				if (this.m_ViewBounds.max.x > this.m_ContentBounds.max.x)
				{
					zero.x = Math.Min(this.m_ViewBounds.min.x - this.m_ContentBounds.min.x, this.m_ViewBounds.max.x - this.m_ContentBounds.max.x);
				}
				else if (this.m_ViewBounds.min.x < this.m_ContentBounds.min.x)
				{
					zero.x = Math.Max(this.m_ViewBounds.min.x - this.m_ContentBounds.min.x, this.m_ViewBounds.max.x - this.m_ContentBounds.max.x);
				}
				if (this.m_ViewBounds.min.y < this.m_ContentBounds.min.y)
				{
					zero.y = Math.Max(this.m_ViewBounds.min.y - this.m_ContentBounds.min.y, this.m_ViewBounds.max.y - this.m_ContentBounds.max.y);
				}
				else if (this.m_ViewBounds.max.y > this.m_ContentBounds.max.y)
				{
					zero.y = Math.Min(this.m_ViewBounds.min.y - this.m_ContentBounds.min.y, this.m_ViewBounds.max.y - this.m_ContentBounds.max.y);
				}
				if (zero.sqrMagnitude > 1E-45f)
				{
					vector = this.m_Content.anchoredPosition + zero;
					if (!this.m_Horizontal)
					{
						vector.x = this.m_Content.anchoredPosition.x;
					}
					if (!this.m_Vertical)
					{
						vector.y = this.m_Content.anchoredPosition.y;
					}
					ScrollRect.AdjustBounds(ref this.m_ViewBounds, ref pivot, ref size, ref vector);
				}
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000118E0 File Offset: 0x0000FAE0
		internal static void AdjustBounds(ref Bounds viewBounds, ref Vector2 contentPivot, ref Vector3 contentSize, ref Vector3 contentPos)
		{
			Vector3 vector = viewBounds.size - contentSize;
			if (vector.x > 0f)
			{
				contentPos.x -= vector.x * (contentPivot.x - 0.5f);
				contentSize.x = viewBounds.size.x;
			}
			if (vector.y > 0f)
			{
				contentPos.y -= vector.y * (contentPivot.y - 0.5f);
				contentSize.y = viewBounds.size.y;
			}
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00011978 File Offset: 0x0000FB78
		private Bounds GetBounds()
		{
			if (this.m_Content == null)
			{
				return default(Bounds);
			}
			this.m_Content.GetWorldCorners(this.m_Corners);
			Matrix4x4 worldToLocalMatrix = this.viewRect.worldToLocalMatrix;
			return ScrollRect.InternalGetBounds(this.m_Corners, ref worldToLocalMatrix);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000119C8 File Offset: 0x0000FBC8
		internal static Bounds InternalGetBounds(Vector3[] corners, ref Matrix4x4 viewWorldToLocalMatrix)
		{
			Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			for (int i = 0; i < 4; i++)
			{
				Vector3 vector3 = viewWorldToLocalMatrix.MultiplyPoint3x4(corners[i]);
				vector = Vector3.Min(vector3, vector);
				vector2 = Vector3.Max(vector3, vector2);
			}
			Bounds bounds = new Bounds(vector, Vector3.zero);
			bounds.Encapsulate(vector2);
			return bounds;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00011A3F File Offset: 0x0000FC3F
		private Vector2 CalculateOffset(Vector2 delta)
		{
			return ScrollRect.InternalCalculateOffset(ref this.m_ViewBounds, ref this.m_ContentBounds, this.m_Horizontal, this.m_Vertical, this.m_MovementType, ref delta);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00011A68 File Offset: 0x0000FC68
		internal static Vector2 InternalCalculateOffset(ref Bounds viewBounds, ref Bounds contentBounds, bool horizontal, bool vertical, ScrollRect.MovementType movementType, ref Vector2 delta)
		{
			Vector2 zero = Vector2.zero;
			if (movementType == ScrollRect.MovementType.Unrestricted)
			{
				return zero;
			}
			Vector2 vector = contentBounds.min;
			Vector2 vector2 = contentBounds.max;
			if (horizontal)
			{
				vector.x += delta.x;
				vector2.x += delta.x;
				float num = viewBounds.max.x - vector2.x;
				float num2 = viewBounds.min.x - vector.x;
				if (num2 < -0.001f)
				{
					zero.x = num2;
				}
				else if (num > 0.001f)
				{
					zero.x = num;
				}
			}
			if (vertical)
			{
				vector.y += delta.y;
				vector2.y += delta.y;
				float num3 = viewBounds.max.y - vector2.y;
				float num4 = viewBounds.min.y - vector.y;
				if (num3 > 0.001f)
				{
					zero.y = num3;
				}
				else if (num4 < -0.001f)
				{
					zero.y = num4;
				}
			}
			return zero;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00011B81 File Offset: 0x0000FD81
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00011B97 File Offset: 0x0000FD97
		protected void SetDirtyCaching()
		{
			if (!this.IsActive())
			{
				return;
			}
			CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			this.m_ViewRect = null;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00005DE4 File Offset: 0x00003FE4
		Transform ICanvasElement.get_transform()
		{
			return base.transform;
		}

		// Token: 0x0400010C RID: 268
		[SerializeField]
		private RectTransform m_Content;

		// Token: 0x0400010D RID: 269
		[SerializeField]
		private bool m_Horizontal = true;

		// Token: 0x0400010E RID: 270
		[SerializeField]
		private bool m_Vertical = true;

		// Token: 0x0400010F RID: 271
		[SerializeField]
		private ScrollRect.MovementType m_MovementType = ScrollRect.MovementType.Elastic;

		// Token: 0x04000110 RID: 272
		[SerializeField]
		private float m_Elasticity = 0.1f;

		// Token: 0x04000111 RID: 273
		[SerializeField]
		private bool m_Inertia = true;

		// Token: 0x04000112 RID: 274
		[SerializeField]
		private float m_DecelerationRate = 0.135f;

		// Token: 0x04000113 RID: 275
		[SerializeField]
		private float m_ScrollSensitivity = 1f;

		// Token: 0x04000114 RID: 276
		[SerializeField]
		private RectTransform m_Viewport;

		// Token: 0x04000115 RID: 277
		[SerializeField]
		private Scrollbar m_HorizontalScrollbar;

		// Token: 0x04000116 RID: 278
		[SerializeField]
		private Scrollbar m_VerticalScrollbar;

		// Token: 0x04000117 RID: 279
		[SerializeField]
		private ScrollRect.ScrollbarVisibility m_HorizontalScrollbarVisibility;

		// Token: 0x04000118 RID: 280
		[SerializeField]
		private ScrollRect.ScrollbarVisibility m_VerticalScrollbarVisibility;

		// Token: 0x04000119 RID: 281
		[SerializeField]
		private float m_HorizontalScrollbarSpacing;

		// Token: 0x0400011A RID: 282
		[SerializeField]
		private float m_VerticalScrollbarSpacing;

		// Token: 0x0400011B RID: 283
		[SerializeField]
		private ScrollRect.ScrollRectEvent m_OnValueChanged = new ScrollRect.ScrollRectEvent();

		// Token: 0x0400011C RID: 284
		private Vector2 m_PointerStartLocalCursor = Vector2.zero;

		// Token: 0x0400011D RID: 285
		protected Vector2 m_ContentStartPosition = Vector2.zero;

		// Token: 0x0400011E RID: 286
		private RectTransform m_ViewRect;

		// Token: 0x0400011F RID: 287
		protected Bounds m_ContentBounds;

		// Token: 0x04000120 RID: 288
		private Bounds m_ViewBounds;

		// Token: 0x04000121 RID: 289
		private Vector2 m_Velocity;

		// Token: 0x04000122 RID: 290
		private bool m_Dragging;

		// Token: 0x04000123 RID: 291
		private bool m_Scrolling;

		// Token: 0x04000124 RID: 292
		private Vector2 m_PrevPosition = Vector2.zero;

		// Token: 0x04000125 RID: 293
		private Bounds m_PrevContentBounds;

		// Token: 0x04000126 RID: 294
		private Bounds m_PrevViewBounds;

		// Token: 0x04000127 RID: 295
		[NonSerialized]
		private bool m_HasRebuiltLayout;

		// Token: 0x04000128 RID: 296
		private bool m_HSliderExpand;

		// Token: 0x04000129 RID: 297
		private bool m_VSliderExpand;

		// Token: 0x0400012A RID: 298
		private float m_HSliderHeight;

		// Token: 0x0400012B RID: 299
		private float m_VSliderWidth;

		// Token: 0x0400012C RID: 300
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x0400012D RID: 301
		private RectTransform m_HorizontalScrollbarRect;

		// Token: 0x0400012E RID: 302
		private RectTransform m_VerticalScrollbarRect;

		// Token: 0x0400012F RID: 303
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x04000130 RID: 304
		private readonly Vector3[] m_Corners = new Vector3[4];

		// Token: 0x0200009F RID: 159
		public enum MovementType
		{
			// Token: 0x040002C4 RID: 708
			Unrestricted,
			// Token: 0x040002C5 RID: 709
			Elastic,
			// Token: 0x040002C6 RID: 710
			Clamped
		}

		// Token: 0x020000A0 RID: 160
		public enum ScrollbarVisibility
		{
			// Token: 0x040002C8 RID: 712
			Permanent,
			// Token: 0x040002C9 RID: 713
			AutoHide,
			// Token: 0x040002CA RID: 714
			AutoHideAndExpandViewport
		}

		// Token: 0x020000A1 RID: 161
		[Serializable]
		public class ScrollRectEvent : UnityEvent<Vector2>
		{
		}
	}
}
