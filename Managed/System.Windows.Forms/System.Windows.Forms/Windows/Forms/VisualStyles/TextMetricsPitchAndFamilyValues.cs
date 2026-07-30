using System;

namespace System.Windows.Forms.VisualStyles
{
	/// <summary>Specifies information about the pitch, technology, and family of the font specified by a visual style for a particular element. </summary>
	// Token: 0x0200052E RID: 1326
	[Flags]
	public enum TextMetricsPitchAndFamilyValues
	{
		/// <summary>If this value is set, the font is a variable pitch font. Otherwise, the font is a fixed-pitch font. Note that the behavior of this value is opposite of what the name implies.</summary>
		// Token: 0x04002C0B RID: 11275
		FixedPitch = 1,
		/// <summary>The font is a vector font.</summary>
		// Token: 0x04002C0C RID: 11276
		Vector = 2,
		/// <summary>The font is a TrueType font.</summary>
		// Token: 0x04002C0D RID: 11277
		TrueType = 4,
		/// <summary>The font is a device font.</summary>
		// Token: 0x04002C0E RID: 11278
		Device = 8
	}
}
