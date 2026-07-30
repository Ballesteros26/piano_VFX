using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Timers;

namespace System.Windows.Forms
{
	/// <summary>Represents a standard Windows track bar.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000388 RID: 904
	[ClassInterface(1)]
	[Designer("System.Windows.Forms.Design.TrackBarDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultEvent("Scroll")]
	[DefaultProperty("Value")]
	[DefaultBindingProperty("Value")]
	[ComVisible(true)]
	public class TrackBar : Control, ISupportInitialize
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.TrackBar" /> class.</summary>
		// Token: 0x06004160 RID: 16736 RVA: 0x001029D0 File Offset: 0x00100BD0
		public TrackBar()
		{
			this.orientation = Orientation.Horizontal;
			this.minimum = 0;
			this.maximum = 10;
			this.tickFrequency = 1;
			this.autosize = true;
			this.position = 0;
			this.tickStyle = TickStyle.BottomRight;
			this.smallChange = 1;
			this.largeChange = 5;
			this.mouse_clickmove = false;
			base.MouseDown += this.OnMouseDownTB;
			base.MouseUp += this.OnMouseUpTB;
			base.MouseMove += this.OnMouseMoveTB;
			base.MouseLeave += new EventHandler(this.OnMouseLeave);
			base.KeyDown += this.OnKeyDownTB;
			base.LostFocus += new EventHandler(this.OnLostFocusTB);
			base.GotFocus += new EventHandler(this.OnGotFocusTB);
			this.holdclick_timer.Elapsed += new ElapsedEventHandler(this.OnFirstClickTimer);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.UseTextForAccessibility, false);
		}

		// Token: 0x06004161 RID: 16737 RVA: 0x00102AF4 File Offset: 0x00100CF4
		// Note: this type is marked as 'beforefieldinit'.
		static TrackBar()
		{
			TrackBar.RightToLeftLayoutChangedEvent = new object();
			TrackBar.ScrollEvent = new object();
			TrackBar.ValueChangedEvent = new object();
			TrackBar.UIAValueParamChangedEvent = new object();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TrackBar.AutoSize" /> property changes.</summary>
		// Token: 0x14000402 RID: 1026
		// (add) Token: 0x06004162 RID: 16738 RVA: 0x00102B2C File Offset: 0x00100D2C
		// (remove) Token: 0x06004163 RID: 16739 RVA: 0x00102B38 File Offset: 0x00100D38
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.BackgroundImage" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000403 RID: 1027
		// (add) Token: 0x06004164 RID: 16740 RVA: 0x00102B44 File Offset: 0x00100D44
		// (remove) Token: 0x06004165 RID: 16741 RVA: 0x00102B50 File Offset: 0x00100D50
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000404 RID: 1028
		// (add) Token: 0x06004166 RID: 16742 RVA: 0x00102B5C File Offset: 0x00100D5C
		// (remove) Token: 0x06004167 RID: 16743 RVA: 0x00102B68 File Offset: 0x00100D68
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

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000405 RID: 1029
		// (add) Token: 0x06004168 RID: 16744 RVA: 0x00102B74 File Offset: 0x00100D74
		// (remove) Token: 0x06004169 RID: 16745 RVA: 0x00102B80 File Offset: 0x00100D80
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000406 RID: 1030
		// (add) Token: 0x0600416A RID: 16746 RVA: 0x00102B8C File Offset: 0x00100D8C
		// (remove) Token: 0x0600416B RID: 16747 RVA: 0x00102B98 File Offset: 0x00100D98
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.Font" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000407 RID: 1031
		// (add) Token: 0x0600416C RID: 16748 RVA: 0x00102BA4 File Offset: 0x00100DA4
		// (remove) Token: 0x0600416D RID: 16749 RVA: 0x00102BB0 File Offset: 0x00100DB0
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.ForeColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000408 RID: 1032
		// (add) Token: 0x0600416E RID: 16750 RVA: 0x00102BBC File Offset: 0x00100DBC
		// (remove) Token: 0x0600416F RID: 16751 RVA: 0x00102BC8 File Offset: 0x00100DC8
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.ImeMode" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000409 RID: 1033
		// (add) Token: 0x06004170 RID: 16752 RVA: 0x00102BD4 File Offset: 0x00100DD4
		// (remove) Token: 0x06004171 RID: 16753 RVA: 0x00102BE0 File Offset: 0x00100DE0
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

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400040A RID: 1034
		// (add) Token: 0x06004172 RID: 16754 RVA: 0x00102BEC File Offset: 0x00100DEC
		// (remove) Token: 0x06004173 RID: 16755 RVA: 0x00102BF8 File Offset: 0x00100DF8
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400040B RID: 1035
		// (add) Token: 0x06004174 RID: 16756 RVA: 0x00102C04 File Offset: 0x00100E04
		// (remove) Token: 0x06004175 RID: 16757 RVA: 0x00102C10 File Offset: 0x00100E10
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TrackBar.Padding" /> property changes.</summary>
		// Token: 0x1400040C RID: 1036
		// (add) Token: 0x06004176 RID: 16758 RVA: 0x00102C1C File Offset: 0x00100E1C
		// (remove) Token: 0x06004177 RID: 16759 RVA: 0x00102C28 File Offset: 0x00100E28
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the <see cref="T:System.Windows.Forms.TrackBar" /> control is drawn.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400040D RID: 1037
		// (add) Token: 0x06004178 RID: 16760 RVA: 0x00102C34 File Offset: 0x00100E34
		// (remove) Token: 0x06004179 RID: 16761 RVA: 0x00102C40 File Offset: 0x00100E40
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.TrackBar.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x1400040E RID: 1038
		// (add) Token: 0x0600417A RID: 16762 RVA: 0x00102C4C File Offset: 0x00100E4C
		// (remove) Token: 0x0600417B RID: 16763 RVA: 0x00102C60 File Offset: 0x00100E60
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.Events.AddHandler(TrackBar.RightToLeftLayoutChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TrackBar.RightToLeftLayoutChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.Text" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400040F RID: 1039
		// (add) Token: 0x0600417C RID: 16764 RVA: 0x00102C74 File Offset: 0x00100E74
		// (remove) Token: 0x0600417D RID: 16765 RVA: 0x00102C80 File Offset: 0x00100E80
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when either a mouse or keyboard action moves the scroll box.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000410 RID: 1040
		// (add) Token: 0x0600417E RID: 16766 RVA: 0x00102C8C File Offset: 0x00100E8C
		// (remove) Token: 0x0600417F RID: 16767 RVA: 0x00102CA0 File Offset: 0x00100EA0
		public event EventHandler Scroll
		{
			add
			{
				base.Events.AddHandler(TrackBar.ScrollEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TrackBar.ScrollEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.TrackBar.Value" /> property of a track bar changes, either by movement of the scroll box or by manipulation in code.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000411 RID: 1041
		// (add) Token: 0x06004180 RID: 16768 RVA: 0x00102CB4 File Offset: 0x00100EB4
		// (remove) Token: 0x06004181 RID: 16769 RVA: 0x00102CC8 File Offset: 0x00100EC8
		public event EventHandler ValueChanged
		{
			add
			{
				base.Events.AddHandler(TrackBar.ValueChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TrackBar.ValueChangedEvent, value);
			}
		}

		// Token: 0x14000412 RID: 1042
		// (add) Token: 0x06004182 RID: 16770 RVA: 0x00102CDC File Offset: 0x00100EDC
		// (remove) Token: 0x06004183 RID: 16771 RVA: 0x00102CF0 File Offset: 0x00100EF0
		internal event EventHandler UIAValueParamChanged
		{
			add
			{
				base.Events.AddHandler(TrackBar.UIAValueParamChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(TrackBar.UIAValueParamChangedEvent, value);
			}
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x00102D04 File Offset: 0x00100F04
		internal void OnUIAValueParamChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[TrackBar.UIAValueParamChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x06004185 RID: 16773 RVA: 0x00102D3C File Offset: 0x00100F3C
		// (set) Token: 0x06004186 RID: 16774 RVA: 0x00102D44 File Offset: 0x00100F44
		internal Rectangle ThumbPos
		{
			get
			{
				return this.thumb_pos;
			}
			set
			{
				this.thumb_pos = value;
			}
		}

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x06004187 RID: 16775 RVA: 0x00102D50 File Offset: 0x00100F50
		// (set) Token: 0x06004188 RID: 16776 RVA: 0x00102D58 File Offset: 0x00100F58
		internal Rectangle ThumbArea
		{
			get
			{
				return this.thumb_area;
			}
			set
			{
				this.thumb_area = value;
			}
		}

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x06004189 RID: 16777 RVA: 0x00102D64 File Offset: 0x00100F64
		// (set) Token: 0x0600418A RID: 16778 RVA: 0x00102D6C File Offset: 0x00100F6C
		internal bool ThumbEntered
		{
			get
			{
				return this.thumb_entered;
			}
			set
			{
				if (this.thumb_entered == value)
				{
					return;
				}
				this.thumb_entered = value;
				if (ThemeEngine.Current.TrackBarHasHotThumbStyle)
				{
					base.Invalidate(this.GetRealThumbRectangle());
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the height or width of the track bar is being automatically sized.</summary>
		/// <returns>true if the track bar is being automatically sized; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x0600418B RID: 16779 RVA: 0x00102DA0 File Offset: 0x00100FA0
		// (set) Token: 0x0600418C RID: 16780 RVA: 0x00102DA8 File Offset: 0x00100FA8
		[DesignerSerializationVisibility(1)]
		[DefaultValue(true)]
		[Browsable(true)]
		[EditorBrowsable(0)]
		public override bool AutoSize
		{
			get
			{
				return this.autosize;
			}
			set
			{
				this.autosize = value;
			}
		}

		/// <summary>Gets or sets the background image for the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> that is the background image for the <see cref="T:System.Windows.Forms.TrackBar" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x0600418D RID: 16781 RVA: 0x00102DB4 File Offset: 0x00100FB4
		// (set) Token: 0x0600418E RID: 16782 RVA: 0x00102DBC File Offset: 0x00100FBC
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

		/// <summary>Gets or sets an <see cref="T:System.Windows.Forms.ImageLayout" /> value; however, setting this property has no effect on the <see cref="T:System.Windows.Forms.TrackBar" /> control. </summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x0600418F RID: 16783 RVA: 0x00102DC8 File Offset: 0x00100FC8
		// (set) Token: 0x06004190 RID: 16784 RVA: 0x00102DD0 File Offset: 0x00100FD0
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

		/// <summary>Overrides the <see cref="P:System.Windows.Forms.Control.CreateParams" /> property.</summary>
		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x06004191 RID: 16785 RVA: 0x00102DDC File Offset: 0x00100FDC
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <summary>Gets a value indicating the mode for the Input Method Editor (IME) for the <see cref="T:System.Windows.Forms.TrackBar" />.</summary>
		/// <returns>Always <see cref="F:System.Windows.Forms.ImeMode.Disable" />.</returns>
		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x06004192 RID: 16786 RVA: 0x00102DE4 File Offset: 0x00100FE4
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		/// <summary>Gets the default size of the <see cref="T:System.Windows.Forms.TrackBar" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> representing the default size of the control. </returns>
		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x06004193 RID: 16787 RVA: 0x00102DE8 File Offset: 0x00100FE8
		protected override Size DefaultSize
		{
			get
			{
				return ThemeEngine.Current.TrackBarDefaultSize;
			}
		}

		/// <summary>Gets or sets a value indicating whether this control should redraw its surface using a secondary buffer to reduce or prevent flicker; however, this property has no effect on the <see cref="T:System.Windows.Forms.TrackBar" /> control </summary>
		/// <returns>true if the control has a secondary buffer; otherwise, false.</returns>
		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x06004194 RID: 16788 RVA: 0x00102DF4 File Offset: 0x00100FF4
		// (set) Token: 0x06004195 RID: 16789 RVA: 0x00102DFC File Offset: 0x00100FFC
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

		/// <summary>Overrides <see cref="P:System.Windows.Forms.Control.Font" /></summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x06004196 RID: 16790 RVA: 0x00102E08 File Offset: 0x00101008
		// (set) Token: 0x06004197 RID: 16791 RVA: 0x00102E10 File Offset: 0x00101010
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Gets the foreground color of the track bar.</summary>
		/// <returns>Always <see cref="P:System.Drawing.SystemColors.WindowText" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x06004198 RID: 16792 RVA: 0x00102E1C File Offset: 0x0010101C
		// (set) Token: 0x06004199 RID: 16793 RVA: 0x00102E24 File Offset: 0x00101024
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

		/// <summary>Gets or sets the Input Method Editor (IME) mode supported by this control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImeMode" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x0600419A RID: 16794 RVA: 0x00102E30 File Offset: 0x00101030
		// (set) Token: 0x0600419B RID: 16795 RVA: 0x00102E38 File Offset: 0x00101038
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

		/// <summary>Gets or sets a value to be added to or subtracted from the <see cref="P:System.Windows.Forms.TrackBar.Value" /> property when the scroll box is moved a large distance.</summary>
		/// <returns>A numeric value. The default is 5.</returns>
		/// <exception cref="T:System.ArgumentException">The assigned value is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x0600419C RID: 16796 RVA: 0x00102E44 File Offset: 0x00101044
		// (set) Token: 0x0600419D RID: 16797 RVA: 0x00102E4C File Offset: 0x0010104C
		[DefaultValue(5)]
		public int LargeChange
		{
			get
			{
				return this.largeChange;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(string.Format("Value '{0}' must be greater than or equal to 0.", value));
				}
				this.largeChange = value;
				this.OnUIAValueParamChanged();
			}
		}

		/// <summary>Gets or sets the upper limit of the range this <see cref="T:System.Windows.Forms.TrackBar" /> is working with.</summary>
		/// <returns>The maximum value for the <see cref="T:System.Windows.Forms.TrackBar" />. The default is 10.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x0600419E RID: 16798 RVA: 0x00102E84 File Offset: 0x00101084
		// (set) Token: 0x0600419F RID: 16799 RVA: 0x00102E8C File Offset: 0x0010108C
		[DefaultValue(10)]
		[RefreshProperties(1)]
		public int Maximum
		{
			get
			{
				return this.maximum;
			}
			set
			{
				if (this.maximum != value)
				{
					this.maximum = value;
					if (this.maximum < this.minimum)
					{
						this.minimum = this.maximum;
					}
					this.Refresh();
					this.OnUIAValueParamChanged();
				}
			}
		}

		/// <summary>Gets or sets the lower limit of the range this <see cref="T:System.Windows.Forms.TrackBar" /> is working with.</summary>
		/// <returns>The minimum value for the <see cref="T:System.Windows.Forms.TrackBar" />. The default is 0.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x060041A0 RID: 16800 RVA: 0x00102ED8 File Offset: 0x001010D8
		// (set) Token: 0x060041A1 RID: 16801 RVA: 0x00102EE0 File Offset: 0x001010E0
		[DefaultValue(0)]
		[RefreshProperties(1)]
		public int Minimum
		{
			get
			{
				return this.minimum;
			}
			set
			{
				if (this.Minimum != value)
				{
					this.minimum = value;
					if (this.minimum > this.maximum)
					{
						this.maximum = this.minimum;
					}
					this.Refresh();
					this.OnUIAValueParamChanged();
				}
			}
		}

		/// <summary>Gets or sets a value indicating the horizontal or vertical orientation of the track bar.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Orientation" /> values.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.Orientation" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x060041A2 RID: 16802 RVA: 0x00102F2C File Offset: 0x0010112C
		// (set) Token: 0x060041A3 RID: 16803 RVA: 0x00102F34 File Offset: 0x00101134
		[Localizable(true)]
		[DefaultValue(Orientation.Horizontal)]
		public Orientation Orientation
		{
			get
			{
				return this.orientation;
			}
			set
			{
				if (!Enum.IsDefined(typeof(Orientation), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for Orientation", value));
				}
				if (this.orientation != value)
				{
					this.orientation = value;
					if (base.IsHandleCreated)
					{
						base.Size = new Size(base.Height, base.Width);
						this.Refresh();
					}
				}
			}
		}

		/// <summary>Gets or sets the space between the edges of a <see cref="T:System.Windows.Forms.TrackBar" /> control and its contents.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> object.</returns>
		// Token: 0x17001110 RID: 4368
		// (get) Token: 0x060041A4 RID: 16804 RVA: 0x00102FAC File Offset: 0x001011AC
		// (set) Token: 0x060041A5 RID: 16805 RVA: 0x00102FB4 File Offset: 0x001011B4
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
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

		/// <summary>Gets or sets a value indicating whether the contents of the <see cref="T:System.Windows.Forms.TrackBar" /> will be laid out from right to left.</summary>
		/// <returns>true if the contents of the <see cref="T:System.Windows.Forms.TrackBar" /> are laid out from right to left; otherwise, false. The default is false.</returns>
		// Token: 0x17001111 RID: 4369
		// (get) Token: 0x060041A6 RID: 16806 RVA: 0x00102FC0 File Offset: 0x001011C0
		// (set) Token: 0x060041A7 RID: 16807 RVA: 0x00102FC8 File Offset: 0x001011C8
		[DefaultValue(false)]
		[Localizable(true)]
		public virtual bool RightToLeftLayout
		{
			get
			{
				return this.right_to_left_layout;
			}
			set
			{
				if (value != this.right_to_left_layout)
				{
					this.right_to_left_layout = value;
					this.OnRightToLeftLayoutChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the value added to or subtracted from the <see cref="P:System.Windows.Forms.TrackBar.Value" /> property when the scroll box is moved a small distance.</summary>
		/// <returns>A numeric value. The default value is 1.</returns>
		/// <exception cref="T:System.ArgumentException">The assigned value is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001112 RID: 4370
		// (get) Token: 0x060041A8 RID: 16808 RVA: 0x00102FE8 File Offset: 0x001011E8
		// (set) Token: 0x060041A9 RID: 16809 RVA: 0x00102FF0 File Offset: 0x001011F0
		[DefaultValue(1)]
		public int SmallChange
		{
			get
			{
				return this.smallChange;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException(string.Format("Value '{0}' must be greater than or equal to 0.", value));
				}
				if (this.smallChange != value)
				{
					this.smallChange = value;
					this.OnUIAValueParamChanged();
				}
			}
		}

		/// <summary>Gets or sets the text of the <see cref="T:System.Windows.Forms.TrackBar" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17001113 RID: 4371
		// (get) Token: 0x060041AA RID: 16810 RVA: 0x00103034 File Offset: 0x00101234
		// (set) Token: 0x060041AB RID: 16811 RVA: 0x0010303C File Offset: 0x0010123C
		[Browsable(false)]
		[Bindable(false)]
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

		/// <summary>Gets or sets a value that specifies the delta between ticks drawn on the control.</summary>
		/// <returns>The numeric value representing the delta between ticks. The default is 1.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001114 RID: 4372
		// (get) Token: 0x060041AC RID: 16812 RVA: 0x00103048 File Offset: 0x00101248
		// (set) Token: 0x060041AD RID: 16813 RVA: 0x00103050 File Offset: 0x00101250
		[DefaultValue(1)]
		public int TickFrequency
		{
			get
			{
				return this.tickFrequency;
			}
			set
			{
				if (value > 0)
				{
					this.tickFrequency = value;
					this.Refresh();
				}
			}
		}

		/// <summary>Gets or sets a value indicating how to display the tick marks on the track bar.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.TickStyle" /> values. The default is <see cref="F:System.Windows.Forms.TickStyle.BottomRight" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not a valid <see cref="T:System.Windows.Forms.TickStyle" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001115 RID: 4373
		// (get) Token: 0x060041AE RID: 16814 RVA: 0x00103068 File Offset: 0x00101268
		// (set) Token: 0x060041AF RID: 16815 RVA: 0x00103070 File Offset: 0x00101270
		[DefaultValue(TickStyle.BottomRight)]
		public TickStyle TickStyle
		{
			get
			{
				return this.tickStyle;
			}
			set
			{
				if (!Enum.IsDefined(typeof(TickStyle), value))
				{
					throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for TickStyle", value));
				}
				if (this.tickStyle != value)
				{
					this.tickStyle = value;
					this.Refresh();
				}
			}
		}

		/// <summary>Gets or sets a numeric value that represents the current position of the scroll box on the track bar.</summary>
		/// <returns>A numeric value that is within the <see cref="P:System.Windows.Forms.TrackBar.Minimum" /> and <see cref="P:System.Windows.Forms.TrackBar.Maximum" /> range. The default value is 0.</returns>
		/// <exception cref="T:System.ArgumentException">The assigned value is less than the value of <see cref="P:System.Windows.Forms.TrackBar.Minimum" />.-or- The assigned value is greater than the value of <see cref="P:System.Windows.Forms.TrackBar.Maximum" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17001116 RID: 4374
		// (get) Token: 0x060041B0 RID: 16816 RVA: 0x001030C8 File Offset: 0x001012C8
		// (set) Token: 0x060041B1 RID: 16817 RVA: 0x001030D0 File Offset: 0x001012D0
		[Bindable(true)]
		[DefaultValue(0)]
		public int Value
		{
			get
			{
				return this.position;
			}
			set
			{
				this.SetValue(value, false);
			}
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x001030DC File Offset: 0x001012DC
		private void SetValue(int value, bool fire_onscroll)
		{
			if (value < this.Minimum || value > this.Maximum)
			{
				throw new ArgumentException(string.Format("'{0}' is not a valid value for 'Value'. 'Value' should be between 'Minimum' and 'Maximum'", value));
			}
			if (this.position == value)
			{
				return;
			}
			this.position = value;
			if (fire_onscroll)
			{
				this.OnScroll(EventArgs.Empty);
			}
			EventHandler eventHandler = (EventHandler)base.Events[TrackBar.ValueChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
			base.Invalidate(this.thumb_area);
		}

		/// <summary>Begins the initialization of a <see cref="T:System.Windows.Forms.TrackBar" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060041B3 RID: 16819 RVA: 0x00103170 File Offset: 0x00101370
		public void BeginInit()
		{
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.CreateHandle" /> method.</summary>
		// Token: 0x060041B4 RID: 16820 RVA: 0x00103174 File Offset: 0x00101374
		protected override void CreateHandle()
		{
			base.CreateHandle();
		}

		/// <summary>Overrides Control.setBoundsCore to enforce autoSize.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control. </param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Top" /> property value of the control. </param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control. </param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control. </param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values. </param>
		// Token: 0x060041B5 RID: 16821 RVA: 0x0010317C File Offset: 0x0010137C
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (this.AutoSize)
			{
				if (this.orientation == Orientation.Vertical)
				{
					width = 45;
				}
				else
				{
					height = 45;
				}
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <summary>Ends the initialization of a <see cref="T:System.Windows.Forms.TrackBar" /> that is used on a form or used by another component. The initialization occurs at run time.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060041B6 RID: 16822 RVA: 0x001031BC File Offset: 0x001013BC
		public void EndInit()
		{
		}

		/// <summary>Handles special input keys, such as PAGE UP, PAGE DOWN, HOME, and END.</summary>
		/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values. </param>
		// Token: 0x060041B7 RID: 16823 RVA: 0x001031C0 File Offset: 0x001013C0
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) == Keys.None)
			{
				switch (keyData & Keys.KeyCode)
				{
				case Keys.PageUp:
				case Keys.PageDown:
				case Keys.End:
				case Keys.Home:
				case Keys.Left:
				case Keys.Up:
				case Keys.Right:
				case Keys.Down:
					return true;
				}
			}
			return base.IsInputKey(keyData);
		}

		/// <summary>This method is called by the control when any property changes. Inheriting controls can override this method to get property change notification on basic properties. Inheriting controls must call base.propertyChanged.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060041B8 RID: 16824 RVA: 0x00103218 File Offset: 0x00101418
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
		}

		/// <summary>Use the <see cref="M:System.Windows.Forms.Control.OnHandleCreated(System.EventArgs)" /> method.</summary>
		// Token: 0x060041B9 RID: 16825 RVA: 0x00103224 File Offset: 0x00101424
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			if (this.AutoSize)
			{
				if (this.Orientation == Orientation.Horizontal)
				{
					base.Size = new Size(base.Width, 40);
				}
				else
				{
					base.Size = new Size(50, base.Height);
				}
			}
			this.UpdatePos(this.Value, true);
		}

		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
		// Token: 0x060041BA RID: 16826 RVA: 0x00103288 File Offset: 0x00101488
		[EditorBrowsable(2)]
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			base.OnMouseWheel(e);
			if (!base.Enabled)
			{
				return;
			}
			if (e.Delta > 0)
			{
				this.SmallDecrement();
			}
			else
			{
				this.SmallIncrement();
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TrackBar.RightToLeftLayoutChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" />  that contains the event data. </param>
		// Token: 0x060041BB RID: 16827 RVA: 0x001032C8 File Offset: 0x001014C8
		[EditorBrowsable(2)]
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TrackBar.RightToLeftLayoutChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TrackBar.Scroll" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060041BC RID: 16828 RVA: 0x001032FC File Offset: 0x001014FC
		protected virtual void OnScroll(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TrackBar.ScrollEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.SystemColorsChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060041BD RID: 16829 RVA: 0x00103330 File Offset: 0x00101530
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.TrackBar.ValueChanged" /> event.</summary>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060041BE RID: 16830 RVA: 0x00103340 File Offset: 0x00101540
		protected virtual void OnValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[TrackBar.ValueChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Sets the minimum and maximum values for a <see cref="T:System.Windows.Forms.TrackBar" />.</summary>
		/// <param name="minValue">The lower limit of the range of the track bar. </param>
		/// <param name="maxValue">The upper limit of the range of the track bar. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060041BF RID: 16831 RVA: 0x00103374 File Offset: 0x00101574
		public void SetRange(int minValue, int maxValue)
		{
			this.Minimum = minValue;
			this.Maximum = maxValue;
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.TrackBar" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.TrackBar" />. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060041C0 RID: 16832 RVA: 0x00103384 File Offset: 0x00101584
		public override string ToString()
		{
			return string.Format("System.Windows.Forms.TrackBar, Minimum: {0}, Maximum: {1}, Value: {2}", this.Minimum, this.Maximum, this.Value);
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" /> method.</summary>
		/// <param name="m">A Windows Message object. </param>
		// Token: 0x060041C1 RID: 16833 RVA: 0x001033BC File Offset: 0x001015BC
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
			if (m.Msg == 71 && base.Visible)
			{
				base.Invalidate();
			}
		}

		// Token: 0x060041C2 RID: 16834 RVA: 0x001033E4 File Offset: 0x001015E4
		private void UpdatePos(int newPos, bool update_trumbpos)
		{
			if (newPos < this.minimum)
			{
				this.SetValue(this.minimum, true);
			}
			else if (newPos > this.maximum)
			{
				this.SetValue(this.maximum, true);
			}
			else
			{
				this.SetValue(newPos, true);
			}
		}

		// Token: 0x060041C3 RID: 16835 RVA: 0x00103438 File Offset: 0x00101638
		internal void LargeIncrement()
		{
			this.UpdatePos(this.position + this.LargeChange, true);
			base.Invalidate(this.thumb_area);
		}

		// Token: 0x060041C4 RID: 16836 RVA: 0x00103468 File Offset: 0x00101668
		internal void LargeDecrement()
		{
			this.UpdatePos(this.position - this.LargeChange, true);
			base.Invalidate(this.thumb_area);
		}

		// Token: 0x060041C5 RID: 16837 RVA: 0x00103498 File Offset: 0x00101698
		private void SmallIncrement()
		{
			this.UpdatePos(this.position + this.SmallChange, true);
			base.Invalidate(this.thumb_area);
		}

		// Token: 0x060041C6 RID: 16838 RVA: 0x001034C8 File Offset: 0x001016C8
		private void SmallDecrement()
		{
			this.UpdatePos(this.position - this.SmallChange, true);
			base.Invalidate(this.thumb_area);
		}

		// Token: 0x060041C7 RID: 16839 RVA: 0x001034F8 File Offset: 0x001016F8
		private void OnMouseUpTB(object sender, MouseEventArgs e)
		{
			if (!base.Enabled)
			{
				return;
			}
			if (this.thumb_pressed || this.mouse_clickmove)
			{
				this.thumb_pressed = false;
				this.holdclick_timer.Enabled = false;
				base.Capture = false;
				base.Invalidate(this.thumb_area);
			}
		}

		// Token: 0x060041C8 RID: 16840 RVA: 0x00103550 File Offset: 0x00101750
		private void OnMouseDownTB(object sender, MouseEventArgs e)
		{
			if (!base.Enabled)
			{
				return;
			}
			this.mouse_moved = false;
			bool flag = false;
			Point point;
			point..ctor(e.X, e.Y);
			if (this.orientation == Orientation.Horizontal)
			{
				if (this.thumb_pos.Contains(point))
				{
					base.Capture = true;
					this.thumb_pressed = true;
					this.thumb_mouseclick = e.X;
					this.mouse_down_x_offset = e.X - this.thumb_pos.X;
					base.Invalidate(this.thumb_area);
				}
				else if (this.thumb_area.Contains(point))
				{
					this.is_moving_right = e.X > this.thumb_pos.X + this.thumb_pos.Width;
					if (this.is_moving_right)
					{
						this.LargeIncrement();
					}
					else
					{
						this.LargeDecrement();
					}
					base.Invalidate(this.thumb_area);
					flag = true;
					this.mouse_clickmove = true;
				}
			}
			else
			{
				Rectangle rectangle = this.thumb_pos;
				rectangle.Width = this.thumb_pos.Height;
				rectangle.Height = this.thumb_pos.Width;
				if (rectangle.Contains(point))
				{
					base.Capture = true;
					this.thumb_pressed = true;
					this.thumb_mouseclick = e.Y;
					this.mouse_down_x_offset = e.Y - this.thumb_pos.Y;
					base.Invalidate(this.thumb_area);
				}
				else if (this.thumb_area.Contains(point))
				{
					this.is_moving_right = e.Y > this.thumb_pos.Y + this.thumb_pos.Width;
					if (this.is_moving_right)
					{
						this.LargeDecrement();
					}
					else
					{
						this.LargeIncrement();
					}
					base.Invalidate(this.thumb_area);
					flag = true;
					this.mouse_clickmove = true;
				}
			}
			if (flag)
			{
				this.holdclick_timer.Interval = 300.0;
				this.holdclick_timer.Enabled = true;
			}
		}

		// Token: 0x060041C9 RID: 16841 RVA: 0x00103758 File Offset: 0x00101958
		private void OnMouseMoveTB(object sender, MouseEventArgs e)
		{
			if (!base.Enabled)
			{
				return;
			}
			this.mouse_moved = true;
			if (this.thumb_pressed)
			{
				this.SetValue(ThemeEngine.Current.TrackBarValueFromMousePosition(e.X, e.Y, this), true);
			}
			this.ThumbEntered = this.GetRealThumbRectangle().Contains(e.Location);
		}

		// Token: 0x060041CA RID: 16842 RVA: 0x001037BC File Offset: 0x001019BC
		private Rectangle GetRealThumbRectangle()
		{
			Rectangle rectangle = this.thumb_pos;
			if (this.Orientation == Orientation.Vertical)
			{
				rectangle.Width = this.thumb_pos.Height;
				rectangle.Height = this.thumb_pos.Width;
			}
			return rectangle;
		}

		// Token: 0x060041CB RID: 16843 RVA: 0x00103804 File Offset: 0x00101A04
		internal override void OnPaintInternal(PaintEventArgs pevent)
		{
			ThemeEngine.Current.DrawTrackBar(pevent.Graphics, pevent.ClipRectangle, this);
		}

		// Token: 0x060041CC RID: 16844 RVA: 0x00103828 File Offset: 0x00101A28
		private void OnLostFocusTB(object sender, EventArgs e)
		{
			base.Invalidate();
		}

		// Token: 0x060041CD RID: 16845 RVA: 0x00103830 File Offset: 0x00101A30
		private void OnGotFocusTB(object sender, EventArgs e)
		{
			base.Invalidate();
		}

		// Token: 0x060041CE RID: 16846 RVA: 0x00103838 File Offset: 0x00101A38
		private void OnKeyDownTB(object sender, KeyEventArgs e)
		{
			bool flag = this.Orientation == Orientation.Horizontal;
			switch (e.KeyCode)
			{
			case Keys.PageUp:
				if (flag)
				{
					this.LargeDecrement();
				}
				else
				{
					this.LargeIncrement();
				}
				break;
			case Keys.PageDown:
				if (flag)
				{
					this.LargeIncrement();
				}
				else
				{
					this.LargeDecrement();
				}
				break;
			case Keys.End:
				if (flag)
				{
					this.SetValue(this.Maximum, true);
				}
				else
				{
					this.SetValue(this.Minimum, true);
				}
				break;
			case Keys.Home:
				if (flag)
				{
					this.SetValue(this.Minimum, true);
				}
				else
				{
					this.SetValue(this.Maximum, true);
				}
				break;
			case Keys.Left:
			case Keys.Up:
				if (flag)
				{
					this.SmallDecrement();
				}
				else
				{
					this.SmallIncrement();
				}
				break;
			case Keys.Right:
			case Keys.Down:
				if (flag)
				{
					this.SmallIncrement();
				}
				else
				{
					this.SmallDecrement();
				}
				break;
			}
		}

		// Token: 0x060041CF RID: 16847 RVA: 0x00103950 File Offset: 0x00101B50
		private void OnFirstClickTimer(object source, ElapsedEventArgs e)
		{
			Point point = base.PointToClient(Control.MousePosition);
			if (this.thumb_area.Contains(point))
			{
				bool flag = false;
				if (this.orientation == Orientation.Horizontal)
				{
					if (point.X > this.thumb_pos.X + this.thumb_pos.Width && this.is_moving_right)
					{
						this.LargeIncrement();
						flag = true;
					}
					else if (point.X < this.thumb_pos.X && !this.is_moving_right)
					{
						this.LargeDecrement();
						flag = true;
					}
				}
				else if (point.Y > this.thumb_pos.Y + this.thumb_pos.Width && this.is_moving_right)
				{
					this.LargeDecrement();
					flag = true;
				}
				else if (point.Y < this.thumb_pos.Y && !this.is_moving_right)
				{
					this.LargeIncrement();
					flag = true;
				}
				if (flag)
				{
					this.Refresh();
				}
			}
		}

		// Token: 0x060041D0 RID: 16848 RVA: 0x00103A64 File Offset: 0x00101C64
		private void OnMouseLeave(object sender, EventArgs e)
		{
			this.ThumbEntered = false;
		}

		// Token: 0x04001BB5 RID: 7093
		private const int size_of_autosize = 45;

		// Token: 0x04001BB6 RID: 7094
		private int minimum;

		// Token: 0x04001BB7 RID: 7095
		private int maximum;

		// Token: 0x04001BB8 RID: 7096
		internal int tickFrequency;

		// Token: 0x04001BB9 RID: 7097
		private bool autosize;

		// Token: 0x04001BBA RID: 7098
		private int position;

		// Token: 0x04001BBB RID: 7099
		private int smallChange;

		// Token: 0x04001BBC RID: 7100
		private int largeChange;

		// Token: 0x04001BBD RID: 7101
		private Orientation orientation;

		// Token: 0x04001BBE RID: 7102
		private TickStyle tickStyle;

		// Token: 0x04001BBF RID: 7103
		private Rectangle thumb_pos = default(Rectangle);

		// Token: 0x04001BC0 RID: 7104
		private Rectangle thumb_area = default(Rectangle);

		// Token: 0x04001BC1 RID: 7105
		internal bool thumb_pressed;

		// Token: 0x04001BC2 RID: 7106
		private Timer holdclick_timer = new Timer();

		// Token: 0x04001BC3 RID: 7107
		internal int thumb_mouseclick;

		// Token: 0x04001BC4 RID: 7108
		private bool mouse_clickmove;

		// Token: 0x04001BC5 RID: 7109
		private bool is_moving_right;

		// Token: 0x04001BC6 RID: 7110
		internal int mouse_down_x_offset;

		// Token: 0x04001BC7 RID: 7111
		internal bool mouse_moved;

		// Token: 0x04001BC8 RID: 7112
		private bool right_to_left_layout;

		// Token: 0x04001BC9 RID: 7113
		private bool thumb_entered;
	}
}
