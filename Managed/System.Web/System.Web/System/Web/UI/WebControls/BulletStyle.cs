using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the bullet styles you can apply to list items in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control. </summary>
	// Token: 0x0200033D RID: 829
	public enum BulletStyle
	{
		/// <summary>The bullet style is not set. The browser that renders the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control will determine the bullet style to display.</summary>
		// Token: 0x04001828 RID: 6184
		NotSet,
		/// <summary>The bullet style is a number (1, 2, 3, ...).</summary>
		// Token: 0x04001829 RID: 6185
		Numbered,
		/// <summary>The bullet style is a lowercase letter (a, b, c, ...).</summary>
		// Token: 0x0400182A RID: 6186
		LowerAlpha,
		/// <summary>The bullet style is an uppercase letter (A, B, C, ...).</summary>
		// Token: 0x0400182B RID: 6187
		UpperAlpha,
		/// <summary>The bullet style is a lowercase Roman numeral (i, ii, iii, ...).</summary>
		// Token: 0x0400182C RID: 6188
		LowerRoman,
		/// <summary>The bullet style is an uppercase Roman numeral (I, II, III, ...).</summary>
		// Token: 0x0400182D RID: 6189
		UpperRoman,
		/// <summary>The bullet style is a filled circle shape.</summary>
		// Token: 0x0400182E RID: 6190
		Disc,
		/// <summary>The bullet style is an empty circle shape.</summary>
		// Token: 0x0400182F RID: 6191
		Circle,
		/// <summary>The bullet style is a filled square shape.</summary>
		// Token: 0x04001830 RID: 6192
		Square,
		/// <summary>The bullet style is a custom image.</summary>
		// Token: 0x04001831 RID: 6193
		CustomImage
	}
}
