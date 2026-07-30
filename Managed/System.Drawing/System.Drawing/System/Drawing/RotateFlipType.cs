using System;

namespace System.Drawing
{
	/// <summary>Specifies how much an image is rotated and the axis used to flip the image.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000031 RID: 49
	public enum RotateFlipType
	{
		/// <summary>Specifies no clockwise rotation and no flipping.</summary>
		// Token: 0x04000284 RID: 644
		RotateNoneFlipNone,
		/// <summary>Specifies a 90-degree clockwise rotation without flipping.</summary>
		// Token: 0x04000285 RID: 645
		Rotate90FlipNone,
		/// <summary>Specifies a 180-degree clockwise rotation without flipping.</summary>
		// Token: 0x04000286 RID: 646
		Rotate180FlipNone,
		/// <summary>Specifies a 270-degree clockwise rotation without flipping.</summary>
		// Token: 0x04000287 RID: 647
		Rotate270FlipNone,
		/// <summary>Specifies no clockwise rotation followed by a horizontal flip.</summary>
		// Token: 0x04000288 RID: 648
		RotateNoneFlipX,
		/// <summary>Specifies a 90-degree clockwise rotation followed by a horizontal flip.</summary>
		// Token: 0x04000289 RID: 649
		Rotate90FlipX,
		/// <summary>Specifies a 180-degree clockwise rotation followed by a horizontal flip.</summary>
		// Token: 0x0400028A RID: 650
		Rotate180FlipX,
		/// <summary>Specifies a 270-degree clockwise rotation followed by a horizontal flip.</summary>
		// Token: 0x0400028B RID: 651
		Rotate270FlipX,
		/// <summary>Specifies no clockwise rotation followed by a vertical flip.</summary>
		// Token: 0x0400028C RID: 652
		RotateNoneFlipY = 6,
		/// <summary>Specifies a 90-degree clockwise rotation followed by a vertical flip.</summary>
		// Token: 0x0400028D RID: 653
		Rotate90FlipY,
		/// <summary>Specifies a 180-degree clockwise rotation followed by a vertical flip.</summary>
		// Token: 0x0400028E RID: 654
		Rotate180FlipY = 4,
		/// <summary>Specifies a 270-degree clockwise rotation followed by a vertical flip.</summary>
		// Token: 0x0400028F RID: 655
		Rotate270FlipY,
		/// <summary>Specifies no clockwise rotation followed by a horizontal and vertical flip.</summary>
		// Token: 0x04000290 RID: 656
		RotateNoneFlipXY = 2,
		/// <summary>Specifies a 90-degree clockwise rotation followed by a horizontal and vertical flip.</summary>
		// Token: 0x04000291 RID: 657
		Rotate90FlipXY,
		/// <summary>Specifies a 180-degree clockwise rotation followed by a horizontal and vertical flip.</summary>
		// Token: 0x04000292 RID: 658
		Rotate180FlipXY = 0,
		/// <summary>Specifies a 270-degree clockwise rotation followed by a horizontal and vertical flip.</summary>
		// Token: 0x04000293 RID: 659
		Rotate270FlipXY
	}
}
