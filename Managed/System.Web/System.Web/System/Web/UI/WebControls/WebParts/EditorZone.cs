using System;
using System.ComponentModel;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the primary control in the Web Parts control set for hosting <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls on a Web page.</summary>
	// Token: 0x020007AB RID: 1963
	[Designer("System.Web.UI.Design.WebControls.WebParts.EditorZoneDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SupportsEventValidation]
	public class EditorZone : EditorZoneBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.EditorZone" /> class.</summary>
		// Token: 0x06004F44 RID: 20292 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public EditorZone()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Provides a template to contain child controls within an <see cref="T:System.Web.UI.WebControls.WebParts.EditorZone" /> control in page persistence format.</summary>
		/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> zone template that acts as a container for child controls in the zone.</returns>
		// Token: 0x17001825 RID: 6181
		// (get) Token: 0x06004F45 RID: 20293 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004F46 RID: 20294 RVA: 0x0000B3E4 File Offset: 0x000095E4
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

		/// <summary>Creates all the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls declared within a zone template in page persistence format.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> that contains references to all the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls declared within the zone template.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control does not have an ID.</exception>
		// Token: 0x06004F47 RID: 20295 RVA: 0x0000E80B File Offset: 0x0000CA0B
		protected override EditorPartCollection CreateEditorParts()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
