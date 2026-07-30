using System;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that indicate how a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> is displayed.</summary>
	// Token: 0x0200010B RID: 267
	public enum DataGridViewComboBoxDisplayStyle
	{
		/// <summary>When it is not in edit mode, the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> mimics the appearance of a <see cref="T:System.Windows.Forms.ComboBox" /> control.</summary>
		// Token: 0x04000B83 RID: 2947
		ComboBox,
		/// <summary>When it is not in edit mode, the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> is displayed with a drop-down button but does not otherwise mimic the appearance of a <see cref="T:System.Windows.Forms.ComboBox" /> control.</summary>
		// Token: 0x04000B84 RID: 2948
		DropDownButton,
		/// <summary>When it is not in edit mode, the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> is displayed without a drop-down button.</summary>
		// Token: 0x04000B85 RID: 2949
		Nothing
	}
}
