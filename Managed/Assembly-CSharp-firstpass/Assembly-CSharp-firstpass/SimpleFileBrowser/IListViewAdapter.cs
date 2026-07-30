using System;

namespace SimpleFileBrowser
{
	// Token: 0x0200000C RID: 12
	public interface IListViewAdapter
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000099 RID: 153
		// (set) Token: 0x0600009A RID: 154
		OnItemClickedHandler OnItemClicked { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600009B RID: 155
		int Count { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600009C RID: 156
		float ItemHeight { get; }

		// Token: 0x0600009D RID: 157
		ListItem CreateItem();

		// Token: 0x0600009E RID: 158
		void SetItemContent(ListItem item);
	}
}
