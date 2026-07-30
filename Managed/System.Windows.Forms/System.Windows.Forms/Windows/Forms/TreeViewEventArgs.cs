using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.AfterCheck" />, <see cref="E:System.Windows.Forms.TreeView.AfterCollapse" />, <see cref="E:System.Windows.Forms.TreeView.AfterExpand" />, or <see cref="E:System.Windows.Forms.TreeView.AfterSelect" /> events of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000396 RID: 918
	public class TreeViewEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> class for the specified tree node.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> that the event is responding to. </param>
		// Token: 0x06004372 RID: 17266 RVA: 0x0010AB38 File Offset: 0x00108D38
		public TreeViewEventArgs(TreeNode node)
		{
			this.node = node;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeViewEventArgs" /> class for the specified tree node and with the specified type of action that raised the event.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> that the event is responding to. </param>
		/// <param name="action">The type of <see cref="T:System.Windows.Forms.TreeViewAction" /> that raised the event. </param>
		// Token: 0x06004373 RID: 17267 RVA: 0x0010AB48 File Offset: 0x00108D48
		public TreeViewEventArgs(TreeNode node, TreeViewAction action)
			: this(node)
		{
			this.action = action;
		}

		/// <summary>Gets the type of action that raised the event.</summary>
		/// <returns>The type of <see cref="T:System.Windows.Forms.TreeViewAction" /> that raised the event.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x06004374 RID: 17268 RVA: 0x0010AB58 File Offset: 0x00108D58
		public TreeViewAction Action
		{
			get
			{
				return this.action;
			}
		}

		/// <summary>Gets the tree node that has been checked, expanded, collapsed, or selected.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that has been checked, expanded, collapsed, or selected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x06004375 RID: 17269 RVA: 0x0010AB60 File Offset: 0x00108D60
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x04001C4F RID: 7247
		private TreeNode node;

		// Token: 0x04001C50 RID: 7248
		private TreeViewAction action;
	}
}
