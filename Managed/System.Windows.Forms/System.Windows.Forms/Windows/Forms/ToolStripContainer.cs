using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Provides panels on each side of the form and a central panel that can hold one or more controls.</summary>
	// Token: 0x02000342 RID: 834
	[Designer("System.Windows.Forms.Design.ToolStripContainerDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ComVisible(true)]
	[ClassInterface(1)]
	public class ToolStripContainer : ContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> class. </summary>
		// Token: 0x06003B06 RID: 15110 RVA: 0x000F13F4 File Offset: 0x000EF5F4
		public ToolStripContainer()
		{
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			this.content_panel = new ToolStripContentPanel();
			this.content_panel.Dock = DockStyle.Fill;
			this.Controls.Add(this.content_panel);
			this.top_panel = new ToolStripPanel();
			this.top_panel.Dock = DockStyle.Top;
			this.top_panel.Height = 0;
			this.Controls.Add(this.top_panel);
			this.bottom_panel = new ToolStripPanel();
			this.bottom_panel.Dock = DockStyle.Bottom;
			this.bottom_panel.Height = 0;
			this.Controls.Add(this.bottom_panel);
			this.left_panel = new ToolStripPanel();
			this.left_panel.Dock = DockStyle.Left;
			this.left_panel.Width = 0;
			this.Controls.Add(this.left_panel);
			this.right_panel = new ToolStripPanel();
			this.right_panel.Dock = DockStyle.Right;
			this.right_panel.Width = 0;
			this.Controls.Add(this.right_panel);
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x1400036E RID: 878
		// (add) Token: 0x06003B07 RID: 15111 RVA: 0x000F1514 File Offset: 0x000EF714
		// (remove) Token: 0x06003B08 RID: 15112 RVA: 0x000F1520 File Offset: 0x000EF720
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler BackColorChanged
		{
			add
			{
				base.BackColorChanged += value;
			}
			remove
			{
				base.BackColorChanged -= value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x1400036F RID: 879
		// (add) Token: 0x06003B09 RID: 15113 RVA: 0x000F152C File Offset: 0x000EF72C
		// (remove) Token: 0x06003B0A RID: 15114 RVA: 0x000F1538 File Offset: 0x000EF738
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000370 RID: 880
		// (add) Token: 0x06003B0B RID: 15115 RVA: 0x000F1544 File Offset: 0x000EF744
		// (remove) Token: 0x06003B0C RID: 15116 RVA: 0x000F1550 File Offset: 0x000EF750
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripContainer.CausesValidation" /> property changes.</summary>
		// Token: 0x14000371 RID: 881
		// (add) Token: 0x06003B0D RID: 15117 RVA: 0x000F155C File Offset: 0x000EF75C
		// (remove) Token: 0x06003B0E RID: 15118 RVA: 0x000F1568 File Offset: 0x000EF768
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler CausesValidationChanged
		{
			add
			{
				base.CausesValidationChanged += value;
			}
			remove
			{
				base.CausesValidationChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripContainer.ContextMenuStrip" /> property changes.</summary>
		// Token: 0x14000372 RID: 882
		// (add) Token: 0x06003B0F RID: 15119 RVA: 0x000F1574 File Offset: 0x000EF774
		// (remove) Token: 0x06003B10 RID: 15120 RVA: 0x000F1580 File Offset: 0x000EF780
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler ContextMenuStripChanged
		{
			add
			{
				base.ContextMenuStripChanged += value;
			}
			remove
			{
				base.ContextMenuStripChanged -= value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000373 RID: 883
		// (add) Token: 0x06003B11 RID: 15121 RVA: 0x000F158C File Offset: 0x000EF78C
		// (remove) Token: 0x06003B12 RID: 15122 RVA: 0x000F1598 File Offset: 0x000EF798
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public new event EventHandler CursorChanged
		{
			add
			{
				base.CursorChanged += value;
			}
			remove
			{
				base.CursorChanged -= value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x14000374 RID: 884
		// (add) Token: 0x06003B13 RID: 15123 RVA: 0x000F15A4 File Offset: 0x000EF7A4
		// (remove) Token: 0x06003B14 RID: 15124 RVA: 0x000F15B0 File Offset: 0x000EF7B0
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				base.ForeColorChanged += value;
			}
			remove
			{
				base.ForeColorChanged -= value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>true to enable automatic scrolling; otherwise, false. </returns>
		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06003B15 RID: 15125 RVA: 0x000F15BC File Offset: 0x000EF7BC
		// (set) Token: 0x06003B16 RID: 15126 RVA: 0x000F15C4 File Offset: 0x000EF7C4
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public override bool AutoScroll
		{
			get
			{
				return base.AutoScroll;
			}
			set
			{
				base.AutoScroll = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> value.</returns>
		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06003B17 RID: 15127 RVA: 0x000F15D0 File Offset: 0x000EF7D0
		// (set) Token: 0x06003B18 RID: 15128 RVA: 0x000F15D8 File Offset: 0x000EF7D8
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new Size AutoScrollMargin
		{
			get
			{
				return base.AutoScrollMargin;
			}
			set
			{
				base.AutoScrollMargin = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> value.</returns>
		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06003B19 RID: 15129 RVA: 0x000F15E4 File Offset: 0x000EF7E4
		// (set) Token: 0x06003B1A RID: 15130 RVA: 0x000F15EC File Offset: 0x000EF7EC
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new Size AutoScrollMinSize
		{
			get
			{
				return base.AutoScrollMinSize;
			}
			set
			{
				base.AutoScrollMinSize = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> value.</returns>
		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06003B1B RID: 15131 RVA: 0x000F15F8 File Offset: 0x000EF7F8
		// (set) Token: 0x06003B1C RID: 15132 RVA: 0x000F1600 File Offset: 0x000EF800
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public new Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06003B1D RID: 15133 RVA: 0x000F160C File Offset: 0x000EF80C
		// (set) Token: 0x06003B1E RID: 15134 RVA: 0x000F1614 File Offset: 0x000EF814
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" />.</returns>
		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06003B1F RID: 15135 RVA: 0x000F1620 File Offset: 0x000EF820
		// (set) Token: 0x06003B20 RID: 15136 RVA: 0x000F1628 File Offset: 0x000EF828
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		/// <summary>Gets the bottom panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripPanel" /> representing the bottom panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06003B21 RID: 15137 RVA: 0x000F1634 File Offset: 0x000EF834
		[DesignerSerializationVisibility(2)]
		[Localizable(false)]
		public ToolStripPanel BottomToolStripPanel
		{
			get
			{
				return this.bottom_panel;
			}
		}

		/// <summary>Gets or sets a value indicating whether the bottom panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible. </summary>
		/// <returns>true if the bottom panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible; otherwise, false. The default is true.</returns>
		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06003B22 RID: 15138 RVA: 0x000F163C File Offset: 0x000EF83C
		// (set) Token: 0x06003B23 RID: 15139 RVA: 0x000F164C File Offset: 0x000EF84C
		[DefaultValue(true)]
		public bool BottomToolStripPanelVisible
		{
			get
			{
				return this.bottom_panel.Visible;
			}
			set
			{
				this.bottom_panel.Visible = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>true if the control causes validation; otherwise, false. </returns>
		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06003B24 RID: 15140 RVA: 0x000F165C File Offset: 0x000EF85C
		// (set) Token: 0x06003B25 RID: 15141 RVA: 0x000F1664 File Offset: 0x000EF864
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public new bool CausesValidation
		{
			get
			{
				return base.CausesValidation;
			}
			set
			{
				base.CausesValidation = value;
			}
		}

		/// <summary>Gets the center panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripContentPanel" /> representing the center panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x06003B26 RID: 15142 RVA: 0x000F1670 File Offset: 0x000EF870
		[DesignerSerializationVisibility(2)]
		[Localizable(false)]
		public ToolStripContentPanel ContentPanel
		{
			get
			{
				return this.content_panel;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ContextMenuStrip" />.</returns>
		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x06003B27 RID: 15143 RVA: 0x000F1678 File Offset: 0x000EF878
		// (set) Token: 0x06003B28 RID: 15144 RVA: 0x000F1680 File Offset: 0x000EF880
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return base.ContextMenuStrip;
			}
			set
			{
				base.ContextMenuStrip = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control.ControlCollection" />.</returns>
		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x06003B29 RID: 15145 RVA: 0x000F168C File Offset: 0x000EF88C
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new Control.ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" />.</returns>
		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x06003B2A RID: 15146 RVA: 0x000F1694 File Offset: 0x000EF894
		// (set) Token: 0x06003B2B RID: 15147 RVA: 0x000F169C File Offset: 0x000EF89C
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override Cursor Cursor
		{
			get
			{
				return base.Cursor;
			}
			set
			{
				base.Cursor = value;
			}
		}

		/// <summary>This property is not relevant for this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x06003B2C RID: 15148 RVA: 0x000F16A8 File Offset: 0x000EF8A8
		// (set) Token: 0x06003B2D RID: 15149 RVA: 0x000F16B0 File Offset: 0x000EF8B0
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		/// <summary>Gets the left panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripPanel" /> representing the left panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x06003B2E RID: 15150 RVA: 0x000F16BC File Offset: 0x000EF8BC
		[DesignerSerializationVisibility(2)]
		[Localizable(false)]
		public ToolStripPanel LeftToolStripPanel
		{
			get
			{
				return this.left_panel;
			}
		}

		/// <summary>Gets or sets a value indicating whether the left panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible.</summary>
		/// <returns>true if the left panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible; otherwise, false. The default is true.</returns>
		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06003B2F RID: 15151 RVA: 0x000F16C4 File Offset: 0x000EF8C4
		// (set) Token: 0x06003B30 RID: 15152 RVA: 0x000F16D4 File Offset: 0x000EF8D4
		[DefaultValue(true)]
		public bool LeftToolStripPanelVisible
		{
			get
			{
				return this.left_panel.Visible;
			}
			set
			{
				this.left_panel.Visible = value;
			}
		}

		/// <summary>Gets the right panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripPanel" /> representing the right panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06003B31 RID: 15153 RVA: 0x000F16E4 File Offset: 0x000EF8E4
		[Localizable(false)]
		[DesignerSerializationVisibility(2)]
		public ToolStripPanel RightToolStripPanel
		{
			get
			{
				return this.right_panel;
			}
		}

		/// <summary>Gets or sets a value indicating whether the right panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible.</summary>
		/// <returns>true if the right panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible; otherwise, false. The default is true.</returns>
		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06003B32 RID: 15154 RVA: 0x000F16EC File Offset: 0x000EF8EC
		// (set) Token: 0x06003B33 RID: 15155 RVA: 0x000F16FC File Offset: 0x000EF8FC
		[DefaultValue(true)]
		public bool RightToolStripPanelVisible
		{
			get
			{
				return this.right_panel.Visible;
			}
			set
			{
				this.right_panel.Visible = value;
			}
		}

		/// <summary>Gets the top panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripPanel" /> representing the top panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" />.</returns>
		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x06003B34 RID: 15156 RVA: 0x000F170C File Offset: 0x000EF90C
		[Localizable(false)]
		[DesignerSerializationVisibility(2)]
		public ToolStripPanel TopToolStripPanel
		{
			get
			{
				return this.top_panel;
			}
		}

		/// <summary>Gets or sets a value indicating whether the top panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible.</summary>
		/// <returns>true if the top panel of the <see cref="T:System.Windows.Forms.ToolStripContainer" /> is visible; otherwise, false. The default is true.</returns>
		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x06003B35 RID: 15157 RVA: 0x000F1714 File Offset: 0x000EF914
		// (set) Token: 0x06003B36 RID: 15158 RVA: 0x000F1724 File Offset: 0x000EF924
		[DefaultValue(true)]
		public bool TopToolStripPanelVisible
		{
			get
			{
				return this.top_panel.Visible;
			}
			set
			{
				this.top_panel.Visible = value;
			}
		}

		/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.ToolStripContainer" />, in pixels.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the horizontal and vertical dimensions of the <see cref="T:System.Windows.Forms.ToolStripContainer" />, in pixels.</returns>
		// Token: 0x17000F77 RID: 3959
		// (get) Token: 0x06003B37 RID: 15159 RVA: 0x000F1734 File Offset: 0x000EF934
		protected override Size DefaultSize
		{
			get
			{
				return new Size(150, 175);
			}
		}

		/// <summary>Creates and returns a <see cref="T:System.Windows.Forms.ToolStripContainer" /> collection.</summary>
		/// <returns>A read-only <see cref="T:System.Windows.Forms.ToolStripContainer" /> collection.</returns>
		// Token: 0x06003B38 RID: 15160 RVA: 0x000F1748 File Offset: 0x000EF948
		[EditorBrowsable(2)]
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new ToolStripContainer.ToolStripContainerTypedControlCollection(this);
		}

		// Token: 0x06003B39 RID: 15161 RVA: 0x000F1750 File Offset: 0x000EF950
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003B3A RID: 15162 RVA: 0x000F175C File Offset: 0x000EF95C
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
		}

		// Token: 0x04001A4C RID: 6732
		private ToolStripPanel bottom_panel;

		// Token: 0x04001A4D RID: 6733
		private ToolStripContentPanel content_panel;

		// Token: 0x04001A4E RID: 6734
		private ToolStripPanel left_panel;

		// Token: 0x04001A4F RID: 6735
		private ToolStripPanel right_panel;

		// Token: 0x04001A50 RID: 6736
		private ToolStripPanel top_panel;

		// Token: 0x02000343 RID: 835
		private class ToolStripContainerTypedControlCollection : Control.ControlCollection
		{
			// Token: 0x06003B3B RID: 15163 RVA: 0x000F1768 File Offset: 0x000EF968
			public ToolStripContainerTypedControlCollection(Control owner)
				: base(owner)
			{
			}
		}
	}
}
