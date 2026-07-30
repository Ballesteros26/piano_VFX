using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents an item (row) in a <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	// Token: 0x0200037B RID: 891
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DataGridItem : TableRow, INamingContainer, IDataItemContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> class.</summary>
		/// <param name="itemIndex">The index of the item from the <see cref="P:System.Web.UI.WebControls.DataGrid.Items" /> collection in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. </param>
		/// <param name="dataSetIndex">The index number of the item, from the bound data source, that appears in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control. </param>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
		// Token: 0x06002211 RID: 8721 RVA: 0x00057A77 File Offset: 0x00055C77
		public DataGridItem(int itemIndex, int dataSetIndex, ListItemType itemType)
		{
			this.item_index = itemIndex;
			this.dataset_index = dataSetIndex;
			this.item_type = itemType;
		}

		/// <summary>Gets or sets the data item represented by the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>A <see cref="T:System.Object" /> that represents a data item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x00057A94 File Offset: 0x00055C94
		// (set) Token: 0x06002213 RID: 8723 RVA: 0x00057A9C File Offset: 0x00055C9C
		public virtual object DataItem
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

		/// <summary>Gets the index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object from the bound data source.</summary>
		/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> from the bound data source.</returns>
		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06002214 RID: 8724 RVA: 0x00057AA5 File Offset: 0x00055CA5
		public virtual int DataSetIndex
		{
			get
			{
				return this.dataset_index;
			}
		}

		/// <summary>Gets the index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object from the <see cref="P:System.Web.UI.WebControls.DataGrid.Items" /> collection of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> from the <see cref="P:System.Web.UI.WebControls.DataGrid.Items" /> collection of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06002215 RID: 8725 RVA: 0x00057AAD File Offset: 0x00055CAD
		public virtual int ItemIndex
		{
			get
			{
				return this.item_index;
			}
		}

		/// <summary>Gets the type of the item represented by the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> object in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values.</returns>
		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x00057AB5 File Offset: 0x00055CB5
		public virtual ListItemType ItemType
		{
			get
			{
				return this.item_type;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItem" />.</summary>
		/// <returns>An object that represents a data item in the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06002217 RID: 8727 RVA: 0x00057A94 File Offset: 0x00055C94
		object IDataItemContainer.DataItem
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItemIndex" />.</summary>
		/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> from the bound data source.</returns>
		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06002218 RID: 8728 RVA: 0x00057AAD File Offset: 0x00055CAD
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.item_index;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DisplayIndex" />.</summary>
		/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> from the <see cref="P:System.Web.UI.WebControls.DataGrid.Items" /> collection of the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</returns>
		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x06002219 RID: 8729 RVA: 0x00057AAD File Offset: 0x00055CAD
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.item_index;
			}
		}

		/// <summary>Used internally by the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> class.</summary>
		/// <returns>true if the event has been canceled; otherwise, false.</returns>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x0600221A RID: 8730 RVA: 0x00057ABD File Offset: 0x00055CBD
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (e is CommandEventArgs)
			{
				base.RaiseBubbleEvent(this, new DataGridCommandEventArgs(this, source, (CommandEventArgs)e));
				return true;
			}
			return base.OnBubbleEvent(source, e);
		}

		/// <summary>Used internally by the <see cref="T:System.Web.UI.WebControls.DataGridItem" /> class.</summary>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values.</param>
		// Token: 0x0600221B RID: 8731 RVA: 0x00057AE5 File Offset: 0x00055CE5
		protected internal virtual void SetItemType(ListItemType itemType)
		{
			this.item_type = itemType;
		}

		// Token: 0x04001903 RID: 6403
		private object item;

		// Token: 0x04001904 RID: 6404
		private int dataset_index;

		// Token: 0x04001905 RID: 6405
		private int item_index;

		// Token: 0x04001906 RID: 6406
		private ListItemType item_type;
	}
}
