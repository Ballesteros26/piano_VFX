using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Specifies the kind of border that surrounds a Web Parts control.</summary>
	// Token: 0x0200046D RID: 1133
	public enum PartChromeType
	{
		/// <summary>A border setting inherited from the part control's containing zone.</summary>
		// Token: 0x04001CE4 RID: 7396
		Default,
		/// <summary>A title bar and a border.</summary>
		// Token: 0x04001CE5 RID: 7397
		TitleAndBorder,
		/// <summary>No border and no title bar.</summary>
		// Token: 0x04001CE6 RID: 7398
		None,
		/// <summary>A title bar only, without a border.</summary>
		// Token: 0x04001CE7 RID: 7399
		TitleOnly,
		/// <summary>A border only, without a title bar.</summary>
		// Token: 0x04001CE8 RID: 7400
		BorderOnly
	}
}
