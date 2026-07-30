using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200036C RID: 876
	internal interface IDataControlButton : IButtonControl
	{
		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060020F8 RID: 8440
		// (set) Token: 0x060020F9 RID: 8441
		Control Container { get; set; }

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060020FA RID: 8442
		// (set) Token: 0x060020FB RID: 8443
		string ImageUrl { get; set; }

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060020FC RID: 8444
		// (set) Token: 0x060020FD RID: 8445
		bool AllowCallback { get; set; }

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060020FE RID: 8446
		ButtonType ButtonType { get; }
	}
}
