using System;
using System.Globalization;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005D1 RID: 1489
	internal static class CharUtil
	{
		// Token: 0x06003AEE RID: 15086 RVA: 0x0014CE3C File Offset: 0x0014B03C
		public static bool IsAlphaNumeric(char ch)
		{
			int unicodeCategory = (int)char.GetUnicodeCategory(ch);
			return unicodeCategory <= 4 || (unicodeCategory <= 10 && unicodeCategory >= 8);
		}

		// Token: 0x06003AEF RID: 15087 RVA: 0x0014CE64 File Offset: 0x0014B064
		public static bool IsDecimalDigitOne(char ch)
		{
			return char.GetUnicodeCategory(ch -= '\u0001') == UnicodeCategory.DecimalDigitNumber && char.GetNumericValue(ch) == 0.0;
		}
	}
}
