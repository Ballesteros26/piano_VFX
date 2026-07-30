using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Drawing
{
	/// <summary>An abstract base class that provides functionality for the <see cref="T:System.Drawing.Bitmap" /> and <see cref="T:System.Drawing.Imaging.Metafile" /> descended classes.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000071 RID: 113
	[ImmutableObject(true)]
	[TypeConverter(typeof(ImageConverter))]
	[ComVisible(true)]
	[Editor("System.Drawing.Design.ImageEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Serializable]
	public abstract class Image : MarshalByRefObject, IDisposable, ICloneable, ISerializable
	{
		// Token: 0x060004D5 RID: 1237 RVA: 0x0000DD70 File Offset: 0x0000BF70
		internal Image()
		{
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000DD84 File Offset: 0x0000BF84
		internal Image(SerializationInfo info, StreamingContext context)
		{
			foreach (SerializationEntry serializationEntry in info)
			{
				if (string.Compare(serializationEntry.Name, "Data", true) == 0)
				{
					byte[] array = (byte[])serializationEntry.Value;
					if (array != null)
					{
						MemoryStream memoryStream = new MemoryStream(array);
						this.nativeObject = Image.InitFromStream(memoryStream);
						if (GDIPlus.RunningOnWindows())
						{
							this.stream = memoryStream;
						}
					}
				}
			}
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x060004D7 RID: 1239 RVA: 0x0000DE00 File Offset: 0x0000C000
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				if (this.RawFormat.Equals(ImageFormat.Icon))
				{
					this.Save(memoryStream, ImageFormat.Png);
				}
				else
				{
					this.Save(memoryStream, this.RawFormat);
				}
				si.AddValue("Data", memoryStream.ToArray());
			}
		}

		/// <summary>Creates an <see cref="T:System.Drawing.Image" /> from the specified file.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> this method creates.</returns>
		/// <param name="filename">A string that contains the name of the file from which to create the <see cref="T:System.Drawing.Image" />. </param>
		/// <exception cref="T:System.OutOfMemoryException">The file does not have a valid image format.-or-GDI+ does not support the pixel format of the file.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file does not exist.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="filename" /> is a <see cref="T:System.Uri" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004D8 RID: 1240 RVA: 0x0000DE70 File Offset: 0x0000C070
		public static Image FromFile(string filename)
		{
			return Image.FromFile(filename, false);
		}

		/// <summary>Creates an <see cref="T:System.Drawing.Image" /> from the specified file using embedded color management information in that file.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> this method creates.</returns>
		/// <param name="filename">A string that contains the name of the file from which to create the <see cref="T:System.Drawing.Image" />. </param>
		/// <param name="useEmbeddedColorManagement">Set to true to use color management information embedded in the image file; otherwise, false. </param>
		/// <exception cref="T:System.OutOfMemoryException">The file does not have a valid image format.-or-GDI+ does not support the pixel format of the file.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The specified file does not exist.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="filename" /> is a <see cref="T:System.Uri" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004D9 RID: 1241 RVA: 0x0000DE7C File Offset: 0x0000C07C
		public static Image FromFile(string filename, bool useEmbeddedColorManagement)
		{
			if (!File.Exists(filename))
			{
				throw new FileNotFoundException(filename);
			}
			IntPtr intPtr;
			Status status;
			if (useEmbeddedColorManagement)
			{
				status = GDIPlus.GdipLoadImageFromFileICM(filename, out intPtr);
			}
			else
			{
				status = GDIPlus.GdipLoadImageFromFile(filename, out intPtr);
			}
			GDIPlus.CheckStatus(status);
			return Image.CreateFromHandle(intPtr);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Bitmap" /> from a handle to a GDI bitmap.</summary>
		/// <returns>The <see cref="T:System.Drawing.Bitmap" /> this method creates.</returns>
		/// <param name="hbitmap">The GDI bitmap handle from which to create the <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004DA RID: 1242 RVA: 0x0000DEBB File Offset: 0x0000C0BB
		public static Bitmap FromHbitmap(IntPtr hbitmap)
		{
			return Image.FromHbitmap(hbitmap, IntPtr.Zero);
		}

		/// <summary>Creates a <see cref="T:System.Drawing.Bitmap" /> from a handle to a GDI bitmap and a handle to a GDI palette.</summary>
		/// <returns>The <see cref="T:System.Drawing.Bitmap" /> this method creates.</returns>
		/// <param name="hbitmap">The GDI bitmap handle from which to create the <see cref="T:System.Drawing.Bitmap" />. </param>
		/// <param name="hpalette">A handle to a GDI palette used to define the bitmap colors if the bitmap specified in the <paramref name="hBitmap" /> parameter is not a device-independent bitmap (DIB). </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004DB RID: 1243 RVA: 0x0000DEC8 File Offset: 0x0000C0C8
		public static Bitmap FromHbitmap(IntPtr hbitmap, IntPtr hpalette)
		{
			IntPtr intPtr;
			GDIPlus.CheckStatus(GDIPlus.GdipCreateBitmapFromHBITMAP(hbitmap, hpalette, out intPtr));
			return new Bitmap(intPtr);
		}

		/// <summary>Creates an <see cref="T:System.Drawing.Image" /> from the specified data stream.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> this method creates.</returns>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Image" />. </param>
		/// <exception cref="T:System.ArgumentException">The stream does not have a valid image format-or-<paramref name="stream" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004DC RID: 1244 RVA: 0x0000DEE9 File Offset: 0x0000C0E9
		public static Image FromStream(Stream stream)
		{
			return Image.LoadFromStream(stream, false);
		}

		/// <summary>Creates an <see cref="T:System.Drawing.Image" /> from the specified data stream, optionally using embedded color management information in that stream.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> this method creates.</returns>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Image" />. </param>
		/// <param name="useEmbeddedColorManagement">true to use color management information embedded in the data stream; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">The stream does not have a valid image format -or-<paramref name="stream" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004DD RID: 1245 RVA: 0x0000DEE9 File Offset: 0x0000C0E9
		[MonoLimitation("useEmbeddedColorManagement  isn't supported.")]
		public static Image FromStream(Stream stream, bool useEmbeddedColorManagement)
		{
			return Image.LoadFromStream(stream, false);
		}

		/// <summary>Creates an <see cref="T:System.Drawing.Image" /> from the specified data stream, optionally using embedded color management information and validating the image data.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> this method creates.</returns>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> that contains the data for this <see cref="T:System.Drawing.Image" />. </param>
		/// <param name="useEmbeddedColorManagement">true to use color management information embedded in the data stream; otherwise, false. </param>
		/// <param name="validateImageData">true to validate the image data; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentException">The stream does not have a valid image format.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004DE RID: 1246 RVA: 0x0000DEE9 File Offset: 0x0000C0E9
		[MonoLimitation("useEmbeddedColorManagement  and validateImageData aren't supported.")]
		public static Image FromStream(Stream stream, bool useEmbeddedColorManagement, bool validateImageData)
		{
			return Image.LoadFromStream(stream, false);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000DEF4 File Offset: 0x0000C0F4
		internal static Image LoadFromStream(Stream stream, bool keepAlive)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			Image image = Image.CreateFromHandle(Image.InitFromStream(stream));
			if (keepAlive && GDIPlus.RunningOnWindows())
			{
				image.stream = stream;
			}
			return image;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000DF2D File Offset: 0x0000C12D
		internal static Image CreateImageObject(IntPtr nativeImage)
		{
			return Image.CreateFromHandle(nativeImage);
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x0000DF38 File Offset: 0x0000C138
		internal static Image CreateFromHandle(IntPtr handle)
		{
			ImageType imageType;
			GDIPlus.CheckStatus(GDIPlus.GdipGetImageType(handle, out imageType));
			if (imageType == ImageType.Bitmap)
			{
				return new Bitmap(handle);
			}
			if (imageType != ImageType.Metafile)
			{
				throw new NotSupportedException(Locale.GetText("Unknown image type."));
			}
			return new Metafile(handle);
		}

		/// <summary>Returns the color depth, in number of bits per pixel, of the specified pixel format.</summary>
		/// <returns>The color depth of the specified pixel format.</returns>
		/// <param name="pixfmt">The <see cref="T:System.Drawing.Imaging.PixelFormat" /> member that specifies the format for which to find the size. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004E2 RID: 1250 RVA: 0x0000DF7C File Offset: 0x0000C17C
		public static int GetPixelFormatSize(PixelFormat pixfmt)
		{
			int num = 0;
			if (pixfmt <= PixelFormat.Format8bppIndexed)
			{
				if (pixfmt <= PixelFormat.Format32bppRgb)
				{
					if (pixfmt - PixelFormat.Format16bppRgb555 > 1)
					{
						if (pixfmt == PixelFormat.Format24bppRgb)
						{
							return 24;
						}
						if (pixfmt != PixelFormat.Format32bppRgb)
						{
							return num;
						}
						goto IL_00A7;
					}
				}
				else
				{
					if (pixfmt == PixelFormat.Format1bppIndexed)
					{
						return 1;
					}
					if (pixfmt == PixelFormat.Format4bppIndexed)
					{
						return 4;
					}
					if (pixfmt != PixelFormat.Format8bppIndexed)
					{
						return num;
					}
					return 8;
				}
			}
			else
			{
				if (pixfmt > PixelFormat.Format16bppGrayScale)
				{
					if (pixfmt <= PixelFormat.Format64bppPArgb)
					{
						if (pixfmt == PixelFormat.Format48bppRgb)
						{
							return 48;
						}
						if (pixfmt != PixelFormat.Format64bppPArgb)
						{
							return num;
						}
					}
					else
					{
						if (pixfmt == PixelFormat.Format32bppArgb)
						{
							goto IL_00A7;
						}
						if (pixfmt != PixelFormat.Format64bppArgb)
						{
							return num;
						}
					}
					return 64;
				}
				if (pixfmt != PixelFormat.Format16bppArgb1555)
				{
					if (pixfmt == PixelFormat.Format32bppPArgb)
					{
						goto IL_00A7;
					}
					if (pixfmt != PixelFormat.Format16bppGrayScale)
					{
						return num;
					}
				}
			}
			return 16;
			IL_00A7:
			num = 32;
			return num;
		}

		/// <summary>Returns a value that indicates whether the pixel format for this <see cref="T:System.Drawing.Image" /> contains alpha information.</summary>
		/// <returns>true if <paramref name="pixfmt" /> contains alpha information; otherwise, false.</returns>
		/// <param name="pixfmt">The <see cref="T:System.Drawing.Imaging.PixelFormat" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004E3 RID: 1251 RVA: 0x0000E048 File Offset: 0x0000C248
		public static bool IsAlphaPixelFormat(PixelFormat pixfmt)
		{
			bool flag = false;
			if (pixfmt > PixelFormat.Format8bppIndexed)
			{
				if (pixfmt <= PixelFormat.Format16bppGrayScale)
				{
					if (pixfmt != PixelFormat.Format16bppArgb1555 && pixfmt != PixelFormat.Format32bppPArgb)
					{
						if (pixfmt != PixelFormat.Format16bppGrayScale)
						{
							return flag;
						}
						goto IL_0098;
					}
				}
				else if (pixfmt <= PixelFormat.Format64bppPArgb)
				{
					if (pixfmt == PixelFormat.Format48bppRgb)
					{
						goto IL_0098;
					}
					if (pixfmt != PixelFormat.Format64bppPArgb)
					{
						return flag;
					}
				}
				else if (pixfmt != PixelFormat.Format32bppArgb && pixfmt != PixelFormat.Format64bppArgb)
				{
					return flag;
				}
				return true;
			}
			if (pixfmt <= PixelFormat.Format32bppRgb)
			{
				if (pixfmt - PixelFormat.Format16bppRgb555 > 1 && pixfmt != PixelFormat.Format24bppRgb && pixfmt != PixelFormat.Format32bppRgb)
				{
					return flag;
				}
			}
			else if (pixfmt != PixelFormat.Format1bppIndexed && pixfmt != PixelFormat.Format4bppIndexed && pixfmt != PixelFormat.Format8bppIndexed)
			{
				return flag;
			}
			IL_0098:
			flag = false;
			return flag;
		}

		/// <summary>Returns a value that indicates whether the pixel format is 32 bits per pixel.</summary>
		/// <returns>true if <paramref name="pixfmt" /> is canonical; otherwise, false.</returns>
		/// <param name="pixfmt">The <see cref="T:System.Drawing.Imaging.PixelFormat" /> to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004E4 RID: 1252 RVA: 0x0000E0F0 File Offset: 0x0000C2F0
		public static bool IsCanonicalPixelFormat(PixelFormat pixfmt)
		{
			return (pixfmt & PixelFormat.Canonical) > PixelFormat.Undefined;
		}

		/// <summary>Returns a value that indicates whether the pixel format is 64 bits per pixel.</summary>
		/// <returns>true if <paramref name="pixfmt" /> is extended; otherwise, false.</returns>
		/// <param name="pixfmt">The <see cref="T:System.Drawing.Imaging.PixelFormat" /> enumeration to test. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004E5 RID: 1253 RVA: 0x0000E0FC File Offset: 0x0000C2FC
		public static bool IsExtendedPixelFormat(PixelFormat pixfmt)
		{
			return (pixfmt & PixelFormat.Extended) > PixelFormat.Undefined;
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x0000E108 File Offset: 0x0000C308
		internal static IntPtr InitFromStream(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentException("stream");
			}
			if (!stream.CanSeek)
			{
				byte[] array = new byte[256];
				int num = 0;
				int num2;
				do
				{
					if (array.Length < num + 256)
					{
						byte[] array2 = new byte[array.Length * 2];
						Array.Copy(array, array2, array.Length);
						array = array2;
					}
					num2 = stream.Read(array, num, 256);
					num += num2;
				}
				while (num2 != 0);
				stream = new MemoryStream(array, 0, num);
			}
			IntPtr intPtr;
			Status status;
			if (GDIPlus.RunningOnUnix())
			{
				GDIPlus.GdiPlusStreamHelper gdiPlusStreamHelper = new GDIPlus.GdiPlusStreamHelper(stream, true);
				status = GDIPlus.GdipLoadImageFromDelegate_linux(gdiPlusStreamHelper.GetHeaderDelegate, gdiPlusStreamHelper.GetBytesDelegate, gdiPlusStreamHelper.PutBytesDelegate, gdiPlusStreamHelper.SeekDelegate, gdiPlusStreamHelper.CloseDelegate, gdiPlusStreamHelper.SizeDelegate, out intPtr);
			}
			else
			{
				status = GDIPlus.GdipLoadImageFromStream(new ComIStreamWrapper(stream), out intPtr);
			}
			if (status != Status.Ok)
			{
				return IntPtr.Zero;
			}
			return intPtr;
		}

		/// <summary>Gets the bounds of the image in the specified unit.</summary>
		/// <returns>The <see cref="T:System.Drawing.RectangleF" /> that represents the bounds of the image, in the specified unit.</returns>
		/// <param name="pageUnit">One of the <see cref="T:System.Drawing.GraphicsUnit" /> values indicating the unit of measure for the bounding rectangle.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004E7 RID: 1255 RVA: 0x0000E1DC File Offset: 0x0000C3DC
		public RectangleF GetBounds(ref GraphicsUnit pageUnit)
		{
			RectangleF rectangleF;
			GDIPlus.CheckStatus(GDIPlus.GdipGetImageBounds(this.nativeObject, out rectangleF, ref pageUnit));
			return rectangleF;
		}

		/// <summary>Returns information about the parameters supported by the specified image encoder.</summary>
		/// <returns>An <see cref="T:System.Drawing.Imaging.EncoderParameters" /> that contains an array of <see cref="T:System.Drawing.Imaging.EncoderParameter" /> objects. Each <see cref="T:System.Drawing.Imaging.EncoderParameter" /> contains information about one of the parameters supported by the specified image encoder.</returns>
		/// <param name="encoder">A GUID that specifies the image encoder. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004E8 RID: 1256 RVA: 0x0000E200 File Offset: 0x0000C400
		public EncoderParameters GetEncoderParameterList(Guid encoder)
		{
			uint num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetEncoderParameterListSize(this.nativeObject, ref encoder, out num));
			IntPtr intPtr = Marshal.AllocHGlobal((int)num);
			EncoderParameters encoderParameters;
			try
			{
				Status status = GDIPlus.GdipGetEncoderParameterList(this.nativeObject, ref encoder, num, intPtr);
				encoderParameters = EncoderParameters.ConvertFromMemory(intPtr);
				GDIPlus.CheckStatus(status);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return encoderParameters;
		}

		/// <summary>Returns the number of frames of the specified dimension.</summary>
		/// <returns>The number of frames in the specified dimension.</returns>
		/// <param name="dimension">A <see cref="T:System.Drawing.Imaging.FrameDimension" /> that specifies the identity of the dimension type. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060004E9 RID: 1257 RVA: 0x0000E260 File Offset: 0x0000C460
		public int GetFrameCount(FrameDimension dimension)
		{
			Guid guid = dimension.Guid;
			uint num;
			GDIPlus.CheckStatus(GDIPlus.GdipImageGetFrameCount(this.nativeObject, ref guid, out num));
			return (int)num;
		}

		/// <summary>Gets the specified property item from this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Imaging.PropertyItem" /> this method gets.</returns>
		/// <param name="propid">The ID of the property item to get. </param>
		/// <exception cref="T:System.ArgumentException">The image format of this image does not support property items.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004EA RID: 1258 RVA: 0x0000E28C File Offset: 0x0000C48C
		public PropertyItem GetPropertyItem(int propid)
		{
			PropertyItem propertyItem = new PropertyItem();
			int num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetPropertyItemSize(this.nativeObject, propid, out num));
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetPropertyItem(this.nativeObject, propid, num, intPtr));
				GdipPropertyItem.MarshalTo((GdipPropertyItem)Marshal.PtrToStructure(intPtr, typeof(GdipPropertyItem)), propertyItem);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return propertyItem;
		}

		/// <summary>Returns a thumbnail for this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> that represents the thumbnail.</returns>
		/// <param name="thumbWidth">The width, in pixels, of the requested thumbnail image. </param>
		/// <param name="thumbHeight">The height, in pixels, of the requested thumbnail image. </param>
		/// <param name="callback">A <see cref="T:System.Drawing.Image.GetThumbnailImageAbort" /> delegate. Note   You must create a delegate and pass a reference to the delegate as the <paramref name="callback" /> parameter, but the delegate is not used.</param>
		/// <param name="callbackData">Must be <see cref="F:System.IntPtr.Zero" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004EB RID: 1259 RVA: 0x0000E304 File Offset: 0x0000C504
		public Image GetThumbnailImage(int thumbWidth, int thumbHeight, Image.GetThumbnailImageAbort callback, IntPtr callbackData)
		{
			if (thumbWidth <= 0 || thumbHeight <= 0)
			{
				throw new OutOfMemoryException("Invalid thumbnail size");
			}
			Image image = new Bitmap(thumbWidth, thumbHeight);
			using (Graphics graphics = Graphics.FromImage(image))
			{
				GDIPlus.CheckStatus(GDIPlus.GdipDrawImageRectRectI(graphics.nativeObject, this.nativeObject, 0, 0, thumbWidth, thumbHeight, 0, 0, this.Width, this.Height, GraphicsUnit.Pixel, IntPtr.Zero, null, IntPtr.Zero));
			}
			return image;
		}

		/// <summary>Removes the specified property item from this <see cref="T:System.Drawing.Image" />.</summary>
		/// <param name="propid">The ID of the property item to remove. </param>
		/// <exception cref="T:System.ArgumentException">The image does not contain the requested property item.-or-The image format for this image does not support property items.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004EC RID: 1260 RVA: 0x0000E384 File Offset: 0x0000C584
		public void RemovePropertyItem(int propid)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipRemovePropertyItem(this.nativeObject, propid));
		}

		/// <summary>Rotates, flips, or rotates and flips the <see cref="T:System.Drawing.Image" />.</summary>
		/// <param name="rotateFlipType">A <see cref="T:System.Drawing.RotateFlipType" /> member that specifies the type of rotation and flip to apply to the image. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060004ED RID: 1261 RVA: 0x0000E397 File Offset: 0x0000C597
		public void RotateFlip(RotateFlipType rotateFlipType)
		{
			GDIPlus.CheckStatus(GDIPlus.GdipImageRotateFlip(this.nativeObject, rotateFlipType));
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0000E3AC File Offset: 0x0000C5AC
		internal ImageCodecInfo findEncoderForFormat(ImageFormat format)
		{
			ImageCodecInfo[] imageEncoders = ImageCodecInfo.GetImageEncoders();
			ImageCodecInfo imageCodecInfo = null;
			if (format.Guid.Equals(ImageFormat.MemoryBmp.Guid))
			{
				format = ImageFormat.Png;
			}
			for (int i = 0; i < imageEncoders.Length; i++)
			{
				if (imageEncoders[i].FormatID.Equals(format.Guid))
				{
					imageCodecInfo = imageEncoders[i];
					break;
				}
			}
			return imageCodecInfo;
		}

		/// <summary>Saves this <see cref="T:System.Drawing.Image" /> to the specified file or stream.</summary>
		/// <param name="filename">A string that contains the name of the file to which to save this <see cref="T:System.Drawing.Image" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filename" /> is null.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format.-or- The image was saved to the same file it was created from.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004EF RID: 1263 RVA: 0x0000E40F File Offset: 0x0000C60F
		public void Save(string filename)
		{
			this.Save(filename, this.RawFormat);
		}

		/// <summary>Saves this <see cref="T:System.Drawing.Image" /> to the specified file in the specified format.</summary>
		/// <param name="filename">A string that contains the name of the file to which to save this <see cref="T:System.Drawing.Image" />. </param>
		/// <param name="format">The <see cref="T:System.Drawing.Imaging.ImageFormat" /> for this <see cref="T:System.Drawing.Image" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filename" /> or <paramref name="format" /> is null.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format.-or- The image was saved to the same file it was created from.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004F0 RID: 1264 RVA: 0x0000E420 File Offset: 0x0000C620
		public void Save(string filename, ImageFormat format)
		{
			ImageCodecInfo imageCodecInfo = this.findEncoderForFormat(format);
			if (imageCodecInfo == null)
			{
				imageCodecInfo = this.findEncoderForFormat(this.RawFormat);
				if (imageCodecInfo == null)
				{
					throw new ArgumentException(Locale.GetText("No codec available for saving format '{0}'.", new object[] { format.Guid }), "format");
				}
			}
			this.Save(filename, imageCodecInfo, null);
		}

		/// <summary>Saves this <see cref="T:System.Drawing.Image" /> to the specified file, with the specified encoder and image-encoder parameters.</summary>
		/// <param name="filename">A string that contains the name of the file to which to save this <see cref="T:System.Drawing.Image" />. </param>
		/// <param name="encoder">The <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> for this <see cref="T:System.Drawing.Image" />. </param>
		/// <param name="encoderParams">An <see cref="T:System.Drawing.Imaging.EncoderParameters" /> to use for this <see cref="T:System.Drawing.Image" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filename" /> or <paramref name="encoder" /> is null.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format.-or- The image was saved to the same file it was created from.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004F1 RID: 1265 RVA: 0x0000E47C File Offset: 0x0000C67C
		public void Save(string filename, ImageCodecInfo encoder, EncoderParameters encoderParams)
		{
			Guid clsid = encoder.Clsid;
			Status status;
			if (encoderParams == null)
			{
				status = GDIPlus.GdipSaveImageToFile(this.nativeObject, filename, ref clsid, IntPtr.Zero);
			}
			else
			{
				IntPtr intPtr = encoderParams.ConvertToMemory();
				status = GDIPlus.GdipSaveImageToFile(this.nativeObject, filename, ref clsid, intPtr);
				Marshal.FreeHGlobal(intPtr);
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Saves this image to the specified stream in the specified format.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> where the image will be saved. </param>
		/// <param name="format">An <see cref="T:System.Drawing.Imaging.ImageFormat" /> that specifies the format of the saved image. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> or <paramref name="format" /> is null.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004F2 RID: 1266 RVA: 0x0000E4CC File Offset: 0x0000C6CC
		public void Save(Stream stream, ImageFormat format)
		{
			ImageCodecInfo imageCodecInfo = this.findEncoderForFormat(format);
			if (imageCodecInfo == null)
			{
				throw new ArgumentException("No codec available for format:" + format.Guid);
			}
			this.Save(stream, imageCodecInfo, null);
		}

		/// <summary>Saves this image to the specified stream, with the specified encoder and image encoder parameters.</summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> where the image will be saved. </param>
		/// <param name="encoder">The <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> for this <see cref="T:System.Drawing.Image" />.</param>
		/// <param name="encoderParams">An <see cref="T:System.Drawing.Imaging.EncoderParameters" /> that specifies parameters used by the image encoder. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.ExternalException">The image was saved with the wrong image format.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004F3 RID: 1267 RVA: 0x0000E508 File Offset: 0x0000C708
		public void Save(Stream stream, ImageCodecInfo encoder, EncoderParameters encoderParams)
		{
			Guid clsid = encoder.Clsid;
			IntPtr intPtr;
			if (encoderParams == null)
			{
				intPtr = IntPtr.Zero;
			}
			else
			{
				intPtr = encoderParams.ConvertToMemory();
			}
			Status status;
			try
			{
				if (GDIPlus.RunningOnUnix())
				{
					GDIPlus.GdiPlusStreamHelper gdiPlusStreamHelper = new GDIPlus.GdiPlusStreamHelper(stream, false);
					status = GDIPlus.GdipSaveImageToDelegate_linux(this.nativeObject, gdiPlusStreamHelper.GetBytesDelegate, gdiPlusStreamHelper.PutBytesDelegate, gdiPlusStreamHelper.SeekDelegate, gdiPlusStreamHelper.CloseDelegate, gdiPlusStreamHelper.SizeDelegate, ref clsid, intPtr);
				}
				else
				{
					status = GDIPlus.GdipSaveImageToStream(new HandleRef(this, this.nativeObject), new ComIStreamWrapper(stream), ref clsid, new HandleRef(encoderParams, intPtr));
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Adds a frame to the file or stream specified in a previous call to the <see cref="Overload:System.Drawing.Image.Save" /> method. Use this method to save selected frames from a multiple-frame image to another multiple-frame image.</summary>
		/// <param name="encoderParams">An <see cref="T:System.Drawing.Imaging.EncoderParameters" /> that holds parameters required by the image encoder that is used by the save-add operation. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060004F4 RID: 1268 RVA: 0x0000E5BC File Offset: 0x0000C7BC
		public void SaveAdd(EncoderParameters encoderParams)
		{
			IntPtr intPtr = encoderParams.ConvertToMemory();
			Status status = GDIPlus.GdipSaveAdd(this.nativeObject, intPtr);
			Marshal.FreeHGlobal(intPtr);
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Adds a frame to the file or stream specified in a previous call to the <see cref="Overload:System.Drawing.Image.Save" /> method.</summary>
		/// <param name="image">An <see cref="T:System.Drawing.Image" /> that contains the frame to add. </param>
		/// <param name="encoderParams">An <see cref="T:System.Drawing.Imaging.EncoderParameters" /> that holds parameters required by the image encoder that is used by the save-add operation. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060004F5 RID: 1269 RVA: 0x0000E5E8 File Offset: 0x0000C7E8
		public void SaveAdd(Image image, EncoderParameters encoderParams)
		{
			IntPtr intPtr = encoderParams.ConvertToMemory();
			Status status = GDIPlus.GdipSaveAddImage(this.nativeObject, image.NativeObject, intPtr);
			Marshal.FreeHGlobal(intPtr);
			GDIPlus.CheckStatus(status);
		}

		/// <summary>Selects the frame specified by the dimension and index.</summary>
		/// <returns>Always returns 0.</returns>
		/// <param name="dimension">A <see cref="T:System.Drawing.Imaging.FrameDimension" /> that specifies the identity of the dimension type. </param>
		/// <param name="frameIndex">The index of the active frame. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004F6 RID: 1270 RVA: 0x0000E61C File Offset: 0x0000C81C
		public int SelectActiveFrame(FrameDimension dimension, int frameIndex)
		{
			Guid guid = dimension.Guid;
			GDIPlus.CheckStatus(GDIPlus.GdipImageSelectActiveFrame(this.nativeObject, ref guid, frameIndex));
			return frameIndex;
		}

		/// <summary>Stores a property item (piece of metadata) in this <see cref="T:System.Drawing.Image" />.</summary>
		/// <param name="propitem">The <see cref="T:System.Drawing.Imaging.PropertyItem" /> to be stored. </param>
		/// <exception cref="T:System.ArgumentException">The image format of this image does not support property items.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060004F7 RID: 1271 RVA: 0x0000E644 File Offset: 0x0000C844
		public unsafe void SetPropertyItem(PropertyItem propitem)
		{
			if (propitem == null)
			{
				throw new ArgumentNullException("propitem");
			}
			int num = Marshal.SizeOf<byte>(propitem.Value[0]) * propitem.Value.Length;
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			try
			{
				GdipPropertyItem gdipPropertyItem = default(GdipPropertyItem);
				gdipPropertyItem.id = propitem.Id;
				gdipPropertyItem.len = propitem.Len;
				gdipPropertyItem.type = propitem.Type;
				Marshal.Copy(propitem.Value, 0, intPtr, num);
				gdipPropertyItem.value = intPtr;
				GDIPlus.CheckStatus(GDIPlus.GdipSetPropertyItem(this.nativeObject, &gdipPropertyItem));
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Gets attribute flags for the pixel data of this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The integer representing a bitwise combination of <see cref="T:System.Drawing.Imaging.ImageFlags" /> for this <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
		[Browsable(false)]
		public int Flags
		{
			get
			{
				int num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageFlags(this.nativeObject, out num));
				return num;
			}
		}

		/// <summary>Gets an array of GUIDs that represent the dimensions of frames within this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>An array of GUIDs that specify the dimensions of frames within this <see cref="T:System.Drawing.Image" /> from most significant to least significant.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000E710 File Offset: 0x0000C910
		[Browsable(false)]
		public Guid[] FrameDimensionsList
		{
			get
			{
				uint num;
				GDIPlus.CheckStatus(GDIPlus.GdipImageGetFrameDimensionsCount(this.nativeObject, out num));
				Guid[] array = new Guid[num];
				GDIPlus.CheckStatus(GDIPlus.GdipImageGetFrameDimensionsList(this.nativeObject, array, num));
				return array;
			}
		}

		/// <summary>Gets the height, in pixels, of this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The height, in pixels, of this <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0000E74C File Offset: 0x0000C94C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(false)]
		public int Height
		{
			get
			{
				uint num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageHeight(this.nativeObject, out num));
				return (int)num;
			}
		}

		/// <summary>Gets the horizontal resolution, in pixels per inch, of this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The horizontal resolution, in pixels per inch, of this <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0000E76C File Offset: 0x0000C96C
		public float HorizontalResolution
		{
			get
			{
				float num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageHorizontalResolution(this.nativeObject, out num));
				return num;
			}
		}

		/// <summary>Gets or sets the color palette used for this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Imaging.ColorPalette" /> that represents the color palette used for this <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x0000E78C File Offset: 0x0000C98C
		// (set) Token: 0x060004FD RID: 1277 RVA: 0x0000E794 File Offset: 0x0000C994
		[Browsable(false)]
		public ColorPalette Palette
		{
			get
			{
				return this.retrieveGDIPalette();
			}
			set
			{
				this.storeGDIPalette(value);
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0000E7A0 File Offset: 0x0000C9A0
		internal ColorPalette retrieveGDIPalette()
		{
			ColorPalette colorPalette = new ColorPalette();
			int num;
			GDIPlus.CheckStatus(GDIPlus.GdipGetImagePaletteSize(this.nativeObject, out num));
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			ColorPalette colorPalette2;
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipGetImagePalette(this.nativeObject, intPtr, num));
				colorPalette.ConvertFromMemory(intPtr);
				colorPalette2 = colorPalette;
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return colorPalette2;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000E804 File Offset: 0x0000CA04
		internal void storeGDIPalette(ColorPalette palette)
		{
			if (palette == null)
			{
				throw new ArgumentNullException("palette");
			}
			IntPtr intPtr = palette.ConvertToMemory();
			if (intPtr == IntPtr.Zero)
			{
				return;
			}
			try
			{
				GDIPlus.CheckStatus(GDIPlus.GdipSetImagePalette(this.nativeObject, intPtr));
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Gets the width and height of this image.</summary>
		/// <returns>A <see cref="T:System.Drawing.SizeF" /> structure that represents the width and height of this <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000500 RID: 1280 RVA: 0x0000E860 File Offset: 0x0000CA60
		public SizeF PhysicalDimension
		{
			get
			{
				float num;
				float num2;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageDimension(this.nativeObject, out num, out num2));
				return new SizeF(num, num2);
			}
		}

		/// <summary>Gets the pixel format for this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Imaging.PixelFormat" /> that represents the pixel format for this <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000E888 File Offset: 0x0000CA88
		public PixelFormat PixelFormat
		{
			get
			{
				PixelFormat pixelFormat;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImagePixelFormat(this.nativeObject, out pixelFormat));
				return pixelFormat;
			}
		}

		/// <summary>Gets IDs of the property items stored in this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>An array of the property IDs, one for each property item stored in this image.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000502 RID: 1282 RVA: 0x0000E8A8 File Offset: 0x0000CAA8
		[Browsable(false)]
		public int[] PropertyIdList
		{
			get
			{
				uint num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPropertyCount(this.nativeObject, out num));
				int[] array = new int[num];
				GDIPlus.CheckStatus(GDIPlus.GdipGetPropertyIdList(this.nativeObject, num, array));
				return array;
			}
		}

		/// <summary>Gets all the property items (pieces of metadata) stored in this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.Imaging.PropertyItem" /> objects, one for each property item stored in the image.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0000E8E4 File Offset: 0x0000CAE4
		[Browsable(false)]
		public PropertyItem[] PropertyItems
		{
			get
			{
				GdipPropertyItem gdipPropertyItem = default(GdipPropertyItem);
				int num;
				int num2;
				GDIPlus.CheckStatus(GDIPlus.GdipGetPropertySize(this.nativeObject, out num, out num2));
				PropertyItem[] array = new PropertyItem[num2];
				if (num2 == 0)
				{
					return array;
				}
				IntPtr intPtr = Marshal.AllocHGlobal(num * num2);
				try
				{
					GDIPlus.CheckStatus(GDIPlus.GdipGetAllPropertyItems(this.nativeObject, num, num2, intPtr));
					int num3 = Marshal.SizeOf<GdipPropertyItem>(gdipPropertyItem);
					IntPtr intPtr2 = intPtr;
					int i = 0;
					while (i < num2)
					{
						gdipPropertyItem = (GdipPropertyItem)Marshal.PtrToStructure(intPtr2, typeof(GdipPropertyItem));
						array[i] = new PropertyItem();
						GdipPropertyItem.MarshalTo(gdipPropertyItem, array[i]);
						i++;
						intPtr2 = new IntPtr(intPtr2.ToInt64() + (long)num3);
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
				return array;
			}
		}

		/// <summary>Gets the file format of this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Imaging.ImageFormat" /> that represents the file format of this <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000504 RID: 1284 RVA: 0x0000E9AC File Offset: 0x0000CBAC
		public ImageFormat RawFormat
		{
			get
			{
				Guid guid;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageRawFormat(this.nativeObject, out guid));
				return new ImageFormat(guid);
			}
		}

		/// <summary>Gets the width and height, in pixels, of this image.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> structure that represents the width and height, in pixels, of this image.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0000E9D1 File Offset: 0x0000CBD1
		public Size Size
		{
			get
			{
				return new Size(this.Width, this.Height);
			}
		}

		/// <summary>Gets or sets an object that provides additional data about the image.</summary>
		/// <returns>The <see cref="T:System.Object" /> that provides additional data about the image.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x0000E9EC File Offset: 0x0000CBEC
		[DefaultValue(null)]
		[Localizable(false)]
		[Bindable(true)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		/// <summary>Gets the vertical resolution, in pixels per inch, of this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The vertical resolution, in pixels per inch, of this <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000508 RID: 1288 RVA: 0x0000E9F8 File Offset: 0x0000CBF8
		public float VerticalResolution
		{
			get
			{
				float num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageVerticalResolution(this.nativeObject, out num));
				return num;
			}
		}

		/// <summary>Gets the width, in pixels, of this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The width, in pixels, of this <see cref="T:System.Drawing.Image" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0000EA18 File Offset: 0x0000CC18
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(false)]
		public int Width
		{
			get
			{
				uint num;
				GDIPlus.CheckStatus(GDIPlus.GdipGetImageWidth(this.nativeObject, out num));
				return (int)num;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x0000EA38 File Offset: 0x0000CC38
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x0000EA40 File Offset: 0x0000CC40
		internal IntPtr NativeObject
		{
			get
			{
				return this.nativeObject;
			}
			set
			{
				this.nativeObject = value;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x0000EA38 File Offset: 0x0000CC38
		internal IntPtr nativeImage
		{
			get
			{
				return this.nativeObject;
			}
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Image" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600050D RID: 1293 RVA: 0x0000EA49 File Offset: 0x0000CC49
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x0000EA58 File Offset: 0x0000CC58
		~Image()
		{
			this.Dispose(false);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Drawing.Image" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x0600050F RID: 1295 RVA: 0x0000EA88 File Offset: 0x0000CC88
		protected virtual void Dispose(bool disposing)
		{
			if (GDIPlus.GdiPlusToken != 0UL && this.nativeObject != IntPtr.Zero)
			{
				Status status = GDIPlus.GdipDisposeImage(this.nativeObject);
				if (this.stream != null)
				{
					this.stream.Dispose();
					this.stream = null;
				}
				this.nativeObject = IntPtr.Zero;
				GDIPlus.CheckStatus(status);
			}
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Image" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Image" /> this method creates, cast as an object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000510 RID: 1296 RVA: 0x0000EAE4 File Offset: 0x0000CCE4
		public object Clone()
		{
			if (GDIPlus.RunningOnWindows() && this.stream != null)
			{
				return this.CloneFromStream();
			}
			IntPtr zero = IntPtr.Zero;
			GDIPlus.CheckStatus(GDIPlus.GdipCloneImage(this.NativeObject, out zero));
			if (this is Bitmap)
			{
				return new Bitmap(zero);
			}
			return new Metafile(zero);
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0000EB34 File Offset: 0x0000CD34
		private object CloneFromStream()
		{
			MemoryStream memoryStream = new MemoryStream(new byte[this.stream.Length]);
			int num = ((this.stream.Length < 4096L) ? ((int)this.stream.Length) : 4096);
			byte[] array = new byte[num];
			this.stream.Position = 0L;
			do
			{
				num = this.stream.Read(array, 0, num);
				memoryStream.Write(array, 0, num);
			}
			while (num == 4096);
			IntPtr intPtr = IntPtr.Zero;
			intPtr = Image.InitFromStream(memoryStream);
			if (this is Bitmap)
			{
				return new Bitmap(intPtr, memoryStream);
			}
			return new Metafile(intPtr, memoryStream);
		}

		// Token: 0x040003F0 RID: 1008
		private object tag;

		// Token: 0x040003F1 RID: 1009
		internal IntPtr nativeObject = IntPtr.Zero;

		// Token: 0x040003F2 RID: 1010
		internal Stream stream;

		/// <summary>Provides a callback method for determining when the <see cref="M:System.Drawing.Image.GetThumbnailImage(System.Int32,System.Int32,System.Drawing.Image.GetThumbnailImageAbort,System.IntPtr)" /> method should prematurely cancel execution.</summary>
		/// <returns>This method returns true if it decides that the <see cref="M:System.Drawing.Image.GetThumbnailImage(System.Int32,System.Int32,System.Drawing.Image.GetThumbnailImageAbort,System.IntPtr)" /> method should prematurely stop execution; otherwise, it returns false.</returns>
		// Token: 0x02000072 RID: 114
		// (Invoke) Token: 0x06000513 RID: 1299
		public delegate bool GetThumbnailImageAbort();
	}
}
