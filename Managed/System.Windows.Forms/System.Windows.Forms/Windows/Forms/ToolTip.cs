using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents a small rectangular pop-up window that displays a brief description of a control's purpose when the user rests the pointer on the control.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000383 RID: 899
	[ToolboxItemFilter("System.Windows.Forms", 0)]
	[DefaultEvent("Popup")]
	[ProvideProperty("ToolTip", typeof(Control))]
	public class ToolTip : Component, IExtenderProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolTip" /> without a specified container.</summary>
		// Token: 0x060040F0 RID: 16624 RVA: 0x0010139C File Offset: 0x000FF59C
		public ToolTip()
		{
			this.is_active = true;
			this.automatic_delay = 500;
			this.autopop_delay = 5000;
			this.initial_delay = 500;
			this.re_show_delay = 100;
			this.show_always = false;
			this.back_color = SystemColors.Info;
			this.fore_color = SystemColors.InfoText;
			this.isBalloon = false;
			this.stripAmpersands = false;
			this.useAnimation = true;
			this.useFading = true;
			this.tooltip_strings = new Hashtable(5);
			this.controls = new ArrayList(5);
			this.tooltip_window = new ToolTip.ToolTipWindow();
			this.tooltip_window.MouseLeave += new EventHandler(this.control_MouseLeave);
			this.tooltip_window.Draw += this.tooltip_window_Draw;
			this.tooltip_window.Popup += this.tooltip_window_Popup;
			this.tooltip_window.UnPopup += delegate(object sender, PopupEventArgs args)
			{
				this.OnUnPopup(args);
			};
			this.UnPopup += ToolTip.OnUIAUnPopup;
			this.timer = new Timer();
			this.timer.Enabled = false;
			this.timer.Tick += new EventHandler(this.timer_Tick);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolTip" /> class with a specified container.</summary>
		/// <param name="cont">An <see cref="T:System.ComponentModel.IContainer" /> that represents the container of the <see cref="T:System.Windows.Forms.ToolTip" />. </param>
		// Token: 0x060040F1 RID: 16625 RVA: 0x001014D8 File Offset: 0x000FF6D8
		public ToolTip(IContainer cont)
			: this()
		{
			cont.Add(this);
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x001014E8 File Offset: 0x000FF6E8
		// Note: this type is marked as 'beforefieldinit'.
		static ToolTip()
		{
			ToolTip.UnPopupEvent = new object();
			ToolTip.PopupEvent = new object();
			ToolTip.DrawEvent = new object();
		}

		// Token: 0x140003F9 RID: 1017
		// (add) Token: 0x060040F3 RID: 16627 RVA: 0x00101508 File Offset: 0x000FF708
		// (remove) Token: 0x060040F4 RID: 16628 RVA: 0x0010151C File Offset: 0x000FF71C
		internal event PopupEventHandler UnPopup
		{
			add
			{
				base.Events.AddHandler(ToolTip.UnPopupEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolTip.UnPopupEvent, value);
			}
		}

		// Token: 0x140003FA RID: 1018
		// (add) Token: 0x060040F5 RID: 16629 RVA: 0x00101530 File Offset: 0x000FF730
		// (remove) Token: 0x060040F6 RID: 16630 RVA: 0x00101548 File Offset: 0x000FF748
		internal static event PopupEventHandler UIAUnPopup;

		// Token: 0x140003FB RID: 1019
		// (add) Token: 0x060040F7 RID: 16631 RVA: 0x00101560 File Offset: 0x000FF760
		// (remove) Token: 0x060040F8 RID: 16632 RVA: 0x00101578 File Offset: 0x000FF778
		internal static event ControlEventHandler UIAToolTipHookUp;

		// Token: 0x140003FC RID: 1020
		// (add) Token: 0x060040F9 RID: 16633 RVA: 0x00101590 File Offset: 0x000FF790
		// (remove) Token: 0x060040FA RID: 16634 RVA: 0x001015A8 File Offset: 0x000FF7A8
		internal static event ControlEventHandler UIAToolTipUnhookUp;

		/// <summary>Occurs before a ToolTip is initially displayed. This is the default event for the <see cref="T:System.Windows.Forms.ToolTip" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003FD RID: 1021
		// (add) Token: 0x060040FB RID: 16635 RVA: 0x001015C0 File Offset: 0x000FF7C0
		// (remove) Token: 0x060040FC RID: 16636 RVA: 0x001015D4 File Offset: 0x000FF7D4
		public event PopupEventHandler Popup
		{
			add
			{
				base.Events.AddHandler(ToolTip.PopupEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolTip.PopupEvent, value);
			}
		}

		/// <summary>Occurs when the ToolTip is drawn and the <see cref="P:System.Windows.Forms.ToolTip.OwnerDraw" /> property is set to true.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140003FE RID: 1022
		// (add) Token: 0x060040FD RID: 16637 RVA: 0x001015E8 File Offset: 0x000FF7E8
		// (remove) Token: 0x060040FE RID: 16638 RVA: 0x001015FC File Offset: 0x000FF7FC
		public event DrawToolTipEventHandler Draw
		{
			add
			{
				base.Events.AddHandler(ToolTip.DrawEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolTip.DrawEvent, value);
			}
		}

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x060040FF RID: 16639 RVA: 0x00101610 File Offset: 0x000FF810
		internal Rectangle UIAToolTipRectangle
		{
			get
			{
				return this.tooltip_window.Bounds;
			}
		}

		// Token: 0x06004100 RID: 16640 RVA: 0x00101620 File Offset: 0x000FF820
		internal static void OnUIAUnPopup(object sender, PopupEventArgs args)
		{
			if (ToolTip.UIAUnPopup != null)
			{
				ToolTip.UIAUnPopup(sender, args);
			}
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x00101638 File Offset: 0x000FF838
		internal static void OnUIAToolTipHookUp(object sender, ControlEventArgs args)
		{
			if (ToolTip.UIAToolTipHookUp != null)
			{
				ToolTip.UIAToolTipHookUp(sender, args);
			}
		}

		// Token: 0x06004102 RID: 16642 RVA: 0x00101650 File Offset: 0x000FF850
		internal static void OnUIAToolTipUnhookUp(object sender, ControlEventArgs args)
		{
			if (ToolTip.UIAToolTipUnhookUp != null)
			{
				ToolTip.UIAToolTipUnhookUp(sender, args);
			}
		}

		/// <summary>Releases the unmanaged resources and performs other cleanup operations before the <see cref="T:System.Windows.Forms.Cursor" /> is reclaimed by the garbage collector.</summary>
		// Token: 0x06004103 RID: 16643 RVA: 0x00101668 File Offset: 0x000FF868
		~ToolTip()
		{
		}

		/// <summary>Gets or sets a value indicating whether the ToolTip is currently active.</summary>
		/// <returns>true if the ToolTip is currently active; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x06004104 RID: 16644 RVA: 0x001016A0 File Offset: 0x000FF8A0
		// (set) Token: 0x06004105 RID: 16645 RVA: 0x001016A8 File Offset: 0x000FF8A8
		[DefaultValue(true)]
		public bool Active
		{
			get
			{
				return this.is_active;
			}
			set
			{
				if (this.is_active != value)
				{
					this.is_active = value;
					if (this.tooltip_window.Visible)
					{
						this.tooltip_window.Visible = false;
						this.active_control = null;
					}
				}
			}
		}

		/// <summary>Gets or sets the automatic delay for the ToolTip.</summary>
		/// <returns>The automatic delay, in milliseconds. The default is 500.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x06004106 RID: 16646 RVA: 0x001016EC File Offset: 0x000FF8EC
		// (set) Token: 0x06004107 RID: 16647 RVA: 0x001016F4 File Offset: 0x000FF8F4
		[RefreshProperties(1)]
		[DefaultValue(500)]
		public int AutomaticDelay
		{
			get
			{
				return this.automatic_delay;
			}
			set
			{
				if (this.automatic_delay != value)
				{
					this.automatic_delay = value;
					this.autopop_delay = this.automatic_delay * 10;
					this.initial_delay = this.automatic_delay;
					this.re_show_delay = this.automatic_delay / 5;
				}
			}
		}

		/// <summary>Gets or sets the period of time the ToolTip remains visible if the pointer is stationary on a control with specified ToolTip text.</summary>
		/// <returns>The period of time, in milliseconds, that the <see cref="T:System.Windows.Forms.ToolTip" /> remains visible when the pointer is stationary on a control. The default value is 5000.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170010ED RID: 4333
		// (get) Token: 0x06004108 RID: 16648 RVA: 0x00101740 File Offset: 0x000FF940
		// (set) Token: 0x06004109 RID: 16649 RVA: 0x00101748 File Offset: 0x000FF948
		[RefreshProperties(1)]
		public int AutoPopDelay
		{
			get
			{
				return this.autopop_delay;
			}
			set
			{
				if (this.autopop_delay != value)
				{
					this.autopop_delay = value;
				}
			}
		}

		/// <summary>Gets or sets the background color for the ToolTip.</summary>
		/// <returns>The background <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010EE RID: 4334
		// (get) Token: 0x0600410A RID: 16650 RVA: 0x00101760 File Offset: 0x000FF960
		// (set) Token: 0x0600410B RID: 16651 RVA: 0x00101768 File Offset: 0x000FF968
		[DefaultValue("Color [Info]")]
		public Color BackColor
		{
			get
			{
				return this.back_color;
			}
			set
			{
				this.back_color = value;
				this.tooltip_window.BackColor = value;
			}
		}

		/// <summary>Gets or sets the foreground color for the ToolTip.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010EF RID: 4335
		// (get) Token: 0x0600410C RID: 16652 RVA: 0x00101780 File Offset: 0x000FF980
		// (set) Token: 0x0600410D RID: 16653 RVA: 0x00101788 File Offset: 0x000FF988
		[DefaultValue("Color [InfoText]")]
		public Color ForeColor
		{
			get
			{
				return this.fore_color;
			}
			set
			{
				this.fore_color = value;
				this.tooltip_window.ForeColor = value;
			}
		}

		/// <summary>Gets or sets the time that passes before the ToolTip appears.</summary>
		/// <returns>The period of time, in milliseconds, that the pointer must remain stationary on a control before the ToolTip window is displayed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170010F0 RID: 4336
		// (get) Token: 0x0600410E RID: 16654 RVA: 0x001017A0 File Offset: 0x000FF9A0
		// (set) Token: 0x0600410F RID: 16655 RVA: 0x001017A8 File Offset: 0x000FF9A8
		[RefreshProperties(1)]
		public int InitialDelay
		{
			get
			{
				return this.initial_delay;
			}
			set
			{
				if (this.initial_delay != value)
				{
					this.initial_delay = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the ToolTip is drawn by the operating system or by code that you provide.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolTip" /> is drawn by code that you provide; false if the <see cref="T:System.Windows.Forms.ToolTip" /> is drawn by the operating system. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows" />
		/// </PermissionSet>
		// Token: 0x170010F1 RID: 4337
		// (get) Token: 0x06004110 RID: 16656 RVA: 0x001017C0 File Offset: 0x000FF9C0
		// (set) Token: 0x06004111 RID: 16657 RVA: 0x001017C8 File Offset: 0x000FF9C8
		[DefaultValue(false)]
		public bool OwnerDraw
		{
			get
			{
				return this.owner_draw;
			}
			set
			{
				this.owner_draw = value;
			}
		}

		/// <summary>Gets or sets the length of time that must transpire before subsequent ToolTip windows appear as the pointer moves from one control to another.</summary>
		/// <returns>The length of time, in milliseconds, that it takes subsequent ToolTip windows to appear.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170010F2 RID: 4338
		// (get) Token: 0x06004112 RID: 16658 RVA: 0x001017D4 File Offset: 0x000FF9D4
		// (set) Token: 0x06004113 RID: 16659 RVA: 0x001017DC File Offset: 0x000FF9DC
		[RefreshProperties(1)]
		public int ReshowDelay
		{
			get
			{
				return this.re_show_delay;
			}
			set
			{
				if (this.re_show_delay != value)
				{
					this.re_show_delay = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a ToolTip window is displayed, even when its parent control is not active.</summary>
		/// <returns>true if the ToolTip is always displayed; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06004114 RID: 16660 RVA: 0x001017F4 File Offset: 0x000FF9F4
		// (set) Token: 0x06004115 RID: 16661 RVA: 0x001017FC File Offset: 0x000FF9FC
		[DefaultValue(false)]
		public bool ShowAlways
		{
			get
			{
				return this.show_always;
			}
			set
			{
				if (this.show_always != value)
				{
					this.show_always = value;
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the ToolTip should use a balloon window.</summary>
		/// <returns>true if a balloon window should be used; otherwise, false if a standard rectangular window should be used. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x06004116 RID: 16662 RVA: 0x00101814 File Offset: 0x000FFA14
		// (set) Token: 0x06004117 RID: 16663 RVA: 0x0010181C File Offset: 0x000FFA1C
		[DefaultValue(false)]
		public bool IsBalloon
		{
			get
			{
				return this.isBalloon;
			}
			set
			{
				this.isBalloon = value;
			}
		}

		/// <summary>Gets or sets a value that determines how ampersand (&amp;) characters are treated.</summary>
		/// <returns>true if ampersand characters are stripped from the ToolTip text; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x06004118 RID: 16664 RVA: 0x00101828 File Offset: 0x000FFA28
		// (set) Token: 0x06004119 RID: 16665 RVA: 0x00101830 File Offset: 0x000FFA30
		[DefaultValue(false)]
		[Browsable(true)]
		public bool StripAmpersands
		{
			get
			{
				return this.stripAmpersands;
			}
			set
			{
				this.stripAmpersands = value;
			}
		}

		/// <summary>Gets or sets the object that contains programmer-supplied data associated with the <see cref="T:System.Windows.Forms.ToolTip" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.ToolTip" />. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x0600411A RID: 16666 RVA: 0x0010183C File Offset: 0x000FFA3C
		// (set) Token: 0x0600411B RID: 16667 RVA: 0x00101844 File Offset: 0x000FFA44
		[TypeConverter(typeof(StringConverter))]
		[Localizable(false)]
		[Bindable(true)]
		[DefaultValue(null)]
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		/// <summary>Gets or sets a value that defines the type of icon to be displayed alongside the ToolTip text.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ToolTipIcon" /> enumerated values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x0600411C RID: 16668 RVA: 0x00101850 File Offset: 0x000FFA50
		// (set) Token: 0x0600411D RID: 16669 RVA: 0x00101858 File Offset: 0x000FFA58
		[DefaultValue(ToolTipIcon.None)]
		public ToolTipIcon ToolTipIcon
		{
			get
			{
				return this.tool_tip_icon;
			}
			set
			{
				switch (value)
				{
				case ToolTipIcon.None:
					this.tooltip_window.icon = null;
					break;
				case ToolTipIcon.Info:
					this.tooltip_window.icon = SystemIcons.Information;
					break;
				case ToolTipIcon.Warning:
					this.tooltip_window.icon = SystemIcons.Warning;
					break;
				case ToolTipIcon.Error:
					this.tooltip_window.icon = SystemIcons.Error;
					break;
				}
				this.tool_tip_icon = value;
			}
		}

		/// <summary>Gets or sets a title for the ToolTip window.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the window title.</returns>
		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x0600411E RID: 16670 RVA: 0x001018DC File Offset: 0x000FFADC
		// (set) Token: 0x0600411F RID: 16671 RVA: 0x001018EC File Offset: 0x000FFAEC
		[DefaultValue("")]
		public string ToolTipTitle
		{
			get
			{
				return this.tooltip_window.title;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.tooltip_window.title = value;
			}
		}

		/// <summary>Gets or sets a value determining whether an animation effect should be used when displaying the ToolTip.</summary>
		/// <returns>true if window animation should be used; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x06004120 RID: 16672 RVA: 0x00101908 File Offset: 0x000FFB08
		// (set) Token: 0x06004121 RID: 16673 RVA: 0x00101910 File Offset: 0x000FFB10
		[Browsable(true)]
		[DefaultValue(true)]
		public bool UseAnimation
		{
			get
			{
				return this.useAnimation;
			}
			set
			{
				this.useAnimation = value;
			}
		}

		/// <summary>Gets or sets a value determining whether a fade effect should be used when displaying the ToolTip.</summary>
		/// <returns>true if window fading should be used; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x06004122 RID: 16674 RVA: 0x0010191C File Offset: 0x000FFB1C
		// (set) Token: 0x06004123 RID: 16675 RVA: 0x00101924 File Offset: 0x000FFB24
		[Browsable(true)]
		[DefaultValue(true)]
		public bool UseFading
		{
			get
			{
				return this.useFading;
			}
			set
			{
				this.useFading = value;
			}
		}

		/// <summary>Gets the creation parameters for the ToolTip window.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> containing the information needed to create the ToolTip.</returns>
		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x06004124 RID: 16676 RVA: 0x00101930 File Offset: 0x000FFB30
		protected virtual CreateParams CreateParams
		{
			get
			{
				return new CreateParams
				{
					Style = 2
				};
			}
		}

		/// <summary>Returns true if the ToolTip can offer an extender property to the specified target component.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ToolTip" /> class can offer one or more extender properties; otherwise, false.</returns>
		/// <param name="target">The target object to add an extender property to. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004125 RID: 16677 RVA: 0x0010194C File Offset: 0x000FFB4C
		public bool CanExtend(object target)
		{
			return false;
		}

		/// <summary>Retrieves the ToolTip text associated with the specified control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the ToolTip text for the specified control.</returns>
		/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> for which to retrieve the <see cref="T:System.Windows.Forms.ToolTip" /> text. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06004126 RID: 16678 RVA: 0x00101950 File Offset: 0x000FFB50
		[Localizable(true)]
		[DefaultValue("")]
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string GetToolTip(Control control)
		{
			string text = (string)this.tooltip_strings[control];
			if (text == null)
			{
				return string.Empty;
			}
			return text;
		}

		/// <summary>Removes all ToolTip text currently associated with the ToolTip component.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004127 RID: 16679 RVA: 0x0010197C File Offset: 0x000FFB7C
		public void RemoveAll()
		{
			this.tooltip_strings.Clear();
			foreach (object obj in this.controls)
			{
				Control control = (Control)obj;
				ToolTip.OnUIAToolTipUnhookUp(this, new ControlEventArgs(control));
			}
			this.controls.Clear();
		}

		/// <summary>Associates ToolTip text with the specified control.</summary>
		/// <param name="control">The <see cref="T:System.Windows.Forms.Control" /> to associate the ToolTip text with. </param>
		/// <param name="caption">The ToolTip text to display when the pointer is on the control. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004128 RID: 16680 RVA: 0x00101A08 File Offset: 0x000FFC08
		public void SetToolTip(Control control, string caption)
		{
			ToolTip.OnUIAToolTipHookUp(this, new ControlEventArgs(control));
			this.tooltip_strings[control] = caption;
			if (!this.controls.Contains(control))
			{
				control.MouseEnter += new EventHandler(this.control_MouseEnter);
				control.MouseMove += this.control_MouseMove;
				control.MouseLeave += new EventHandler(this.control_MouseLeave);
				control.MouseDown += this.control_MouseDown;
				this.controls.Add(control);
			}
			if (this.active_control == control && caption != null && this.state == ToolTip.TipState.Show)
			{
				Size size = ThemeEngine.Current.ToolTipSize(this.tooltip_window, caption);
				this.tooltip_window.Width = size.Width;
				this.tooltip_window.Height = size.Height;
				this.tooltip_window.Text = caption;
				this.timer.Stop();
				this.timer.Start();
			}
			else if (control.IsHandleCreated && this.MouseInControl(control, false))
			{
				this.ShowTooltip(control);
			}
		}

		/// <summary>Returns a string representation for this control.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a description of the <see cref="T:System.Windows.Forms.ToolTip" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06004129 RID: 16681 RVA: 0x00101B30 File Offset: 0x000FFD30
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.ToString(),
				" InitialDelay: ",
				this.initial_delay,
				", ShowAlways: ",
				this.show_always
			});
		}

		/// <summary>Sets the ToolTip text associated with the specified control, and displays the ToolTip modally.</summary>
		/// <param name="text">A <see cref="T:System.String" /> containing the new ToolTip text. </param>
		/// <param name="window">The <see cref="T:System.Windows.Forms.Control" /> to display the ToolTip for.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="window" /> parameter is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600412A RID: 16682 RVA: 0x00101B80 File Offset: 0x000FFD80
		public void Show(string text, IWin32Window window)
		{
			this.Show(text, window, 0);
		}

		/// <summary>Sets the ToolTip text associated with the specified control, and then displays the ToolTip for the specified duration.</summary>
		/// <param name="text">A <see cref="T:System.String" /> containing the new ToolTip text. </param>
		/// <param name="window">The <see cref="T:System.Windows.Forms.Control" /> to display the ToolTip for.</param>
		/// <param name="duration">An <see cref="T:System.Int32" /> containing the duration, in milliseconds, to display the ToolTip.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="window" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="duration" /> is less than or equal to 0.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600412B RID: 16683 RVA: 0x00101B8C File Offset: 0x000FFD8C
		public void Show(string text, IWin32Window window, int duration)
		{
			if (window == null)
			{
				throw new ArgumentNullException("window");
			}
			if (duration < 0)
			{
				throw new ArgumentOutOfRangeException("duration", "duration cannot be less than zero");
			}
			if (!this.Active)
			{
				return;
			}
			this.timer.Stop();
			Control control = (Control)window;
			XplatUI.SetOwner(this.tooltip_window.Handle, control.TopLevelControl.Handle);
			if (control.ClientRectangle.Contains(control.PointToClient(Control.MousePosition)))
			{
				this.tooltip_window.Location = Control.MousePosition;
				this.tooltip_strings[control] = text;
				this.HookupControlEvents(control);
			}
			else
			{
				this.tooltip_window.Location = control.PointToScreen(new Point(control.Width / 2, control.Height / 2));
			}
			this.HookupFormEvents((Form)control.TopLevelControl);
			this.tooltip_window.PresentModal((Control)window, text);
			this.state = ToolTip.TipState.Show;
			if (duration > 0)
			{
				this.timer.Interval = duration;
				this.timer.Start();
			}
		}

		/// <summary>Sets the ToolTip text associated with the specified control, and then displays the ToolTip modally at the specified relative position.</summary>
		/// <param name="text">A <see cref="T:System.String" /> containing the new ToolTip text. </param>
		/// <param name="window">The <see cref="T:System.Windows.Forms.Control" /> to display the ToolTip for.</param>
		/// <param name="point">A <see cref="T:System.Drawing.Point" /> containing the offset, in pixels, relative to the upper-left corner of the associated control window, to display the ToolTip.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="window" /> parameter is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600412C RID: 16684 RVA: 0x00101CB0 File Offset: 0x000FFEB0
		public void Show(string text, IWin32Window window, Point point)
		{
			this.Show(text, window, point, 0);
		}

		/// <summary>Sets the ToolTip text associated with the specified control, and then displays the ToolTip modally at the specified relative position.</summary>
		/// <param name="text">A <see cref="T:System.String" /> containing the new ToolTip text. </param>
		/// <param name="window">The <see cref="T:System.Windows.Forms.Control" /> to display the ToolTip for.</param>
		/// <param name="x">The horizontal offset, in pixels, relative to the upper-left corner of the associated control window, to display the ToolTip.</param>
		/// <param name="y">The vertical offset, in pixels, relative to the upper-left corner of the associated control window, to display the ToolTip.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600412D RID: 16685 RVA: 0x00101CBC File Offset: 0x000FFEBC
		public void Show(string text, IWin32Window window, int x, int y)
		{
			this.Show(text, window, new Point(x, y), 0);
		}

		/// <summary>Sets the ToolTip text associated with the specified control, and then displays the ToolTip for the specified duration at the specified relative position.</summary>
		/// <param name="text">A <see cref="T:System.String" /> containing the new ToolTip text. </param>
		/// <param name="window">The <see cref="T:System.Windows.Forms.Control" /> to display the ToolTip for.</param>
		/// <param name="point">A <see cref="T:System.Drawing.Point" /> containing the offset, in pixels, relative to the upper-left corner of the associated control window, to display the ToolTip.</param>
		/// <param name="duration">An <see cref="T:System.Int32" /> containing the duration, in milliseconds, to display the ToolTip.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="window" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="duration" /> is less than or equal to 0.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600412E RID: 16686 RVA: 0x00101CD0 File Offset: 0x000FFED0
		public void Show(string text, IWin32Window window, Point point, int duration)
		{
			if (window == null)
			{
				throw new ArgumentNullException("window");
			}
			if (duration < 0)
			{
				throw new ArgumentOutOfRangeException("duration", "duration cannot be less than zero");
			}
			if (!this.Active)
			{
				return;
			}
			this.timer.Stop();
			Control control = (Control)window;
			Point point2 = control.PointToScreen(Point.Empty);
			point2.X += point.X;
			point2.Y += point.Y;
			XplatUI.SetOwner(this.tooltip_window.Handle, control.TopLevelControl.Handle);
			this.HookupFormEvents((Form)control.TopLevelControl);
			this.tooltip_window.Location = point2;
			this.tooltip_window.PresentModal((Control)window, text);
			this.state = ToolTip.TipState.Show;
			if (duration > 0)
			{
				this.timer.Interval = duration;
				this.timer.Start();
			}
		}

		/// <summary>Sets the ToolTip text associated with the specified control, and then displays the ToolTip for the specified duration at the specified relative position.</summary>
		/// <param name="text">A <see cref="T:System.String" /> containing the new ToolTip text. </param>
		/// <param name="window">The <see cref="T:System.Windows.Forms.Control" /> to display the ToolTip for.</param>
		/// <param name="x">The horizontal offset, in pixels, relative to the upper-left corner of the associated control window, to display the ToolTip.</param>
		/// <param name="y">The vertical offset, in pixels, relative to the upper-left corner of the associated control window, to display the ToolTip.</param>
		/// <param name="duration">An <see cref="T:System.Int32" /> containing the duration, in milliseconds, to display the ToolTip.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="window" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="duration" /> is less than or equal to 0.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600412F RID: 16687 RVA: 0x00101DCC File Offset: 0x000FFFCC
		public void Show(string text, IWin32Window window, int x, int y, int duration)
		{
			this.Show(text, window, new Point(x, y), duration);
		}

		/// <summary>Hides the specified ToolTip window.</summary>
		/// <param name="win">The <see cref="T:System.Windows.Forms.IWin32Window" /> of the associated window or control that the ToolTip is associated with.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="win" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06004130 RID: 16688 RVA: 0x00101DE0 File Offset: 0x000FFFE0
		public void Hide(IWin32Window win)
		{
			this.timer.Stop();
			this.state = ToolTip.TipState.Initial;
			this.UnhookFormEvents();
			this.tooltip_window.Visible = false;
		}

		/// <summary>Disposes of the <see cref="T:System.Windows.Forms.ToolTip" /> component.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06004131 RID: 16689 RVA: 0x00101E14 File Offset: 0x00100014
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				this.timer.Stop();
				this.timer.Dispose();
				this.tooltip_window.Dispose();
				this.tooltip_strings.Clear();
				foreach (object obj in this.controls)
				{
					Control control = (Control)obj;
					ToolTip.OnUIAToolTipUnhookUp(this, new ControlEventArgs(control));
				}
				this.controls.Clear();
			}
		}

		/// <summary>Stops the timer that hides displayed ToolTips.</summary>
		// Token: 0x06004132 RID: 16690 RVA: 0x00101ECC File Offset: 0x001000CC
		protected void StopTimer()
		{
			this.timer.Stop();
		}

		// Token: 0x06004133 RID: 16691 RVA: 0x00101EDC File Offset: 0x001000DC
		private void HookupFormEvents(Form form)
		{
			this.hooked_form = form;
			form.Deactivate += new EventHandler(this.Form_Deactivate);
			form.Closed += new EventHandler(this.Form_Closed);
			form.Resize += new EventHandler(this.Form_Resize);
		}

		// Token: 0x06004134 RID: 16692 RVA: 0x00101F1C File Offset: 0x0010011C
		private void HookupControlEvents(Control control)
		{
			if (!this.controls.Contains(control))
			{
				control.MouseEnter += new EventHandler(this.control_MouseEnter);
				control.MouseMove += this.control_MouseMove;
				control.MouseLeave += new EventHandler(this.control_MouseLeave);
				control.MouseDown += this.control_MouseDown;
				this.controls.Add(control);
			}
		}

		// Token: 0x06004135 RID: 16693 RVA: 0x00101F90 File Offset: 0x00100190
		private void UnhookControlEvents(Control control)
		{
			control.MouseEnter -= new EventHandler(this.control_MouseEnter);
			control.MouseMove -= this.control_MouseMove;
			control.MouseLeave -= new EventHandler(this.control_MouseLeave);
			control.MouseDown -= this.control_MouseDown;
		}

		// Token: 0x06004136 RID: 16694 RVA: 0x00101FE8 File Offset: 0x001001E8
		private void UnhookFormEvents()
		{
			if (this.hooked_form == null)
			{
				return;
			}
			this.hooked_form.Deactivate -= new EventHandler(this.Form_Deactivate);
			this.hooked_form.Closed -= new EventHandler(this.Form_Closed);
			this.hooked_form.Resize -= new EventHandler(this.Form_Resize);
			this.hooked_form = null;
		}

		// Token: 0x06004137 RID: 16695 RVA: 0x00102050 File Offset: 0x00100250
		private void Form_Resize(object sender, EventArgs e)
		{
			Form form = (Form)sender;
			if (form.WindowState == FormWindowState.Minimized)
			{
				this.tooltip_window.Visible = false;
			}
		}

		// Token: 0x06004138 RID: 16696 RVA: 0x0010207C File Offset: 0x0010027C
		private void Form_Closed(object sender, EventArgs e)
		{
			this.tooltip_window.Visible = false;
		}

		// Token: 0x06004139 RID: 16697 RVA: 0x0010208C File Offset: 0x0010028C
		private void Form_Deactivate(object sender, EventArgs e)
		{
			this.tooltip_window.Visible = false;
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x0010209C File Offset: 0x0010029C
		internal void Present(Control control, string text)
		{
			this.tooltip_window.Present(control, text);
		}

		// Token: 0x0600413B RID: 16699 RVA: 0x001020AC File Offset: 0x001002AC
		private void control_MouseEnter(object sender, EventArgs e)
		{
			this.ShowTooltip(sender as Control);
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x001020BC File Offset: 0x001002BC
		private void ShowTooltip(Control control)
		{
			this.last_control = control;
			this.tooltip_window.Visible = false;
			this.timer.Stop();
			this.state = ToolTip.TipState.Initial;
			if (!this.is_active)
			{
				return;
			}
			if (!this.show_always && control.FindForm() != Form.ActiveForm)
			{
				return;
			}
			string text = (string)this.tooltip_strings[control];
			if (text != null && text.Length > 0)
			{
				if (this.active_control == null)
				{
					this.timer.Interval = Math.Max(this.initial_delay, 1);
				}
				else
				{
					this.timer.Interval = Math.Max(this.re_show_delay, 1);
				}
				this.active_control = control;
				this.timer.Start();
			}
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x0010218C File Offset: 0x0010038C
		private void timer_Tick(object sender, EventArgs e)
		{
			this.timer.Stop();
			ToolTip.TipState tipState = this.state;
			if (tipState != ToolTip.TipState.Initial)
			{
				if (tipState != ToolTip.TipState.Show)
				{
					throw new Exception("Timer shouldn't be running in state: " + this.state);
				}
				this.tooltip_window.Visible = false;
				this.state = ToolTip.TipState.Down;
			}
			else
			{
				if (this.active_control == null)
				{
					return;
				}
				this.tooltip_window.Present(this.active_control, (string)this.tooltip_strings[this.active_control]);
				this.state = ToolTip.TipState.Show;
				this.timer.Interval = this.autopop_delay;
				this.timer.Start();
			}
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x0010224C File Offset: 0x0010044C
		private void tooltip_window_Popup(object sender, PopupEventArgs e)
		{
			e.ToolTipSize = ThemeEngine.Current.ToolTipSize(this.tooltip_window, this.tooltip_window.Text);
			this.OnPopup(e);
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x00102284 File Offset: 0x00100484
		private void tooltip_window_Draw(object sender, DrawToolTipEventArgs e)
		{
			if (this.OwnerDraw)
			{
				this.OnDraw(e);
			}
			else
			{
				ThemeEngine.Current.DrawToolTip(e.Graphics, e.Bounds, this.tooltip_window);
			}
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x001022C4 File Offset: 0x001004C4
		private bool MouseInControl(Control control, bool fuzzy)
		{
			if (control == null)
			{
				return false;
			}
			Point mousePosition = Control.MousePosition;
			Point point;
			point..ctor(control.Bounds.X, control.Bounds.Y);
			if (control.Parent != null)
			{
				point = control.Parent.PointToScreen(point);
			}
			Size clientSize = control.ClientSize;
			Rectangle rectangle;
			rectangle..ctor(point, clientSize);
			if (fuzzy)
			{
				rectangle.Inflate(2, 2);
			}
			return rectangle.Contains(mousePosition);
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x00102344 File Offset: 0x00100544
		private void control_MouseLeave(object sender, EventArgs e)
		{
			this.timer.Stop();
			this.active_control = null;
			this.tooltip_window.Visible = false;
			if (this.last_control == sender)
			{
				this.last_control = null;
			}
		}

		// Token: 0x06004142 RID: 16706 RVA: 0x00102378 File Offset: 0x00100578
		private void control_MouseDown(object sender, MouseEventArgs e)
		{
			this.timer.Stop();
			this.active_control = null;
			this.tooltip_window.Visible = false;
			if (this.last_control == sender)
			{
				this.last_control = null;
			}
		}

		// Token: 0x06004143 RID: 16707 RVA: 0x001023AC File Offset: 0x001005AC
		private void control_MouseMove(object sender, MouseEventArgs e)
		{
			if (this.state != ToolTip.TipState.Down)
			{
				this.timer.Stop();
				this.timer.Start();
			}
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x001023DC File Offset: 0x001005DC
		internal void OnDraw(DrawToolTipEventArgs e)
		{
			DrawToolTipEventHandler drawToolTipEventHandler = (DrawToolTipEventHandler)base.Events[ToolTip.DrawEvent];
			if (drawToolTipEventHandler != null)
			{
				drawToolTipEventHandler(this, e);
			}
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x00102410 File Offset: 0x00100610
		internal void OnPopup(PopupEventArgs e)
		{
			PopupEventHandler popupEventHandler = (PopupEventHandler)base.Events[ToolTip.PopupEvent];
			if (popupEventHandler != null)
			{
				popupEventHandler(this, e);
			}
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x00102444 File Offset: 0x00100644
		internal void OnUnPopup(PopupEventArgs e)
		{
			PopupEventHandler popupEventHandler = (PopupEventHandler)base.Events[ToolTip.UnPopupEvent];
			if (popupEventHandler != null)
			{
				popupEventHandler(this, e);
			}
		}

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x06004147 RID: 16711 RVA: 0x00102478 File Offset: 0x00100678
		internal bool Visible
		{
			get
			{
				return this.tooltip_window.Visible;
			}
		}

		// Token: 0x04001B86 RID: 7046
		internal bool is_active;

		// Token: 0x04001B87 RID: 7047
		internal int automatic_delay;

		// Token: 0x04001B88 RID: 7048
		internal int autopop_delay;

		// Token: 0x04001B89 RID: 7049
		internal int initial_delay;

		// Token: 0x04001B8A RID: 7050
		internal int re_show_delay;

		// Token: 0x04001B8B RID: 7051
		internal bool show_always;

		// Token: 0x04001B8C RID: 7052
		internal Color back_color;

		// Token: 0x04001B8D RID: 7053
		internal Color fore_color;

		// Token: 0x04001B8E RID: 7054
		internal ToolTip.ToolTipWindow tooltip_window;

		// Token: 0x04001B8F RID: 7055
		internal Hashtable tooltip_strings;

		// Token: 0x04001B90 RID: 7056
		internal ArrayList controls;

		// Token: 0x04001B91 RID: 7057
		internal Control active_control;

		// Token: 0x04001B92 RID: 7058
		internal Control last_control;

		// Token: 0x04001B93 RID: 7059
		internal Timer timer;

		// Token: 0x04001B94 RID: 7060
		private Form hooked_form;

		// Token: 0x04001B95 RID: 7061
		private bool isBalloon;

		// Token: 0x04001B96 RID: 7062
		private bool owner_draw;

		// Token: 0x04001B97 RID: 7063
		private bool stripAmpersands;

		// Token: 0x04001B98 RID: 7064
		private ToolTipIcon tool_tip_icon;

		// Token: 0x04001B99 RID: 7065
		private bool useAnimation;

		// Token: 0x04001B9A RID: 7066
		private bool useFading;

		// Token: 0x04001B9B RID: 7067
		private object tag;

		// Token: 0x04001B9D RID: 7069
		private ToolTip.TipState state;

		// Token: 0x02000384 RID: 900
		internal class ToolTipWindow : Control
		{
			// Token: 0x06004149 RID: 16713 RVA: 0x00102494 File Offset: 0x00100694
			internal ToolTipWindow()
			{
				base.Visible = false;
				base.Size = new Size(100, 20);
				this.ForeColor = ThemeEngine.Current.ColorInfoText;
				this.BackColor = ThemeEngine.Current.ColorInfo;
				base.VisibleChanged += new EventHandler(this.ToolTipWindow_VisibleChanged);
				base.VisibleChanged += new EventHandler(this.OnUIAToolTip_VisibleChanged);
				base.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
				base.SetStyle(ControlStyles.ResizeRedraw, true);
				if (ThemeEngine.Current.ToolTipTransparentBackground)
				{
					base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
					this.BackColor = Color.Transparent;
				}
				else
				{
					base.SetStyle(ControlStyles.Opaque, true);
				}
			}

			// Token: 0x0600414A RID: 16714 RVA: 0x00102554 File Offset: 0x00100754
			// Note: this type is marked as 'beforefieldinit'.
			static ToolTipWindow()
			{
				ToolTip.ToolTipWindow.DrawEvent = new object();
				ToolTip.ToolTipWindow.PopupEvent = new object();
				ToolTip.ToolTipWindow.UnPopupEvent = new object();
			}

			// Token: 0x140003FF RID: 1023
			// (add) Token: 0x0600414B RID: 16715 RVA: 0x00102574 File Offset: 0x00100774
			// (remove) Token: 0x0600414C RID: 16716 RVA: 0x00102588 File Offset: 0x00100788
			public event DrawToolTipEventHandler Draw
			{
				add
				{
					base.Events.AddHandler(ToolTip.ToolTipWindow.DrawEvent, value);
				}
				remove
				{
					base.Events.RemoveHandler(ToolTip.ToolTipWindow.DrawEvent, value);
				}
			}

			// Token: 0x14000400 RID: 1024
			// (add) Token: 0x0600414D RID: 16717 RVA: 0x0010259C File Offset: 0x0010079C
			// (remove) Token: 0x0600414E RID: 16718 RVA: 0x001025B0 File Offset: 0x001007B0
			public event PopupEventHandler Popup
			{
				add
				{
					base.Events.AddHandler(ToolTip.ToolTipWindow.PopupEvent, value);
				}
				remove
				{
					base.Events.RemoveHandler(ToolTip.ToolTipWindow.PopupEvent, value);
				}
			}

			// Token: 0x14000401 RID: 1025
			// (add) Token: 0x0600414F RID: 16719 RVA: 0x001025C4 File Offset: 0x001007C4
			// (remove) Token: 0x06004150 RID: 16720 RVA: 0x001025D8 File Offset: 0x001007D8
			internal event PopupEventHandler UnPopup
			{
				add
				{
					base.Events.AddHandler(ToolTip.ToolTipWindow.UnPopupEvent, value);
				}
				remove
				{
					base.Events.RemoveHandler(ToolTip.ToolTipWindow.UnPopupEvent, value);
				}
			}

			// Token: 0x06004151 RID: 16721 RVA: 0x001025EC File Offset: 0x001007EC
			protected override void OnCreateControl()
			{
				base.OnCreateControl();
				XplatUI.SetTopmost(this.window.Handle, true);
			}

			// Token: 0x170010FD RID: 4349
			// (get) Token: 0x06004152 RID: 16722 RVA: 0x00102608 File Offset: 0x00100808
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.Style = int.MinValue;
					createParams.Style |= 67108864;
					createParams.ExStyle = 136;
					return createParams;
				}
			}

			// Token: 0x06004153 RID: 16723 RVA: 0x00102648 File Offset: 0x00100848
			protected override void OnPaint(PaintEventArgs pevent)
			{
				base.OnPaint(pevent);
				this.OnDraw(new DrawToolTipEventArgs(pevent.Graphics, this.associated_control, this.associated_control, base.ClientRectangle, this.Text, this.BackColor, this.ForeColor, this.Font));
			}

			// Token: 0x06004154 RID: 16724 RVA: 0x00102698 File Offset: 0x00100898
			protected override void OnTextChanged(EventArgs args)
			{
				base.Invalidate();
				base.OnTextChanged(args);
			}

			// Token: 0x06004155 RID: 16725 RVA: 0x001026A8 File Offset: 0x001008A8
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 7 && m.WParam != IntPtr.Zero)
				{
					XplatUI.SetFocus(m.WParam);
				}
				base.WndProc(ref m);
			}

			// Token: 0x06004156 RID: 16726 RVA: 0x001026E8 File Offset: 0x001008E8
			internal virtual void OnDraw(DrawToolTipEventArgs e)
			{
				DrawToolTipEventHandler drawToolTipEventHandler = (DrawToolTipEventHandler)base.Events[ToolTip.ToolTipWindow.DrawEvent];
				if (drawToolTipEventHandler != null)
				{
					drawToolTipEventHandler(this, e);
				}
				else
				{
					ThemeEngine.Current.DrawToolTip(e.Graphics, e.Bounds, this);
				}
			}

			// Token: 0x06004157 RID: 16727 RVA: 0x00102738 File Offset: 0x00100938
			internal virtual void OnPopup(PopupEventArgs e)
			{
				PopupEventHandler popupEventHandler = (PopupEventHandler)base.Events[ToolTip.ToolTipWindow.PopupEvent];
				if (popupEventHandler != null)
				{
					popupEventHandler(this, e);
				}
				else
				{
					e.ToolTipSize = ThemeEngine.Current.ToolTipSize(this, this.Text);
				}
			}

			// Token: 0x06004158 RID: 16728 RVA: 0x00102788 File Offset: 0x00100988
			private void ToolTipWindow_VisibleChanged(object sender, EventArgs e)
			{
				Control control = (Control)sender;
				if (control.is_visible)
				{
					XplatUI.SetTopmost(control.window.Handle, true);
				}
				else
				{
					XplatUI.SetTopmost(control.window.Handle, false);
				}
			}

			// Token: 0x06004159 RID: 16729 RVA: 0x001027D0 File Offset: 0x001009D0
			private void OnUIAToolTip_VisibleChanged(object sender, EventArgs e)
			{
				if (!base.Visible)
				{
					this.OnUnPopup(new PopupEventArgs(this.associated_control, this.associated_control, false, Size.Empty));
				}
			}

			// Token: 0x0600415A RID: 16730 RVA: 0x00102808 File Offset: 0x00100A08
			private void OnUnPopup(PopupEventArgs e)
			{
				PopupEventHandler popupEventHandler = (PopupEventHandler)base.Events[ToolTip.ToolTipWindow.UnPopupEvent];
				if (popupEventHandler != null)
				{
					popupEventHandler(this, e);
				}
			}

			// Token: 0x170010FE RID: 4350
			// (get) Token: 0x0600415B RID: 16731 RVA: 0x0010283C File Offset: 0x00100A3C
			internal override bool ActivateOnShow
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600415C RID: 16732 RVA: 0x00102840 File Offset: 0x00100A40
			public void PresentModal(Control control, string text)
			{
				if (base.IsDisposed)
				{
					return;
				}
				Size size;
				XplatUI.GetDisplaySize(out size);
				this.associated_control = control;
				this.Text = text;
				PopupEventArgs popupEventArgs = new PopupEventArgs(control, control, false, Size.Empty);
				this.OnPopup(popupEventArgs);
				if (popupEventArgs.Cancel)
				{
					return;
				}
				base.Size = popupEventArgs.ToolTipSize;
				base.Visible = true;
			}

			// Token: 0x0600415D RID: 16733 RVA: 0x001028A4 File Offset: 0x00100AA4
			public void Present(Control control, string text)
			{
				if (base.IsDisposed)
				{
					return;
				}
				Size size;
				XplatUI.GetDisplaySize(out size);
				this.associated_control = control;
				this.Text = text;
				PopupEventArgs popupEventArgs = new PopupEventArgs(control, control, false, Size.Empty);
				this.OnPopup(popupEventArgs);
				if (popupEventArgs.Cancel)
				{
					return;
				}
				Size toolTipSize = popupEventArgs.ToolTipSize;
				base.Width = toolTipSize.Width;
				base.Height = toolTipSize.Height;
				int num;
				int num2;
				int num3;
				int num4;
				XplatUI.GetCursorInfo(control.Cursor.Handle, out num, out num2, out num3, out num4);
				Point mousePosition = Control.MousePosition;
				mousePosition.Y += num2 - num4;
				if (mousePosition.X + base.Width > size.Width)
				{
					mousePosition.X = size.Width - base.Width;
				}
				if (mousePosition.Y + base.Height > size.Height)
				{
					mousePosition.Y = Control.MousePosition.Y - base.Height - num4;
				}
				base.Location = mousePosition;
				base.Visible = true;
			}

			// Token: 0x04001BA3 RID: 7075
			private Control associated_control;

			// Token: 0x04001BA4 RID: 7076
			internal Icon icon;

			// Token: 0x04001BA5 RID: 7077
			internal string title = string.Empty;

			// Token: 0x04001BA6 RID: 7078
			internal Rectangle icon_rect;

			// Token: 0x04001BA7 RID: 7079
			internal Rectangle title_rect;

			// Token: 0x04001BA8 RID: 7080
			internal Rectangle text_rect;
		}

		// Token: 0x02000385 RID: 901
		private enum TipState
		{
			// Token: 0x04001BAD RID: 7085
			Initial,
			// Token: 0x04001BAE RID: 7086
			Show,
			// Token: 0x04001BAF RID: 7087
			Down
		}
	}
}
