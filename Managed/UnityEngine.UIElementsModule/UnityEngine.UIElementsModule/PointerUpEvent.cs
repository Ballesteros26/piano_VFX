using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200017F RID: 383
	public sealed class PointerUpEvent : PointerEventBase<PointerUpEvent>
	{
		// Token: 0x06000AAD RID: 2733 RVA: 0x000282CA File Offset: 0x000264CA
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x000280B0 File Offset: 0x000262B0
		private void LocalInit()
		{
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = true;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000282DB File Offset: 0x000264DB
		public PointerUpEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000282EC File Offset: 0x000264EC
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
					pooled.target.SendEvent(pooled);
				}
			}
			panel.ActivateCompatibilityMouseEvents(base.pointerId);
			base.PostDispatch(panel);
		}
	}
}
