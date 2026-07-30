using System;
using System.Collections;

namespace System.Web.UI.WebControls
{
	// Token: 0x020002D9 RID: 729
	internal interface IWizardSideBarListControl
	{
		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x06001B80 RID: 7040
		// (set) Token: 0x06001B81 RID: 7041
		object DataSource { get; set; }

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001B82 RID: 7042
		IEnumerable Items { get; }

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001B83 RID: 7043
		// (set) Token: 0x06001B84 RID: 7044
		ITemplate ItemTemplate { get; set; }

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001B85 RID: 7045
		// (set) Token: 0x06001B86 RID: 7046
		int SelectedIndex { get; set; }

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06001B87 RID: 7047
		// (remove) Token: 0x06001B88 RID: 7048
		event CommandEventHandler ItemCommand;

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06001B89 RID: 7049
		// (remove) Token: 0x06001B8A RID: 7050
		event EventHandler<WizardSideBarListControlItemEventArgs> ItemDataBound;

		// Token: 0x06001B8B RID: 7051
		void DataBind();
	}
}
