using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows progress bar control.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200029A RID: 666
	[DefaultBindingProperty("Value")]
	[ComVisible(true)]
	[DefaultProperty("Value")]
	[ClassInterface(1)]
	public class ProgressBar : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ProgressBar" /> class.</summary>
		// Token: 0x06002C1D RID: 11293 RVA: 0x000AB374 File Offset: 0x000A9574
		public ProgressBar()
		{
			this.maximum = 100;
			this.minimum = 0;
			this.step = 10;
			this.val = 0;
			base.Resize += new EventHandler(this.OnResizeTB);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.UseTextForAccessibility, false);
			this.force_double_buffer = true;
			this.ForeColor = ProgressBar.defaultForeColor;
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x000AB3F8 File Offset: 0x000A95F8
		// Note: this type is marked as 'beforefieldinit'.
		static ProgressBar()
		{
			ProgressBar.RightToLeftLayoutChangedEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.BackgroundImage" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400029B RID: 667
		// (add) Token: 0x06002C1F RID: 11295 RVA: 0x000AB410 File Offset: 0x000A9610
		// (remove) Token: 0x06002C20 RID: 11296 RVA: 0x000AB41C File Offset: 0x000A961C
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400029C RID: 668
		// (add) Token: 0x06002C21 RID: 11297 RVA: 0x000AB428 File Offset: 0x000A9628
		// (remove) Token: 0x06002C22 RID: 11298 RVA: 0x000AB434 File Offset: 0x000A9634
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.CausesValidation" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400029D RID: 669
		// (add) Token: 0x06002C23 RID: 11299 RVA: 0x000AB440 File Offset: 0x000A9640
		// (remove) Token: 0x06002C24 RID: 11300 RVA: 0x000AB44C File Offset: 0x000A964C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler CausesValidationChanged
		{
			add
			{
				base.CausesValidationChanged += value;
			}
			remove
			{
				base.CausesValidationChanged -= value;
			}
		}

		/// <summary>Occurs when the user double-clicks the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400029E RID: 670
		// (add) Token: 0x06002C25 RID: 11301 RVA: 0x000AB458 File Offset: 0x000A9658
		// (remove) Token: 0x06002C26 RID: 11302 RVA: 0x000AB464 File Offset: 0x000A9664
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when focus enters the <see cref="T:System.Windows.Forms.ProgressBar" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400029F RID: 671
		// (add) Token: 0x06002C27 RID: 11303 RVA: 0x000AB470 File Offset: 0x000A9670
		// (remove) Token: 0x06002C28 RID: 11304 RVA: 0x000AB47C File Offset: 0x000A967C
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.Font" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002A0 RID: 672
		// (add) Token: 0x06002C29 RID: 11305 RVA: 0x000AB488 File Offset: 0x000A9688
		// (remove) Token: 0x06002C2A RID: 11306 RVA: 0x000AB494 File Offset: 0x000A9694
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.ImeMode" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002A1 RID: 673
		// (add) Token: 0x06002C2B RID: 11307 RVA: 0x000AB4A0 File Offset: 0x000A96A0
		// (remove) Token: 0x06002C2C RID: 11308 RVA: 0x000AB4AC File Offset: 0x000A96AC
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

		/// <summary>Occurs when the user presses a key while the control has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002A2 RID: 674
		// (add) Token: 0x06002C2D RID: 11309 RVA: 0x000AB4B8 File Offset: 0x000A96B8
		// (remove) Token: 0x06002C2E RID: 11310 RVA: 0x000AB4C4 File Offset: 0x000A96C4
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the user presses a key while the control has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002A3 RID: 675
		// (add) Token: 0x06002C2F RID: 11311 RVA: 0x000AB4D0 File Offset: 0x000A96D0
		// (remove) Token: 0x06002C30 RID: 11312 RVA: 0x000AB4DC File Offset: 0x000A96DC
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the user releases a key while the control has focus.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002A4 RID: 676
		// (add) Token: 0x06002C31 RID: 11313 RVA: 0x000AB4E8 File Offset: 0x000A96E8
		// (remove) Token: 0x06002C32 RID: 11314 RVA: 0x000AB4F4 File Offset: 0x000A96F4
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

		/// <summary>Occurs when focus leaves the <see cref="T:System.Windows.Forms.ProgressBar" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002A5 RID: 677
		// (add) Token: 0x06002C33 RID: 11315 RVA: 0x000AB500 File Offset: 0x000A9700
		// (remove) Token: 0x06002C34 RID: 11316 RVA: 0x000AB50C File Offset: 0x000A970C
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

		/// <summary>Occurs when the user double-clicks the control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002A6 RID: 678
		// (add) Token: 0x06002C35 RID: 11317 RVA: 0x000AB518 File Offset: 0x000A9718
		// (remove) Token: 0x06002C36 RID: 11318 RVA: 0x000AB524 File Offset: 0x000A9724
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ProgressBar.Padding" /> property changes.</summary>
		// Token: 0x140002A7 RID: 679
		// (add) Token: 0x06002C37 RID: 11319 RVA: 0x000AB530 File Offset: 0x000A9730
		// (remove) Token: 0x06002C38 RID: 11320 RVA: 0x000AB53C File Offset: 0x000A973C
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.ProgressBar" /> is drawn.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002A8 RID: 680
		// (add) Token: 0x06002C39 RID: 11321 RVA: 0x000AB548 File Offset: 0x000A9748
		// (remove) Token: 0x06002C3A RID: 11322 RVA: 0x000AB554 File Offset: 0x000A9754
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ProgressBar.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x140002A9 RID: 681
		// (add) Token: 0x06002C3B RID: 11323 RVA: 0x000AB560 File Offset: 0x000A9760
		// (remove) Token: 0x06002C3C RID: 11324 RVA: 0x000AB574 File Offset: 0x000A9774
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.Events.AddHandler(ProgressBar.RightToLeftLayoutChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ProgressBar.RightToLeftLayoutChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ProgressBar.TabStop" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002AA RID: 682
		// (add) Token: 0x06002C3D RID: 11325 RVA: 0x000AB588 File Offset: 0x000A9788
		// (remove) Token: 0x06002C3E RID: 11326 RVA: 0x000AB594 File Offset: 0x000A9794
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.ProgressBar.Text" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x140002AB RID: 683
		// (add) Token: 0x06002C3F RID: 11327 RVA: 0x000AB5A0 File Offset: 0x000A97A0
		// (remove) Token: 0x06002C40 RID: 11328 RVA: 0x000AB5AC File Offset: 0x000A97AC
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

		/// <summary>Overrides <see cref="P:System.Windows.Forms.Control.AllowDrop" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x06002C41 RID: 11329 RVA: 0x000AB5B8 File Offset: 0x000A97B8
		// (set) Token: 0x06002C42 RID: 11330 RVA: 0x000AB5C0 File Offset: 0x000A97C0
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Gets or sets the background image for the <see cref="T:System.Windows.Forms.ProgressBar" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x06002C43 RID: 11331 RVA: 0x000AB5CC File Offset: 0x000A97CC
		// (set) Token: 0x06002C44 RID: 11332 RVA: 0x000AB5D4 File Offset: 0x000A97D4
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

		/// <summary>Gets or sets the layout of the background image of the progress bar.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x06002C45 RID: 11333 RVA: 0x000AB5E0 File Offset: 0x000A97E0
		// (set) Token: 0x06002C46 RID: 11334 RVA: 0x000AB5E8 File Offset: 0x000A97E8
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

		/// <summary>Gets or sets a value indicating whether the control, when it receives focus, causes validation to be performed on any controls that require validation.</summary>
		/// <returns>true if the control, when it receives focus, causes validation to be performed on any controls that require validation; otherwise, false. The default is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x06002C47 RID: 11335 RVA: 0x000AB5F4 File Offset: 0x000A97F4
		// (set) Token: 0x06002C48 RID: 11336 RVA: 0x000AB5FC File Offset: 0x000A97FC
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new bool CausesValidation
		{
			get
			{
				return base.CausesValidation;
			}
			set
			{
				base.CausesValidation = value;
			}
		}

		/// <summary>Overrides <see cref="P:System.Windows.Forms.Control.CreateParams" />.</summary>
		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06002C49 RID: 11337 RVA: 0x000AB608 File Offset: 0x000A9808
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06002C4A RID: 11338 RVA: 0x000AB610 File Offset: 0x000A9810
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return base.DefaultImeMode;
			}
		}

		/// <summary>Gets the default size of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the default size of the control.</returns>
		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06002C4B RID: 11339 RVA: 0x000AB618 File Offset: 0x000A9818
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.ProgressBarDefaultSize;
			}
		}

		/// <summary>Gets or sets a value indicating whether the control should redraw its surface using a secondary buffer.</summary>
		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06002C4C RID: 11340 RVA: 0x000AB624 File Offset: 0x000A9824
		// (set) Token: 0x06002C4D RID: 11341 RVA: 0x000AB62C File Offset: 0x000A982C
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

		/// <summary>Gets or sets the font of text in the <see cref="T:System.Windows.Forms.ProgressBar" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Font" /> of the text. The default is the font set by the container.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06002C4E RID: 11342 RVA: 0x000AB638 File Offset: 0x000A9838
		// (set) Token: 0x06002C4F RID: 11343 RVA: 0x000AB640 File Offset: 0x000A9840
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
				base.Font = value;
			}
		}

		/// <summary>Gets or sets the input method editor (IME) for the <see cref="T:System.Windows.Forms.ProgressBar" /></summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06002C50 RID: 11344 RVA: 0x000AB64C File Offset: 0x000A984C
		// (set) Token: 0x06002C51 RID: 11345 RVA: 0x000AB654 File Offset: 0x000A9854
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Gets or sets the maximum value of the range of the control.</summary>
		/// <returns>The maximum value of the range. The default is 100.</returns>
		/// <exception cref="T:System.ArgumentException">The value specified is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06002C52 RID: 11346 RVA: 0x000AB660 File Offset: 0x000A9860
		// (set) Token: 0x06002C53 RID: 11347 RVA: 0x000AB668 File Offset: 0x000A9868
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
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("Maximum", string.Format("Value '{0}' must be greater than or equal to 0.", value));
				}
				this.maximum = value;
				this.minimum = Math.Min(this.minimum, this.maximum);
				this.val = Math.Min(this.val, this.maximum);
				this.Refresh();
			}
		}

		/// <summary>Gets or sets the minimum value of the range of the control.</summary>
		/// <returns>The minimum value of the range. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentException">The value specified for the property is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06002C54 RID: 11348 RVA: 0x000AB6D4 File Offset: 0x000A98D4
		// (set) Token: 0x06002C55 RID: 11349 RVA: 0x000AB6DC File Offset: 0x000A98DC
		[DefaultValue(0)]
		[RefreshProperties(2)]
		public int Minimum
		{
			get
			{
				return this.minimum;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("Minimum", string.Format("Value '{0}' must be greater than or equal to 0.", value));
				}
				this.minimum = value;
				this.maximum = Math.Max(this.maximum, this.minimum);
				this.val = Math.Max(this.val, this.minimum);
				this.Refresh();
			}
		}

		/// <summary>Gets or sets the space between the edges of a <see cref="T:System.Windows.Forms.ProgressBar" /> control and its contents.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.Padding.Empty" /> in all cases.</returns>
		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06002C56 RID: 11350 RVA: 0x000AB748 File Offset: 0x000A9948
		// (set) Token: 0x06002C57 RID: 11351 RVA: 0x000AB750 File Offset: 0x000A9950
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ProgressBar" /> and any text it contains is displayed from right to left. </summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.ProgressBar" /> is displayed from right to left; otherwise, false. The default is false.</returns>
		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06002C58 RID: 11352 RVA: 0x000AB75C File Offset: 0x000A995C
		// (set) Token: 0x06002C59 RID: 11353 RVA: 0x000AB764 File Offset: 0x000A9964
		[DefaultValue(false)]
		[MonoTODO("RTL is not supported")]
		[Localizable(true)]
		public virtual bool RightToLeftLayout
		{
			get
			{
				return this.right_to_left_layout;
			}
			set
			{
				if (this.right_to_left_layout != value)
				{
					this.right_to_left_layout = value;
					this.OnRightToLeftLayoutChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the amount by which a call to the <see cref="M:System.Windows.Forms.ProgressBar.PerformStep" /> method increases the current position of the progress bar.</summary>
		/// <returns>The amount by which to increment the progress bar with each call to the <see cref="M:System.Windows.Forms.ProgressBar.PerformStep" /> method. The default is 10.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06002C5A RID: 11354 RVA: 0x000AB784 File Offset: 0x000A9984
		// (set) Token: 0x06002C5B RID: 11355 RVA: 0x000AB78C File Offset: 0x000A998C
		[DefaultValue(10)]
		public int Step
		{
			get
			{
				return this.step;
			}
			set
			{
				this.step = value;
				this.Refresh();
			}
		}

		/// <summary>Gets or sets the manner in which progress should be indicated on the progress bar.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ProgressBarStyle" /> values. The default is <see cref="F:System.Windows.Forms.ProgressBarStyle.Blocks" /></returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not a member of the <see cref="T:System.Windows.Forms.ProgressBarStyle" /> enumeration. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06002C5C RID: 11356 RVA: 0x000AB79C File Offset: 0x000A999C
		// (set) Token: 0x06002C5D RID: 11357 RVA: 0x000AB7A4 File Offset: 0x000A99A4
		[Browsable(true)]
		[DefaultValue(ProgressBarStyle.Blocks)]
		[EditorBrowsable(0)]
		public ProgressBarStyle Style
		{
			get
			{
				return this.style;
			}
			set
			{
				if (value != ProgressBarStyle.Blocks && value != ProgressBarStyle.Continuous && value != ProgressBarStyle.Marquee)
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ProgressBarStyle));
				}
				if (this.style != value)
				{
					this.style = value;
					if (this.style == ProgressBarStyle.Marquee)
					{
						if (this.marquee_timer == null)
						{
							this.marquee_timer = new Timer();
							this.marquee_timer.Interval = 10;
							this.marquee_timer.Tick += new EventHandler(this.marquee_timer_Tick);
						}
						this.marquee_timer.Start();
					}
					else
					{
						if (this.marquee_timer != null)
						{
							this.marquee_timer.Stop();
						}
						this.Refresh();
					}
				}
			}
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x000AB860 File Offset: 0x000A9A60
		private void marquee_timer_Tick(object sender, EventArgs e)
		{
			base.Invalidate();
		}

		/// <summary>Gets or sets the time period, in milliseconds, that it takes the progress block to scroll across the progress bar.</summary>
		/// <returns>The time period, in milliseconds, that it takes the progress block to scroll across the progress bar.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The indicated time period is less than 0.</exception>
		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06002C5F RID: 11359 RVA: 0x000AB868 File Offset: 0x000A9A68
		// (set) Token: 0x06002C60 RID: 11360 RVA: 0x000AB870 File Offset: 0x000A9A70
		[DefaultValue(100)]
		public int MarqueeAnimationSpeed
		{
			get
			{
				return this.marquee_animation_speed;
			}
			set
			{
				this.marquee_animation_speed = value;
			}
		}

		/// <summary>Overrides <see cref="P:System.Windows.Forms.Control.TabStop" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x06002C61 RID: 11361 RVA: 0x000AB87C File Offset: 0x000A9A7C
		// (set) Token: 0x06002C62 RID: 11362 RVA: 0x000AB884 File Offset: 0x000A9A84
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

		/// <summary>Overrides <see cref="P:System.Windows.Forms.Control.Text" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x06002C63 RID: 11363 RVA: 0x000AB890 File Offset: 0x000A9A90
		// (set) Token: 0x06002C64 RID: 11364 RVA: 0x000AB898 File Offset: 0x000A9A98
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
				base.Text = value;
			}
		}

		/// <summary>Gets or sets the current position of the progress bar.</summary>
		/// <returns>The position within the range of the progress bar. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentException">The value specified is greater than the value of the <see cref="P:System.Windows.Forms.ProgressBar.Maximum" /> property.-or- The value specified is less than the value of the <see cref="P:System.Windows.Forms.ProgressBar.Minimum" /> property. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x06002C65 RID: 11365 RVA: 0x000AB8A4 File Offset: 0x000A9AA4
		// (set) Token: 0x06002C66 RID: 11366 RVA: 0x000AB8AC File Offset: 0x000A9AAC
		[DefaultValue(0)]
		[Bindable(true)]
		public int Value
		{
			get
			{
				return this.val;
			}
			set
			{
				if (value < this.Minimum || value > this.Maximum)
				{
					throw new ArgumentOutOfRangeException("Value", string.Format("'{0}' is not a valid value for 'Value'. 'Value' should be between 'Minimum' and 'Maximum'", value));
				}
				this.val = value;
				this.Refresh();
			}
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x000AB8FC File Offset: 0x000A9AFC
		protected override void CreateHandle()
		{
			base.CreateHandle();
		}

		/// <summary>Advances the current position of the progress bar by the specified amount.</summary>
		/// <param name="value">The amount by which to increment the progress bar's current position. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Windows.Forms.ProgressBar.Style" /> property is set to <see cref="F:System.Windows.Forms.ProgressBarStyle.Marquee" /></exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002C68 RID: 11368 RVA: 0x000AB904 File Offset: 0x000A9B04
		public void Increment(int value)
		{
			if (this.Style == ProgressBarStyle.Marquee)
			{
				throw new InvalidOperationException("Increment should not be called if the style is Marquee.");
			}
			int num = this.Value + value;
			if (num < this.Minimum)
			{
				num = this.Minimum;
			}
			if (num > this.Maximum)
			{
				num = this.Maximum;
			}
			this.Value = num;
			this.Refresh();
		}

		/// <summary>Overrides <see cref="M:System.Windows.Forms.Control.OnHandleCreated(System.EventArgs)" /></summary>
		// Token: 0x06002C69 RID: 11369 RVA: 0x000AB964 File Offset: 0x000A9B64
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.UpdateAreas();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.BackColorChanged" /> event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002C6A RID: 11370 RVA: 0x000AB974 File Offset: 0x000A9B74
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.ForeColorChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C6B RID: 11371 RVA: 0x000AB980 File Offset: 0x000A9B80
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002C6C RID: 11372 RVA: 0x000AB98C File Offset: 0x000A9B8C
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="P:System.Windows.Forms.ProgressBar.RightToLeftLayout" /> event. </summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002C6D RID: 11373 RVA: 0x000AB998 File Offset: 0x000A9B98
		[EditorBrowsable(2)]
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ProgressBar.RightToLeftLayoutChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Advances the current position of the progress bar by the amount of the <see cref="P:System.Windows.Forms.ProgressBar.Step" /> property.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Windows.Forms.ProgressBar.Style" /> is set to <see cref="F:System.Windows.Forms.ProgressBarStyle.Marquee" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002C6E RID: 11374 RVA: 0x000AB9CC File Offset: 0x000A9BCC
		public void PerformStep()
		{
			if (this.Style == ProgressBarStyle.Marquee)
			{
				throw new InvalidOperationException("PerformStep should not be called if the style is Marquee.");
			}
			this.Increment(this.Step);
		}

		/// <summary>Resets the <see cref="P:System.Windows.Forms.Control.ForeColor" /> to its default value.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002C6F RID: 11375 RVA: 0x000AB9F4 File Offset: 0x000A9BF4
		[EditorBrowsable(1)]
		public override void ResetForeColor()
		{
			this.ForeColor = ProgressBar.defaultForeColor;
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.ProgressBar" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.ProgressBar" />. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002C70 RID: 11376 RVA: 0x000ABA04 File Offset: 0x000A9C04
		public override string ToString()
		{
			return string.Format("{0}, Minimum: {1}, Maximum: {2}, Value: {3}", new object[]
			{
				base.GetType().FullName,
				this.Minimum.ToString(),
				this.Maximum.ToString(),
				this.Value.ToString()
			});
		}

		// Token: 0x06002C71 RID: 11377 RVA: 0x000ABA64 File Offset: 0x000A9C64
		private void UpdateAreas()
		{
			int num = 2;
			this.client_area.Y = num;
			this.client_area.X = num;
			this.client_area.Width = base.Width - 4;
			this.client_area.Height = base.Height - 4;
		}

		// Token: 0x06002C72 RID: 11378 RVA: 0x000ABAB4 File Offset: 0x000A9CB4
		private void OnResizeTB(object o, EventArgs e)
		{
			if (base.Width <= 0 || base.Height <= 0)
			{
				return;
			}
			this.UpdateAreas();
			base.Invalidate();
		}

		// Token: 0x06002C73 RID: 11379 RVA: 0x000ABADC File Offset: 0x000A9CDC
		internal override void OnPaintInternal(PaintEventArgs pevent)
		{
			ThemeEngine.Current.DrawProgressBar(pevent.Graphics, pevent.ClipRectangle, this);
		}

		// Token: 0x04001597 RID: 5527
		private int maximum;

		// Token: 0x04001598 RID: 5528
		private int minimum;

		// Token: 0x04001599 RID: 5529
		internal int step;

		// Token: 0x0400159A RID: 5530
		internal int val;

		// Token: 0x0400159B RID: 5531
		internal DateTime start = DateTime.Now;

		// Token: 0x0400159C RID: 5532
		internal Rectangle client_area = default(Rectangle);

		// Token: 0x0400159D RID: 5533
		internal ProgressBarStyle style;

		// Token: 0x0400159E RID: 5534
		private Timer marquee_timer;

		// Token: 0x0400159F RID: 5535
		private bool right_to_left_layout;

		// Token: 0x040015A0 RID: 5536
		private static readonly Color defaultForeColor = SystemColors.Highlight;

		// Token: 0x040015A2 RID: 5538
		private int marquee_animation_speed = 100;
	}
}
