using System;
using System.Diagnostics;

namespace UnityEngine.UIElements
{
	// Token: 0x02000007 RID: 7
	public class Clickable : PointerManipulator
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000015 RID: 21 RVA: 0x00002244 File Offset: 0x00000444
		// (remove) Token: 0x06000016 RID: 22 RVA: 0x0000227C File Offset: 0x0000047C
		[field: DebuggerBrowsable(0)]
		public event Action<EventBase> clickedWithEventInfo;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000017 RID: 23 RVA: 0x000022B4 File Offset: 0x000004B4
		// (remove) Token: 0x06000018 RID: 24 RVA: 0x000022EC File Offset: 0x000004EC
		[field: DebuggerBrowsable(0)]
		public event Action clicked;

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002321 File Offset: 0x00000521
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002329 File Offset: 0x00000529
		protected bool active { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002332 File Offset: 0x00000532
		// (set) Token: 0x0600001C RID: 28 RVA: 0x0000233A File Offset: 0x0000053A
		public Vector2 lastMousePosition { get; private set; }

		// Token: 0x0600001D RID: 29 RVA: 0x00002343 File Offset: 0x00000543
		public Clickable(Action handler, long delay, long interval)
			: this(handler)
		{
			this.m_Delay = delay;
			this.m_Interval = interval;
			this.active = false;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002364 File Offset: 0x00000564
		public Clickable(Action<EventBase> handler)
		{
			this.clickedWithEventInfo = handler;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000023A0 File Offset: 0x000005A0
		public Clickable(Action handler)
		{
			this.clicked = handler;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.LeftMouse
			});
			this.active = false;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000023E4 File Offset: 0x000005E4
		private void OnTimer(TimerState timerState)
		{
			bool flag = (this.clicked != null || this.clickedWithEventInfo != null) && this.IsRepeatable();
			if (flag)
			{
				bool flag2 = base.target.ContainsPoint(this.lastMousePosition);
				if (flag2)
				{
					this.Invoke(null);
					base.target.pseudoStates |= PseudoStates.Active;
				}
				else
				{
					base.target.pseudoStates &= ~PseudoStates.Active;
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002460 File Offset: 0x00000660
		private bool IsRepeatable()
		{
			return this.m_Delay > 0L || this.m_Interval > 0L;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000248C File Offset: 0x0000068C
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<MouseDownEvent>(new EventCallback<MouseDownEvent>(this.OnMouseDown), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<MouseMoveEvent>(new EventCallback<MouseMoveEvent>(this.OnMouseMove), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnMouseUp), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000024E8 File Offset: 0x000006E8
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<MouseDownEvent>(new EventCallback<MouseDownEvent>(this.OnMouseDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<MouseMoveEvent>(new EventCallback<MouseMoveEvent>(this.OnMouseMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<MouseUpEvent>(new EventCallback<MouseUpEvent>(this.OnMouseUp), TrickleDown.NoTrickleDown);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002541 File Offset: 0x00000741
		protected void Invoke(EventBase evt)
		{
			Action action = this.clicked;
			if (action != null)
			{
				action.Invoke();
			}
			Action<EventBase> action2 = this.clickedWithEventInfo;
			if (action2 != null)
			{
				action2.Invoke(evt);
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x0000256C File Offset: 0x0000076C
		protected void OnMouseDown(MouseDownEvent evt)
		{
			bool flag = base.CanStartManipulation(evt);
			if (flag)
			{
				this.ProcessDownEvent(evt, evt.localMousePosition, PointerId.mousePointerId);
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002598 File Offset: 0x00000798
		protected void OnMouseMove(MouseMoveEvent evt)
		{
			bool active = this.active;
			if (active)
			{
				this.ProcessMoveEvent(evt, evt.localMousePosition);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025C0 File Offset: 0x000007C0
		protected void OnMouseUp(MouseUpEvent evt)
		{
			bool flag = this.active && base.CanStopManipulation(evt);
			if (flag)
			{
				this.ProcessUpEvent(evt, evt.localMousePosition, PointerId.mousePointerId);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025F8 File Offset: 0x000007F8
		protected virtual void ProcessDownEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.active = true;
			base.target.CapturePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(PointerId.mousePointerId);
			}
			this.lastMousePosition = localPosition;
			bool flag2 = this.IsRepeatable();
			if (flag2)
			{
				bool flag3 = base.target.ContainsPoint(localPosition);
				if (flag3)
				{
					this.Invoke(evt);
				}
				bool flag4 = this.m_Repeater == null;
				if (flag4)
				{
					this.m_Repeater = base.target.schedule.Execute(new Action<TimerState>(this.OnTimer)).Every(this.m_Interval).StartingIn(this.m_Delay);
				}
				else
				{
					this.m_Repeater.ExecuteLater(this.m_Delay);
				}
			}
			base.target.pseudoStates |= PseudoStates.Active;
			evt.StopImmediatePropagation();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000026E8 File Offset: 0x000008E8
		protected virtual void ProcessMoveEvent(EventBase evt, Vector2 localPosition)
		{
			this.lastMousePosition = localPosition;
			bool flag = base.target.ContainsPoint(localPosition);
			if (flag)
			{
				base.target.pseudoStates |= PseudoStates.Active;
			}
			else
			{
				base.target.pseudoStates &= ~PseudoStates.Active;
			}
			evt.StopPropagation();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002744 File Offset: 0x00000944
		protected virtual void ProcessUpEvent(EventBase evt, Vector2 localPosition, int pointerId)
		{
			this.active = false;
			base.target.ReleasePointer(pointerId);
			bool flag = !(evt is IPointerEvent);
			if (flag)
			{
				base.target.panel.ProcessPointerCapture(PointerId.mousePointerId);
			}
			base.target.pseudoStates &= ~PseudoStates.Active;
			bool flag2 = this.IsRepeatable();
			if (flag2)
			{
				IVisualElementScheduledItem repeater = this.m_Repeater;
				if (repeater != null)
				{
					repeater.Pause();
				}
			}
			else
			{
				bool flag3 = base.target.ContainsPoint(localPosition);
				if (flag3)
				{
					this.Invoke(evt);
				}
			}
			evt.StopPropagation();
		}

		// Token: 0x0400000F RID: 15
		private readonly long m_Delay;

		// Token: 0x04000010 RID: 16
		private readonly long m_Interval;

		// Token: 0x04000013 RID: 19
		private IVisualElementScheduledItem m_Repeater;
	}
}
