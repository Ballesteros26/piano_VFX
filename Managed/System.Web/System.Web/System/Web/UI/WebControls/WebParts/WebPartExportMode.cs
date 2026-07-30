using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Specifies whether all, some, or none of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control's properties can be exported.</summary>
	// Token: 0x02000479 RID: 1145
	public enum WebPartExportMode
	{
		/// <summary>None of a Web Parts control's properties can be exported. </summary>
		// Token: 0x04001CF7 RID: 7415
		None,
		/// <summary>All of a Web Parts control's properties can be exported.</summary>
		// Token: 0x04001CF8 RID: 7416
		All,
		/// <summary>Only properties of a Web Parts control that have been defined as non-sensitive can be exported.  </summary>
		// Token: 0x04001CF9 RID: 7417
		NonSensitiveData
	}
}
