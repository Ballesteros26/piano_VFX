using System;

namespace System.Windows.Forms
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Windows.Forms.DataGridView.CellValueNeeded" /> event or <see cref="E:System.Windows.Forms.DataGridView.CellValuePushed" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />. </summary>
	/// <param name="sender">The source of the event. </param>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellValueEventArgs" /> that contains the event data.</param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200066F RID: 1647
	// (Invoke) Token: 0x0600516E RID: 20846
	public delegate void DataGridViewCellValueEventHandler(object sender, DataGridViewCellValueEventArgs e);
}
