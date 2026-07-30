using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeCheckChanged" />, <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeCollapsed" />, <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeDataBound" />, <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodeExpanded" />, and <see cref="E:System.Web.UI.WebControls.TreeView.TreeNodePopulate" /> events of the <see cref="T:System.Web.UI.WebControls.TreeView" /> control. This class cannot be inherited.</summary>
	// Token: 0x0200031E RID: 798
	public sealed class TreeNodeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.TreeNodeEventArgs" /> class using the specified <see cref="T:System.Web.UI.WebControls.TreeNode" /> object.</summary>
		/// <param name="node">A <see cref="T:System.Web.UI.WebControls.TreeNode" /> that represents the current node when the event is raised. </param>
		// Token: 0x06001C19 RID: 7193 RVA: 0x00046371 File Offset: 0x00044571
		public TreeNodeEventArgs(TreeNode node)
		{
			this._node = node;
		}

		/// <summary>Gets the node that raised the event.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TreeNode" /> that represents the node that raised the event.</returns>
		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x00046380 File Offset: 0x00044580
		public TreeNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x04001796 RID: 6038
		private TreeNode _node;
	}
}
