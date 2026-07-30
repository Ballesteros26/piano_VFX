using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the action that raised a <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000393 RID: 915
	public enum TreeViewAction
	{
		/// <summary>The action that caused the event is unknown.</summary>
		// Token: 0x04001C44 RID: 7236
		Unknown,
		/// <summary>The event was caused by a keystroke.</summary>
		// Token: 0x04001C45 RID: 7237
		ByKeyboard,
		/// <summary>The event was caused by a mouse operation.</summary>
		// Token: 0x04001C46 RID: 7238
		ByMouse,
		/// <summary>The event was caused by the <see cref="T:System.Windows.Forms.TreeNode" /> collapsing.</summary>
		// Token: 0x04001C47 RID: 7239
		Collapse,
		/// <summary>The event was caused by the <see cref="T:System.Windows.Forms.TreeNode" /> expanding.</summary>
		// Token: 0x04001C48 RID: 7240
		Expand
	}
}
