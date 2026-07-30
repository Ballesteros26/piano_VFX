using System;
using System.Drawing;

namespace System.Windows.Forms.Design
{
	// Token: 0x02000029 RID: 41
	internal interface IUISelectionService
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000166 RID: 358
		bool SelectionInProgress { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000167 RID: 359
		bool DragDropInProgress { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000168 RID: 360
		bool ResizeInProgress { get; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000169 RID: 361
		Rectangle SelectionBounds { get; }

		// Token: 0x0600016A RID: 362
		void MouseDragBegin(Control container, int x, int y);

		// Token: 0x0600016B RID: 363
		void MouseDragMove(int x, int y);

		// Token: 0x0600016C RID: 364
		void MouseDragEnd(bool cancel);

		// Token: 0x0600016D RID: 365
		void DragBegin();

		// Token: 0x0600016E RID: 366
		void DragOver(Control container, int x, int y);

		// Token: 0x0600016F RID: 367
		void DragDrop(bool cancel, Control container, int x, int y);

		// Token: 0x06000170 RID: 368
		void PaintAdornments(Control container, Graphics gfx);

		// Token: 0x06000171 RID: 369
		bool SetCursor(int x, int y);

		// Token: 0x06000172 RID: 370
		bool AdornmentsHitTest(Control control, int x, int y);
	}
}
