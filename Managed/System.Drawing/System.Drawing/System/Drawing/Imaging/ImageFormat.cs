using System;
using System.ComponentModel;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies the file format of the image. Not inheritable.</summary>
	// Token: 0x02000114 RID: 276
	[TypeConverter(typeof(ImageFormatConverter))]
	public sealed class ImageFormat
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ImageFormat" /> class by using the specified <see cref="T:System.Guid" /> structure.</summary>
		/// <param name="guid">The <see cref="T:System.Guid" /> structure that specifies a particular image format. </param>
		// Token: 0x06000CD5 RID: 3285 RVA: 0x0001CA55 File Offset: 0x0001AC55
		public ImageFormat(Guid guid)
		{
			this.guid = guid;
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0001CA64 File Offset: 0x0001AC64
		private ImageFormat(string name, string guid)
		{
			this.name = name;
			this.guid = new Guid(guid);
		}

		/// <summary>Returns a value that indicates whether the specified object is an <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that is equivalent to this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object.</summary>
		/// <returns>true if <paramref name="o" /> is an <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that is equivalent to this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object; otherwise, false.</returns>
		/// <param name="o">The object to test. </param>
		// Token: 0x06000CD7 RID: 3287 RVA: 0x0001CA80 File Offset: 0x0001AC80
		public override bool Equals(object o)
		{
			ImageFormat imageFormat = o as ImageFormat;
			return imageFormat != null && imageFormat.Guid.Equals(this.guid);
		}

		/// <summary>Returns a hash code value that represents this object.</summary>
		/// <returns>A hash code that represents this object.</returns>
		// Token: 0x06000CD8 RID: 3288 RVA: 0x0001CAAD File Offset: 0x0001ACAD
		public override int GetHashCode()
		{
			return this.guid.GetHashCode();
		}

		/// <summary>Converts this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object to a human-readable string.</summary>
		/// <returns>A string that represents this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object.</returns>
		// Token: 0x06000CD9 RID: 3289 RVA: 0x0001CAC0 File Offset: 0x0001ACC0
		public override string ToString()
		{
			if (this.name != null)
			{
				return this.name;
			}
			return "[ImageFormat: " + this.guid.ToString() + "]";
		}

		/// <summary>Gets a <see cref="T:System.Guid" /> structure that represents this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure that represents this <see cref="T:System.Drawing.Imaging.ImageFormat" /> object.</returns>
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x0001CAF1 File Offset: 0x0001ACF1
		public Guid Guid
		{
			get
			{
				return this.guid;
			}
		}

		/// <summary>Gets the bitmap (BMP) image format.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the bitmap image format.</returns>
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0001CAFC File Offset: 0x0001ACFC
		public static ImageFormat Bmp
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat bmpImageFormat;
				lock (obj)
				{
					if (ImageFormat.BmpImageFormat == null)
					{
						ImageFormat.BmpImageFormat = new ImageFormat("Bmp", "b96b3cab-0728-11d3-9d7b-0000f81ef32e");
					}
					bmpImageFormat = ImageFormat.BmpImageFormat;
				}
				return bmpImageFormat;
			}
		}

		/// <summary>Gets the enhanced metafile (EMF) image format.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the enhanced metafile image format.</returns>
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0001CB58 File Offset: 0x0001AD58
		public static ImageFormat Emf
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat emfImageFormat;
				lock (obj)
				{
					if (ImageFormat.EmfImageFormat == null)
					{
						ImageFormat.EmfImageFormat = new ImageFormat("Emf", "b96b3cac-0728-11d3-9d7b-0000f81ef32e");
					}
					emfImageFormat = ImageFormat.EmfImageFormat;
				}
				return emfImageFormat;
			}
		}

		/// <summary>Gets the Exchangeable Image File (Exif) format.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the Exif format.</returns>
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0001CBB4 File Offset: 0x0001ADB4
		public static ImageFormat Exif
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat exifImageFormat;
				lock (obj)
				{
					if (ImageFormat.ExifImageFormat == null)
					{
						ImageFormat.ExifImageFormat = new ImageFormat("Exif", "b96b3cb2-0728-11d3-9d7b-0000f81ef32e");
					}
					exifImageFormat = ImageFormat.ExifImageFormat;
				}
				return exifImageFormat;
			}
		}

		/// <summary>Gets the Graphics Interchange Format (GIF) image format.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the GIF image format.</returns>
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x0001CC10 File Offset: 0x0001AE10
		public static ImageFormat Gif
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat gifImageFormat;
				lock (obj)
				{
					if (ImageFormat.GifImageFormat == null)
					{
						ImageFormat.GifImageFormat = new ImageFormat("Gif", "b96b3cb0-0728-11d3-9d7b-0000f81ef32e");
					}
					gifImageFormat = ImageFormat.GifImageFormat;
				}
				return gifImageFormat;
			}
		}

		/// <summary>Gets the Windows icon image format.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the Windows icon image format.</returns>
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0001CC6C File Offset: 0x0001AE6C
		public static ImageFormat Icon
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat iconImageFormat;
				lock (obj)
				{
					if (ImageFormat.IconImageFormat == null)
					{
						ImageFormat.IconImageFormat = new ImageFormat("Icon", "b96b3cb5-0728-11d3-9d7b-0000f81ef32e");
					}
					iconImageFormat = ImageFormat.IconImageFormat;
				}
				return iconImageFormat;
			}
		}

		/// <summary>Gets the Joint Photographic Experts Group (JPEG) image format.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the JPEG image format.</returns>
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0001CCC8 File Offset: 0x0001AEC8
		public static ImageFormat Jpeg
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat jpegImageFormat;
				lock (obj)
				{
					if (ImageFormat.JpegImageFormat == null)
					{
						ImageFormat.JpegImageFormat = new ImageFormat("Jpeg", "b96b3cae-0728-11d3-9d7b-0000f81ef32e");
					}
					jpegImageFormat = ImageFormat.JpegImageFormat;
				}
				return jpegImageFormat;
			}
		}

		/// <summary>Gets the format of a bitmap in memory.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the format of a bitmap in memory.</returns>
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0001CD24 File Offset: 0x0001AF24
		public static ImageFormat MemoryBmp
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat memoryBmpImageFormat;
				lock (obj)
				{
					if (ImageFormat.MemoryBmpImageFormat == null)
					{
						ImageFormat.MemoryBmpImageFormat = new ImageFormat("MemoryBMP", "b96b3caa-0728-11d3-9d7b-0000f81ef32e");
					}
					memoryBmpImageFormat = ImageFormat.MemoryBmpImageFormat;
				}
				return memoryBmpImageFormat;
			}
		}

		/// <summary>Gets the W3C Portable Network Graphics (PNG) image format.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the PNG image format.</returns>
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0001CD80 File Offset: 0x0001AF80
		public static ImageFormat Png
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat pngImageFormat;
				lock (obj)
				{
					if (ImageFormat.PngImageFormat == null)
					{
						ImageFormat.PngImageFormat = new ImageFormat("Png", "b96b3caf-0728-11d3-9d7b-0000f81ef32e");
					}
					pngImageFormat = ImageFormat.PngImageFormat;
				}
				return pngImageFormat;
			}
		}

		/// <summary>Gets the Tagged Image File Format (TIFF) image format.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the TIFF image format.</returns>
		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x0001CDDC File Offset: 0x0001AFDC
		public static ImageFormat Tiff
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat tiffImageFormat;
				lock (obj)
				{
					if (ImageFormat.TiffImageFormat == null)
					{
						ImageFormat.TiffImageFormat = new ImageFormat("Tiff", "b96b3cb1-0728-11d3-9d7b-0000f81ef32e");
					}
					tiffImageFormat = ImageFormat.TiffImageFormat;
				}
				return tiffImageFormat;
			}
		}

		/// <summary>Gets the Windows metafile (WMF) image format.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.ImageFormat" /> object that indicates the Windows metafile image format.</returns>
		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0001CE38 File Offset: 0x0001B038
		public static ImageFormat Wmf
		{
			get
			{
				object obj = ImageFormat.locker;
				ImageFormat wmfImageFormat;
				lock (obj)
				{
					if (ImageFormat.WmfImageFormat == null)
					{
						ImageFormat.WmfImageFormat = new ImageFormat("Wmf", "b96b3cad-0728-11d3-9d7b-0000f81ef32e");
					}
					wmfImageFormat = ImageFormat.WmfImageFormat;
				}
				return wmfImageFormat;
			}
		}

		// Token: 0x04000A3E RID: 2622
		private Guid guid;

		// Token: 0x04000A3F RID: 2623
		private string name;

		// Token: 0x04000A40 RID: 2624
		private const string BmpGuid = "b96b3cab-0728-11d3-9d7b-0000f81ef32e";

		// Token: 0x04000A41 RID: 2625
		private const string EmfGuid = "b96b3cac-0728-11d3-9d7b-0000f81ef32e";

		// Token: 0x04000A42 RID: 2626
		private const string ExifGuid = "b96b3cb2-0728-11d3-9d7b-0000f81ef32e";

		// Token: 0x04000A43 RID: 2627
		private const string GifGuid = "b96b3cb0-0728-11d3-9d7b-0000f81ef32e";

		// Token: 0x04000A44 RID: 2628
		private const string TiffGuid = "b96b3cb1-0728-11d3-9d7b-0000f81ef32e";

		// Token: 0x04000A45 RID: 2629
		private const string PngGuid = "b96b3caf-0728-11d3-9d7b-0000f81ef32e";

		// Token: 0x04000A46 RID: 2630
		private const string MemoryBmpGuid = "b96b3caa-0728-11d3-9d7b-0000f81ef32e";

		// Token: 0x04000A47 RID: 2631
		private const string IconGuid = "b96b3cb5-0728-11d3-9d7b-0000f81ef32e";

		// Token: 0x04000A48 RID: 2632
		private const string JpegGuid = "b96b3cae-0728-11d3-9d7b-0000f81ef32e";

		// Token: 0x04000A49 RID: 2633
		private const string WmfGuid = "b96b3cad-0728-11d3-9d7b-0000f81ef32e";

		// Token: 0x04000A4A RID: 2634
		private static object locker = new object();

		// Token: 0x04000A4B RID: 2635
		private static ImageFormat BmpImageFormat;

		// Token: 0x04000A4C RID: 2636
		private static ImageFormat EmfImageFormat;

		// Token: 0x04000A4D RID: 2637
		private static ImageFormat ExifImageFormat;

		// Token: 0x04000A4E RID: 2638
		private static ImageFormat GifImageFormat;

		// Token: 0x04000A4F RID: 2639
		private static ImageFormat TiffImageFormat;

		// Token: 0x04000A50 RID: 2640
		private static ImageFormat PngImageFormat;

		// Token: 0x04000A51 RID: 2641
		private static ImageFormat MemoryBmpImageFormat;

		// Token: 0x04000A52 RID: 2642
		private static ImageFormat IconImageFormat;

		// Token: 0x04000A53 RID: 2643
		private static ImageFormat JpegImageFormat;

		// Token: 0x04000A54 RID: 2644
		private static ImageFormat WmfImageFormat;
	}
}
