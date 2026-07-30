using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows status bar control. Although <see cref="T:System.Windows.Forms.ToolStripStatusLabel" /> replaces and adds functionality to the <see cref="T:System.Windows.Forms.StatusBar" /> control of previous versions, <see cref="T:System.Windows.Forms.StatusBar" /> is retained for both backward compatibility and future use if you choose.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002E7 RID: 743
	[DefaultEvent("PanelClick")]
	[ClassInterface(1)]
	[ComVisible(true)]
	[Designer("System.Windows.Forms.Design.StatusBarDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("Text")]
	public class StatusBar : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.StatusBar" /> class.</summary>
		// Token: 0x06003125 RID: 12581 RVA: 0x000BD4AC File Offset: 0x000BB6AC
		public StatusBar()
		{
			this.Dock = DockStyle.Bottom;
			this.TabStop = false;
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.Selectable, false);
			base.MouseMove += this.StatusBar_MouseMove;
			base.MouseLeave += new EventHandler(this.StatusBar_MouseLeave);
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x000BD504 File Offset: 0x000BB704
		// Note: this type is marked as 'beforefieldinit'.
		static StatusBar()
		{
			StatusBar.DrawItemEvent = new object();
			StatusBar.PanelClickEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.StatusBar.BackColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400030A RID: 778
		// (add) Token: 0x06003127 RID: 12583 RVA: 0x000BD51C File Offset: 0x000BB71C
		// (remove) Token: 0x06003128 RID: 12584 RVA: 0x000BD528 File Offset: 0x000BB728
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.StatusBar.BackgroundImage" /> property is changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400030B RID: 779
		// (add) Token: 0x06003129 RID: 12585 RVA: 0x000BD534 File Offset: 0x000BB734
		// (remove) Token: 0x0600312A RID: 12586 RVA: 0x000BD540 File Offset: 0x000BB740
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.StatusBar.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400030C RID: 780
		// (add) Token: 0x0600312B RID: 12587 RVA: 0x000BD54C File Offset: 0x000BB74C
		// (remove) Token: 0x0600312C RID: 12588 RVA: 0x000BD558 File Offset: 0x000BB758
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.StatusBar.ForeColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400030D RID: 781
		// (add) Token: 0x0600312D RID: 12589 RVA: 0x000BD564 File Offset: 0x000BB764
		// (remove) Token: 0x0600312E RID: 12590 RVA: 0x000BD570 File Offset: 0x000BB770
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.StatusBar.ImeMode" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400030E RID: 782
		// (add) Token: 0x0600312F RID: 12591 RVA: 0x000BD57C File Offset: 0x000BB77C
		// (remove) Token: 0x06003130 RID: 12592 RVA: 0x000BD588 File Offset: 0x000BB788
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

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.StatusBar" /> control is redrawn.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400030F RID: 783
		// (add) Token: 0x06003131 RID: 12593 RVA: 0x000BD594 File Offset: 0x000BB794
		// (remove) Token: 0x06003132 RID: 12594 RVA: 0x000BD5A0 File Offset: 0x000BB7A0
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

		/// <summary>Occurs when a visual aspect of an owner-drawn status bar control changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000310 RID: 784
		// (add) Token: 0x06003133 RID: 12595 RVA: 0x000BD5AC File Offset: 0x000BB7AC
		// (remove) Token: 0x06003134 RID: 12596 RVA: 0x000BD5C0 File Offset: 0x000BB7C0
		public event StatusBarDrawItemEventHandler DrawItem
		{
			add
			{
				base.Events.AddHandler(StatusBar.DrawItemEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(StatusBar.DrawItemEvent, value);
			}
		}

		/// <summary>Occurs when a <see cref="T:System.Windows.Forms.StatusBarPanel" /> object on a <see cref="T:System.Windows.Forms.StatusBar" /> control is clicked.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000311 RID: 785
		// (add) Token: 0x06003135 RID: 12597 RVA: 0x000BD5D4 File Offset: 0x000BB7D4
		// (remove) Token: 0x06003136 RID: 12598 RVA: 0x000BD5E8 File Offset: 0x000BB7E8
		public event StatusBarPanelClickEventHandler PanelClick
		{
			add
			{
				base.Events.AddHandler(StatusBar.PanelClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(StatusBar.PanelClickEvent, value);
			}
		}

		/// <summary>Gets or sets the background color for the <see cref="T:System.Windows.Forms.StatusBar" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Color" /> that is the background color of the control</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06003137 RID: 12599 RVA: 0x000BD5FC File Offset: 0x000BB7FC
		// (set) Token: 0x06003138 RID: 12600 RVA: 0x000BD604 File Offset: 0x000BB804
		[EditorBrowsable(1)]
		[Browsable(false)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		/// <summary>Gets or sets the background image for the <see cref="T:System.Windows.Forms.StatusBar" />.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that is the background image for the <see cref="T:System.Windows.Forms.StatusBar" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06003139 RID: 12601 RVA: 0x000BD610 File Offset: 0x000BB810
		// (set) Token: 0x0600313A RID: 12602 RVA: 0x000BD618 File Offset: 0x000BB818
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

		/// <summary>Gets or sets the layout of the background image of the <see cref="T:System.Windows.Forms.StatusBar" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x0600313B RID: 12603 RVA: 0x000BD624 File Offset: 0x000BB824
		// (set) Token: 0x0600313C RID: 12604 RVA: 0x000BD62C File Offset: 0x000BB82C
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Gets or sets the docking behavior of the <see cref="T:System.Windows.Forms.StatusBar" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DockStyle" /> values. The default is Bottom.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x0600313D RID: 12605 RVA: 0x000BD638 File Offset: 0x000BB838
		// (set) Token: 0x0600313E RID: 12606 RVA: 0x000BD640 File Offset: 0x000BB840
		[Localizable(true)]
		[DefaultValue(DockStyle.Bottom)]
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

		/// <summary>Gets or sets a value indicating whether this control should redraw its surface using a secondary buffer to reduce or prevent flicker, however this property has no effect on the <see cref="T:System.Windows.Forms.StatusBar" /> control</summary>
		/// <returns>true if the control has a secondary buffer; otherwise, false. </returns>
		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x0600313F RID: 12607 RVA: 0x000BD64C File Offset: 0x000BB84C
		// (set) Token: 0x06003140 RID: 12608 RVA: 0x000BD654 File Offset: 0x000BB854
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

		/// <summary>Gets or sets the font the <see cref="T:System.Windows.Forms.StatusBar" /> control will use to display information.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> of the text. The default is the font of the container, unless you override it.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06003141 RID: 12609 RVA: 0x000BD660 File Offset: 0x000BB860
		// (set) Token: 0x06003142 RID: 12610 RVA: 0x000BD668 File Offset: 0x000BB868
		[Localizable(true)]
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				if (value == this.Font)
				{
					return;
				}
				base.Font = value;
				this.UpdateStatusBar();
			}
		}

		/// <summary>Gets or sets the forecolor for the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the forecolor of the control. The default is Empty.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06003143 RID: 12611 RVA: 0x000BD684 File Offset: 0x000BB884
		// (set) Token: 0x06003144 RID: 12612 RVA: 0x000BD68C File Offset: 0x000BB88C
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Gets or sets the Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06003145 RID: 12613 RVA: 0x000BD698 File Offset: 0x000BB898
		// (set) Token: 0x06003146 RID: 12614 RVA: 0x000BD6A0 File Offset: 0x000BB8A0
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

		/// <summary>Gets the collection of <see cref="T:System.Windows.Forms.StatusBar" /> panels contained within the control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> containing the <see cref="T:System.Windows.Forms.StatusBarPanel" /> objects of the <see cref="T:System.Windows.Forms.StatusBar" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06003147 RID: 12615 RVA: 0x000BD6AC File Offset: 0x000BB8AC
		[MergableProperty(false)]
		[DesignerSerializationVisibility(2)]
		[Localizable(true)]
		public StatusBar.StatusBarPanelCollection Panels
		{
			get
			{
				if (this.panels == null)
				{
					this.panels = new StatusBar.StatusBarPanelCollection(this);
				}
				return this.panels;
			}
		}

		/// <summary>Gets or sets a value indicating whether any panels that have been added to the control are displayed.</summary>
		/// <returns>true if panels are displayed; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06003148 RID: 12616 RVA: 0x000BD6CC File Offset: 0x000BB8CC
		// (set) Token: 0x06003149 RID: 12617 RVA: 0x000BD6D4 File Offset: 0x000BB8D4
		[DefaultValue(false)]
		public bool ShowPanels
		{
			get
			{
				return this.show_panels;
			}
			set
			{
				if (this.show_panels == value)
				{
					return;
				}
				this.show_panels = value;
				this.UpdateStatusBar();
			}
		}

		/// <summary>Gets or sets a value indicating whether a sizing grip is displayed in the lower-right corner of the control.</summary>
		/// <returns>true if a sizing grip is displayed; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x0600314A RID: 12618 RVA: 0x000BD6F0 File Offset: 0x000BB8F0
		// (set) Token: 0x0600314B RID: 12619 RVA: 0x000BD6F8 File Offset: 0x000BB8F8
		[DefaultValue(true)]
		public bool SizingGrip
		{
			get
			{
				return this.sizing_grip;
			}
			set
			{
				if (this.sizing_grip == value)
				{
					return;
				}
				this.sizing_grip = value;
				this.UpdateStatusBar();
			}
		}

		/// <summary>Gets or sets a value indicating whether the user will be able to tab to the <see cref="T:System.Windows.Forms.StatusBar" />.</summary>
		/// <returns>true if the tab key moves focus to the <see cref="T:System.Windows.Forms.StatusBar" />; otherwise false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x0600314C RID: 12620 RVA: 0x000BD714 File Offset: 0x000BB914
		// (set) Token: 0x0600314D RID: 12621 RVA: 0x000BD71C File Offset: 0x000BB91C
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

		/// <summary>Gets or sets the text associated with the <see cref="T:System.Windows.Forms.StatusBar" /> control.</summary>
		/// <returns>The text associated with the <see cref="T:System.Windows.Forms.StatusBar" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x0600314E RID: 12622 RVA: 0x000BD728 File Offset: 0x000BB928
		// (set) Token: 0x0600314F RID: 12623 RVA: 0x000BD730 File Offset: 0x000BB930
		[Localizable(true)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				if (value == this.Text)
				{
					return;
				}
				base.Text = value;
				this.UpdateStatusBar();
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.CreateParams" /> used to create the handle for this control.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.CreateParams" /> used to create the handle for this control.</returns>
		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06003150 RID: 12624 RVA: 0x000BD754 File Offset: 0x000BB954
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets the default Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x000BD75C File Offset: 0x000BB95C
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the control.</returns>
		// Token: 0x17000C91 RID: 3217
		// (get) Token: 0x06003152 RID: 12626 RVA: 0x000BD760 File Offset: 0x000BB960
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.StatusBarDefaultSize;
			}
		}

		/// <summary>Returns a string representation for this control.</summary>
		/// <returns>String </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06003153 RID: 12627 RVA: 0x000BD76C File Offset: 0x000BB96C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.ToString(),
				", Panels.Count: ",
				this.Panels.Count,
				(this.Panels.Count <= 0) ? string.Empty : (", Panels[0]: " + this.Panels[0])
			});
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.CreateHandle" />.</summary>
		// Token: 0x06003154 RID: 12628 RVA: 0x000BD7DC File Offset: 0x000BB9DC
		protected override void CreateHandle()
		{
			base.CreateHandle();
		}

		/// <summary>Disposes of the resources (other than memory) used by the <see cref="T:System.Windows.Forms.StatusBar" />.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003155 RID: 12629 RVA: 0x000BD7E4 File Offset: 0x000BB9E4
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.StatusBar.OnDrawItem(System.Windows.Forms.StatusBarDrawItemEventArgs)" /> event.</summary>
		/// <param name="sbdievent">A <see cref="T:System.Windows.Forms.StatusBarDrawItemEventArgs" /> that contains the event data. </param>
		// Token: 0x06003156 RID: 12630 RVA: 0x000BD7F0 File Offset: 0x000BB9F0
		protected virtual void OnDrawItem(StatusBarDrawItemEventArgs sbdievent)
		{
			StatusBarDrawItemEventHandler statusBarDrawItemEventHandler = (StatusBarDrawItemEventHandler)base.Events[StatusBar.DrawItemEvent];
			if (statusBarDrawItemEventHandler != null)
			{
				statusBarDrawItemEventHandler(this, sbdievent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003157 RID: 12631 RVA: 0x000BD824 File Offset: 0x000BBA24
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.CalcPanelSizes();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06003158 RID: 12632 RVA: 0x000BD834 File Offset: 0x000BBA34
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the Layout event.</summary>
		/// <param name="levent">A LayoutEventArgs that contains the event data. </param>
		// Token: 0x06003159 RID: 12633 RVA: 0x000BD840 File Offset: 0x000BBA40
		protected override void OnLayout(LayoutEventArgs levent)
		{
			base.OnLayout(levent);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.StatusBar.OnMouseDown(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x0600315A RID: 12634 RVA: 0x000BD84C File Offset: 0x000BBA4C
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (this.panels == null)
			{
				return;
			}
			float num = 0f;
			float num2 = (float)ThemeEngine.Current.StatusBarHorzGapWidth;
			for (int i = 0; i < this.panels.Count; i++)
			{
				float num3 = (float)this.panels[i].Width + num + ((i != this.panels.Count - 1) ? (num2 / 2f) : num2);
				if ((float)e.X >= num && (float)e.X <= num3)
				{
					this.OnPanelClick(new StatusBarPanelClickEventArgs(this.panels[i], e.Button, e.Clicks, e.X, e.Y));
					break;
				}
				num = num3;
			}
			base.OnMouseDown(e);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.StatusBar.OnPanelClick(System.Windows.Forms.StatusBarPanelClickEventArgs)" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.StatusBarPanelClickEventArgs" /> that contains the event data. </param>
		// Token: 0x0600315B RID: 12635 RVA: 0x000BD920 File Offset: 0x000BBB20
		protected virtual void OnPanelClick(StatusBarPanelClickEventArgs e)
		{
			StatusBarPanelClickEventHandler statusBarPanelClickEventHandler = (StatusBarPanelClickEventHandler)base.Events[StatusBar.PanelClickEvent];
			if (statusBarPanelClickEventHandler != null)
			{
				statusBarPanelClickEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.StatusBar.OnResize(System.EventArgs)" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600315C RID: 12636 RVA: 0x000BD954 File Offset: 0x000BBB54
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (base.Width <= 0 || base.Height <= 0)
			{
				return;
			}
			this.UpdateStatusBar();
		}

		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process. </param>
		// Token: 0x0600315D RID: 12637 RVA: 0x000BD988 File Offset: 0x000BBB88
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x000BD994 File Offset: 0x000BBB94
		internal void OnDrawItemInternal(StatusBarDrawItemEventArgs e)
		{
			this.OnDrawItem(e);
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x000BD9A0 File Offset: 0x000BBBA0
		internal void UpdatePanel(StatusBarPanel panel)
		{
			if (panel.AutoSize == StatusBarPanelAutoSize.Contents)
			{
				this.UpdateStatusBar();
				return;
			}
			this.UpdateStatusBar();
		}

		// Token: 0x06003160 RID: 12640 RVA: 0x000BD9BC File Offset: 0x000BBBBC
		internal void UpdatePanelContents(StatusBarPanel panel)
		{
			if (panel.AutoSize == StatusBarPanelAutoSize.Contents)
			{
				this.UpdateStatusBar();
				base.Invalidate();
				return;
			}
			base.Invalidate(new Rectangle(panel.X + 2, 2, panel.Width - 4, this.bounds.Height - 4));
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x000BDA0C File Offset: 0x000BBC0C
		private void UpdateStatusBar()
		{
			this.CalcPanelSizes();
			this.Refresh();
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x000BDA1C File Offset: 0x000BBC1C
		internal override void OnPaintInternal(PaintEventArgs pevent)
		{
			this.Draw(pevent.Graphics, pevent.ClipRectangle);
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x000BDA30 File Offset: 0x000BBC30
		private void CalcPanelSizes()
		{
			if (this.panels == null || !this.show_panels)
			{
				return;
			}
			if (base.Width == 0 || base.Height == 0)
			{
				return;
			}
			int num = 2;
			int statusBarHorzGapWidth = ThemeEngine.Current.StatusBarHorzGapWidth;
			ArrayList arrayList = null;
			int num2 = num;
			for (int i = 0; i < this.panels.Count; i++)
			{
				StatusBarPanel statusBarPanel = this.panels[i];
				if (statusBarPanel.AutoSize == StatusBarPanelAutoSize.None)
				{
					num2 += statusBarPanel.Width;
					num2 += statusBarHorzGapWidth;
				}
				else if (statusBarPanel.AutoSize == StatusBarPanelAutoSize.Contents)
				{
					int num3 = (int)(TextRenderer.MeasureString(statusBarPanel.Text, this.Font).Width + 0.5f);
					if (statusBarPanel.Icon != null)
					{
						num3 += 21;
					}
					statusBarPanel.SetWidth(num3 + 8);
					num2 += statusBarPanel.Width;
					num2 += statusBarHorzGapWidth;
				}
				else if (statusBarPanel.AutoSize == StatusBarPanelAutoSize.Spring)
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(statusBarPanel);
					num2 += statusBarHorzGapWidth;
				}
			}
			if (arrayList != null)
			{
				int count = arrayList.Count;
				int num4 = base.Width - num2 - ((!this.SizingGrip) ? 0 : ThemeEngine.Current.StatusBarSizeGripWidth);
				for (int j = 0; j < count; j++)
				{
					StatusBarPanel statusBarPanel2 = (StatusBarPanel)arrayList[j];
					int num5 = num4 / count;
					statusBarPanel2.SetWidth((num5 < statusBarPanel2.MinWidth) ? statusBarPanel2.MinWidth : num5);
				}
			}
			num2 = num;
			for (int k = 0; k < this.panels.Count; k++)
			{
				StatusBarPanel statusBarPanel3 = this.panels[k];
				statusBarPanel3.X = num2;
				num2 += statusBarPanel3.Width + statusBarHorzGapWidth;
			}
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x000BDC24 File Offset: 0x000BBE24
		private void Draw(Graphics dc, Rectangle clip)
		{
			ThemeEngine.Current.DrawStatusBar(dc, clip, this);
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x000BDC34 File Offset: 0x000BBE34
		private void StatusBar_MouseMove(object sender, MouseEventArgs e)
		{
			if (!this.show_panels)
			{
				return;
			}
			StatusBarPanel panelAtPoint = this.GetPanelAtPoint(e.Location);
			if (panelAtPoint != this.tooltip_currently_showing)
			{
				this.MouseLeftPanel(this.tooltip_currently_showing);
			}
			if (panelAtPoint != null && this.tooltip_currently_showing == null)
			{
				this.MouseEnteredPanel(panelAtPoint);
			}
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x000BDC8C File Offset: 0x000BBE8C
		private void StatusBar_MouseLeave(object sender, EventArgs e)
		{
			if (this.tooltip_currently_showing != null)
			{
				this.MouseLeftPanel(this.tooltip_currently_showing);
			}
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x000BDCA8 File Offset: 0x000BBEA8
		private StatusBarPanel GetPanelAtPoint(Point point)
		{
			foreach (object obj in this.Panels)
			{
				StatusBarPanel statusBarPanel = (StatusBarPanel)obj;
				if (point.X >= statusBarPanel.X && point.X <= statusBarPanel.X + statusBarPanel.Width)
				{
					return statusBarPanel;
				}
			}
			return null;
		}

		// Token: 0x06003168 RID: 12648 RVA: 0x000BDD48 File Offset: 0x000BBF48
		private void MouseEnteredPanel(StatusBarPanel item)
		{
			this.tooltip_currently_showing = item;
			this.ToolTipTimer.Start();
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x000BDD5C File Offset: 0x000BBF5C
		private void MouseLeftPanel(StatusBarPanel item)
		{
			this.ToolTipTimer.Stop();
			this.ToolTipWindow.Hide(this);
			this.tooltip_currently_showing = null;
		}

		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x0600316A RID: 12650 RVA: 0x000BDD88 File Offset: 0x000BBF88
		private Timer ToolTipTimer
		{
			get
			{
				if (this.tooltip_timer == null)
				{
					this.tooltip_timer = new Timer();
					this.tooltip_timer.Enabled = false;
					this.tooltip_timer.Interval = 500;
					this.tooltip_timer.Tick += new EventHandler(this.ToolTipTimer_Tick);
				}
				return this.tooltip_timer;
			}
		}

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x0600316B RID: 12651 RVA: 0x000BDDE4 File Offset: 0x000BBFE4
		private ToolTip ToolTipWindow
		{
			get
			{
				if (this.tooltip_window == null)
				{
					this.tooltip_window = new ToolTip();
				}
				return this.tooltip_window;
			}
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x000BDE04 File Offset: 0x000BC004
		private void ToolTipTimer_Tick(object o, EventArgs args)
		{
			string toolTipText = this.tooltip_currently_showing.ToolTipText;
			if (toolTipText != null && toolTipText.Length > 0)
			{
				this.ToolTipWindow.Present(this, toolTipText);
			}
			this.ToolTipTimer.Stop();
		}

		// Token: 0x040017E5 RID: 6117
		private StatusBar.StatusBarPanelCollection panels;

		// Token: 0x040017E6 RID: 6118
		private bool show_panels;

		// Token: 0x040017E7 RID: 6119
		private bool sizing_grip = true;

		// Token: 0x040017E8 RID: 6120
		private Timer tooltip_timer;

		// Token: 0x040017E9 RID: 6121
		private ToolTip tooltip_window;

		// Token: 0x040017EA RID: 6122
		private StatusBarPanel tooltip_currently_showing;

		/// <summary>Represents the collection of panels in a <see cref="T:System.Windows.Forms.StatusBar" /> control.</summary>
		// Token: 0x020002E8 RID: 744
		[ListBindable(false)]
		public class StatusBarPanelCollection : ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.StatusBar" /> control that contains this collection. </param>
			// Token: 0x0600316D RID: 12653 RVA: 0x000BDE48 File Offset: 0x000BC048
			public StatusBarPanelCollection(StatusBar owner)
			{
				this.owner = owner;
			}

			// Token: 0x0600316E RID: 12654 RVA: 0x000BDE64 File Offset: 0x000BC064
			// Note: this type is marked as 'beforefieldinit'.
			static StatusBarPanelCollection()
			{
				StatusBar.StatusBarPanelCollection.UIACollectionChangedEvent = new object();
			}

			// Token: 0x14000312 RID: 786
			// (add) Token: 0x0600316F RID: 12655 RVA: 0x000BDE70 File Offset: 0x000BC070
			// (remove) Token: 0x06003170 RID: 12656 RVA: 0x000BDE88 File Offset: 0x000BC088
			internal event CollectionChangeEventHandler UIACollectionChanged
			{
				add
				{
					this.owner.Events.AddHandler(StatusBar.StatusBarPanelCollection.UIACollectionChangedEvent, value);
				}
				remove
				{
					this.owner.Events.RemoveHandler(StatusBar.StatusBarPanelCollection.UIACollectionChangedEvent, value);
				}
			}

			/// <summary>Gets a value indicating whether access to the collection is synchronized (thread safe).</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000C94 RID: 3220
			// (get) Token: 0x06003171 RID: 12657 RVA: 0x000BDEA0 File Offset: 0x000BC0A0
			bool ICollection.IsSynchronized
			{
				get
				{
					return this.panels.IsSynchronized;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the collection.</summary>
			/// <returns>The object used to synchronize access to the collection.</returns>
			// Token: 0x17000C95 RID: 3221
			// (get) Token: 0x06003172 RID: 12658 RVA: 0x000BDEB0 File Offset: 0x000BC0B0
			object ICollection.SyncRoot
			{
				get
				{
					return this.panels.SyncRoot;
				}
			}

			/// <summary>Copies the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> to a compatible one-dimensional array, starting at the specified index of the target array.</summary>
			/// <param name="dest">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.  </param>
			/// <param name="index">The zero-based index in the array at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="array" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="array" /> is multidimensional.-or-The number of elements in the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> is greater than the available space from index to the end of <paramref name="array" />.</exception>
			/// <exception cref="T:System.InvalidCastException">The type in the collection cannot be cast automatically to the type of <paramref name="array" />.</exception>
			// Token: 0x06003173 RID: 12659 RVA: 0x000BDEC0 File Offset: 0x000BC0C0
			void ICollection.CopyTo(Array dest, int index)
			{
				this.panels.CopyTo(dest, index);
			}

			/// <summary>Gets or sets the element at the specified index.</summary>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.StatusBarPanel" />.</exception>
			// Token: 0x17000C96 RID: 3222
			// (get) Token: 0x06003174 RID: 12660 RVA: 0x000BDED0 File Offset: 0x000BC0D0
			// (set) Token: 0x06003175 RID: 12661 RVA: 0x000BDEDC File Offset: 0x000BC0DC
			object IList.Item
			{
				get
				{
					return this[index];
				}
				set
				{
					if (!(value is StatusBarPanel))
					{
						throw new ArgumentException("Value must be of type StatusBarPanel.", "value");
					}
					this[index] = (StatusBarPanel)value;
				}
			}

			/// <summary>Adds a <see cref="T:System.Windows.Forms.StatusBarPanel" /> to the collection.</summary>
			/// <param name="value">A <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel to add to the collection.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.StatusBarPanel" />.-or-The parent of value is not null.</exception>
			// Token: 0x06003176 RID: 12662 RVA: 0x000BDF14 File Offset: 0x000BC114
			int IList.Add(object value)
			{
				if (!(value is StatusBarPanel))
				{
					throw new ArgumentException("Value must be of type StatusBarPanel.", "value");
				}
				return this.AddInternal((StatusBarPanel)value, true);
			}

			/// <summary>Determines whether the specified panel is located within the collection.</summary>
			/// <returns>true if panel is a <see cref="T:System.Windows.Forms.StatusBarPanel" /> located within the collection; otherwise, false.</returns>
			/// <param name="panel">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> to locate in the collection.</param>
			// Token: 0x06003177 RID: 12663 RVA: 0x000BDF4C File Offset: 0x000BC14C
			bool IList.Contains(object panel)
			{
				return this.panels.Contains(panel);
			}

			/// <summary>Returns the index of the specified panel within the collection.</summary>
			/// <returns>The zero-based index of panel, if found, within the entire collection; otherwise, -1.</returns>
			/// <param name="panel">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> to locate in the collection.</param>
			// Token: 0x06003178 RID: 12664 RVA: 0x000BDF5C File Offset: 0x000BC15C
			int IList.IndexOf(object panel)
			{
				return this.panels.IndexOf(panel);
			}

			/// <summary>Inserts the specified <see cref="T:System.Windows.Forms.StatusBarPanel" /> into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the panel is inserted. </param>
			/// <param name="value">A <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel to insert.</param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The index parameter is less than zero or greater than the value of the Count property.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.StatusBarPanel" />.-or-The parent of value is not null.</exception>
			// Token: 0x06003179 RID: 12665 RVA: 0x000BDF6C File Offset: 0x000BC16C
			void IList.Insert(int index, object value)
			{
				if (!(value is StatusBarPanel))
				{
					throw new ArgumentException("Value must be of type StatusBarPanel.", "value");
				}
				this.Insert(index, (StatusBarPanel)value);
			}

			/// <summary>Gets a value indicating whether the collection has a fixed size.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000C97 RID: 3223
			// (get) Token: 0x0600317A RID: 12666 RVA: 0x000BDFA4 File Offset: 0x000BC1A4
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Removes the specified <see cref="T:System.Windows.Forms.StatusBarPanel" /> from the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel to remove from the collection.</param>
			// Token: 0x0600317B RID: 12667 RVA: 0x000BDFA8 File Offset: 0x000BC1A8
			void IList.Remove(object value)
			{
				StatusBarPanel statusBarPanel = value as StatusBarPanel;
				this.Remove(statusBarPanel);
			}

			// Token: 0x0600317C RID: 12668 RVA: 0x000BDFC4 File Offset: 0x000BC1C4
			internal void OnUIACollectionChanged(CollectionChangeEventArgs e)
			{
				CollectionChangeEventHandler collectionChangeEventHandler = (CollectionChangeEventHandler)this.owner.Events[StatusBar.StatusBarPanelCollection.UIACollectionChangedEvent];
				if (collectionChangeEventHandler != null)
				{
					collectionChangeEventHandler.Invoke(this.owner, e);
				}
			}

			// Token: 0x0600317D RID: 12669 RVA: 0x000BE000 File Offset: 0x000BC200
			private int AddInternal(StatusBarPanel p, bool refresh)
			{
				if (p == null)
				{
					throw new ArgumentNullException("value");
				}
				p.SetParent(this.owner);
				int num = this.panels.Add(p);
				if (refresh)
				{
					this.owner.CalcPanelSizes();
					this.owner.Refresh();
				}
				this.OnUIACollectionChanged(new CollectionChangeEventArgs(1, num));
				return num;
			}

			/// <summary>Gets the number of items in the collection.</summary>
			/// <returns>The number of <see cref="T:System.Windows.Forms.StatusBarPanel" /> objects in the collection.</returns>
			// Token: 0x17000C98 RID: 3224
			// (get) Token: 0x0600317E RID: 12670 RVA: 0x000BE068 File Offset: 0x000BC268
			[EditorBrowsable(1)]
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.panels.Count;
				}
			}

			/// <summary>Gets a value indicating whether this collection is read-only.</summary>
			/// <returns>true if this collection is read-only; otherwise, false.</returns>
			// Token: 0x17000C99 RID: 3225
			// (get) Token: 0x0600317F RID: 12671 RVA: 0x000BE078 File Offset: 0x000BC278
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.StatusBarPanel" /> at the specified index.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.StatusBarPanel" /> representing the panel located at the specified index within the collection.</returns>
			/// <param name="index">The index of the panel in the collection to get or set. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.StatusBar.StatusBarPanelCollection.Count" /> property of the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> class. </exception>
			/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> assigned to the collection was null. </exception>
			// Token: 0x17000C9A RID: 3226
			public virtual StatusBarPanel this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return (StatusBarPanel)this.panels[index];
				}
				set
				{
					if (value == null)
					{
						throw new ArgumentNullException("index");
					}
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					this.OnUIACollectionChanged(new CollectionChangeEventArgs(2, index));
					value.SetParent(this.owner);
					this.panels[index] = value;
					this.OnUIACollectionChanged(new CollectionChangeEventArgs(1, index));
				}
			}

			/// <summary>Gets an item with the specified key from the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified key.</returns>
			/// <param name="key">The name of the item to retrieve from the collection.</param>
			// Token: 0x17000C9B RID: 3227
			public virtual StatusBarPanel this[string key]
			{
				get
				{
					int num = this.IndexOfKey(key);
					if (num >= 0 && num < this.Count)
					{
						return (StatusBarPanel)this.panels[num];
					}
					return null;
				}
			}

			/// <summary>Adds a <see cref="T:System.Windows.Forms.StatusBarPanel" /> to the collection.</summary>
			/// <returns>The zero-based index of the item in the collection.</returns>
			/// <param name="value">A <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel to add to the collection. </param>
			/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> being added to the collection was null. </exception>
			/// <exception cref="T:System.ArgumentException">The parent of the <see cref="T:System.Windows.Forms.StatusBarPanel" /> specified in the <paramref name="value" /> parameter is not null. </exception>
			// Token: 0x06003183 RID: 12675 RVA: 0x000BE168 File Offset: 0x000BC368
			public virtual int Add(StatusBarPanel value)
			{
				return this.AddInternal(value, true);
			}

			/// <summary>Adds a <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified text to the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.StatusBarPanel" /> that represents the panel that was added to the collection.</returns>
			/// <param name="text">The text for the <see cref="T:System.Windows.Forms.StatusBarPanel" /> that is being added. </param>
			// Token: 0x06003184 RID: 12676 RVA: 0x000BE174 File Offset: 0x000BC374
			public virtual StatusBarPanel Add(string text)
			{
				StatusBarPanel statusBarPanel = new StatusBarPanel();
				statusBarPanel.Text = text;
				this.Add(statusBarPanel);
				return statusBarPanel;
			}

			/// <summary>Adds an array of <see cref="T:System.Windows.Forms.StatusBarPanel" /> objects to the collection.</summary>
			/// <param name="panels">An array of <see cref="T:System.Windows.Forms.StatusBarPanel" /> objects to add. </param>
			/// <exception cref="T:System.ArgumentNullException">The array of <see cref="T:System.Windows.Forms.StatusBarPanel" /> objects being added to the collection was null. </exception>
			// Token: 0x06003185 RID: 12677 RVA: 0x000BE198 File Offset: 0x000BC398
			public virtual void AddRange(StatusBarPanel[] panels)
			{
				if (panels == null)
				{
					throw new ArgumentNullException("panels");
				}
				if (panels.Length == 0)
				{
					return;
				}
				for (int i = 0; i < panels.Length; i++)
				{
					this.AddInternal(panels[i], false);
				}
				this.owner.Refresh();
			}

			/// <summary>Removes all items from the collection.</summary>
			// Token: 0x06003186 RID: 12678 RVA: 0x000BE1EC File Offset: 0x000BC3EC
			public virtual void Clear()
			{
				this.panels.Clear();
				this.owner.Refresh();
				this.OnUIACollectionChanged(new CollectionChangeEventArgs(3, -1));
			}

			/// <summary>Determines whether the specified panel is located within the collection.</summary>
			/// <returns>true if the panel is located within the collection; otherwise, false.</returns>
			/// <param name="panel">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> to locate in the collection. </param>
			// Token: 0x06003187 RID: 12679 RVA: 0x000BE224 File Offset: 0x000BC424
			public bool Contains(StatusBarPanel panel)
			{
				return this.panels.Contains(panel);
			}

			/// <summary>Determines whether the collection contains a <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified key. </summary>
			/// <returns>true to indicate the collection contains a <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified key; otherwise, false. </returns>
			/// <param name="key">The name of the item to find in the collection.</param>
			// Token: 0x06003188 RID: 12680 RVA: 0x000BE234 File Offset: 0x000BC434
			public virtual bool ContainsKey(string key)
			{
				int num = this.IndexOfKey(key);
				return num >= 0 && num < this.Count;
			}

			/// <summary>Returns an enumerator to use to iterate through the item collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the item collection.</returns>
			// Token: 0x06003189 RID: 12681 RVA: 0x000BE25C File Offset: 0x000BC45C
			public IEnumerator GetEnumerator()
			{
				return this.panels.GetEnumerator();
			}

			/// <summary>Returns the index within the collection of the specified panel.</summary>
			/// <returns>The zero-based index where the panel is located within the collection; otherwise, negative one (-1).</returns>
			/// <param name="panel">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> to locate in the collection. </param>
			// Token: 0x0600318A RID: 12682 RVA: 0x000BE26C File Offset: 0x000BC46C
			public int IndexOf(StatusBarPanel panel)
			{
				return this.panels.IndexOf(panel);
			}

			/// <summary>Returns the index of the first occurrence of a <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified key.</summary>
			/// <returns>The zero-based index of the first occurrence of the <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified key, if found; otherwise, -1.</returns>
			/// <param name="key">The name of the <see cref="T:System.Windows.Forms.StatusBarPanel" /> to find in the collection.</param>
			// Token: 0x0600318B RID: 12683 RVA: 0x000BE27C File Offset: 0x000BC47C
			public virtual int IndexOfKey(string key)
			{
				if (key == null || key == string.Empty)
				{
					return -1;
				}
				if (this.last_index_by_key >= 0 && this.last_index_by_key < this.Count && string.Compare(((StatusBarPanel)this.panels[this.last_index_by_key]).Name, key, 5) == 0)
				{
					return this.last_index_by_key;
				}
				for (int i = 0; i < this.Count; i++)
				{
					StatusBarPanel statusBarPanel = this.panels[i] as StatusBarPanel;
					if (statusBarPanel != null && string.Compare(statusBarPanel.Name, key, 5) == 0)
					{
						this.last_index_by_key = i;
						return i;
					}
				}
				return -1;
			}

			/// <summary>Inserts the specified <see cref="T:System.Windows.Forms.StatusBarPanel" /> into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the panel is inserted. </param>
			/// <param name="value">A <see cref="T:System.Windows.Forms.StatusBarPanel" /> representing the panel to insert. </param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
			/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> parameter's parent is not null. </exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than the value of the <see cref="P:System.Windows.Forms.StatusBar.StatusBarPanelCollection.Count" /> property of the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> class. </exception>
			/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The <see cref="P:System.Windows.Forms.StatusBarPanel.AutoSize" /> property of the <paramref name="value" /> parameter's panel is not a valid <see cref="T:System.Windows.Forms.StatusBarPanelAutoSize" /> value. </exception>
			// Token: 0x0600318C RID: 12684 RVA: 0x000BE338 File Offset: 0x000BC538
			public virtual void Insert(int index, StatusBarPanel value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (index > this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				value.SetParent(this.owner);
				this.panels.Insert(index, value);
				this.owner.Refresh();
				this.OnUIACollectionChanged(new CollectionChangeEventArgs(1, index));
			}

			/// <summary>Removes the specified <see cref="T:System.Windows.Forms.StatusBarPanel" /> from the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> representing the panel to remove from the collection. </param>
			/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Windows.Forms.StatusBarPanel" /> assigned to the <paramref name="value" /> parameter is null. </exception>
			// Token: 0x0600318D RID: 12685 RVA: 0x000BE3A4 File Offset: 0x000BC5A4
			public virtual void Remove(StatusBarPanel value)
			{
				int num = this.IndexOf(value);
				this.panels.Remove(value);
				if (num >= 0)
				{
					this.OnUIACollectionChanged(new CollectionChangeEventArgs(2, num));
				}
			}

			/// <summary>Removes the <see cref="T:System.Windows.Forms.StatusBarPanel" /> located at the specified index within the collection.</summary>
			/// <param name="index">The zero-based index of the item to remove. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is less than zero or greater than or equal to the value of the <see cref="P:System.Windows.Forms.StatusBar.StatusBarPanelCollection.Count" /> property of the <see cref="T:System.Windows.Forms.StatusBar.StatusBarPanelCollection" /> class. </exception>
			// Token: 0x0600318E RID: 12686 RVA: 0x000BE3E0 File Offset: 0x000BC5E0
			public virtual void RemoveAt(int index)
			{
				this.panels.RemoveAt(index);
				this.OnUIACollectionChanged(new CollectionChangeEventArgs(2, index));
			}

			/// <summary>Removes the <see cref="T:System.Windows.Forms.StatusBarPanel" /> with the specified key from the collection.</summary>
			/// <param name="key">The name of the <see cref="T:System.Windows.Forms.StatusBarPanel" /> to remove from the collection.</param>
			// Token: 0x0600318F RID: 12687 RVA: 0x000BE400 File Offset: 0x000BC600
			public virtual void RemoveByKey(string key)
			{
				int num = this.IndexOfKey(key);
				if (num >= 0 && num < this.Count)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x040017ED RID: 6125
			private StatusBar owner;

			// Token: 0x040017EE RID: 6126
			private ArrayList panels = new ArrayList();

			// Token: 0x040017EF RID: 6127
			private int last_index_by_key;
		}
	}
}
