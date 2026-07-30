using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Windows.Forms
{
	/// <summary>Represents a Windows control that enables the user to select a date using a visual monthly calendar display.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200026B RID: 619
	[ComVisible(true)]
	[ClassInterface(1)]
	[DefaultBindingProperty("SelectionRange")]
	[DefaultProperty("SelectionRange")]
	[Designer("System.Windows.Forms.Design.MonthCalendarDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultEvent("DateChanged")]
	public class MonthCalendar : Control
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.MonthCalendar" /> class.</summary>
		// Token: 0x060027F3 RID: 10227 RVA: 0x0009973C File Offset: 0x0009793C
		public MonthCalendar()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick, false);
			this.timer = new Timer();
			this.timer.Interval = 500;
			this.timer.Enabled = false;
			DateTime date = DateTime.Now.Date;
			this.selection_range = new SelectionRange(date, date);
			this.today_date = date;
			this.current_month = new DateTime(date.Year, date.Month, 1);
			this.annually_bolded_dates = null;
			this.bolded_dates = null;
			this.calendar_dimensions = new Size(1, 1);
			this.first_day_of_week = Day.Default;
			this.max_date = new DateTime(9998, 12, 31);
			this.max_selection_count = 7;
			this.min_date = new DateTime(1753, 1, 1);
			this.monthly_bolded_dates = null;
			this.scroll_change = 0;
			this.show_today = true;
			this.show_today_circle = true;
			this.show_week_numbers = false;
			this.title_back_color = ThemeEngine.Current.ColorActiveCaption;
			this.title_fore_color = ThemeEngine.Current.ColorActiveCaptionText;
			this.today_date_set = false;
			this.trailing_fore_color = SystemColors.GrayText;
			this.bold_font = new Font(this.Font, this.Font.Style | 1);
			this.centered_format = new StringFormat(StringFormat.GenericTypographic);
			this.centered_format.FormatFlags = this.centered_format.FormatFlags | 2048 | 4096 | 4;
			this.centered_format.FormatFlags &= -16385;
			this.centered_format.LineAlignment = 1;
			this.centered_format.Alignment = 1;
			this.ForeColor = SystemColors.WindowText;
			this.BackColor = ThemeEngine.Current.ColorWindow;
			this.button_x_offset = 5;
			this.button_size = new Size(22, 17);
			this.date_cell_size = new Size(24, 16);
			this.divider_line_offset = 4;
			this.calendar_spacing = new Size(4, 5);
			this.clicked_date = date;
			this.is_date_clicked = false;
			this.is_previous_clicked = false;
			this.is_next_clicked = false;
			this.is_shift_pressed = false;
			this.click_state = new bool[3];
			this.first_select_start_date = date;
			this.month_title_click_location = Point.Empty;
			this.SetUpTodayMenu();
			this.SetUpMonthMenu();
			this.timer.Tick += new EventHandler(this.TimerHandler);
			base.MouseMove += this.MouseMoveHandler;
			base.MouseDown += this.MouseDownHandler;
			base.KeyDown += this.KeyDownHandler;
			base.MouseUp += this.MouseUpHandler;
			base.KeyUp += this.KeyUpHandler;
			base.Paint += this.PaintHandler;
			this.Size = this.DefaultSize;
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x00099A14 File Offset: 0x00097C14
		internal MonthCalendar(DateTimePicker owner)
			: this()
		{
			this.owner = owner;
			this.is_visible = false;
			this.Size = this.DefaultSize;
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x00099A44 File Offset: 0x00097C44
		// Note: this type is marked as 'beforefieldinit'.
		static MonthCalendar()
		{
			MonthCalendar.DateChangedEvent = new object();
			MonthCalendar.DateSelectedEvent = new object();
			MonthCalendar.RightToLeftLayoutChangedEvent = new object();
			MonthCalendar.UIAMaxSelectionCountChangedEvent = new object();
			MonthCalendar.UIASelectionChangedEvent = new object();
		}

		/// <summary>Occurs when the date selected in the <see cref="T:System.Windows.Forms.MonthCalendar" /> changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400024B RID: 587
		// (add) Token: 0x060027F6 RID: 10230 RVA: 0x00099A84 File Offset: 0x00097C84
		// (remove) Token: 0x060027F7 RID: 10231 RVA: 0x00099A98 File Offset: 0x00097C98
		public event DateRangeEventHandler DateChanged
		{
			add
			{
				base.Events.AddHandler(MonthCalendar.DateChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MonthCalendar.DateChangedEvent, value);
			}
		}

		/// <summary>Occurs when the user makes an explicit date selection using the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400024C RID: 588
		// (add) Token: 0x060027F8 RID: 10232 RVA: 0x00099AAC File Offset: 0x00097CAC
		// (remove) Token: 0x060027F9 RID: 10233 RVA: 0x00099AC0 File Offset: 0x00097CC0
		public event DateRangeEventHandler DateSelected
		{
			add
			{
				base.Events.AddHandler(MonthCalendar.DateSelectedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MonthCalendar.DateSelectedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.MonthCalendar.BackgroundImage" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400024D RID: 589
		// (add) Token: 0x060027FA RID: 10234 RVA: 0x00099AD4 File Offset: 0x00097CD4
		// (remove) Token: 0x060027FB RID: 10235 RVA: 0x00099AE0 File Offset: 0x00097CE0
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.MonthCalendar.BackgroundImageLayout" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400024E RID: 590
		// (add) Token: 0x060027FC RID: 10236 RVA: 0x00099AEC File Offset: 0x00097CEC
		// (remove) Token: 0x060027FD RID: 10237 RVA: 0x00099AF8 File Offset: 0x00097CF8
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
				base.BackgroundImageLayoutChanged += value;
			}
		}

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400024F RID: 591
		// (add) Token: 0x060027FE RID: 10238 RVA: 0x00099B04 File Offset: 0x00097D04
		// (remove) Token: 0x060027FF RID: 10239 RVA: 0x00099B10 File Offset: 0x00097D10
		[EditorBrowsable(1)]
		[Browsable(false)]
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

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000250 RID: 592
		// (add) Token: 0x06002800 RID: 10240 RVA: 0x00099B1C File Offset: 0x00097D1C
		// (remove) Token: 0x06002801 RID: 10241 RVA: 0x00099B28 File Offset: 0x00097D28
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

		/// <summary>Occurs when the <see cref="P:System.Windows.Forms.MonthCalendar.ImeMode" /> property has changed.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000251 RID: 593
		// (add) Token: 0x06002802 RID: 10242 RVA: 0x00099B34 File Offset: 0x00097D34
		// (remove) Token: 0x06002803 RID: 10243 RVA: 0x00099B40 File Offset: 0x00097D40
		[Browsable(false)]
		[EditorBrowsable(1)]
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

		/// <summary>Occurs when the user clicks the <see cref="T:System.Windows.Forms.MonthCalendar" /> control with the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000252 RID: 594
		// (add) Token: 0x06002804 RID: 10244 RVA: 0x00099B4C File Offset: 0x00097D4C
		// (remove) Token: 0x06002805 RID: 10245 RVA: 0x00099B58 File Offset: 0x00097D58
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

		/// <summary>Occurs when the user double-clicks the <see cref="T:System.Windows.Forms.MonthCalendar" /> control with the mouse.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000253 RID: 595
		// (add) Token: 0x06002806 RID: 10246 RVA: 0x00099B64 File Offset: 0x00097D64
		// (remove) Token: 0x06002807 RID: 10247 RVA: 0x00099B70 File Offset: 0x00097D70
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

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.MonthCalendar.Padding" /> property changes.</summary>
		// Token: 0x14000254 RID: 596
		// (add) Token: 0x06002808 RID: 10248 RVA: 0x00099B7C File Offset: 0x00097D7C
		// (remove) Token: 0x06002809 RID: 10249 RVA: 0x00099B88 File Offset: 0x00097D88
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

		/// <summary>Occurs when the control is redrawn.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000255 RID: 597
		// (add) Token: 0x0600280A RID: 10250 RVA: 0x00099B94 File Offset: 0x00097D94
		// (remove) Token: 0x0600280B RID: 10251 RVA: 0x00099BB0 File Offset: 0x00097DB0
		[Browsable(false)]
		[EditorBrowsable(1)]
		public new event PaintEventHandler Paint;

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.MonthCalendar.RightToLeftLayout" /> property changes.</summary>
		// Token: 0x14000256 RID: 598
		// (add) Token: 0x0600280C RID: 10252 RVA: 0x00099BCC File Offset: 0x00097DCC
		// (remove) Token: 0x0600280D RID: 10253 RVA: 0x00099BE0 File Offset: 0x00097DE0
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.Events.AddHandler(MonthCalendar.RightToLeftLayoutChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MonthCalendar.RightToLeftLayoutChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Windows.Forms.MonthCalendar.Text" /> property changes.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x14000257 RID: 599
		// (add) Token: 0x0600280E RID: 10254 RVA: 0x00099BF4 File Offset: 0x00097DF4
		// (remove) Token: 0x0600280F RID: 10255 RVA: 0x00099C00 File Offset: 0x00097E00
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

		// Token: 0x14000258 RID: 600
		// (add) Token: 0x06002810 RID: 10256 RVA: 0x00099C0C File Offset: 0x00097E0C
		// (remove) Token: 0x06002811 RID: 10257 RVA: 0x00099C20 File Offset: 0x00097E20
		internal event EventHandler UIAMaxSelectionCountChanged
		{
			add
			{
				base.Events.AddHandler(MonthCalendar.UIAMaxSelectionCountChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MonthCalendar.UIAMaxSelectionCountChangedEvent, value);
			}
		}

		// Token: 0x14000259 RID: 601
		// (add) Token: 0x06002812 RID: 10258 RVA: 0x00099C34 File Offset: 0x00097E34
		// (remove) Token: 0x06002813 RID: 10259 RVA: 0x00099C48 File Offset: 0x00097E48
		internal event EventHandler UIASelectionChanged
		{
			add
			{
				base.Events.AddHandler(MonthCalendar.UIASelectionChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(MonthCalendar.UIASelectionChangedEvent, value);
			}
		}

		/// <summary>Gets or sets the array of <see cref="T:System.DateTime" /> objects that determines which annual days are displayed in bold.</summary>
		/// <returns>An array of <see cref="T:System.DateTime" /> objects.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06002815 RID: 10261 RVA: 0x00099C98 File Offset: 0x00097E98
		// (set) Token: 0x06002814 RID: 10260 RVA: 0x00099C5C File Offset: 0x00097E5C
		[Localizable(true)]
		public DateTime[] AnnuallyBoldedDates
		{
			get
			{
				if (this.annually_bolded_dates == null || this.annually_bolded_dates.Count == 0)
				{
					return new DateTime[0];
				}
				DateTime[] array = new DateTime[this.annually_bolded_dates.Count];
				this.annually_bolded_dates.CopyTo(array);
				return array;
			}
			set
			{
				if (this.annually_bolded_dates == null)
				{
					this.annually_bolded_dates = new ArrayList(value);
				}
				else
				{
					this.annually_bolded_dates.Clear();
					this.annually_bolded_dates.AddRange(value);
				}
				this.UpdateBoldedDates();
			}
		}

		/// <summary>Gets or sets the background image for the <see cref="T:System.Windows.Forms.MonthCalendar" /></summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> that is the background image for the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06002816 RID: 10262 RVA: 0x00099CE8 File Offset: 0x00097EE8
		// (set) Token: 0x06002817 RID: 10263 RVA: 0x00099CF0 File Offset: 0x00097EF0
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

		/// <summary>Gets or sets a value indicating the layout for the <see cref="P:System.Windows.Forms.MonthCalendar.BackgroundImage" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.ImageLayout" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06002818 RID: 10264 RVA: 0x00099CFC File Offset: 0x00097EFC
		// (set) Token: 0x06002819 RID: 10265 RVA: 0x00099D04 File Offset: 0x00097F04
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

		/// <summary>Gets or sets the background color for the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the background color of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultBackColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x0600281B RID: 10267 RVA: 0x00099D1C File Offset: 0x00097F1C
		// (set) Token: 0x0600281A RID: 10266 RVA: 0x00099D10 File Offset: 0x00097F10
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

		/// <summary>Gets or sets the array of <see cref="T:System.DateTime" /> objects that determines which nonrecurring dates are displayed in bold.</summary>
		/// <returns>The array of bold dates.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x0600281D RID: 10269 RVA: 0x00099D60 File Offset: 0x00097F60
		// (set) Token: 0x0600281C RID: 10268 RVA: 0x00099D24 File Offset: 0x00097F24
		[Localizable(true)]
		public DateTime[] BoldedDates
		{
			get
			{
				if (this.bolded_dates == null || this.bolded_dates.Count == 0)
				{
					return new DateTime[0];
				}
				DateTime[] array = new DateTime[this.bolded_dates.Count];
				this.bolded_dates.CopyTo(array);
				return array;
			}
			set
			{
				if (this.bolded_dates == null)
				{
					this.bolded_dates = new ArrayList(value);
				}
				else
				{
					this.bolded_dates.Clear();
					this.bolded_dates.AddRange(value);
				}
				this.UpdateBoldedDates();
			}
		}

		/// <summary>Gets or sets the number of columns and rows of months displayed.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> with the number of columns and rows to use to display the calendar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x0600281F RID: 10271 RVA: 0x00099EDC File Offset: 0x000980DC
		// (set) Token: 0x0600281E RID: 10270 RVA: 0x00099DB0 File Offset: 0x00097FB0
		[Localizable(true)]
		public Size CalendarDimensions
		{
			get
			{
				return this.calendar_dimensions;
			}
			set
			{
				if (value.Width < 0 || value.Height < 0)
				{
					throw new ArgumentException();
				}
				if (this.calendar_dimensions != value)
				{
					if (value.Width * value.Height > 12)
					{
						if (value.Width > 12 && value.Height > 12)
						{
							this.calendar_dimensions = new Size(4, 3);
						}
						else if (value.Width > 12)
						{
							for (int i = 12; i > 0; i--)
							{
								if (i * value.Height <= 12)
								{
									this.calendar_dimensions = new Size(i, value.Height);
									break;
								}
							}
						}
						else if (value.Height > 12)
						{
							for (int j = 12; j > 0; j--)
							{
								if (j * value.Width <= 12)
								{
									this.calendar_dimensions = new Size(value.Width, j);
									break;
								}
							}
						}
					}
					else
					{
						this.calendar_dimensions = value;
					}
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the control should redraw its surface using a secondary buffer.</summary>
		/// <returns>true if the control should use a secondary buffer to redraw; otherwise, false. The default is false.</returns>
		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06002820 RID: 10272 RVA: 0x00099EE4 File Offset: 0x000980E4
		// (set) Token: 0x06002821 RID: 10273 RVA: 0x00099EEC File Offset: 0x000980EC
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

		/// <summary>Gets or sets the first day of the week as displayed in the month calendar.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.Day" /> values. The default is <see cref="F:System.Windows.Forms.Day.Default" />.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The assigned value is not one of the <see cref="T:System.Windows.Forms.Day" /> enumeration members. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06002823 RID: 10275 RVA: 0x00099F14 File Offset: 0x00098114
		// (set) Token: 0x06002822 RID: 10274 RVA: 0x00099EF8 File Offset: 0x000980F8
		[Localizable(true)]
		[DefaultValue(Day.Default)]
		public Day FirstDayOfWeek
		{
			get
			{
				return this.first_day_of_week;
			}
			set
			{
				if (this.first_day_of_week != value)
				{
					this.first_day_of_week = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the foreground color of the control.</summary>
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control. The default is the value of the <see cref="P:System.Windows.Forms.Control.DefaultForeColor" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06002825 RID: 10277 RVA: 0x00099F28 File Offset: 0x00098128
		// (set) Token: 0x06002824 RID: 10276 RVA: 0x00099F1C File Offset: 0x0009811C
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
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06002826 RID: 10278 RVA: 0x00099F30 File Offset: 0x00098130
		// (set) Token: 0x06002827 RID: 10279 RVA: 0x00099F38 File Offset: 0x00098138
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

		/// <summary>Gets or sets the maximum allowable date.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> representing the maximum allowable date. The default is 12/31/9998.</returns>
		/// <exception cref="T:System.ArgumentException">The value is less than the <see cref="P:System.Windows.Forms.MonthCalendar.MinDate" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009D4 RID: 2516
		// (get) Token: 0x06002829 RID: 10281 RVA: 0x0009A058 File Offset: 0x00098258
		// (set) Token: 0x06002828 RID: 10280 RVA: 0x00099F44 File Offset: 0x00098144
		public DateTime MaxDate
		{
			get
			{
				return this.max_date;
			}
			set
			{
				if (value < this.MinDate)
				{
					string text = string.Format(CultureInfo.CurrentCulture, "Value of '{0}' is not valid for 'MaxDate'. 'MaxDate' must be greater than or equal to MinDate.", new object[] { value.ToString("d", CultureInfo.CurrentCulture) });
					throw new ArgumentOutOfRangeException("MaxDate", text);
				}
				if (this.max_date == value)
				{
					return;
				}
				this.max_date = value;
				if (this.max_date < this.selection_range.Start || this.max_date < this.selection_range.End)
				{
					DateTime dateTime = ((!(this.max_date < this.selection_range.Start)) ? this.selection_range.Start : this.max_date);
					DateTime dateTime2 = ((!(this.max_date < this.selection_range.End)) ? this.selection_range.End : this.max_date);
					this.SelectionRange = new SelectionRange(dateTime, dateTime2);
				}
			}
		}

		/// <summary>Gets or sets the maximum number of days that can be selected in a month calendar control.</summary>
		/// <returns>The maximum number of days that you can select. The default is 7.</returns>
		/// <exception cref="T:System.ArgumentException">The value is less than 1.-or- The <see cref="P:System.Windows.Forms.MonthCalendar.MaxSelectionCount" /> cannot be set. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x0600282B RID: 10283 RVA: 0x0009A0E8 File Offset: 0x000982E8
		// (set) Token: 0x0600282A RID: 10282 RVA: 0x0009A060 File Offset: 0x00098260
		[DefaultValue(7)]
		public int MaxSelectionCount
		{
			get
			{
				return this.max_selection_count;
			}
			set
			{
				if (value < 1)
				{
					string text = string.Format(CultureInfo.CurrentCulture, "Value of '{0}' is not valid for 'MaxSelectionCount'. 'MaxSelectionCount' must be greater than or equal to {1}.", new object[] { value, 1 });
					throw new ArgumentOutOfRangeException("MaxSelectionCount", text);
				}
				if ((this.SelectionEnd - this.SelectionStart).Days > value)
				{
					throw new ArgumentException();
				}
				if (this.max_selection_count != value)
				{
					this.max_selection_count = value;
					this.OnUIAMaxSelectionCountChanged();
				}
			}
		}

		/// <summary>Gets or sets the minimum allowable date.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> representing the minimum allowable date. The default is 01/01/1753.</returns>
		/// <exception cref="T:System.ArgumentException">The date set is greater than the <see cref="P:System.Windows.Forms.MonthCalendar.MaxDate" />.-or-The date set is earlier than 01/01/1753. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x0600282D RID: 10285 RVA: 0x0009A26C File Offset: 0x0009846C
		// (set) Token: 0x0600282C RID: 10284 RVA: 0x0009A0F0 File Offset: 0x000982F0
		public DateTime MinDate
		{
			get
			{
				return this.min_date;
			}
			set
			{
				DateTime dateTime;
				dateTime..ctor(1753, 1, 1);
				if (value < dateTime)
				{
					string text = string.Format(CultureInfo.CurrentCulture, "Value of '{0}' is not valid for 'MinDate'. 'MinDate' must be greater than or equal to {1}.", new object[]
					{
						value.ToString("d", CultureInfo.CurrentCulture),
						dateTime.ToString("d", CultureInfo.CurrentCulture)
					});
					throw new ArgumentOutOfRangeException("MinDate", text);
				}
				if (value > this.MaxDate)
				{
					string text2 = string.Format(CultureInfo.CurrentCulture, "Value of '{0}' is not valid for 'MinDate'. 'MinDate' must be less than MaxDate.", new object[] { value.ToString("d", CultureInfo.CurrentCulture) });
					throw new ArgumentOutOfRangeException("MinDate", text2);
				}
				if (this.min_date == value)
				{
					return;
				}
				this.min_date = value;
				if (this.min_date > this.selection_range.Start || this.min_date > this.selection_range.End)
				{
					DateTime dateTime2 = ((!(this.min_date > this.selection_range.Start)) ? this.selection_range.Start : this.min_date);
					DateTime dateTime3 = ((!(this.min_date > this.selection_range.End)) ? this.selection_range.End : this.min_date);
					this.SelectionRange = new SelectionRange(dateTime2, dateTime3);
				}
			}
		}

		/// <summary>Gets or sets the array of <see cref="T:System.DateTime" /> objects that determine which monthly days to bold.</summary>
		/// <returns>An array of <see cref="T:System.DateTime" /> objects.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x0600282F RID: 10287 RVA: 0x0009A2B0 File Offset: 0x000984B0
		// (set) Token: 0x0600282E RID: 10286 RVA: 0x0009A274 File Offset: 0x00098474
		[Localizable(true)]
		public DateTime[] MonthlyBoldedDates
		{
			get
			{
				if (this.monthly_bolded_dates == null || this.monthly_bolded_dates.Count == 0)
				{
					return new DateTime[0];
				}
				DateTime[] array = new DateTime[this.monthly_bolded_dates.Count];
				this.monthly_bolded_dates.CopyTo(array);
				return array;
			}
			set
			{
				if (this.monthly_bolded_dates == null)
				{
					this.monthly_bolded_dates = new ArrayList(value);
				}
				else
				{
					this.monthly_bolded_dates.Clear();
					this.monthly_bolded_dates.AddRange(value);
				}
				this.UpdateBoldedDates();
			}
		}

		/// <summary>Gets or sets the space between the edges of a <see cref="T:System.Windows.Forms.MonthCalendar" /> control and its contents.</summary>
		/// <returns>
		///   <see cref="F:System.Windows.Forms.Padding.Empty" /> in all cases.</returns>
		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x0009A300 File Offset: 0x00098500
		// (set) Token: 0x06002831 RID: 10289 RVA: 0x0009A308 File Offset: 0x00098508
		[EditorBrowsable(1)]
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
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

		/// <summary>Gets or sets a value indicating whether the control is laid out from right to left.</summary>
		/// <returns>true if the control is laid out from right to left; otherwise, false. The default is false.</returns>
		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06002832 RID: 10290 RVA: 0x0009A314 File Offset: 0x00098514
		// (set) Token: 0x06002833 RID: 10291 RVA: 0x0009A31C File Offset: 0x0009851C
		[Localizable(true)]
		[DefaultValue(false)]
		public virtual bool RightToLeftLayout
		{
			get
			{
				return this.right_to_left_layout;
			}
			set
			{
				this.right_to_left_layout = value;
			}
		}

		/// <summary>Gets or sets the scroll rate for a month calendar control.</summary>
		/// <returns>A positive number representing the current scroll rate in number of months moved. The default is the number of months currently displayed. The maximum is 20,000.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than 0.-or- The value is greater than 20,000. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x0009A358 File Offset: 0x00098558
		// (set) Token: 0x06002834 RID: 10292 RVA: 0x0009A328 File Offset: 0x00098528
		[DefaultValue(0)]
		public int ScrollChange
		{
			get
			{
				return this.scroll_change;
			}
			set
			{
				if (value < 0 || value > 20000)
				{
					throw new ArgumentException();
				}
				if (this.scroll_change != value)
				{
					this.scroll_change = value;
				}
			}
		}

		/// <summary>Gets or sets the end date of the selected range of dates.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> indicating the last date in the selection range.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date value is less than the <see cref="P:System.Windows.Forms.MonthCalendar.MinDate" /> value.-or- The date value is greater than the <see cref="P:System.Windows.Forms.MonthCalendar.MaxDate" /> value. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06002837 RID: 10295 RVA: 0x0009A460 File Offset: 0x00098660
		// (set) Token: 0x06002836 RID: 10294 RVA: 0x0009A360 File Offset: 0x00098560
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public DateTime SelectionEnd
		{
			get
			{
				return this.SelectionRange.End;
			}
			set
			{
				if (value < this.MinDate || value > this.MaxDate)
				{
					throw new ArgumentException();
				}
				if (this.SelectionRange.End != value)
				{
					DateTime end = this.SelectionRange.End;
					if (value < this.SelectionRange.Start)
					{
						this.SelectionRange.Start = value;
					}
					if (value.AddDays((double)((this.MaxSelectionCount - 1) * -1)) > this.SelectionRange.Start)
					{
						this.SelectionRange.Start = value.AddDays((double)((this.MaxSelectionCount - 1) * -1));
					}
					this.SelectionRange.End = value;
					this.InvalidateDateRange(new SelectionRange(end, this.SelectionRange.End));
					this.OnDateChanged(new DateRangeEventArgs(this.SelectionStart, this.SelectionEnd));
					this.OnUIASelectionChanged();
				}
			}
		}

		/// <summary>Gets or sets the selected range of dates for a month calendar control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.SelectionRange" /> with the start and end dates of the selected range.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Windows.Forms.SelectionRange.Start" /> value of the assigned <see cref="T:System.Windows.Forms.SelectionRange" /> is less than the minimum date allowable for a month calendar control.-or- The <see cref="P:System.Windows.Forms.SelectionRange.Start" /> value of the assigned <see cref="T:System.Windows.Forms.SelectionRange" /> is greater than the maximum allowable date for a month calendar control.-or- The <see cref="P:System.Windows.Forms.SelectionRange.End" /> value of the assigned <see cref="T:System.Windows.Forms.SelectionRange" /> is less than the minimum date allowable for a month calendar control.-or- The <see cref="P:System.Windows.Forms.SelectionRange.End" /> value of the assigned <see cref="T:System.Windows.Forms.SelectionRange" /> is greater than the maximum allowable date for a month calendar control. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06002839 RID: 10297 RVA: 0x0009A780 File Offset: 0x00098980
		// (set) Token: 0x06002838 RID: 10296 RVA: 0x0009A470 File Offset: 0x00098670
		[Bindable(true)]
		public SelectionRange SelectionRange
		{
			get
			{
				return this.selection_range;
			}
			set
			{
				if (this.selection_range != value)
				{
					if (value.Start < this.MinDate)
					{
						throw new ArgumentException("SelectionStart cannot be less than MinDate");
					}
					if (value.End > this.MaxDate)
					{
						throw new ArgumentException("SelectionEnd cannot be greated than MaxDate");
					}
					SelectionRange selectionRange = this.selection_range;
					if (value.End.AddDays((double)((this.MaxSelectionCount - 1) * -1)) > value.Start)
					{
						this.selection_range = new SelectionRange(value.End.AddDays((double)((this.MaxSelectionCount - 1) * -1)), value.End);
					}
					else
					{
						this.selection_range = value;
					}
					SelectionRange displayRange = this.GetDisplayRange(true);
					if (displayRange.Start > this.selection_range.End)
					{
						this.current_month = new DateTime(this.selection_range.Start.Year, this.selection_range.Start.Month, 1);
						base.Invalidate();
					}
					else if (displayRange.End < this.selection_range.Start)
					{
						int num = this.selection_range.End.Year - displayRange.End.Year;
						int num2 = this.selection_range.End.Month - displayRange.End.Month;
						this.current_month = this.current_month.AddMonths(num * 12 + num2);
						base.Invalidate();
					}
					DateTime dateTime = selectionRange.Start;
					DateTime dateTime2 = selectionRange.End;
					if (selectionRange.Start > this.SelectionRange.Start)
					{
						dateTime = this.SelectionRange.Start;
					}
					else if (selectionRange.Start == this.SelectionRange.Start)
					{
						if (selectionRange.End < this.SelectionRange.End)
						{
							dateTime = selectionRange.End;
						}
						else
						{
							dateTime = this.SelectionRange.End;
						}
					}
					if (selectionRange.End < this.SelectionRange.End)
					{
						dateTime2 = this.SelectionRange.End;
					}
					else if (selectionRange.End == this.SelectionRange.End)
					{
						if (selectionRange.Start < this.SelectionRange.Start)
						{
							dateTime2 = this.SelectionRange.Start;
						}
						else
						{
							dateTime2 = selectionRange.Start;
						}
					}
					SelectionRange selectionRange2 = new SelectionRange(dateTime, dateTime2);
					if (selectionRange2.End != selectionRange.End || selectionRange2.Start != selectionRange.Start)
					{
						this.InvalidateDateRange(selectionRange2);
					}
					this.OnDateChanged(new DateRangeEventArgs(this.SelectionStart, this.SelectionEnd));
					this.OnUIASelectionChanged();
				}
			}
		}

		/// <summary>Gets or sets the start date of the selected range of dates.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> indicating the first date in the selection range.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date value is less than <see cref="P:System.Windows.Forms.MonthCalendar.MinDate" />.-or- The date value is greater than <see cref="P:System.Windows.Forms.MonthCalendar.MaxDate" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x0600283B RID: 10299 RVA: 0x0009A898 File Offset: 0x00098A98
		// (set) Token: 0x0600283A RID: 10298 RVA: 0x0009A788 File Offset: 0x00098988
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public DateTime SelectionStart
		{
			get
			{
				return this.selection_range.Start;
			}
			set
			{
				if (value < this.MinDate || value > this.MaxDate)
				{
					throw new ArgumentException();
				}
				if (this.SelectionRange.Start != value)
				{
					if (value > this.SelectionRange.End)
					{
						this.SelectionRange.End = value;
					}
					else if (value.AddDays((double)(this.MaxSelectionCount - 1)) < this.SelectionRange.End)
					{
						this.SelectionRange.End = value.AddDays((double)(this.MaxSelectionCount - 1));
					}
					this.SelectionRange.Start = value;
					DateTime dateTime;
					dateTime..ctor(value.Year, value.Month, 1);
					if (this.current_month != dateTime)
					{
						this.current_month = dateTime;
					}
					base.Invalidate();
					this.OnDateChanged(new DateRangeEventArgs(this.SelectionStart, this.SelectionEnd));
					this.OnUIASelectionChanged();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the date represented by the <see cref="P:System.Windows.Forms.MonthCalendar.TodayDate" /> property is displayed at the bottom of the control.</summary>
		/// <returns>true if today's date is displayed; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x0600283D RID: 10301 RVA: 0x0009A8C4 File Offset: 0x00098AC4
		// (set) Token: 0x0600283C RID: 10300 RVA: 0x0009A8A8 File Offset: 0x00098AA8
		[DefaultValue(true)]
		public bool ShowToday
		{
			get
			{
				return this.show_today;
			}
			set
			{
				if (this.show_today != value)
				{
					this.show_today = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether today's date is identified with a circle or a square.</summary>
		/// <returns>true if today's date is identified with a circle or a square; otherwise, false. The default is true.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x0600283F RID: 10303 RVA: 0x0009A8E8 File Offset: 0x00098AE8
		// (set) Token: 0x0600283E RID: 10302 RVA: 0x0009A8CC File Offset: 0x00098ACC
		[DefaultValue(true)]
		public bool ShowTodayCircle
		{
			get
			{
				return this.show_today_circle;
			}
			set
			{
				if (this.show_today_circle != value)
				{
					this.show_today_circle = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating whether the month calendar control displays week numbers (1-52) to the left of each row of days.</summary>
		/// <returns>true if the week numbers are displayed; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06002841 RID: 10305 RVA: 0x0009A938 File Offset: 0x00098B38
		// (set) Token: 0x06002840 RID: 10304 RVA: 0x0009A8F0 File Offset: 0x00098AF0
		[Localizable(true)]
		[DefaultValue(false)]
		public bool ShowWeekNumbers
		{
			get
			{
				return this.show_week_numbers;
			}
			set
			{
				if (this.show_week_numbers != value)
				{
					this.show_week_numbers = value;
					this.SetBoundsCore(base.Left, base.Top, base.Width, base.Height, BoundsSpecified.Width);
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets the minimum size to display one month of the calendar.</summary>
		/// <returns>The size, in pixels, necessary to fully display one month in the calendar.</returns>
		/// <exception cref="T:System.InvalidOperationException">The dimensions cannot be retrieved. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x0009A940 File Offset: 0x00098B40
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public Size SingleMonthSize
		{
			get
			{
				if (this.Font == null)
				{
					throw new InvalidOperationException();
				}
				int height = this.Font.Height;
				int num = ((!this.ShowWeekNumbers) ? 7 : 8);
				int num2 = 7;
				this.date_cell_size = new Size((int)Math.Ceiling(1.8 * (double)height), height);
				this.title_size = new Size(this.date_cell_size.Width * num, 2 * height);
				return new Size(num * this.date_cell_size.Width, num2 * this.date_cell_size.Height + this.title_size.Height);
			}
		}

		/// <summary>Gets or sets the size of the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
		/// <returns>The <see cref="T:System.Drawing.Size" /> of the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</returns>
		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06002843 RID: 10307 RVA: 0x0009A9E4 File Offset: 0x00098BE4
		// (set) Token: 0x06002844 RID: 10308 RVA: 0x0009A9EC File Offset: 0x00098BEC
		[Localizable(false)]
		[DesignerSerializationVisibility(0)]
		public new Size Size
		{
			get
			{
				return base.Size;
			}
			set
			{
				base.Size = value;
			}
		}

		/// <summary>Gets or sets the text to display on the <see cref="T:System.Windows.Forms.MonthCalendar" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06002845 RID: 10309 RVA: 0x0009A9F8 File Offset: 0x00098BF8
		// (set) Token: 0x06002846 RID: 10310 RVA: 0x0009AA00 File Offset: 0x00098C00
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		[EditorBrowsable(1)]
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

		/// <summary>Gets or sets a value indicating the background color of the title area of the calendar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />. The default is the system color for active captions.</returns>
		/// <exception cref="T:System.ArgumentException">The value is not a valid <see cref="T:System.Drawing.Color" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002848 RID: 10312 RVA: 0x0009AA2C File Offset: 0x00098C2C
		// (set) Token: 0x06002847 RID: 10311 RVA: 0x0009AA0C File Offset: 0x00098C0C
		public Color TitleBackColor
		{
			get
			{
				return this.title_back_color;
			}
			set
			{
				if (this.title_back_color != value)
				{
					this.title_back_color = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets a value indicating the foreground color of the title area of the calendar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />. The default is the system color for active caption text.</returns>
		/// <exception cref="T:System.ArgumentException">The value is not a valid <see cref="T:System.Drawing.Color" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x0600284A RID: 10314 RVA: 0x0009AA54 File Offset: 0x00098C54
		// (set) Token: 0x06002849 RID: 10313 RVA: 0x0009AA34 File Offset: 0x00098C34
		public Color TitleForeColor
		{
			get
			{
				return this.title_fore_color;
			}
			set
			{
				if (this.title_fore_color != value)
				{
					this.title_fore_color = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets or sets the value that is used by <see cref="T:System.Windows.Forms.MonthCalendar" /> as today's date.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> representing today's date. The default value is the current system date.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than the minimum allowable date.-or- The value is greater than the maximum allowable date.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x0600284C RID: 10316 RVA: 0x0009AA84 File Offset: 0x00098C84
		// (set) Token: 0x0600284B RID: 10315 RVA: 0x0009AA5C File Offset: 0x00098C5C
		public DateTime TodayDate
		{
			get
			{
				return this.today_date;
			}
			set
			{
				this.today_date_set = true;
				if (this.today_date != value)
				{
					this.today_date = value;
					base.Invalidate();
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="P:System.Windows.Forms.MonthCalendar.TodayDate" /> property has been explicitly set.</summary>
		/// <returns>true if the value for the <see cref="P:System.Windows.Forms.MonthCalendar.TodayDate" /> property has been explicitly set; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x0600284D RID: 10317 RVA: 0x0009AA8C File Offset: 0x00098C8C
		[Browsable(false)]
		[DesignerSerializationVisibility(0)]
		public bool TodayDateSet
		{
			get
			{
				return this.today_date_set;
			}
		}

		/// <summary>Gets or sets a value indicating the color of days in months that are not fully displayed in the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" />. The default is <see cref="P:System.Drawing.Color.Gray" />.</returns>
		/// <exception cref="T:System.ArgumentException">The value is not a valid <see cref="T:System.Drawing.Color" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x0600284F RID: 10319 RVA: 0x0009AAF8 File Offset: 0x00098CF8
		// (set) Token: 0x0600284E RID: 10318 RVA: 0x0009AA94 File Offset: 0x00098C94
		public Color TrailingForeColor
		{
			get
			{
				return this.trailing_fore_color;
			}
			set
			{
				if (this.trailing_fore_color != value)
				{
					this.trailing_fore_color = value;
					SelectionRange displayRange = this.GetDisplayRange(false);
					SelectionRange displayRange2 = this.GetDisplayRange(true);
					this.InvalidateDateRange(new SelectionRange(displayRange.Start, displayRange2.Start));
					this.InvalidateDateRange(new SelectionRange(displayRange.End, displayRange2.End));
				}
			}
		}

		/// <summary>Gets a <see cref="T:System.Windows.Forms.CreateParams" /> for creating a <see cref="T:System.Windows.Forms.MonthCalendar" /> control. </summary>
		/// <returns>A <see cref="T:System.Windows.Forms.CreateParams" /> with the information for creating a <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</returns>
		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06002850 RID: 10320 RVA: 0x0009AB00 File Offset: 0x00098D00
		protected override CreateParams CreateParams
		{
			get
			{
				if (this.owner == null)
				{
					return base.CreateParams;
				}
				CreateParams createParams = base.CreateParams;
				createParams.Style ^= 1073741824;
				createParams.Style |= int.MinValue;
				createParams.ExStyle |= 136;
				return createParams;
			}
		}

		/// <summary>Gets a value indicating the input method editor for the <see cref="T:System.Windows.Forms.MonthCalendar" />.</summary>
		/// <returns>As implemented for this object, always <see cref="F:System.Windows.Forms.ImeMode.Disable" />.</returns>
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06002851 RID: 10321 RVA: 0x0009AB60 File Offset: 0x00098D60
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return base.DefaultImeMode;
			}
		}

		/// <summary>Gets the default margin settings for the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Padding" /> structure with a padding size of 9 pixels, for all of its edges.</returns>
		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06002852 RID: 10322 RVA: 0x0009AB68 File Offset: 0x00098D68
		protected override Padding DefaultMargin
		{
			get
			{
				return new Padding(9);
			}
		}

		/// <summary>Gets the default size of the calendar.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> specifying the height and width, in pixels, of the control.</returns>
		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002853 RID: 10323 RVA: 0x0009AB74 File Offset: 0x00098D74
		protected override Size DefaultSize
		{
			get
			{
				Size singleMonthSize = this.SingleMonthSize;
				int num = this.calendar_dimensions.Width * singleMonthSize.Width;
				if (this.calendar_dimensions.Width > 1)
				{
					num += (this.calendar_dimensions.Width - 1) * this.calendar_spacing.Width;
				}
				int num2 = this.calendar_dimensions.Height * singleMonthSize.Height;
				if (this.ShowToday)
				{
					num2 += this.date_cell_size.Height + 2;
				}
				if (this.calendar_dimensions.Height > 1)
				{
					num2 += (this.calendar_dimensions.Height - 1) * this.calendar_spacing.Height;
				}
				if (num > 0)
				{
					num += 2;
				}
				if (num2 > 0)
				{
					num2 += 2;
				}
				return new Size(num, num2);
			}
		}

		/// <summary>Adds a day that is displayed in bold on an annual basis in the month calendar.</summary>
		/// <param name="date">The date to be displayed in bold. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002854 RID: 10324 RVA: 0x0009AC44 File Offset: 0x00098E44
		public void AddAnnuallyBoldedDate(DateTime date)
		{
			if (this.annually_bolded_dates == null)
			{
				this.annually_bolded_dates = new ArrayList();
			}
			if (!this.annually_bolded_dates.Contains(date))
			{
				this.annually_bolded_dates.Add(date);
			}
		}

		/// <summary>Adds a day to be displayed in bold in the month calendar.</summary>
		/// <param name="date">The date to be displayed in bold. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002855 RID: 10325 RVA: 0x0009AC90 File Offset: 0x00098E90
		public void AddBoldedDate(DateTime date)
		{
			if (this.bolded_dates == null)
			{
				this.bolded_dates = new ArrayList();
			}
			if (!this.bolded_dates.Contains(date))
			{
				this.bolded_dates.Add(date);
			}
		}

		/// <summary>Adds a day that is displayed in bold on a monthly basis in the month calendar.</summary>
		/// <param name="date">The date to be displayed in bold. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06002856 RID: 10326 RVA: 0x0009ACDC File Offset: 0x00098EDC
		public void AddMonthlyBoldedDate(DateTime date)
		{
			if (this.monthly_bolded_dates == null)
			{
				this.monthly_bolded_dates = new ArrayList();
			}
			if (!this.monthly_bolded_dates.Contains(date))
			{
				this.monthly_bolded_dates.Add(date);
			}
		}

		/// <summary>Retrieves date information that represents the low and high limits of the displayed dates of the control.</summary>
		/// <returns>The begin and end dates of the displayed calendar.</returns>
		/// <param name="visible">true to retrieve only the dates that are fully contained in displayed months; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002857 RID: 10327 RVA: 0x0009AD28 File Offset: 0x00098F28
		public SelectionRange GetDisplayRange(bool visible)
		{
			DateTime firstDateInMonthGrid;
			firstDateInMonthGrid..ctor(this.current_month.Year, this.current_month.Month, 1);
			DateTime dateTime = firstDateInMonthGrid.AddMonths(this.calendar_dimensions.Width * this.calendar_dimensions.Height).AddDays(-1.0);
			if (!visible)
			{
				firstDateInMonthGrid = this.GetFirstDateInMonthGrid(firstDateInMonthGrid);
				dateTime = this.GetLastDateInMonthGrid(dateTime);
			}
			return new SelectionRange(firstDateInMonthGrid, dateTime);
		}

		/// <summary>Returns a <see cref="T:System.Windows.Forms.MonthCalendar.HitTestInfo" /> with information on which portion of a month calendar control is at a specified x- and y-coordinate.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MonthCalendar.HitTestInfo" /> that contains information about the specified point on the <see cref="T:System.Windows.Forms.MonthCalendar" />.</returns>
		/// <param name="x">The <see cref="P:System.Drawing.Point.X" /> coordinate of the point to be hit tested. </param>
		/// <param name="y">The <see cref="P:System.Drawing.Point.Y" /> coordinate of the point to be hit tested. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002858 RID: 10328 RVA: 0x0009ADA0 File Offset: 0x00098FA0
		public MonthCalendar.HitTestInfo HitTest(int x, int y)
		{
			return this.HitTest(new Point(x, y));
		}

		/// <summary>Returns an object with information on which portion of a month calendar control is at a location specified by a <see cref="T:System.Drawing.Point" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.MonthCalendar.HitTestInfo" /> that contains information about the specified point on the <see cref="T:System.Windows.Forms.MonthCalendar" />.</returns>
		/// <param name="point">A <see cref="T:System.Drawing.Point" /> containing the <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> coordinates of the point to be hit tested. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002859 RID: 10329 RVA: 0x0009ADB0 File Offset: 0x00098FB0
		public MonthCalendar.HitTestInfo HitTest(Point point)
		{
			return this.HitTest(point, out this.last_clicked_calendar_index, out this.last_clicked_calendar_rect);
		}

		/// <summary>Removes all the annually bold dates.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600285A RID: 10330 RVA: 0x0009ADC8 File Offset: 0x00098FC8
		public void RemoveAllAnnuallyBoldedDates()
		{
			if (this.annually_bolded_dates != null)
			{
				this.annually_bolded_dates.Clear();
			}
		}

		/// <summary>Removes all the nonrecurring bold dates.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600285B RID: 10331 RVA: 0x0009ADE0 File Offset: 0x00098FE0
		public void RemoveAllBoldedDates()
		{
			if (this.bolded_dates != null)
			{
				this.bolded_dates.Clear();
			}
		}

		/// <summary>Removes all the monthly bold dates.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600285C RID: 10332 RVA: 0x0009ADF8 File Offset: 0x00098FF8
		public void RemoveAllMonthlyBoldedDates()
		{
			if (this.monthly_bolded_dates != null)
			{
				this.monthly_bolded_dates.Clear();
			}
		}

		/// <summary>Removes the specified date from the list of annually bold dates.</summary>
		/// <param name="date">The date to remove from the date list. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600285D RID: 10333 RVA: 0x0009AE10 File Offset: 0x00099010
		public void RemoveAnnuallyBoldedDate(DateTime date)
		{
			if (this.annually_bolded_dates == null)
			{
				return;
			}
			for (int i = 0; i < this.annually_bolded_dates.Count; i++)
			{
				DateTime dateTime = (DateTime)this.annually_bolded_dates[i];
				if (dateTime.Day == date.Day && dateTime.Month == date.Month)
				{
					this.annually_bolded_dates.RemoveAt(i);
					return;
				}
			}
		}

		/// <summary>Removes the specified date from the list of nonrecurring bold dates.</summary>
		/// <param name="date">The date to remove from the date list. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600285E RID: 10334 RVA: 0x0009AE8C File Offset: 0x0009908C
		public void RemoveBoldedDate(DateTime date)
		{
			if (this.bolded_dates == null)
			{
				return;
			}
			for (int i = 0; i < this.bolded_dates.Count; i++)
			{
				DateTime dateTime = (DateTime)this.bolded_dates[i];
				if (dateTime.Year == date.Year && dateTime.Month == date.Month && dateTime.Day == date.Day)
				{
					this.bolded_dates.RemoveAt(i);
					return;
				}
			}
		}

		/// <summary>Removes the specified date from the list of monthly bolded dates.</summary>
		/// <param name="date">The date to remove from the date list. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600285F RID: 10335 RVA: 0x0009AF1C File Offset: 0x0009911C
		public void RemoveMonthlyBoldedDate(DateTime date)
		{
			if (this.monthly_bolded_dates == null)
			{
				return;
			}
			for (int i = 0; i < this.monthly_bolded_dates.Count; i++)
			{
				DateTime dateTime = (DateTime)this.monthly_bolded_dates[i];
				if (dateTime.Day == date.Day && dateTime.Month == date.Month)
				{
					this.monthly_bolded_dates.RemoveAt(i);
					return;
				}
			}
		}

		/// <summary>Sets the number of columns and rows of months to display.</summary>
		/// <param name="x">The number of columns. </param>
		/// <param name="y">The number of rows. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="x" /> or <paramref name="y" /> is less than 1. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002860 RID: 10336 RVA: 0x0009AF98 File Offset: 0x00099198
		public void SetCalendarDimensions(int x, int y)
		{
			this.CalendarDimensions = new Size(x, y);
		}

		/// <summary>Sets a date as the currently selected date.</summary>
		/// <param name="date">The date to be selected. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is less than the minimum allowable date.-or- The value is greater than the maximum allowable date. This exception will only be thrown if <see cref="P:System.Windows.Forms.MonthCalendar.MinDate" /> or <see cref="P:System.Windows.Forms.MonthCalendar.MaxDate" /> have been set explicitly.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002861 RID: 10337 RVA: 0x0009AFA8 File Offset: 0x000991A8
		public void SetDate(DateTime date)
		{
			this.SetSelectionRange(date.Date, date.Date);
		}

		/// <summary>Sets the selected dates in a month calendar control to the specified date range.</summary>
		/// <param name="date1">The beginning date of the selection range. </param>
		/// <param name="date2">The end date of the selection range. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="date1" /> is less than the minimum date allowable for a month calendar control.-or- <paramref name="date1" /> is greater than the maximum allowable date for a month calendar control.-or- <paramref name="date2" /> is less than the minimum date allowable for a month calendar control.-or- <paramref name="date2" /> is greater than the maximum allowable date for a month calendar control. This exception will only be thrown if <see cref="P:System.Windows.Forms.MonthCalendar.MinDate" /> or <see cref="P:System.Windows.Forms.MonthCalendar.MaxDate" /> have been set explicitly.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002862 RID: 10338 RVA: 0x0009AFC0 File Offset: 0x000991C0
		public void SetSelectionRange(DateTime date1, DateTime date2)
		{
			this.SelectionRange = new SelectionRange(date1, date2);
		}

		/// <summary>Returns a string that represents the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Windows.Forms.MonthCalendar" />. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06002863 RID: 10339 RVA: 0x0009AFD0 File Offset: 0x000991D0
		public override string ToString()
		{
			return base.GetType().Name + ", " + this.SelectionRange.ToString();
		}

		/// <summary>Repaints the bold dates to reflect the dates set in the lists of bold dates.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002864 RID: 10340 RVA: 0x0009B000 File Offset: 0x00099200
		public void UpdateBoldedDates()
		{
			base.Invalidate();
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.CreateHandle" /> method.</summary>
		// Token: 0x06002865 RID: 10341 RVA: 0x0009B008 File Offset: 0x00099208
		protected override void CreateHandle()
		{
			base.CreateHandle();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.MonthCalendar" />. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06002866 RID: 10342 RVA: 0x0009B010 File Offset: 0x00099210
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
		}

		/// <returns>true if the specified key is a regular input key; otherwise, false.</returns>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values. </param>
		// Token: 0x06002867 RID: 10343 RVA: 0x0009B01C File Offset: 0x0009921C
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
				return base.IsInputKey(keyData);
			}
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002868 RID: 10344 RVA: 0x0009B058 File Offset: 0x00099258
		protected override void OnBackColorChanged(EventArgs e)
		{
			base.OnBackColorChanged(e);
			base.Invalidate();
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MonthCalendar.DateChanged" /> event.</summary>
		/// <param name="drevent">A <see cref="T:System.Windows.Forms.DateRangeEventArgs" /> that contains the event data. </param>
		// Token: 0x06002869 RID: 10345 RVA: 0x0009B068 File Offset: 0x00099268
		protected virtual void OnDateChanged(DateRangeEventArgs drevent)
		{
			DateRangeEventHandler dateRangeEventHandler = (DateRangeEventHandler)base.Events[MonthCalendar.DateChangedEvent];
			if (dateRangeEventHandler != null)
			{
				dateRangeEventHandler(this, drevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MonthCalendar.DateSelected" /> event.</summary>
		/// <param name="drevent">A <see cref="T:System.Windows.Forms.DateRangeEventArgs" /> that contains the event data. </param>
		// Token: 0x0600286A RID: 10346 RVA: 0x0009B09C File Offset: 0x0009929C
		protected virtual void OnDateSelected(DateRangeEventArgs drevent)
		{
			DateRangeEventHandler dateRangeEventHandler = (DateRangeEventHandler)base.Events[MonthCalendar.DateSelectedEvent];
			if (dateRangeEventHandler != null)
			{
				dateRangeEventHandler(this, drevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.FontChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600286B RID: 10347 RVA: 0x0009B0D0 File Offset: 0x000992D0
		protected override void OnFontChanged(EventArgs e)
		{
			this.Size = new Size(this.CalendarDimensions.Width * this.SingleMonthSize.Width, this.CalendarDimensions.Height * this.SingleMonthSize.Height);
			this.bold_font = new Font(this.Font, this.Font.Style | 1);
			base.OnFontChanged(e);
		}

		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600286C RID: 10348 RVA: 0x0009B148 File Offset: 0x00099348
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.OnHandleCreated(System.EventArgs)" /> method.</summary>
		// Token: 0x0600286D RID: 10349 RVA: 0x0009B154 File Offset: 0x00099354
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.HandleDestroyed" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600286E RID: 10350 RVA: 0x0009B160 File Offset: 0x00099360
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.MonthCalendar.RightToLeftLayoutChanged" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600286F RID: 10351 RVA: 0x0009B16C File Offset: 0x0009936C
		[EditorBrowsable(2)]
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[MonthCalendar.RightToLeftLayoutChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, e);
			}
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.SetBoundsCore(System.Int32,System.Int32,System.Int32,System.Int32,System.Windows.Forms.BoundsSpecified)" /> method.</summary>
		/// <param name="x">The new <see cref="P:System.Windows.Forms.Control.Left" /> property value of the control.</param>
		/// <param name="y">The new <see cref="P:System.Windows.Forms.Control.Right" /> property value of the control.</param>
		/// <param name="width">The new <see cref="P:System.Windows.Forms.Control.Width" /> property value of the control.</param>
		/// <param name="height">The new <see cref="P:System.Windows.Forms.Control.Height" /> property value of the control.</param>
		/// <param name="specified">A bitwise combination of the <see cref="T:System.Windows.Forms.BoundsSpecified" /> values.</param>
		// Token: 0x06002870 RID: 10352 RVA: 0x0009B1A0 File Offset: 0x000993A0
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			Size defaultSize = this.DefaultSize;
			Size size = defaultSize;
			Size size2;
			size2..ctor(defaultSize.Width + this.SingleMonthSize.Width + this.calendar_spacing.Width, defaultSize.Height + this.SingleMonthSize.Height + this.calendar_spacing.Height);
			int num = (size2.Width + size.Width) / 2;
			int num2 = (size2.Height + size.Height) / 2;
			if (width < num)
			{
				width = size.Width;
			}
			else
			{
				width = size2.Width;
			}
			if (height < num2)
			{
				height = size.Height;
			}
			else
			{
				height = size2.Height;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		/// <summary>Overrides the <see cref="M:System.Windows.Forms.Control.WndProc(System.Windows.Forms.Message@)" /> method.</summary>
		// Token: 0x06002871 RID: 10353 RVA: 0x0009B274 File Offset: 0x00099474
		protected override void WndProc(ref Message m)
		{
			base.WndProc(ref m);
		}

		// Token: 0x06002872 RID: 10354 RVA: 0x0009B280 File Offset: 0x00099480
		private void AddYears(int years, bool fast)
		{
			if (fast && this.CurrentMonth.Year + years * 5 <= this.MaxDate.Year)
			{
				DateTime dateTime = this.CurrentMonth.AddYears(years * 5);
				if (this.MaxDate >= dateTime && this.MinDate <= dateTime)
				{
					this.CurrentMonth = dateTime;
					return;
				}
			}
			if (this.CurrentMonth.Year + years <= this.MaxDate.Year)
			{
				DateTime dateTime = this.CurrentMonth.AddYears(years);
				if (this.MaxDate >= dateTime && this.MinDate <= dateTime)
				{
					this.CurrentMonth = dateTime;
				}
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002873 RID: 10355 RVA: 0x0009B354 File Offset: 0x00099554
		// (set) Token: 0x06002874 RID: 10356 RVA: 0x0009B35C File Offset: 0x0009955C
		internal bool IsYearGoingUp
		{
			get
			{
				return this.is_year_going_up;
			}
			set
			{
				if (value)
				{
					this.is_year_going_down = false;
					this.year_moving_count = ((!this.is_year_going_up) ? 1 : (this.year_moving_count + 1));
					if (this.is_year_going_up)
					{
						this.year_moving_count++;
					}
					else
					{
						this.year_moving_count = 1;
					}
					this.AddYears(1, this.year_moving_count > 10);
					if (this.is_mouse_moving_year)
					{
						this.StartHideTimer();
					}
				}
				else
				{
					this.year_moving_count = 0;
				}
				this.is_year_going_up = value;
				base.Invalidate();
			}
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002875 RID: 10357 RVA: 0x0009B3F8 File Offset: 0x000995F8
		// (set) Token: 0x06002876 RID: 10358 RVA: 0x0009B400 File Offset: 0x00099600
		internal bool IsYearGoingDown
		{
			get
			{
				return this.is_year_going_down;
			}
			set
			{
				if (value)
				{
					this.is_year_going_up = false;
					this.year_moving_count = ((!this.is_year_going_down) ? 1 : (this.year_moving_count + 1));
					if (this.is_year_going_down)
					{
						this.year_moving_count++;
					}
					else
					{
						this.year_moving_count = 1;
					}
					this.AddYears(-1, this.year_moving_count > 10);
					if (this.is_mouse_moving_year)
					{
						this.StartHideTimer();
					}
				}
				else
				{
					this.year_moving_count = 0;
				}
				this.is_year_going_down = value;
				base.Invalidate();
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x06002877 RID: 10359 RVA: 0x0009B49C File Offset: 0x0009969C
		// (set) Token: 0x06002878 RID: 10360 RVA: 0x0009B4A4 File Offset: 0x000996A4
		internal bool ShowYearUpDown
		{
			get
			{
				return this.show_year_updown;
			}
			set
			{
				if (this.show_year_updown != value)
				{
					this.show_year_updown = value;
					base.Invalidate();
				}
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x0600287A RID: 10362 RVA: 0x0009B574 File Offset: 0x00099774
		// (set) Token: 0x06002879 RID: 10361 RVA: 0x0009B4C0 File Offset: 0x000996C0
		internal DateTime CurrentMonth
		{
			get
			{
				return this.current_month;
			}
			set
			{
				if (value < this.MinDate || value > this.MaxDate)
				{
					return;
				}
				if (value.Month != this.current_month.Month || value.Year != this.current_month.Year)
				{
					this.SelectionRange = new SelectionRange(this.SelectionStart.Add(value.Subtract(this.current_month)), this.SelectionEnd.Add(value.Subtract(this.current_month)));
					this.current_month = value;
					this.UpdateBoldedDates();
					base.Invalidate();
				}
			}
		}

		// Token: 0x0600287B RID: 10363 RVA: 0x0009B57C File Offset: 0x0009977C
		internal MonthCalendar.HitTestInfo HitTest(Point point, out int calendar_index, out Rectangle calendar_rect)
		{
			calendar_index = -1;
			calendar_rect = Rectangle.Empty;
			Rectangle rectangle;
			rectangle..ctor(base.ClientRectangle.X, base.ClientRectangle.Bottom - this.date_cell_size.Height, 7 * this.date_cell_size.Width, this.date_cell_size.Height);
			if (rectangle.Contains(point) && this.ShowToday)
			{
				return new MonthCalendar.HitTestInfo(MonthCalendar.HitArea.TodayLink, point, DateTime.Now);
			}
			Size singleMonthSize = this.SingleMonthSize;
			Rectangle[] array = new Rectangle[this.CalendarDimensions.Width * this.CalendarDimensions.Height];
			for (int i = 0; i < this.CalendarDimensions.Width * this.CalendarDimensions.Height; i++)
			{
				if (i == 0)
				{
					array[i] = new Rectangle(new Point(base.ClientRectangle.X + 1, base.ClientRectangle.Y + 1), singleMonthSize);
				}
				else if (i % this.CalendarDimensions.Width == 0)
				{
					array[i] = new Rectangle(new Point(array[i - this.CalendarDimensions.Width].X, array[i - this.CalendarDimensions.Width].Bottom + this.calendar_spacing.Height), singleMonthSize);
				}
				else
				{
					array[i] = new Rectangle(new Point(array[i - 1].Right + this.calendar_spacing.Width, array[i - 1].Y), singleMonthSize);
				}
			}
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j].Contains(point))
				{
					Rectangle rectangle2;
					rectangle2..ctor(array[j].Location, this.title_size);
					if (rectangle2.Contains(point))
					{
						if (j == 0)
						{
							Rectangle rectangle3;
							rectangle3..ctor(new Point(array[j].X + this.button_x_offset, (this.title_size.Height - this.button_size.Height) / 2), this.button_size);
							if (rectangle3.Contains(point))
							{
								return new MonthCalendar.HitTestInfo(MonthCalendar.HitArea.PrevMonthButton, point, new DateTime(1, 1, 1));
							}
						}
						if (j % this.CalendarDimensions.Height == 0 && j % this.CalendarDimensions.Width == this.calendar_dimensions.Width - 1)
						{
							Rectangle rectangle4;
							rectangle4..ctor(new Point(array[j].Right - this.button_x_offset - this.button_size.Width, (this.title_size.Height - this.button_size.Height) / 2), this.button_size);
							if (rectangle4.Contains(point))
							{
								return new MonthCalendar.HitTestInfo(MonthCalendar.HitArea.NextMonthButton, point, new DateTime(1, 1, 1));
							}
						}
						calendar_index = j;
						calendar_rect = array[j];
						if (this.GetMonthNameRectangle(rectangle2, j).Contains(point))
						{
							return new MonthCalendar.HitTestInfo(MonthCalendar.HitArea.TitleMonth, point, new DateTime(1, 1, 1));
						}
						Rectangle rectangle5;
						Rectangle rectangle6;
						Rectangle rectangle7;
						this.GetYearNameRectangles(rectangle2, j, out rectangle5, out rectangle6, out rectangle7);
						if (rectangle5.Contains(point))
						{
							return new MonthCalendar.HitTestInfo(MonthCalendar.HitArea.TitleYear, point, new DateTime(1, 1, 1), MonthCalendar.HitAreaExtra.YearRectangle);
						}
						if (rectangle6.Contains(point))
						{
							return new MonthCalendar.HitTestInfo(MonthCalendar.HitArea.TitleYear, point, new DateTime(1, 1, 1), MonthCalendar.HitAreaExtra.UpButton);
						}
						if (rectangle7.Contains(point))
						{
							return new MonthCalendar.HitTestInfo(MonthCalendar.HitArea.TitleYear, point, new DateTime(1, 1, 1), MonthCalendar.HitAreaExtra.DownButton);
						}
						return new MonthCalendar.HitTestInfo(MonthCalendar.HitArea.TitleBackground, point, new DateTime(1, 1, 1));
					}
					else
					{
						Point point2;
						point2..ctor(array[j].X, rectangle2.Bottom);
						if (this.ShowWeekNumbers)
						{
							Rectangle rectangle8;
							rectangle8..ctor(point2, new Size(this.date_cell_size.Width, Math.Max(array[j].Height - rectangle2.Height, 0)));
							if (rectangle8.Contains(point))
							{
								return new MonthCalendar.HitTestInfo(MonthCalendar.HitArea.WeekNumbers, point, DateTime.Now);
							}
							point2.X += this.date_cell_size.Width;
						}
						Rectangle rectangle9;
						rectangle9..ctor(point2, new Size(Math.Max(array[j].Right - point2.X, 0), this.date_cell_size.Height));
						if (rectangle9.Contains(point))
						{
							return new MonthCalendar.HitTestInfo(MonthCalendar.HitArea.DayOfWeek, point, new DateTime(1, 1, 1));
						}
						Rectangle rectangle10;
						rectangle10..ctor(new Point(rectangle9.X, rectangle9.Bottom), new Size(rectangle9.Width, Math.Max(array[j].Bottom - rectangle9.Bottom, 0)));
						if (rectangle10.Contains(point))
						{
							this.clicked_rect = rectangle10;
							Point point3;
							point3..ctor(point.X - rectangle10.X, point.Y - rectangle10.Y);
							int num = point3.Y / this.date_cell_size.Height;
							int num2 = point3.X / this.date_cell_size.Width;
							DateTime dateTime = this.CurrentMonth.AddMonths(j);
							DateTime dateTime2 = this.GetFirstDateInMonthGrid(dateTime).AddDays((double)(num * 7 + num2));
							if (dateTime2.Year == dateTime.Year && dateTime2.Month == dateTime.Month)
							{
								return new MonthCalendar.HitTestInfo(MonthCalendar.HitArea.Date, point, dateTime2);
							}
							if (dateTime2 < dateTime && j == 0)
							{
								return new MonthCalendar.HitTestInfo(MonthCalendar.HitArea.PrevMonthDate, point, new DateTime(1, 1, 1), dateTime2);
							}
							if (dateTime2 > dateTime && j == this.CalendarDimensions.Width * this.CalendarDimensions.Height - 1)
							{
								return new MonthCalendar.HitTestInfo(MonthCalendar.HitArea.NextMonthDate, point, new DateTime(1, 1, 1), dateTime2);
							}
							return new MonthCalendar.HitTestInfo(MonthCalendar.HitArea.Nowhere, point, new DateTime(1, 1, 1));
						}
					}
				}
			}
			return new MonthCalendar.HitTestInfo();
		}

		// Token: 0x0600287C RID: 10364 RVA: 0x0009BBD8 File Offset: 0x00099DD8
		internal DateTime GetFirstDateInMonthGrid(DateTime month)
		{
			DayOfWeek dayOfWeek = this.GetDayOfWeek(this.first_day_of_week);
			DateTime dateTime;
			dateTime..ctor(month.Year, month.Month, 1);
			DayOfWeek dayOfWeek2 = dateTime.DayOfWeek;
			int num = dayOfWeek2 - dayOfWeek;
			if (num < 0)
			{
				num += 7;
			}
			return dateTime.AddDays((double)(-1 * num));
		}

		// Token: 0x0600287D RID: 10365 RVA: 0x0009BC2C File Offset: 0x00099E2C
		internal DateTime GetLastDateInMonthGrid(DateTime month)
		{
			return this.GetFirstDateInMonthGrid(month).AddDays(41.0);
		}

		// Token: 0x0600287E RID: 10366 RVA: 0x0009BC54 File Offset: 0x00099E54
		internal bool IsBoldedDate(DateTime date)
		{
			if (this.bolded_dates != null && this.bolded_dates.Count > 0)
			{
				foreach (object obj in this.bolded_dates)
				{
					if (((DateTime)obj).Date == date.Date)
					{
						return true;
					}
				}
			}
			if (this.monthly_bolded_dates != null && this.monthly_bolded_dates.Count > 0)
			{
				foreach (object obj2 in this.monthly_bolded_dates)
				{
					if (((DateTime)obj2).Day == date.Day)
					{
						return true;
					}
				}
			}
			if (this.annually_bolded_dates != null && this.annually_bolded_dates.Count > 0)
			{
				foreach (object obj3 in this.annually_bolded_dates)
				{
					DateTime dateTime = (DateTime)obj3;
					if (dateTime.Month == date.Month && dateTime.Day == date.Day)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600287F RID: 10367 RVA: 0x0009BE3C File Offset: 0x0009A03C
		private void SetUpTodayMenu()
		{
			this.today_menu = new ContextMenu();
			MenuItem menuItem = new MenuItem("Go to today");
			menuItem.Click += new EventHandler(this.TodayMenuItemClickHandler);
			this.today_menu.MenuItems.Add(menuItem);
		}

		// Token: 0x06002880 RID: 10368 RVA: 0x0009BE84 File Offset: 0x0009A084
		private void SetUpMonthMenu()
		{
			this.month_menu = new ContextMenu();
			for (int i = 0; i < 12; i++)
			{
				DateTime dateTime;
				dateTime..ctor(2000, i + 1, 1);
				MenuItem menuItem = new MenuItem(dateTime.ToString("MMMM"));
				menuItem.Click += new EventHandler(this.MonthMenuItemClickHandler);
				this.month_menu.MenuItems.Add(menuItem);
			}
		}

		// Token: 0x06002881 RID: 10369 RVA: 0x0009BEF8 File Offset: 0x0009A0F8
		private DateTime GetFirstDateInMonth(DateTime date)
		{
			return new DateTime(date.Year, date.Month, 1);
		}

		// Token: 0x06002882 RID: 10370 RVA: 0x0009BF10 File Offset: 0x0009A110
		private DateTime GetLastDateInMonth(DateTime date)
		{
			DateTime dateTime;
			dateTime..ctor(date.Year, date.Month, 1);
			return dateTime.AddMonths(1).AddDays(-1.0);
		}

		// Token: 0x06002883 RID: 10371 RVA: 0x0009BF4C File Offset: 0x0009A14C
		private void AddTimeToSelection(int delta, bool isDays)
		{
			DateTime dateTime;
			if (this.SelectionStart != this.first_select_start_date)
			{
				dateTime = this.SelectionStart;
			}
			else
			{
				dateTime = this.SelectionEnd;
			}
			DateTime dateTime2;
			if (isDays)
			{
				dateTime2 = dateTime.AddDays((double)delta);
			}
			else
			{
				dateTime2 = dateTime.AddMonths(delta);
			}
			SelectionRange selectionRange = new SelectionRange(this.first_select_start_date, dateTime2);
			if (selectionRange.Start.AddDays((double)(this.MaxSelectionCount - 1)) < selectionRange.End)
			{
				if (selectionRange.Start != this.first_select_start_date)
				{
					selectionRange.Start = selectionRange.End.AddDays((double)((this.MaxSelectionCount - 1) * -1));
				}
				else
				{
					selectionRange.End = selectionRange.Start.AddDays((double)(this.MaxSelectionCount - 1));
				}
			}
			if (selectionRange.Start != this.selection_range.Start || selectionRange.End != this.selection_range.End)
			{
				this.SelectionRange = selectionRange;
			}
		}

		// Token: 0x06002884 RID: 10372 RVA: 0x0009C06C File Offset: 0x0009A26C
		private void SelectDate(DateTime date)
		{
			SelectionRange selectionRange = null;
			if (this.is_shift_pressed || this.click_state[0])
			{
				selectionRange = new SelectionRange(this.first_select_start_date, date);
				if (selectionRange.Start.AddDays((double)(this.MaxSelectionCount - 1)) < selectionRange.End)
				{
					if (selectionRange.Start != this.first_select_start_date)
					{
						selectionRange.Start = selectionRange.End.AddDays((double)((this.MaxSelectionCount - 1) * -1));
					}
					else
					{
						selectionRange.End = selectionRange.Start.AddDays((double)(this.MaxSelectionCount - 1));
					}
				}
			}
			else if (date >= this.MinDate && date <= this.MaxDate)
			{
				selectionRange = new SelectionRange(date, date);
				this.first_select_start_date = date;
			}
			if ((selectionRange != null && selectionRange.Start != this.selection_range.Start) || selectionRange.End != this.selection_range.End)
			{
				this.SelectionRange = selectionRange;
			}
		}

		// Token: 0x06002885 RID: 10373 RVA: 0x0009C198 File Offset: 0x0009A398
		internal int GetWeekOfYear(DateTime date)
		{
			DayOfWeek dayOfWeek = this.GetDayOfWeek(this.first_day_of_week);
			DateTime dateTime;
			dateTime..ctor(date.Year, 1, 1);
			DayOfWeek dayOfWeek2 = dateTime.DayOfWeek;
			int num = dayOfWeek2 - dayOfWeek;
			return (date.DayOfYear + num) / 7 + 1;
		}

		// Token: 0x06002886 RID: 10374 RVA: 0x0009C1E0 File Offset: 0x0009A3E0
		internal DayOfWeek GetDayOfWeek(Day day)
		{
			if (day == Day.Default)
			{
				return Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
			}
			return (int)Enum.Parse(typeof(DayOfWeek), day.ToString());
		}

		// Token: 0x06002887 RID: 10375 RVA: 0x0009C228 File Offset: 0x0009A428
		internal Rectangle GetMonthNameRectangle(Rectangle title_rect, int calendar_index)
		{
			DateTime dateTime = this.current_month.AddMonths(calendar_index);
			Size size = TextRenderer.MeasureString(dateTime.ToString("MMMM yyyy"), this.Font).ToSize();
			Size size2 = TextRenderer.MeasureString(dateTime.ToString("MMMM"), this.Font).ToSize();
			return new Rectangle(new Point(title_rect.X + (title_rect.Width - size.Width) / 2, title_rect.Y + (title_rect.Height - size.Height) / 2), size2);
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x0009C2C0 File Offset: 0x0009A4C0
		internal void GetYearNameRectangles(Rectangle title_rect, int calendar_index, out Rectangle year_rect, out Rectangle up_rect, out Rectangle down_rect)
		{
			DateTime dateTime = this.current_month.AddMonths(calendar_index);
			SizeF sizeF = TextRenderer.MeasureString(dateTime.ToString("MMMM yyyy"), this.bold_font, int.MaxValue, this.centered_format);
			SizeF sizeF2 = TextRenderer.MeasureString(dateTime.ToString("yyyy"), this.bold_font, int.MaxValue, this.centered_format);
			RectangleF rectangleF;
			rectangleF..ctor(new PointF((float)title_rect.X + ((float)title_rect.Width - sizeF.Width) / 2f, (float)title_rect.Y + ((float)title_rect.Height - sizeF.Height) / 2f), sizeF);
			year_rect..ctor(new Point((int)(rectangleF.Right - sizeF2.Width + 1f), (int)rectangleF.Y), new Size((int)(sizeF2.Width + 1f), (int)(sizeF2.Height + 1f)));
			year_rect.Inflate(0, 1);
			up_rect = default(Rectangle);
			up_rect.Location = new Point(year_rect.X + year_rect.Width + 2, year_rect.Y);
			up_rect.Size = new Size(16, year_rect.Height / 2);
			down_rect = default(Rectangle);
			down_rect.Location = new Point(up_rect.X, up_rect.Y + up_rect.Height + 1);
			down_rect.Size = up_rect.Size;
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x0009C438 File Offset: 0x0009A638
		internal Rectangle GetYearNameRectangle(Rectangle title_rect, int calendar_index)
		{
			Rectangle rectangle;
			Rectangle rectangle2;
			this.GetYearNameRectangles(title_rect, calendar_index, out rectangle, out rectangle2, out rectangle2);
			return rectangle;
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x0009C454 File Offset: 0x0009A654
		internal bool IsValidWeekToDraw(DateTime month, DateTime date, int row, int col)
		{
			DateTime dateTime = month.AddMonths(-1);
			if ((month.Year == date.Year && month.Month == date.Month) || (dateTime.Year == date.Year && dateTime.Month == date.Month))
			{
				return true;
			}
			if (row == this.CalendarDimensions.Height - 1 && col == this.CalendarDimensions.Width - 1)
			{
				dateTime = month.AddMonths(1);
				return dateTime.Year == date.Year && dateTime.Month == date.Month;
			}
			return false;
		}

		// Token: 0x0600288B RID: 10379 RVA: 0x0009C518 File Offset: 0x0009A718
		private void SetItemClick(MonthCalendar.HitTestInfo hti)
		{
			switch (hti.HitArea)
			{
			case MonthCalendar.HitArea.NextMonthButton:
				this.is_previous_clicked = false;
				this.is_next_clicked = true;
				this.is_date_clicked = false;
				return;
			case MonthCalendar.HitArea.PrevMonthButton:
				this.is_previous_clicked = true;
				this.is_next_clicked = false;
				this.is_date_clicked = false;
				return;
			case MonthCalendar.HitArea.Date:
			case MonthCalendar.HitArea.NextMonthDate:
			case MonthCalendar.HitArea.PrevMonthDate:
				this.clicked_date = hti.hit_time;
				this.is_previous_clicked = false;
				this.is_next_clicked = false;
				this.is_date_clicked = true;
				return;
			}
			this.is_previous_clicked = false;
			this.is_next_clicked = false;
			this.is_date_clicked = false;
		}

		// Token: 0x0600288C RID: 10380 RVA: 0x0009C5C8 File Offset: 0x0009A7C8
		private void TodayMenuItemClickHandler(object sender, EventArgs e)
		{
			this.SetSelectionRange(DateTime.Now.Date, DateTime.Now.Date);
			this.OnDateSelected(new DateRangeEventArgs(this.SelectionStart, this.SelectionEnd));
		}

		// Token: 0x0600288D RID: 10381 RVA: 0x0009C60C File Offset: 0x0009A80C
		private void MonthMenuItemClickHandler(object sender, EventArgs e)
		{
			MenuItem menuItem = sender as MenuItem;
			if (menuItem != null && this.month_title_click_location != Point.Empty)
			{
				if (menuItem.Parent == null)
				{
					return;
				}
				int num = menuItem.Parent.MenuItems.IndexOf(menuItem) + 1;
				if (num == 0)
				{
					return;
				}
				Size singleMonthSize = this.SingleMonthSize;
				for (int i = 0; i < this.CalendarDimensions.Height; i++)
				{
					for (int j = 0; j < this.CalendarDimensions.Width; j++)
					{
						int num2 = i * this.CalendarDimensions.Width + j;
						Rectangle rectangle;
						rectangle..ctor(new Point(0, 0), singleMonthSize);
						if (j == 0)
						{
							rectangle.X = base.ClientRectangle.X + 1;
						}
						else
						{
							rectangle.X = base.ClientRectangle.X + 1 + j * (singleMonthSize.Width + this.calendar_spacing.Width);
						}
						if (i == 0)
						{
							rectangle.Y = base.ClientRectangle.Y + 1;
						}
						else
						{
							rectangle.Y = base.ClientRectangle.Y + 1 + i * (singleMonthSize.Height + this.calendar_spacing.Height);
						}
						if (rectangle.Contains(this.month_title_click_location))
						{
							int num3 = num - this.CurrentMonth.AddMonths(num2).Month;
							this.CurrentMonth = this.CurrentMonth.AddMonths(num3);
							break;
						}
					}
				}
				this.month_title_click_location = Point.Empty;
			}
		}

		// Token: 0x0600288E RID: 10382 RVA: 0x0009C7D0 File Offset: 0x0009A9D0
		private void TimerHandler(object sender, EventArgs e)
		{
			if (base.Capture)
			{
				MonthCalendar.HitTestInfo hitTestInfo = this.HitTest(base.PointToClient(Control.MousePosition));
				if (this.click_state[1] || this.click_state[2])
				{
					this.DoMouseUp();
					if (hitTestInfo.HitArea == MonthCalendar.HitArea.PrevMonthButton || hitTestInfo.HitArea == MonthCalendar.HitArea.NextMonthButton)
					{
						this.DoButtonMouseDown(hitTestInfo);
						this.click_state[1] = hitTestInfo.HitArea == MonthCalendar.HitArea.PrevMonthButton;
						this.click_state[2] = !this.click_state[1];
					}
					if (this.timer.Interval != 300)
					{
						this.timer.Interval = 300;
					}
				}
			}
			else
			{
				this.timer.Enabled = false;
			}
		}

		// Token: 0x0600288F RID: 10383 RVA: 0x0009C894 File Offset: 0x0009AA94
		private void DoButtonMouseDown(MonthCalendar.HitTestInfo hti)
		{
			this.SetItemClick(hti);
			if (hti.HitArea == MonthCalendar.HitArea.PrevMonthButton)
			{
				base.Invalidate(new Rectangle(base.ClientRectangle.X + 1 + this.button_x_offset, base.ClientRectangle.Y + 1 + (this.title_size.Height - this.button_size.Height) / 2, this.button_size.Width, this.button_size.Height));
				int num = ((this.scroll_change != 0) ? this.scroll_change : (this.CalendarDimensions.Width * this.CalendarDimensions.Height));
				this.CurrentMonth = this.CurrentMonth.AddMonths(-num);
			}
			else
			{
				base.Invalidate(new Rectangle(base.ClientRectangle.Right - 1 - this.button_x_offset - this.button_size.Width, base.ClientRectangle.Y + 1 + (this.title_size.Height - this.button_size.Height) / 2, this.button_size.Width, this.button_size.Height));
				int num2 = ((this.scroll_change != 0) ? this.scroll_change : (this.CalendarDimensions.Width * this.CalendarDimensions.Height));
				this.CurrentMonth = this.CurrentMonth.AddMonths(num2);
			}
		}

		// Token: 0x06002890 RID: 10384 RVA: 0x0009CA24 File Offset: 0x0009AC24
		private void DoDateMouseDown(MonthCalendar.HitTestInfo hti)
		{
			this.SetItemClick(hti);
		}

		// Token: 0x06002891 RID: 10385 RVA: 0x0009CA30 File Offset: 0x0009AC30
		private void DoMouseUp()
		{
			this.IsYearGoingDown = false;
			this.IsYearGoingUp = false;
			this.is_mouse_moving_year = false;
			if (this.is_next_clicked)
			{
				base.Invalidate(new Rectangle(base.ClientRectangle.Right - 1 - this.button_x_offset - this.button_size.Width, base.ClientRectangle.Y + 1 + (this.title_size.Height - this.button_size.Height) / 2, this.button_size.Width, this.button_size.Height));
			}
			if (this.is_previous_clicked)
			{
				base.Invalidate(new Rectangle(base.ClientRectangle.X + 1 + this.button_x_offset, base.ClientRectangle.Y + 1 + (this.title_size.Height - this.button_size.Height) / 2, this.button_size.Width, this.button_size.Height));
			}
			if (this.is_date_clicked)
			{
				this.InvalidateDateRange(new SelectionRange(this.clicked_date, this.clicked_date));
			}
			this.is_previous_clicked = false;
			this.is_next_clicked = false;
			this.is_date_clicked = false;
		}

		// Token: 0x06002892 RID: 10386 RVA: 0x0009CB70 File Offset: 0x0009AD70
		private void UpDownTimerTick(object sender, EventArgs e)
		{
			if (this.IsYearGoingUp)
			{
				this.IsYearGoingUp = true;
			}
			if (this.IsYearGoingDown)
			{
				this.IsYearGoingDown = true;
			}
			if (!this.IsYearGoingDown && !this.IsYearGoingUp)
			{
				this.updown_timer.Enabled = false;
			}
			else if (this.IsYearGoingDown || this.IsYearGoingUp)
			{
				this.updown_timer.Interval = 100;
			}
		}

		// Token: 0x06002893 RID: 10387 RVA: 0x0009CBEC File Offset: 0x0009ADEC
		private void StartHideTimer()
		{
			if (this.updown_timer == null)
			{
				this.updown_timer = new Timer();
				this.updown_timer.Tick += new EventHandler(this.UpDownTimerTick);
			}
			this.updown_timer.Interval = 500;
			this.updown_timer.Enabled = true;
		}

		// Token: 0x06002894 RID: 10388 RVA: 0x0009CC44 File Offset: 0x0009AE44
		private void MouseMoveHandler(object sender, MouseEventArgs e)
		{
			MonthCalendar.HitTestInfo hitTestInfo = this.HitTest(e.X, e.Y);
			if (this.click_state[0] && (hitTestInfo.HitArea == MonthCalendar.HitArea.PrevMonthDate || hitTestInfo.HitArea == MonthCalendar.HitArea.NextMonthDate || hitTestInfo.HitArea == MonthCalendar.HitArea.Date))
			{
				Rectangle rectangle = this.clicked_rect;
				DateTime dateTime = this.clicked_date;
				this.DoDateMouseDown(hitTestInfo);
				if (this.owner == null)
				{
					this.click_state[0] = true;
				}
				else
				{
					this.click_state[0] = false;
					this.click_state[1] = false;
					this.click_state[2] = false;
				}
				if (dateTime != this.clicked_date)
				{
					this.SelectDate(this.clicked_date);
					this.date_selected_event_pending = true;
					Rectangle rectangle2 = Rectangle.Union(rectangle, this.clicked_rect);
					base.Invalidate(rectangle2);
				}
			}
		}

		// Token: 0x06002895 RID: 10389 RVA: 0x0009CD18 File Offset: 0x0009AF18
		private void MouseDownHandler(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.None)
			{
				return;
			}
			this.click_state[0] = false;
			this.click_state[1] = false;
			this.click_state[2] = false;
			if (this.timer.Enabled)
			{
				this.timer.Stop();
				this.timer.Enabled = false;
			}
			Point point;
			point..ctor(e.X, e.Y);
			if (this.owner != null && !base.ClientRectangle.Contains(point))
			{
				this.owner.HideMonthCalendar();
				return;
			}
			MonthCalendar.HitTestInfo hitTestInfo = this.HitTest(point);
			if (this.ShowYearUpDown && hitTestInfo.HitArea != MonthCalendar.HitArea.TitleYear)
			{
				this.ShowYearUpDown = false;
			}
			switch (hitTestInfo.HitArea)
			{
			case MonthCalendar.HitArea.TitleMonth:
				this.month_title_click_location = hitTestInfo.Point;
				this.month_menu.Show(this, hitTestInfo.Point);
				if (base.Capture && this.owner != null)
				{
					base.Capture = false;
					base.Capture = true;
				}
				return;
			case MonthCalendar.HitArea.TitleYear:
				if (this.ShowYearUpDown)
				{
					if (hitTestInfo.hit_area_extra == MonthCalendar.HitAreaExtra.UpButton)
					{
						this.is_mouse_moving_year = true;
						this.IsYearGoingUp = true;
					}
					else if (hitTestInfo.hit_area_extra == MonthCalendar.HitAreaExtra.DownButton)
					{
						this.is_mouse_moving_year = true;
						this.IsYearGoingDown = true;
					}
					return;
				}
				this.ShowYearUpDown = true;
				return;
			case MonthCalendar.HitArea.NextMonthButton:
			case MonthCalendar.HitArea.PrevMonthButton:
				this.DoButtonMouseDown(hitTestInfo);
				this.click_state[1] = hitTestInfo.HitArea == MonthCalendar.HitArea.PrevMonthDate;
				this.click_state[2] = !this.click_state[1];
				this.timer.Interval = 750;
				this.timer.Start();
				return;
			case MonthCalendar.HitArea.Date:
			case MonthCalendar.HitArea.NextMonthDate:
			case MonthCalendar.HitArea.PrevMonthDate:
				this.DoDateMouseDown(hitTestInfo);
				this.SelectDate(this.clicked_date);
				this.date_selected_event_pending = true;
				if (this.owner == null)
				{
					this.click_state[0] = true;
				}
				else
				{
					this.click_state[0] = false;
					this.click_state[1] = false;
					this.click_state[2] = false;
				}
				return;
			case MonthCalendar.HitArea.TodayLink:
				this.SetSelectionRange(DateTime.Now.Date, DateTime.Now.Date);
				this.OnDateSelected(new DateRangeEventArgs(this.SelectionStart, this.SelectionEnd));
				return;
			}
			this.is_previous_clicked = false;
			this.is_next_clicked = false;
			this.is_date_clicked = false;
		}

		// Token: 0x06002896 RID: 10390 RVA: 0x0009CFAC File Offset: 0x0009B1AC
		private void KeyDownHandler(object sender, KeyEventArgs e)
		{
			if (this.ShowYearUpDown)
			{
				Keys keys = e.KeyCode;
				switch (keys)
				{
				case Keys.Up:
					this.IsYearGoingUp = true;
					break;
				default:
					if (keys == Keys.Return)
					{
						this.ShowYearUpDown = false;
						this.IsYearGoingDown = false;
						this.IsYearGoingUp = false;
					}
					break;
				case Keys.Down:
					this.IsYearGoingDown = true;
					break;
				}
			}
			else
			{
				if (!this.is_shift_pressed && e.Shift)
				{
					this.first_select_start_date = this.SelectionStart;
					this.is_shift_pressed = e.Shift;
					e.Handled = true;
				}
				Keys keys = e.KeyCode;
				switch (keys)
				{
				case Keys.PageUp:
					if (this.is_shift_pressed)
					{
						this.AddTimeToSelection(-1, false);
					}
					else
					{
						DateTime dateTime = this.SelectionStart.AddMonths(-1);
						this.SetSelectionRange(dateTime, dateTime);
					}
					e.Handled = true;
					break;
				case Keys.PageDown:
					if (this.is_shift_pressed)
					{
						this.AddTimeToSelection(1, false);
					}
					else
					{
						DateTime dateTime2 = this.SelectionStart.AddMonths(1);
						this.SetSelectionRange(dateTime2, dateTime2);
					}
					e.Handled = true;
					break;
				case Keys.End:
					if (this.is_shift_pressed)
					{
						DateTime dateTime3 = this.GetLastDateInMonth(this.first_select_start_date);
						if (dateTime3 > this.first_select_start_date.AddDays((double)(this.MaxSelectionCount - 1)))
						{
							dateTime3 = this.first_select_start_date.AddDays((double)(this.MaxSelectionCount - 1));
						}
						this.SetSelectionRange(dateTime3, this.first_select_start_date);
					}
					else
					{
						DateTime lastDateInMonth = this.GetLastDateInMonth(this.SelectionStart);
						this.SetSelectionRange(lastDateInMonth, lastDateInMonth);
					}
					e.Handled = true;
					break;
				case Keys.Home:
					if (this.is_shift_pressed)
					{
						DateTime dateTime4 = this.GetFirstDateInMonth(this.first_select_start_date);
						if (dateTime4 < this.first_select_start_date.AddDays((double)((this.MaxSelectionCount - 1) * -1)))
						{
							dateTime4 = this.first_select_start_date.AddDays((double)((this.MaxSelectionCount - 1) * -1));
						}
						this.SetSelectionRange(dateTime4, this.first_select_start_date);
					}
					else
					{
						DateTime firstDateInMonth = this.GetFirstDateInMonth(this.SelectionStart);
						this.SetSelectionRange(firstDateInMonth, firstDateInMonth);
					}
					e.Handled = true;
					break;
				case Keys.Left:
					if (this.is_shift_pressed)
					{
						this.AddTimeToSelection(-1, true);
					}
					else
					{
						DateTime dateTime5 = this.SelectionStart.AddDays(-1.0);
						this.SetSelectionRange(dateTime5, dateTime5);
					}
					e.Handled = true;
					break;
				case Keys.Up:
					if (this.is_shift_pressed)
					{
						this.AddTimeToSelection(-7, true);
					}
					else
					{
						DateTime dateTime6 = this.SelectionStart.AddDays(-7.0);
						this.SetSelectionRange(dateTime6, dateTime6);
					}
					e.Handled = true;
					break;
				case Keys.Right:
					if (this.is_shift_pressed)
					{
						this.AddTimeToSelection(1, true);
					}
					else
					{
						DateTime dateTime7 = this.SelectionStart.AddDays(1.0);
						this.SetSelectionRange(dateTime7, dateTime7);
					}
					e.Handled = true;
					break;
				case Keys.Down:
					if (this.is_shift_pressed)
					{
						this.AddTimeToSelection(7, true);
					}
					else
					{
						DateTime dateTime8 = this.SelectionStart.AddDays(7.0);
						this.SetSelectionRange(dateTime8, dateTime8);
					}
					e.Handled = true;
					break;
				default:
					if (keys == Keys.F4)
					{
						if (e.Alt && this.owner != null)
						{
							base.Hide();
							e.Handled = true;
						}
					}
					break;
				}
			}
		}

		// Token: 0x06002897 RID: 10391 RVA: 0x0009D36C File Offset: 0x0009B56C
		private void MouseUpHandler(object sender, MouseEventArgs e)
		{
			if ((e.Button & MouseButtons.Left) == MouseButtons.None)
			{
				if (this.show_today && this.ContextMenu == null)
				{
					this.today_menu.Show(this, new Point(e.X, e.Y));
				}
				return;
			}
			if (this.timer.Enabled)
			{
				this.timer.Stop();
			}
			this.click_state[0] = false;
			this.click_state[1] = false;
			this.click_state[2] = false;
			this.DoMouseUp();
			if (this.date_selected_event_pending)
			{
				this.OnDateSelected(new DateRangeEventArgs(this.SelectionStart, this.SelectionEnd));
				this.date_selected_event_pending = false;
			}
		}

		// Token: 0x06002898 RID: 10392 RVA: 0x0009D424 File Offset: 0x0009B624
		private void KeyUpHandler(object sender, KeyEventArgs e)
		{
			this.is_shift_pressed = e.Shift;
			e.Handled = true;
			this.IsYearGoingUp = false;
			this.IsYearGoingDown = false;
		}

		// Token: 0x06002899 RID: 10393 RVA: 0x0009D454 File Offset: 0x0009B654
		private void PaintHandler(object sender, PaintEventArgs pe)
		{
			if (base.Width <= 0 || base.Height <= 0 || !base.Visible)
			{
				return;
			}
			this.Draw(pe.ClipRectangle, pe.Graphics);
			if (this.Paint != null)
			{
				this.Paint(sender, pe);
			}
		}

		// Token: 0x0600289A RID: 10394 RVA: 0x0009D4B0 File Offset: 0x0009B6B0
		private void InvalidateDateRange(SelectionRange range)
		{
			SelectionRange displayRange = this.GetDisplayRange(false);
			if (range.End < displayRange.Start || range.Start > displayRange.End)
			{
				return;
			}
			if (range.Start < displayRange.Start)
			{
				range = new SelectionRange(displayRange.Start, range.End);
			}
			if (range.End > displayRange.End)
			{
				range = new SelectionRange(range.Start, displayRange.End);
			}
			DateTime dateTime = this.current_month.AddMonths(this.CalendarDimensions.Width * this.CalendarDimensions.Height).AddDays(-1.0);
			DateTime dateTime2 = range.Start;
			while (dateTime2 <= range.End)
			{
				DateTime dateTime3;
				dateTime3..ctor(dateTime2.Year, dateTime2.Month, 1);
				DateTime dateTime4 = dateTime3.AddMonths(1).AddDays(-1.0);
				Rectangle rectangle;
				Rectangle rectangle2;
				if (range.End <= dateTime4 && dateTime2 < dateTime)
				{
					if (dateTime2 < this.current_month)
					{
						rectangle = this.GetDateRowRect(this.current_month, this.current_month);
					}
					else
					{
						rectangle = this.GetDateRowRect(dateTime2, dateTime2);
					}
					rectangle2 = this.GetDateRowRect(dateTime2, range.End);
				}
				else if (dateTime2 < dateTime)
				{
					rectangle = this.GetDateRowRect(dateTime2, dateTime2);
					rectangle2 = this.GetDateRowRect(dateTime4, dateTime4);
				}
				else
				{
					rectangle = this.GetDateRowRect(dateTime, dateTime.AddDays(1.0));
					rectangle2 = this.GetDateRowRect(dateTime, range.End);
				}
				dateTime2 = dateTime4.AddDays(1.0);
				base.Invalidate(new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, Math.Max(rectangle2.Bottom - rectangle.Y, 0)));
			}
		}

		// Token: 0x0600289B RID: 10395 RVA: 0x0009D6C8 File Offset: 0x0009B8C8
		private Rectangle GetDateRowRect(DateTime month, DateTime date)
		{
			Size singleMonthSize = this.SingleMonthSize;
			Rectangle empty = Rectangle.Empty;
			for (int i = 0; i < this.CalendarDimensions.Width * this.CalendarDimensions.Height; i++)
			{
				DateTime dateTime = this.current_month.AddMonths(i);
				if (month.Year == dateTime.Year && month.Month == dateTime.Month)
				{
					empty..ctor(base.ClientRectangle.X + 1 + singleMonthSize.Width * (i % this.CalendarDimensions.Width) + this.calendar_spacing.Width * (i % this.CalendarDimensions.Width), base.ClientRectangle.Y + 1 + singleMonthSize.Height * (i / this.CalendarDimensions.Width) + this.calendar_spacing.Height * (i / this.CalendarDimensions.Width), singleMonthSize.Width, singleMonthSize.Height);
					break;
				}
			}
			if (empty == Rectangle.Empty)
			{
				return Rectangle.Empty;
			}
			int num = -1;
			DateTime dateTime2 = this.GetFirstDateInMonthGrid(month);
			DateTime dateTime3 = dateTime2.AddDays(7.0);
			for (int j = 0; j < 6; j++)
			{
				if (date >= dateTime2 && date < dateTime3)
				{
					num = j;
					break;
				}
				dateTime2 = dateTime3;
				dateTime3 = dateTime3.AddDays(7.0);
			}
			if (num < 0)
			{
				return Rectangle.Empty;
			}
			int num2 = ((!this.ShowWeekNumbers) ? 0 : this.date_cell_size.Width);
			int num3 = this.title_size.Height + this.date_cell_size.Height * (num + 1);
			return new Rectangle(empty.X + num2, empty.Y + num3, this.date_cell_size.Width * 7, this.date_cell_size.Height);
		}

		// Token: 0x0600289C RID: 10396 RVA: 0x0009D8F0 File Offset: 0x0009BAF0
		internal void Draw(Rectangle clip_rect, Graphics dc)
		{
			ThemeEngine.Current.DrawMonthCalendar(dc, clip_rect, this);
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x0600289D RID: 10397 RVA: 0x0009D900 File Offset: 0x0009BB00
		// (set) Token: 0x0600289E RID: 10398 RVA: 0x0009D908 File Offset: 0x0009BB08
		internal override bool InternalCapture
		{
			get
			{
				return base.InternalCapture;
			}
			set
			{
				if (this.owner == null)
				{
					base.InternalCapture = value;
				}
			}
		}

		// Token: 0x0600289F RID: 10399 RVA: 0x0009D91C File Offset: 0x0009BB1C
		private void OnUIAMaxSelectionCountChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[MonthCalendar.UIAMaxSelectionCountChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x060028A0 RID: 10400 RVA: 0x0009D954 File Offset: 0x0009BB54
		private void OnUIASelectionChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[MonthCalendar.UIASelectionChangedEvent];
			if (eventHandler != null)
			{
				eventHandler.Invoke(this, EventArgs.Empty);
			}
		}

		// Token: 0x04001405 RID: 5125
		private const int initial_delay = 500;

		// Token: 0x04001406 RID: 5126
		private const int subsequent_delay = 100;

		// Token: 0x04001407 RID: 5127
		private ArrayList annually_bolded_dates;

		// Token: 0x04001408 RID: 5128
		private ArrayList monthly_bolded_dates;

		// Token: 0x04001409 RID: 5129
		private ArrayList bolded_dates;

		// Token: 0x0400140A RID: 5130
		private Size calendar_dimensions;

		// Token: 0x0400140B RID: 5131
		private Day first_day_of_week;

		// Token: 0x0400140C RID: 5132
		private DateTime max_date;

		// Token: 0x0400140D RID: 5133
		private int max_selection_count;

		// Token: 0x0400140E RID: 5134
		private DateTime min_date;

		// Token: 0x0400140F RID: 5135
		private int scroll_change;

		// Token: 0x04001410 RID: 5136
		private SelectionRange selection_range;

		// Token: 0x04001411 RID: 5137
		private bool show_today;

		// Token: 0x04001412 RID: 5138
		private bool show_today_circle;

		// Token: 0x04001413 RID: 5139
		private bool show_week_numbers;

		// Token: 0x04001414 RID: 5140
		private Color title_back_color;

		// Token: 0x04001415 RID: 5141
		private Color title_fore_color;

		// Token: 0x04001416 RID: 5142
		private DateTime today_date;

		// Token: 0x04001417 RID: 5143
		private bool today_date_set;

		// Token: 0x04001418 RID: 5144
		private Color trailing_fore_color;

		// Token: 0x04001419 RID: 5145
		private ContextMenu today_menu;

		// Token: 0x0400141A RID: 5146
		private ContextMenu month_menu;

		// Token: 0x0400141B RID: 5147
		private Timer timer;

		// Token: 0x0400141C RID: 5148
		private Timer updown_timer;

		// Token: 0x0400141D RID: 5149
		private bool is_year_going_up;

		// Token: 0x0400141E RID: 5150
		private bool is_year_going_down;

		// Token: 0x0400141F RID: 5151
		private bool is_mouse_moving_year;

		// Token: 0x04001420 RID: 5152
		private int year_moving_count;

		// Token: 0x04001421 RID: 5153
		private bool date_selected_event_pending;

		// Token: 0x04001422 RID: 5154
		private bool right_to_left_layout;

		// Token: 0x04001423 RID: 5155
		internal bool show_year_updown;

		// Token: 0x04001424 RID: 5156
		internal DateTime current_month;

		// Token: 0x04001425 RID: 5157
		internal DateTimePicker owner;

		// Token: 0x04001426 RID: 5158
		internal int button_x_offset;

		// Token: 0x04001427 RID: 5159
		internal Size button_size;

		// Token: 0x04001428 RID: 5160
		internal Size title_size;

		// Token: 0x04001429 RID: 5161
		internal Size date_cell_size;

		// Token: 0x0400142A RID: 5162
		internal Size calendar_spacing;

		// Token: 0x0400142B RID: 5163
		internal int divider_line_offset;

		// Token: 0x0400142C RID: 5164
		internal DateTime clicked_date;

		// Token: 0x0400142D RID: 5165
		internal Rectangle clicked_rect;

		// Token: 0x0400142E RID: 5166
		internal bool is_date_clicked;

		// Token: 0x0400142F RID: 5167
		internal bool is_previous_clicked;

		// Token: 0x04001430 RID: 5168
		internal bool is_next_clicked;

		// Token: 0x04001431 RID: 5169
		internal bool is_shift_pressed;

		// Token: 0x04001432 RID: 5170
		internal DateTime first_select_start_date;

		// Token: 0x04001433 RID: 5171
		internal int last_clicked_calendar_index;

		// Token: 0x04001434 RID: 5172
		internal Rectangle last_clicked_calendar_rect;

		// Token: 0x04001435 RID: 5173
		internal Font bold_font;

		// Token: 0x04001436 RID: 5174
		internal StringFormat centered_format;

		// Token: 0x04001437 RID: 5175
		private Point month_title_click_location;

		// Token: 0x04001438 RID: 5176
		private bool[] click_state;

		/// <summary>Defines constants that represent areas in a <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
		// Token: 0x0200026C RID: 620
		public enum HitArea
		{
			/// <summary>The specified point is either not on the month calendar control, or it is in an inactive portion of the control.</summary>
			// Token: 0x04001440 RID: 5184
			Nowhere,
			/// <summary>The specified point is over the background of a month's title.</summary>
			// Token: 0x04001441 RID: 5185
			TitleBackground,
			/// <summary>The specified point is in a month's title bar, over a month name.</summary>
			// Token: 0x04001442 RID: 5186
			TitleMonth,
			/// <summary>The specified point is in a month's title bar, over the year value.</summary>
			// Token: 0x04001443 RID: 5187
			TitleYear,
			/// <summary>The specified point is over the button at the upper-right corner of the control. If the user clicks here, the month calendar scrolls its display to the next month or set of months.</summary>
			// Token: 0x04001444 RID: 5188
			NextMonthButton,
			/// <summary>The specified point is over the button at the upper-left corner of the control. If the user clicks here, the month calendar scrolls its display to the previous month or set of months.</summary>
			// Token: 0x04001445 RID: 5189
			PrevMonthButton,
			/// <summary>The specified point is part of the calendar's background.</summary>
			// Token: 0x04001446 RID: 5190
			CalendarBackground,
			/// <summary>The specified point is on a date within the calendar. The <see cref="P:System.Windows.Forms.MonthCalendar.HitTestInfo.Time" /> property of <see cref="T:System.Windows.Forms.MonthCalendar.HitTestInfo" /> is set to the date at the specified point.</summary>
			// Token: 0x04001447 RID: 5191
			Date,
			/// <summary>The specified point is over a date from the next month (partially displayed at the top of the currently displayed month). If the user clicks here, the month calendar scrolls its display to the next month or set of months.</summary>
			// Token: 0x04001448 RID: 5192
			NextMonthDate,
			/// <summary>The specified point is over a date from the previous month (partially displayed at the top of the currently displayed month). If the user clicks here, the month calendar scrolls its display to the previous month or set of months.</summary>
			// Token: 0x04001449 RID: 5193
			PrevMonthDate,
			/// <summary>The specified point is over a day abbreviation ("Fri", for example). The <see cref="P:System.Windows.Forms.MonthCalendar.HitTestInfo.Time" /> property of <see cref="T:System.Windows.Forms.MonthCalendar.HitTestInfo" /> is set to January 1, 0001.</summary>
			// Token: 0x0400144A RID: 5194
			DayOfWeek,
			/// <summary>The specified point is over a week number. This occurs only if the <see cref="P:System.Windows.Forms.MonthCalendar.ShowWeekNumbers" /> property of <see cref="T:System.Windows.Forms.MonthCalendar" /> is enabled. The <see cref="P:System.Windows.Forms.MonthCalendar.HitTestInfo.Time" /> property of <see cref="T:System.Windows.Forms.MonthCalendar.HitTestInfo" /> is set to the corresponding date in the leftmost column.</summary>
			// Token: 0x0400144B RID: 5195
			WeekNumbers,
			/// <summary>The specified point is on the today link at the bottom of the month calendar control.</summary>
			// Token: 0x0400144C RID: 5196
			TodayLink
		}

		// Token: 0x0200026D RID: 621
		internal enum HitAreaExtra
		{
			// Token: 0x0400144E RID: 5198
			YearRectangle,
			// Token: 0x0400144F RID: 5199
			UpButton,
			// Token: 0x04001450 RID: 5200
			DownButton
		}

		/// <summary>Contains information about an area of a <see cref="T:System.Windows.Forms.MonthCalendar" /> control. This class cannot be inherited.</summary>
		// Token: 0x0200026E RID: 622
		public sealed class HitTestInfo
		{
			// Token: 0x060028A1 RID: 10401 RVA: 0x0009D98C File Offset: 0x0009BB8C
			internal HitTestInfo()
			{
				this.hit_area = MonthCalendar.HitArea.Nowhere;
				this.point = new Point(0, 0);
				this.time = DateTime.Now;
			}

			// Token: 0x060028A2 RID: 10402 RVA: 0x0009D9B4 File Offset: 0x0009BBB4
			internal HitTestInfo(MonthCalendar.HitArea hit_area, Point point, DateTime time)
			{
				this.hit_area = hit_area;
				this.point = point;
				this.time = time;
				this.hit_time = time;
			}

			// Token: 0x060028A3 RID: 10403 RVA: 0x0009D9E4 File Offset: 0x0009BBE4
			internal HitTestInfo(MonthCalendar.HitArea hit_area, Point point, DateTime time, DateTime hit_time)
			{
				this.hit_area = hit_area;
				this.point = point;
				this.time = time;
				this.hit_time = hit_time;
			}

			// Token: 0x060028A4 RID: 10404 RVA: 0x0009DA0C File Offset: 0x0009BC0C
			internal HitTestInfo(MonthCalendar.HitArea hit_area, Point point, DateTime time, MonthCalendar.HitAreaExtra hit_area_extra)
			{
				this.hit_area = hit_area;
				this.hit_area_extra = hit_area_extra;
				this.point = point;
				this.time = time;
			}

			/// <summary>Gets the <see cref="T:System.Windows.Forms.MonthCalendar.HitArea" /> that represents the area of the calendar evaluated by the hit-test operation.</summary>
			/// <returns>One of the <see cref="T:System.Windows.Forms.MonthCalendar.HitArea" /> values. The default is <see cref="F:System.Windows.Forms.MonthCalendar.HitArea.Nowhere" />.</returns>
			// Token: 0x170009F2 RID: 2546
			// (get) Token: 0x060028A5 RID: 10405 RVA: 0x0009DA34 File Offset: 0x0009BC34
			public MonthCalendar.HitArea HitArea
			{
				get
				{
					return this.hit_area;
				}
			}

			/// <summary>Gets the point that was hit-tested.</summary>
			/// <returns>A <see cref="T:System.Drawing.Point" /> containing the <see cref="P:System.Drawing.Point.X" /> and <see cref="P:System.Drawing.Point.Y" /> values tested.</returns>
			// Token: 0x170009F3 RID: 2547
			// (get) Token: 0x060028A6 RID: 10406 RVA: 0x0009DA3C File Offset: 0x0009BC3C
			public Point Point
			{
				get
				{
					return this.point;
				}
			}

			/// <summary>Gets the time information specific to the location that was hit-tested.</summary>
			/// <returns>A <see cref="T:System.DateTime" />.</returns>
			// Token: 0x170009F4 RID: 2548
			// (get) Token: 0x060028A7 RID: 10407 RVA: 0x0009DA44 File Offset: 0x0009BC44
			public DateTime Time
			{
				get
				{
					return this.time;
				}
			}

			// Token: 0x04001451 RID: 5201
			private MonthCalendar.HitArea hit_area;

			// Token: 0x04001452 RID: 5202
			private Point point;

			// Token: 0x04001453 RID: 5203
			private DateTime time;

			// Token: 0x04001454 RID: 5204
			internal MonthCalendar.HitAreaExtra hit_area_extra;

			// Token: 0x04001455 RID: 5205
			internal DateTime hit_time;
		}
	}
}
