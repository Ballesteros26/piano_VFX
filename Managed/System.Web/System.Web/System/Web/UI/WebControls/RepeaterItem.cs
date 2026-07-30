using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents an item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
	// Token: 0x020003FF RID: 1023
	[ToolboxItem("")]
	public class RepeaterItem : Control, INamingContainer, IDataItemContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> class.</summary>
		/// <param name="itemIndex">The index of the item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control from the <see cref="P:System.Web.UI.WebControls.Repeater.Items" /> collection of the control. </param>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
		// Token: 0x06002D73 RID: 11635 RVA: 0x00078A73 File Offset: 0x00076C73
		public RepeaterItem(int itemIndex, ListItemType itemType)
		{
			this.idx = itemIndex;
			this.type = itemType;
		}

		/// <summary>Assigns any sources of the event and its information to the parent <see cref="T:System.Web.UI.WebControls.Repeater" /> control, if the <see cref="T:System.EventArgs" /> parameter is an instance of <see cref="T:System.Web.UI.WebControls.RepeaterCommandEventArgs" />.</summary>
		/// <returns>true if the event assigned to the parent was raised, otherwise false.</returns>
		/// <param name="source">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002D74 RID: 11636 RVA: 0x00078A8C File Offset: 0x00076C8C
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			CommandEventArgs commandEventArgs = e as CommandEventArgs;
			if (commandEventArgs != null)
			{
				base.RaiseBubbleEvent(this, new RepeaterCommandEventArgs(this, source, commandEventArgs));
				return true;
			}
			return false;
		}

		/// <summary>Gets or sets a data item associated with the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> object in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
		/// <returns>A <see cref="T:System.Object" /> that represents a data item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</returns>
		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06002D75 RID: 11637 RVA: 0x00078AB5 File Offset: 0x00076CB5
		// (set) Token: 0x06002D76 RID: 11638 RVA: 0x00078ABD File Offset: 0x00076CBD
		public virtual object DataItem
		{
			get
			{
				return this.data_item;
			}
			set
			{
				this.data_item = value;
			}
		}

		/// <summary>Gets the index of the item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control from the <see cref="P:System.Web.UI.WebControls.Repeater.Items" /> collection of the control.</summary>
		/// <returns>The index of the item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control from the <see cref="P:System.Web.UI.WebControls.Repeater.Items" /> collection of the control.</returns>
		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06002D77 RID: 11639 RVA: 0x00078AC6 File Offset: 0x00076CC6
		public virtual int ItemIndex
		{
			get
			{
				return this.idx;
			}
		}

		/// <summary>Gets the type of the item in the <see cref="T:System.Web.UI.WebControls.Repeater" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values.</returns>
		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06002D78 RID: 11640 RVA: 0x00078ACE File Offset: 0x00076CCE
		public virtual ListItemType ItemType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItemIndex" />.</summary>
		/// <returns>An <see cref="P:System.Web.UI.WebControls.RepeaterItem.ItemIndex" /> property.</returns>
		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06002D79 RID: 11641 RVA: 0x00078AD6 File Offset: 0x00076CD6
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DisplayIndex" />.</summary>
		/// <returns>An <see cref="P:System.Web.UI.WebControls.RepeaterItem.ItemIndex" /> property.</returns>
		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x00078AD6 File Offset: 0x00076CD6
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x04001B7B RID: 7035
		private object data_item;

		// Token: 0x04001B7C RID: 7036
		private int idx;

		// Token: 0x04001B7D RID: 7037
		private ListItemType type;
	}
}
