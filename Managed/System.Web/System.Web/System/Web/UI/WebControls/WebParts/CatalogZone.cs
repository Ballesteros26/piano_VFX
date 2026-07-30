using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the primary control in the Web Parts control set for hosting <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls on a Web page.</summary>
	// Token: 0x020007A6 RID: 1958
	[SupportsEventValidation]
	[Designer("System.Web.UI.Design.WebControls.WebParts.CatalogZoneDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class CatalogZone : CatalogZoneBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZone" /> class.</summary>
		// Token: 0x06004EE5 RID: 20197 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public CatalogZone()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets a template to contain child controls within a <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZone" /> control in page persistence format.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that acts as a container for child controls in the zone.</returns>
		// Token: 0x170017F9 RID: 6137
		// (get) Token: 0x06004EE6 RID: 20198 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004EE7 RID: 20199 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual ITemplate ZoneTemplate
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

		/// <summary>Creates an instance of each <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> type that is declared in the zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPartCollection" /> with references to all the <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> controls in the zone.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Web.UI.WebControls.WebParts.CatalogPart" /> control does not have an ID.</exception>
		// Token: 0x06004EE8 RID: 20200 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override CatalogPartCollection CreateCatalogParts()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
