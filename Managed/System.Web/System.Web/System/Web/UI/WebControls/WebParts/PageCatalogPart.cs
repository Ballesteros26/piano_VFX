using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides a catalog that keeps references to all <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls (and other server controls contained in <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zones) that a user has closed on a single Web Parts page, which enables users to add the closed controls back to the page. This class cannot be inherited.</summary>
	// Token: 0x020007AE RID: 1966
	[Designer("System.Web.UI.Design.WebControls.WebParts.PageCatalogPartDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class PageCatalogPart : CatalogPart
	{
		/// <summary>Initializes a new instance of the class.</summary>
		// Token: 0x06004F58 RID: 20312 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public PageCatalogPart()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a collection of descriptions of the available <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a catalog.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescriptionCollection" /> that contains a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> for each closed <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control in a page catalog.</returns>
		// Token: 0x06004F59 RID: 20313 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override WebPartDescriptionCollection GetAvailableWebPartDescriptions()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Returns a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control based on the value of the description passed into the method.</summary>
		/// <returns>A reference to the actual instance of a closed <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control whose description matches <paramref name="description" />.</returns>
		/// <param name="description">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> that contains details about the control. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="description" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="description" /> is not an available <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> value.</exception>
		// Token: 0x06004F5A RID: 20314 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public override WebPart GetWebPart(WebPartDescription description)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
