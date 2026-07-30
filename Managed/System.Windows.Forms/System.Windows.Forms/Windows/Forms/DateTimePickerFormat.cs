using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the date and time format the <see cref="T:System.Windows.Forms.DateTimePicker" /> control displays.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000147 RID: 327
	public enum DateTimePickerFormat
	{
		/// <summary>The <see cref="T:System.Windows.Forms.DateTimePicker" /> control displays the date/time value in a custom format. For more information, see <see cref="P:System.Windows.Forms.DateTimePicker.CustomFormat" />.</summary>
		// Token: 0x04000C8B RID: 3211
		Custom = 8,
		/// <summary>The <see cref="T:System.Windows.Forms.DateTimePicker" /> control displays the date/time value in the long date format set by the user's operating system.</summary>
		// Token: 0x04000C8C RID: 3212
		Long = 1,
		/// <summary>The <see cref="T:System.Windows.Forms.DateTimePicker" /> control displays the date/time value in the short date format set by the user's operating system.</summary>
		// Token: 0x04000C8D RID: 3213
		Short,
		/// <summary>The <see cref="T:System.Windows.Forms.DateTimePicker" /> control displays the date/time value in the time format set by the user's operating system.</summary>
		// Token: 0x04000C8E RID: 3214
		Time = 4
	}
}
