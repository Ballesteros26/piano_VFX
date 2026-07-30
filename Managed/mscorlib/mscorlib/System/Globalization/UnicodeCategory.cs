using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Defines the Unicode category of a character.</summary>
	// Token: 0x0200043A RID: 1082
	[ComVisible(true)]
	[Serializable]
	public enum UnicodeCategory
	{
		/// <summary>Uppercase letter. Signified by the Unicode designation "Lu" (letter, uppercase). The value is 0.</summary>
		// Token: 0x04001B76 RID: 7030
		UppercaseLetter,
		/// <summary>Lowercase letter. Signified by the Unicode designation "Ll" (letter, lowercase). The value is 1.</summary>
		// Token: 0x04001B77 RID: 7031
		LowercaseLetter,
		/// <summary>Titlecase letter. Signified by the Unicode designation "Lt" (letter, titlecase). The value is 2.</summary>
		// Token: 0x04001B78 RID: 7032
		TitlecaseLetter,
		/// <summary>Modifier letter character, which is free-standing spacing character that indicates modifications of a preceding letter. Signified by the Unicode designation "Lm" (letter, modifier). The value is 3.</summary>
		// Token: 0x04001B79 RID: 7033
		ModifierLetter,
		/// <summary>Letter that is not an uppercase letter, a lowercase letter, a titlecase letter, or a modifier letter. Signified by the Unicode designation "Lo" (letter, other). The value is 4.</summary>
		// Token: 0x04001B7A RID: 7034
		OtherLetter,
		/// <summary>Nonspacing character that indicates modifications of a base character. Signified by the Unicode designation "Mn" (mark, nonspacing). The value is 5.</summary>
		// Token: 0x04001B7B RID: 7035
		NonSpacingMark,
		/// <summary>Spacing character that indicates modifications of a base character and affects the width of the glyph for that base character. Signified by the Unicode designation "Mc" (mark, spacing combining). The value is 6.</summary>
		// Token: 0x04001B7C RID: 7036
		SpacingCombiningMark,
		/// <summary>Enclosing mark character, which is a nonspacing combining character that surrounds all previous characters up to and including a base character. Signified by the Unicode designation "Me" (mark, enclosing). The value is 7.</summary>
		// Token: 0x04001B7D RID: 7037
		EnclosingMark,
		/// <summary>Decimal digit character, that is, a character in the range 0 through 9. Signified by the Unicode designation "Nd" (number, decimal digit). The value is 8.</summary>
		// Token: 0x04001B7E RID: 7038
		DecimalDigitNumber,
		/// <summary>Number represented by a letter, instead of a decimal digit, for example, the Roman numeral for five, which is "V". The indicator is signified by the Unicode designation "Nl" (number, letter). The value is 9.</summary>
		// Token: 0x04001B7F RID: 7039
		LetterNumber,
		/// <summary>Number that is neither a decimal digit nor a letter number, for example, the fraction 1/2. The indicator is signified by the Unicode designation "No" (number, other). The value is 10.</summary>
		// Token: 0x04001B80 RID: 7040
		OtherNumber,
		/// <summary>Space character, which has no glyph but is not a control or format character. Signified by the Unicode designation "Zs" (separator, space). The value is 11.</summary>
		// Token: 0x04001B81 RID: 7041
		SpaceSeparator,
		/// <summary>Character that is used to separate lines of text. Signified by the Unicode designation "Zl" (separator, line). The value is 12.</summary>
		// Token: 0x04001B82 RID: 7042
		LineSeparator,
		/// <summary>Character used to separate paragraphs. Signified by the Unicode designation "Zp" (separator, paragraph). The value is 13.</summary>
		// Token: 0x04001B83 RID: 7043
		ParagraphSeparator,
		/// <summary>Control code character, with a Unicode value of U+007F or in the range U+0000 through U+001F or U+0080 through U+009F. Signified by the Unicode designation "Cc" (other, control). The value is 14.</summary>
		// Token: 0x04001B84 RID: 7044
		Control,
		/// <summary>Format character that affects the layout of text or the operation of text processes, but is not normally rendered. Signified by the Unicode designation "Cf" (other, format). The value is 15.</summary>
		// Token: 0x04001B85 RID: 7045
		Format,
		/// <summary>High surrogate or a low surrogate character. Surrogate code values are in the range U+D800 through U+DFFF. Signified by the Unicode designation "Cs" (other, surrogate). The value is 16.</summary>
		// Token: 0x04001B86 RID: 7046
		Surrogate,
		/// <summary>Private-use character, with a Unicode value in the range U+E000 through U+F8FF. Signified by the Unicode designation "Co" (other, private use). The value is 17.</summary>
		// Token: 0x04001B87 RID: 7047
		PrivateUse,
		/// <summary>Connector punctuation character that connects two characters. Signified by the Unicode designation "Pc" (punctuation, connector). The value is 18.</summary>
		// Token: 0x04001B88 RID: 7048
		ConnectorPunctuation,
		/// <summary>Dash or hyphen character. Signified by the Unicode designation "Pd" (punctuation, dash). The value is 19.</summary>
		// Token: 0x04001B89 RID: 7049
		DashPunctuation,
		/// <summary>Opening character of one of the paired punctuation marks, such as parentheses, square brackets, and braces. Signified by the Unicode designation "Ps" (punctuation, open). The value is 20.</summary>
		// Token: 0x04001B8A RID: 7050
		OpenPunctuation,
		/// <summary>Closing character of one of the paired punctuation marks, such as parentheses, square brackets, and braces. Signified by the Unicode designation "Pe" (punctuation, close). The value is 21.</summary>
		// Token: 0x04001B8B RID: 7051
		ClosePunctuation,
		/// <summary>Opening or initial quotation mark character. Signified by the Unicode designation "Pi" (punctuation, initial quote). The value is 22.</summary>
		// Token: 0x04001B8C RID: 7052
		InitialQuotePunctuation,
		/// <summary>Closing or final quotation mark character. Signified by the Unicode designation "Pf" (punctuation, final quote). The value is 23.</summary>
		// Token: 0x04001B8D RID: 7053
		FinalQuotePunctuation,
		/// <summary>Punctuation character that is not a connector, a dash, open punctuation, close punctuation, an initial quote, or a final quote. Signified by the Unicode designation "Po" (punctuation, other). The value is 24.</summary>
		// Token: 0x04001B8E RID: 7054
		OtherPunctuation,
		/// <summary>Mathematical symbol character, such as "+" or "= ". Signified by the Unicode designation "Sm" (symbol, math). The value is 25.</summary>
		// Token: 0x04001B8F RID: 7055
		MathSymbol,
		/// <summary>Currency symbol character. Signified by the Unicode designation "Sc" (symbol, currency). The value is 26.</summary>
		// Token: 0x04001B90 RID: 7056
		CurrencySymbol,
		/// <summary>Modifier symbol character, which indicates modifications of surrounding characters. For example, the fraction slash indicates that the number to the left is the numerator and the number to the right is the denominator. The indicator is signified by the Unicode designation "Sk" (symbol, modifier). The value is 27.</summary>
		// Token: 0x04001B91 RID: 7057
		ModifierSymbol,
		/// <summary>Symbol character that is not a mathematical symbol, a currency symbol or a modifier symbol. Signified by the Unicode designation "So" (symbol, other). The value is 28.</summary>
		// Token: 0x04001B92 RID: 7058
		OtherSymbol,
		/// <summary>Character that is not assigned to any Unicode category. Signified by the Unicode designation "Cn" (other, not assigned). The value is 29.</summary>
		// Token: 0x04001B93 RID: 7059
		OtherNotAssigned
	}
}
