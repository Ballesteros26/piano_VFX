using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x020000BE RID: 190
	internal class DataGridDataSource
	{
		// Token: 0x06000B9D RID: 2973 RVA: 0x0002FBF0 File Offset: 0x0002DDF0
		public DataGridDataSource(DataGrid owner, CurrencyManager list_manager, object data_source, string data_member, object view_data, DataGridCell current)
		{
			this.owner = owner;
			this.list_manager = list_manager;
			this.view = view_data;
			this.data_source = data_source;
			this.data_member = data_member;
			this.current = current;
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0002FC28 File Offset: 0x0002DE28
		// (set) Token: 0x06000B9F RID: 2975 RVA: 0x0002FC30 File Offset: 0x0002DE30
		public DataGridRelationshipRow[] Rows
		{
			get
			{
				return this.rows;
			}
			set
			{
				this.rows = value;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0002FC3C File Offset: 0x0002DE3C
		// (set) Token: 0x06000BA1 RID: 2977 RVA: 0x0002FC44 File Offset: 0x0002DE44
		public Hashtable SelectedRows
		{
			get
			{
				return this.selected_rows;
			}
			set
			{
				this.selected_rows = value;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0002FC50 File Offset: 0x0002DE50
		// (set) Token: 0x06000BA3 RID: 2979 RVA: 0x0002FC58 File Offset: 0x0002DE58
		public int SelectionStart
		{
			get
			{
				return this.selection_start;
			}
			set
			{
				this.selection_start = value;
			}
		}

		// Token: 0x040008D4 RID: 2260
		public DataGrid owner;

		// Token: 0x040008D5 RID: 2261
		public CurrencyManager list_manager;

		// Token: 0x040008D6 RID: 2262
		public object view;

		// Token: 0x040008D7 RID: 2263
		public string data_member;

		// Token: 0x040008D8 RID: 2264
		public object data_source;

		// Token: 0x040008D9 RID: 2265
		public DataGridCell current;

		// Token: 0x040008DA RID: 2266
		private DataGridRelationshipRow[] rows;

		// Token: 0x040008DB RID: 2267
		private Hashtable selected_rows;

		// Token: 0x040008DC RID: 2268
		private int selection_start;
	}
}
