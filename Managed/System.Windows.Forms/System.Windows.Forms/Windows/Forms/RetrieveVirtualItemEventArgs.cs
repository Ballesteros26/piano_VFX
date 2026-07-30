using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.RetrieveVirtualItem" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002B6 RID: 694
	public class RetrieveVirtualItemEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.RetrieveVirtualItemEventArgs" /> class. </summary>
		/// <param name="itemIndex">The index of the item to retrieve.</param>
		// Token: 0x06002E1B RID: 11803 RVA: 0x000B1C8C File Offset: 0x000AFE8C
		public RetrieveVirtualItemEventArgs(int itemIndex)
		{
			this.item_index = itemIndex;
		}

		/// <summary>Gets or sets the item retrieved from the cache.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> retrieved from the cache.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06002E1C RID: 11804 RVA: 0x000B1C9C File Offset: 0x000AFE9C
		// (set) Token: 0x06002E1D RID: 11805 RVA: 0x000B1CA4 File Offset: 0x000AFEA4
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
			set
			{
				this.item = value;
			}
		}

		/// <summary>Gets the index of the item to retrieve from the cache.</summary>
		/// <returns>The index of the item to retrieve from the cache.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06002E1E RID: 11806 RVA: 0x000B1CB0 File Offset: 0x000AFEB0
		public int ItemIndex
		{
			get
			{
				return this.item_index;
			}
		}

		// Token: 0x04001621 RID: 5665
		private ListViewItem item;

		// Token: 0x04001622 RID: 5666
		private int item_index;
	}
}
