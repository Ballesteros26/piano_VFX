using System;

namespace System.Drawing.Drawing2D
{
	/// <summary>Specifies whether smoothing (antialiasing) is applied to lines and curves and the edges of filled areas.</summary>
	// Token: 0x0200014C RID: 332
	public enum SmoothingMode
	{
		/// <summary>Specifies an invalid mode.</summary>
		// Token: 0x04000B46 RID: 2886
		Invalid = -1,
		/// <summary>Specifies no antialiasing.</summary>
		// Token: 0x04000B47 RID: 2887
		Default,
		/// <summary>Specifies no antialiasing.</summary>
		// Token: 0x04000B48 RID: 2888
		HighSpeed,
		/// <summary>Specifies antialiased rendering.</summary>
		// Token: 0x04000B49 RID: 2889
		HighQuality,
		/// <summary>Specifies no antialiasing.</summary>
		// Token: 0x04000B4A RID: 2890
		None,
		/// <summary>Specifies antialiased rendering.</summary>
		// Token: 0x04000B4B RID: 2891
		AntiAlias
	}
}
