using System;

namespace System.Windows.Forms
{
	/// <summary>Contains information about an area of a <see cref="T:System.Windows.Forms.ListView" /> control or a <see cref="T:System.Windows.Forms.ListViewItem" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200022D RID: 557
	public class ListViewHitTestInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ListViewHitTestInfo" /> class. </summary>
		/// <param name="hitItem">The <see cref="T:System.Windows.Forms.ListViewItem" /> located at the position indicated by the hit test.</param>
		/// <param name="hitSubItem">The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> located at the position indicated by the hit test.</param>
		/// <param name="hitLocation">One of the <see cref="T:System.Windows.Forms.ListViewHitTestLocations" /> values.</param>
		// Token: 0x0600247A RID: 9338 RVA: 0x00089978 File Offset: 0x00087B78
		public ListViewHitTestInfo(ListViewItem hitItem, ListViewItem.ListViewSubItem hitSubItem, ListViewHitTestLocations hitLocation)
		{
			this.item = hitItem;
			this.subItem = hitSubItem;
			this.location = hitLocation;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ListViewItem" /> at the position indicated by a hit test on a <see cref="T:System.Windows.Forms.ListView" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem" /> at the position indicated by a hit test on a <see cref="T:System.Windows.Forms.ListView" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x0600247B RID: 9339 RVA: 0x000899A8 File Offset: 0x00087BA8
		public ListViewItem Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>Gets the location of a hit test on a <see cref="T:System.Windows.Forms.ListView" /> control, in relation to the <see cref="T:System.Windows.Forms.ListView" /> and the items it contains.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ListViewHitTestLocations" /> values. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x0600247C RID: 9340 RVA: 0x000899B0 File Offset: 0x00087BB0
		public ListViewHitTestLocations Location
		{
			get
			{
				return this.location;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> at the position indicated by a hit test on a <see cref="T:System.Windows.Forms.ListView" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ListViewItem.ListViewSubItem" /> at the position indicated by a hit test on a <see cref="T:System.Windows.Forms.ListView" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x0600247D RID: 9341 RVA: 0x000899B8 File Offset: 0x00087BB8
		public ListViewItem.ListViewSubItem SubItem
		{
			get
			{
				return this.subItem;
			}
		}

		// Token: 0x0400129B RID: 4763
		private ListViewItem item;

		// Token: 0x0400129C RID: 4764
		private ListViewItem.ListViewSubItem subItem;

		// Token: 0x0400129D RID: 4765
		private ListViewHitTestLocations location = ListViewHitTestLocations.None;
	}
}
