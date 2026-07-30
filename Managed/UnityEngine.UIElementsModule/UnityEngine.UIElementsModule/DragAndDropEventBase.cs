using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000133 RID: 307
	public abstract class DragAndDropEventBase<T> : MouseEventBase<T>, IDragAndDropEvent where T : DragAndDropEventBase<T>, new()
	{
	}
}
