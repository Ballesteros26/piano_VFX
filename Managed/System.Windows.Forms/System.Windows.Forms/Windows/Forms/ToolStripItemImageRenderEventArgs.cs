using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderItemImage" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200035D RID: 861
	public class ToolStripItemImageRenderEventArgs : ToolStripItemRenderEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> within the specified space and that has the specified properties.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to paint the image.</param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" />.</param>
		/// <param name="imageRectangle">The bounding area of the image.</param>
		// Token: 0x06003E24 RID: 15908 RVA: 0x000F7FCC File Offset: 0x000F61CC
		public ToolStripItemImageRenderEventArgs(Graphics g, ToolStripItem item, Rectangle imageRectangle)
			: this(g, item, null, imageRectangle)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemImageRenderEventArgs" /> class for the specified <see cref="T:System.Windows.Forms.ToolStripItem" /> that displays an image within the specified space and that has the specified properties. </summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to paint the image.</param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> on which to draw the image.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to paint.</param>
		/// <param name="imageRectangle">The bounding area of the image.</param>
		// Token: 0x06003E25 RID: 15909 RVA: 0x000F7FD8 File Offset: 0x000F61D8
		public ToolStripItemImageRenderEventArgs(Graphics g, ToolStripItem item, Image image, Rectangle imageRectangle)
			: base(g, item)
		{
			this.image = image;
			this.image_rectangle = imageRectangle;
		}

		/// <summary>Gets the image painted on the <see cref="T:System.Windows.Forms.ToolStrip" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> painted on the <see cref="T:System.Windows.Forms.ToolStrip" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x06003E26 RID: 15910 RVA: 0x000F7FF4 File Offset: 0x000F61F4
		public Image Image
		{
			get
			{
				return this.image;
			}
		}

		/// <summary>Gets the rectangle that represents the bounding area of the image.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounding area of the image.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x06003E27 RID: 15911 RVA: 0x000F7FFC File Offset: 0x000F61FC
		public Rectangle ImageRectangle
		{
			get
			{
				return this.image_rectangle;
			}
		}

		// Token: 0x04001AF5 RID: 6901
		private Image image;

		// Token: 0x04001AF6 RID: 6902
		private Rectangle image_rectangle;
	}
}
