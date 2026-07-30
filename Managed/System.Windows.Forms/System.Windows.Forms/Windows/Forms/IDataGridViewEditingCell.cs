using System;

namespace System.Windows.Forms
{
	/// <summary>Defines common functionality for a cell that allows the manipulation of its value.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001CA RID: 458
	public interface IDataGridViewEditingCell
	{
		/// <summary>Gets or sets the formatted value of the cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the cell's value.</returns>
		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001DD8 RID: 7640
		// (set) Token: 0x06001DD9 RID: 7641
		object EditingCellFormattedValue { get; set; }

		/// <summary>Gets or sets a value indicating whether the value of the cell has changed.</summary>
		/// <returns>true if the value of the cell has changed; otherwise, false.</returns>
		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001DDA RID: 7642
		// (set) Token: 0x06001DDB RID: 7643
		bool EditingCellValueChanged { get; set; }

		/// <summary>Retrieves the formatted value of the cell.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the formatted version of the cell contents.</returns>
		/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that specifies the context in which the data is needed.</param>
		// Token: 0x06001DDC RID: 7644
		object GetEditingCellFormattedValue(DataGridViewDataErrorContexts context);

		/// <summary>Prepares the currently selected cell for editing</summary>
		/// <param name="selectAll">true to select the cell contents; otherwise, false.</param>
		// Token: 0x06001DDD RID: 7645
		void PrepareEditingCellForEdit(bool selectAll);
	}
}
