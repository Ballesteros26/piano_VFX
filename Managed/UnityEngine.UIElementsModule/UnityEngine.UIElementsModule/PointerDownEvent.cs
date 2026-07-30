using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200017C RID: 380
	public sealed class PointerDownEvent : PointerEventBase<PointerDownEvent>
	{
		// Token: 0x06000AA2 RID: 2722 RVA: 0x0002809F File Offset: 0x0002629F
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000280B0 File Offset: 0x000262B0
		private void LocalInit()
		{
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = true;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000280BB File Offset: 0x000262BB
		public PointerDownEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000280CC File Offset: 0x000262CC
		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = !base.isDefaultPrevented;
			if (flag)
			{
				bool flag2 = panel.ShouldSendCompatibilityMouseEvents(this);
				if (flag2)
				{
					using (MouseDownEvent pooled = MouseDownEvent.GetPooled(this))
					{
						pooled.target = base.target;
						pooled.target.SendEvent(pooled);
					}
				}
			}
			else
			{
				panel.PreventCompatibilityMouseEvents(base.pointerId);
			}
			base.PostDispatch(panel);
		}
	}
}
