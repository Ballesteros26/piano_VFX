using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Specifies the type of access to files allowed through the File dialog boxes.</summary>
	// Token: 0x0200058A RID: 1418
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum FileDialogPermissionAccess
	{
		/// <summary>No access to files through the File dialog boxes.</summary>
		// Token: 0x04002025 RID: 8229
		None = 0,
		/// <summary>Ability to open files through the File dialog boxes.</summary>
		// Token: 0x04002026 RID: 8230
		Open = 1,
		/// <summary>Ability to save files through the File dialog boxes.</summary>
		// Token: 0x04002027 RID: 8231
		Save = 2,
		/// <summary>Ability to open and save files through the File dialog boxes.</summary>
		// Token: 0x04002028 RID: 8232
		OpenSave = 3
	}
}
