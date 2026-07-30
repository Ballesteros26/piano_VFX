using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderArrow" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200033E RID: 830
	public class ToolStripArrowRenderEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripArrowRenderEventArgs" /> class. </summary>
		/// <param name="g">The graphics used to paint the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</param>
		/// <param name="toolStripItem">The <see cref="T:System.Windows.Forms.ToolStripItem" /> on which to paint the arrow.</param>
		/// <param name="arrowRectangle">The bounding area of the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</param>
		/// <param name="arrowColor">The color of the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</param>
		/// <param name="arrowDirection">The direction in which the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow points.</param>
		// Token: 0x06003A8B RID: 14987 RVA: 0x000F082C File Offset: 0x000EEA2C
		public ToolStripArrowRenderEventArgs(Graphics g, ToolStripItem toolStripItem, Rectangle arrowRectangle, Color arrowColor, ArrowDirection arrowDirection)
		{
			this.graphics = g;
			this.tool_strip_item = toolStripItem;
			this.arrow_rectangle = arrowRectangle;
			this.arrow_color = arrowColor;
			this.arrow_direction = arrowDirection;
		}

		/// <summary>Gets or sets the color of the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of the arrow.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06003A8C RID: 14988 RVA: 0x000F085C File Offset: 0x000EEA5C
		// (set) Token: 0x06003A8D RID: 14989 RVA: 0x000F0864 File Offset: 0x000EEA64
		public Color ArrowColor
		{
			get
			{
				return this.arrow_color;
			}
			set
			{
				this.arrow_color = value;
			}
		}

		/// <summary>Gets or sets the bounding area of the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding area.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x06003A8E RID: 14990 RVA: 0x000F0870 File Offset: 0x000EEA70
		// (set) Token: 0x06003A8F RID: 14991 RVA: 0x000F0878 File Offset: 0x000EEA78
		public Rectangle ArrowRectangle
		{
			get
			{
				return this.arrow_rectangle;
			}
			set
			{
				this.arrow_rectangle = value;
			}
		}

		/// <summary>Gets or sets the direction in which the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow points.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ArrowDirection" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x06003A90 RID: 14992 RVA: 0x000F0884 File Offset: 0x000EEA84
		// (set) Token: 0x06003A91 RID: 14993 RVA: 0x000F088C File Offset: 0x000EEA8C
		public ArrowDirection Direction
		{
			get
			{
				return this.arrow_direction;
			}
			set
			{
				this.arrow_direction = value;
			}
		}

		/// <summary>Gets the graphics used to paint the <see cref="T:System.Windows.Forms.ToolStrip" /> arrow.</summary>
		/// <returns>The <see cref="T:System.Drawing.Graphics" /> used to paint. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x06003A92 RID: 14994 RVA: 0x000F0898 File Offset: 0x000EEA98
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripItem" /> on which to paint the arrow.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x06003A93 RID: 14995 RVA: 0x000F08A0 File Offset: 0x000EEAA0
		public ToolStripItem Item
		{
			get
			{
				return this.tool_strip_item;
			}
		}

		// Token: 0x04001A3D RID: 6717
		private Color arrow_color;

		// Token: 0x04001A3E RID: 6718
		private Rectangle arrow_rectangle;

		// Token: 0x04001A3F RID: 6719
		private ArrowDirection arrow_direction;

		// Token: 0x04001A40 RID: 6720
		private Graphics graphics;

		// Token: 0x04001A41 RID: 6721
		private ToolStripItem tool_strip_item;
	}
}
