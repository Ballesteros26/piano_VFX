using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	/// <summary>Creates a container within which other controls can share horizontal or vertical space.</summary>
	// Token: 0x0200036D RID: 877
	[Designer("System.Windows.Forms.Design.ToolStripPanelDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ComVisible(true)]
	[ToolboxBitmap("")]
	[ClassInterface(1)]
	public class ToolStripPanel : ContainerControl, IDisposable, IComponent, IBindableComponent, IDropTarget
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripPanel" /> class. </summary>
		// Token: 0x06003ED6 RID: 16086 RVA: 0x000FAC2C File Offset: 0x000F8E2C
		public ToolStripPanel()
		{
			base.AutoSize = true;
			this.locked = false;
			this.renderer = null;
			this.render_mode = ToolStripRenderMode.ManagerRenderMode;
			this.row_margin = new Padding(3, 0, 0, 0);
			this.rows = new ToolStripPanel.ToolStripPanelRowCollection(this);
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x000FAC78 File Offset: 0x000F8E78
		// Note: this type is marked as 'beforefieldinit'.
		static ToolStripPanel()
		{
			ToolStripPanel.RendererChangedEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripPanel.AutoSize" /> property changes. </summary>
		// Token: 0x140003CA RID: 970
		// (add) Token: 0x06003ED8 RID: 16088 RVA: 0x000FAC84 File Offset: 0x000F8E84
		// (remove) Token: 0x06003ED9 RID: 16089 RVA: 0x000FAC90 File Offset: 0x000F8E90
		[Browsable(true)]
		[EditorBrowsable(0)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripPanel.Renderer" /> property changes.</summary>
		// Token: 0x140003CB RID: 971
		// (add) Token: 0x06003EDA RID: 16090 RVA: 0x000FAC9C File Offset: 0x000F8E9C
		// (remove) Token: 0x06003EDB RID: 16091 RVA: 0x000FACB0 File Offset: 0x000F8EB0
		public event EventHandler RendererChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripPanel.RendererChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripPanel.RendererChangedEvent, value);
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x140003CC RID: 972
		// (add) Token: 0x06003EDC RID: 16092 RVA: 0x000FACC4 File Offset: 0x000F8EC4
		// (remove) Token: 0x06003EDD RID: 16093 RVA: 0x000FACD0 File Offset: 0x000F8ED0
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler TabIndexChanged
		{
			add
			{
				base.TabIndexChanged += value;
			}
			remove
			{
				base.TabIndexChanged -= value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x140003CD RID: 973
		// (add) Token: 0x06003EDE RID: 16094 RVA: 0x000FACDC File Offset: 0x000F8EDC
		// (remove) Token: 0x06003EDF RID: 16095 RVA: 0x000FACE8 File Offset: 0x000F8EE8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler TabStopChanged
		{
			add
			{
				base.TabStopChanged += value;
			}
			remove
			{
				base.TabStopChanged -= value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x140003CE RID: 974
		// (add) Token: 0x06003EE0 RID: 16096 RVA: 0x000FACF4 File Offset: 0x000F8EF4
		// (remove) Token: 0x06003EE1 RID: 16097 RVA: 0x000FAD00 File Offset: 0x000F8F00
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06003EE2 RID: 16098 RVA: 0x000FAD0C File Offset: 0x000F8F0C
		// (set) Token: 0x06003EE3 RID: 16099 RVA: 0x000FAD14 File Offset: 0x000F8F14
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override bool AllowDrop
		{
			get
			{
				return base.AllowDrop;
			}
			set
			{
				base.AllowDrop = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x06003EE4 RID: 16100 RVA: 0x000FAD20 File Offset: 0x000F8F20
		// (set) Token: 0x06003EE5 RID: 16101 RVA: 0x000FAD28 File Offset: 0x000F8F28
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		// Token: 0x1700106B RID: 4203
		// (get) Token: 0x06003EE6 RID: 16102 RVA: 0x000FAD34 File Offset: 0x000F8F34
		// (set) Token: 0x06003EE7 RID: 16103 RVA: 0x000FAD3C File Offset: 0x000F8F3C
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		// Token: 0x1700106C RID: 4204
		// (get) Token: 0x06003EE8 RID: 16104 RVA: 0x000FAD48 File Offset: 0x000F8F48
		// (set) Token: 0x06003EE9 RID: 16105 RVA: 0x000FAD50 File Offset: 0x000F8F50
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
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

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripPanel" /> automatically adjusts its size when the form is resized.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripPanel" /> automatically resizes; otherwise, false. The default is true.</returns>
		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x06003EEA RID: 16106 RVA: 0x000FAD5C File Offset: 0x000F8F5C
		// (set) Token: 0x06003EEB RID: 16107 RVA: 0x000FAD64 File Offset: 0x000F8F64
		[DefaultValue(true)]
		[DesignerSerializationVisibility(1)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06003EEC RID: 16108 RVA: 0x000FAD70 File Offset: 0x000F8F70
		// (set) Token: 0x06003EED RID: 16109 RVA: 0x000FAD78 File Offset: 0x000F8F78
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
				switch (value)
				{
				case DockStyle.None:
				case DockStyle.Top:
				case DockStyle.Bottom:
					this.orientation = Orientation.Horizontal;
					break;
				case DockStyle.Left:
				case DockStyle.Right:
					this.orientation = Orientation.Vertical;
					break;
				}
			}
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x06003EEE RID: 16110 RVA: 0x000FADC8 File Offset: 0x000F8FC8
		public override LayoutEngine LayoutEngine
		{
			get
			{
				if (this.layout_engine == null)
				{
					this.layout_engine = new FlowLayout();
				}
				return this.layout_engine;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripPanel" /> can be moved or resized.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripPanel" /> can be moved or resized; otherwise, false. The default is false.</returns>
		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x06003EEF RID: 16111 RVA: 0x000FADE8 File Offset: 0x000F8FE8
		// (set) Token: 0x06003EF0 RID: 16112 RVA: 0x000FADF0 File Offset: 0x000F8FF0
		[EditorBrowsable(2)]
		[DefaultValue(false)]
		[Browsable(false)]
		public bool Locked
		{
			get
			{
				return this.locked;
			}
			set
			{
				this.locked = value;
			}
		}

		/// <summary>Gets or sets a value indicating the horizontal or vertical orientation of the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Orientation" /> values.</returns>
		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06003EF1 RID: 16113 RVA: 0x000FADFC File Offset: 0x000F8FFC
		// (set) Token: 0x06003EF2 RID: 16114 RVA: 0x000FAE04 File Offset: 0x000F9004
		public Orientation Orientation
		{
			get
			{
				return this.orientation;
			}
			set
			{
				this.orientation = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Windows.Forms.ToolStripRenderer" /> used to customize the appearance of a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripRenderer" /> that handles painting.</returns>
		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x06003EF3 RID: 16115 RVA: 0x000FAE10 File Offset: 0x000F9010
		// (set) Token: 0x06003EF4 RID: 16116 RVA: 0x000FAE2C File Offset: 0x000F902C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public ToolStripRenderer Renderer
		{
			get
			{
				if (this.render_mode == ToolStripRenderMode.ManagerRenderMode)
				{
					return ToolStripManager.Renderer;
				}
				return this.renderer;
			}
			set
			{
				if (this.renderer != value)
				{
					this.renderer = value;
					this.render_mode = ToolStripRenderMode.Custom;
					this.OnRendererChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the painting styles to be applied to the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolStripRenderMode" /> values.</returns>
		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x06003EF5 RID: 16117 RVA: 0x000FAE54 File Offset: 0x000F9054
		// (set) Token: 0x06003EF6 RID: 16118 RVA: 0x000FAE5C File Offset: 0x000F905C
		public ToolStripRenderMode RenderMode
		{
			get
			{
				return this.render_mode;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ToolStripRenderMode), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ToolStripRenderMode", value));
				}
				if (value == ToolStripRenderMode.Custom && this.renderer == null)
				{
					throw new NotSupportedException("Must set Renderer property before setting RenderMode to Custom");
				}
				if (value == ToolStripRenderMode.Professional || value == ToolStripRenderMode.System)
				{
					this.Renderer = new ToolStripProfessionalRenderer();
				}
				this.render_mode = value;
			}
		}

		/// <summary>Gets or sets the spacing, in pixels, between the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />s and the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing, in pixels.</returns>
		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x06003EF7 RID: 16119 RVA: 0x000FAED8 File Offset: 0x000F90D8
		// (set) Token: 0x06003EF8 RID: 16120 RVA: 0x000FAEE0 File Offset: 0x000F90E0
		public Padding RowMargin
		{
			get
			{
				return this.row_margin;
			}
			set
			{
				this.row_margin = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />s in this <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> representing the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />s in this <see cref="T:System.Windows.Forms.ToolStripPanel" />.</returns>
		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x06003EF9 RID: 16121 RVA: 0x000FAEEC File Offset: 0x000F90EC
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public ToolStripPanelRow[] Rows
		{
			get
			{
				ToolStripPanelRow[] array = new ToolStripPanelRow[this.rows.Count];
				this.rows.CopyTo(array, 0);
				return array;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the tab index.</returns>
		// Token: 0x17001076 RID: 4214
		// (get) Token: 0x06003EFA RID: 16122 RVA: 0x000FAF18 File Offset: 0x000F9118
		// (set) Token: 0x06003EFB RID: 16123 RVA: 0x000FAF20 File Offset: 0x000F9120
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new int TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if enabled; otherwise, false.</returns>
		// Token: 0x17001077 RID: 4215
		// (get) Token: 0x06003EFC RID: 16124 RVA: 0x000FAF2C File Offset: 0x000F912C
		// (set) Token: 0x06003EFD RID: 16125 RVA: 0x000FAF34 File Offset: 0x000F9134
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the display text.</returns>
		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06003EFE RID: 16126 RVA: 0x000FAF40 File Offset: 0x000F9140
		// (set) Token: 0x06003EFF RID: 16127 RVA: 0x000FAF48 File Offset: 0x000F9148
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06003F00 RID: 16128 RVA: 0x000FAF54 File Offset: 0x000F9154
		protected override Padding DefaultMargin
		{
			get
			{
				return new Padding(0);
			}
		}

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06003F01 RID: 16129 RVA: 0x000FAF5C File Offset: 0x000F915C
		protected override Padding DefaultPadding
		{
			get
			{
				return new Padding(0);
			}
		}

		/// <summary>Begins the initialization of a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		// Token: 0x06003F02 RID: 16130 RVA: 0x000FAF64 File Offset: 0x000F9164
		public void BeginInit()
		{
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		// Token: 0x06003F03 RID: 16131 RVA: 0x000FAF68 File Offset: 0x000F9168
		public void EndInit()
		{
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ToolStrip" /> to a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		/// <param name="toolStripToDrag">The <see cref="T:System.Windows.Forms.ToolStrip" /> to add to the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
		// Token: 0x06003F04 RID: 16132 RVA: 0x000FAF6C File Offset: 0x000F916C
		[MonoTODO("Not implemented")]
		public void Join(ToolStrip toolStripToDrag)
		{
			if (!base.Contains(toolStripToDrag))
			{
				base.Controls.Add(toolStripToDrag);
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ToolStrip" /> to a <see cref="T:System.Windows.Forms.ToolStripPanel" /> in the specified row.</summary>
		/// <param name="toolStripToDrag">The <see cref="T:System.Windows.Forms.ToolStrip" /> to add to the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
		/// <param name="row">An <see cref="T:System.Int32" /> representing the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to which the <see cref="T:System.Windows.Forms.ToolStrip" /> is added.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="row" /> parameter is less than zero (0).</exception>
		// Token: 0x06003F05 RID: 16133 RVA: 0x000FAF88 File Offset: 0x000F9188
		[MonoTODO("Not implemented")]
		public void Join(ToolStrip toolStripToDrag, int row)
		{
			this.Join(toolStripToDrag);
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ToolStrip" /> to a <see cref="T:System.Windows.Forms.ToolStripPanel" /> at the specified location.</summary>
		/// <param name="toolStripToDrag">The <see cref="T:System.Windows.Forms.ToolStrip" /> to add to the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
		/// <param name="location">A <see cref="T:System.Drawing.Point" /> value representing the x- and y-client coordinates, in pixels, of the new location for the <see cref="T:System.Windows.Forms.ToolStrip" />.</param>
		// Token: 0x06003F06 RID: 16134 RVA: 0x000FAF94 File Offset: 0x000F9194
		[MonoTODO("Not implemented")]
		public void Join(ToolStrip toolStripToDrag, Point location)
		{
			this.Join(toolStripToDrag);
		}

		/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ToolStrip" /> to a <see cref="T:System.Windows.Forms.ToolStripPanel" /> at the specified coordinates.</summary>
		/// <param name="toolStripToDrag">The <see cref="T:System.Windows.Forms.ToolStrip" /> to add to the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
		/// <param name="x">The horizontal client coordinate, in pixels.</param>
		/// <param name="y">The vertical client coordinate, in pixels.</param>
		// Token: 0x06003F07 RID: 16135 RVA: 0x000FAFA0 File Offset: 0x000F91A0
		[MonoTODO("Not implemented")]
		public void Join(ToolStrip toolStripToDrag, int x, int y)
		{
			this.Join(toolStripToDrag);
		}

		/// <summary>Retrieves the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> given a point within the <see cref="T:System.Windows.Forms.ToolStripPanel" /> client area.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> that contains the <paramref name="raftingContainerPoint" />, or null if no such <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> exists.</returns>
		/// <param name="clientLocation">A <see cref="T:System.Drawing.Point" /> used as a reference to find the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</param>
		// Token: 0x06003F08 RID: 16136 RVA: 0x000FAFAC File Offset: 0x000F91AC
		public ToolStripPanelRow PointToRow(Point clientLocation)
		{
			foreach (object obj in this.rows)
			{
				ToolStripPanelRow toolStripPanelRow = (ToolStripPanelRow)obj;
				if (toolStripPanelRow.Bounds.Contains(clientLocation))
				{
					return toolStripPanelRow;
				}
			}
			return null;
		}

		/// <summary>Retrieves a collection of <see cref="T:System.Windows.Forms.ToolStripPanel" /> controls.</summary>
		/// <returns>A collection of <see cref="T:System.Windows.Forms.ToolStripPanel" /> controls.</returns>
		// Token: 0x06003F09 RID: 16137 RVA: 0x000FB038 File Offset: 0x000F9238
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new ToolStripPanel.ToolStripPanelControlCollection(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripPanel" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003F0A RID: 16138 RVA: 0x000FB040 File Offset: 0x000F9240
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.ControlAdded" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data.</param>
		// Token: 0x06003F0B RID: 16139 RVA: 0x000FB04C File Offset: 0x000F924C
		protected override void OnControlAdded(ControlEventArgs e)
		{
			if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
			{
				(e.Control as ToolStrip).LayoutStyle = ToolStripLayoutStyle.VerticalStackWithOverflow;
			}
			else
			{
				(e.Control as ToolStrip).LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
			}
			if (this.done_first_layout && e.Control is ToolStrip)
			{
				this.AddControlToRows(e.Control);
			}
			base.OnControlAdded(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStrip.ControlRemoved" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ControlEventArgs" /> that contains the event data.</param>
		// Token: 0x06003F0C RID: 16140 RVA: 0x000FB0C8 File Offset: 0x000F92C8
		protected override void OnControlRemoved(ControlEventArgs e)
		{
			base.OnControlRemoved(e);
			foreach (object obj in this.rows)
			{
				ToolStripPanelRow toolStripPanelRow = (ToolStripPanelRow)obj;
				if (toolStripPanelRow.controls.Contains(e.Control))
				{
					toolStripPanelRow.OnControlRemoved(e.Control, 0);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.DockChanged" /> event. </summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003F0D RID: 16141 RVA: 0x000FB15C File Offset: 0x000F935C
		protected override void OnDockChanged(EventArgs e)
		{
			base.OnDockChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x06003F0E RID: 16142 RVA: 0x000FB168 File Offset: 0x000F9368
		protected override void OnLayout(LayoutEventArgs e)
		{
			if (!base.Created)
			{
				return;
			}
			if (!this.done_first_layout)
			{
				ArrayList arrayList = new ArrayList(base.Controls);
				arrayList.Sort(new ToolStripPanel.TabIndexComparer());
				foreach (object obj in arrayList)
				{
					ToolStrip toolStrip = (ToolStrip)obj;
					this.AddControlToRows(toolStrip);
				}
				this.done_first_layout = true;
			}
			Point location = this.DisplayRectangle.Location;
			if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
			{
				foreach (object obj2 in this.rows)
				{
					ToolStripPanelRow toolStripPanelRow = (ToolStripPanelRow)obj2;
					toolStripPanelRow.SetBounds(new Rectangle(location, new Size(toolStripPanelRow.Bounds.Width, base.Height)));
					location.X += toolStripPanelRow.Bounds.Width;
				}
				if (this.rows.Count > 0)
				{
					int right = this.rows[this.rows.Count - 1].Bounds.Right;
					if (right != base.Width)
					{
						base.SetBounds(this.bounds.X, this.bounds.Y, right, this.bounds.Bottom);
					}
				}
			}
			else
			{
				foreach (object obj3 in this.rows)
				{
					ToolStripPanelRow toolStripPanelRow2 = (ToolStripPanelRow)obj3;
					toolStripPanelRow2.SetBounds(new Rectangle(location, new Size(base.Width, toolStripPanelRow2.Bounds.Height)));
					location.Y += toolStripPanelRow2.Bounds.Height;
				}
				if (this.rows.Count > 0)
				{
					int bottom = this.rows[this.rows.Count - 1].Bounds.Bottom;
					if (bottom != base.Height)
					{
						base.SetBounds(this.bounds.X, this.bounds.Y, this.bounds.Width, bottom);
					}
				}
			}
			base.Invalidate();
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
		// Token: 0x06003F0F RID: 16143 RVA: 0x000FB468 File Offset: 0x000F9668
		[EditorBrowsable(2)]
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			this.Renderer.DrawToolStripPanelBackground(new ToolStripPanelRenderEventArgs(e.Graphics, this));
		}

		// Token: 0x06003F10 RID: 16144 RVA: 0x000FB494 File Offset: 0x000F9694
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripPanel.RendererChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003F11 RID: 16145 RVA: 0x000FB4A0 File Offset: 0x000F96A0
		protected virtual void OnRendererChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripPanel.RendererChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.RightToLeftChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003F12 RID: 16146 RVA: 0x000FB4D4 File Offset: 0x000F96D4
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
		}

		// Token: 0x06003F13 RID: 16147 RVA: 0x000FB4E0 File Offset: 0x000F96E0
		private void AddControlToRows(Control control)
		{
			if (this.rows.Count > 0 && this.rows[this.rows.Count - 1].CanMove((ToolStrip)control))
			{
				this.rows[this.rows.Count - 1].OnControlAdded(control, 0);
				return;
			}
			ToolStripPanelRow toolStripPanelRow = new ToolStripPanelRow(this);
			if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
			{
				toolStripPanelRow.SetBounds(new Rectangle(0, 0, 25, base.Height));
			}
			else
			{
				toolStripPanelRow.SetBounds(new Rectangle(0, 0, base.Width, 25));
			}
			this.rows.Add(toolStripPanelRow);
			toolStripPanelRow.OnControlAdded(control, 0);
		}

		// Token: 0x04001B2F RID: 6959
		private bool done_first_layout;

		// Token: 0x04001B30 RID: 6960
		private LayoutEngine layout_engine;

		// Token: 0x04001B31 RID: 6961
		private bool locked;

		// Token: 0x04001B32 RID: 6962
		private Orientation orientation;

		// Token: 0x04001B33 RID: 6963
		private ToolStripRenderer renderer;

		// Token: 0x04001B34 RID: 6964
		private ToolStripRenderMode render_mode;

		// Token: 0x04001B35 RID: 6965
		private Padding row_margin;

		// Token: 0x04001B36 RID: 6966
		private ToolStripPanel.ToolStripPanelRowCollection rows;

		/// <summary>Represents all the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> objects in a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
		// Token: 0x0200036E RID: 878
		[ListBindable(false)]
		[ComVisible(false)]
		public class ToolStripPanelRowCollection : ArrangedElementCollection, ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> class in the specified <see cref="T:System.Windows.Forms.ToolStripPanel" />. </summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ToolStripPanel" /> that holds this <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			// Token: 0x06003F14 RID: 16148 RVA: 0x000FB5AC File Offset: 0x000F97AC
			public ToolStripPanelRowCollection(ToolStripPanel owner)
			{
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> class with the specified number of rows in the specified <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ToolStripPanel" /> that holds this <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			/// <param name="value">The number of rows in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			// Token: 0x06003F15 RID: 16149 RVA: 0x000FB5B4 File Offset: 0x000F97B4
			public ToolStripPanelRowCollection(ToolStripPanel owner, ToolStripPanelRow[] value)
				: this(owner)
			{
				if (value != null)
				{
					foreach (ToolStripPanelRow toolStripPanelRow in value)
					{
						this.Add(toolStripPanelRow);
					}
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> at the specified index.</returns>
			/// <param name="index">The zero-based index of the element to get.</param>
			// Token: 0x1700107B RID: 4219
			// (get) Token: 0x06003F16 RID: 16150 RVA: 0x000FB5F0 File Offset: 0x000F97F0
			// (set) Token: 0x06003F17 RID: 16151 RVA: 0x000FB5FC File Offset: 0x000F97FC
			object IList.Item
			{
				get
				{
					return this[index];
				}
				[MonoTODO("Stub, does nothing")]
				set
				{
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x1700107C RID: 4220
			// (get) Token: 0x06003F18 RID: 16152 RVA: 0x000FB600 File Offset: 0x000F9800
			bool IList.IsFixedSize
			{
				get
				{
					return base.IsFixedSize;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.IsReadOnly" />.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x1700107D RID: 4221
			// (get) Token: 0x06003F19 RID: 16153 RVA: 0x000FB608 File Offset: 0x000F9808
			bool IList.IsReadOnly
			{
				get
				{
					return this.IsReadOnly;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <returns>The zero-based index of the item to add.</returns>
			/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
			// Token: 0x06003F1A RID: 16154 RVA: 0x000FB610 File Offset: 0x000F9810
			int IList.Add(object value)
			{
				return this.Add(value as ToolStripPanelRow);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Clear" />.</summary>
			// Token: 0x06003F1B RID: 16155 RVA: 0x000FB620 File Offset: 0x000F9820
			void IList.Clear()
			{
				this.Clear();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
			/// <returns>true if <paramref name="value" /> is a <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> found in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />; otherwise, false.</returns>
			/// <param name="value">The item to locate in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			// Token: 0x06003F1C RID: 16156 RVA: 0x000FB628 File Offset: 0x000F9828
			bool IList.Contains(object value)
			{
				return this.Contains(value as ToolStripPanelRow);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
			/// <returns>The index of <paramref name="value" /> if it is a <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> found in the list; otherwise, -1.</returns>
			/// <param name="value">The object to locate in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			// Token: 0x06003F1D RID: 16157 RVA: 0x000FB638 File Offset: 0x000F9838
			int IList.IndexOf(object value)
			{
				return this.IndexOf(value as ToolStripPanelRow);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
			/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to insert into the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			// Token: 0x06003F1E RID: 16158 RVA: 0x000FB648 File Offset: 0x000F9848
			void IList.Insert(int index, object value)
			{
				this.Insert(index, value as ToolStripPanelRow);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to remove from the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			// Token: 0x06003F1F RID: 16159 RVA: 0x000FB658 File Offset: 0x000F9858
			void IList.Remove(object value)
			{
				this.Remove(value as ToolStripPanelRow);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.RemoveAt(System.Int32)" />.</summary>
			/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to remove.</param>
			// Token: 0x06003F20 RID: 16160 RVA: 0x000FB668 File Offset: 0x000F9868
			void IList.RemoveAt(int index)
			{
				base.InternalRemoveAt(index);
			}

			/// <summary>Gets a particular <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> within the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> of the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> as specified by the <paramref name="index" /> parameter.</returns>
			/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> within the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			// Token: 0x1700107E RID: 4222
			public virtual ToolStripPanelRow this[int index]
			{
				get
				{
					return (ToolStripPanelRow)base[index];
				}
			}

			/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
			/// <returns>The position of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</returns>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to add to the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is null.</exception>
			// Token: 0x06003F22 RID: 16162 RVA: 0x000FB684 File Offset: 0x000F9884
			public int Add(ToolStripPanelRow value)
			{
				return base.Add(value);
			}

			/// <summary>Adds the specified <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> to a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> to add to the <see cref="T:System.Windows.Forms.ToolStripPanel" />.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is null.</exception>
			// Token: 0x06003F23 RID: 16163 RVA: 0x000FB690 File Offset: 0x000F9890
			public void AddRange(ToolStripPanel.ToolStripPanelRowCollection value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				foreach (object obj in value)
				{
					ToolStripPanelRow toolStripPanelRow = (ToolStripPanelRow)obj;
					this.Add(toolStripPanelRow);
				}
			}

			/// <summary>Adds an array of <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> objects to a <see cref="T:System.Windows.Forms.ToolStripPanel" />.</summary>
			/// <param name="value">An array of <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> objects.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is null.</exception>
			// Token: 0x06003F24 RID: 16164 RVA: 0x000FB70C File Offset: 0x000F990C
			public void AddRange(ToolStripPanelRow[] value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				foreach (ToolStripPanelRow toolStripPanelRow in value)
				{
					this.Add(toolStripPanelRow);
				}
			}

			/// <summary>Removes all <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> objects from the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
			// Token: 0x06003F25 RID: 16165 RVA: 0x000FB74C File Offset: 0x000F994C
			public new virtual void Clear()
			{
				base.Clear();
			}

			/// <summary>Determines whether the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> is in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
			/// <returns>true if the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> is in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />; otherwise, false.</returns>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to search for in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</param>
			// Token: 0x06003F26 RID: 16166 RVA: 0x000FB754 File Offset: 0x000F9954
			public bool Contains(ToolStripPanelRow value)
			{
				return base.Contains(value);
			}

			/// <summary>Copies the entire <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> into an existing array at a specified location within the array.</summary>
			/// <param name="array">An <see cref="T:System.Array" /> representing the array to copy the contents of the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> to.</param>
			/// <param name="index">The location within the destination array to copy the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" /> to.</param>
			// Token: 0x06003F27 RID: 16167 RVA: 0x000FB760 File Offset: 0x000F9960
			public void CopyTo(ToolStripPanelRow[] array, int index)
			{
				base.CopyTo(array, index);
			}

			/// <summary>Gets the index of the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
			/// <returns>The index of the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</returns>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to return the index of.</param>
			// Token: 0x06003F28 RID: 16168 RVA: 0x000FB76C File Offset: 0x000F996C
			public int IndexOf(ToolStripPanelRow value)
			{
				return base.IndexOf(value);
			}

			/// <summary>Inserts the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> at the specified location in the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
			/// <param name="index">The zero-based index at which to insert the <see cref="T:System.Windows.Forms.ToolStripPanelRow" />.</param>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to insert.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is null.</exception>
			// Token: 0x06003F29 RID: 16169 RVA: 0x000FB778 File Offset: 0x000F9978
			public void Insert(int index, ToolStripPanelRow value)
			{
				base.Insert(index, value);
			}

			/// <summary>Removes the specified <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> from the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to remove.</param>
			// Token: 0x06003F2A RID: 16170 RVA: 0x000FB784 File Offset: 0x000F9984
			public void Remove(ToolStripPanelRow value)
			{
				base.Remove(value);
			}

			/// <summary>Removes the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> at the specified index from the <see cref="T:System.Windows.Forms.ToolStripPanel.ToolStripPanelRowCollection" />.</summary>
			/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.ToolStripPanelRow" /> to remove.</param>
			// Token: 0x06003F2B RID: 16171 RVA: 0x000FB790 File Offset: 0x000F9990
			public void RemoveAt(int index)
			{
				base.InternalRemoveAt(index);
			}
		}

		// Token: 0x0200036F RID: 879
		private class ToolStripPanelControlCollection : Control.ControlCollection
		{
			// Token: 0x06003F2C RID: 16172 RVA: 0x000FB79C File Offset: 0x000F999C
			public ToolStripPanelControlCollection(Control owner)
				: base(owner)
			{
			}
		}

		// Token: 0x02000370 RID: 880
		private class TabIndexComparer : IComparer
		{
			// Token: 0x06003F2E RID: 16174 RVA: 0x000FB7B0 File Offset: 0x000F99B0
			public int Compare(object x, object y)
			{
				if (!(x is Control) || !(y is Control))
				{
					throw new ArgumentException();
				}
				return (x as Control).TabIndex - (y as Control).TabIndex;
			}
		}
	}
}
