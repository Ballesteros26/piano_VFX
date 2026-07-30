using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Defines the different language versions of the Gregorian calendar.</summary>
	// Token: 0x02000412 RID: 1042
	[ComVisible(true)]
	[Serializable]
	public enum GregorianCalendarTypes
	{
		/// <summary>Refers to the localized version of the Gregorian calendar, based on the language of the <see cref="T:System.Globalization.CultureInfo" /> that uses the <see cref="T:System.Globalization.DateTimeFormatInfo" />.</summary>
		// Token: 0x04001A31 RID: 6705
		Localized = 1,
		/// <summary>Refers to the U.S. English version of the Gregorian calendar.</summary>
		// Token: 0x04001A32 RID: 6706
		USEnglish,
		/// <summary>Refers to the Middle East French version of the Gregorian calendar.</summary>
		// Token: 0x04001A33 RID: 6707
		MiddleEastFrench = 9,
		/// <summary>Refers to the Arabic version of the Gregorian calendar.</summary>
		// Token: 0x04001A34 RID: 6708
		Arabic,
		/// <summary>Refers to the transliterated English version of the Gregorian calendar.</summary>
		// Token: 0x04001A35 RID: 6709
		TransliteratedEnglish,
		/// <summary>Refers to the transliterated French version of the Gregorian calendar.</summary>
		// Token: 0x04001A36 RID: 6710
		TransliteratedFrench
	}
}
