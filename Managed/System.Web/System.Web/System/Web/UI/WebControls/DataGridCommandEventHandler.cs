using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the method that handles the <see cref="E:System.Web.UI.WebControls.DataGrid.CancelCommand" />, <see cref="E:System.Web.UI.WebControls.DataGrid.DeleteCommand" />, <see cref="E:System.Web.UI.WebControls.DataGrid.EditCommand" />, <see cref="E:System.Web.UI.WebControls.DataGrid.ItemCommand" />, and <see cref="E:System.Web.UI.WebControls.DataGrid.UpdateCommand" /> events of a <see cref="T:System.Web.UI.WebControls.DataGrid" /> control.</summary>
	/// <param name="source">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataGridCommandEventArgs" /> that contains the event data. </param>
	// Token: 0x02000292 RID: 658
	// (Invoke) Token: 0x06001A8A RID: 6794
	public delegate void DataGridCommandEventHandler(object source, DataGridCommandEventArgs e);
}
