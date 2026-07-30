using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the type of selection in a <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002BD RID: 701
	[Flags]
	public enum RichTextBoxSelectionTypes
	{
		/// <summary>No text is selected in the current selection.</summary>
		// Token: 0x04001660 RID: 5728
		Empty = 0,
		/// <summary>The current selection contains only text.</summary>
		// Token: 0x04001661 RID: 5729
		Text = 1,
		/// <summary>At least one Object Linking and Embedding (OLE) object is selected.</summary>
		// Token: 0x04001662 RID: 5730
		Object = 2,
		/// <summary>More than one character is selected.</summary>
		// Token: 0x04001663 RID: 5731
		MultiChar = 4,
		/// <summary>More than one Object Linking and Embedding (OLE) object is selected.</summary>
		// Token: 0x04001664 RID: 5732
		MultiObject = 8
	}
}
