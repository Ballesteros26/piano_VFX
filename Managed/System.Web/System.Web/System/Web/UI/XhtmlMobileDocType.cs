using System;

namespace System.Web.UI
{
	/// <summary>Specifies the type of XHTML for the <see cref="T:System.Web.UI.XhtmlTextWriter" /> class to render to the page or control.</summary>
	// Token: 0x02000252 RID: 594
	public enum XhtmlMobileDocType
	{
		/// <summary>Specifies the XHTML Basic format. This format does not support frames and styles.</summary>
		// Token: 0x04001617 RID: 5655
		XhtmlBasic,
		/// <summary>Specifies the XHTML Mobile Profile format.</summary>
		// Token: 0x04001618 RID: 5656
		XhtmlMobileProfile,
		/// <summary>Specifies the WML 2.0 format.</summary>
		// Token: 0x04001619 RID: 5657
		Wml20
	}
}
