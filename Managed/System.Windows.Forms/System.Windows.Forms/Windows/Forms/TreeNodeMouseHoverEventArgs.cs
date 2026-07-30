using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.NodeMouseHover" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000390 RID: 912
	[ComVisible(true)]
	public class TreeNodeMouseHoverEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeNodeMouseHoverEventArgs" /> class. </summary>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> the mouse pointer is currently resting on.</param>
		// Token: 0x06004285 RID: 17029 RVA: 0x001069C4 File Offset: 0x00104BC4
		public TreeNodeMouseHoverEventArgs(TreeNode node)
		{
			this.node = node;
		}

		/// <summary>Gets the node the mouse pointer is currently resting on.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> the mouse pointer is currently resting on.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x06004286 RID: 17030 RVA: 0x001069D4 File Offset: 0x00104BD4
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		// Token: 0x04001BED RID: 7149
		private TreeNode node;
	}
}
