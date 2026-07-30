using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000028 RID: 40
	internal interface ISelectionUIHandler
	{
		// Token: 0x06000159 RID: 345
		bool BeginDrag(object[] components, SelectionRules rules, int initialX, int initialY);

		// Token: 0x0600015A RID: 346
		void DragMoved(object[] components, Rectangle offset);

		// Token: 0x0600015B RID: 347
		void EndDrag(object[] components, bool cancel);

		// Token: 0x0600015C RID: 348
		Rectangle GetComponentBounds(object component);

		// Token: 0x0600015D RID: 349
		SelectionRules GetComponentRules(object component);

		// Token: 0x0600015E RID: 350
		Rectangle GetSelectionClipRect(object component);

		// Token: 0x0600015F RID: 351
		void OleDragDrop(DragEventArgs de);

		// Token: 0x06000160 RID: 352
		void OleDragEnter(DragEventArgs de);

		// Token: 0x06000161 RID: 353
		void OleDragLeave();

		// Token: 0x06000162 RID: 354
		void OleDragOver(DragEventArgs de);

		// Token: 0x06000163 RID: 355
		void OnSelectionDoubleClick(IComponent component);

		// Token: 0x06000164 RID: 356
		bool QueryBeginDrag(object[] components, SelectionRules rules, int initialX, int initialY);

		// Token: 0x06000165 RID: 357
		void ShowContextMenu(IComponent component);
	}
}
