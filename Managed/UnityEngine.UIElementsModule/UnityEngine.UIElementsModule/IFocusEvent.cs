using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200014A RID: 330
	public interface IFocusEvent
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000960 RID: 2400
		Focusable relatedTarget { get; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000961 RID: 2401
		FocusChangeDirection direction { get; }
	}
}
