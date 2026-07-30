using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies which edges of a visual style element to draw.</summary>
	// Token: 0x020004D9 RID: 1241
	[Flags]
	public enum Edges
	{
		/// <summary>The left edge of the element.</summary>
		// Token: 0x04002A4E RID: 10830
		Left = 1,
		/// <summary>The top edge of the element.</summary>
		// Token: 0x04002A4F RID: 10831
		Top = 2,
		/// <summary>The right edge of the element.</summary>
		// Token: 0x04002A50 RID: 10832
		Right = 4,
		/// <summary>The bottom edge of the element.</summary>
		// Token: 0x04002A51 RID: 10833
		Bottom = 8,
		/// <summary>A diagonal border.</summary>
		// Token: 0x04002A52 RID: 10834
		Diagonal = 16
	}
}
