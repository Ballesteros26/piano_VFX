using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows status bar control. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002EF RID: 751
	[ClassInterface(1)]
	[ComVisible(true)]
	public class StatusStrip : ToolStrip
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.StatusStrip" /> class. </summary>
		// Token: 0x060031B9 RID: 12729 RVA: 0x000BE7C4 File Offset: 0x000BC9C4
		public StatusStrip()
		{
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			base.CanOverflow = false;
			this.GripStyle = ToolStripGripStyle.Hidden;
			base.LayoutStyle = ToolStripLayoutStyle.Table;
			base.RenderMode = ToolStripRenderMode.System;
			this.sizing_grip = true;
			base.Stretch = true;
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000314 RID: 788
		// (add) Token: 0x060031BA RID: 12730 RVA: 0x000BE80C File Offset: 0x000BCA0C
		// (remove) Token: 0x060031BB RID: 12731 RVA: 0x000BE818 File Offset: 0x000BCA18
		[Browsable(false)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		/// <summary>Gets or sets which <see cref="T:System.Windows.Forms.StatusStrip" /> borders are docked to its parent control and determines how a <see cref="T:System.Windows.Forms.StatusStrip" /> is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.Bottom" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x060031BC RID: 12732 RVA: 0x000BE824 File Offset: 0x000BCA24
		// (set) Token: 0x060031BD RID: 12733 RVA: 0x000BE82C File Offset: 0x000BCA2C
		[DefaultValue(DockStyle.Bottom)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.StatusStrip" /> supports overflow functionality.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.StatusStrip" /> supports overflow functionality; otherwise, false. The default is false.</returns>
		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x060031BE RID: 12734 RVA: 0x000BE838 File Offset: 0x000BCA38
		// (set) Token: 0x060031BF RID: 12735 RVA: 0x000BE840 File Offset: 0x000BCA40
		[DefaultValue(false)]
		[Browsable(false)]
		public new bool CanOverflow
		{
			get
			{
				return base.CanOverflow;
			}
			set
			{
				base.CanOverflow = value;
			}
		}

		/// <summary>Gets or sets the visibility of the grip used to reposition the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripGripStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripGripStyle.Hidden" />.</returns>
		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x060031C0 RID: 12736 RVA: 0x000BE84C File Offset: 0x000BCA4C
		// (set) Token: 0x060031C1 RID: 12737 RVA: 0x000BE854 File Offset: 0x000BCA54
		[DefaultValue(ToolStripGripStyle.Hidden)]
		public new ToolStripGripStyle GripStyle
		{
			get
			{
				return base.GripStyle;
			}
			set
			{
				base.GripStyle = value;
			}
		}

		/// <summary>Gets or sets a value indicating how the <see cref="T:System.Windows.Forms.StatusStrip" /> lays out the items collection.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripLayoutStyle" /> values. The default is <see cref="F:System.Windows.Forms.ToolStripLayoutStyle.Table" />.</returns>
		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x000BE860 File Offset: 0x000BCA60
		// (set) Token: 0x060031C3 RID: 12739 RVA: 0x000BE868 File Offset: 0x000BCA68
		[DefaultValue(ToolStripLayoutStyle.Table)]
		public new ToolStripLayoutStyle LayoutStyle
		{
			get
			{
				return base.LayoutStyle;
			}
			set
			{
				base.LayoutStyle = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value.</returns>
		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x000BE874 File Offset: 0x000BCA74
		// (set) Token: 0x060031C5 RID: 12741 RVA: 0x000BE87C File Offset: 0x000BCA7C
		[Browsable(false)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether ToolTips are shown for the <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
		/// <returns>true if ToolTips are shown for the <see cref="T:System.Windows.Forms.StatusStrip" />; otherwise, false. The default is false.</returns>
		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x000BE888 File Offset: 0x000BCA88
		// (set) Token: 0x060031C7 RID: 12743 RVA: 0x000BE890 File Offset: 0x000BCA90
		[DefaultValue(false)]
		public new bool ShowItemToolTips
		{
			get
			{
				return base.ShowItemToolTips;
			}
			set
			{
				base.ShowItemToolTips = value;
			}
		}

		/// <summary>Gets the boundaries of the sizing handle (grip) for a <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> representing the grip boundaries.</returns>
		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x060031C8 RID: 12744 RVA: 0x000BE89C File Offset: 0x000BCA9C
		[Browsable(false)]
		public Rectangle SizeGripBounds
		{
			get
			{
				return new Rectangle(base.Width - 12, 0, 12, base.Height);
			}
		}

		/// <summary>Gets or sets a value indicating whether a sizing handle (grip) is displayed in the lower-right corner of the control.</summary>
		/// <returns>true if a grip is displayed; otherwise, false. The default is true.</returns>
		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x060031C9 RID: 12745 RVA: 0x000BE8C0 File Offset: 0x000BCAC0
		// (set) Token: 0x060031CA RID: 12746 RVA: 0x000BE8C8 File Offset: 0x000BCAC8
		[DefaultValue(true)]
		public bool SizingGrip
		{
			get
			{
				return this.sizing_grip;
			}
			set
			{
				this.sizing_grip = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.StatusStrip" /> stretches from end to end in its container.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.StatusStrip" /> stretches from end to end in its <see cref="T:System.Windows.Forms.ToolStripContainer" />; otherwise, false. The default is true.</returns>
		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x060031CB RID: 12747 RVA: 0x000BE8D4 File Offset: 0x000BCAD4
		// (set) Token: 0x060031CC RID: 12748 RVA: 0x000BE8DC File Offset: 0x000BCADC
		[DefaultValue(true)]
		public new bool Stretch
		{
			get
			{
				return base.Stretch;
			}
			set
			{
				base.Stretch = value;
			}
		}

		/// <summary>Gets which borders of the <see cref="T:System.Windows.Forms.StatusStrip" /> are docked to the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.Bottom" />.</returns>
		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x060031CD RID: 12749 RVA: 0x000BE8E8 File Offset: 0x000BCAE8
		protected override DockStyle DefaultDock
		{
			get
			{
				return DockStyle.Bottom;
			}
		}

		/// <summary>Gets the spacing, in pixels, between the left, right, top, and bottom edges of the <see cref="T:System.Windows.Forms.StatusStrip" /> from the edges of the form.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> that represents the spacing. The default is {Left=6, Top=2, Right=0, Bottom=2}.</returns>
		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x060031CE RID: 12750 RVA: 0x000BE8EC File Offset: 0x000BCAEC
		protected override Padding DefaultPadding
		{
			get
			{
				return new Padding(1, 0, 14, 0);
			}
		}

		/// <summary>Gets a value indicating whether ToolTips are shown for the <see cref="T:System.Windows.Forms.StatusStrip" /> by default.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x060031CF RID: 12751 RVA: 0x000BE8F8 File Offset: 0x000BCAF8
		protected override bool DefaultShowItemToolTips
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the size, in pixels, of the <see cref="T:System.Windows.Forms.StatusStrip" /> when it is first created.</summary>
		/// <returns>A <see cref="M:System.Drawing.Point.#ctor(System.Drawing.Size)" /> constructor representing the size of the <see cref="T:System.Windows.Forms.StatusStrip" />, in pixels.</returns>
		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x060031D0 RID: 12752 RVA: 0x000BE8FC File Offset: 0x000BCAFC
		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 22);
			}
		}

		/// <summary>Creates a new accessibility object for the control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x060031D1 RID: 12753 RVA: 0x000BE90C File Offset: 0x000BCB0C
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new StatusStrip.StatusStripAccessibleObject();
		}

		/// <summary>Creates a default <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> with the specified text, image, and event handler on a new <see cref="T:System.Windows.Forms.StatusStrip" /> instance.</summary>
		/// <returns>A <see cref="M:System.Windows.Forms.ToolStripStatusLabel.#ctor(System.String,System.Drawing.Image,System.EventHandler)" />, or a <see cref="T:System.Windows.Forms.ToolStripSeparator" /> if the <paramref name="text" /> parameter is a hyphen (-).</returns>
		/// <param name="text">The text to use for the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />. If the <paramref name="text" /> parameter is a hyphen (-), this method creates a <see cref="T:System.Windows.Forms.ToolStripSeparator" />.</param>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to display on the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" />.</param>
		/// <param name="onClick">An event handler that raises the <see cref="E:System.Windows.Forms.Control.Click" /> event when the <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> is clicked.</param>
		// Token: 0x060031D2 RID: 12754 RVA: 0x000BE914 File Offset: 0x000BCB14
		protected internal override ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
		{
			if (text == "-")
			{
				return new ToolStripSeparator();
			}
			return new ToolStripLabel(text, image, false, onClick);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.StatusStrip" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060031D3 RID: 12755 RVA: 0x000BE938 File Offset: 0x000BCB38
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <param name="levent"></param>
		// Token: 0x060031D4 RID: 12756 RVA: 0x000BE944 File Offset: 0x000BCB44
		protected override void OnLayout(LayoutEventArgs levent)
		{
			this.OnSpringTableLayoutCore();
			base.Invalidate();
		}

		/// <summary>Paints the background of the control.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains information about the <see cref="T:System.Windows.Forms.StatusStrip" /> to paint.</param>
		// Token: 0x060031D5 RID: 12757 RVA: 0x000BE954 File Offset: 0x000BCB54
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			if (this.sizing_grip)
			{
				base.Renderer.DrawStatusStripSizingGrip(new ToolStripRenderEventArgs(e.Graphics, this, this.Bounds, SystemColors.Control));
			}
		}

		/// <summary>Provides custom table layout for a <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
		// Token: 0x060031D6 RID: 12758 RVA: 0x000BE998 File Offset: 0x000BCB98
		protected virtual void OnSpringTableLayoutCore()
		{
			if (!base.Created)
			{
				return;
			}
			ToolStripItemOverflow[] array = new ToolStripItemOverflow[this.Items.Count];
			ToolStripItemPlacement[] array2 = new ToolStripItemPlacement[this.Items.Count];
			Size size;
			size..ctor(0, this.Bounds.Height);
			int[] array3 = new int[this.Items.Count];
			int i = 0;
			int width = this.DisplayRectangle.Width;
			int num = 0;
			int num2 = 0;
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				array[num] = toolStripItem.Overflow;
				array3[num] = toolStripItem.GetPreferredSize(size).Width + toolStripItem.Margin.Horizontal;
				array2[num] = ((toolStripItem.Overflow != ToolStripItemOverflow.Always) ? ToolStripItemPlacement.Main : ToolStripItemPlacement.None);
				array2[num] = ((!toolStripItem.Available || !toolStripItem.InternalVisible) ? ToolStripItemPlacement.None : array2[num]);
				i += ((array2[num] != ToolStripItemPlacement.Main) ? 0 : array3[num]);
				if (toolStripItem is ToolStripStatusLabel && (toolStripItem as ToolStripStatusLabel).Spring)
				{
					num2++;
				}
				num++;
			}
			while (i > width)
			{
				bool flag = false;
				for (int j = array3.Length - 1; j >= 0; j--)
				{
					if (array[j] == ToolStripItemOverflow.AsNeeded && array2[j] == ToolStripItemPlacement.Main)
					{
						array2[j] = ToolStripItemPlacement.None;
						i -= array3[j];
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					for (int k = array3.Length - 1; k >= 0; k--)
					{
						if (array[k] == ToolStripItemOverflow.Never && array2[k] == ToolStripItemPlacement.Main)
						{
							array2[k] = ToolStripItemPlacement.None;
							i -= array3[k];
							flag = true;
							break;
						}
					}
				}
				if (!flag)
				{
					break;
				}
			}
			if (num2 > 0)
			{
				int num3 = (width - i) / num2;
				num = 0;
				foreach (object obj2 in this.Items)
				{
					ToolStripItem toolStripItem2 = (ToolStripItem)obj2;
					if (toolStripItem2 is ToolStripStatusLabel && (toolStripItem2 as ToolStripStatusLabel).Spring)
					{
						array3[num] += num3;
					}
					num++;
				}
			}
			num = 0;
			Point point;
			point..ctor(this.DisplayRectangle.Left, this.DisplayRectangle.Top);
			int height = this.DisplayRectangle.Height;
			foreach (object obj3 in this.Items)
			{
				ToolStripItem toolStripItem3 = (ToolStripItem)obj3;
				toolStripItem3.SetPlacement(array2[num]);
				if (array2[num] == ToolStripItemPlacement.Main)
				{
					toolStripItem3.SetBounds(new Rectangle(point.X + toolStripItem3.Margin.Left, point.Y + toolStripItem3.Margin.Top, array3[num] - toolStripItem3.Margin.Horizontal, height - toolStripItem3.Margin.Vertical));
					point.X += array3[num];
				}
				num++;
			}
			this.SetDisplayedItems();
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000BEDA4 File Offset: 0x000BCFA4
		protected override void SetDisplayedItems()
		{
			this.displayed_items.Clear();
			foreach (object obj in this.Items)
			{
				ToolStripItem toolStripItem = (ToolStripItem)obj;
				if (toolStripItem.Placement == ToolStripItemPlacement.Main && toolStripItem.Available)
				{
					this.displayed_items.AddNoOwnerOrLayout(toolStripItem);
					toolStripItem.Parent = this;
				}
			}
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		// Token: 0x060031D8 RID: 12760 RVA: 0x000BEE44 File Offset: 0x000BD044
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			if (msg != Msg.WM_MOUSEMOVE)
			{
				if (msg == Msg.WM_LBUTTONDOWN)
				{
					Point point;
					point..ctor(Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()));
					if (this.SizingGrip && this.SizeGripBounds.Contains(point))
					{
						XplatUI.SendMessage(base.FindForm().Handle, Msg.WM_NCLBUTTONDOWN, (IntPtr)17, IntPtr.Zero);
						return;
					}
				}
			}
			else if (Control.FromParamToMouseButtons((long)m.WParam.ToInt32()) == MouseButtons.None)
			{
				Point point2;
				point2..ctor(Control.LowOrder(m.LParam.ToInt32()), Control.HighOrder((long)m.LParam.ToInt32()));
				if (this.SizingGrip && this.SizeGripBounds.Contains(point2))
				{
					this.Cursor = Cursors.SizeNWSE;
					return;
				}
				this.Cursor = Cursors.Default;
			}
			base.WndProc(ref m);
		}

		// Token: 0x0400180D RID: 6157
		private bool sizing_grip;

		// Token: 0x020002F0 RID: 752
		private class StatusStripAccessibleObject : AccessibleObject
		{
		}
	}
}
