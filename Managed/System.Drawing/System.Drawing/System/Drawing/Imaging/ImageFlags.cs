using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies the attributes of the pixel data contained in an <see cref="T:System.Drawing.Image" /> object. The <see cref="P:System.Drawing.Image.Flags" /> property returns a member of this enumeration.</summary>
	// Token: 0x02000107 RID: 263
	[Flags]
	public enum ImageFlags
	{
		/// <summary>There is no format information.</summary>
		// Token: 0x040009B3 RID: 2483
		None = 0,
		/// <summary>The pixel data is scalable.</summary>
		// Token: 0x040009B4 RID: 2484
		Scalable = 1,
		/// <summary>The pixel data contains alpha information.</summary>
		// Token: 0x040009B5 RID: 2485
		HasAlpha = 2,
		/// <summary>Specifies that the pixel data has alpha values other than 0 (transparent) and 255 (opaque).</summary>
		// Token: 0x040009B6 RID: 2486
		HasTranslucent = 4,
		/// <summary>The pixel data is partially scalable, but there are some limitations.</summary>
		// Token: 0x040009B7 RID: 2487
		PartiallyScalable = 8,
		/// <summary>The pixel data uses an RGB color space.</summary>
		// Token: 0x040009B8 RID: 2488
		ColorSpaceRgb = 16,
		/// <summary>The pixel data uses a CMYK color space.</summary>
		// Token: 0x040009B9 RID: 2489
		ColorSpaceCmyk = 32,
		/// <summary>The pixel data is grayscale.</summary>
		// Token: 0x040009BA RID: 2490
		ColorSpaceGray = 64,
		/// <summary>Specifies that the image is stored using a YCBCR color space.</summary>
		// Token: 0x040009BB RID: 2491
		ColorSpaceYcbcr = 128,
		/// <summary>Specifies that the image is stored using a YCCK color space.</summary>
		// Token: 0x040009BC RID: 2492
		ColorSpaceYcck = 256,
		/// <summary>Specifies that dots per inch information is stored in the image.</summary>
		// Token: 0x040009BD RID: 2493
		HasRealDpi = 4096,
		/// <summary>Specifies that the pixel size is stored in the image.</summary>
		// Token: 0x040009BE RID: 2494
		HasRealPixelSize = 8192,
		/// <summary>The pixel data is read-only.</summary>
		// Token: 0x040009BF RID: 2495
		ReadOnly = 65536,
		/// <summary>The pixel data can be cached for faster access.</summary>
		// Token: 0x040009C0 RID: 2496
		Caching = 131072
	}
}
