using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Defines different rules for determining the first week of the year.</summary>
	// Token: 0x020003FC RID: 1020
	[ComVisible(true)]
	[Serializable]
	public enum CalendarWeekRule
	{
		/// <summary>Indicates that the first week of the year starts on the first day of the year and ends before the following designated first day of the week. The value is 0.</summary>
		// Token: 0x04001909 RID: 6409
		FirstDay,
		/// <summary>Indicates that the first week of the year begins on the first occurrence of the designated first day of the week on or after the first day of the year. The value is 1.</summary>
		// Token: 0x0400190A RID: 6410
		FirstFullWeek,
		/// <summary>Indicates that the first week of the year is the first week with four or more days before the designated first day of the week. The value is 2.</summary>
		// Token: 0x0400190B RID: 6411
		FirstFourDayWeek
	}
}
