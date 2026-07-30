using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.Calendar.VisibleMonthChanged" /> event of a <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	// Token: 0x020002EA RID: 746
	public class MonthChangedEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.MonthChangedEventArgs" /> class.</summary>
		/// <param name="newDate">The date that determines the month currently displayed by the <see cref="T:System.Web.UI.WebControls.Calendar" />. </param>
		/// <param name="previousDate">The date that determines the month previously displayed by the <see cref="T:System.Web.UI.WebControls.Calendar" />. </param>
		// Token: 0x06001BA8 RID: 7080 RVA: 0x0004613E File Offset: 0x0004433E
		public MonthChangedEventArgs(DateTime newDate, DateTime previousDate)
		{
			this.newDate = newDate;
			this.previousDate = previousDate;
		}

		/// <summary>Gets the date that determines the currently displayed month in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>The date that determines the month currently displayed by the <see cref="T:System.Web.UI.WebControls.Calendar" />.</returns>
		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x00046154 File Offset: 0x00044354
		public DateTime NewDate
		{
			get
			{
				return this.newDate;
			}
		}

		/// <summary>Gets the date that determines the previously displayed month in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>The date that determines the month previously displayed by the <see cref="T:System.Web.UI.WebControls.Calendar" />.</returns>
		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x0004615C File Offset: 0x0004435C
		public DateTime PreviousDate
		{
			get
			{
				return this.previousDate;
			}
		}

		// Token: 0x04001725 RID: 5925
		private DateTime newDate;

		// Token: 0x04001726 RID: 5926
		private DateTime previousDate;
	}
}
