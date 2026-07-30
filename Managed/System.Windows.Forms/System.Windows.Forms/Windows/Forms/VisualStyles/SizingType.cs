using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies how elements with a bitmap background will adjust to fill a bounds.</summary>
	// Token: 0x02000528 RID: 1320
	public enum SizingType
	{
		/// <summary>The element cannot be resized.</summary>
		// Token: 0x04002BD1 RID: 11217
		FixedSize,
		/// <summary>The background image stretches to fill the bounds.</summary>
		// Token: 0x04002BD2 RID: 11218
		Stretch,
		/// <summary>The background image repeats the pattern to fill the bounds.</summary>
		// Token: 0x04002BD3 RID: 11219
		Tile
	}
}
