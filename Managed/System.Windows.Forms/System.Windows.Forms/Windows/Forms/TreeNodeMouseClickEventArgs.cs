using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.NodeMouseClick" /> and <see cref="E:System.Windows.Forms.TreeView.NodeMouseDoubleClick" /> events. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200038F RID: 911
	public class TreeNodeMouseClickEventArgs : MouseEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNodeMouseClickEventArgs" /> class. </summary>
		/// <param name="node">The node that was clicked.</param>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> members.</param>
		/// <param name="clicks">The number of clicks that occurred.</param>
		/// <param name="x">The x-coordinate where the click occurred.</param>
		/// <param name="y">The y-coordinate where the click occurred.</param>
		// Token: 0x06004283 RID: 17027 RVA: 0x001069A4 File Offset: 0x00104BA4
		public TreeNodeMouseClickEventArgs(TreeNode node, MouseButtons button, int clicks, int x, int y)
			: base(button, clicks, x, y, 0)
		{
			this.node = node;
		}

		/// <summary>Gets the node that was clicked.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> that was clicked.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x06004284 RID: 17028 RVA: 0x001069BC File Offset: 0x00104BBC
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x04001BEC RID: 7148
		private TreeNode node;
	}
}
