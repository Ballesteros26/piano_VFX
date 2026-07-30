using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200017D RID: 381
	public sealed class PointerMoveEvent : PointerEventBase<PointerMoveEvent>
	{
		// Token: 0x06000AA6 RID: 2726 RVA: 0x00028150 File Offset: 0x00026350
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000280B0 File Offset: 0x000262B0
		private void LocalInit()
		{
			((IPointerEventInternal)this).recomputeTopElementUnderPointer = true;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00028161 File Offset: 0x00026361
		public PointerMoveEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00028174 File Offset: 0x00026374
		protected internal override void PostDispatch(IPanel panel)
		{
			bool flag = panel.ShouldSendCompatibilityMouseEvents(this);
			if (flag)
			{
				bool flag2 = base.imguiEvent != null && base.imguiEvent.rawType == EventType.MouseDown;
				if (flag2)
				{
					using (MouseDownEvent pooled = MouseDownEvent.GetPooled(this))
					{
						pooled.target = base.target;
						pooled.target.SendEvent(pooled);
					}
				}
				else
				{
					bool flag3 = base.imguiEvent != null && base.imguiEvent.rawType == EventType.MouseUp;
					if (flag3)
					{
						using (MouseUpEvent pooled2 = MouseUpEvent.GetPooled(this))
						{
							pooled2.target = base.target;
							pooled2.target.SendEvent(pooled2);
						}
					}
					else
					{
						using (MouseMoveEvent pooled3 = MouseMoveEvent.GetPooled(this))
						{
							pooled3.target = base.target;
							pooled3.target.SendEvent(pooled3);
						}
					}
				}
			}
			base.PostDispatch(panel);
		}
	}
}
