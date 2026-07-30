using System;

namespace System.Web.UI
{
	/// <summary>Defines the property and event a control implements to act as a check box. </summary>
	// Token: 0x0200019C RID: 412
	public interface ICheckBoxControl
	{
		/// <summary>Gets or sets the value of an <see cref="T:System.Web.UI.ICheckBoxControl" /> control that indicates whether the control is selected.</summary>
		/// <returns>true if the check box is selected; otherwise, false.</returns>
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06000FD1 RID: 4049
		// (set) Token: 0x06000FD2 RID: 4050
		bool Checked { get; set; }

		/// <summary>Occurs when the value of the <see cref="P:System.Web.UI.ICheckBoxControl.Checked" /> property changes between posts to the server.</summary>
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06000FD3 RID: 4051
		// (remove) Token: 0x06000FD4 RID: 4052
		event EventHandler CheckedChanged;
	}
}
