using System;

namespace System.Windows.Forms
{
	// Token: 0x02000187 RID: 391
	internal class FixedSizeTextBox : TextBox
	{
		// Token: 0x06001946 RID: 6470 RVA: 0x00060644 File Offset: 0x0005E844
		public FixedSizeTextBox()
		{
			base.SetStyle(ControlStyles.FixedWidth, true);
			base.SetStyle(ControlStyles.FixedHeight, true);
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00060660 File Offset: 0x0005E860
		public FixedSizeTextBox(bool fixed_horz, bool fixed_vert)
		{
			base.SetStyle(ControlStyles.FixedWidth, fixed_horz);
			base.SetStyle(ControlStyles.FixedHeight, fixed_vert);
		}
	}
}
