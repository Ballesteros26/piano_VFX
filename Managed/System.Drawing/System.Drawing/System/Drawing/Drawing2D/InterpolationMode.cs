using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>The <see cref="T:System.Drawing.Drawing2D.InterpolationMode" /> enumeration specifies the algorithm that is used when images are scaled or rotated. </summary>
	// Token: 0x0200013F RID: 319
	public enum InterpolationMode
	{
		/// <summary>Equivalent to the <see cref="F:System.Drawing.Drawing2D.QualityMode.Invalid" /> element of the <see cref="T:System.Drawing.Drawing2D.QualityMode" /> enumeration.</summary>
		// Token: 0x04000AFF RID: 2815
		Invalid = -1,
		/// <summary>Specifies default mode.</summary>
		// Token: 0x04000B00 RID: 2816
		Default,
		/// <summary>Specifies low quality interpolation.</summary>
		// Token: 0x04000B01 RID: 2817
		Low,
		/// <summary>Specifies high quality interpolation.</summary>
		// Token: 0x04000B02 RID: 2818
		High,
		/// <summary>Specifies bilinear interpolation. No prefiltering is done. This mode is not suitable for shrinking an image below 50 percent of its original size. </summary>
		// Token: 0x04000B03 RID: 2819
		Bilinear,
		/// <summary>Specifies bicubic interpolation. No prefiltering is done. This mode is not suitable for shrinking an image below 25 percent of its original size.</summary>
		// Token: 0x04000B04 RID: 2820
		Bicubic,
		/// <summary>Specifies nearest-neighbor interpolation.</summary>
		// Token: 0x04000B05 RID: 2821
		NearestNeighbor,
		/// <summary>Specifies high-quality, bilinear interpolation. Prefiltering is performed to ensure high-quality shrinking. </summary>
		// Token: 0x04000B06 RID: 2822
		HighQualityBilinear,
		/// <summary>Specifies high-quality, bicubic interpolation. Prefiltering is performed to ensure high-quality shrinking. This mode produces the highest quality transformed images.</summary>
		// Token: 0x04000B07 RID: 2823
		HighQualityBicubic
	}
}
