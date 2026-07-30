using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderGrip" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000379 RID: 889
	public class ToolStripSeparatorRenderEventArgs : ToolStripItemRenderEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripSeparatorRenderEventArgs" /> class. </summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to paint with.</param>
		/// <param name="separator">The <see cref="T:System.Windows.Forms.ToolStripSeparator" /> to be painted.</param>
		/// <param name="vertical">A value indicating whether or not the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> is to be drawn vertically.</param>
		// Token: 0x0600402F RID: 16431 RVA: 0x000FF540 File Offset: 0x000FD740
		public ToolStripSeparatorRenderEventArgs(Graphics g, ToolStripSeparator separator, bool vertical)
			: base(g, separator)
		{
			this.vertical = vertical;
		}

		/// <summary>Gets a value indicating whether the display style for the grip is vertical. </summary>
		/// <returns>true if the display style for the <see cref="T:System.Windows.Forms.ToolStripSeparator" /> is vertical; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x06004030 RID: 16432 RVA: 0x000FF554 File Offset: 0x000FD754
		public bool Vertical
		{
			get
			{
				return this.vertical;
			}
		}

		// Token: 0x04001B63 RID: 7011
		private bool vertical;
	}
}
