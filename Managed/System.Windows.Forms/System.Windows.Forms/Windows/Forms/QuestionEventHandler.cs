using System;

namespace System.Windows.Forms
{
	/// <summary>Represents the method that will handle the <see cref="E:System.Windows.Forms.DataGridView.CancelRowEdit" /> event or the <see cref="E:System.Windows.Forms.DataGridView.RowDirtyStateNeeded" /> event of a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	/// <param name="sender">The source of the event.</param>
	/// <param name="e">A <see cref="T:System.Windows.Forms.QuestionEventArgs" /> that contains the event data.</param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006AF RID: 1711
	// (Invoke) Token: 0x0600526E RID: 21102
	public delegate void QuestionEventHandler(object sender, QuestionEventArgs e);
}
