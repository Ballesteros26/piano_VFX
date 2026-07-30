using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides information about a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control that you can display in a catalog of Web Parts controls without having to create an instance of the control.</summary>
	// Token: 0x020007A5 RID: 1957
	public class WebPartDescription
	{
		/// <summary>Initializes a new instance of the class by using several strings that contain description information for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <param name="id">The value to assign to the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartDescription.ID" />. </param>
		/// <param name="title">The value to assign to the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartDescription.Title" />. </param>
		/// <param name="description">The value to assign to the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartDescription.Description" />.  </param>
		/// <param name="imageUrl">The value to assign to the <see cref="P:System.Web.UI.WebControls.WebParts.WebPartDescription.CatalogIconImageUrl" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="id" /> or <paramref name="title" /> is null or an empty string ("").</exception>
		// Token: 0x06004EDF RID: 20191 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebPartDescription(string id, string title, string description, string imageUrl)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the class when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control instance is available.</summary>
		/// <param name="part">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control whose information is contained in a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" />. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.Control.ID" /> property of <paramref name="part" /> is null or an empty string ("").</exception>
		// Token: 0x06004EE0 RID: 20192 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebPartDescription(WebPart part)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a URL containing the path to an image used as an icon for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A string that contains a URL; the default value is an empty string ("").</returns>
		// Token: 0x170017F5 RID: 6133
		// (get) Token: 0x06004EE1 RID: 20193 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string CatalogIconImageUrl
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the text of a description for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A string that contains the description for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control; the default value is an empty string ("").</returns>
		// Token: 0x170017F6 RID: 6134
		// (get) Token: 0x06004EE2 RID: 20194 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string Description
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the ID of a corresponding <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A string that contains the ID of the control.</returns>
		// Token: 0x170017F7 RID: 6135
		// (get) Token: 0x06004EE3 RID: 20195 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string ID
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the title text of a corresponding <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A string that contains the title of the corresponding control. The default value is a calculated string supplied by the .NET Framework.</returns>
		// Token: 0x170017F8 RID: 6136
		// (get) Token: 0x06004EE4 RID: 20196 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string Title
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}
	}
}
