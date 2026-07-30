using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows control that displays a frame around a group of controls with an optional caption.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001AA RID: 426
	[DefaultProperty("Text")]
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.GroupBoxDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ClassInterface(1)]
	[DefaultEvent("Enter")]
	public class GroupBox : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.GroupBox" /> class.</summary>
		// Token: 0x06001BBD RID: 7101 RVA: 0x0006B778 File Offset: 0x00069978
		public GroupBox()
		{
			this.TabStop = false;
			this.flat_style = FlatStyle.Standard;
			base.SetStyle(ControlStyles.ContainerControl | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor, true);
			base.SetStyle(ControlStyles.Selectable, false);
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.GroupBox.AutoSize" /> property changes.</summary>
		// Token: 0x140001B6 RID: 438
		// (add) Token: 0x06001BBE RID: 7102 RVA: 0x0006B7C0 File Offset: 0x000699C0
		// (remove) Token: 0x06001BBF RID: 7103 RVA: 0x0006B7CC File Offset: 0x000699CC
		[EditorBrowsable(0)]
		[Browsable(true)]
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

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.GroupBox" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001B7 RID: 439
		// (add) Token: 0x06001BC0 RID: 7104 RVA: 0x0006B7D8 File Offset: 0x000699D8
		// (remove) Token: 0x06001BC1 RID: 7105 RVA: 0x0006B7E4 File Offset: 0x000699E4
		[Browsable(false)]
		[EditorBrowsable(2)]
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

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.GroupBox" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001B8 RID: 440
		// (add) Token: 0x06001BC2 RID: 7106 RVA: 0x0006B7F0 File Offset: 0x000699F0
		// (remove) Token: 0x06001BC3 RID: 7107 RVA: 0x0006B7FC File Offset: 0x000699FC
		[Browsable(false)]
		[EditorBrowsable(2)]
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

		/// <summary>Occurs when the user presses a key while the <see cref="T:System.Windows.Forms.GroupBox" /> control has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001B9 RID: 441
		// (add) Token: 0x06001BC4 RID: 7108 RVA: 0x0006B808 File Offset: 0x00069A08
		// (remove) Token: 0x06001BC5 RID: 7109 RVA: 0x0006B814 File Offset: 0x00069A14
		[EditorBrowsable(2)]
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

		/// <summary>Occurs when the user presses a key while the <see cref="T:System.Windows.Forms.GroupBox" /> control has focus. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001BA RID: 442
		// (add) Token: 0x06001BC6 RID: 7110 RVA: 0x0006B820 File Offset: 0x00069A20
		// (remove) Token: 0x06001BC7 RID: 7111 RVA: 0x0006B82C File Offset: 0x00069A2C
		[Browsable(false)]
		[EditorBrowsable(2)]
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

		/// <summary>Occurs when the user releases a key while the <see cref="T:System.Windows.Forms.GroupBox" /> control has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001BB RID: 443
		// (add) Token: 0x06001BC8 RID: 7112 RVA: 0x0006B838 File Offset: 0x00069A38
		// (remove) Token: 0x06001BC9 RID: 7113 RVA: 0x0006B844 File Offset: 0x00069A44
		[EditorBrowsable(2)]
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

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.GroupBox" /> control with the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001BC RID: 444
		// (add) Token: 0x06001BCA RID: 7114 RVA: 0x0006B850 File Offset: 0x00069A50
		// (remove) Token: 0x06001BCB RID: 7115 RVA: 0x0006B85C File Offset: 0x00069A5C
		[EditorBrowsable(2)]
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

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.GroupBox" /> control with the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001BD RID: 445
		// (add) Token: 0x06001BCC RID: 7116 RVA: 0x0006B868 File Offset: 0x00069A68
		// (remove) Token: 0x06001BCD RID: 7117 RVA: 0x0006B874 File Offset: 0x00069A74
		[Browsable(false)]
		[EditorBrowsable(2)]
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

		/// <summary>Occurs when the user presses a mouse button while the mouse pointer is over the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001BE RID: 446
		// (add) Token: 0x06001BCE RID: 7118 RVA: 0x0006B880 File Offset: 0x00069A80
		// (remove) Token: 0x06001BCF RID: 7119 RVA: 0x0006B88C File Offset: 0x00069A8C
		[Browsable(false)]
		[EditorBrowsable(2)]
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

		/// <summary>Occurs when the mouse pointer enters the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001BF RID: 447
		// (add) Token: 0x06001BD0 RID: 7120 RVA: 0x0006B898 File Offset: 0x00069A98
		// (remove) Token: 0x06001BD1 RID: 7121 RVA: 0x0006B8A4 File Offset: 0x00069AA4
		[Browsable(false)]
		[EditorBrowsable(2)]
		public new event EventHandler MouseEnter
		{
			add
			{
				base.MouseEnter += value;
			}
			remove
			{
				base.MouseEnter -= value;
			}
		}

		/// <summary>Occurs when the mouse pointer leaves the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001C0 RID: 448
		// (add) Token: 0x06001BD2 RID: 7122 RVA: 0x0006B8B0 File Offset: 0x00069AB0
		// (remove) Token: 0x06001BD3 RID: 7123 RVA: 0x0006B8BC File Offset: 0x00069ABC
		[EditorBrowsable(2)]
		[Browsable(false)]
		public new event EventHandler MouseLeave
		{
			add
			{
				base.MouseLeave += value;
			}
			remove
			{
				base.MouseLeave -= value;
			}
		}

		/// <summary>Occurs when the user moves the mouse pointer over the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001C1 RID: 449
		// (add) Token: 0x06001BD4 RID: 7124 RVA: 0x0006B8C8 File Offset: 0x00069AC8
		// (remove) Token: 0x06001BD5 RID: 7125 RVA: 0x0006B8D4 File Offset: 0x00069AD4
		[Browsable(false)]
		[EditorBrowsable(2)]
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

		/// <summary>Occurs when the user releases a mouse button while the mouse pointer is over the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001C2 RID: 450
		// (add) Token: 0x06001BD6 RID: 7126 RVA: 0x0006B8E0 File Offset: 0x00069AE0
		// (remove) Token: 0x06001BD7 RID: 7127 RVA: 0x0006B8EC File Offset: 0x00069AEC
		[Browsable(false)]
		[EditorBrowsable(2)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.GroupBox.TabStop" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001C3 RID: 451
		// (add) Token: 0x06001BD8 RID: 7128 RVA: 0x0006B8F8 File Offset: 0x00069AF8
		// (remove) Token: 0x06001BD9 RID: 7129 RVA: 0x0006B904 File Offset: 0x00069B04
		[Browsable(false)]
		[EditorBrowsable(2)]
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

		/// <summary>Gets or sets a value that indicates whether the control will allow drag-and-drop operations and events to be used.</summary>
		/// <returns>true to allow drag-and-drop operations and events to be used; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001BDA RID: 7130 RVA: 0x0006B910 File Offset: 0x00069B10
		// (set) Token: 0x06001BDB RID: 7131 RVA: 0x0006B918 File Offset: 0x00069B18
		[EditorBrowsable(2)]
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

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Windows.Forms.GroupBox" /> resizes based on its contents.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.GroupBox" /> automatically resizes based on its contents; otherwise, false. The default is true.</returns>
		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001BDC RID: 7132 RVA: 0x0006B924 File Offset: 0x00069B24
		// (set) Token: 0x06001BDD RID: 7133 RVA: 0x0006B92C File Offset: 0x00069B2C
		[DesignerSerializationVisibility(1)]
		[Browsable(true)]
		[EditorBrowsable(0)]
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

		/// <summary>Gets or sets how the <see cref="T:System.Windows.Forms.GroupBox" /> behaves when its <see cref="P:System.Windows.Forms.Control.AutoSize" /> property is enabled. </summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.AutoSizeMode" /> values. The default is <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly" />.</returns>
		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x0006B938 File Offset: 0x00069B38
		// (set) Token: 0x06001BDF RID: 7135 RVA: 0x0006B940 File Offset: 0x00069B40
		[DefaultValue(AutoSizeMode.GrowOnly)]
		[Browsable(true)]
		[Localizable(true)]
		public AutoSizeMode AutoSizeMode
		{
			get
			{
				return base.GetAutoSizeMode();
			}
			set
			{
				base.SetAutoSizeMode(value);
			}
		}

		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x0006B94C File Offset: 0x00069B4C
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x0006B954 File Offset: 0x00069B54
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.GroupBoxDefaultSize;
			}
		}

		/// <summary>Gets a rectangle that represents the dimensions of the <see cref="T:System.Windows.Forms.GroupBox" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> with the dimensions of the <see cref="T:System.Windows.Forms.GroupBox" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001BE2 RID: 7138 RVA: 0x0006B960 File Offset: 0x00069B60
		public override Rectangle DisplayRectangle
		{
			get
			{
				this.display_rectangle.X = base.Padding.Left;
				this.display_rectangle.Y = this.Font.Height + base.Padding.Top;
				this.display_rectangle.Width = base.Width - base.Padding.Horizontal;
				this.display_rectangle.Height = base.Height - this.Font.Height - base.Padding.Vertical;
				return this.display_rectangle;
			}
		}

		/// <summary>Gets or sets the flat style appearance of the group box control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. The default value is <see cref="F:System.Windows.Forms.FlatStyle.Standard" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.FlatStyle" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x0006BA00 File Offset: 0x00069C00
		// (set) Token: 0x06001BE4 RID: 7140 RVA: 0x0006BA08 File Offset: 0x00069C08
		[DefaultValue(FlatStyle.Standard)]
		public FlatStyle FlatStyle
		{
			get
			{
				return this.flat_style;
			}
			set
			{
				if (!Enum.IsDefined(typeof(FlatStyle), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for FlatStyle", value));
				}
				if (this.flat_style == value)
				{
					return;
				}
				this.flat_style = value;
				this.Refresh();
			}
		}

		/// <summary>Gets or sets a value that indicates whether the user can press the TAB key to give the focus to the <see cref="T:System.Windows.Forms.GroupBox" />.</summary>
		/// <returns>true to allow the user to press the TAB key to give the focus to the <see cref="T:System.Windows.Forms.GroupBox" />; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001BE5 RID: 7141 RVA: 0x0006BA60 File Offset: 0x00069C60
		// (set) Token: 0x06001BE6 RID: 7142 RVA: 0x0006BA68 File Offset: 0x00069C68
		[EditorBrowsable(2)]
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

		/// <returns>The text associated with this control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001BE7 RID: 7143 RVA: 0x0006BA74 File Offset: 0x00069C74
		// (set) Token: 0x06001BE8 RID: 7144 RVA: 0x0006BA7C File Offset: 0x00069C7C
		[Localizable(true)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if (base.Text == value)
				{
					return;
				}
				base.Text = value;
				this.Refresh();
			}
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.GroupBox" />.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the <see cref="T:System.Windows.Forms.GroupBox" />.</returns>
		// Token: 0x06001BE9 RID: 7145 RVA: 0x0006BAA0 File Offset: 0x00069CA0
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new GroupBox.GroupBoxAccessibleObject(this);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001BEA RID: 7146 RVA: 0x0006BAA8 File Offset: 0x00069CA8
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.Refresh();
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x06001BEB RID: 7147 RVA: 0x0006BAB8 File Offset: 0x00069CB8
		protected override void OnPaint(PaintEventArgs e)
		{
			ThemeEngine.Current.DrawGroupBox(e.Graphics, base.ClientRectangle, this);
			base.OnPaint(e);
		}

		/// <returns>true if the character was processed as a mnemonic by the control; otherwise, false.</returns>
		/// <param name="charCode">The character to process. </param>
		// Token: 0x06001BEC RID: 7148 RVA: 0x0006BAE4 File Offset: 0x00069CE4
		protected override bool ProcessMnemonic(char charCode)
		{
			if (Control.IsMnemonic(charCode, this.Text))
			{
				if (base.Parent != null)
				{
					base.Parent.SelectNextControl(this, true, false, true, false);
				}
				return true;
			}
			return base.ProcessMnemonic(charCode);
		}

		/// <summary>Scales the <see cref="T:System.Windows.Forms.GroupBox" /> by the specified factor and scaling instruction.</summary>
		/// <param name="factor">The <see cref="T:System.Drawing.SizeF" /> that indicates the height and width of the scaled control.</param>
		/// <param name="specified">One of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values that indicates how the control should be scaled.</param>
		// Token: 0x06001BED RID: 7149 RVA: 0x0006BB28 File Offset: 0x00069D28
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			base.ScaleControl(factor, specified);
		}

		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.GroupBox" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001BEE RID: 7150 RVA: 0x0006BB34 File Offset: 0x00069D34
		public override string ToString()
		{
			return base.GetType().FullName + ", Text: " + this.Text;
		}

		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x06001BEF RID: 7151 RVA: 0x0006BB5C File Offset: 0x00069D5C
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		/// <summary>Gets or sets a value that determines whether to use the <see cref="T:System.Drawing.Graphics" /> class (GDI+) or the <see cref="T:System.Windows.Forms.TextRenderer" /> class (GDI) to render text</summary>
		/// <returns>true if the <see cref="T:System.Drawing.Graphics" /> class should be used to perform text rendering for compatibility with versions 1.0 and 1.1. of the .NET Framework; otherwise, false. The default is false.</returns>
		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001BF0 RID: 7152 RVA: 0x0006BB68 File Offset: 0x00069D68
		// (set) Token: 0x06001BF1 RID: 7153 RVA: 0x0006BB70 File Offset: 0x00069D70
		[DefaultValue(false)]
		public bool UseCompatibleTextRendering
		{
			get
			{
				return this.use_compatible_text_rendering;
			}
			set
			{
				if (this.use_compatible_text_rendering != value)
				{
					this.use_compatible_text_rendering = value;
					if (base.Parent != null)
					{
						base.Parent.PerformLayout(this, "UseCompatibleTextRendering");
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Padding" /> structure that contains the default padding settings for a <see cref="T:System.Windows.Forms.GroupBox" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> with all its edges set to three pixels. </returns>
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001BF2 RID: 7154 RVA: 0x0006BBA8 File Offset: 0x00069DA8
		protected override Padding DefaultPadding
		{
			get
			{
				return new Padding(3);
			}
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x0006BBB0 File Offset: 0x00069DB0
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			Size size;
			size..ctor(base.Padding.Left, base.Padding.Top);
			foreach (object obj in base.Controls)
			{
				Control control = (Control)obj;
				if (control.Dock == DockStyle.Fill)
				{
					if (control.Bounds.Right > size.Width)
					{
						size.Width = control.Bounds.Right;
					}
				}
				else if (control.Dock != DockStyle.Top && control.Dock != DockStyle.Bottom && control.Bounds.Right + control.Margin.Right > size.Width)
				{
					size.Width = control.Bounds.Right + control.Margin.Right;
				}
				if (control.Dock == DockStyle.Fill)
				{
					if (control.Bounds.Bottom > size.Height)
					{
						size.Height = control.Bounds.Bottom;
					}
				}
				else if (control.Dock != DockStyle.Left && control.Dock != DockStyle.Right && control.Bounds.Bottom + control.Margin.Bottom > size.Height)
				{
					size.Height = control.Bounds.Bottom + control.Margin.Bottom;
				}
			}
			size.Width += base.Padding.Right;
			size.Height += base.Padding.Bottom;
			size.Height += this.Font.Height;
			return size;
		}

		// Token: 0x04000F22 RID: 3874
		private FlatStyle flat_style;

		// Token: 0x04000F23 RID: 3875
		private Rectangle display_rectangle = default(Rectangle);

		// Token: 0x020001AB RID: 427
		private class GroupBoxAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x06001BF4 RID: 7156 RVA: 0x0006BDEC File Offset: 0x00069FEC
			public GroupBoxAccessibleObject(Control owner)
				: base(owner)
			{
			}
		}
	}
}
