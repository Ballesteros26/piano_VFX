using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.TreeView.DrawNode" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200015C RID: 348
	public class DrawTreeNodeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DrawTreeNodeEventArgs" /> class.</summary>
		/// <param name="graphics">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw. </param>
		/// <param name="node">The <see cref="T:System.Windows.Forms.TreeNode" /> to draw. </param>
		/// <param name="bounds">The <see cref="T:System.Drawing.Rectangle" /> within which to draw. </param>
		/// <param name="state">A bitwise combination of the <see cref="T:System.Windows.Forms.TreeNodeStates" /> values indicating the current state of the <see cref="T:System.Windows.Forms.TreeNode" /> to draw. </param>
		// Token: 0x06001782 RID: 6018 RVA: 0x00056558 File Offset: 0x00054758
		public DrawTreeNodeEventArgs(Graphics graphics, TreeNode node, Rectangle bounds, TreeNodeStates state)
		{
			this.bounds = bounds;
			this.draw_default = false;
			this.graphics = graphics;
			this.node = node;
			this.state = state;
		}

		/// <summary>Gets the size and location of the <see cref="T:System.Windows.Forms.TreeNode" /> to draw.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.TreeNode" /> to draw.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001783 RID: 6019 RVA: 0x00056590 File Offset: 0x00054790
		public Rectangle Bounds
		{
			get
			{
				return this.bounds;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.TreeNode" /> should be drawn by the operating system rather than being owner drawn.</summary>
		/// <returns>true if the node should be drawn by the operating system; false if the node will be drawn in the event handler. The default value is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x00056598 File Offset: 0x00054798
		// (set) Token: 0x06001785 RID: 6021 RVA: 0x000565A0 File Offset: 0x000547A0
		public bool DrawDefault
		{
			get
			{
				return this.draw_default;
			}
			set
			{
				this.draw_default = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Drawing.Graphics" /> object used to draw the <see cref="T:System.Windows.Forms.TreeNode" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Graphics" /> used to draw the <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x000565AC File Offset: 0x000547AC
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.TreeNode" /> to draw.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TreeNode" /> to draw.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001787 RID: 6023 RVA: 0x000565B4 File Offset: 0x000547B4
		public TreeNode Node
		{
			get
			{
				return this.node;
			}
		}

		/// <summary>Gets the current state of the <see cref="T:System.Windows.Forms.TreeNode" /> to draw.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.TreeNodeStates" /> values indicating the current state of the <see cref="T:System.Windows.Forms.TreeNode" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x000565BC File Offset: 0x000547BC
		public TreeNodeStates State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x04000CFF RID: 3327
		private Rectangle bounds;

		// Token: 0x04000D00 RID: 3328
		private bool draw_default;

		// Token: 0x04000D01 RID: 3329
		private Graphics graphics;

		// Token: 0x04000D02 RID: 3330
		private TreeNode node;

		// Token: 0x04000D03 RID: 3331
		private TreeNodeStates state;
	}
}
