using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Implements the basic functionality of a scroll bar control.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020002C6 RID: 710
	[ComVisible(true)]
	[DefaultProperty("Value")]
	[DefaultEvent("Scroll")]
	[ClassInterface(1)]
	public abstract class ScrollBar : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ScrollBar" /> class.</summary>
		// Token: 0x06002EF2 RID: 12018 RVA: 0x000B5040 File Offset: 0x000B3240
		public ScrollBar()
		{
			this.position = 0;
			this.minimum = 0;
			this.maximum = 100;
			this.large_change = 10;
			this.small_change = 1;
			this.timer.Tick += new EventHandler(this.OnTimer);
			base.MouseEnter += new EventHandler(this.OnMouseEnter);
			base.MouseLeave += new EventHandler(this.OnMouseLeave);
			base.KeyDown += this.OnKeyDownSB;
			base.MouseDown += this.OnMouseDownSB;
			base.MouseUp += this.OnMouseUpSB;
			base.MouseMove += this.OnMouseMoveSB;
			base.Resize += new EventHandler(this.OnResizeSB);
			base.TabStop = false;
			base.Cursor = Cursors.Default;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.UseTextForAccessibility, false);
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x000B517C File Offset: 0x000B337C
		// Note: this type is marked as 'beforefieldinit'.
		static ScrollBar()
		{
			ScrollBar.ScrollEvent = new object();
			ScrollBar.ValueChangedEvent = new object();
			ScrollBar.UIAScrollEvent = new object();
			ScrollBar.UIAValueChangeEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ScrollBar.AutoSize" /> property changes.</summary>
		// Token: 0x140002D7 RID: 727
		// (add) Token: 0x06002EF4 RID: 12020 RVA: 0x000B51B4 File Offset: 0x000B33B4
		// (remove) Token: 0x06002EF5 RID: 12021 RVA: 0x000B51C0 File Offset: 0x000B33C0
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ScrollBar.BackColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002D8 RID: 728
		// (add) Token: 0x06002EF6 RID: 12022 RVA: 0x000B51CC File Offset: 0x000B33CC
		// (remove) Token: 0x06002EF7 RID: 12023 RVA: 0x000B51D8 File Offset: 0x000B33D8
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ScrollBar.BackgroundImage" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002D9 RID: 729
		// (add) Token: 0x06002EF8 RID: 12024 RVA: 0x000B51E4 File Offset: 0x000B33E4
		// (remove) Token: 0x06002EF9 RID: 12025 RVA: 0x000B51F0 File Offset: 0x000B33F0
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ScrollBar.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002DA RID: 730
		// (add) Token: 0x06002EFA RID: 12026 RVA: 0x000B51FC File Offset: 0x000B33FC
		// (remove) Token: 0x06002EFB RID: 12027 RVA: 0x000B5208 File Offset: 0x000B3408
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

		/// <summary>Occurs when the control is clicked if the <see cref="F:System.Windows.Forms.ControlStyles.StandardClick" /> bit flag is set to true in a derived class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002DB RID: 731
		// (add) Token: 0x06002EFC RID: 12028 RVA: 0x000B5214 File Offset: 0x000B3414
		// (remove) Token: 0x06002EFD RID: 12029 RVA: 0x000B5220 File Offset: 0x000B3420
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler Click
		{
			add
			{
				base.Click += value;
			}
			remove
			{
				base.Click -= value;
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ScrollBar" /> control is double-clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002DC RID: 732
		// (add) Token: 0x06002EFE RID: 12030 RVA: 0x000B522C File Offset: 0x000B342C
		// (remove) Token: 0x06002EFF RID: 12031 RVA: 0x000B5238 File Offset: 0x000B3438
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler DoubleClick
		{
			add
			{
				base.DoubleClick += value;
			}
			remove
			{
				base.DoubleClick -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ScrollBar.Font" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002DD RID: 733
		// (add) Token: 0x06002F00 RID: 12032 RVA: 0x000B5244 File Offset: 0x000B3444
		// (remove) Token: 0x06002F01 RID: 12033 RVA: 0x000B5250 File Offset: 0x000B3450
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ScrollBar.ForeColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002DE RID: 734
		// (add) Token: 0x06002F02 RID: 12034 RVA: 0x000B525C File Offset: 0x000B345C
		// (remove) Token: 0x06002F03 RID: 12035 RVA: 0x000B5268 File Offset: 0x000B3468
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ScrollBar.ImeMode" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002DF RID: 735
		// (add) Token: 0x06002F04 RID: 12036 RVA: 0x000B5274 File Offset: 0x000B3474
		// (remove) Token: 0x06002F05 RID: 12037 RVA: 0x000B5280 File Offset: 0x000B3480
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.ScrollBar" /> control with the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002E0 RID: 736
		// (add) Token: 0x06002F06 RID: 12038 RVA: 0x000B528C File Offset: 0x000B348C
		// (remove) Token: 0x06002F07 RID: 12039 RVA: 0x000B5298 File Offset: 0x000B3498
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event MouseEventHandler MouseClick
		{
			add
			{
				base.MouseClick += value;
			}
			remove
			{
				base.MouseClick -= value;
			}
		}

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.ScrollBar" /> control with the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002E1 RID: 737
		// (add) Token: 0x06002F08 RID: 12040 RVA: 0x000B52A4 File Offset: 0x000B34A4
		// (remove) Token: 0x06002F09 RID: 12041 RVA: 0x000B52B0 File Offset: 0x000B34B0
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event MouseEventHandler MouseDoubleClick
		{
			add
			{
				base.MouseDoubleClick += value;
			}
			remove
			{
				base.MouseDoubleClick -= value;
			}
		}

		/// <summary>Occurs when the mouse pointer is over the control and the user presses a mouse button.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002E2 RID: 738
		// (add) Token: 0x06002F0A RID: 12042 RVA: 0x000B52BC File Offset: 0x000B34BC
		// (remove) Token: 0x06002F0B RID: 12043 RVA: 0x000B52C8 File Offset: 0x000B34C8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event MouseEventHandler MouseDown
		{
			add
			{
				base.MouseDown += value;
			}
			remove
			{
				base.MouseDown -= value;
			}
		}

		/// <summary>Occurs when the user moves the mouse pointer over the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002E3 RID: 739
		// (add) Token: 0x06002F0C RID: 12044 RVA: 0x000B52D4 File Offset: 0x000B34D4
		// (remove) Token: 0x06002F0D RID: 12045 RVA: 0x000B52E0 File Offset: 0x000B34E0
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event MouseEventHandler MouseMove
		{
			add
			{
				base.MouseMove += value;
			}
			remove
			{
				base.MouseMove -= value;
			}
		}

		/// <summary>Occurs when the user moves the mouse pointer over the control and releases a mouse button.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002E4 RID: 740
		// (add) Token: 0x06002F0E RID: 12046 RVA: 0x000B52EC File Offset: 0x000B34EC
		// (remove) Token: 0x06002F0F RID: 12047 RVA: 0x000B52F8 File Offset: 0x000B34F8
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event MouseEventHandler MouseUp
		{
			add
			{
				base.MouseUp += value;
			}
			remove
			{
				base.MouseUp -= value;
			}
		}

		/// <summary>Occurs when the control is redrawn.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002E5 RID: 741
		// (add) Token: 0x06002F10 RID: 12048 RVA: 0x000B5304 File Offset: 0x000B3504
		// (remove) Token: 0x06002F11 RID: 12049 RVA: 0x000B5310 File Offset: 0x000B3510
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event PaintEventHandler Paint
		{
			add
			{
				base.Paint += value;
			}
			remove
			{
				base.Paint -= value;
			}
		}

		/// <summary>Occurs when the scroll box has been moved by either a mouse or keyboard action.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002E6 RID: 742
		// (add) Token: 0x06002F12 RID: 12050 RVA: 0x000B531C File Offset: 0x000B351C
		// (remove) Token: 0x06002F13 RID: 12051 RVA: 0x000B5330 File Offset: 0x000B3530
		public event ScrollEventHandler Scroll
		{
			add
			{
				base.Events.AddHandler(ScrollBar.ScrollEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScrollBar.ScrollEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ScrollBar.Text" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002E7 RID: 743
		// (add) Token: 0x06002F14 RID: 12052 RVA: 0x000B5344 File Offset: 0x000B3544
		// (remove) Token: 0x06002F15 RID: 12053 RVA: 0x000B5350 File Offset: 0x000B3550
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ScrollBar.Value" /> property is changed, either by a <see cref="E:System.Windows.Forms.ScrollBar.Scroll" /> event or programmatically.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002E8 RID: 744
		// (add) Token: 0x06002F16 RID: 12054 RVA: 0x000B535C File Offset: 0x000B355C
		// (remove) Token: 0x06002F17 RID: 12055 RVA: 0x000B5370 File Offset: 0x000B3570
		public event EventHandler ValueChanged
		{
			add
			{
				base.Events.AddHandler(ScrollBar.ValueChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScrollBar.ValueChangedEvent, value);
			}
		}

		// Token: 0x140002E9 RID: 745
		// (add) Token: 0x06002F18 RID: 12056 RVA: 0x000B5384 File Offset: 0x000B3584
		// (remove) Token: 0x06002F19 RID: 12057 RVA: 0x000B5398 File Offset: 0x000B3598
		internal event ScrollEventHandler UIAScroll
		{
			add
			{
				base.Events.AddHandler(ScrollBar.UIAScrollEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScrollBar.UIAScrollEvent, value);
			}
		}

		// Token: 0x140002EA RID: 746
		// (add) Token: 0x06002F1A RID: 12058 RVA: 0x000B53AC File Offset: 0x000B35AC
		// (remove) Token: 0x06002F1B RID: 12059 RVA: 0x000B53C0 File Offset: 0x000B35C0
		internal event ScrollEventHandler UIAValueChanged
		{
			add
			{
				base.Events.AddHandler(ScrollBar.UIAValueChangeEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScrollBar.UIAValueChangeEvent, value);
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x06002F1C RID: 12060 RVA: 0x000B53D4 File Offset: 0x000B35D4
		// (set) Token: 0x06002F1D RID: 12061 RVA: 0x000B53DC File Offset: 0x000B35DC
		internal Rectangle FirstArrowArea
		{
			get
			{
				return this.first_arrow_area;
			}
			set
			{
				this.first_arrow_area = value;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x06002F1E RID: 12062 RVA: 0x000B53E8 File Offset: 0x000B35E8
		// (set) Token: 0x06002F1F RID: 12063 RVA: 0x000B53F0 File Offset: 0x000B35F0
		internal Rectangle SecondArrowArea
		{
			get
			{
				return this.second_arrow_area;
			}
			set
			{
				this.second_arrow_area = value;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06002F20 RID: 12064 RVA: 0x000B53FC File Offset: 0x000B35FC
		private int MaximumAllowed
		{
			get
			{
				return (!this.use_manual_thumb_size) ? (this.maximum - this.LargeChange + 1) : (this.maximum - this.manual_thumb_size + 1);
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06002F21 RID: 12065 RVA: 0x000B5438 File Offset: 0x000B3638
		// (set) Token: 0x06002F22 RID: 12066 RVA: 0x000B5440 File Offset: 0x000B3640
		internal Rectangle ThumbPos
		{
			get
			{
				return this.thumb_pos;
			}
			set
			{
				this.thumb_pos = value;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06002F23 RID: 12067 RVA: 0x000B544C File Offset: 0x000B364C
		// (set) Token: 0x06002F24 RID: 12068 RVA: 0x000B5454 File Offset: 0x000B3654
		internal bool FirstButtonEntered
		{
			get
			{
				return this.first_button_entered;
			}
			private set
			{
				if (this.first_button_entered == value)
				{
					return;
				}
				this.first_button_entered = value;
				if (ThemeEngine.Current.ScrollBarHasHotElementStyles)
				{
					base.Invalidate(this.first_arrow_area);
				}
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06002F25 RID: 12069 RVA: 0x000B5488 File Offset: 0x000B3688
		// (set) Token: 0x06002F26 RID: 12070 RVA: 0x000B5490 File Offset: 0x000B3690
		internal bool SecondButtonEntered
		{
			get
			{
				return this.second_button_entered;
			}
			private set
			{
				if (this.second_button_entered == value)
				{
					return;
				}
				this.second_button_entered = value;
				if (ThemeEngine.Current.ScrollBarHasHotElementStyles)
				{
					base.Invalidate(this.second_arrow_area);
				}
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06002F27 RID: 12071 RVA: 0x000B54C4 File Offset: 0x000B36C4
		// (set) Token: 0x06002F28 RID: 12072 RVA: 0x000B54CC File Offset: 0x000B36CC
		internal bool ThumbEntered
		{
			get
			{
				return this.thumb_entered;
			}
			private set
			{
				if (this.thumb_entered == value)
				{
					return;
				}
				this.thumb_entered = value;
				if (ThemeEngine.Current.ScrollBarHasHotElementStyles)
				{
					base.Invalidate(this.thumb_pos);
				}
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06002F29 RID: 12073 RVA: 0x000B5500 File Offset: 0x000B3700
		// (set) Token: 0x06002F2A RID: 12074 RVA: 0x000B5508 File Offset: 0x000B3708
		internal bool ThumbPressed
		{
			get
			{
				return this.thumb_pressed;
			}
			private set
			{
				if (this.thumb_pressed == value)
				{
					return;
				}
				this.thumb_pressed = value;
				if (ThemeEngine.Current.ScrollBarHasPressedThumbStyle)
				{
					base.Invalidate(this.thumb_pos);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ScrollBar" /> is automatically resized to fit its contents.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ScrollBar" /> should be automatically resized to fit its contents; otherwise, false.</returns>
		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06002F2B RID: 12075 RVA: 0x000B553C File Offset: 0x000B373C
		// (set) Token: 0x06002F2C RID: 12076 RVA: 0x000B5544 File Offset: 0x000B3744
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
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

		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06002F2D RID: 12077 RVA: 0x000B5550 File Offset: 0x000B3750
		// (set) Token: 0x06002F2E RID: 12078 RVA: 0x000B5558 File Offset: 0x000B3758
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				if (base.BackColor == value)
				{
					return;
				}
				base.BackColor = value;
				this.Refresh();
			}
		}

		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x000B557C File Offset: 0x000B377C
		// (set) Token: 0x06002F30 RID: 12080 RVA: 0x000B5584 File Offset: 0x000B3784
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				if (base.BackgroundImage == value)
				{
					return;
				}
				base.BackgroundImage = value;
			}
		}

		/// <returns>One of the values of <see cref="T:System.Windows.Forms.ImageLayout" /> (<see cref="F:System.Windows.Forms.ImageLayout.Center" /> , <see cref="F:System.Windows.Forms.ImageLayout.None" />, <see cref="F:System.Windows.Forms.ImageLayout.Stretch" />, <see cref="F:System.Windows.Forms.ImageLayout.Tile" />, or <see cref="F:System.Windows.Forms.ImageLayout.Zoom" />). <see cref="F:System.Windows.Forms.ImageLayout.Tile" /> is the default value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06002F31 RID: 12081 RVA: 0x000B559C File Offset: 0x000B379C
		// (set) Token: 0x06002F32 RID: 12082 RVA: 0x000B55A4 File Offset: 0x000B37A4
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

		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06002F33 RID: 12083 RVA: 0x000B55B0 File Offset: 0x000B37B0
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets the default distance between the <see cref="T:System.Windows.Forms.ScrollBar" /> control edges and its contents.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.Padding.Empty" /> in all cases.</returns>
		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06002F34 RID: 12084 RVA: 0x000B55B8 File Offset: 0x000B37B8
		protected override Padding DefaultMargin
		{
			get
			{
				return Padding.Empty;
			}
		}

		/// <summary>Gets the default Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06002F35 RID: 12085 RVA: 0x000B55C0 File Offset: 0x000B37C0
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		/// <returns>The <see cref="T:System.Drawing.Font" /> to apply to the text displayed by the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultFont" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06002F36 RID: 12086 RVA: 0x000B55C4 File Offset: 0x000B37C4
		// (set) Token: 0x06002F37 RID: 12087 RVA: 0x000B55CC File Offset: 0x000B37CC
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				if (base.Font.Equals(value))
				{
					return;
				}
				base.Font = value;
			}
		}

		/// <summary>Gets or sets the foreground color of the scroll bar control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color for this scroll bar control. The default is the foreground color of the parent control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06002F38 RID: 12088 RVA: 0x000B55E8 File Offset: 0x000B37E8
		// (set) Token: 0x06002F39 RID: 12089 RVA: 0x000B55F0 File Offset: 0x000B37F0
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
				if (base.ForeColor == value)
				{
					return;
				}
				base.ForeColor = value;
				this.Refresh();
			}
		}

		/// <summary>Gets or sets the Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06002F3A RID: 12090 RVA: 0x000B5614 File Offset: 0x000B3814
		// (set) Token: 0x06002F3B RID: 12091 RVA: 0x000B561C File Offset: 0x000B381C
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
				if (base.ImeMode == value)
				{
					return;
				}
				base.ImeMode = value;
			}
		}

		/// <summary>Gets or sets a value to be added to or subtracted from the <see cref="P:System.Windows.Forms.ScrollBar.Value" /> property when the scroll box is moved a large distance.</summary>
		/// <returns>A numeric value. The default value is 10.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than 0. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06002F3C RID: 12092 RVA: 0x000B5634 File Offset: 0x000B3834
		// (set) Token: 0x06002F3D RID: 12093 RVA: 0x000B5650 File Offset: 0x000B3850
		[RefreshProperties(2)]
		[DefaultValue(10)]
		[MWFCategory("Behaviour")]
		[MWFDescription("Scroll amount when clicking in the scroll area")]
		public int LargeChange
		{
			get
			{
				return Math.Min(this.large_change, this.maximum - this.minimum + 1);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("LargeChange", string.Format("Value '{0}' must be greater than or equal to 0.", value));
				}
				if (this.large_change != value)
				{
					this.large_change = value;
					this.CalcThumbArea();
					this.UpdatePos(this.Value, true);
					this.InvalidateDirty();
					this.OnUIAValueChanged(new ScrollEventArgs(ScrollEventType.LargeIncrement, value));
				}
			}
		}

		/// <summary>Gets or sets the upper limit of values of the scrollable range.</summary>
		/// <returns>A numeric value. The default value is 100.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06002F3E RID: 12094 RVA: 0x000B56B8 File Offset: 0x000B38B8
		// (set) Token: 0x06002F3F RID: 12095 RVA: 0x000B56C0 File Offset: 0x000B38C0
		[MWFCategory("Behaviour")]
		[MWFDescription("Highest value for scrollbar")]
		[RefreshProperties(2)]
		[DefaultValue(100)]
		public int Maximum
		{
			get
			{
				return this.maximum;
			}
			set
			{
				if (this.maximum == value)
				{
					return;
				}
				this.maximum = value;
				this.OnUIAValueChanged(new ScrollEventArgs(ScrollEventType.Last, value));
				if (this.maximum < this.minimum)
				{
					this.minimum = this.maximum;
				}
				if (this.Value > this.maximum)
				{
					this.Value = this.maximum;
				}
				this.CalcThumbArea();
				this.UpdatePos(this.Value, true);
				this.InvalidateDirty();
			}
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x000B5744 File Offset: 0x000B3944
		internal void SetValues(int maximum, int large_change)
		{
			this.SetValues(-1, maximum, -1, large_change);
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x000B5750 File Offset: 0x000B3950
		internal void SetValues(int minimum, int maximum, int small_change, int large_change)
		{
			bool flag = false;
			if (minimum != -1 && this.minimum != minimum)
			{
				this.minimum = minimum;
				if (minimum > this.maximum)
				{
					this.maximum = minimum;
				}
				flag = true;
				this.position = Math.Max(this.position, minimum);
			}
			if (maximum != -1 && this.maximum != maximum)
			{
				this.maximum = maximum;
				if (maximum < this.minimum)
				{
					this.minimum = maximum;
				}
				flag = true;
				this.position = Math.Min(this.position, maximum);
			}
			if (small_change != -1 && this.small_change != small_change)
			{
				this.small_change = small_change;
			}
			if (this.large_change != large_change)
			{
				this.large_change = large_change;
				flag = true;
			}
			if (flag)
			{
				this.CalcThumbArea();
				this.UpdatePos(this.Value, true);
				this.InvalidateDirty();
			}
		}

		/// <summary>Gets or sets the lower limit of values of the scrollable range.</summary>
		/// <returns>A numeric value. The default value is 0.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06002F42 RID: 12098 RVA: 0x000B5834 File Offset: 0x000B3A34
		// (set) Token: 0x06002F43 RID: 12099 RVA: 0x000B583C File Offset: 0x000B3A3C
		[RefreshProperties(2)]
		[MWFCategory("Behaviour")]
		[MWFDescription("Smallest value for scrollbar")]
		[DefaultValue(0)]
		public int Minimum
		{
			get
			{
				return this.minimum;
			}
			set
			{
				if (this.minimum == value)
				{
					return;
				}
				this.minimum = value;
				this.OnUIAValueChanged(new ScrollEventArgs(ScrollEventType.First, value));
				if (this.minimum > this.maximum)
				{
					this.maximum = this.minimum;
				}
				this.CalcThumbArea();
				this.UpdatePos(this.Value, true);
				this.InvalidateDirty();
			}
		}

		/// <summary>Gets or sets the value to be added to or subtracted from the <see cref="P:System.Windows.Forms.ScrollBar.Value" /> property when the scroll box is moved a small distance.</summary>
		/// <returns>A numeric value. The default value is 1.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than 0. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x06002F44 RID: 12100 RVA: 0x000B58A0 File Offset: 0x000B3AA0
		// (set) Token: 0x06002F45 RID: 12101 RVA: 0x000B58D0 File Offset: 0x000B3AD0
		[MWFDescription("Scroll amount when clicking scroll arrows")]
		[DefaultValue(1)]
		[MWFCategory("Behaviour")]
		public int SmallChange
		{
			get
			{
				return (this.small_change <= this.LargeChange) ? this.small_change : this.LargeChange;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("SmallChange", string.Format("Value '{0}' must be greater than or equal to 0.", value));
				}
				if (this.small_change != value)
				{
					this.small_change = value;
					this.UpdatePos(this.Value, true);
					this.InvalidateDirty();
					this.OnUIAValueChanged(new ScrollEventArgs(ScrollEventType.SmallIncrement, value));
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the user can give the focus to the <see cref="T:System.Windows.Forms.ScrollBar" /> control by using the TAB key.</summary>
		/// <returns>true if the user can give the focus to the control by using the TAB key; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06002F46 RID: 12102 RVA: 0x000B5934 File Offset: 0x000B3B34
		// (set) Token: 0x06002F47 RID: 12103 RVA: 0x000B593C File Offset: 0x000B3B3C
		[DefaultValue(false)]
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

		/// <returns>The text associated with this control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x06002F48 RID: 12104 RVA: 0x000B5948 File Offset: 0x000B3B48
		// (set) Token: 0x06002F49 RID: 12105 RVA: 0x000B5950 File Offset: 0x000B3B50
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Bindable(false)]
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

		/// <summary>Gets or sets a numeric value that represents the current position of the scroll box on the scroll bar control.</summary>
		/// <returns>A numeric value that is within the <see cref="P:System.Windows.Forms.ScrollBar.Minimum" /> and <see cref="P:System.Windows.Forms.ScrollBar.Maximum" /> range. The default value is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The assigned value is less than the <see cref="P:System.Windows.Forms.ScrollBar.Minimum" /> property value.-or- The assigned value is greater than the <see cref="P:System.Windows.Forms.ScrollBar.Maximum" /> property value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x06002F4A RID: 12106 RVA: 0x000B595C File Offset: 0x000B3B5C
		// (set) Token: 0x06002F4B RID: 12107 RVA: 0x000B5964 File Offset: 0x000B3B64
		[MWFCategory("Behaviour")]
		[Bindable(true)]
		[DefaultValue(0)]
		[MWFDescription("Current value for scrollbar")]
		public int Value
		{
			get
			{
				return this.position;
			}
			set
			{
				if (value < this.minimum || value > this.maximum)
				{
					throw new ArgumentOutOfRangeException("Value", string.Format("'{0}' is not a valid value for 'Value'. 'Value' should be between 'Minimum' and 'Maximum'", value));
				}
				if (this.position != value)
				{
					this.position = value;
					this.OnValueChanged(EventArgs.Empty);
					if (base.IsHandleCreated)
					{
						Rectangle rectangle = this.thumb_pos;
						this.UpdateThumbPos(((!this.vert) ? this.thumb_area.X : this.thumb_area.Y) + (int)((float)(this.position - this.minimum) * this.pixel_per_pos), false, false);
						this.MoveThumb(rectangle, (!this.vert) ? this.thumb_pos.X : this.thumb_pos.Y);
					}
				}
			}
		}

		/// <summary>Returns the bounds to use when the <see cref="T:System.Windows.Forms.ScrollBar" /> is scaled by a specified amount.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> specifying the scaled bounds.</returns>
		/// <param name="bounds">A <see cref="T:System.Drawing.Rectangle" /> that specifies the initial bounds.</param>
		/// <param name="factor">A <see cref="T:System.Drawing.SizeF" /> that indicates the amount the current bounds should be increased by.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values that indicate the how to define the control's size and position returned by <see cref="M:System.Windows.Forms.ScrollBar.GetScaledBounds(System.Drawing.Rectangle,System.Drawing.SizeF,System.Windows.Forms.BoundsSpecified)" />. </param>
		// Token: 0x06002F4C RID: 12108 RVA: 0x000B5A48 File Offset: 0x000B3C48
		protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			if (this.vert)
			{
				return base.GetScaledBounds(bounds, factor, (specified & BoundsSpecified.Height) | (specified & BoundsSpecified.Location));
			}
			return base.GetScaledBounds(bounds, factor, (specified & BoundsSpecified.Width) | (specified & BoundsSpecified.Location));
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002F4D RID: 12109 RVA: 0x000B5A80 File Offset: 0x000B3C80
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			if (base.Enabled)
			{
				this.firstbutton_state = (this.secondbutton_state = ButtonState.Normal);
			}
			else
			{
				this.firstbutton_state = (this.secondbutton_state = ButtonState.Inactive);
			}
			this.Refresh();
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002F4E RID: 12110 RVA: 0x000B5AD0 File Offset: 0x000B3CD0
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.CalcButtonSizes();
			this.CalcThumbArea();
			this.UpdateThumbPos(this.thumb_area.Y + (int)((float)(this.position - this.minimum) * this.pixel_per_pos), true, false);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ScrollBar.Scroll" /> event.</summary>
		/// <param name="se">A <see cref="T:System.Windows.Forms.ScrollEventArgs" /> that contains the event data. </param>
		// Token: 0x06002F4F RID: 12111 RVA: 0x000B5B1C File Offset: 0x000B3D1C
		protected virtual void OnScroll(ScrollEventArgs se)
		{
			ScrollEventHandler scrollEventHandler = (ScrollEventHandler)base.Events[ScrollBar.ScrollEvent];
			if (scrollEventHandler == null)
			{
				return;
			}
			if (se.NewValue < this.Minimum)
			{
				se.NewValue = this.Minimum;
			}
			if (se.NewValue > this.Maximum)
			{
				se.NewValue = this.Maximum;
			}
			scrollEventHandler(this, se);
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x000B5B88 File Offset: 0x000B3D88
		private void SendWMScroll(ScrollBarCommands cmd)
		{
			if (base.Parent != null && base.Parent.IsHandleCreated)
			{
				if (this.vert)
				{
					XplatUI.SendMessage(base.Parent.Handle, Msg.WM_VSCROLL, (IntPtr)((int)cmd), (!this.implicit_control) ? this.Handle : IntPtr.Zero);
				}
				else
				{
					XplatUI.SendMessage(base.Parent.Handle, Msg.WM_HSCROLL, (IntPtr)((int)cmd), (!this.implicit_control) ? this.Handle : IntPtr.Zero);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ScrollBar.ValueChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002F51 RID: 12113 RVA: 0x000B5C30 File Offset: 0x000B3E30
		protected virtual void OnValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ScrollBar.ValueChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ScrollBar" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.ScrollBar" />. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002F52 RID: 12114 RVA: 0x000B5C64 File Offset: 0x000B3E64
		public override string ToString()
		{
			return string.Format("{0}, Minimum: {1}, Maximum: {2}, Value: {3}", new object[]
			{
				base.GetType().FullName,
				this.minimum,
				this.maximum,
				this.position
			});
		}

		/// <summary>Updates the <see cref="T:System.Windows.Forms.ScrollBar" /> control.</summary>
		// Token: 0x06002F53 RID: 12115 RVA: 0x000B5CBC File Offset: 0x000B3EBC
		protected void UpdateScrollInfo()
		{
			this.Refresh();
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" /> method.</summary>
		// Token: 0x06002F54 RID: 12116 RVA: 0x000B5CC4 File Offset: 0x000B3EC4
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x000B5CD0 File Offset: 0x000B3ED0
		private void CalcButtonSizes()
		{
			if (this.vert)
			{
				if (base.Height < ThemeEngine.Current.ScrollBarButtonSize * 2)
				{
					this.scrollbutton_height = base.Height / 2;
				}
				else
				{
					this.scrollbutton_height = ThemeEngine.Current.ScrollBarButtonSize;
				}
			}
			else if (base.Width < ThemeEngine.Current.ScrollBarButtonSize * 2)
			{
				this.scrollbutton_width = base.Width / 2;
			}
			else
			{
				this.scrollbutton_width = ThemeEngine.Current.ScrollBarButtonSize;
			}
		}

		// Token: 0x06002F56 RID: 12118 RVA: 0x000B5D64 File Offset: 0x000B3F64
		private void CalcThumbArea()
		{
			int num = ((!this.use_manual_thumb_size) ? this.LargeChange : this.manual_thumb_size);
			if (this.vert)
			{
				this.thumb_area.Height = base.Height - this.scrollbutton_height - this.scrollbutton_height;
				this.thumb_area.X = 0;
				this.thumb_area.Y = this.scrollbutton_height;
				this.thumb_area.Width = base.Width;
				if (base.Height < 40)
				{
					this.thumb_size = 0;
				}
				else
				{
					double num2 = (double)num / (double)(1 + this.maximum - this.minimum);
					this.thumb_size = 1 + (int)((double)this.thumb_area.Height * num2);
					if (this.thumb_size < 8)
					{
						this.thumb_size = 8;
					}
					if (this.LargeChange == 0)
					{
						this.thumb_size = 17;
					}
				}
				this.pixel_per_pos = (float)(this.thumb_area.Height - this.thumb_size) / (float)(this.maximum - this.minimum - num + 1);
			}
			else
			{
				this.thumb_area.Y = 0;
				this.thumb_area.X = this.scrollbutton_width;
				this.thumb_area.Height = base.Height;
				this.thumb_area.Width = base.Width - this.scrollbutton_width - this.scrollbutton_width;
				if (base.Width < 40)
				{
					this.thumb_size = 0;
				}
				else
				{
					double num3 = (double)num / (double)(1 + this.maximum - this.minimum);
					this.thumb_size = 1 + (int)((double)this.thumb_area.Width * num3);
					if (this.thumb_size < 8)
					{
						this.thumb_size = 8;
					}
					if (this.LargeChange == 0)
					{
						this.thumb_size = 17;
					}
				}
				this.pixel_per_pos = (float)(this.thumb_area.Width - this.thumb_size) / (float)(this.maximum - this.minimum - num + 1);
			}
		}

		// Token: 0x06002F57 RID: 12119 RVA: 0x000B5F64 File Offset: 0x000B4164
		private void LargeIncrement()
		{
			int num = Math.Min(this.MaximumAllowed, this.position + this.large_change);
			ScrollEventArgs scrollEventArgs = new ScrollEventArgs(ScrollEventType.LargeIncrement, num);
			this.OnScroll(scrollEventArgs);
			this.Value = scrollEventArgs.NewValue;
			scrollEventArgs = new ScrollEventArgs(ScrollEventType.EndScroll, this.Value);
			this.OnScroll(scrollEventArgs);
			this.Value = scrollEventArgs.NewValue;
			this.OnUIAScroll(new ScrollEventArgs(ScrollEventType.LargeIncrement, this.Value));
		}

		// Token: 0x06002F58 RID: 12120 RVA: 0x000B5FD8 File Offset: 0x000B41D8
		private void LargeDecrement()
		{
			int num = Math.Max(this.Minimum, this.position - this.large_change);
			ScrollEventArgs scrollEventArgs = new ScrollEventArgs(ScrollEventType.LargeDecrement, num);
			this.OnScroll(scrollEventArgs);
			this.Value = scrollEventArgs.NewValue;
			scrollEventArgs = new ScrollEventArgs(ScrollEventType.EndScroll, this.Value);
			this.OnScroll(scrollEventArgs);
			this.Value = scrollEventArgs.NewValue;
			this.OnUIAScroll(new ScrollEventArgs(ScrollEventType.LargeDecrement, this.Value));
		}

		// Token: 0x06002F59 RID: 12121 RVA: 0x000B604C File Offset: 0x000B424C
		private void OnResizeSB(object o, EventArgs e)
		{
			if (base.Width <= 0 || base.Height <= 0)
			{
				return;
			}
			this.CalcButtonSizes();
			this.CalcThumbArea();
			this.UpdatePos(this.position, true);
			this.Refresh();
		}

		// Token: 0x06002F5A RID: 12122 RVA: 0x000B6094 File Offset: 0x000B4294
		internal override void OnPaintInternal(PaintEventArgs pevent)
		{
			ThemeEngine.Current.DrawScrollBar(pevent.Graphics, pevent.ClipRectangle, this);
		}

		// Token: 0x06002F5B RID: 12123 RVA: 0x000B60B8 File Offset: 0x000B42B8
		private void OnTimer(object source, EventArgs e)
		{
			this.ClearDirty();
			switch (this.timer_type)
			{
			case ScrollBar.TimerType.HoldButton:
				this.SetRepeatButtonTimer();
				break;
			case ScrollBar.TimerType.RepeatButton:
				if ((this.firstbutton_state & ButtonState.Pushed) == ButtonState.Pushed && this.position != this.Minimum)
				{
					this.SmallDecrement();
					this.SendWMScroll(ScrollBarCommands.SB_LINEUP);
				}
				if ((this.secondbutton_state & ButtonState.Pushed) == ButtonState.Pushed && this.position != this.Maximum)
				{
					this.SmallIncrement();
					this.SendWMScroll(ScrollBarCommands.SB_LINEDOWN);
				}
				break;
			case ScrollBar.TimerType.HoldThumbArea:
				this.SetRepeatThumbAreaTimer();
				break;
			case ScrollBar.TimerType.RepeatThumbArea:
			{
				Rectangle rectangle = this.thumb_area;
				Point point = base.PointToScreen(new Point(this.thumb_area.X, this.thumb_area.Y));
				rectangle.X = point.X;
				rectangle.Y = point.Y;
				if (!rectangle.Contains(Control.MousePosition))
				{
					this.timer.Enabled = false;
					this.thumb_moving = ScrollBar.ThumbMoving.None;
					this.DirtyThumbArea();
					this.InvalidateDirty();
				}
				Point point2 = base.PointToClient(Control.MousePosition);
				if (this.vert)
				{
					this.lastclick_pos = point2.Y;
				}
				else
				{
					this.lastclick_pos = point2.X;
				}
				if (this.thumb_moving == ScrollBar.ThumbMoving.Forward)
				{
					if ((this.vert && this.thumb_pos.Y + this.thumb_size > this.lastclick_pos) || (!this.vert && this.thumb_pos.X + this.thumb_size > this.lastclick_pos) || !this.thumb_area.Contains(point2))
					{
						this.timer.Enabled = false;
						this.thumb_moving = ScrollBar.ThumbMoving.None;
						this.Refresh();
						return;
					}
					this.LargeIncrement();
					this.SendWMScroll(ScrollBarCommands.SB_PAGEDOWN);
				}
				else if ((this.vert && this.thumb_pos.Y < this.lastclick_pos) || (!this.vert && this.thumb_pos.X < this.lastclick_pos))
				{
					this.timer.Enabled = false;
					this.thumb_moving = ScrollBar.ThumbMoving.None;
					this.SendWMScroll(ScrollBarCommands.SB_PAGEUP);
					this.Refresh();
				}
				else
				{
					this.LargeDecrement();
					this.SendWMScroll(ScrollBarCommands.SB_PAGEUP);
				}
				break;
			}
			}
			this.InvalidateDirty();
		}

		// Token: 0x06002F5C RID: 12124 RVA: 0x000B6338 File Offset: 0x000B4538
		private void MoveThumb(Rectangle original_thumbpos, int value)
		{
			if (this.vert)
			{
				int num = value - original_thumbpos.Y;
				if (num < 0)
				{
					original_thumbpos.Y += num;
					original_thumbpos.Height -= num;
				}
				else
				{
					original_thumbpos.Height += num;
				}
				XplatUI.ScrollWindow(this.Handle, original_thumbpos, 0, num, false);
			}
			else
			{
				int num = value - original_thumbpos.X;
				if (num < 0)
				{
					original_thumbpos.X += num;
					original_thumbpos.Width -= num;
				}
				else
				{
					original_thumbpos.Width += num;
				}
				XplatUI.ScrollWindow(this.Handle, original_thumbpos, num, 0, false);
			}
			base.Update();
		}

		// Token: 0x06002F5D RID: 12125 RVA: 0x000B6400 File Offset: 0x000B4600
		private void OnMouseMoveSB(object sender, MouseEventArgs e)
		{
			if (!base.Enabled)
			{
				return;
			}
			this.FirstButtonEntered = this.first_arrow_area.Contains(e.Location);
			this.SecondButtonEntered = this.second_arrow_area.Contains(e.Location);
			if (this.thumb_size == 0)
			{
				return;
			}
			this.ThumbEntered = this.thumb_pos.Contains(e.Location);
			if (this.firstbutton_pressed)
			{
				if (!this.first_arrow_area.Contains(e.X, e.Y) && (this.firstbutton_state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					this.firstbutton_state = ButtonState.Normal;
					base.Invalidate(this.first_arrow_area);
					base.Update();
					return;
				}
				if (this.first_arrow_area.Contains(e.X, e.Y))
				{
					this.firstbutton_state = ButtonState.Pushed;
					base.Invalidate(this.first_arrow_area);
					base.Update();
					return;
				}
			}
			else if (this.secondbutton_pressed)
			{
				if (!this.second_arrow_area.Contains(e.X, e.Y) && (this.secondbutton_state & ButtonState.Pushed) == ButtonState.Pushed)
				{
					this.secondbutton_state = ButtonState.Normal;
					base.Invalidate(this.second_arrow_area);
					base.Update();
					return;
				}
				if (this.second_arrow_area.Contains(e.X, e.Y))
				{
					this.secondbutton_state = ButtonState.Pushed;
					base.Invalidate(this.second_arrow_area);
					base.Update();
					return;
				}
			}
			else if (this.thumb_pressed)
			{
				if (this.vert)
				{
					int num = e.Y - this.thumbclick_offset;
					if (num < this.thumb_area.Y)
					{
						num = this.thumb_area.Y;
					}
					else if (num > this.thumb_area.Bottom - this.thumb_size)
					{
						num = this.thumb_area.Bottom - this.thumb_size;
					}
					if (num != this.thumb_pos.Y)
					{
						Rectangle rectangle = this.thumb_pos;
						this.UpdateThumbPos(num, false, true);
						this.MoveThumb(rectangle, this.thumb_pos.Y);
						this.OnScroll(new ScrollEventArgs(ScrollEventType.ThumbTrack, this.position));
					}
					this.SendWMScroll(ScrollBarCommands.SB_THUMBTRACK);
				}
				else
				{
					int num2 = e.X - this.thumbclick_offset;
					if (num2 < this.thumb_area.X)
					{
						num2 = this.thumb_area.X;
					}
					else if (num2 > this.thumb_area.Right - this.thumb_size)
					{
						num2 = this.thumb_area.Right - this.thumb_size;
					}
					if (num2 != this.thumb_pos.X)
					{
						Rectangle rectangle2 = this.thumb_pos;
						this.UpdateThumbPos(num2, false, true);
						this.MoveThumb(rectangle2, this.thumb_pos.X);
						this.OnScroll(new ScrollEventArgs(ScrollEventType.ThumbTrack, this.position));
					}
					this.SendWMScroll(ScrollBarCommands.SB_THUMBTRACK);
				}
			}
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x000B6700 File Offset: 0x000B4900
		private void OnMouseDownSB(object sender, MouseEventArgs e)
		{
			this.ClearDirty();
			if (!base.Enabled || (e.Button & MouseButtons.Left) == MouseButtons.None)
			{
				return;
			}
			if (this.firstbutton_state != ButtonState.Inactive && this.first_arrow_area.Contains(e.X, e.Y))
			{
				this.SendWMScroll(ScrollBarCommands.SB_LINEUP);
				this.firstbutton_state = ButtonState.Pushed;
				this.firstbutton_pressed = true;
				base.Invalidate(this.first_arrow_area);
				base.Update();
				if (!this.timer.Enabled)
				{
					this.SetHoldButtonClickTimer();
					this.timer.Enabled = true;
				}
			}
			if (this.secondbutton_state != ButtonState.Inactive && this.second_arrow_area.Contains(e.X, e.Y))
			{
				this.SendWMScroll(ScrollBarCommands.SB_LINEDOWN);
				this.secondbutton_state = ButtonState.Pushed;
				this.secondbutton_pressed = true;
				base.Invalidate(this.second_arrow_area);
				base.Update();
				if (!this.timer.Enabled)
				{
					this.SetHoldButtonClickTimer();
					this.timer.Enabled = true;
				}
			}
			if (this.thumb_size > 0 && this.thumb_pos.Contains(e.X, e.Y))
			{
				this.ThumbPressed = true;
				this.SendWMScroll(ScrollBarCommands.SB_THUMBTRACK);
				if (this.vert)
				{
					this.thumbclick_offset = e.Y - this.thumb_pos.Y;
					this.lastclick_pos = e.Y;
				}
				else
				{
					this.thumbclick_offset = e.X - this.thumb_pos.X;
					this.lastclick_pos = e.X;
				}
			}
			else if (this.thumb_size > 0 && this.thumb_area.Contains(e.X, e.Y))
			{
				if (this.vert)
				{
					this.lastclick_pos = e.Y;
					if (e.Y > this.thumb_pos.Y + this.thumb_pos.Height)
					{
						this.SendWMScroll(ScrollBarCommands.SB_PAGEDOWN);
						this.LargeIncrement();
						this.thumb_moving = ScrollBar.ThumbMoving.Forward;
						this.Dirty(new Rectangle(0, this.thumb_pos.Y + this.thumb_pos.Height, base.ClientRectangle.Width, base.ClientRectangle.Height - (this.thumb_pos.Y + this.thumb_pos.Height) - this.scrollbutton_height));
					}
					else
					{
						this.SendWMScroll(ScrollBarCommands.SB_PAGEUP);
						this.LargeDecrement();
						this.thumb_moving = ScrollBar.ThumbMoving.Backwards;
						this.Dirty(new Rectangle(0, this.scrollbutton_height, base.ClientRectangle.Width, this.thumb_pos.Y - this.scrollbutton_height));
					}
				}
				else
				{
					this.lastclick_pos = e.X;
					if (e.X > this.thumb_pos.X + this.thumb_pos.Width)
					{
						this.SendWMScroll(ScrollBarCommands.SB_PAGEDOWN);
						this.thumb_moving = ScrollBar.ThumbMoving.Forward;
						this.LargeIncrement();
						this.Dirty(new Rectangle(this.thumb_pos.X + this.thumb_pos.Width, 0, base.ClientRectangle.Width - (this.thumb_pos.X + this.thumb_pos.Width) - this.scrollbutton_width, base.ClientRectangle.Height));
					}
					else
					{
						this.SendWMScroll(ScrollBarCommands.SB_PAGEUP);
						this.thumb_moving = ScrollBar.ThumbMoving.Backwards;
						this.LargeDecrement();
						this.Dirty(new Rectangle(this.scrollbutton_width, 0, this.thumb_pos.X - this.scrollbutton_width, base.ClientRectangle.Height));
					}
				}
				this.SetHoldThumbAreaTimer();
				this.timer.Enabled = true;
				this.InvalidateDirty();
			}
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x000B6AE0 File Offset: 0x000B4CE0
		private void OnMouseUpSB(object sender, MouseEventArgs e)
		{
			this.ClearDirty();
			if (!base.Enabled)
			{
				return;
			}
			this.timer.Enabled = false;
			if (this.thumb_moving != ScrollBar.ThumbMoving.None)
			{
				this.DirtyThumbArea();
				this.thumb_moving = ScrollBar.ThumbMoving.None;
			}
			if (this.firstbutton_pressed)
			{
				this.firstbutton_state = ButtonState.Normal;
				if (this.first_arrow_area.Contains(e.X, e.Y))
				{
					this.SmallDecrement();
				}
				this.SendWMScroll(ScrollBarCommands.SB_LINEUP);
				this.firstbutton_pressed = false;
				this.Dirty(this.first_arrow_area);
			}
			else if (this.secondbutton_pressed)
			{
				this.secondbutton_state = ButtonState.Normal;
				if (this.second_arrow_area.Contains(e.X, e.Y))
				{
					this.SmallIncrement();
				}
				this.SendWMScroll(ScrollBarCommands.SB_LINEDOWN);
				this.Dirty(this.second_arrow_area);
				this.secondbutton_pressed = false;
			}
			else if (this.thumb_pressed)
			{
				this.OnScroll(new ScrollEventArgs(ScrollEventType.ThumbPosition, this.position));
				this.OnScroll(new ScrollEventArgs(ScrollEventType.EndScroll, this.position));
				this.SendWMScroll(ScrollBarCommands.SB_THUMBPOSITION);
				this.ThumbPressed = false;
				return;
			}
			this.InvalidateDirty();
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x000B6C10 File Offset: 0x000B4E10
		private void OnKeyDownSB(object o, KeyEventArgs key)
		{
			if (!base.Enabled)
			{
				return;
			}
			this.ClearDirty();
			switch (key.KeyCode)
			{
			case Keys.PageUp:
				this.LargeDecrement();
				break;
			case Keys.PageDown:
				this.LargeIncrement();
				break;
			case Keys.End:
				this.SetEndPosition();
				break;
			case Keys.Home:
				this.SetHomePosition();
				break;
			case Keys.Up:
				this.SmallDecrement();
				break;
			case Keys.Down:
				this.SmallIncrement();
				break;
			}
			this.InvalidateDirty();
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x000B6CB4 File Offset: 0x000B4EB4
		internal void SafeValueSet(int value)
		{
			value = Math.Min(value, this.maximum);
			value = Math.Max(value, this.minimum);
			this.Value = value;
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x000B6CDC File Offset: 0x000B4EDC
		private void SetEndPosition()
		{
			int num = this.MaximumAllowed;
			ScrollEventArgs scrollEventArgs = new ScrollEventArgs(ScrollEventType.Last, num);
			this.OnScroll(scrollEventArgs);
			num = scrollEventArgs.NewValue;
			scrollEventArgs = new ScrollEventArgs(ScrollEventType.EndScroll, num);
			this.OnScroll(scrollEventArgs);
			num = scrollEventArgs.NewValue;
			this.SetValue(num);
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x000B6D24 File Offset: 0x000B4F24
		private void SetHomePosition()
		{
			int num = this.Minimum;
			ScrollEventArgs scrollEventArgs = new ScrollEventArgs(ScrollEventType.First, num);
			this.OnScroll(scrollEventArgs);
			num = scrollEventArgs.NewValue;
			scrollEventArgs = new ScrollEventArgs(ScrollEventType.EndScroll, num);
			this.OnScroll(scrollEventArgs);
			num = scrollEventArgs.NewValue;
			this.SetValue(num);
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x000B6D6C File Offset: 0x000B4F6C
		private void SmallIncrement()
		{
			int num = Math.Min(this.MaximumAllowed, this.position + this.SmallChange);
			ScrollEventArgs scrollEventArgs = new ScrollEventArgs(ScrollEventType.SmallIncrement, num);
			this.OnScroll(scrollEventArgs);
			this.Value = scrollEventArgs.NewValue;
			scrollEventArgs = new ScrollEventArgs(ScrollEventType.EndScroll, this.Value);
			this.OnScroll(scrollEventArgs);
			this.Value = scrollEventArgs.NewValue;
			this.OnUIAScroll(new ScrollEventArgs(ScrollEventType.SmallIncrement, this.Value));
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x000B6DE0 File Offset: 0x000B4FE0
		private void SmallDecrement()
		{
			int num = Math.Max(this.Minimum, this.position - this.SmallChange);
			ScrollEventArgs scrollEventArgs = new ScrollEventArgs(ScrollEventType.SmallDecrement, num);
			this.OnScroll(scrollEventArgs);
			this.Value = scrollEventArgs.NewValue;
			scrollEventArgs = new ScrollEventArgs(ScrollEventType.EndScroll, this.Value);
			this.OnScroll(scrollEventArgs);
			this.Value = scrollEventArgs.NewValue;
			this.OnUIAScroll(new ScrollEventArgs(ScrollEventType.SmallDecrement, this.Value));
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x000B6E54 File Offset: 0x000B5054
		private void SetHoldButtonClickTimer()
		{
			this.timer.Enabled = false;
			this.timer.Interval = 200;
			this.timer_type = ScrollBar.TimerType.HoldButton;
			this.timer.Enabled = true;
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x000B6E88 File Offset: 0x000B5088
		private void SetRepeatButtonTimer()
		{
			this.timer.Enabled = false;
			this.timer.Interval = 50;
			this.timer_type = ScrollBar.TimerType.RepeatButton;
			this.timer.Enabled = true;
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x000B6EC4 File Offset: 0x000B50C4
		private void SetHoldThumbAreaTimer()
		{
			this.timer.Enabled = false;
			this.timer.Interval = 200;
			this.timer_type = ScrollBar.TimerType.HoldThumbArea;
			this.timer.Enabled = true;
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x000B6EF8 File Offset: 0x000B50F8
		private void SetRepeatThumbAreaTimer()
		{
			this.timer.Enabled = false;
			this.timer.Interval = 50;
			this.timer_type = ScrollBar.TimerType.RepeatThumbArea;
			this.timer.Enabled = true;
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x000B6F34 File Offset: 0x000B5134
		private void UpdatePos(int newPos, bool update_thumbpos)
		{
			int num;
			if (newPos < this.minimum)
			{
				num = this.minimum;
			}
			else if (newPos > this.MaximumAllowed)
			{
				num = this.MaximumAllowed;
			}
			else
			{
				num = newPos;
			}
			if (num < this.minimum)
			{
				num = this.minimum;
			}
			if (num > this.maximum)
			{
				num = this.maximum;
			}
			if (update_thumbpos)
			{
				if (this.vert)
				{
					this.UpdateThumbPos(this.thumb_area.Y + (int)((float)(num - this.minimum) * this.pixel_per_pos), true, false);
				}
				else
				{
					this.UpdateThumbPos(this.thumb_area.X + (int)((float)(num - this.minimum) * this.pixel_per_pos), true, false);
				}
				this.SetValue(num);
			}
			else
			{
				this.position = num;
				EventHandler eventHandler = (EventHandler)base.Events[ScrollBar.ValueChangedEvent];
				if (eventHandler != null)
				{
					eventHandler.Invoke(this, EventArgs.Empty);
				}
			}
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x000B7034 File Offset: 0x000B5234
		private void UpdateThumbPos(int pixel, bool dirty, bool update_value)
		{
			float num;
			if (this.vert)
			{
				if (dirty)
				{
					this.Dirty(this.thumb_pos);
				}
				if (pixel < this.thumb_area.Y)
				{
					this.thumb_pos.Y = this.thumb_area.Y;
				}
				else if (pixel > this.thumb_area.Bottom - this.thumb_size)
				{
					this.thumb_pos.Y = this.thumb_area.Bottom - this.thumb_size;
				}
				else
				{
					this.thumb_pos.Y = pixel;
				}
				this.thumb_pos.X = 0;
				this.thumb_pos.Width = ThemeEngine.Current.ScrollBarButtonSize;
				this.thumb_pos.Height = this.thumb_size;
				num = (float)(this.thumb_pos.Y - this.thumb_area.Y);
				num /= this.pixel_per_pos;
				if (dirty)
				{
					this.Dirty(this.thumb_pos);
				}
			}
			else
			{
				if (dirty)
				{
					this.Dirty(this.thumb_pos);
				}
				if (pixel < this.thumb_area.X)
				{
					this.thumb_pos.X = this.thumb_area.X;
				}
				else if (pixel > this.thumb_area.Right - this.thumb_size)
				{
					this.thumb_pos.X = this.thumb_area.Right - this.thumb_size;
				}
				else
				{
					this.thumb_pos.X = pixel;
				}
				this.thumb_pos.Y = 0;
				this.thumb_pos.Width = this.thumb_size;
				this.thumb_pos.Height = ThemeEngine.Current.ScrollBarButtonSize;
				num = (float)(this.thumb_pos.X - this.thumb_area.X);
				num /= this.pixel_per_pos;
				if (dirty)
				{
					this.Dirty(this.thumb_pos);
				}
			}
			if (update_value)
			{
				this.UpdatePos((int)num + this.minimum, false);
			}
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x000B7244 File Offset: 0x000B5444
		private void SetValue(int value)
		{
			if (value < this.minimum || value > this.maximum)
			{
				throw new ArgumentException(string.Format("'{0}' is not a valid value for 'Value'. 'Value' should be between 'Minimum' and 'Maximum'", value));
			}
			if (this.position != value)
			{
				this.position = value;
				this.OnValueChanged(EventArgs.Empty);
				this.UpdatePos(value, true);
			}
		}

		// Token: 0x06002F6D RID: 12141 RVA: 0x000B72A8 File Offset: 0x000B54A8
		private void ClearDirty()
		{
			this.dirty = Rectangle.Empty;
		}

		// Token: 0x06002F6E RID: 12142 RVA: 0x000B72B8 File Offset: 0x000B54B8
		private void Dirty(Rectangle r)
		{
			if (this.dirty == Rectangle.Empty)
			{
				this.dirty = r;
				return;
			}
			this.dirty = Rectangle.Union(this.dirty, r);
		}

		// Token: 0x06002F6F RID: 12143 RVA: 0x000B72EC File Offset: 0x000B54EC
		private void DirtyThumbArea()
		{
			if (this.thumb_moving == ScrollBar.ThumbMoving.Forward)
			{
				if (this.vert)
				{
					this.Dirty(new Rectangle(0, this.thumb_pos.Y + this.thumb_pos.Height, base.ClientRectangle.Width, base.ClientRectangle.Height - (this.thumb_pos.Y + this.thumb_pos.Height) - this.scrollbutton_height));
				}
				else
				{
					this.Dirty(new Rectangle(this.thumb_pos.X + this.thumb_pos.Width, 0, base.ClientRectangle.Width - (this.thumb_pos.X + this.thumb_pos.Width) - this.scrollbutton_width, base.ClientRectangle.Height));
				}
			}
			else if (this.thumb_moving == ScrollBar.ThumbMoving.Backwards)
			{
				if (this.vert)
				{
					this.Dirty(new Rectangle(0, this.scrollbutton_height, base.ClientRectangle.Width, this.thumb_pos.Y - this.scrollbutton_height));
				}
				else
				{
					this.Dirty(new Rectangle(this.scrollbutton_width, 0, this.thumb_pos.X - this.scrollbutton_width, base.ClientRectangle.Height));
				}
			}
		}

		// Token: 0x06002F70 RID: 12144 RVA: 0x000B7458 File Offset: 0x000B5658
		private void InvalidateDirty()
		{
			base.Invalidate(this.dirty);
			base.Update();
			this.dirty = Rectangle.Empty;
		}

		// Token: 0x06002F71 RID: 12145 RVA: 0x000B7478 File Offset: 0x000B5678
		private void OnMouseEnter(object sender, EventArgs e)
		{
			if (ThemeEngine.Current.ScrollBarHasHoverArrowButtonStyle)
			{
				Region region = new Region(this.first_arrow_area);
				region.Union(this.second_arrow_area);
				base.Invalidate(region);
			}
		}

		// Token: 0x06002F72 RID: 12146 RVA: 0x000B74B4 File Offset: 0x000B56B4
		private void OnMouseLeave(object sender, EventArgs e)
		{
			Region region = new Region();
			region.MakeEmpty();
			bool flag = false;
			if (ThemeEngine.Current.ScrollBarHasHoverArrowButtonStyle)
			{
				region.Union(this.first_arrow_area);
				region.Union(this.second_arrow_area);
				flag = true;
			}
			else if (ThemeEngine.Current.ScrollBarHasHotElementStyles)
			{
				if (this.first_button_entered)
				{
					region.Union(this.first_arrow_area);
					flag = true;
				}
				else if (this.second_button_entered)
				{
					region.Union(this.second_arrow_area);
					flag = true;
				}
			}
			if (ThemeEngine.Current.ScrollBarHasHotElementStyles && this.thumb_entered)
			{
				region.Union(this.thumb_pos);
				flag = true;
			}
			this.first_button_entered = false;
			this.second_button_entered = false;
			this.thumb_entered = false;
			if (flag)
			{
				base.Invalidate(region);
			}
			region.Dispose();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /></param>
		// Token: 0x06002F73 RID: 12147 RVA: 0x000B7594 File Offset: 0x000B5794
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
		}

		// Token: 0x06002F74 RID: 12148 RVA: 0x000B75A0 File Offset: 0x000B57A0
		internal void OnUIAScroll(ScrollEventArgs args)
		{
			ScrollEventHandler scrollEventHandler = (ScrollEventHandler)base.Events[ScrollBar.UIAScrollEvent];
			if (scrollEventHandler != null)
			{
				scrollEventHandler(this, args);
			}
		}

		// Token: 0x06002F75 RID: 12149 RVA: 0x000B75D4 File Offset: 0x000B57D4
		internal void OnUIAValueChanged(ScrollEventArgs args)
		{
			ScrollEventHandler scrollEventHandler = (ScrollEventHandler)base.Events[ScrollBar.UIAValueChangeEvent];
			if (scrollEventHandler != null)
			{
				scrollEventHandler(this, args);
			}
		}

		// Token: 0x06002F76 RID: 12150 RVA: 0x000B7608 File Offset: 0x000B5808
		internal void UIALargeIncrement()
		{
			this.LargeIncrement();
		}

		// Token: 0x06002F77 RID: 12151 RVA: 0x000B7610 File Offset: 0x000B5810
		internal void UIALargeDecrement()
		{
			this.LargeDecrement();
		}

		// Token: 0x06002F78 RID: 12152 RVA: 0x000B7618 File Offset: 0x000B5818
		internal void UIASmallIncrement()
		{
			this.SmallIncrement();
		}

		// Token: 0x06002F79 RID: 12153 RVA: 0x000B7620 File Offset: 0x000B5820
		internal void UIASmallDecrement()
		{
			this.SmallDecrement();
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06002F7A RID: 12154 RVA: 0x000B7628 File Offset: 0x000B5828
		internal Rectangle UIAThumbArea
		{
			get
			{
				return this.thumb_area;
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06002F7B RID: 12155 RVA: 0x000B7630 File Offset: 0x000B5830
		internal Rectangle UIAThumbPosition
		{
			get
			{
				return this.thumb_pos;
			}
		}

		// Token: 0x04001681 RID: 5761
		private const int thumb_min_size = 8;

		// Token: 0x04001682 RID: 5762
		private const int thumb_notshown_size = 40;

		// Token: 0x04001683 RID: 5763
		private int position;

		// Token: 0x04001684 RID: 5764
		private int minimum;

		// Token: 0x04001685 RID: 5765
		private int maximum;

		// Token: 0x04001686 RID: 5766
		private int large_change;

		// Token: 0x04001687 RID: 5767
		private int small_change;

		// Token: 0x04001688 RID: 5768
		internal int scrollbutton_height;

		// Token: 0x04001689 RID: 5769
		internal int scrollbutton_width;

		// Token: 0x0400168A RID: 5770
		private Rectangle first_arrow_area = default(Rectangle);

		// Token: 0x0400168B RID: 5771
		private Rectangle second_arrow_area = default(Rectangle);

		// Token: 0x0400168C RID: 5772
		private Rectangle thumb_pos = default(Rectangle);

		// Token: 0x0400168D RID: 5773
		private Rectangle thumb_area = default(Rectangle);

		// Token: 0x0400168E RID: 5774
		internal ButtonState firstbutton_state;

		// Token: 0x0400168F RID: 5775
		internal ButtonState secondbutton_state;

		// Token: 0x04001690 RID: 5776
		private bool firstbutton_pressed;

		// Token: 0x04001691 RID: 5777
		private bool secondbutton_pressed;

		// Token: 0x04001692 RID: 5778
		private bool thumb_pressed;

		// Token: 0x04001693 RID: 5779
		private float pixel_per_pos;

		// Token: 0x04001694 RID: 5780
		private Timer timer = new Timer();

		// Token: 0x04001695 RID: 5781
		private ScrollBar.TimerType timer_type;

		// Token: 0x04001696 RID: 5782
		private int thumb_size = 40;

		// Token: 0x04001697 RID: 5783
		internal bool use_manual_thumb_size;

		// Token: 0x04001698 RID: 5784
		internal int manual_thumb_size;

		// Token: 0x04001699 RID: 5785
		internal bool vert;

		// Token: 0x0400169A RID: 5786
		internal bool implicit_control;

		// Token: 0x0400169B RID: 5787
		private int lastclick_pos;

		// Token: 0x0400169C RID: 5788
		private int thumbclick_offset;

		// Token: 0x0400169D RID: 5789
		private Rectangle dirty;

		// Token: 0x0400169E RID: 5790
		internal ScrollBar.ThumbMoving thumb_moving;

		// Token: 0x0400169F RID: 5791
		private bool first_button_entered;

		// Token: 0x040016A0 RID: 5792
		private bool second_button_entered;

		// Token: 0x040016A1 RID: 5793
		private bool thumb_entered;

		// Token: 0x040016A5 RID: 5797
		private static object UIAValueChangeEvent;

		// Token: 0x020002C7 RID: 711
		private enum TimerType
		{
			// Token: 0x040016A7 RID: 5799
			HoldButton,
			// Token: 0x040016A8 RID: 5800
			RepeatButton,
			// Token: 0x040016A9 RID: 5801
			HoldThumbArea,
			// Token: 0x040016AA RID: 5802
			RepeatThumbArea
		}

		// Token: 0x020002C8 RID: 712
		internal enum ThumbMoving
		{
			// Token: 0x040016AC RID: 5804
			None,
			// Token: 0x040016AD RID: 5805
			Forward,
			// Token: 0x040016AE RID: 5806
			Backwards
		}
	}
}
