using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000134 RID: 308
	public class DragExitedEvent : DragAndDropEventBase<DragExitedEvent>
	{
		// Token: 0x060008B5 RID: 2229 RVA: 0x00022DF7 File Offset: 0x00020FF7
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00022E08 File Offset: 0x00021008
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown;
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00022E13 File Offset: 0x00021013
		public DragExitedEvent()
		{
			this.LocalInit();
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x00022E24 File Offset: 0x00021024
		public new static DragExitedEvent GetPooled(Event systemEvent)
		{
			bool flag = systemEvent != null;
			if (flag)
			{
				PointerDeviceState.ReleaseButton(PointerId.mousePointerId, systemEvent.button);
			}
			return MouseEventBase<DragExitedEvent>.GetPooled(systemEvent);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00022E58 File Offset: 0x00021058
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
