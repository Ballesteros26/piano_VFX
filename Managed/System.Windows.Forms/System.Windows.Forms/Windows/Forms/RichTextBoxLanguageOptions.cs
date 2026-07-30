using System;

namespace System.Windows.Forms
{
	/// <summary>Provides <see cref="T:System.Windows.Forms.RichTextBox" /> settings for Input Method Editor (IME) and Asian language support.</summary>
	// Token: 0x020002BA RID: 698
	[Flags]
	public enum RichTextBoxLanguageOptions
	{
		/// <summary>Specifies that the <see cref="T:System.Windows.Forms.RichTextBox" /> control automatically changes the keyboard layout when the user explicitly changes to a different font, or when the user explicitly changes the insertion point to a new location in the text.  </summary>
		// Token: 0x0400164C RID: 5708
		AutoKeyboard = 1,
		/// <summary>Specifies that the control automatically changes fonts when the user explicitly changes to a different keyboard layout.</summary>
		// Token: 0x0400164D RID: 5709
		AutoFont = 2,
		/// <summary>Specifies how the control uses the composition string of an Input Method Editor (IME) if the user cancels it. If this flag is set, the control discards the composition string. If this flag is not set, the control uses the composition string as the result string.</summary>
		// Token: 0x0400164E RID: 5710
		ImeCancelComplete = 4,
		/// <summary>Specifies how the client is notified during IME composition. A setting of 0 specifies that no EN_CHANGE or EN_SELCHANGE events occur during an undetermined state. Notification is sent when the final string comes in. This is the default. A setting of 1 specifies that EN_CHANGE and EN_SELCHANGE events occur during an undetermined state.</summary>
		// Token: 0x0400164F RID: 5711
		ImeAlwaysSendNotify = 8,
		/// <summary>Specifies that font-bound font sizes are scaled from the insertion point size according to a script. For example, Asian fonts are slightly larger than Western fonts. This is the default.</summary>
		// Token: 0x04001650 RID: 5712
		AutoFontSizeAdjust = 16,
		/// <summary>Specifies that user-interface default fonts be used. This option is turned off by default.</summary>
		// Token: 0x04001651 RID: 5713
		UIFonts = 32,
		/// <summary>Sets the control to dual-font mode. Used for Asian language text. The <see cref="T:System.Windows.Forms.RichTextBox" /> control uses an English font for ASCII text and an Asian font for Asian text.</summary>
		// Token: 0x04001652 RID: 5714
		DualFont = 128
	}
}
