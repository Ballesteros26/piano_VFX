using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.MonthCalendar.DateChanged" /> or <see cref="E:System.Windows.Forms.MonthCalendar.DateSelected" /> events of the <see cref="T:System.Windows.Forms.MonthCalendar" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000142 RID: 322
	public class DateRangeEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DateRangeEventArgs" /> class.</summary>
		/// <param name="start">The first date/time value in the range that the user has selected. </param>
		/// <param name="end">The last date/time value in the range that the user has selected. </param>
		// Token: 0x06001652 RID: 5714 RVA: 0x00052314 File Offset: 0x00050514
		public DateRangeEventArgs(DateTime start, DateTime end)
		{
			this.start = start;
			this.end = end;
		}

		/// <summary>Gets the last date/time value in the range that the user has selected.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that represents the last date in the date range that the user has selected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001653 RID: 5715 RVA: 0x0005232C File Offset: 0x0005052C
		public DateTime End
		{
			get
			{
				return this.end;
			}
		}

		/// <summary>Gets the first date/time value in the range that the user has selected.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that represents the first date in the date range that the user has selected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06001654 RID: 5716 RVA: 0x00052334 File Offset: 0x00050534
		public DateTime Start
		{
			get
			{
				return this.start;
			}
		}

		// Token: 0x04000C48 RID: 3144
		private DateTime end;

		// Token: 0x04000C49 RID: 3145
		private DateTime start;
	}
}
