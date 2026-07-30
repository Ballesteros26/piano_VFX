using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Imports a description file for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control (or other ASP.NET server control used as a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control), so that users can add the control to a Web page with pre-defined settings. This class cannot be inherited.</summary>
	// Token: 0x020007AC RID: 1964
	public sealed class ImportCatalogPart : CatalogPart
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.ImportCatalogPart" /> class. </summary>
		// Token: 0x06004F48 RID: 20296 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public ImportCatalogPart()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets a text message that instructs users to browse to the location of a description file.</summary>
		/// <returns>A string that contains the text of the message. The default value is a culture-specific message supplied by the .NET Framework.</returns>
		// Token: 0x17001826 RID: 6182
		// (get) Token: 0x06004F49 RID: 20297 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F4A RID: 20298 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string BrowseHelpText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets text displayed after a user imports a description file to represent or describe the imported control within the catalog of imported controls.</summary>
		/// <returns>A string that contains the text of the label. The default value is a culture-specific message supplied by the .NET Framework.</returns>
		// Token: 0x17001827 RID: 6183
		// (get) Token: 0x06004F4B RID: 20299 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F4C RID: 20300 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string ImportedPartLabelText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets an error message that is displayed if an error occurs during the import process.</summary>
		/// <returns>A string that contains the text of the label. The default value is a culture-specific message supplied by the .NET Framework.</returns>
		// Token: 0x17001828 RID: 6184
		// (get) Token: 0x06004F4D RID: 20301 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F4E RID: 20302 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string PartImportErrorLabelText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text for the <see cref="T:System.Web.UI.WebControls.Button" /> control that initiates the upload of a description file.</summary>
		/// <returns>A string that is used as the text for a <see cref="T:System.Web.UI.WebControls.Button" />. The default value is a culture-specific string supplied by the Web Parts control set.</returns>
		// Token: 0x17001829 RID: 6185
		// (get) Token: 0x06004F4F RID: 20303 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F50 RID: 20304 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string UploadButtonText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the text of the message that tells the user how to upload a description file.</summary>
		/// <returns>A string that is used as instructions for the user to upload a description file. The default value is a culture-specific string supplied by the Web Parts control set.</returns>
		// Token: 0x1700182A RID: 6186
		// (get) Token: 0x06004F51 RID: 20305 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F52 RID: 20306 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string UploadHelpText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Returns a collection of descriptions of the available <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a catalog.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescriptionCollection" /> that contains a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> for each control in a catalog.</returns>
		// Token: 0x06004F53 RID: 20307 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override WebPartDescriptionCollection GetAvailableWebPartDescriptions()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control based on the values in the description passed into the method.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control whose description matches the values in <paramref name="description" />.</returns>
		/// <param name="description">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> that contains details about the control. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="description" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="description" /> is not an available <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> value.</exception>
		// Token: 0x06004F54 RID: 20308 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override WebPart GetWebPart(WebPartDescription description)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
