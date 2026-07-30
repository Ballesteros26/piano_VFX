using System;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows progress bar control contained in a <see cref="T:System.Windows.Forms.StatusStrip" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000374 RID: 884
	[DefaultProperty("Value")]
	public class ToolStripProgressBar : ToolStripControlHost
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" /> class. </summary>
		// Token: 0x06003F63 RID: 16227 RVA: 0x000FDC50 File Offset: 0x000FBE50
		public ToolStripProgressBar()
			: base(new ProgressBar())
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" /> class with specified name. </summary>
		/// <param name="name">The name of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" />.</param>
		// Token: 0x06003F64 RID: 16228 RVA: 0x000FDC60 File Offset: 0x000FBE60
		public ToolStripProgressBar(string name)
			: this()
		{
			base.Name = name;
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x140003CF RID: 975
		// (add) Token: 0x06003F65 RID: 16229 RVA: 0x000FDC70 File Offset: 0x000FBE70
		// (remove) Token: 0x06003F66 RID: 16230 RVA: 0x000FDC7C File Offset: 0x000FBE7C
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x140003D0 RID: 976
		// (add) Token: 0x06003F67 RID: 16231 RVA: 0x000FDC88 File Offset: 0x000FBE88
		// (remove) Token: 0x06003F68 RID: 16232 RVA: 0x000FDC94 File Offset: 0x000FBE94
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x140003D1 RID: 977
		// (add) Token: 0x06003F69 RID: 16233 RVA: 0x000FDCA0 File Offset: 0x000FBEA0
		// (remove) Token: 0x06003F6A RID: 16234 RVA: 0x000FDCAC File Offset: 0x000FBEAC
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x140003D2 RID: 978
		// (add) Token: 0x06003F6B RID: 16235 RVA: 0x000FDCB8 File Offset: 0x000FBEB8
		// (remove) Token: 0x06003F6C RID: 16236 RVA: 0x000FDCC4 File Offset: 0x000FBEC4
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x140003D3 RID: 979
		// (add) Token: 0x06003F6D RID: 16237 RVA: 0x000FDCD0 File Offset: 0x000FBED0
		// (remove) Token: 0x06003F6E RID: 16238 RVA: 0x000FDCDC File Offset: 0x000FBEDC
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler OwnerChanged
		{
			add
			{
				base.OwnerChanged += value;
			}
			remove
			{
				base.OwnerChanged -= value;
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.ToolStripProgressBar.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x140003D4 RID: 980
		// (add) Token: 0x06003F6F RID: 16239 RVA: 0x000FDCE8 File Offset: 0x000FBEE8
		// (remove) Token: 0x06003F70 RID: 16240 RVA: 0x000FDCF8 File Offset: 0x000FBEF8
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				this.ProgressBar.RightToLeftLayoutChanged += value;
			}
			remove
			{
				this.ProgressBar.RightToLeftLayoutChanged -= value;
			}
		}

		/// <summary>This event is not relevant for this class.</summary>
		// Token: 0x140003D5 RID: 981
		// (add) Token: 0x06003F71 RID: 16241 RVA: 0x000FDD08 File Offset: 0x000FBF08
		// (remove) Token: 0x06003F72 RID: 16242 RVA: 0x000FDD14 File Offset: 0x000FBF14
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

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140003D6 RID: 982
		// (add) Token: 0x06003F73 RID: 16243 RVA: 0x000FDD20 File Offset: 0x000FBF20
		// (remove) Token: 0x06003F74 RID: 16244 RVA: 0x000FDD2C File Offset: 0x000FBF2C
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event EventHandler Validated
		{
			add
			{
				base.Validated += value;
			}
			remove
			{
				base.Validated -= value;
			}
		}

		/// <summary>This event is not relevant to this class.</summary>
		// Token: 0x140003D7 RID: 983
		// (add) Token: 0x06003F75 RID: 16245 RVA: 0x000FDD38 File Offset: 0x000FBF38
		// (remove) Token: 0x06003F76 RID: 16246 RVA: 0x000FDD44 File Offset: 0x000FBF44
		[EditorBrowsable(1)]
		[Browsable(false)]
		public new event CancelEventHandler Validating
		{
			add
			{
				base.Validating += value;
			}
			remove
			{
				base.Validating -= value;
			}
		}

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" />.</returns>
		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x06003F77 RID: 16247 RVA: 0x000FDD50 File Offset: 0x000FBF50
		// (set) Token: 0x06003F78 RID: 16248 RVA: 0x000FDD58 File Offset: 0x000FBF58
		[DesignerSerializationVisibility(0)]
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

		/// <summary>This property is not relevant to this class.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.ImageLayout" /> value.</returns>
		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x06003F79 RID: 16249 RVA: 0x000FDD64 File Offset: 0x000FBF64
		// (set) Token: 0x06003F7A RID: 16250 RVA: 0x000FDD6C File Offset: 0x000FBF6C
		[DesignerSerializationVisibility(0)]
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

		/// <summary>Gets or sets a value representing the delay between each <see cref="F:System.Windows.Forms.ProgressBarStyle.Marquee" /> display update, in milliseconds.</summary>
		/// <returns>An integer representing the delay, in milliseconds.</returns>
		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x06003F7B RID: 16251 RVA: 0x000FDD78 File Offset: 0x000FBF78
		// (set) Token: 0x06003F7C RID: 16252 RVA: 0x000FDD88 File Offset: 0x000FBF88
		[DefaultValue(100)]
		public int MarqueeAnimationSpeed
		{
			get
			{
				return this.ProgressBar.MarqueeAnimationSpeed;
			}
			set
			{
				this.ProgressBar.MarqueeAnimationSpeed = value;
			}
		}

		/// <summary>Gets or sets the upper bound of the range that is defined for this <see cref="T:System.Windows.Forms.ToolStripProgressBar" />.</summary>
		/// <returns>An integer representing the upper bound of the range. The default is 100.</returns>
		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x06003F7D RID: 16253 RVA: 0x000FDD98 File Offset: 0x000FBF98
		// (set) Token: 0x06003F7E RID: 16254 RVA: 0x000FDDA8 File Offset: 0x000FBFA8
		[DefaultValue(100)]
		[RefreshProperties(2)]
		public int Maximum
		{
			get
			{
				return this.ProgressBar.Maximum;
			}
			set
			{
				this.ProgressBar.Maximum = value;
			}
		}

		/// <summary>Gets or sets the lower bound of the range that is defined for this <see cref="T:System.Windows.Forms.ToolStripProgressBar" />.</summary>
		/// <returns>An integer representing the lower bound of the range. The default is 0.</returns>
		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x06003F7F RID: 16255 RVA: 0x000FDDB8 File Offset: 0x000FBFB8
		// (set) Token: 0x06003F80 RID: 16256 RVA: 0x000FDDC8 File Offset: 0x000FBFC8
		[DefaultValue(0)]
		[RefreshProperties(2)]
		public int Minimum
		{
			get
			{
				return this.ProgressBar.Minimum;
			}
			set
			{
				this.ProgressBar.Minimum = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Windows.Forms.ProgressBar" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.ProgressBar" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x06003F81 RID: 16257 RVA: 0x000FDDD8 File Offset: 0x000FBFD8
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public ProgressBar ProgressBar
		{
			get
			{
				return (ProgressBar)base.Control;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.ToolStripProgressBar" /> layout is right-to-left or left-to-right when the <see cref="T:System.Windows.Forms.RightToLeft" /> property is set to <see cref="F:System.Windows.Forms.RightToLeft.Yes" />. </summary>
		/// <returns>true to turn on mirroring and lay out control from right to left when the <see cref="T:System.Windows.Forms.RightToLeft" /> property is set to <see cref="F:System.Windows.Forms.RightToLeft.Yes" />; otherwise, false. The default is false.</returns>
		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x06003F82 RID: 16258 RVA: 0x000FDDE8 File Offset: 0x000FBFE8
		// (set) Token: 0x06003F83 RID: 16259 RVA: 0x000FDDF8 File Offset: 0x000FBFF8
		[DefaultValue(false)]
		[Localizable(true)]
		public virtual bool RightToLeftLayout
		{
			get
			{
				return this.ProgressBar.RightToLeftLayout;
			}
			set
			{
				this.ProgressBar.RightToLeftLayout = value;
			}
		}

		/// <summary>Gets or sets the amount by which to increment the current value of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" /> when the <see cref="M:System.Windows.Forms.ToolStripProgressBar.PerformStep" /> method is called.</summary>
		/// <returns>An integer representing the incremental amount. The default value is 10.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x06003F84 RID: 16260 RVA: 0x000FDE08 File Offset: 0x000FC008
		// (set) Token: 0x06003F85 RID: 16261 RVA: 0x000FDE18 File Offset: 0x000FC018
		[DefaultValue(10)]
		public int Step
		{
			get
			{
				return this.ProgressBar.Step;
			}
			set
			{
				this.ProgressBar.Step = value;
			}
		}

		/// <summary>Gets or sets the style of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ProgressBarStyle" /> values. The default value is <see cref="F:System.Windows.Forms.ProgressBarStyle.Blocks" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x06003F86 RID: 16262 RVA: 0x000FDE28 File Offset: 0x000FC028
		// (set) Token: 0x06003F87 RID: 16263 RVA: 0x000FDE38 File Offset: 0x000FC038
		[DefaultValue(ProgressBarStyle.Blocks)]
		public ProgressBarStyle Style
		{
			get
			{
				return this.ProgressBar.Style;
			}
			set
			{
				this.ProgressBar.Style = value;
			}
		}

		/// <summary>Gets or sets the text displayed on the <see cref="T:System.Windows.Forms.ToolStripProgressBar" />.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the display text.</returns>
		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x06003F88 RID: 16264 RVA: 0x000FDE48 File Offset: 0x000FC048
		// (set) Token: 0x06003F89 RID: 16265 RVA: 0x000FDE50 File Offset: 0x000FC050
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
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

		/// <summary>Gets or sets the current value of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" />.</summary>
		/// <returns>An integer representing the current value.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x06003F8A RID: 16266 RVA: 0x000FDE5C File Offset: 0x000FC05C
		// (set) Token: 0x06003F8B RID: 16267 RVA: 0x000FDE6C File Offset: 0x000FC06C
		[DefaultValue(0)]
		[Bindable(true)]
		public int Value
		{
			get
			{
				return this.ProgressBar.Value;
			}
			set
			{
				this.ProgressBar.Value = value;
			}
		}

		/// <summary>Gets the spacing between the <see cref="T:System.Windows.Forms.ToolStripProgressBar" /> and adjacent items.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> value representing the spacing.</returns>
		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x06003F8C RID: 16268 RVA: 0x000FDE7C File Offset: 0x000FC07C
		protected internal override Padding DefaultMargin
		{
			get
			{
				return new Padding(1, 2, 1, 1);
			}
		}

		/// <summary>Gets the height and width of the <see cref="T:System.Windows.Forms.ToolStripProgressBar" /> in pixels.</summary>
		/// <returns>A <see cref="M:System.Drawing.Point.#ctor(System.Drawing.Size)" /> value representing the height and width.</returns>
		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x06003F8D RID: 16269 RVA: 0x000FDE88 File Offset: 0x000FC088
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 15);
			}
		}

		/// <summary>Advances the current position of the progress bar by the specified amount.</summary>
		/// <param name="value">The amount by which to increment the progress bar's current position.</param>
		// Token: 0x06003F8E RID: 16270 RVA: 0x000FDE94 File Offset: 0x000FC094
		public void Increment(int value)
		{
			this.ProgressBar.Increment(value);
		}

		/// <summary>Advances the current position of the progress bar by the amount of the <see cref="P:System.Windows.Forms.ToolStripProgressBar.Step" /> property.</summary>
		// Token: 0x06003F8F RID: 16271 RVA: 0x000FDEA4 File Offset: 0x000FC0A4
		public void PerformStep()
		{
			this.ProgressBar.PerformStep();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.ProgressBar.RightToLeftLayoutChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003F90 RID: 16272 RVA: 0x000FDEB4 File Offset: 0x000FC0B4
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
		}

		/// <param name="control">The control from which to subscribe events.</param>
		// Token: 0x06003F91 RID: 16273 RVA: 0x000FDEB8 File Offset: 0x000FC0B8
		protected override void OnSubscribeControlEvents(Control control)
		{
			base.OnSubscribeControlEvents(control);
		}

		/// <param name="control">The control from which to unsubscribe events.</param>
		// Token: 0x06003F92 RID: 16274 RVA: 0x000FDEC4 File Offset: 0x000FC0C4
		protected override void OnUnsubscribeControlEvents(Control control)
		{
			base.OnUnsubscribeControlEvents(control);
		}
	}
}
