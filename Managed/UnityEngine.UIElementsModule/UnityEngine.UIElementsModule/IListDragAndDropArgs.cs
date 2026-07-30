using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000116 RID: 278
	internal interface IListDragAndDropArgs
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000855 RID: 2133
		object target { get; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000856 RID: 2134
		int insertAtIndex { get; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000857 RID: 2135
		IDragAndDropData dragAndDropData { get; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000858 RID: 2136
		DragAndDropPosition dragAndDropPosition { get; }
	}
}
