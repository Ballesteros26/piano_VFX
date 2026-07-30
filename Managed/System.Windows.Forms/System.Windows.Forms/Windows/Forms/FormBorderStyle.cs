using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies the border styles for a form.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000198 RID: 408
	[ComVisible(true)]
	public enum FormBorderStyle
	{
		/// <summary>No border.</summary>
		// Token: 0x04000EEA RID: 3818
		None,
		/// <summary>A fixed, single-line border.</summary>
		// Token: 0x04000EEB RID: 3819
		FixedSingle,
		/// <summary>A fixed, three-dimensional border.</summary>
		// Token: 0x04000EEC RID: 3820
		Fixed3D,
		/// <summary>A thick, fixed dialog-style border.</summary>
		// Token: 0x04000EED RID: 3821
		FixedDialog,
		/// <summary>A resizable border.</summary>
		// Token: 0x04000EEE RID: 3822
		Sizable,
		/// <summary>A tool window border that is not resizable. A tool window does not appear in the taskbar or in the window that appears when the user presses ALT+TAB. Although forms that specify <see cref="F:System.Windows.Forms.FormBorderStyle.FixedToolWindow" /> typically are not shown in the taskbar, you must also ensure that the <see cref="P:System.Windows.Forms.Form.ShowInTaskbar" /> property is set to false, since its default value is true.</summary>
		// Token: 0x04000EEF RID: 3823
		FixedToolWindow,
		/// <summary>A resizable tool window border. A tool window does not appear in the taskbar or in the window that appears when the user presses ALT+TAB.</summary>
		// Token: 0x04000EF0 RID: 3824
		SizableToolWindow
	}
}
