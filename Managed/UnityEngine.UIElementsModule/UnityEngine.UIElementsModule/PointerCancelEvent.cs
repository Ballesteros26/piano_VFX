using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000180 RID: 384
	public sealed class PointerCancelEvent : PointerEventBase<PointerCancelEvent>
	{
		// Token: 0x06000AB1 RID: 2737 RVA: 0x00028390 File Offset: 0x00026590
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x000283A1 File Offset: 0x000265A1
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown;
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = true;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x000283B4 File Offset: 0x000265B4
		public PointerCancelEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000283C8 File Offset: 0x000265C8
		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = PointerType.IsDirectManipulationDevice(base.pointerType);
			if (flag)
			{
				panel.ReleasePointer(base.pointerId);
				BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
				if (baseVisualElementPanel != null)
				{
					baseVisualElementPanel.SetElementUnderPointer(null, this);
				}
			}
			bool flag2 = panel.ShouldSendCompatibilityMouseEvents(this);
			if (flag2)
			{
				using (MouseUpEvent pooled = MouseUpEvent.GetPooled(this))
				{
					pooled.target = base.target;
					base.target.SendEvent(pooled);
				}
			}
			base.PostDispatch(panel);
		}
	}
}
