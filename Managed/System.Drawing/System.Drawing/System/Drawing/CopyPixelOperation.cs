using System;
using System.Runtime.InteropServices;

namespace System.Drawing
{
	/// <summary>Determines how the source color in a copy pixel operation is combined with the destination color to result in a final color.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005E RID: 94
	[ComVisible(true)]
	public enum CopyPixelOperation
	{
		/// <summary>The destination area is filled by using the color associated with index 0 in the physical palette. (This color is black for the default physical palette.)</summary>
		// Token: 0x0400038C RID: 908
		Blackness = 66,
		/// <summary>Windows that are layered on top of your window are included in the resulting image. By default, the image contains only your window. Note that this generally cannot be used for printing device contexts.</summary>
		// Token: 0x0400038D RID: 909
		CaptureBlt = 1073741824,
		/// <summary>The destination area is inverted.</summary>
		// Token: 0x0400038E RID: 910
		DestinationInvert = 5570569,
		/// <summary>The colors of the source area are merged with the colors of the selected brush of the destination device context using the Boolean AND operator.</summary>
		// Token: 0x0400038F RID: 911
		MergeCopy = 12583114,
		/// <summary>The colors of the inverted source area are merged with the colors of the destination area by using the Boolean OR operator.</summary>
		// Token: 0x04000390 RID: 912
		MergePaint = 12255782,
		/// <summary>The bitmap is not mirrored.</summary>
		// Token: 0x04000391 RID: 913
		NoMirrorBitmap = -2147483648,
		/// <summary>The inverted source area is copied to the destination.</summary>
		// Token: 0x04000392 RID: 914
		NotSourceCopy = 3342344,
		/// <summary>The source and destination colors are combined using the Boolean OR operator, and then resultant color is then inverted.</summary>
		// Token: 0x04000393 RID: 915
		NotSourceErase = 1114278,
		/// <summary>The brush currently selected in the destination device context is copied to the destination bitmap.</summary>
		// Token: 0x04000394 RID: 916
		PatCopy = 15728673,
		/// <summary>The colors of the brush currently selected in the destination device context are combined with the colors of the destination are using the Boolean XOR operator.</summary>
		// Token: 0x04000395 RID: 917
		PatInvert = 5898313,
		/// <summary>The colors of the brush currently selected in the destination device context are combined with the colors of the inverted source area using the Boolean OR operator. The result of this operation is combined with the colors of the destination area using the Boolean OR operator.</summary>
		// Token: 0x04000396 RID: 918
		PatPaint = 16452105,
		/// <summary>The colors of the source and destination areas are combined using the Boolean AND operator.</summary>
		// Token: 0x04000397 RID: 919
		SourceAnd = 8913094,
		/// <summary>The source area is copied directly to the destination area.</summary>
		// Token: 0x04000398 RID: 920
		SourceCopy = 13369376,
		/// <summary>The inverted colors of the destination area are combined with the colors of the source area using the Boolean AND operator.</summary>
		// Token: 0x04000399 RID: 921
		SourceErase = 4457256,
		/// <summary>The colors of the source and destination areas are combined using the Boolean XOR operator.</summary>
		// Token: 0x0400039A RID: 922
		SourceInvert = 6684742,
		/// <summary>The colors of the source and destination areas are combined using the Boolean OR operator.</summary>
		// Token: 0x0400039B RID: 923
		SourcePaint = 15597702,
		/// <summary>The destination area is filled by using the color associated with index 1 in the physical palette. (This color is white for the default physical palette.)</summary>
		// Token: 0x0400039C RID: 924
		Whiteness = 16711778
	}
}
