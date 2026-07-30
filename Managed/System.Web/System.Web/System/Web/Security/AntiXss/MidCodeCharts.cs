using System;

namespace System.Web.Security.AntiXss
{
	/// <summary>Specifies values for the middle region of the UTF-8 Unicode code charts, from U1F00 to U2DDF.</summary>
	// Token: 0x020004D9 RID: 1241
	[Flags]
	public enum MidCodeCharts : long
	{
		/// <summary>None of the UTF-8 Unicode code charts from the middle region are marked as safe.</summary>
		// Token: 0x04001E79 RID: 7801
		None = 0L,
		/// <summary>The Greek Extended code chart.</summary>
		// Token: 0x04001E7A RID: 7802
		GreekExtended = 1L,
		/// <summary>The General Punctuation code chart.</summary>
		// Token: 0x04001E7B RID: 7803
		GeneralPunctuation = 2L,
		/// <summary>The Superscripts and Subscripts code chart.</summary>
		// Token: 0x04001E7C RID: 7804
		SuperscriptsAndSubscripts = 4L,
		/// <summary>The Currency Symbols code chart.</summary>
		// Token: 0x04001E7D RID: 7805
		CurrencySymbols = 8L,
		/// <summary>The Combining Diacritical Marks for Symbols code chart.</summary>
		// Token: 0x04001E7E RID: 7806
		CombiningDiacriticalMarksForSymbols = 16L,
		/// <summary>The Letterlike Symbols code chart.</summary>
		// Token: 0x04001E7F RID: 7807
		LetterlikeSymbols = 32L,
		/// <summary>The Number Forms code chart.</summary>
		// Token: 0x04001E80 RID: 7808
		NumberForms = 64L,
		/// <summary>The Arrows code chart.</summary>
		// Token: 0x04001E81 RID: 7809
		Arrows = 128L,
		/// <summary>The Mathematical Operators code chart.</summary>
		// Token: 0x04001E82 RID: 7810
		MathematicalOperators = 256L,
		/// <summary>The Miscellaneous Technical code chart.</summary>
		// Token: 0x04001E83 RID: 7811
		MiscellaneousTechnical = 512L,
		/// <summary>The Control Pictures code chart.</summary>
		// Token: 0x04001E84 RID: 7812
		ControlPictures = 1024L,
		/// <summary>The Optical Character Recognition code chart.</summary>
		// Token: 0x04001E85 RID: 7813
		OpticalCharacterRecognition = 2048L,
		/// <summary>The Enclosed Alphanumerics code chart.</summary>
		// Token: 0x04001E86 RID: 7814
		EnclosedAlphanumerics = 4096L,
		/// <summary>The Box Drawing code chart.</summary>
		// Token: 0x04001E87 RID: 7815
		BoxDrawing = 8192L,
		/// <summary>The Block Elements code chart.</summary>
		// Token: 0x04001E88 RID: 7816
		BlockElements = 16384L,
		/// <summary>The Geometric Shapes code chart.</summary>
		// Token: 0x04001E89 RID: 7817
		GeometricShapes = 32768L,
		/// <summary>The Miscellaneous Symbols code chart.</summary>
		// Token: 0x04001E8A RID: 7818
		MiscellaneousSymbols = 65536L,
		/// <summary>The Dingbats code chart.</summary>
		// Token: 0x04001E8B RID: 7819
		Dingbats = 131072L,
		/// <summary>The Miscellaneous Mathematical Symbols-A code chart.</summary>
		// Token: 0x04001E8C RID: 7820
		MiscellaneousMathematicalSymbolsA = 262144L,
		/// <summary>The Supplemental Arrows-A code chart.</summary>
		// Token: 0x04001E8D RID: 7821
		SupplementalArrowsA = 524288L,
		/// <summary>The Braille Patterns code chart.</summary>
		// Token: 0x04001E8E RID: 7822
		BraillePatterns = 1048576L,
		/// <summary>The Supplemental Arrows-B code chart.</summary>
		// Token: 0x04001E8F RID: 7823
		SupplementalArrowsB = 2097152L,
		/// <summary>The Miscellaneous Mathematical Symbols-B code chart.</summary>
		// Token: 0x04001E90 RID: 7824
		MiscellaneousMathematicalSymbolsB = 4194304L,
		/// <summary>The Supplemental Mathematical Operators code chart.</summary>
		// Token: 0x04001E91 RID: 7825
		SupplementalMathematicalOperators = 8388608L,
		/// <summary>The Miscellaneous Symbols and Arrows code chart.</summary>
		// Token: 0x04001E92 RID: 7826
		MiscellaneousSymbolsAndArrows = 16777216L,
		/// <summary>The Glagolitic code chart.</summary>
		// Token: 0x04001E93 RID: 7827
		Glagolitic = 33554432L,
		/// <summary>The Latin Extended-C code chart.</summary>
		// Token: 0x04001E94 RID: 7828
		LatinExtendedC = 67108864L,
		/// <summary>The Coptic code chart.</summary>
		// Token: 0x04001E95 RID: 7829
		Coptic = 134217728L,
		/// <summary>The Georgian Supplement code chart.</summary>
		// Token: 0x04001E96 RID: 7830
		GeorgianSupplement = 268435456L,
		/// <summary>The Tifinagh code chart.</summary>
		// Token: 0x04001E97 RID: 7831
		Tifinagh = 536870912L,
		/// <summary>The Ethiopic Extended code chart.</summary>
		// Token: 0x04001E98 RID: 7832
		EthiopicExtended = 16384L
	}
}
