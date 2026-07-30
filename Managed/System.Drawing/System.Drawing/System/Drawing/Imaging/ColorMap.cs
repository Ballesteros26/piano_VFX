using System;

namespace System.Drawing.Imaging
{
	/// <summary>Defines a map for converting colors. Several methods of the <see cref="T:System.Drawing.Imaging.ImageAttributes" /> class adjust image colors by using a color-remap table, which is an array of <see cref="T:System.Drawing.Imaging.ColorMap" /> structures. Not inheritable.</summary>
	// Token: 0x020000F4 RID: 244
	public sealed class ColorMap
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ColorMap" /> class.</summary>
		// Token: 0x06000BE2 RID: 3042 RVA: 0x0001A688 File Offset: 0x00018888
		public ColorMap()
		{
			this._oldColor = default(Color);
			this._newColor = default(Color);
		}

		/// <summary>Gets or sets the existing <see cref="T:System.Drawing.Color" /> structure to be converted.</summary>
		/// <returns>The existing <see cref="T:System.Drawing.Color" /> structure to be converted.</returns>
		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0001A6A8 File Offset: 0x000188A8
		// (set) Token: 0x06000BE4 RID: 3044 RVA: 0x0001A6B0 File Offset: 0x000188B0
		public Color OldColor
		{
			get
			{
				return this._oldColor;
			}
			set
			{
				this._oldColor = value;
			}
		}

		/// <summary>Gets or sets the new <see cref="T:System.Drawing.Color" /> structure to which to convert.</summary>
		/// <returns>The new <see cref="T:System.Drawing.Color" /> structure to which to convert.</returns>
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x0001A6B9 File Offset: 0x000188B9
		// (set) Token: 0x06000BE6 RID: 3046 RVA: 0x0001A6C1 File Offset: 0x000188C1
		public Color NewColor
		{
			get
			{
				return this._newColor;
			}
			set
			{
				this._newColor = value;
			}
		}

		// Token: 0x0400082D RID: 2093
		private Color _oldColor;

		// Token: 0x0400082E RID: 2094
		private Color _newColor;
	}
}
