using System;

namespace System.Web.Security.AntiXss
{
	/// <summary>Specifies values for the upper-middle region of the UTF-8 Unicode code charts, from U2DE0 to UA8DF.</summary>
	// Token: 0x020004DA RID: 1242
	[Flags]
	public enum UpperMidCodeCharts : long
	{
		/// <summary>None of the UTF-8 Unicode code charts from the upper-middle region are marked as safe.</summary>
		// Token: 0x04001E9A RID: 7834
		None = 0L,
		/// <summary>The Cyrillic Extended-A code chart.</summary>
		// Token: 0x04001E9B RID: 7835
		CyrillicExtendedA = 1L,
		/// <summary>The Supplemental Punctuation code chart.</summary>
		// Token: 0x04001E9C RID: 7836
		SupplementalPunctuation = 2L,
		/// <summary>The CJK Radicals Supplement code chart.</summary>
		// Token: 0x04001E9D RID: 7837
		CjkRadicalsSupplement = 4L,
		/// <summary>The Kangxi Radicals code chart.</summary>
		// Token: 0x04001E9E RID: 7838
		KangxiRadicals = 8L,
		/// <summary>The Ideographic Description Characters code chart.</summary>
		// Token: 0x04001E9F RID: 7839
		IdeographicDescriptionCharacters = 16L,
		/// <summary>The CJK Symbols and Punctuation code chart.</summary>
		// Token: 0x04001EA0 RID: 7840
		CjkSymbolsAndPunctuation = 32L,
		/// <summary>The Hiragana code chart.</summary>
		// Token: 0x04001EA1 RID: 7841
		Hiragana = 64L,
		/// <summary>The Katakana code chart.</summary>
		// Token: 0x04001EA2 RID: 7842
		Katakana = 128L,
		/// <summary>The Bopomofo code chart.</summary>
		// Token: 0x04001EA3 RID: 7843
		Bopomofo = 256L,
		/// <summary>The Hangul Compatibility Jamo code chart.</summary>
		// Token: 0x04001EA4 RID: 7844
		HangulCompatibilityJamo = 512L,
		/// <summary>The Kanbun code chart.</summary>
		// Token: 0x04001EA5 RID: 7845
		Kanbun = 1024L,
		/// <summary>The Bopomofo Extended code chart.</summary>
		// Token: 0x04001EA6 RID: 7846
		BopomofoExtended = 2048L,
		/// <summary>The CJK Strokes code chart.</summary>
		// Token: 0x04001EA7 RID: 7847
		CjkStrokes = 4096L,
		/// <summary>The Katakana Phonetic Extensions code chart.</summary>
		// Token: 0x04001EA8 RID: 7848
		KatakanaPhoneticExtensions = 8192L,
		/// <summary>The Enclosed CJK Letters and Months code chart.</summary>
		// Token: 0x04001EA9 RID: 7849
		EnclosedCjkLettersAndMonths = 16384L,
		/// <summary>The CJK Compatibility code chart.</summary>
		// Token: 0x04001EAA RID: 7850
		CjkCompatibility = 32768L,
		/// <summary>The CJK Unified Ideographs Extension-A code chart.</summary>
		// Token: 0x04001EAB RID: 7851
		CjkUnifiedIdeographsExtensionA = 65536L,
		/// <summary>The Yijing Hexagram Symbols code chart.</summary>
		// Token: 0x04001EAC RID: 7852
		YijingHexagramSymbols = 131072L,
		/// <summary>The CJK Unified Ideographs code chart.</summary>
		// Token: 0x04001EAD RID: 7853
		CjkUnifiedIdeographs = 262144L,
		/// <summary>The Yi Syllables code chart.</summary>
		// Token: 0x04001EAE RID: 7854
		YiSyllables = 524288L,
		/// <summary>The Yi Radicals code chart.</summary>
		// Token: 0x04001EAF RID: 7855
		YiRadicals = 1048576L,
		/// <summary>The Lisu code chart.</summary>
		// Token: 0x04001EB0 RID: 7856
		Lisu = 2097152L,
		/// <summary>The Vai code chart.</summary>
		// Token: 0x04001EB1 RID: 7857
		Vai = 4194304L,
		/// <summary>The Cyrillic Extended-B code chart.</summary>
		// Token: 0x04001EB2 RID: 7858
		CyrillicExtendedB = 8388608L,
		/// <summary>The Bamum code chart.</summary>
		// Token: 0x04001EB3 RID: 7859
		Bamum = 16777216L,
		/// <summary>The Modifier Tone Letters code chart.</summary>
		// Token: 0x04001EB4 RID: 7860
		ModifierToneLetters = 33554432L,
		/// <summary>The Latin Extended-D code chart.</summary>
		// Token: 0x04001EB5 RID: 7861
		LatinExtendedD = 67108864L,
		/// <summary>The Syloti Nagri code chart.</summary>
		// Token: 0x04001EB6 RID: 7862
		SylotiNagri = 134217728L,
		/// <summary>The Common Indic Number Forms code chart.</summary>
		// Token: 0x04001EB7 RID: 7863
		CommonIndicNumberForms = 268435456L,
		/// <summary>The Phags-Pa code chart.</summary>
		// Token: 0x04001EB8 RID: 7864
		Phagspa = 536870912L,
		/// <summary>The Saurashtra code chart.</summary>
		// Token: 0x04001EB9 RID: 7865
		Saurashtra = 1073741824L
	}
}
