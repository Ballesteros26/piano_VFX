using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the different node types (leaf, parent, and root) in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control.</summary>
	// Token: 0x02000321 RID: 801
	[Flags]
	public enum TreeNodeTypes
	{
		/// <summary>No nodes.</summary>
		// Token: 0x0400179D RID: 6045
		None = 0,
		/// <summary>A node with no parent node and one or more child nodes.</summary>
		// Token: 0x0400179E RID: 6046
		Root = 1,
		/// <summary>A node with a parent node and one or more child nodes.</summary>
		// Token: 0x0400179F RID: 6047
		Parent = 2,
		/// <summary>A node with no child nodes.</summary>
		// Token: 0x040017A0 RID: 6048
		Leaf = 4,
		/// <summary>All nodes.</summary>
		// Token: 0x040017A1 RID: 6049
		All = 7
	}
}
