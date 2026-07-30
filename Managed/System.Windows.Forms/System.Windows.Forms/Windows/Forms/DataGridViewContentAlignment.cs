using System;

namespace System.Windows.Forms
{
	/// <summary>Defines constants that indicate the alignment of content within a <see cref="T:System.Windows.Forms.DataGridView" /> cell.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200010D RID: 269
	public enum DataGridViewContentAlignment
	{
		/// <summary>The alignment is not set.</summary>
		// Token: 0x04000B8B RID: 2955
		NotSet,
		/// <summary>The content is aligned vertically at the top and horizontally at the left of a cell.</summary>
		// Token: 0x04000B8C RID: 2956
		TopLeft,
		/// <summary>The content is aligned vertically at the top and horizontally at the center of a cell.</summary>
		// Token: 0x04000B8D RID: 2957
		TopCenter,
		/// <summary>The content is aligned vertically at the top and horizontally at the right of a cell.</summary>
		// Token: 0x04000B8E RID: 2958
		TopRight = 4,
		/// <summary>The content is aligned vertically at the middle and horizontally at the left of a cell.</summary>
		// Token: 0x04000B8F RID: 2959
		MiddleLeft = 16,
		/// <summary>The content is aligned at the vertical and horizontal center of a cell.</summary>
		// Token: 0x04000B90 RID: 2960
		MiddleCenter = 32,
		/// <summary>The content is aligned vertically at the middle and horizontally at the right of a cell.</summary>
		// Token: 0x04000B91 RID: 2961
		MiddleRight = 64,
		/// <summary>The content is aligned vertically at the bottom and horizontally at the left of a cell.</summary>
		// Token: 0x04000B92 RID: 2962
		BottomLeft = 256,
		/// <summary>The content is aligned vertically at the bottom and horizontally at the center of a cell.</summary>
		// Token: 0x04000B93 RID: 2963
		BottomCenter = 512,
		/// <summary>The content is aligned vertically at the bottom and horizontally at the right of a cell.</summary>
		// Token: 0x04000B94 RID: 2964
		BottomRight = 1024
	}
}
