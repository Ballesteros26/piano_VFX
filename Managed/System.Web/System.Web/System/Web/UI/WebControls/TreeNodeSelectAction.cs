using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents the event or events to raise when a node in the <see cref="T:System.Web.UI.WebControls.TreeView" /> control is selected.</summary>
	// Token: 0x02000320 RID: 800
	public enum TreeNodeSelectAction
	{
		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TreeView.SelectedNodeChanged" /> event when a node is selected.</summary>
		// Token: 0x04001798 RID: 6040
		Select,
		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeExpanded" /> event when a node is selected.</summary>
		// Token: 0x04001799 RID: 6041
		Expand,
		/// <summary>Raises both the <see cref="E:System.Web.UI.WebControls.TreeView.SelectedNodeChanged" /> and <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeExpanded" /> events when a node is selected.</summary>
		// Token: 0x0400179A RID: 6042
		SelectExpand,
		/// <summary>Raises no events when a node is selected.</summary>
		// Token: 0x0400179B RID: 6043
		None
	}
}
