using System;

namespace System.Web.Security.AntiXss
{
	/// <summary>Specifies values for the lower-middle region of the UTF-8 Unicode code charts, from U1000 to U1EFF.</summary>
	// Token: 0x020004D8 RID: 1240
	[Flags]
	public enum LowerMidCodeCharts : long
	{
		/// <summary>None of the UTF-8 Unicode code charts from the lower-middle region are marked as safe.</summary>
		// Token: 0x04001E58 RID: 7768
		None = 0L,
		/// <summary>The Myanmar code chart.</summary>
		// Token: 0x04001E59 RID: 7769
		Myanmar = 1L,
		/// <summary>The Georgian code chart.</summary>
		// Token: 0x04001E5A RID: 7770
		Georgian = 2L,
		/// <summary>The Hangul Jamo code chart</summary>
		// Token: 0x04001E5B RID: 7771
		HangulJamo = 4L,
		/// <summary>The Ethiopic code chart.</summary>
		// Token: 0x04001E5C RID: 7772
		Ethiopic = 8L,
		/// <summary>The Ethiopic Supplement code chart.</summary>
		// Token: 0x04001E5D RID: 7773
		EthiopicSupplement = 16L,
		/// <summary>The Cherokee code chart.</summary>
		// Token: 0x04001E5E RID: 7774
		Cherokee = 32L,
		/// <summary>The Unified Canadian Aboriginal Syllabics code chart.</summary>
		// Token: 0x04001E5F RID: 7775
		UnifiedCanadianAboriginalSyllabics = 64L,
		/// <summary>The Ogham code chart.</summary>
		// Token: 0x04001E60 RID: 7776
		Ogham = 128L,
		/// <summary>The Runic code chart.</summary>
		// Token: 0x04001E61 RID: 7777
		Runic = 256L,
		/// <summary>The Tagalog code chart.</summary>
		// Token: 0x04001E62 RID: 7778
		Tagalog = 512L,
		/// <summary>The Hanunoo code chart.</summary>
		// Token: 0x04001E63 RID: 7779
		Hanunoo = 1024L,
		/// <summary>The Buhid code chart</summary>
		// Token: 0x04001E64 RID: 7780
		Buhid = 2048L,
		/// <summary>The Tagbanwa code chart.</summary>
		// Token: 0x04001E65 RID: 7781
		Tagbanwa = 4096L,
		/// <summary>The Khmer code chart.</summary>
		// Token: 0x04001E66 RID: 7782
		Khmer = 8192L,
		/// <summary>The Mongolian code chart.</summary>
		// Token: 0x04001E67 RID: 7783
		Mongolian = 16384L,
		/// <summary>The Unified Canadian Aboriginal Syllabics Extended code chart.</summary>
		// Token: 0x04001E68 RID: 7784
		UnifiedCanadianAboriginalSyllabicsExtended = 32768L,
		/// <summary>The Limbu code chart.</summary>
		// Token: 0x04001E69 RID: 7785
		Limbu = 65536L,
		/// <summary>The Tai Le code chart.</summary>
		// Token: 0x04001E6A RID: 7786
		TaiLe = 131072L,
		/// <summary>The New Tai Lue code chart.</summary>
		// Token: 0x04001E6B RID: 7787
		NewTaiLue = 262144L,
		/// <summary>The Khmer Symbols code chart.</summary>
		// Token: 0x04001E6C RID: 7788
		KhmerSymbols = 524288L,
		/// <summary>The Buginese code chart</summary>
		// Token: 0x04001E6D RID: 7789
		Buginese = 1048576L,
		/// <summary>The Tai Tham code chart.</summary>
		// Token: 0x04001E6E RID: 7790
		TaiTham = 2097152L,
		/// <summary>The Balinese code chart.</summary>
		// Token: 0x04001E6F RID: 7791
		Balinese = 4194304L,
		/// <summary>The Sudanese code chart.</summary>
		// Token: 0x04001E70 RID: 7792
		Sudanese = 8388608L,
		/// <summary>The Lepcha code chart.</summary>
		// Token: 0x04001E71 RID: 7793
		Lepcha = 16777216L,
		/// <summary>The Ol Chiki code chart.</summary>
		// Token: 0x04001E72 RID: 7794
		OlChiki = 33554432L,
		/// <summary>The Vedic Extensions code chart.</summary>
		// Token: 0x04001E73 RID: 7795
		VedicExtensions = 67108864L,
		/// <summary>The Phonetic Extensions code chart.</summary>
		// Token: 0x04001E74 RID: 7796
		PhoneticExtensions = 134217728L,
		/// <summary>The Phonetic Extensions Supplement code chart.</summary>
		// Token: 0x04001E75 RID: 7797
		PhoneticExtensionsSupplement = 268435456L,
		/// <summary>The Combining Diacritical Marks Supplement code chart.</summary>
		// Token: 0x04001E76 RID: 7798
		CombiningDiacriticalMarksSupplement = 536870912L,
		/// <summary>The Latin Extended Additional code chart.</summary>
		// Token: 0x04001E77 RID: 7799
		LatinExtendedAdditional = 1073741824L
	}
}
