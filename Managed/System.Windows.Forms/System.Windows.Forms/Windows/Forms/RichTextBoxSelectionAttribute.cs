using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies whether any characters in the current selection have the style or attribute.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020002BC RID: 700
	public enum RichTextBoxSelectionAttribute
	{
		/// <summary>No characters.</summary>
		// Token: 0x0400165C RID: 5724
		None,
		/// <summary>All characters.</summary>
		// Token: 0x0400165D RID: 5725
		All,
		/// <summary>Some but not all characters.</summary>
		// Token: 0x0400165E RID: 5726
		Mixed = -1
	}
}
