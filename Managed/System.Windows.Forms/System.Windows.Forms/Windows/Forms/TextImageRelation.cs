using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the position of the text and image relative to each other on a control.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000323 RID: 803
	public enum TextImageRelation
	{
		/// <summary>Specifies that the image and text share the same space on a control.</summary>
		// Token: 0x0400196E RID: 6510
		Overlay,
		/// <summary>Specifies that the image is displayed vertically above the text of a control.</summary>
		// Token: 0x0400196F RID: 6511
		ImageAboveText,
		/// <summary>Specifies that the text is displayed vertically above the image of a control.</summary>
		// Token: 0x04001970 RID: 6512
		TextAboveImage,
		/// <summary>Specifies that the image is displayed horizontally before the text of a control.</summary>
		// Token: 0x04001971 RID: 6513
		ImageBeforeText = 4,
		/// <summary>Specifies that the text is displayed horizontally before the image of a control.</summary>
		// Token: 0x04001972 RID: 6514
		TextBeforeImage = 8
	}
}
