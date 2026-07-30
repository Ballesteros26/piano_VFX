using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a splitter control that enables the user to resize docked controls. <see cref="T:System.Windows.Forms.Splitter" /> has been replaced by <see cref="T:System.Windows.Forms.SplitContainer" /> and is provided only for compatibility with previous versions.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002E3 RID: 739
	[ClassInterface(1)]
	[DefaultEvent("SplitterMoved")]
	[Designer("System.Windows.Forms.Design.SplitterDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("Dock")]
	[ComVisible(true)]
	public class Splitter : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Splitter" /> class. <see cref="T:System.Windows.Forms.Splitter" /> has been replaced by <see cref="T:System.Windows.Forms.SplitContainer" />, and is provided only for compatibility with previous versions.</summary>
		// Token: 0x06003097 RID: 12439 RVA: 0x000BC2E4 File Offset: 0x000BA4E4
		public Splitter()
		{
			this.min_extra = 25;
			this.min_size = 25;
			this.split_requested = -1;
			this.splitter_size = 3;
			this.horizontal = false;
			base.SetStyle(ControlStyles.Selectable, false);
			this.Anchor = AnchorStyles.None;
			this.Dock = DockStyle.Left;
			base.Layout += this.LayoutSplitter;
			base.ParentChanged += new EventHandler(this.ReparentSplitter);
			this.Cursor = Splitter.splitter_we;
		}

		// Token: 0x06003098 RID: 12440 RVA: 0x000BC368 File Offset: 0x000BA568
		static Splitter()
		{
			Splitter.SplitterMovedEvent = new object();
			Splitter.SplitterMovingEvent = new object();
			Splitter.splitter_ns = Cursors.HSplit;
			Splitter.splitter_we = Cursors.VSplit;
		}

		/// <summary>This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002F6 RID: 758
		// (add) Token: 0x06003099 RID: 12441 RVA: 0x000BC3A0 File Offset: 0x000BA5A0
		// (remove) Token: 0x0600309A RID: 12442 RVA: 0x000BC3AC File Offset: 0x000BA5AC
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002F7 RID: 759
		// (add) Token: 0x0600309B RID: 12443 RVA: 0x000BC3B8 File Offset: 0x000BA5B8
		// (remove) Token: 0x0600309C RID: 12444 RVA: 0x000BC3C4 File Offset: 0x000BA5C4
		[EditorBrowsable(1)]
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

		/// <summary>This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002F8 RID: 760
		// (add) Token: 0x0600309D RID: 12445 RVA: 0x000BC3D0 File Offset: 0x000BA5D0
		// (remove) Token: 0x0600309E RID: 12446 RVA: 0x000BC3DC File Offset: 0x000BA5DC
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler Enter
		{
			add
			{
				base.Enter += value;
			}
			remove
			{
				base.Enter -= value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002F9 RID: 761
		// (add) Token: 0x0600309F RID: 12447 RVA: 0x000BC3E8 File Offset: 0x000BA5E8
		// (remove) Token: 0x060030A0 RID: 12448 RVA: 0x000BC3F4 File Offset: 0x000BA5F4
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler FontChanged
		{
			add
			{
				base.FontChanged += value;
			}
			remove
			{
				base.FontChanged -= value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002FA RID: 762
		// (add) Token: 0x060030A1 RID: 12449 RVA: 0x000BC400 File Offset: 0x000BA600
		// (remove) Token: 0x060030A2 RID: 12450 RVA: 0x000BC40C File Offset: 0x000BA60C
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

		/// <summary>This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002FB RID: 763
		// (add) Token: 0x060030A3 RID: 12451 RVA: 0x000BC418 File Offset: 0x000BA618
		// (remove) Token: 0x060030A4 RID: 12452 RVA: 0x000BC424 File Offset: 0x000BA624
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler ImeModeChanged
		{
			add
			{
				base.ImeModeChanged += value;
			}
			remove
			{
				base.ImeModeChanged -= value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002FC RID: 764
		// (add) Token: 0x060030A5 RID: 12453 RVA: 0x000BC430 File Offset: 0x000BA630
		// (remove) Token: 0x060030A6 RID: 12454 RVA: 0x000BC43C File Offset: 0x000BA63C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event KeyEventHandler KeyDown
		{
			add
			{
				base.KeyDown += value;
			}
			remove
			{
				base.KeyDown -= value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002FD RID: 765
		// (add) Token: 0x060030A7 RID: 12455 RVA: 0x000BC448 File Offset: 0x000BA648
		// (remove) Token: 0x060030A8 RID: 12456 RVA: 0x000BC454 File Offset: 0x000BA654
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event KeyPressEventHandler KeyPress
		{
			add
			{
				base.KeyPress += value;
			}
			remove
			{
				base.KeyPress -= value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002FE RID: 766
		// (add) Token: 0x060030A9 RID: 12457 RVA: 0x000BC460 File Offset: 0x000BA660
		// (remove) Token: 0x060030AA RID: 12458 RVA: 0x000BC46C File Offset: 0x000BA66C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event KeyEventHandler KeyUp
		{
			add
			{
				base.KeyUp += value;
			}
			remove
			{
				base.KeyUp -= value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002FF RID: 767
		// (add) Token: 0x060030AB RID: 12459 RVA: 0x000BC478 File Offset: 0x000BA678
		// (remove) Token: 0x060030AC RID: 12460 RVA: 0x000BC484 File Offset: 0x000BA684
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler Leave
		{
			add
			{
				base.Leave += value;
			}
			remove
			{
				base.Leave -= value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000300 RID: 768
		// (add) Token: 0x060030AD RID: 12461 RVA: 0x000BC490 File Offset: 0x000BA690
		// (remove) Token: 0x060030AE RID: 12462 RVA: 0x000BC49C File Offset: 0x000BA69C
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>This event is not relevant to this class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000301 RID: 769
		// (add) Token: 0x060030AF RID: 12463 RVA: 0x000BC4A8 File Offset: 0x000BA6A8
		// (remove) Token: 0x060030B0 RID: 12464 RVA: 0x000BC4B4 File Offset: 0x000BA6B4
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

		/// <summary>Occurs when the splitter control is moved. <see cref="E:System.Windows.Forms.Splitter.SplitterMoved" /> has been replaced by <see cref="E:System.Windows.Forms.SplitContainer.SplitterMoved" /> and is provided only for compatibility with previous versions.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000302 RID: 770
		// (add) Token: 0x060030B1 RID: 12465 RVA: 0x000BC4C0 File Offset: 0x000BA6C0
		// (remove) Token: 0x060030B2 RID: 12466 RVA: 0x000BC4D4 File Offset: 0x000BA6D4
		public event SplitterEventHandler SplitterMoved
		{
			add
			{
				base.Events.AddHandler(Splitter.SplitterMovedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Splitter.SplitterMovedEvent, value);
			}
		}

		/// <summary>Occurs when the splitter control is in the process of moving. <see cref="E:System.Windows.Forms.Splitter.SplitterMoving" /> has been replaced by <see cref="E:System.Windows.Forms.SplitContainer.SplitterMoving" /> and is provided only for compatibility with previous versions.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000303 RID: 771
		// (add) Token: 0x060030B3 RID: 12467 RVA: 0x000BC4E8 File Offset: 0x000BA6E8
		// (remove) Token: 0x060030B4 RID: 12468 RVA: 0x000BC4FC File Offset: 0x000BA6FC
		public event SplitterEventHandler SplitterMoving
		{
			add
			{
				base.Events.AddHandler(Splitter.SplitterMovingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Splitter.SplitterMovingEvent, value);
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if enabled; otherwise, false. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x000BC510 File Offset: 0x000BA710
		// (set) Token: 0x060030B6 RID: 12470 RVA: 0x000BC518 File Offset: 0x000BA718
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
		/// <returns>One of the <see cref="T:System.Windows.Forms.AnchorStyles" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x060030B7 RID: 12471 RVA: 0x000BC524 File Offset: 0x000BA724
		// (set) Token: 0x060030B8 RID: 12472 RVA: 0x000BC528 File Offset: 0x000BA728
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DefaultValue(AnchorStyles.None)]
		public override AnchorStyles Anchor
		{
			get
			{
				return AnchorStyles.None;
			}
			set
			{
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x060030B9 RID: 12473 RVA: 0x000BC52C File Offset: 0x000BA72C
		// (set) Token: 0x060030BA RID: 12474 RVA: 0x000BC534 File Offset: 0x000BA734
		[Browsable(false)]
		[EditorBrowsable(1)]
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
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x060030BB RID: 12475 RVA: 0x000BC540 File Offset: 0x000BA740
		// (set) Token: 0x060030BC RID: 12476 RVA: 0x000BC548 File Offset: 0x000BA748
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

		/// <summary>Gets or sets the style of border for the control. <see cref="P:System.Windows.Forms.Splitter.BorderStyle" /> has been replaced by <see cref="P:System.Windows.Forms.SplitContainer.BorderStyle" /> and is provided only for compatibility with previous versions.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is BorderStyle.None.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value of the property is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x060030BD RID: 12477 RVA: 0x000BC554 File Offset: 0x000BA754
		// (set) Token: 0x060030BE RID: 12478 RVA: 0x000BC55C File Offset: 0x000BA75C
		[MWFDescription("Sets the border style for the splitter")]
		[DispId(-504)]
		[DefaultValue(BorderStyle.None)]
		[MWFCategory("Appearance")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.border_style;
			}
			set
			{
				this.border_style = value;
				switch (value)
				{
				case BorderStyle.None:
					this.splitter_size = 3;
					break;
				case BorderStyle.FixedSingle:
					this.splitter_size = 4;
					break;
				case BorderStyle.Fixed3D:
					value = BorderStyle.None;
					this.splitter_size = 3;
					break;
				default:
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for BorderStyle", value));
				}
				base.InternalBorderStyle = value;
			}
		}

		/// <summary>Gets or sets which <see cref="T:System.Windows.Forms.Splitter" /> borders are docked to its parent control and determines how a <see cref="T:System.Windows.Forms.Splitter" /> is resized with its parent.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.Left" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Windows.Forms.Splitter.Dock" /> is not set to one of the valid <see cref="T:System.Windows.Forms.DockStyle" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x060030BF RID: 12479 RVA: 0x000BC5D0 File Offset: 0x000BA7D0
		// (set) Token: 0x060030C0 RID: 12480 RVA: 0x000BC5D8 File Offset: 0x000BA7D8
		[DefaultValue(DockStyle.Left)]
		[Localizable(true)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				if (!Enum.IsDefined(typeof(DockStyle), value) || value == DockStyle.None || value == DockStyle.Fill)
				{
					throw new ArgumentException("Splitter must be docked left, top, bottom or right");
				}
				if (value == DockStyle.Top || value == DockStyle.Bottom)
				{
					this.horizontal = true;
					this.Cursor = Splitter.splitter_ns;
				}
				else
				{
					this.horizontal = false;
					this.Cursor = Splitter.splitter_we;
				}
				base.Dock = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x060030C1 RID: 12481 RVA: 0x000BC658 File Offset: 0x000BA858
		// (set) Token: 0x060030C2 RID: 12482 RVA: 0x000BC660 File Offset: 0x000BA860
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x060030C3 RID: 12483 RVA: 0x000BC66C File Offset: 0x000BA86C
		// (set) Token: 0x060030C4 RID: 12484 RVA: 0x000BC674 File Offset: 0x000BA874
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override Color ForeColor
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x060030C5 RID: 12485 RVA: 0x000BC680 File Offset: 0x000BA880
		// (set) Token: 0x060030C6 RID: 12486 RVA: 0x000BC688 File Offset: 0x000BA888
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new ImeMode ImeMode
		{
			get
			{
				return base.ImeMode;
			}
			set
			{
				base.ImeMode = value;
			}
		}

		/// <summary>Gets or sets the minimum distance that must remain between the splitter control and the edge of the opposite side of the container (or the closest control docked to that side). <see cref="P:System.Windows.Forms.Splitter.MinExtra" /> has been replaced by similar properties in <see cref="T:System.Windows.Forms.SplitContainer" /> and is provided only for compatibility with previous versions.</summary>
		/// <returns>The minimum distance, in pixels, between the <see cref="T:System.Windows.Forms.Splitter" /> control and the edge of the opposite side of the container (or the closest control docked to that side). The default is 25.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x060030C7 RID: 12487 RVA: 0x000BC694 File Offset: 0x000BA894
		// (set) Token: 0x060030C8 RID: 12488 RVA: 0x000BC69C File Offset: 0x000BA89C
		[MWFCategory("Behaviour")]
		[DefaultValue(25)]
		[Localizable(true)]
		[MWFDescription("Sets minimum size of undocked window")]
		public int MinExtra
		{
			get
			{
				return this.min_extra;
			}
			set
			{
				this.min_extra = value;
			}
		}

		/// <summary>Gets or sets the minimum distance that must remain between the splitter control and the container edge that the control is docked to. <see cref="P:System.Windows.Forms.Splitter.MinSize" /> has been replaced by <see cref="P:System.Windows.Forms.SplitContainer.Panel1MinSize" /> and <see cref="P:System.Windows.Forms.SplitContainer.Panel2MinSize" /> and is provided only for compatibility with previous versions.</summary>
		/// <returns>The minimum distance, in pixels, between the <see cref="T:System.Windows.Forms.Splitter" /> control and the container edge that the control is docked to. The default is 25.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x060030C9 RID: 12489 RVA: 0x000BC6A8 File Offset: 0x000BA8A8
		// (set) Token: 0x060030CA RID: 12490 RVA: 0x000BC6B0 File Offset: 0x000BA8B0
		[DefaultValue(25)]
		[Localizable(true)]
		[MWFDescription("Sets minimum size of the resized control")]
		[MWFCategory("Behaviour")]
		public int MinSize
		{
			get
			{
				return this.min_size;
			}
			set
			{
				this.min_size = value;
			}
		}

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x060030CB RID: 12491 RVA: 0x000BC6BC File Offset: 0x000BA8BC
		internal int MaxSize
		{
			get
			{
				if (base.Parent == null)
				{
					return 0;
				}
				if (this.affected == null)
				{
					this.affected = this.AffectedControl;
				}
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				foreach (object obj in base.Parent.Controls)
				{
					Control control = (Control)obj;
					if (control != this.affected)
					{
						switch (control.Dock)
						{
						case DockStyle.Top:
						case DockStyle.Bottom:
							num2 += control.Height;
							if (control.Location.Y < base.Location.Y)
							{
								num4 += control.Height;
							}
							break;
						case DockStyle.Left:
						case DockStyle.Right:
							num += control.Width;
							if (control.Location.X < base.Location.X)
							{
								num3 += control.Width;
							}
							break;
						}
					}
				}
				if (this.horizontal)
				{
					this.moving_offset = num4;
					return base.Parent.ClientSize.Height - num2 - this.MinExtra;
				}
				this.moving_offset = num3;
				return base.Parent.ClientSize.Width - num - this.MinExtra;
			}
		}

		/// <summary>Gets or sets the distance between the splitter control and the container edge that the control is docked to. <see cref="P:System.Windows.Forms.Splitter.SplitPosition" /> has been replaced by <see cref="P:System.Windows.Forms.SplitContainer.Panel1MinSize" /> and <see cref="P:System.Windows.Forms.SplitContainer.Panel2MinSize" /> and is provided only for compatibility with previous versions.</summary>
		/// <returns>The distance, in pixels, between the <see cref="T:System.Windows.Forms.Splitter" /> control and the container edge that the control is docked to. If the <see cref="T:System.Windows.Forms.Splitter" /> control is not bound to a control, the value is -1.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x060030CC RID: 12492 RVA: 0x000BC860 File Offset: 0x000BAA60
		// (set) Token: 0x060030CD RID: 12493 RVA: 0x000BC8BC File Offset: 0x000BAABC
		[MWFCategory("Layout")]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[MWFDescription("Current splitter position")]
		public int SplitPosition
		{
			get
			{
				this.affected = this.AffectedControl;
				if (this.affected == null)
				{
					return -1;
				}
				if (base.Capture)
				{
					return this.CalculateSplitPosition();
				}
				if (this.horizontal)
				{
					return this.affected.Height;
				}
				return this.affected.Width;
			}
			set
			{
				if (value > this.MaxSize)
				{
					value = this.MaxSize;
				}
				if (value < this.MinSize)
				{
					value = this.MinSize;
				}
				this.affected = this.AffectedControl;
				if (this.affected == null)
				{
					this.split_requested = value;
				}
				else
				{
					if (this.horizontal)
					{
						this.affected.Height = value;
					}
					else
					{
						this.affected.Width = value;
					}
					this.OnSplitterMoved(new SplitterEventArgs(base.Left, base.Top, value, value));
				}
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if enabled; otherwise, false. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x060030CE RID: 12494 RVA: 0x000BC958 File Offset: 0x000BAB58
		// (set) Token: 0x060030CF RID: 12495 RVA: 0x000BC960 File Offset: 0x000BAB60
		[EditorBrowsable(1)]
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
		/// <returns>A string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x060030D0 RID: 12496 RVA: 0x000BC96C File Offset: 0x000BAB6C
		// (set) Token: 0x060030D1 RID: 12497 RVA: 0x000BC974 File Offset: 0x000BAB74
		[Browsable(false)]
		[Bindable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Returns the parameters needed to create the handle. </summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x060030D2 RID: 12498 RVA: 0x000BC980 File Offset: 0x000BAB80
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets or sets the default cursor for the control.</summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.Cursor" /> representing the current default cursor.</returns>
		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x060030D3 RID: 12499 RVA: 0x000BC988 File Offset: 0x000BAB88
		protected override Cursor DefaultCursor
		{
			get
			{
				return base.DefaultCursor;
			}
		}

		/// <summary>Gets the default Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x060030D4 RID: 12500 RVA: 0x000BC990 File Offset: 0x000BAB90
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the default size of the control.</returns>
		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x060030D5 RID: 12501 RVA: 0x000BC994 File Offset: 0x000BAB94
		protected override Size DefaultSize
		{
			get
			{
				return new Size(3, 3);
			}
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.Splitter" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.Splitter" />. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060030D6 RID: 12502 RVA: 0x000BC9A0 File Offset: 0x000BABA0
		public override string ToString()
		{
			return base.ToString() + string.Format(", MinExtra: {0}, MinSize: {1}", this.min_extra, this.min_size);
		}

		/// <summary>This method is not relevant to this class.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x060030D7 RID: 12503 RVA: 0x000BC9D0 File Offset: 0x000BABD0
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (base.Capture && e.KeyCode == Keys.Escape)
			{
				base.Capture = false;
				this.SplitterEndMove(Point.Empty, true);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060030D8 RID: 12504 RVA: 0x000BCA10 File Offset: 0x000BAC10
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (this.affected == null)
			{
				this.affected = this.AffectedControl;
			}
			this.max_size = this.MaxSize;
			if (this.affected == null || e.Button != MouseButtons.Left)
			{
				return;
			}
			base.Capture = true;
			this.SplitterBeginMove(base.Parent.PointToClient(base.PointToScreen(new Point(e.X, e.Y))));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseMove" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060030D9 RID: 12505 RVA: 0x000BCA94 File Offset: 0x000BAC94
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (!base.Capture || e.Button != MouseButtons.Left || this.affected == null)
			{
				return;
			}
			this.SplitterMove(base.Parent.PointToClient(base.PointToScreen(new Point(e.X, e.Y))));
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
		// Token: 0x060030DA RID: 12506 RVA: 0x000BCAF8 File Offset: 0x000BACF8
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (!base.Capture || e.Button != MouseButtons.Left || this.affected == null)
			{
				base.OnMouseUp(e);
				return;
			}
			base.OnMouseUp(e);
			base.Capture = false;
			this.SplitterEndMove(base.Parent.PointToClient(base.PointToScreen(new Point(e.X, e.Y))), false);
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000BCB6C File Offset: 0x000BAD6C
		private void SplitterBeginMove(Point location)
		{
			this.splitter_rectangle_moving = new Rectangle(this.Bounds.X, this.Bounds.Y, base.Width, base.Height);
			this.splitter_prev_move = ((!this.horizontal) ? location.X : location.Y);
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000BCBD0 File Offset: 0x000BADD0
		private void SplitterMove(Point location)
		{
			int num = ((!this.horizontal) ? location.X : location.Y);
			int num2 = num - this.splitter_prev_move;
			Rectangle rectangle = this.splitter_rectangle_moving;
			bool flag = false;
			int num3 = this.MinSize + this.moving_offset;
			int num4 = this.max_size + this.moving_offset;
			if (this.horizontal)
			{
				if (this.splitter_rectangle_moving.Y + num2 > num3 && this.splitter_rectangle_moving.Y + num2 < num4)
				{
					this.splitter_rectangle_moving.Y = this.splitter_rectangle_moving.Y + num2;
					flag = true;
				}
				else if (this.splitter_rectangle_moving.Y + num2 <= num3 && this.splitter_rectangle_moving.Y != num3)
				{
					this.splitter_rectangle_moving.Y = num3;
					flag = true;
				}
				else if (this.splitter_rectangle_moving.Y + num2 >= num4 && this.splitter_rectangle_moving.Y != num4)
				{
					this.splitter_rectangle_moving.Y = num4;
					flag = true;
				}
			}
			else if (this.splitter_rectangle_moving.X + num2 > num3 && this.splitter_rectangle_moving.X + num2 < num4)
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
			if (flag)
			{
				this.splitter_prev_move = num;
				this.OnSplitterMoving(new SplitterEventArgs(location.X, location.Y, this.splitter_rectangle_moving.X, this.splitter_rectangle_moving.Y));
				XplatUI.DrawReversibleRectangle(base.Parent.Handle, rectangle, 1);
				XplatUI.DrawReversibleRectangle(base.Parent.Handle, this.splitter_rectangle_moving, 1);
			}
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000BCE04 File Offset: 0x000BB004
		private void SplitterEndMove(Point location, bool cancel)
		{
			if (!cancel)
			{
				if (this.horizontal)
				{
					this.affected.Height = this.CalculateSplitPosition();
				}
				else
				{
					this.affected.Width = this.CalculateSplitPosition();
				}
			}
			base.Parent.Refresh();
			SplitterEventArgs splitterEventArgs = new SplitterEventArgs(location.X, location.Y, this.splitter_rectangle_moving.X, this.splitter_rectangle_moving.Y);
			this.OnSplitterMoved(splitterEventArgs);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Splitter.SplitterMoved" /> event. <see cref="M:System.Windows.Forms.Splitter.OnSplitterMoved(System.Windows.Forms.SplitterEventArgs)" /> has been replaced by <see cref="M:System.Windows.Forms.SplitContainer.OnSplitterMoved(System.Windows.Forms.SplitterEventArgs)" /> and is provided only for compatibility with previous versions.</summary>
		/// <param name="sevent">A <see cref="T:System.Windows.Forms.SplitterEventArgs" /> that contains the event data. </param>
		// Token: 0x060030DE RID: 12510 RVA: 0x000BCE88 File Offset: 0x000BB088
		protected virtual void OnSplitterMoved(SplitterEventArgs sevent)
		{
			SplitterEventHandler splitterEventHandler = (SplitterEventHandler)base.Events[Splitter.SplitterMovedEvent];
			if (splitterEventHandler != null)
			{
				splitterEventHandler(this, sevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Splitter.SplitterMoving" /> event. <see cref="M:System.Windows.Forms.Splitter.OnSplitterMoving(System.Windows.Forms.SplitterEventArgs)" /> has been replaced by <see cref="M:System.Windows.Forms.SplitContainer.OnSplitterMoving(System.Windows.Forms.SplitterCancelEventArgs)" /> and is provided only for compatibility with previous versions.</summary>
		/// <param name="sevent">A <see cref="T:System.Windows.Forms.SplitterEventArgs" /> that contains the event data. </param>
		// Token: 0x060030DF RID: 12511 RVA: 0x000BCEBC File Offset: 0x000BB0BC
		protected virtual void OnSplitterMoving(SplitterEventArgs sevent)
		{
			SplitterEventHandler splitterEventHandler = (SplitterEventHandler)base.Events[Splitter.SplitterMovingEvent];
			if (splitterEventHandler != null)
			{
				splitterEventHandler(this, sevent);
			}
		}

		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. </param>
		// Token: 0x060030E0 RID: 12512 RVA: 0x000BCEF0 File Offset: 0x000BB0F0
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (this.horizontal)
			{
				this.splitter_size = height;
				if (this.splitter_size < 1)
				{
					this.splitter_size = 3;
				}
				base.SetBoundsCore(x, y, width, this.splitter_size, specified);
			}
			else
			{
				this.splitter_size = width;
				if (this.splitter_size < 1)
				{
					this.splitter_size = 3;
				}
				base.SetBoundsCore(x, y, this.splitter_size, height, specified);
			}
		}

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x060030E1 RID: 12513 RVA: 0x000BCF68 File Offset: 0x000BB168
		private Control AffectedControl
		{
			get
			{
				if (base.Parent == null)
				{
					return null;
				}
				for (int i = base.Parent.Controls.GetChildIndex(this) + 1; i < base.Parent.Controls.Count; i++)
				{
					switch (this.Dock)
					{
					case DockStyle.Top:
						if (base.Top == base.Parent.Controls[i].Bottom)
						{
							return base.Parent.Controls[i];
						}
						break;
					case DockStyle.Bottom:
						if (base.Bottom == base.Parent.Controls[i].Top)
						{
							return base.Parent.Controls[i];
						}
						break;
					case DockStyle.Left:
						if (base.Left == base.Parent.Controls[i].Right)
						{
							return base.Parent.Controls[i];
						}
						break;
					case DockStyle.Right:
						if (base.Right == base.Parent.Controls[i].Left)
						{
							return base.Parent.Controls[i];
						}
						break;
					}
				}
				return null;
			}
		}

		// Token: 0x060030E2 RID: 12514 RVA: 0x000BD0BC File Offset: 0x000BB2BC
		private int CalculateSplitPosition()
		{
			if (this.horizontal)
			{
				if (this.Dock == DockStyle.Top)
				{
					return this.splitter_rectangle_moving.Y - this.affected.Top;
				}
				return this.affected.Bottom - this.splitter_rectangle_moving.Y - this.splitter_size;
			}
			else
			{
				if (this.Dock == DockStyle.Left)
				{
					return this.splitter_rectangle_moving.X - this.affected.Left;
				}
				return this.affected.Right - this.splitter_rectangle_moving.X - this.splitter_size;
			}
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x000BD15C File Offset: 0x000BB35C
		internal override void OnPaintInternal(PaintEventArgs e)
		{
			e.Graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.BackColor), e.ClipRectangle);
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000BD190 File Offset: 0x000BB390
		private void LayoutSplitter(object sender, LayoutEventArgs e)
		{
			this.affected = this.AffectedControl;
			if (this.split_requested != -1)
			{
				this.SplitPosition = this.split_requested;
				this.split_requested = -1;
			}
		}

		// Token: 0x060030E5 RID: 12517 RVA: 0x000BD1C0 File Offset: 0x000BB3C0
		private void ReparentSplitter(object sender, EventArgs e)
		{
			this.affected = null;
		}

		// Token: 0x040017CE RID: 6094
		private static Cursor splitter_ns;

		// Token: 0x040017CF RID: 6095
		private static Cursor splitter_we;

		// Token: 0x040017D0 RID: 6096
		private new BorderStyle border_style;

		// Token: 0x040017D1 RID: 6097
		private int min_extra;

		// Token: 0x040017D2 RID: 6098
		private int min_size;

		// Token: 0x040017D3 RID: 6099
		private int max_size;

		// Token: 0x040017D4 RID: 6100
		private int splitter_size;

		// Token: 0x040017D5 RID: 6101
		private bool horizontal;

		// Token: 0x040017D6 RID: 6102
		private Control affected;

		// Token: 0x040017D7 RID: 6103
		private int split_requested;

		// Token: 0x040017D8 RID: 6104
		private int splitter_prev_move;

		// Token: 0x040017D9 RID: 6105
		private Rectangle splitter_rectangle_moving;

		// Token: 0x040017DA RID: 6106
		private int moving_offset;
	}
}
