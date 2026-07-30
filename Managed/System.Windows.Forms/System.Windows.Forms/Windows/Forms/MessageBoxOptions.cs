using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies options on a <see cref="T:System.Windows.Forms.MessageBox" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200025C RID: 604
	[Flags]
	public enum MessageBoxOptions
	{
		/// <summary>The message box is displayed on the active desktop.</summary>
		// Token: 0x040013BD RID: 5053
		DefaultDesktopOnly = 131072,
		/// <summary>The message box text is right-aligned.</summary>
		// Token: 0x040013BE RID: 5054
		RightAlign = 524288,
		/// <summary>Specifies that the message box text is displayed with right to left reading order.</summary>
		// Token: 0x040013BF RID: 5055
		RtlReading = 1048576,
		/// <summary>The message box is displayed on the active desktop.</summary>
		// Token: 0x040013C0 RID: 5056
		ServiceNotification = 2097152
	}
}
