using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the types of punctuation tables that can be used with the <see cref="T:System.Windows.Forms.RichTextBox" /> control's word-wrapping and word-breaking features.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002BF RID: 703
	public enum RichTextBoxWordPunctuations
	{
		/// <summary>Use pre-defined Level 1 punctuation table as default.</summary>
		// Token: 0x0400166C RID: 5740
		Level1 = 128,
		/// <summary>Use pre-defined Level 2 punctuation table as default.</summary>
		// Token: 0x0400166D RID: 5741
		Level2 = 256,
		/// <summary>Use a custom defined punctuation table.</summary>
		// Token: 0x0400166E RID: 5742
		Custom = 512,
		/// <summary>Used as a mask.</summary>
		// Token: 0x0400166F RID: 5743
		All = 896
	}
}
