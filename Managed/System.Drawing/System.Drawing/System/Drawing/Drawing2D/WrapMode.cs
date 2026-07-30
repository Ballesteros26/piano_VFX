using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies how a texture or gradient is tiled when it is smaller than the area being filled.</summary>
	// Token: 0x0200014E RID: 334
	public enum WrapMode
	{
		/// <summary>Tiles the gradient or texture.</summary>
		// Token: 0x04000B50 RID: 2896
		Tile,
		/// <summary>Reverses the texture or gradient horizontally and then tiles the texture or gradient.</summary>
		// Token: 0x04000B51 RID: 2897
		TileFlipX,
		/// <summary>Reverses the texture or gradient vertically and then tiles the texture or gradient.</summary>
		// Token: 0x04000B52 RID: 2898
		TileFlipY,
		/// <summary>Reverses the texture or gradient horizontally and vertically and then tiles the texture or gradient.</summary>
		// Token: 0x04000B53 RID: 2899
		TileFlipXY,
		/// <summary>The texture or gradient is not tiled.</summary>
		// Token: 0x04000B54 RID: 2900
		Clamp
	}
}
