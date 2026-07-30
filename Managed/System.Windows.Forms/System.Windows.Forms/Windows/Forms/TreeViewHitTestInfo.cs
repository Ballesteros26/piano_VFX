using System;

namespace System.Windows.Forms
{
	/// <summary>Contains information about an area of a <see cref="T:System.Windows.Forms.TreeView" /> control or a <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000397 RID: 919
	public class TreeViewHitTestInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TreeViewHitTestInfo" /> class. </summary>
		/// <param name="hitNode">The tree node located at the position indicated by the hit test.</param>
		/// <param name="hitLocation">One of the <see cref="T:System.Windows.Forms.TreeViewHitTestLocations" /> values.</param>
		// Token: 0x06004376 RID: 17270 RVA: 0x0010AB68 File Offset: 0x00108D68
		public TreeViewHitTestInfo(TreeNode hitNode, TreeViewHitTestLocations hitLocation)
		{
			this.node = hitNode;
			this.location = hitLocation;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.TreeNode" /> at the position indicated by a hit test of a <see cref="T:System.Windows.Forms.TreeView" /> control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> at the position indicated by a hit test of a <see cref="T:System.Windows.Forms.TreeView" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x06004377 RID: 17271 RVA: 0x0010AB80 File Offset: 0x00108D80
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		/// <summary>Gets the location of a hit test on a <see cref="T:System.Windows.Forms.TreeView" /> control, in relation to the <see cref="T:System.Windows.Forms.TreeView" /> and the nodes it contains.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TreeViewHitTestLocations" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x06004378 RID: 17272 RVA: 0x0010AB88 File Offset: 0x00108D88
		public TreeViewHitTestLocations Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x04001C51 RID: 7249
		private TreeNode node;

		// Token: 0x04001C52 RID: 7250
		private TreeViewHitTestLocations location;
	}
}
