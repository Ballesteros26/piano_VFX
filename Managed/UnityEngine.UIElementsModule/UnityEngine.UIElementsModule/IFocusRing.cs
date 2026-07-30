using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200001F RID: 31
	public interface IFocusRing
	{
		// Token: 0x0600009F RID: 159
		FocusChangeDirection GetFocusChangeDirection(Focusable currentFocusable, EventBase e);

		// Token: 0x060000A0 RID: 160
		Focusable GetNextFocusable(Focusable currentFocusable, FocusChangeDirection direction);
	}
}
