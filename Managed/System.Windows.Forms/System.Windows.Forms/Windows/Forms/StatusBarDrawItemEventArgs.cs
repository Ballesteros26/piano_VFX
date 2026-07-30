using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.StatusBar.DrawItem" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002E9 RID: 745
	public class StatusBarDrawItemEventArgs : DrawItemEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.StatusBarDrawItemEventArgs" /> class without specifying a background and foreground color for the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to use to draw the <see cref="T:System.Windows.Forms.StatusBarPanel" />. </param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> used to render text. </param>
		/// <param name="r">The <see cref="T:System.Drawing.Rectangle" /> that represents the client area of the <see cref="T:System.Windows.Forms.StatusBarPanel" />. </param>
		/// <param name="itemId">The zero-based index of the panel in the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> of the <see cref="T:System.Windows.Forms.StatusBar" /> control. </param>
		/// <param name="itemState">One of the <see cref="T:System.Windows.Forms.DrawItemState" /> values that represents state information about the <see cref="T:System.Windows.Forms.StatusBarPanel" />. </param>
		/// <param name="panel">A <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel to draw. </param>
		// Token: 0x06003190 RID: 12688 RVA: 0x000BE430 File Offset: 0x000BC630
		public StatusBarDrawItemEventArgs(Graphics g, Font font, Rectangle r, int itemId, DrawItemState itemState, StatusBarPanel panel)
			: this(g, font, r, itemId, itemState, panel, Control.DefaultForeColor, Control.DefaultBackColor)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.StatusBarDrawItemEventArgs" /> class with a specified background and foreground color for the <see cref="T:System.Windows.Forms.StatusBarPanel" />.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> to use to draw the <see cref="T:System.Windows.Forms.StatusBarPanel" />. </param>
		/// <param name="font">The <see cref="T:System.Drawing.Font" /> used to render text. </param>
		/// <param name="r">The <see cref="T:System.Drawing.Rectangle" /> that represents the client area of the <see cref="T:System.Windows.Forms.StatusBarPanel" />. </param>
		/// <param name="itemId">The zero-based index of the panel in the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> of the <see cref="T:System.Windows.Forms.StatusBar" /> control. </param>
		/// <param name="itemState">One of the <see cref="T:System.Windows.Forms.DrawItemState" /> values that represents state information about the <see cref="T:System.Windows.Forms.StatusBarPanel" />. </param>
		/// <param name="panel">A <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel to draw. </param>
		/// <param name="foreColor">One of the <see cref="T:System.Drawing.Color" /> values that represents the foreground color of the panel to draw. </param>
		/// <param name="backColor">One of the <see cref="T:System.Drawing.Color" /> values that represents the background color of the panel to draw. </param>
		// Token: 0x06003191 RID: 12689 RVA: 0x000BE458 File Offset: 0x000BC658
		public StatusBarDrawItemEventArgs(Graphics g, Font font, Rectangle r, int itemId, DrawItemState itemState, StatusBarPanel panel, Color foreColor, Color backColor)
			: base(g, font, r, itemId, itemState)
		{
			this.panel = panel;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.StatusBarPanel" /> to draw.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.StatusBarPanel" /> to draw.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06003192 RID: 12690 RVA: 0x000BE470 File Offset: 0x000BC670
		public StatusBarPanel Panel
		{
			get
			{
				return this.panel;
			}
		}

		// Token: 0x040017F1 RID: 6129
		private StatusBarPanel panel;
	}
}
