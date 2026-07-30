using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.BeforeLabelEdit" /> and <see cref="E:System.Windows.Forms.TreeView.AfterLabelEdit" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000273 RID: 627
	public class NodeLabelEditEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NodeLabelEditEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		/// <param name="node">The tree node containing the text to edit. </param>
		// Token: 0x060028C4 RID: 10436 RVA: 0x0009E0A0 File Offset: 0x0009C2A0
		public NodeLabelEditEventArgs(TreeNode node)
		{
			this.node = node;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.NodeLabelEditEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.TreeNode" /> and the specified text with which to update the tree node label.</summary>
		/// <param name="node">The tree node containing the text to edit. </param>
		/// <param name="label">The new text to associate with the tree node. </param>
		// Token: 0x060028C5 RID: 10437 RVA: 0x0009E0B0 File Offset: 0x0009C2B0
		public NodeLabelEditEventArgs(TreeNode node, string label)
			: this(node)
		{
			this.label = label;
		}

		/// <summary>Gets or sets a value indicating whether the edit has been canceled.</summary>
		/// <returns>true if the edit has been canceled; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060028C6 RID: 10438 RVA: 0x0009E0C0 File Offset: 0x0009C2C0
		// (set) Token: 0x060028C7 RID: 10439 RVA: 0x0009E0C8 File Offset: 0x0009C2C8
		public bool CancelEdit
		{
			get
			{
				return this.cancel;
			}
			set
			{
				this.cancel = value;
				if (this.cancel)
				{
					this.node.EndEdit(true);
				}
			}
		}

		/// <summary>Gets the tree node containing the text to edit.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TreeNode" /> that represents the tree node containing the text to edit.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x060028C8 RID: 10440 RVA: 0x0009E0E8 File Offset: 0x0009C2E8
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		/// <summary>Gets the new text to associate with the tree node.</summary>
		/// <returns>The string value that represents the new <see cref="T:System.Windows.Forms.TreeNode" /> label or null if the user cancels the edit. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x060028C9 RID: 10441 RVA: 0x0009E0F0 File Offset: 0x0009C2F0
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x0009E0F8 File Offset: 0x0009C2F8
		internal void SetLabel(string label)
		{
			this.label = label;
		}

		// Token: 0x04001466 RID: 5222
		private TreeNode node;

		// Token: 0x04001467 RID: 5223
		private string label;

		// Token: 0x04001468 RID: 5224
		private bool cancel;
	}
}
