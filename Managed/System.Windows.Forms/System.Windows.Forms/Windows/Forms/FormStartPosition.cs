using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the initial position of a form.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200019C RID: 412
	[ComVisible(true)]
	public enum FormStartPosition
	{
		/// <summary>The position of the form is determined by the <see cref="P:System.Windows.Forms.Control.Location" /> property.</summary>
		// Token: 0x04000EF4 RID: 3828
		Manual,
		/// <summary>The form is centered on the current display, and has the dimensions specified in the form's size.</summary>
		// Token: 0x04000EF5 RID: 3829
		CenterScreen,
		/// <summary>The form is positioned at the Windows default location and has the dimensions specified in the form's size.</summary>
		// Token: 0x04000EF6 RID: 3830
		WindowsDefaultLocation,
		/// <summary>The form is positioned at the Windows default location and has the bounds determined by Windows default.</summary>
		// Token: 0x04000EF7 RID: 3831
		WindowsDefaultBounds,
		/// <summary>The form is centered within the bounds of its parent form.</summary>
		// Token: 0x04000EF8 RID: 3832
		CenterParent
	}
}
