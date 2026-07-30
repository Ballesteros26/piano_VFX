using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Contains information about a windows-format (WMF) metafile.</summary>
	// Token: 0x02000116 RID: 278
	[StructLayout(LayoutKind.Sequential)]
	public sealed class MetaHeader
	{
		/// <summary>Initializes a new instance of the MetaHeader class.</summary>
		// Token: 0x06000CE6 RID: 3302 RVA: 0x00002050 File Offset: 0x00000250
		public MetaHeader()
		{
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x0001CEA0 File Offset: 0x0001B0A0
		internal MetaHeader(WmfMetaHeader header)
		{
			this.wmf.file_type = header.file_type;
			this.wmf.header_size = header.header_size;
			this.wmf.version = header.version;
			this.wmf.file_size_low = header.file_size_low;
			this.wmf.file_size_high = header.file_size_high;
			this.wmf.num_of_objects = header.num_of_objects;
			this.wmf.max_record_size = header.max_record_size;
			this.wmf.num_of_params = header.num_of_params;
		}

		/// <summary>Gets or sets the size, in bytes, of the header file.</summary>
		/// <returns>The size, in bytes, of the header file.</returns>
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0001CF3B File Offset: 0x0001B13B
		// (set) Token: 0x06000CE9 RID: 3305 RVA: 0x0001CF48 File Offset: 0x0001B148
		public short HeaderSize
		{
			get
			{
				return this.wmf.header_size;
			}
			set
			{
				this.wmf.header_size = value;
			}
		}

		/// <summary>Gets or sets the size, in bytes, of the largest record in the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</summary>
		/// <returns>The size, in bytes, of the largest record in the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</returns>
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0001CF56 File Offset: 0x0001B156
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x0001CF63 File Offset: 0x0001B163
		public int MaxRecord
		{
			get
			{
				return this.wmf.max_record_size;
			}
			set
			{
				this.wmf.max_record_size = value;
			}
		}

		/// <summary>Gets or sets the maximum number of objects that exist in the <see cref="T:System.Drawing.Imaging.Metafile" /> object at the same time.</summary>
		/// <returns>The maximum number of objects that exist in the <see cref="T:System.Drawing.Imaging.Metafile" /> object at the same time.</returns>
		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0001CF71 File Offset: 0x0001B171
		// (set) Token: 0x06000CED RID: 3309 RVA: 0x0001CF7E File Offset: 0x0001B17E
		public short NoObjects
		{
			get
			{
				return this.wmf.num_of_objects;
			}
			set
			{
				this.wmf.num_of_objects = value;
			}
		}

		/// <summary>Not used. Always returns 0.</summary>
		/// <returns>Always 0.</returns>
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0001CF8C File Offset: 0x0001B18C
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x0001CF99 File Offset: 0x0001B199
		public short NoParameters
		{
			get
			{
				return this.wmf.num_of_params;
			}
			set
			{
				this.wmf.num_of_params = value;
			}
		}

		/// <summary>Gets or sets the size, in bytes, of the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</summary>
		/// <returns>The size, in bytes, of the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</returns>
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0001CFA7 File Offset: 0x0001B1A7
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x0001CFE8 File Offset: 0x0001B1E8
		public int Size
		{
			get
			{
				if (BitConverter.IsLittleEndian)
				{
					return ((int)this.wmf.file_size_high << 16) | (int)this.wmf.file_size_low;
				}
				return ((int)this.wmf.file_size_low << 16) | (int)this.wmf.file_size_high;
			}
			set
			{
				if (BitConverter.IsLittleEndian)
				{
					this.wmf.file_size_high = (ushort)(value >> 16);
					this.wmf.file_size_low = (ushort)value;
					return;
				}
				this.wmf.file_size_high = (ushort)value;
				this.wmf.file_size_low = (ushort)(value >> 16);
			}
		}

		/// <summary>Gets or sets the type of the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</summary>
		/// <returns>The type of the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</returns>
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0001D037 File Offset: 0x0001B237
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x0001D044 File Offset: 0x0001B244
		public short Type
		{
			get
			{
				return this.wmf.file_type;
			}
			set
			{
				this.wmf.file_type = value;
			}
		}

		/// <summary>Gets or sets the version number of the header format.</summary>
		/// <returns>The version number of the header format.</returns>
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0001D052 File Offset: 0x0001B252
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x0001D05F File Offset: 0x0001B25F
		public short Version
		{
			get
			{
				return this.wmf.version;
			}
			set
			{
				this.wmf.version = value;
			}
		}

		// Token: 0x04000A5D RID: 2653
		private WmfMetaHeader wmf;
	}
}
