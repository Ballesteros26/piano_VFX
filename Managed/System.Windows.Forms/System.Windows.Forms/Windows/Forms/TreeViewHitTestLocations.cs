using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that represent areas of a <see cref="T:System.Windows.Forms.TreeView" /> or <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000398 RID: 920
	[Flags]
	[ComVisible(true)]
	public enum TreeViewHitTestLocations
	{
		/// <summary>A position in the client area of the <see cref="T:System.Windows.Forms.TreeView" /> control, but not on a node or a portion of a node.</summary>
		// Token: 0x04001C54 RID: 7252
		None = 1,
		/// <summary>A position within the bounds of an image contained on a <see cref="T:System.Windows.Forms.TreeView" /> or <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		// Token: 0x04001C55 RID: 7253
		Image = 2,
		/// <summary>A position on the text portion of a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		// Token: 0x04001C56 RID: 7254
		Label = 4,
		/// <summary>A position in the indentation area for a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		// Token: 0x04001C57 RID: 7255
		Indent = 8,
		/// <summary>A position on the plus/minus area of a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		// Token: 0x04001C58 RID: 7256
		PlusMinus = 16,
		/// <summary>A position to the right of the text area of a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		// Token: 0x04001C59 RID: 7257
		RightOfLabel = 32,
		/// <summary>A position within the bounds of a state image for a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		// Token: 0x04001C5A RID: 7258
		StateImage = 64,
		/// <summary>A position above the client portion of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		// Token: 0x04001C5B RID: 7259
		AboveClientArea = 256,
		/// <summary>A position below the client portion of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		// Token: 0x04001C5C RID: 7260
		BelowClientArea = 512,
		/// <summary>A position to the right of the client area of the <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		// Token: 0x04001C5D RID: 7261
		RightOfClientArea = 1024,
		/// <summary>A position to the left of the client area of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		// Token: 0x04001C5E RID: 7262
		LeftOfClientArea = 2048
	}
}
