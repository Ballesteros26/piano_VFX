using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the alignment of an image in relation to the text of a Web page.</summary>
	// Token: 0x020002DA RID: 730
	public enum ImageAlign
	{
		/// <summary>The alignment is not set.</summary>
		// Token: 0x040016F8 RID: 5880
		NotSet,
		/// <summary>The image is aligned on the left edge of the Web page with text wrapping on the right.</summary>
		// Token: 0x040016F9 RID: 5881
		Left,
		/// <summary>The image is aligned on the right edge of the Web page with text wrapping on the left.</summary>
		// Token: 0x040016FA RID: 5882
		Right,
		/// <summary>The lower edge of the image is aligned with the lower edge of the first line of text.</summary>
		// Token: 0x040016FB RID: 5883
		Baseline,
		/// <summary>The upper edge of the image is aligned with the upper edge of the highest element on the same line.</summary>
		// Token: 0x040016FC RID: 5884
		Top,
		/// <summary>The middle of the image is aligned with the lower edge of the first line of text.</summary>
		// Token: 0x040016FD RID: 5885
		Middle,
		/// <summary>The lower edge of the image is aligned with the lower edge of the first line of text.</summary>
		// Token: 0x040016FE RID: 5886
		Bottom,
		/// <summary>The lower edge of the image is aligned with the lower edge of the largest element on the same line.</summary>
		// Token: 0x040016FF RID: 5887
		AbsBottom,
		/// <summary>The middle of the image is aligned with the middle of the largest element on the same line.</summary>
		// Token: 0x04001700 RID: 5888
		AbsMiddle,
		/// <summary>The upper edge of the image is aligned with the upper edge of the highest text on the same line.</summary>
		// Token: 0x04001701 RID: 5889
		TextTop
	}
}
