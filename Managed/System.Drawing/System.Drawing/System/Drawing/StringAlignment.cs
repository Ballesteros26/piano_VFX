using System;

namespace System.Drawing
{
	/// <summary>Specifies the alignment of a text string relative to its layout rectangle.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000033 RID: 51
	public enum StringAlignment
	{
		/// <summary>Specifies the text be aligned near the layout. In a left-to-right layout, the near position is left. In a right-to-left layout, the near position is right.</summary>
		// Token: 0x04000297 RID: 663
		Near,
		/// <summary>Specifies that text is aligned in the center of the layout rectangle.</summary>
		// Token: 0x04000298 RID: 664
		Center,
		/// <summary>Specifies that text is aligned far from the origin position of the layout rectangle. In a left-to-right layout, the far position is right. In a right-to-left layout, the far position is left.</summary>
		// Token: 0x04000299 RID: 665
		Far
	}
}
