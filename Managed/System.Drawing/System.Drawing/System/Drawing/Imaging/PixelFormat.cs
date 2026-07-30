using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies the format of the color data for each pixel in the image.</summary>
	// Token: 0x0200010E RID: 270
	public enum PixelFormat
	{
		/// <summary>The pixel data contains color-indexed values, which means the values are an index to colors in the system color table, as opposed to individual color values.</summary>
		// Token: 0x04000A05 RID: 2565
		Indexed = 65536,
		/// <summary>The pixel data contains GDI colors.</summary>
		// Token: 0x04000A06 RID: 2566
		Gdi = 131072,
		/// <summary>The pixel data contains alpha values that are not premultiplied.</summary>
		// Token: 0x04000A07 RID: 2567
		Alpha = 262144,
		/// <summary>The pixel format contains premultiplied alpha values.</summary>
		// Token: 0x04000A08 RID: 2568
		PAlpha = 524288,
		/// <summary>Reserved.</summary>
		// Token: 0x04000A09 RID: 2569
		Extended = 1048576,
		/// <summary>The default pixel format of 32 bits per pixel. The format specifies 24-bit color depth and an 8-bit alpha channel.</summary>
		// Token: 0x04000A0A RID: 2570
		Canonical = 2097152,
		/// <summary>The pixel format is undefined.</summary>
		// Token: 0x04000A0B RID: 2571
		Undefined = 0,
		/// <summary>No pixel format is specified.</summary>
		// Token: 0x04000A0C RID: 2572
		DontCare = 0,
		/// <summary>Specifies that the pixel format is 1 bit per pixel and that it uses indexed color. The color table therefore has two colors in it.</summary>
		// Token: 0x04000A0D RID: 2573
		Format1bppIndexed = 196865,
		/// <summary>Specifies that the format is 4 bits per pixel, indexed.</summary>
		// Token: 0x04000A0E RID: 2574
		Format4bppIndexed = 197634,
		/// <summary>Specifies that the format is 8 bits per pixel, indexed. The color table therefore has 256 colors in it.</summary>
		// Token: 0x04000A0F RID: 2575
		Format8bppIndexed = 198659,
		/// <summary>The pixel format is 16 bits per pixel. The color information specifies 65536 shades of gray.</summary>
		// Token: 0x04000A10 RID: 2576
		Format16bppGrayScale = 1052676,
		/// <summary>Specifies that the format is 16 bits per pixel; 5 bits each are used for the red, green, and blue components. The remaining bit is not used.</summary>
		// Token: 0x04000A11 RID: 2577
		Format16bppRgb555 = 135173,
		/// <summary>Specifies that the format is 16 bits per pixel; 5 bits are used for the red component, 6 bits are used for the green component, and 5 bits are used for the blue component.</summary>
		// Token: 0x04000A12 RID: 2578
		Format16bppRgb565,
		/// <summary>The pixel format is 16 bits per pixel. The color information specifies 32,768 shades of color, of which 5 bits are red, 5 bits are green, 5 bits are blue, and 1 bit is alpha.</summary>
		// Token: 0x04000A13 RID: 2579
		Format16bppArgb1555 = 397319,
		/// <summary>Specifies that the format is 24 bits per pixel; 8 bits each are used for the red, green, and blue components.</summary>
		// Token: 0x04000A14 RID: 2580
		Format24bppRgb = 137224,
		/// <summary>Specifies that the format is 32 bits per pixel; 8 bits each are used for the red, green, and blue components. The remaining 8 bits are not used.</summary>
		// Token: 0x04000A15 RID: 2581
		Format32bppRgb = 139273,
		/// <summary>Specifies that the format is 32 bits per pixel; 8 bits each are used for the alpha, red, green, and blue components.</summary>
		// Token: 0x04000A16 RID: 2582
		Format32bppArgb = 2498570,
		/// <summary>Specifies that the format is 32 bits per pixel; 8 bits each are used for the alpha, red, green, and blue components. The red, green, and blue components are premultiplied, according to the alpha component.</summary>
		// Token: 0x04000A17 RID: 2583
		Format32bppPArgb = 925707,
		/// <summary>Specifies that the format is 48 bits per pixel; 16 bits each are used for the red, green, and blue components.</summary>
		// Token: 0x04000A18 RID: 2584
		Format48bppRgb = 1060876,
		/// <summary>Specifies that the format is 64 bits per pixel; 16 bits each are used for the alpha, red, green, and blue components.</summary>
		// Token: 0x04000A19 RID: 2585
		Format64bppArgb = 3424269,
		/// <summary>Specifies that the format is 64 bits per pixel; 16 bits each are used for the alpha, red, green, and blue components. The red, green, and blue components are premultiplied according to the alpha component.</summary>
		// Token: 0x04000A1A RID: 2586
		Format64bppPArgb = 1851406,
		/// <summary>The maximum value for this enumeration.</summary>
		// Token: 0x04000A1B RID: 2587
		Max = 15
	}
}
