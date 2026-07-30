using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200017B RID: 379
	public abstract class PointerEventBase<T> : EventBase<T>, IPointerEvent, IPointerEventInternal where T : PointerEventBase<T>, new()
	{
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x000274D0 File Offset: 0x000256D0
		// (set) Token: 0x06000A6A RID: 2666 RVA: 0x000274D8 File Offset: 0x000256D8
		public int pointerId { get; protected set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x000274E1 File Offset: 0x000256E1
		// (set) Token: 0x06000A6C RID: 2668 RVA: 0x000274E9 File Offset: 0x000256E9
		public string pointerType { get; protected set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x000274F2 File Offset: 0x000256F2
		// (set) Token: 0x06000A6E RID: 2670 RVA: 0x000274FA File Offset: 0x000256FA
		public bool isPrimary { get; protected set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x00027503 File Offset: 0x00025703
		// (set) Token: 0x06000A70 RID: 2672 RVA: 0x0002750B File Offset: 0x0002570B
		public int button { get; protected set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000A71 RID: 2673 RVA: 0x00027514 File Offset: 0x00025714
		// (set) Token: 0x06000A72 RID: 2674 RVA: 0x0002751C File Offset: 0x0002571C
		public int pressedButtons { get; protected set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000A73 RID: 2675 RVA: 0x00027525 File Offset: 0x00025725
		// (set) Token: 0x06000A74 RID: 2676 RVA: 0x0002752D File Offset: 0x0002572D
		public Vector3 position { get; protected set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00027536 File Offset: 0x00025736
		// (set) Token: 0x06000A76 RID: 2678 RVA: 0x0002753E File Offset: 0x0002573E
		public Vector3 localPosition { get; protected set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000A77 RID: 2679 RVA: 0x00027547 File Offset: 0x00025747
		// (set) Token: 0x06000A78 RID: 2680 RVA: 0x0002754F File Offset: 0x0002574F
		public Vector3 deltaPosition { get; protected set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00027558 File Offset: 0x00025758
		// (set) Token: 0x06000A7A RID: 2682 RVA: 0x00027560 File Offset: 0x00025760
		public float deltaTime { get; protected set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x00027569 File Offset: 0x00025769
		// (set) Token: 0x06000A7C RID: 2684 RVA: 0x00027571 File Offset: 0x00025771
		public int clickCount { get; protected set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000A7D RID: 2685 RVA: 0x0002757A File Offset: 0x0002577A
		// (set) Token: 0x06000A7E RID: 2686 RVA: 0x00027582 File Offset: 0x00025782
		public float pressure { get; protected set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0002758B File Offset: 0x0002578B
		// (set) Token: 0x06000A80 RID: 2688 RVA: 0x00027593 File Offset: 0x00025793
		public float tangentialPressure { get; protected set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x0002759C File Offset: 0x0002579C
		// (set) Token: 0x06000A82 RID: 2690 RVA: 0x000275A4 File Offset: 0x000257A4
		public float altitudeAngle { get; protected set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x000275AD File Offset: 0x000257AD
		// (set) Token: 0x06000A84 RID: 2692 RVA: 0x000275B5 File Offset: 0x000257B5
		public float azimuthAngle { get; protected set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x000275BE File Offset: 0x000257BE
		// (set) Token: 0x06000A86 RID: 2694 RVA: 0x000275C6 File Offset: 0x000257C6
		public float twist { get; protected set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x000275CF File Offset: 0x000257CF
		// (set) Token: 0x06000A88 RID: 2696 RVA: 0x000275D7 File Offset: 0x000257D7
		public Vector2 radius { get; protected set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x000275E0 File Offset: 0x000257E0
		// (set) Token: 0x06000A8A RID: 2698 RVA: 0x000275E8 File Offset: 0x000257E8
		public Vector2 radiusVariance { get; protected set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x000275F1 File Offset: 0x000257F1
		// (set) Token: 0x06000A8C RID: 2700 RVA: 0x000275F9 File Offset: 0x000257F9
		public EventModifiers modifiers { get; protected set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00027604 File Offset: 0x00025804
		public bool shiftKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00027624 File Offset: 0x00025824
		public bool ctrlKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x00027644 File Offset: 0x00025844
		public bool commandKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x00027664 File Offset: 0x00025864
		public bool altKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x00027684 File Offset: 0x00025884
		public bool actionKey
		{
			get
			{
				bool flag = Application.platform == RuntimePlatform.OSXEditor || Application.platform == RuntimePlatform.OSXPlayer;
				bool flag2;
				if (flag)
				{
					flag2 = this.commandKey;
				}
				else
				{
					flag2 = this.ctrlKey;
				}
				return flag2;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000A92 RID: 2706 RVA: 0x000276BD File Offset: 0x000258BD
		// (set) Token: 0x06000A93 RID: 2707 RVA: 0x000276C5 File Offset: 0x000258C5
		bool IPointerEventInternal.triggeredByOS { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x000276CE File Offset: 0x000258CE
		// (set) Token: 0x06000A95 RID: 2709 RVA: 0x000276D6 File Offset: 0x000258D6
		bool IPointerEventInternal.recomputeTopElementUnderPointer { get; set; }

		// Token: 0x06000A96 RID: 2710 RVA: 0x000276DF File Offset: 0x000258DF
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000276F0 File Offset: 0x000258F0
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable;
			base.propagateToIMGUI = false;
			this.pointerId = 0;
			this.pointerType = PointerType.unknown;
			this.isPrimary = false;
			this.button = -1;
			this.pressedButtons = 0;
			this.position = Vector3.zero;
			this.localPosition = Vector3.zero;
			this.deltaPosition = Vector3.zero;
			this.deltaTime = 0f;
			this.clickCount = 0;
			this.pressure = 0f;
			this.tangentialPressure = 0f;
			this.altitudeAngle = 0f;
			this.azimuthAngle = 0f;
			this.twist = 0f;
			this.radius = Vector2.zero;
			this.radiusVariance = Vector2.zero;
			this.modifiers = EventModifiers.None;
			((IPointerEventInternal)this).triggeredByOS = false;
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = false;
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x000277E0 File Offset: 0x000259E0
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x000277F8 File Offset: 0x000259F8
		public override IEventHandler currentTarget
		{
			get
			{
				return base.currentTarget;
			}
			internal set
			{
				base.currentTarget = value;
				VisualElement visualElement = this.currentTarget as VisualElement;
				bool flag = visualElement != null;
				if (flag)
				{
					this.localPosition = visualElement.WorldToLocal(this.position);
				}
				else
				{
					this.localPosition = this.position;
				}
			}
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00027854 File Offset: 0x00025A54
		private static bool IsMouse(Event systemEvent)
		{
			EventType rawType = systemEvent.rawType;
			return rawType == EventType.MouseMove || rawType == EventType.MouseDown || rawType == EventType.MouseUp || rawType == EventType.MouseDrag || rawType == EventType.ContextClick || rawType == EventType.MouseEnterWindow || rawType == EventType.MouseLeaveWindow;
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00027890 File Offset: 0x00025A90
		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			bool flag = !PointerEventBase<T>.IsMouse(systemEvent) && systemEvent.rawType != EventType.DragUpdated;
			if (flag)
			{
				Debug.Assert(false, string.Concat(new object[] { "Unexpected event type: ", systemEvent.rawType, " (", systemEvent.type, ")" }));
			}
			PointerType pointerType = systemEvent.pointerType;
			if (pointerType != PointerType.Touch)
			{
				if (pointerType != PointerType.Pen)
				{
					pooled.pointerType = PointerType.mouse;
					pooled.pointerId = PointerId.mousePointerId;
				}
				else
				{
					pooled.pointerType = PointerType.pen;
					pooled.pointerId = PointerId.penPointerIdBase;
				}
			}
			else
			{
				pooled.pointerType = PointerType.touch;
				pooled.pointerId = PointerId.touchPointerIdBase;
			}
			pooled.isPrimary = true;
			pooled.altitudeAngle = 0f;
			pooled.azimuthAngle = 0f;
			pooled.twist = 0f;
			pooled.radius = Vector2.zero;
			pooled.radiusVariance = Vector2.zero;
			pooled.imguiEvent = systemEvent;
			bool flag2 = systemEvent.rawType == EventType.MouseDown;
			if (flag2)
			{
				PointerDeviceState.PressButton(PointerId.mousePointerId, systemEvent.button);
				pooled.button = systemEvent.button;
			}
			else
			{
				bool flag3 = systemEvent.rawType == EventType.MouseUp;
				if (flag3)
				{
					PointerDeviceState.ReleaseButton(PointerId.mousePointerId, systemEvent.button);
					pooled.button = systemEvent.button;
				}
				else
				{
					bool flag4 = systemEvent.rawType == EventType.MouseMove;
					if (flag4)
					{
						pooled.button = -1;
					}
				}
			}
			pooled.pressedButtons = PointerDeviceState.GetPressedButtons(pooled.pointerId);
			pooled.position = systemEvent.mousePosition;
			pooled.localPosition = systemEvent.mousePosition;
			pooled.deltaPosition = systemEvent.delta;
			pooled.clickCount = systemEvent.clickCount;
			pooled.modifiers = systemEvent.modifiers;
			PointerType pointerType2 = systemEvent.pointerType;
			if (pointerType2 - PointerType.Touch > 1)
			{
				pooled.pressure = ((pooled.pressedButtons == 0) ? 0f : 0.5f);
			}
			else
			{
				pooled.pressure = systemEvent.pressure;
			}
			pooled.tangentialPressure = 0f;
			pooled.triggeredByOS = true;
			return pooled;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00027B74 File Offset: 0x00025D74
		public static T GetPooled(Touch touch, EventModifiers modifiers = EventModifiers.None)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.pointerId = touch.fingerId + PointerId.touchPointerIdBase;
			pooled.pointerType = PointerType.touch;
			bool flag = false;
			for (int i = PointerId.touchPointerIdBase; i < PointerId.touchPointerIdBase + PointerId.touchPointerCount; i++)
			{
				bool flag2 = i != pooled.pointerId && PointerDeviceState.GetPressedButtons(i) != 0;
				if (flag2)
				{
					flag = true;
					break;
				}
			}
			pooled.isPrimary = !flag;
			bool flag3 = touch.phase == TouchPhase.Began;
			if (flag3)
			{
				PointerDeviceState.PressButton(pooled.pointerId, 0);
				pooled.button = 0;
			}
			else
			{
				bool flag4 = touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled;
				if (flag4)
				{
					PointerDeviceState.ReleaseButton(pooled.pointerId, 0);
					pooled.button = 0;
				}
				else
				{
					pooled.button = -1;
				}
			}
			pooled.pressedButtons = PointerDeviceState.GetPressedButtons(pooled.pointerId);
			pooled.position = touch.position;
			pooled.localPosition = touch.position;
			pooled.deltaPosition = touch.deltaPosition;
			pooled.deltaTime = touch.deltaTime;
			pooled.clickCount = touch.tapCount;
			pooled.pressure = ((Mathf.Abs(touch.maximumPossiblePressure) > Mathf.Epsilon) ? (touch.pressure / touch.maximumPossiblePressure) : 1f);
			pooled.tangentialPressure = 0f;
			pooled.altitudeAngle = touch.altitudeAngle;
			pooled.azimuthAngle = touch.azimuthAngle;
			pooled.twist = 0f;
			pooled.radius = new Vector2(touch.radius, touch.radius);
			pooled.radiusVariance = new Vector2(touch.radiusVariance, touch.radiusVariance);
			pooled.modifiers = modifiers;
			pooled.triggeredByOS = true;
			return pooled;
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00027DF4 File Offset: 0x00025FF4
		internal static T GetPooled(IPointerEvent triggerEvent, Vector2 position, int pointerId)
		{
			bool flag = triggerEvent != null;
			T t;
			if (flag)
			{
				t = PointerEventBase<T>.GetPooled(triggerEvent);
			}
			else
			{
				T pooled = EventBase<T>.GetPooled();
				pooled.position = position;
				pooled.localPosition = position;
				pooled.pointerId = pointerId;
				pooled.pointerType = PointerType.GetPointerType(pointerId);
				t = pooled;
			}
			return t;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00027E64 File Offset: 0x00026064
		public static T GetPooled(IPointerEvent triggerEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			bool flag = triggerEvent != null;
			if (flag)
			{
				pooled.pointerId = triggerEvent.pointerId;
				pooled.pointerType = triggerEvent.pointerType;
				pooled.isPrimary = triggerEvent.isPrimary;
				pooled.button = triggerEvent.button;
				pooled.pressedButtons = triggerEvent.pressedButtons;
				pooled.position = triggerEvent.position;
				pooled.localPosition = triggerEvent.localPosition;
				pooled.deltaPosition = triggerEvent.deltaPosition;
				pooled.deltaTime = triggerEvent.deltaTime;
				pooled.clickCount = triggerEvent.clickCount;
				pooled.pressure = triggerEvent.pressure;
				pooled.tangentialPressure = triggerEvent.tangentialPressure;
				pooled.altitudeAngle = triggerEvent.altitudeAngle;
				pooled.azimuthAngle = triggerEvent.azimuthAngle;
				pooled.twist = triggerEvent.twist;
				pooled.radius = triggerEvent.radius;
				pooled.radiusVariance = triggerEvent.radiusVariance;
				pooled.modifiers = triggerEvent.modifiers;
				IPointerEventInternal pointerEventInternal = triggerEvent as IPointerEventInternal;
				bool flag2 = pointerEventInternal != null;
				if (flag2)
				{
					pooled.triggeredByOS = pointerEventInternal.triggeredByOS;
				}
			}
			return pooled;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00027FF4 File Offset: 0x000261F4
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool triggeredByOS = ((IPointerEventInternal)this).triggeredByOS;
			if (triggeredByOS)
			{
				PointerDeviceState.SavePointerPosition(this.pointerId, this.position, panel);
			}
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00028030 File Offset: 0x00026230
		protected internal override void PostDispatch(IPanel panel)
		{
			for (int i = 0; i < PointerId.maxPointers; i++)
			{
				panel.ProcessPointerCapture(i);
			}
			bool flag = !panel.ShouldSendCompatibilityMouseEvents(this) && ((IPointerEventInternal)this).triggeredByOS;
			if (flag)
			{
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.CommitElementUnderPointers();
				}
			}
			base.PostDispatch(panel);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0002808E File Offset: 0x0002628E
		protected PointerEventBase()
		{
			this.LocalInit();
		}
	}
}
