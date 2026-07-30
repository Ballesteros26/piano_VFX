using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the display behavior of error messages in validation controls.</summary>
	// Token: 0x02000327 RID: 807
	public enum ValidatorDisplay
	{
		/// <summary>Validator content never displayed inline.</summary>
		// Token: 0x040017D0 RID: 6096
		None,
		/// <summary>Validator content physically part of the page layout.</summary>
		// Token: 0x040017D1 RID: 6097
		Static,
		/// <summary>Validator content dynamically added to the page when validation fails.</summary>
		// Token: 0x040017D2 RID: 6098
		Dynamic
	}
}
