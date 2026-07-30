using System;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.ToolStripRenderer.RenderItemText" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000362 RID: 866
	public class ToolStripItemTextRenderEventArgs : ToolStripItemRenderEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemTextRenderEventArgs" /> class with the specified text and text properties. </summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text.</param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> on which to draw the text.</param>
		/// <param name="text">The text to be drawn.</param>
		/// <param name="textRectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds to draw the text in.</param>
		/// <param name="textColor">The <see cref="T:System.Drawing.Color" /> used to draw the text.</param>
		/// <param name="textFont">The <see cref="T:System.Drawing.Font" /> used to draw the text.</param>
		/// <param name="textAlign">The <see cref="T:System.Drawing.ContentAlignment" /> that specifies the vertical and horizontal alignment of the text in the bounding area.</param>
		// Token: 0x06003E2C RID: 15916 RVA: 0x000F803C File Offset: 0x000F623C
		public ToolStripItemTextRenderEventArgs(Graphics g, ToolStripItem item, string text, Rectangle textRectangle, Color textColor, Font textFont, ContentAlignment textAlign)
			: base(g, item)
		{
			this.text = text;
			this.text_rectangle = textRectangle;
			this.text_color = textColor;
			this.text_font = textFont;
			this.text_direction = item.TextDirection;
			switch (textAlign)
			{
			case 1:
				this.text_format = TextFormatFlags.Left;
				break;
			case 2:
				this.text_format = TextFormatFlags.HorizontalCenter;
				break;
			default:
				if (textAlign != 16)
				{
					if (textAlign == 32)
					{
						this.text_format = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
						break;
					}
					if (textAlign == 64)
					{
						this.text_format = TextFormatFlags.Right | TextFormatFlags.VerticalCenter;
						break;
					}
					if (textAlign == 256)
					{
						this.text_format = TextFormatFlags.Bottom;
						break;
					}
					if (textAlign == 512)
					{
						this.text_format = TextFormatFlags.HorizontalCenter | TextFormatFlags.Bottom;
						break;
					}
					if (textAlign == 1024)
					{
						this.text_format = TextFormatFlags.Right | TextFormatFlags.Bottom;
						break;
					}
				}
				this.text_format = TextFormatFlags.VerticalCenter;
				break;
			case 4:
				this.text_format = TextFormatFlags.Right;
				break;
			}
			if ((Application.KeyboardCapture == null || !ToolStripManager.ActivatedByKeyboard) && !SystemInformation.MenuAccessKeysUnderlined)
			{
				this.text_format |= TextFormatFlags.HidePrefix;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripItemTextRenderEventArgs" /> class with the specified text and text properties format.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> used to draw the text.</param>
		/// <param name="item">The <see cref="T:System.Windows.Forms.ToolStripItem" /> on which to draw the text.</param>
		/// <param name="text">The text to be drawn.</param>
		/// <param name="textRectangle">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds to draw the text in.</param>
		/// <param name="textColor">The <see cref="T:System.Drawing.Color" /> used to draw the text.</param>
		/// <param name="textFont">The <see cref="T:System.Drawing.Font" /> used to draw the text.</param>
		/// <param name="format">The display and layout information for text strings.</param>
		// Token: 0x06003E2D RID: 15917 RVA: 0x000F8174 File Offset: 0x000F6374
		public ToolStripItemTextRenderEventArgs(Graphics g, ToolStripItem item, string text, Rectangle textRectangle, Color textColor, Font textFont, TextFormatFlags format)
			: base(g, item)
		{
			this.text = text;
			this.text_rectangle = textRectangle;
			this.text_color = textColor;
			this.text_font = textFont;
			this.text_format = format;
			this.text_direction = ToolStripTextDirection.Horizontal;
		}

		/// <summary>Gets or sets the text to be drawn on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A string that represents the text to be painted on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x06003E2E RID: 15918 RVA: 0x000F81B8 File Offset: 0x000F63B8
		// (set) Token: 0x06003E2F RID: 15919 RVA: 0x000F81C0 File Offset: 0x000F63C0
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		/// <summary>Gets or sets the color of the <see cref="T:System.Windows.Forms.ToolStripItem" /> text. </summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color of the <see cref="T:System.Windows.Forms.ToolStripItem" /> text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x06003E30 RID: 15920 RVA: 0x000F81CC File Offset: 0x000F63CC
		// (set) Token: 0x06003E31 RID: 15921 RVA: 0x000F81D4 File Offset: 0x000F63D4
		public Color TextColor
		{
			get
			{
				return this.text_color;
			}
			set
			{
				this.text_color = value;
			}
		}

		/// <summary>Gets or sets whether the text is drawn vertically or horizontally.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripTextDirection" /> values. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x06003E32 RID: 15922 RVA: 0x000F81E0 File Offset: 0x000F63E0
		// (set) Token: 0x06003E33 RID: 15923 RVA: 0x000F81E8 File Offset: 0x000F63E8
		public ToolStripTextDirection TextDirection
		{
			get
			{
				return this.text_direction;
			}
			set
			{
				this.text_direction = value;
			}
		}

		/// <summary>Gets or sets the font of the text drawn on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> of the text drawn on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x06003E34 RID: 15924 RVA: 0x000F81F4 File Offset: 0x000F63F4
		// (set) Token: 0x06003E35 RID: 15925 RVA: 0x000F81FC File Offset: 0x000F63FC
		public Font TextFont
		{
			get
			{
				return this.text_font;
			}
			set
			{
				this.text_font = value;
			}
		}

		/// <summary>Gets or sets the display and layout information of the text drawn on the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.TextFormatFlags" /> values that specify the display and layout information of the drawn text. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x06003E36 RID: 15926 RVA: 0x000F8208 File Offset: 0x000F6408
		// (set) Token: 0x06003E37 RID: 15927 RVA: 0x000F8210 File Offset: 0x000F6410
		public TextFormatFlags TextFormat
		{
			get
			{
				return this.text_format;
			}
			set
			{
				this.text_format = value;
			}
		}

		/// <summary>Gets or sets the rectangle that represents the bounds to draw the text in.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds to draw the text in.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x06003E38 RID: 15928 RVA: 0x000F821C File Offset: 0x000F641C
		// (set) Token: 0x06003E39 RID: 15929 RVA: 0x000F8224 File Offset: 0x000F6424
		public Rectangle TextRectangle
		{
			get
			{
				return this.text_rectangle;
			}
			set
			{
				this.text_rectangle = value;
			}
		}

		// Token: 0x04001B04 RID: 6916
		private string text;

		// Token: 0x04001B05 RID: 6917
		private Color text_color;

		// Token: 0x04001B06 RID: 6918
		private ToolStripTextDirection text_direction;

		// Token: 0x04001B07 RID: 6919
		private Font text_font;

		// Token: 0x04001B08 RID: 6920
		private TextFormatFlags text_format;

		// Token: 0x04001B09 RID: 6921
		private Rectangle text_rectangle;
	}
}
