using System;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a date in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	// Token: 0x02000345 RID: 837
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CalendarDay
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CalendarDay" /> class.</summary>
		/// <param name="date">A <see cref="T:System.DateTime" /> object that contains the date represented by an instance of this class. </param>
		/// <param name="isWeekend">true to indicate that the date represented by an instance of this class is either a Saturday or a Sunday; otherwise, false. </param>
		/// <param name="isToday">true to indicate that the date represented by an instance of this class is the current date; otherwise, false. </param>
		/// <param name="isSelected">true to indicate that the date represented by an instance of this class is selected on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control; otherwise, false. </param>
		/// <param name="isOtherMonth">true to indicate that the date represented by an instance of this class is in a month other than the displayed month on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control; otherwise, false. </param>
		/// <param name="dayNumberText">The day number for the date represented by this class. </param>
		// Token: 0x06001E40 RID: 7744 RVA: 0x0004C04A File Offset: 0x0004A24A
		public CalendarDay(DateTime date, bool isWeekend, bool isToday, bool isSelected, bool isOtherMonth, string dayNumberText)
		{
			this.date = date;
			this.isWeekend = isWeekend;
			this.isToday = isToday;
			this.isSelected = isSelected;
			this.isOtherMonth = isOtherMonth;
			this.dayNumberText = dayNumberText;
			this.isSelectable = false;
		}

		/// <summary>Gets the date represented by an instance of this class. This property is read-only.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that contains the date represented by an instance of this class. This allows you to programmatically control the appearance or behavior of the day, based on this value.</returns>
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06001E41 RID: 7745 RVA: 0x0004C086 File Offset: 0x0004A286
		public DateTime Date
		{
			get
			{
				return this.date;
			}
		}

		/// <summary>Gets the string equivalent of the day number for the date represented by an instance of the <see cref="T:System.Web.UI.WebControls.CalendarDay" /> class. This property is read-only.</summary>
		/// <returns>The string equivalent of the day number for the date represented by an instance of this class.</returns>
		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06001E42 RID: 7746 RVA: 0x0004C08E File Offset: 0x0004A28E
		public string DayNumberText
		{
			get
			{
				return this.dayNumberText;
			}
		}

		/// <summary>Gets a value that indicates whether the date represented by an instance of this class is in a month other than the month displayed in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control. This property is read-only.</summary>
		/// <returns>true if the date represented by an instance of this class is in a month other than the month displayed in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control; otherwise, false.</returns>
		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06001E43 RID: 7747 RVA: 0x0004C096 File Offset: 0x0004A296
		public bool IsOtherMonth
		{
			get
			{
				return this.isOtherMonth;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the date represented by an instance of this class can be selected in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>true if the date can be selected; otherwise, false.</returns>
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06001E44 RID: 7748 RVA: 0x0004C09E File Offset: 0x0004A29E
		// (set) Token: 0x06001E45 RID: 7749 RVA: 0x0004C0A6 File Offset: 0x0004A2A6
		public bool IsSelectable
		{
			get
			{
				return this.isSelectable;
			}
			set
			{
				this.isSelectable = value;
			}
		}

		/// <summary>Gets a value that indicates whether the date represented by an instance of this class is selected in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control. This property is read-only.</summary>
		/// <returns>true if the date represented by an instance of this class is selected in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control; otherwise, false.</returns>
		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06001E46 RID: 7750 RVA: 0x0004C0AF File Offset: 0x0004A2AF
		public bool IsSelected
		{
			get
			{
				return this.isSelected;
			}
		}

		/// <summary>Gets a value that indicates whether the date represented by an instance of this class is the same date specified by the <see cref="P:System.Web.UI.WebControls.Calendar.TodaysDate" /> property of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control. This property is read-only.</summary>
		/// <returns>true if the date represented by an instance of this class is the same date specified by the <see cref="P:System.Web.UI.WebControls.Calendar.TodaysDate" /> property of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control; otherwise, false.</returns>
		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x0004C0B7 File Offset: 0x0004A2B7
		public bool IsToday
		{
			get
			{
				return this.isToday;
			}
		}

		/// <summary>Gets a value that indicates whether the date represented by an instance of this class is a either Saturday or Sunday. This property is read-only.</summary>
		/// <returns>true if the date represented by an instance of this class is either a Saturday or a Sunday; otherwise, false.</returns>
		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06001E48 RID: 7752 RVA: 0x0004C0BF File Offset: 0x0004A2BF
		public bool IsWeekend
		{
			get
			{
				return this.isWeekend;
			}
		}

		// Token: 0x0400184F RID: 6223
		private DateTime date;

		// Token: 0x04001850 RID: 6224
		private bool isWeekend;

		// Token: 0x04001851 RID: 6225
		private bool isToday;

		// Token: 0x04001852 RID: 6226
		private bool isSelected;

		// Token: 0x04001853 RID: 6227
		private bool isOtherMonth;

		// Token: 0x04001854 RID: 6228
		private string dayNumberText;

		// Token: 0x04001855 RID: 6229
		private bool isSelectable;
	}
}
