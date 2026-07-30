using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200014D RID: 333
	public class BlurEvent : FocusEventBase<BlurEvent>
	{
		// Token: 0x0600096F RID: 2415 RVA: 0x00024D78 File Offset: 0x00022F78
		protected internal override void PreDispatch(IPanel panel)
		{
			base.PreDispatch(panel);
			bool flag = base.relatedTarget == null;
			if (flag)
			{
				base.focusController.DoFocusChange(null);
			}
		}
	}
}
