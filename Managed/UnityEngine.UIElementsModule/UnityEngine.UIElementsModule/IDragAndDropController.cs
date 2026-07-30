using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000113 RID: 275
	internal interface IDragAndDropController<TItem, in TArgs>
	{
		// Token: 0x06000848 RID: 2120
		bool CanStartDrag(IEnumerable<TItem> items);

		// Token: 0x06000849 RID: 2121
		StartDragArgs SetupDragAndDrop(IEnumerable<TItem> items);

		// Token: 0x0600084A RID: 2122
		DragVisualMode HandleDragAndDrop(TArgs args);

		// Token: 0x0600084B RID: 2123
		void OnDrop(TArgs args);
	}
}
