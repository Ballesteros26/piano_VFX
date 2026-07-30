using System;

namespace System.Drawing.Imaging
{
	/// <summary>Specifies the type of color data in the system palette. The data can be color data with alpha, grayscale data only, or halftone data.</summary>
	// Token: 0x0200010D RID: 269
	[Flags]
	public enum PaletteFlags
	{
		/// <summary>Alpha data.</summary>
		// Token: 0x04000A01 RID: 2561
		HasAlpha = 1,
		/// <summary>Grayscale data.</summary>
		// Token: 0x04000A02 RID: 2562
		GrayScale = 2,
		/// <summary>Halftone data.</summary>
		// Token: 0x04000A03 RID: 2563
		Halftone = 4
	}
}
