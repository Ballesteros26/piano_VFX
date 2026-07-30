using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the type of windows that code is allowed to use.</summary>
	// Token: 0x020005BD RID: 1469
	[ComVisible(true)]
	[Serializable]
	public enum UIPermissionWindow
	{
		/// <summary>Users cannot use any windows or user interface events. No user interface can be used.</summary>
		// Token: 0x04002103 RID: 8451
		NoWindows,
		/// <summary>Users can only use <see cref="F:System.Security.Permissions.UIPermissionWindow.SafeSubWindows" /> for drawing, and can only use user input events for user interface within that subwindow. Examples of <see cref="F:System.Security.Permissions.UIPermissionWindow.SafeSubWindows" /> are a <see cref="T:System.Windows.Forms.MessageBox" />, common dialog controls, and a control displayed within a browser.</summary>
		// Token: 0x04002104 RID: 8452
		SafeSubWindows,
		/// <summary>Users can only use <see cref="F:System.Security.Permissions.UIPermissionWindow.SafeTopLevelWindows" /> and <see cref="F:System.Security.Permissions.UIPermissionWindow.SafeSubWindows" /> for drawing, and can only use user input events for the user interface within those top-level windows and subwindows. </summary>
		// Token: 0x04002105 RID: 8453
		SafeTopLevelWindows,
		/// <summary>Users can use all windows and user input events without restriction.</summary>
		// Token: 0x04002106 RID: 8454
		AllWindows
	}
}
