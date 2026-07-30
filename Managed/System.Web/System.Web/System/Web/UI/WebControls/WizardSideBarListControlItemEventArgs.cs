using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200032B RID: 811
	internal sealed class WizardSideBarListControlItemEventArgs : EventArgs
	{
		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x000467EA File Offset: 0x000449EA
		// (set) Token: 0x06001C2C RID: 7212 RVA: 0x000467F2 File Offset: 0x000449F2
		public WizardSideBarListControlItem Item { get; private set; }

		// Token: 0x06001C2D RID: 7213 RVA: 0x000467FB File Offset: 0x000449FB
		public WizardSideBarListControlItemEventArgs(WizardSideBarListControlItem item)
		{
			this.Item = item;
		}
	}
}
