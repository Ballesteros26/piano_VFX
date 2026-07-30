using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000168 RID: 360
	public class MouseEnterWindowEvent : MouseEventBase<MouseEnterWindowEvent>
	{
		// Token: 0x06000A0C RID: 2572 RVA: 0x0002657E File Offset: 0x0002477E
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0002658F File Offset: 0x0002478F
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Cancellable;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0002659A File Offset: 0x0002479A
		public MouseEnterWindowEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x000265AC File Offset: 0x000247AC
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
