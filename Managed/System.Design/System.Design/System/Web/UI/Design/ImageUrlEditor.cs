using System;

namespace System.Web.UI.Design
{
	/// <summary>Provides a user interface for selecting a URL that references an image.</summary>
	// Token: 0x0200009A RID: 154
	public class ImageUrlEditor : UrlEditor
	{
		/// <summary>Gets the caption to display on the selection dialog window.</summary>
		/// <returns>The caption to display on the selection dialog window.</returns>
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x000092A5 File Offset: 0x000074A5
		protected override string Caption
		{
			get
			{
				return "Select Image";
			}
		}

		/// <summary>Gets the file name filter string for the editor. This string is used to determine the items that appear in the file list of the dialog box.</summary>
		/// <returns>The filter string that can be used to filter the file list of the dialog box.</returns>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x000092AC File Offset: 0x000074AC
		protected override string Filter
		{
			get
			{
				return "Image Files(*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png)|*.gif;*.jpg;*.jpeg;*.bmp;*.wmf;*.png|All Files(*.*)|*.*|";
			}
		}
	}
}
