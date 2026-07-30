using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	/// <summary>Specifies a value that determines the Input Method Editor (IME) status of an object when the object is selected.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001DF RID: 479
	[ComVisible(true)]
	public enum ImeMode
	{
		/// <summary>None (Default).</summary>
		// Token: 0x04000FF0 RID: 4080
		NoControl,
		/// <summary>The IME is on. This value indicates that the IME is on and characters specific to Chinese or Japanese can be entered. This setting is valid for Japanese, Simplified Chinese, and Traditional Chinese IME only.</summary>
		// Token: 0x04000FF1 RID: 4081
		On,
		/// <summary>The IME is off. This mode indicates that the IME is off, meaning that the object behaves the same as English entry mode. This setting is valid for Japanese, Simplified Chinese, and Traditional Chinese IME only.</summary>
		// Token: 0x04000FF2 RID: 4082
		Off,
		/// <summary>The IME is disabled. With this setting, the users cannot turn the IME on from the keyboard, and the IME floating window is hidden.</summary>
		// Token: 0x04000FF3 RID: 4083
		Disable,
		/// <summary>Hiragana DBC. This setting is valid for the Japanese IME only.</summary>
		// Token: 0x04000FF4 RID: 4084
		Hiragana,
		/// <summary>Katakana DBC. This setting is valid for the Japanese IME only.</summary>
		// Token: 0x04000FF5 RID: 4085
		Katakana,
		/// <summary>Katakana SBC. This setting is valid for the Japanese IME only.</summary>
		// Token: 0x04000FF6 RID: 4086
		KatakanaHalf,
		/// <summary>Alphanumeric double-byte characters. This setting is valid for Korean and Japanese IME only.</summary>
		// Token: 0x04000FF7 RID: 4087
		AlphaFull,
		/// <summary>Alphanumeric single-byte characters(SBC). This setting is valid for Korean and Japanese IME only.</summary>
		// Token: 0x04000FF8 RID: 4088
		Alpha,
		/// <summary>Hangul DBC. This setting is valid for the Korean IME only.</summary>
		// Token: 0x04000FF9 RID: 4089
		HangulFull,
		/// <summary>Hangul SBC. This setting is valid for the Korean IME only.</summary>
		// Token: 0x04000FFA RID: 4090
		Hangul,
		/// <summary>Inherits the IME mode of the parent control.</summary>
		// Token: 0x04000FFB RID: 4091
		Inherit = -1,
		/// <summary>IME closed. This setting is valid for Chinese IME only.</summary>
		// Token: 0x04000FFC RID: 4092
		Close = 11,
		/// <summary>IME on HalfShape. This setting is valid for Chinese IME only.</summary>
		// Token: 0x04000FFD RID: 4093
		OnHalf
	}
}
