using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a panel in a <see cref="T:System.Windows.Forms.StatusStrip" /> control. </summary>
	// Token: 0x0200037D RID: 893
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.StatusStrip)]
	public class ToolStripStatusLabel : ToolStripLabel
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> class. </summary>
		// Token: 0x06004064 RID: 16484 RVA: 0x001006C4 File Offset: 0x000FE8C4
		public ToolStripStatusLabel()
			: this(string.Empty, null, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> class that displays the specified image. </summary>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> that is displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		// Token: 0x06004065 RID: 16485 RVA: 0x001006D8 File Offset: 0x000FE8D8
		public ToolStripStatusLabel(Image image)
			: this(string.Empty, image, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> class that displays the specified text.</summary>
		/// <param name="text">A <see cref="T:System.String" /> representing the text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		// Token: 0x06004066 RID: 16486 RVA: 0x001006EC File Offset: 0x000FE8EC
		public ToolStripStatusLabel(string text)
			: this(text, null, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> class that displays the specified image and text.</summary>
		/// <param name="text">A <see cref="T:System.String" /> representing the text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> that is displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		// Token: 0x06004067 RID: 16487 RVA: 0x001006FC File Offset: 0x000FE8FC
		public ToolStripStatusLabel(string text, Image image)
			: this(text, image, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> class that displays the specified image and text, and that carries out the specified action when the user clicks the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</summary>
		/// <param name="text">A <see cref="T:System.String" /> representing the text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> that is displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		/// <param name="onClick">Specifies the action to carry out when the control is clicked.</param>
		// Token: 0x06004068 RID: 16488 RVA: 0x0010070C File Offset: 0x000FE90C
		public ToolStripStatusLabel(string text, Image image, EventHandler onClick)
			: this(text, image, onClick, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> class with the specified name that displays the specified image and text, and that carries out the specified action when the user clicks the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</summary>
		/// <param name="text">A <see cref="T:System.String" /> representing the text to be displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> that is displayed on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		/// <param name="onClick">Specifies the action to carry out when the control is clicked.</param>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		// Token: 0x06004069 RID: 16489 RVA: 0x0010071C File Offset: 0x000FE91C
		public ToolStripStatusLabel(string text, Image image, EventHandler onClick, string name)
			: base(text, image, false, onClick, name)
		{
			this.border_style = Border3DStyle.Flat;
		}

		/// <summary>Gets or sets a value that determines where the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> is aligned on the <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripItemAlignment" /> values.</returns>
		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x0600406A RID: 16490 RVA: 0x00100738 File Offset: 0x000FE938
		// (set) Token: 0x0600406B RID: 16491 RVA: 0x00100740 File Offset: 0x000FE940
		[EditorBrowsable(2)]
		[Browsable(false)]
		public new ToolStripItemAlignment Alignment
		{
			get
			{
				return base.Alignment;
			}
			set
			{
				base.Alignment = value;
			}
		}

		/// <summary>Gets or sets a value that indicates which sides of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> show borders.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripStatusLabelBorderSides" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripStatusLabelBorderSides.None" />.</returns>
		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x0600406C RID: 16492 RVA: 0x0010074C File Offset: 0x000FE94C
		// (set) Token: 0x0600406D RID: 16493 RVA: 0x00100754 File Offset: 0x000FE954
		[DefaultValue(ToolStripStatusLabelBorderSides.None)]
		public ToolStripStatusLabelBorderSides BorderSides
		{
			get
			{
				return this.border_sides;
			}
			set
			{
				this.border_sides = value;
			}
		}

		/// <summary>Gets or sets the border style of the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Border3DStyle" /> values. The default is <see cref="F:System.Windows.Forms.Border3DStyle.Flat" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of <see cref="P:System.Windows.Forms.ToolStripStatusLabel.BorderStyle" /> is not one of the <see cref="T:System.Windows.Forms.Border3DStyle" /> values.</exception>
		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x0600406E RID: 16494 RVA: 0x00100760 File Offset: 0x000FE960
		// (set) Token: 0x0600406F RID: 16495 RVA: 0x00100768 File Offset: 0x000FE968
		[DefaultValue(Border3DStyle.Flat)]
		public Border3DStyle BorderStyle
		{
			get
			{
				return this.border_style;
			}
			set
			{
				this.border_style = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> automatically fills the available space on the <see cref="T:System.Windows.Forms.StatusStrip" /> as the form is resized. </summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> automatically fills the available space on the <see cref="T:System.Windows.Forms.StatusStrip" /> as the form is resized; otherwise, false. The default is false.</returns>
		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x06004070 RID: 16496 RVA: 0x00100774 File Offset: 0x000FE974
		// (set) Token: 0x06004071 RID: 16497 RVA: 0x0010077C File Offset: 0x000FE97C
		[DefaultValue(false)]
		public bool Spring
		{
			get
			{
				return this.spring;
			}
			set
			{
				if (this.spring != value)
				{
					this.spring = value;
					base.CalculateAutoSize();
				}
			}
		}

		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> representing the margin.</returns>
		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x06004072 RID: 16498 RVA: 0x00100798 File Offset: 0x000FE998
		protected internal override Padding DefaultMargin
		{
			get
			{
				return new Padding(0, 3, 0, 2);
			}
		}

		/// <summary>Retrieves the size of a rectangular area into which a <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> can be fitted.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" />, representing the width and height of a rectangle.</returns>
		/// <param name="constrainingSize">The custom-sized area for a control.</param>
		// Token: 0x06004073 RID: 16499 RVA: 0x001007A4 File Offset: 0x000FE9A4
		public override Size GetPreferredSize(Size constrainingSize)
		{
			return base.GetPreferredSize(constrainingSize);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06004074 RID: 16500 RVA: 0x001007B0 File Offset: 0x000FE9B0
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
		}

		// Token: 0x04001B6B RID: 7019
		private ToolStripStatusLabelBorderSides border_sides;

		// Token: 0x04001B6C RID: 7020
		private Border3DStyle border_style;

		// Token: 0x04001B6D RID: 7021
		private bool spring;
	}
}
