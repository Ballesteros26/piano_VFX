using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000181 RID: 385
	public sealed class ClickEvent : PointerEventBase<ClickEvent>
	{
		// Token: 0x06000AB5 RID: 2741 RVA: 0x00028460 File Offset: 0x00026660
		internal static ClickEvent GetPooled(PointerUpEvent pointerEvent, int clickCount)
		{
			ClickEvent pooled = PointerEventBase<ClickEvent>.GetPooled(pointerEvent);
			pooled.clickCount = clickCount;
			return pooled;
		}
	}
}
