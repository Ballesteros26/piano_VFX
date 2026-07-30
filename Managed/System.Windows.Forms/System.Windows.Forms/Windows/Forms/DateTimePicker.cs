using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows control that allows the user to select a date and a time and to display the date and time with a specified format.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000143 RID: 323
	[DefaultBindingProperty("Value")]
	[Designer("System.Windows.Forms.Design.DateTimePickerDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("Value")]
	[DefaultEvent("ValueChanged")]
	[ComVisible(true)]
	[ClassInterface(1)]
	public class DateTimePicker : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DateTimePicker" /> class.</summary>
		// Token: 0x06001655 RID: 5717 RVA: 0x0005233C File Offset: 0x0005053C
		public DateTimePicker()
		{
			this.month_calendar = new MonthCalendar(this);
			this.month_calendar.CalendarDimensions = new Size(1, 1);
			this.month_calendar.MaxSelectionCount = 1;
			this.month_calendar.ForeColor = Control.DefaultForeColor;
			this.month_calendar.BackColor = DateTimePicker.DefaultMonthBackColor;
			this.month_calendar.TitleBackColor = DateTimePicker.DefaultTitleBackColor;
			this.month_calendar.TitleForeColor = DateTimePicker.DefaultTitleForeColor;
			this.month_calendar.TrailingForeColor = DateTimePicker.DefaultTrailingForeColor;
			this.month_calendar.Visible = false;
			this.updown_timer = new Timer();
			this.updown_timer.Interval = 500;
			this.is_checked = true;
			this.custom_format = null;
			this.drop_down_align = LeftRightAlignment.Left;
			this.format = DateTimePickerFormat.Long;
			this.max_date = DateTimePicker.MaxDateTime;
			this.min_date = DateTimePicker.MinDateTime;
			this.show_check_box = false;
			this.show_up_down = false;
			this.date_value = DateTime.Now;
			this.is_drop_down_visible = false;
			this.BackColor = SystemColors.Window;
			this.ForeColor = SystemColors.WindowText;
			this.month_calendar.DateChanged += this.MonthCalendarDateChangedHandler;
			this.month_calendar.DateSelected += this.MonthCalendarDateSelectedHandler;
			this.month_calendar.LostFocus += new EventHandler(this.MonthCalendarLostFocusHandler);
			this.updown_timer.Tick += new EventHandler(this.UpDownTimerTick);
			base.KeyPress += this.KeyPressHandler;
			base.KeyDown += this.KeyDownHandler;
			base.GotFocus += new EventHandler(this.GotFocusHandler);
			base.LostFocus += new EventHandler(this.LostFocusHandler);
			base.MouseDown += this.MouseDownHandler;
			base.MouseUp += this.MouseUpHandler;
			base.MouseEnter += new EventHandler(this.OnMouseEnter);
			base.MouseLeave += new EventHandler(this.OnMouseLeave);
			base.MouseMove += this.OnMouseMove;
			this.Paint += this.PaintHandler;
			base.Resize += new EventHandler(this.ResizeHandler);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick, false);
			base.SetStyle(ControlStyles.FixedHeight, true);
			base.SetStyle(ControlStyles.Selectable, true);
			this.CalculateFormats();
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x000525B0 File Offset: 0x000507B0
		// Note: this type is marked as 'beforefieldinit'.
		static DateTimePicker()
		{
			DateTimePicker.CloseUpEvent = new object();
			DateTimePicker.DropDownEvent = new object();
			DateTimePicker.FormatChangedEvent = new object();
			DateTimePicker.ValueChangedEvent = new object();
			DateTimePicker.RightToLeftLayoutChangedEvent = new object();
			DateTimePicker.UIAMinimumChangedEvent = new object();
			DateTimePicker.UIAMaximumChangedEvent = new object();
			DateTimePicker.UIASelectionChangedEvent = new object();
			DateTimePicker.UIACheckedEvent = new object();
			DateTimePicker.UIAShowCheckBoxChangedEvent = new object();
			DateTimePicker.UIAShowUpDownChangedEvent = new object();
		}

		/// <summary>Occurs when the drop-down calendar is dismissed and disappears.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400016E RID: 366
		// (add) Token: 0x06001657 RID: 5719 RVA: 0x0005268C File Offset: 0x0005088C
		// (remove) Token: 0x06001658 RID: 5720 RVA: 0x000526A0 File Offset: 0x000508A0
		public event EventHandler CloseUp
		{
			add
			{
				base.Events.AddHandler(DateTimePicker.CloseUpEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DateTimePicker.CloseUpEvent, value);
			}
		}

		/// <summary>Occurs when the drop-down calendar is shown.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400016F RID: 367
		// (add) Token: 0x06001659 RID: 5721 RVA: 0x000526B4 File Offset: 0x000508B4
		// (remove) Token: 0x0600165A RID: 5722 RVA: 0x000526C8 File Offset: 0x000508C8
		public event EventHandler DropDown
		{
			add
			{
				base.Events.AddHandler(DateTimePicker.DropDownEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DateTimePicker.DropDownEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DateTimePicker.Format" /> property value has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000170 RID: 368
		// (add) Token: 0x0600165B RID: 5723 RVA: 0x000526DC File Offset: 0x000508DC
		// (remove) Token: 0x0600165C RID: 5724 RVA: 0x000526F0 File Offset: 0x000508F0
		public event EventHandler FormatChanged
		{
			add
			{
				base.Events.AddHandler(DateTimePicker.FormatChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DateTimePicker.FormatChangedEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DateTimePicker.Value" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000171 RID: 369
		// (add) Token: 0x0600165D RID: 5725 RVA: 0x00052704 File Offset: 0x00050904
		// (remove) Token: 0x0600165E RID: 5726 RVA: 0x00052718 File Offset: 0x00050918
		public event EventHandler ValueChanged
		{
			add
			{
				base.Events.AddHandler(DateTimePicker.ValueChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DateTimePicker.ValueChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DateTimePicker.BackColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000172 RID: 370
		// (add) Token: 0x0600165F RID: 5727 RVA: 0x0005272C File Offset: 0x0005092C
		// (remove) Token: 0x06001660 RID: 5728 RVA: 0x00052738 File Offset: 0x00050938
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DateTimePicker.BackgroundImage" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000173 RID: 371
		// (add) Token: 0x06001661 RID: 5729 RVA: 0x00052744 File Offset: 0x00050944
		// (remove) Token: 0x06001662 RID: 5730 RVA: 0x00052750 File Offset: 0x00050950
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DateTimePicker.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000174 RID: 372
		// (add) Token: 0x06001663 RID: 5731 RVA: 0x0005275C File Offset: 0x0005095C
		// (remove) Token: 0x06001664 RID: 5732 RVA: 0x00052768 File Offset: 0x00050968
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

		/// <summary>Occurs when the control is clicked.</summary>
		// Token: 0x14000175 RID: 373
		// (add) Token: 0x06001665 RID: 5733 RVA: 0x00052774 File Offset: 0x00050974
		// (remove) Token: 0x06001666 RID: 5734 RVA: 0x00052780 File Offset: 0x00050980
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

		/// <summary>Occurs when the control is double-clicked.</summary>
		// Token: 0x14000176 RID: 374
		// (add) Token: 0x06001667 RID: 5735 RVA: 0x0005278C File Offset: 0x0005098C
		// (remove) Token: 0x06001668 RID: 5736 RVA: 0x00052798 File Offset: 0x00050998
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DateTimePicker.ForeColor" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000177 RID: 375
		// (add) Token: 0x06001669 RID: 5737 RVA: 0x000527A4 File Offset: 0x000509A4
		// (remove) Token: 0x0600166A RID: 5738 RVA: 0x000527B0 File Offset: 0x000509B0
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

		/// <summary>Occurs when the control is clicked with the mouse.</summary>
		// Token: 0x14000178 RID: 376
		// (add) Token: 0x0600166B RID: 5739 RVA: 0x000527BC File Offset: 0x000509BC
		// (remove) Token: 0x0600166C RID: 5740 RVA: 0x000527C8 File Offset: 0x000509C8
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

		/// <summary>Occurs when the control is double-clicked with the mouse.</summary>
		// Token: 0x14000179 RID: 377
		// (add) Token: 0x0600166D RID: 5741 RVA: 0x000527D4 File Offset: 0x000509D4
		// (remove) Token: 0x0600166E RID: 5742 RVA: 0x000527E0 File Offset: 0x000509E0
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DateTimePicker.Padding" /> property changes.</summary>
		// Token: 0x1400017A RID: 378
		// (add) Token: 0x0600166F RID: 5743 RVA: 0x000527EC File Offset: 0x000509EC
		// (remove) Token: 0x06001670 RID: 5744 RVA: 0x000527F8 File Offset: 0x000509F8
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

		/// <summary>Occurs when the control is redrawn.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400017B RID: 379
		// (add) Token: 0x06001671 RID: 5745 RVA: 0x00052804 File Offset: 0x00050A04
		// (remove) Token: 0x06001672 RID: 5746 RVA: 0x00052810 File Offset: 0x00050A10
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.DateTimePicker.RightToLeftLayout" /> property changes. </summary>
		// Token: 0x1400017C RID: 380
		// (add) Token: 0x06001673 RID: 5747 RVA: 0x0005281C File Offset: 0x00050A1C
		// (remove) Token: 0x06001674 RID: 5748 RVA: 0x00052830 File Offset: 0x00050A30
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.Events.AddHandler(DateTimePicker.RightToLeftLayoutChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DateTimePicker.RightToLeftLayoutChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.DateTimePicker.Text" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400017D RID: 381
		// (add) Token: 0x06001675 RID: 5749 RVA: 0x00052844 File Offset: 0x00050A44
		// (remove) Token: 0x06001676 RID: 5750 RVA: 0x00052850 File Offset: 0x00050A50
		[EditorBrowsable(2)]
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

		// Token: 0x1400017E RID: 382
		// (add) Token: 0x06001677 RID: 5751 RVA: 0x0005285C File Offset: 0x00050A5C
		// (remove) Token: 0x06001678 RID: 5752 RVA: 0x00052870 File Offset: 0x00050A70
		internal event EventHandler UIAMinimumChanged
		{
			add
			{
				base.Events.AddHandler(DateTimePicker.UIAMinimumChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DateTimePicker.UIAMinimumChangedEvent, value);
			}
		}

		// Token: 0x1400017F RID: 383
		// (add) Token: 0x06001679 RID: 5753 RVA: 0x00052884 File Offset: 0x00050A84
		// (remove) Token: 0x0600167A RID: 5754 RVA: 0x00052898 File Offset: 0x00050A98
		internal event EventHandler UIAMaximumChanged
		{
			add
			{
				base.Events.AddHandler(DateTimePicker.UIAMinimumChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DateTimePicker.UIAMinimumChangedEvent, value);
			}
		}

		// Token: 0x14000180 RID: 384
		// (add) Token: 0x0600167B RID: 5755 RVA: 0x000528AC File Offset: 0x00050AAC
		// (remove) Token: 0x0600167C RID: 5756 RVA: 0x000528C0 File Offset: 0x00050AC0
		internal event EventHandler UIASelectionChanged
		{
			add
			{
				base.Events.AddHandler(DateTimePicker.UIASelectionChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DateTimePicker.UIASelectionChangedEvent, value);
			}
		}

		// Token: 0x14000181 RID: 385
		// (add) Token: 0x0600167D RID: 5757 RVA: 0x000528D4 File Offset: 0x00050AD4
		// (remove) Token: 0x0600167E RID: 5758 RVA: 0x000528E8 File Offset: 0x00050AE8
		internal event EventHandler UIAChecked
		{
			add
			{
				base.Events.AddHandler(DateTimePicker.UIACheckedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DateTimePicker.UIACheckedEvent, value);
			}
		}

		// Token: 0x14000182 RID: 386
		// (add) Token: 0x0600167F RID: 5759 RVA: 0x000528FC File Offset: 0x00050AFC
		// (remove) Token: 0x06001680 RID: 5760 RVA: 0x00052910 File Offset: 0x00050B10
		internal event EventHandler UIAShowCheckBoxChanged
		{
			add
			{
				base.Events.AddHandler(DateTimePicker.UIAShowCheckBoxChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DateTimePicker.UIAShowCheckBoxChangedEvent, value);
			}
		}

		// Token: 0x14000183 RID: 387
		// (add) Token: 0x06001681 RID: 5761 RVA: 0x00052924 File Offset: 0x00050B24
		// (remove) Token: 0x06001682 RID: 5762 RVA: 0x00052938 File Offset: 0x00050B38
		internal event EventHandler UIAShowUpDownChanged
		{
			add
			{
				base.Events.AddHandler(DateTimePicker.UIAShowUpDownChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DateTimePicker.UIAShowUpDownChangedEvent, value);
			}
		}

		/// <summary>Gets or sets a value indicating the background color of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>The background <see cref="T:System.Drawing.Color" /> of the <see cref="T:System.Windows.Forms.DateTimePicker" />. </returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001684 RID: 5764 RVA: 0x00052958 File Offset: 0x00050B58
		// (set) Token: 0x06001683 RID: 5763 RVA: 0x0005294C File Offset: 0x00050B4C
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

		/// <summary>Gets or sets the background image for the control.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x0005296C File Offset: 0x00050B6C
		// (set) Token: 0x06001685 RID: 5765 RVA: 0x00052960 File Offset: 0x00050B60
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

		/// <summary>Gets or sets the layout of the background image of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x00052974 File Offset: 0x00050B74
		// (set) Token: 0x06001688 RID: 5768 RVA: 0x0005297C File Offset: 0x00050B7C
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

		/// <summary>Gets or sets the font style applied to the calendar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Font" /> that represents the font style applied to the calendar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x00052998 File Offset: 0x00050B98
		// (set) Token: 0x06001689 RID: 5769 RVA: 0x00052988 File Offset: 0x00050B88
		[AmbientValue(null)]
		[Localizable(true)]
		public Font CalendarFont
		{
			get
			{
				return this.month_calendar.Font;
			}
			set
			{
				this.month_calendar.Font = value;
			}
		}

		/// <summary>Gets or sets the foreground color of the calendar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the calendar.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x0600168C RID: 5772 RVA: 0x000529B8 File Offset: 0x00050BB8
		// (set) Token: 0x0600168B RID: 5771 RVA: 0x000529A8 File Offset: 0x00050BA8
		public Color CalendarForeColor
		{
			get
			{
				return this.month_calendar.ForeColor;
			}
			set
			{
				this.month_calendar.ForeColor = value;
			}
		}

		/// <summary>Gets or sets the background color of the calendar month.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the calendar month.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x0600168E RID: 5774 RVA: 0x000529D8 File Offset: 0x00050BD8
		// (set) Token: 0x0600168D RID: 5773 RVA: 0x000529C8 File Offset: 0x00050BC8
		public Color CalendarMonthBackground
		{
			get
			{
				return this.month_calendar.BackColor;
			}
			set
			{
				this.month_calendar.BackColor = value;
			}
		}

		/// <summary>Gets or sets the background color of the calendar title.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the calendar title.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x000529F8 File Offset: 0x00050BF8
		// (set) Token: 0x0600168F RID: 5775 RVA: 0x000529E8 File Offset: 0x00050BE8
		public Color CalendarTitleBackColor
		{
			get
			{
				return this.month_calendar.TitleBackColor;
			}
			set
			{
				this.month_calendar.TitleBackColor = value;
			}
		}

		/// <summary>Gets or sets the foreground color of the calendar title.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the calendar title.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06001692 RID: 5778 RVA: 0x00052A18 File Offset: 0x00050C18
		// (set) Token: 0x06001691 RID: 5777 RVA: 0x00052A08 File Offset: 0x00050C08
		public Color CalendarTitleForeColor
		{
			get
			{
				return this.month_calendar.TitleForeColor;
			}
			set
			{
				this.month_calendar.TitleForeColor = value;
			}
		}

		/// <summary>Gets or sets the foreground color of the calendar trailing dates.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the foreground color of the calendar trailing dates.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x00052A38 File Offset: 0x00050C38
		// (set) Token: 0x06001693 RID: 5779 RVA: 0x00052A28 File Offset: 0x00050C28
		public Color CalendarTrailingForeColor
		{
			get
			{
				return this.month_calendar.TrailingForeColor;
			}
			set
			{
				this.month_calendar.TrailingForeColor = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="P:System.Windows.Forms.DateTimePicker.Value" /> property has been set with a valid date/time value and the displayed value is able to be updated.</summary>
		/// <returns>true if the <see cref="P:System.Windows.Forms.DateTimePicker.Value" /> property has been set with a valid <see cref="T:System.DateTime" /> value and the displayed value is able to be updated; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x00052AB4 File Offset: 0x00050CB4
		// (set) Token: 0x06001695 RID: 5781 RVA: 0x00052A48 File Offset: 0x00050C48
		[DefaultValue(true)]
		[Bindable(true)]
		public bool Checked
		{
			get
			{
				return this.is_checked;
			}
			set
			{
				if (this.is_checked != value)
				{
					this.is_checked = value;
					if (this.ShowCheckBox)
					{
						for (int i = 0; i < this.part_data.Length; i++)
						{
							this.part_data[i].Selected = false;
						}
						base.Invalidate(this.date_area_rect);
						this.OnUIAChecked();
						this.OnUIASelectionChanged();
					}
				}
			}
		}

		/// <summary>Gets or sets the custom date/time format string.</summary>
		/// <returns>A string that represents the custom date/time format. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x00052AF4 File Offset: 0x00050CF4
		// (set) Token: 0x06001697 RID: 5783 RVA: 0x00052ABC File Offset: 0x00050CBC
		[Localizable(true)]
		[RefreshProperties(2)]
		[DefaultValue(null)]
		public string CustomFormat
		{
			get
			{
				return this.custom_format;
			}
			set
			{
				if (this.custom_format != value)
				{
					this.custom_format = value;
					if (this.Format == DateTimePickerFormat.Custom)
					{
						this.CalculateFormats();
					}
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the control should redraw its surface using a secondary buffer. Setting this property has no effect on the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>true if the control should redraw its surface using a secondary buffer; otherwise, false.</returns>
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x00052AFC File Offset: 0x00050CFC
		// (set) Token: 0x0600169A RID: 5786 RVA: 0x00052B04 File Offset: 0x00050D04
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

		/// <summary>Gets or sets the alignment of the drop-down calendar on the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>The alignment of the drop-down calendar on the control. The default is <see cref="F:System.Windows.Forms.LeftRightAlignment.Left" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.LeftRightAlignment" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x00052B28 File Offset: 0x00050D28
		// (set) Token: 0x0600169B RID: 5787 RVA: 0x00052B10 File Offset: 0x00050D10
		[Localizable(true)]
		[DefaultValue(LeftRightAlignment.Left)]
		public LeftRightAlignment DropDownAlign
		{
			get
			{
				return this.drop_down_align;
			}
			set
			{
				if (this.drop_down_align != value)
				{
					this.drop_down_align = value;
				}
			}
		}

		/// <summary>Gets or sets the foreground color of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the <see cref="T:System.Windows.Forms.DateTimePicker" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00052B3C File Offset: 0x00050D3C
		// (set) Token: 0x0600169D RID: 5789 RVA: 0x00052B30 File Offset: 0x00050D30
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

		/// <summary>Gets or sets the format of the date and time displayed in the control.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.DateTimePickerFormat" /> values. The default is <see cref="F:System.Windows.Forms.DateTimePickerFormat.Long" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value assigned is not one of the <see cref="T:System.Windows.Forms.DateTimePickerFormat" /> values. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x00052B88 File Offset: 0x00050D88
		// (set) Token: 0x0600169F RID: 5791 RVA: 0x00052B44 File Offset: 0x00050D44
		[RefreshProperties(2)]
		public DateTimePickerFormat Format
		{
			get
			{
				return this.format;
			}
			set
			{
				if (this.format != value)
				{
					this.format = value;
					base.RecreateHandle();
					this.CalculateFormats();
					this.OnFormatChanged(EventArgs.Empty);
					base.Invalidate(this.date_area_rect);
				}
			}
		}

		/// <summary>Gets or sets the maximum date and time that can be selected in the control.</summary>
		/// <returns>The maximum date and time that can be selected in the control. The default is 12/31/9998 23:59:59.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned is less than the <see cref="P:System.Windows.Forms.DateTimePicker.MinDate" /> value. </exception>
		/// <exception cref="T:System.SystemException">The value assigned is greater than the <see cref="F:System.Windows.Forms.DateTimePicker.MaxDateTime" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x060016A2 RID: 5794 RVA: 0x00052C78 File Offset: 0x00050E78
		// (set) Token: 0x060016A1 RID: 5793 RVA: 0x00052B90 File Offset: 0x00050D90
		public DateTime MaxDate
		{
			get
			{
				return this.max_date;
			}
			set
			{
				if (value < this.min_date)
				{
					string text = string.Format(CultureInfo.CurrentCulture, "'{0}' is not a valid value for 'MaxDate'. 'MaxDate' must be greater than or equal to MinDate.", new object[] { value.ToString("G") });
					throw new ArgumentOutOfRangeException("MaxDate", text);
				}
				if (value > DateTimePicker.MaxDateTime)
				{
					string text2 = string.Format(CultureInfo.CurrentCulture, "DateTimePicker does not support dates after {0}.", new object[] { DateTimePicker.MaxDateTime.ToString("G", CultureInfo.CurrentCulture) });
					throw new ArgumentOutOfRangeException("MaxDate", text2);
				}
				if (this.max_date != value)
				{
					this.max_date = value;
					if (this.Value > this.max_date)
					{
						this.Value = this.max_date;
						base.Invalidate(this.date_area_rect);
					}
					this.OnUIAMaximumChanged();
				}
			}
		}

		/// <summary>Gets the maximum date value allowed for the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> representing the maximum date value for the <see cref="P:System.Windows.Forms.DateTimePicker.MaximumDateTime" /> control.</returns>
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x060016A3 RID: 5795 RVA: 0x00052C80 File Offset: 0x00050E80
		public static DateTime MaximumDateTime
		{
			get
			{
				return DateTimePicker.MaxDateTime;
			}
		}

		/// <summary>Gets or sets the minimum date and time that can be selected in the control.</summary>
		/// <returns>The minimum date and time that can be selected in the control. The default is 1/1/1753 00:00:00.</returns>
		/// <exception cref="T:System.ArgumentException">The value assigned is not less than the <see cref="P:System.Windows.Forms.DateTimePicker.MaxDate" /> value. </exception>
		/// <exception cref="T:System.SystemException">The value assigned is less than the <see cref="F:System.Windows.Forms.DateTimePicker.MinDateTime" /> value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x060016A5 RID: 5797 RVA: 0x00052D88 File Offset: 0x00050F88
		// (set) Token: 0x060016A4 RID: 5796 RVA: 0x00052C88 File Offset: 0x00050E88
		public DateTime MinDate
		{
			get
			{
				return this.min_date;
			}
			set
			{
				if (value == DateTime.MinValue)
				{
					value = DateTimePicker.MinDateTime;
				}
				if (value > this.MaxDate)
				{
					string text = string.Format(CultureInfo.CurrentCulture, "'{0}' is not a valid value for 'MinDate'. 'MinDate' must be less than MaxDate.", new object[] { value.ToString("G") });
					throw new ArgumentOutOfRangeException("MinDate", text);
				}
				if (value < DateTimePicker.MinDateTime)
				{
					string text2 = string.Format(CultureInfo.CurrentCulture, "DateTimePicker does not support dates before {0}.", new object[] { DateTimePicker.MinDateTime.ToString("G", CultureInfo.CurrentCulture) });
					throw new ArgumentOutOfRangeException("MinDate", text2);
				}
				if (this.min_date != value)
				{
					this.min_date = value;
					if (this.Value < this.min_date)
					{
						this.Value = this.min_date;
						base.Invalidate(this.date_area_rect);
					}
					this.OnUIAMinimumChanged();
				}
			}
		}

		/// <summary>Gets the minimum date value allowed for the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> representing the minimum date value for the <see cref="P:System.Windows.Forms.DateTimePicker.MaximumDateTime" /> control.</returns>
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x060016A6 RID: 5798 RVA: 0x00052D90 File Offset: 0x00050F90
		public static DateTime MinimumDateTime
		{
			get
			{
				return DateTimePicker.MinDateTime;
			}
		}

		/// <summary>Gets or sets the spacing between the contents of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control and its edges.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.Padding.Empty" /> in all cases.</returns>
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060016A7 RID: 5799 RVA: 0x00052D98 File Offset: 0x00050F98
		// (set) Token: 0x060016A8 RID: 5800 RVA: 0x00052DA0 File Offset: 0x00050FA0
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

		/// <summary>Gets the preferred height of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>The preferred height, in pixels, of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x060016A9 RID: 5801 RVA: 0x00052DAC File Offset: 0x00050FAC
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		public int PreferredHeight
		{
			get
			{
				return (int)Math.Ceiling((double)this.Font.Height * 1.5);
			}
		}

		/// <summary>Gets or sets whether the contents of the <see cref="T:System.Windows.Forms.DateTimePicker" /> are laid out from right to left.</summary>
		/// <returns>true if the layout of the <see cref="T:System.Windows.Forms.DateTimePicker" /> contents is from right to left; otherwise, false. The default is false.</returns>
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x060016AA RID: 5802 RVA: 0x00052DCC File Offset: 0x00050FCC
		// (set) Token: 0x060016AB RID: 5803 RVA: 0x00052DD4 File Offset: 0x00050FD4
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
				if (this.right_to_left_layout != value)
				{
					this.right_to_left_layout = value;
					this.OnRightToLeftLayoutChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a check box is displayed to the left of the selected date.</summary>
		/// <returns>true if a check box is displayed to the left of the selected date; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x00052E1C File Offset: 0x0005101C
		// (set) Token: 0x060016AC RID: 5804 RVA: 0x00052DF4 File Offset: 0x00050FF4
		[DefaultValue(false)]
		public bool ShowCheckBox
		{
			get
			{
				return this.show_check_box;
			}
			set
			{
				if (this.show_check_box != value)
				{
					this.show_check_box = value;
					base.Invalidate(this.date_area_rect);
					this.OnUIAShowCheckBoxChanged();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether a spin button control (also known as an up-down control) is used to adjust the date/time value.</summary>
		/// <returns>true if a spin button control is used to adjust the date/time value; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x00052E48 File Offset: 0x00051048
		// (set) Token: 0x060016AE RID: 5806 RVA: 0x00052E24 File Offset: 0x00051024
		[DefaultValue(false)]
		public bool ShowUpDown
		{
			get
			{
				return this.show_up_down;
			}
			set
			{
				if (this.show_up_down != value)
				{
					this.show_up_down = value;
					base.Invalidate();
					this.OnUIAShowUpDownChanged();
				}
			}
		}

		/// <summary>Gets or sets the text associated with this control.</summary>
		/// <returns>A string that represents the text associated with this control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x00052EDC File Offset: 0x000510DC
		// (set) Token: 0x060016B0 RID: 5808 RVA: 0x00052E50 File Offset: 0x00051050
		[DesignerSerializationVisibility(0)]
		[Browsable(false)]
		[EditorBrowsable(2)]
		public override string Text
		{
			get
			{
				if (!base.IsHandleCreated)
				{
					return string.Empty;
				}
				if (this.format == DateTimePickerFormat.Custom)
				{
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < this.part_data.Length; i++)
					{
						stringBuilder.Append(this.part_data[i].GetText(this.date_value));
					}
					return stringBuilder.ToString();
				}
				return this.Value.ToString(this.GetExactFormat());
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					this.date_value = DateTime.Now;
					this.OnValueChanged(EventArgs.Empty);
					this.OnTextChanged(EventArgs.Empty);
					return;
				}
				DateTime dateTime;
				if (this.format == DateTimePickerFormat.Custom)
				{
					dateTime = DateTime.ParseExact(value, this.GetExactFormat(), null);
				}
				else
				{
					dateTime = DateTime.ParseExact(value, this.GetExactFormat(), null);
				}
				if (this.date_value != dateTime)
				{
					this.Value = dateTime;
				}
			}
		}

		/// <summary>Gets or sets the date/time value assigned to the control.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> value assign to the control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The set value is less than <see cref="P:System.Windows.Forms.DateTimePicker.MinDate" /> or more than <see cref="P:System.Windows.Forms.DateTimePicker.MaxDate" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x00052FCC File Offset: 0x000511CC
		// (set) Token: 0x060016B2 RID: 5810 RVA: 0x00052F5C File Offset: 0x0005115C
		[RefreshProperties(1)]
		[Bindable(true)]
		public DateTime Value
		{
			get
			{
				return this.date_value;
			}
			set
			{
				if (this.date_value != value)
				{
					if (value < this.MinDate || value > this.MaxDate)
					{
						throw new ArgumentOutOfRangeException("value", "value must be between MinDate and MaxDate");
					}
					this.date_value = value;
					this.OnValueChanged(EventArgs.Empty);
					base.Invalidate(this.date_area_rect);
				}
			}
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.DateTimePicker" />. The string includes the type and the <see cref="P:System.Windows.Forms.DateTimePicker.Value" /> property of the control.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060016B4 RID: 5812 RVA: 0x00052FD4 File Offset: 0x000511D4
		public override string ToString()
		{
			return this.Text;
		}

		/// <summary>Returns the <see cref="T:System.Windows.Forms.CreateParams" /> used to create this window.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> that contains the required creation parameters when the handle to the control is created.</returns>
		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x060016B5 RID: 5813 RVA: 0x00052FDC File Offset: 0x000511DC
		protected override CreateParams CreateParams
		{
			get
			{
				return base.CreateParams;
			}
		}

		/// <returns>The default <see cref="T:System.Drawing.Size" /> of the control.</returns>
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060016B6 RID: 5814 RVA: 0x00052FE4 File Offset: 0x000511E4
		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, this.PreferredHeight);
			}
		}

		/// <summary>Creates a new accessibility object for the <see cref="T:System.Windows.Forms.DateTimePicker" /> control.</summary>
		/// <returns>A new <see cref="T:System.Windows.Forms.DateTimePicker.DateTimePickerAccessibleObject" /> for the control.</returns>
		// Token: 0x060016B7 RID: 5815 RVA: 0x00052FF8 File Offset: 0x000511F8
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return base.CreateAccessibilityInstance();
		}

		/// <summary>Creates the physical window handle.</summary>
		// Token: 0x060016B8 RID: 5816 RVA: 0x00053000 File Offset: 0x00051200
		protected override void CreateHandle()
		{
			base.CreateHandle();
		}

		/// <summary>Destroys the physical window handle.</summary>
		// Token: 0x060016B9 RID: 5817 RVA: 0x00053008 File Offset: 0x00051208
		protected override void DestroyHandle()
		{
			base.DestroyHandle();
		}

		/// <summary>Determines whether the specified key is a regular input key or a special key that requires preprocessing.</summary>
		/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values. </param>
		// Token: 0x060016BA RID: 5818 RVA: 0x00053010 File Offset: 0x00051210
		protected override bool IsInputKey(Keys keyData)
		{
			switch (keyData)
			{
			case Keys.Left:
			case Keys.Up:
			case Keys.Right:
			case Keys.Down:
				return true;
			default:
				return false;
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DateTimePicker.CloseUp" /> event.</summary>
		/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060016BB RID: 5819 RVA: 0x00053040 File Offset: 0x00051240
		protected virtual void OnCloseUp(EventArgs eventargs)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DateTimePicker.CloseUpEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, eventargs);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DateTimePicker.DropDown" /> event.</summary>
		/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060016BC RID: 5820 RVA: 0x00053074 File Offset: 0x00051274
		protected virtual void OnDropDown(EventArgs eventargs)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DateTimePicker.DropDownEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, eventargs);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060016BD RID: 5821 RVA: 0x000530A8 File Offset: 0x000512A8
		protected override void OnFontChanged(EventArgs e)
		{
			this.month_calendar.Font = this.Font;
			base.Size = new Size(base.Size.Width, this.PreferredHeight);
			base.OnFontChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DateTimePicker.FormatChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060016BE RID: 5822 RVA: 0x000530EC File Offset: 0x000512EC
		protected virtual void OnFormatChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DateTimePicker.FormatChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060016BF RID: 5823 RVA: 0x00053120 File Offset: 0x00051320
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060016C0 RID: 5824 RVA: 0x0005312C File Offset: 0x0005132C
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="P:System.Windows.Forms.DateTimePicker.RightToLeftLayout" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060016C1 RID: 5825 RVA: 0x00053138 File Offset: 0x00051338
		[EditorBrowsable(2)]
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DateTimePicker.RightToLeftLayoutChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.OnSystemColorsChanged(System.EventArgs)" /> method.</summary>
		// Token: 0x060016C2 RID: 5826 RVA: 0x0005316C File Offset: 0x0005136C
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.DateTimePicker.ValueChanged" /> event.</summary>
		/// <param name="eventargs">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060016C3 RID: 5827 RVA: 0x00053178 File Offset: 0x00051378
		protected virtual void OnValueChanged(EventArgs eventargs)
		{
			EventHandler eventHandler = (EventHandler)base.Events[DateTimePicker.ValueChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, eventargs);
			}
		}

		// Token: 0x060016C4 RID: 5828 RVA: 0x000531AC File Offset: 0x000513AC
		internal override int OverrideHeight(int height)
		{
			return this.DefaultSize.Height;
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" /> method.</summary>
		// Token: 0x060016C5 RID: 5829 RVA: 0x000531C8 File Offset: 0x000513C8
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060016C6 RID: 5830 RVA: 0x000531D4 File Offset: 0x000513D4
		internal Rectangle date_area_rect
		{
			get
			{
				return ThemeEngine.Current.DateTimePickerGetDateArea(this);
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060016C7 RID: 5831 RVA: 0x000531E4 File Offset: 0x000513E4
		internal Rectangle CheckBoxRect
		{
			get
			{
				Rectangle rectangle;
				rectangle..ctor(4, base.ClientSize.Height / 2 - 6, 13, 13);
				return rectangle;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060016C8 RID: 5832 RVA: 0x00053210 File Offset: 0x00051410
		internal Rectangle drop_down_arrow_rect
		{
			get
			{
				return ThemeEngine.Current.DateTimePickerGetDropDownButtonArea(this);
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060016C9 RID: 5833 RVA: 0x00053220 File Offset: 0x00051420
		internal Rectangle hilight_date_area
		{
			get
			{
				return Rectangle.Empty;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060016CA RID: 5834 RVA: 0x00053228 File Offset: 0x00051428
		internal bool DropDownButtonEntered
		{
			get
			{
				return this.drop_down_button_entered;
			}
		}

		// Token: 0x060016CB RID: 5835 RVA: 0x00053230 File Offset: 0x00051430
		private void ResizeHandler(object sender, EventArgs e)
		{
			base.Invalidate();
		}

		// Token: 0x060016CC RID: 5836 RVA: 0x00053238 File Offset: 0x00051438
		private void UpDownTimerTick(object sender, EventArgs e)
		{
			if (this.updown_timer.Interval == 500)
			{
				this.updown_timer.Interval = 100;
			}
			if (this.is_down_pressed)
			{
				this.IncrementSelectedPart(-1);
			}
			else if (this.is_up_pressed)
			{
				this.IncrementSelectedPart(1);
			}
			else
			{
				this.updown_timer.Enabled = false;
			}
		}

		// Token: 0x060016CD RID: 5837 RVA: 0x000532A4 File Offset: 0x000514A4
		internal float CalculateMaxWidth(string format, Graphics gr, StringFormat string_format)
		{
			float num = 0f;
			Font font = this.Font;
			if (format != null)
			{
				if (DateTimePicker.<>f__switch$map1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(21);
					dictionary.Add("M", 0);
					dictionary.Add("MM", 0);
					dictionary.Add("MMM", 0);
					dictionary.Add("MMMM", 0);
					dictionary.Add("d", 1);
					dictionary.Add("dd", 1);
					dictionary.Add("ddd", 1);
					dictionary.Add("dddd", 1);
					dictionary.Add("h", 2);
					dictionary.Add("hh", 2);
					dictionary.Add("H", 3);
					dictionary.Add("HH", 3);
					dictionary.Add("m", 4);
					dictionary.Add("mm", 4);
					dictionary.Add("s", 5);
					dictionary.Add("ss", 5);
					dictionary.Add("t", 6);
					dictionary.Add("tt", 6);
					dictionary.Add("y", 7);
					dictionary.Add("yy", 7);
					dictionary.Add("yyyy", 7);
					DateTimePicker.<>f__switch$map1 = dictionary;
				}
				int num2;
				if (DateTimePicker.<>f__switch$map1.TryGetValue(format, ref num2))
				{
					switch (num2)
					{
					case 0:
					{
						for (int i = 1; i <= 12; i++)
						{
							string text = DateTimePicker.PartData.GetText(this.Value.AddMonths(i), format);
							num = Math.Max(num, gr.MeasureString(text, font, int.MaxValue, string_format).Width);
						}
						return num;
					}
					case 1:
					{
						for (int j = 1; j <= 12; j++)
						{
							string text = DateTimePicker.PartData.GetText(this.Value.AddDays((double)j), format);
							num = Math.Max(num, gr.MeasureString(text, font, int.MaxValue, string_format).Width);
						}
						return num;
					}
					case 2:
					{
						for (int k = 1; k <= 12; k++)
						{
							string text = DateTimePicker.PartData.GetText(this.Value.AddHours((double)k), format);
							num = Math.Max(num, gr.MeasureString(text, font, int.MaxValue, string_format).Width);
						}
						return num;
					}
					case 3:
					{
						for (int l = 1; l <= 24; l++)
						{
							string text = DateTimePicker.PartData.GetText(this.Value.AddDays((double)l), format);
							num = Math.Max(num, gr.MeasureString(text, font, int.MaxValue, string_format).Width);
						}
						return num;
					}
					case 4:
					{
						for (int m = 1; m <= 60; m++)
						{
							string text = DateTimePicker.PartData.GetText(this.Value.AddMinutes((double)m), format);
							num = Math.Max(num, gr.MeasureString(text, font, int.MaxValue, string_format).Width);
						}
						return num;
					}
					case 5:
					{
						for (int n = 1; n <= 60; n++)
						{
							string text = DateTimePicker.PartData.GetText(this.Value.AddSeconds((double)n), format);
							num = Math.Max(num, gr.MeasureString(text, font, int.MaxValue, string_format).Width);
						}
						return num;
					}
					case 6:
					{
						for (int num3 = 1; num3 <= 2; num3++)
						{
							string text = DateTimePicker.PartData.GetText(this.Value.AddHours((double)(num3 * 12)), format);
							num = Math.Max(num, gr.MeasureString(text, font, int.MaxValue, string_format).Width);
						}
						return num;
					}
					case 7:
					{
						string text = DateTimePicker.PartData.GetText(this.Value, format);
						return Math.Max(num, gr.MeasureString(text, font, int.MaxValue, string_format).Width);
					}
					}
				}
			}
			return gr.MeasureString(format, font, int.MaxValue, string_format).Width;
		}

		// Token: 0x060016CE RID: 5838 RVA: 0x000536A0 File Offset: 0x000518A0
		private string GetExactFormat()
		{
			switch (this.format)
			{
			case DateTimePickerFormat.Long:
				return Thread.CurrentThread.CurrentCulture.DateTimeFormat.LongDatePattern;
			case DateTimePickerFormat.Short:
				return Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern;
			case DateTimePickerFormat.Time:
				return Thread.CurrentThread.CurrentCulture.DateTimeFormat.LongTimePattern;
			case DateTimePickerFormat.Custom:
				return (this.custom_format != null) ? this.custom_format : string.Empty;
			}
			return Thread.CurrentThread.CurrentCulture.DateTimeFormat.LongDatePattern;
		}

		// Token: 0x060016CF RID: 5839 RVA: 0x00053750 File Offset: 0x00051950
		private void CalculateFormats()
		{
			StringBuilder stringBuilder = new StringBuilder();
			ArrayList arrayList = new ArrayList();
			bool flag = false;
			char c = '\0';
			string exactFormat = this.GetExactFormat();
			for (int i = 0; i < exactFormat.Length; i++)
			{
				char c2 = exactFormat.get_Chars(i);
				if (!flag || c2 == '\'')
				{
					char c3 = c2;
					switch (c3)
					{
					case 'd':
					case 'g':
					case 'h':
						goto IL_00AD;
					default:
						if (c3 == 's' || c3 == 't')
						{
							goto IL_00AD;
						}
						if (c3 != '\'')
						{
							if (c3 == 'H' || c3 == 'M' || c3 == 'm' || c3 == 'y')
							{
								goto IL_00AD;
							}
							if (stringBuilder.Length != 0)
							{
								arrayList.Add(new DateTimePicker.PartData(stringBuilder.ToString(), false, this));
								stringBuilder.Length = 0;
							}
							arrayList.Add(new DateTimePicker.PartData(c2.ToString(), true, this));
						}
						else if (flag && i < exactFormat.Length - 1 && exactFormat.get_Chars(i + 1) == '\'')
						{
							stringBuilder.Append(c2);
							i++;
						}
						else if (stringBuilder.Length == 0)
						{
							flag = !flag;
						}
						else
						{
							arrayList.Add(new DateTimePicker.PartData(stringBuilder.ToString(), flag, this));
							stringBuilder.Length = 0;
							flag = !flag;
						}
						break;
					}
					IL_01A5:
					c = c2;
					goto IL_01A9;
					IL_00AD:
					if (c != c2 && c != '\0' && stringBuilder.Length != 0)
					{
						arrayList.Add(new DateTimePicker.PartData(stringBuilder.ToString(), false, this));
						stringBuilder.Length = 0;
					}
					stringBuilder.Append(c2);
					goto IL_01A5;
				}
				stringBuilder.Append(c2);
				IL_01A9:;
			}
			if (stringBuilder.Length >= 0)
			{
				arrayList.Add(new DateTimePicker.PartData(stringBuilder.ToString(), flag, this));
			}
			this.part_data = new DateTimePicker.PartData[arrayList.Count];
			arrayList.CopyTo(this.part_data);
		}

		// Token: 0x060016D0 RID: 5840 RVA: 0x00053958 File Offset: 0x00051B58
		private Point CalculateDropDownLocation(Rectangle parent_control_rect, Size child_size, bool align_left)
		{
			Point point;
			point..ctor(parent_control_rect.Left + 5, parent_control_rect.Bottom);
			if (!align_left)
			{
				point.X = parent_control_rect.Right - child_size.Width;
			}
			Point point2 = base.PointToScreen(point);
			Rectangle workingArea = Screen.FromControl(this).WorkingArea;
			if (point2.X < workingArea.X)
			{
				point2.X = workingArea.X;
			}
			if (point2.Y + child_size.Height > workingArea.Bottom)
			{
				point2.Y -= parent_control_rect.Height + child_size.Height;
			}
			if (this.month_calendar.Parent != null)
			{
				point2 = this.month_calendar.Parent.PointToClient(point2);
			}
			return point2;
		}

		// Token: 0x060016D1 RID: 5841 RVA: 0x00053A28 File Offset: 0x00051C28
		internal void Draw(Rectangle clip_rect, Graphics dc)
		{
			ThemeEngine.Current.DrawDateTimePicker(dc, clip_rect, this);
		}

		// Token: 0x060016D2 RID: 5842 RVA: 0x00053A38 File Offset: 0x00051C38
		internal void DropDownMonthCalendar()
		{
			this.EndDateEdit(true);
			this.month_calendar.SetDate(this.date_value);
			Rectangle date_area_rect = this.date_area_rect;
			date_area_rect.Y = base.ClientRectangle.Y;
			date_area_rect.Height = base.ClientRectangle.Height;
			this.month_calendar.Location = this.CalculateDropDownLocation(date_area_rect, this.month_calendar.Size, this.DropDownAlign == LeftRightAlignment.Left);
			this.month_calendar.Show();
			this.month_calendar.Focus();
			this.month_calendar.Capture = true;
			EventHandler eventHandler = (EventHandler)base.Events[DateTimePicker.DropDownEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x060016D3 RID: 5843 RVA: 0x00053B00 File Offset: 0x00051D00
		internal void HideMonthCalendar()
		{
			this.is_drop_down_visible = false;
			base.Invalidate(this.drop_down_arrow_rect);
			this.month_calendar.Capture = false;
			if (this.month_calendar.Visible)
			{
				this.month_calendar.Hide();
			}
			base.Focus();
		}

		// Token: 0x060016D4 RID: 5844 RVA: 0x00053B50 File Offset: 0x00051D50
		private int GetSelectedPartIndex()
		{
			for (int i = 0; i < this.part_data.Length; i++)
			{
				if (this.part_data[i].Selected && !this.part_data[i].is_literal)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060016D5 RID: 5845 RVA: 0x00053BA0 File Offset: 0x00051DA0
		internal void IncrementSelectedPart(int delta)
		{
			int selectedPartIndex = this.GetSelectedPartIndex();
			if (selectedPartIndex == -1)
			{
				return;
			}
			this.EndDateEdit(false);
			DateTimePicker.DateTimePart date_time_part = this.part_data[selectedPartIndex].date_time_part;
			switch (date_time_part)
			{
			case DateTimePicker.DateTimePart.Seconds:
				this.SetPart(this.Value.Second + delta, date_time_part);
				break;
			case DateTimePicker.DateTimePart.Minutes:
				this.SetPart(this.Value.Minute + delta, date_time_part);
				break;
			case DateTimePicker.DateTimePart.AMPMHour:
			case DateTimePicker.DateTimePart.Hour:
				this.SetPart(this.Value.Hour + delta, date_time_part);
				break;
			case DateTimePicker.DateTimePart.Day:
				if (delta < 0)
				{
					if (this.Value.Day == 1)
					{
						this.SetPart(DateTime.DaysInMonth(this.Value.Year, this.Value.Month), date_time_part);
					}
					else
					{
						this.SetPart(this.Value.Day + delta, date_time_part);
					}
				}
				else if (this.Value.Day == DateTime.DaysInMonth(this.Value.Year, this.Value.Month))
				{
					this.SetPart(1, date_time_part);
				}
				else
				{
					this.SetPart(this.Value.Day + delta, date_time_part);
				}
				break;
			case DateTimePicker.DateTimePart.DayName:
				this.Value = this.Value.AddDays((double)delta);
				break;
			case DateTimePicker.DateTimePart.Month:
				this.SetPart(this.Value.Month + delta, date_time_part, true);
				break;
			case DateTimePicker.DateTimePart.Year:
				this.SetPart(this.Value.Year + delta, date_time_part);
				break;
			case DateTimePicker.DateTimePart.AMPMSpecifier:
			{
				int num = this.Value.Hour;
				num = ((num < 0 || num > 11) ? (num - 12) : (num + 12));
				this.SetPart(num, DateTimePicker.DateTimePart.Hour);
				break;
			}
			}
		}

		// Token: 0x060016D6 RID: 5846 RVA: 0x00053DB4 File Offset: 0x00051FB4
		internal void SelectPart(int index)
		{
			this.is_checkbox_selected = false;
			for (int i = 0; i < this.part_data.Length; i++)
			{
				this.part_data[i].Selected = i == index;
			}
			base.Invalidate();
			this.OnUIASelectionChanged();
		}

		// Token: 0x060016D7 RID: 5847 RVA: 0x00053E00 File Offset: 0x00052000
		internal void SelectNextPart()
		{
			if (this.is_checkbox_selected)
			{
				for (int i = 0; i < this.part_data.Length; i++)
				{
					if (!this.part_data[i].is_literal)
					{
						this.is_checkbox_selected = false;
						this.part_data[i].Selected = true;
						base.Invalidate();
						break;
					}
				}
			}
			else
			{
				int selectedPartIndex = this.GetSelectedPartIndex();
				if (selectedPartIndex >= 0)
				{
					this.part_data[selectedPartIndex].Selected = false;
				}
				for (int j = selectedPartIndex + 1; j < this.part_data.Length; j++)
				{
					if (!this.part_data[j].is_literal)
					{
						this.part_data[j].Selected = true;
						base.Invalidate();
						break;
					}
				}
				if (this.GetSelectedPartIndex() == -1)
				{
					if (this.ShowCheckBox)
					{
						this.is_checkbox_selected = true;
						base.Invalidate();
					}
					else
					{
						for (int k = 0; k <= selectedPartIndex; k++)
						{
							if (!this.part_data[k].is_literal)
							{
								this.part_data[k].Selected = true;
								base.Invalidate();
								break;
							}
						}
					}
				}
			}
			this.OnUIASelectionChanged();
		}

		// Token: 0x060016D8 RID: 5848 RVA: 0x00053F38 File Offset: 0x00052138
		internal void SelectPreviousPart()
		{
			if (this.is_checkbox_selected)
			{
				for (int i = this.part_data.Length - 1; i >= 0; i--)
				{
					if (!this.part_data[i].is_literal)
					{
						this.is_checkbox_selected = false;
						this.part_data[i].Selected = true;
						base.Invalidate();
						break;
					}
				}
			}
			else
			{
				int selectedPartIndex = this.GetSelectedPartIndex();
				if (selectedPartIndex >= 0)
				{
					this.part_data[selectedPartIndex].Selected = false;
				}
				for (int j = selectedPartIndex - 1; j >= 0; j--)
				{
					if (!this.part_data[j].is_literal)
					{
						this.part_data[j].Selected = true;
						base.Invalidate();
						break;
					}
				}
				if (this.GetSelectedPartIndex() == -1)
				{
					if (this.ShowCheckBox)
					{
						this.is_checkbox_selected = true;
						base.Invalidate();
					}
					else
					{
						for (int k = this.part_data.Length - 1; k >= selectedPartIndex; k--)
						{
							if (!this.part_data[k].is_literal)
							{
								this.part_data[k].Selected = true;
								base.Invalidate();
								break;
							}
						}
					}
				}
			}
			this.OnUIASelectionChanged();
		}

		// Token: 0x060016D9 RID: 5849 RVA: 0x00054074 File Offset: 0x00052274
		private void KeyDownHandler(object sender, KeyEventArgs e)
		{
			Keys keyCode = e.KeyCode;
			switch (keyCode)
			{
			case Keys.Left:
				if (this.ShowCheckBox && !this.Checked)
				{
					return;
				}
				this.SelectPreviousPart();
				e.Handled = true;
				return;
			case Keys.Up:
				break;
			case Keys.Right:
				if (this.ShowCheckBox && !this.Checked)
				{
					return;
				}
				this.SelectNextPart();
				e.Handled = true;
				return;
			case Keys.Down:
				goto IL_0070;
			default:
				switch (keyCode)
				{
				case Keys.Add:
					break;
				default:
					if (keyCode != Keys.F4)
					{
						return;
					}
					if (!e.Alt && !this.is_drop_down_visible)
					{
						this.DropDownMonthCalendar();
						e.Handled = true;
					}
					return;
				case Keys.Subtract:
					goto IL_0070;
				}
				break;
			}
			if (this.ShowCheckBox && !this.Checked)
			{
				return;
			}
			this.IncrementSelectedPart(1);
			e.Handled = true;
			return;
			IL_0070:
			if (!this.ShowCheckBox || this.Checked)
			{
				this.IncrementSelectedPart(-1);
				e.Handled = true;
			}
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x000541A4 File Offset: 0x000523A4
		private void KeyPressHandler(object sender, KeyPressEventArgs e)
		{
			char keyChar = e.KeyChar;
			switch (keyChar)
			{
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
			{
				int num = (int)(e.KeyChar - '0');
				int selectedPartIndex = this.GetSelectedPartIndex();
				if (selectedPartIndex != -1)
				{
					if (this.part_data[selectedPartIndex].is_numeric_format)
					{
						DateTimePicker.DateTimePart date_time_part = this.part_data[selectedPartIndex].date_time_part;
						if (this.editing_part_index < 0)
						{
							this.editing_part_index = selectedPartIndex;
							this.editing_number = 0;
							this.editing_text = string.Empty;
						}
						this.editing_text += num.ToString();
						int num2 = 0;
						switch (date_time_part)
						{
						case DateTimePicker.DateTimePart.Seconds:
						case DateTimePicker.DateTimePart.Minutes:
						case DateTimePicker.DateTimePart.AMPMHour:
						case DateTimePicker.DateTimePart.Hour:
						case DateTimePicker.DateTimePart.Day:
						case DateTimePicker.DateTimePart.Month:
							num2 = 2;
							break;
						case DateTimePicker.DateTimePart.Year:
							num2 = 4;
							break;
						}
						this.editing_number = this.editing_number * 10 + num;
						if (this.editing_text.Length >= num2)
						{
							this.EndDateEdit(false);
						}
						base.Invalidate(this.date_area_rect);
					}
				}
				break;
			}
			default:
				if (keyChar == ' ')
				{
					if (this.show_check_box && this.is_checkbox_selected)
					{
						this.Checked = !this.Checked;
					}
				}
				break;
			}
			e.Handled = true;
		}

		// Token: 0x060016DB RID: 5851 RVA: 0x00054328 File Offset: 0x00052528
		private void EndDateEdit(bool invalidate)
		{
			if (this.editing_part_index == -1)
			{
				return;
			}
			DateTimePicker.PartData partData = this.part_data[this.editing_part_index];
			if (partData.date_time_part == DateTimePicker.DateTimePart.Year)
			{
				if (this.editing_number > 0 && this.editing_number < 30)
				{
					this.editing_number += 2000;
				}
				else if (this.editing_number >= 30 && this.editing_number < 100)
				{
					this.editing_number += 1900;
				}
			}
			this.SetPart(this.editing_number, partData.date_time_part);
			this.editing_part_index = (this.editing_number = -1);
			this.editing_text = null;
			if (invalidate)
			{
				base.Invalidate(this.date_area_rect);
			}
		}

		// Token: 0x060016DC RID: 5852 RVA: 0x000543F4 File Offset: 0x000525F4
		internal void SetPart(int value, DateTimePicker.DateTimePart dt_part)
		{
			this.SetPart(value, dt_part, false);
		}

		// Token: 0x060016DD RID: 5853 RVA: 0x00054400 File Offset: 0x00052600
		internal void SetPart(int value, DateTimePicker.DateTimePart dt_part, bool adjust)
		{
			switch (dt_part)
			{
			case DateTimePicker.DateTimePart.Seconds:
				if (value == -1)
				{
					value = 59;
				}
				if (value >= 0 && value <= 59)
				{
					this.Value = new DateTime(this.Value.Year, this.Value.Month, this.Value.Day, this.Value.Hour, this.Value.Minute, value, this.Value.Millisecond);
				}
				break;
			case DateTimePicker.DateTimePart.Minutes:
				if (value == -1)
				{
					value = 59;
				}
				if (value >= 0 && value <= 59)
				{
					this.Value = new DateTime(this.Value.Year, this.Value.Month, this.Value.Day, this.Value.Hour, value, this.Value.Second, this.Value.Millisecond);
				}
				break;
			case DateTimePicker.DateTimePart.AMPMHour:
				if (value == -1)
				{
					value = 23;
				}
				if (value >= 0 && value <= 23)
				{
					int hour = this.Value.Hour;
					if (hour >= 12 && hour <= 23 && value < 12)
					{
						value += 12;
					}
					this.Value = new DateTime(this.Value.Year, this.Value.Month, this.Value.Day, value, this.Value.Minute, this.Value.Second, this.Value.Millisecond);
				}
				break;
			case DateTimePicker.DateTimePart.Hour:
				if (value == -1)
				{
					value = 23;
				}
				if (value >= 0 && value <= 23)
				{
					this.Value = new DateTime(this.Value.Year, this.Value.Month, this.Value.Day, value, this.Value.Minute, this.Value.Second, this.Value.Millisecond);
				}
				break;
			case DateTimePicker.DateTimePart.Day:
			{
				int num = DateTime.DaysInMonth(this.Value.Year, this.Value.Month);
				if (value >= 1 && value <= 31 && value <= num)
				{
					this.Value = new DateTime(this.Value.Year, this.Value.Month, value, this.Value.Hour, this.Value.Minute, this.Value.Second, this.Value.Millisecond);
				}
				break;
			}
			case DateTimePicker.DateTimePart.Month:
			{
				DateTime dateTime = this.Value;
				if (adjust)
				{
					if (value == 0)
					{
						dateTime = dateTime.AddYears(-1);
						value = 12;
					}
					else if (value == 13)
					{
						dateTime = dateTime.AddYears(1);
						value = 1;
					}
				}
				if (value >= 1 && value <= 12)
				{
					int num2 = DateTime.DaysInMonth(dateTime.Year, value);
					if (dateTime.Day > num2)
					{
						this.Value = new DateTime(dateTime.Year, value, num2, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
					}
					else
					{
						this.Value = new DateTime(dateTime.Year, value, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
					}
				}
				break;
			}
			case DateTimePicker.DateTimePart.Year:
				if (value >= this.min_date.Year && value <= this.max_date.Year)
				{
					int num3 = DateTime.DaysInMonth(value, this.Value.Month);
					if (this.Value.Day > num3)
					{
						this.Value = new DateTime(value, this.Value.Month, num3, this.Value.Hour, this.Value.Minute, this.Value.Second, this.Value.Millisecond);
					}
					else
					{
						this.Value = new DateTime(value, this.Value.Month, this.Value.Day, this.Value.Hour, this.Value.Minute, this.Value.Second, this.Value.Millisecond);
					}
				}
				break;
			}
		}

		// Token: 0x060016DE RID: 5854 RVA: 0x00054910 File Offset: 0x00052B10
		private void GotFocusHandler(object sender, EventArgs e)
		{
			if (this.ShowCheckBox)
			{
				this.is_checkbox_selected = true;
				base.Invalidate(this.CheckBoxRect);
				this.OnUIASelectionChanged();
			}
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x00054944 File Offset: 0x00052B44
		private void LostFocusHandler(object sender, EventArgs e)
		{
			int selectedPartIndex = this.GetSelectedPartIndex();
			if (selectedPartIndex != -1)
			{
				this.part_data[selectedPartIndex].Selected = false;
				Rectangle rectangle = Rectangle.Ceiling(this.part_data[selectedPartIndex].drawing_rectangle);
				rectangle.Inflate(2, 2);
				base.Invalidate(rectangle);
				this.OnUIASelectionChanged();
			}
			else if (this.is_checkbox_selected)
			{
				this.is_checkbox_selected = false;
				base.Invalidate(this.CheckBoxRect);
				this.OnUIASelectionChanged();
			}
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x000549C0 File Offset: 0x00052BC0
		private void MonthCalendarLostFocusHandler(object sender, EventArgs e)
		{
			if (!this.is_drop_down_visible || !this.month_calendar.Focused)
			{
			}
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x000549E0 File Offset: 0x00052BE0
		private void MonthCalendarDateChangedHandler(object sender, DateRangeEventArgs e)
		{
			if (this.month_calendar.Visible)
			{
				this.Value = e.Start.Date.Add(this.Value.TimeOfDay);
			}
		}

		// Token: 0x060016E2 RID: 5858 RVA: 0x00054A28 File Offset: 0x00052C28
		private void MonthCalendarDateSelectedHandler(object sender, DateRangeEventArgs e)
		{
			this.HideMonthCalendar();
		}

		// Token: 0x060016E3 RID: 5859 RVA: 0x00054A30 File Offset: 0x00052C30
		private void MouseUpHandler(object sender, MouseEventArgs e)
		{
			if (this.ShowUpDown && (this.is_up_pressed || this.is_down_pressed))
			{
				this.updown_timer.Enabled = false;
				this.is_up_pressed = false;
				this.is_down_pressed = false;
				base.Invalidate(this.drop_down_arrow_rect);
			}
		}

		// Token: 0x060016E4 RID: 5860 RVA: 0x00054A84 File Offset: 0x00052C84
		private void MouseDownHandler(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
			{
				return;
			}
			if (this.ShowCheckBox && this.CheckBoxRect.Contains(e.X, e.Y))
			{
				this.is_checkbox_selected = true;
				this.Checked = !this.Checked;
				this.OnUIASelectionChanged();
				return;
			}
			if (this.Checked)
			{
				this.is_checkbox_selected = false;
				this.OnUIASelectionChanged();
			}
			if (this.ShowUpDown && this.drop_down_arrow_rect.Contains(e.X, e.Y))
			{
				if (!this.ShowCheckBox || this.Checked)
				{
					if (e.Y < base.Height / 2)
					{
						this.is_up_pressed = true;
						this.is_down_pressed = false;
						this.IncrementSelectedPart(1);
					}
					else
					{
						this.is_up_pressed = false;
						this.is_down_pressed = true;
						this.IncrementSelectedPart(-1);
					}
					base.Invalidate(this.drop_down_arrow_rect);
					this.updown_timer.Interval = 500;
					this.updown_timer.Enabled = true;
				}
			}
			else if (!this.is_drop_down_visible && this.drop_down_arrow_rect.Contains(e.X, e.Y))
			{
				this.DropDownButtonClicked();
			}
			else
			{
				if (this.is_drop_down_visible)
				{
					this.HideMonthCalendar();
				}
				if (!this.ShowCheckBox || this.Checked)
				{
					bool flag = false;
					for (int i = 0; i < this.part_data.Length; i++)
					{
						bool selected = this.part_data[i].Selected;
						if (!this.part_data[i].is_literal)
						{
							if (this.part_data[i].drawing_rectangle.Contains((float)e.X, (float)e.Y))
							{
								this.part_data[i].Selected = true;
							}
							else
							{
								this.part_data[i].Selected = false;
							}
							if (selected != this.part_data[i].Selected)
							{
								flag = true;
							}
						}
					}
					if (flag)
					{
						base.Invalidate();
						this.OnUIASelectionChanged();
					}
				}
			}
		}

		// Token: 0x060016E5 RID: 5861 RVA: 0x00054CBC File Offset: 0x00052EBC
		internal void DropDownButtonClicked()
		{
			if (!this.is_drop_down_visible)
			{
				this.is_drop_down_visible = true;
				if (!this.Checked)
				{
					this.Checked = true;
				}
				base.Invalidate(this.drop_down_arrow_rect);
				this.DropDownMonthCalendar();
			}
			else
			{
				this.HideMonthCalendar();
			}
		}

		// Token: 0x060016E6 RID: 5862 RVA: 0x00054D0C File Offset: 0x00052F0C
		private void PaintHandler(object sender, PaintEventArgs pe)
		{
			if (base.Width <= 0 || base.Height <= 0 || !base.Visible)
			{
				return;
			}
			this.Draw(pe.ClipRectangle, pe.Graphics);
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x00054D50 File Offset: 0x00052F50
		private void OnMouseEnter(object sender, EventArgs e)
		{
			if (ThemeEngine.Current.DateTimePickerBorderHasHotElementStyle)
			{
				base.Invalidate();
			}
		}

		// Token: 0x060016E8 RID: 5864 RVA: 0x00054D68 File Offset: 0x00052F68
		private void OnMouseLeave(object sender, EventArgs e)
		{
			this.drop_down_button_entered = false;
			if (ThemeEngine.Current.DateTimePickerBorderHasHotElementStyle)
			{
				base.Invalidate();
			}
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00054D88 File Offset: 0x00052F88
		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (!this.is_drop_down_visible && ThemeEngine.Current.DateTimePickerDropDownButtonHasHotElementStyle && this.drop_down_button_entered != this.drop_down_arrow_rect.Contains(e.Location))
			{
				this.drop_down_button_entered = !this.drop_down_button_entered;
				base.Invalidate(this.drop_down_arrow_rect);
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060016EA RID: 5866 RVA: 0x00054DEC File Offset: 0x00052FEC
		internal bool UIAIsCheckBoxSelected
		{
			get
			{
				return this.is_checkbox_selected;
			}
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x00054DF4 File Offset: 0x00052FF4
		internal void OnUIAMinimumChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[DateTimePicker.UIAMinimumChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x00054E2C File Offset: 0x0005302C
		internal void OnUIAMaximumChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[DateTimePicker.UIAMaximumChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00054E64 File Offset: 0x00053064
		internal void OnUIASelectionChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[DateTimePicker.UIASelectionChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x00054E9C File Offset: 0x0005309C
		internal void OnUIAChecked()
		{
			EventHandler eventHandler = (EventHandler)base.Events[DateTimePicker.UIACheckedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x00054ED4 File Offset: 0x000530D4
		internal void OnUIAShowCheckBoxChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[DateTimePicker.UIAShowCheckBoxChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x00054F0C File Offset: 0x0005310C
		internal void OnUIAShowUpDownChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[DateTimePicker.UIAShowUpDownChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x04000C4A RID: 3146
		internal const int check_box_size = 13;

		// Token: 0x04000C4B RID: 3147
		internal const int check_box_space = 4;

		// Token: 0x04000C4C RID: 3148
		internal const int up_down_width = 13;

		// Token: 0x04000C4D RID: 3149
		internal const int initial_timer_delay = 500;

		// Token: 0x04000C4E RID: 3150
		internal const int subsequent_timer_delay = 100;

		/// <summary>Specifies the maximum date value of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000C4F RID: 3151
		[Browsable(false)]
		[EditorBrowsable(1)]
		public static readonly DateTime MaxDateTime = new DateTime(9998, 12, 31, 0, 0, 0);

		/// <summary>Gets the minimum date value of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000C50 RID: 3152
		[EditorBrowsable(1)]
		[Browsable(false)]
		public static readonly DateTime MinDateTime = new DateTime(1753, 1, 1);

		/// <summary>Specifies the default month background color of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control. This field is read-only.</summary>
		// Token: 0x04000C51 RID: 3153
		protected static readonly Color DefaultMonthBackColor = ThemeEngine.Current.ColorWindow;

		/// <summary>Specifies the default title back color of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control. This field is read-only.</summary>
		// Token: 0x04000C52 RID: 3154
		protected static readonly Color DefaultTitleBackColor = ThemeEngine.Current.ColorActiveCaption;

		/// <summary>Specifies the default title foreground color of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control. This field is read-only.</summary>
		// Token: 0x04000C53 RID: 3155
		protected static readonly Color DefaultTitleForeColor = ThemeEngine.Current.ColorActiveCaptionText;

		/// <summary>Specifies the default trailing foreground color of the <see cref="T:System.Windows.Forms.DateTimePicker" /> control. This field is read-only.</summary>
		// Token: 0x04000C54 RID: 3156
		protected static readonly Color DefaultTrailingForeColor = SystemColors.GrayText;

		// Token: 0x04000C55 RID: 3157
		internal MonthCalendar month_calendar;

		// Token: 0x04000C56 RID: 3158
		private bool is_checked;

		// Token: 0x04000C57 RID: 3159
		private string custom_format;

		// Token: 0x04000C58 RID: 3160
		private LeftRightAlignment drop_down_align;

		// Token: 0x04000C59 RID: 3161
		private DateTimePickerFormat format;

		// Token: 0x04000C5A RID: 3162
		private DateTime max_date;

		// Token: 0x04000C5B RID: 3163
		private DateTime min_date;

		// Token: 0x04000C5C RID: 3164
		private bool show_check_box;

		// Token: 0x04000C5D RID: 3165
		private bool show_up_down;

		// Token: 0x04000C5E RID: 3166
		private DateTime date_value;

		// Token: 0x04000C5F RID: 3167
		private bool right_to_left_layout;

		// Token: 0x04000C60 RID: 3168
		internal bool is_drop_down_visible;

		// Token: 0x04000C61 RID: 3169
		internal bool is_up_pressed;

		// Token: 0x04000C62 RID: 3170
		internal bool is_down_pressed;

		// Token: 0x04000C63 RID: 3171
		internal Timer updown_timer;

		// Token: 0x04000C64 RID: 3172
		internal bool is_checkbox_selected;

		// Token: 0x04000C65 RID: 3173
		internal DateTimePicker.PartData[] part_data;

		// Token: 0x04000C66 RID: 3174
		internal int editing_part_index = -1;

		// Token: 0x04000C67 RID: 3175
		internal int editing_number = -1;

		// Token: 0x04000C68 RID: 3176
		internal string editing_text;

		// Token: 0x04000C69 RID: 3177
		private bool drop_down_button_entered;

		/// <summary>Provides information about the <see cref="T:System.Windows.Forms.DateTimePicker" /> control to accessibility client applications.</summary>
		// Token: 0x02000144 RID: 324
		[ComVisible(true)]
		public class DateTimePickerAccessibleObject : Control.ControlAccessibleObject
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DateTimePicker.DateTimePickerAccessibleObject" /> class. </summary>
			/// <param name="owner">The <see cref="T:System.Windows.Forms.DateTimePicker" /> that owns the <see cref="T:System.Windows.Forms.DateTimePicker.DateTimePickerAccessibleObject" />.</param>
			// Token: 0x060016F1 RID: 5873 RVA: 0x00054F44 File Offset: 0x00053144
			public DateTimePickerAccessibleObject(DateTimePicker owner)
				: base(owner)
			{
				this.owner = owner;
			}

			/// <summary>Gets the shortcut key or access key for the accessible object. </summary>
			/// <returns>The shortcut key or access key for the accessible object.</returns>
			// Token: 0x17000564 RID: 1380
			// (get) Token: 0x060016F2 RID: 5874 RVA: 0x00054F54 File Offset: 0x00053154
			public override string KeyboardShortcut
			{
				get
				{
					return base.KeyboardShortcut;
				}
			}

			/// <summary>Gets the role of this accessible object.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleRole" /> values indicating the role of the <see cref="T:System.Windows.Forms.DateTimePicker.DateTimePickerAccessibleObject" />.</returns>
			// Token: 0x17000565 RID: 1381
			// (get) Token: 0x060016F3 RID: 5875 RVA: 0x00054F5C File Offset: 0x0005315C
			public override AccessibleRole Role
			{
				get
				{
					return base.Role;
				}
			}

			/// <summary>Gets the state of the accessible object.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.AccessibleStates" /> values indicating the state of the <see cref="T:System.Windows.Forms.DateTimePicker.DateTimePickerAccessibleObject" />. </returns>
			// Token: 0x17000566 RID: 1382
			// (get) Token: 0x060016F4 RID: 5876 RVA: 0x00054F64 File Offset: 0x00053164
			public override AccessibleStates State
			{
				get
				{
					AccessibleStates accessibleStates = AccessibleStates.Default;
					if (this.owner.Checked)
					{
						accessibleStates |= AccessibleStates.Checked;
					}
					return accessibleStates;
				}
			}

			/// <summary>Gets the value of an accessible object.</summary>
			// Token: 0x17000567 RID: 1383
			// (get) Token: 0x060016F5 RID: 5877 RVA: 0x00054F90 File Offset: 0x00053190
			public override string Value
			{
				get
				{
					return this.owner.Text;
				}
			}

			// Token: 0x04000C76 RID: 3190
			private new DateTimePicker owner;
		}

		// Token: 0x02000145 RID: 325
		internal enum DateTimePart
		{
			// Token: 0x04000C78 RID: 3192
			Seconds,
			// Token: 0x04000C79 RID: 3193
			Minutes,
			// Token: 0x04000C7A RID: 3194
			AMPMHour,
			// Token: 0x04000C7B RID: 3195
			Hour,
			// Token: 0x04000C7C RID: 3196
			Day,
			// Token: 0x04000C7D RID: 3197
			DayName,
			// Token: 0x04000C7E RID: 3198
			Month,
			// Token: 0x04000C7F RID: 3199
			Year,
			// Token: 0x04000C80 RID: 3200
			AMPMSpecifier,
			// Token: 0x04000C81 RID: 3201
			Literal
		}

		// Token: 0x02000146 RID: 326
		internal class PartData
		{
			// Token: 0x060016F6 RID: 5878 RVA: 0x00054FA0 File Offset: 0x000531A0
			internal PartData(string value, bool is_literal, DateTimePicker owner)
			{
				this.value = value;
				this.is_literal = is_literal;
				this.owner = owner;
				this.date_time_part = DateTimePicker.PartData.GetDateTimePart(value);
			}

			// Token: 0x17000568 RID: 1384
			// (get) Token: 0x060016F7 RID: 5879 RVA: 0x00054FCC File Offset: 0x000531CC
			internal bool is_numeric_format
			{
				get
				{
					if (this.is_literal)
					{
						return false;
					}
					string text = this.value;
					if (text != null)
					{
						if (DateTimePicker.PartData.<>f__switch$map2 == null)
						{
							Dictionary<string, int> dictionary = new Dictionary<string, int>(17);
							dictionary.Add("m", 0);
							dictionary.Add("mm", 0);
							dictionary.Add("d", 0);
							dictionary.Add("dd", 0);
							dictionary.Add("h", 0);
							dictionary.Add("hh", 0);
							dictionary.Add("H", 0);
							dictionary.Add("HH", 0);
							dictionary.Add("M", 0);
							dictionary.Add("MM", 0);
							dictionary.Add("s", 0);
							dictionary.Add("ss", 0);
							dictionary.Add("y", 0);
							dictionary.Add("yy", 0);
							dictionary.Add("yyyy", 0);
							dictionary.Add("ddd", 1);
							dictionary.Add("dddd", 1);
							DateTimePicker.PartData.<>f__switch$map2 = dictionary;
						}
						int num;
						if (DateTimePicker.PartData.<>f__switch$map2.TryGetValue(text, ref num))
						{
							if (num == 0)
							{
								return true;
							}
							if (num == 1)
							{
								return false;
							}
						}
					}
					return false;
				}
			}

			// Token: 0x17000569 RID: 1385
			// (get) Token: 0x060016F8 RID: 5880 RVA: 0x00055100 File Offset: 0x00053300
			// (set) Token: 0x060016F9 RID: 5881 RVA: 0x00055108 File Offset: 0x00053308
			internal bool Selected
			{
				get
				{
					return this.is_selected;
				}
				set
				{
					if (value == this.is_selected)
					{
						return;
					}
					this.owner.EndDateEdit(false);
					this.is_selected = value;
				}
			}

			// Token: 0x060016FA RID: 5882 RVA: 0x00055138 File Offset: 0x00053338
			internal string GetText(DateTime date)
			{
				if (this.is_literal)
				{
					return this.value;
				}
				return DateTimePicker.PartData.GetText(date, this.value);
			}

			// Token: 0x060016FB RID: 5883 RVA: 0x00055158 File Offset: 0x00053358
			private static DateTimePicker.DateTimePart GetDateTimePart(string value)
			{
				if (value != null)
				{
					if (DateTimePicker.PartData.<>f__switch$map3 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(21);
						dictionary.Add("s", 0);
						dictionary.Add("ss", 0);
						dictionary.Add("m", 1);
						dictionary.Add("mm", 1);
						dictionary.Add("h", 2);
						dictionary.Add("hh", 2);
						dictionary.Add("H", 3);
						dictionary.Add("HH", 3);
						dictionary.Add("d", 4);
						dictionary.Add("dd", 4);
						dictionary.Add("ddd", 5);
						dictionary.Add("dddd", 5);
						dictionary.Add("M", 6);
						dictionary.Add("MM", 6);
						dictionary.Add("MMMM", 6);
						dictionary.Add("y", 7);
						dictionary.Add("yy", 7);
						dictionary.Add("yyy", 7);
						dictionary.Add("yyyy", 7);
						dictionary.Add("t", 8);
						dictionary.Add("tt", 8);
						DateTimePicker.PartData.<>f__switch$map3 = dictionary;
					}
					int num;
					if (DateTimePicker.PartData.<>f__switch$map3.TryGetValue(value, ref num))
					{
						switch (num)
						{
						case 0:
							return DateTimePicker.DateTimePart.Seconds;
						case 1:
							return DateTimePicker.DateTimePart.Minutes;
						case 2:
							return DateTimePicker.DateTimePart.AMPMHour;
						case 3:
							return DateTimePicker.DateTimePart.Hour;
						case 4:
							return DateTimePicker.DateTimePart.Day;
						case 5:
							return DateTimePicker.DateTimePart.DayName;
						case 6:
							return DateTimePicker.DateTimePart.Month;
						case 7:
							return DateTimePicker.DateTimePart.Year;
						case 8:
							return DateTimePicker.DateTimePart.AMPMSpecifier;
						}
					}
				}
				return DateTimePicker.DateTimePart.Literal;
			}

			// Token: 0x060016FC RID: 5884 RVA: 0x000552D8 File Offset: 0x000534D8
			internal static string GetText(DateTime date, string format)
			{
				if (format.StartsWith("g"))
				{
					return " ";
				}
				if (format.Length == 1)
				{
					return date.ToString("%" + format);
				}
				if (format == "yyyyy" || format == "yyyyyy" || format == "yyyyyyy" || format == "yyyyyyyy")
				{
					return date.ToString("yyyy");
				}
				if (format.Length > 1)
				{
					return date.ToString(format);
				}
				return string.Empty;
			}

			// Token: 0x04000C82 RID: 3202
			internal string value;

			// Token: 0x04000C83 RID: 3203
			internal bool is_literal;

			// Token: 0x04000C84 RID: 3204
			private bool is_selected;

			// Token: 0x04000C85 RID: 3205
			internal RectangleF drawing_rectangle;

			// Token: 0x04000C86 RID: 3206
			internal DateTimePicker.DateTimePart date_time_part;

			// Token: 0x04000C87 RID: 3207
			private DateTimePicker owner;
		}
	}
}
