using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="T:System.Windows.Forms.DataGridView" /><see cref="E:System.Windows.Forms.DataGridView.AutoSizeRowsModeChanged" /> and <see cref="E:System.Windows.Forms.DataGridView.RowHeadersWidthSizeModeChanged" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000DB RID: 219
	public class DataGridViewAutoSizeModeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewAutoSizeModeEventArgs" /> class.</summary>
		/// <param name="previousModeAutoSized">true if the <see cref="P:System.Windows.Forms.DataGridView.AutoSizeRowsMode" /> property was previously set to any <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode" /> value other than <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.None" /> or the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersWidthSizeMode" /> property was previously set to any <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode" /> value other than <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing" /> or <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing" />; otherwise, false.</param>
		// Token: 0x06001128 RID: 4392 RVA: 0x00044DBC File Offset: 0x00042FBC
		public DataGridViewAutoSizeModeEventArgs(bool previousModeAutoSized)
		{
			this.previousModeAutoSized = previousModeAutoSized;
		}

		/// <summary>Gets a value specifying whether the <see cref="T:System.Windows.Forms.DataGridView" /> was previously set to automatically resize.</summary>
		/// <returns>true if the <see cref="P:System.Windows.Forms.DataGridView.AutoSizeRowsMode" /> property was previously set to any <see cref="T:System.Windows.Forms.DataGridViewAutoSizeRowsMode" /> value other than <see cref="F:System.Windows.Forms.DataGridViewAutoSizeRowsMode.None" /> or the <see cref="P:System.Windows.Forms.DataGridView.RowHeadersWidthSizeMode" /> property was previously set to any <see cref="T:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode" /> value other than <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing" /> or <see cref="F:System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.EnableResizing" />; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x00044DCC File Offset: 0x00042FCC
		public bool PreviousModeAutoSized
		{
			get
			{
				return this.previousModeAutoSized;
			}
		}

		// Token: 0x04000AB8 RID: 2744
		private bool previousModeAutoSized;
	}
}
