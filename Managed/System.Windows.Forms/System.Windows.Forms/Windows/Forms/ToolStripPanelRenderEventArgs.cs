using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for <see cref="T:System.Windows.Forms.ToolStripPanel" /> drawing.</summary>
	// Token: 0x02000371 RID: 881
	public class ToolStripPanelRenderEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripPanelRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStripPanel" /> that uses the specified graphics for drawing. </summary>
		/// <param name="g">The graphics used to paint the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
		/// <param name="toolStripPanel">The <see cref="T:System.Windows.Forms.ToolStripPanel" /> to draw.</param>
		// Token: 0x06003F2F RID: 16175 RVA: 0x000FB7E8 File Offset: 0x000F99E8
		public ToolStripPanelRenderEventArgs(Graphics g, ToolStripPanel toolStripPanel)
		{
			this.graphics = g;
			this.tool_strip_panel = toolStripPanel;
			this.handled = false;
		}

		/// <summary>Gets or sets the graphics used to paint the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint.</returns>
		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06003F30 RID: 16176 RVA: 0x000FB808 File Offset: 0x000F9A08
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets or sets a value indicating whether the event was handled.</summary>
		/// <returns>true if the event was handled; otherwise, false. </returns>
		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06003F31 RID: 16177 RVA: 0x000FB810 File Offset: 0x000F9A10
		// (set) Token: 0x06003F32 RID: 16178 RVA: 0x000FB818 File Offset: 0x000F9A18
		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripPanel" /> to paint.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripPanel" /> to paint.</returns>
		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06003F33 RID: 16179 RVA: 0x000FB824 File Offset: 0x000F9A24
		public ToolStripPanel ToolStripPanel
		{
			get
			{
				return this.tool_strip_panel;
			}
		}

		// Token: 0x04001B38 RID: 6968
		private Graphics graphics;

		// Token: 0x04001B39 RID: 6969
		private bool handled;

		// Token: 0x04001B3A RID: 6970
		private ToolStripPanel tool_strip_panel;
	}
}
