using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000111 RID: 273
	internal interface IDragAndDrop
	{
		// Token: 0x06000841 RID: 2113
		void StartDrag(StartDragArgs args);

		// Token: 0x06000842 RID: 2114
		void AcceptDrag();

		// Token: 0x06000843 RID: 2115
		void SetVisualMode(DragVisualMode visualMode);

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000844 RID: 2116
		IDragAndDropData data { get; }
	}
}
