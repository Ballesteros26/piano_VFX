using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000169 RID: 361
	public class MouseLeaveWindowEvent : MouseEventBase<MouseLeaveWindowEvent>
	{
		// Token: 0x06000A10 RID: 2576 RVA: 0x000265EA File Offset: 0x000247EA
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x000265FB File Offset: 0x000247FB
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Cancellable;
			((IMouseEventInternal)this).recomputeTopElementUnderMouse = false;
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002660E File Offset: 0x0002480E
		public MouseLeaveWindowEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00026620 File Offset: 0x00024820
		public new static MouseLeaveWindowEvent GetPooled(Event systemEvent)
		{
			bool flag = systemEvent != null;
			if (flag)
			{
				PointerDeviceState.ReleaseAllButtons(PointerId.mousePointerId);
			}
			return MouseEventBase<MouseLeaveWindowEvent>.GetPooled(systemEvent);
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002664C File Offset: 0x0002484C
		protected internal override void PostDispatch(IPanel panel)
		{
			EventBase eventBase = ((IMouseEventInternal)this).sourcePointerEvent as EventBase;
			bool flag = eventBase == null;
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
	}
}
