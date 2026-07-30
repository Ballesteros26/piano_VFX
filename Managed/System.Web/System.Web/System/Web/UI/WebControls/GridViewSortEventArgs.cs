using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.Sorting" /> event.</summary>
	// Token: 0x020002C9 RID: 713
	public class GridViewSortEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewSortEventArgs" /> class.</summary>
		/// <param name="sortExpression">The sort expression used to sort the items in the control.</param>
		/// <param name="sortDirection">A <see cref="T:System.Web.UI.WebControls.SortDirection" /> that indicates the direction in which to sort the items in the control.</param>
		// Token: 0x06001B3F RID: 6975 RVA: 0x00046073 File Offset: 0x00044273
		public GridViewSortEventArgs(string sortExpression, SortDirection sortDirection)
		{
			this._sortExpression = sortExpression;
			this._sortDirection = sortDirection;
		}

		/// <summary>Gets or sets the direction in which to sort the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SortDirection" /> values.</returns>
		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06001B40 RID: 6976 RVA: 0x00046089 File Offset: 0x00044289
		// (set) Token: 0x06001B41 RID: 6977 RVA: 0x00046091 File Offset: 0x00044291
		public SortDirection SortDirection
		{
			get
			{
				return this._sortDirection;
			}
			set
			{
				this._sortDirection = value;
			}
		}

		/// <summary>Gets or sets the expression used to sort the items in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>The expression used to sort the items in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06001B42 RID: 6978 RVA: 0x0004609A File Offset: 0x0004429A
		// (set) Token: 0x06001B43 RID: 6979 RVA: 0x000460A2 File Offset: 0x000442A2
		public string SortExpression
		{
			get
			{
				return this._sortExpression;
			}
			set
			{
				this._sortExpression = value;
			}
		}

		// Token: 0x040016EA RID: 5866
		private string _sortExpression;

		// Token: 0x040016EB RID: 5867
		private SortDirection _sortDirection;
	}
}
