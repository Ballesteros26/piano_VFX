using System;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that represent the ways a <see cref="T:System.Windows.Forms.TreeView" /> can be drawn.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000395 RID: 917
	public enum TreeViewDrawMode
	{
		/// <summary>The <see cref="T:System.Windows.Forms.TreeView" /> is drawn by the operating system.</summary>
		// Token: 0x04001C4C RID: 7244
		Normal,
		/// <summary>The label portion of the <see cref="T:System.Windows.Forms.TreeView" /> nodes are drawn manually. Other node elements are drawn by the operating system, including icons, checkboxes, plus and minus signs, and lines connecting the nodes.</summary>
		// Token: 0x04001C4D RID: 7245
		OwnerDrawText,
		/// <summary>All elements of a <see cref="T:System.Windows.Forms.TreeView" /> node are drawn manually, including icons, checkboxes, plus and minus signs, and lines connecting the nodes.</summary>
		// Token: 0x04001C4E RID: 7246
		OwnerDrawAll
	}
}
