using System;

namespace System.Drawing.Imaging
{
	/// <summary>Used to specify the parameter value passed to a JPEG or TIFF image encoder when using the <see cref="M:System.Drawing.Image.Save(System.String,System.Drawing.Imaging.ImageCodecInfo,System.Drawing.Imaging.EncoderParameters)" /> or <see cref="M:System.Drawing.Image.SaveAdd(System.Drawing.Imaging.EncoderParameters)" /> methods.</summary>
	// Token: 0x02000101 RID: 257
	public enum EncoderValue
	{
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x04000973 RID: 2419
		ColorTypeCMYK,
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x04000974 RID: 2420
		ColorTypeYCCK,
		/// <summary>Specifies the LZW compression scheme. Can be passed to the TIFF encoder as a parameter that belongs to the Compression category.</summary>
		// Token: 0x04000975 RID: 2421
		CompressionLZW,
		/// <summary>Specifies the CCITT3 compression scheme. Can be passed to the TIFF encoder as a parameter that belongs to the compression category.</summary>
		// Token: 0x04000976 RID: 2422
		CompressionCCITT3,
		/// <summary>Specifies the CCITT4 compression scheme. Can be passed to the TIFF encoder as a parameter that belongs to the compression category.</summary>
		// Token: 0x04000977 RID: 2423
		CompressionCCITT4,
		/// <summary>Specifies the RLE compression scheme. Can be passed to the TIFF encoder as a parameter that belongs to the compression category.</summary>
		// Token: 0x04000978 RID: 2424
		CompressionRle,
		/// <summary>Specifies no compression. Can be passed to the TIFF encoder as a parameter that belongs to the compression category.</summary>
		// Token: 0x04000979 RID: 2425
		CompressionNone,
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x0400097A RID: 2426
		ScanMethodInterlaced,
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x0400097B RID: 2427
		ScanMethodNonInterlaced,
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x0400097C RID: 2428
		VersionGif87,
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x0400097D RID: 2429
		VersionGif89,
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x0400097E RID: 2430
		RenderProgressive,
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x0400097F RID: 2431
		RenderNonProgressive,
		/// <summary>Specifies that the image is to be rotated clockwise 90 degrees about its center. Can be passed to the JPEG encoder as a parameter that belongs to the transformation category.</summary>
		// Token: 0x04000980 RID: 2432
		TransformRotate90,
		/// <summary>Specifies that the image is to be rotated 180 degrees about its center. Can be passed to the JPEG encoder as a parameter that belongs to the transformation category.</summary>
		// Token: 0x04000981 RID: 2433
		TransformRotate180,
		/// <summary>Specifies that the image is to be rotated clockwise 270 degrees about its center. Can be passed to the JPEG encoder as a parameter that belongs to the transformation category.</summary>
		// Token: 0x04000982 RID: 2434
		TransformRotate270,
		/// <summary>Specifies that the image is to be flipped horizontally (about the vertical axis). Can be passed to the JPEG encoder as a parameter that belongs to the transformation category.</summary>
		// Token: 0x04000983 RID: 2435
		TransformFlipHorizontal,
		/// <summary>Specifies that the image is to be flipped vertically (about the horizontal axis). Can be passed to the JPEG encoder as a parameter that belongs to the transformation category.</summary>
		// Token: 0x04000984 RID: 2436
		TransformFlipVertical,
		/// <summary>Specifies that the image has more than one frame (page). Can be passed to the TIFF encoder as a parameter that belongs to the save flag category.</summary>
		// Token: 0x04000985 RID: 2437
		MultiFrame,
		/// <summary>Specifies the last frame in a multiple-frame image. Can be passed to the TIFF encoder as a parameter that belongs to the save flag category.</summary>
		// Token: 0x04000986 RID: 2438
		LastFrame,
		/// <summary>Specifies that a multiple-frame file or stream should be closed. Can be passed to the TIFF encoder as a parameter that belongs to the save flag category.</summary>
		// Token: 0x04000987 RID: 2439
		Flush,
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x04000988 RID: 2440
		FrameDimensionTime,
		/// <summary>Not used in GDI+ version 1.0.</summary>
		// Token: 0x04000989 RID: 2441
		FrameDimensionResolution,
		/// <summary>Specifies that a frame is to be added to the page dimension of an image. Can be passed to the TIFF encoder as a parameter that belongs to the save flag category.</summary>
		// Token: 0x0400098A RID: 2442
		FrameDimensionPage
	}
}
