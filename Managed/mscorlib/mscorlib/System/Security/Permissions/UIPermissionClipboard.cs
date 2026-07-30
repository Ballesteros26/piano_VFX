using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the type of clipboard access that is allowed to the calling code.</summary>
	// Token: 0x020005BC RID: 1468
	[ComVisible(true)]
	[Serializable]
	public enum UIPermissionClipboard
	{
		/// <summary>Clipboard cannot be used.</summary>
		// Token: 0x040020FF RID: 8447
		NoClipboard,
		/// <summary>The ability to put data on the clipboard (Copy, Cut) is unrestricted. Intrinsic controls that accept Paste, such as text box, can accept the clipboard data, but user controls that must programmatically read the clipboard cannot.</summary>
		// Token: 0x04002100 RID: 8448
		OwnClipboard,
		/// <summary>Clipboard can be used without restriction.</summary>
		// Token: 0x04002101 RID: 8449
		AllClipboard
	}
}
