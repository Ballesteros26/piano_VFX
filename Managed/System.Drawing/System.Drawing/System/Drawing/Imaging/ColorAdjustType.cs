using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies which GDI+ objects use color adjustment information.</summary>
	// Token: 0x020000F2 RID: 242
	public enum ColorAdjustType
	{
		/// <summary>Color adjustment information that is used by all GDI+ objects that do not have their own color adjustment information.</summary>
		// Token: 0x04000820 RID: 2080
		Default,
		/// <summary>Color adjustment information for <see cref="T:System.Drawing.Bitmap" /> objects.</summary>
		// Token: 0x04000821 RID: 2081
		Bitmap,
		/// <summary>Color adjustment information for <see cref="T:System.Drawing.Brush" /> objects.</summary>
		// Token: 0x04000822 RID: 2082
		Brush,
		/// <summary>Color adjustment information for <see cref="T:System.Drawing.Pen" /> objects.</summary>
		// Token: 0x04000823 RID: 2083
		Pen,
		/// <summary>Color adjustment information for text.</summary>
		// Token: 0x04000824 RID: 2084
		Text,
		/// <summary>The number of types specified.</summary>
		// Token: 0x04000825 RID: 2085
		Count,
		/// <summary>The number of types specified.</summary>
		// Token: 0x04000826 RID: 2086
		Any
	}
}
