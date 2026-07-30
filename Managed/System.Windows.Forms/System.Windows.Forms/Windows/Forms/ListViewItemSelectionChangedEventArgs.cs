using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ItemSelectionChanged" /> event. </summary>
	// Token: 0x02000237 RID: 567
	public class ListViewItemSelectionChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItemSelectionChangedEventArgs" /> class. </summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> whose selection state has changed.</param>
		/// <param name="itemIndex">The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> whose selection state has changed.</param>
		/// <param name="isSelected">true to indicate the item's state has changed to selected; false to indicate the item's state has changed to deselected.</param>
		// Token: 0x0600252E RID: 9518 RVA: 0x0008CAAC File Offset: 0x0008ACAC
		public ListViewItemSelectionChangedEventArgs(ListViewItem item, int itemIndex, bool isSelected)
		{
			this.item = item;
			this.item_index = itemIndex;
			this.is_selected = isSelected;
		}

		/// <summary>Gets the item whose selection state has changed.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> whose selection state has changed.</returns>
		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x0600252F RID: 9519 RVA: 0x0008CACC File Offset: 0x0008ACCC
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>Gets a value indicating whether the item's state has changed to selected. </summary>
		/// <returns>true if the item's state has changed to selected; false if the item's state has changed to deselected.</returns>
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002530 RID: 9520 RVA: 0x0008CAD4 File Offset: 0x0008ACD4
		public bool IsSelected
		{
			get
			{
				return this.is_selected;
			}
		}

		/// <summary>Gets the index of the item whose selection state has changed.</summary>
		/// <returns>The index of the <see cref="T:System.Windows.Forms.ListViewItem" /> whose selection state has changed.</returns>
		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002531 RID: 9521 RVA: 0x0008CADC File Offset: 0x0008ACDC
		public int ItemIndex
		{
			get
			{
				return this.item_index;
			}
		}

		// Token: 0x040012D5 RID: 4821
		private bool is_selected;

		// Token: 0x040012D6 RID: 4822
		private ListViewItem item;

		// Token: 0x040012D7 RID: 4823
		private int item_index;
	}
}
