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
	/// <summary>Represents a Windows icon, which is a small bitmap image that is used to represent an object. Icons can be thought of as transparent bitmaps, although their size is determined by the system.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000069 RID: 105
	[TypeConverter(typeof(IconConverter))]
	[Editor("System.Drawing.Design.IconEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Serializable]
	public sealed class Icon : MarshalByRefObject, ISerializable, ICloneable, IDisposable
	{
		// Token: 0x060004A9 RID: 1193 RVA: 0x0000C554 File Offset: 0x0000A754
		private Icon()
		{
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000C568 File Offset: 0x0000A768
		private Icon(IntPtr handle)
		{
			this.handle = handle;
			this.bitmap = Bitmap.FromHicon(handle);
			this.iconSize = new Size(this.bitmap.Width, this.bitmap.Height);
			if (GDIPlus.RunningOnUnix())
			{
				this.bitmap = Bitmap.FromHicon(handle);
				this.iconSize = new Size(this.bitmap.Width, this.bitmap.Height);
			}
			else
			{
				IconInfo iconInfo;
				GDIPlus.GetIconInfo(handle, out iconInfo);
				if (!iconInfo.IsIcon)
				{
					throw new NotImplementedException(Locale.GetText("Handle doesn't represent an ICON."));
				}
				this.iconSize = new Size(iconInfo.xHotspot * 2, iconInfo.yHotspot * 2);
				this.bitmap = Image.FromHbitmap(iconInfo.hbmColor);
			}
			this.undisposable = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class and attempts to find a version of the icon that matches the requested size.</summary>
		/// <param name="original">The icon to load the different size from. </param>
		/// <param name="width">The width of the new icon. </param>
		/// <param name="height">The height of the new icon. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="original" /> parameter is null.</exception>
		// Token: 0x060004AB RID: 1195 RVA: 0x0000C645 File Offset: 0x0000A845
		public Icon(Icon original, int width, int height)
			: this(original, new Size(width, height))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class and attempts to find a version of the icon that matches the requested size.</summary>
		/// <param name="original">The <see cref="T:System.Drawing.Icon" /> from which to load the newly sized icon. </param>
		/// <param name="size">A <see cref="T:System.Drawing.Size" /> structure that specifies the height and width of the new <see cref="T:System.Drawing.Icon" />. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="original" /> parameter is null.</exception>
		// Token: 0x060004AC RID: 1196 RVA: 0x0000C658 File Offset: 0x0000A858
		public Icon(Icon original, Size size)
		{
			if (original == null)
			{
				throw new ArgumentException("original");
			}
			this.iconSize = size;
			this.iconDir = original.iconDir;
			int idCount = (int)this.iconDir.idCount;
			if (idCount > 0)
			{
				this.imageData = original.imageData;
				this.id = ushort.MaxValue;
				ushort num = 0;
				while ((int)num < idCount)
				{
					Icon.IconDirEntry iconDirEntry = this.iconDir.idEntries[(int)num];
					if (((int)iconDirEntry.height == size.Height || (int)iconDirEntry.width == size.Width) && !iconDirEntry.ignore)
					{
						this.id = num;
						break;
					}
					num += 1;
				}
				if (this.id == 65535)
				{
					int num2 = Math.Min(size.Height, size.Width);
					Icon.IconDirEntry? iconDirEntry2 = null;
					ushort num3 = 0;
					while ((int)num3 < idCount)
					{
						Icon.IconDirEntry iconDirEntry3 = this.iconDir.idEntries[(int)num3];
						if (((int)iconDirEntry3.height < num2 || (int)iconDirEntry3.width < num2) && !iconDirEntry3.ignore)
						{
							if (iconDirEntry2 == null)
							{
								iconDirEntry2 = new Icon.IconDirEntry?(iconDirEntry3);
								this.id = num3;
							}
							else if (iconDirEntry3.height > iconDirEntry2.Value.height || iconDirEntry3.width > iconDirEntry2.Value.width)
							{
								iconDirEntry2 = new Icon.IconDirEntry?(iconDirEntry3);
								this.id = num3;
							}
						}
						num3 += 1;
					}
				}
				if (this.id == 65535)
				{
					int num4 = idCount;
					while (this.id == 65535 && num4 > 0)
					{
						num4--;
						if (!this.iconDir.idEntries[num4].ignore)
						{
							this.id = (ushort)num4;
						}
					}
				}
				if (this.id == 65535)
				{
					throw new ArgumentException("Icon", "No valid icon image found");
				}
				this.iconSize.Height = (int)this.iconDir.idEntries[(int)this.id].height;
				this.iconSize.Width = (int)this.iconDir.idEntries[(int)this.id].width;
			}
			else
			{
				this.iconSize.Height = size.Height;
				this.iconSize.Width = size.Width;
			}
			if (original.bitmap != null)
			{
				this.bitmap = (Bitmap)original.bitmap.Clone();
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class from the specified data stream.</summary>
		/// <param name="stream">The data stream from which to load the <see cref="T:System.Drawing.Icon" />. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> parameter is null.</exception>
		// Token: 0x060004AD RID: 1197 RVA: 0x0000C8D3 File Offset: 0x0000AAD3
		public Icon(Stream stream)
			: this(stream, 32, 32)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class from the specified data stream and with the specified width and height.</summary>
		/// <param name="stream">The data stream from which to load the icon. </param>
		/// <param name="width">The width, in pixels, of the icon. </param>
		/// <param name="height">The height, in pixels, of the icon. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> parameter is null.</exception>
		// Token: 0x060004AE RID: 1198 RVA: 0x0000C8E0 File Offset: 0x0000AAE0
		public Icon(Stream stream, int width, int height)
		{
			this.InitFromStreamWithSize(stream, width, height);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class from the specified file name.</summary>
		/// <param name="fileName">The file to load the <see cref="T:System.Drawing.Icon" /> from. </param>
		// Token: 0x060004AF RID: 1199 RVA: 0x0000C8FC File Offset: 0x0000AAFC
		public Icon(string fileName)
		{
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				this.InitFromStreamWithSize(fileStream, 32, 32);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class from a resource in the specified assembly.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that specifies the assembly in which to look for the resource. </param>
		/// <param name="resource">The resource name to load. </param>
		/// <exception cref="T:System.ArgumentException">An icon specified by <paramref name="resource" /> cannot be found in the assembly that contains the specified <paramref name="type" />.</exception>
		// Token: 0x060004B0 RID: 1200 RVA: 0x0000C948 File Offset: 0x0000AB48
		public Icon(Type type, string resource)
		{
			if (resource == null)
			{
				throw new ArgumentException("resource");
			}
			if (type == null)
			{
				throw new NullReferenceException();
			}
			using (Stream manifestResourceStream = type.GetTypeInfo().Assembly.GetManifestResourceStream(type, resource))
			{
				if (manifestResourceStream == null)
				{
					throw new FileNotFoundException(Locale.GetText("Resource '{0}' was not found.", new object[] { resource }));
				}
				this.InitFromStreamWithSize(manifestResourceStream, 32, 32);
			}
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000C9DC File Offset: 0x0000ABDC
		private Icon(SerializationInfo info, StreamingContext context)
		{
			MemoryStream memoryStream = null;
			int num = 0;
			int num2 = 0;
			foreach (SerializationEntry serializationEntry in info)
			{
				if (string.Compare(serializationEntry.Name, "IconData", true) == 0)
				{
					memoryStream = new MemoryStream((byte[])serializationEntry.Value);
				}
				if (string.Compare(serializationEntry.Name, "IconSize", true) == 0)
				{
					Size size = (Size)serializationEntry.Value;
					num = size.Width;
					num2 = size.Height;
				}
			}
			if (memoryStream != null)
			{
				memoryStream.Seek(0L, SeekOrigin.Begin);
				this.InitFromStreamWithSize(memoryStream, num, num2);
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000CA88 File Offset: 0x0000AC88
		internal Icon(string resourceName, bool undisposable)
		{
			using (Stream manifestResourceStream = typeof(Icon).GetTypeInfo().Assembly.GetManifestResourceStream(resourceName))
			{
				if (manifestResourceStream == null)
				{
					throw new FileNotFoundException(Locale.GetText("Resource '{0}' was not found.", new object[] { resourceName }));
				}
				this.InitFromStreamWithSize(manifestResourceStream, 32, 32);
			}
			this.undisposable = true;
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data that is required to serialize the target object.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x060004B3 RID: 1203 RVA: 0x0000CB0C File Offset: 0x0000AD0C
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			MemoryStream memoryStream = new MemoryStream();
			this.Save(memoryStream);
			si.AddValue("IconSize", this.Size, typeof(Size));
			si.AddValue("IconData", memoryStream.ToArray());
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class of the specified size from the specified stream.</summary>
		/// <param name="stream">The stream that contains the icon data.</param>
		/// <param name="size">The desired size of the icon.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="stream" /> is null or does not contain image data.</exception>
		// Token: 0x060004B4 RID: 1204 RVA: 0x0000CB57 File Offset: 0x0000AD57
		public Icon(Stream stream, Size size)
			: this(stream, size.Width, size.Height)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class with the specified width and height from the specified file.</summary>
		/// <param name="fileName">The name and path to the file that contains the <see cref="T:System.Drawing.Icon" /> data.</param>
		/// <param name="width">The desired width of the <see cref="T:System.Drawing.Icon" />.</param>
		/// <param name="height">The desired height of the <see cref="T:System.Drawing.Icon" />.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="string" /> is null or does not contain image data.</exception>
		// Token: 0x060004B5 RID: 1205 RVA: 0x0000CB70 File Offset: 0x0000AD70
		public Icon(string fileName, int width, int height)
		{
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				this.InitFromStreamWithSize(fileStream, width, height);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Icon" /> class of the specified size from the specified file.</summary>
		/// <param name="fileName">The name and path to the file that contains the icon data.</param>
		/// <param name="size">The desired size of the icon.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="string" /> is null or does not contain image data.</exception>
		// Token: 0x060004B6 RID: 1206 RVA: 0x0000CBBC File Offset: 0x0000ADBC
		public Icon(string fileName, Size size)
		{
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				this.InitFromStreamWithSize(fileStream, size.Width, size.Height);
			}
		}

		/// <summary>Returns an icon representation of an image that is contained in the specified file.</summary>
		/// <returns>The <see cref="T:System.Drawing.Icon" /> representation of the image that is contained in the specified file.</returns>
		/// <param name="filePath">The path to the file that contains an image.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="filePath" /> does not indicate a valid file.-or-The <paramref name="filePath" /> indicates a Universal Naming Convention (UNC) path.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004B7 RID: 1207 RVA: 0x0000CC14 File Offset: 0x0000AE14
		[MonoLimitation("The same icon, SystemIcons.WinLogo, is returned for all file types.")]
		public static Icon ExtractAssociatedIcon(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw new ArgumentException(Locale.GetText("Null or empty path."), "filePath");
			}
			if (!File.Exists(filePath))
			{
				throw new FileNotFoundException(Locale.GetText("Couldn't find specified file."), filePath);
			}
			return SystemIcons.WinLogo;
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Icon" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004B8 RID: 1208 RVA: 0x0000CC54 File Offset: 0x0000AE54
		public void Dispose()
		{
			if (this.undisposable)
			{
				return;
			}
			if (!this.disposed)
			{
				if (GDIPlus.RunningOnWindows() && this.handle != IntPtr.Zero)
				{
					GDIPlus.DestroyIcon(this.handle);
					this.handle = IntPtr.Zero;
				}
				if (this.bitmap != null)
				{
					this.bitmap.Dispose();
					this.bitmap = null;
				}
				GC.SuppressFinalize(this);
			}
			this.disposed = true;
		}

		/// <summary>Clones the <see cref="T:System.Drawing.Icon" />, creating a duplicate image.</summary>
		/// <returns>An object that can be cast to an <see cref="T:System.Drawing.Icon" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060004B9 RID: 1209 RVA: 0x0000CCC9 File Offset: 0x0000AEC9
		public object Clone()
		{
			return new Icon(this, this.Size);
		}

		/// <summary>Creates a GDI+ <see cref="T:System.Drawing.Icon" /> from the specified Windows handle to an icon (HICON).</summary>
		/// <returns>The <see cref="T:System.Drawing.Icon" /> this method creates.</returns>
		/// <param name="handle">A Windows handle to an icon. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004BA RID: 1210 RVA: 0x0000CCD7 File Offset: 0x0000AED7
		public static Icon FromHandle(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentException("handle");
			}
			return new Icon(handle);
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000CCF8 File Offset: 0x0000AEF8
		private void SaveIconImage(BinaryWriter writer, Icon.IconImage ii)
		{
			Icon.BitmapInfoHeader iconHeader = ii.iconHeader;
			writer.Write(iconHeader.biSize);
			writer.Write(iconHeader.biWidth);
			writer.Write(iconHeader.biHeight);
			writer.Write(iconHeader.biPlanes);
			writer.Write(iconHeader.biBitCount);
			writer.Write(iconHeader.biCompression);
			writer.Write(iconHeader.biSizeImage);
			writer.Write(iconHeader.biXPelsPerMeter);
			writer.Write(iconHeader.biYPelsPerMeter);
			writer.Write(iconHeader.biClrUsed);
			writer.Write(iconHeader.biClrImportant);
			int num = ii.iconColors.Length;
			for (int i = 0; i < num; i++)
			{
				writer.Write(ii.iconColors[i]);
			}
			writer.Write(ii.iconXOR);
			writer.Write(ii.iconAND);
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0000CDCB File Offset: 0x0000AFCB
		private void SaveIconDump(BinaryWriter writer, Icon.IconDump id)
		{
			writer.Write(id.data);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0000CDDC File Offset: 0x0000AFDC
		private void SaveIconDirEntry(BinaryWriter writer, Icon.IconDirEntry ide, uint offset)
		{
			writer.Write(ide.width);
			writer.Write(ide.height);
			writer.Write(ide.colorCount);
			writer.Write(ide.reserved);
			writer.Write(ide.planes);
			writer.Write(ide.bitCount);
			writer.Write(ide.bytesInRes);
			writer.Write((offset == uint.MaxValue) ? ide.imageOffset : offset);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0000CE50 File Offset: 0x0000B050
		private void SaveAll(BinaryWriter writer)
		{
			writer.Write(this.iconDir.idReserved);
			writer.Write(this.iconDir.idType);
			ushort idCount = this.iconDir.idCount;
			writer.Write(idCount);
			for (int i = 0; i < (int)idCount; i++)
			{
				this.SaveIconDirEntry(writer, this.iconDir.idEntries[i], uint.MaxValue);
			}
			for (int j = 0; j < (int)idCount; j++)
			{
				while (writer.BaseStream.Length < (long)((ulong)this.iconDir.idEntries[j].imageOffset))
				{
					writer.Write(0);
				}
				if (this.imageData[j] is Icon.IconDump)
				{
					this.SaveIconDump(writer, (Icon.IconDump)this.imageData[j]);
				}
				else
				{
					this.SaveIconImage(writer, (Icon.IconImage)this.imageData[j]);
				}
			}
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0000CF28 File Offset: 0x0000B128
		private void SaveBestSingleIcon(BinaryWriter writer, int width, int height)
		{
			writer.Write(this.iconDir.idReserved);
			writer.Write(this.iconDir.idType);
			writer.Write(1);
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < (int)this.iconDir.idCount; i++)
			{
				Icon.IconDirEntry iconDirEntry = this.iconDir.idEntries[i];
				if (width == (int)iconDirEntry.width && height == (int)iconDirEntry.height && (int)iconDirEntry.bitCount >= num2)
				{
					num2 = (int)iconDirEntry.bitCount;
					num = i;
				}
			}
			this.SaveIconDirEntry(writer, this.iconDir.idEntries[num], 22U);
			this.SaveIconImage(writer, (Icon.IconImage)this.imageData[num]);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0000CFDC File Offset: 0x0000B1DC
		private void SaveBitmapAsIcon(BinaryWriter writer)
		{
			writer.Write(0);
			writer.Write(1);
			writer.Write(1);
			Icon.IconDirEntry iconDirEntry = default(Icon.IconDirEntry);
			iconDirEntry.width = (byte)this.bitmap.Width;
			iconDirEntry.height = (byte)this.bitmap.Height;
			iconDirEntry.colorCount = 0;
			iconDirEntry.reserved = 0;
			iconDirEntry.planes = 0;
			iconDirEntry.bitCount = 32;
			iconDirEntry.imageOffset = 22U;
			Icon.BitmapInfoHeader bitmapInfoHeader = default(Icon.BitmapInfoHeader);
			bitmapInfoHeader.biSize = (uint)Marshal.SizeOf(typeof(Icon.BitmapInfoHeader));
			bitmapInfoHeader.biWidth = this.bitmap.Width;
			bitmapInfoHeader.biHeight = 2 * this.bitmap.Height;
			bitmapInfoHeader.biPlanes = 1;
			bitmapInfoHeader.biBitCount = 32;
			bitmapInfoHeader.biCompression = 0U;
			bitmapInfoHeader.biSizeImage = 0U;
			bitmapInfoHeader.biXPelsPerMeter = 0;
			bitmapInfoHeader.biYPelsPerMeter = 0;
			bitmapInfoHeader.biClrUsed = 0U;
			bitmapInfoHeader.biClrImportant = 0U;
			Icon.IconImage iconImage = new Icon.IconImage();
			iconImage.iconHeader = bitmapInfoHeader;
			iconImage.iconColors = new uint[0];
			int num = ((((int)bitmapInfoHeader.biBitCount * this.bitmap.Width + 31) & -32) >> 3) * this.bitmap.Height;
			iconImage.iconXOR = new byte[num];
			int num2 = 0;
			for (int i = this.bitmap.Height - 1; i >= 0; i--)
			{
				for (int j = 0; j < this.bitmap.Width; j++)
				{
					Color pixel = this.bitmap.GetPixel(j, i);
					iconImage.iconXOR[num2++] = pixel.B;
					iconImage.iconXOR[num2++] = pixel.G;
					iconImage.iconXOR[num2++] = pixel.R;
					iconImage.iconXOR[num2++] = pixel.A;
				}
			}
			int num3 = (((this.Width + 31) & -32) >> 3) * this.bitmap.Height;
			iconImage.iconAND = new byte[num3];
			iconDirEntry.bytesInRes = (uint)((ulong)bitmapInfoHeader.biSize + (ulong)((long)num) + (ulong)((long)num3));
			this.SaveIconDirEntry(writer, iconDirEntry, uint.MaxValue);
			this.SaveIconImage(writer, iconImage);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0000D21C File Offset: 0x0000B41C
		private void Save(Stream outputStream, int width, int height)
		{
			BinaryWriter binaryWriter = new BinaryWriter(outputStream);
			if (this.iconDir.idEntries != null)
			{
				if (width == -1 && height == -1)
				{
					this.SaveAll(binaryWriter);
				}
				else
				{
					this.SaveBestSingleIcon(binaryWriter, width, height);
				}
			}
			else if (this.bitmap != null)
			{
				this.SaveBitmapAsIcon(binaryWriter);
			}
			binaryWriter.Flush();
		}

		/// <summary>Saves this <see cref="T:System.Drawing.Icon" /> to the specified output <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="outputStream">The <see cref="T:System.IO.Stream" /> to save to. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060004C2 RID: 1218 RVA: 0x0000D26E File Offset: 0x0000B46E
		public void Save(Stream outputStream)
		{
			if (outputStream == null)
			{
				throw new NullReferenceException("outputStream");
			}
			this.Save(outputStream, -1, -1);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0000D288 File Offset: 0x0000B488
		internal Bitmap BuildBitmapOnWin32()
		{
			if (this.imageData == null)
			{
				return new Bitmap(32, 32);
			}
			Icon.IconImage iconImage = (Icon.IconImage)this.imageData[(int)this.id];
			Icon.BitmapInfoHeader iconHeader = iconImage.iconHeader;
			int num = iconHeader.biHeight / 2;
			if (iconHeader.biClrUsed == 0U)
			{
				ushort biBitCount = iconHeader.biBitCount;
			}
			ushort biBitCount2 = iconHeader.biBitCount;
			Bitmap bitmap;
			if (biBitCount2 <= 4)
			{
				if (biBitCount2 == 1)
				{
					bitmap = new Bitmap(iconHeader.biWidth, num, PixelFormat.Format1bppIndexed);
					goto IL_00FB;
				}
				if (biBitCount2 == 4)
				{
					bitmap = new Bitmap(iconHeader.biWidth, num, PixelFormat.Format4bppIndexed);
					goto IL_00FB;
				}
			}
			else
			{
				if (biBitCount2 == 8)
				{
					bitmap = new Bitmap(iconHeader.biWidth, num, PixelFormat.Format8bppIndexed);
					goto IL_00FB;
				}
				if (biBitCount2 == 24)
				{
					bitmap = new Bitmap(iconHeader.biWidth, num, PixelFormat.Format24bppRgb);
					goto IL_00FB;
				}
				if (biBitCount2 == 32)
				{
					bitmap = new Bitmap(iconHeader.biWidth, num, PixelFormat.Format32bppArgb);
					goto IL_00FB;
				}
			}
			throw new Exception(Locale.GetText("Unexpected number of bits: {0}", new object[] { iconHeader.biBitCount }));
			IL_00FB:
			if (iconHeader.biBitCount < 24)
			{
				ColorPalette palette = bitmap.Palette;
				for (int i = 0; i < iconImage.iconColors.Length; i++)
				{
					palette.Entries[i] = Color.FromArgb((int)(iconImage.iconColors[i] | 4278190080U));
				}
				bitmap.Palette = palette;
			}
			int num2 = ((iconHeader.biWidth * (int)iconHeader.biBitCount + 31) & -32) >> 3;
			BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
			for (int j = 0; j < num; j++)
			{
				Marshal.Copy(iconImage.iconXOR, num2 * j, (IntPtr)(bitmapData.Scan0.ToInt64() + (long)(bitmapData.Stride * (num - 1 - j))), num2);
			}
			bitmap.UnlockBits(bitmapData);
			bitmap = new Bitmap(bitmap);
			num2 = ((iconHeader.biWidth + 31) & -32) >> 3;
			for (int k = 0; k < num; k++)
			{
				for (int l = 0; l < iconHeader.biWidth / 8; l++)
				{
					for (int m = 7; m >= 0; m--)
					{
						if (((iconImage.iconAND[k * num2 + l] >> m) & 1) != 0)
						{
							bitmap.SetPixel(l * 8 + 7 - m, num - k - 1, Color.Transparent);
						}
					}
				}
			}
			return bitmap;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000D4F0 File Offset: 0x0000B6F0
		internal Bitmap GetInternalBitmap()
		{
			if (this.bitmap == null)
			{
				if (GDIPlus.RunningOnUnix())
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						this.Save(memoryStream, this.Width, this.Height);
						memoryStream.Position = 0L;
						this.bitmap = (Bitmap)Image.LoadFromStream(memoryStream, false);
						goto IL_005A;
					}
				}
				this.bitmap = this.BuildBitmapOnWin32();
			}
			IL_005A:
			return this.bitmap;
		}

		/// <summary>Converts this <see cref="T:System.Drawing.Icon" /> to a GDI+ <see cref="T:System.Drawing.Bitmap" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Bitmap" /> that represents the converted <see cref="T:System.Drawing.Icon" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x060004C5 RID: 1221 RVA: 0x0000D570 File Offset: 0x0000B770
		public Bitmap ToBitmap()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Icon instance was disposed."));
			}
			return new Bitmap(this.GetInternalBitmap());
		}

		/// <summary>Gets a human-readable string that describes the <see cref="T:System.Drawing.Icon" />.</summary>
		/// <returns>A string that describes the <see cref="T:System.Drawing.Icon" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060004C6 RID: 1222 RVA: 0x0000D595 File Offset: 0x0000B795
		public override string ToString()
		{
			return "<Icon>";
		}

		/// <summary>Gets the Windows handle for this <see cref="T:System.Drawing.Icon" />. This is not a copy of the handle; do not free it.</summary>
		/// <returns>The Windows handle for the icon.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000D59C File Offset: 0x0000B79C
		[Browsable(false)]
		public IntPtr Handle
		{
			get
			{
				if (!this.disposed && this.handle == IntPtr.Zero)
				{
					if (GDIPlus.RunningOnUnix())
					{
						this.handle = this.GetInternalBitmap().NativeObject;
					}
					else
					{
						IconInfo iconInfo = default(IconInfo);
						iconInfo.IsIcon = true;
						iconInfo.hbmColor = this.ToBitmap().GetHbitmap();
						iconInfo.hbmMask = iconInfo.hbmColor;
						this.handle = GDIPlus.CreateIconIndirect(ref iconInfo);
					}
				}
				return this.handle;
			}
		}

		/// <summary>Gets the height of this <see cref="T:System.Drawing.Icon" />.</summary>
		/// <returns>The height of this <see cref="T:System.Drawing.Icon" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x0000D61F File Offset: 0x0000B81F
		[Browsable(false)]
		public int Height
		{
			get
			{
				return this.iconSize.Height;
			}
		}

		/// <summary>Gets the size of this <see cref="T:System.Drawing.Icon" />.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> structure that specifies the width and height of this <see cref="T:System.Drawing.Icon" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000D62C File Offset: 0x0000B82C
		public Size Size
		{
			get
			{
				return this.iconSize;
			}
		}

		/// <summary>Gets the width of this <see cref="T:System.Drawing.Icon" />.</summary>
		/// <returns>The width of this <see cref="T:System.Drawing.Icon" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0000D634 File Offset: 0x0000B834
		[Browsable(false)]
		public int Width
		{
			get
			{
				return this.iconSize.Width;
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000D644 File Offset: 0x0000B844
		~Icon()
		{
			this.Dispose();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000D670 File Offset: 0x0000B870
		private void InitFromStreamWithSize(Stream stream, int width, int height)
		{
			if (stream == null || stream.Length == 0L)
			{
				throw new ArgumentException("The argument 'stream' must be a picture that can be used as a Icon", "stream");
			}
			BinaryReader binaryReader = new BinaryReader(stream);
			this.iconDir.idReserved = binaryReader.ReadUInt16();
			if (this.iconDir.idReserved != 0)
			{
				throw new ArgumentException("Invalid Argument", "stream");
			}
			this.iconDir.idType = binaryReader.ReadUInt16();
			if (this.iconDir.idType != 1)
			{
				throw new ArgumentException("Invalid Argument", "stream");
			}
			ushort num = binaryReader.ReadUInt16();
			this.imageData = new Icon.ImageData[(int)num];
			this.iconDir.idCount = num;
			this.iconDir.idEntries = new Icon.IconDirEntry[(int)num];
			bool flag = false;
			for (int i = 0; i < (int)num; i++)
			{
				Icon.IconDirEntry iconDirEntry;
				iconDirEntry.width = binaryReader.ReadByte();
				iconDirEntry.height = binaryReader.ReadByte();
				iconDirEntry.colorCount = binaryReader.ReadByte();
				iconDirEntry.reserved = binaryReader.ReadByte();
				iconDirEntry.planes = binaryReader.ReadUInt16();
				iconDirEntry.bitCount = binaryReader.ReadUInt16();
				iconDirEntry.bytesInRes = binaryReader.ReadUInt32();
				iconDirEntry.imageOffset = binaryReader.ReadUInt32();
				if (iconDirEntry.width == 0 && iconDirEntry.height == 0)
				{
					iconDirEntry.ignore = true;
				}
				else
				{
					iconDirEntry.ignore = false;
				}
				this.iconDir.idEntries[i] = iconDirEntry;
				if (!flag && ((int)iconDirEntry.height == height || (int)iconDirEntry.width == width) && !iconDirEntry.ignore)
				{
					this.id = (ushort)i;
					flag = true;
					this.iconSize.Height = (int)iconDirEntry.height;
					this.iconSize.Width = (int)iconDirEntry.width;
				}
			}
			int num2 = 0;
			for (int j = 0; j < (int)num; j++)
			{
				if (!this.iconDir.idEntries[j].ignore)
				{
					num2++;
				}
			}
			if (num2 == 0)
			{
				throw new Win32Exception(0, "No valid icon entry were found.");
			}
			if (!flag)
			{
				uint num3 = 0U;
				for (int k = 0; k < (int)num; k++)
				{
					if (this.iconDir.idEntries[k].bytesInRes >= num3 && !this.iconDir.idEntries[k].ignore)
					{
						num3 = this.iconDir.idEntries[k].bytesInRes;
						this.id = (ushort)k;
						this.iconSize.Height = (int)this.iconDir.idEntries[k].height;
						this.iconSize.Width = (int)this.iconDir.idEntries[k].width;
					}
				}
			}
			for (int l = 0; l < (int)num; l++)
			{
				if (this.iconDir.idEntries[l].ignore)
				{
					Icon.IconDump iconDump = new Icon.IconDump();
					stream.Seek((long)((ulong)this.iconDir.idEntries[l].imageOffset), SeekOrigin.Begin);
					iconDump.data = new byte[this.iconDir.idEntries[l].bytesInRes];
					stream.Read(iconDump.data, 0, iconDump.data.Length);
					this.imageData[l] = iconDump;
				}
				else
				{
					Icon.IconImage iconImage = new Icon.IconImage();
					Icon.BitmapInfoHeader bitmapInfoHeader = default(Icon.BitmapInfoHeader);
					stream.Seek((long)((ulong)this.iconDir.idEntries[l].imageOffset), SeekOrigin.Begin);
					byte[] array = new byte[this.iconDir.idEntries[l].bytesInRes];
					stream.Read(array, 0, array.Length);
					BinaryReader binaryReader2 = new BinaryReader(new MemoryStream(array));
					bitmapInfoHeader.biSize = binaryReader2.ReadUInt32();
					bitmapInfoHeader.biWidth = binaryReader2.ReadInt32();
					bitmapInfoHeader.biHeight = binaryReader2.ReadInt32();
					bitmapInfoHeader.biPlanes = binaryReader2.ReadUInt16();
					bitmapInfoHeader.biBitCount = binaryReader2.ReadUInt16();
					bitmapInfoHeader.biCompression = binaryReader2.ReadUInt32();
					bitmapInfoHeader.biSizeImage = binaryReader2.ReadUInt32();
					bitmapInfoHeader.biXPelsPerMeter = binaryReader2.ReadInt32();
					bitmapInfoHeader.biYPelsPerMeter = binaryReader2.ReadInt32();
					bitmapInfoHeader.biClrUsed = binaryReader2.ReadUInt32();
					bitmapInfoHeader.biClrImportant = binaryReader2.ReadUInt32();
					iconImage.iconHeader = bitmapInfoHeader;
					ushort biBitCount = bitmapInfoHeader.biBitCount;
					int num4;
					if (biBitCount != 1)
					{
						if (biBitCount != 4)
						{
							if (biBitCount != 8)
							{
								num4 = 0;
							}
							else
							{
								num4 = 256;
							}
						}
						else
						{
							num4 = 16;
						}
					}
					else
					{
						num4 = 2;
					}
					iconImage.iconColors = new uint[num4];
					for (int m = 0; m < num4; m++)
					{
						iconImage.iconColors[m] = binaryReader2.ReadUInt32();
					}
					int num5 = bitmapInfoHeader.biHeight / 2;
					int num6 = (bitmapInfoHeader.biWidth * (int)bitmapInfoHeader.biPlanes * (int)bitmapInfoHeader.biBitCount + 31 >> 5 << 2) * num5;
					iconImage.iconXOR = new byte[num6];
					int num7 = binaryReader2.Read(iconImage.iconXOR, 0, num6);
					if (num7 != num6)
					{
						throw new ArgumentException(Locale.GetText("{0} data length expected {1}, read {2}", new object[] { "XOR", num6, num7 }), "stream");
					}
					int num8 = (((bitmapInfoHeader.biWidth + 31) & -32) >> 3) * num5;
					iconImage.iconAND = new byte[num8];
					num7 = binaryReader2.Read(iconImage.iconAND, 0, num8);
					if (num7 != num8)
					{
						throw new ArgumentException(Locale.GetText("{0} data length expected {1}, read {2}", new object[] { "AND", num8, num7 }), "stream");
					}
					this.imageData[l] = iconImage;
					binaryReader2.Dispose();
				}
			}
			binaryReader.Dispose();
		}

		// Token: 0x040003CB RID: 971
		private Size iconSize;

		// Token: 0x040003CC RID: 972
		private IntPtr handle = IntPtr.Zero;

		// Token: 0x040003CD RID: 973
		private Icon.IconDir iconDir;

		// Token: 0x040003CE RID: 974
		private ushort id;

		// Token: 0x040003CF RID: 975
		private Icon.ImageData[] imageData;

		// Token: 0x040003D0 RID: 976
		private bool undisposable;

		// Token: 0x040003D1 RID: 977
		private bool disposed;

		// Token: 0x040003D2 RID: 978
		private Bitmap bitmap;

		// Token: 0x0200006A RID: 106
		internal struct IconDirEntry
		{
			// Token: 0x040003D3 RID: 979
			internal byte width;

			// Token: 0x040003D4 RID: 980
			internal byte height;

			// Token: 0x040003D5 RID: 981
			internal byte colorCount;

			// Token: 0x040003D6 RID: 982
			internal byte reserved;

			// Token: 0x040003D7 RID: 983
			internal ushort planes;

			// Token: 0x040003D8 RID: 984
			internal ushort bitCount;

			// Token: 0x040003D9 RID: 985
			internal uint bytesInRes;

			// Token: 0x040003DA RID: 986
			internal uint imageOffset;

			// Token: 0x040003DB RID: 987
			internal bool ignore;
		}

		// Token: 0x0200006B RID: 107
		internal struct IconDir
		{
			// Token: 0x040003DC RID: 988
			internal ushort idReserved;

			// Token: 0x040003DD RID: 989
			internal ushort idType;

			// Token: 0x040003DE RID: 990
			internal ushort idCount;

			// Token: 0x040003DF RID: 991
			internal Icon.IconDirEntry[] idEntries;
		}

		// Token: 0x0200006C RID: 108
		internal struct BitmapInfoHeader
		{
			// Token: 0x040003E0 RID: 992
			internal uint biSize;

			// Token: 0x040003E1 RID: 993
			internal int biWidth;

			// Token: 0x040003E2 RID: 994
			internal int biHeight;

			// Token: 0x040003E3 RID: 995
			internal ushort biPlanes;

			// Token: 0x040003E4 RID: 996
			internal ushort biBitCount;

			// Token: 0x040003E5 RID: 997
			internal uint biCompression;

			// Token: 0x040003E6 RID: 998
			internal uint biSizeImage;

			// Token: 0x040003E7 RID: 999
			internal int biXPelsPerMeter;

			// Token: 0x040003E8 RID: 1000
			internal int biYPelsPerMeter;

			// Token: 0x040003E9 RID: 1001
			internal uint biClrUsed;

			// Token: 0x040003EA RID: 1002
			internal uint biClrImportant;
		}

		// Token: 0x0200006D RID: 109
		[StructLayout(LayoutKind.Sequential)]
		internal abstract class ImageData
		{
		}

		// Token: 0x0200006E RID: 110
		[StructLayout(LayoutKind.Sequential)]
		internal class IconImage : Icon.ImageData
		{
			// Token: 0x040003EB RID: 1003
			internal Icon.BitmapInfoHeader iconHeader;

			// Token: 0x040003EC RID: 1004
			internal uint[] iconColors;

			// Token: 0x040003ED RID: 1005
			internal byte[] iconXOR;

			// Token: 0x040003EE RID: 1006
			internal byte[] iconAND;
		}

		// Token: 0x0200006F RID: 111
		[StructLayout(LayoutKind.Sequential)]
		internal class IconDump : Icon.ImageData
		{
			// Token: 0x040003EF RID: 1007
			internal byte[] data;
		}
	}
}
