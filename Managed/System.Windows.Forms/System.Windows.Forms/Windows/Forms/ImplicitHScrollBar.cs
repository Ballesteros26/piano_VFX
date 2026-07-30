using System;

namespace System.Windows.Forms
{
	// Token: 0x020001E0 RID: 480
	internal class ImplicitHScrollBar : HScrollBar
	{
		// Token: 0x06001E83 RID: 7811 RVA: 0x00072C5C File Offset: 0x00070E5C
		public ImplicitHScrollBar()
		{
			this.implicit_control = true;
			base.SetStyle(ControlStyles.Selectable, false);
		}
	}
}
