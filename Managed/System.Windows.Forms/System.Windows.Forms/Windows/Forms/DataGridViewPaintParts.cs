using System;

namespace System.Windows.Forms
{
	/// <summary>Defines values for specifying the parts of a <see cref="T:System.Windows.Forms.DataGridViewCell" /> that are to be painted.</summary>
	// Token: 0x0200011E RID: 286
	[Flags]
	public enum DataGridViewPaintParts
	{
		/// <summary>Nothing should be painted.</summary>
		// Token: 0x04000BDE RID: 3038
		None = 0,
		/// <summary>The background of the cell should be painted.</summary>
		// Token: 0x04000BDF RID: 3039
		Background = 1,
		/// <summary>The border of the cell should be painted.</summary>
		// Token: 0x04000BE0 RID: 3040
		Border = 2,
		/// <summary>The background of the cell content should be painted.</summary>
		// Token: 0x04000BE1 RID: 3041
		ContentBackground = 4,
		/// <summary>The foreground of the cell content should be painted.</summary>
		// Token: 0x04000BE2 RID: 3042
		ContentForeground = 8,
		/// <summary>The cell error icon should be painted.</summary>
		// Token: 0x04000BE3 RID: 3043
		ErrorIcon = 16,
		/// <summary>The focus rectangle should be painted around the cell.</summary>
		// Token: 0x04000BE4 RID: 3044
		Focus = 32,
		/// <summary>The background of the cell should be painted when the cell is selected.</summary>
		// Token: 0x04000BE5 RID: 3045
		SelectionBackground = 64,
		/// <summary>All parts of the cell should be painted.</summary>
		// Token: 0x04000BE6 RID: 3046
		All = 127
	}
}
