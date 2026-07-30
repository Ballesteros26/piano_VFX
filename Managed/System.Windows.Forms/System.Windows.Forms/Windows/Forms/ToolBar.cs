using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows toolbar. Although <see cref="T:System.Windows.Forms.ToolStrip" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.ToolBar" /> control of previous versions, <see cref="T:System.Windows.Forms.ToolBar" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000334 RID: 820
	[ClassInterface(1)]
	[ComVisible(true)]
	[DefaultEvent("ButtonClick")]
	[DefaultProperty("Buttons")]
	[Designer("System.Windows.Forms.Design.ToolBarDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public class ToolBar : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolBar" /> class.</summary>
		// Token: 0x060038E6 RID: 14566 RVA: 0x000EA728 File Offset: 0x000E8928
		public ToolBar()
		{
			this.background_color = ThemeEngine.Current.DefaultControlBackColor;
			this.foreground_color = ThemeEngine.Current.DefaultControlForeColor;
			this.buttons = new ToolBar.ToolBarButtonCollection(this);
			this.Dock = DockStyle.Top;
			base.GotFocus += new EventHandler(this.FocusChanged);
			base.LostFocus += new EventHandler(this.FocusChanged);
			base.MouseDown += this.ToolBar_MouseDown;
			base.MouseHover += new EventHandler(this.ToolBar_MouseHover);
			base.MouseLeave += new EventHandler(this.ToolBar_MouseLeave);
			base.MouseMove += this.ToolBar_MouseMove;
			base.MouseUp += this.ToolBar_MouseUp;
			this.BackgroundImageChanged += new EventHandler(this.ToolBar_BackgroundImageChanged);
			this.TabStop = false;
			base.SetStyle(ControlStyles.UserPaint, false);
			base.SetStyle(ControlStyles.FixedHeight, true);
			base.SetStyle(ControlStyles.FixedWidth, false);
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x000EA850 File Offset: 0x000E8A50
		// Note: this type is marked as 'beforefieldinit'.
		static ToolBar()
		{
			ToolBar.ButtonClickEvent = new object();
			ToolBar.ButtonDropDownEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolBar.AutoSize" /> property has changed.</summary>
		// Token: 0x14000344 RID: 836
		// (add) Token: 0x060038E8 RID: 14568 RVA: 0x000EA868 File Offset: 0x000E8A68
		// (remove) Token: 0x060038E9 RID: 14569 RVA: 0x000EA874 File Offset: 0x000E8A74
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.BackColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000345 RID: 837
		// (add) Token: 0x060038EA RID: 14570 RVA: 0x000EA880 File Offset: 0x000E8A80
		// (remove) Token: 0x060038EB RID: 14571 RVA: 0x000EA88C File Offset: 0x000E8A8C
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.BackgroundImage" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000346 RID: 838
		// (add) Token: 0x060038EC RID: 14572 RVA: 0x000EA898 File Offset: 0x000E8A98
		// (remove) Token: 0x060038ED RID: 14573 RVA: 0x000EA8A4 File Offset: 0x000E8AA4
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000347 RID: 839
		// (add) Token: 0x060038EE RID: 14574 RVA: 0x000EA8B0 File Offset: 0x000E8AB0
		// (remove) Token: 0x060038EF RID: 14575 RVA: 0x000EA8BC File Offset: 0x000E8ABC
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

		/// <summary>Occurs when a <see cref="T:System.Windows.Forms.ToolBarButton" /> on the <see cref="T:System.Windows.Forms.ToolBar" /> is clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000348 RID: 840
		// (add) Token: 0x060038F0 RID: 14576 RVA: 0x000EA8C8 File Offset: 0x000E8AC8
		// (remove) Token: 0x060038F1 RID: 14577 RVA: 0x000EA8DC File Offset: 0x000E8ADC
		public event ToolBarButtonClickEventHandler ButtonClick
		{
			add
			{
				base.Events.AddHandler(ToolBar.ButtonClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolBar.ButtonClickEvent, value);
			}
		}

		/// <summary>Occurs when a drop-down style <see cref="T:System.Windows.Forms.ToolBarButton" /> or its down arrow is clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000349 RID: 841
		// (add) Token: 0x060038F2 RID: 14578 RVA: 0x000EA8F0 File Offset: 0x000E8AF0
		// (remove) Token: 0x060038F3 RID: 14579 RVA: 0x000EA904 File Offset: 0x000E8B04
		public event ToolBarButtonClickEventHandler ButtonDropDown
		{
			add
			{
				base.Events.AddHandler(ToolBar.ButtonDropDownEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolBar.ButtonDropDownEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.ForeColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400034A RID: 842
		// (add) Token: 0x060038F4 RID: 14580 RVA: 0x000EA918 File Offset: 0x000E8B18
		// (remove) Token: 0x060038F5 RID: 14581 RVA: 0x000EA924 File Offset: 0x000E8B24
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.ImeMode" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400034B RID: 843
		// (add) Token: 0x060038F6 RID: 14582 RVA: 0x000EA930 File Offset: 0x000E8B30
		// (remove) Token: 0x060038F7 RID: 14583 RVA: 0x000EA93C File Offset: 0x000E8B3C
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

		/// <summary>This member is not meaningful for this control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400034C RID: 844
		// (add) Token: 0x060038F8 RID: 14584 RVA: 0x000EA948 File Offset: 0x000E8B48
		// (remove) Token: 0x060038F9 RID: 14585 RVA: 0x000EA954 File Offset: 0x000E8B54
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.RightToLeft" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400034D RID: 845
		// (add) Token: 0x060038FA RID: 14586 RVA: 0x000EA960 File Offset: 0x000E8B60
		// (remove) Token: 0x060038FB RID: 14587 RVA: 0x000EA96C File Offset: 0x000E8B6C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler RightToLeftChanged
		{
			add
			{
				base.RightToLeftChanged += value;
			}
			remove
			{
				base.RightToLeftChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolBar.Text" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400034E RID: 846
		// (add) Token: 0x060038FC RID: 14588 RVA: 0x000EA978 File Offset: 0x000E8B78
		// (remove) Token: 0x060038FD RID: 14589 RVA: 0x000EA984 File Offset: 0x000E8B84
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x060038FE RID: 14590 RVA: 0x000EA990 File Offset: 0x000E8B90
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams createParams = base.CreateParams;
				if (this.appearance == ToolBarAppearance.Flat)
				{
					createParams.Style |= 2048;
				}
				return createParams;
			}
		}

		/// <summary>Gets the default Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x060038FF RID: 14591 RVA: 0x000EA9C4 File Offset: 0x000E8BC4
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06003900 RID: 14592 RVA: 0x000EA9C8 File Offset: 0x000E8BC8
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.ToolBarDefaultSize;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value.</returns>
		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06003901 RID: 14593 RVA: 0x000EA9D4 File Offset: 0x000E8BD4
		// (set) Token: 0x06003902 RID: 14594 RVA: 0x000EA9DC File Offset: 0x000E8BDC
		[EditorBrowsable(1)]
		protected override bool DoubleBuffered
		{
			get
			{
				return base.DoubleBuffered;
			}
			set
			{
				base.DoubleBuffered = value;
			}
		}

		/// <summary>Gets or set the value that determines the appearance of a toolbar control and its buttons.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolBarAppearance" /> values. The default is ToolBarAppearance.Normal.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ToolBarAppearance" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06003903 RID: 14595 RVA: 0x000EA9E8 File Offset: 0x000E8BE8
		// (set) Token: 0x06003904 RID: 14596 RVA: 0x000EA9F0 File Offset: 0x000E8BF0
		[Localizable(true)]
		[DefaultValue(ToolBarAppearance.Normal)]
		public ToolBarAppearance Appearance
		{
			get
			{
				return this.appearance;
			}
			set
			{
				if (value == this.appearance)
				{
					return;
				}
				this.appearance = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets or sets a value indicating whether the toolbar adjusts its size automatically, based on the size of the buttons and the dock style.</summary>
		/// <returns>true if the toolbar adjusts its size automatically, based on the size of the buttons and dock style; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06003905 RID: 14597 RVA: 0x000EAA10 File Offset: 0x000E8C10
		// (set) Token: 0x06003906 RID: 14598 RVA: 0x000EAA18 File Offset: 0x000E8C18
		[DefaultValue(true)]
		[Localizable(true)]
		[Browsable(true)]
		[DesignerSerializationVisibility(1)]
		[EditorBrowsable(0)]
		public override bool AutoSize
		{
			get
			{
				return this.autosize;
			}
			set
			{
				if (value == this.autosize)
				{
					return;
				}
				this.autosize = value;
				if (base.IsHandleCreated)
				{
					this.Redraw(true);
				}
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06003907 RID: 14599 RVA: 0x000EAA4C File Offset: 0x000E8C4C
		// (set) Token: 0x06003908 RID: 14600 RVA: 0x000EAA54 File Offset: 0x000E8C54
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override Color BackColor
		{
			get
			{
				return this.background_color;
			}
			set
			{
				if (value == this.background_color)
				{
					return;
				}
				this.background_color = value;
				this.OnBackColorChanged(EventArgs.Empty);
				this.Redraw(false);
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06003909 RID: 14601 RVA: 0x000EAA84 File Offset: 0x000E8C84
		// (set) Token: 0x0600390A RID: 14602 RVA: 0x000EAA8C File Offset: 0x000E8C8C
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
				base.BackgroundImage = value;
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x0600390B RID: 14603 RVA: 0x000EAA98 File Offset: 0x000E8C98
		// (set) Token: 0x0600390C RID: 14604 RVA: 0x000EAAA0 File Offset: 0x000E8CA0
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

		/// <summary>Gets or sets the border style of the toolbar control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. The default is BorderStyle.None.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.BorderStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x0600390D RID: 14605 RVA: 0x000EAAAC File Offset: 0x000E8CAC
		// (set) Token: 0x0600390E RID: 14606 RVA: 0x000EAAB4 File Offset: 0x000E8CB4
		[DispId(-504)]
		[DefaultValue(BorderStyle.None)]
		public BorderStyle BorderStyle
		{
			get
			{
				return base.InternalBorderStyle;
			}
			set
			{
				base.InternalBorderStyle = value;
			}
		}

		/// <summary>Gets the collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls assigned to the toolbar control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolBar.ToolBarButtonCollection" /> that contains a collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x0600390F RID: 14607 RVA: 0x000EAAC0 File Offset: 0x000E8CC0
		[DesignerSerializationVisibility(2)]
		[Localizable(true)]
		[MergableProperty(false)]
		public ToolBar.ToolBarButtonCollection Buttons
		{
			get
			{
				return this.buttons;
			}
		}

		/// <summary>Gets or sets the size of the buttons on the toolbar control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> object that represents the size of the <see cref="T:System.Windows.Forms.ToolBarButton" /> controls on the toolbar. The default size has a width of 24 pixels and a height of 22 pixels, or large enough to accommodate the <see cref="T:System.Drawing.Image" /> and text, whichever is greater.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Drawing.Size.Width" /> or <see cref="P:System.Drawing.Size.Height" /> property of the <see cref="T:System.Drawing.Size" /> object is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06003910 RID: 14608 RVA: 0x000EAAC8 File Offset: 0x000E8CC8
		// (set) Token: 0x06003911 RID: 14609 RVA: 0x000EAB24 File Offset: 0x000E8D24
		[Localizable(true)]
		[RefreshProperties(1)]
		public Size ButtonSize
		{
			get
			{
				if (!this.button_size.IsEmpty)
				{
					return this.button_size;
				}
				if (this.buttons.Count == 0)
				{
					return new Size(39, 36);
				}
				Size size = this.CalcButtonSize();
				if (size.IsEmpty)
				{
					return new Size(24, 22);
				}
				return size;
			}
			set
			{
				this.size_specified = value != Size.Empty;
				if (this.button_size == value)
				{
					return;
				}
				this.button_size = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets or sets a value indicating whether the toolbar displays a divider.</summary>
		/// <returns>true if the toolbar displays a divider; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06003912 RID: 14610 RVA: 0x000EAB58 File Offset: 0x000E8D58
		// (set) Token: 0x06003913 RID: 14611 RVA: 0x000EAB60 File Offset: 0x000E8D60
		[DefaultValue(true)]
		public bool Divider
		{
			get
			{
				return this.divider;
			}
			set
			{
				if (value == this.divider)
				{
					return;
				}
				this.divider = value;
				this.Redraw(false);
			}
		}

		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is <see cref="F:System.Windows.Forms.DockStyle.None" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06003914 RID: 14612 RVA: 0x000EAB80 File Offset: 0x000E8D80
		// (set) Token: 0x06003915 RID: 14613 RVA: 0x000EAB88 File Offset: 0x000E8D88
		[Localizable(true)]
		[DefaultValue(DockStyle.Top)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				if (base.Dock == value)
				{
					if (value != DockStyle.None)
					{
						base.Dock = value;
					}
					return;
				}
				if (this.Vertical)
				{
					base.SetStyle(ControlStyles.FixedWidth, this.AutoSize);
					base.SetStyle(ControlStyles.FixedHeight, false);
				}
				else
				{
					base.SetStyle(ControlStyles.FixedHeight, this.AutoSize);
					base.SetStyle(ControlStyles.FixedWidth, false);
				}
				this.LayoutToolBar();
				base.Dock = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether drop-down buttons on a toolbar display down arrows.</summary>
		/// <returns>true if drop-down toolbar buttons display down arrows; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06003916 RID: 14614 RVA: 0x000EABFC File Offset: 0x000E8DFC
		// (set) Token: 0x06003917 RID: 14615 RVA: 0x000EAC04 File Offset: 0x000E8E04
		[DefaultValue(false)]
		[Localizable(true)]
		public bool DropDownArrows
		{
			get
			{
				return this.drop_down_arrows;
			}
			set
			{
				if (value == this.drop_down_arrows)
				{
					return;
				}
				this.drop_down_arrows = value;
				this.Redraw(true);
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06003918 RID: 14616 RVA: 0x000EAC24 File Offset: 0x000E8E24
		// (set) Token: 0x06003919 RID: 14617 RVA: 0x000EAC2C File Offset: 0x000E8E2C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override Color ForeColor
		{
			get
			{
				return this.foreground_color;
			}
			set
			{
				if (value == this.foreground_color)
				{
					return;
				}
				this.foreground_color = value;
				this.OnForeColorChanged(EventArgs.Empty);
				this.Redraw(false);
			}
		}

		/// <summary>Gets or sets the collection of images available to the toolbar button controls.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that contains images available to the <see cref="T:System.Windows.Forms.ToolBarButton" /> controls. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x0600391A RID: 14618 RVA: 0x000EAC5C File Offset: 0x000E8E5C
		// (set) Token: 0x0600391B RID: 14619 RVA: 0x000EAC64 File Offset: 0x000E8E64
		[DefaultValue(null)]
		public ImageList ImageList
		{
			get
			{
				return this.image_list;
			}
			set
			{
				if (this.image_list == value)
				{
					return;
				}
				this.image_list = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets the size of the images in the image list assigned to the toolbar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the images (in the <see cref="T:System.Windows.Forms.ImageList" />) assigned to the <see cref="T:System.Windows.Forms.ToolBar" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x0600391C RID: 14620 RVA: 0x000EAC84 File Offset: 0x000E8E84
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(2)]
		public Size ImageSize
		{
			get
			{
				if (this.ImageList == null)
				{
					return Size.Empty;
				}
				return this.ImageList.ImageSize;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x0600391D RID: 14621 RVA: 0x000EACA4 File Offset: 0x000E8EA4
		// (set) Token: 0x0600391E RID: 14622 RVA: 0x000EACAC File Offset: 0x000E8EAC
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new ImeMode ImeMode
		{
			get
			{
				return this.ime_mode;
			}
			set
			{
				if (value == this.ime_mode)
				{
					return;
				}
				this.ime_mode = value;
				this.OnImeModeChanged(EventArgs.Empty);
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.RightToLeft" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x0600391F RID: 14623 RVA: 0x000EACD0 File Offset: 0x000E8ED0
		// (set) Token: 0x06003920 RID: 14624 RVA: 0x000EACD8 File Offset: 0x000E8ED8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				if (value == base.RightToLeft)
				{
					return;
				}
				base.RightToLeft = value;
				this.OnRightToLeftChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating whether the toolbar displays a ToolTip for each button.</summary>
		/// <returns>true if the toolbar display a ToolTip for each button; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06003921 RID: 14625 RVA: 0x000EACFC File Offset: 0x000E8EFC
		// (set) Token: 0x06003922 RID: 14626 RVA: 0x000EAD04 File Offset: 0x000E8F04
		[DefaultValue(false)]
		[Localizable(true)]
		public bool ShowToolTips
		{
			get
			{
				return this.show_tooltips;
			}
			set
			{
				this.show_tooltips = value;
			}
		}

		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06003923 RID: 14627 RVA: 0x000EAD10 File Offset: 0x000E8F10
		// (set) Token: 0x06003924 RID: 14628 RVA: 0x000EAD18 File Offset: 0x000E8F18
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

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.String" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06003925 RID: 14629 RVA: 0x000EAD24 File Offset: 0x000E8F24
		// (set) Token: 0x06003926 RID: 14630 RVA: 0x000EAD2C File Offset: 0x000E8F2C
		[DesignerSerializationVisibility(0)]
		[Bindable(false)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if (value == base.Text)
				{
					return;
				}
				base.Text = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets or sets the alignment of text in relation to each image displayed on the toolbar button controls.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolBarTextAlign" /> values. The default is ToolBarTextAlign.Underneath.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.ToolBarTextAlign" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06003927 RID: 14631 RVA: 0x000EAD5C File Offset: 0x000E8F5C
		// (set) Token: 0x06003928 RID: 14632 RVA: 0x000EAD64 File Offset: 0x000E8F64
		[Localizable(true)]
		[DefaultValue(ToolBarTextAlign.Underneath)]
		public ToolBarTextAlign TextAlign
		{
			get
			{
				return this.text_alignment;
			}
			set
			{
				if (value == this.text_alignment)
				{
					return;
				}
				this.text_alignment = value;
				this.Redraw(true);
			}
		}

		/// <summary>Gets or sets a value indicating whether the toolbar buttons wrap to the next line if the toolbar becomes too small to display all the buttons on the same line.</summary>
		/// <returns>true if the toolbar buttons wrap to another line if the toolbar becomes too small to display all the buttons on the same line; otherwise, false. The default value is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06003929 RID: 14633 RVA: 0x000EAD84 File Offset: 0x000E8F84
		// (set) Token: 0x0600392A RID: 14634 RVA: 0x000EAD8C File Offset: 0x000E8F8C
		[Localizable(true)]
		[DefaultValue(true)]
		public bool Wrappable
		{
			get
			{
				return this.wrappable;
			}
			set
			{
				if (value == this.wrappable)
				{
					return;
				}
				this.wrappable = value;
				this.Redraw(true);
			}
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ToolBar" /> control.</summary>
		/// <returns>A String that represents the current <see cref="T:System.Windows.Forms.ToolBar" />. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600392B RID: 14635 RVA: 0x000EADAC File Offset: 0x000E8FAC
		public override string ToString()
		{
			int count = this.Buttons.Count;
			if (count == 0)
			{
				return string.Format("System.Windows.Forms.ToolBar, Buttons.Count: 0", new object[0]);
			}
			return string.Format("System.Windows.Forms.ToolBar, Buttons.Count: {0}, Buttons[0]: {1}", count, this.Buttons[0].ToString());
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x000EAE00 File Offset: 0x000E9000
		protected override void CreateHandle()
		{
			base.CreateHandle();
			this.default_size = this.CalcButtonSize();
			if (this.appearance != ToolBarAppearance.Flat)
			{
				this.Redraw(true);
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolBar" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600392D RID: 14637 RVA: 0x000EAE28 File Offset: 0x000E9028
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.ImageList = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000EAE40 File Offset: 0x000E9040
		internal void UIAPerformClick(ToolBarButton button)
		{
			ToolBarItem toolBarItem = this.current_item;
			this.current_item = null;
			foreach (ToolBarItem toolBarItem2 in this.items)
			{
				if (toolBarItem2.Button == button)
				{
					this.current_item = toolBarItem2;
					break;
				}
			}
			try
			{
				if (this.current_item == null)
				{
					throw new ArgumentException("button", "The button specified is not part of this toolbar");
				}
				this.PerformButtonClick(new ToolBarButtonClickEventArgs(button));
			}
			finally
			{
				this.current_item = toolBarItem;
			}
		}

		// Token: 0x0600392F RID: 14639 RVA: 0x000EAEE4 File Offset: 0x000E90E4
		private void PerformButtonClick(ToolBarButtonClickEventArgs e)
		{
			if (e.Button.Style == ToolBarButtonStyle.ToggleButton)
			{
				if (!e.Button.Pushed)
				{
					e.Button.Pushed = true;
				}
				else
				{
					e.Button.Pushed = false;
				}
			}
			this.current_item.Pressed = false;
			this.current_item.Invalidate();
			this.button_for_focus = this.current_item.Button;
			this.button_for_focus.UIAHasFocus = true;
			this.OnButtonClick(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolBar.ButtonClick" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolBarButtonClickEventArgs" /> that contains the event data. </param>
		// Token: 0x06003930 RID: 14640 RVA: 0x000EAF6C File Offset: 0x000E916C
		protected virtual void OnButtonClick(ToolBarButtonClickEventArgs e)
		{
			ToolBarButtonClickEventHandler toolBarButtonClickEventHandler = (ToolBarButtonClickEventHandler)base.Events[ToolBar.ButtonClickEvent];
			if (toolBarButtonClickEventHandler != null)
			{
				toolBarButtonClickEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolBar.ButtonDropDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolBarButtonClickEventArgs" /> that contains the event data. </param>
		// Token: 0x06003931 RID: 14641 RVA: 0x000EAFA0 File Offset: 0x000E91A0
		protected virtual void OnButtonDropDown(ToolBarButtonClickEventArgs e)
		{
			ToolBarButtonClickEventHandler toolBarButtonClickEventHandler = (ToolBarButtonClickEventHandler)base.Events[ToolBar.ButtonDropDownEvent];
			if (toolBarButtonClickEventHandler != null)
			{
				toolBarButtonClickEventHandler(this, e);
			}
			if (e.Button.DropDownMenu == null)
			{
				return;
			}
			this.ShowDropDownMenu(this.current_item);
		}

		// Token: 0x06003932 RID: 14642 RVA: 0x000EAFF0 File Offset: 0x000E91F0
		internal void ShowDropDownMenu(ToolBarItem item)
		{
			Point point;
			point..ctor(item.Rectangle.X + 1, item.Rectangle.Bottom + 1);
			((ContextMenu)item.Button.DropDownMenu).Show(this, point);
			item.DDPressed = false;
			item.Hilight = false;
			item.Invalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003933 RID: 14643 RVA: 0x000EB050 File Offset: 0x000E9250
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.Redraw(true);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003934 RID: 14644 RVA: 0x000EB060 File Offset: 0x000E9260
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003935 RID: 14645 RVA: 0x000EB06C File Offset: 0x000E926C
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.LayoutToolBar();
		}

		/// <param name="factor">The factor by which the height and width of the control will be scaled.</param>
		/// <param name="specified">A <see cref="T:System.Windows.Forms.BoundsSpecified" /> value that specifies the bounds of the control to use when defining its size and position.</param>
		// Token: 0x06003936 RID: 14646 RVA: 0x000EB07C File Offset: 0x000E927C
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			specified &= ~BoundsSpecified.Height;
			base.ScaleControl(factor, specified);
		}

		/// <param name="dx">The horizontal scaling factor.</param>
		/// <param name="dy">The vertical scaling factor.</param>
		// Token: 0x06003937 RID: 14647 RVA: 0x000EB08C File Offset: 0x000E928C
		[EditorBrowsable(1)]
		protected override void ScaleCore(float dx, float dy)
		{
			dy = 1f;
			base.ScaleCore(dx, dy);
		}

		/// <summary>Sets the specified bounds of the <see cref="T:System.Windows.Forms.ToolBar" /> control.</summary>
		/// <param name="x">The new Left property value of the control.</param>
		/// <param name="y">The new Top property value of the control.</param>
		/// <param name="width">The new Width property value of the control.</param>
		/// <param name="height">Not used.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x06003938 RID: 14648 RVA: 0x000EB0A0 File Offset: 0x000E92A0
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (this.Vertical)
			{
				if (!this.AutoSize && this.requested_size != width && (specified & BoundsSpecified.Width) != BoundsSpecified.None)
				{
					this.requested_size = width;
				}
			}
			else if (!this.AutoSize && this.requested_size != height && (specified & BoundsSpecified.Height) != BoundsSpecified.None)
			{
				this.requested_size = height;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x06003939 RID: 14649 RVA: 0x000EB11C File Offset: 0x000E931C
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x0600393A RID: 14650 RVA: 0x000EB128 File Offset: 0x000E9328
		internal override bool InternalPreProcessMessage(ref Message msg)
		{
			if (msg.Msg == 256)
			{
				Keys keys = (Keys)msg.WParam.ToInt32();
				if (this.HandleKeyDown(ref msg, keys))
				{
					return true;
				}
			}
			return base.InternalPreProcessMessage(ref msg);
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x0600393B RID: 14651 RVA: 0x000EB16C File Offset: 0x000E936C
		// (set) Token: 0x0600393C RID: 14652 RVA: 0x000EB180 File Offset: 0x000E9380
		internal int CurrentItem
		{
			get
			{
				return Array.IndexOf<ToolBarItem>(this.items, this.current_item);
			}
			set
			{
				if (this.current_item != null)
				{
					this.current_item.Hilight = false;
				}
				this.current_item = ((value != -1) ? this.items[value] : null);
				if (this.current_item != null)
				{
					this.current_item.Hilight = true;
				}
			}
		}

		// Token: 0x0600393D RID: 14653 RVA: 0x000EB1D8 File Offset: 0x000E93D8
		private void FocusChanged(object sender, EventArgs args)
		{
			if (!this.Focused && this.button_for_focus != null)
			{
				this.button_for_focus.UIAHasFocus = false;
			}
			this.button_for_focus = null;
			if (this.Appearance != ToolBarAppearance.Flat || this.Buttons.Count == 0)
			{
				return;
			}
			ToolBarItem toolBarItem = null;
			foreach (ToolBarItem toolBarItem2 in this.items)
			{
				if (toolBarItem2.Hilight)
				{
					toolBarItem = toolBarItem2;
					break;
				}
			}
			if (this.Focused && toolBarItem == null)
			{
				foreach (ToolBarItem toolBarItem3 in this.items)
				{
					if (toolBarItem3.Button.Enabled)
					{
						toolBarItem3.Hilight = true;
						break;
					}
				}
			}
			else if (toolBarItem != null)
			{
				toolBarItem.Hilight = false;
			}
		}

		// Token: 0x0600393E RID: 14654 RVA: 0x000EB2C8 File Offset: 0x000E94C8
		private bool HandleKeyDown(ref Message msg, Keys key_data)
		{
			if (this.Appearance != ToolBarAppearance.Flat || this.Buttons.Count == 0)
			{
				return false;
			}
			if (this.HandleKeyOnDropDown(ref msg, key_data))
			{
				return true;
			}
			switch (key_data)
			{
			case Keys.Space:
				break;
			default:
				if (key_data != Keys.Return)
				{
					return false;
				}
				break;
			case Keys.Left:
			case Keys.Up:
				this.HighlightButton(-1);
				return true;
			case Keys.Right:
			case Keys.Down:
				this.HighlightButton(1);
				return true;
			}
			if (this.current_item != null)
			{
				this.OnButtonClick(new ToolBarButtonClickEventArgs(this.current_item.Button));
				return true;
			}
			return false;
		}

		// Token: 0x0600393F RID: 14655 RVA: 0x000EB37C File Offset: 0x000E957C
		private bool HandleKeyOnDropDown(ref Message msg, Keys key_data)
		{
			if (this.current_item == null || this.current_item.Button.Style != ToolBarButtonStyle.DropDownButton || this.current_item.Button.DropDownMenu == null)
			{
				return false;
			}
			Menu dropDownMenu = this.current_item.Button.DropDownMenu;
			if (dropDownMenu.Tracker.active)
			{
				dropDownMenu.ProcessCmdKey(ref msg, key_data);
				return true;
			}
			if (key_data == Keys.Up || key_data == Keys.Down)
			{
				this.current_item.DDPressed = true;
				this.current_item.Invalidate();
				this.OnButtonDropDown(new ToolBarButtonClickEventArgs(this.current_item.Button));
				return true;
			}
			return false;
		}

		// Token: 0x06003940 RID: 14656 RVA: 0x000EB430 File Offset: 0x000E9630
		private void HighlightButton(int offset)
		{
			ArrayList arrayList = new ArrayList();
			int num = 0;
			int num2 = -1;
			ToolBarItem toolBarItem = null;
			foreach (ToolBarItem toolBarItem2 in this.items)
			{
				if (toolBarItem2.Hilight)
				{
					num2 = num;
					toolBarItem = toolBarItem2;
				}
				if (toolBarItem2.Button.Enabled)
				{
					arrayList.Add(toolBarItem2);
					num++;
				}
			}
			int num3 = (num2 + offset) % num;
			if (num3 < 0)
			{
				num3 = num - 1;
			}
			if (num3 == num2)
			{
				return;
			}
			if (toolBarItem != null)
			{
				toolBarItem.Hilight = false;
			}
			this.current_item = arrayList[num3] as ToolBarItem;
			this.current_item.Hilight = true;
		}

		// Token: 0x06003941 RID: 14657 RVA: 0x000EB4EC File Offset: 0x000E96EC
		private void ToolBar_BackgroundImageChanged(object sender, EventArgs args)
		{
			this.Redraw(false, true);
		}

		// Token: 0x06003942 RID: 14658 RVA: 0x000EB4F8 File Offset: 0x000E96F8
		private void ToolBar_MouseDown(object sender, MouseEventArgs me)
		{
			if (!base.Enabled || (me.Button & MouseButtons.Left) == MouseButtons.None)
			{
				return;
			}
			Point point;
			point..ctor(me.X, me.Y);
			if (this.ItemAtPoint(point) == null)
			{
				return;
			}
			if (this.tip_window != null && this.tip_window.Visible && (me.Button & MouseButtons.Left) == MouseButtons.Left)
			{
				this.TipDownTimer.Stop();
				this.tip_window.Hide(this);
			}
			foreach (ToolBarItem toolBarItem in this.items)
			{
				if (toolBarItem.Button.Enabled && toolBarItem.Rectangle.Contains(point))
				{
					if (toolBarItem.Button.Style == ToolBarButtonStyle.DropDownButton)
					{
						Rectangle rectangle = toolBarItem.Rectangle;
						if (this.DropDownArrows)
						{
							rectangle.Width = ThemeEngine.Current.ToolBarDropDownWidth;
							rectangle.X = toolBarItem.Rectangle.Right - rectangle.Width;
						}
						if (rectangle.Contains(point))
						{
							if (toolBarItem.Button.DropDownMenu != null)
							{
								toolBarItem.DDPressed = true;
								base.Invalidate(rectangle);
							}
							break;
						}
					}
					toolBarItem.Pressed = true;
					toolBarItem.Inside = true;
					toolBarItem.Invalidate();
					break;
				}
			}
		}

		// Token: 0x06003943 RID: 14659 RVA: 0x000EB66C File Offset: 0x000E986C
		private void ToolBar_MouseUp(object sender, MouseEventArgs me)
		{
			if (!base.Enabled || (me.Button & MouseButtons.Left) == MouseButtons.None)
			{
				return;
			}
			Point point;
			point..ctor(me.X, me.Y);
			ArrayList arrayList = new ArrayList(this.items);
			foreach (object obj in arrayList)
			{
				ToolBarItem toolBarItem = (ToolBarItem)obj;
				if (toolBarItem.Button.Enabled && toolBarItem.Rectangle.Contains(point))
				{
					if (toolBarItem.Button.Style == ToolBarButtonStyle.DropDownButton)
					{
						Rectangle rectangle = toolBarItem.Rectangle;
						rectangle.Width = ThemeEngine.Current.ToolBarDropDownWidth;
						rectangle.X = toolBarItem.Rectangle.Right - rectangle.Width;
						if (rectangle.Contains(point))
						{
							this.current_item = toolBarItem;
							if (toolBarItem.DDPressed)
							{
								this.OnButtonDropDown(new ToolBarButtonClickEventArgs(toolBarItem.Button));
							}
							continue;
						}
					}
					this.current_item = toolBarItem;
					if (toolBarItem.Pressed && (me.Button & MouseButtons.Left) == MouseButtons.Left)
					{
						this.PerformButtonClick(new ToolBarButtonClickEventArgs(toolBarItem.Button));
					}
				}
				else if (toolBarItem.Pressed)
				{
					toolBarItem.Pressed = false;
					toolBarItem.Invalidate();
				}
			}
		}

		// Token: 0x06003944 RID: 14660 RVA: 0x000EB808 File Offset: 0x000E9A08
		private ToolBarItem ItemAtPoint(Point pt)
		{
			foreach (ToolBarItem toolBarItem in this.items)
			{
				if (toolBarItem.Rectangle.Contains(pt))
				{
					return toolBarItem;
				}
			}
			return null;
		}

		// Token: 0x06003945 RID: 14661 RVA: 0x000EB84C File Offset: 0x000E9A4C
		private void PopDownTip(object o, EventArgs args)
		{
			this.tip_window.Hide(this);
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06003946 RID: 14662 RVA: 0x000EB85C File Offset: 0x000E9A5C
		private Timer TipDownTimer
		{
			get
			{
				if (this.tipdown_timer == null)
				{
					this.tipdown_timer = new Timer();
					this.tipdown_timer.Enabled = false;
					this.tipdown_timer.Interval = 5000;
					this.tipdown_timer.Tick += new EventHandler(this.PopDownTip);
				}
				return this.tipdown_timer;
			}
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x000EB8B8 File Offset: 0x000E9AB8
		private void ToolBar_MouseHover(object sender, EventArgs e)
		{
			if (base.Capture)
			{
				return;
			}
			if (this.tip_window == null)
			{
				this.tip_window = new ToolTip();
			}
			ToolBarItem toolBarItem = this.ItemAtPoint(base.PointToClient(Control.MousePosition));
			this.current_item = toolBarItem;
			if (toolBarItem == null || toolBarItem.Button.ToolTipText.Length == 0)
			{
				return;
			}
			this.tip_window.Present(this, toolBarItem.Button.ToolTipText);
			this.TipDownTimer.Start();
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x000EB940 File Offset: 0x000E9B40
		private void ToolBar_MouseLeave(object sender, EventArgs e)
		{
			if (this.tipdown_timer != null)
			{
				this.tipdown_timer.Dispose();
			}
			this.tipdown_timer = null;
			if (this.tip_window != null)
			{
				this.tip_window.Dispose();
			}
			this.tip_window = null;
			if (!base.Enabled || this.current_item == null)
			{
				return;
			}
			this.current_item.Hilight = false;
			this.current_item = null;
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x000EB9B4 File Offset: 0x000E9BB4
		private void ToolBar_MouseMove(object sender, MouseEventArgs me)
		{
			if (!base.Enabled)
			{
				return;
			}
			if (this.tip_window != null && this.tip_window.Visible)
			{
				this.TipDownTimer.Stop();
				this.TipDownTimer.Start();
			}
			Point point;
			point..ctor(me.X, me.Y);
			if (base.Capture)
			{
				foreach (ToolBarItem toolBarItem in this.items)
				{
					if (toolBarItem.Pressed && toolBarItem.Inside != toolBarItem.Rectangle.Contains(point))
					{
						toolBarItem.Inside = toolBarItem.Rectangle.Contains(point);
						toolBarItem.Hilight = false;
						break;
					}
				}
				return;
			}
			if (this.current_item != null && this.current_item.Rectangle.Contains(point))
			{
				if (ThemeEngine.Current.ToolBarHasHotElementStyles(this))
				{
					if (this.current_item.Hilight || (!ThemeEngine.Current.ToolBarHasHotCheckedElementStyles && this.current_item.Button.Pushed) || !this.current_item.Button.Enabled)
					{
						return;
					}
					this.current_item.Hilight = true;
				}
			}
			else
			{
				if (this.tip_window != null)
				{
					if (this.tip_window.Visible)
					{
						this.tip_window.Hide(this);
						this.TipDownTimer.Stop();
					}
					this.current_item = this.ItemAtPoint(point);
					if (this.current_item != null && this.current_item.Button.ToolTipText.Length > 0)
					{
						this.tip_window.Present(this, this.current_item.Button.ToolTipText);
						this.TipDownTimer.Start();
					}
				}
				if (ThemeEngine.Current.ToolBarHasHotElementStyles(this))
				{
					foreach (ToolBarItem toolBarItem2 in this.items)
					{
						if (toolBarItem2.Rectangle.Contains(point) && toolBarItem2.Button.Enabled)
						{
							this.current_item = toolBarItem2;
							if (!this.current_item.Hilight && (ThemeEngine.Current.ToolBarHasHotCheckedElementStyles || !this.current_item.Button.Pushed))
							{
								this.current_item.Hilight = true;
							}
						}
						else if (toolBarItem2.Hilight)
						{
							toolBarItem2.Hilight = false;
						}
					}
				}
			}
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x000EBC68 File Offset: 0x000E9E68
		internal override void OnPaintInternal(PaintEventArgs pevent)
		{
			if (base.GetStyle(ControlStyles.UserPaint))
			{
				return;
			}
			ThemeEngine.Current.DrawToolBar(pevent.Graphics, pevent.ClipRectangle, this);
			pevent.Handled = true;
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x000EBCA0 File Offset: 0x000E9EA0
		internal void Redraw(bool recalculate)
		{
			this.Redraw(recalculate, true);
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x000EBCAC File Offset: 0x000E9EAC
		internal void Redraw(bool recalculate, bool force)
		{
			bool flag = true;
			if (recalculate)
			{
				flag = this.LayoutToolBar();
			}
			if (force || flag)
			{
				base.Invalidate();
			}
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x0600394D RID: 14669 RVA: 0x000EBCDC File Offset: 0x000E9EDC
		internal bool SizeSpecified
		{
			get
			{
				return this.size_specified;
			}
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x0600394E RID: 14670 RVA: 0x000EBCE4 File Offset: 0x000E9EE4
		internal bool Vertical
		{
			get
			{
				return this.Dock == DockStyle.Left || this.Dock == DockStyle.Right;
			}
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x000EBD00 File Offset: 0x000E9F00
		private Size CalcButtonSize()
		{
			if (this.Buttons.Count == 0)
			{
				return Size.Empty;
			}
			string text = this.Buttons[0].Text;
			for (int i = 1; i < this.Buttons.Count; i++)
			{
				if (this.Buttons[i].Text.Length > text.Length)
				{
					text = this.Buttons[i].Text;
				}
			}
			Size empty = Size.Empty;
			if (text != null && text.Length > 0)
			{
				SizeF sizeF = TextRenderer.MeasureString(text, this.Font);
				if (sizeF != SizeF.Empty)
				{
					empty..ctor((int)Math.Ceiling((double)sizeF.Width) + 6, (int)Math.Ceiling((double)sizeF.Height));
				}
			}
			Size size = ((this.ImageList != null) ? this.ImageSize : new Size(16, 16));
			Theme theme = ThemeEngine.Current;
			int num = size.Width + 2 * theme.ToolBarImageGripWidth;
			int num2 = size.Height + 2 * theme.ToolBarImageGripWidth;
			if (this.text_alignment == ToolBarTextAlign.Right)
			{
				empty.Width = num + empty.Width;
				empty.Height = ((empty.Height <= num2) ? num2 : empty.Height);
			}
			else
			{
				empty.Height = num2 + empty.Height;
				empty.Width = ((empty.Width <= num) ? num : empty.Width);
			}
			empty.Width += theme.ToolBarImageGripWidth;
			empty.Height += theme.ToolBarImageGripWidth;
			return empty;
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06003950 RID: 14672 RVA: 0x000EBED0 File Offset: 0x000EA0D0
		private Size AdjustedButtonSize
		{
			get
			{
				Size size;
				if (this.default_size.IsEmpty || this.Appearance == ToolBarAppearance.Normal)
				{
					size = this.ButtonSize;
				}
				else
				{
					size = this.default_size;
				}
				if (this.size_specified)
				{
					if (this.Appearance == ToolBarAppearance.Flat)
					{
						size = this.CalcButtonSize();
					}
					else
					{
						int toolBarImageGripWidth = ThemeEngine.Current.ToolBarImageGripWidth;
						if (size.Width < this.ImageSize.Width + 2 * toolBarImageGripWidth)
						{
							size.Width = this.ImageSize.Width + 2 * toolBarImageGripWidth;
						}
						if (size.Height < this.ImageSize.Height + 2 * toolBarImageGripWidth)
						{
							size.Height = this.ImageSize.Height + 2 * toolBarImageGripWidth;
						}
					}
				}
				return size;
			}
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x000EBFAC File Offset: 0x000EA1AC
		private bool LayoutToolBar()
		{
			bool flag = false;
			Theme theme = ThemeEngine.Current;
			int num = theme.ToolBarGripWidth;
			int num2 = theme.ToolBarGripWidth;
			Size adjustedButtonSize = this.AdjustedButtonSize;
			int num3 = ((!this.Vertical) ? adjustedButtonSize.Height : adjustedButtonSize.Width) + theme.ToolBarGripWidth;
			int num4 = -1;
			this.items = new ToolBarItem[this.buttons.Count];
			for (int i = 0; i < this.buttons.Count; i++)
			{
				ToolBarButton toolBarButton = this.buttons[i];
				ToolBarItem toolBarItem = new ToolBarItem(toolBarButton);
				this.items[i] = toolBarItem;
				if (toolBarButton.Visible)
				{
					if (this.size_specified && toolBarButton.Style != ToolBarButtonStyle.Separator)
					{
						flag = toolBarItem.Layout(adjustedButtonSize);
					}
					else
					{
						flag = toolBarItem.Layout(this.Vertical, num3);
					}
					bool flag2 = toolBarButton.Style == ToolBarButtonStyle.Separator;
					if (this.Vertical)
					{
						if (num2 + toolBarItem.Rectangle.Height < base.Height || flag2 || !this.Wrappable)
						{
							if (toolBarItem.Location.X != num || toolBarItem.Location.Y != num2)
							{
								flag = true;
							}
							toolBarItem.Location = new Point(num, num2);
							num2 += toolBarItem.Rectangle.Height;
							if (flag2)
							{
								num4 = i;
							}
						}
						else if (num4 > 0)
						{
							i = num4;
							num4 = -1;
							num2 = theme.ToolBarGripWidth;
							num += num3;
						}
						else
						{
							num2 = theme.ToolBarGripWidth;
							num += num3;
							if (toolBarItem.Location.X != num || toolBarItem.Location.Y != num2)
							{
								flag = true;
							}
							toolBarItem.Location = new Point(num, num2);
							num2 += toolBarItem.Rectangle.Height;
						}
					}
					else if (num + toolBarItem.Rectangle.Width < base.Width || flag2 || !this.Wrappable)
					{
						if (toolBarItem.Location.X != num || toolBarItem.Location.Y != num2)
						{
							flag = true;
						}
						toolBarItem.Location = new Point(num, num2);
						num += toolBarItem.Rectangle.Width;
						if (flag2)
						{
							num4 = i;
						}
					}
					else if (num4 > 0)
					{
						i = num4;
						num4 = -1;
						num = theme.ToolBarGripWidth;
						num2 += num3;
					}
					else
					{
						num = theme.ToolBarGripWidth;
						num2 += num3;
						if (toolBarItem.Location.X != num || toolBarItem.Location.Y != num2)
						{
							flag = true;
						}
						toolBarItem.Location = new Point(num, num2);
						num += toolBarItem.Rectangle.Width;
					}
				}
			}
			if (base.Parent == null)
			{
				return flag;
			}
			if (this.Wrappable)
			{
				num3 += ((!this.Vertical) ? num2 : num);
			}
			if (base.IsHandleCreated)
			{
				if (this.Vertical)
				{
					base.Width = num3;
				}
				else
				{
					base.Height = num3;
				}
			}
			return flag;
		}

		// Token: 0x040019D1 RID: 6609
		internal const int text_padding = 3;

		// Token: 0x040019D2 RID: 6610
		private bool size_specified;

		// Token: 0x040019D3 RID: 6611
		private ToolBarItem current_item;

		// Token: 0x040019D4 RID: 6612
		internal ToolBarItem[] items;

		// Token: 0x040019D5 RID: 6613
		internal Size default_size;

		// Token: 0x040019D8 RID: 6616
		private ToolBarAppearance appearance;

		// Token: 0x040019D9 RID: 6617
		private bool autosize = true;

		// Token: 0x040019DA RID: 6618
		private ToolBar.ToolBarButtonCollection buttons;

		// Token: 0x040019DB RID: 6619
		private Size button_size;

		// Token: 0x040019DC RID: 6620
		private bool divider = true;

		// Token: 0x040019DD RID: 6621
		private bool drop_down_arrows = true;

		// Token: 0x040019DE RID: 6622
		private ImageList image_list;

		// Token: 0x040019DF RID: 6623
		private ImeMode ime_mode = ImeMode.Disable;

		// Token: 0x040019E0 RID: 6624
		private bool show_tooltips = true;

		// Token: 0x040019E1 RID: 6625
		private ToolBarTextAlign text_alignment;

		// Token: 0x040019E2 RID: 6626
		private bool wrappable = true;

		// Token: 0x040019E3 RID: 6627
		private ToolBarButton button_for_focus;

		// Token: 0x040019E4 RID: 6628
		private int requested_size = -1;

		// Token: 0x040019E5 RID: 6629
		private ToolTip tip_window;

		// Token: 0x040019E6 RID: 6630
		private Timer tipdown_timer;

		/// <summary>Encapsulates a collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls for use by the <see cref="T:System.Windows.Forms.ToolBar" /> class.</summary>
		// Token: 0x02000335 RID: 821
		public class ToolBarButtonCollection : ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolBar.ToolBarButtonCollection" /> class and assigns it to the specified toolbar.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.ToolBar" /> that is the parent of the collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls. </param>
			// Token: 0x06003952 RID: 14674 RVA: 0x000EC32C File Offset: 0x000EA52C
			public ToolBarButtonCollection(ToolBar owner)
			{
				this.list = new ArrayList();
				this.owner = owner;
				this.redraw = true;
			}

			// Token: 0x06003953 RID: 14675 RVA: 0x000EC350 File Offset: 0x000EA550
			// Note: this type is marked as 'beforefieldinit'.
			static ToolBarButtonCollection()
			{
				ToolBar.ToolBarButtonCollection.UIACollectionChangedEvent = new object();
			}

			// Token: 0x1400034F RID: 847
			// (add) Token: 0x06003954 RID: 14676 RVA: 0x000EC35C File Offset: 0x000EA55C
			// (remove) Token: 0x06003955 RID: 14677 RVA: 0x000EC374 File Offset: 0x000EA574
			internal event CollectionChangeEventHandler UIACollectionChanged
			{
				add
				{
					this.owner.Events.AddHandler(ToolBar.ToolBarButtonCollection.UIACollectionChangedEvent, value);
				}
				remove
				{
					this.owner.Events.RemoveHandler(ToolBar.ToolBarButtonCollection.UIACollectionChangedEvent, value);
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000EE4 RID: 3812
			// (get) Token: 0x06003956 RID: 14678 RVA: 0x000EC38C File Offset: 0x000EA58C
			bool ICollection.IsSynchronized
			{
				get
				{
					return this.list.IsSynchronized;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection of buttons.</summary>
			// Token: 0x17000EE5 RID: 3813
			// (get) Token: 0x06003957 RID: 14679 RVA: 0x000EC39C File Offset: 0x000EA59C
			object ICollection.SyncRoot
			{
				get
				{
					return this.list.SyncRoot;
				}
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000EE6 RID: 3814
			// (get) Token: 0x06003958 RID: 14680 RVA: 0x000EC3AC File Offset: 0x000EA5AC
			bool IList.IsFixedSize
			{
				get
				{
					return this.list.IsFixedSize;
				}
			}

			/// <summary>Gets or sets the item at a specified index.</summary>
			/// <returns>The element at the specified index.</returns>
			/// <param name="index">The zero-based index of the element to get or set. </param>
			// Token: 0x17000EE7 RID: 3815
			// (get) Token: 0x06003959 RID: 14681 RVA: 0x000EC3BC File Offset: 0x000EA5BC
			// (set) Token: 0x0600395A RID: 14682 RVA: 0x000EC3C8 File Offset: 0x000EA5C8
			object IList.Item
			{
				get
				{
					return this[index];
				}
				set
				{
					if (!(value is ToolBarButton))
					{
						throw new ArgumentException("Not of type ToolBarButton", "value");
					}
					this[index] = (ToolBarButton)value;
				}
			}

			/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
			/// <param name="dest">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing. </param>
			/// <param name="index">The zero-based index in <paramref name="dest" /> at which copying begins. </param>
			// Token: 0x0600395B RID: 14683 RVA: 0x000EC400 File Offset: 0x000EA600
			void ICollection.CopyTo(Array dest, int index)
			{
				this.list.CopyTo(dest, index);
			}

			/// <summary>Adds the specified toolbar button to the end of the toolbar button collection.</summary>
			/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.ToolBarButton" /> added to the collection.</returns>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to be added after all existing buttons.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="button" /> is not a <see cref="T:System.Windows.Forms.ToolBarButton" />.</exception>
			// Token: 0x0600395C RID: 14684 RVA: 0x000EC410 File Offset: 0x000EA610
			int IList.Add(object button)
			{
				if (!(button is ToolBarButton))
				{
					throw new ArgumentException("Not of type ToolBarButton", "button");
				}
				return this.Add((ToolBarButton)button);
			}

			/// <summary>Determines whether the collection contains a specific value.</summary>
			/// <returns>true if the item is found in the collection; otherwise, false.</returns>
			/// <param name="button">The item to locate in the collection. </param>
			// Token: 0x0600395D RID: 14685 RVA: 0x000EC43C File Offset: 0x000EA63C
			bool IList.Contains(object button)
			{
				if (!(button is ToolBarButton))
				{
					throw new ArgumentException("Not of type ToolBarButton", "button");
				}
				return this.Contains((ToolBarButton)button);
			}

			/// <summary>Determines the index of a specific item in the collection.</summary>
			/// <returns>The index of <paramref name="button" /> if found in the list; otherwise, -1.</returns>
			/// <param name="button">The item to locate in the collection. </param>
			// Token: 0x0600395E RID: 14686 RVA: 0x000EC468 File Offset: 0x000EA668
			int IList.IndexOf(object button)
			{
				if (!(button is ToolBarButton))
				{
					throw new ArgumentException("Not of type ToolBarButton", "button");
				}
				return this.IndexOf((ToolBarButton)button);
			}

			/// <summary>Inserts an existing toolbar button in the toolbar button collection at the specified location.</summary>
			/// <param name="index">The indexed location within the collection to insert the toolbar button. </param>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to insert.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="button" /> is not a <see cref="T:System.Windows.Forms.ToolBarButton" />.</exception>
			// Token: 0x0600395F RID: 14687 RVA: 0x000EC494 File Offset: 0x000EA694
			void IList.Insert(int index, object button)
			{
				if (!(button is ToolBarButton))
				{
					throw new ArgumentException("Not of type ToolBarButton", "button");
				}
				this.Insert(index, (ToolBarButton)button);
			}

			/// <summary>Removes the first occurrence of an item from the collection.</summary>
			/// <param name="button">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />. </param>
			// Token: 0x06003960 RID: 14688 RVA: 0x000EC4CC File Offset: 0x000EA6CC
			void IList.Remove(object button)
			{
				if (!(button is ToolBarButton))
				{
					throw new ArgumentException("Not of type ToolBarButton", "button");
				}
				this.Remove((ToolBarButton)button);
			}

			// Token: 0x06003961 RID: 14689 RVA: 0x000EC4F8 File Offset: 0x000EA6F8
			internal void OnUIACollectionChanged(CollectionChangeEventArgs e)
			{
				CollectionChangeEventHandler collectionChangeEventHandler = (CollectionChangeEventHandler)this.owner.Events[ToolBar.ToolBarButtonCollection.UIACollectionChangedEvent];
				if (collectionChangeEventHandler != null)
				{
					collectionChangeEventHandler.Invoke(this.owner, e);
				}
			}

			/// <summary>Gets the number of buttons in the toolbar button collection.</summary>
			/// <returns>The number of the <see cref="T:System.Windows.Forms.ToolBarButton" /> controls assigned to the toolbar.</returns>
			// Token: 0x17000EE8 RID: 3816
			// (get) Token: 0x06003962 RID: 14690 RVA: 0x000EC534 File Offset: 0x000EA734
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.list.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false. The default is false.</returns>
			// Token: 0x17000EE9 RID: 3817
			// (get) Token: 0x06003963 RID: 14691 RVA: 0x000EC544 File Offset: 0x000EA744
			public bool IsReadOnly
			{
				get
				{
					return this.list.IsReadOnly;
				}
			}

			/// <summary>Gets or sets the toolbar button at the specified indexed location in the toolbar button collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.ToolBarButton" /> that represents the toolbar button at the specified indexed location.</returns>
			/// <param name="index">The indexed location of the <see cref="T:System.Windows.Forms.ToolBarButton" /> in the collection. </param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="index" /> value is null. </exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> value is less than zero.-or- The <paramref name="index" /> value is greater than the number of buttons in the collection, and the collection of buttons is not null. </exception>
			// Token: 0x17000EEA RID: 3818
			public virtual ToolBarButton this[int index]
			{
				get
				{
					return (ToolBarButton)this.list[index];
				}
				set
				{
					this.OnUIACollectionChanged(new CollectionChangeEventArgs(2, index));
					value.SetParent(this.owner);
					this.list[index] = value;
					this.owner.Redraw(true);
					this.OnUIACollectionChanged(new CollectionChangeEventArgs(1, index));
				}
			}

			/// <summary>Gets a <see cref="T:System.Windows.Forms.ToolBarButton" /> with the specified key from the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.ToolBarButton" /> whose <see cref="P:System.Windows.Forms.ToolBarButton.Name" /> property matches the specified key.</returns>
			/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ToolBarButton" /> to retrieve.</param>
			// Token: 0x17000EEB RID: 3819
			public virtual ToolBarButton this[string key]
			{
				get
				{
					if (string.IsNullOrEmpty(key))
					{
						return null;
					}
					foreach (object obj in this.list)
					{
						ToolBarButton toolBarButton = (ToolBarButton)obj;
						if (string.Compare(toolBarButton.Name, key, true) == 0)
						{
							return toolBarButton;
						}
					}
					return null;
				}
			}

			/// <summary>Adds a new toolbar button to the end of the toolbar button collection with the specified <see cref="P:System.Windows.Forms.ToolBarButton.Text" /> property value.</summary>
			/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.ToolBarButton" /> added to the collection.</returns>
			/// <param name="text">The text to display on the new <see cref="T:System.Windows.Forms.ToolBarButton" />. </param>
			// Token: 0x06003967 RID: 14695 RVA: 0x000EC654 File Offset: 0x000EA854
			public int Add(string text)
			{
				ToolBarButton toolBarButton = new ToolBarButton(text);
				return this.Add(toolBarButton);
			}

			/// <summary>Adds the specified toolbar button to the end of the toolbar button collection.</summary>
			/// <returns>The zero-based index value of the <see cref="T:System.Windows.Forms.ToolBarButton" /> added to the collection.</returns>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to be added after all existing buttons. </param>
			// Token: 0x06003968 RID: 14696 RVA: 0x000EC670 File Offset: 0x000EA870
			public int Add(ToolBarButton button)
			{
				button.SetParent(this.owner);
				int num = this.list.Add(button);
				if (this.redraw)
				{
					this.owner.Redraw(true);
				}
				this.OnUIACollectionChanged(new CollectionChangeEventArgs(1, num));
				return num;
			}

			/// <summary>Adds a collection of toolbar buttons to this toolbar button collection.</summary>
			/// <param name="buttons">The collection of <see cref="T:System.Windows.Forms.ToolBarButton" /> controls to add to this <see cref="T:System.Windows.Forms.ToolBar.ToolBarButtonCollection" /> contained in an array. </param>
			// Token: 0x06003969 RID: 14697 RVA: 0x000EC6C0 File Offset: 0x000EA8C0
			public void AddRange(ToolBarButton[] buttons)
			{
				try
				{
					this.redraw = false;
					foreach (ToolBarButton toolBarButton in buttons)
					{
						this.Add(toolBarButton);
					}
				}
				finally
				{
					this.redraw = true;
					this.owner.Redraw(true);
				}
			}

			/// <summary>Removes all buttons from the toolbar button collection.</summary>
			// Token: 0x0600396A RID: 14698 RVA: 0x000EC72C File Offset: 0x000EA92C
			public void Clear()
			{
				this.list.Clear();
				this.owner.Redraw(false);
				this.OnUIACollectionChanged(new CollectionChangeEventArgs(3, -1));
			}

			/// <summary>Determines if the specified toolbar button is a member of the collection.</summary>
			/// <returns>true if the <see cref="T:System.Windows.Forms.ToolBarButton" /> is a member of the collection; otherwise, false.</returns>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to locate in the collection. </param>
			// Token: 0x0600396B RID: 14699 RVA: 0x000EC758 File Offset: 0x000EA958
			public bool Contains(ToolBarButton button)
			{
				return this.list.Contains(button);
			}

			/// <summary>Determines if a <see cref="T:System.Windows.Forms.ToolBarButton" /> with the specified key is contained in the collection.</summary>
			/// <returns>true to indicate a <see cref="T:System.Windows.Forms.ToolBarButton" /> with the specified key is found; otherwise, false. </returns>
			/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ToolBarButton" /> to search for.</param>
			// Token: 0x0600396C RID: 14700 RVA: 0x000EC768 File Offset: 0x000EA968
			public virtual bool ContainsKey(string key)
			{
				return this[key] != null;
			}

			/// <summary>Returns an enumerator that can be used to iterate through the toolbar button collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the tree node collection.</returns>
			// Token: 0x0600396D RID: 14701 RVA: 0x000EC778 File Offset: 0x000EA978
			public IEnumerator GetEnumerator()
			{
				return this.list.GetEnumerator();
			}

			/// <summary>Retrieves the index of the specified toolbar button in the collection.</summary>
			/// <returns>The zero-based index of the item found in the collection; otherwise, -1.</returns>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to locate in the collection. </param>
			// Token: 0x0600396E RID: 14702 RVA: 0x000EC788 File Offset: 0x000EA988
			public int IndexOf(ToolBarButton button)
			{
				return this.list.IndexOf(button);
			}

			/// <summary>Retrieves the index of the first occurrence of a <see cref="T:System.Windows.Forms.ToolBarButton" /> with the specified key.</summary>
			/// <returns>The index of the first occurrence of a <see cref="T:System.Windows.Forms.ToolBarButton" /> with the specified key, if found; otherwise, -1.</returns>
			/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ToolBarButton" /> to search for.</param>
			// Token: 0x0600396F RID: 14703 RVA: 0x000EC798 File Offset: 0x000EA998
			public virtual int IndexOfKey(string key)
			{
				return this.IndexOf(this[key]);
			}

			/// <summary>Inserts an existing toolbar button in the toolbar button collection at the specified location.</summary>
			/// <param name="index">The indexed location within the collection to insert the toolbar button. </param>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to insert. </param>
			// Token: 0x06003970 RID: 14704 RVA: 0x000EC7A8 File Offset: 0x000EA9A8
			public void Insert(int index, ToolBarButton button)
			{
				this.list.Insert(index, button);
				this.owner.Redraw(true);
				this.OnUIACollectionChanged(new CollectionChangeEventArgs(1, index));
			}

			/// <summary>Removes a given button from the toolbar button collection.</summary>
			/// <param name="button">The <see cref="T:System.Windows.Forms.ToolBarButton" /> to remove from the collection. </param>
			// Token: 0x06003971 RID: 14705 RVA: 0x000EC7D8 File Offset: 0x000EA9D8
			public void Remove(ToolBarButton button)
			{
				this.list.Remove(button);
				this.owner.Redraw(true);
			}

			/// <summary>Removes a given button from the toolbar button collection.</summary>
			/// <param name="index">The indexed location of the <see cref="T:System.Windows.Forms.ToolBarButton" /> in the collection. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> value is less than 0, or it is greater than the number of buttons in the collection. </exception>
			// Token: 0x06003972 RID: 14706 RVA: 0x000EC7F4 File Offset: 0x000EA9F4
			public void RemoveAt(int index)
			{
				this.list.RemoveAt(index);
				this.owner.Redraw(true);
				this.OnUIACollectionChanged(new CollectionChangeEventArgs(2, index));
			}

			/// <summary>Removes the <see cref="T:System.Windows.Forms.ToolBarButton" /> with the specified key from the collection.</summary>
			/// <param name="key">The name of the <see cref="T:System.Windows.Forms.ToolBarButton" /> to remove from the collection.</param>
			// Token: 0x06003973 RID: 14707 RVA: 0x000EC82C File Offset: 0x000EAA2C
			public virtual void RemoveByKey(string key)
			{
				this.Remove(this[key]);
			}

			// Token: 0x040019E7 RID: 6631
			private ArrayList list;

			// Token: 0x040019E8 RID: 6632
			private ToolBar owner;

			// Token: 0x040019E9 RID: 6633
			private bool redraw;
		}
	}
}
