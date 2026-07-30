using System;

namespace System.Drawing.Printing
{
	/// <summary>Specifies print preview information for a single page. This class cannot be inherited.</summary>
	// Token: 0x020000BA RID: 186
	public sealed class PreviewPageInfo
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Printing.PreviewPageInfo" /> class.</summary>
		/// <param name="image">The image of the printed page. </param>
		/// <param name="physicalSize">The size of the printed page, in hundredths of an inch. </param>
		// Token: 0x06000A77 RID: 2679 RVA: 0x00016BC0 File Offset: 0x00014DC0
		public PreviewPageInfo(Image image, Size physicalSize)
		{
			this._image = image;
			this._physicalSize = physicalSize;
		}

		/// <summary>Gets the image of the printed page.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> representing the printed page.</returns>
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000A78 RID: 2680 RVA: 0x00016BE1 File Offset: 0x00014DE1
		public Image Image
		{
			get
			{
				return this._image;
			}
		}

		/// <summary>Gets the size of the printed page, in hundredths of an inch.</summary>
		/// <returns>A <see cref="T:System.Drawing.Size" /> that specifies the size of the printed page, in hundredths of an inch.</returns>
		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000A79 RID: 2681 RVA: 0x00016BE9 File Offset: 0x00014DE9
		public Size PhysicalSize
		{
			get
			{
				return this._physicalSize;
			}
		}

		// Token: 0x040006DC RID: 1756
		private Image _image;

		// Token: 0x040006DD RID: 1757
		private Size _physicalSize = Size.Empty;
	}
}
