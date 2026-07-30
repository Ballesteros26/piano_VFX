using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Creates a panel that is associated with a <see cref="T:System.Windows.Forms.SplitContainer" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020002E6 RID: 742
	[ToolboxItem(false)]
	[Designer("System.Windows.Forms.Design.SplitterPanelDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Docking(DockingBehavior.Never)]
	[ClassInterface(1)]
	[ComVisible(true)]
	public sealed class SplitterPanel : Panel
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.SplitterPanel" /> class with its specified <see cref="T:System.Windows.Forms.SplitContainer" />. </summary>
		/// <param name="owner">The <see cref="T:System.Windows.Forms.SplitContainer" /> that contains the <see cref="T:System.Windows.Forms.SplitterPanel" />.</param>
		// Token: 0x060030F4 RID: 12532 RVA: 0x000BD28C File Offset: 0x000BB48C
		public SplitterPanel(SplitContainer owner)
		{
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x14000304 RID: 772
		// (add) Token: 0x060030F5 RID: 12533 RVA: 0x000BD294 File Offset: 0x000BB494
		// (remove) Token: 0x060030F6 RID: 12534 RVA: 0x000BD2A0 File Offset: 0x000BB4A0
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.SplitterPanel.Dock" /> property changes. This event is not relevant to this class.</summary>
		// Token: 0x14000305 RID: 773
		// (add) Token: 0x060030F7 RID: 12535 RVA: 0x000BD2AC File Offset: 0x000BB4AC
		// (remove) Token: 0x060030F8 RID: 12536 RVA: 0x000BD2B8 File Offset: 0x000BB4B8
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public new event EventHandler DockChanged
		{
			add
			{
				base.DockChanged += value;
			}
			remove
			{
				base.DockChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.SplitterPanel.Location" /> property changes. This event is not relevant to this class.</summary>
		// Token: 0x14000306 RID: 774
		// (add) Token: 0x060030F9 RID: 12537 RVA: 0x000BD2C4 File Offset: 0x000BB4C4
		// (remove) Token: 0x060030FA RID: 12538 RVA: 0x000BD2D0 File Offset: 0x000BB4D0
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler LocationChanged
		{
			add
			{
				base.LocationChanged += value;
			}
			remove
			{
				base.LocationChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.SplitterPanel.TabIndex" /> property changes. This event is not relevant to this class.</summary>
		// Token: 0x14000307 RID: 775
		// (add) Token: 0x060030FB RID: 12539 RVA: 0x000BD2DC File Offset: 0x000BB4DC
		// (remove) Token: 0x060030FC RID: 12540 RVA: 0x000BD2E8 File Offset: 0x000BB4E8
		[DesignerSerializationVisibility(0)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.SplitterPanel.TabStop" /> property changes. This event is not relevant to this class.</summary>
		// Token: 0x14000308 RID: 776
		// (add) Token: 0x060030FD RID: 12541 RVA: 0x000BD2F4 File Offset: 0x000BB4F4
		// (remove) Token: 0x060030FE RID: 12542 RVA: 0x000BD300 File Offset: 0x000BB500
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.SplitterPanel.Visible" /> property changes. This event is not relevant to this class.</summary>
		// Token: 0x14000309 RID: 777
		// (add) Token: 0x060030FF RID: 12543 RVA: 0x000BD30C File Offset: 0x000BB50C
		// (remove) Token: 0x06003100 RID: 12544 RVA: 0x000BD318 File Offset: 0x000BB518
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new event EventHandler VisibleChanged
		{
			add
			{
				base.VisibleChanged += value;
			}
			remove
			{
				base.VisibleChanged -= value;
			}
		}

		/// <summary>Gets or sets how a <see cref="T:System.Windows.Forms.SplitterPanel" /> attaches to the edges of the <see cref="T:System.Windows.Forms.SplitContainer" />. This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06003101 RID: 12545 RVA: 0x000BD324 File Offset: 0x000BB524
		// (set) Token: 0x06003102 RID: 12546 RVA: 0x000BD32C File Offset: 0x000BB52C
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public new AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				base.Anchor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.SplitterPanel" /> is automatically resized to display its entire contents. This property is not relevant to this class.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.SplitterPanel" /> is automatically resized; otherwise, false.</returns>
		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06003103 RID: 12547 RVA: 0x000BD338 File Offset: 0x000BB538
		// (set) Token: 0x06003104 RID: 12548 RVA: 0x000BD340 File Offset: 0x000BB540
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new bool AutoSize
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

		/// <summary>Enables the <see cref="T:System.Windows.Forms.SplitterPanel" /> to shrink when <see cref="P:System.Windows.Forms.SplitterPanel.AutoSize" /> is true. This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values. The default is <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly" />.</returns>
		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06003105 RID: 12549 RVA: 0x000BD34C File Offset: 0x000BB54C
		// (set) Token: 0x06003106 RID: 12550 RVA: 0x000BD354 File Offset: 0x000BB554
		[EditorBrowsable(1)]
		[Browsable(false)]
		[Localizable(false)]
		[DesignerSerializationVisibility(0)]
		public override AutoSizeMode AutoSizeMode
		{
			get
			{
				return base.AutoSizeMode;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the border style for the <see cref="T:System.Windows.Forms.SplitterPanel" />. This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06003107 RID: 12551 RVA: 0x000BD358 File Offset: 0x000BB558
		// (set) Token: 0x06003108 RID: 12552 RVA: 0x000BD360 File Offset: 0x000BB560
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public new BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		/// <summary>Gets or sets which edge of the <see cref="T:System.Windows.Forms.SplitContainer" /> that the <see cref="T:System.Windows.Forms.SplitterPanel" /> is docked to. This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06003109 RID: 12553 RVA: 0x000BD36C File Offset: 0x000BB56C
		// (set) Token: 0x0600310A RID: 12554 RVA: 0x000BD374 File Offset: 0x000BB574
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
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

		/// <summary>Gets the internal spacing between the <see cref="T:System.Windows.Forms.SplitterPanel" /> and its edges. This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ScrollableControl.DockPaddingEdges" /> that represents the padding for all the edges of a docked control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x000BD380 File Offset: 0x000BB580
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new ScrollableControl.DockPaddingEdges DockPadding
		{
			get
			{
				return base.DockPadding;
			}
		}

		/// <summary>Gets or sets the height of the <see cref="T:System.Windows.Forms.SplitterPanel" />.</summary>
		/// <returns>The height of the <see cref="T:System.Windows.Forms.SplitterPanel" />, in pixels.</returns>
		/// <exception cref="T:System.NotSupportedException">The height cannot be set.</exception>
		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x000BD388 File Offset: 0x000BB588
		// (set) Token: 0x0600310D RID: 12557 RVA: 0x000BD3A4 File Offset: 0x000BB5A4
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(0)]
		[Browsable(false)]
		public new int Height
		{
			get
			{
				return (!this.Visible) ? 0 : base.Height;
			}
			set
			{
				throw new NotSupportedException("The height cannot be set");
			}
		}

		/// <summary>Gets or sets the coordinates of the upper-left corner of the <see cref="T:System.Windows.Forms.SplitterPanel" /> relative to the upper-left corner of its <see cref="T:System.Windows.Forms.SplitContainer" />. This property is not relevant to this class.</summary>
		/// <returns>The <see cref="T:System.Drawing.Point" /> that represents the upper-left corner of the <see cref="T:System.Windows.Forms.SplitterPanel" /> relative to the upper-left corner of its <see cref="T:System.Windows.Forms.SplitContainer" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x0600310E RID: 12558 RVA: 0x000BD3B0 File Offset: 0x000BB5B0
		// (set) Token: 0x0600310F RID: 12559 RVA: 0x000BD3B8 File Offset: 0x000BB5B8
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
			}
		}

		/// <summary>Gets or sets the size that is the upper limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify. This property is not relevant to this class.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06003110 RID: 12560 RVA: 0x000BD3C4 File Offset: 0x000BB5C4
		// (set) Token: 0x06003111 RID: 12561 RVA: 0x000BD3CC File Offset: 0x000BB5CC
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public new Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				base.MaximumSize = value;
			}
		}

		/// <summary>Gets or sets the size that is the lower limit that <see cref="M:System.Windows.Forms.Control.GetPreferredSize(System.Drawing.Size)" /> can specify. This property is not relevant to this class.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <exception cref="T:System.NotSupportedException">The width cannot be set.</exception>
		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06003112 RID: 12562 RVA: 0x000BD3D8 File Offset: 0x000BB5D8
		// (set) Token: 0x06003113 RID: 12563 RVA: 0x000BD3E0 File Offset: 0x000BB5E0
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = value;
			}
		}

		/// <summary>The name of this <see cref="T:System.Windows.Forms.SplitterPanel" />. This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the name of this <see cref="T:System.Windows.Forms.SplitterPanel" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06003114 RID: 12564 RVA: 0x000BD3EC File Offset: 0x000BB5EC
		// (set) Token: 0x06003115 RID: 12565 RVA: 0x000BD3F4 File Offset: 0x000BB5F4
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.SplitContainer" /> that contains this <see cref="T:System.Windows.Forms.SplitterPanel" />. This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Control" /> representing the <see cref="T:System.Windows.Forms.SplitContainer" /> that contains this <see cref="T:System.Windows.Forms.SplitterPanel" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06003116 RID: 12566 RVA: 0x000BD400 File Offset: 0x000BB600
		// (set) Token: 0x06003117 RID: 12567 RVA: 0x000BD408 File Offset: 0x000BB608
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new Control Parent
		{
			get
			{
				return base.Parent;
			}
			set
			{
				throw new NotSupportedException("The parent cannot be set");
			}
		}

		/// <summary>Gets or sets the height and width of the <see cref="T:System.Windows.Forms.SplitterPanel" />. This property is not relevant to this class.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> that represents the height and width of the <see cref="T:System.Windows.Forms.SplitterPanel" /> in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06003118 RID: 12568 RVA: 0x000BD414 File Offset: 0x000BB614
		// (set) Token: 0x06003119 RID: 12569 RVA: 0x000BD41C File Offset: 0x000BB61C
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}

		/// <summary>Gets or sets the tab order of the <see cref="T:System.Windows.Forms.SplitterPanel" /> within its <see cref="T:System.Windows.Forms.SplitContainer" />. This property is not relevant to this class.</summary>
		/// <returns>The index value of the <see cref="T:System.Windows.Forms.SplitterPanel" /> within the set of other <see cref="T:System.Windows.Forms.SplitterPanel" /> objects within its <see cref="T:System.Windows.Forms.SplitContainer" /> that are included in the tab order.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x0600311A RID: 12570 RVA: 0x000BD428 File Offset: 0x000BB628
		// (set) Token: 0x0600311B RID: 12571 RVA: 0x000BD430 File Offset: 0x000BB630
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

		/// <summary>Gets or sets a value indicating whether the user can give the focus to this <see cref="T:System.Windows.Forms.SplitterPanel" /> using the TAB key. This property is not relevant to this class.</summary>
		/// <returns>true if the user can give the focus to this <see cref="T:System.Windows.Forms.SplitterPanel" /> using the TAB key; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x0600311C RID: 12572 RVA: 0x000BD43C File Offset: 0x000BB63C
		// (set) Token: 0x0600311D RID: 12573 RVA: 0x000BD444 File Offset: 0x000BB644
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.SplitterPanel" /> is displayed. This property is not relevant to this class.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.SplitterPanel" /> is displayed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x0600311E RID: 12574 RVA: 0x000BD450 File Offset: 0x000BB650
		// (set) Token: 0x0600311F RID: 12575 RVA: 0x000BD458 File Offset: 0x000BB658
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public new bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		/// <summary>Gets or sets the width of the <see cref="T:System.Windows.Forms.SplitterPanel" />.</summary>
		/// <returns>The width of the <see cref="T:System.Windows.Forms.SplitterPanel" /> in pixels.</returns>
		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06003120 RID: 12576 RVA: 0x000BD464 File Offset: 0x000BB664
		// (set) Token: 0x06003121 RID: 12577 RVA: 0x000BD480 File Offset: 0x000BB680
		[EditorBrowsable(0)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public new int Width
		{
			get
			{
				return (!this.Visible) ? 0 : base.Width;
			}
			set
			{
				throw new NotSupportedException("The width cannot be set");
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06003122 RID: 12578 RVA: 0x000BD48C File Offset: 0x000BB68C
		protected override Padding DefaultMargin
		{
			get
			{
				return new Padding(0);
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (set) Token: 0x06003123 RID: 12579 RVA: 0x000BD494 File Offset: 0x000BB694
		internal int InternalHeight
		{
			set
			{
				base.Height = value;
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (set) Token: 0x06003124 RID: 12580 RVA: 0x000BD4A0 File Offset: 0x000BB6A0
		internal int InternalWidth
		{
			set
			{
				base.Width = value;
			}
		}
	}
}
