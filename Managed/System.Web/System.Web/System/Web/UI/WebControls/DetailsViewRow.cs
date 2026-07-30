using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a row within a <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
	// Token: 0x0200038D RID: 909
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DetailsViewRow : TableRow
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> class.</summary>
		/// <param name="rowIndex">The index of the row in the <see cref="P:System.Web.UI.WebControls.DetailsView.Rows" /> collection of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</param>
		/// <param name="rowType">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> enumeration values.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</param>
		// Token: 0x060023B1 RID: 9137 RVA: 0x0005CE33 File Offset: 0x0005B033
		public DetailsViewRow(int rowIndex, DataControlRowType rowType, DataControlRowState rowState)
		{
			this.rowIndex = rowIndex;
			this.rowType = rowType;
			this.rowState = rowState;
		}

		/// <summary>Gets the index of the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> object in the <see cref="P:System.Web.UI.WebControls.DetailsView.Rows" /> collection of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
		/// <returns>The index of the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> in the <see cref="P:System.Web.UI.WebControls.DetailsView.Rows" /> collection of the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x060023B2 RID: 9138 RVA: 0x0005CE50 File Offset: 0x0005B050
		public virtual int RowIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets the state of the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> object.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</returns>
		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x060023B3 RID: 9139 RVA: 0x0005CE58 File Offset: 0x0005B058
		public virtual DataControlRowState RowState
		{
			get
			{
				return this.rowState;
			}
		}

		/// <summary>Gets the row type of the <see cref="T:System.Web.UI.WebControls.DetailsViewRow" /> object.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> values.</returns>
		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x0005CE60 File Offset: 0x0005B060
		public virtual DataControlRowType RowType
		{
			get
			{
				return this.rowType;
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x060023B5 RID: 9141 RVA: 0x0005CE68 File Offset: 0x0005B068
		// (set) Token: 0x060023B6 RID: 9142 RVA: 0x0005CE70 File Offset: 0x0005B070
		internal DataControlField ContainingField
		{
			get
			{
				return this.containingField;
			}
			set
			{
				this.containingField = value;
			}
		}

		/// <summary>Determines whether to pass an event up the page's ASP.NET server control hierarchy.</summary>
		/// <returns>true if the event has been canceled; otherwise, false.</returns>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060023B7 RID: 9143 RVA: 0x0005CE7C File Offset: 0x0005B07C
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (base.OnBubbleEvent(source, e))
			{
				return true;
			}
			if (e is CommandEventArgs)
			{
				DetailsViewCommandEventArgs detailsViewCommandEventArgs = new DetailsViewCommandEventArgs(source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(source, detailsViewCommandEventArgs);
				return true;
			}
			return false;
		}

		// Token: 0x04001985 RID: 6533
		private int rowIndex;

		// Token: 0x04001986 RID: 6534
		private DataControlRowState rowState;

		// Token: 0x04001987 RID: 6535
		private DataControlRowType rowType;

		// Token: 0x04001988 RID: 6536
		private DataControlField containingField;
	}
}
