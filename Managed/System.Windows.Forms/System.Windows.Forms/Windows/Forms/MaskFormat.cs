using System;

namespace System.Windows.Forms
{
	/// <summary>Defines how to format the text inside of a <see cref="T:System.Windows.Forms.MaskedTextBox" />.</summary>
	// Token: 0x0200023D RID: 573
	public enum MaskFormat
	{
		/// <summary>Return only text input by the user. </summary>
		// Token: 0x040012EA RID: 4842
		ExcludePromptAndLiterals,
		/// <summary>Return text input by the user as well as any instances of the prompt character.</summary>
		// Token: 0x040012EB RID: 4843
		IncludePrompt,
		/// <summary>Return text input by the user as well as any literal characters defined in the mask.</summary>
		// Token: 0x040012EC RID: 4844
		IncludeLiterals,
		/// <summary>Return text input by the user as well as any literal characters defined in the mask and any instances of the prompt character. </summary>
		// Token: 0x040012ED RID: 4845
		IncludePromptAndLiterals
	}
}
