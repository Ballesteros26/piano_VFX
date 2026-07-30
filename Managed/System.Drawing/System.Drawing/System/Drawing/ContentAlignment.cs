using System;
using System.ComponentModel;
using System.Drawing.Design;

namespace System.Drawing
{
	/// <summary>Specifies alignment of content on the drawing surface.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005D RID: 93
	[Editor("System.Drawing.Design.ContentAlignmentEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public enum ContentAlignment
	{
		/// <summary>Content is vertically aligned at the top, and horizontally aligned on the left.</summary>
		// Token: 0x04000382 RID: 898
		TopLeft = 1,
		/// <summary>Content is vertically aligned at the top, and horizontally aligned at the center.</summary>
		// Token: 0x04000383 RID: 899
		TopCenter,
		/// <summary>Content is vertically aligned at the top, and horizontally aligned on the right.</summary>
		// Token: 0x04000384 RID: 900
		TopRight = 4,
		/// <summary>Content is vertically aligned in the middle, and horizontally aligned on the left.</summary>
		// Token: 0x04000385 RID: 901
		MiddleLeft = 16,
		/// <summary>Content is vertically aligned in the middle, and horizontally aligned at the center.</summary>
		// Token: 0x04000386 RID: 902
		MiddleCenter = 32,
		/// <summary>Content is vertically aligned in the middle, and horizontally aligned on the right.</summary>
		// Token: 0x04000387 RID: 903
		MiddleRight = 64,
		/// <summary>Content is vertically aligned at the bottom, and horizontally aligned on the left.</summary>
		// Token: 0x04000388 RID: 904
		BottomLeft = 256,
		/// <summary>Content is vertically aligned at the bottom, and horizontally aligned at the center.</summary>
		// Token: 0x04000389 RID: 905
		BottomCenter = 512,
		/// <summary>Content is vertically aligned at the bottom, and horizontally aligned on the right.</summary>
		// Token: 0x0400038A RID: 906
		BottomRight = 1024
	}
}
