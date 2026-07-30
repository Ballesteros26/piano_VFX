using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ItemChecked" /> event of the <see cref="T:System.Windows.Forms.ListView" /> control. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001F1 RID: 497
	public class ItemCheckedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ItemCheckedEventArgs" /> class. </summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> that is being checked or unchecked.</param>
		// Token: 0x06001EFD RID: 7933 RVA: 0x00074EC0 File Offset: 0x000730C0
		public ItemCheckedEventArgs(ListViewItem item)
		{
			this.item = item;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ListViewItem" /> whose checked state is changing.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> whose checked state is changing.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x00074ED0 File Offset: 0x000730D0
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x04001049 RID: 4169
		private ListViewItem item;
	}
}
