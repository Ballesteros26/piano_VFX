using System;
using System.Collections;

namespace System.Windows.Forms
{
	// Token: 0x02000170 RID: 368
	internal class MwfFileViewItemComparer : IComparer
	{
		// Token: 0x060018A5 RID: 6309 RVA: 0x0005CCE4 File Offset: 0x0005AEE4
		public MwfFileViewItemComparer(bool asc)
		{
			this.asc = asc;
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x0005CCF4 File Offset: 0x0005AEF4
		// (set) Token: 0x060018A7 RID: 6311 RVA: 0x0005CCFC File Offset: 0x0005AEFC
		public int ColumnIndex
		{
			get
			{
				return this.column_index;
			}
			set
			{
				this.column_index = value;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x0005CD08 File Offset: 0x0005AF08
		// (set) Token: 0x060018A9 RID: 6313 RVA: 0x0005CD10 File Offset: 0x0005AF10
		public bool Ascendent
		{
			get
			{
				return this.asc;
			}
			set
			{
				this.asc = value;
			}
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0005CD1C File Offset: 0x0005AF1C
		public int Compare(object a, object b)
		{
			ListViewItem listViewItem = (ListViewItem)a;
			ListViewItem listViewItem2 = (ListViewItem)b;
			int num;
			if (this.asc)
			{
				num = string.Compare(listViewItem.SubItems[this.column_index].Text, listViewItem2.SubItems[this.column_index].Text);
			}
			else
			{
				num = string.Compare(listViewItem2.SubItems[this.column_index].Text, listViewItem.SubItems[this.column_index].Text);
			}
			return num;
		}

		// Token: 0x04000DC2 RID: 3522
		private int column_index;

		// Token: 0x04000DC3 RID: 3523
		private bool asc;
	}
}
