using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization;

namespace System.Windows.Forms
{
	/// <summary>Provides the data portion of an <see cref="T:System.Windows.Forms.ImageList" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DE RID: 478
	[Serializable]
	public sealed class ImageListStreamer : ISerializable
	{
		// Token: 0x06001E78 RID: 7800 RVA: 0x000723BC File Offset: 0x000705BC
		internal ImageListStreamer(ImageList.ImageCollection imageCollection)
		{
			this.imageCollection = imageCollection;
		}

		// Token: 0x06001E79 RID: 7801 RVA: 0x000723CC File Offset: 0x000705CC
		private ImageListStreamer(SerializationInfo info, StreamingContext context)
		{
			byte[] array = (byte[])info.GetValue("Data", typeof(byte[]));
			if (array == null || array.Length <= 4)
			{
				return;
			}
			if (array[0] != 77 || array[1] != 83 || array[2] != 70 || array[3] != 116)
			{
				return;
			}
			MemoryStream decodedStream = ImageListStreamer.GetDecodedStream(array, 4, array.Length - 4);
			decodedStream.Position = 4L;
			BinaryReader binaryReader = new BinaryReader(decodedStream);
			ushort num = binaryReader.ReadUInt16();
			binaryReader.ReadUInt16();
			ushort num2 = binaryReader.ReadUInt16();
			ushort num3 = binaryReader.ReadUInt16();
			ushort num4 = binaryReader.ReadUInt16();
			uint num5 = binaryReader.ReadUInt32();
			this.back_color = Color.FromArgb((int)num5);
			binaryReader.ReadUInt16();
			short[] array2 = new short[4];
			for (int i = 0; i < 4; i++)
			{
				array2[i] = binaryReader.ReadInt16();
			}
			byte[] buffer = decodedStream.GetBuffer();
			int num6 = 28;
			int num7 = (int)buffer[num6 + 2] + ((int)buffer[num6 + 3] << 8) + ((int)buffer[num6 + 4] << 16) + ((int)buffer[num6 + 5] << 24);
			int num8 = (int)buffer[num6 + 34] + ((int)buffer[num6 + 35] << 8) + ((int)buffer[num6 + 36] << 16) + ((int)buffer[num6 + 37] << 24);
			int num9 = num8 + num7;
			MemoryStream memoryStream = new MemoryStream(buffer, num6, num9);
			Bitmap bitmap = null;
			Bitmap bitmap2 = null;
			bitmap = new Bitmap(memoryStream);
			MemoryStream memoryStream2 = new MemoryStream(buffer, num6 + num9, (int)(decodedStream.Length - (long)num6 - (long)num9));
			if (memoryStream2.Length > 0L)
			{
				bitmap2 = new Bitmap(memoryStream2);
			}
			if (num5 == 4294967295U)
			{
				this.back_color = bitmap.GetPixel(0, 0);
			}
			if (bitmap2 != null)
			{
				int width = bitmap.Width;
				int height = bitmap.Height;
				Bitmap bitmap3 = new Bitmap(bitmap);
				for (int j = 0; j < height; j++)
				{
					for (int k = 0; k < width; k++)
					{
						if (bitmap2.GetPixel(k, j).B != 0)
						{
							bitmap3.SetPixel(k, j, Color.Transparent);
						}
					}
				}
				bitmap.Dispose();
				bitmap = bitmap3;
				bitmap2.Dispose();
			}
			this.images = new Image[(int)num];
			this.image_size = new Size((int)num3, (int)num4);
			Rectangle rectangle;
			rectangle..ctor(0, 0, (int)num3, (int)num4);
			if ((int)num2 * bitmap.Width > (int)num3)
			{
				num2 = (ushort)(bitmap.Width / (int)num3);
			}
			for (int l = 0; l < (int)num; l++)
			{
				int num10 = l % (int)num2;
				int num11 = l / (int)num2;
				Rectangle rectangle2;
				rectangle2..ctor(num10 * (int)num3, num11 * (int)num4, (int)num3, (int)num4);
				Bitmap bitmap4 = new Bitmap((int)num3, (int)num4);
				using (Graphics graphics = Graphics.FromImage(bitmap4))
				{
					graphics.DrawImage(bitmap, rectangle, rectangle2, 2);
				}
				this.images[l] = bitmap4;
			}
			bitmap.Dispose();
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="si">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that is the destination for this serialization.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> cannot be populated with data because no data exists, or it is not in the correct format.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x06001E7B RID: 7803 RVA: 0x00072714 File Offset: 0x00070914
		public void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(ImageListStreamer.header);
			Image[] array = ((this.imageCollection == null) ? this.images : this.imageCollection.ToArray());
			int num = 4;
			int num2 = array.Length / num;
			if (array.Length % num > 0)
			{
				num2++;
			}
			binaryWriter.Write((ushort)array.Length);
			binaryWriter.Write((ushort)array.Length);
			binaryWriter.Write(4);
			binaryWriter.Write((ushort)array[0].Width);
			binaryWriter.Write((ushort)array[0].Height);
			binaryWriter.Write(uint.MaxValue);
			binaryWriter.Write(4105);
			for (int i = 0; i < 4; i++)
			{
				binaryWriter.Write(-1);
			}
			Bitmap bitmap = new Bitmap(num * this.ImageSize.Width, num2 * this.ImageSize.Height);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.FillRectangle(ThemeEngine.Current.ResPool.GetSolidBrush(this.BackColor), 0, 0, bitmap.Width, bitmap.Height);
				for (int j = 0; j < array.Length; j++)
				{
					graphics.DrawImage(array[j], j % num * this.ImageSize.Width, j / num * this.ImageSize.Height);
				}
			}
			MemoryStream memoryStream2 = new MemoryStream();
			bitmap.Save(memoryStream2, ImageFormat.Bmp);
			memoryStream2.WriteTo(memoryStream);
			Bitmap bitmap2 = this.Get1bppMask(bitmap);
			bitmap.Dispose();
			bitmap = null;
			memoryStream2 = new MemoryStream();
			bitmap2.Save(memoryStream2, ImageFormat.Bmp);
			memoryStream2.WriteTo(memoryStream);
			bitmap2.Dispose();
			memoryStream = ImageListStreamer.GetRLEStream(memoryStream, 4);
			si.AddValue("Data", memoryStream.ToArray(), typeof(byte[]));
		}

