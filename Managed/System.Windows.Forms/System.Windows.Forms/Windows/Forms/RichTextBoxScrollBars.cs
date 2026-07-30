using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the type of scroll bars to display in a <see cref="T:System.Windows.Forms.RichTextBox" /> control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002BB RID: 699
	public enum RichTextBoxScrollBars
	{
		/// <summary>No scroll bars are displayed.</summary>
		// Token: 0x04001654 RID: 5716
		None,
		/// <summary>Display a horizontal scroll bar only when text is longer than the width of the control.</summary>
		// Token: 0x04001655 RID: 5717
		Horizontal,
		/// <summary>Display a vertical scroll bar only when text is longer than the height of the control.</summary>
		// Token: 0x04001656 RID: 5718
		Vertical,
		/// <summary>Display both a horizontal and a vertical scroll bar when needed.</summary>
		// Token: 0x04001657 RID: 5719
		Both,
		/// <summary>Always display a horizontal scroll bar.</summary>
		// Token: 0x04001658 RID: 5720
		ForcedHorizontal = 17,
		/// <summary>Always display a vertical scroll bar.</summary>
		// Token: 0x04001659 RID: 5721
		ForcedVertical,
		/// <summary>Always display both a horizontal and a vertical scroll bar.</summary>
		// Token: 0x0400165A RID: 5722
		ForcedBoth
	}
}
