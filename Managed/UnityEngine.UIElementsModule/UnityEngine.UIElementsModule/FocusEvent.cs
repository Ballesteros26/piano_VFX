using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200014F RID: 335
	public class FocusEvent : FocusEventBase<FocusEvent>
	{
		// Token: 0x06000974 RID: 2420 RVA: 0x00024DD5 File Offset: 0x00022FD5
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			base.focusController.DoFocusChange(base.target as Focusable);
		}
	}
}
