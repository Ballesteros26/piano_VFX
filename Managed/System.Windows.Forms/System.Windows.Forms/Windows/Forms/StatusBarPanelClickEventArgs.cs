using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.StatusBar.PanelClick" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002ED RID: 749
	public class StatusBarPanelClickEventArgs : MouseEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.StatusBarPanelClickEventArgs" /> class.</summary>
		/// <param name="statusBarPanel">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel that was clicked. </param>
		/// <param name="button">One of the <see cref="T:System.Windows.Forms.MouseButtons" /> values that represents the mouse buttons that were clicked while over the <see cref="T:System.Windows.Forms.StatusBarPanel" />. </param>
		/// <param name="clicks">The number of times that the mouse button was clicked. </param>
		/// <param name="x">The x-coordinate of the mouse click. </param>
		/// <param name="y">The y-coordinate of the mouse click. </param>
		// Token: 0x060031B7 RID: 12727 RVA: 0x000BE7A4 File Offset: 0x000BC9A4
		public StatusBarPanelClickEventArgs(StatusBarPanel statusBarPanel, MouseButtons button, int clicks, int x, int y)
			: base(button, clicks, x, y, 0)
		{
			this.panel = statusBarPanel;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.StatusBarPanel" /> to draw.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.StatusBarPanel" /> to draw.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x060031B8 RID: 12728 RVA: 0x000BE7BC File Offset: 0x000BC9BC
		public StatusBarPanel StatusBarPanel
		{
			get
			{
				return this.panel;
			}
		}

		// Token: 0x04001809 RID: 6153
		private StatusBarPanel panel;
	}
}
