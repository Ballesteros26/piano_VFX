using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the formats used with text-related methods of the <see cref="T:System.Windows.Forms.Clipboard" /> and <see cref="T:System.Windows.Forms.DataObject" /> classes.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000321 RID: 801
	public enum TextDataFormat
	{
		/// <summary>Specifies the standard ANSI text format.</summary>
		// Token: 0x0400194B RID: 6475
		Text,
		/// <summary>Specifies the standard Windows Unicode text format.</summary>
		// Token: 0x0400194C RID: 6476
		UnicodeText,
		/// <summary>Specifies text consisting of rich text format (RTF) data.</summary>
		// Token: 0x0400194D RID: 6477
		Rtf,
		/// <summary>Specifies text consisting of HTML data.</summary>
		// Token: 0x0400194E RID: 6478
		Html,
		/// <summary>Specifies a comma-separated value (CSV) format, which is a common interchange format used by spreadsheets.</summary>
		// Token: 0x0400194F RID: 6479
		CommaSeparatedValue
	}
}
