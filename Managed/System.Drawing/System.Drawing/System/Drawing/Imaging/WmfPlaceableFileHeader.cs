using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Defines a placeable metafile. Not inheritable.</summary>
	// Token: 0x02000111 RID: 273
	[StructLayout(LayoutKind.Sequential)]
	public sealed class WmfPlaceableFileHeader
	{
		/// <summary>Gets or sets a value indicating the presence of a placeable metafile header.</summary>
		/// <returns>A value indicating presence of a placeable metafile header.</returns>
		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0001C764 File Offset: 0x0001A964
		// (set) Token: 0x06000CAF RID: 3247 RVA: 0x0001C76C File Offset: 0x0001A96C
		public int Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		/// <summary>Gets or sets the handle of the metafile in memory.</summary>
		/// <returns>The handle of the metafile in memory.</returns>
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0001C775 File Offset: 0x0001A975
		// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x0001C77D File Offset: 0x0001A97D
		public short Hmf
		{
			get
			{
				return this._hmf;
			}
			set
			{
				this._hmf = value;
			}
		}

		/// <summary>Gets or sets the x-coordinate of the upper-left corner of the bounding rectangle of the metafile image on the output device.</summary>
		/// <returns>The x-coordinate of the upper-left corner of the bounding rectangle of the metafile image on the output device.</returns>
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0001C786 File Offset: 0x0001A986
		// (set) Token: 0x06000CB3 RID: 3251 RVA: 0x0001C78E File Offset: 0x0001A98E
		public short BboxLeft
		{
			get
			{
				return this._bboxLeft;
			}
			set
			{
				this._bboxLeft = value;
			}
		}

		/// <summary>Gets or sets the y-coordinate of the upper-left corner of the bounding rectangle of the metafile image on the output device.</summary>
		/// <returns>The y-coordinate of the upper-left corner of the bounding rectangle of the metafile image on the output device.</returns>
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0001C797 File Offset: 0x0001A997
		// (set) Token: 0x06000CB5 RID: 3253 RVA: 0x0001C79F File Offset: 0x0001A99F
		public short BboxTop
		{
			get
			{
				return this._bboxTop;
			}
			set
			{
				this._bboxTop = value;
			}
		}

		/// <summary>Gets or sets the x-coordinate of the lower-right corner of the bounding rectangle of the metafile image on the output device.</summary>
		/// <returns>The x-coordinate of the lower-right corner of the bounding rectangle of the metafile image on the output device.</returns>
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0001C7A8 File Offset: 0x0001A9A8
		// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x0001C7B0 File Offset: 0x0001A9B0
		public short BboxRight
		{
			get
			{
				return this._bboxRight;
			}
			set
			{
				this._bboxRight = value;
			}
		}

		/// <summary>Gets or sets the y-coordinate of the lower-right corner of the bounding rectangle of the metafile image on the output device.</summary>
		/// <returns>The y-coordinate of the lower-right corner of the bounding rectangle of the metafile image on the output device.</returns>
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0001C7B9 File Offset: 0x0001A9B9
		// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x0001C7C1 File Offset: 0x0001A9C1
		public short BboxBottom
		{
			get
			{
				return this._bboxBottom;
			}
			set
			{
				this._bboxBottom = value;
			}
		}

		/// <summary>Gets or sets the number of twips per inch.</summary>
		/// <returns>The number of twips per inch.</returns>
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0001C7CA File Offset: 0x0001A9CA
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x0001C7D2 File Offset: 0x0001A9D2
		public short Inch
		{
			get
			{
				return this._inch;
			}
			set
			{
				this._inch = value;
			}
		}

		/// <summary>Reserved. Do not use.</summary>
		/// <returns>Reserved. Do not use.</returns>
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0001C7DB File Offset: 0x0001A9DB
		// (set) Token: 0x06000CBD RID: 3261 RVA: 0x0001C7E3 File Offset: 0x0001A9E3
		public int Reserved
		{
			get
			{
				return this._reserved;
			}
			set
			{
				this._reserved = value;
			}
		}

		/// <summary>Gets or sets the checksum value for the previous ten WORD s in the header.</summary>
		/// <returns>The checksum value for the previous ten WORD s in the header.</returns>
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0001C7EC File Offset: 0x0001A9EC
		// (set) Token: 0x06000CBF RID: 3263 RVA: 0x0001C7F4 File Offset: 0x0001A9F4
		public short Checksum
		{
			get
			{
				return this._checksum;
			}
			set
			{
				this._checksum = value;
			}
		}

		// Token: 0x04000A20 RID: 2592
		private int _key = -1698247209;

		// Token: 0x04000A21 RID: 2593
		private short _hmf;

		// Token: 0x04000A22 RID: 2594
		private short _bboxLeft;

		// Token: 0x04000A23 RID: 2595
		private short _bboxTop;

		// Token: 0x04000A24 RID: 2596
		private short _bboxRight;

		// Token: 0x04000A25 RID: 2597
		private short _bboxBottom;

		// Token: 0x04000A26 RID: 2598
		private short _inch;

		// Token: 0x04000A27 RID: 2599
		private int _reserved;

		// Token: 0x04000A28 RID: 2600
		private short _checksum;
	}
}
