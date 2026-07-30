using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ListView.ItemMouseHover" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000236 RID: 566
	[ComVisible(true)]
	public class ListViewItemMouseHoverEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewItemMouseHoverEventArgs" /> class. </summary>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ListViewItem" /> the mouse pointer is currently hovering over.</param>
		// Token: 0x0600252C RID: 9516 RVA: 0x0008CA94 File Offset: 0x0008AC94
		public ListViewItemMouseHoverEventArgs(ListViewItem item)
		{
			this.item = item;
		}

		/// <summary>Gets the item the mouse pointer is currently hovering over.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> that the mouse pointer is currently hovering over.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x0600252D RID: 9517 RVA: 0x0008CAA4 File Offset: 0x0008ACA4
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x040012D4 RID: 4820
		private ListViewItem item;
	}
}
