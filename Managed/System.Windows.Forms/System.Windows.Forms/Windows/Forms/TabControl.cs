using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Forms.Theming;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	/// <summary>Manages a related set of tab pages.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020002F6 RID: 758
	[ComVisible(true)]
	[DefaultProperty("TabPages")]
	[Designer("System.Windows.Forms.Design.TabControlDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ClassInterface(1)]
	[DefaultEvent("SelectedIndexChanged")]
	public class TabControl : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabControl" /> class.</summary>
		// Token: 0x06003243 RID: 12867 RVA: 0x000BF37C File Offset: 0x000BD57C
		public TabControl()
		{
			this.tab_pages = new TabControl.TabPageCollection(this);
			base.SetStyle(ControlStyles.UserPaint, false);
			this.padding = ThemeEngine.Current.TabControlDefaultPadding;
			this.item_size = ThemeEngine.Current.TabControlDefaultItemSize;
			base.MouseDown += this.MouseDownHandler;
			base.MouseLeave += new EventHandler(this.OnMouseLeave);
			base.MouseMove += this.OnMouseMove;
			base.MouseUp += this.MouseUpHandler;
			base.SizeChanged += new EventHandler(this.SizeChangedHandler);
		}

		// Token: 0x06003244 RID: 12868 RVA: 0x000BF440 File Offset: 0x000BD640
		// Note: this type is marked as 'beforefieldinit'.
		static TabControl()
		{
			TabControl.UIAHorizontallyScrollableChangedEvent = new object();
			TabControl.UIAHorizontallyScrolledEvent = new object();
			TabControl.DrawItemEvent = new object();
			TabControl.SelectedIndexChangedEvent = new object();
			TabControl.SelectedEvent = new object();
			TabControl.DeselectedEvent = new object();
			TabControl.SelectingEvent = new object();
			TabControl.DeselectingEvent = new object();
			TabControl.RightToLeftLayoutChangedEvent = new object();
		}

		// Token: 0x14000315 RID: 789
		// (add) Token: 0x06003245 RID: 12869 RVA: 0x000BF4A8 File Offset: 0x000BD6A8
		// (remove) Token: 0x06003246 RID: 12870 RVA: 0x000BF4BC File Offset: 0x000BD6BC
		internal event EventHandler UIAHorizontallyScrollableChanged
		{
			add
			{
				base.Events.AddHandler(TabControl.UIAHorizontallyScrollableChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.UIAHorizontallyScrollableChangedEvent, value);
			}
		}

		// Token: 0x14000316 RID: 790
		// (add) Token: 0x06003247 RID: 12871 RVA: 0x000BF4D0 File Offset: 0x000BD6D0
		// (remove) Token: 0x06003248 RID: 12872 RVA: 0x000BF4E4 File Offset: 0x000BD6E4
		internal event EventHandler UIAHorizontallyScrolled
		{
			add
			{
				base.Events.AddHandler(TabControl.UIAHorizontallyScrolledEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.UIAHorizontallyScrolledEvent, value);
			}
		}

		/// <summary>This event is not meaningful for this control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000317 RID: 791
		// (add) Token: 0x06003249 RID: 12873 RVA: 0x000BF4F8 File Offset: 0x000BD6F8
		// (remove) Token: 0x0600324A RID: 12874 RVA: 0x000BF504 File Offset: 0x000BD704
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabControl.BackgroundImage" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000318 RID: 792
		// (add) Token: 0x0600324B RID: 12875 RVA: 0x000BF510 File Offset: 0x000BD710
		// (remove) Token: 0x0600324C RID: 12876 RVA: 0x000BF51C File Offset: 0x000BD71C
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabControl.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000319 RID: 793
		// (add) Token: 0x0600324D RID: 12877 RVA: 0x000BF528 File Offset: 0x000BD728
		// (remove) Token: 0x0600324E RID: 12878 RVA: 0x000BF534 File Offset: 0x000BD734
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabControl.ForeColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400031A RID: 794
		// (add) Token: 0x0600324F RID: 12879 RVA: 0x000BF540 File Offset: 0x000BD740
		// (remove) Token: 0x06003250 RID: 12880 RVA: 0x000BF54C File Offset: 0x000BD74C
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

		/// <summary>This event is not meaningful for this control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400031B RID: 795
		// (add) Token: 0x06003251 RID: 12881 RVA: 0x000BF558 File Offset: 0x000BD758
		// (remove) Token: 0x06003252 RID: 12882 RVA: 0x000BF564 File Offset: 0x000BD764
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabControl.Text" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400031C RID: 796
		// (add) Token: 0x06003253 RID: 12883 RVA: 0x000BF570 File Offset: 0x000BD770
		// (remove) Token: 0x06003254 RID: 12884 RVA: 0x000BF57C File Offset: 0x000BD77C
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

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.TabControl" /> needs to paint each of its tabs if the <see cref="P:System.Windows.Forms.TabControl.DrawMode" /> property is set to <see cref="F:System.Windows.Forms.TabDrawMode.OwnerDrawFixed" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400031D RID: 797
		// (add) Token: 0x06003255 RID: 12885 RVA: 0x000BF588 File Offset: 0x000BD788
		// (remove) Token: 0x06003256 RID: 12886 RVA: 0x000BF59C File Offset: 0x000BD79C
		public event DrawItemEventHandler DrawItem
		{
			add
			{
				base.Events.AddHandler(TabControl.DrawItemEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.DrawItemEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TabControl.SelectedIndex" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400031E RID: 798
		// (add) Token: 0x06003257 RID: 12887 RVA: 0x000BF5B0 File Offset: 0x000BD7B0
		// (remove) Token: 0x06003258 RID: 12888 RVA: 0x000BF5C4 File Offset: 0x000BD7C4
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(TabControl.SelectedIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.SelectedIndexChangedEvent, value);
			}
		}

		/// <summary>Occurs when a tab is selected.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400031F RID: 799
		// (add) Token: 0x06003259 RID: 12889 RVA: 0x000BF5D8 File Offset: 0x000BD7D8
		// (remove) Token: 0x0600325A RID: 12890 RVA: 0x000BF5EC File Offset: 0x000BD7EC
		public event TabControlEventHandler Selected
		{
			add
			{
				base.Events.AddHandler(TabControl.SelectedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.SelectedEvent, value);
			}
		}

		/// <summary>Occurs when a tab is deselected. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000320 RID: 800
		// (add) Token: 0x0600325B RID: 12891 RVA: 0x000BF600 File Offset: 0x000BD800
		// (remove) Token: 0x0600325C RID: 12892 RVA: 0x000BF614 File Offset: 0x000BD814
		public event TabControlEventHandler Deselected
		{
			add
			{
				base.Events.AddHandler(TabControl.DeselectedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.DeselectedEvent, value);
			}
		}

		/// <summary>Occurs before a tab is selected, enabling a handler to cancel the tab change.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000321 RID: 801
		// (add) Token: 0x0600325D RID: 12893 RVA: 0x000BF628 File Offset: 0x000BD828
		// (remove) Token: 0x0600325E RID: 12894 RVA: 0x000BF63C File Offset: 0x000BD83C
		public event TabControlCancelEventHandler Selecting
		{
			add
			{
				base.Events.AddHandler(TabControl.SelectingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.SelectingEvent, value);
			}
		}

		/// <summary>Occurs before a tab is deselected, enabling a handler to cancel the tab change.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000322 RID: 802
		// (add) Token: 0x0600325F RID: 12895 RVA: 0x000BF650 File Offset: 0x000BD850
		// (remove) Token: 0x06003260 RID: 12896 RVA: 0x000BF664 File Offset: 0x000BD864
		public event TabControlCancelEventHandler Deselecting
		{
			add
			{
				base.Events.AddHandler(TabControl.DeselectingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.DeselectingEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TabControl.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x14000323 RID: 803
		// (add) Token: 0x06003261 RID: 12897 RVA: 0x000BF678 File Offset: 0x000BD878
		// (remove) Token: 0x06003262 RID: 12898 RVA: 0x000BF68C File Offset: 0x000BD88C
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.Events.AddHandler(TabControl.RightToLeftLayoutChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.RightToLeftLayoutChangedEvent, value);
			}
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x000BF6A0 File Offset: 0x000BD8A0
		internal void OnUIAHorizontallyScrollableChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TabControl.UIAHorizontallyScrollableChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x000BF6D4 File Offset: 0x000BD8D4
		internal void OnUIAHorizontallyScrolled(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TabControl.UIAHorizontallyScrolledEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06003265 RID: 12901 RVA: 0x000BF708 File Offset: 0x000BD908
		internal double UIAHorizontalViewSize
		{
			get
			{
				return (double)(this.LeftScrollButtonArea.Left * 100 / this.TabPages[this.TabCount - 1].TabBounds.Right);
			}
		}

		/// <summary>Gets or sets the area of the control (for example, along the top) where the tabs are aligned.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TabAlignment" /> values. The default is Top.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:System.Windows.Forms.TabAlignment" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06003266 RID: 12902 RVA: 0x000BF748 File Offset: 0x000BD948
		// (set) Token: 0x06003267 RID: 12903 RVA: 0x000BF750 File Offset: 0x000BD950
		[RefreshProperties(1)]
		[Localizable(true)]
		[DefaultValue(TabAlignment.Top)]
		public TabAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				if (this.alignment == value)
				{
					return;
				}
				this.alignment = value;
				if (this.alignment == TabAlignment.Left || this.alignment == TabAlignment.Right)
				{
					this.multiline = true;
				}
				this.Redraw();
			}
		}

		/// <summary>Gets or sets the visual appearance of the control's tabs.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TabAppearance" /> values. The default is Normal.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:System.Windows.Forms.TabAppearance" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06003268 RID: 12904 RVA: 0x000BF78C File Offset: 0x000BD98C
		// (set) Token: 0x06003269 RID: 12905 RVA: 0x000BF794 File Offset: 0x000BD994
		[Localizable(true)]
		[DefaultValue(TabAppearance.Normal)]
		public TabAppearance Appearance
		{
			get
			{
				return this.appearance;
			}
			set
			{
				if (this.appearance == value)
				{
					return;
				}
				this.appearance = value;
				this.Redraw();
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>Always <see cref="P:System.Drawing.SystemColors.Control" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x0600326A RID: 12906 RVA: 0x000BF7B0 File Offset: 0x000BD9B0
		// (set) Token: 0x0600326B RID: 12907 RVA: 0x000BF7BC File Offset: 0x000BD9BC
		[Browsable(false)]
		[EditorBrowsable(1)]
		public override Color BackColor
		{
			get
			{
				return ThemeEngine.Current.ColorControl;
			}
			set
			{
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x0600326C RID: 12908 RVA: 0x000BF7C0 File Offset: 0x000BD9C0
		// (set) Token: 0x0600326D RID: 12909 RVA: 0x000BF7C8 File Offset: 0x000BD9C8
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

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x0600326E RID: 12910 RVA: 0x000BF7D4 File Offset: 0x000BD9D4
		// (set) Token: 0x0600326F RID: 12911 RVA: 0x000BF7DC File Offset: 0x000BD9DC
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

		/// <summary>Gets the display area of the control's tab pages.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the display area of the tab pages.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06003270 RID: 12912 RVA: 0x000BF7E8 File Offset: 0x000BD9E8
		public override Rectangle DisplayRectangle
		{
			get
			{
				return ThemeEngine.Current.TabControlGetDisplayRectangle(this);
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value.</returns>
		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06003271 RID: 12913 RVA: 0x000BF7F8 File Offset: 0x000BD9F8
		// (set) Token: 0x06003272 RID: 12914 RVA: 0x000BF800 File Offset: 0x000BDA00
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

		/// <summary>Gets or sets the way that the control's tabs are drawn.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TabDrawMode" /> values. The default is Normal.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:System.Windows.Forms.TabDrawMode" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06003273 RID: 12915 RVA: 0x000BF80C File Offset: 0x000BDA0C
		// (set) Token: 0x06003274 RID: 12916 RVA: 0x000BF814 File Offset: 0x000BDA14
		[DefaultValue(TabDrawMode.Normal)]
		public TabDrawMode DrawMode
		{
			get
			{
				return this.draw_mode;
			}
			set
			{
				if (this.draw_mode == value)
				{
					return;
				}
				this.draw_mode = value;
				this.Redraw();
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06003275 RID: 12917 RVA: 0x000BF830 File Offset: 0x000BDA30
		// (set) Token: 0x06003276 RID: 12918 RVA: 0x000BF838 File Offset: 0x000BDA38
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

		/// <summary>Gets or sets a value indicating whether the control's tabs change in appearance when the mouse passes over them.</summary>
		/// <returns>true if the tabs change in appearance when the mouse passes over them; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06003277 RID: 12919 RVA: 0x000BF844 File Offset: 0x000BDA44
		// (set) Token: 0x06003278 RID: 12920 RVA: 0x000BF84C File Offset: 0x000BDA4C
		[DefaultValue(false)]
		public bool HotTrack
		{
			get
			{
				return this.hottrack;
			}
			set
			{
				if (this.hottrack == value)
				{
					return;
				}
				this.hottrack = value;
				this.Redraw();
			}
		}

		/// <summary>Gets or sets the images to display on the control's tabs.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageList" /> that specifies the images to display on the tabs.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06003279 RID: 12921 RVA: 0x000BF868 File Offset: 0x000BDA68
		// (set) Token: 0x0600327A RID: 12922 RVA: 0x000BF870 File Offset: 0x000BDA70
		[RefreshProperties(2)]
		[DefaultValue(null)]
		public ImageList ImageList
		{
			get
			{
				return this.image_list;
			}
			set
			{
				this.image_list = value;
			}
		}

		/// <summary>Gets or sets the size of the control's tabs.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the size of the tabs. The default automatically sizes the tabs to fit the icons and labels on the tabs.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The width or height of the <see cref="T:System.Drawing.Size" /> is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D2B RID: 3371
		// (get) Token: 0x0600327B RID: 12923 RVA: 0x000BF87C File Offset: 0x000BDA7C
		// (set) Token: 0x0600327C RID: 12924 RVA: 0x000BF884 File Offset: 0x000BDA84
		[Localizable(true)]
		public Size ItemSize
		{
			get
			{
				return this.item_size;
			}
			set
			{
				if (value.Height < 0 || value.Width < 0)
				{
					throw new ArgumentException("'" + value + "' is not a valid value for 'ItemSize'.");
				}
				this.item_size = value;
				this.Redraw();
			}
		}

		/// <summary>Gets or sets a value indicating whether more than one row of tabs can be displayed.</summary>
		/// <returns>true if more than one row of tabs can be displayed; otherwise, false. The default is false.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D2C RID: 3372
		// (get) Token: 0x0600327D RID: 12925 RVA: 0x000BF8D4 File Offset: 0x000BDAD4
		// (set) Token: 0x0600327E RID: 12926 RVA: 0x000BF8DC File Offset: 0x000BDADC
		[DefaultValue(false)]
		public bool Multiline
		{
			get
			{
				return this.multiline;
			}
			set
			{
				if (this.multiline == value)
				{
					return;
				}
				this.multiline = value;
				if ((!this.multiline && this.alignment == TabAlignment.Left) || this.alignment == TabAlignment.Right)
				{
					this.alignment = TabAlignment.Top;
				}
				this.Redraw();
			}
		}

		/// <summary>Gets or sets the amount of space around each item on the control's tab pages.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that specifies the amount of space around each item. The default is (6,3).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The width or height of the <see cref="T:System.Drawing.Point" /> is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D2D RID: 3373
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x000BF930 File Offset: 0x000BDB30
		// (set) Token: 0x06003280 RID: 12928 RVA: 0x000BF938 File Offset: 0x000BDB38
		[Localizable(true)]
		public new Point Padding
		{
			get
			{
				return this.padding;
			}
			set
			{
				if (value.X < 0 || value.Y < 0)
				{
					throw new ArgumentException("'" + value + "' is not a valid value for 'Padding'.");
				}
				if (this.padding == value)
				{
					return;
				}
				this.padding = value;
				this.Redraw();
			}
		}

		/// <summary>Gets or sets a value indicating whether right-to-left mirror placement is turned on.</summary>
		/// <returns>true if right-to-left mirror placement is turned on; false for standard child control placement. The default is false.</returns>
		// Token: 0x17000D2E RID: 3374
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x000BF99C File Offset: 0x000BDB9C
		// (set) Token: 0x06003282 RID: 12930 RVA: 0x000BF9A4 File Offset: 0x000BDBA4
		[Localizable(true)]
		[MonoTODO("RTL not supported")]
		[DefaultValue(false)]
		public virtual bool RightToLeftLayout
		{
			get
			{
				return this.rightToLeftLayout;
			}
			set
			{
				if (value != this.rightToLeftLayout)
				{
					this.rightToLeftLayout = value;
					this.OnRightToLeftLayoutChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets the number of rows that are currently being displayed in the control's tab strip.</summary>
		/// <returns>The number of rows that are currently being displayed in the tab strip.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D2F RID: 3375
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x000BF9C4 File Offset: 0x000BDBC4
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int RowCount
		{
			get
			{
				return this.row_count;
			}
		}

		/// <summary>Gets or sets the index of the currently selected tab page.</summary>
		/// <returns>The zero-based index of the currently selected tab page. The default is -1, which is also the value if no tab page is selected.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than -1. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D30 RID: 3376
		// (get) Token: 0x06003284 RID: 12932 RVA: 0x000BF9CC File Offset: 0x000BDBCC
		// (set) Token: 0x06003285 RID: 12933 RVA: 0x000BF9D4 File Offset: 0x000BDBD4
		[Browsable(false)]
		[DefaultValue(-1)]
		public int SelectedIndex
		{
			get
			{
				return this.selected_index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("SelectedIndex", "Value of '" + value + "' is valid for 'SelectedIndex'. 'SelectedIndex' must be greater than or equal to -1.");
				}
				if (!base.IsHandleCreated)
				{
					if (this.selected_index != value)
					{
						this.selected_index = value;
					}
					return;
				}
				if (value >= this.TabCount)
				{
					if (value != this.selected_index)
					{
						this.OnSelectedIndexChanged(EventArgs.Empty);
					}
					return;
				}
				if (value == this.selected_index)
				{
					if (this.selected_index > -1)
					{
						base.Invalidate(this.GetTabRect(this.selected_index));
					}
					return;
				}
				TabControlCancelEventArgs tabControlCancelEventArgs = new TabControlCancelEventArgs(this.SelectedTab, this.selected_index, false, TabControlAction.Deselecting);
				this.OnDeselecting(tabControlCancelEventArgs);
				if (tabControlCancelEventArgs.Cancel)
				{
					return;
				}
				base.Focus();
				int num = this.selected_index;
				this.selected_index = value;
				tabControlCancelEventArgs = new TabControlCancelEventArgs(this.SelectedTab, this.selected_index, false, TabControlAction.Selecting);
				this.OnSelecting(tabControlCancelEventArgs);
				if (tabControlCancelEventArgs.Cancel)
				{
					this.selected_index = num;
					return;
				}
				base.SuspendLayout();
				Rectangle rectangle = Rectangle.Empty;
				bool flag = false;
				if (value != -1 && this.show_slider && value < this.slider_pos)
				{
					this.slider_pos = value;
					flag = true;
				}
				if (value != -1)
				{
					int right = this.TabPages[value].TabBounds.Right;
					int left = this.LeftScrollButtonArea.Left;
					if (this.show_slider && right > left)
					{
						int i;
						for (i = 0; i < value - 1; i++)
						{
							if (this.TabPages[i].TabBounds.Left >= 0)
							{
								if (this.TabPages[value].TabBounds.Right - this.TabPages[i].TabBounds.Right < left)
								{
									i++;
									break;
								}
							}
						}
						this.slider_pos = i;
						flag = true;
					}
				}
				if (num != -1 && value != -1)
				{
					if (!flag)
					{
						rectangle = this.GetTabRect(num);
					}
					((TabPage)base.Controls[num]).SetVisible(false);
				}
				TabPage tabPage = null;
				if (value != -1)
				{
					tabPage = (TabPage)base.Controls[value];
					rectangle = Rectangle.Union(rectangle, this.GetTabRect(value));
					tabPage.SetVisible(true);
				}
				this.OnSelectedIndexChanged(EventArgs.Empty);
				base.ResumeLayout();
				if (flag)
				{
					this.SizeTabs();
					this.Refresh();
				}
				else if (value != -1 && tabPage.Row != this.BottomRow)
				{
					this.DropRow(this.TabPages[value].Row);
					this.SizeTabs();
					this.Refresh();
				}
				else
				{
					this.SizeTabs();
					if (this.appearance == TabAppearance.Normal)
					{
						rectangle.Inflate(6, 4);
						rectangle.Intersect(base.ClientRectangle);
					}
					base.Invalidate(rectangle);
				}
			}
		}

		/// <summary>Gets or sets the currently selected tab page.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TabPage" /> that represents the selected tab page. If no tab page is selected, the value is null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06003286 RID: 12934 RVA: 0x000BFCF4 File Offset: 0x000BDEF4
		// (set) Token: 0x06003287 RID: 12935 RVA: 0x000BFD18 File Offset: 0x000BDF18
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public TabPage SelectedTab
		{
			get
			{
				if (this.selected_index == -1)
				{
					return null;
				}
				return this.tab_pages[this.selected_index];
			}
			set
			{
				int num = this.IndexForTabPage(value);
				if (num == this.selected_index)
				{
					return;
				}
				this.SelectedIndex = num;
			}
		}

		/// <summary>Gets or sets a value indicating whether a tab's ToolTip is shown when the mouse passes over the tab.</summary>
		/// <returns>true if ToolTips are shown for the tabs that have them; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06003288 RID: 12936 RVA: 0x000BFD44 File Offset: 0x000BDF44
		// (set) Token: 0x06003289 RID: 12937 RVA: 0x000BFD4C File Offset: 0x000BDF4C
		[DefaultValue(false)]
		[Localizable(true)]
		public bool ShowToolTips
		{
			get
			{
				return this.show_tool_tips;
			}
			set
			{
				if (this.show_tool_tips == value)
				{
					return;
				}
				this.show_tool_tips = value;
				this.Redraw();
			}
		}

		/// <summary>Gets or sets the way that the control's tabs are sized.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TabSizeMode" /> values. The default is Normal.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The property value is not a valid <see cref="T:System.Windows.Forms.TabSizeMode" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x0600328A RID: 12938 RVA: 0x000BFD68 File Offset: 0x000BDF68
		// (set) Token: 0x0600328B RID: 12939 RVA: 0x000BFD70 File Offset: 0x000BDF70
		[DefaultValue(TabSizeMode.Normal)]
		[RefreshProperties(2)]
		public TabSizeMode SizeMode
		{
			get
			{
				return this.size_mode;
			}
			set
			{
				if (this.size_mode == value)
				{
					return;
				}
				this.size_mode = value;
				this.Redraw();
			}
		}

		/// <summary>Gets the number of tabs in the tab strip.</summary>
		/// <returns>The number of tabs in the tab strip.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x0600328C RID: 12940 RVA: 0x000BFD8C File Offset: 0x000BDF8C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public int TabCount
		{
			get
			{
				return this.tab_pages.Count;
			}
		}

		/// <summary>Gets the collection of tab pages in this tab control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" /> that contains the <see cref="T:System.Windows.Forms.TabPage" /> objects in this <see cref="T:System.Windows.Forms.TabControl" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x0600328D RID: 12941 RVA: 0x000BFD9C File Offset: 0x000BDF9C
		[Editor("System.Windows.Forms.Design.TabPageCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		[DesignerSerializationVisibility(0)]
		public TabControl.TabPageCollection TabPages
		{
			get
			{
				return this.tab_pages;
			}
		}

		/// <summary>This member is not meaningful for this control.</summary>
		/// <returns>A <see cref="T:System.String" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x0600328E RID: 12942 RVA: 0x000BFDA4 File Offset: 0x000BDFA4
		// (set) Token: 0x0600328F RID: 12943 RVA: 0x000BFDAC File Offset: 0x000BDFAC
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06003290 RID: 12944 RVA: 0x000BFDB8 File Offset: 0x000BDFB8
		// (set) Token: 0x06003291 RID: 12945 RVA: 0x000BFDC0 File Offset: 0x000BDFC0
		internal bool ShowSlider
		{
			get
			{
				return this.show_slider;
			}
			set
			{
				this.show_slider = value;
				this.OnUIAHorizontallyScrollableChanged(EventArgs.Empty);
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06003292 RID: 12946 RVA: 0x000BFDD4 File Offset: 0x000BDFD4
		internal int SliderPos
		{
			get
			{
				return this.slider_pos;
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06003293 RID: 12947 RVA: 0x000BFDDC File Offset: 0x000BDFDC
		// (set) Token: 0x06003294 RID: 12948 RVA: 0x000BFDE4 File Offset: 0x000BDFE4
		internal PushButtonState RightSliderState
		{
			get
			{
				return this.right_slider_state;
			}
			private set
			{
				if (this.right_slider_state == value)
				{
					return;
				}
				PushButtonState pushButtonState = this.right_slider_state;
				this.right_slider_state = value;
				if (this.NeedsToInvalidateScrollButton(pushButtonState, value))
				{
					base.Invalidate(this.RightScrollButtonArea);
				}
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06003295 RID: 12949 RVA: 0x000BFE28 File Offset: 0x000BE028
		// (set) Token: 0x06003296 RID: 12950 RVA: 0x000BFE30 File Offset: 0x000BE030
		internal PushButtonState LeftSliderState
		{
			get
			{
				return this.left_slider_state;
			}
			set
			{
				if (this.left_slider_state == value)
				{
					return;
				}
				PushButtonState pushButtonState = this.left_slider_state;
				this.left_slider_state = value;
				if (this.NeedsToInvalidateScrollButton(pushButtonState, value))
				{
					base.Invalidate(this.LeftScrollButtonArea);
				}
			}
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x000BFE74 File Offset: 0x000BE074
		private bool NeedsToInvalidateScrollButton(PushButtonState oldState, PushButtonState newState)
		{
			return ((oldState != PushButtonState.Hot || newState != PushButtonState.Normal) && (oldState != PushButtonState.Normal || newState != PushButtonState.Hot)) || this.HasHotElementStyles;
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06003298 RID: 12952 RVA: 0x000BFEA8 File Offset: 0x000BE0A8
		// (set) Token: 0x06003299 RID: 12953 RVA: 0x000BFEB0 File Offset: 0x000BE0B0
		internal TabPage EnteredTabPage
		{
			get
			{
				return this.entered_tab_page;
			}
			private set
			{
				if (this.entered_tab_page == value)
				{
					return;
				}
				if (this.HasHotElementStyles)
				{
					Region region = new Region();
					region.MakeEmpty();
					if (this.entered_tab_page != null)
					{
						region.Union(this.entered_tab_page.TabBounds);
					}
					this.entered_tab_page = value;
					if (this.entered_tab_page != null)
					{
						region.Union(this.entered_tab_page.TabBounds);
					}
					base.Invalidate(region);
					region.Dispose();
				}
				else
				{
					this.entered_tab_page = value;
				}
			}
		}

		/// <summary>This member overrides <see cref="P:System.Windows.Forms.Control.CreateParams" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x0600329A RID: 12954 RVA: 0x000BFF3C File Offset: 0x000BE13C
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x0600329B RID: 12955 RVA: 0x000BFF54 File Offset: 0x000BE154
		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 100);
			}
		}

		/// <summary>Returns the bounding rectangle for a specified tab in this tab control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the specified tab.</returns>
		/// <param name="index">The zero-based index of the tab you want. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index is less than zero.-or- The index is greater than or equal to <see cref="P:System.Windows.Forms.TabControl.TabPageCollection.Count" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600329C RID: 12956 RVA: 0x000BFF64 File Offset: 0x000BE164
		public Rectangle GetTabRect(int index)
		{
			TabPage tab = this.GetTab(index);
			return tab.TabBounds;
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.TabPage" /> control at the specified location.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TabPage" /> at the specified location.</returns>
		/// <param name="index">The index of the <see cref="T:System.Windows.Forms.TabPage" /> to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than the <see cref="P:System.Windows.Forms.TabControl.TabCount" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600329D RID: 12957 RVA: 0x000BFF80 File Offset: 0x000BE180
		public Control GetControl(int index)
		{
			return this.GetTab(index);
		}

		/// <summary>Makes the specified <see cref="T:System.Windows.Forms.TabPage" /> the current tab.</summary>
		/// <param name="tabPage">The <see cref="T:System.Windows.Forms.TabPage" /> to select.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than the number of <see cref="T:System.Windows.Forms.TabPage" /> controls in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection minus 1.-or-<paramref name="tabPage" /> is not in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tabPage" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600329E RID: 12958 RVA: 0x000BFF8C File Offset: 0x000BE18C
		public void SelectTab(TabPage tabPage)
		{
			if (tabPage == null)
			{
				throw new ArgumentNullException("tabPage");
			}
			this.SelectTab(this.tab_pages[tabPage]);
		}

		/// <summary>Makes the tab with the specified name the current tab.</summary>
		/// <param name="tabPageName">The <see cref="P:System.Windows.Forms.Control.Name" /> of the tab to select.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tabPageName" /> is null.-or-<paramref name="tabPageName" /> does not match the <see cref="P:System.Windows.Forms.Control.Name" /> property of any <see cref="T:System.Windows.Forms.TabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600329F RID: 12959 RVA: 0x000BFFB4 File Offset: 0x000BE1B4
		public void SelectTab(string tabPageName)
		{
			if (tabPageName == null)
			{
				throw new ArgumentNullException("tabPageName");
			}
			this.SelectTab(this.tab_pages[tabPageName]);
		}

		/// <summary>Makes the tab with the specified index the current tab.</summary>
		/// <param name="index">The index in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection of the tab to select.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than the number of <see cref="T:System.Windows.Forms.TabPage" /> controls in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection minus 1.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060032A0 RID: 12960 RVA: 0x000BFFDC File Offset: 0x000BE1DC
		public void SelectTab(int index)
		{
			if (index < 0 || index > this.tab_pages.Count - 1)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			this.SelectedIndex = index;
		}

		/// <summary>Makes the tab following the specified <see cref="T:System.Windows.Forms.TabPage" /> the current tab.</summary>
		/// <param name="tabPage">The <see cref="T:System.Windows.Forms.TabPage" /> to deselect.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than the number of <see cref="T:System.Windows.Forms.TabPage" /> controls in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection minus 1.-or-<paramref name="tabPage" /> is not in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tabPage" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060032A1 RID: 12961 RVA: 0x000C0018 File Offset: 0x000BE218
		public void DeselectTab(TabPage tabPage)
		{
			if (tabPage == null)
			{
				throw new ArgumentNullException("tabPage");
			}
			this.DeselectTab(this.tab_pages[tabPage]);
		}

		/// <summary>Makes the tab following the tab with the specified name the current tab.</summary>
		/// <param name="tabPageName">The <see cref="P:System.Windows.Forms.Control.Name" /> of the tab to deselect.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="tabPageName" /> is null.-or-<paramref name="tabPageName" /> does not match the <see cref="P:System.Windows.Forms.Control.Name" /> property of any <see cref="T:System.Windows.Forms.TabPage" /> in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060032A2 RID: 12962 RVA: 0x000C0040 File Offset: 0x000BE240
		public void DeselectTab(string tabPageName)
		{
			if (tabPageName == null)
			{
				throw new ArgumentNullException("tabPageName");
			}
			this.DeselectTab(this.tab_pages[tabPageName]);
		}

		/// <summary>Makes the tab following the tab with the specified index the current tab.</summary>
		/// <param name="index">The index in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection of the tab to deselect.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than 0 or greater than the number of <see cref="T:System.Windows.Forms.TabPage" /> controls in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection minus 1.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060032A3 RID: 12963 RVA: 0x000C0068 File Offset: 0x000BE268
		public void DeselectTab(int index)
		{
			if (index == this.SelectedIndex)
			{
				if (index >= 0 && index < this.tab_pages.Count - 1)
				{
					index = (this.SelectedIndex = index + 1);
				}
				else
				{
					this.SelectedIndex = 0;
				}
			}
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.TabControl" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.TabControl" />. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060032A4 RID: 12964 RVA: 0x000C00B4 File Offset: 0x000BE2B4
		public override string ToString()
		{
			string text = base.ToString() + ", TabPages.Count: " + this.TabCount;
			if (this.TabCount > 0)
			{
				text = text + ", TabPages[0]: " + this.TabPages[0];
			}
			return text;
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.CreateControlsInstance" />.</summary>
		/// <returns>A new instance of <see cref="T:System.Windows.Forms.Control.ControlCollection" /> assigned to the control.</returns>
		// Token: 0x060032A5 RID: 12965 RVA: 0x000C0104 File Offset: 0x000BE304
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new TabControl.ControlCollection(this);
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.CreateHandle" />.</summary>
		// Token: 0x060032A6 RID: 12966 RVA: 0x000C010C File Offset: 0x000BE30C
		protected override void CreateHandle()
		{
			base.CreateHandle();
			this.selected_index = ((this.selected_index < this.TabCount) ? this.selected_index : ((this.TabCount <= 0) ? (-1) : 0));
			if (this.TabCount > 0)
			{
				if (this.selected_index > -1)
				{
					this.SelectedTab.SetVisible(true);
				}
				else
				{
					this.tab_pages[0].SetVisible(true);
				}
			}
			this.ResizeTabPages();
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.OnHandleCreated(System.EventArgs)" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060032A7 RID: 12967 RVA: 0x000C0198 File Offset: 0x000BE398
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060032A8 RID: 12968 RVA: 0x000C01A4 File Offset: 0x000BE3A4
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060032A9 RID: 12969 RVA: 0x000C01B0 File Offset: 0x000BE3B0
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TabControl.DrawItem" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.DrawItemEventArgs" /> that contains the event data. </param>
		// Token: 0x060032AA RID: 12970 RVA: 0x000C01BC File Offset: 0x000BE3BC
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
			if (this.DrawMode != TabDrawMode.OwnerDrawFixed)
			{
				return;
			}
			DrawItemEventHandler drawItemEventHandler = (DrawItemEventHandler)base.Events[TabControl.DrawItemEvent];
			if (drawItemEventHandler != null)
			{
				drawItemEventHandler(this, e);
			}
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x000C01FC File Offset: 0x000BE3FC
		internal void OnDrawItemInternal(DrawItemEventArgs e)
		{
			this.OnDrawItem(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060032AC RID: 12972 RVA: 0x000C0208 File Offset: 0x000BE408
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.ResizeTabPages();
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.OnResize(System.EventArgs)" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060032AD RID: 12973 RVA: 0x000C0218 File Offset: 0x000BE418
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.OnStyleChanged(System.EventArgs)" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060032AE RID: 12974 RVA: 0x000C0224 File Offset: 0x000BE424
		protected override void OnStyleChanged(EventArgs e)
		{
			base.OnStyleChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TabControl.SelectedIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060032AF RID: 12975 RVA: 0x000C0230 File Offset: 0x000BE430
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TabControl.SelectedIndexChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x000C0264 File Offset: 0x000BE464
		internal override void OnPaintInternal(PaintEventArgs pe)
		{
			if (base.GetStyle(ControlStyles.UserPaint))
			{
				return;
			}
			this.Draw(pe.Graphics, pe.ClipRectangle);
			pe.Handled = true;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Enter" /> event of the <see cref="T:System.Windows.Forms.TabControl" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060032B1 RID: 12977 RVA: 0x000C0298 File Offset: 0x000BE498
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			if (this.SelectedTab != null)
			{
				this.SelectedTab.FireEnter();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event of the <see cref="T:System.Windows.Forms.TabControl" />.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060032B2 RID: 12978 RVA: 0x000C02B8 File Offset: 0x000BE4B8
		protected override void OnLeave(EventArgs e)
		{
			if (this.SelectedTab != null)
			{
				this.SelectedTab.FireLeave();
			}
			base.OnLeave(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TabControl.RightToLeftLayoutChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060032B3 RID: 12979 RVA: 0x000C02E4 File Offset: 0x000BE4E4
		[EditorBrowsable(2)]
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TabControl.RightToLeftLayoutChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.ScaleCore(System.Single,System.Single)" />.</summary>
		/// <param name="dx">The horizontal scaling factor. </param>
		/// <param name="dy">The vertical scaling factor. </param>
		// Token: 0x060032B4 RID: 12980 RVA: 0x000C0318 File Offset: 0x000BE518
		[EditorBrowsable(1)]
		protected override void ScaleCore(float dx, float dy)
		{
			base.ScaleCore(dx, dy);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TabControl.Deselecting" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TabControlCancelEventArgs" /> that contains the event data. </param>
		// Token: 0x060032B5 RID: 12981 RVA: 0x000C0324 File Offset: 0x000BE524
		protected virtual void OnDeselecting(TabControlCancelEventArgs e)
		{
			TabControlCancelEventHandler tabControlCancelEventHandler = (TabControlCancelEventHandler)base.Events[TabControl.DeselectingEvent];
			if (tabControlCancelEventHandler != null)
			{
				tabControlCancelEventHandler(this, e);
			}
			if (!e.Cancel)
			{
				this.OnDeselected(new TabControlEventArgs(this.SelectedTab, this.selected_index, TabControlAction.Deselected));
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TabControl.Deselected" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TabControlEventArgs" /> that contains the event data. </param>
		// Token: 0x060032B6 RID: 12982 RVA: 0x000C0378 File Offset: 0x000BE578
		protected virtual void OnDeselected(TabControlEventArgs e)
		{
			TabControlEventHandler tabControlEventHandler = (TabControlEventHandler)base.Events[TabControl.DeselectedEvent];
			if (tabControlEventHandler != null)
			{
				tabControlEventHandler(this, e);
			}
			if (this.SelectedTab != null)
			{
				this.SelectedTab.FireLeave();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TabControl.Selecting" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TabControlCancelEventArgs" /> that contains the event data. </param>
		// Token: 0x060032B7 RID: 12983 RVA: 0x000C03C0 File Offset: 0x000BE5C0
		protected virtual void OnSelecting(TabControlCancelEventArgs e)
		{
			TabControlCancelEventHandler tabControlCancelEventHandler = (TabControlCancelEventHandler)base.Events[TabControl.SelectingEvent];
			if (tabControlCancelEventHandler != null)
			{
				tabControlCancelEventHandler(this, e);
			}
			if (!e.Cancel)
			{
				this.OnSelected(new TabControlEventArgs(this.SelectedTab, this.selected_index, TabControlAction.Selected));
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TabControl.Selected" /> event. </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.TabControlEventArgs" /> that contains the event data. </param>
		// Token: 0x060032B8 RID: 12984 RVA: 0x000C0414 File Offset: 0x000BE614
		protected virtual void OnSelected(TabControlEventArgs e)
		{
			TabControlEventHandler tabControlEventHandler = (TabControlEventHandler)base.Events[TabControl.SelectedEvent];
			if (tabControlEventHandler != null)
			{
				tabControlEventHandler(this, e);
			}
			if (this.SelectedTab != null)
			{
				this.SelectedTab.FireEnter();
			}
		}

		/// <returns>true if the message was processed by the control; otherwise, false.</returns>
		/// <param name="m">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the window message to process. </param>
		// Token: 0x060032B9 RID: 12985 RVA: 0x000C045C File Offset: 0x000BE65C
		protected override bool ProcessKeyPreview(ref Message m)
		{
			return base.ProcessKeyPreview(ref m);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.KeyDown" /> event. </summary>
		/// <param name="ke">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param>
		// Token: 0x060032BA RID: 12986 RVA: 0x000C0468 File Offset: 0x000BE668
		protected override void OnKeyDown(KeyEventArgs ke)
		{
			base.OnKeyDown(ke);
			if (ke.Handled)
			{
				return;
			}
			if (ke.KeyCode == Keys.Tab && (ke.KeyData & Keys.Control) != Keys.None)
			{
				if ((ke.KeyData & Keys.Shift) == Keys.None)
				{
					this.SelectedIndex = (this.SelectedIndex + 1) % this.TabCount;
				}
				else
				{
					this.SelectedIndex = (this.SelectedIndex + this.TabCount - 1) % this.TabCount;
				}
				ke.Handled = true;
			}
			else if (ke.KeyCode == Keys.Home)
			{
				this.SelectedIndex = 0;
				ke.Handled = true;
			}
			else if (ke.KeyCode == Keys.End)
			{
				this.SelectedIndex = this.TabCount - 1;
				ke.Handled = true;
			}
			else if (this.NavigateTabs(ke.KeyCode))
			{
				ke.Handled = true;
			}
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values. </param>
		// Token: 0x060032BB RID: 12987 RVA: 0x000C0558 File Offset: 0x000BE758
		protected override bool IsInputKey(Keys keyData)
		{
			switch (keyData & Keys.KeyCode)
			{
			case Keys.End:
			case Keys.Home:
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
				return true;
			default:
				return base.IsInputKey(keyData);
			}
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x000C059C File Offset: 0x000BE79C
		private bool NavigateTabs(Keys keycode)
		{
			bool flag = false;
			bool flag2 = false;
			if (this.alignment == TabAlignment.Bottom || this.alignment == TabAlignment.Top)
			{
				if (keycode == Keys.Left)
				{
					flag = true;
				}
				else if (keycode == Keys.Right)
				{
					flag2 = true;
				}
			}
			else if (keycode == Keys.Up)
			{
				flag = true;
			}
			else if (keycode == Keys.Down)
			{
				flag2 = true;
			}
			if (flag && this.SelectedIndex > 0)
			{
				this.SelectedIndex--;
				return true;
			}
			if (flag2 && this.SelectedIndex < this.TabCount - 1)
			{
				this.SelectedIndex++;
				return true;
			}
			return false;
		}

		/// <summary>Removes all the tab pages and additional controls from this tab control.</summary>
		// Token: 0x060032BD RID: 12989 RVA: 0x000C0648 File Offset: 0x000BE848
		protected void RemoveAll()
		{
			base.Controls.Clear();
		}

		/// <summary>Gets an array of <see cref="T:System.Windows.Forms.TabPage" /> controls that belong to the <see cref="T:System.Windows.Forms.TabControl" /> control.</summary>
		/// <returns>An array of <see cref="T:System.Windows.Forms.TabPage" /> controls that belong to the <see cref="T:System.Windows.Forms.TabControl" />.</returns>
		// Token: 0x060032BE RID: 12990 RVA: 0x000C0658 File Offset: 0x000BE858
		protected virtual object[] GetItems()
		{
			TabPage[] array = new TabPage[base.Controls.Count];
			base.Controls.CopyTo(array, 0);
			return array;
		}

		/// <summary>Copies the <see cref="T:System.Windows.Forms.TabPage" /> controls in the <see cref="T:System.Windows.Forms.TabControl" /> to an array of the specified type.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.TabPage" /> controls that belong to the <see cref="T:System.Windows.Forms.TabControl" /> as an array of the specified type.</returns>
		/// <param name="baseType">The <see cref="T:System.Type" /> of the array to create.</param>
		/// <exception cref="T:System.ArrayTypeMismatchException">The type <see cref="T:System.Windows.Forms.TabPage" /> cannot be converted to <paramref name="baseType" />.</exception>
		// Token: 0x060032BF RID: 12991 RVA: 0x000C0684 File Offset: 0x000BE884
		protected virtual object[] GetItems(Type baseType)
		{
			object[] array = (object[])Array.CreateInstance(baseType, base.Controls.Count);
			base.Controls.CopyTo(array, 0);
			return array;
		}

		/// <summary>Sets the <see cref="P:System.Windows.Forms.TabPage.Visible" /> property to true for the appropriate <see cref="T:System.Windows.Forms.TabPage" /> control in the <see cref="P:System.Windows.Forms.TabControl.TabPages" /> collection.</summary>
		/// <param name="updateFocus">true to change focus to the next <see cref="T:System.Windows.Forms.TabPage" />; otherwise, false.</param>
		// Token: 0x060032C0 RID: 12992 RVA: 0x000C06B8 File Offset: 0x000BE8B8
		protected void UpdateTabSelection(bool updateFocus)
		{
			this.ResizeTabPages();
		}

		/// <summary>Gets the ToolTip for the specified <see cref="T:System.Windows.Forms.TabPage" />.</summary>
		/// <returns>The ToolTip text.</returns>
		/// <param name="item">The <see cref="T:System.Windows.Forms.TabPage" /> that owns the desired ToolTip.</param>
		// Token: 0x060032C1 RID: 12993 RVA: 0x000C06C0 File Offset: 0x000BE8C0
		protected string GetToolTipText(object item)
		{
			TabPage tabPage = (TabPage)item;
			return tabPage.ToolTipText;
		}

		/// <summary>This member overrides <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" />.</summary>
		/// <param name="m">A Windows Message Object. </param>
		// Token: 0x060032C2 RID: 12994 RVA: 0x000C06DC File Offset: 0x000BE8DC
		protected override void WndProc(ref Message m)
		{
			Msg msg = (Msg)m.Msg;
			if (msg != Msg.WM_SETFOCUS)
			{
				if (msg != Msg.WM_KILLFOCUS)
				{
					base.WndProc(ref m);
				}
				else
				{
					if (this.selected_index != -1)
					{
						base.Invalidate(this.GetTabRect(this.selected_index));
					}
					base.WndProc(ref m);
				}
			}
			else
			{
				if (this.selected_index == -1 && this.TabCount > 0)
				{
					this.SelectedIndex = 0;
				}
				if (this.selected_index != -1)
				{
					base.Invalidate(this.GetTabRect(this.selected_index));
				}
				base.WndProc(ref m);
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x060032C3 RID: 12995 RVA: 0x000C0784 File Offset: 0x000BE984
		private bool CanScrollRight
		{
			get
			{
				return this.slider_pos < this.TabCount - 1;
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x060032C4 RID: 12996 RVA: 0x000C0798 File Offset: 0x000BE998
		private bool CanScrollLeft
		{
			get
			{
				return this.slider_pos > 0;
			}
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x000C07A4 File Offset: 0x000BE9A4
		private void MouseDownHandler(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.None)
			{
				return;
			}
			if (this.ShowSlider)
			{
				Rectangle rightScrollButtonArea = this.RightScrollButtonArea;
				Rectangle leftScrollButtonArea = this.LeftScrollButtonArea;
				if (rightScrollButtonArea.Contains(e.X, e.Y))
				{
					this.right_slider_state = PushButtonState.Pressed;
					if (this.CanScrollRight)
					{
						this.slider_pos++;
						this.SizeTabs();
						this.OnUIAHorizontallyScrolled(EventArgs.Empty);
						switch (this.Alignment)
						{
						case TabAlignment.Top:
							base.Invalidate(new Rectangle(0, 0, base.Width, this.ItemSize.Height));
							break;
						case TabAlignment.Bottom:
							base.Invalidate(new Rectangle(0, this.DisplayRectangle.Bottom, base.Width, base.Height - this.DisplayRectangle.Bottom));
							break;
						case TabAlignment.Left:
							base.Invalidate(new Rectangle(0, 0, this.DisplayRectangle.Left, base.Height));
							break;
						case TabAlignment.Right:
							base.Invalidate(new Rectangle(this.DisplayRectangle.Right, 0, base.Width - this.DisplayRectangle.Right, base.Height));
							break;
						}
					}
					else
					{
						base.Invalidate(rightScrollButtonArea);
					}
					return;
				}
				if (leftScrollButtonArea.Contains(e.X, e.Y))
				{
					this.left_slider_state = PushButtonState.Pressed;
					if (this.CanScrollLeft)
					{
						this.slider_pos--;
						this.SizeTabs();
						this.OnUIAHorizontallyScrolled(EventArgs.Empty);
						switch (this.Alignment)
						{
						case TabAlignment.Top:
							base.Invalidate(new Rectangle(0, 0, base.Width, this.ItemSize.Height));
							break;
						case TabAlignment.Bottom:
							base.Invalidate(new Rectangle(0, this.DisplayRectangle.Bottom, base.Width, base.Height - this.DisplayRectangle.Bottom));
							break;
						case TabAlignment.Left:
							base.Invalidate(new Rectangle(0, 0, this.DisplayRectangle.Left, base.Height));
							break;
						case TabAlignment.Right:
							base.Invalidate(new Rectangle(this.DisplayRectangle.Right, 0, base.Width - this.DisplayRectangle.Right, base.Height));
							break;
						}
					}
					else
					{
						base.Invalidate(leftScrollButtonArea);
					}
					return;
				}
			}
			int count = base.Controls.Count;
			for (int i = this.SliderPos; i < count; i++)
			{
				if (this.GetTabRect(i).Contains(e.X, e.Y))
				{
					this.SelectedIndex = i;
					this.mouse_down_on_a_tab_page = true;
					break;
				}
			}
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x000C0AB8 File Offset: 0x000BECB8
		private void MouseUpHandler(object sender, MouseEventArgs e)
		{
			this.mouse_down_on_a_tab_page = false;
			if (this.ShowSlider && (this.left_slider_state == PushButtonState.Pressed || this.right_slider_state == PushButtonState.Pressed))
			{
				Rectangle rectangle;
				if (this.left_slider_state == PushButtonState.Pressed)
				{
					rectangle = this.LeftScrollButtonArea;
					this.left_slider_state = TabControl.GetScrollButtonState(rectangle, e.Location);
				}
				else
				{
					rectangle = this.RightScrollButtonArea;
					this.right_slider_state = TabControl.GetScrollButtonState(rectangle, e.Location);
				}
				base.Invalidate(rectangle);
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x060032C7 RID: 12999 RVA: 0x000C0B3C File Offset: 0x000BED3C
		private bool HasHotElementStyles
		{
			get
			{
				return ThemeElements.CurrentTheme.TabControlPainter.HasHotElementStyles(this);
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x060032C8 RID: 13000 RVA: 0x000C0B50 File Offset: 0x000BED50
		private Rectangle LeftScrollButtonArea
		{
			get
			{
				return ThemeElements.CurrentTheme.TabControlPainter.GetLeftScrollRect(this);
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x060032C9 RID: 13001 RVA: 0x000C0B64 File Offset: 0x000BED64
		private Rectangle RightScrollButtonArea
		{
			get
			{
				return ThemeElements.CurrentTheme.TabControlPainter.GetRightScrollRect(this);
			}
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000C0B78 File Offset: 0x000BED78
		private static PushButtonState GetScrollButtonState(Rectangle scrollButtonArea, Point cursorLocation)
		{
			return (!scrollButtonArea.Contains(cursorLocation)) ? PushButtonState.Normal : PushButtonState.Hot;
		}

		// Token: 0x060032CB RID: 13003 RVA: 0x000C0B90 File Offset: 0x000BED90
		private void SizeChangedHandler(object sender, EventArgs e)
		{
			this.Redraw();
		}

		// Token: 0x060032CC RID: 13004 RVA: 0x000C0B98 File Offset: 0x000BED98
		internal int IndexForTabPage(TabPage page)
		{
			for (int i = 0; i < this.tab_pages.Count; i++)
			{
				if (page == this.tab_pages[i])
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060032CD RID: 13005 RVA: 0x000C0BD8 File Offset: 0x000BEDD8
		private void ResizeTabPages()
		{
			this.CalcTabRows();
			this.SizeTabs();
			Rectangle displayRectangle = this.DisplayRectangle;
			foreach (object obj in base.Controls)
			{
				TabPage tabPage = (TabPage)obj;
				tabPage.Bounds = displayRectangle;
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x060032CE RID: 13006 RVA: 0x000C0C5C File Offset: 0x000BEE5C
		private int MinimumTabWidth
		{
			get
			{
				return ThemeEngine.Current.TabControlMinimumTabWidth;
			}
		}

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x060032CF RID: 13007 RVA: 0x000C0C68 File Offset: 0x000BEE68
		private Size TabSpacing
		{
			get
			{
				return ThemeEngine.Current.TabControlGetSpacing(this);
			}
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000C0C78 File Offset: 0x000BEE78
		private void CalcTabRows()
		{
			TabAlignment tabAlignment = this.Alignment;
			if (tabAlignment != TabAlignment.Left && tabAlignment != TabAlignment.Right)
			{
				this.CalcTabRows(base.Width);
			}
			else
			{
				this.CalcTabRows(base.Height);
			}
		}

		// Token: 0x060032D1 RID: 13009 RVA: 0x000C0CC4 File Offset: 0x000BEEC4
		private void CalcTabRows(int row_width)
		{
			int num = 0;
			int num2 = 0;
			Size tabSpacing = this.TabSpacing;
			if (this.TabPages.Count > 0)
			{
				this.row_count = 1;
			}
			this.show_slider = false;
			for (int i = 0; i < this.TabPages.Count; i++)
			{
				TabPage tabPage = this.TabPages[i];
				int num3 = 0;
				this.SizeTab(tabPage, i, row_width, ref num, ref num2, tabSpacing, 0, ref num3, true);
			}
			if (this.SelectedIndex != -1 && this.TabPages.Count > this.SelectedIndex && this.TabPages[this.SelectedIndex].Row != this.BottomRow)
			{
				this.DropRow(this.TabPages[this.SelectedIndex].Row);
			}
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x060032D2 RID: 13010 RVA: 0x000C0D9C File Offset: 0x000BEF9C
		private int BottomRow
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x060032D3 RID: 13011 RVA: 0x000C0DA0 File Offset: 0x000BEFA0
		private int Direction
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060032D4 RID: 13012 RVA: 0x000C0DA4 File Offset: 0x000BEFA4
		private void DropRow(int row)
		{
			if (this.Appearance != TabAppearance.Normal)
			{
				return;
			}
			int bottomRow = this.BottomRow;
			int direction = this.Direction;
			foreach (object obj in this.TabPages)
			{
				TabPage tabPage = (TabPage)obj;
				if (tabPage.Row == row)
				{
					tabPage.Row = bottomRow;
				}
				else if (direction == 1 && tabPage.Row < row)
				{
					tabPage.Row += direction;
				}
				else if (direction == -1 && tabPage.Row > row)
				{
					tabPage.Row += direction;
				}
			}
		}

		// Token: 0x060032D5 RID: 13013 RVA: 0x000C0E88 File Offset: 0x000BF088
		private int CalcYPos()
		{
			if (this.Alignment == TabAlignment.Bottom || this.Alignment == TabAlignment.Left)
			{
				return ThemeEngine.Current.TabControlGetPanelRect(this).Bottom;
			}
			if (this.Appearance == TabAppearance.Normal)
			{
				return base.ClientRectangle.Y + ThemeEngine.Current.TabControlSelectedDelta.Y;
			}
			return base.ClientRectangle.Y;
		}

		// Token: 0x060032D6 RID: 13014 RVA: 0x000C0EFC File Offset: 0x000BF0FC
		private int CalcXPos()
		{
			if (this.Alignment == TabAlignment.Right)
			{
				return ThemeEngine.Current.TabControlGetPanelRect(this).Right;
			}
			if (this.Appearance == TabAppearance.Normal)
			{
				return base.ClientRectangle.X + ThemeEngine.Current.TabControlSelectedDelta.X;
			}
			return base.ClientRectangle.X;
		}

		// Token: 0x060032D7 RID: 13015 RVA: 0x000C0F64 File Offset: 0x000BF164
		private void SizeTabs()
		{
			TabAlignment tabAlignment = this.Alignment;
			if (tabAlignment != TabAlignment.Left && tabAlignment != TabAlignment.Right)
			{
				this.SizeTabs(base.Width, false);
			}
			else
			{
				this.SizeTabs(base.Height, true);
			}
		}

		// Token: 0x060032D8 RID: 13016 RVA: 0x000C0FB0 File Offset: 0x000BF1B0
		private void SizeTabs(int row_width, bool vertical)
		{
			int num = 0;
			int num2 = 0;
			Size tabSpacing = this.TabSpacing;
			int num3 = 0;
			if (this.TabPages.Count == 0)
			{
				return;
			}
			int num4 = this.TabPages[0].Row;
			if (!this.show_slider)
			{
				this.slider_pos = 0;
			}
			else
			{
				for (int i = 0; i < this.slider_pos; i++)
				{
					TabPage tabPage = this.TabPages[i];
					Rectangle tabBounds = tabPage.TabBounds;
					tabBounds.X = -1;
					tabPage.TabBounds = tabBounds;
				}
			}
			for (int j = this.slider_pos; j < this.TabPages.Count; j++)
			{
				TabPage tabPage2 = this.TabPages[j];
				this.SizeTab(tabPage2, j, row_width, ref num2, ref num, tabSpacing, num4, ref num3, false);
				num4 = tabPage2.Row;
			}
			if (this.SizeMode == TabSizeMode.FillToRight && !this.ShowSlider)
			{
				this.FillRow(num3, this.TabPages.Count - 1, (row_width - this.TabPages[this.TabPages.Count - 1].TabBounds.Right) / (this.TabPages.Count - num3), tabSpacing, vertical);
			}
			if (this.SelectedIndex != -1)
			{
				this.ExpandSelected(this.TabPages[this.SelectedIndex], 0, row_width - 1);
			}
		}

		// Token: 0x060032D9 RID: 13017 RVA: 0x000C1128 File Offset: 0x000BF328
		private void SizeTab(TabPage page, int i, int row_width, ref int xpos, ref int ypos, Size spacing, int prev_row, ref int begin_prev, bool widthOnly)
		{
			int num;
			if (this.SizeMode == TabSizeMode.Fixed)
			{
				num = this.item_size.Width;
			}
			else
			{
				num = this.MeasureStringWidth(base.DeviceContext, page.Text, page.Font);
				num += this.Padding.X * 2 + 2;
				if (this.ImageList != null && page.ImageIndex >= 0 && page.ImageIndex < this.ImageList.Images.Count)
				{
					num += this.ImageList.ImageSize.Width + ThemeEngine.Current.TabControlImagePadding.X;
					int num2 = this.ImageList.ImageSize.Height + ThemeEngine.Current.TabControlImagePadding.Y;
					if (this.item_size.Height < num2)
					{
						this.item_size.Height = num2;
					}
				}
			}
			int num3 = this.item_size.Height - ThemeEngine.Current.TabControlSelectedDelta.Height;
			if (num < this.MinimumTabWidth)
			{
				num = this.MinimumTabWidth;
			}
			if (i == this.SelectedIndex)
			{
				num += ThemeEngine.Current.TabControlSelectedSpacing;
			}
			if (widthOnly)
			{
				page.TabBounds = new Rectangle(xpos, 0, num, 0);
				page.Row = this.row_count;
				if (xpos + num > row_width && this.multiline)
				{
					xpos = 0;
					this.row_count++;
				}
				else if (xpos + num > row_width)
				{
					this.show_slider = true;
				}
				if (i == this.selected_index && this.show_slider)
				{
					for (int j = i - 1; j >= 0; j--)
					{
						if (this.TabPages[j].TabBounds.Left < xpos + num - row_width)
						{
							this.slider_pos = j + 1;
							break;
						}
					}
				}
			}
			else
			{
				if (page.Row != prev_row)
				{
					xpos = 0;
				}
				switch (this.Alignment)
				{
				case TabAlignment.Top:
					page.TabBounds = new Rectangle(xpos + this.CalcXPos(), ypos + (num3 + spacing.Height) * (this.row_count - page.Row) + this.CalcYPos(), num, num3);
					break;
				case TabAlignment.Bottom:
					page.TabBounds = new Rectangle(xpos + this.CalcXPos(), ypos + (num3 + spacing.Height) * (this.row_count - page.Row) + this.CalcYPos(), num, num3);
					break;
				case TabAlignment.Left:
					if (this.Appearance == TabAppearance.Normal)
					{
						page.TabBounds = new Rectangle(ypos + (num3 + spacing.Height) * (this.row_count - page.Row) + this.CalcXPos(), xpos, num3, num);
					}
					else
					{
						page.TabBounds = new Rectangle(ypos + (num3 + spacing.Height) * (page.Row - 1) + this.CalcXPos(), xpos, num3, num);
					}
					break;
				case TabAlignment.Right:
					if (this.Appearance == TabAppearance.Normal)
					{
						page.TabBounds = new Rectangle(ypos + (num3 + spacing.Height) * (page.Row - 1) + this.CalcXPos(), xpos, num3, num);
					}
					else
					{
						page.TabBounds = new Rectangle(ypos + (num3 + spacing.Height) * (this.row_count - page.Row) + this.CalcXPos(), xpos, num3, num);
					}
					break;
				}
				if (page.Row != prev_row)
				{
					if (this.SizeMode == TabSizeMode.FillToRight && !this.ShowSlider)
					{
						bool flag = this.alignment == TabAlignment.Right || this.alignment == TabAlignment.Left;
						int num4 = ((!flag) ? this.TabPages[i - 1].TabBounds.Right : this.TabPages[i - 1].TabBounds.Bottom);
						this.FillRow(begin_prev, i - 1, (row_width - num4) / (i - begin_prev), spacing, flag);
					}
					begin_prev = i;
				}
			}
			xpos += num + spacing.Width + ThemeEngine.Current.TabControlColSpacing;
		}

		// Token: 0x060032DA RID: 13018 RVA: 0x000C1590 File Offset: 0x000BF790
		private void FillRow(int start, int end, int amount, Size spacing, bool vertical)
		{
			if (vertical)
			{
				this.FillRowV(start, end, amount, spacing);
			}
			else
			{
				this.FillRow(start, end, amount, spacing);
			}
		}

		// Token: 0x060032DB RID: 13019 RVA: 0x000C15C0 File Offset: 0x000BF7C0
		private void FillRow(int start, int end, int amount, Size spacing)
		{
			int num = this.TabPages[start].TabBounds.Left;
			for (int i = start; i <= end; i++)
			{
				TabPage tabPage = this.TabPages[i];
				int num2 = num;
				int num3 = ((i != end) ? (tabPage.TabBounds.Width + amount) : (base.Width - num2 - 3));
				tabPage.TabBounds = new Rectangle(num2, tabPage.TabBounds.Top, num3, tabPage.TabBounds.Height);
				num = tabPage.TabBounds.Right + 1 + spacing.Width;
			}
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x000C1678 File Offset: 0x000BF878
		private void FillRowV(int start, int end, int amount, Size spacing)
		{
			int num = this.TabPages[start].TabBounds.Top;
			for (int i = start; i <= end; i++)
			{
				TabPage tabPage = this.TabPages[i];
				int num2 = num;
				int num3 = ((i != end) ? (tabPage.TabBounds.Height + amount) : (base.Height - num2 - 5));
				tabPage.TabBounds = new Rectangle(tabPage.TabBounds.Left, num2, tabPage.TabBounds.Width, num3);
				num = tabPage.TabBounds.Bottom + 1;
			}
		}

		// Token: 0x060032DD RID: 13021 RVA: 0x000C1728 File Offset: 0x000BF928
		private void ExpandSelected(TabPage page, int left_edge, int right_edge)
		{
			if (this.Appearance != TabAppearance.Normal)
			{
				return;
			}
			Rectangle tabBounds = page.TabBounds;
			switch (this.Alignment)
			{
			case TabAlignment.Top:
			case TabAlignment.Left:
				tabBounds.Y -= ThemeEngine.Current.TabControlSelectedDelta.Y;
				tabBounds.X -= ThemeEngine.Current.TabControlSelectedDelta.X;
				break;
			case TabAlignment.Bottom:
				tabBounds.Y -= ThemeEngine.Current.TabControlSelectedDelta.Y;
				tabBounds.X -= ThemeEngine.Current.TabControlSelectedDelta.X;
				break;
			case TabAlignment.Right:
				tabBounds.Y -= ThemeEngine.Current.TabControlSelectedDelta.Y;
				tabBounds.X -= ThemeEngine.Current.TabControlSelectedDelta.X;
				break;
			}
			tabBounds.Width += ThemeEngine.Current.TabControlSelectedDelta.Width;
			tabBounds.Height += ThemeEngine.Current.TabControlSelectedDelta.Height;
			if (tabBounds.Left < left_edge)
			{
				tabBounds.X = left_edge;
			}
			if (tabBounds.Right > right_edge && this.SizeMode != TabSizeMode.Normal && this.alignment != TabAlignment.Right)
			{
				tabBounds.Width = right_edge - tabBounds.X;
			}
			page.TabBounds = tabBounds;
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x000C18D0 File Offset: 0x000BFAD0
		private void Draw(Graphics dc, Rectangle clip)
		{
			ThemeEngine.Current.DrawTabControl(dc, clip, this);
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000C18E0 File Offset: 0x000BFAE0
		private TabPage GetTab(int index)
		{
			return base.Controls[index] as TabPage;
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x000C18F4 File Offset: 0x000BFAF4
		private void SetTab(int index, TabPage value)
		{
			if (!this.tab_pages.Contains(value))
			{
				base.Controls.Add(value);
			}
			base.Controls.RemoveAt(index);
			base.Controls.SetChildIndex(value, index);
			this.Redraw();
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x000C1940 File Offset: 0x000BFB40
		private void InsertTab(int index, TabPage value)
		{
			if (!this.tab_pages.Contains(value))
			{
				base.Controls.Add(value);
			}
			base.Controls.SetChildIndex(value, index);
			this.Redraw();
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000C1980 File Offset: 0x000BFB80
		internal void Redraw()
		{
			if (!base.IsHandleCreated)
			{
				return;
			}
			this.ResizeTabPages();
			this.Refresh();
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x000C199C File Offset: 0x000BFB9C
		private int MeasureStringWidth(Graphics graphics, string text, Font font)
		{
			if (text == string.Empty)
			{
				return 0;
			}
			StringFormat stringFormat = new StringFormat();
			RectangleF bounds;
			bounds..ctor(0f, 0f, 1000f, 1000f);
			CharacterRange[] array = new CharacterRange[]
			{
				new CharacterRange(0, text.Length)
			};
			Region[] array2 = new Region[1];
			stringFormat.SetMeasurableCharacterRanges(array);
			stringFormat.FormatFlags = 16384;
			stringFormat.FormatFlags |= 4096;
			array2 = graphics.MeasureCharacterRanges(text + "I", font, bounds, stringFormat);
			bounds = array2[0].GetBounds(graphics);
			return (int)bounds.Width;
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x000C1A50 File Offset: 0x000BFC50
		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (!this.mouse_down_on_a_tab_page && this.ShowSlider)
			{
				if (this.LeftSliderState == PushButtonState.Pressed || this.RightSliderState == PushButtonState.Pressed)
				{
					return;
				}
				if (this.LeftScrollButtonArea.Contains(e.Location))
				{
					this.LeftSliderState = PushButtonState.Hot;
					this.RightSliderState = PushButtonState.Normal;
					this.EnteredTabPage = null;
					return;
				}
				if (this.RightScrollButtonArea.Contains(e.Location))
				{
					this.RightSliderState = PushButtonState.Hot;
					this.LeftSliderState = PushButtonState.Normal;
					this.EnteredTabPage = null;
					return;
				}
				this.LeftSliderState = PushButtonState.Normal;
				this.RightSliderState = PushButtonState.Normal;
			}
			if (this.EnteredTabPage != null && this.EnteredTabPage.TabBounds.Contains(e.Location))
			{
				return;
			}
			for (int i = 0; i < this.TabCount; i++)
			{
				TabPage tabPage = this.TabPages[i];
				if (tabPage.TabBounds.Contains(e.Location))
				{
					this.EnteredTabPage = tabPage;
					return;
				}
			}
			this.EnteredTabPage = null;
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000C1B70 File Offset: 0x000BFD70
		private void OnMouseLeave(object sender, EventArgs e)
		{
			if (this.ShowSlider)
			{
				this.LeftSliderState = PushButtonState.Normal;
				this.RightSliderState = PushButtonState.Normal;
			}
			this.EnteredTabPage = null;
		}

		// Token: 0x04001827 RID: 6183
		private int selected_index = -1;

		// Token: 0x04001828 RID: 6184
		private TabAlignment alignment;

		// Token: 0x04001829 RID: 6185
		private TabAppearance appearance;

		// Token: 0x0400182A RID: 6186
		private TabDrawMode draw_mode;

		// Token: 0x0400182B RID: 6187
		private bool multiline;

		// Token: 0x0400182C RID: 6188
		private ImageList image_list;

		// Token: 0x0400182D RID: 6189
		private Size item_size = Size.Empty;

		// Token: 0x0400182E RID: 6190
		private Point padding;

		// Token: 0x0400182F RID: 6191
		private int row_count;

		// Token: 0x04001830 RID: 6192
		private bool hottrack;

		// Token: 0x04001831 RID: 6193
		private TabControl.TabPageCollection tab_pages;

		// Token: 0x04001832 RID: 6194
		private bool show_tool_tips;

		// Token: 0x04001833 RID: 6195
		private TabSizeMode size_mode;

		// Token: 0x04001834 RID: 6196
		private bool show_slider;

		// Token: 0x04001835 RID: 6197
		private PushButtonState right_slider_state = PushButtonState.Normal;

		// Token: 0x04001836 RID: 6198
		private PushButtonState left_slider_state = PushButtonState.Normal;

		// Token: 0x04001837 RID: 6199
		private int slider_pos;

		// Token: 0x04001838 RID: 6200
		private TabPage entered_tab_page;

		// Token: 0x04001839 RID: 6201
		private bool mouse_down_on_a_tab_page;

		// Token: 0x0400183A RID: 6202
		private bool rightToLeftLayout;

		/// <summary>Contains a collection of <see cref="T:System.Windows.Forms.Control" /> objects.</summary>
		// Token: 0x020002F7 RID: 759
		[ComVisible(false)]
		public new class ControlCollection : Control.ControlCollection
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabControl.ControlCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.TabControl" /> that this collection belongs to. </param>
			// Token: 0x060032E6 RID: 13030 RVA: 0x000C1BA0 File Offset: 0x000BFDA0
			public ControlCollection(TabControl owner)
				: base(owner)
			{
				this.owner = owner;
			}

			/// <summary>Adds a <see cref="T:System.Windows.Forms.Control" /> to the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.Control" /> to add. </param>
			/// <exception cref="T:System.Exception">The specified <see cref="T:System.Windows.Forms.Control" /> is a <see cref="T:System.Windows.Forms.TabPage" />. </exception>
			// Token: 0x060032E7 RID: 13031 RVA: 0x000C1BB0 File Offset: 0x000BFDB0
			public override void Add(Control value)
			{
				TabPage tabPage = value as TabPage;
				if (tabPage == null)
				{
					throw new ArgumentException("Cannot add " + value.GetType().Name + " to TabControl. Only TabPages can be directly added to TabControls.");
				}
				tabPage.SetVisible(false);
				base.Add(value);
				if (this.owner.TabCount == 1 && this.owner.selected_index < 0)
				{
					this.owner.SelectedIndex = 0;
				}
				this.owner.Redraw();
			}

			/// <summary>Removes a <see cref="T:System.Windows.Forms.Control" /> from the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.Control" /> to remove. </param>
			// Token: 0x060032E8 RID: 13032 RVA: 0x000C1C34 File Offset: 0x000BFE34
			public override void Remove(Control value)
			{
				bool flag = false;
				TabPage tabPage = value as TabPage;
				if (tabPage != null && this.owner.Controls.Contains(tabPage))
				{
					int num = this.owner.IndexForTabPage(tabPage);
					if (num < this.owner.SelectedIndex || this.owner.SelectedIndex == this.Count - 1)
					{
						flag = true;
					}
				}
				base.Remove(value);
				if (flag && this.Count > 0)
				{
					int selectedIndex = this.owner.SelectedIndex;
					this.owner.selected_index = -1;
					this.owner.SelectedIndex = selectedIndex - 1;
				}
				else if (flag)
				{
					this.owner.selected_index = -1;
					this.owner.OnSelectedIndexChanged(EventArgs.Empty);
				}
				else
				{
					this.owner.Redraw();
				}
			}

			// Token: 0x04001844 RID: 6212
			private TabControl owner;
		}

		/// <summary>Contains a collection of <see cref="T:System.Windows.Forms.TabPage" /> objects.</summary>
		// Token: 0x020002F8 RID: 760
		public class TabPageCollection : ICollection, IEnumerable, IList
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" /> class.</summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.TabControl" /> that this collection belongs to. </param>
			/// <exception cref="T:System.ArgumentNullException">The specified <see cref="T:System.Windows.Forms.TabControl" /> is null. </exception>
			// Token: 0x060032E9 RID: 13033 RVA: 0x000C1D18 File Offset: 0x000BFF18
			public TabPageCollection(TabControl owner)
			{
				if (owner == null)
				{
					throw new ArgumentNullException("Value cannot be null.");
				}
				this.owner = owner;
			}

			/// <summary>Gets a value indicating whether access to the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" /> is synchronized (thread safe).</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000D47 RID: 3399
			// (get) Token: 0x060032EA RID: 13034 RVA: 0x000C1D38 File Offset: 0x000BFF38
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" />.</summary>
			/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" />.</returns>
			// Token: 0x17000D48 RID: 3400
			// (get) Token: 0x060032EB RID: 13035 RVA: 0x000C1D3C File Offset: 0x000BFF3C
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			/// <summary>Gets a value indicating whether the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" /> has a fixed size.</summary>
			/// <returns>false in all cases.</returns>
			// Token: 0x17000D49 RID: 3401
			// (get) Token: 0x060032EC RID: 13036 RVA: 0x000C1D40 File Offset: 0x000BFF40
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets or sets a <see cref="T:System.Windows.Forms.TabPage" /> in the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.TabPage" /> at the specified index.</returns>
			/// <param name="index">The zero-based index of the element to get.</param>
			/// <exception cref="T:System.ArgumentException">The value is not a <see cref="T:System.Windows.Forms.TabPage" />.</exception>
			// Token: 0x17000D4A RID: 3402
			// (get) Token: 0x060032ED RID: 13037 RVA: 0x000C1D44 File Offset: 0x000BFF44
			// (set) Token: 0x060032EE RID: 13038 RVA: 0x000C1D54 File Offset: 0x000BFF54
			object IList.Item
			{
				get
				{
					return this.owner.GetTab(index);
				}
				set
				{
					this.owner.SetTab(index, (TabPage)value);
				}
			}

			/// <summary>Copies the elements of the collection to the specified array, starting at the specified index.</summary>
			/// <param name="dest">The one-dimensional array that is the destination of the elements copied from the collection. The array must have zero-based indexing.</param>
			/// <param name="index">The zero-based index in the array at which copying begins.</param>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="dest" /> is null.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero.</exception>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="dest" /> is multidimensional.-or-The number of elements in the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" /> is greater than the available space from index to the end of <paramref name="dest" />.</exception>
			/// <exception cref="T:System.InvalidCastException">The items in the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" /> cannot be cast automatically to the type of <paramref name="dest" />.</exception>
			// Token: 0x060032EF RID: 13039 RVA: 0x000C1D68 File Offset: 0x000BFF68
			void ICollection.CopyTo(Array dest, int index)
			{
				this.owner.Controls.CopyTo(dest, index);
			}

			/// <summary>Adds a <see cref="T:System.Windows.Forms.TabPage" /> control to the collection.</summary>
			/// <returns>The position into which the <see cref="T:System.Windows.Forms.TabPage" /> was inserted.</returns>
			/// <param name="value">The <see cref="T:System.Windows.Forms.TabPage" /> to add to the collection.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="value" /> is not a <see cref="T:System.Windows.Forms.TabPage" />.</exception>
			/// <exception cref="T:System.ArgumentNullException">
			///   <paramref name="value" /> is null.</exception>
			// Token: 0x060032F0 RID: 13040 RVA: 0x000C1D7C File Offset: 0x000BFF7C
			int IList.Add(object value)
			{
				TabPage tabPage = value as TabPage;
				if (value == null)
				{
					throw new ArgumentException("value");
				}
				this.owner.Controls.Add(tabPage);
				return this.owner.Controls.IndexOf(tabPage);
			}

			/// <summary>Determines whether the specified <see cref="T:System.Windows.Forms.TabPage" /> control is in the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" />.</summary>
			/// <returns>true if the specified object is a <see cref="T:System.Windows.Forms.TabPage" /> in the collection; otherwise, false.</returns>
			/// <param name="page">The object to locate in the collection.</param>
			// Token: 0x060032F1 RID: 13041 RVA: 0x000C1DC4 File Offset: 0x000BFFC4
			bool IList.Contains(object page)
			{
				TabPage tabPage = page as TabPage;
				return tabPage != null && this.Contains(tabPage);
			}

			/// <summary>Returns the index of the specified <see cref="T:System.Windows.Forms.TabPage" /> control in the collection.</summary>
			/// <returns>The zero-based index if page is a <see cref="T:System.Windows.Forms.TabPage" /> in the collection; otherwise -1.</returns>
			/// <param name="page">The <see cref="T:System.Windows.Forms.TabPage" /> to locate in the collection.</param>
			// Token: 0x060032F2 RID: 13042 RVA: 0x000C1DE8 File Offset: 0x000BFFE8
			int IList.IndexOf(object page)
			{
				TabPage tabPage = page as TabPage;
				if (tabPage == null)
				{
					return -1;
				}
				return this.IndexOf(tabPage);
			}

			/// <summary>Inserts a <see cref="T:System.Windows.Forms.TabPage" /> control into the collection.</summary>
			/// <param name="index">The zero-based index at which the <see cref="T:System.Windows.Forms.TabPage" /> should be inserted.</param>
			/// <param name="tabPage">The <see cref="T:System.Windows.Forms.TabPage" /> to insert into the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" />.</param>
			/// <exception cref="T:System.ArgumentException">
			///   <paramref name="tabPage" /> is not a <see cref="T:System.Windows.Forms.TabPage" />.</exception>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than 0, or index is greater than or equal to <see cref="P:System.Windows.Forms.TabControl.TabPageCollection.Count" />.</exception>
			// Token: 0x060032F3 RID: 13043 RVA: 0x000C1E0C File Offset: 0x000C000C
			void IList.Insert(int index, object tabPage)
			{
				throw new NotSupportedException();
			}

			/// <summary>Removes a <see cref="T:System.Windows.Forms.TabPage" /> from the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.TabPage" /> to remove.</param>
			// Token: 0x060032F4 RID: 13044 RVA: 0x000C1E14 File Offset: 0x000C0014
			void IList.Remove(object value)
			{
				if (!(value is TabPage))
				{
					return;
				}
				this.Remove((TabPage)value);
			}

			/// <summary>Gets the number of tab pages in the collection.</summary>
			/// <returns>The number of tab pages in the collection.</returns>
			// Token: 0x17000D4B RID: 3403
			// (get) Token: 0x060032F5 RID: 13045 RVA: 0x000C1E3C File Offset: 0x000C003C
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.owner.Controls.Count;
				}
			}

			/// <summary>Gets a value indicating whether the collection is read-only.</summary>
			/// <returns>This property always returns false.</returns>
			// Token: 0x17000D4C RID: 3404
			// (get) Token: 0x060032F6 RID: 13046 RVA: 0x000C1E50 File Offset: 0x000C0050
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			/// <summary>Gets or sets a <see cref="T:System.Windows.Forms.TabPage" /> in the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.TabPage" /> at the specified index.</returns>
			/// <param name="index">The zero-based index of the tab page to get or set. </param>
			/// <exception cref="T:System.ArgumentOutOfRangeException">
			///   <paramref name="index" /> is less than zero or greater than the highest available index. </exception>
			// Token: 0x17000D4D RID: 3405
			public virtual TabPage this[int index]
			{
				get
				{
					return this.owner.GetTab(index);
				}
				set
				{
					this.owner.SetTab(index, value);
				}
			}

			/// <summary>Gets a tab page with the specified key from the collection.</summary>
			/// <returns>The <see cref="T:System.Windows.Forms.TabPage" /> with the specified key.</returns>
			/// <param name="key">The name of the tab page to retrieve.</param>
			// Token: 0x17000D4E RID: 3406
			public virtual TabPage this[string key]
			{
				get
				{
					if (string.IsNullOrEmpty(key))
					{
						return null;
					}
					int num = this.IndexOfKey(key);
					if (num < 0 || num >= this.Count)
					{
						return null;
					}
					return this[num];
				}
			}

			// Token: 0x17000D4F RID: 3407
			internal int this[TabPage tabPage]
			{
				get
				{
					if (tabPage == null)
					{
						return -1;
					}
					for (int i = 0; i < this.Count; i++)
					{
						if (this[i].Equals(tabPage))
						{
							return i;
						}
					}
					return -1;
				}
			}

			/// <summary>Adds a <see cref="T:System.Windows.Forms.TabPage" /> to the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.TabPage" /> to add. </param>
			/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="value" /> is null. </exception>
			// Token: 0x060032FB RID: 13051 RVA: 0x000C1EF8 File Offset: 0x000C00F8
			public void Add(TabPage value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("Value cannot be null.");
				}
				this.owner.Controls.Add(value);
			}

			/// <summary>Creates a tab page with the specified text, and adds it to the collection.</summary>
			/// <param name="text">The text to display on the tab page.</param>
			// Token: 0x060032FC RID: 13052 RVA: 0x000C1F28 File Offset: 0x000C0128
			public void Add(string text)
			{
				TabPage tabPage = new TabPage(text);
				this.Add(tabPage);
			}

			/// <summary>Creates a tab page with the specified text and key, and adds it to the collection.</summary>
			/// <param name="key">The name of the tab page.</param>
			/// <param name="text">The text to display on the tab page.</param>
			// Token: 0x060032FD RID: 13053 RVA: 0x000C1F44 File Offset: 0x000C0144
			public void Add(string key, string text)
			{
				this.Add(new TabPage(text)
				{
					Name = key
				});
			}

			/// <summary>Creates a tab page with the specified key, text, and image, and adds it to the collection.</summary>
			/// <param name="key">The name of the tab page.</param>
			/// <param name="text">The text to display on the tab page.</param>
			/// <param name="imageIndex">The index of the image to display on the tab page.</param>
			// Token: 0x060032FE RID: 13054 RVA: 0x000C1F68 File Offset: 0x000C0168
			public void Add(string key, string text, int imageIndex)
			{
				this.Add(new TabPage(text)
				{
					Name = key,
					ImageIndex = imageIndex
				});
			}

			/// <summary>Creates a tab page with the specified key, text, and image, and adds it to the collection.</summary>
			/// <param name="key">The name of the tab page.</param>
			/// <param name="text">The text to display on the tab page.</param>
			/// <param name="imageKey">The key of the image to display on the tab page.</param>
			// Token: 0x060032FF RID: 13055 RVA: 0x000C1F94 File Offset: 0x000C0194
			public void Add(string key, string text, string imageKey)
			{
				this.Add(new TabPage(text)
				{
					Name = key,
					ImageKey = imageKey
				});
			}

			/// <summary>Adds a set of tab pages to the collection.</summary>
			/// <param name="pages">An array of type <see cref="T:System.Windows.Forms.TabPage" /> that contains the tab pages to add. </param>
			/// <exception cref="T:System.ArgumentNullException">The value of pages equals null. </exception>
			// Token: 0x06003300 RID: 13056 RVA: 0x000C1FC0 File Offset: 0x000C01C0
			public void AddRange(TabPage[] pages)
			{
				if (pages == null)
				{
					throw new ArgumentNullException("Value cannot be null.");
				}
				this.owner.Controls.AddRange(pages);
			}

			/// <summary>Removes all the tab pages from the collection.</summary>
			// Token: 0x06003301 RID: 13057 RVA: 0x000C1FF0 File Offset: 0x000C01F0
			public virtual void Clear()
			{
				this.owner.Controls.Clear();
				this.owner.Invalidate();
			}

			/// <summary>Determines whether a specified tab page is in the collection.</summary>
			/// <returns>true if the specified <see cref="T:System.Windows.Forms.TabPage" /> is in the collection; otherwise, false.</returns>
			/// <param name="page">The <see cref="T:System.Windows.Forms.TabPage" /> to locate in the collection. </param>
			/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="page" /> is null. </exception>
			// Token: 0x06003302 RID: 13058 RVA: 0x000C2010 File Offset: 0x000C0210
			public bool Contains(TabPage page)
			{
				if (page == null)
				{
					throw new ArgumentNullException("Value cannot be null.");
				}
				return this.owner.Controls.Contains(page);
			}

			/// <summary>Determines whether the collection contains a tab page with the specified key.</summary>
			/// <returns>true to indicate a tab page with the specified key was found in the collection; otherwise, false. </returns>
			/// <param name="key">The name of the tab page to search for.</param>
			// Token: 0x06003303 RID: 13059 RVA: 0x000C2040 File Offset: 0x000C0240
			public virtual bool ContainsKey(string key)
			{
				int num = this.IndexOfKey(key);
				return num >= 0 && num < this.Count;
			}

			/// <summary>Returns an enumeration of all the tab pages in the collection.</summary>
			/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Windows.Forms.TabControl.TabPageCollection" />.</returns>
			// Token: 0x06003304 RID: 13060 RVA: 0x000C2068 File Offset: 0x000C0268
			public IEnumerator GetEnumerator()
			{
				return this.owner.Controls.GetEnumerator();
			}

			/// <summary>Returns the index of the specified tab page in the collection.</summary>
			/// <returns>The zero-based index of the tab page; -1 if it cannot be found.</returns>
			/// <param name="page">The <see cref="T:System.Windows.Forms.TabPage" /> to locate in the collection. </param>
			/// <exception cref="T:System.ArgumentNullException">The value of <paramref name="page" /> is null. </exception>
			// Token: 0x06003305 RID: 13061 RVA: 0x000C207C File Offset: 0x000C027C
			public int IndexOf(TabPage page)
			{
				return this.owner.Controls.IndexOf(page);
			}

			/// <summary>Returns the index of the first occurrence of the <see cref="T:System.Windows.Forms.TabPage" /> with the specified key.</summary>
			/// <returns>The zero-based index of the first occurrence of a tab page with the specified key, if found; otherwise, -1.</returns>
			/// <param name="key">The name of the tab page to find in the collection.</param>
			// Token: 0x06003306 RID: 13062 RVA: 0x000C2090 File Offset: 0x000C0290
			public virtual int IndexOfKey(string key)
			{
				if (string.IsNullOrEmpty(key))
				{
					return -1;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (string.Compare(this[i].Name, key, true, CultureInfo.InvariantCulture) == 0)
					{
						return i;
					}
				}
				return -1;
			}

			/// <summary>Removes a <see cref="T:System.Windows.Forms.TabPage" /> from the collection.</summary>
			/// <param name="value">The <see cref="T:System.Windows.Forms.TabPage" /> to remove. </param>
			/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
			// Token: 0x06003307 RID: 13063 RVA: 0x000C20E4 File Offset: 0x000C02E4
			public void Remove(TabPage value)
			{
				this.owner.Controls.Remove(value);
				this.owner.Invalidate();
			}

			/// <summary>Removes the tab page at the specified index from the collection.</summary>
			/// <param name="index">The zero-based index of the <see cref="T:System.Windows.Forms.TabPage" /> to remove. </param>
			// Token: 0x06003308 RID: 13064 RVA: 0x000C2104 File Offset: 0x000C0304
			public void RemoveAt(int index)
			{
				this.owner.Controls.RemoveAt(index);
				this.owner.Invalidate();
			}

			/// <summary>Removes the tab page with the specified key from the collection.</summary>
			/// <param name="key">The name of the tab page to remove.</param>
			// Token: 0x06003309 RID: 13065 RVA: 0x000C2124 File Offset: 0x000C0324
			public virtual void RemoveByKey(string key)
			{
				int num = this.IndexOfKey(key);
				if (num >= 0 && num < this.Count)
				{
					this.RemoveAt(num);
				}
			}

			/// <summary>Creates a new tab page with the specified text and inserts it into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the tab page is inserted.</param>
			/// <param name="text">The text to display in the tab page.</param>
			// Token: 0x0600330A RID: 13066 RVA: 0x000C2154 File Offset: 0x000C0354
			public void Insert(int index, string text)
			{
				this.owner.InsertTab(index, new TabPage(text));
			}

			/// <summary>Inserts an existing tab page into the collection at the specified index. </summary>
			/// <param name="index">The zero-based index location where the tab page is inserted.</param>
			/// <param name="tabPage">The <see cref="T:System.Windows.Forms.TabPage" /> to insert in the collection.</param>
			// Token: 0x0600330B RID: 13067 RVA: 0x000C2168 File Offset: 0x000C0368
			public void Insert(int index, TabPage tabPage)
			{
				this.owner.InsertTab(index, tabPage);
			}

			/// <summary>Creates a new tab page with the specified key and text, and inserts it into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the tab page is inserted.</param>
			/// <param name="key">The name of the tab page.</param>
			/// <param name="text">The text to display on the tab page.</param>
			// Token: 0x0600330C RID: 13068 RVA: 0x000C2178 File Offset: 0x000C0378
			public void Insert(int index, string key, string text)
			{
				TabPage tabPage = new TabPage(text);
				tabPage.Name = key;
				this.owner.InsertTab(index, tabPage);
			}

			/// <summary>Creates a new tab page with the specified key, text, and image, and inserts it into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the tab page is inserted</param>
			/// <param name="key">The name of the tab page.</param>
			/// <param name="text">The text to display on the tab page</param>
			/// <param name="imageIndex">The zero-based index of the image to display on the tab page.</param>
			// Token: 0x0600330D RID: 13069 RVA: 0x000C21A0 File Offset: 0x000C03A0
			public void Insert(int index, string key, string text, int imageIndex)
			{
				TabPage tabPage = new TabPage(text);
				tabPage.Name = key;
				this.owner.InsertTab(index, tabPage);
				tabPage.ImageIndex = imageIndex;
			}

			/// <summary>Creates a tab page with the specified key, text, and image, and inserts it into the collection at the specified index.</summary>
			/// <param name="index">The zero-based index location where the tab page is inserted.</param>
			/// <param name="key">The name of the tab page.</param>
			/// <param name="text">The text to display on the tab page.</param>
			/// <param name="imageKey">The key of the image to display on the tab page.</param>
			// Token: 0x0600330E RID: 13070 RVA: 0x000C21D0 File Offset: 0x000C03D0
			public void Insert(int index, string key, string text, string imageKey)
			{
				TabPage tabPage = new TabPage(text);
				tabPage.Name = key;
				this.owner.InsertTab(index, tabPage);
				tabPage.ImageKey = imageKey;
			}

			// Token: 0x04001845 RID: 6213
			private TabControl owner;
		}
	}
}
