using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Drawing
{
	/// <summary>Encapsulates a GDI+ bitmap, which consists of the pixel data for a graphics image and its attributes. A <see cref="T:System.Drawing.Bitmap" /> is an object used to work with images defined by pixel data.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200003D RID: 61
	[ComVisible(true)]
	[Editor("System.Drawing.Design.BitmapEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Serializable]
	public sealed class Bitmap : Image
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00004348 File Offset: 0x00002548
		private Bitmap()
		{
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004350 File Offset: 0x00002550
		internal Bitmap(IntPtr ptr)
		{
			this.nativeObject = ptr;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000435F File Offset: 0x0000255F
		internal Bitmap(IntPtr ptr, Stream stream)
		{
			if (GDIPlus.RunningOnWindows())
			{
				this.stream = stream;
			}
			this.nativeObject = ptr;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class with the specified size.</summary>
		/// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x06000135 RID: 309 RVA: 0x0000437C File Offset: 0x0000257C
		public Bitmap(int width, int height)
			: this(width, height, PixelFormat.Format32bppArgb)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class with the specified size and with the resolution of the specified <see cref="T:System.Drawing.Graphics" /> object.</summary>
		/// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> object that specifies the resolution for the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="g" /> is null.</exception>
		// Token: 0x06000136 RID: 310 RVA: 0x0000438C File Offset: 0x0000258C
		public Bitmap(int width, int height, Graphics g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromGraphics(width, height, g.nativeObject, out intPtr));
			this.nativeObject = intPtr;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class with the specified size and format.</summary>
		/// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="format">The pixel format for the new <see cref="T:System.Drawing.Bitmap" />. This must specify a value that begins with <paramref name="Format" />.</param>
		/// <exception cref="T:System.ArgumentException">A <see cref="T:System.Drawing.Imaging.PixelFormat" /> value is specified whose name does not start with Format. For example, specifying <see cref="F:System.Drawing.Imaging.PixelFormat.Gdi" /> will cause an <see cref="T:System.ArgumentException" />, but <see cref="F:System.Drawing.Imaging.PixelFormat.Format48bppRgb" /> will not.</exception>
		// Token: 0x06000137 RID: 311 RVA: 0x000043C8 File Offset: 0x000025C8
		public Bitmap(int width, int height, PixelFormat format)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromScan0(width, height, 0, format, IntPtr.Zero, out intPtr));
			this.nativeObject = intPtr;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified existing image.</summary>
		/// <param name="original">The <see cref="T:System.Drawing.Image" /> from which to create the new <see cref="T:System.Drawing.Bitmap" />. </param>
		// Token: 0x06000138 RID: 312 RVA: 0x000043F7 File Offset: 0x000025F7
		public Bitmap(Image original)
			: this(original, original.Width, original.Height)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified data stream.</summary>
		/// <param name="stream">The data stream used to load the image. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not contain image data or is null.-or-<paramref name="stream" /> contains a PNG image file with a single dimension greater than 65,535 pixels.</exception>
		// Token: 0x06000139 RID: 313 RVA: 0x0000440C File Offset: 0x0000260C
		public Bitmap(Stream stream)
			: this(stream, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified file.</summary>
		/// <param name="filename">The bitmap file name and path. </param>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file is not found.</exception>
		// Token: 0x0600013A RID: 314 RVA: 0x00004416 File Offset: 0x00002616
		public Bitmap(string filename)
			: this(filename, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified existing image, scaled to the specified size.</summary>
		/// <param name="original">The <see cref="T:System.Drawing.Image" /> from which to create the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="newSize">The <see cref="T:System.Drawing.Size" /> structure that represent the size of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x0600013B RID: 315 RVA: 0x00004420 File Offset: 0x00002620
		public Bitmap(Image original, Size newSize)
			: this(original, newSize.Width, newSize.Height)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified data stream.</summary>
		/// <param name="stream">The data stream used to load the image. </param>
		/// <param name="useIcm">true to use color correction for this <see cref="T:System.Drawing.Bitmap" />; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="stream" /> does not contain image data or is null.-or-<paramref name="stream" /> contains a PNG image file with a single dimension greater than 65,535 pixels.</exception>
		// Token: 0x0600013C RID: 316 RVA: 0x00004437 File Offset: 0x00002637
		public Bitmap(Stream stream, bool useIcm)
		{
			this.nativeObject = Image.InitFromStream(stream);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified file.</summary>
		/// <param name="filename">The name of the bitmap file. </param>
		/// <param name="useIcm">true to use color correction for this <see cref="T:System.Drawing.Bitmap" />; otherwise, false. </param>
		// Token: 0x0600013D RID: 317 RVA: 0x0000444C File Offset: 0x0000264C
		public Bitmap(string filename, bool useIcm)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			IntPtr intPtr;
			Status status;
			if (useIcm)
			{
				status = GDIPlus.GdipCreateBitmapFromFileICM(filename, out intPtr);
			}
			else
			{
				status = GDIPlus.GdipCreateBitmapFromFile(filename, out intPtr);
			}
			GDIPlus.CheckStatus(status);
			this.nativeObject = intPtr;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from a specified resource.</summary>
		/// <param name="type">The class used to extract the resource. </param>
		/// <param name="resource">The name of the resource. </param>
		// Token: 0x0600013E RID: 318 RVA: 0x00004494 File Offset: 0x00002694
		public Bitmap(Type type, string resource)
		{
			if (resource == null)
			{
				throw new ArgumentException("resource");
			}
			if (type == null)
			{
				throw new NullReferenceException();
			}
			Stream manifestResourceStream = type.GetTypeInfo().Assembly.GetManifestResourceStream(type, resource);
			if (manifestResourceStream == null)
			{
				throw new FileNotFoundException(Locale.GetText("Resource '{0}' was not found.", new object[] { resource }));
			}
			this.nativeObject = Image.InitFromStream(manifestResourceStream);
			if (GDIPlus.RunningOnWindows())
			{
				this.stream = manifestResourceStream;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class from the specified existing image, scaled to the specified size.</summary>
		/// <param name="original">The <see cref="T:System.Drawing.Image" /> from which to create the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		// Token: 0x0600013F RID: 319 RVA: 0x0000450E File Offset: 0x0000270E
		public Bitmap(Image original, int width, int height)
			: this(width, height, PixelFormat.Format32bppArgb)
		{
			Graphics graphics = Graphics.FromImage(this);
			graphics.DrawImage(original, 0, 0, width, height);
			graphics.Dispose();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Bitmap" /> class with the specified size, pixel format, and pixel data.</summary>
		/// <param name="width">The width, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="height">The height, in pixels, of the new <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="stride">Integer that specifies the byte offset between the beginning of one scan line and the next. This is usually (but not necessarily) the number of bytes in the pixel format (for example, 2 for 16 bits per pixel) multiplied by the width of the bitmap. The value passed to this parameter must be a multiple of four.. </param>
		/// <param name="format">The pixel format for the new <see cref="T:System.Drawing.Bitmap" />. This must specify a value that begins with <paramref name="Format" />.</param>
		/// <param name="scan0">Pointer to an array of bytes that contains the pixel data.</param>
		/// <exception cref="T:System.ArgumentException">A <see cref="T:System.Drawing.Imaging.PixelFormat" /> value is specified whose name does not start with Format. For example, specifying <see cref="F:System.Drawing.Imaging.PixelFormat.Gdi" /> will cause an <see cref="T:System.ArgumentException" />, but <see cref="F:System.Drawing.Imaging.PixelFormat.Format48bppRgb" /> will not.</exception>
		// Token: 0x06000140 RID: 320 RVA: 0x00004534 File Offset: 0x00002734
		public Bitmap(int width, int height, int stride, PixelFormat format, IntPtr scan0)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromScan0(width, height, stride, format, scan0, out intPtr));
			this.nativeObject = intPtr;
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004561 File Offset: 0x00002761
		private Bitmap(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>Gets the color of the specified pixel in this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> structure that represents the color of the specified pixel.</returns>
		/// <param name="x">The x-coordinate of the pixel to retrieve. </param>
		/// <param name="y">The y-coordinate of the pixel to retrieve. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="x" /> is less than 0, or greater than or equal to <see cref="P:System.Drawing.Image.Width" />. -or-<paramref name="y" /> is less than 0, or greater than or equal to <see cref="P:System.Drawing.Image.Height" />.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000142 RID: 322 RVA: 0x0000456C File Offset: 0x0000276C
		public Color GetPixel(int x, int y)
		{
			int num;
			GDIPlus.CheckStatus(GDIPlus.GdipBitmapGetPixel(this.nativeObject, x, y, out num));
			return Color.FromArgb(num);
		}

		/// <summary>Sets the color of the specified pixel in this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <param name="x">The x-coordinate of the pixel to set. </param>
		/// <param name="y">The y-coordinate of the pixel to set. </param>
		/// <param name="color">A <see cref="T:System.Drawing.Color" /> structure that represents the color to assign to the specified pixel. </param>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000143 RID: 323 RVA: 0x00004593 File Offset: 0x00002793
		public void SetPixel(int x, int y, Color color)
		{
			Status status = GDIPlus.GdipBitmapSetPixel(this.nativeObject, x, y, color.ToArgb());
			if (status == Status.InvalidParameter && (base.PixelFormat & PixelFormat.Indexed) != PixelFormat.Undefined)
			{
				throw new InvalidOperationException(Locale.GetText("SetPixel cannot be called on indexed bitmaps."));
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Creates a copy of the section of this <see cref="T:System.Drawing.Bitmap" /> defined by <see cref="T:System.Drawing.Rectangle" /> structure and with a specified <see cref="T:System.Drawing.Imaging.PixelFormat" /> enumeration.</summary>
		/// <returns>The new <see cref="T:System.Drawing.Bitmap" /> that this method creates.</returns>
		/// <param name="rect">Defines the portion of this <see cref="T:System.Drawing.Bitmap" /> to copy. Coordinates are relative to this <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="format">The pixel format for the new <see cref="T:System.Drawing.Bitmap" />. This must specify a value that begins with <paramref name="Format" />.</param>
		/// <exception cref="T:System.OutOfMemoryException">
		///   <paramref name="rect" /> is outside of the source bitmap bounds.</exception>
		/// <exception cref="T:System.ArgumentException">The height or width of <paramref name="rect" /> is 0. -or-A <see cref="T:System.Drawing.Imaging.PixelFormat" /> value is specified whose name does not start with Format. For example, specifying <see cref="F:System.Drawing.Imaging.PixelFormat.Gdi" /> will cause an <see cref="T:System.ArgumentException" />, but <see cref="F:System.Drawing.Imaging.PixelFormat.Format48bppRgb" /> will not.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000144 RID: 324 RVA: 0x000045D0 File Offset: 0x000027D0
		public Bitmap Clone(Rectangle rect, PixelFormat format)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneBitmapAreaI(rect.X, rect.Y, rect.Width, rect.Height, format, this.nativeObject, out intPtr));
			return new Bitmap(intPtr);
		}

		/// <summary>Creates a copy of the section of this <see cref="T:System.Drawing.Bitmap" /> defined with a specified <see cref="T:System.Drawing.Imaging.PixelFormat" /> enumeration.</summary>
		/// <returns>The <see cref="T:System.Drawing.Bitmap" /> that this method creates.</returns>
		/// <param name="rect">Defines the portion of this <see cref="T:System.Drawing.Bitmap" /> to copy. </param>
		/// <param name="format">Specifies the <see cref="T:System.Drawing.Imaging.PixelFormat" /> enumeration for the destination <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <exception cref="T:System.OutOfMemoryException">
		///   <paramref name="rect" /> is outside of the source bitmap bounds.</exception>
		/// <exception cref="T:System.ArgumentException">The height or width of <paramref name="rect" /> is 0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000145 RID: 325 RVA: 0x00004614 File Offset: 0x00002814
		public Bitmap Clone(RectangleF rect, PixelFormat format)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneBitmapArea(rect.X, rect.Y, rect.Width, rect.Height, format, this.nativeObject, out intPtr));
			return new Bitmap(intPtr);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Bitmap" /> from a Windows handle to an icon.</summary>
		/// <returns>The <see cref="T:System.Drawing.Bitmap" /> that this method creates.</returns>
		/// <param name="hicon">A handle to an icon. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000146 RID: 326 RVA: 0x00004658 File Offset: 0x00002858
		public static Bitmap FromHicon(IntPtr hicon)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromHICON(hicon, out intPtr));
			return new Bitmap(intPtr);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Bitmap" /> from the specified Windows resource.</summary>
		/// <returns>The <see cref="T:System.Drawing.Bitmap" /> that this method creates.</returns>
		/// <param name="hinstance">A handle to an instance of the executable file that contains the resource. </param>
		/// <param name="bitmapName">A string that contains the name of the resource bitmap. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000147 RID: 327 RVA: 0x00004678 File Offset: 0x00002878
		public static Bitmap FromResource(IntPtr hinstance, string bitmapName)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromResource(hinstance, bitmapName, out intPtr));
			return new Bitmap(intPtr);
		}

		/// <summary>Creates a GDI bitmap object from this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <returns>A handle to the GDI bitmap object that this method creates.</returns>
		/// <exception cref="T:System.ArgumentException">The height or width of the bitmap is greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000148 RID: 328 RVA: 0x00004699 File Offset: 0x00002899
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IntPtr GetHbitmap()
		{
			return this.GetHbitmap(Color.Gray);
		}

		/// <summary>Creates a GDI bitmap object from this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <returns>A handle to the GDI bitmap object that this method creates.</returns>
		/// <param name="background">A <see cref="T:System.Drawing.Color" /> structure that specifies the background color. This parameter is ignored if the bitmap is totally opaque. </param>
		/// <exception cref="T:System.ArgumentException">The height or width of the bitmap is greater than <see cref="F:System.Int16.MaxValue" />.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000149 RID: 329 RVA: 0x000046A8 File Offset: 0x000028A8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IntPtr GetHbitmap(Color background)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateHBITMAPFromBitmap(this.nativeObject, out intPtr, background.ToArgb()));
			return intPtr;
		}

		/// <summary>Returns the handle to an icon.</summary>
		/// <returns>A Windows handle to an icon with the same image as the <see cref="T:System.Drawing.Bitmap" />.</returns>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600014A RID: 330 RVA: 0x000046D0 File Offset: 0x000028D0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public IntPtr GetHicon()
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateHICONFromBitmap(this.nativeObject, out intPtr));
			return intPtr;
		}

		/// <summary>Locks a <see cref="T:System.Drawing.Bitmap" /> into system memory.</summary>
		/// <returns>A <see cref="T:System.Drawing.Imaging.BitmapData" /> that contains information about this lock operation.</returns>
		/// <param name="rect">A <see cref="T:System.Drawing.Rectangle" /> structure that specifies the portion of the <see cref="T:System.Drawing.Bitmap" /> to lock. </param>
		/// <param name="flags">An <see cref="T:System.Drawing.Imaging.ImageLockMode" /> enumeration that specifies the access level (read/write) for the <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="format">A <see cref="T:System.Drawing.Imaging.PixelFormat" /> enumeration that specifies the data format of this <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Drawing.Imaging.PixelFormat" /> is not a specific bits-per-pixel value.-or-The incorrect <see cref="T:System.Drawing.Imaging.PixelFormat" /> is passed in for a bitmap.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600014B RID: 331 RVA: 0x000046F0 File Offset: 0x000028F0
		public BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format)
		{
			BitmapData bitmapData = new BitmapData();
			return this.LockBits(rect, flags, format, bitmapData);
		}

		/// <summary>Locks a <see cref="T:System.Drawing.Bitmap" /> into system memory </summary>
		/// <returns>A <see cref="T:System.Drawing.Imaging.BitmapData" /> that contains information about the lock operation.</returns>
		/// <param name="rect">A rectangle structure that specifies the portion of the <see cref="T:System.Drawing.Bitmap" /> to lock.</param>
		/// <param name="flags">One of the <see cref="T:System.Drawing.Imaging.ImageLockMode" /> values that specifies the access level (read/write) for the <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="format">One of the <see cref="T:System.Drawing.Imaging.PixelFormat" /> values that specifies the data format of the <see cref="T:System.Drawing.Bitmap" />.</param>
		/// <param name="bitmapData">A <see cref="T:System.Drawing.Imaging.BitmapData" /> that contains information about the lock operation.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="T:System.Drawing.Imaging.PixelFormat" /> value is not a specific bits-per-pixel value.-or-The incorrect <see cref="T:System.Drawing.Imaging.PixelFormat" /> is passed in for a bitmap.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600014C RID: 332 RVA: 0x0000470D File Offset: 0x0000290D
		public BitmapData LockBits(Rectangle rect, ImageLockMode flags, PixelFormat format, BitmapData bitmapData)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipBitmapLockBits(this.nativeObject, ref rect, flags, format, bitmapData));
			return bitmapData;
		}

		/// <summary>Makes the default transparent color transparent for this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The image format of the <see cref="T:System.Drawing.Bitmap" /> is an icon format.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600014D RID: 333 RVA: 0x00004728 File Offset: 0x00002928
		public void MakeTransparent()
		{
			Color pixel = this.GetPixel(0, 0);
			this.MakeTransparent(pixel);
		}

		/// <summary>Makes the specified color transparent for this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <param name="transparentColor">The <see cref="T:System.Drawing.Color" /> structure that represents the color to make transparent. </param>
		/// <exception cref="T:System.InvalidOperationException">The image format of the <see cref="T:System.Drawing.Bitmap" /> is an icon format.</exception>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600014E RID: 334 RVA: 0x00004748 File Offset: 0x00002948
		public void MakeTransparent(Color transparentColor)
		{
			Bitmap bitmap = new Bitmap(base.Width, base.Height, PixelFormat.Format32bppArgb);
			Graphics graphics = Graphics.FromImage(bitmap);
			Rectangle rectangle = new Rectangle(0, 0, base.Width, base.Height);
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.SetColorKey(transparentColor, transparentColor);
			graphics.DrawImage(this, rectangle, 0, 0, base.Width, base.Height, GraphicsUnit.Pixel, imageAttributes);
			IntPtr nativeObject = this.nativeObject;
			this.nativeObject = bitmap.nativeObject;
			bitmap.nativeObject = nativeObject;
			graphics.Dispose();
			bitmap.Dispose();
			imageAttributes.Dispose();
		}

		/// <summary>Sets the resolution for this <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <param name="xDpi">The horizontal resolution, in dots per inch, of the <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="yDpi">The vertical resolution, in dots per inch, of the <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600014F RID: 335 RVA: 0x000047D8 File Offset: 0x000029D8
		public void SetResolution(float xDpi, float yDpi)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipBitmapSetResolution(this.nativeObject, xDpi, yDpi));
		}

		/// <summary>Unlocks this <see cref="T:System.Drawing.Bitmap" /> from system memory.</summary>
		/// <param name="bitmapdata">A <see cref="T:System.Drawing.Imaging.BitmapData" /> that specifies information about the lock operation. </param>
		/// <exception cref="T:System.Exception">The operation failed.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000150 RID: 336 RVA: 0x000047EC File Offset: 0x000029EC
		public void UnlockBits(BitmapData bitmapdata)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipBitmapUnlockBits(this.nativeObject, bitmapdata));
		}
	}
}
