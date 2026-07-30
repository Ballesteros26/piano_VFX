using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the primary control in the Web Parts control set for hosting <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls on a Web page. </summary>
	// Token: 0x020007C1 RID: 1985
	[Designer("System.Web.UI.Design.WebControls.WebParts.WebPartZoneDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SupportsEventValidation]
	public class WebPartZone : WebPartZoneBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZone" /> class.</summary>
		// Token: 0x06004FF0 RID: 20464 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public WebPartZone()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets or sets a reference to an <see cref="T:System.Web.UI.ITemplate" /> instance that contains the controls declared in the markup of a Web page.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> that contains the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in a zone.</returns>
		/// <exception cref="T:System.InvalidOperationException">Registration of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls in the zone is already complete.</exception>
		// Token: 0x1700184B RID: 6219
		// (get) Token: 0x06004FF1 RID: 20465 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004FF2 RID: 20466 RVA: 0x0000B3E4 File Offset: 0x000095E4
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

		/// <summary>Overrides the abstract base method and gets the initial set of static <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls contained within the zone's template.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartCollection" /> that contains all the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> or other server controls contained in the zone's template. </returns>
		// Token: 0x06004FF3 RID: 20467 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected internal override WebPartCollection GetInitialWebParts()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
