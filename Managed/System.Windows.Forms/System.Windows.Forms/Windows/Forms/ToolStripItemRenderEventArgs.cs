using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the events that render the background of objects derived from <see cref="T:System.Windows.Forms.ToolStripItem" /> in the <see cref="T:System.Windows.Forms.ToolStripRenderer" /> class. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000361 RID: 865
	public class ToolStripItemRenderEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> and using the specified <see cref="T:System.Drawing.Graphics" />. </summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object used to draw the item.</param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> to be drawn.</param>
		// Token: 0x06003E28 RID: 15912 RVA: 0x000F8004 File Offset: 0x000F6204
		public ToolStripItemRenderEventArgs(Graphics g, ToolStripItem item)
		{
			this.graphics = g;
			this.item = item;
		}

		/// <summary>Gets the graphics used to paint the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x06003E29 RID: 15913 RVA: 0x000F801C File Offset: 0x000F621C
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripItem" /> to paint.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" /> to paint.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x06003E2A RID: 15914 RVA: 0x000F8024 File Offset: 0x000F6224
		public ToolStripItem Item
		{
			get
			{
				return this.item;
			}
		}

		/// <summary>Gets the value of the <see cref="P:System.Windows.Forms.ToolStripItem.Owner" /> property for the <see cref="T:System.Windows.Forms.ToolStripItem" /> to paint.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStrip" /> that is the owner of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x06003E2B RID: 15915 RVA: 0x000F802C File Offset: 0x000F622C
		public ToolStrip ToolStrip
		{
			get
			{
				return this.item.Owner;
			}
		}

		// Token: 0x04001B02 RID: 6914
		private Graphics graphics;

		// Token: 0x04001B03 RID: 6915
		private ToolStripItem item;
	}
}
