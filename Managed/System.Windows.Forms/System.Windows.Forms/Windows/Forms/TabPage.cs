using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a single tab page in a <see cref="T:System.Windows.Forms.TabControl" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002FD RID: 765
	[Designer("System.Windows.Forms.Design.TabPageDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ComVisible(true)]
	[ToolboxItem(false)]
	[ClassInterface(1)]
	[DefaultEvent("Click")]
	[DesignTimeVisible(false)]
	[DefaultProperty("Text")]
	public class TabPage : Panel
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabPage" /> class.</summary>
		// Token: 0x06003317 RID: 13079 RVA: 0x000C2270 File Offset: 0x000C0470
		public TabPage()
		{
			this.Visible = true;
			base.SetStyle(ControlStyles.CacheText, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabPage" /> class and specifies the text for the tab.</summary>
		/// <param name="text">The text for the tab. </param>
		// Token: 0x06003318 RID: 13080 RVA: 0x000C22A0 File Offset: 0x000C04A0
		public TabPage(string text)
		{
			base.Text = text;
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.AutoSize" /> property changes.</summary>
		// Token: 0x14000324 RID: 804
		// (add) Token: 0x06003319 RID: 13081 RVA: 0x000C22C4 File Offset: 0x000C04C4
		// (remove) Token: 0x0600331A RID: 13082 RVA: 0x000C22D0 File Offset: 0x000C04D0
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.Dock" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000325 RID: 805
		// (add) Token: 0x0600331B RID: 13083 RVA: 0x000C22DC File Offset: 0x000C04DC
		// (remove) Token: 0x0600331C RID: 13084 RVA: 0x000C22E8 File Offset: 0x000C04E8
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.Enabled" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000326 RID: 806
		// (add) Token: 0x0600331D RID: 13085 RVA: 0x000C22F4 File Offset: 0x000C04F4
		// (remove) Token: 0x0600331E RID: 13086 RVA: 0x000C2300 File Offset: 0x000C0500
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler EnabledChanged
		{
			add
			{
				base.EnabledChanged += value;
			}
			remove
			{
				base.EnabledChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.Location" /> property changes.</summary>
		// Token: 0x14000327 RID: 807
		// (add) Token: 0x0600331F RID: 13087 RVA: 0x000C230C File Offset: 0x000C050C
		// (remove) Token: 0x06003320 RID: 13088 RVA: 0x000C2318 File Offset: 0x000C0518
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.TabIndex" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000328 RID: 808
		// (add) Token: 0x06003321 RID: 13089 RVA: 0x000C2324 File Offset: 0x000C0524
		// (remove) Token: 0x06003322 RID: 13090 RVA: 0x000C2330 File Offset: 0x000C0530
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.TabStop" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000329 RID: 809
		// (add) Token: 0x06003323 RID: 13091 RVA: 0x000C233C File Offset: 0x000C053C
		// (remove) Token: 0x06003324 RID: 13092 RVA: 0x000C2348 File Offset: 0x000C0548
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

		// Token: 0x1400032A RID: 810
		// (add) Token: 0x06003325 RID: 13093 RVA: 0x000C2354 File Offset: 0x000C0554
		// (remove) Token: 0x06003326 RID: 13094 RVA: 0x000C2360 File Offset: 0x000C0560
		[Browsable(true)]
		[EditorBrowsable(0)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabPage.Visible" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400032B RID: 811
		// (add) Token: 0x06003327 RID: 13095 RVA: 0x000C236C File Offset: 0x000C056C
		// (remove) Token: 0x06003328 RID: 13096 RVA: 0x000C2378 File Offset: 0x000C0578
		[Browsable(false)]
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

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>The default value is false.</returns>
		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06003329 RID: 13097 RVA: 0x000C2384 File Offset: 0x000C0584
		// (set) Token: 0x0600332A RID: 13098 RVA: 0x000C238C File Offset: 0x000C058C
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>Always <see cref="F:System.Windows.Forms.AutoSizeMode.GrowOnly" />.</returns>
		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x0600332B RID: 13099 RVA: 0x000C2398 File Offset: 0x000C0598
		// (set) Token: 0x0600332C RID: 13100 RVA: 0x000C23A0 File Offset: 0x000C05A0
		[Browsable(false)]
		[EditorBrowsable(1)]
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
				base.AutoSizeMode = value;
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x0600332D RID: 13101 RVA: 0x000C23AC File Offset: 0x000C05AC
		// (set) Token: 0x0600332E RID: 13102 RVA: 0x000C23B4 File Offset: 0x000C05B4
		[Browsable(false)]
		[EditorBrowsable(1)]
		[DefaultValue("{Width=0, Height=0}")]
		public override Size MaximumSize
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

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x0600332F RID: 13103 RVA: 0x000C23C0 File Offset: 0x000C05C0
		// (set) Token: 0x06003330 RID: 13104 RVA: 0x000C23C8 File Offset: 0x000C05C8
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override Size MinimumSize
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

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" />.</returns>
		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06003331 RID: 13105 RVA: 0x000C23D4 File Offset: 0x000C05D4
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new Size PreferredSize
		{
			get
			{
				return base.PreferredSize;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.TabPage" /> background renders using the current visual style when visual styles are enabled.</summary>
		/// <returns>true to render the background using the current visual style; otherwise, false. The default is false.</returns>
		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x000C23DC File Offset: 0x000C05DC
		// (set) Token: 0x06003333 RID: 13107 RVA: 0x000C23E4 File Offset: 0x000C05E4
		[DefaultValue(false)]
		public bool UseVisualStyleBackColor
		{
			get
			{
				return this.use_visual_style_back_color;
			}
			set
			{
				this.use_visual_style_back_color = value;
			}
		}

		/// <summary>Gets or sets the background color for the <see cref="T:System.Windows.Forms.TabPage" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the <see cref="T:System.Windows.Forms.TabPage" />. </returns>
		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06003334 RID: 13108 RVA: 0x000C23F0 File Offset: 0x000C05F0
		// (set) Token: 0x06003335 RID: 13109 RVA: 0x000C23F8 File Offset: 0x000C05F8
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				this.use_visual_style_back_color = false;
				base.BackColor = value;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.AnchorStyles" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x000C2408 File Offset: 0x000C0608
		// (set) Token: 0x06003337 RID: 13111 RVA: 0x000C2410 File Offset: 0x000C0610
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override AnchorStyles Anchor
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

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DockStyle" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06003338 RID: 13112 RVA: 0x000C241C File Offset: 0x000C061C
		// (set) Token: 0x06003339 RID: 13113 RVA: 0x000C2424 File Offset: 0x000C0624
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override DockStyle Dock
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

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x0600333A RID: 13114 RVA: 0x000C2430 File Offset: 0x000C0630
		// (set) Token: 0x0600333B RID: 13115 RVA: 0x000C2438 File Offset: 0x000C0638
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		/// <summary>Gets or sets the index to the image displayed on this tab.</summary>
		/// <returns>The zero-based index to the image in the <see cref="P:System.Windows.Forms.TabControl.ImageList" /> that appears on the tab. The default is -1, which signifies no image.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Windows.Forms.TabPage.ImageIndex" /> value is less than -1. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x0600333C RID: 13116 RVA: 0x000C2444 File Offset: 0x000C0644
		// (set) Token: 0x0600333D RID: 13117 RVA: 0x000C244C File Offset: 0x000C064C
		[RefreshProperties(2)]
		[TypeConverter(typeof(ImageIndexConverter))]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue(-1)]
		public int ImageIndex
		{
			get
			{
				return this.imageIndex;
			}
			set
			{
				if (this.imageIndex == value)
				{
					return;
				}
				this.imageIndex = value;
				this.UpdateOwner();
			}
		}

		/// <summary>Gets or sets the key accessor for the image in the <see cref="P:System.Windows.Forms.TabControl.ImageList" /> of the associated <see cref="T:System.Windows.Forms.TabControl" />.</summary>
		/// <returns>A string representing the key of the image.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x0600333E RID: 13118 RVA: 0x000C2468 File Offset: 0x000C0668
		// (set) Token: 0x0600333F RID: 13119 RVA: 0x000C2470 File Offset: 0x000C0670
		[TypeConverter(typeof(ImageKeyConverter))]
		[Localizable(true)]
		[RefreshProperties(2)]
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string ImageKey
		{
			get
			{
				return this.imageKey;
			}
			set
			{
				this.imageKey = value;
				TabControl tabControl = base.Parent as TabControl;
				if (tabControl != null)
				{
					this.ImageIndex = tabControl.ImageList.Images.IndexOfKey(this.imageKey);
				}
			}
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>An <see cref="T:System.Int32" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06003340 RID: 13120 RVA: 0x000C24B4 File Offset: 0x000C06B4
		// (set) Token: 0x06003341 RID: 13121 RVA: 0x000C24BC File Offset: 0x000C06BC
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06003342 RID: 13122 RVA: 0x000C24C8 File Offset: 0x000C06C8
		// (set) Token: 0x06003343 RID: 13123 RVA: 0x000C24D0 File Offset: 0x000C06D0
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

		/// <summary>Gets or sets the text to display on the tab.</summary>
		/// <returns>The text to display on the tab.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06003344 RID: 13124 RVA: 0x000C24DC File Offset: 0x000C06DC
		// (set) Token: 0x06003345 RID: 13125 RVA: 0x000C24E4 File Offset: 0x000C06E4
		[Browsable(true)]
		[EditorBrowsable(0)]
		[Localizable(true)]
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
				this.UpdateOwner();
			}
		}

		/// <summary>Gets or sets the ToolTip text for this tab.</summary>
		/// <returns>The ToolTip text for this tab.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06003346 RID: 13126 RVA: 0x000C2508 File Offset: 0x000C0708
		// (set) Token: 0x06003347 RID: 13127 RVA: 0x000C2510 File Offset: 0x000C0710
		[Localizable(true)]
		[DefaultValue("")]
		public string ToolTipText
		{
			get
			{
				return this.tooltip_text;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.tooltip_text = value;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06003348 RID: 13128 RVA: 0x000C2528 File Offset: 0x000C0728
		// (set) Token: 0x06003349 RID: 13129 RVA: 0x000C2530 File Offset: 0x000C0730
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
			}
		}

		/// <summary>Retrieves the tab page that contains the specified object.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TabPage" /> that contains the specified object, or null if the object cannot be found.</returns>
		/// <param name="comp">The object to look for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600334A RID: 13130 RVA: 0x000C2534 File Offset: 0x000C0734
		public static TabPage GetTabPageOfComponent(object comp)
		{
			Control control = comp as Control;
			if (control == null)
			{
				return null;
			}
			for (control = control.Parent; control != null; control = control.Parent)
			{
				if (control is TabPage)
				{
					break;
				}
			}
			return control as TabPage;
		}

		/// <summary>Returns a string containing the value of the <see cref="P:System.Windows.Forms.TabPage.Text" /> property.</summary>
		/// <returns>A string containing the value of the <see cref="P:System.Windows.Forms.TabPage.Text" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600334B RID: 13131 RVA: 0x000C2580 File Offset: 0x000C0780
		public override string ToString()
		{
			return "TabPage: {" + this.Text + "}";
		}

		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x0600334C RID: 13132 RVA: 0x000C2598 File Offset: 0x000C0798
		// (set) Token: 0x0600334D RID: 13133 RVA: 0x000C25A0 File Offset: 0x000C07A0
		internal Rectangle TabBounds
		{
			get
			{
				return this.tab_bounds;
			}
			set
			{
				this.tab_bounds = value;
			}
		}

		// Token: 0x17000D68 RID: 3432
		// (get) Token: 0x0600334E RID: 13134 RVA: 0x000C25AC File Offset: 0x000C07AC
		// (set) Token: 0x0600334F RID: 13135 RVA: 0x000C25B4 File Offset: 0x000C07B4
		internal int Row
		{
			get
			{
				return this.row;
			}
			set
			{
				this.row = value;
			}
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x000C25C0 File Offset: 0x000C07C0
		private void UpdateOwner()
		{
			if (this.Owner != null)
			{
				this.Owner.Redraw();
			}
		}

		// Token: 0x17000D69 RID: 3433
		// (get) Token: 0x06003351 RID: 13137 RVA: 0x000C25D8 File Offset: 0x000C07D8
		private TabControl Owner
		{
			get
			{
				return base.Parent as TabControl;
			}
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x000C25E8 File Offset: 0x000C07E8
		internal void SetVisible(bool value)
		{
			base.Visible = value;
		}

		/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
		// Token: 0x06003353 RID: 13139 RVA: 0x000C25F4 File Offset: 0x000C07F4
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new TabPage.TabPageControlCollection(this);
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.SetBoundsCore(System.Int32,System.Int32,System.Int32,System.Int32,System.Windows.Forms.BoundsSpecified)" />.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
		/// <param name="specified">A bitwise combination of <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x06003354 RID: 13140 RVA: 0x000C25FC File Offset: 0x000C07FC
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (this.Owner != null && this.Owner.IsHandleCreated)
			{
				Rectangle displayRectangle = this.Owner.DisplayRectangle;
				base.SetBoundsCore(displayRectangle.X, displayRectangle.Y, displayRectangle.Width, displayRectangle.Height, BoundsSpecified.All);
			}
			else
			{
				base.SetBoundsCore(x, y, width, height, specified);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event of the <see cref="T:System.Windows.Forms.TabPage" />. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003355 RID: 13141 RVA: 0x000C2668 File Offset: 0x000C0868
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event of the <see cref="T:System.Windows.Forms.TabPage" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003356 RID: 13142 RVA: 0x000C2674 File Offset: 0x000C0874
		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
		}

		/// <summary>Paints the background of the <see cref="T:System.Windows.Forms.TabPage" />.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains data useful for painting the background. </param>
		// Token: 0x06003357 RID: 13143 RVA: 0x000C2680 File Offset: 0x000C0880
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
		}

		/// <summary>This property is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D6A RID: 3434
		// (get) Token: 0x06003358 RID: 13144 RVA: 0x000C268C File Offset: 0x000C088C
		// (set) Token: 0x06003359 RID: 13145 RVA: 0x000C2694 File Offset: 0x000C0894
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		// Token: 0x04001854 RID: 6228
		private int imageIndex = -1;

		// Token: 0x04001855 RID: 6229
		private string imageKey;

		// Token: 0x04001856 RID: 6230
		private string tooltip_text = string.Empty;

		// Token: 0x04001857 RID: 6231
		private Rectangle tab_bounds;

		// Token: 0x04001858 RID: 6232
		private int row;

		// Token: 0x04001859 RID: 6233
		private bool use_visual_style_back_color;

		/// <summary>Contains the collection of controls that the <see cref="T:System.Windows.Forms.TabPage" /> uses.</summary>
		// Token: 0x020002FE RID: 766
		[ComVisible(false)]
		public class TabPageControlCollection : Control.ControlCollection
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabPage.TabPageControlCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.TabPage" /> that contains this collection of controls. </param>
			// Token: 0x0600335A RID: 13146 RVA: 0x000C26A0 File Offset: 0x000C08A0
			public TabPageControlCollection(TabPage owner)
				: base(owner)
			{
			}

			/// <summary>Adds a control to the collection.</summary>
			/// <param name="value">The control to add. </param>
			/// <exception cref="T:System.ArgumentException">The specified control is a <see cref="T:System.Windows.Forms.TabPage" />. </exception>
			// Token: 0x0600335B RID: 13147 RVA: 0x000C26AC File Offset: 0x000C08AC
			public override void Add(Control value)
			{
				base.Add(value);
			}
		}
	}
}
