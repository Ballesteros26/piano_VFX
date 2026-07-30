using System;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that represent the possible states of a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000391 RID: 913
	[Flags]
	public enum TreeNodeStates
	{
		/// <summary>The node is selected.</summary>
		// Token: 0x04001BEF RID: 7151
		Selected = 1,
		/// <summary>The node is disabled.</summary>
		// Token: 0x04001BF0 RID: 7152
		Grayed = 2,
		/// <summary>The node is checked.</summary>
		// Token: 0x04001BF1 RID: 7153
		Checked = 8,
		/// <summary>The node has focus.</summary>
		// Token: 0x04001BF2 RID: 7154
		Focused = 16,
		/// <summary>The node is in its default state.</summary>
		// Token: 0x04001BF3 RID: 7155
		Default = 32,
		/// <summary>The node is hot. This state occurs when the <see cref="P:System.Windows.Forms.TreeView.HotTracking" /> property is set to true and the mouse pointer is over the node.</summary>
		// Token: 0x04001BF4 RID: 7156
		Hot = 64,
		/// <summary>The node is marked.</summary>
		// Token: 0x04001BF5 RID: 7157
		Marked = 128,
		/// <summary>The node in an indeterminate state.</summary>
		// Token: 0x04001BF6 RID: 7158
		Indeterminate = 256,
		/// <summary>The node should indicate a keyboard shortcut.</summary>
		// Token: 0x04001BF7 RID: 7159
		ShowKeyboardCues = 512
	}
}
