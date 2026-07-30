using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000160 RID: 352
	public abstract class MouseEventBase<T> : EventBase<T>, IMouseEvent, IMouseEventInternal where T : MouseEventBase<T>, new()
	{
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00025BA5 File Offset: 0x00023DA5
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x00025BAD File Offset: 0x00023DAD
		public EventModifiers modifiers { get; protected set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x00025BB6 File Offset: 0x00023DB6
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x00025BBE File Offset: 0x00023DBE
		public Vector2 mousePosition { get; protected set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x00025BC7 File Offset: 0x00023DC7
		// (set) Token: 0x060009D0 RID: 2512 RVA: 0x00025BCF File Offset: 0x00023DCF
		public Vector2 localMousePosition { get; internal set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x00025BD8 File Offset: 0x00023DD8
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x00025BE0 File Offset: 0x00023DE0
		public Vector2 mouseDelta { get; protected set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x00025BE9 File Offset: 0x00023DE9
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x00025BF1 File Offset: 0x00023DF1
		public int clickCount { get; protected set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x00025BFA File Offset: 0x00023DFA
		// (set) Token: 0x060009D6 RID: 2518 RVA: 0x00025C02 File Offset: 0x00023E02
		public int button { get; protected set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x00025C0B File Offset: 0x00023E0B
		// (set) Token: 0x060009D8 RID: 2520 RVA: 0x00025C13 File Offset: 0x00023E13
		public int pressedButtons { get; protected set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00025C1C File Offset: 0x00023E1C
		public bool shiftKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Shift) > EventModifiers.None;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00025C3C File Offset: 0x00023E3C
		public bool ctrlKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Control) > EventModifiers.None;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x00025C5C File Offset: 0x00023E5C
		public bool commandKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Command) > EventModifiers.None;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x00025C7C File Offset: 0x00023E7C
		public bool altKey
		{
			get
			{
				return (this.modifiers & EventModifiers.Alt) > EventModifiers.None;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060009DD RID: 2525 RVA: 0x00025C9C File Offset: 0x00023E9C
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

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00025CD5 File Offset: 0x00023ED5
		// (set) Token: 0x060009DF RID: 2527 RVA: 0x00025CDD File Offset: 0x00023EDD
		bool IMouseEventInternal.triggeredByOS { get; set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060009E0 RID: 2528 RVA: 0x00025CE6 File Offset: 0x00023EE6
		// (set) Token: 0x060009E1 RID: 2529 RVA: 0x00025CEE File Offset: 0x00023EEE
		bool IMouseEventInternal.recomputeTopElementUnderMouse { get; set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x060009E2 RID: 2530 RVA: 0x00025CF7 File Offset: 0x00023EF7
		// (set) Token: 0x060009E3 RID: 2531 RVA: 0x00025CFF File Offset: 0x00023EFF
		IPointerEvent IMouseEventInternal.sourcePointerEvent { get; set; }

		// Token: 0x060009E4 RID: 2532 RVA: 0x00025D08 File Offset: 0x00023F08
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00025D1C File Offset: 0x00023F1C
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.Cancellable;
			this.modifiers = EventModifiers.None;
			this.mousePosition = Vector2.zero;
			this.localMousePosition = Vector2.zero;
			this.mouseDelta = Vector2.zero;
			this.clickCount = 0;
			this.button = 0;
			this.pressedButtons = 0;
			((IMouseEventInternal)this).triggeredByOS = false;
			((IMouseEventInternal)this).recomputeTopElementUnderMouse = true;
			((IMouseEventInternal)this).sourcePointerEvent = null;
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00025D90 File Offset: 0x00023F90
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x00025DA8 File Offset: 0x00023FA8
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
					this.localMousePosition = visualElement.WorldToLocal(this.mousePosition);
				}
				else
				{
					this.localMousePosition = this.mousePosition;
				}
			}
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x00025DF8 File Offset: 0x00023FF8
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool triggeredByOS = ((IMouseEventInternal)this).triggeredByOS;
			if (triggeredByOS)
			{
				PointerDeviceState.SavePointerPosition(PointerId.mousePointerId, this.mousePosition, panel);
			}
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x00025E2C File Offset: 0x0002402C
		protected internal override void PostDispatch(IPanel panel)
		{
			EventBase eventBase = ((IMouseEventInternal)this).sourcePointerEvent as EventBase;
			bool flag = eventBase != null;
			if (flag)
			{
				Debug.Assert(eventBase.processed);
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.CommitElementUnderPointers();
				}
				bool isPropagationStopped = base.isPropagationStopped;
				if (isPropagationStopped)
				{
					eventBase.StopPropagation();
				}
				bool isImmediatePropagationStopped = base.isImmediatePropagationStopped;
				if (isImmediatePropagationStopped)
				{
					eventBase.StopImmediatePropagation();
				}
				bool isDefaultPrevented = base.isDefaultPrevented;
				if (isDefaultPrevented)
				{
					eventBase.PreventDefault();
				}
			}
			base.PostDispatch(panel);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x00025EB4 File Offset: 0x000240B4
		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.imguiEvent = systemEvent;
			bool flag = systemEvent != null;
			if (flag)
			{
				pooled.modifiers = systemEvent.modifiers;
				pooled.mousePosition = systemEvent.mousePosition;
				pooled.localMousePosition = systemEvent.mousePosition;
				pooled.mouseDelta = systemEvent.delta;
				pooled.button = systemEvent.button;
				pooled.pressedButtons = PointerDeviceState.GetPressedButtons(PointerId.mousePointerId);
				pooled.clickCount = systemEvent.clickCount;
				pooled.triggeredByOS = true;
				pooled.recomputeTopElementUnderMouse = true;
			}
			return pooled;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x00025F84 File Offset: 0x00024184
		public static T GetPooled(Vector2 position, int button, int clickCount, Vector2 delta, EventModifiers modifiers = EventModifiers.None)
		{
			return MouseEventBase<T>.GetPooled(position, button, clickCount, delta, modifiers, false);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00025FA4 File Offset: 0x000241A4
		internal static T GetPooled(Vector2 position, int button, int clickCount, Vector2 delta, EventModifiers modifiers, bool fromOS)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.modifiers = modifiers;
			pooled.mousePosition = position;
			pooled.localMousePosition = position;
			pooled.mouseDelta = delta;
			pooled.button = button;
			pooled.pressedButtons = PointerDeviceState.GetPressedButtons(PointerId.mousePointerId);
			pooled.clickCount = clickCount;
			pooled.triggeredByOS = fromOS;
			pooled.recomputeTopElementUnderMouse = true;
			return pooled;
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x00026040 File Offset: 0x00024240
		internal static T GetPooled(IMouseEvent triggerEvent, Vector2 mousePosition, bool recomputeTopElementUnderMouse)
		{
			bool flag = triggerEvent != null;
			T t;
			if (flag)
			{
				t = MouseEventBase<T>.GetPooled(triggerEvent);
			}
			else
			{
				T pooled = EventBase<T>.GetPooled();
				pooled.mousePosition = mousePosition;
				pooled.localMousePosition = mousePosition;
				pooled.recomputeTopElementUnderMouse = recomputeTopElementUnderMouse;
				t = pooled;
			}
			return t;
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x00026094 File Offset: 0x00024294
		public static T GetPooled(IMouseEvent triggerEvent)
		{
			T pooled = EventBase<T>.GetPooled(triggerEvent as EventBase);
			bool flag = triggerEvent != null;
			if (flag)
			{
				pooled.modifiers = triggerEvent.modifiers;
				pooled.mousePosition = triggerEvent.mousePosition;
				pooled.localMousePosition = triggerEvent.mousePosition;
				pooled.mouseDelta = triggerEvent.mouseDelta;
				pooled.button = triggerEvent.button;
				pooled.pressedButtons = triggerEvent.pressedButtons;
				pooled.clickCount = triggerEvent.clickCount;
				IMouseEventInternal mouseEventInternal = triggerEvent as IMouseEventInternal;
				bool flag2 = mouseEventInternal != null;
				if (flag2)
				{
					pooled.triggeredByOS = mouseEventInternal.triggeredByOS;
					pooled.recomputeTopElementUnderMouse = false;
				}
			}
			return pooled;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x00026170 File Offset: 0x00024370
		protected static T GetPooled(IPointerEvent pointerEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			EventBase eventBase = pooled;
			EventBase eventBase2 = pointerEvent as EventBase;
			eventBase.target = ((eventBase2 != null) ? eventBase2.target : null);
			EventBase eventBase3 = pooled;
			EventBase eventBase4 = pointerEvent as EventBase;
			eventBase3.imguiEvent = ((eventBase4 != null) ? eventBase4.imguiEvent : null);
			EventBase eventBase5 = pointerEvent as EventBase;
			bool flag = ((eventBase5 != null) ? eventBase5.path : null) != null;
			if (flag)
			{
				pooled.path = (pointerEvent as EventBase).path;
			}
			pooled.modifiers = pointerEvent.modifiers;
			pooled.mousePosition = pointerEvent.position;
			pooled.localMousePosition = pointerEvent.position;
			pooled.mouseDelta = pointerEvent.deltaPosition;
			pooled.button = ((pointerEvent.button == -1) ? 0 : pointerEvent.button);
			pooled.pressedButtons = pointerEvent.pressedButtons;
			pooled.clickCount = pointerEvent.clickCount;
			IPointerEventInternal pointerEventInternal = pointerEvent as IPointerEventInternal;
			bool flag2 = pointerEventInternal != null;
			if (flag2)
			{
				pooled.triggeredByOS = pointerEventInternal.triggeredByOS;
				pooled.recomputeTopElementUnderMouse = true;
				pooled.sourcePointerEvent = pointerEvent;
			}
			return pooled;
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x000262CD File Offset: 0x000244CD
		protected MouseEventBase()
		{
			this.LocalInit();
		}
	}
}
