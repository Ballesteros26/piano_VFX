using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the reason that a form was closed.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200007C RID: 124
	public enum CloseReason
	{
		/// <summary>The cause of the closure was not defined or could not be determined.</summary>
		// Token: 0x040006D4 RID: 1748
		None,
		/// <summary>The operating system is closing all applications before shutting down.</summary>
		// Token: 0x040006D5 RID: 1749
		WindowsShutDown,
		/// <summary>The parent form of this multiple document interface (MDI) form is closing.</summary>
		// Token: 0x040006D6 RID: 1750
		MdiFormClosing,
		/// <summary>The user is closing the form through the user interface (UI), for example by clicking the Close button on the form window, selecting Close from the window's control menu, or pressing ALT+F4.</summary>
		// Token: 0x040006D7 RID: 1751
		UserClosing,
		/// <summary>The form is  closing because the user clicked End Task in Microsoft Windows Task Manager.  Note that if the user ends a process by clicking End Process, the form closes without raising the <see cref="E:System.Windows.Forms.Form.FormClosing" /> or <see cref="E:System.Windows.Forms.Form.FormClosed" /> event.</summary>
		// Token: 0x040006D8 RID: 1752
		TaskManagerClosing,
		/// <summary>The owner form is closing.</summary>
		// Token: 0x040006D9 RID: 1753
		FormOwnerClosing,
		/// <summary>The <see cref="M:System.Windows.Forms.Application.Exit" /> method of the <see cref="T:System.Windows.Forms.Application" /> class was invoked. </summary>
		// Token: 0x040006DA RID: 1754
		ApplicationExitCall
	}
}
