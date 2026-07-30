using System;

namespace System.Windows.Forms
{
	/// <summary>Provides static, predefined <see cref="T:System.Windows.Forms.Clipboard" /> format names. Use them to identify the format of data that you store in an <see cref="T:System.Windows.Forms.IDataObject" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000BB RID: 187
	public class DataFormats
	{
		// Token: 0x06000B8A RID: 2954 RVA: 0x0002F5DC File Offset: 0x0002D7DC
		private DataFormats()
		{
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0002F6D0 File Offset: 0x0002D8D0
		internal static bool ContainsFormat(int id)
		{
			object obj = DataFormats.lock_object;
			bool flag;
			lock (obj)
			{
				if (!DataFormats.initialized)
				{
					DataFormats.Init();
				}
				flag = DataFormats.Format.Find(id) != null;
			}
			return flag;
		}

		/// <summary>Returns a <see cref="T:System.Windows.Forms.DataFormats.Format" /> with the Windows Clipboard numeric ID and name for the specified ID.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataFormats.Format" /> that has the Windows Clipboard numeric ID and the name of the format.</returns>
		/// <param name="id">The format ID. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B8D RID: 2957 RVA: 0x0002F734 File Offset: 0x0002D934
		public static DataFormats.Format GetFormat(int id)
		{
			object obj = DataFormats.lock_object;
			DataFormats.Format format;
			lock (obj)
			{
				if (!DataFormats.initialized)
				{
					DataFormats.Init();
				}
				format = DataFormats.Format.Find(id);
			}
			return format;
		}

		/// <summary>Returns a <see cref="T:System.Windows.Forms.DataFormats.Format" /> with the Windows Clipboard numeric ID and name for the specified format.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DataFormats.Format" /> that has the Windows Clipboard numeric ID and the name of the format.</returns>
		/// <param name="format">The format name. </param>
		/// <exception cref="T:System.ComponentModel.Win32Exception">Registering a new <see cref="T:System.Windows.Forms.Clipboard" /> format failed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B8E RID: 2958 RVA: 0x0002F794 File Offset: 0x0002D994
		public static DataFormats.Format GetFormat(string format)
		{
			object obj = DataFormats.lock_object;
			DataFormats.Format format2;
			lock (obj)
			{
				if (!DataFormats.initialized)
				{
					DataFormats.Init();
				}
				format2 = DataFormats.Format.Add(format);
			}
			return format2;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0002F7F4 File Offset: 0x0002D9F4
		private static void Init()
		{
			if (DataFormats.initialized)
			{
				return;
			}
			IntPtr intPtr = XplatUI.ClipboardOpen(false);
			new DataFormats.Format(DataFormats.Text, XplatUI.ClipboardGetID(intPtr, DataFormats.Text));
			new DataFormats.Format(DataFormats.Bitmap, XplatUI.ClipboardGetID(intPtr, DataFormats.Bitmap));
			new DataFormats.Format(DataFormats.MetafilePict, XplatUI.ClipboardGetID(intPtr, DataFormats.MetafilePict));
			new DataFormats.Format(DataFormats.SymbolicLink, XplatUI.ClipboardGetID(intPtr, DataFormats.SymbolicLink));
			new DataFormats.Format(DataFormats.Dif, XplatUI.ClipboardGetID(intPtr, DataFormats.Dif));
			new DataFormats.Format(DataFormats.Tiff, XplatUI.ClipboardGetID(intPtr, DataFormats.Tiff));
			new DataFormats.Format(DataFormats.OemText, XplatUI.ClipboardGetID(intPtr, DataFormats.OemText));
			new DataFormats.Format(DataFormats.Dib, XplatUI.ClipboardGetID(intPtr, DataFormats.Dib));
			new DataFormats.Format(DataFormats.Palette, XplatUI.ClipboardGetID(intPtr, DataFormats.Palette));
			new DataFormats.Format(DataFormats.PenData, XplatUI.ClipboardGetID(intPtr, DataFormats.PenData));
			new DataFormats.Format(DataFormats.Riff, XplatUI.ClipboardGetID(intPtr, DataFormats.Riff));
			new DataFormats.Format(DataFormats.WaveAudio, XplatUI.ClipboardGetID(intPtr, DataFormats.WaveAudio));
			new DataFormats.Format(DataFormats.UnicodeText, XplatUI.ClipboardGetID(intPtr, DataFormats.UnicodeText));
			new DataFormats.Format(DataFormats.EnhancedMetafile, XplatUI.ClipboardGetID(intPtr, DataFormats.EnhancedMetafile));
			new DataFormats.Format(DataFormats.FileDrop, XplatUI.ClipboardGetID(intPtr, DataFormats.FileDrop));
			new DataFormats.Format(DataFormats.Locale, XplatUI.ClipboardGetID(intPtr, DataFormats.Locale));
			new DataFormats.Format(DataFormats.CommaSeparatedValue, XplatUI.ClipboardGetID(intPtr, DataFormats.CommaSeparatedValue));
			new DataFormats.Format(DataFormats.Html, XplatUI.ClipboardGetID(intPtr, DataFormats.Html));
			new DataFormats.Format(DataFormats.Rtf, XplatUI.ClipboardGetID(intPtr, DataFormats.Rtf));
			new DataFormats.Format(DataFormats.Serializable, XplatUI.ClipboardGetID(intPtr, DataFormats.Serializable));
			new DataFormats.Format(DataFormats.StringFormat, XplatUI.ClipboardGetID(intPtr, DataFormats.StringFormat));
			XplatUI.ClipboardClose(intPtr);
			DataFormats.initialized = true;
		}

		/// <summary>Specifies a Windows bitmap format. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008B0 RID: 2224
		public static readonly string Bitmap = "Bitmap";

		/// <summary>Specifies a comma-separated value (CSV) format, which is a common interchange format used by spreadsheets. This format is not used directly by Windows Forms. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008B1 RID: 2225
		public static readonly string CommaSeparatedValue = "Csv";

		/// <summary>Specifies the Windows device-independent bitmap (DIB) format. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008B2 RID: 2226
		public static readonly string Dib = "DeviceIndependentBitmap";

		/// <summary>Specifies the Windows Data Interchange Format (DIF), which Windows Forms does not directly use. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008B3 RID: 2227
		public static readonly string Dif = "DataInterchangeFormat";

		/// <summary>Specifies the Windows enhanced metafile format. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008B4 RID: 2228
		public static readonly string EnhancedMetafile = "EnhancedMetafile";

		/// <summary>Specifies the Windows file drop format, which Windows Forms does not directly use. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008B5 RID: 2229
		public static readonly string FileDrop = "FileDrop";

		/// <summary>Specifies text in the HTML Clipboard format. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008B6 RID: 2230
		public static readonly string Html = "HTML Format";

		/// <summary>Specifies the Windows culture format, which Windows Forms does not directly use. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008B7 RID: 2231
		public static readonly string Locale = "Locale";

		/// <summary>Specifies the Windows metafile format, which Windows Forms does not directly use. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008B8 RID: 2232
		public static readonly string MetafilePict = "MetaFilePict";

		/// <summary>Specifies the standard Windows original equipment manufacturer (OEM) text format. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008B9 RID: 2233
		public static readonly string OemText = "OEMText";

		/// <summary>Specifies the Windows palette format. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008BA RID: 2234
		public static readonly string Palette = "Palette";

		/// <summary>Specifies the Windows pen data format, which consists of pen strokes for handwriting software; Windows Forms does not use this format. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008BB RID: 2235
		public static readonly string PenData = "PenData";

		/// <summary>Specifies the Resource Interchange File Format (RIFF) audio format, which Windows Forms does not directly use. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008BC RID: 2236
		public static readonly string Riff = "RiffAudio";

		/// <summary>Specifies text consisting of Rich Text Format (RTF) data. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008BD RID: 2237
		public static readonly string Rtf = "Rich Text Format";

		/// <summary>Specifies a format that encapsulates any type of Windows Forms object. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008BE RID: 2238
		public static readonly string Serializable = "WindowsForms10PersistentObject";

		/// <summary>Specifies the Windows Forms string class format, which Windows Forms uses to store string objects. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008BF RID: 2239
		public static readonly string StringFormat = "System.String";

		/// <summary>Specifies the Windows symbolic link format, which Windows Forms does not directly use. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008C0 RID: 2240
		public static readonly string SymbolicLink = "SymbolicLink";

		/// <summary>Specifies the standard ANSI text format. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008C1 RID: 2241
		public static readonly string Text = "Text";

		/// <summary>Specifies the Tagged Image File Format (TIFF), which Windows Forms does not directly use. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008C2 RID: 2242
		public static readonly string Tiff = "Tiff";

		/// <summary>Specifies the standard Windows Unicode text format. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008C3 RID: 2243
		public static readonly string UnicodeText = "UnicodeText";

		/// <summary>Specifies the wave audio format, which Windows Forms does not directly use. This static field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008C4 RID: 2244
		public static readonly string WaveAudio = "WaveAudio";

		// Token: 0x040008C5 RID: 2245
		private static object lock_object = new object();

		// Token: 0x040008C6 RID: 2246
		private static bool initialized;

		/// <summary>Represents a Clipboard format type.</summary>
		// Token: 0x020000BC RID: 188
		public class Format
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataFormats.Format" /> class with a Boolean that indicates whether a Win32 handle is expected.</summary>
			/// <param name="name">The name of this format. </param>
			/// <param name="id">The ID number for this format. </param>
			// Token: 0x06000B90 RID: 2960 RVA: 0x0002F9F0 File Offset: 0x0002DBF0
			public Format(string name, int id)
			{
				this.name = name;
				this.id = id;
				object obj = DataFormats.Format.lockobj;
				lock (obj)
				{
					if (DataFormats.Format.formats == null)
					{
						DataFormats.Format.formats = this;
					}
					else
					{
						DataFormats.Format format = DataFormats.Format.formats;
						while (format.next != null)
						{
							format = format.next;
						}
						format.next = this;
					}
				}
			}

			/// <summary>Gets the ID number for this format.</summary>
			/// <returns>The ID number for this format.</returns>
			// Token: 0x17000294 RID: 660
			// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0002FA8C File Offset: 0x0002DC8C
			public int Id
			{
				get
				{
					return this.id;
				}
			}

			/// <summary>Gets the name of this format.</summary>
			/// <returns>The name of this format.</returns>
			// Token: 0x17000295 RID: 661
			// (get) Token: 0x06000B93 RID: 2963 RVA: 0x0002FA94 File Offset: 0x0002DC94
			public string Name
			{
				get
				{
					return this.name;
				}
			}

			// Token: 0x17000296 RID: 662
			// (get) Token: 0x06000B94 RID: 2964 RVA: 0x0002FA9C File Offset: 0x0002DC9C
			internal DataFormats.Format Next
			{
				get
				{
					return this.next;
				}
			}

			// Token: 0x06000B95 RID: 2965 RVA: 0x0002FAA4 File Offset: 0x0002DCA4
			internal static DataFormats.Format Add(string name)
			{
				DataFormats.Format format = DataFormats.Format.Find(name);
				if (format == null)
				{
					IntPtr intPtr = XplatUI.ClipboardOpen(false);
					format = new DataFormats.Format(name, XplatUI.ClipboardGetID(intPtr, name));
					XplatUI.ClipboardClose(intPtr);
				}
				return format;
			}

			// Token: 0x06000B96 RID: 2966 RVA: 0x0002FADC File Offset: 0x0002DCDC
			internal static DataFormats.Format Add(int id)
			{
				DataFormats.Format format = DataFormats.Format.Find(id);
				if (format == null)
				{
					format = new DataFormats.Format("Format" + id.ToString(), id);
				}
				return format;
			}

			// Token: 0x06000B97 RID: 2967 RVA: 0x0002FB10 File Offset: 0x0002DD10
			internal static DataFormats.Format Find(int id)
			{
				DataFormats.Format format = DataFormats.Format.formats;
				while (format != null && format.Id != id)
				{
					format = format.next;
				}
				return format;
			}

			// Token: 0x06000B98 RID: 2968 RVA: 0x0002FB44 File Offset: 0x0002DD44
			internal static DataFormats.Format Find(string name)
			{
				DataFormats.Format format = DataFormats.Format.formats;
				while (format != null && !format.Name.Equals(name))
				{
					format = format.next;
				}
				return format;
			}

			// Token: 0x17000297 RID: 663
			// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0002FB7C File Offset: 0x0002DD7C
			internal static DataFormats.Format List
			{
				get
				{
					return DataFormats.Format.formats;
				}
			}

			// Token: 0x040008C7 RID: 2247
			private static readonly object lockobj = new object();

			// Token: 0x040008C8 RID: 2248
			private static DataFormats.Format formats;

			// Token: 0x040008C9 RID: 2249
			private string name;

			// Token: 0x040008CA RID: 2250
			private int id;

			// Token: 0x040008CB RID: 2251
			private DataFormats.Format next;

			// Token: 0x040008CC RID: 2252
			internal bool is_serializable;
		}
	}
}
