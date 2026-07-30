using System;
using System.Collections;
using System.IO;
using System.Text;

namespace System.Resources
{
	// Token: 0x020002BA RID: 698
	internal class Win32VersionResource : Win32Resource
	{
		// Token: 0x06001FC3 RID: 8131 RVA: 0x0007CD54 File Offset: 0x0007AF54
		public Win32VersionResource(int id, int language, bool compilercontext)
			: base(Win32ResourceType.RT_VERSION, id, language)
		{
			this.signature = (long)((ulong)(-17890115));
			this.struct_version = 65536;
			this.file_flags_mask = 63;
			this.file_flags = 0;
			this.file_os = 4;
			this.file_type = 2;
			this.file_subtype = 0;
			this.file_date = 0L;
			this.file_lang = (compilercontext ? 0 : 127);
			this.file_codepage = 1200;
			this.properties = new Hashtable();
			string text = (compilercontext ? string.Empty : " ");
			foreach (string text2 in this.WellKnownProperties)
			{
				this.properties[text2] = text;
			}
			this.LegalCopyright = " ";
			this.FileDescription = " ";
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x0007CE6C File Offset: 0x0007B06C
		// (set) Token: 0x06001FC5 RID: 8133 RVA: 0x0007CEF4 File Offset: 0x0007B0F4
		public string Version
		{
			get
			{
				return string.Concat(new object[]
				{
					this.file_version >> 48,
					".",
					(this.file_version >> 32) & 65535L,
					".",
					(this.file_version >> 16) & 65535L,
					".",
					this.file_version & 65535L
				});
			}
			set
			{
				long[] array = new long[4];
				if (value != null)
				{
					string[] array2 = value.Split(new char[] { '.' });
					try
					{
						for (int i = 0; i < array2.Length; i++)
						{
							if (i < array.Length)
							{
								array[i] = (long)int.Parse(array2[i]);
							}
						}
					}
					catch (FormatException)
					{
					}
				}
				this.file_version = (array[0] << 48) | (array[1] << 32) | ((array[2] << 16) + array[3]);
				this.properties["FileVersion"] = this.Version;
			}
		}

		// Token: 0x17000460 RID: 1120
		public virtual string this[string key]
		{
			set
			{
				this.properties[key] = value;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001FC7 RID: 8135 RVA: 0x0007CF97 File Offset: 0x0007B197
		// (set) Token: 0x06001FC8 RID: 8136 RVA: 0x0007CFAE File Offset: 0x0007B1AE
		public virtual string Comments
		{
			get
			{
				return (string)this.properties["Comments"];
			}
			set
			{
				this.properties["Comments"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001FC9 RID: 8137 RVA: 0x0007CFD5 File Offset: 0x0007B1D5
		// (set) Token: 0x06001FCA RID: 8138 RVA: 0x0007CFEC File Offset: 0x0007B1EC
		public virtual string CompanyName
		{
			get
			{
				return (string)this.properties["CompanyName"];
			}
			set
			{
				this.properties["CompanyName"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001FCB RID: 8139 RVA: 0x0007D013 File Offset: 0x0007B213
		// (set) Token: 0x06001FCC RID: 8140 RVA: 0x0007D02A File Offset: 0x0007B22A
		public virtual string LegalCopyright
		{
			get
			{
				return (string)this.properties["LegalCopyright"];
			}
			set
			{
				this.properties["LegalCopyright"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x0007D051 File Offset: 0x0007B251
		// (set) Token: 0x06001FCE RID: 8142 RVA: 0x0007D068 File Offset: 0x0007B268
		public virtual string LegalTrademarks
		{
			get
			{
				return (string)this.properties["LegalTrademarks"];
			}
			set
			{
				this.properties["LegalTrademarks"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001FCF RID: 8143 RVA: 0x0007D08F File Offset: 0x0007B28F
		// (set) Token: 0x06001FD0 RID: 8144 RVA: 0x0007D0A6 File Offset: 0x0007B2A6
		public virtual string OriginalFilename
		{
			get
			{
				return (string)this.properties["OriginalFilename"];
			}
			set
			{
				this.properties["OriginalFilename"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001FD1 RID: 8145 RVA: 0x0007D0CD File Offset: 0x0007B2CD
		// (set) Token: 0x06001FD2 RID: 8146 RVA: 0x0007D0E4 File Offset: 0x0007B2E4
		public virtual string ProductName
		{
			get
			{
				return (string)this.properties["ProductName"];
			}
			set
			{
				this.properties["ProductName"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x0007D10B File Offset: 0x0007B30B
		// (set) Token: 0x06001FD4 RID: 8148 RVA: 0x0007D124 File Offset: 0x0007B324
		public virtual string ProductVersion
		{
			get
			{
				return (string)this.properties["ProductVersion"];
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					value = " ";
				}
				long[] array = new long[4];
				string[] array2 = value.Split(new char[] { '.' });
				try
				{
					for (int i = 0; i < array2.Length; i++)
					{
						if (i < array.Length)
						{
							array[i] = (long)int.Parse(array2[i]);
						}
					}
				}
				catch (FormatException)
				{
				}
				this.properties["ProductVersion"] = value;
				this.product_version = (array[0] << 48) | (array[1] << 32) | ((array[2] << 16) + array[3]);
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001FD5 RID: 8149 RVA: 0x0007D1C0 File Offset: 0x0007B3C0
		// (set) Token: 0x06001FD6 RID: 8150 RVA: 0x0007D1D7 File Offset: 0x0007B3D7
		public virtual string InternalName
		{
			get
			{
				return (string)this.properties["InternalName"];
			}
			set
			{
				this.properties["InternalName"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001FD7 RID: 8151 RVA: 0x0007D1FE File Offset: 0x0007B3FE
		// (set) Token: 0x06001FD8 RID: 8152 RVA: 0x0007D215 File Offset: 0x0007B415
		public virtual string FileDescription
		{
			get
			{
				return (string)this.properties["FileDescription"];
			}
			set
			{
				this.properties["FileDescription"] = ((value == string.Empty) ? " " : value);
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001FD9 RID: 8153 RVA: 0x0007D23C File Offset: 0x0007B43C
		// (set) Token: 0x06001FDA RID: 8154 RVA: 0x0007D244 File Offset: 0x0007B444
		public virtual int FileLanguage
		{
			get
			{
				return this.file_lang;
			}
			set
			{
				this.file_lang = value;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001FDB RID: 8155 RVA: 0x0007D24D File Offset: 0x0007B44D
		// (set) Token: 0x06001FDC RID: 8156 RVA: 0x0007D264 File Offset: 0x0007B464
		public virtual string FileVersion
		{
			get
			{
				return (string)this.properties["FileVersion"];
			}
			set
			{
				if (value == null || value.Length == 0)
				{
					value = " ";
				}
				long[] array = new long[4];
				string[] array2 = value.Split(new char[] { '.' });
				try
				{
					for (int i = 0; i < array2.Length; i++)
					{
						if (i < array.Length)
						{
							array[i] = (long)int.Parse(array2[i]);
						}
					}
				}
				catch (FormatException)
				{
				}
				this.properties["FileVersion"] = value;
				this.file_version = (array[0] << 48) | (array[1] << 32) | ((array[2] << 16) + array[3]);
			}
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x0007D300 File Offset: 0x0007B500
		private void emit_padding(BinaryWriter w)
		{
			if (w.BaseStream.Position % 4L != 0L)
			{
				w.Write(0);
			}
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x0007D31C File Offset: 0x0007B51C
		private void patch_length(BinaryWriter w, long len_pos)
		{
			Stream baseStream = w.BaseStream;
			long position = baseStream.Position;
			baseStream.Position = len_pos;
			w.Write((short)(position - len_pos));
			baseStream.Position = position;
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x0007D350 File Offset: 0x0007B550
		public override void WriteTo(Stream ms)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(ms, Encoding.Unicode))
			{
				binaryWriter.Write(0);
				binaryWriter.Write(52);
				binaryWriter.Write(0);
				binaryWriter.Write("VS_VERSION_INFO".ToCharArray());
				binaryWriter.Write(0);
				this.emit_padding(binaryWriter);
				binaryWriter.Write((uint)this.signature);
				binaryWriter.Write(this.struct_version);
				binaryWriter.Write((int)(this.file_version >> 32));
				binaryWriter.Write((int)(this.file_version & (long)((ulong)(-1))));
				binaryWriter.Write((int)(this.product_version >> 32));
				binaryWriter.Write((int)(this.product_version & (long)((ulong)(-1))));
				binaryWriter.Write(this.file_flags_mask);
				binaryWriter.Write(this.file_flags);
				binaryWriter.Write(this.file_os);
				binaryWriter.Write(this.file_type);
				binaryWriter.Write(this.file_subtype);
				binaryWriter.Write((int)(this.file_date >> 32));
				binaryWriter.Write((int)(this.file_date & (long)((ulong)(-1))));
				this.emit_padding(binaryWriter);
				long position = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write("VarFileInfo".ToCharArray());
				binaryWriter.Write(0);
				if (ms.Position % 4L != 0L)
				{
					binaryWriter.Write(0);
				}
				long position2 = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(4);
				binaryWriter.Write(0);
				binaryWriter.Write("Translation".ToCharArray());
				binaryWriter.Write(0);
				if (ms.Position % 4L != 0L)
				{
					binaryWriter.Write(0);
				}
				binaryWriter.Write((short)this.file_lang);
				binaryWriter.Write((short)this.file_codepage);
				this.patch_length(binaryWriter, position2);
				this.patch_length(binaryWriter, position);
				long position3 = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write("StringFileInfo".ToCharArray());
				this.emit_padding(binaryWriter);
				long position4 = ms.Position;
				binaryWriter.Write(0);
				binaryWriter.Write(0);
				binaryWriter.Write(1);
				binaryWriter.Write(string.Format("{0:x4}{1:x4}", this.file_lang, this.file_codepage).ToCharArray());
				this.emit_padding(binaryWriter);
				foreach (object obj in this.properties.Keys)
				{
					string text = (string)obj;
					string text2 = (string)this.properties[text];
					long position5 = ms.Position;
					binaryWriter.Write(0);
					binaryWriter.Write((short)(text2.ToCharArray().Length + 1));
					binaryWriter.Write(1);
					binaryWriter.Write(text.ToCharArray());
					binaryWriter.Write(0);
					this.emit_padding(binaryWriter);
					binaryWriter.Write(text2.ToCharArray());
					binaryWriter.Write(0);
					this.emit_padding(binaryWriter);
					this.patch_length(binaryWriter, position5);
				}
				this.patch_length(binaryWriter, position4);
				this.patch_length(binaryWriter, position3);
				this.patch_length(binaryWriter, 0L);
			}
		}

		// Token: 0x04001147 RID: 4423
		public string[] WellKnownProperties = new string[] { "Comments", "CompanyName", "FileVersion", "InternalName", "LegalTrademarks", "OriginalFilename", "ProductName", "ProductVersion" };

		// Token: 0x04001148 RID: 4424
		private long signature;

		// Token: 0x04001149 RID: 4425
		private int struct_version;

		// Token: 0x0400114A RID: 4426
		private long file_version;

		// Token: 0x0400114B RID: 4427
		private long product_version;

		// Token: 0x0400114C RID: 4428
		private int file_flags_mask;

		// Token: 0x0400114D RID: 4429
		private int file_flags;

		// Token: 0x0400114E RID: 4430
		private int file_os;

		// Token: 0x0400114F RID: 4431
		private int file_type;

		// Token: 0x04001150 RID: 4432
		private int file_subtype;

		// Token: 0x04001151 RID: 4433
		private long file_date;

		// Token: 0x04001152 RID: 4434
		private int file_lang;

		// Token: 0x04001153 RID: 4435
		private int file_codepage;

		// Token: 0x04001154 RID: 4436
		private Hashtable properties;
	}
}
