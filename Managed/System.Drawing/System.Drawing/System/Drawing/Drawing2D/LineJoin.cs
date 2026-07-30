using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies how to join consecutive line or curve segments in a figure (subpath) contained in a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object.</summary>
	// Token: 0x02000141 RID: 321
	public enum LineJoin
	{
		/// <summary>Specifies a mitered join. This produces a sharp corner or a clipped corner, depending on whether the length of the miter exceeds the miter limit.</summary>
		// Token: 0x04000B15 RID: 2837
		Miter,
		/// <summary>Specifies a beveled join. This produces a diagonal corner.</summary>
		// Token: 0x04000B16 RID: 2838
		Bevel,
		/// <summary>Specifies a circular join. This produces a smooth, circular arc between the lines.</summary>
		// Token: 0x04000B17 RID: 2839
		Round,
		/// <summary>Specifies a mitered join. This produces a sharp corner or a beveled corner, depending on whether the length of the miter exceeds the miter limit.</summary>
		// Token: 0x04000B18 RID: 2840
		MiterClipped
	}
}
