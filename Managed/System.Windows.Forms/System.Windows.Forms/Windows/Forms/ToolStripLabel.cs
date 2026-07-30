using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms.Design;

namespace System.Windows.Forms
{
	/// <summary>Represents a nonselectable <see cref="T:System.Windows.Forms.ToolStripItem" /> that renders text and images and can display hyperlinks.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000363 RID: 867
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip)]
	public class ToolStripLabel : ToolStripItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class.</summary>
		// Token: 0x06003E3A RID: 15930 RVA: 0x000F8230 File Offset: 0x000F6430
		public ToolStripLabel()
			: this(null, null, false, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class, specifying the image to display.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		// Token: 0x06003E3B RID: 15931 RVA: 0x000F8244 File Offset: 0x000F6444
		public ToolStripLabel(Image image)
			: this(null, image, false, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class, specifying the text to display.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		// Token: 0x06003E3C RID: 15932 RVA: 0x000F8258 File Offset: 0x000F6458
		public ToolStripLabel(string text)
			: this(text, null, false, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class, specifying the text and image to display.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		// Token: 0x06003E3D RID: 15933 RVA: 0x000F826C File Offset: 0x000F646C
		public ToolStripLabel(string text, Image image)
			: this(text, image, false, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class, specifying the text and image to display and whether the <see cref="T:System.Windows.Forms.ToolStripLabel" /> acts as a link.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		/// <param name="isLink">true if the <see cref="T:System.Windows.Forms.ToolStripLabel" /> acts as a link; otherwise, false. </param>
		// Token: 0x06003E3E RID: 15934 RVA: 0x000F8280 File Offset: 0x000F6480
		public ToolStripLabel(string text, Image image, bool isLink)
			: this(text, image, isLink, null, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class, specifying the text and image to display, whether the <see cref="T:System.Windows.Forms.ToolStripLabel" /> acts as a link, and providing a <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event handler.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		/// <param name="isLink">true if the <see cref="T:System.Windows.Forms.ToolStripLabel" /> acts as a link; otherwise, false. </param>
		/// <param name="onClick">A <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event handler.</param>
		// Token: 0x06003E3F RID: 15935 RVA: 0x000F8294 File Offset: 0x000F6494
		public ToolStripLabel(string text, Image image, bool isLink, EventHandler onClick)
			: this(text, image, isLink, onClick, string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripLabel" /> class, specifying the text and image to display, whether the <see cref="T:System.Windows.Forms.ToolStripLabel" /> acts as a link, and providing a <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event handler and name for the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</summary>
		/// <param name="text">The text to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		/// <param name="isLink">true if the <see cref="T:System.Windows.Forms.ToolStripLabel" /> acts as a link; otherwise, false. </param>
		/// <param name="onClick">A <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event handler.</param>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripLabel" />.</param>
		// Token: 0x06003E40 RID: 15936 RVA: 0x000F82A8 File Offset: 0x000F64A8
		public ToolStripLabel(string text, Image image, bool isLink, EventHandler onClick, string name)
			: base(text, image, onClick, name)
		{
			this.active_link_color = Color.Red;
			this.is_link = isLink;
			this.link_behavior = LinkBehavior.SystemDefault;
			this.link_color = Color.FromArgb(0, 0, 255);
			this.link_visited = false;
			this.visited_link_color = Color.FromArgb(128, 0, 128);
		}

		// Token: 0x06003E41 RID: 15937 RVA: 0x000F830C File Offset: 0x000F650C
		// Note: this type is marked as 'beforefieldinit'.
		static ToolStripLabel()
		{
			ToolStripLabel.UIAIsLinkChangedEvent = new object();
		}

		// Token: 0x140003C3 RID: 963
		// (add) Token: 0x06003E42 RID: 15938 RVA: 0x000F8318 File Offset: 0x000F6518
		// (remove) Token: 0x06003E43 RID: 15939 RVA: 0x000F832C File Offset: 0x000F652C
		internal event EventHandler UIAIsLinkChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripLabel.UIAIsLinkChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripLabel.UIAIsLinkChangedEvent, value);
			}
		}

		// Token: 0x06003E44 RID: 15940 RVA: 0x000F8340 File Offset: 0x000F6540
		internal void OnUIAIsLinkChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripLabel.UIAIsLinkChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Gets or sets the color used to display an active link.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color to display an active link. The default color is specified by the system. Typically, this color is Color.Red.</returns>
		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x06003E45 RID: 15941 RVA: 0x000F8374 File Offset: 0x000F6574
		// (set) Token: 0x06003E46 RID: 15942 RVA: 0x000F837C File Offset: 0x000F657C
		public Color ActiveLinkColor
		{
			get
			{
				return this.active_link_color;
			}
			set
			{
				this.active_link_color = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets a value indicating the selectable state of a <see cref="T:System.Windows.Forms.ToolStripLabel" />.</summary>
		/// <returns>false in all cases.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x06003E47 RID: 15943 RVA: 0x000F838C File Offset: 0x000F658C
		public override bool CanSelect
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripLabel" /> is a hyperlink. </summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripLabel" /> is a hyperlink; otherwise, false. The default is false.</returns>
		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x06003E48 RID: 15944 RVA: 0x000F8390 File Offset: 0x000F6590
		// (set) Token: 0x06003E49 RID: 15945 RVA: 0x000F8398 File Offset: 0x000F6598
		[DefaultValue(false)]
		public bool IsLink
		{
			get
			{
				return this.is_link;
			}
			set
			{
				this.is_link = value;
				base.Invalidate();
				this.OnUIAIsLinkChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value that represents the behavior of a link.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.LinkBehavior" /> values. The default is LinkBehavior.SystemDefault.</returns>
		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x06003E4A RID: 15946 RVA: 0x000F83B4 File Offset: 0x000F65B4
		// (set) Token: 0x06003E4B RID: 15947 RVA: 0x000F83BC File Offset: 0x000F65BC
		[DefaultValue(LinkBehavior.SystemDefault)]
		public LinkBehavior LinkBehavior
		{
			get
			{
				return this.link_behavior;
			}
			set
			{
				this.link_behavior = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the color used when displaying a normal link.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to displaying a normal link. The default color is specified by the system. Typically, this color is Color.Blue.</returns>
		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x06003E4C RID: 15948 RVA: 0x000F83CC File Offset: 0x000F65CC
		// (set) Token: 0x06003E4D RID: 15949 RVA: 0x000F83D4 File Offset: 0x000F65D4
		public Color LinkColor
		{
			get
			{
				return this.link_color;
			}
			set
			{
				this.link_color = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets a value indicating whether a link should be displayed as though it were visited.</summary>
		/// <returns>true if links should display as though they were visited; otherwise, false. The default is false.</returns>
		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x06003E4E RID: 15950 RVA: 0x000F83E4 File Offset: 0x000F65E4
		// (set) Token: 0x06003E4F RID: 15951 RVA: 0x000F83EC File Offset: 0x000F65EC
		[DefaultValue(false)]
		public bool LinkVisited
		{
			get
			{
				return this.link_visited;
			}
			set
			{
				this.link_visited = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the color used when displaying a link that that has been previously visited.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to display links that have been visited. The default color is specified by the system. Typically, this color is Color.Purple.</returns>
		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x06003E50 RID: 15952 RVA: 0x000F83FC File Offset: 0x000F65FC
		// (set) Token: 0x06003E51 RID: 15953 RVA: 0x000F8404 File Offset: 0x000F6604
		public Color VisitedLinkColor
		{
			get
			{
				return this.visited_link_color;
			}
			set
			{
				this.visited_link_color = value;
				base.Invalidate();
			}
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x000F8414 File Offset: 0x000F6614
		[EditorBrowsable(2)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ToolStripItem.ToolStripItemAccessibleObject(this)
			{
				role = AccessibleRole.StaticText,
				state = AccessibleStates.ReadOnly
			};
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003E53 RID: 15955 RVA: 0x000F843C File Offset: 0x000F663C
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseEnter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003E54 RID: 15956 RVA: 0x000F8448 File Offset: 0x000F6648
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003E55 RID: 15957 RVA: 0x000F8454 File Offset: 0x000F6654
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripItem.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06003E56 RID: 15958 RVA: 0x000F8460 File Offset: 0x000F6660
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (base.Owner != null)
			{
				Color color = ((!this.Enabled) ? SystemColors.GrayText : this.ForeColor);
				Image image = ((!this.Enabled) ? ToolStripRenderer.CreateDisabledImage(this.Image) : this.Image);
				base.Owner.Renderer.DrawLabelBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
				Rectangle rectangle;
				Rectangle rectangle2;
				base.CalculateTextAndImageRectangles(out rectangle, out rectangle2);
				if (base.IsOnDropDown)
				{
					if (base.ShowMargin)
					{
						rectangle..ctor(35, rectangle.Top, rectangle.Width, rectangle.Height);
					}
					else
					{
						rectangle..ctor(7, rectangle.Top, rectangle.Width, rectangle.Height);
					}
					if (rectangle2 != Rectangle.Empty)
					{
						rectangle2..ctor(new Point(4, 3), base.GetImageSize());
					}
				}
				if (rectangle2 != Rectangle.Empty)
				{
					base.Owner.Renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, image, rectangle2));
				}
				if (rectangle != Rectangle.Empty)
				{
					if (this.is_link)
					{
						if (this.Pressed)
						{
							switch (this.link_behavior)
							{
							case LinkBehavior.SystemDefault:
							case LinkBehavior.AlwaysUnderline:
							case LinkBehavior.HoverUnderline:
								base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, rectangle, this.active_link_color, new Font(this.Font, 4), this.TextAlign));
								break;
							case LinkBehavior.NeverUnderline:
								base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, rectangle, this.active_link_color, this.Font, this.TextAlign));
								break;
							}
						}
						else if (this.Selected)
						{
							switch (this.link_behavior)
							{
							case LinkBehavior.SystemDefault:
							case LinkBehavior.AlwaysUnderline:
							case LinkBehavior.HoverUnderline:
								base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, rectangle, this.link_color, new Font(this.Font, 4), this.TextAlign));
								break;
							case LinkBehavior.NeverUnderline:
								base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, rectangle, this.link_color, this.Font, this.TextAlign));
								break;
							}
						}
						else if (this.link_visited)
						{
							switch (this.link_behavior)
							{
							case LinkBehavior.SystemDefault:
							case LinkBehavior.AlwaysUnderline:
								base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, rectangle, this.visited_link_color, new Font(this.Font, 4), this.TextAlign));
								break;
							case LinkBehavior.HoverUnderline:
							case LinkBehavior.NeverUnderline:
								base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, rectangle, this.visited_link_color, this.Font, this.TextAlign));
								break;
							}
						}
						else
						{
							switch (this.link_behavior)
							{
							case LinkBehavior.SystemDefault:
							case LinkBehavior.AlwaysUnderline:
								base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, rectangle, this.link_color, new Font(this.Font, 4), this.TextAlign));
								break;
							case LinkBehavior.HoverUnderline:
							case LinkBehavior.NeverUnderline:
								base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, rectangle, this.link_color, this.Font, this.TextAlign));
								break;
							}
						}
					}
					else
					{
						base.Owner.Renderer.DrawItemText(new ToolStripItemTextRenderEventArgs(e.Graphics, this, this.Text, rectangle, color, this.Font, this.TextAlign));
					}
				}
			}
		}

		/// <returns>true in all cases.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x06003E57 RID: 15959 RVA: 0x000F8884 File Offset: 0x000F6A84
		protected internal override bool ProcessMnemonic(char charCode)
		{
			base.Parent.SelectNextToolStripItem(this, true);
			return true;
		}

		// Token: 0x04001B0A RID: 6922
		private Color active_link_color;

		// Token: 0x04001B0B RID: 6923
		private bool is_link;

		// Token: 0x04001B0C RID: 6924
		private LinkBehavior link_behavior;

		// Token: 0x04001B0D RID: 6925
		private Color link_color;

		// Token: 0x04001B0E RID: 6926
		private bool link_visited;

		// Token: 0x04001B0F RID: 6927
		private Color visited_link_color;
	}
}
