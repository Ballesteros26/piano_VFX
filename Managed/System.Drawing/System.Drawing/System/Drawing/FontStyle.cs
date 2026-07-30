using System;

namespace System.Drawing
{
	/// <summary>Specifies style information applied to text.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000017 RID: 23
	[Flags]
	public enum FontStyle
	{
		/// <summary>Normal text.</summary>
		// Token: 0x0400009B RID: 155
		Regular = 0,
		/// <summary>Bold text.</summary>
		// Token: 0x0400009C RID: 156
		Bold = 1,
		/// <summary>Italic text.</summary>
		// Token: 0x0400009D RID: 157
		Italic = 2,
		/// <summary>Underlined text.</summary>
		// Token: 0x0400009E RID: 158
		Underline = 4,
		/// <summary>Text with a line through the middle.</summary>
		// Token: 0x0400009F RID: 159
		Strikeout = 8
	}
}
