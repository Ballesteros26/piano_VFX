using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the base class for controls that reside in <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZoneBase" /> zones, and that provide catalogs of available Web server controls (especially <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls) that users can add to a Web page. </summary>
	// Token: 0x020007A0 RID: 1952
	[Designer("System.Web.UI.Design.WebControls.WebParts.CatalogPartDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Bindable(false)]
	public abstract class CatalogPart : Part
	{
		/// <summary>Initializes the class for use by an inherited class instance. This constructor can only be called by an inherited class.</summary>
		// Token: 0x06004EA5 RID: 20133 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected CatalogPart()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a string that contains the actual current title of a <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> control.</summary>
		/// <returns>A string that represents the complete, visible title of the control. The default value is an empty string ("").</returns>
		// Token: 0x170017E3 RID: 6115
		// (get) Token: 0x06004EA6 RID: 20134 RVA: 0x0000E80B File Offset: 0x0000CA0B
		public string DisplayTitle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to the current instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> class.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> for the current Web page.</returns>
		// Token: 0x170017E4 RID: 6116
		// (get) Token: 0x06004EA7 RID: 20135 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected WebPartManager WebPartManager
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZoneBase" /> zone that contains a <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZoneBase" /> that corresponds to the zone that contains the control.</returns>
		// Token: 0x170017E5 RID: 6117
		// (get) Token: 0x06004EA8 RID: 20136 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected CatalogZoneBase Zone
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a collection of descriptions of the available <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a catalog.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescriptionCollection" /> that contains a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> for each control in a catalog. </returns>
		// Token: 0x06004EA9 RID: 20137
		public abstract WebPartDescriptionCollection GetAvailableWebPartDescriptions();

		/// <summary>Retrieves from a catalog the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control that is referenced by the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> object passed to the method.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> that is referenced by the object in the <paramref name="description" /> parameter.</returns>
		/// <param name="description">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDescription" /> that contains a reference to a specific <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />.</param>
		// Token: 0x06004EAA RID: 20138
		public abstract WebPart GetWebPart(WebPartDescription description);
	}
}
