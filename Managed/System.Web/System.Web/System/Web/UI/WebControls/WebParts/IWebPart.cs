using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines common user interface (UI) properties used by ASP.NET <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls. </summary>
	// Token: 0x02000466 RID: 1126
	public interface IWebPart
	{
		/// <summary>Gets or sets the URL to an image that represents a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control in a catalog of controls.</summary>
		/// <returns>A string that represents the URL to an image used to represent the control in a catalog. The default value is an empty string ("").</returns>
		// Token: 0x17001058 RID: 4184
		// (get) Token: 0x060033ED RID: 13293
		// (set) Token: 0x060033EE RID: 13294
		string CatalogIconImageUrl { get; set; }

		/// <summary>Gets or sets a brief phrase that summarizes what a control does, for use in ToolTips and catalogs of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
		/// <returns>A string that briefly summarizes the control's functionality. The default value is an empty string ("").</returns>
		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x060033EF RID: 13295
		// (set) Token: 0x060033F0 RID: 13296
		string Description { get; set; }

		/// <summary>Gets a string that is concatenated with the <see cref="P:System.Web.UI.WebControls.WebParts.IWebPart.Title" /> property value to form a complete title for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A string that serves as a subtitle for the control. The default value is an empty string ("").</returns>
		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x060033F1 RID: 13297
		string Subtitle { get; }

		/// <summary>Gets or sets the title of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A string that contains the title of the control. The default value is an empty string ("").</returns>
		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x060033F2 RID: 13298
		// (set) Token: 0x060033F3 RID: 13299
		string Title { get; set; }

		/// <summary>Gets or sets the URL to an image used to represent a Web Parts control in the control's own title bar.</summary>
		/// <returns>A string that represents the URL to an image. The default value is an empty string ("").</returns>
		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x060033F4 RID: 13300
		// (set) Token: 0x060033F5 RID: 13301
		string TitleIconImageUrl { get; set; }

		/// <summary>Gets or sets a URL to supplemental information about a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A string that represents a URL to more information about a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control. The default value is an empty string ("").</returns>
		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x060033F6 RID: 13302
		// (set) Token: 0x060033F7 RID: 13303
		string TitleUrl { get; set; }
	}
}
