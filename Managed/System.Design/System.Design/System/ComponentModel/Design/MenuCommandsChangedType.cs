using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	/// <summary>Specifies the type of action that occurred to the related object's <see cref="T:System.Windows.Forms.Design.MenuCommands" /> collection.</summary>
	// Token: 0x02000130 RID: 304
	[ComVisible(true)]
	public enum MenuCommandsChangedType
	{
		/// <summary>Specifies that one or more command objects were added.</summary>
		// Token: 0x04000200 RID: 512
		CommandAdded,
		/// <summary>Specifies that one or more commands were removed.</summary>
		// Token: 0x04000201 RID: 513
		CommandRemoved,
		/// <summary>Specifies that one or more commands have changed their status.</summary>
		// Token: 0x04000202 RID: 514
		CommandChanged
	}
}
