using System;

namespace System.Windows.Forms
{
	/// <summary>Provides an editing notification interface.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001C8 RID: 456
	public interface IDataGridColumnStyleEditingNotificationService
	{
		/// <summary>Informs the <see cref="T:System.Windows.Forms.DataGrid" /> that the user has begun editing the column.</summary>
		/// <param name="editingControl">The <see cref="T:System.Windows.Forms.Control" /> that is editing the column. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001DD5 RID: 7637
		void ColumnStartedEditing(Control editingControl);
	}
}
