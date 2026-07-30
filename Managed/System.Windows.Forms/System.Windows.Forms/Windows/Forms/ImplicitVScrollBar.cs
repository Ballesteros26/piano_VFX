using System;

namespace System.Windows.Forms
{
	// Token: 0x020001E1 RID: 481
	internal class ImplicitVScrollBar : VScrollBar
	{
		// Token: 0x06001E84 RID: 7812 RVA: 0x00072C78 File Offset: 0x00070E78
		public ImplicitVScrollBar()
		{
			this.implicit_control = true;
			base.SetStyle(ControlStyles.Selectable, false);
		}
	}
}
