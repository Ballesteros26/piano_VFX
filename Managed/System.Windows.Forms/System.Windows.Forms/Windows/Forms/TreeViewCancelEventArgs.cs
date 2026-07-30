using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.BeforeCheck" />, <see cref="E:System.Windows.Forms.TreeView.BeforeCollapse" />, <see cref="E:System.Windows.Forms.TreeView.BeforeExpand" />, and <see cref="E:System.Windows.Forms.TreeView.BeforeSelect" /> events of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000394 RID: 916
	public class TreeViewCancelEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeViewCancelEventArgs" /> class with the specified tree node, a value specifying whether the event is to be canceled, and the type of tree view action that raised the event.</summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> that the event is responding to. </param>
		/// <param name="cancel">true to cancel the event; otherwise, false. </param>
		/// <param name="action">One of the <see cref="T:System.Windows.Forms.TreeViewAction" /> values indicating the type of action that raised the event. </param>
		// Token: 0x0600436F RID: 17263 RVA: 0x0010AB10 File Offset: 0x00108D10
		public TreeViewCancelEventArgs(TreeNode node, bool cancel, TreeViewAction action)
			: base(cancel)
		{
			this.node = node;
			this.action = action;
		}

		/// <summary>Gets the tree node to be checked, expanded, collapsed, or selected.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> to be checked, expanded, collapsed, or selected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x06004370 RID: 17264 RVA: 0x0010AB28 File Offset: 0x00108D28
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		/// <summary>Gets the type of <see cref="T:System.Windows.Forms.TreeView" /> action that raised the event.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TreeViewAction" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x06004371 RID: 17265 RVA: 0x0010AB30 File Offset: 0x00108D30
		public TreeViewAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x04001C49 RID: 7241
		private TreeNode node;

		// Token: 0x04001C4A RID: 7242
		private TreeViewAction action;
	}
}
