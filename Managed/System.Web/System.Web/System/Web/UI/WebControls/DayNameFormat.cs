using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the display format for the days of the week on a <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
	// Token: 0x0200029D RID: 669
	public enum DayNameFormat
	{
		/// <summary>The days of the week displayed in full format. For example, Monday.</summary>
		// Token: 0x040016B3 RID: 5811
		Full,
		/// <summary>The days of the week displayed in abbreviated format. For example, Mon represents Monday.</summary>
		// Token: 0x040016B4 RID: 5812
		Short,
		/// <summary>The days of the week displayed with just the first letter. For example, M represents Monday.</summary>
		// Token: 0x040016B5 RID: 5813
		FirstLetter,
		/// <summary>The days of the week displayed with just the first two letters. For example, Mo represents Monday.</summary>
		// Token: 0x040016B6 RID: 5814
		FirstTwoLetters,
		/// <summary>The days of the week displayed in the shortest abbreviation format possible for the current culture.</summary>
		// Token: 0x040016B7 RID: 5815
		Shortest
	}
}
