using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Hosts custom controls or Windows Forms controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000346 RID: 838
	public class ToolStripControlHost : ToolStripItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripControlHost" /> class that hosts the specified control.</summary>
		/// <param name="c">The <see cref="T:System.Windows.Forms.Control" /> hosted by this <see cref="T:System.Windows.Forms.ToolStripControlHost" /> class. </param>
		/// <exception cref="T:System.ArgumentNullException">The control referred to by the <paramref name="c" /> parameter is null.</exception>
		// Token: 0x06003B79 RID: 15225 RVA: 0x000F1B84 File Offset: 0x000EFD84
		public ToolStripControlHost(Control c)
		{
			if (c == null)
			{
				throw new ArgumentNullException("c");
			}
			this.RightToLeft = RightToLeft.No;
			this.control = c;
			this.control_align = 32;
			this.control.TabStop = false;
			this.control.Resize += new EventHandler(this.ControlResizeHandler);
			this.Size = this.DefaultSize;
			this.OnSubscribeControlEvents(this.control);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripControlHost" /> class that hosts the specified control and that has the specified name.</summary>
		/// <param name="c">The <see cref="T:System.Windows.Forms.Control" /> hosted by this <see cref="T:System.Windows.Forms.ToolStripControlHost" /> class.</param>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripControlHost" />.</param>
		// Token: 0x06003B7A RID: 15226 RVA: 0x000F1BFC File Offset: 0x000EFDFC
		public ToolStripControlHost(Control c, string name)
			: this(c)
		{
			base.Name = name;
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x000F1C0C File Offset: 0x000EFE0C
		// Note: this type is marked as 'beforefieldinit'.
		static ToolStripControlHost()
		{
			ToolStripControlHost.EnterEvent = new object();
			ToolStripControlHost.GotFocusEvent = new object();
			ToolStripControlHost.KeyDownEvent = new object();
			ToolStripControlHost.KeyPressEvent = new object();
			ToolStripControlHost.KeyUpEvent = new object();
			ToolStripControlHost.LeaveEvent = new object();
			ToolStripControlHost.LostFocusEvent = new object();
			ToolStripControlHost.ValidatedEvent = new object();
			ToolStripControlHost.ValidatingEvent = new object();
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x1400037D RID: 893
		// (add) Token: 0x06003B7C RID: 15228 RVA: 0x000F1C74 File Offset: 0x000EFE74
		// (remove) Token: 0x06003B7D RID: 15229 RVA: 0x000F1C80 File Offset: 0x000EFE80
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler DisplayStyleChanged
		{
			add
			{
				base.DisplayStyleChanged += value;
			}
			remove
			{
				base.DisplayStyleChanged -= value;
			}
		}

		/// <summary>Occurs when the hosted control is entered.</summary>
		// Token: 0x1400037E RID: 894
		// (add) Token: 0x06003B7E RID: 15230 RVA: 0x000F1C8C File Offset: 0x000EFE8C
		// (remove) Token: 0x06003B7F RID: 15231 RVA: 0x000F1CA0 File Offset: 0x000EFEA0
		public event EventHandler Enter
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.EnterEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.EnterEvent, value);
			}
		}

		/// <summary>Occurs when the hosted control receives focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400037F RID: 895
		// (add) Token: 0x06003B80 RID: 15232 RVA: 0x000F1CB4 File Offset: 0x000EFEB4
		// (remove) Token: 0x06003B81 RID: 15233 RVA: 0x000F1CC8 File Offset: 0x000EFEC8
		[Browsable(false)]
		[EditorBrowsable(2)]
		public event EventHandler GotFocus
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.GotFocusEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.GotFocusEvent, value);
			}
		}

		/// <summary>Occurs when a key is pressed and held down while the hosted control has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000380 RID: 896
		// (add) Token: 0x06003B82 RID: 15234 RVA: 0x000F1CDC File Offset: 0x000EFEDC
		// (remove) Token: 0x06003B83 RID: 15235 RVA: 0x000F1CF0 File Offset: 0x000EFEF0
		public event KeyEventHandler KeyDown
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.KeyDownEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.KeyDownEvent, value);
			}
		}

		/// <summary>Occurs when a key is pressed while the hosted control has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000381 RID: 897
		// (add) Token: 0x06003B84 RID: 15236 RVA: 0x000F1D04 File Offset: 0x000EFF04
		// (remove) Token: 0x06003B85 RID: 15237 RVA: 0x000F1D18 File Offset: 0x000EFF18
		public event KeyPressEventHandler KeyPress
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.KeyPressEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.KeyPressEvent, value);
			}
		}

		/// <summary>Occurs when a key is released while the hosted control has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000382 RID: 898
		// (add) Token: 0x06003B86 RID: 15238 RVA: 0x000F1D2C File Offset: 0x000EFF2C
		// (remove) Token: 0x06003B87 RID: 15239 RVA: 0x000F1D40 File Offset: 0x000EFF40
		public event KeyEventHandler KeyUp
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.KeyUpEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.KeyUpEvent, value);
			}
		}

		/// <summary>Occurs when the input focus leaves the hosted control.</summary>
		// Token: 0x14000383 RID: 899
		// (add) Token: 0x06003B88 RID: 15240 RVA: 0x000F1D54 File Offset: 0x000EFF54
		// (remove) Token: 0x06003B89 RID: 15241 RVA: 0x000F1D68 File Offset: 0x000EFF68
		public event EventHandler Leave
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.LeaveEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.LeaveEvent, value);
			}
		}

		/// <summary>Occurs when the hosted control loses focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000384 RID: 900
		// (add) Token: 0x06003B8A RID: 15242 RVA: 0x000F1D7C File Offset: 0x000EFF7C
		// (remove) Token: 0x06003B8B RID: 15243 RVA: 0x000F1D90 File Offset: 0x000EFF90
		[Browsable(false)]
		[EditorBrowsable(2)]
		public event EventHandler LostFocus
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.LostFocusEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.LostFocusEvent, value);
			}
		}

		/// <summary>Occurs after the hosted control has been successfully validated.</summary>
		// Token: 0x14000385 RID: 901
		// (add) Token: 0x06003B8C RID: 15244 RVA: 0x000F1DA4 File Offset: 0x000EFFA4
		// (remove) Token: 0x06003B8D RID: 15245 RVA: 0x000F1DB8 File Offset: 0x000EFFB8
		public event EventHandler Validated
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.ValidatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.ValidatedEvent, value);
			}
		}

		/// <summary>Occurs while the hosted control is validating.</summary>
		// Token: 0x14000386 RID: 902
		// (add) Token: 0x06003B8E RID: 15246 RVA: 0x000F1DCC File Offset: 0x000EFFCC
		// (remove) Token: 0x06003B8F RID: 15247 RVA: 0x000F1DE0 File Offset: 0x000EFFE0
		public event CancelEventHandler Validating
		{
			add
			{
				base.Events.AddHandler(ToolStripControlHost.ValidatingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripControlHost.ValidatingEvent, value);
			}
		}

		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the item. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06003B90 RID: 15248 RVA: 0x000F1DF4 File Offset: 0x000EFFF4
		// (set) Token: 0x06003B91 RID: 15249 RVA: 0x000F1E04 File Offset: 0x000F0004
		public override Color BackColor
		{
			get
			{
				return this.control.BackColor;
			}
			set
			{
				this.control.BackColor = value;
			}
		}

		/// <summary>Gets or sets the background image displayed in the control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the image to display in the background of the control.</returns>
		// Token: 0x17000F8D RID: 3981
		// (get) Token: 0x06003B92 RID: 15250 RVA: 0x000F1E14 File Offset: 0x000F0014
		// (set) Token: 0x06003B93 RID: 15251 RVA: 0x000F1E1C File Offset: 0x000F001C
		[DefaultValue(null)]
		[Localizable(true)]
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

		/// <summary>Gets or sets the background image layout as defined in the ImageLayout enumeration.</summary>
		/// <returns>One of the values of <see cref="T:System.Windows.Forms.ImageLayout" />:<see cref="F:System.Windows.Forms.ImageLayout.Center" /><see cref="F:System.Windows.Forms.ImageLayout.None" /><see cref="F:System.Windows.Forms.ImageLayout.Stretch" /><see cref="F:System.Windows.Forms.ImageLayout.Tile" /> (default)<see cref="F:System.Windows.Forms.ImageLayout.Zoom" /></returns>
		// Token: 0x17000F8E RID: 3982
		// (get) Token: 0x06003B94 RID: 15252 RVA: 0x000F1E28 File Offset: 0x000F0028
		// (set) Token: 0x06003B95 RID: 15253 RVA: 0x000F1E30 File Offset: 0x000F0030
		[Localizable(true)]
		[DefaultValue(ImageLayout.Tile)]
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

		/// <summary>Gets a value indicating whether the control can be selected.</summary>
		/// <returns>true if the control can be selected; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F8F RID: 3983
		// (get) Token: 0x06003B96 RID: 15254 RVA: 0x000F1E3C File Offset: 0x000F003C
		public override bool CanSelect
		{
			get
			{
				return this.control.CanSelect;
			}
		}

		/// <summary>Gets or sets a value indicating whether the hosted control causes and raises validation events on other controls when the hosted control receives focus.</summary>
		/// <returns>true if the hosted control causes and raises validation events on other controls when the hosted control receives focus; otherwise, false. The default is true.</returns>
		// Token: 0x17000F90 RID: 3984
		// (get) Token: 0x06003B97 RID: 15255 RVA: 0x000F1E4C File Offset: 0x000F004C
		// (set) Token: 0x06003B98 RID: 15256 RVA: 0x000F1E5C File Offset: 0x000F005C
		[DefaultValue(true)]
		public bool CausesValidation
		{
			get
			{
				return this.control.CausesValidation;
			}
			set
			{
				this.control.CausesValidation = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.Control" /> that this <see cref="T:System.Windows.Forms.ToolStripControlHost" /> is hosting.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Control" /> that this <see cref="T:System.Windows.Forms.ToolStripControlHost" /> is hosting.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F91 RID: 3985
		// (get) Token: 0x06003B99 RID: 15257 RVA: 0x000F1E6C File Offset: 0x000F006C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Control Control
		{
			get
			{
				return this.control;
			}
		}

		/// <summary>Gets or sets the alignment of the control on the form.</summary>
		/// <returns>One of the <see cref="T:System.Drawing.ContentAlignment" /> values. The default is <see cref="F:System.Drawing.ContentAlignment.MiddleCenter" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <see cref="P:System.Windows.Forms.ToolStripControlHost.ControlAlign" /> property is set to a value that is not one of the <see cref="T:System.Drawing.ContentAlignment" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F92 RID: 3986
		// (get) Token: 0x06003B9A RID: 15258 RVA: 0x000F1E74 File Offset: 0x000F0074
		// (set) Token: 0x06003B9B RID: 15259 RVA: 0x000F1E7C File Offset: 0x000F007C
		[Browsable(false)]
		[DefaultValue(32)]
		public ContentAlignment ControlAlign
		{
			get
			{
				return this.control_align;
			}
			set
			{
				if (this.control_align != value)
				{
					if (!Enum.IsDefined(typeof(ContentAlignment), value))
					{
						throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for ContentAlignment", value));
					}
					this.control_align = value;
					if (this.control != null)
					{
						this.control.Bounds = base.AlignInRectangle(this.Bounds, this.control.Size, this.control_align);
					}
				}
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemDisplayStyle" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F93 RID: 3987
		// (get) Token: 0x06003B9C RID: 15260 RVA: 0x000F1F00 File Offset: 0x000F0100
		// (set) Token: 0x06003B9D RID: 15261 RVA: 0x000F1F08 File Offset: 0x000F0108
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public new ToolStripItemDisplayStyle DisplayStyle
		{
			get
			{
				return base.DisplayStyle;
			}
			set
			{
				base.DisplayStyle = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if double clicking is enabled; otherwise, false. </returns>
		// Token: 0x17000F94 RID: 3988
		// (get) Token: 0x06003B9E RID: 15262 RVA: 0x000F1F14 File Offset: 0x000F0114
		// (set) Token: 0x06003B9F RID: 15263 RVA: 0x000F1F1C File Offset: 0x000F011C
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DefaultValue(false)]
		public new bool DoubleClickEnabled
		{
			get
			{
				return this.double_click_enabled;
			}
			set
			{
				this.double_click_enabled = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the parent control of the <see cref="T:System.Windows.Forms.ToolStripItem" /> is enabled.</summary>
		/// <returns>true if the parent control of the <see cref="T:System.Windows.Forms.ToolStripItem" /> is enabled; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F95 RID: 3989
		// (get) Token: 0x06003BA0 RID: 15264 RVA: 0x000F1F28 File Offset: 0x000F0128
		// (set) Token: 0x06003BA1 RID: 15265 RVA: 0x000F1F30 File Offset: 0x000F0130
		public override bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
				this.control.Enabled = value;
			}
		}

		/// <summary>Gets a value indicating whether the control has input focus.</summary>
		/// <returns>true if the control has input focus; otherwise, false. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F96 RID: 3990
		// (get) Token: 0x06003BA2 RID: 15266 RVA: 0x000F1F48 File Offset: 0x000F0148
		[Browsable(false)]
		[EditorBrowsable(0)]
		public virtual bool Focused
		{
			get
			{
				return this.control.Focused;
			}
		}

		/// <summary>Gets or sets the font to be used on the hosted control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> for the hosted control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F97 RID: 3991
		// (get) Token: 0x06003BA3 RID: 15267 RVA: 0x000F1F58 File Offset: 0x000F0158
		// (set) Token: 0x06003BA4 RID: 15268 RVA: 0x000F1F68 File Offset: 0x000F0168
		public override Font Font
		{
			get
			{
				return this.control.Font;
			}
			set
			{
				this.control.Font = value;
			}
		}

		/// <summary>Gets or sets the foreground color of the hosted control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> representing the foreground color of the hosted control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F98 RID: 3992
		// (get) Token: 0x06003BA5 RID: 15269 RVA: 0x000F1F78 File Offset: 0x000F0178
		// (set) Token: 0x06003BA6 RID: 15270 RVA: 0x000F1F88 File Offset: 0x000F0188
		public override Color ForeColor
		{
			get
			{
				return this.control.ForeColor;
			}
			set
			{
				this.control.ForeColor = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
		// Token: 0x17000F99 RID: 3993
		// (get) Token: 0x06003BA7 RID: 15271 RVA: 0x000F1F98 File Offset: 0x000F0198
		// (set) Token: 0x06003BA8 RID: 15272 RVA: 0x000F1FA0 File Offset: 0x000F01A0
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public override Image Image
		{
			get
			{
				return base.Image;
			}
			set
			{
				base.Image = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.ContentAlignment" />.</returns>
		// Token: 0x17000F9A RID: 3994
		// (get) Token: 0x06003BA9 RID: 15273 RVA: 0x000F1FAC File Offset: 0x000F01AC
		// (set) Token: 0x06003BAA RID: 15274 RVA: 0x000F1FB4 File Offset: 0x000F01B4
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public new ContentAlignment ImageAlign
		{
			get
			{
				return base.ImageAlign;
			}
			set
			{
				base.ImageAlign = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripItemImageScaling" />.</returns>
		// Token: 0x17000F9B RID: 3995
		// (get) Token: 0x06003BAB RID: 15275 RVA: 0x000F1FC0 File Offset: 0x000F01C0
		// (set) Token: 0x06003BAC RID: 15276 RVA: 0x000F1FC8 File Offset: 0x000F01C8
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DesignerSerializationVisibility(0)]
		public new ToolStripItemImageScaling ImageScaling
		{
			get
			{
				return base.ImageScaling;
			}
			set
			{
				base.ImageScaling = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
		// Token: 0x17000F9C RID: 3996
		// (get) Token: 0x06003BAD RID: 15277 RVA: 0x000F1FD4 File Offset: 0x000F01D4
		// (set) Token: 0x06003BAE RID: 15278 RVA: 0x000F1FDC File Offset: 0x000F01DC
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new Color ImageTransparentColor
		{
			get
			{
				return base.ImageTransparentColor;
			}
			set
			{
				base.ImageTransparentColor = value;
			}
		}

		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000F9D RID: 3997
		// (get) Token: 0x06003BAF RID: 15279 RVA: 0x000F1FE8 File Offset: 0x000F01E8
		// (set) Token: 0x06003BB0 RID: 15280 RVA: 0x000F1FF0 File Offset: 0x000F01F0
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>true if the image is mirrored; otherwise, false.</returns>
		// Token: 0x17000F9E RID: 3998
		// (get) Token: 0x06003BB1 RID: 15281 RVA: 0x000F1FFC File Offset: 0x000F01FC
		// (set) Token: 0x06003BB2 RID: 15282 RVA: 0x000F2004 File Offset: 0x000F0204
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new bool RightToLeftAutoMirrorImage
		{
			get
			{
				return base.RightToLeftAutoMirrorImage;
			}
			set
			{
				base.RightToLeftAutoMirrorImage = value;
			}
		}

		/// <summary>Gets a value indicating whether the item is selected.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolStripItem" /> is selected; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06003BB3 RID: 15283 RVA: 0x000F2010 File Offset: 0x000F0210
		public override bool Selected
		{
			get
			{
				return base.Selected;
			}
		}

		/// <summary>Gets or sets the site of the hosted control.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.ISite" /> associated with the control.</returns>
		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06003BB4 RID: 15284 RVA: 0x000F2018 File Offset: 0x000F0218
		// (set) Token: 0x06003BB5 RID: 15285 RVA: 0x000F2028 File Offset: 0x000F0228
		[EditorBrowsable(2)]
		public override ISite Site
		{
			get
			{
				return this.control.Site;
			}
			set
			{
				this.control.Site = value;
			}
		}

		/// <summary>Gets or sets the size of the <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06003BB6 RID: 15286 RVA: 0x000F2038 File Offset: 0x000F0238
		// (set) Token: 0x06003BB7 RID: 15287 RVA: 0x000F2040 File Offset: 0x000F0240
		public override Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				this.control.Size = value;
				base.Size = value;
				if (base.Owner != null)
				{
					base.Owner.PerformLayout();
				}
			}
		}

		/// <summary>Gets or sets the text to be displayed on the hosted control.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the text.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06003BB8 RID: 15288 RVA: 0x000F2078 File Offset: 0x000F0278
		// (set) Token: 0x06003BB9 RID: 15289 RVA: 0x000F2088 File Offset: 0x000F0288
		[DefaultValue("")]
		public override string Text
		{
			get
			{
				return this.control.Text;
			}
			set
			{
				base.Text = value;
				this.control.Text = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.VisualStyles.ContentAlignment" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06003BBA RID: 15290 RVA: 0x000F20A0 File Offset: 0x000F02A0
		// (set) Token: 0x06003BBB RID: 15291 RVA: 0x000F20A8 File Offset: 0x000F02A8
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new ContentAlignment TextAlign
		{
			get
			{
				return base.TextAlign;
			}
			set
			{
				base.TextAlign = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ToolStripTextDirection" />.</returns>
		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06003BBC RID: 15292 RVA: 0x000F20B4 File Offset: 0x000F02B4
		// (set) Token: 0x06003BBD RID: 15293 RVA: 0x000F20BC File Offset: 0x000F02BC
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DefaultValue(ToolStripTextDirection.Horizontal)]
		public override ToolStripTextDirection TextDirection
		{
			get
			{
				return base.TextDirection;
			}
			set
			{
				base.TextDirection = value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TextImageRelation" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06003BBE RID: 15294 RVA: 0x000F20C8 File Offset: 0x000F02C8
		// (set) Token: 0x06003BBF RID: 15295 RVA: 0x000F20D0 File Offset: 0x000F02D0
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new TextImageRelation TextImageRelation
		{
			get
			{
				return base.TextImageRelation;
			}
			set
			{
				base.TextImageRelation = value;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06003BC0 RID: 15296 RVA: 0x000F20DC File Offset: 0x000F02DC
		protected override Size DefaultSize
		{
			get
			{
				if (this.control == null)
				{
					return new Size(23, 23);
				}
				return this.control.Size;
			}
		}

		/// <summary>Gives the focus to a control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003BC1 RID: 15297 RVA: 0x000F210C File Offset: 0x000F030C
		[EditorBrowsable(2)]
		public void Focus()
		{
			this.control.Focus();
		}

		/// <summary>Retrieves the size of a rectangular area into which a control can be fitted.</summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		/// <param name="constrainingSize">The custom-sized area for a control. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06003BC2 RID: 15298 RVA: 0x000F211C File Offset: 0x000F031C
		public override Size GetPreferredSize(Size constrainingSize)
		{
			return this.control.GetPreferredSize(constrainingSize);
		}

		/// <summary>This method is not relevant to this class.</summary>
		// Token: 0x06003BC3 RID: 15299 RVA: 0x000F212C File Offset: 0x000F032C
		[EditorBrowsable(1)]
		public override void ResetBackColor()
		{
			base.ResetBackColor();
		}

		/// <summary>This method is not relevant to this class.</summary>
		// Token: 0x06003BC4 RID: 15300 RVA: 0x000F2134 File Offset: 0x000F0334
		[EditorBrowsable(1)]
		public override void ResetForeColor()
		{
			base.ResetForeColor();
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x000F213C File Offset: 0x000F033C
		[EditorBrowsable(2)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return this.control.AccessibilityObject;
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ToolStripControlHost" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003BC6 RID: 15302 RVA: 0x000F214C File Offset: 0x000F034C
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this.control.Created && !this.control.IsDisposed)
			{
				this.control.Dispose();
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ToolStripItem.Bounds" /> property changes.</summary>
		// Token: 0x06003BC7 RID: 15303 RVA: 0x000F218C File Offset: 0x000F038C
		protected override void OnBoundsChanged()
		{
			if (this.control != null)
			{
				this.control.Bounds = base.AlignInRectangle(this.Bounds, this.control.Size, this.control_align);
			}
			base.OnBoundsChanged();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.Enter" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003BC8 RID: 15304 RVA: 0x000F21D4 File Offset: 0x000F03D4
		protected virtual void OnEnter(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripControlHost.EnterEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003BC9 RID: 15305 RVA: 0x000F2208 File Offset: 0x000F0408
		protected virtual void OnGotFocus(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripControlHost.GotFocusEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x06003BCA RID: 15306 RVA: 0x000F223C File Offset: 0x000F043C
		private void ControlResizeHandler(object obj, EventArgs args)
		{
			this.OnHostedControlResize(args);
		}

		/// <summary>Synchronizes the resizing of the control host with the resizing of the hosted control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003BCB RID: 15307 RVA: 0x000F2248 File Offset: 0x000F0448
		protected virtual void OnHostedControlResize(EventArgs e)
		{
			if (this.control != null)
			{
				this.control.Location = base.AlignInRectangle(this.Bounds, this.control.Size, this.control_align).Location;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.KeyDown" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x06003BCC RID: 15308 RVA: 0x000F2290 File Offset: 0x000F0490
		protected virtual void OnKeyDown(KeyEventArgs e)
		{
			KeyEventHandler keyEventHandler = (KeyEventHandler)base.Events[ToolStripControlHost.KeyDownEvent];
			if (keyEventHandler != null)
			{
				keyEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.KeyPress" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyPressEventArgs" /> that contains the event data.</param>
		// Token: 0x06003BCD RID: 15309 RVA: 0x000F22C4 File Offset: 0x000F04C4
		protected virtual void OnKeyPress(KeyPressEventArgs e)
		{
			KeyPressEventHandler keyPressEventHandler = (KeyPressEventHandler)base.Events[ToolStripControlHost.KeyPressEvent];
			if (keyPressEventHandler != null)
			{
				keyPressEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.KeyUp" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x06003BCE RID: 15310 RVA: 0x000F22F8 File Offset: 0x000F04F8
		protected virtual void OnKeyUp(KeyEventArgs e)
		{
			KeyEventHandler keyEventHandler = (KeyEventHandler)base.Events[ToolStripControlHost.KeyUpEvent];
			if (keyEventHandler != null)
			{
				keyEventHandler(this, e);
			}
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		// Token: 0x06003BCF RID: 15311 RVA: 0x000F232C File Offset: 0x000F052C
		protected override void OnLayout(LayoutEventArgs e)
		{
			base.OnLayout(e);
			if (this.control != null)
			{
				this.control.Bounds = base.AlignInRectangle(this.Bounds, this.control.Size, this.control_align);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.Leave" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003BD0 RID: 15312 RVA: 0x000F2374 File Offset: 0x000F0574
		protected virtual void OnLeave(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripControlHost.LeaveEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.LostFocus" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003BD1 RID: 15313 RVA: 0x000F23A8 File Offset: 0x000F05A8
		protected virtual void OnLostFocus(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripControlHost.LostFocusEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x06003BD2 RID: 15314 RVA: 0x000F23DC File Offset: 0x000F05DC
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
		}

		/// <param name="oldParent">The original parent of the item. </param>
		/// <param name="newParent">The new parent of the item. </param>
		// Token: 0x06003BD3 RID: 15315 RVA: 0x000F23E8 File Offset: 0x000F05E8
		protected override void OnParentChanged(ToolStrip oldParent, ToolStrip newParent)
		{
			base.OnParentChanged(oldParent, newParent);
			if (oldParent != null)
			{
				oldParent.Controls.Remove(this.control);
			}
			if (newParent != null)
			{
				newParent.Controls.Add(this.control);
			}
		}

		/// <summary>Subscribes events from the hosted control.</summary>
		/// <param name="control">The control from which to subscribe events.</param>
		// Token: 0x06003BD4 RID: 15316 RVA: 0x000F242C File Offset: 0x000F062C
		protected virtual void OnSubscribeControlEvents(Control control)
		{
			this.control.Enter += new EventHandler(this.HandleEnter);
			this.control.GotFocus += new EventHandler(this.HandleGotFocus);
			this.control.KeyDown += this.HandleKeyDown;
			this.control.KeyPress += this.HandleKeyPress;
			this.control.KeyUp += this.HandleKeyUp;
			this.control.Leave += new EventHandler(this.HandleLeave);
			this.control.LostFocus += new EventHandler(this.HandleLostFocus);
			this.control.Validated += new EventHandler(this.HandleValidated);
			this.control.Validating += new CancelEventHandler(this.HandleValidating);
		}

		/// <summary>Unsubscribes events from the hosted control.</summary>
		/// <param name="control">The control from which to unsubscribe events.</param>
		// Token: 0x06003BD5 RID: 15317 RVA: 0x000F2508 File Offset: 0x000F0708
		protected virtual void OnUnsubscribeControlEvents(Control control)
		{
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.Validated" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003BD6 RID: 15318 RVA: 0x000F250C File Offset: 0x000F070C
		protected virtual void OnValidated(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ToolStripControlHost.ValidatedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ToolStripControlHost.Validating" /> event.</summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		// Token: 0x06003BD7 RID: 15319 RVA: 0x000F2540 File Offset: 0x000F0740
		protected virtual void OnValidating(CancelEventArgs e)
		{
			CancelEventHandler cancelEventHandler = (CancelEventHandler)base.Events[ToolStripControlHost.ValidatingEvent];
			if (cancelEventHandler != null)
			{
				cancelEventHandler.Invoke(this, e);
			}
		}

		/// <returns>false in all cases.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06003BD8 RID: 15320 RVA: 0x000F2574 File Offset: 0x000F0774
		protected internal override bool ProcessCmdKey(ref Message m, Keys keyData)
		{
			return base.ProcessCmdKey(ref m, keyData);
		}

		/// <returns>true if the key was processed by the item; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process. </param>
		// Token: 0x06003BD9 RID: 15321 RVA: 0x000F2580 File Offset: 0x000F0780
		protected internal override bool ProcessDialogKey(Keys keyData)
		{
			return base.ProcessDialogKey(keyData);
		}

		/// <param name="visible">true to make the <see cref="T:System.Windows.Forms.ToolStripItem" /> visible; otherwise, false.</param>
		// Token: 0x06003BDA RID: 15322 RVA: 0x000F258C File Offset: 0x000F078C
		protected override void SetVisibleCore(bool visible)
		{
			base.SetVisibleCore(visible);
			this.control.Visible = visible;
			if (this.control != null)
			{
				this.control.Bounds = base.AlignInRectangle(this.Bounds, this.control.Size, this.control_align);
			}
		}

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06003BDB RID: 15323 RVA: 0x000F25E0 File Offset: 0x000F07E0
		internal override ToolStripTextDirection DefaultTextDirection
		{
			get
			{
				return ToolStripTextDirection.Horizontal;
			}
		}

		// Token: 0x06003BDC RID: 15324 RVA: 0x000F25E4 File Offset: 0x000F07E4
		internal override void Dismiss(ToolStripDropDownCloseReason reason)
		{
			if (this.Selected)
			{
				base.Parent.Focus();
			}
			base.Dismiss(reason);
		}

		// Token: 0x06003BDD RID: 15325 RVA: 0x000F2610 File Offset: 0x000F0810
		private void HandleEnter(object sender, EventArgs e)
		{
			this.OnEnter(e);
		}

		// Token: 0x06003BDE RID: 15326 RVA: 0x000F261C File Offset: 0x000F081C
		private void HandleGotFocus(object sender, EventArgs e)
		{
			this.OnGotFocus(e);
		}

		// Token: 0x06003BDF RID: 15327 RVA: 0x000F2628 File Offset: 0x000F0828
		private void HandleKeyDown(object sender, KeyEventArgs e)
		{
			this.OnKeyDown(e);
		}

		// Token: 0x06003BE0 RID: 15328 RVA: 0x000F2634 File Offset: 0x000F0834
		private void HandleKeyPress(object sender, KeyPressEventArgs e)
		{
			this.OnKeyPress(e);
		}

		// Token: 0x06003BE1 RID: 15329 RVA: 0x000F2640 File Offset: 0x000F0840
		private void HandleKeyUp(object sender, KeyEventArgs e)
		{
			this.OnKeyUp(e);
		}

		// Token: 0x06003BE2 RID: 15330 RVA: 0x000F264C File Offset: 0x000F084C
		private void HandleLeave(object sender, EventArgs e)
		{
			this.OnLeave(e);
		}

		// Token: 0x06003BE3 RID: 15331 RVA: 0x000F2658 File Offset: 0x000F0858
		private void HandleLostFocus(object sender, EventArgs e)
		{
			this.OnLostFocus(e);
		}

		// Token: 0x06003BE4 RID: 15332 RVA: 0x000F2664 File Offset: 0x000F0864
		private void HandleValidated(object sender, EventArgs e)
		{
			this.OnValidated(e);
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x000F2670 File Offset: 0x000F0870
		private void HandleValidating(object sender, CancelEventArgs e)
		{
			this.OnValidating(e);
		}

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06003BE6 RID: 15334 RVA: 0x000F267C File Offset: 0x000F087C
		// (set) Token: 0x06003BE7 RID: 15335 RVA: 0x000F2684 File Offset: 0x000F0884
		internal override bool InternalVisible
		{
			get
			{
				return base.InternalVisible;
			}
			set
			{
				this.Control.Visible = value;
				base.InternalVisible = value;
			}
		}

		// Token: 0x04001A58 RID: 6744
		private Control control;

		// Token: 0x04001A59 RID: 6745
		private ContentAlignment control_align;

		// Token: 0x04001A5A RID: 6746
		private bool double_click_enabled;
	}
}
