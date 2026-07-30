using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a row within a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	// Token: 0x020003A2 RID: 930
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class FormViewRow : TableRow
	{
		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x06002522 RID: 9506 RVA: 0x00060AD6 File Offset: 0x0005ECD6
		// (set) Token: 0x06002523 RID: 9507 RVA: 0x00060ADE File Offset: 0x0005ECDE
		internal bool RenderJustCellContents { get; set; }

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> class.</summary>
		/// <param name="itemIndex">The index of the data item in the data source.</param>
		/// <param name="rowType">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> enumeration values.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</param>
		// Token: 0x06002524 RID: 9508 RVA: 0x00060AE7 File Offset: 0x0005ECE7
		public FormViewRow(int itemIndex, DataControlRowType rowType, DataControlRowState rowState)
		{
			this.rowIndex = itemIndex;
			this.rowType = rowType;
			this.rowState = rowState;
		}

		/// <summary>Gets the index of the data item displayed from the data source.</summary>
		/// <returns>The index of the data item displayed from the data source.</returns>
		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x06002525 RID: 9509 RVA: 0x00060B04 File Offset: 0x0005ED04
		public virtual int ItemIndex
		{
			get
			{
				return this.rowIndex;
			}
		}

		/// <summary>Gets the state of the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</returns>
		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06002526 RID: 9510 RVA: 0x00060B0C File Offset: 0x0005ED0C
		public virtual DataControlRowState RowState
		{
			get
			{
				return this.rowState;
			}
		}

		/// <summary>Gets the row type of the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> values.</returns>
		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06002527 RID: 9511 RVA: 0x00060B14 File Offset: 0x0005ED14
		public virtual DataControlRowType RowType
		{
			get
			{
				return this.rowType;
			}
		}

		/// <summary>Determines whether to pass an event up the page's ASP.NET server control hierarchy.</summary>
		/// <returns>true if the event has been canceled; otherwise, false.</returns>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">The event data.</param>
		// Token: 0x06002528 RID: 9512 RVA: 0x00060B1C File Offset: 0x0005ED1C
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (base.OnBubbleEvent(source, e))
			{
				return true;
			}
			if (e is CommandEventArgs)
			{
				FormViewCommandEventArgs formViewCommandEventArgs = new FormViewCommandEventArgs(source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(source, formViewCommandEventArgs);
				return true;
			}
			return false;
		}

		/// <summary>Renders the control to the specified HTML writer.</summary>
		/// <param name="writer">The HTML text writer object that receives the control content.</param>
		// Token: 0x06002529 RID: 9513 RVA: 0x00060B58 File Offset: 0x0005ED58
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (!this.RenderJustCellContents)
			{
				base.Render(writer);
				return;
			}
			foreach (object obj in this.Cells)
			{
				((TableCell)obj).RenderContents(writer);
			}
		}

		// Token: 0x040019EB RID: 6635
		private int rowIndex;

		// Token: 0x040019EC RID: 6636
		private DataControlRowState rowState;

		// Token: 0x040019ED RID: 6637
		private DataControlRowType rowType;
	}
}
