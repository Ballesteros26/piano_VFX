using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Defines a graphic metafile. A metafile contains records that describe a sequence of graphics operations that can be recorded (constructed) and played back (displayed). This class is not inheritable.</summary>
	// Token: 0x02000117 RID: 279
	[MonoTODO("Metafiles, both WMF and EMF formats, are only partially supported.")]
	[Editor("System.Drawing.Design.MetafileEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Serializable]
	public sealed class Metafile : Image
	{
		// Token: 0x06000CF6 RID: 3318 RVA: 0x00004350 File Offset: 0x00002550
		internal Metafile(IntPtr ptr)
		{
			this.nativeObject = ptr;
		}

		// Token: 0x06000CF7 RID: 3319 RVA: 0x0000435F File Offset: 0x0000255F
		internal Metafile(IntPtr ptr, Stream stream)
		{
			if (GDIPlus.RunningOnWindows())
			{
				this.stream = stream;
			}
			this.nativeObject = ptr;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> from which to create the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> is null.</exception>
		// Token: 0x06000CF8 RID: 3320 RVA: 0x0001D070 File Offset: 0x0001B270
		public Metafile(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentException("stream");
			}
			Status status;
			if (GDIPlus.RunningOnUnix())
			{
				GDIPlus.GdiPlusStreamHelper gdiPlusStreamHelper = new GDIPlus.GdiPlusStreamHelper(stream, false);
				status = GDIPlus.GdipCreateMetafileFromDelegate_linux(gdiPlusStreamHelper.GetHeaderDelegate, gdiPlusStreamHelper.GetBytesDelegate, gdiPlusStreamHelper.PutBytesDelegate, gdiPlusStreamHelper.SeekDelegate, gdiPlusStreamHelper.CloseDelegate, gdiPlusStreamHelper.SizeDelegate, out this.nativeObject);
			}
			else
			{
				status = GDIPlus.GdipCreateMetafileFromStream(new ComIStreamWrapper(stream), out this.nativeObject);
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified file name.</summary>
		/// <param name="filename">A <see cref="T:System.String" /> that represents the file name from which to create the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000CF9 RID: 3321 RVA: 0x0001D0EC File Offset: 0x0001B2EC
		public Metafile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			if (filename.Length == 0)
			{
				throw new ArgumentException("filename");
			}
			Status status = GDIPlus.GdipCreateMetafileFromFile(filename, out this.nativeObject);
			if (status == Status.GenericError)
			{
				throw new ExternalException("Couldn't load specified file.");
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified handle.</summary>
		/// <param name="henhmetafile">A handle to an enhanced metafile. </param>
		/// <param name="deleteEmf">true to delete the enhanced metafile handle when the <see cref="T:System.Drawing.Imaging.Metafile" /> is deleted; otherwise, false. </param>
		// Token: 0x06000CFA RID: 3322 RVA: 0x0001D140 File Offset: 0x0001B340
		public Metafile(IntPtr henhmetafile, bool deleteEmf)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateMetafileFromEmf(henhmetafile, deleteEmf, out this.nativeObject));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified handle to a device context and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="referenceHdc">The handle to a device context. </param>
		/// <param name="emfType">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000CFB RID: 3323 RVA: 0x0001D15C File Offset: 0x0001B35C
		public Metafile(IntPtr referenceHdc, EmfType emfType)
			: this(referenceHdc, default(RectangleF), MetafileFrameUnit.GdiCompatible, emfType, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle.</summary>
		/// <param name="referenceHdc">The handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000CFC RID: 3324 RVA: 0x0001D17C File Offset: 0x0001B37C
		public Metafile(IntPtr referenceHdc, Rectangle frameRect)
			: this(referenceHdc, frameRect, MetafileFrameUnit.GdiCompatible, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle.</summary>
		/// <param name="referenceHdc">The handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000CFD RID: 3325 RVA: 0x0001D189 File Offset: 0x0001B389
		public Metafile(IntPtr referenceHdc, RectangleF frameRect)
			: this(referenceHdc, frameRect, MetafileFrameUnit.GdiCompatible, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified handle and a <see cref="T:System.Drawing.Imaging.WmfPlaceableFileHeader" />.</summary>
		/// <param name="hmetafile">A windows handle to a <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="wmfHeader">A <see cref="T:System.Drawing.Imaging.WmfPlaceableFileHeader" />. </param>
		// Token: 0x06000CFE RID: 3326 RVA: 0x0001D196 File Offset: 0x0001B396
		public Metafile(IntPtr hmetafile, WmfPlaceableFileHeader wmfHeader)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateMetafileFromEmf(hmetafile, false, out this.nativeObject));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		// Token: 0x06000CFF RID: 3327 RVA: 0x0001D1B0 File Offset: 0x0001B3B0
		public Metafile(Stream stream, IntPtr referenceHdc)
			: this(stream, referenceHdc, default(RectangleF), MetafileFrameUnit.GdiCompatible, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		// Token: 0x06000D00 RID: 3328 RVA: 0x0001D1D4 File Offset: 0x0001B3D4
		public Metafile(string fileName, IntPtr referenceHdc)
			: this(fileName, referenceHdc, default(RectangleF), MetafileFrameUnit.GdiCompatible, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified handle to a device context and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A string can be supplied to name the file.</summary>
		/// <param name="referenceHdc">The handle to a device context. </param>
		/// <param name="emfType">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="description">A descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D01 RID: 3329 RVA: 0x0001D1F8 File Offset: 0x0001B3F8
		public Metafile(IntPtr referenceHdc, EmfType emfType, string description)
			: this(referenceHdc, default(RectangleF), MetafileFrameUnit.GdiCompatible, emfType, description)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle that uses the supplied unit of measure.</summary>
		/// <param name="referenceHdc">The handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		// Token: 0x06000D02 RID: 3330 RVA: 0x0001D218 File Offset: 0x0001B418
		public Metafile(IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit)
			: this(referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle that uses the supplied unit of measure.</summary>
		/// <param name="referenceHdc">The handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		// Token: 0x06000D03 RID: 3331 RVA: 0x0001D225 File Offset: 0x0001B425
		public Metafile(IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit)
			: this(referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified handle and a <see cref="T:System.Drawing.Imaging.WmfPlaceableFileHeader" />. Also, the <paramref name="deleteWmf" /> parameter can be used to delete the handle when the metafile is deleted.</summary>
		/// <param name="hmetafile">A windows handle to a <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="wmfHeader">A <see cref="T:System.Drawing.Imaging.WmfPlaceableFileHeader" />. </param>
		/// <param name="deleteWmf">true to delete the handle to the new <see cref="T:System.Drawing.Imaging.Metafile" /> when the <see cref="T:System.Drawing.Imaging.Metafile" /> is deleted; otherwise, false. </param>
		// Token: 0x06000D04 RID: 3332 RVA: 0x0001D232 File Offset: 0x0001B432
		public Metafile(IntPtr hmetafile, WmfPlaceableFileHeader wmfHeader, bool deleteWmf)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipCreateMetafileFromEmf(hmetafile, deleteWmf, out this.nativeObject));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D05 RID: 3333 RVA: 0x0001D24C File Offset: 0x0001B44C
		public Metafile(Stream stream, IntPtr referenceHdc, EmfType type)
			: this(stream, referenceHdc, default(RectangleF), MetafileFrameUnit.GdiCompatible, type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, and a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D06 RID: 3334 RVA: 0x0001D26D File Offset: 0x0001B46D
		public Metafile(Stream stream, IntPtr referenceHdc, Rectangle frameRect)
			: this(stream, referenceHdc, frameRect, MetafileFrameUnit.GdiCompatible, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, and a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D07 RID: 3335 RVA: 0x0001D27B File Offset: 0x0001B47B
		public Metafile(Stream stream, IntPtr referenceHdc, RectangleF frameRect)
			: this(stream, referenceHdc, frameRect, MetafileFrameUnit.GdiCompatible, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D08 RID: 3336 RVA: 0x0001D28C File Offset: 0x0001B48C
		public Metafile(string fileName, IntPtr referenceHdc, EmfType type)
			: this(fileName, referenceHdc, default(RectangleF), MetafileFrameUnit.GdiCompatible, type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, and a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D09 RID: 3337 RVA: 0x0001D2AD File Offset: 0x0001B4AD
		public Metafile(string fileName, IntPtr referenceHdc, Rectangle frameRect)
			: this(fileName, referenceHdc, frameRect, MetafileFrameUnit.GdiCompatible, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, and a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D0A RID: 3338 RVA: 0x0001D2BB File Offset: 0x0001B4BB
		public Metafile(string fileName, IntPtr referenceHdc, RectangleF frameRect)
			: this(fileName, referenceHdc, frameRect, MetafileFrameUnit.GdiCompatible, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle that uses the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="referenceHdc">The handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D0B RID: 3339 RVA: 0x0001D2C9 File Offset: 0x0001B4C9
		public Metafile(IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit, EmfType type)
			: this(referenceHdc, frameRect, frameUnit, type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle that uses the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="referenceHdc">The handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D0C RID: 3340 RVA: 0x0001D2D7 File Offset: 0x0001B4D7
		public Metafile(IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit, EmfType type)
			: this(referenceHdc, frameRect, frameUnit, type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. Also, a string that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" /> can be added.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D0D RID: 3341 RVA: 0x0001D2E8 File Offset: 0x0001B4E8
		public Metafile(Stream stream, IntPtr referenceHdc, EmfType type, string description)
			: this(stream, referenceHdc, default(RectangleF), MetafileFrameUnit.GdiCompatible, type, description)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, and the supplied unit of measure.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		// Token: 0x06000D0E RID: 3342 RVA: 0x0001D30A File Offset: 0x0001B50A
		public Metafile(Stream stream, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit)
			: this(stream, referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, and the supplied unit of measure.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		// Token: 0x06000D0F RID: 3343 RVA: 0x0001D319 File Offset: 0x0001B519
		public Metafile(Stream stream, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit)
			: this(stream, referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A descriptive string can be added, as well.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D10 RID: 3344 RVA: 0x0001D328 File Offset: 0x0001B528
		public Metafile(string fileName, IntPtr referenceHdc, EmfType type, string description)
			: this(fileName, referenceHdc, default(RectangleF), MetafileFrameUnit.GdiCompatible, type, description)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, and the supplied unit of measure.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		// Token: 0x06000D11 RID: 3345 RVA: 0x0001D34A File Offset: 0x0001B54A
		public Metafile(string fileName, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit)
			: this(fileName, referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, and the supplied unit of measure.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		// Token: 0x06000D12 RID: 3346 RVA: 0x0001D359 File Offset: 0x0001B559
		public Metafile(string fileName, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit)
			: this(fileName, referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle that uses the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A string can be provided to name the file.</summary>
		/// <param name="referenceHdc">The handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="desc">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D13 RID: 3347 RVA: 0x0001D368 File Offset: 0x0001B568
		public Metafile(IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit, EmfType type, string desc)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRecordMetafileI(referenceHdc, type, ref frameRect, frameUnit, desc, out this.nativeObject));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified device context, bounded by the specified rectangle that uses the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A string can be provided to name the file.</summary>
		/// <param name="referenceHdc">The handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D14 RID: 3348 RVA: 0x0001D388 File Offset: 0x0001B588
		public Metafile(IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit, EmfType type, string description)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRecordMetafile(referenceHdc, type, ref frameRect, frameUnit, description, out this.nativeObject));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D15 RID: 3349 RVA: 0x0001D3A8 File Offset: 0x0001B5A8
		public Metafile(Stream stream, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit, EmfType type)
			: this(stream, referenceHdc, frameRect, frameUnit, type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D16 RID: 3350 RVA: 0x0001D3B8 File Offset: 0x0001B5B8
		public Metafile(Stream stream, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit, EmfType type)
			: this(stream, referenceHdc, frameRect, frameUnit, type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D17 RID: 3351 RVA: 0x0001D3C8 File Offset: 0x0001B5C8
		public Metafile(string fileName, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit, EmfType type)
			: this(fileName, referenceHdc, frameRect, frameUnit, type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, and the supplied unit of measure. A descriptive string can also be added.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		/// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D18 RID: 3352 RVA: 0x0001D3D8 File Offset: 0x0001B5D8
		public Metafile(string fileName, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit, string description)
			: this(fileName, referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual, description)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D19 RID: 3353 RVA: 0x0001D3E8 File Offset: 0x0001B5E8
		public Metafile(string fileName, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit, EmfType type)
			: this(fileName, referenceHdc, frameRect, frameUnit, type, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, and the supplied unit of measure. A descriptive string can also be added.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		/// <param name="desc">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D1A RID: 3354 RVA: 0x0001D3F8 File Offset: 0x0001B5F8
		public Metafile(string fileName, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit, string desc)
			: this(fileName, referenceHdc, frameRect, frameUnit, EmfType.EmfPlusDual, desc)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A string that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" /> can be added.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D1B RID: 3355 RVA: 0x0001D408 File Offset: 0x0001B608
		public Metafile(Stream stream, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit, EmfType type, string description)
		{
			if (stream == null)
			{
				throw new NullReferenceException("stream");
			}
			Status status;
			if (GDIPlus.RunningOnUnix())
			{
				GDIPlus.GdiPlusStreamHelper gdiPlusStreamHelper = new GDIPlus.GdiPlusStreamHelper(stream, false);
				status = GDIPlus.GdipRecordMetafileFromDelegateI_linux(gdiPlusStreamHelper.GetHeaderDelegate, gdiPlusStreamHelper.GetBytesDelegate, gdiPlusStreamHelper.PutBytesDelegate, gdiPlusStreamHelper.SeekDelegate, gdiPlusStreamHelper.CloseDelegate, gdiPlusStreamHelper.SizeDelegate, referenceHdc, type, ref frameRect, frameUnit, description, out this.nativeObject);
			}
			else
			{
				status = GDIPlus.GdipRecordMetafileStreamI(new ComIStreamWrapper(stream), referenceHdc, type, ref frameRect, frameUnit, description, out this.nativeObject);
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class from the specified data stream, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A string that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" /> can be added.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D1C RID: 3356 RVA: 0x0001D498 File Offset: 0x0001B698
		public Metafile(Stream stream, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit, EmfType type, string description)
		{
			if (stream == null)
			{
				throw new NullReferenceException("stream");
			}
			Status status;
			if (GDIPlus.RunningOnUnix())
			{
				GDIPlus.GdiPlusStreamHelper gdiPlusStreamHelper = new GDIPlus.GdiPlusStreamHelper(stream, false);
				status = GDIPlus.GdipRecordMetafileFromDelegate_linux(gdiPlusStreamHelper.GetHeaderDelegate, gdiPlusStreamHelper.GetBytesDelegate, gdiPlusStreamHelper.PutBytesDelegate, gdiPlusStreamHelper.SeekDelegate, gdiPlusStreamHelper.CloseDelegate, gdiPlusStreamHelper.SizeDelegate, referenceHdc, type, ref frameRect, frameUnit, description, out this.nativeObject);
			}
			else
			{
				status = GDIPlus.GdipRecordMetafileStream(new ComIStreamWrapper(stream), referenceHdc, type, ref frameRect, frameUnit, description, out this.nativeObject);
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.Rectangle" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A descriptive string can also be added.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.Rectangle" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D1D RID: 3357 RVA: 0x0001D526 File Offset: 0x0001B726
		public Metafile(string fileName, IntPtr referenceHdc, Rectangle frameRect, MetafileFrameUnit frameUnit, EmfType type, string description)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRecordMetafileFileNameI(fileName, referenceHdc, type, ref frameRect, frameUnit, description, out this.nativeObject));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.Metafile" /> class with the specified file name, a Windows handle to a device context, a <see cref="T:System.Drawing.RectangleF" /> structure that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />, the supplied unit of measure, and an <see cref="T:System.Drawing.Imaging.EmfType" /> enumeration that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. A descriptive string can also be added.</summary>
		/// <param name="fileName">A <see cref="T:System.String" /> that represents the file name of the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="referenceHdc">A Windows handle to a device context. </param>
		/// <param name="frameRect">A <see cref="T:System.Drawing.RectangleF" /> that represents the rectangle that bounds the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="frameUnit">A <see cref="T:System.Drawing.Imaging.MetafileFrameUnit" /> that specifies the unit of measure for <paramref name="frameRect" />. </param>
		/// <param name="type">An <see cref="T:System.Drawing.Imaging.EmfType" /> that specifies the format of the <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		/// <param name="description">A <see cref="T:System.String" /> that contains a descriptive name for the new <see cref="T:System.Drawing.Imaging.Metafile" />. </param>
		// Token: 0x06000D1E RID: 3358 RVA: 0x0001D548 File Offset: 0x0001B748
		public Metafile(string fileName, IntPtr referenceHdc, RectangleF frameRect, MetafileFrameUnit frameUnit, EmfType type, string description)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRecordMetafileFileName(fileName, referenceHdc, type, ref frameRect, frameUnit, description, out this.nativeObject));
		}

		/// <summary>Returns a Windows handle to an enhanced <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <returns>A Windows handle to this enhanced <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000D1F RID: 3359 RVA: 0x0000EA38 File Offset: 0x0000CC38
		public IntPtr GetHenhmetafile()
		{
			return this.nativeObject;
		}

		/// <summary>Returns the <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with this <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with this <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000D20 RID: 3360 RVA: 0x0001D56C File Offset: 0x0001B76C
		[MonoLimitation("Metafiles aren't only partially supported by libgdiplus.")]
		public MetafileHeader GetMetafileHeader()
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MetafileHeader)));
			MetafileHeader metafileHeader;
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetMetafileHeaderFromMetafile(this.nativeObject, intPtr));
				metafileHeader = new MetafileHeader(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return metafileHeader;
		}

		/// <summary>Returns the <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		/// <param name="henhmetafile">The handle to the enhanced <see cref="T:System.Drawing.Imaging.Metafile" /> for which a header is returned. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000D21 RID: 3361 RVA: 0x0001D5C0 File Offset: 0x0001B7C0
		[MonoLimitation("Metafiles aren't only partially supported by libgdiplus.")]
		public static MetafileHeader GetMetafileHeader(IntPtr henhmetafile)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MetafileHeader)));
			MetafileHeader metafileHeader;
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetMetafileHeaderFromEmf(henhmetafile, intPtr));
				metafileHeader = new MetafileHeader(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return metafileHeader;
		}

		/// <summary>Returns the <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> containing the <see cref="T:System.Drawing.Imaging.Metafile" /> for which a header is retrieved. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000D22 RID: 3362 RVA: 0x0001D610 File Offset: 0x0001B810
		[MonoLimitation("Metafiles aren't only partially supported by libgdiplus.")]
		public static MetafileHeader GetMetafileHeader(Stream stream)
		{
			if (stream == null)
			{
				throw new NullReferenceException("stream");
			}
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MetafileHeader)));
			MetafileHeader metafileHeader;
			try
			{
				Status status;
				if (GDIPlus.RunningOnUnix())
				{
					GDIPlus.GdiPlusStreamHelper gdiPlusStreamHelper = new GDIPlus.GdiPlusStreamHelper(stream, false);
					status = GDIPlus.GdipGetMetafileHeaderFromDelegate_linux(gdiPlusStreamHelper.GetHeaderDelegate, gdiPlusStreamHelper.GetBytesDelegate, gdiPlusStreamHelper.PutBytesDelegate, gdiPlusStreamHelper.SeekDelegate, gdiPlusStreamHelper.CloseDelegate, gdiPlusStreamHelper.SizeDelegate, intPtr);
				}
				else
				{
					status = GDIPlus.GdipGetMetafileHeaderFromStream(new ComIStreamWrapper(stream), intPtr);
				}
				GDIPlus.CheckStatus(status);
				metafileHeader = new MetafileHeader(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return metafileHeader;
		}

		/// <summary>Returns the <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		/// <param name="fileName">A <see cref="T:System.String" /> containing the name of the <see cref="T:System.Drawing.Imaging.Metafile" /> for which a header is retrieved. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000D23 RID: 3363 RVA: 0x0001D6B0 File Offset: 0x0001B8B0
		[MonoLimitation("Metafiles aren't only partially supported by libgdiplus.")]
		public static MetafileHeader GetMetafileHeader(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MetafileHeader)));
			MetafileHeader metafileHeader;
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetMetafileHeaderFromFile(fileName, intPtr));
				metafileHeader = new MetafileHeader(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return metafileHeader;
		}

		/// <summary>Returns the <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Imaging.MetafileHeader" /> associated with the specified <see cref="T:System.Drawing.Imaging.Metafile" />.</returns>
		/// <param name="hmetafile">The handle to the <see cref="T:System.Drawing.Imaging.Metafile" /> for which to return a header. </param>
		/// <param name="wmfHeader">A <see cref="T:System.Drawing.Imaging.WmfPlaceableFileHeader" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000D24 RID: 3364 RVA: 0x0001D710 File Offset: 0x0001B910
		[MonoLimitation("Metafiles aren't only partially supported by libgdiplus.")]
		public static MetafileHeader GetMetafileHeader(IntPtr hmetafile, WmfPlaceableFileHeader wmfHeader)
		{
			IntPtr intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(MetafileHeader)));
			MetafileHeader metafileHeader;
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetMetafileHeaderFromEmf(hmetafile, intPtr));
				metafileHeader = new MetafileHeader(intPtr);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return metafileHeader;
		}

		/// <summary>Plays an individual metafile record.</summary>
		/// <param name="recordType">Element of the <see cref="T:System.Drawing.Imaging.EmfPlusRecordType" /> that specifies the type of metafile record being played. </param>
		/// <param name="flags">A set of flags that specify attributes of the record. </param>
		/// <param name="dataSize">The number of bytes in the record data. </param>
		/// <param name="data">An array of bytes that contains the record data. </param>
		// Token: 0x06000D25 RID: 3365 RVA: 0x0001D760 File Offset: 0x0001B960
		[MonoLimitation("Metafiles aren't only partially supported by libgdiplus.")]
		public void PlayRecord(EmfPlusRecordType recordType, int flags, int dataSize, byte[] data)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipPlayMetafileRecord(this.nativeObject, recordType, flags, dataSize, data));
		}
	}
}
