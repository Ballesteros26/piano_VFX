using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies the type of point in a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object.</summary>
	// Token: 0x02000145 RID: 325
	public enum PathPointType
	{
		/// <summary>The starting point of a <see cref="T:System.Drawing.Drawing2D.GraphicsPath" /> object.</summary>
		// Token: 0x04000B24 RID: 2852
		Start,
		/// <summary>A line segment.</summary>
		// Token: 0x04000B25 RID: 2853
		Line,
		/// <summary>A default Bézier curve.</summary>
		// Token: 0x04000B26 RID: 2854
		Bezier = 3,
		/// <summary>A mask point.</summary>
		// Token: 0x04000B27 RID: 2855
		PathTypeMask = 7,
		/// <summary>The corresponding segment is dashed.</summary>
		// Token: 0x04000B28 RID: 2856
		DashMode = 16,
		/// <summary>A path marker.</summary>
		// Token: 0x04000B29 RID: 2857
		PathMarker = 32,
		/// <summary>The endpoint of a subpath.</summary>
		// Token: 0x04000B2A RID: 2858
		CloseSubpath = 128,
		/// <summary>A cubic Bézier curve.</summary>
		// Token: 0x04000B2B RID: 2859
		Bezier3 = 3
	}
}
