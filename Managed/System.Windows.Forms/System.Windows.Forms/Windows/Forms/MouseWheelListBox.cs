using System;

namespace System.Windows.Forms
{
	// Token: 0x02000195 RID: 405
	internal class MouseWheelListBox : ListBox
	{
		// Token: 0x060019E1 RID: 6625 RVA: 0x00064BE4 File Offset: 0x00062DE4
		public void SendMouseWheelEvent(MouseEventArgs e)
		{
			this.OnMouseWheel(e);
		}
	}
}
