using System;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides an interface for a <see cref="T:System.Drawing.Design.UITypeEditor" /> to display Windows Forms or to display a control in a drop-down area from a property grid control in design mode.</summary>
	// Token: 0x02000016 RID: 22
	public interface IWindowsFormsEditorService
	{
		/// <summary>Closes any previously opened drop down control area.</summary>
		// Token: 0x060000BC RID: 188
		void CloseDropDown();

		/// <summary>Displays the specified control in a drop down area below a value field of the property grid that provides this service.</summary>
		/// <param name="control">The drop down list <see cref="T:System.Windows.Forms.Control" /> to open. </param>
		// Token: 0x060000BD RID: 189
		void DropDownControl(Control control);

		/// <summary>Shows the specified <see cref="T:System.Windows.Forms.Form" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DialogResult" /> indicating the result code returned by the <see cref="T:System.Windows.Forms.Form" />.</returns>
		/// <param name="dialog">The <see cref="T:System.Windows.Forms.Form" /> to display. </param>
		// Token: 0x060000BE RID: 190
		DialogResult ShowDialog(Form dialog);
	}
}
