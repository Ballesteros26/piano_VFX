using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents an individual row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
	// Token: 0x020003A8 RID: 936
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class GridViewRow : TableRow, IDataItemContainer, INamingContainer
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> class.</summary>
		/// <param name="rowIndex">The index of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object in the <see cref="P:System.Web.UI.WebControls.GridView.Rows" /> collection of a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</param>
		/// <param name="dataItemIndex">The index of the <see cref="P:System.Web.UI.WebControls.GridViewRow.DataItem" /> in the underlying <see cref="T:System.Data.DataSet" />.</param>
		/// <param name="rowType">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> enumeration values.</param>
		/// <param name="rowState">A bitwise combination of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</param>
		// Token: 0x0600262E RID: 9774 RVA: 0x0006469A File Offset: 0x0006289A
		public GridViewRow(int rowIndex, int dataItemIndex, DataControlRowType rowType, DataControlRowState rowState)
		{
			this.rowIndex = rowIndex;
			this.dataItemIndex = dataItemIndex;
			this.rowType = rowType;
			this.rowState = rowState;
		}

		/// <summary>Gets the underlying data object to which the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object is bound.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the underlying data object to which the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object is bound. </returns>
		// Token: 0x17000C2A RID: 3114
		// (get) Token: 0x0600262F RID: 9775 RVA: 0x000646BF File Offset: 0x000628BF
		// (set) Token: 0x06002630 RID: 9776 RVA: 0x000646C7 File Offset: 0x000628C7
		public virtual object DataItem
		{
			get
			{
				return this.dataItem;
			}
			set
			{
				this.dataItem = value;
			}
		}

		/// <summary>Gets the index of the <see cref="P:System.Web.UI.WebControls.GridViewRow.DataItem" /> in the underlying <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The index of the <see cref="P:System.Web.UI.WebControls.GridViewRow.DataItem" /> in the underlying data source.</returns>
		// Token: 0x17000C2B RID: 3115
		// (get) Token: 0x06002631 RID: 9777 RVA: 0x000646D0 File Offset: 0x000628D0
		public virtual int DataItemIndex
		{
			get
			{
				return this.dataItemIndex;
			}
		}

		/// <summary>Gets the index of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object in the <see cref="P:System.Web.UI.WebControls.GridView.Rows" /> collection of a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object in the <see cref="P:System.Web.UI.WebControls.GridView.Rows" /> collection of a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x06002632 RID: 9778 RVA: 0x000646D8 File Offset: 0x000628D8
		public virtual int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets the state of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</returns>
		// Token: 0x17000C2D RID: 3117
		// (get) Token: 0x06002633 RID: 9779 RVA: 0x000646E0 File Offset: 0x000628E0
		// (set) Token: 0x06002634 RID: 9780 RVA: 0x000646E8 File Offset: 0x000628E8
		public virtual DataControlRowState RowState
		{
			get
			{
				return this.rowState;
			}
			set
			{
				this.rowState = value;
			}
		}

		/// <summary>Gets the row type of the <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> values.</returns>
		// Token: 0x17000C2E RID: 3118
		// (get) Token: 0x06002635 RID: 9781 RVA: 0x000646F1 File Offset: 0x000628F1
		// (set) Token: 0x06002636 RID: 9782 RVA: 0x000646F9 File Offset: 0x000628F9
		public virtual DataControlRowType RowType
		{
			get
			{
				return this.rowType;
			}
			set
			{
				this.rowType = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItem" />.</summary>
		/// <returns>Returns <see cref="P:System.Web.UI.WebControls.GridViewRow.DataItem" />.</returns>
		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06002637 RID: 9783 RVA: 0x00064702 File Offset: 0x00062902
		object IDataItemContainer.DataItem
		{
			get
			{
				return this.DataItem;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItemIndex" />.</summary>
		/// <returns>Returns <see cref="P:System.Web.UI.WebControls.GridViewRow.DataItemIndex" />.</returns>
		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06002638 RID: 9784 RVA: 0x0006470A File Offset: 0x0006290A
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.DataItemIndex;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DisplayIndex" />.</summary>
		/// <returns>Returns <see cref="P:System.Web.UI.WebControls.GridViewRow.RowIndex" />.</returns>
		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06002639 RID: 9785 RVA: 0x00064712 File Offset: 0x00062912
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.RowIndex;
			}
		}

		/// <summary>Determines whether to pass an event up the page's ASP.NET server control hierarchy.</summary>
		/// <returns>true if the event has been canceled; otherwise, false.</returns>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x0600263A RID: 9786 RVA: 0x0006471C File Offset: 0x0006291C
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (base.OnBubbleEvent(source, e))
			{
				return true;
			}
			if (e is CommandEventArgs)
			{
				GridViewCommandEventArgs gridViewCommandEventArgs = new GridViewCommandEventArgs(this, source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(source, gridViewCommandEventArgs);
				return true;
			}
			return false;
		}

		// Token: 0x04001A3E RID: 6718
		private object dataItem;

		// Token: 0x04001A3F RID: 6719
		private int rowIndex;

		// Token: 0x04001A40 RID: 6720
		private int dataItemIndex;

		// Token: 0x04001A41 RID: 6721
		private DataControlRowState rowState;

		// Token: 0x04001A42 RID: 6722
		private DataControlRowType rowType;
	}
}
