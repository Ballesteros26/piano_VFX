using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the date selection mode of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	// Token: 0x02000285 RID: 645
	public enum CalendarSelectionMode
	{
		/// <summary>No dates can be selected on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		// Token: 0x04001686 RID: 5766
		None,
		/// <summary>A single date can be selected on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		// Token: 0x04001687 RID: 5767
		Day,
		/// <summary>A single date or entire week can be selected on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		// Token: 0x04001688 RID: 5768
		DayWeek,
		/// <summary>A single date, week, or entire month can be selected on the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		// Token: 0x04001689 RID: 5769
		DayWeekMonth
	}
}
