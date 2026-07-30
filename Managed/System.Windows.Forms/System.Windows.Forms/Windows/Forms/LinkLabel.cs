using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Runtime.InteropServices;
using System.Windows.Forms.Theming;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows label control that can display hyperlinks.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200020A RID: 522
	[DefaultEvent("LinkClicked")]
	[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ComVisible(true)]
	[ClassInterface(1)]
	public class LinkLabel : Label, IButtonControl
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.Windows.Forms.LinkLabel" /> class.</summary>
		// Token: 0x06002011 RID: 8209 RVA: 0x000783C8 File Offset: 0x000765C8
		public LinkLabel()
		{
			this.LinkArea = new LinkArea(0, -1);
			this.link_behavior = LinkBehavior.SystemDefault;
			this.link_visited = false;
			this.pieces = null;
			this.focused_index = -1;
			this.string_format.FormatFlags |= 16384;
			this.ActiveLinkColor = Color.Red;
			this.DisabledLinkColor = ThemeEngine.Current.ColorGrayText;
			this.LinkColor = Color.FromArgb(255, 0, 0, 255);
			this.VisitedLinkColor = Color.FromArgb(255, 128, 0, 128);
			base.SetStyle(ControlStyles.Selectable, false);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.CreateLinkPieces();
		}

		// Token: 0x06002012 RID: 8210 RVA: 0x00078494 File Offset: 0x00076694
		// Note: this type is marked as 'beforefieldinit'.
		static LinkLabel()
		{
			LinkLabel.LinkClickedEvent = new object();
		}

		/// <summary>Occurs when a link is clicked within the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001F7 RID: 503
		// (add) Token: 0x06002013 RID: 8211 RVA: 0x000784A0 File Offset: 0x000766A0
		// (remove) Token: 0x06002014 RID: 8212 RVA: 0x000784B4 File Offset: 0x000766B4
		public event LinkLabelLinkClickedEventHandler LinkClicked
		{
			add
			{
				base.Events.AddHandler(LinkLabel.LinkClickedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(LinkLabel.LinkClickedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.Label.TabStop" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140001F8 RID: 504
		// (add) Token: 0x06002015 RID: 8213 RVA: 0x000784C8 File Offset: 0x000766C8
		// (remove) Token: 0x06002016 RID: 8214 RVA: 0x000784D4 File Offset: 0x000766D4
		[EditorBrowsable(0)]
		[Browsable(true)]
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

		/// <summary>For a description of this member, see <see cref="P:System.Windows.Forms.IButtonControl.DialogResult" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DialogResult" /> values.</returns>
		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x000784E0 File Offset: 0x000766E0
		// (set) Token: 0x06002018 RID: 8216 RVA: 0x000784E8 File Offset: 0x000766E8
		DialogResult IButtonControl.DialogResult
		{
			get
			{
				return this.dialog_result;
			}
			set
			{
				this.dialog_result = value;
			}
		}

		/// <summary>Notifies the <see cref="T:System.Windows.Forms.LinkLabel" /> control that it is the default button.</summary>
		/// <param name="value">true if the control should behave as a default button; otherwise, false.</param>
		// Token: 0x06002019 RID: 8217 RVA: 0x000784F4 File Offset: 0x000766F4
		void IButtonControl.NotifyDefault(bool value)
		{
		}

		/// <summary>Generates a <see cref="E:System.Windows.Forms.Control.Click" /> event for the <see cref="T:System.Windows.Forms.LinkLabel" /> control.</summary>
		// Token: 0x0600201A RID: 8218 RVA: 0x000784F8 File Offset: 0x000766F8
		void IButtonControl.PerformClick()
		{
		}

		/// <summary>Gets or sets the color used to display an active link.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color to display an active link. The default color is specified by the system, typically this color is Color.Red.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x0600201B RID: 8219 RVA: 0x000784FC File Offset: 0x000766FC
		// (set) Token: 0x0600201C RID: 8220 RVA: 0x00078504 File Offset: 0x00076704
		public Color ActiveLinkColor
		{
			get
			{
				return this.active_link_color;
			}
			set
			{
				if (this.active_link_color == value)
				{
					return;
				}
				this.active_link_color = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the color used when displaying a disabled link.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color when displaying a disabled link. The default is Empty.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x0600201D RID: 8221 RVA: 0x00078528 File Offset: 0x00076728
		// (set) Token: 0x0600201E RID: 8222 RVA: 0x00078530 File Offset: 0x00076730
		public Color DisabledLinkColor
		{
			get
			{
				return this.disabled_link_color;
			}
			set
			{
				if (this.disabled_link_color == value)
				{
					return;
				}
				this.disabled_link_color = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the color used when displaying a normal link.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to displaying a normal link. The default color is specified by the system, typically this color is Color.Blue.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x0600201F RID: 8223 RVA: 0x00078554 File Offset: 0x00076754
		// (set) Token: 0x06002020 RID: 8224 RVA: 0x0007855C File Offset: 0x0007675C
		public Color LinkColor
		{
			get
			{
				return this.link_color;
			}
			set
			{
				if (this.link_color == value)
				{
					return;
				}
				this.link_color = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the color used when displaying a link that that has been previously visited.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the color used to display links that have been visited. The default color is specified by the system, typically this color is Color.Purple.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06002021 RID: 8225 RVA: 0x00078580 File Offset: 0x00076780
		// (set) Token: 0x06002022 RID: 8226 RVA: 0x00078588 File Offset: 0x00076788
		public Color VisitedLinkColor
		{
			get
			{
				return this.visited_color;
			}
			set
			{
				if (this.visited_color == value)
				{
					return;
				}
				this.visited_color = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the range in the text to treat as a link.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.LinkArea" /> that represents the area treated as a link.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Windows.Forms.LinkArea.Start" /> property of the <see cref="T:System.Windows.Forms.LinkArea" /> object is less than zero.-or- The <see cref="P:System.Windows.Forms.LinkArea.Length" /> property of the <see cref="T:System.Windows.Forms.LinkArea" /> object is less than -1. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06002023 RID: 8227 RVA: 0x000785AC File Offset: 0x000767AC
		// (set) Token: 0x06002024 RID: 8228 RVA: 0x000785B4 File Offset: 0x000767B4
		[Editor("System.Windows.Forms.Design.LinkAreaEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[RefreshProperties(2)]
		public LinkArea LinkArea
		{
			get
			{
				return this.link_area;
			}
			set
			{
				if (value.Start < 0 || value.Length < -1)
				{
					throw new ArgumentException();
				}
				this.Links.Clear();
				if (!value.IsEmpty)
				{
					this.Links.Add(value.Start, value.Length);
					this.link_area = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value that represents the behavior of a link.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.LinkBehavior" /> values. The default is LinkBehavior.SystemDefault.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">A value is assigned that is not one of the <see cref="T:System.Windows.Forms.LinkBehavior" /> values.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06002025 RID: 8229 RVA: 0x00078620 File Offset: 0x00076820
		// (set) Token: 0x06002026 RID: 8230 RVA: 0x00078628 File Offset: 0x00076828
		[DefaultValue(LinkBehavior.SystemDefault)]
		public LinkBehavior LinkBehavior
		{
			get
			{
				return this.link_behavior;
			}
			set
			{
				if (this.link_behavior == value)
				{
					return;
				}
				this.link_behavior = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets the collection of links contained within the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.LinkLabel.LinkCollection" /> that represents the links contained within the <see cref="T:System.Windows.Forms.LinkLabel" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06002027 RID: 8231 RVA: 0x00078644 File Offset: 0x00076844
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public LinkLabel.LinkCollection Links
		{
			get
			{
				if (this.link_collection == null)
				{
					this.link_collection = new LinkLabel.LinkCollection(this);
				}
				return this.link_collection;
			}
		}

		/// <summary>Gets or sets a value indicating whether a link should be displayed as though it were visited.</summary>
		/// <returns>true if links should display as though they were visited; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06002028 RID: 8232 RVA: 0x00078664 File Offset: 0x00076864
		// (set) Token: 0x06002029 RID: 8233 RVA: 0x0007866C File Offset: 0x0007686C
		[DefaultValue(false)]
		public bool LinkVisited
		{
			get
			{
				return this.link_visited;
			}
			set
			{
				if (this.link_visited == value)
				{
					return;
				}
				this.link_visited = value;
				base.Invalidate();
			}
		}

		/// <summary>Gets or sets the mouse pointer to use when the mouse pointer is within the bounds of the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Cursor" /> to use when the mouse pointer is within the <see cref="T:System.Windows.Forms.LinkLabel" /> bounds.</returns>
		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x0600202A RID: 8234 RVA: 0x00078688 File Offset: 0x00076888
		// (set) Token: 0x0600202B RID: 8235 RVA: 0x000786B8 File Offset: 0x000768B8
		protected Cursor OverrideCursor
		{
			get
			{
				if (this.override_cursor == null)
				{
					this.override_cursor = Cursors.Hand;
				}
				return this.override_cursor;
			}
			set
			{
				this.override_cursor = value;
			}
		}

		/// <filterpriority>1</filterpriority>
		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x0600202C RID: 8236 RVA: 0x000786C4 File Offset: 0x000768C4
		// (set) Token: 0x0600202D RID: 8237 RVA: 0x000786CC File Offset: 0x000768CC
		[RefreshProperties(2)]
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
				this.CreateLinkPieces();
			}
		}

		/// <summary>Gets or sets the flat style appearance of the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.FlatStyle" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x0600202E RID: 8238 RVA: 0x000786F0 File Offset: 0x000768F0
		// (set) Token: 0x0600202F RID: 8239 RVA: 0x000786F8 File Offset: 0x000768F8
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new FlatStyle FlatStyle
		{
			get
			{
				return base.FlatStyle;
			}
			set
			{
				if (base.FlatStyle == value)
				{
					return;
				}
				base.FlatStyle = value;
			}
		}

		/// <summary>Gets or sets the interior spacing, in pixels, between the edges of a <see cref="T:System.Windows.Forms.LinkLabel" /> and its contents.</summary>
		/// <returns>
		///   <see cref="T:System.Windows.Forms.Padding" /> values representing the interior spacing, in pixels.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06002030 RID: 8240 RVA: 0x00078710 File Offset: 0x00076910
		// (set) Token: 0x06002031 RID: 8241 RVA: 0x00078718 File Offset: 0x00076918
		[RefreshProperties(2)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				if (base.Padding == value)
				{
					return;
				}
				base.Padding = value;
				this.CreateLinkPieces();
			}
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.LinkLabel" /> control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.AccessibleObject" /> for the control.</returns>
		// Token: 0x06002032 RID: 8242 RVA: 0x0007873C File Offset: 0x0007693C
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return base.CreateAccessibilityInstance();
		}

		/// <summary>Creates a handle for this control. This method is called by the .NET Framework, this should not be called. Inheriting classes should always call base.createHandle when overriding this method.</summary>
		// Token: 0x06002033 RID: 8243 RVA: 0x00078744 File Offset: 0x00076944
		protected override void CreateHandle()
		{
			base.CreateHandle();
			this.CreateLinkPieces();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Label.AutoSizeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002034 RID: 8244 RVA: 0x00078754 File Offset: 0x00076954
		protected override void OnAutoSizeChanged(EventArgs e)
		{
			base.OnAutoSizeChanged(e);
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x00078760 File Offset: 0x00076960
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			base.Invalidate();
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x00078770 File Offset: 0x00076970
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.CreateLinkPieces();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002037 RID: 8247 RVA: 0x00078780 File Offset: 0x00076980
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			if (this.focused_index == -1)
			{
				if ((Control.ModifierKeys & Keys.Shift) == Keys.None)
				{
					for (int i = 0; i < this.sorted_links.Length; i++)
					{
						if (this.sorted_links[i].Enabled)
						{
							this.focused_index = i;
							break;
						}
					}
				}
				else
				{
					if (this.focused_index == -1)
					{
						this.focused_index = this.sorted_links.Length;
					}
					for (int j = this.focused_index - 1; j >= 0; j--)
					{
						if (this.sorted_links[j].Enabled)
						{
							this.sorted_links[j].Focused = true;
							this.focused_index = j;
							return;
						}
					}
				}
			}
			if (this.focused_index != -1)
			{
				this.sorted_links[this.focused_index].Focused = true;
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnKeyDown(System.Windows.Forms.KeyEventArgs)" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data. </param>
		// Token: 0x06002038 RID: 8248 RVA: 0x00078868 File Offset: 0x00076A68
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return && this.focused_index != -1)
			{
				this.OnLinkClicked(new LinkLabelLinkClickedEventArgs(this.sorted_links[this.focused_index]));
			}
			base.OnKeyDown(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.LinkLabel.LinkClicked" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LinkLabelLinkClickedEventArgs" /> that contains the event data. </param>
		// Token: 0x06002039 RID: 8249 RVA: 0x000788B0 File Offset: 0x00076AB0
		protected virtual void OnLinkClicked(LinkLabelLinkClickedEventArgs e)
		{
			LinkLabelLinkClickedEventHandler linkLabelLinkClickedEventHandler = (LinkLabelLinkClickedEventHandler)base.Events[LinkLabel.LinkClickedEvent];
			if (linkLabelLinkClickedEventHandler != null)
			{
				linkLabelLinkClickedEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.LostFocus" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600203A RID: 8250 RVA: 0x000788E4 File Offset: 0x00076AE4
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			if (this.focused_index != -1)
			{
				this.sorted_links[this.focused_index].Focused = false;
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseDown(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x0600203B RID: 8251 RVA: 0x00078918 File Offset: 0x00076B18
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (!base.Enabled)
			{
				return;
			}
			base.OnMouseDown(e);
			for (int i = 0; i < this.sorted_links.Length; i++)
			{
				if (this.sorted_links[i].Contains(e.X, e.Y) && this.sorted_links[i].Enabled)
				{
					this.sorted_links[i].Active = true;
					if (this.focused_index != -1)
					{
						this.sorted_links[this.focused_index].Focused = false;
					}
					this.active_link = this.sorted_links[i];
					this.focused_index = i;
					this.sorted_links[this.focused_index].Focused = true;
					break;
				}
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseLeave(System.EventArgs)" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600203C RID: 8252 RVA: 0x000789DC File Offset: 0x00076BDC
		protected override void OnMouseLeave(EventArgs e)
		{
			if (!base.Enabled)
			{
				return;
			}
			base.OnMouseLeave(e);
			this.UpdateHover(null);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.PaddingChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600203D RID: 8253 RVA: 0x000789F8 File Offset: 0x00076BF8
		protected override void OnPaddingChanged(EventArgs e)
		{
			base.OnPaddingChanged(e);
		}

		// Token: 0x0600203E RID: 8254 RVA: 0x00078A04 File Offset: 0x00076C04
		private void UpdateHover(LinkLabel.Link link)
		{
			if (link == this.hovered_link)
			{
				return;
			}
			if (this.hovered_link != null)
			{
				this.hovered_link.Hovered = false;
			}
			this.hovered_link = link;
			if (this.hovered_link != null)
			{
				this.hovered_link.Hovered = true;
			}
			this.Cursor = ((this.hovered_link == null) ? Cursors.Default : this.OverrideCursor);
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseMove(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x0600203F RID: 8255 RVA: 0x00078A7C File Offset: 0x00076C7C
		protected override void OnMouseMove(MouseEventArgs e)
		{
			this.UpdateHover(this.PointInLink(e.X, e.Y));
			base.OnMouseMove(e);
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnMouseUp(System.Windows.Forms.MouseEventArgs)" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x06002040 RID: 8256 RVA: 0x00078AA8 File Offset: 0x00076CA8
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (!base.Enabled)
			{
				return;
			}
			base.OnMouseUp(e);
			if (this.active_link == null)
			{
				return;
			}
			LinkLabel.Link link = ((this.PointInLink(e.X, e.Y) != this.active_link) ? null : this.active_link);
			this.active_link.Active = false;
			this.active_link = null;
			if (link != null)
			{
				this.OnLinkClicked(new LinkLabelLinkClickedEventArgs(link, e.Button));
			}
		}

		/// <summary>Raises the <see cref="M:System.Windows.Forms.Control.OnPaint(System.Windows.Forms.PaintEventArgs)" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data. </param>
		// Token: 0x06002041 RID: 8257 RVA: 0x00078B2C File Offset: 0x00076D2C
		protected override void OnPaint(PaintEventArgs e)
		{
			base.InvokePaintBackground(this, e);
			ThemeElements.LinkLabelPainter.Draw(e.Graphics, e.ClipRectangle, this);
		}

		/// <param name="e"></param>
		// Token: 0x06002042 RID: 8258 RVA: 0x00078B58 File Offset: 0x00076D58
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002043 RID: 8259 RVA: 0x00078B64 File Offset: 0x00076D64
		protected override void OnTextAlignChanged(EventArgs e)
		{
			this.CreateLinkPieces();
			base.OnTextAlignChanged(e);
		}

		// Token: 0x06002044 RID: 8260 RVA: 0x00078B74 File Offset: 0x00076D74
		protected override void OnTextChanged(EventArgs e)
		{
			this.CreateLinkPieces();
			base.OnTextChanged(e);
		}

		/// <summary>Gets the link located at the specified client coordinates.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> representing the link located at the specified coordinates. If the point does not contain a link, null is returned.</returns>
		/// <param name="x">The horizontal coordinate of the point to search for a link. </param>
		/// <param name="y">The vertical coordinate of the point to search for a link. </param>
		// Token: 0x06002045 RID: 8261 RVA: 0x00078B84 File Offset: 0x00076D84
		protected LinkLabel.Link PointInLink(int x, int y)
		{
			for (int i = 0; i < this.sorted_links.Length; i++)
			{
				if (this.sorted_links[i].Contains(x, y))
				{
					return this.sorted_links[i];
				}
			}
			return null;
		}

		/// <summary>Processes a dialog key. </summary>
		/// <returns>true to consume the key; false to allow further processing.</returns>
		/// <param name="keyData">Key code and modifier flags. </param>
		// Token: 0x06002046 RID: 8262 RVA: 0x00078BC8 File Offset: 0x00076DC8
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if ((keyData & Keys.KeyCode) == Keys.Tab)
			{
				this.Select(true, (keyData & Keys.Shift) == Keys.None);
				return true;
			}
			return base.ProcessDialogKey(keyData);
		}

		/// <param name="directed">true to specify the direction of the control to select; otherwise, false. </param>
		/// <param name="forward">true to move forward in the tab order; false to move backward in the tab order. </param>
		// Token: 0x06002047 RID: 8263 RVA: 0x00078C00 File Offset: 0x00076E00
		protected override void Select(bool directed, bool forward)
		{
			if (directed)
			{
				if (this.focused_index != -1)
				{
					this.sorted_links[this.focused_index].Focused = false;
					this.focused_index = -1;
				}
				if (forward)
				{
					for (int i = this.focused_index + 1; i < this.sorted_links.Length; i++)
					{
						if (this.sorted_links[i].Enabled)
						{
							this.sorted_links[i].Focused = true;
							this.focused_index = i;
							base.Select(directed, forward);
							return;
						}
					}
				}
				else
				{
					if (this.focused_index == -1)
					{
						this.focused_index = this.sorted_links.Length;
					}
					for (int j = this.focused_index - 1; j >= 0; j--)
					{
						if (this.sorted_links[j].Enabled)
						{
							this.sorted_links[j].Focused = true;
							this.focused_index = j;
							base.Select(directed, forward);
							return;
						}
					}
				}
				this.focused_index = -1;
				if (base.Parent != null)
				{
					base.Parent.SelectNextControl(this, forward, false, true, true);
				}
			}
		}

		/// <summary>Performs the work of setting the bounds of this control. </summary>
		/// <param name="x">New left of the control. </param>
		/// <param name="y">New right of the control. </param>
		/// <param name="width">New width of the control. </param>
		/// <param name="height">New height of the control. </param>
		/// <param name="specified">Which values were specified. This parameter reflects user intent, not which values have changed. </param>
		// Token: 0x06002048 RID: 8264 RVA: 0x00078D1C File Offset: 0x00076F1C
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			base.SetBoundsCore(x, y, width, height, specified);
			this.CreateLinkPieces();
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x00078D34 File Offset: 0x00076F34
		protected override void WndProc(ref Message msg)
		{
			base.WndProc(ref msg);
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x00078D40 File Offset: 0x00076F40
		private ArrayList CreatePiecesFromText(int start, int len, LinkLabel.Link link)
		{
			ArrayList arrayList = new ArrayList();
			if (start + len > this.Text.Length)
			{
				len = this.Text.Length - start;
			}
			if (len < 0)
			{
				return arrayList;
			}
			string text = this.Text.Substring(start, len);
			int num = 0;
			for (int i = 0; i < text.Length; i++)
			{
				if (text.get_Chars(i) == '\n')
				{
					if (i != 0)
					{
						LinkLabel.Piece piece = new LinkLabel.Piece(start + num, i + 1 - num, text.Substring(num, i + 1 - num), link);
						arrayList.Add(piece);
					}
					num = i + 1;
				}
			}
			if (num < text.Length)
			{
				LinkLabel.Piece piece2 = new LinkLabel.Piece(start + num, text.Length - num, text.Substring(num, text.Length - num), link);
				arrayList.Add(piece2);
			}
			return arrayList;
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x00078E1C File Offset: 0x0007701C
		private void CreateLinkPieces()
		{
			if (this.Text.Length == 0)
			{
				base.SetStyle(ControlStyles.Selectable, false);
				base.TabStop = false;
				this.link_area.Start = 0;
				this.link_area.Length = 0;
				return;
			}
			if (this.Links.Count == 1 && this.Links[0].Start == 0 && this.Links[0].Length == -1)
			{
				this.Links[0].Length = this.Text.Length;
			}
			this.SortLinks();
			if (this.Links.Count > 0)
			{
				this.link_area.Start = this.Links[0].Start;
				this.link_area.Length = this.Links[0].Length;
			}
			else
			{
				this.link_area.Start = 0;
				this.link_area.Length = 0;
			}
			base.TabStop = this.LinkArea.Length > 0;
			base.SetStyle(ControlStyles.Selectable, base.TabStop);
			if (!base.IsHandleCreated)
			{
				return;
			}
			ArrayList arrayList = new ArrayList();
			int num = 0;
			for (int i = 0; i < this.sorted_links.Length; i++)
			{
				int start = this.sorted_links[i].Start;
				if (start > num)
				{
					ArrayList arrayList2 = this.CreatePiecesFromText(num, start - num, null);
					arrayList.AddRange(arrayList2);
				}
				ArrayList arrayList3 = this.CreatePiecesFromText(start, this.sorted_links[i].Length, this.sorted_links[i]);
				arrayList.AddRange(arrayList3);
				this.sorted_links[i].pieces.AddRange(arrayList3);
				num = this.sorted_links[i].Start + this.sorted_links[i].Length;
			}
			if (num < this.Text.Length)
			{
				ArrayList arrayList4 = this.CreatePiecesFromText(num, this.Text.Length - num, null);
				arrayList.AddRange(arrayList4);
			}
			this.pieces = new LinkLabel.Piece[arrayList.Count];
			arrayList.CopyTo(this.pieces, 0);
			CharacterRange[] array = new CharacterRange[this.pieces.Length];
			for (int j = 0; j < this.pieces.Length; j++)
			{
				array[j] = new CharacterRange(this.pieces[j].start, this.pieces[j].length);
			}
			this.string_format.SetMeasurableCharacterRanges(array);
			Region[] array2 = TextRenderer.MeasureCharacterRanges(this.Text, ThemeEngine.Current.GetLinkFont(this), base.PaddingClientRectangle, this.string_format);
			for (int k = 0; k < this.pieces.Length; k++)
			{
				this.pieces[k].region = array2[k];
				this.pieces[k].region.Translate(this.Padding.Left, this.Padding.Top);
			}
			base.Invalidate();
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x00079148 File Offset: 0x00077348
		private void SortLinks()
		{
			if (this.sorted_links != null)
			{
				return;
			}
			this.sorted_links = new LinkLabel.Link[this.Links.Count];
			this.Links.CopyTo(this.sorted_links, 0);
			Array.Sort(this.sorted_links, new LinkLabel.LinkComparer());
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x0007919C File Offset: 0x0007739C
		private void CheckLinks()
		{
			this.SortLinks();
			int num = 0;
			for (int i = 0; i < this.sorted_links.Length; i++)
			{
				if (this.sorted_links[i].Start < num)
				{
					throw new InvalidOperationException("Overlapping link regions.");
				}
				num = this.sorted_links[i].Start + this.sorted_links[i].Length;
			}
		}

		/// <summary>Gets or sets a value that determines whether to use the <see cref="T:System.Drawing.Graphics" /> class (GDI+) or the <see cref="T:System.Windows.Forms.TextRenderer" /> class (GDI) to render text.</summary>
		/// <returns>true if the <see cref="T:System.Drawing.Graphics" /> class should be used to perform text rendering for compatibility with versions 1.0 and 1.1. of the .NET Framework; otherwise, false. The default is false.</returns>
		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x0600204E RID: 8270 RVA: 0x00079204 File Offset: 0x00077404
		// (set) Token: 0x0600204F RID: 8271 RVA: 0x0007920C File Offset: 0x0007740C
		[RefreshProperties(2)]
		public new bool UseCompatibleTextRendering
		{
			get
			{
				return this.use_compatible_text_rendering;
			}
			set
			{
				this.use_compatible_text_rendering = value;
			}
		}

		// Token: 0x04001171 RID: 4465
		private Color active_link_color;

		// Token: 0x04001172 RID: 4466
		private Color disabled_link_color;

		// Token: 0x04001173 RID: 4467
		private Color link_color;

		// Token: 0x04001174 RID: 4468
		private Color visited_color;

		// Token: 0x04001175 RID: 4469
		private LinkArea link_area;

		// Token: 0x04001176 RID: 4470
		private LinkBehavior link_behavior;

		// Token: 0x04001177 RID: 4471
		private LinkLabel.LinkCollection link_collection;

		// Token: 0x04001178 RID: 4472
		private ArrayList links = new ArrayList();

		// Token: 0x04001179 RID: 4473
		internal LinkLabel.Link[] sorted_links;

		// Token: 0x0400117A RID: 4474
		private bool link_visited;

		// Token: 0x0400117B RID: 4475
		internal LinkLabel.Piece[] pieces;

		// Token: 0x0400117C RID: 4476
		private Cursor override_cursor;

		// Token: 0x0400117D RID: 4477
		private DialogResult dialog_result;

		// Token: 0x0400117E RID: 4478
		private LinkLabel.Link active_link;

		// Token: 0x0400117F RID: 4479
		private LinkLabel.Link hovered_link;

		// Token: 0x04001180 RID: 4480
		private int focused_index;

		// Token: 0x0200020B RID: 523
		internal class Piece
		{
			// Token: 0x06002050 RID: 8272 RVA: 0x00079218 File Offset: 0x00077418
			public Piece(int start, int length, string text, LinkLabel.Link link)
			{
				this.start = start;
				this.length = length;
				this.text = text;
				this.link = link;
			}

			// Token: 0x04001182 RID: 4482
			public string text;

			// Token: 0x04001183 RID: 4483
			public int start;

			// Token: 0x04001184 RID: 4484
			public int length;

			// Token: 0x04001185 RID: 4485
			public LinkLabel.Link link;

			// Token: 0x04001186 RID: 4486
			public Region region;
		}

		/// <summary>Represents a link within a <see cref="T:System.Windows.Forms.LinkLabel" /> control.</summary>
		// Token: 0x0200020C RID: 524
		[TypeConverter(typeof(LinkConverter))]
		public class Link
		{
			// Token: 0x06002051 RID: 8273 RVA: 0x00079240 File Offset: 0x00077440
			internal Link(LinkLabel owner)
			{
				this.focused = false;
				this.enabled = true;
				this.visited = false;
				this.length = (this.start = 0);
				this.linkData = null;
				this.owner = owner;
				this.pieces = new ArrayList();
				this.name = string.Empty;
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabel.Link" /> class. </summary>
			// Token: 0x06002052 RID: 8274 RVA: 0x0007929C File Offset: 0x0007749C
			public Link()
			{
				this.enabled = true;
				this.name = string.Empty;
				this.pieces = new ArrayList();
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabel.Link" /> class with the specified starting location and number of characters after the starting location within the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
			/// <param name="start">The zero-based starting location of the link area within the text of the <see cref="T:System.Windows.Forms.LinkLabel" />.</param>
			/// <param name="length">The number of characters, after the starting character, to include in the link area.</param>
			// Token: 0x06002053 RID: 8275 RVA: 0x000792C4 File Offset: 0x000774C4
			public Link(int start, int length)
				: this()
			{
				this.start = start;
				this.length = length;
			}

			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabel.Link" /> class with the specified starting location, number of characters after the starting location within the <see cref="T:System.Windows.Forms.LinkLabel" />, and the data associated with the link.</summary>
			/// <param name="start">The zero-based starting location of the link area within the text of the <see cref="T:System.Windows.Forms.LinkLabel" />.</param>
			/// <param name="length">The number of characters, after the starting character, to include in the link area.</param>
			/// <param name="linkData">The data associated with the link.</param>
			// Token: 0x06002054 RID: 8276 RVA: 0x000792DC File Offset: 0x000774DC
			public Link(int start, int length, object linkData)
				: this(start, length)
			{
				this.linkData = linkData;
			}

			/// <summary>Gets or sets a text description of the link.</summary>
			/// <returns>A <see cref="T:System.String" /> representing a text description of the link.</returns>
			// Token: 0x170007FB RID: 2043
			// (get) Token: 0x06002055 RID: 8277 RVA: 0x000792F0 File Offset: 0x000774F0
			// (set) Token: 0x06002056 RID: 8278 RVA: 0x000792F8 File Offset: 0x000774F8
			public string Description
			{
				get
				{
					return this.description;
				}
				set
				{
					this.description = value;
				}
			}

			/// <summary>Gets or sets the name of the <see cref="T:System.Windows.Forms.LinkLabel.Link" />.</summary>
			/// <returns>A <see cref="T:System.String" /> representing the name of the <see cref="T:System.Windows.Forms.LinkLabel.Link" />. The default value is the empty string ("").</returns>
			// Token: 0x170007FC RID: 2044
			// (get) Token: 0x06002057 RID: 8279 RVA: 0x00079304 File Offset: 0x00077504
			// (set) Token: 0x06002058 RID: 8280 RVA: 0x0007930C File Offset: 0x0007750C
			[DefaultValue("")]
			public string Name
			{
				get
				{
					return this.name;
				}
				set
				{
					this.name = value;
				}
			}

			/// <summary>Gets or sets the object that contains data about the <see cref="T:System.Windows.Forms.LinkLabel.Link" />.</summary>
			/// <returns>An <see cref="T:System.Object" /> that contains data about the control. The default is null.</returns>
			// Token: 0x170007FD RID: 2045
			// (get) Token: 0x06002059 RID: 8281 RVA: 0x00079318 File Offset: 0x00077518
			// (set) Token: 0x0600205A RID: 8282 RVA: 0x00079320 File Offset: 0x00077520
			[Bindable(true)]
			[TypeConverter(typeof(StringConverter))]
			[DefaultValue(null)]
			[Localizable(false)]
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

			/// <summary>Gets or sets a value indicating whether the link is enabled.</summary>
			/// <returns>true if the link is enabled; otherwise, false.</returns>
			// Token: 0x170007FE RID: 2046
			// (get) Token: 0x0600205B RID: 8283 RVA: 0x0007932C File Offset: 0x0007752C
			// (set) Token: 0x0600205C RID: 8284 RVA: 0x00079334 File Offset: 0x00077534
			[DefaultValue(true)]
			public bool Enabled
			{
				get
				{
					return this.enabled;
				}
				set
				{
					if (this.enabled != value)
					{
						this.Invalidate();
					}
					this.enabled = value;
				}
			}

			/// <summary>Gets or sets the number of characters in the link text.</summary>
			/// <returns>The number of characters, including spaces, in the link text.</returns>
			// Token: 0x170007FF RID: 2047
			// (get) Token: 0x0600205D RID: 8285 RVA: 0x00079350 File Offset: 0x00077550
			// (set) Token: 0x0600205E RID: 8286 RVA: 0x00079378 File Offset: 0x00077578
			public int Length
			{
				get
				{
					if (this.length == -1)
					{
						return this.owner.Text.Length;
					}
					return this.length;
				}
				set
				{
					if (this.length == value)
					{
						return;
					}
					this.length = value;
					this.owner.CreateLinkPieces();
				}
			}

			/// <summary>Gets or sets the data associated with the link.</summary>
			/// <returns>An <see cref="T:System.Object" /> representing the data associated with the link.</returns>
			// Token: 0x17000800 RID: 2048
			// (get) Token: 0x0600205F RID: 8287 RVA: 0x0007939C File Offset: 0x0007759C
			// (set) Token: 0x06002060 RID: 8288 RVA: 0x000793A4 File Offset: 0x000775A4
			[DefaultValue(null)]
			public object LinkData
			{
				get
				{
					return this.linkData;
				}
				set
				{
					this.linkData = value;
				}
			}

			/// <summary>Gets or sets the starting location of the link within the text of the <see cref="T:System.Windows.Forms.LinkLabel" />.</summary>
			/// <returns>The location within the text of the <see cref="T:System.Windows.Forms.LinkLabel" /> control where the link starts.</returns>
			// Token: 0x17000801 RID: 2049
			// (get) Token: 0x06002061 RID: 8289 RVA: 0x000793B0 File Offset: 0x000775B0
			// (set) Token: 0x06002062 RID: 8290 RVA: 0x000793B8 File Offset: 0x000775B8
			public int Start
			{
				get
				{
					return this.start;
				}
				set
				{
					if (this.start == value)
					{
						return;
					}
					this.start = value;
					this.owner.sorted_links = null;
					this.owner.CreateLinkPieces();
				}
			}

			/// <summary>Gets or sets a value indicating whether the user has visited the link.</summary>
			/// <returns>true if the link has been visited; otherwise, false.</returns>
			// Token: 0x17000802 RID: 2050
			// (get) Token: 0x06002063 RID: 8291 RVA: 0x000793E8 File Offset: 0x000775E8
			// (set) Token: 0x06002064 RID: 8292 RVA: 0x000793F0 File Offset: 0x000775F0
			[DefaultValue(false)]
			public bool Visited
			{
				get
				{
					return this.visited;
				}
				set
				{
					if (this.visited != value)
					{
						this.Invalidate();
					}
					this.visited = value;
				}
			}

			// Token: 0x17000803 RID: 2051
			// (get) Token: 0x06002065 RID: 8293 RVA: 0x0007940C File Offset: 0x0007760C
			// (set) Token: 0x06002066 RID: 8294 RVA: 0x00079414 File Offset: 0x00077614
			internal bool Hovered
			{
				get
				{
					return this.hovered;
				}
				set
				{
					if (this.hovered != value)
					{
						this.Invalidate();
					}
					this.hovered = value;
				}
			}

			// Token: 0x17000804 RID: 2052
			// (get) Token: 0x06002067 RID: 8295 RVA: 0x00079430 File Offset: 0x00077630
			// (set) Token: 0x06002068 RID: 8296 RVA: 0x00079438 File Offset: 0x00077638
			internal bool Focused
			{
				get
				{
					return this.focused;
				}
				set
				{
					if (this.focused != value)
					{
						this.Invalidate();
					}
					this.focused = value;
				}
			}

			// Token: 0x17000805 RID: 2053
			// (get) Token: 0x06002069 RID: 8297 RVA: 0x00079454 File Offset: 0x00077654
			// (set) Token: 0x0600206A RID: 8298 RVA: 0x0007945C File Offset: 0x0007765C
			internal bool Active
			{
				get
				{
					return this.active;
				}
				set
				{
					if (this.active != value)
					{
						this.Invalidate();
					}
					this.active = value;
				}
			}

			// Token: 0x17000806 RID: 2054
			// (set) Token: 0x0600206B RID: 8299 RVA: 0x00079478 File Offset: 0x00077678
			internal LinkLabel Owner
			{
				set
				{
					this.owner = value;
				}
			}

			// Token: 0x0600206C RID: 8300 RVA: 0x00079484 File Offset: 0x00077684
			private void Invalidate()
			{
				for (int i = 0; i < this.pieces.Count; i++)
				{
					this.owner.Invalidate(((LinkLabel.Piece)this.pieces[i]).region);
				}
			}

			// Token: 0x0600206D RID: 8301 RVA: 0x000794D0 File Offset: 0x000776D0
			internal bool Contains(int x, int y)
			{
				foreach (object obj in this.pieces)
				{
					LinkLabel.Piece piece = (LinkLabel.Piece)obj;
					if (piece.region.IsVisible(new Point(x, y)))
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x04001187 RID: 4487
			private bool enabled;

			// Token: 0x04001188 RID: 4488
			internal int length;

			// Token: 0x04001189 RID: 4489
			private object linkData;

			// Token: 0x0400118A RID: 4490
			private int start;

			// Token: 0x0400118B RID: 4491
			private bool visited;

			// Token: 0x0400118C RID: 4492
			private LinkLabel owner;

			// Token: 0x0400118D RID: 4493
			private bool hovered;

			// Token: 0x0400118E RID: 4494
			internal ArrayList pieces;

			// Token: 0x0400118F RID: 4495
			private bool focused;

			// Token: 0x04001190 RID: 4496
			private bool active;

			// Token: 0x04001191 RID: 4497
			private string description;

			// Token: 0x04001192 RID: 4498
			private string name;

			// Token: 0x04001193 RID: 4499
			private object tag;
		}

		// Token: 0x0200020D RID: 525
		private class LinkComparer : IComparer
		{
			// Token: 0x0600206F RID: 8303 RVA: 0x00079564 File Offset: 0x00077764
			public int Compare(object x, object y)
			{
				LinkLabel.Link link = (LinkLabel.Link)x;
				LinkLabel.Link link2 = (LinkLabel.Link)y;
				return link.Start - link2.Start;
			}
		}

		/// <summary>Represents the collection of links within a <see cref="T:System.Windows.Forms.LinkLabel" /> control.</summary>
		// Token: 0x0200020E RID: 526
		public class LinkCollection : ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.LinkLabel.LinkCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.LinkLabel" /> control that owns the collection. </param>
			// Token: 0x06002070 RID: 8304 RVA: 0x0007958C File Offset: 0x0007778C
			public LinkCollection(LinkLabel owner)
			{
				if (owner == null)
				{
					throw new ArgumentNullException("owner");
				}
				this.owner = owner;
			}

			/// <summary>For a description of this member, see .<see cref="P:System.Collections.IList.IsFixedSize" />.</summary>
			/// <returns>true if the <see cref="T:System.Collections.IList" /> has a fixed size; otherwise, false.</returns>
			// Token: 0x17000807 RID: 2055
			// (get) Token: 0x06002071 RID: 8305 RVA: 0x000795AC File Offset: 0x000777AC
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IList.Item(System.Int32)" />.</summary>
			/// <returns>The element at the specified index.</returns>
			/// <param name="index">The zero-based index of the element to get or set.</param>
			// Token: 0x17000808 RID: 2056
			// (get) Token: 0x06002072 RID: 8306 RVA: 0x000795B0 File Offset: 0x000777B0
			// (set) Token: 0x06002073 RID: 8307 RVA: 0x000795C4 File Offset: 0x000777C4
			object IList.Item
			{
				get
				{
					return this.owner.links[index];
				}
				set
				{
					this.owner.links[index] = value;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
			// Token: 0x17000809 RID: 2057
			// (get) Token: 0x06002074 RID: 8308 RVA: 0x000795D8 File Offset: 0x000777D8
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
			/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
			// Token: 0x1700080A RID: 2058
			// (get) Token: 0x06002075 RID: 8309 RVA: 0x000795DC File Offset: 0x000777DC
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
			/// <param name="dest">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
			// Token: 0x06002076 RID: 8310 RVA: 0x000795E0 File Offset: 0x000777E0
			void ICollection.CopyTo(Array dest, int index)
			{
				this.owner.links.CopyTo(dest, index);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Add(System.Object)" />.</summary>
			/// <returns>The position into which the new element was inserted.</returns>
			/// <param name="value">The <see cref="T:System.Object" /> to add to the <see cref="T:System.Collections.IList" />.</param>
			// Token: 0x06002077 RID: 8311 RVA: 0x000795F4 File Offset: 0x000777F4
			int IList.Add(object value)
			{
				int num = this.owner.links.Add(value);
				this.owner.sorted_links = null;
				this.owner.CheckLinks();
				this.owner.CreateLinkPieces();
				return num;
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Contains(System.Object)" />.</summary>
			/// <returns>true if the <see cref="T:System.Object" /> is found in the <see cref="T:System.Collections.IList" />; otherwise, false.</returns>
			/// <param name="link">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
			// Token: 0x06002078 RID: 8312 RVA: 0x00079638 File Offset: 0x00077838
			bool IList.Contains(object link)
			{
				return this.Contains((LinkLabel.Link)link);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.IndexOf(System.Object)" />.</summary>
			/// <returns>The index of the <paramref name="link" /> parameter, if found in the list; otherwise, -1.</returns>
			/// <param name="link">The <see cref="T:System.Object" /> to locate in the <see cref="T:System.Collections.IList" />.</param>
			// Token: 0x06002079 RID: 8313 RVA: 0x00079648 File Offset: 0x00077848
			int IList.IndexOf(object link)
			{
				return this.owner.links.IndexOf(link);
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Insert(System.Int32,System.Object)" />.</summary>
			/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
			/// <param name="value">The <see cref="T:System.Object" /> to insert into the <see cref="T:System.Collections.IList" />.</param>
			// Token: 0x0600207A RID: 8314 RVA: 0x0007965C File Offset: 0x0007785C
			void IList.Insert(int index, object value)
			{
				this.owner.links.Insert(index, value);
				this.owner.sorted_links = null;
				this.owner.CheckLinks();
				this.owner.CreateLinkPieces();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IList.Remove(System.Object)" />.</summary>
			/// <param name="value">The <see cref="T:System.Object" /> to remove from the <see cref="T:System.Collections.IList" />.</param>
			// Token: 0x0600207B RID: 8315 RVA: 0x000796A0 File Offset: 0x000778A0
			void IList.Remove(object value)
			{
				this.Remove((LinkLabel.Link)value);
			}

			/// <summary>Gets the number of links in the collection.</summary>
			/// <returns>The number of links in the collection.</returns>
			// Token: 0x1700080B RID: 2059
			// (get) Token: 0x0600207C RID: 8316 RVA: 0x000796B0 File Offset: 0x000778B0
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.owner.links.Count;
				}
			}

			/// <summary>Gets a value indicating whether this collection is read-only.</summary>
			/// <returns>true if the collection is read-only; otherwise, false.</returns>
			// Token: 0x1700080C RID: 2060
			// (get) Token: 0x0600207D RID: 8317 RVA: 0x000796C4 File Offset: 0x000778C4
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets and sets the link at the specified index within the collection.</summary>
			/// <returns>An object representing the link located at the specified index within the collection.</returns>
			/// <param name="index">The index of the link in the collection to get. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="index" /> is a negative value or greater than the number of items in the collection. </exception>
			// Token: 0x1700080D RID: 2061
			public virtual LinkLabel.Link this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException();
					}
					return (LinkLabel.Link)this.owner.links[index];
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException();
					}
					this.owner.links[index] = value;
				}
			}

			/// <summary>Gets a link with the specified key from the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.LinkLabel.Link" /> with the specified key within the collection.</returns>
			/// <param name="key">The name of the link to retrieve from the collection.</param>
			// Token: 0x1700080E RID: 2062
			public virtual LinkLabel.Link this[string key]
			{
				get
				{
					if (string.IsNullOrEmpty(key))
					{
						return null;
					}
					foreach (object obj in this.owner.links)
					{
						LinkLabel.Link link = (LinkLabel.Link)obj;
						if (string.Compare(link.Name, key, true) == 0)
						{
							return link;
						}
					}
					return null;
				}
			}

			/// <summary>Adds a link with the specified value to the collection.</summary>
			/// <returns>The zero-based index where the link specified by the <paramref name="value" /> parameter is located in the collection.</returns>
			/// <param name="value">A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> representing the link to add.</param>
			// Token: 0x06002081 RID: 8321 RVA: 0x000797C4 File Offset: 0x000779C4
			public int Add(LinkLabel.Link value)
			{
				value.Owner = this.owner;
				if (this.IsDefault)
				{
					this.owner.links.Clear();
				}
				int num = this.owner.links.Add(value);
				this.links_added = true;
				this.owner.sorted_links = null;
				this.owner.CheckLinks();
				this.owner.CreateLinkPieces();
				return num;
			}

			/// <summary>Adds a link to the collection.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> representing the link that was created and added to the collection.</returns>
			/// <param name="start">The starting character within the text of the label where the link is created. </param>
			/// <param name="length">The number of characters after the starting character to include in the link text. </param>
			// Token: 0x06002082 RID: 8322 RVA: 0x00079834 File Offset: 0x00077A34
			public LinkLabel.Link Add(int start, int length)
			{
				return this.Add(start, length, null);
			}

			// Token: 0x1700080F RID: 2063
			// (get) Token: 0x06002083 RID: 8323 RVA: 0x00079840 File Offset: 0x00077A40
			internal bool IsDefault
			{
				get
				{
					return this.Count == 1 && this[0].Start == 0 && this[0].length == -1;
				}
			}

			/// <summary>Adds a link to the collection with information to associate with the link.</summary>
			/// <returns>A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> representing the link that was created and added to the collection.</returns>
			/// <param name="start">The starting character within the text of the label where the link is created. </param>
			/// <param name="length">The number of characters after the starting character to include in the link text. </param>
			/// <param name="linkData">The object containing the information to associate with the link. </param>
			// Token: 0x06002084 RID: 8324 RVA: 0x0007987C File Offset: 0x00077A7C
			public LinkLabel.Link Add(int start, int length, object linkData)
			{
				int num = this.Add(new LinkLabel.Link(this.owner)
				{
					Length = length,
					Start = start,
					LinkData = linkData
				});
				return (LinkLabel.Link)this.owner.links[num];
			}

			/// <summary>Clears all links from the collection.</summary>
			// Token: 0x06002085 RID: 8325 RVA: 0x000798C8 File Offset: 0x00077AC8
			public virtual void Clear()
			{
				this.owner.links.Clear();
				this.owner.sorted_links = null;
				this.owner.CreateLinkPieces();
			}

			/// <summary>Determines whether the specified link is within the collection.</summary>
			/// <returns>true if the specified link is within the collection; otherwise, false.</returns>
			/// <param name="link">A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> representing the link to search for in the collection. </param>
			// Token: 0x06002086 RID: 8326 RVA: 0x000798F4 File Offset: 0x00077AF4
			public bool Contains(LinkLabel.Link link)
			{
				return this.owner.links.Contains(link);
			}

			/// <summary>Returns a value indicating whether the collection contains a link with the specified key.</summary>
			/// <returns>true if the collection contains an item with the specified key; otherwise, false.</returns>
			/// <param name="key">The link to search for in the collection.</param>
			// Token: 0x06002087 RID: 8327 RVA: 0x00079908 File Offset: 0x00077B08
			public virtual bool ContainsKey(string key)
			{
				return this[key] != null;
			}

			/// <summary>Returns an enumerator to use to iterate through the link collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that represents the link collection.</returns>
			// Token: 0x06002088 RID: 8328 RVA: 0x00079918 File Offset: 0x00077B18
			public IEnumerator GetEnumerator()
			{
				return this.owner.links.GetEnumerator();
			}

			/// <summary>Returns the index of the specified link within the collection.</summary>
			/// <returns>The zero-based index where the link is located within the collection; otherwise, negative one (-1).</returns>
			/// <param name="link">A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> representing the link to search for in the collection. </param>
			// Token: 0x06002089 RID: 8329 RVA: 0x0007992C File Offset: 0x00077B2C
			public int IndexOf(LinkLabel.Link link)
			{
				return this.owner.links.IndexOf(link);
			}

			/// <summary>Retrieves the zero-based index of the first occurrence of the specified key within the entire collection.</summary>
			/// <returns>The zero-based index of the first occurrence of value within the entire collection, if found; otherwise, -1.</returns>
			/// <param name="key">The key to search the collection for.</param>
			// Token: 0x0600208A RID: 8330 RVA: 0x00079940 File Offset: 0x00077B40
			public virtual int IndexOfKey(string key)
			{
				if (string.IsNullOrEmpty(key))
				{
					return -1;
				}
				return this.IndexOf(this[key]);
			}

			/// <summary>Gets a value indicating whether links have been added to the <see cref="T:System.Windows.Forms.LinkLabel.LinkCollection" />. </summary>
			/// <returns>true if links have been added to the <see cref="T:System.Windows.Forms.LinkLabel.LinkCollection" />; otherwise, false.</returns>
			// Token: 0x17000810 RID: 2064
			// (get) Token: 0x0600208B RID: 8331 RVA: 0x0007995C File Offset: 0x00077B5C
			public bool LinksAdded
			{
				get
				{
					return this.links_added;
				}
			}

			/// <summary>Removes the specified link from the collection.</summary>
			/// <param name="value">A <see cref="T:System.Windows.Forms.LinkLabel.Link" /> that represents the link to remove from the collection. </param>
			// Token: 0x0600208C RID: 8332 RVA: 0x00079964 File Offset: 0x00077B64
			public void Remove(LinkLabel.Link value)
			{
				this.owner.links.Remove(value);
				this.owner.sorted_links = null;
				this.owner.CreateLinkPieces();
			}

			/// <summary>Removes the link with the specified key. </summary>
			/// <param name="key">The key of the link to remove.</param>
			// Token: 0x0600208D RID: 8333 RVA: 0x0007999C File Offset: 0x00077B9C
			public virtual void RemoveByKey(string key)
			{
				this.Remove(this[key]);
			}

			/// <summary>Removes a link at a specified location within the collection.</summary>
			/// <param name="index">The zero-based index of the item to remove from the collection. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">The value of <paramref name="index" /> is a negative value or greater than the number of items in the collection. </exception>
			// Token: 0x0600208E RID: 8334 RVA: 0x000799AC File Offset: 0x00077BAC
			public void RemoveAt(int index)
			{
				if (index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("Invalid value for array index");
				}
				this.owner.links.Remove(this.owner.links[index]);
				this.owner.sorted_links = null;
				this.owner.CreateLinkPieces();
			}

			// Token: 0x04001194 RID: 4500
			private LinkLabel owner;

			// Token: 0x04001195 RID: 4501
			private bool links_added;
		}
	}
}
