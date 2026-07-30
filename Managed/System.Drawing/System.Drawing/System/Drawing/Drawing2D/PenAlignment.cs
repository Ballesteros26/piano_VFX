using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the alignment of a <see cref="T:System.Drawing.Pen" /> object in relation to the theoretical, zero-width line.</summary>
	// Token: 0x02000146 RID: 326
	public enum PenAlignment
	{
		/// <summary>Specifies that the <see cref="T:System.Drawing.Pen" /> object is centered over the theoretical line.</summary>
		// Token: 0x04000B2D RID: 2861
		Center,
		/// <summary>Specifies that the <see cref="T:System.Drawing.Pen" /> is positioned on the inside of the theoretical line.</summary>
		// Token: 0x04000B2E RID: 2862
		Inset,
		/// <summary>Specifies the <see cref="T:System.Drawing.Pen" /> is positioned on the outside of the theoretical line.</summary>
		// Token: 0x04000B2F RID: 2863
		Outset,
		/// <summary>Specifies the <see cref="T:System.Drawing.Pen" /> is positioned to the left of the theoretical line.</summary>
		// Token: 0x04000B30 RID: 2864
		Left,
		/// <summary>Specifies the <see cref="T:System.Drawing.Pen" /> is positioned to the right of the theoretical line.</summary>
		// Token: 0x04000B31 RID: 2865
		Right
	}
}
