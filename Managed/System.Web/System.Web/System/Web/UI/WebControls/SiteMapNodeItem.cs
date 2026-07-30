using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>The <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> class is used by the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control to functionally represent a <see cref="T:System.Web.SiteMapNode" />.</summary>
	// Token: 0x0200040B RID: 1035
	[ToolboxItem(false)]
	public class SiteMapNodeItem : WebControl, IDataItemContainer, INamingContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> class, using the specified index and <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemType" />.</summary>
		/// <param name="itemIndex">The index in the <see cref="P:System.Web.UI.Control.Controls" /> collection that the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control uses to track the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> object. </param>
		/// <param name="itemType">The functional type of <see cref="T:System.Web.SiteMapNode" /> that this <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> represents. </param>
		// Token: 0x06002DE0 RID: 11744 RVA: 0x0007950D File Offset: 0x0007770D
		public SiteMapNodeItem(int itemIndex, SiteMapNodeItemType itemType)
		{
			this.itemIndex = itemIndex;
			this.SetItemType(itemType);
		}

		/// <summary>Sets the current <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /><see cref="P:System.Web.UI.WebControls.SiteMapNodeItem.ItemType" /> property.</summary>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemType" /> values. </param>
		// Token: 0x06002DE1 RID: 11745 RVA: 0x00079523 File Offset: 0x00077723
		protected internal virtual void SetItemType(SiteMapNodeItemType itemType)
		{
			this.itemType = itemType;
		}

		/// <summary>Retrieves the index that the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control uses to track and manage the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> in its internal collections.</summary>
		/// <returns>An integer that represents the location of the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> in the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control's internal collections.</returns>
		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x06002DE2 RID: 11746 RVA: 0x0007952C File Offset: 0x0007772C
		public virtual int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		/// <summary>Retrieves the functional type of the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" />.</summary>
		/// <returns>A member of the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItemType" /> enumeration that indicates the functional role of the node item in the navigation path hierarchy.</returns>
		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x00079534 File Offset: 0x00077734
		public virtual SiteMapNodeItemType ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.SiteMapNode" /> object that the <see cref="T:System.Web.UI.WebControls.SiteMapNodeItem" /> represents.</summary>
		/// <returns>A <see cref="T:System.Web.SiteMapNode" /> object that the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control uses to display a site navigation user interface.</returns>
		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x0007953C File Offset: 0x0007773C
		// (set) Token: 0x06002DE5 RID: 11749 RVA: 0x00079544 File Offset: 0x00077744
		public virtual SiteMapNode SiteMapNode
		{
			get
			{
				return this.node;
			}
			set
			{
				this.node = value;
			}
		}

		/// <summary>Gets an object that is used in simplified data-binding operations.</summary>
		/// <returns>An object that represents the value to use when data-binding operations are performed.</returns>
		// Token: 0x17000E9D RID: 3741
		// (get) Token: 0x06002DE6 RID: 11750 RVA: 0x0007953C File Offset: 0x0007773C
		object IDataItemContainer.DataItem
		{
			get
			{
				return this.node;
			}
		}

		/// <summary>Gets the index of the data item that is bound to the control.</summary>
		/// <returns>An integer that represents the location of the data item in the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control's internal collections.</returns>
		// Token: 0x17000E9E RID: 3742
		// (get) Token: 0x06002DE7 RID: 11751 RVA: 0x0007952C File Offset: 0x0007772C
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		/// <summary>Gets the position of the data item as displayed in the control.</summary>
		/// <returns>An integer that represents the location of the data item in the <see cref="T:System.Web.UI.WebControls.SiteMapPath" /> control's internal collections.</returns>
		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x06002DE8 RID: 11752 RVA: 0x0007952C File Offset: 0x0007772C
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		// Token: 0x04001B89 RID: 7049
		private int itemIndex;

		// Token: 0x04001B8A RID: 7050
		private SiteMapNodeItemType itemType;

		// Token: 0x04001B8B RID: 7051
		private SiteMapNode node;
	}
}
