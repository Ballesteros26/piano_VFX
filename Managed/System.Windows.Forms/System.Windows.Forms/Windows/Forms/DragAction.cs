using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies how and if a drag-and-drop operation should continue.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000152 RID: 338
	[ComVisible(true)]
	public enum DragAction
	{
		/// <summary>The operation will continue.</summary>
		// Token: 0x04000CB7 RID: 3255
		Continue,
		/// <summary>The operation will stop with a drop.</summary>
		// Token: 0x04000CB8 RID: 3256
		Drop,
		/// <summary>The operation is canceled with no drop message.</summary>
		// Token: 0x04000CB9 RID: 3257
		Cancel
	}
}
