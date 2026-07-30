using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000046 RID: 70
	internal class PointerClickable : Clickable
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x0000743A File Offset: 0x0000563A
		public PointerClickable(Action handler)
			: base(handler)
		{
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00007445 File Offset: 0x00005645
		public PointerClickable(Action<EventBase> handler)
			: base(handler)
		{
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00007450 File Offset: 0x00005650
		public PointerClickable(Action handler, long delay, long interval)
			: base(handler, delay, interval)
		{
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00007460 File Offset: 0x00005660
		public Vector2 lastPointerPosition
		{
			get
			{
				return base.lastMousePosition;
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00007478 File Offset: 0x00005678
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			base.RegisterCallbacksOnTarget();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000074D8 File Offset: 0x000056D8
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
			base.UnregisterCallbacksFromTarget();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00007538 File Offset: 0x00005738
		protected void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = !base.CanStartManipulation(evt);
			if (!flag)
			{
				bool flag2 = evt.pointerId != PointerId.mousePointerId;
				if (flag2)
				{
					this.ProcessDownEvent(evt, evt.localPosition, evt.pointerId);
					evt.PreventDefault();
				}
				else
				{
					evt.StopImmediatePropagation();
				}
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00007598 File Offset: 0x00005798
		protected void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = evt.pointerId != PointerId.mousePointerId && base.active;
			if (flag)
			{
				this.ProcessMoveEvent(evt, evt.localPosition);
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000075D8 File Offset: 0x000057D8
		protected void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = evt.pointerId != PointerId.mousePointerId && base.active && base.CanStopManipulation(evt);
			if (flag)
			{
				this.ProcessUpEvent(evt, evt.localPosition, evt.pointerId);
			}
		}
	}
}
