using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies how a text search is carried out in a <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002B9 RID: 697
	[Flags]
	public enum RichTextBoxFinds
	{
		/// <summary>Locate all instances of the search text, whether the instances found in the search are whole words or not.</summary>
		// Token: 0x04001646 RID: 5702
		None = 0,
		/// <summary>Locate only instances of the search text that are whole words.</summary>
		// Token: 0x04001647 RID: 5703
		WholeWord = 2,
		/// <summary>Locate only instances of the search text that have the exact casing.</summary>
		// Token: 0x04001648 RID: 5704
		MatchCase = 4,
		/// <summary>The search text, if found, should not be highlighted.</summary>
		// Token: 0x04001649 RID: 5705
		NoHighlight = 8,
		/// <summary>The search starts at the end of the control's document and searches to the beginning of the document.</summary>
		// Token: 0x0400164A RID: 5706
		Reverse = 16
	}
}
