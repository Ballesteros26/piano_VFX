using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200032A RID: 810
	internal sealed class WizardSideBarListControlItem
	{
		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x00046784 File Offset: 0x00044984
		// (set) Token: 0x06001C24 RID: 7204 RVA: 0x0004678C File Offset: 0x0004498C
		public object DataItem { get; private set; }

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x00046795 File Offset: 0x00044995
		// (set) Token: 0x06001C26 RID: 7206 RVA: 0x0004679D File Offset: 0x0004499D
		public ListItemType ItemType { get; private set; }

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06001C27 RID: 7207 RVA: 0x000467A6 File Offset: 0x000449A6
		// (set) Token: 0x06001C28 RID: 7208 RVA: 0x000467AE File Offset: 0x000449AE
		public int ItemIndex { get; private set; }

		// Token: 0x06001C29 RID: 7209 RVA: 0x000467B7 File Offset: 0x000449B7
		public WizardSideBarListControlItem(object dataItem, ListItemType itemType, int itemIndex, Control container)
		{
			this.DataItem = dataItem;
			this.ItemType = itemType;
			this.ItemIndex = itemIndex;
			this._container = container;
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x000467DC File Offset: 0x000449DC
		internal Control FindControl(string id)
		{
			return this._container.FindControl(id);
		}

		// Token: 0x040017D9 RID: 6105
		private Control _container;
	}
}
