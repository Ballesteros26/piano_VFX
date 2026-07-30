using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripContentPanel.RendererChanged" /> event. </summary>
	// Token: 0x02000345 RID: 837
	public class ToolStripContentPanelRenderEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripContentPanelRenderEventArgs" /> class. </summary>
		/// <param name="g">A <see cref="T:System.Drawing.Graphics" /> representing the GDI+ drawing surface.</param>
		/// <param name="contentPanel">The <see cref="T:System.Windows.Forms.ToolStripContentPanel" /> to render.</param>
		// Token: 0x06003B74 RID: 15220 RVA: 0x000F1B40 File Offset: 0x000EFD40
		public ToolStripContentPanelRenderEventArgs(Graphics g, ToolStripContentPanel contentPanel)
		{
			this.graphics = g;
			this.tool_strip_content_panel = contentPanel;
			this.handled = false;
		}

		/// <summary>Gets the object to use for drawing.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> to use for drawing.</returns>
		// Token: 0x17000F89 RID: 3977
		// (get) Token: 0x06003B75 RID: 15221 RVA: 0x000F1B60 File Offset: 0x000EFD60
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets or sets a value indicating whether the event was handled.</summary>
		/// <returns>true if the event was handled; otherwise, false. </returns>
		// Token: 0x17000F8A RID: 3978
		// (get) Token: 0x06003B76 RID: 15222 RVA: 0x000F1B68 File Offset: 0x000EFD68
		// (set) Token: 0x06003B77 RID: 15223 RVA: 0x000F1B70 File Offset: 0x000EFD70
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

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripContentPanel" /> affected by the click.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripContentPanel" />.</returns>
		// Token: 0x17000F8B RID: 3979
		// (get) Token: 0x06003B78 RID: 15224 RVA: 0x000F1B7C File Offset: 0x000EFD7C
		public ToolStripContentPanel ToolStripContentPanel
		{
			get
			{
				return this.tool_strip_content_panel;
			}
		}

		// Token: 0x04001A55 RID: 6741
		private Graphics graphics;

		// Token: 0x04001A56 RID: 6742
		private bool handled;

		// Token: 0x04001A57 RID: 6743
		private ToolStripContentPanel tool_strip_content_panel;
	}
}
