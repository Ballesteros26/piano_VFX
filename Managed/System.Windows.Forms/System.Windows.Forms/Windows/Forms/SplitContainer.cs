using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a control consisting of a movable bar that divides a container's display area into two resizable panels. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020002E1 RID: 737
	[DefaultEvent("SplitterMoved")]
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.SplitContainerDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Docking(DockingBehavior.AutoDock)]
	[ClassInterface(1)]
	public class SplitContainer : ContainerControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SplitContainer" /> class.</summary>
		// Token: 0x0600302D RID: 12333 RVA: 0x000BAEF8 File Offset: 0x000B90F8
		public SplitContainer()
		{
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
			base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.fixed_panel = FixedPanel.None;
			this.orientation = Orientation.Vertical;
			this.splitter_rectangle = new Rectangle(50, 0, 4, base.Height);
			this.splitter_increment = 1;
			this.splitter_prev_move = -1;
			this.restore_cursor = null;
			this.splitter_fixed = false;
			this.panel1_collapsed = false;
			this.panel2_collapsed = false;
			this.panel1_min_size = 25;
			this.panel2_min_size = 25;
			this.panel1 = new SplitterPanel(this);
			this.panel2 = new SplitterPanel(this);
			this.panel1.Size = new Size(50, 50);
			this.UpdateSplitter();
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x000BAFD4 File Offset: 0x000B91D4
		// Note: this type is marked as 'beforefieldinit'.
		static SplitContainer()
		{
			SplitContainer.SplitterMovedEvent = new object();
			SplitContainer.SplitterMovingEvent = new object();
			SplitContainer.UIACanResizeChangedEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.SplitContainer.AutoSize" /> property changes. This property is not relevant to this class.</summary>
		// Token: 0x140002EC RID: 748
		// (add) Token: 0x0600302F RID: 12335 RVA: 0x000BAFF4 File Offset: 0x000B91F4
		// (remove) Token: 0x06003030 RID: 12336 RVA: 0x000BB000 File Offset: 0x000B9200
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.SplitContainer.BackgroundImage" /> property changes. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002ED RID: 749
		// (add) Token: 0x06003031 RID: 12337 RVA: 0x000BB00C File Offset: 0x000B920C
		// (remove) Token: 0x06003032 RID: 12338 RVA: 0x000BB018 File Offset: 0x000B9218
		[EditorBrowsable(0)]
		[Browsable(true)]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.SplitContainer.BackgroundImageLayout" /> property changes. This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002EE RID: 750
		// (add) Token: 0x06003033 RID: 12339 RVA: 0x000BB024 File Offset: 0x000B9224
		// (remove) Token: 0x06003034 RID: 12340 RVA: 0x000BB030 File Offset: 0x000B9230
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002EF RID: 751
		// (add) Token: 0x06003035 RID: 12341 RVA: 0x000BB03C File Offset: 0x000B923C
		// (remove) Token: 0x06003036 RID: 12342 RVA: 0x000BB048 File Offset: 0x000B9248
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event ControlEventHandler ControlAdded
		{
			add
			{
				base.ControlAdded += value;
			}
			remove
			{
				base.ControlAdded -= value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002F0 RID: 752
		// (add) Token: 0x06003037 RID: 12343 RVA: 0x000BB054 File Offset: 0x000B9254
		// (remove) Token: 0x06003038 RID: 12344 RVA: 0x000BB060 File Offset: 0x000B9260
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event ControlEventHandler ControlRemoved
		{
			add
			{
				base.ControlRemoved += value;
			}
			remove
			{
				base.ControlRemoved -= value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002F1 RID: 753
		// (add) Token: 0x06003039 RID: 12345 RVA: 0x000BB06C File Offset: 0x000B926C
		// (remove) Token: 0x0600303A RID: 12346 RVA: 0x000BB078 File Offset: 0x000B9278
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the splitter control is moved.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002F2 RID: 754
		// (add) Token: 0x0600303B RID: 12347 RVA: 0x000BB084 File Offset: 0x000B9284
		// (remove) Token: 0x0600303C RID: 12348 RVA: 0x000BB098 File Offset: 0x000B9298
		public event SplitterEventHandler SplitterMoved
		{
			add
			{
				base.Events.AddHandler(SplitContainer.SplitterMovedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(SplitContainer.SplitterMovedEvent, value);
			}
		}

		/// <summary>Occurs when the splitter control is in the process of moving.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002F3 RID: 755
		// (add) Token: 0x0600303D RID: 12349 RVA: 0x000BB0AC File Offset: 0x000B92AC
		// (remove) Token: 0x0600303E RID: 12350 RVA: 0x000BB0C0 File Offset: 0x000B92C0
		public event SplitterCancelEventHandler SplitterMoving
		{
			add
			{
				base.Events.AddHandler(SplitContainer.SplitterMovingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(SplitContainer.SplitterMovingEvent, value);
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140002F4 RID: 756
		// (add) Token: 0x0600303F RID: 12351 RVA: 0x000BB0D4 File Offset: 0x000B92D4
		// (remove) Token: 0x06003040 RID: 12352 RVA: 0x000BB0E0 File Offset: 0x000B92E0
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

		// Token: 0x140002F5 RID: 757
		// (add) Token: 0x06003041 RID: 12353 RVA: 0x000BB0EC File Offset: 0x000B92EC
		// (remove) Token: 0x06003042 RID: 12354 RVA: 0x000BB100 File Offset: 0x000B9300
		internal event EventHandler UIACanResizeChanged
		{
			add
			{
				base.Events.AddHandler(SplitContainer.UIACanResizeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(SplitContainer.UIACanResizeChangedEvent, value);
			}
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000BB114 File Offset: 0x000B9314
		internal void OnUIACanResizeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[SplitContainer.UIACanResizeChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>When overridden in a derived class, gets or sets a value indicating whether scroll bars automatically appear if controls are placed outside the <see cref="T:System.Windows.Forms.SplitContainer" /> client area. This property is not relevant to this class.</summary>
		/// <returns>true if scroll bars to automatically appear when controls are placed outside the <see cref="T:System.Windows.Forms.SplitContainer" /> client area; otherwise, false. The default is false.</returns>
		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06003044 RID: 12356 RVA: 0x000BB148 File Offset: 0x000B9348
		// (set) Token: 0x06003045 RID: 12357 RVA: 0x000BB150 File Offset: 0x000B9350
		[Browsable(false)]
		[Localizable(true)]
		[EditorBrowsable(1)]
		[DefaultValue(false)]
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

		/// <summary>Gets or sets the size of the auto-scroll margin. This property is not relevant to this class. This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> value that represents the height and width, in pixels, of the auto-scroll margin.</returns>
		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06003046 RID: 12358 RVA: 0x000BB15C File Offset: 0x000B935C
		// (set) Token: 0x06003047 RID: 12359 RVA: 0x000BB164 File Offset: 0x000B9364
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
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

		/// <summary>Gets or sets the minimum size of the scroll bar. This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the minimum height and width of the scroll bar, in pixels.</returns>
		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06003048 RID: 12360 RVA: 0x000BB170 File Offset: 0x000B9370
		// (set) Token: 0x06003049 RID: 12361 RVA: 0x000BB178 File Offset: 0x000B9378
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> value.</returns>
		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x0600304A RID: 12362 RVA: 0x000BB184 File Offset: 0x000B9384
		// (set) Token: 0x0600304B RID: 12363 RVA: 0x000BB18C File Offset: 0x000B938C
		[Browsable(false)]
		[DefaultValue("{X=0,Y=0}")]
		[EditorBrowsable(1)]
		public override Point AutoScrollOffset
		{
			get
			{
				return base.AutoScrollOffset;
			}
			set
			{
				base.AutoScrollOffset = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> value.</returns>
		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x0600304C RID: 12364 RVA: 0x000BB198 File Offset: 0x000B9398
		// (set) Token: 0x0600304D RID: 12365 RVA: 0x000BB1A0 File Offset: 0x000B93A0
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new Point AutoScrollPosition
		{
			get
			{
				return base.AutoScrollPosition;
			}
			set
			{
				base.AutoScrollPosition = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.SplitContainer" /> is automatically resized to display its entire contents. This property is not relevant to this class.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.SplitContainer" /> is automatically resized; otherwise, false.</returns>
		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x0600304E RID: 12366 RVA: 0x000BB1AC File Offset: 0x000B93AC
		// (set) Token: 0x0600304F RID: 12367 RVA: 0x000BB1B4 File Offset: 0x000B93B4
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Gets or sets the background image displayed in the control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06003050 RID: 12368 RVA: 0x000BB1C0 File Offset: 0x000B93C0
		// (set) Token: 0x06003051 RID: 12369 RVA: 0x000BB1C8 File Offset: 0x000B93C8
		[EditorBrowsable(0)]
		[Browsable(true)]
		public override Image BackgroundImage
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06003052 RID: 12370 RVA: 0x000BB1D4 File Offset: 0x000B93D4
		// (set) Token: 0x06003053 RID: 12371 RVA: 0x000BB1DC File Offset: 0x000B93DC
		[EditorBrowsable(1)]
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

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.BindingContext" /> for the <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.BindingContext" /> for the control.</returns>
		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06003054 RID: 12372 RVA: 0x000BB1E8 File Offset: 0x000B93E8
		// (set) Token: 0x06003055 RID: 12373 RVA: 0x000BB1F0 File Offset: 0x000B93F0
		[Browsable(false)]
		public override BindingContext BindingContext
		{
			get
			{
				return base.BindingContext;
			}
			set
			{
				base.BindingContext = value;
			}
		}

		/// <summary>Gets or sets the style of border for the <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is <see cref="F:System.Windows.Forms.BorderStyle.Fixed3D" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of the property is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x06003056 RID: 12374 RVA: 0x000BB1FC File Offset: 0x000B93FC
		// (set) Token: 0x06003057 RID: 12375 RVA: 0x000BB20C File Offset: 0x000B940C
		[DispId(-504)]
		[DefaultValue(BorderStyle.None)]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.panel1.BorderStyle;
			}
			set
			{
				if (!Enum.IsDefined(typeof(BorderStyle), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for BorderStyle", value));
				}
				this.panel1.BorderStyle = value;
				this.panel2.BorderStyle = value;
			}
		}

		/// <summary>Gets a collection of child controls. This property is not relevant to this class.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.Control.ControlCollection" /> that contains the child controls.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x06003058 RID: 12376 RVA: 0x000BB264 File Offset: 0x000B9464
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public new Control.ControlCollection Controls
		{
			get
			{
				return base.Controls;
			}
		}

		/// <summary>Gets or sets which <see cref="T:System.Windows.Forms.SplitContainer" /> borders are attached to the edges of the container.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default value is None.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06003059 RID: 12377 RVA: 0x000BB26C File Offset: 0x000B946C
		// (set) Token: 0x0600305A RID: 12378 RVA: 0x000BB274 File Offset: 0x000B9474
		public new DockStyle Dock
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

		/// <summary>Gets or sets which <see cref="T:System.Windows.Forms.SplitContainer" /> panel remains the same size when the container is resized.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.FixedPanel" />. The default value is None.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.FixedPanel" /> values.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x0600305B RID: 12379 RVA: 0x000BB280 File Offset: 0x000B9480
		// (set) Token: 0x0600305C RID: 12380 RVA: 0x000BB288 File Offset: 0x000B9488
		[DefaultValue(FixedPanel.None)]
		public FixedPanel FixedPanel
		{
			get
			{
				return this.fixed_panel;
			}
			set
			{
				if (!Enum.IsDefined(typeof(FixedPanel), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for FixedPanel", value));
				}
				this.fixed_panel = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the splitter is fixed or movable.</summary>
		/// <returns>true if the splitter is fixed; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x0600305D RID: 12381 RVA: 0x000BB2C4 File Offset: 0x000B94C4
		// (set) Token: 0x0600305E RID: 12382 RVA: 0x000BB2CC File Offset: 0x000B94CC
		[Localizable(true)]
		[DefaultValue(false)]
		public bool IsSplitterFixed
		{
			get
			{
				return this.splitter_fixed;
			}
			set
			{
				this.splitter_fixed = value;
			}
		}

		/// <summary>Gets or sets a value indicating the horizontal or vertical orientation of the <see cref="T:System.Windows.Forms.SplitContainer" /> panels.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Orientation" /> values. The default is Vertical.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.Orientation" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x0600305F RID: 12383 RVA: 0x000BB2D8 File Offset: 0x000B94D8
		// (set) Token: 0x06003060 RID: 12384 RVA: 0x000BB2E0 File Offset: 0x000B94E0
		[Localizable(true)]
		[DefaultValue(Orientation.Vertical)]
		public Orientation Orientation
		{
			get
			{
				return this.orientation;
			}
			set
			{
				if (!Enum.IsDefined(typeof(Orientation), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for Orientation", value));
				}
				if (this.orientation != value)
				{
					if (value == Orientation.Vertical)
					{
						this.splitter_rectangle.Width = this.splitter_rectangle.Height;
						this.splitter_rectangle.X = this.splitter_rectangle.Y;
					}
					else
					{
						this.splitter_rectangle.Height = this.splitter_rectangle.Width;
						this.splitter_rectangle.Y = this.splitter_rectangle.X;
					}
					this.orientation = value;
					this.UpdateSplitter();
				}
			}
		}

		/// <summary>Gets or sets the interior spacing, in pixels, between the edges of a <see cref="T:System.Windows.Forms.SplitterPanel" /> and its contents. This property is not relevant to this class.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.Padding" /> representing the interior spacing.</returns>
		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06003061 RID: 12385 RVA: 0x000BB39C File Offset: 0x000B959C
		// (set) Token: 0x06003062 RID: 12386 RVA: 0x000BB3A4 File Offset: 0x000B95A4
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
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

		/// <summary>Gets the left or top panel of the <see cref="T:System.Windows.Forms.SplitContainer" />, depending on <see cref="P:System.Windows.Forms.SplitContainer.Orientation" />.</summary>
		/// <returns>If <see cref="P:System.Windows.Forms.SplitContainer.Orientation" /> is Vertical, the left panel of the <see cref="T:System.Windows.Forms.SplitContainer" />. If <see cref="P:System.Windows.Forms.SplitContainer.Orientation" /> is Horizontal, the top panel of the <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06003063 RID: 12387 RVA: 0x000BB3B0 File Offset: 0x000B95B0
		[Localizable(false)]
		[DesignerSerializationVisibility(2)]
		public SplitterPanel Panel1
		{
			get
			{
				return this.panel1;
			}
		}

		/// <summary>Gets or sets a value determining whether <see cref="P:System.Windows.Forms.SplitContainer.Panel1" /> is collapsed or expanded.</summary>
		/// <returns>true if <see cref="P:System.Windows.Forms.SplitContainer.Panel1" /> is collapsed; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06003064 RID: 12388 RVA: 0x000BB3B8 File Offset: 0x000B95B8
		// (set) Token: 0x06003065 RID: 12389 RVA: 0x000BB3C0 File Offset: 0x000B95C0
		[DefaultValue(false)]
		public bool Panel1Collapsed
		{
			get
			{
				return this.panel1_collapsed;
			}
			set
			{
				if (this.panel1_collapsed != value)
				{
					this.panel1_collapsed = value;
					this.panel1.Visible = !value;
					this.OnUIACanResizeChanged(EventArgs.Empty);
					base.PerformLayout();
				}
			}
		}

		/// <summary>Gets or sets the minimum distance in pixels of the splitter from the left or top edge of <see cref="P:System.Windows.Forms.SplitContainer.Panel1" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the minimum distance in pixels of the splitter from the left or top edge of <see cref="P:System.Windows.Forms.SplitContainer.Panel1" />. The default value is 25 pixels, regardless of <see cref="P:System.Windows.Forms.SplitContainer.Orientation" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is incompatible with the orientation. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06003066 RID: 12390 RVA: 0x000BB3F8 File Offset: 0x000B95F8
		// (set) Token: 0x06003067 RID: 12391 RVA: 0x000BB400 File Offset: 0x000B9600
		[RefreshProperties(1)]
		[Localizable(true)]
		[DefaultValue(25)]
		public int Panel1MinSize
		{
			get
			{
				return this.panel1_min_size;
			}
			set
			{
				this.panel1_min_size = value;
			}
		}

		/// <summary>Gets the right or bottom panel of the <see cref="T:System.Windows.Forms.SplitContainer" />, depending on <see cref="P:System.Windows.Forms.SplitContainer.Orientation" />.</summary>
		/// <returns>If <see cref="P:System.Windows.Forms.SplitContainer.Orientation" /> is Vertical, the right panel of the <see cref="T:System.Windows.Forms.SplitContainer" />. If <see cref="P:System.Windows.Forms.SplitContainer.Orientation" /> is Horizontal, the bottom panel of the <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06003068 RID: 12392 RVA: 0x000BB40C File Offset: 0x000B960C
		[Localizable(false)]
		[DesignerSerializationVisibility(2)]
		public SplitterPanel Panel2
		{
			get
			{
				return this.panel2;
			}
		}

		/// <summary>Gets or sets a value determining whether <see cref="P:System.Windows.Forms.SplitContainer.Panel2" /> is collapsed or expanded.</summary>
		/// <returns>true if <see cref="P:System.Windows.Forms.SplitContainer.Panel2" /> is collapsed; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C49 RID: 3145
		// (get) Token: 0x06003069 RID: 12393 RVA: 0x000BB414 File Offset: 0x000B9614
		// (set) Token: 0x0600306A RID: 12394 RVA: 0x000BB41C File Offset: 0x000B961C
		[DefaultValue(false)]
		public bool Panel2Collapsed
		{
			get
			{
				return this.panel2_collapsed;
			}
			set
			{
				if (this.panel2_collapsed != value)
				{
					this.panel2_collapsed = value;
					this.panel2.Visible = !value;
					this.OnUIACanResizeChanged(EventArgs.Empty);
					base.PerformLayout();
				}
			}
		}

		/// <summary>Gets or sets the minimum distance in pixels of the splitter from the right or bottom edge of <see cref="P:System.Windows.Forms.SplitContainer.Panel2" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the minimum distance in pixels of the splitter from the right or bottom edge of <see cref="P:System.Windows.Forms.SplitContainer.Panel2" />. The default value is 25 pixels, regardless of <see cref="P:System.Windows.Forms.SplitContainer.Orientation" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is incompatible with the orientation.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x0600306B RID: 12395 RVA: 0x000BB454 File Offset: 0x000B9654
		// (set) Token: 0x0600306C RID: 12396 RVA: 0x000BB45C File Offset: 0x000B965C
		[Localizable(true)]
		[DefaultValue(25)]
		[RefreshProperties(1)]
		public int Panel2MinSize
		{
			get
			{
				return this.panel2_min_size;
			}
			set
			{
				this.panel2_min_size = value;
			}
		}

		/// <summary>Gets or sets the location of the splitter, in pixels, from the left or top edge of the <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the location of the splitter, in pixels, from the left or top edge of the <see cref="T:System.Windows.Forms.SplitContainer" />. The default value is 50 pixels.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than zero. </exception>
		/// <exception cref="T:System.InvalidOperationException">The value is incompatible with the orientation.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x0600306D RID: 12397 RVA: 0x000BB468 File Offset: 0x000B9668
		// (set) Token: 0x0600306E RID: 12398 RVA: 0x000BB490 File Offset: 0x000B9690
		[Localizable(true)]
		[SettingsBindable(true)]
		[DefaultValue(50)]
		public int SplitterDistance
		{
			get
			{
				if (this.orientation == Orientation.Vertical)
				{
					return this.splitter_rectangle.X;
				}
				return this.splitter_rectangle.Y;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (value < this.panel1_min_size)
				{
					value = this.panel1_min_size;
				}
				bool flag = true;
				if (this.orientation == Orientation.Vertical)
				{
					if (base.Width - (this.SplitterWidth + value) < this.panel2_min_size)
					{
						value = base.Width - (this.SplitterWidth + this.panel2_min_size);
					}
					if (this.splitter_rectangle.X != value)
					{
						this.splitter_rectangle.X = value;
						flag = true;
					}
				}
				else
				{
					if (base.Height - (this.SplitterWidth + value) < this.panel2_min_size)
					{
						value = base.Height - (this.SplitterWidth + this.panel2_min_size);
					}
					if (this.splitter_rectangle.Y != value)
					{
						this.splitter_rectangle.Y = value;
						flag = true;
					}
				}
				if (flag)
				{
					this.UpdateSplitter();
					this.OnSplitterMoved(new SplitterEventArgs(base.Left, base.Top, this.splitter_rectangle.X, this.splitter_rectangle.Y));
				}
			}
		}

		/// <summary>Gets or sets a value representing the increment of splitter movement in pixels.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the increment of splitter movement in pixels. The default value is one pixel.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than one. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x0600306F RID: 12399 RVA: 0x000BB5A8 File Offset: 0x000B97A8
		// (set) Token: 0x06003070 RID: 12400 RVA: 0x000BB5B0 File Offset: 0x000B97B0
		[DefaultValue(1)]
		[MonoTODO("Stub, never called")]
		[Localizable(true)]
		public int SplitterIncrement
		{
			get
			{
				return this.splitter_increment;
			}
			set
			{
				this.splitter_increment = value;
			}
		}

		/// <summary>Gets the size and location of the splitter relative to the <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that specifies the size and location of the splitter relative to the <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06003071 RID: 12401 RVA: 0x000BB5BC File Offset: 0x000B97BC
		[Browsable(false)]
		public Rectangle SplitterRectangle
		{
			get
			{
				return this.splitter_rectangle;
			}
		}

		/// <summary>Gets or sets the width of the splitter in pixels.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing the width of the splitter, in pixels. The default is four pixels.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than one or is incompatible with the orientation. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06003072 RID: 12402 RVA: 0x000BB5C4 File Offset: 0x000B97C4
		// (set) Token: 0x06003073 RID: 12403 RVA: 0x000BB5EC File Offset: 0x000B97EC
		[Localizable(true)]
		[DefaultValue(4)]
		public int SplitterWidth
		{
			get
			{
				if (this.orientation == Orientation.Vertical)
				{
					return this.splitter_rectangle.Width;
				}
				return this.splitter_rectangle.Height;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (this.orientation == Orientation.Vertical)
				{
					this.splitter_rectangle.Width = value;
				}
				else
				{
					this.splitter_rectangle.Height = value;
				}
				this.UpdateSplitter();
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can give the focus to the splitter using the TAB key.</summary>
		/// <returns>true if the user can give the focus to the splitter using the TAB key; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06003074 RID: 12404 RVA: 0x000BB638 File Offset: 0x000B9838
		// (set) Token: 0x06003075 RID: 12405 RVA: 0x000BB63C File Offset: 0x000B983C
		[DispId(-516)]
		[DefaultValue(true)]
		[MonoTODO("Stub, never called")]
		public new bool TabStop
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A string.</returns>
		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06003076 RID: 12406 RVA: 0x000BB640 File Offset: 0x000B9840
		// (set) Token: 0x06003077 RID: 12407 RVA: 0x000BB648 File Offset: 0x000B9848
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06003078 RID: 12408 RVA: 0x000BB654 File Offset: 0x000B9854
		protected override Size DefaultSize
		{
			get
			{
				return new Size(150, 100);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.SplitContainer.SplitterMoved" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.SplitterEventArgs" /> that contains the event data. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003079 RID: 12409 RVA: 0x000BB664 File Offset: 0x000B9864
		public void OnSplitterMoved(SplitterEventArgs e)
		{
			SplitterEventHandler splitterEventHandler = (SplitterEventHandler)base.Events[SplitContainer.SplitterMovedEvent];
			if (splitterEventHandler != null)
			{
				splitterEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.SplitContainer.SplitterMoving" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.SplitterEventArgs" /> that contains the event data. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600307A RID: 12410 RVA: 0x000BB698 File Offset: 0x000B9898
		public void OnSplitterMoving(SplitterCancelEventArgs e)
		{
			SplitterCancelEventHandler splitterCancelEventHandler = (SplitterCancelEventHandler)base.Events[SplitContainer.SplitterMovingEvent];
			if (splitterCancelEventHandler != null)
			{
				splitterCancelEventHandler(this, e);
			}
		}

		/// <summary>Creates a new instance of the control collection for the control.</summary>
		/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
		// Token: 0x0600307B RID: 12411 RVA: 0x000BB6CC File Offset: 0x000B98CC
		[EditorBrowsable(2)]
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new SplitContainer.SplitContainerTypedControlCollection(this);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600307C RID: 12412 RVA: 0x000BB6D4 File Offset: 0x000B98D4
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x0600307D RID: 12413 RVA: 0x000BB6E0 File Offset: 0x000B98E0
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x0600307E RID: 12414 RVA: 0x000BB6EC File Offset: 0x000B98EC
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data. </param>
		// Token: 0x0600307F RID: 12415 RVA: 0x000BB6F8 File Offset: 0x000B98F8
		protected override void OnLayout(LayoutEventArgs e)
		{
			this.UpdateLayout();
			base.OnLayout(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003080 RID: 12416 RVA: 0x000BB708 File Offset: 0x000B9908
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003081 RID: 12417 RVA: 0x000BB714 File Offset: 0x000B9914
		protected override void OnMouseCaptureChanged(EventArgs e)
		{
			base.OnMouseCaptureChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06003082 RID: 12418 RVA: 0x000BB720 File Offset: 0x000B9920
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (!this.splitter_fixed && this.SplitterHitTest(e.Location))
			{
				this.splitter_dragging = true;
				this.SplitterBeginMove(e.Location);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003083 RID: 12419 RVA: 0x000BB764 File Offset: 0x000B9964
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this.SplitterRestoreCursor();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06003084 RID: 12420 RVA: 0x000BB774 File Offset: 0x000B9974
		[EditorBrowsable(2)]
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (this.splitter_dragging)
			{
				this.SplitterMove(e.Location);
			}
			if (!this.splitter_fixed && this.SplitterHitTest(e.Location))
			{
				this.SplitterSetCursor(this.orientation);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06003085 RID: 12421 RVA: 0x000BB7C8 File Offset: 0x000B99C8
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			if (this.splitter_dragging)
			{
				this.SplitterEndMove(e.Location, false);
				this.SplitterRestoreCursor();
				this.splitter_dragging = false;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x06003086 RID: 12422 RVA: 0x000BB804 File Offset: 0x000B9A04
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x000BB810 File Offset: 0x000B9A10
		[EditorBrowsable(2)]
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
		}

		/// <summary>Processes a dialog box key.</summary>
		/// <returns>true if the key was processed by the control; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06003088 RID: 12424 RVA: 0x000BB81C File Offset: 0x000B9A1C
		protected override bool ProcessDialogKey(Keys keyData)
		{
			return base.ProcessDialogKey(keyData);
		}

		/// <summary>Selects the next available control and makes it the active control.</summary>
		/// <returns>true if a control is selected; otherwise, false.</returns>
		/// <param name="forward">true to cycle forward through the controls in the <see cref="T:System.Windows.Forms.ContainerControl" />; otherwise, false. </param>
		// Token: 0x06003089 RID: 12425 RVA: 0x000BB828 File Offset: 0x000B9A28
		protected override bool ProcessTabKey(bool forward)
		{
			return base.ProcessTabKey(forward);
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x000BB834 File Offset: 0x000B9A34
		[EditorBrowsable(2)]
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			base.ScaleControl(factor, specified);
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x000BB840 File Offset: 0x000B9A40
		protected override void Select(bool directed, bool forward)
		{
			base.Select(directed, forward);
		}

		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. </param>
		// Token: 0x0600308C RID: 12428 RVA: 0x000BB84C File Offset: 0x000B9A4C
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <summary>Processes Windows messages.</summary>
		/// <param name="msg">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x0600308D RID: 12429 RVA: 0x000BB85C File Offset: 0x000B9A5C
		protected override void WndProc(ref Message msg)
		{
			base.WndProc(ref msg);
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x000BB868 File Offset: 0x000B9A68
		private bool SplitterHitTest(Point location)
		{
			return location.X >= this.splitter_rectangle.X && location.X <= this.splitter_rectangle.X + this.splitter_rectangle.Width && location.Y >= this.splitter_rectangle.Y && location.Y <= this.splitter_rectangle.Y + this.splitter_rectangle.Height;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x000BB8EC File Offset: 0x000B9AEC
		private void SplitterBeginMove(Point location)
		{
			this.splitter_prev_move = ((this.orientation != Orientation.Vertical) ? location.Y : location.X);
			this.splitter_rectangle_moving = this.splitter_rectangle;
			this.splitter_rectangle_before_move = this.splitter_rectangle;
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x000BB92C File Offset: 0x000B9B2C
		private void SplitterMove(Point location)
		{
			int num = ((this.orientation != Orientation.Vertical) ? location.Y : location.X);
			int num2 = num - this.splitter_prev_move;
			Rectangle rectangle = this.splitter_rectangle_moving;
			bool flag = false;
			if (this.orientation == Orientation.Vertical)
			{
				int num3 = this.panel1_min_size;
				int num4 = this.panel2.Location.X + (this.panel2.Width - this.panel2_min_size) - this.splitter_rectangle_moving.Width;
				if (this.splitter_rectangle_moving.X + num2 > num3 && this.splitter_rectangle_moving.X + num2 < num4)
				{
					this.splitter_rectangle_moving.X = this.splitter_rectangle_moving.X + num2;
					flag = true;
				}
				else if (this.splitter_rectangle_moving.X + num2 <= num3 && this.splitter_rectangle_moving.X != num3)
				{
					this.splitter_rectangle_moving.X = num3;
					flag = true;
				}
				else if (this.splitter_rectangle_moving.X + num2 >= num4 && this.splitter_rectangle_moving.X != num4)
				{
					this.splitter_rectangle_moving.X = num4;
					flag = true;
				}
			}
			else if (this.orientation == Orientation.Horizontal)
			{
				int num5 = this.panel1_min_size;
				int num6 = this.panel2.Location.Y + (this.panel2.Height - this.panel2_min_size) - this.splitter_rectangle_moving.Height;
				if (this.splitter_rectangle_moving.Y + num2 > num5 && this.splitter_rectangle_moving.Y + num2 < num6)
				{
					this.splitter_rectangle_moving.Y = this.splitter_rectangle_moving.Y + num2;
					flag = true;
				}
				else if (this.splitter_rectangle_moving.Y + num2 <= num5 && this.splitter_rectangle_moving.Y != num5)
				{
					this.splitter_rectangle_moving.Y = num5;
					flag = true;
				}
				else if (this.splitter_rectangle_moving.Y + num2 >= num6 && this.splitter_rectangle_moving.Y != num6)
				{
					this.splitter_rectangle_moving.Y = num6;
					flag = true;
				}
			}
			if (flag)
			{
				this.splitter_prev_move = num;
				this.OnSplitterMoving(new SplitterCancelEventArgs(location.X, location.Y, this.splitter_rectangle.X, this.splitter_rectangle.Y));
				XplatUI.DrawReversibleRectangle(this.Handle, rectangle, 1);
				XplatUI.DrawReversibleRectangle(this.Handle, this.splitter_rectangle_moving, 1);
			}
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x000BBBC0 File Offset: 0x000B9DC0
		private void SplitterEndMove(Point location, bool cancel)
		{
			if (!cancel && this.splitter_rectangle_before_move != this.splitter_rectangle_moving)
			{
				this.splitter_rectangle = this.splitter_rectangle_moving;
				this.UpdateSplitter();
			}
			SplitterEventArgs splitterEventArgs = new SplitterEventArgs(location.X, location.Y, this.splitter_rectangle.X, this.splitter_rectangle.Y);
			this.OnSplitterMoved(splitterEventArgs);
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x000BBC2C File Offset: 0x000B9E2C
		private void SplitterSetCursor(Orientation orientation)
		{
			if (this.restore_cursor == null)
			{
				this.restore_cursor = this.Cursor;
			}
			this.Cursor = ((orientation != Orientation.Vertical) ? Cursors.HSplit : Cursors.VSplit);
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x000BBC74 File Offset: 0x000B9E74
		private void SplitterRestoreCursor()
		{
			if (this.restore_cursor != null)
			{
				this.Cursor = this.restore_cursor;
				this.restore_cursor = null;
			}
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x000BBCA8 File Offset: 0x000B9EA8
		private void UpdateSplitter()
		{
			base.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			if (this.panel1_collapsed)
			{
				this.panel2.Size = base.Size;
				this.panel2.Location = new Point(0, 0);
			}
			else if (this.panel2_collapsed)
			{
				this.panel1.Size = base.Size;
				this.panel1.Location = new Point(0, 0);
			}
			else
			{
				this.panel1.Location = new Point(0, 0);
				if (this.orientation == Orientation.Vertical)
				{
					this.splitter_rectangle.Y = 0;
					SplitterPanel splitterPanel = this.panel1;
					int num = base.Height;
					this.panel2.InternalHeight = num;
					splitterPanel.InternalHeight = num;
					this.panel1.InternalWidth = Math.Max(this.SplitterDistance, this.panel1_min_size);
					this.panel2.Location = new Point(this.SplitterWidth + this.SplitterDistance, 0);
					this.panel2.InternalWidth = Math.Max(base.Width - (this.SplitterWidth + this.SplitterDistance), this.panel2_min_size);
					this.fixed_none_ratio = (double)base.Width / (double)this.SplitterDistance;
				}
				else if (this.orientation == Orientation.Horizontal)
				{
					this.splitter_rectangle.X = 0;
					SplitterPanel splitterPanel2 = this.panel1;
					int num = base.Width;
					this.panel2.InternalWidth = num;
					splitterPanel2.InternalWidth = num;
					this.panel1.InternalHeight = Math.Max(this.SplitterDistance, this.panel1_min_size);
					this.panel2.Location = new Point(0, this.SplitterWidth + this.SplitterDistance);
					this.panel2.InternalHeight = Math.Max(base.Height - (this.SplitterWidth + this.SplitterDistance), this.panel2_min_size);
					this.fixed_none_ratio = (double)base.Height / (double)this.SplitterDistance;
				}
			}
			this.panel1.ResumeLayout();
			this.panel2.ResumeLayout();
			base.ResumeLayout();
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x000BBECC File Offset: 0x000BA0CC
		private void UpdateLayout()
		{
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			if (this.panel1_collapsed)
			{
				this.panel2.Size = base.Size;
				this.panel2.Location = new Point(0, 0);
			}
			else if (this.panel2_collapsed)
			{
				this.panel1.Size = base.Size;
				this.panel1.Location = new Point(0, 0);
			}
			else
			{
				this.panel1.Location = new Point(0, 0);
				if (this.orientation == Orientation.Vertical)
				{
					this.panel1.Location = new Point(0, 0);
					SplitterPanel splitterPanel = this.panel1;
					int num = base.Height;
					this.panel2.InternalHeight = num;
					splitterPanel.InternalHeight = num;
					this.splitter_rectangle.Height = base.Height;
					if (this.fixed_panel == FixedPanel.None)
					{
						this.splitter_rectangle.X = Math.Max((int)Math.Floor((double)base.Width / this.fixed_none_ratio), this.panel1_min_size);
						this.panel1.InternalWidth = this.SplitterDistance;
						this.panel2.InternalWidth = base.Width - (this.SplitterWidth + this.SplitterDistance);
						this.panel2.Location = new Point(this.SplitterWidth + this.SplitterDistance, 0);
					}
					else if (this.fixed_panel == FixedPanel.Panel1)
					{
						this.panel1.InternalWidth = this.SplitterDistance;
						this.panel2.InternalWidth = Math.Max(base.Width - (this.SplitterWidth + this.SplitterDistance), this.panel2_min_size);
						this.panel2.Location = new Point(this.SplitterWidth + this.SplitterDistance, 0);
					}
					else if (this.fixed_panel == FixedPanel.Panel2)
					{
						this.splitter_rectangle.X = Math.Max(base.Width - (this.SplitterWidth + this.panel2.Width), this.panel1_min_size);
						this.panel1.InternalWidth = this.SplitterDistance;
						this.panel2.Location = new Point(this.SplitterWidth + this.SplitterDistance, 0);
					}
				}
				else if (this.orientation == Orientation.Horizontal)
				{
					this.panel1.Location = new Point(0, 0);
					SplitterPanel splitterPanel2 = this.panel1;
					int num = base.Width;
					this.panel2.InternalWidth = num;
					splitterPanel2.InternalWidth = num;
					this.splitter_rectangle.Width = base.Width;
					if (this.fixed_panel == FixedPanel.None)
					{
						this.splitter_rectangle.Y = Math.Max((int)Math.Floor((double)base.Height / this.fixed_none_ratio), this.panel1_min_size);
						this.panel1.InternalHeight = this.SplitterDistance;
						this.panel2.InternalHeight = base.Height - (this.SplitterWidth + this.SplitterDistance);
						this.panel2.Location = new Point(0, this.SplitterWidth + this.SplitterDistance);
					}
					else if (this.fixed_panel == FixedPanel.Panel1)
					{
						this.panel1.InternalHeight = this.SplitterDistance;
						this.panel2.InternalHeight = Math.Max(base.Height - (this.SplitterWidth + this.SplitterDistance), this.panel2_min_size);
						this.panel2.Location = new Point(0, this.SplitterWidth + this.SplitterDistance);
					}
					else if (this.fixed_panel == FixedPanel.Panel2)
					{
						this.splitter_rectangle.Y = Math.Max(base.Height - (this.SplitterWidth + this.panel2.Height), this.panel1_min_size);
						this.panel1.InternalHeight = this.SplitterDistance;
						this.panel2.Location = new Point(0, this.SplitterWidth + this.SplitterDistance);
					}
				}
			}
			this.panel1.ResumeLayout();
			this.panel2.ResumeLayout();
		}

		// Token: 0x040017BA RID: 6074
		private FixedPanel fixed_panel;

		// Token: 0x040017BB RID: 6075
		private Orientation orientation;

		// Token: 0x040017BC RID: 6076
		private int splitter_increment;

		// Token: 0x040017BD RID: 6077
		private Rectangle splitter_rectangle;

		// Token: 0x040017BE RID: 6078
		private Rectangle splitter_rectangle_moving;

		// Token: 0x040017BF RID: 6079
		private Rectangle splitter_rectangle_before_move;

		// Token: 0x040017C0 RID: 6080
		private bool splitter_fixed;

		// Token: 0x040017C1 RID: 6081
		private bool splitter_dragging;

		// Token: 0x040017C2 RID: 6082
		private int splitter_prev_move;

		// Token: 0x040017C3 RID: 6083
		private Cursor restore_cursor;

		// Token: 0x040017C4 RID: 6084
		private double fixed_none_ratio;

		// Token: 0x040017C5 RID: 6085
		private SplitterPanel panel1;

		// Token: 0x040017C6 RID: 6086
		private bool panel1_collapsed;

		// Token: 0x040017C7 RID: 6087
		private int panel1_min_size;

		// Token: 0x040017C8 RID: 6088
		private SplitterPanel panel2;

		// Token: 0x040017C9 RID: 6089
		private bool panel2_collapsed;

		// Token: 0x040017CA RID: 6090
		private int panel2_min_size;

		// Token: 0x020002E2 RID: 738
		internal class SplitContainerTypedControlCollection : Control.ControlCollection
		{
			// Token: 0x06003096 RID: 12438 RVA: 0x000BC2D8 File Offset: 0x000BA4D8
			public SplitContainerTypedControlCollection(Control owner)
				: base(owner)
			{
			}
		}
	}
}
