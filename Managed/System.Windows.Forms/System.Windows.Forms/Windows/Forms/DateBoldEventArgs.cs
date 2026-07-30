using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for events that are internal to the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000141 RID: 321
	public class DateBoldEventArgs : EventArgs
	{
		// Token: 0x0600164D RID: 5709 RVA: 0x000522D0 File Offset: 0x000504D0
		private DateBoldEventArgs(DateTime start, int size, int[] daysToBold)
		{
			this.start = start;
			this.size = size;
			this.days_to_bold = daysToBold;
		}

		/// <summary>Gets or sets dates that are bold.</summary>
		/// <returns>The dates that are bold.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x0600164E RID: 5710 RVA: 0x000522F0 File Offset: 0x000504F0
		// (set) Token: 0x0600164F RID: 5711 RVA: 0x000522F8 File Offset: 0x000504F8
		public int[] DaysToBold
		{
			get
			{
				return this.days_to_bold;
			}
			set
			{
				this.days_to_bold = value;
			}
		}

		/// <summary>Gets the number of dates that are bold.</summary>
		/// <returns>The number of dates that are bold.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06001650 RID: 5712 RVA: 0x00052304 File Offset: 0x00050504
		public int Size
		{
			get
			{
				return this.size;
			}
		}

		/// <summary>Gets the first date that is bold.</summary>
		/// <returns>The first date that is bold.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06001651 RID: 5713 RVA: 0x0005230C File Offset: 0x0005050C
		public DateTime StartDate
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x04000C45 RID: 3141
		private int size;

		// Token: 0x04000C46 RID: 3142
		private DateTime start;

		// Token: 0x04000C47 RID: 3143
		private int[] days_to_bold;
	}
}
