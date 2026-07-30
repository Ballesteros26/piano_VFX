using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Windows.Forms
{
	/// <summary>Represents the image used to paint the mouse pointer.</summary>
	/// <filterpriority>1</filterpriority>
	/// <completionlist cref="T:System.Windows.Forms.Cursors" />
	// Token: 0x020000B4 RID: 180
	[Editor("System.Drawing.Design.CursorEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[TypeConverter(typeof(CursorConverter))]
	[Serializable]
	public sealed class Cursor : IDisposable, ISerializable
	{
		// Token: 0x06000B44 RID: 2884 RVA: 0x0002DB60 File Offset: 0x0002BD60
		internal Cursor(StdCursor cursor)
			: this(XplatUI.DefineStdCursor(cursor))
		{
			this.std_cursor = cursor;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0002DB78 File Offset: 0x0002BD78
		private Cursor(SerializationInfo info, StreamingContext context)
		{
			this.std_cursor = (StdCursor)(-1);
			base..ctor();
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x0002DB88 File Offset: 0x0002BD88
		private Cursor()
		{
			this.std_cursor = (StdCursor)(-1);
			base..ctor();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor" /> class from the specified Windows handle.</summary>
		/// <param name="handle">An <see cref="T:System.IntPtr" /> that represents the Windows handle of the cursor to create. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is <see cref="F:System.IntPtr.Zero" />. </exception>
		// Token: 0x06000B47 RID: 2887 RVA: 0x0002DB98 File Offset: 0x0002BD98
		public Cursor(IntPtr handle)
		{
			this.std_cursor = (StdCursor)(-1);
			base..ctor();
			this.handle = handle;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor" /> class from the specified data stream.</summary>
		/// <param name="stream">The data stream to load the <see cref="T:System.Windows.Forms.Cursor" /> from. </param>
		// Token: 0x06000B48 RID: 2888 RVA: 0x0002DBB0 File Offset: 0x0002BDB0
		public Cursor(Stream stream)
		{
			this.std_cursor = (StdCursor)(-1);
			base..ctor();
			this.CreateCursor(stream);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor" /> class from the specified file.</summary>
		/// <param name="fileName">The cursor file to load. </param>
		// Token: 0x06000B49 RID: 2889 RVA: 0x0002DBC8 File Offset: 0x0002BDC8
		public Cursor(string fileName)
		{
			this.std_cursor = (StdCursor)(-1);
			base..ctor();
			using (FileStream fileStream = File.OpenRead(fileName))
			{
				this.CreateCursor(fileStream);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Cursor" /> class from the specified resource with the specified resource type.</summary>
		/// <param name="type">The resource <see cref="T:System.Type" />. </param>
		/// <param name="resource">The name of the resource. </param>
		// Token: 0x06000B4A RID: 2890 RVA: 0x0002DC20 File Offset: 0x0002BE20
		public Cursor(Type type, string resource)
		{
			this.std_cursor = (StdCursor)(-1);
			base..ctor();
			using (Stream manifestResourceStream = type.Assembly.GetManifestResourceStream(type, resource))
			{
				if (manifestResourceStream != null)
				{
					this.CreateCursor(manifestResourceStream);
					return;
				}
			}
			using (Stream manifestResourceStream2 = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
			{
				if (manifestResourceStream2 != null)
				{
					this.CreateCursor(manifestResourceStream2);
					return;
				}
			}
			throw new FileNotFoundException("Resource name was not found: `" + resource + "'");
		}

		/// <summary>Serializes the object.</summary>
		/// <param name="si">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> class.</param>
		/// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> class.</param>
		// Token: 0x06000B4B RID: 2891 RVA: 0x0002DCE8 File Offset: 0x0002BEE8
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			MemoryStream memoryStream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			Cursor.CursorImage cursorImage = this.cursor_data[this.id];
			binaryWriter.Write(0);
			binaryWriter.Write(2);
			binaryWriter.Write(1);
			binaryWriter.Write(this.cursor_dir.idEntries[this.id].width);
			binaryWriter.Write(this.cursor_dir.idEntries[this.id].height);
			binaryWriter.Write(this.cursor_dir.idEntries[this.id].colorCount);
			binaryWriter.Write(this.cursor_dir.idEntries[this.id].reserved);
			binaryWriter.Write(this.cursor_dir.idEntries[this.id].xHotspot);
			binaryWriter.Write(this.cursor_dir.idEntries[this.id].yHotspot);
			binaryWriter.Write((uint)(40 + cursorImage.cursorColors.Length * 4 + cursorImage.cursorXOR.Length + cursorImage.cursorAND.Length));
			binaryWriter.Write(22U);
			binaryWriter.Write(cursorImage.cursorHeader.biSize);
			binaryWriter.Write(cursorImage.cursorHeader.biWidth);
			binaryWriter.Write(cursorImage.cursorHeader.biHeight);
			binaryWriter.Write(cursorImage.cursorHeader.biPlanes);
			binaryWriter.Write(cursorImage.cursorHeader.biBitCount);
			binaryWriter.Write(cursorImage.cursorHeader.biCompression);
			binaryWriter.Write(cursorImage.cursorHeader.biSizeImage);
			binaryWriter.Write(cursorImage.cursorHeader.biXPelsPerMeter);
			binaryWriter.Write(cursorImage.cursorHeader.biYPelsPerMeter);
			binaryWriter.Write(cursorImage.cursorHeader.biClrUsed);
			binaryWriter.Write(cursorImage.cursorHeader.biClrImportant);
			for (int i = 0; i < cursorImage.cursorColors.Length; i++)
			{
				binaryWriter.Write(cursorImage.cursorColors[i]);
			}
			binaryWriter.Write(cursorImage.cursorXOR);
			binaryWriter.Write(cursorImage.cursorAND);
			binaryWriter.Flush();
			si.AddValue("CursorData", memoryStream.ToArray());
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x0002DF44 File Offset: 0x0002C144
		private void CreateCursor(Stream stream)
		{
			this.InitFromStream(stream);
			this.shape = this.ToBitmap(true, false);
			this.mask = this.ToBitmap(false, false);
			this.handle = XplatUI.DefineCursor(this.shape, this.mask, Color.FromArgb(255, 255, 255), Color.FromArgb(255, 255, 255), (int)this.cursor_dir.idEntries[this.id].xHotspot, (int)this.cursor_dir.idEntries[this.id].yHotspot);
			this.shape.Dispose();
			this.shape = null;
			this.mask.Dispose();
			this.mask = null;
			if (this.handle != IntPtr.Zero)
			{
				this.cursor = this.ToBitmap(true, true);
			}
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0002E030 File Offset: 0x0002C230
		~Cursor()
		{
			this.Dispose();
		}

		/// <summary>Gets or sets the bounds that represent the clipping rectangle for the cursor.</summary>
		/// <returns>The <see cref="T:System.Drawing.Rectangle" /> that represents the clipping rectangle for the <see cref="T:System.Windows.Forms.Cursor" />, in screen coordinates.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x0002E06C File Offset: 0x0002C26C
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x0002E0CC File Offset: 0x0002C2CC
		public static Rectangle Clip
		{
			get
			{
				IntPtr intPtr;
				bool flag;
				Rectangle rectangle;
				XplatUI.GrabInfo(out intPtr, out flag, out rectangle);
				if (intPtr != IntPtr.Zero)
				{
					return rectangle;
				}
				Size size;
				XplatUI.GetDisplaySize(out size);
				rectangle.X = 0;
				rectangle.Y = 0;
				rectangle.Width = size.Width;
				rectangle.Height = size.Height;
				return rectangle;
			}
			[MonoInternalNote("First need to add ability to set cursor clip rectangle to XplatUI drivers to implement this property")]
			[MonoTODO("Stub, does nothing")]
			set
			{
			}
		}

		/// <summary>Gets or sets a cursor object that represents the mouse cursor.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the mouse cursor. The default is null if the mouse cursor is not visible.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x0002E0D0 File Offset: 0x0002C2D0
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x0002E0F0 File Offset: 0x0002C2F0
		public static Cursor Current
		{
			get
			{
				if (Cursor.current != null)
				{
					return Cursor.current;
				}
				return Cursors.Default;
			}
			set
			{
				if (Cursor.current != value)
				{
					Cursor.current = value;
					if (Cursor.current == null)
					{
						XplatUI.OverrideCursor(IntPtr.Zero);
					}
					else
					{
						XplatUI.OverrideCursor(Cursor.current.handle);
					}
				}
			}
		}

		/// <summary>Gets or sets the cursor's position.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> that represents the cursor's position in screen coordinates.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x0002E144 File Offset: 0x0002C344
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x0002E168 File Offset: 0x0002C368
		public static Point Position
		{
			get
			{
				int num;
				int num2;
				XplatUI.GetCursorPos(IntPtr.Zero, out num, out num2);
				return new Point(num, num2);
			}
			set
			{
				XplatUI.SetCursorPos(IntPtr.Zero, value.X, value.Y);
			}
		}

		/// <summary>Gets the handle of the cursor.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that represents the cursor's handle.</returns>
		/// <exception cref="T:System.Exception">The handle value is <see cref="F:System.IntPtr.Zero" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x0002E184 File Offset: 0x0002C384
		public IntPtr Handle
		{
			get
			{
				return this.handle;
			}
		}

		/// <summary>Gets the cursor hot spot.</summary>
		/// <returns>A <see cref="T:System.Drawing.Point" /> representing the cursor hot spot.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000B55 RID: 2901 RVA: 0x0002E18C File Offset: 0x0002C38C
		[MonoTODO("Implemented for Win32, X11 always returns 0,0")]
		public Point HotSpot
		{
			get
			{
				int num;
				int num2;
				int num3;
				int num4;
				XplatUI.GetCursorInfo(this.Handle, out num, out num2, out num3, out num4);
				return new Point(num3, num4);
			}
		}

		/// <summary>Gets the size of the cursor object.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that represents the width and height of the <see cref="T:System.Windows.Forms.Cursor" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x0002E1B4 File Offset: 0x0002C3B4
		public Size Size
		{
			get
			{
				return this.size;
			}
		}

		/// <summary>Gets or sets the object that contains data about the <see cref="T:System.Windows.Forms.Cursor" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.Cursor" />. The default is null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000B57 RID: 2903 RVA: 0x0002E1BC File Offset: 0x0002C3BC
		// (set) Token: 0x06000B58 RID: 2904 RVA: 0x0002E1C4 File Offset: 0x0002C3C4
		[TypeConverter(typeof(StringConverter))]
		[MWFCategory("Data")]
		[Localizable(false)]
		[DefaultValue(null)]
		[Bindable(true)]
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

		/// <summary>Hides the cursor.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B59 RID: 2905 RVA: 0x0002E1D0 File Offset: 0x0002C3D0
		public static void Hide()
		{
			XplatUI.ShowCursor(false);
		}

		/// <summary>Displays the cursor.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B5A RID: 2906 RVA: 0x0002E1D8 File Offset: 0x0002C3D8
		public static void Show()
		{
			XplatUI.ShowCursor(true);
		}

		/// <summary>Copies the handle of this <see cref="T:System.Windows.Forms.Cursor" />.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> that represents the cursor's handle.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B5B RID: 2907 RVA: 0x0002E1E0 File Offset: 0x0002C3E0
		public IntPtr CopyHandle()
		{
			return this.handle;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.Cursor" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B5C RID: 2908 RVA: 0x0002E1E8 File Offset: 0x0002C3E8
		public void Dispose()
		{
			if (this.cursor != null)
			{
				this.cursor.Dispose();
				this.cursor = null;
			}
			if (this.shape != null)
			{
				this.shape.Dispose();
				this.shape = null;
			}
			if (this.mask != null)
			{
				this.mask.Dispose();
				this.mask = null;
			}
			GC.SuppressFinalize(this);
		}

		/// <summary>Draws the cursor on the specified surface, within the specified bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw the <see cref="T:System.Windows.Forms.Cursor" />. </param>
		/// <param name="targetRect">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.Cursor" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B5D RID: 2909 RVA: 0x0002E254 File Offset: 0x0002C454
		public void Draw(Graphics g, Rectangle targetRect)
		{
			if (this.cursor == null && this.std_cursor != (StdCursor)(-1))
			{
				this.cursor = XplatUI.DefineStdCursorBitmap(this.std_cursor);
			}
			if (this.cursor != null)
			{
				g.DrawImage(this.cursor, targetRect.X, targetRect.Y);
			}
		}

		/// <summary>Draws the cursor in a stretched format on the specified surface, within the specified bounds.</summary>
		/// <param name="g">The <see cref="T:System.Drawing.Graphics" /> surface on which to draw the <see cref="T:System.Windows.Forms.Cursor" />. </param>
		/// <param name="targetRect">The <see cref="T:System.Drawing.Rectangle" /> that represents the bounds of the <see cref="T:System.Windows.Forms.Cursor" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B5E RID: 2910 RVA: 0x0002E2B0 File Offset: 0x0002C4B0
		public void DrawStretched(Graphics g, Rectangle targetRect)
		{
			if (this.cursor == null && this.std_cursor != (StdCursor)(-1))
			{
				this.cursor = XplatUI.DefineStdCursorBitmap(this.std_cursor);
			}
			if (this.cursor != null)
			{
				g.DrawImage(this.cursor, targetRect, new Rectangle(0, 0, this.cursor.Width, this.cursor.Height), 2);
			}
		}

		/// <summary>Returns a value indicating whether this cursor is equal to the specified <see cref="T:System.Windows.Forms.Cursor" />.</summary>
		/// <returns>true if this cursor is equal to the specified <see cref="T:System.Windows.Forms.Cursor" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Windows.Forms.Cursor" /> to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B5F RID: 2911 RVA: 0x0002E31C File Offset: 0x0002C51C
		public override bool Equals(object obj)
		{
			return obj is Cursor && ((Cursor)obj).handle == this.handle;
		}

		/// <summary>Retrieves the hash code for the current <see cref="T:System.Windows.Forms.Cursor" />.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Windows.Forms.Cursor" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B60 RID: 2912 RVA: 0x0002E34C File Offset: 0x0002C54C
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Retrieves a human readable string representing this <see cref="T:System.Windows.Forms.Cursor" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents this <see cref="T:System.Windows.Forms.Cursor" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B61 RID: 2913 RVA: 0x0002E354 File Offset: 0x0002C554
		public override string ToString()
		{
			if (this.name != null)
			{
				return "[Cursor:" + this.name + "]";
			}
			throw new FormatException("Cannot convert custom cursors to string.");
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0002E384 File Offset: 0x0002C584
		private void InitFromStream(Stream stream)
		{
			if (stream == null || stream.Length == 0L)
			{
				throw new ArgumentException("The argument 'stream' must be a picture that can be used as a cursor", "stream");
			}
			BinaryReader binaryReader = new BinaryReader(stream);
			this.cursor_dir = default(Cursor.CursorDir);
			this.cursor_dir.idReserved = binaryReader.ReadUInt16();
			this.cursor_dir.idType = binaryReader.ReadUInt16();
			if (this.cursor_dir.idReserved != 0 || (this.cursor_dir.idType != 2 && this.cursor_dir.idType != 1))
			{
				throw new ArgumentException("Invalid Argument, format error", "stream");
			}
			ushort num = binaryReader.ReadUInt16();
			this.cursor_dir.idCount = num;
			this.cursor_dir.idEntries = new Cursor.CursorEntry[(int)num];
			this.cursor_data = new Cursor.CursorImage[(int)num];
			for (int i = 0; i < (int)num; i++)
			{
				Cursor.CursorEntry cursorEntry = default(Cursor.CursorEntry);
				cursorEntry.width = binaryReader.ReadByte();
				cursorEntry.height = binaryReader.ReadByte();
				cursorEntry.colorCount = binaryReader.ReadByte();
				cursorEntry.reserved = binaryReader.ReadByte();
				cursorEntry.xHotspot = binaryReader.ReadUInt16();
				cursorEntry.yHotspot = binaryReader.ReadUInt16();
				if (this.cursor_dir.idType == 1)
				{
					cursorEntry.xHotspot = (ushort)(cursorEntry.width / 2);
					cursorEntry.yHotspot = (ushort)(cursorEntry.height / 2);
				}
				cursorEntry.sizeInBytes = binaryReader.ReadUInt32();
				cursorEntry.fileOffset = binaryReader.ReadUInt32();
				this.cursor_dir.idEntries[i] = cursorEntry;
			}
			uint num2 = 0U;
			for (int j = 0; j < (int)num; j++)
			{
				if (this.cursor_dir.idEntries[j].sizeInBytes >= num2)
				{
					num2 = this.cursor_dir.idEntries[j].sizeInBytes;
					this.id = (int)((ushort)j);
					this.size.Height = (int)this.cursor_dir.idEntries[j].height;
					this.size.Width = (int)this.cursor_dir.idEntries[j].width;
				}
			}
			for (int k = 0; k < (int)num; k++)
			{
				Cursor.CursorImage cursorImage = default(Cursor.CursorImage);
				Cursor.CursorInfoHeader cursorInfoHeader = default(Cursor.CursorInfoHeader);
				stream.Seek((long)((ulong)this.cursor_dir.idEntries[k].fileOffset), 0);
				byte[] array = new byte[this.cursor_dir.idEntries[k].sizeInBytes];
				stream.Read(array, 0, array.Length);
				BinaryReader binaryReader2 = new BinaryReader(new MemoryStream(array));
				cursorInfoHeader.biSize = binaryReader2.ReadUInt32();
				if (cursorInfoHeader.biSize != 40U)
				{
					throw new ArgumentException("Invalid cursor file", "stream");
				}
				cursorInfoHeader.biWidth = binaryReader2.ReadInt32();
				cursorInfoHeader.biHeight = binaryReader2.ReadInt32();
				cursorInfoHeader.biPlanes = binaryReader2.ReadUInt16();
				cursorInfoHeader.biBitCount = binaryReader2.ReadUInt16();
				cursorInfoHeader.biCompression = binaryReader2.ReadUInt32();
				cursorInfoHeader.biSizeImage = binaryReader2.ReadUInt32();
				cursorInfoHeader.biXPelsPerMeter = binaryReader2.ReadInt32();
				cursorInfoHeader.biYPelsPerMeter = binaryReader2.ReadInt32();
				cursorInfoHeader.biClrUsed = binaryReader2.ReadUInt32();
				cursorInfoHeader.biClrImportant = binaryReader2.ReadUInt32();
				cursorImage.cursorHeader = cursorInfoHeader;
				ushort biBitCount = cursorInfoHeader.biBitCount;
				int num3;
				switch (biBitCount)
				{
				case 1:
					num3 = 2;
					break;
				default:
					if (biBitCount != 8)
					{
						num3 = 0;
					}
					else
					{
						num3 = 256;
					}
					break;
				case 4:
					num3 = 16;
					break;
				}
				cursorImage.cursorColors = new uint[num3];
				for (int l = 0; l < num3; l++)
				{
					cursorImage.cursorColors[l] = binaryReader2.ReadUInt32();
				}
				int num4 = cursorInfoHeader.biHeight / 2;
				int num5 = cursorInfoHeader.biWidth * (int)cursorInfoHeader.biPlanes * (int)cursorInfoHeader.biBitCount + 31 >> 5 << 2;
				int num6 = num5 * num4;
				cursorImage.cursorXOR = new byte[num6];
				for (int m = 0; m < num6; m++)
				{
					cursorImage.cursorXOR[m] = binaryReader2.ReadByte();
				}
				int num7 = (int)(binaryReader2.BaseStream.Length - binaryReader2.BaseStream.Position);
				cursorImage.cursorAND = new byte[num7];
				for (int n = 0; n < num7; n++)
				{
					cursorImage.cursorAND[n] = binaryReader2.ReadByte();
				}
				this.cursor_data[k] = cursorImage;
				binaryReader2.Close();
			}
			binaryReader.Close();
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0002E870 File Offset: 0x0002CA70
		private Bitmap ToBitmap(bool xor, bool transparent)
		{
			if (this.cursor_data == null)
			{
				return new Bitmap(32, 32);
			}
			Cursor.CursorImage cursorImage = this.cursor_data[this.id];
			Cursor.CursorInfoHeader cursorHeader = cursorImage.cursorHeader;
			int num = cursorHeader.biHeight / 2;
			Bitmap bitmap;
			if (!xor)
			{
				bitmap = new Bitmap(cursorHeader.biWidth, num, 196865);
				ColorPalette colorPalette = bitmap.Palette;
				colorPalette.Entries[0] = Color.FromArgb(0, 0, 0);
				colorPalette.Entries[1] = Color.FromArgb(-1);
				BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), 2, bitmap.PixelFormat);
				for (int i = 0; i < num; i++)
				{
					Marshal.Copy(cursorImage.cursorAND, bitmapData.Stride * i, (IntPtr)(bitmapData.Scan0.ToInt64() + (long)(bitmapData.Stride * (num - 1 - i))), bitmapData.Stride);
				}
				bitmap.UnlockBits(bitmapData);
			}
			else
			{
				if (cursorHeader.biClrUsed == 0U && cursorHeader.biBitCount < 24)
				{
					int num2 = 1 << (int)cursorHeader.biBitCount;
				}
				ushort biBitCount = cursorHeader.biBitCount;
				switch (biBitCount)
				{
				case 1:
					bitmap = new Bitmap(cursorHeader.biWidth, num, 196865);
					break;
				default:
					if (biBitCount != 8)
					{
						if (biBitCount != 24 && biBitCount != 32)
						{
							throw new Exception("Unexpected number of bits:" + cursorHeader.biBitCount.ToString());
						}
						bitmap = new Bitmap(cursorHeader.biWidth, num, 2498570);
					}
					else
					{
						bitmap = new Bitmap(cursorHeader.biWidth, num, 198659);
					}
					break;
				case 4:
					bitmap = new Bitmap(cursorHeader.biWidth, num, 197634);
					break;
				}
				if (cursorHeader.biBitCount < 24)
				{
					ColorPalette colorPalette = bitmap.Palette;
					for (int j = 0; j < cursorImage.cursorColors.Length; j++)
					{
						colorPalette.Entries[j] = Color.FromArgb((int)(cursorImage.cursorColors[j] | 4278190080U));
					}
					bitmap.Palette = colorPalette;
				}
				int num3 = ((cursorHeader.biWidth * (int)cursorHeader.biBitCount + 31) & -32) >> 3;
				BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), 2, bitmap.PixelFormat);
				for (int k = 0; k < num; k++)
				{
					Marshal.Copy(cursorImage.cursorXOR, num3 * k, (IntPtr)(bitmapData.Scan0.ToInt64() + (long)(bitmapData.Stride * (num - 1 - k))), num3);
				}
				bitmap.UnlockBits(bitmapData);
			}
			if (transparent)
			{
				bitmap = new Bitmap(bitmap);
				for (int l = 0; l < num; l++)
				{
					for (int m = 0; m < cursorHeader.biWidth / 8; m++)
					{
						for (int n = 7; n >= 0; n--)
						{
							if (((cursorImage.cursorAND[l * cursorHeader.biWidth / 8 + m] >> n) & 1) != 0)
							{
								bitmap.SetPixel(m * 8 + 7 - n, num - l - 1, Color.Transparent);
							}
						}
					}
				}
			}
			return bitmap;
		}

		/// <summary>Returns a value indicating whether two instances of the <see cref="T:System.Windows.Forms.Cursor" /> class are not equal.</summary>
		/// <returns>true if two instances of the <see cref="T:System.Windows.Forms.Cursor" /> class are not equal; otherwise, false.</returns>
		/// <param name="left">A <see cref="T:System.Windows.Forms.Cursor" /> to compare. </param>
		/// <param name="right">A <see cref="T:System.Windows.Forms.Cursor" /> to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000B64 RID: 2916 RVA: 0x0002EC14 File Offset: 0x0002CE14
		public static bool operator !=(Cursor left, Cursor right)
		{
			return left != right && (left == null || right == null || !(left.handle == right.handle));
		}

		/// <summary>Returns a value indicating whether two instances of the <see cref="T:System.Windows.Forms.Cursor" /> class are equal.</summary>
		/// <returns>true if two instances of the <see cref="T:System.Windows.Forms.Cursor" /> class are equal; otherwise, false.</returns>
		/// <param name="left">A <see cref="T:System.Windows.Forms.Cursor" /> to compare. </param>
		/// <param name="right">A <see cref="T:System.Windows.Forms.Cursor" /> to compare. </param>
		/// <filterpriority>3</filterpriority>
		// Token: 0x06000B65 RID: 2917 RVA: 0x0002EC54 File Offset: 0x0002CE54
		public static bool operator ==(Cursor left, Cursor right)
		{
			return left == right || (left != null && right != null && left.handle == right.handle);
		}

		// Token: 0x0400086C RID: 2156
		private static Cursor current;

		// Token: 0x0400086D RID: 2157
		private Cursor.CursorDir cursor_dir;

		// Token: 0x0400086E RID: 2158
		private Cursor.CursorImage[] cursor_data;

		// Token: 0x0400086F RID: 2159
		private int id;

		// Token: 0x04000870 RID: 2160
		internal IntPtr handle;

		// Token: 0x04000871 RID: 2161
		private Size size;

		// Token: 0x04000872 RID: 2162
		private Bitmap shape;

		// Token: 0x04000873 RID: 2163
		private Bitmap mask;

		// Token: 0x04000874 RID: 2164
		private Bitmap cursor;

		// Token: 0x04000875 RID: 2165
		internal string name;

		// Token: 0x04000876 RID: 2166
		private StdCursor std_cursor;

		// Token: 0x04000877 RID: 2167
		private object tag;

		// Token: 0x020000B5 RID: 181
		private struct CursorDir
		{
			// Token: 0x04000878 RID: 2168
			internal ushort idReserved;

			// Token: 0x04000879 RID: 2169
			internal ushort idType;

			// Token: 0x0400087A RID: 2170
			internal ushort idCount;

			// Token: 0x0400087B RID: 2171
			internal Cursor.CursorEntry[] idEntries;
		}

		// Token: 0x020000B6 RID: 182
		private struct CursorEntry
		{
			// Token: 0x0400087C RID: 2172
			internal byte width;

			// Token: 0x0400087D RID: 2173
			internal byte height;

			// Token: 0x0400087E RID: 2174
			internal byte colorCount;

			// Token: 0x0400087F RID: 2175
			internal byte reserved;

			// Token: 0x04000880 RID: 2176
			internal ushort xHotspot;

			// Token: 0x04000881 RID: 2177
			internal ushort yHotspot;

			// Token: 0x04000882 RID: 2178
			internal ushort bitCount;

			// Token: 0x04000883 RID: 2179
			internal uint sizeInBytes;

			// Token: 0x04000884 RID: 2180
			internal uint fileOffset;
		}

		// Token: 0x020000B7 RID: 183
		private struct CursorInfoHeader
		{
			// Token: 0x04000885 RID: 2181
			internal uint biSize;

			// Token: 0x04000886 RID: 2182
			internal int biWidth;

			// Token: 0x04000887 RID: 2183
			internal int biHeight;

			// Token: 0x04000888 RID: 2184
			internal ushort biPlanes;

			// Token: 0x04000889 RID: 2185
			internal ushort biBitCount;

			// Token: 0x0400088A RID: 2186
			internal uint biCompression;

			// Token: 0x0400088B RID: 2187
			internal uint biSizeImage;

			// Token: 0x0400088C RID: 2188
			internal int biXPelsPerMeter;

			// Token: 0x0400088D RID: 2189
			internal int biYPelsPerMeter;

			// Token: 0x0400088E RID: 2190
			internal uint biClrUsed;

			// Token: 0x0400088F RID: 2191
			internal uint biClrImportant;
		}

		// Token: 0x020000B8 RID: 184
		private struct CursorImage
		{
			// Token: 0x04000890 RID: 2192
			internal Cursor.CursorInfoHeader cursorHeader;

			// Token: 0x04000891 RID: 2193
			internal uint[] cursorColors;

			// Token: 0x04000892 RID: 2194
			internal byte[] cursorXOR;

			// Token: 0x04000893 RID: 2195
			internal byte[] cursorAND;
		}
	}
}
