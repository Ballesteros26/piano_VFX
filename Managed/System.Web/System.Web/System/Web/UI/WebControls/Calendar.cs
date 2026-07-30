using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000344 RID: 836
	[SupportsEventValidation]
	[ControlValueProperty("SelectedDate", "1/1/0001 12:00:00 AM")]
	[DefaultEvent("SelectionChanged")]
	[Designer("System.Web.UI.Design.WebControls.CalendarDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("SelectedDate")]
	[DataBindingHandler("System.Web.UI.Design.WebControls.CalendarDataBindingHandler, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class Calendar : WebControl, IPostBackEventHandler
	{
		/// <summary>Gets or sets a text value that is rendered as a caption for the calendar.</summary>
		/// <returns>The table caption.</returns>
		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06001DEA RID: 7658 RVA: 0x00047A03 File Offset: 0x00045C03
		// (set) Token: 0x06001DEB RID: 7659 RVA: 0x0004AA49 File Offset: 0x00048C49
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string Caption
		{
			get
			{
				return this.ViewState.GetString("Caption", string.Empty);
			}
			set
			{
				this.ViewState["Caption"] = value;
			}
		}

		/// <summary>Gets or sets the alignment of the text that is rendered as a caption for the calendar.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> value that indicates the alignment of the caption. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is not one of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> values.</exception>
		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x00047A41 File Offset: 0x00045C41
		// (set) Token: 0x06001DED RID: 7661 RVA: 0x0004AA5C File Offset: 0x00048C5C
		[WebCategory("Accessibility")]
		[WebSysDescription("")]
		[DefaultValue(TableCaptionAlign.NotSet)]
		public virtual TableCaptionAlign CaptionAlign
		{
			get
			{
				return (TableCaptionAlign)this.ViewState.GetInt("CaptionAlign", 0);
			}
			set
			{
				this.ViewState["CaptionAlign"] = value;
			}
		}

		/// <summary>Gets or sets the amount of space between the contents of a cell and the cell's border.</summary>
		/// <returns>The amount of space (in pixels) between the contents of a cell and the cell's border. The default value is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified cell padding is less than -1. </exception>
		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x0004AA74 File Offset: 0x00048C74
		// (set) Token: 0x06001DEF RID: 7663 RVA: 0x0004AA87 File Offset: 0x00048C87
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[DefaultValue(2)]
		public int CellPadding
		{
			get
			{
				return this.ViewState.GetInt("CellPadding", 2);
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("The specified cell padding is less than -1.");
				}
				this.ViewState["CellPadding"] = value;
			}
		}

		/// <summary>Gets or sets the amount of space between cells.</summary>
		/// <returns>The amount of space (in pixels) between cells. The default value is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified cell spacing is less than -1. </exception>
		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x0004AAAE File Offset: 0x00048CAE
		// (set) Token: 0x06001DF1 RID: 7665 RVA: 0x0004AAC1 File Offset: 0x00048CC1
		[WebSysDescription("")]
		[DefaultValue(0)]
		[WebCategory("Layout")]
		public int CellSpacing
		{
			get
			{
				return this.ViewState.GetInt("CellSpacing", 0);
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("The specified cell spacing is less than -1");
				}
				this.ViewState["CellSpacing"] = value;
			}
		}

		/// <summary>Gets the style properties for the section that displays the day of the week.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the section that displays the day of the week. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x0004AAE8 File Offset: 0x00048CE8
		[WebSysDescription("")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Style")]
		public TableItemStyle DayHeaderStyle
		{
			get
			{
				if (this.dayHeaderStyle == null)
				{
					this.dayHeaderStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.dayHeaderStyle.TrackViewState();
					}
				}
				return this.dayHeaderStyle;
			}
		}

		/// <summary>Gets or sets the name format for days of the week.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.DayNameFormat" /> values. The default value is Short.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified day name format is not one of the <see cref="T:System.Web.UI.WebControls.DayNameFormat" /> values. </exception>
		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06001DF3 RID: 7667 RVA: 0x0004AB16 File Offset: 0x00048D16
		// (set) Token: 0x06001DF4 RID: 7668 RVA: 0x0004AB29 File Offset: 0x00048D29
		[WebSysDescription("")]
		[DefaultValue(DayNameFormat.Short)]
		[WebCategory("Appearance")]
		public DayNameFormat DayNameFormat
		{
			get
			{
				return (DayNameFormat)this.ViewState.GetInt("DayNameFormat", 1);
			}
			set
			{
				if (value != DayNameFormat.FirstLetter && value != DayNameFormat.FirstTwoLetters && value != DayNameFormat.Full && value != DayNameFormat.Short && value != DayNameFormat.Shortest)
				{
					throw new ArgumentOutOfRangeException("The specified day name format is not one of the DayNameFormat values.");
				}
				this.ViewState["DayNameFormat"] = value;
			}
		}

		/// <summary>Gets the style properties for the days in the displayed month.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the days in the displayed month. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06001DF5 RID: 7669 RVA: 0x0004AB5F File Offset: 0x00048D5F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		[DefaultValue(null)]
		public TableItemStyle DayStyle
		{
			get
			{
				if (this.dayStyle == null)
				{
					this.dayStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.dayStyle.TrackViewState();
					}
				}
				return this.dayStyle;
			}
		}

		/// <summary>Gets or sets the day of the week to display in the first day column of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.FirstDayOfWeek" /> values. The default is Default, which indicates that the day specified in the system setting is used.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The date specified is not one of the <see cref="T:System.Web.UI.WebControls.FirstDayOfWeek" /> values. </exception>
		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x0004AB8D File Offset: 0x00048D8D
		// (set) Token: 0x06001DF7 RID: 7671 RVA: 0x0004ABA0 File Offset: 0x00048DA0
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[DefaultValue(FirstDayOfWeek.Default)]
		public FirstDayOfWeek FirstDayOfWeek
		{
			get
			{
				return (FirstDayOfWeek)this.ViewState.GetInt("FirstDayOfWeek", 7);
			}
			set
			{
				if (value < FirstDayOfWeek.Sunday || value > FirstDayOfWeek.Default)
				{
					throw new ArgumentOutOfRangeException("The specified day name format is not one of the DayNameFormat values.");
				}
				this.ViewState["FirstDayOfWeek"] = value;
			}
		}

		/// <summary>Gets or sets the text displayed for the next month navigation control.</summary>
		/// <returns>The caption text for the next month navigation control. The default value is "&amp;gt;", which is rendered as the greater than sign (&gt;).</returns>
		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06001DF8 RID: 7672 RVA: 0x0004ABCB File Offset: 0x00048DCB
		// (set) Token: 0x06001DF9 RID: 7673 RVA: 0x0004ABE2 File Offset: 0x00048DE2
		[DefaultValue("&gt;")]
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public string NextMonthText
		{
			get
			{
				return this.ViewState.GetString("NextMonthText", "&gt;");
			}
			set
			{
				this.ViewState["NextMonthText"] = value;
			}
		}

		/// <summary>Gets or sets the format of the next and previous month navigation elements in the title section of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.NextPrevFormat" /> values. The default value is CustomText.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified format is not one of the <see cref="T:System.Web.UI.WebControls.NextPrevFormat" /> values. </exception>
		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06001DFA RID: 7674 RVA: 0x0004ABF5 File Offset: 0x00048DF5
		// (set) Token: 0x06001DFB RID: 7675 RVA: 0x0004AC08 File Offset: 0x00048E08
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DefaultValue(NextPrevFormat.CustomText)]
		public NextPrevFormat NextPrevFormat
		{
			get
			{
				return (NextPrevFormat)this.ViewState.GetInt("NextPrevFormat", 0);
			}
			set
			{
				if (value != NextPrevFormat.CustomText && value != NextPrevFormat.ShortMonth && value != NextPrevFormat.FullMonth)
				{
					throw new ArgumentOutOfRangeException("The specified day name format is not one of the DayNameFormat values.");
				}
				this.ViewState["NextPrevFormat"] = value;
			}
		}

		/// <summary>Gets the style properties for the next and previous month navigation elements.</summary>
		/// <returns>The style properties for the next and previous month navigation elements. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06001DFC RID: 7676 RVA: 0x0004AC36 File Offset: 0x00048E36
		[WebCategory("Style")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[WebSysDescription("")]
		public TableItemStyle NextPrevStyle
		{
			get
			{
				if (this.nextPrevStyle == null)
				{
					this.nextPrevStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.nextPrevStyle.TrackViewState();
					}
				}
				return this.nextPrevStyle;
			}
		}

		/// <summary>Gets the style properties for the days on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control that are not in the displayed month.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the days on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control that are not in the displayed month. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06001DFD RID: 7677 RVA: 0x0004AC64 File Offset: 0x00048E64
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle OtherMonthDayStyle
		{
			get
			{
				if (this.otherMonthDayStyle == null)
				{
					this.otherMonthDayStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.otherMonthDayStyle.TrackViewState();
					}
				}
				return this.otherMonthDayStyle;
			}
		}

		/// <summary>Gets or sets the text displayed for the previous month navigation control.</summary>
		/// <returns>The caption text for the previous month navigation control. The default value is "&amp;lt;", which is rendered as the less than sign (&lt;).</returns>
		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06001DFE RID: 7678 RVA: 0x0004AC92 File Offset: 0x00048E92
		// (set) Token: 0x06001DFF RID: 7679 RVA: 0x0004ACA9 File Offset: 0x00048EA9
		[DefaultValue("&lt;")]
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public string PrevMonthText
		{
			get
			{
				return this.ViewState.GetString("PrevMonthText", "&lt;");
			}
			set
			{
				this.ViewState["PrevMonthText"] = value;
			}
		}

		/// <summary>Gets or sets the selected date.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that represents the selected date. The default value is <see cref="F:System.DateTime.MinValue" />.</returns>
		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x0004ACBC File Offset: 0x00048EBC
		// (set) Token: 0x06001E01 RID: 7681 RVA: 0x0004ACDE File Offset: 0x00048EDE
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[Bindable(true, BindingDirection.TwoWay)]
		[DefaultValue("1/1/0001 12:00:00 AM")]
		public DateTime SelectedDate
		{
			get
			{
				if (this.SelectedDates.Count > 0)
				{
					return this.SelectedDates[0];
				}
				return DateTime.MinValue;
			}
			set
			{
				this.SelectedDates.SelectRange(value, value);
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.DateTime" /> objects that represent the selected dates on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" /> that contains a collection of <see cref="T:System.DateTime" /> objects representing the selected dates on the <see cref="T:System.Web.UI.WebControls.Calendar" />. The default value is an empty <see cref="T:System.Web.UI.WebControls.SelectedDatesCollection" />.</returns>
		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06001E02 RID: 7682 RVA: 0x0004ACED File Offset: 0x00048EED
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public SelectedDatesCollection SelectedDates
		{
			get
			{
				if (this.dateList == null)
				{
					this.dateList = new ArrayList();
				}
				if (this.selectedDatesCollection == null)
				{
					this.selectedDatesCollection = new SelectedDatesCollection(this.dateList);
				}
				return this.selectedDatesCollection;
			}
		}

		/// <summary>Gets the style properties for the selected dates.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the selected dates. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06001E03 RID: 7683 RVA: 0x0004AD21 File Offset: 0x00048F21
		[WebCategory("Style")]
		[WebSysDescription("")]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle SelectedDayStyle
		{
			get
			{
				if (this.selectedDayStyle == null)
				{
					this.selectedDayStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.selectedDayStyle.TrackViewState();
					}
				}
				return this.selectedDayStyle;
			}
		}

		/// <summary>Gets or sets the date selection mode on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control that specifies whether the user can select a single day, a week, or an entire month.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.CalendarSelectionMode" /> values. The default value is Day.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified selection mode is not one of the <see cref="T:System.Web.UI.WebControls.CalendarSelectionMode" /> values. </exception>
		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x0004AD4F File Offset: 0x00048F4F
		// (set) Token: 0x06001E05 RID: 7685 RVA: 0x0004AD62 File Offset: 0x00048F62
		[DefaultValue(CalendarSelectionMode.Day)]
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public CalendarSelectionMode SelectionMode
		{
			get
			{
				return (CalendarSelectionMode)this.ViewState.GetInt("SelectionMode", 1);
			}
			set
			{
				if (value != CalendarSelectionMode.Day && value != CalendarSelectionMode.DayWeek && value != CalendarSelectionMode.DayWeekMonth && value != CalendarSelectionMode.None)
				{
					throw new ArgumentOutOfRangeException("The specified selection mode is not one of the CalendarSelectionMode values.");
				}
				this.ViewState["SelectionMode"] = value;
			}
		}

		/// <summary>Gets or sets the text displayed for the month selection element in the selector column.</summary>
		/// <returns>The text displayed for the month selection element in the selector column. The default value is "&amp;gt;&amp;gt;", which is rendered as two greater than signs (&gt;&gt;).</returns>
		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06001E06 RID: 7686 RVA: 0x0004AD94 File Offset: 0x00048F94
		// (set) Token: 0x06001E07 RID: 7687 RVA: 0x0004ADAB File Offset: 0x00048FAB
		[DefaultValue("&gt;&gt;")]
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public string SelectMonthText
		{
			get
			{
				return this.ViewState.GetString("SelectMonthText", "&gt;&gt;");
			}
			set
			{
				this.ViewState["SelectMonthText"] = value;
			}
		}

		/// <summary>Gets the style properties for the week and month selector column.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the week and month selector column. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06001E08 RID: 7688 RVA: 0x0004ADBE File Offset: 0x00048FBE
		[WebCategory("Style")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebSysDescription("")]
		public TableItemStyle SelectorStyle
		{
			get
			{
				if (this.selectorStyle == null)
				{
					this.selectorStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.selectorStyle.TrackViewState();
					}
				}
				return this.selectorStyle;
			}
		}

		/// <summary>Gets or sets the text displayed for the week selection element in the selector column.</summary>
		/// <returns>The text displayed for the week selection element in the selector column. The default value is "&amp;gt;", which is rendered as a greater than sign (&gt;).</returns>
		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06001E09 RID: 7689 RVA: 0x0004ADEC File Offset: 0x00048FEC
		// (set) Token: 0x06001E0A RID: 7690 RVA: 0x0004AE03 File Offset: 0x00049003
		[Localizable(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[DefaultValue("&gt;")]
		public string SelectWeekText
		{
			get
			{
				return this.ViewState.GetString("SelectWeekText", "&gt;");
			}
			set
			{
				this.ViewState["SelectWeekText"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the heading for the days of the week is displayed.</summary>
		/// <returns>true if the heading for the days of the week is displayed; otherwise, false. The default is true.</returns>
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06001E0B RID: 7691 RVA: 0x0004AE16 File Offset: 0x00049016
		// (set) Token: 0x06001E0C RID: 7692 RVA: 0x0004AE29 File Offset: 0x00049029
		[DefaultValue(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public bool ShowDayHeader
		{
			get
			{
				return this.ViewState.GetBool("ShowDayHeader", true);
			}
			set
			{
				this.ViewState["ShowDayHeader"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the days on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control are separated with gridlines.</summary>
		/// <returns>true if the days on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control are separated with gridlines; otherwise, false. The default value is false.</returns>
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06001E0D RID: 7693 RVA: 0x0004AE41 File Offset: 0x00049041
		// (set) Token: 0x06001E0E RID: 7694 RVA: 0x0004AE54 File Offset: 0x00049054
		[DefaultValue(false)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public bool ShowGridLines
		{
			get
			{
				return this.ViewState.GetBool("ShowGridLines", false);
			}
			set
			{
				this.ViewState["ShowGridLines"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.WebControls.Calendar" /> control displays the next and previous month navigation elements in the title section.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.Calendar" /> displays the next and previous month navigation elements in the title section; otherwise, false. The default value is true.</returns>
		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06001E0F RID: 7695 RVA: 0x0004AE6C File Offset: 0x0004906C
		// (set) Token: 0x06001E10 RID: 7696 RVA: 0x0004AE7F File Offset: 0x0004907F
		[DefaultValue(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public bool ShowNextPrevMonth
		{
			get
			{
				return this.ViewState.GetBool("ShowNextPrevMonth", true);
			}
			set
			{
				this.ViewState["ShowNextPrevMonth"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the title section is displayed.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.Calendar" /> displays the title section; otherwise, false. The default value is true.</returns>
		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06001E11 RID: 7697 RVA: 0x0004AE97 File Offset: 0x00049097
		// (set) Token: 0x06001E12 RID: 7698 RVA: 0x0004AEAA File Offset: 0x000490AA
		[WebSysDescription("")]
		[DefaultValue(true)]
		[WebCategory("Appearance")]
		public bool ShowTitle
		{
			get
			{
				return this.ViewState.GetBool("ShowTitle", true);
			}
			set
			{
				this.ViewState["ShowTitle"] = value;
			}
		}

		/// <summary>Gets or sets the format for the title section.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TitleFormat" /> values. The default value is MonthYear.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified title format is not one of the <see cref="T:System.Web.UI.WebControls.TitleFormat" /> values. </exception>
		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06001E13 RID: 7699 RVA: 0x0004AEC2 File Offset: 0x000490C2
		// (set) Token: 0x06001E14 RID: 7700 RVA: 0x0004AED5 File Offset: 0x000490D5
		[DefaultValue(TitleFormat.MonthYear)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public TitleFormat TitleFormat
		{
			get
			{
				return (TitleFormat)this.ViewState.GetInt("TitleFormat", 1);
			}
			set
			{
				if (value != TitleFormat.Month && value != TitleFormat.MonthYear)
				{
					throw new ArgumentOutOfRangeException("The specified title format is not one of the TitleFormat values.");
				}
				this.ViewState["TitleFormat"] = value;
			}
		}

		/// <summary>Gets the style properties of the title heading for the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties of the title heading for the <see cref="T:System.Web.UI.WebControls.Calendar" />. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06001E15 RID: 7701 RVA: 0x0004AEFF File Offset: 0x000490FF
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		public TableItemStyle TitleStyle
		{
			get
			{
				if (this.titleStyle == null)
				{
					this.titleStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.titleStyle.TrackViewState();
					}
				}
				return this.titleStyle;
			}
		}

		/// <summary>Gets the style properties for today's date on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for today's date on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06001E16 RID: 7702 RVA: 0x0004AF2D File Offset: 0x0004912D
		[WebSysDescription("")]
		[WebCategory("Style")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle TodayDayStyle
		{
			get
			{
				if (this.todayDayStyle == null)
				{
					this.todayDayStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.todayDayStyle.TrackViewState();
					}
				}
				return this.todayDayStyle;
			}
		}

		/// <summary>Gets or sets the value for today's date.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the value that the <see cref="T:System.Web.UI.WebControls.Calendar" /> considers to be today's date. If this property is not explicitly set, this date will be the date on the server.</returns>
		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06001E17 RID: 7703 RVA: 0x0004AF5C File Offset: 0x0004915C
		// (set) Token: 0x06001E18 RID: 7704 RVA: 0x0004AF8A File Offset: 0x0004918A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Style")]
		[Browsable(false)]
		[WebSysDescription("")]
		public DateTime TodaysDate
		{
			get
			{
				object obj = this.ViewState["TodaysDate"];
				if (obj != null)
				{
					return (DateTime)obj;
				}
				return this.today;
			}
			set
			{
				this.ViewState["TodaysDate"] = value.Date;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to render the table header &lt;th&gt; HTML element for the day headers instead of the table data &lt;td&gt; HTML element.</summary>
		/// <returns>true if the &lt;th&gt; element is used for day header cells; false if the &lt;td&gt; element is used for day headers.</returns>
		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06001E19 RID: 7705 RVA: 0x0004AFA8 File Offset: 0x000491A8
		// (set) Token: 0x06001E1A RID: 7706 RVA: 0x00047C78 File Offset: 0x00045E78
		[DefaultValue(true)]
		[WebSysDescription("")]
		[WebCategory("Accessibility")]
		public virtual bool UseAccessibleHeader
		{
			get
			{
				return this.ViewState.GetBool("UseAccessibleHeader", true);
			}
			set
			{
				this.ViewState["UseAccessibleHeader"] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.DateTime" /> value that specifies the month to display on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that specifies the month to display on the <see cref="T:System.Web.UI.WebControls.Calendar" />. The default value is <see cref="F:System.DateTime.MinValue" />, which displays the month that contains the date specified by <see cref="P:System.Web.UI.WebControls.Calendar.TodaysDate" />.</returns>
		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x0004AFBC File Offset: 0x000491BC
		// (set) Token: 0x06001E1C RID: 7708 RVA: 0x0004AFE9 File Offset: 0x000491E9
		[WebSysDescription("")]
		[WebCategory("Style")]
		[DefaultValue("1/1/0001 12:00:00 AM")]
		[Bindable(true)]
		public DateTime VisibleDate
		{
			get
			{
				object obj = this.ViewState["VisibleDate"];
				if (obj != null)
				{
					return (DateTime)obj;
				}
				return DateTime.MinValue;
			}
			set
			{
				this.ViewState["VisibleDate"] = value.Date;
			}
		}

		/// <summary>Gets the style properties for the weekend dates on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that contains the style properties for the weekend dates on the <see cref="T:System.Web.UI.WebControls.Calendar" />. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" />.</returns>
		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06001E1D RID: 7709 RVA: 0x0004B007 File Offset: 0x00049207
		[WebCategory("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebSysDescription("")]
		public TableItemStyle WeekendDayStyle
		{
			get
			{
				if (this.weekendDayStyle == null)
				{
					this.weekendDayStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.weekendDayStyle.TrackViewState();
					}
				}
				return this.weekendDayStyle;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x0004789D File Offset: 0x00045A9D
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x0004B035 File Offset: 0x00049235
		private DateTimeFormatInfo DateInfo
		{
			get
			{
				if (this.dateInfo == null)
				{
					this.dateInfo = Thread.CurrentThread.CurrentCulture.DateTimeFormat;
				}
				return this.dateInfo;
			}
		}

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x0004B05C File Offset: 0x0004925C
		private DateTime DisplayDate
		{
			get
			{
				DateTime dateTime = this.VisibleDate;
				if (dateTime == DateTime.MinValue)
				{
					dateTime = this.TodaysDate;
				}
				return dateTime;
			}
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06001E21 RID: 7713 RVA: 0x0004B085 File Offset: 0x00049285
		private DayOfWeek DisplayFirstDayOfWeek
		{
			get
			{
				if (this.FirstDayOfWeek != FirstDayOfWeek.Default)
				{
					return (DayOfWeek)this.FirstDayOfWeek;
				}
				return this.DateInfo.FirstDayOfWeek;
			}
		}

		/// <summary>Creates a collection to store child controls.</summary>
		/// <returns>Always returns an InternalControlCollection object.</returns>
		// Token: 0x06001E22 RID: 7714 RVA: 0x0004B0A2 File Offset: 0x000492A2
		protected override ControlCollection CreateControlCollection()
		{
			return base.CreateControlCollection();
		}

		/// <summary>Determines whether a <see cref="T:System.Web.UI.WebControls.CalendarSelectionMode" /> object contains week selectors.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.CalendarSelectionMode" /> contains week selectors; otherwise, false.</returns>
		/// <param name="selectionMode">One of the <see cref="T:System.Web.UI.WebControls.CalendarSelectionMode" /> values. </param>
		// Token: 0x06001E23 RID: 7715 RVA: 0x0004B0AA File Offset: 0x000492AA
		protected bool HasWeekSelectors(CalendarSelectionMode selectionMode)
		{
			return selectionMode == CalendarSelectionMode.DayWeek || selectionMode == CalendarSelectionMode.DayWeekMonth;
		}

		/// <summary>Raises events on postback for the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <param name="eventArgument">The argument for the event. </param>
		// Token: 0x06001E24 RID: 7716 RVA: 0x0004B0B7 File Offset: 0x000492B7
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Raises an event for the <see cref="T:System.Web.UI.WebControls.Calendar" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">A <see cref="T:System.String" /> that represents the event argument passed to the event handler. </param>
		// Token: 0x06001E25 RID: 7717 RVA: 0x0004B0C0 File Offset: 0x000492C0
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (eventArgument.Length < 1)
			{
				return;
			}
			if (eventArgument[0] == 'V')
			{
				DateTime visibleDate = this.VisibleDate;
				int num = int.Parse(eventArgument.Substring(1));
				DateTime dateTime = this.GetGlobalCalendar().AddDays(Calendar.dateZenith, num);
				this.VisibleDate = dateTime;
				this.OnVisibleMonthChanged(this.VisibleDate, visibleDate);
				return;
			}
			if (eventArgument[0] == 'R')
			{
				string text = eventArgument.Substring(1);
				string text2 = text.Substring(text.Length - 2, 2);
				string text3 = text.Substring(0, text.Length - 2);
				DateTime dateTime2 = this.GetGlobalCalendar().AddDays(Calendar.dateZenith, int.Parse(text3));
				this.SelectedDates.SelectRange(dateTime2, dateTime2.AddDays((double)int.Parse(text2)));
				this.OnSelectionChanged();
				return;
			}
			int num2 = int.Parse(eventArgument);
			DateTime dateTime3 = this.GetGlobalCalendar().AddDays(Calendar.dateZenith, num2);
			this.SelectedDates.SelectRange(dateTime3, dateTime3);
			this.OnSelectionChanged();
		}

		/// <summary>Loads a saved state of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <param name="savedState">A <see cref="T:System.Object" /> that contains the saved condition of the <see cref="T:System.Web.UI.WebControls.Calendar" />. </param>
		// Token: 0x06001E26 RID: 7718 RVA: 0x0004B1D0 File Offset: 0x000493D0
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array[0] != null)
			{
				base.LoadViewState(array[0]);
			}
			if (array[1] != null)
			{
				this.DayHeaderStyle.LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				this.DayStyle.LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				this.NextPrevStyle.LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				this.OtherMonthDayStyle.LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				this.SelectedDayStyle.LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				this.TitleStyle.LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				this.TodayDayStyle.LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				this.SelectorStyle.LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				this.WeekendDayStyle.LoadViewState(array[9]);
			}
			ArrayList arrayList = (ArrayList)this.ViewState["SelectedDates"];
			if (arrayList != null)
			{
				this.dateList = arrayList;
				this.selectedDatesCollection = new SelectedDatesCollection(this.dateList);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Calendar.DayRender" /> event of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control and allows you to provide a custom handler for the <see cref="E:System.Web.UI.WebControls.Calendar.DayRender" /> event.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> that contains information about the cell to render. </param>
		/// <param name="day">A <see cref="T:System.Web.UI.WebControls.CalendarDay" /> that contains information about the day to render. </param>
		// Token: 0x06001E27 RID: 7719 RVA: 0x0004B2D0 File Offset: 0x000494D0
		protected virtual void OnDayRender(TableCell cell, CalendarDay day)
		{
			DayRenderEventHandler dayRenderEventHandler = (DayRenderEventHandler)base.Events[Calendar.DayRenderEvent];
			if (dayRenderEventHandler != null)
			{
				Page page = this.Page;
				if (page != null)
				{
					dayRenderEventHandler(this, new DayRenderEventArgs(cell, day, page.ClientScript.GetPostBackClientHyperlink(this, this.GetDaysFromZenith(day.Date).ToString(), true)));
					return;
				}
				dayRenderEventHandler(this, new DayRenderEventArgs(cell, day));
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x06001E28 RID: 7720 RVA: 0x000419F4 File Offset: 0x0003FBF4
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Calendar.SelectionChanged" /> event of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control and allows you to provide a custom handler for the <see cref="E:System.Web.UI.WebControls.Calendar.SelectionChanged" /> event.</summary>
		// Token: 0x06001E29 RID: 7721 RVA: 0x0004B340 File Offset: 0x00049540
		protected virtual void OnSelectionChanged()
		{
			EventHandler eventHandler = (EventHandler)base.Events[Calendar.SelectionChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.Calendar.VisibleMonthChanged" /> event of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control and allows you to provide a custom handler for the <see cref="E:System.Web.UI.WebControls.Calendar.VisibleMonthChanged" /> event.</summary>
		/// <param name="newDate">A <see cref="T:System.DateTime" /> that represents the month currently displayed in the <see cref="T:System.Web.UI.WebControls.Calendar" />. </param>
		/// <param name="previousDate">A <see cref="T:System.DateTime" /> that represents the previous month displayed by the <see cref="T:System.Web.UI.WebControls.Calendar" />. </param>
		// Token: 0x06001E2A RID: 7722 RVA: 0x0004B374 File Offset: 0x00049574
		protected virtual void OnVisibleMonthChanged(DateTime newDate, DateTime previousDate)
		{
			MonthChangedEventHandler monthChangedEventHandler = (MonthChangedEventHandler)base.Events[Calendar.VisibleMonthChangedEvent];
			if (monthChangedEventHandler != null)
			{
				monthChangedEventHandler(this, new MonthChangedEventArgs(newDate, previousDate));
			}
		}

		/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.Calendar" /> control on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream for rendering on the client. </param>
		// Token: 0x06001E2B RID: 7723 RVA: 0x0004B3A8 File Offset: 0x000495A8
		protected internal override void Render(HtmlTextWriter writer)
		{
			TableStyle tableStyle = new TableStyle();
			tableStyle.CellSpacing = this.CellSpacing;
			tableStyle.CellPadding = this.CellPadding;
			tableStyle.BorderWidth = 1;
			if (base.ControlStyleCreated)
			{
				tableStyle.CopyFrom(base.ControlStyle);
			}
			if (this.ShowGridLines)
			{
				tableStyle.GridLines = GridLines.Both;
			}
			tableStyle.AddAttributesToRender(writer);
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			writer.RenderBeginTag(HtmlTextWriterTag.Table);
			if (!string.IsNullOrEmpty(this.Caption))
			{
				this.WriteCaption(writer);
			}
			bool isEnabled = base.IsEnabled;
			if (this.ShowTitle)
			{
				this.WriteTitle(writer, isEnabled);
			}
			if (this.ShowDayHeader)
			{
				this.WriteDayHeader(writer, isEnabled);
			}
			this.WriteDays(writer, isEnabled);
			writer.RenderEndTag();
		}

		/// <summary>Stores the state of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>An object that contains the saved state of the <see cref="T:System.Web.UI.WebControls.Calendar" />.</returns>
		// Token: 0x06001E2C RID: 7724 RVA: 0x0004B468 File Offset: 0x00049668
		protected override object SaveViewState()
		{
			object[] array = new object[10];
			if (this.dayHeaderStyle != null)
			{
				array[1] = this.dayHeaderStyle.SaveViewState();
			}
			if (this.dayStyle != null)
			{
				array[2] = this.dayStyle.SaveViewState();
			}
			if (this.nextPrevStyle != null)
			{
				array[3] = this.nextPrevStyle.SaveViewState();
			}
			if (this.otherMonthDayStyle != null)
			{
				array[4] = this.otherMonthDayStyle.SaveViewState();
			}
			if (this.selectedDayStyle != null)
			{
				array[5] = this.selectedDayStyle.SaveViewState();
			}
			if (this.titleStyle != null)
			{
				array[6] = this.titleStyle.SaveViewState();
			}
			if (this.todayDayStyle != null)
			{
				array[7] = this.todayDayStyle.SaveViewState();
			}
			if (this.selectorStyle != null)
			{
				array[8] = this.selectorStyle.SaveViewState();
			}
			if (this.weekendDayStyle != null)
			{
				array[9] = this.weekendDayStyle.SaveViewState();
			}
			if (this.SelectedDates.Count > 0)
			{
				this.ViewState["SelectedDates"] = this.dateList;
			}
			array[0] = base.SaveViewState();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Marks the starting point to begin tracking and saving changes to the control as part of the control view state.</summary>
		// Token: 0x06001E2D RID: 7725 RVA: 0x0004B588 File Offset: 0x00049788
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.dayHeaderStyle != null)
			{
				this.dayHeaderStyle.TrackViewState();
			}
			if (this.dayStyle != null)
			{
				this.dayStyle.TrackViewState();
			}
			if (this.nextPrevStyle != null)
			{
				this.nextPrevStyle.TrackViewState();
			}
			if (this.otherMonthDayStyle != null)
			{
				this.otherMonthDayStyle.TrackViewState();
			}
			if (this.selectedDayStyle != null)
			{
				this.selectedDayStyle.TrackViewState();
			}
			if (this.titleStyle != null)
			{
				this.titleStyle.TrackViewState();
			}
			if (this.todayDayStyle != null)
			{
				this.todayDayStyle.TrackViewState();
			}
			if (this.selectorStyle != null)
			{
				this.selectorStyle.TrackViewState();
			}
			if (this.weekendDayStyle != null)
			{
				this.weekendDayStyle.TrackViewState();
			}
		}

		// Token: 0x06001E2E RID: 7726 RVA: 0x0004B648 File Offset: 0x00049848
		private void WriteDayHeader(HtmlTextWriter writer, bool enabled)
		{
			int num2;
			int num = (num2 = (int)this.DisplayFirstDayOfWeek);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			if (this.SelectionMode == CalendarSelectionMode.DayWeek)
			{
				TableCell tableCell = new TableCell();
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.ApplyStyle(this.DayHeaderStyle);
				tableCell.RenderBeginTag(writer);
				tableCell.RenderEndTag(writer);
			}
			else if (this.SelectionMode == CalendarSelectionMode.DayWeekMonth)
			{
				TableCell tableCell2 = new TableCell();
				tableCell2.ApplyStyle(this.SelectorStyle);
				tableCell2.HorizontalAlign = HorizontalAlign.Center;
				DateTime dateTime = new DateTime(this.DisplayDate.Year, this.DisplayDate.Month, 1);
				int num3 = DateTime.DaysInMonth(this.DisplayDate.Year, this.DisplayDate.Month);
				tableCell2.RenderBeginTag(writer);
				writer.Write(this.BuildLink("R" + this.GetDaysFromZenith(dateTime) + num3, this.SelectMonthText, this.DayHeaderStyle.ForeColor, enabled));
				tableCell2.RenderEndTag(writer);
			}
			DateTimeFormatInfo dateTimeFormatInfo = this.DateInfo;
			for (;;)
			{
				DayOfWeek dayOfWeek = (DayOfWeek)num2;
				string text = dateTimeFormatInfo.GetDayName(dayOfWeek);
				TableCell tableCell;
				if (this.UseAccessibleHeader)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Abbr, text);
					writer.AddAttribute(HtmlTextWriterAttribute.Scope, "col", false);
					tableCell = new TableHeaderCell();
				}
				else
				{
					tableCell = new TableCell();
				}
				tableCell.HorizontalAlign = HorizontalAlign.Center;
				tableCell.ApplyStyle(this.DayHeaderStyle);
				tableCell.RenderBeginTag(writer);
				switch (this.DayNameFormat)
				{
				case DayNameFormat.Full:
					break;
				case DayNameFormat.Short:
					goto IL_01A1;
				case DayNameFormat.FirstLetter:
					text = text.Substring(0, 1);
					break;
				case DayNameFormat.FirstTwoLetters:
					text = text.Substring(0, 2);
					break;
				case DayNameFormat.Shortest:
					text = dateTimeFormatInfo.GetShortestDayName(dayOfWeek);
					break;
				default:
					goto IL_01A1;
				}
				IL_01AB:
				writer.Write(text);
				tableCell.RenderEndTag(writer);
				if (num2 >= 6)
				{
					num2 = 0;
				}
				else
				{
					num2++;
				}
				if (num2 == num)
				{
					break;
				}
				continue;
				IL_01A1:
				text = dateTimeFormatInfo.GetAbbreviatedDayName(dayOfWeek);
				goto IL_01AB;
			}
			writer.RenderEndTag();
		}

		// Token: 0x06001E2F RID: 7727 RVA: 0x0004B828 File Offset: 0x00049A28
		private void WriteDay(DateTime date, HtmlTextWriter writer, bool enabled)
		{
			TableItemStyle tableItemStyle = new TableItemStyle();
			TableCell tableCell = new TableCell();
			CalendarDay calendarDay = new CalendarDay(date, this.IsWeekEnd(date.DayOfWeek), date == this.TodaysDate, this.SelectedDates.Contains(date), this.GetGlobalCalendar().GetMonth(this.DisplayDate) != this.GetGlobalCalendar().GetMonth(date), date.Day.ToString());
			calendarDay.IsSelectable = this.SelectionMode > CalendarSelectionMode.None;
			tableCell.HorizontalAlign = HorizontalAlign.Center;
			tableCell.Width = Unit.Percentage(this.GetCellWidth());
			LiteralControl literalControl = new LiteralControl(calendarDay.DayNumberText);
			tableCell.Controls.Add(literalControl);
			this.OnDayRender(tableCell, calendarDay);
			if (this.dayStyle != null && !this.dayStyle.IsEmpty)
			{
				tableItemStyle.CopyFrom(this.dayStyle);
			}
			if (calendarDay.IsWeekend && this.weekendDayStyle != null && !this.weekendDayStyle.IsEmpty)
			{
				tableItemStyle.CopyFrom(this.weekendDayStyle);
			}
			if (calendarDay.IsToday && this.todayDayStyle != null && !this.todayDayStyle.IsEmpty)
			{
				tableItemStyle.CopyFrom(this.todayDayStyle);
			}
			if (calendarDay.IsOtherMonth && this.otherMonthDayStyle != null && !this.otherMonthDayStyle.IsEmpty)
			{
				tableItemStyle.CopyFrom(this.otherMonthDayStyle);
			}
			if (enabled && calendarDay.IsSelected)
			{
				tableItemStyle.BackColor = Color.Silver;
				tableItemStyle.ForeColor = Color.White;
				if (this.selectedDayStyle != null && !this.selectedDayStyle.IsEmpty)
				{
					tableItemStyle.CopyFrom(this.selectedDayStyle);
				}
			}
			tableCell.ApplyStyle(tableItemStyle);
			literalControl.Text = this.BuildLink(this.GetDaysFromZenith(date).ToString(), calendarDay.DayNumberText, tableCell.ForeColor, enabled && calendarDay.IsSelectable);
			tableCell.RenderControl(writer);
		}

		// Token: 0x06001E30 RID: 7728 RVA: 0x0004BA08 File Offset: 0x00049C08
		private void WriteDays(HtmlTextWriter writer, bool enabled)
		{
			DateTime dateTime = new DateTime(this.DisplayDate.Year, this.DisplayDate.Month, 1);
			TableCell tableCell = null;
			int num = 0;
			while (num < 7 && dateTime.DayOfWeek != this.DisplayFirstDayOfWeek)
			{
				dateTime = this.GetGlobalCalendar().AddDays(dateTime, -1);
				num++;
			}
			if (num == 0)
			{
				dateTime = this.GetGlobalCalendar().AddDays(dateTime, -7);
			}
			DateTime dateTime2 = this.GetGlobalCalendar().AddDays(dateTime, 42);
			do
			{
				writer.RenderBeginTag(HtmlTextWriterTag.Tr);
				if (this.HasWeekSelectors(this.SelectionMode))
				{
					if (tableCell == null)
					{
						tableCell = new TableCell();
						tableCell.ApplyStyle(this.SelectorStyle);
						tableCell.HorizontalAlign = HorizontalAlign.Center;
						tableCell.Width = Unit.Percentage(this.GetCellWidth());
					}
					tableCell.RenderBeginTag(writer);
					writer.Write(this.BuildLink("R" + this.GetDaysFromZenith(dateTime) + "07", this.SelectWeekText, tableCell.ForeColor, enabled));
					tableCell.RenderEndTag(writer);
				}
				for (int i = 0; i < 7; i++)
				{
					this.WriteDay(dateTime, writer, enabled);
					dateTime = this.GetGlobalCalendar().AddDays(dateTime, 1);
				}
				writer.RenderEndTag();
			}
			while (!(dateTime >= dateTime2));
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x0004BB48 File Offset: 0x00049D48
		private string BuildLink(string arg, string text, Color foreColor, bool hasLink)
		{
			StringBuilder stringBuilder = new StringBuilder();
			Page page = this.Page;
			hasLink = page != null && hasLink;
			if (hasLink)
			{
				stringBuilder.Append("<a href=\"");
				stringBuilder.Append(page.ClientScript.GetPostBackClientHyperlink(this, arg, true));
				stringBuilder.Append('"');
				Color color;
				if (!foreColor.IsEmpty)
				{
					color = foreColor;
				}
				else if (this.ForeColor.IsEmpty)
				{
					color = Color.Black;
				}
				else
				{
					color = this.ForeColor;
				}
				stringBuilder.Append(" style=\"color:" + ColorTranslator.ToHtml(color));
				stringBuilder.Append("\">");
				stringBuilder.Append(text);
				stringBuilder.Append("</a>");
			}
			else
			{
				stringBuilder.Append(text);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x0004BC18 File Offset: 0x00049E18
		private int GetDaysFromZenith(DateTime date)
		{
			return date.Subtract(Calendar.dateZenith).Days;
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0004BC3C File Offset: 0x00049E3C
		private void WriteCaption(HtmlTextWriter writer)
		{
			if (this.CaptionAlign != TableCaptionAlign.NotSet)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Align, this.CaptionAlign.ToString(Helpers.InvariantCulture));
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Caption);
			writer.Write(this.Caption);
			writer.RenderEndTag();
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0004BC8C File Offset: 0x00049E8C
		private void WriteTitle(HtmlTextWriter writer, bool enabled)
		{
			TableCell tableCell = null;
			TableCell tableCell2 = new TableCell();
			Table table = new Table();
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			tableCell2.ColumnSpan = (this.HasWeekSelectors(this.SelectionMode) ? 8 : 7);
			if (this.titleStyle != null && !this.titleStyle.IsEmpty && !this.titleStyle.BackColor.IsEmpty)
			{
				tableCell2.BackColor = this.titleStyle.BackColor;
			}
			else
			{
				tableCell2.BackColor = Color.Silver;
			}
			tableCell2.RenderBeginTag(writer);
			table.Width = Unit.Percentage(100.0);
			if (this.titleStyle != null && !this.titleStyle.IsEmpty)
			{
				table.ApplyStyle(this.titleStyle);
			}
			table.RenderBeginTag(writer);
			writer.RenderBeginTag(HtmlTextWriterTag.Tr);
			if (this.ShowNextPrevMonth)
			{
				tableCell = new TableCell();
				tableCell.ApplyStyle(this.nextPrevStyle);
				tableCell.Width = Unit.Percentage(15.0);
				DateTime dateTime = this.GetGlobalCalendar().AddMonths(this.DisplayDate, -1);
				dateTime = this.GetGlobalCalendar().AddDays(dateTime, -dateTime.Day + 1);
				tableCell.RenderBeginTag(writer);
				writer.Write(this.BuildLink("V" + this.GetDaysFromZenith(dateTime), this.GetNextPrevFormatText(dateTime, false), tableCell.ForeColor, enabled));
				tableCell.RenderEndTag(writer);
			}
			DateTimeFormatInfo dateTimeFormatInfo = this.DateInfo;
			TableCell tableCell3 = new TableCell();
			tableCell3.Width = Unit.Percentage(70.0);
			tableCell3.HorizontalAlign = HorizontalAlign.Center;
			tableCell3.RenderBeginTag(writer);
			string text;
			if (this.TitleFormat == TitleFormat.MonthYear)
			{
				text = this.DisplayDate.ToString(dateTimeFormatInfo.YearMonthPattern, dateTimeFormatInfo);
			}
			else
			{
				text = dateTimeFormatInfo.GetMonthName(this.GetGlobalCalendar().GetMonth(this.DisplayDate));
			}
			writer.Write(text);
			tableCell3.RenderEndTag(writer);
			if (this.ShowNextPrevMonth)
			{
				DateTime dateTime2 = this.GetGlobalCalendar().AddMonths(this.DisplayDate, 1);
				dateTime2 = this.GetGlobalCalendar().AddDays(dateTime2, -dateTime2.Day + 1);
				tableCell.HorizontalAlign = HorizontalAlign.Right;
				tableCell.RenderBeginTag(writer);
				writer.Write(this.BuildLink("V" + this.GetDaysFromZenith(dateTime2), this.GetNextPrevFormatText(dateTime2, true), tableCell.ForeColor, enabled));
				tableCell.RenderEndTag(writer);
			}
			writer.RenderEndTag();
			table.RenderEndTag(writer);
			tableCell2.RenderEndTag(writer);
			writer.RenderEndTag();
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x0004BF0C File Offset: 0x0004A10C
		private string GetNextPrevFormatText(DateTime date, bool next)
		{
			DateTimeFormatInfo dateTimeFormatInfo = this.DateInfo;
			switch (this.NextPrevFormat)
			{
			case NextPrevFormat.ShortMonth:
				return dateTimeFormatInfo.GetAbbreviatedMonthName(this.GetGlobalCalendar().GetMonth(date));
			case NextPrevFormat.FullMonth:
				return dateTimeFormatInfo.GetMonthName(this.GetGlobalCalendar().GetMonth(date));
			}
			return next ? this.NextMonthText : this.PrevMonthText;
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x0004BF78 File Offset: 0x0004A178
		private bool IsWeekEnd(DayOfWeek day)
		{
			return day == DayOfWeek.Saturday || day == DayOfWeek.Sunday;
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x0004BF84 File Offset: 0x0004A184
		private double GetCellWidth()
		{
			return (double)(this.HasWeekSelectors(this.SelectionMode) ? 12 : 14);
		}

		// Token: 0x06001E38 RID: 7736 RVA: 0x0004BF9B File Offset: 0x0004A19B
		private Calendar GetGlobalCalendar()
		{
			return DateTimeFormatInfo.CurrentInfo.Calendar;
		}

		/// <summary>Occurs when each day is created in the control hierarchy for the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06001E39 RID: 7737 RVA: 0x0004BFA7 File Offset: 0x0004A1A7
		// (remove) Token: 0x06001E3A RID: 7738 RVA: 0x0004BFBA File Offset: 0x0004A1BA
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event DayRenderEventHandler DayRender
		{
			add
			{
				base.Events.AddHandler(Calendar.DayRenderEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Calendar.DayRenderEvent, value);
			}
		}

		/// <summary>Occurs when the user selects a day, a week, or an entire month by clicking the date selector controls.</summary>
		// Token: 0x1400004C RID: 76
		// (add) Token: 0x06001E3B RID: 7739 RVA: 0x0004BFCD File Offset: 0x0004A1CD
		// (remove) Token: 0x06001E3C RID: 7740 RVA: 0x0004BFE0 File Offset: 0x0004A1E0
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event EventHandler SelectionChanged
		{
			add
			{
				base.Events.AddHandler(Calendar.SelectionChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Calendar.SelectionChangedEvent, value);
			}
		}

		/// <summary>Occurs when the user clicks the next or previous month navigation controls on the title heading.</summary>
		// Token: 0x1400004D RID: 77
		// (add) Token: 0x06001E3D RID: 7741 RVA: 0x0004BFF3 File Offset: 0x0004A1F3
		// (remove) Token: 0x06001E3E RID: 7742 RVA: 0x0004C006 File Offset: 0x0004A206
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event MonthChangedEventHandler VisibleMonthChanged
		{
			add
			{
				base.Events.AddHandler(Calendar.VisibleMonthChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(Calendar.VisibleMonthChangedEvent, value);
			}
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x0004C019 File Offset: 0x0004A219
		// Note: this type is marked as 'beforefieldinit'.
		static Calendar()
		{
			Calendar.DayRenderEvent = new object();
			Calendar.SelectionChangedEvent = new object();
			Calendar.VisibleMonthChangedEvent = new object();
		}

		// Token: 0x0400183D RID: 6205
		private TableItemStyle dayHeaderStyle;

		// Token: 0x0400183E RID: 6206
		private TableItemStyle dayStyle;

		// Token: 0x0400183F RID: 6207
		private TableItemStyle nextPrevStyle;

		// Token: 0x04001840 RID: 6208
		private TableItemStyle otherMonthDayStyle;

		// Token: 0x04001841 RID: 6209
		private TableItemStyle selectedDayStyle;

		// Token: 0x04001842 RID: 6210
		private TableItemStyle titleStyle;

		// Token: 0x04001843 RID: 6211
		private TableItemStyle todayDayStyle;

		// Token: 0x04001844 RID: 6212
		private TableItemStyle selectorStyle;

		// Token: 0x04001845 RID: 6213
		private TableItemStyle weekendDayStyle;

		// Token: 0x04001846 RID: 6214
		private DateTimeFormatInfo dateInfo;

		// Token: 0x04001847 RID: 6215
		private SelectedDatesCollection selectedDatesCollection;

		// Token: 0x04001848 RID: 6216
		private ArrayList dateList;

		// Token: 0x04001849 RID: 6217
		private DateTime today = DateTime.Today;

		// Token: 0x0400184A RID: 6218
		private static DateTime dateZenith = new DateTime(2000, 1, 1);

		// Token: 0x0400184B RID: 6219
		private const int daysInAWeek = 7;
	}
}
