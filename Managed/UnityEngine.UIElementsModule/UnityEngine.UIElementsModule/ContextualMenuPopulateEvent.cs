using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200016C RID: 364
	public class ContextualMenuPopulateEvent : MouseEventBase<ContextualMenuPopulateEvent>
	{
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x0002669C File Offset: 0x0002489C
		// (set) Token: 0x06000A18 RID: 2584 RVA: 0x000266A4 File Offset: 0x000248A4
		public DropdownMenu menu { get; private set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x000266AD File Offset: 0x000248AD
		// (set) Token: 0x06000A1A RID: 2586 RVA: 0x000266B5 File Offset: 0x000248B5
		public EventBase triggerEvent { get; private set; }

		// Token: 0x06000A1B RID: 2587 RVA: 0x000266C0 File Offset: 0x000248C0
		public static ContextualMenuPopulateEvent GetPooled(EventBase triggerEvent, DropdownMenu menu, IEventHandler target, ContextualMenuManager menuManager)
		{
			ContextualMenuPopulateEvent pooled = EventBase<ContextualMenuPopulateEvent>.GetPooled(triggerEvent);
			bool flag = triggerEvent != null;
			if (flag)
			{
				triggerEvent.Acquire();
				pooled.triggerEvent = triggerEvent;
				IMouseEvent mouseEvent = triggerEvent as IMouseEvent;
				bool flag2 = mouseEvent != null;
				if (flag2)
				{
					pooled.modifiers = mouseEvent.modifiers;
					pooled.mousePosition = mouseEvent.mousePosition;
					pooled.localMousePosition = mouseEvent.mousePosition;
					pooled.mouseDelta = mouseEvent.mouseDelta;
					pooled.button = mouseEvent.button;
					pooled.clickCount = mouseEvent.clickCount;
				}
				else
				{
					IPointerEvent pointerEvent = triggerEvent as IPointerEvent;
					bool flag3 = pointerEvent != null;
					if (flag3)
					{
						pooled.modifiers = pointerEvent.modifiers;
						pooled.mousePosition = pointerEvent.position;
						pooled.localMousePosition = pointerEvent.position;
						pooled.mouseDelta = pointerEvent.deltaPosition;
						pooled.button = pointerEvent.button;
						pooled.clickCount = pointerEvent.clickCount;
					}
				}
				IMouseEventInternal mouseEventInternal = triggerEvent as IMouseEventInternal;
				bool flag4 = mouseEventInternal != null;
				if (flag4)
				{
					((IMouseEventInternal)pooled).triggeredByOS = mouseEventInternal.triggeredByOS;
				}
				else
				{
					IPointerEventInternal pointerEventInternal = triggerEvent as IPointerEventInternal;
					bool flag5 = pointerEventInternal != null;
					if (flag5)
					{
						((IMouseEventInternal)pooled).triggeredByOS = pointerEventInternal.triggeredByOS;
					}
				}
			}
			pooled.target = target;
			pooled.menu = menu;
			pooled.m_ContextualMenuManager = menuManager;
			return pooled;
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00026833 File Offset: 0x00024A33
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00026844 File Offset: 0x00024A44
		private void LocalInit()
		{
			this.menu = null;
			this.m_ContextualMenuManager = null;
			bool flag = this.triggerEvent != null;
			if (flag)
			{
				this.triggerEvent.Dispose();
				this.triggerEvent = null;
			}
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00026884 File Offset: 0x00024A84
		public ContextualMenuPopulateEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00026898 File Offset: 0x00024A98
		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = !base.isDefaultPrevented && this.m_ContextualMenuManager != null;
			if (flag)
			{
				this.menu.PrepareForDisplay(this.triggerEvent);
				this.m_ContextualMenuManager.DoDisplayMenu(this.menu, this.triggerEvent);
			}
			base.PostDispatch(panel);
		}

		// Token: 0x04000442 RID: 1090
		private ContextualMenuManager m_ContextualMenuManager;
	}
}
