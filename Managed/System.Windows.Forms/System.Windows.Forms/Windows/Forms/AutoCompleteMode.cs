using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the mode for the automatic completion feature used in the <see cref="T:System.Windows.Forms.ComboBox" /> and <see cref="T:System.Windows.Forms.TextBox" /> controls.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000047 RID: 71
	public enum AutoCompleteMode
	{
		/// <summary>Disables the automatic completion feature for the <see cref="T:System.Windows.Forms.ComboBox" /> and <see cref="T:System.Windows.Forms.TextBox" /> controls.</summary>
		// Token: 0x040005E1 RID: 1505
		None,
		/// <summary>Displays the auxiliary drop-down list associated with the edit control. This drop-down is populated with one or more suggested completion strings.</summary>
		// Token: 0x040005E2 RID: 1506
		Suggest,
		/// <summary>Appends the remainder of the most likely candidate string to the existing characters, highlighting the appended characters.</summary>
		// Token: 0x040005E3 RID: 1507
		Append,
		/// <summary>Applies both Suggest and Append options.</summary>
		// Token: 0x040005E4 RID: 1508
		SuggestAppend
	}
}
