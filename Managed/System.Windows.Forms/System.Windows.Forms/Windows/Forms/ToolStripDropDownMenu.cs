using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Provides basic functionality for the <see cref="T:System.Windows.Forms.ContextMenuStrip" /> control. Although <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> and <see cref="T:System.Windows.Forms.ToolStripDropDown" /> replace and add functionality to the <see cref="T:System.Windows.Forms.Menu" /> control of previous versions, <see cref="T:System.Windows.Forms.Menu" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000350 RID: 848
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.ToolStripDropDownDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ClassInterface(1)]
	public class ToolStripDropDownMenu : ToolStripDropDown
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" /> class. </summary>
		// Token: 0x06003CCD RID: 15565 RVA: 0x000F454C File Offset: 0x000F274C
		public ToolStripDropDownMenu()
		{
			this.layout_style = ToolStripLayoutStyle.Flow;
			this.show_image_margin = true;
		}

		/// <summary>Gets the rectangle that represents the display area of the <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the display area.</returns>
		// Token: 0x17000FDB RID: 4059
		// (get) Token: 0x06003CCE RID: 15566 RVA: 0x000F4564 File Offset: 0x000F2764
		public override Rectangle DisplayRectangle
		{
			get
			{
				return base.DisplayRectangle;
			}
		}

		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FDC RID: 4060
		// (get) Token: 0x06003CCF RID: 15567 RVA: 0x000F456C File Offset: 0x000F276C
		public override LayoutEngine LayoutEngine
		{
			get
			{
				return base.LayoutEngine;
			}
		}

		/// <summary>Gets or sets a value indicating how the items of <see cref="T:System.Windows.Forms.ContextMenuStrip" /> are displayed.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Flow" />.</returns>
		// Token: 0x17000FDD RID: 4061
		// (get) Token: 0x06003CD0 RID: 15568 RVA: 0x000F4574 File Offset: 0x000F2774
		// (set) Token: 0x06003CD1 RID: 15569 RVA: 0x000F457C File Offset: 0x000F277C
		[DefaultValue(ToolStripLayoutStyle.Flow)]
		public new ToolStripLayoutStyle LayoutStyle
		{
			get
			{
				return this.layout_style;
			}
			set
			{
				this.layout_style = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether space for a check mark is shown on the left edge of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />. </summary>
		/// <returns>true if the check margin is shown; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06003CD2 RID: 15570 RVA: 0x000F4588 File Offset: 0x000F2788
		// (set) Token: 0x06003CD3 RID: 15571 RVA: 0x000F4590 File Offset: 0x000F2790
		[DefaultValue(false)]
		public bool ShowCheckMargin
		{
			get
			{
				return this.show_check_margin;
			}
			set
			{
				if (this.show_check_margin != value)
				{
					this.show_check_margin = value;
					base.PerformLayout(this, "ShowCheckMargin");
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether space for an image is shown on the left edge of the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</summary>
		/// <returns>true if the image margin is shown; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06003CD4 RID: 15572 RVA: 0x000F45B4 File Offset: 0x000F27B4
		// (set) Token: 0x06003CD5 RID: 15573 RVA: 0x000F45BC File Offset: 0x000F27BC
		[DefaultValue(true)]
		public bool ShowImageMargin
		{
			get
			{
				return this.show_image_margin;
			}
			set
			{
				if (this.show_image_margin != value)
				{
					this.show_image_margin = value;
					base.PerformLayout(this, "ShowImageMargin");
				}
			}
		}

		/// <summary>Gets the internal spacing, in pixels, of the control.</summary>
		/// <returns>A Padding object representing the spacing.</returns>
		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06003CD6 RID: 15574 RVA: 0x000F45E0 File Offset: 0x000F27E0
		protected override Padding DefaultPadding
		{
			get
			{
				return base.DefaultPadding;
			}
		}

		/// <summary>Gets the maximum height and width, in pixels, of the <see cref="T:System.Windows.Forms.ContextMenuStrip" />.</summary>
		/// <returns>A Size object representing the height and width of the control, in pixels.</returns>
		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06003CD7 RID: 15575 RVA: 0x000F45E8 File Offset: 0x000F27E8
		protected internal override Size MaxItemSize
		{
			get
			{
				return base.Size;
			}
		}

		/// <summary>Creates a default <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> with the specified text, image, and event handler on a new <see cref="T:System.Windows.Forms.ToolStripDropDownMenu" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripMenuItem" />, or a <see cref="T:System.Windows.Forms.ToolStripSeparator" /> if the <paramref name="text" /> parameter is a hyphen (-).</returns>
		/// <param name="text">The text to use for the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />. If the <paramref name="text" /> parameter is a hyphen (-), this method creates a <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripMenuItem" />.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.ToolStripItem.Click" /> event when the <see cref="T:System.Windows.Forms.ToolStripMenuItem" /> is clicked.</param>
		// Token: 0x06003CD8 RID: 15576 RVA: 0x000F45F0 File Offset: 0x000F27F0
		protected internal override ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
		{
			return base.CreateDefaultItem(text, image, onClick);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003CD9 RID: 15577 RVA: 0x000F45FC File Offset: 0x000F27FC
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data. </param>
		// Token: 0x06003CDA RID: 15578 RVA: 0x000F4608 File Offset: 0x000F2808
		protected override void OnLayout(LayoutEventArgs e)
		{
			int num = 0;
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Available)
				{
					toolStripItem.SetPlacement(ToolStripItemPlacement.Main);
					num = Math.Max(num, toolStripItem.GetPreferredSize(Size.Empty).Width);
				}
			}
			int left = base.Padding.Left;
			if (this.show_check_margin || this.show_image_margin)
			{
				num += 68 - base.Padding.Horizontal;
			}
			else
			{
				num += 47 - base.Padding.Horizontal;
			}
			int num2 = base.Padding.Top;
			foreach (object obj2 in this.Items)
			{
				ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
				if (toolStripItem2.Available)
				{
					num2 += toolStripItem2.Margin.Top;
					Size preferredSize = toolStripItem2.GetPreferredSize(Size.Empty);
					int num3;
					if (preferredSize.Height > 22)
					{
						num3 = preferredSize.Height;
					}
					else if (toolStripItem2 is ToolStripSeparator)
					{
						num3 = 7;
					}
					else
					{
						num3 = 22;
					}
					toolStripItem2.SetBounds(new Rectangle(left, num2, num, num3));
					num2 += num3 + toolStripItem2.Margin.Bottom;
				}
			}
			base.Size = new Size(num + base.Padding.Horizontal, num2 + base.Padding.Bottom);
			this.SetDisplayedItems();
			this.OnLayoutCompleted(EventArgs.Empty);
			base.Invalidate();
		}

		/// <summary>Paints the background of the control.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06003CDB RID: 15579 RVA: 0x000F4848 File Offset: 0x000F2A48
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			Rectangle rectangle;
			rectangle..ctor(Point.Empty, base.Size);
			ToolStripRenderEventArgs toolStripRenderEventArgs = new ToolStripRenderEventArgs(e.Graphics, this, rectangle, SystemColors.Control);
			toolStripRenderEventArgs.InternalConnectedArea = this.CalculateConnectedArea();
			base.Renderer.DrawToolStripBackground(toolStripRenderEventArgs);
			if (this.ShowCheckMargin || this.ShowImageMargin)
			{
				toolStripRenderEventArgs = new ToolStripRenderEventArgs(e.Graphics, this, new Rectangle(toolStripRenderEventArgs.AffectedBounds.Location, new Size(25, toolStripRenderEventArgs.AffectedBounds.Height)), SystemColors.Control);
				base.Renderer.DrawImageMargin(toolStripRenderEventArgs);
			}
		}

		/// <summary>Resets the collection of displayed and overflow items after a layout is done.</summary>
		// Token: 0x06003CDC RID: 15580 RVA: 0x000F48F0 File Offset: 0x000F2AF0
		protected override void SetDisplayedItems()
		{
			base.SetDisplayedItems();
		}

		// Token: 0x06003CDD RID: 15581 RVA: 0x000F48F8 File Offset: 0x000F2AF8
		internal override Rectangle CalculateConnectedArea()
		{
			if (base.OwnerItem != null && !base.OwnerItem.IsOnDropDown && !(base.OwnerItem is MdiControlStrip.SystemMenuItem))
			{
				return new Rectangle(base.OwnerItem.GetCurrentParent().PointToScreen(base.OwnerItem.Location).X - base.Left, 0, base.OwnerItem.Width - 1, 2);
			}
			return base.CalculateConnectedArea();
		}

		// Token: 0x04001A86 RID: 6790
		private ToolStripLayoutStyle layout_style;

		// Token: 0x04001A87 RID: 6791
		private bool show_check_margin;

		// Token: 0x04001A88 RID: 6792
		private bool show_image_margin;
	}
}
