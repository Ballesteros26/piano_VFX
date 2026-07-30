using System;

namespace System.Windows.Forms.Design.Behavior
{
	/// <summary>Specifies the orientation and relative location of a snapline.</summary>
	// Token: 0x0200004F RID: 79
	public enum SnapLineType
	{
		/// <summary>A horizontal snapline typically aligned to the top edge of a control.</summary>
		// Token: 0x0400010F RID: 271
		Top,
		/// <summary>A horizontal snapline typically aligned to the bottom edge of a control.</summary>
		// Token: 0x04000110 RID: 272
		Bottom,
		/// <summary>A vertical snapline typically aligned to the left edge of a control.</summary>
		// Token: 0x04000111 RID: 273
		Left,
		/// <summary>A vertical snapline typically aligned to the right edge of a control.</summary>
		// Token: 0x04000112 RID: 274
		Right,
		/// <summary>A horizontal snapline typically not associated with an edge of a control.</summary>
		// Token: 0x04000113 RID: 275
		Horizontal,
		/// <summary>A vertical snapline typically not associated with an edge of a control.</summary>
		// Token: 0x04000114 RID: 276
		Vertical,
		/// <summary>A horizontal snapline typically associated with a primary internal feature of a control; for example, the base of the text string in a <see cref="T:System.Windows.Forms.Label" /> control.</summary>
		// Token: 0x04000115 RID: 277
		Baseline
	}
}