		// Token: 0x06001E7C RID: 7804 RVA: 0x00072930 File Offset: 0x00070B30
		private unsafe Bitmap Get1bppMask(Bitmap main)
		{
			Rectangle rectangle;
			rectangle..ctor(0, 0, main.Width, main.Height);
			Bitmap bitmap = new Bitmap(main.Width, main.Height, 196865);
			BitmapData bitmapData = bitmap.LockBits(rectangle, 3, 196865);
			int width = this.images[0].Width;
			int height = this.images[0].Height;
			byte* ptr = (byte*)bitmapData.Scan0.ToPointer();
			int stride = bitmapData.Stride;
			for (int i = 0; i < this.images.Length; i++)
			{
				Bitmap bitmap2 = (Bitmap)this.images[i];
				Color pixel = bitmap2.GetPixel(0, 0);
				if (pixel.A != 0 && pixel == this.back_color)
				{
					bitmap2.MakeTransparent(this.back_color);
				}
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			for (int j = 0; j < main.Height; j++)
			{
				if (num3 == height)
				{
					num3 = 0;
					num4 += 4;
				}
				int num5 = 0;
				int num6 = 0;
				for (int k = 0; k < main.Width; k++)
				{
					if (num6 == width)
					{
						num6 = 0;
						num5++;
					}
					num2 = num4 + num5;
					if (num2 >= this.images.Length)
					{
						break;
					}
					Bitmap bitmap2 = (Bitmap)this.images[num2];
					if (bitmap2.GetPixel(num6, num3).A == 0)
					{
						int num7 = num + (k >> 3);
						byte* ptr2 = ptr + num7;
						*ptr2 |= (byte)(128 >> (k & 7));
					}
					num6++;
				}
				if (num2 >= this.images.Length)
				{
					break;
				}
				num += stride;
				num3++;
			}
			bitmap.UnlockBits(bitmapData);
			return bitmap;
		}

		// Token: 0x06001E7D RID: 7805 RVA: 0x00072B18 File Offset: 0x00070D18
		private static MemoryStream GetDecodedStream(byte[] bytes, int offset, int size)
		{
			byte[] array = new byte[512];
			int num = 0;
			MemoryStream memoryStream = new MemoryStream();
			while (size > 0)
			{
				int num2 = (int)bytes[offset++];
				int num3 = (int)bytes[offset++];
				if (512 - num2 < num)
				{
					memoryStream.Write(array, 0, num);
					num = 0;
				}
				for (int i = 0; i < num2; i++)
				{
					array[num++] = (byte)num3;
				}
				size -= 2;
			}
			if (num > 0)
			{
				memoryStream.Write(array, 0, num);
			}
			memoryStream.Position = 0L;
			return memoryStream;
		}

		// Token: 0x06001E7E RID: 7806 RVA: 0x00072BB0 File Offset: 0x00070DB0
		private static MemoryStream GetRLEStream(MemoryStream input, int start)
		{
			MemoryStream memoryStream = new MemoryStream();
			byte[] buffer = input.GetBuffer();
			memoryStream.Write(buffer, 0, start);
			input.Position = (long)start;
			int num = -1;
			int num2 = 0;
			int num3;
			while ((num3 = input.ReadByte()) != -1)
			{
				if (num != num3 || num2 == 255)
				{
					if (num != -1)
					{
						memoryStream.WriteByte((byte)num2);
						memoryStream.WriteByte((byte)num);
					}
					num = num3;
					num2 = 0;
				}
				num2++;
			}
			if (num2 > 0)
			{
				memoryStream.WriteByte((byte)num2);
				memoryStream.WriteByte((byte)num3);
			}
			return memoryStream;
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001E7F RID: 7807 RVA: 0x00072C40 File Offset: 0x00070E40
		internal Image[] Images
		{
			get
			{
				return this.images;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x00072C48 File Offset: 0x00070E48
		internal Size ImageSize
		{
			get
			{
				return this.image_size;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x00072C50 File Offset: 0x00070E50
		internal ColorDepth ColorDepth
		{
			get
			{
				return ColorDepth.Depth32Bit;
			}
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001E82 RID: 7810 RVA: 0x00072C54 File Offset: 0x00070E54
		internal Color BackColor
		{
			get
			{
				return this.back_color;
			}
		}

		// Token: 0x04000FEA RID: 4074
		private readonly ImageList.ImageCollection imageCollection;

		// Token: 0x04000FEB RID: 4075
		private Image[] images;

		// Token: 0x04000FEC RID: 4076
		private Size image_size;

		// Token: 0x04000FED RID: 4077
		private Color back_color;

		// Token: 0x04000FEE RID: 4078
		private static byte[] header = new byte[] { 77, 83, 70, 116, 73, 76, 3, 0 };
	}
}
