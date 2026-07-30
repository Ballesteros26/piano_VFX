using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x0200053D RID: 1341
	internal class MonoHttpDate
	{
		// Token: 0x06002986 RID: 10630 RVA: 0x000A08C0 File Offset: 0x0009EAC0
		internal static DateTime Parse(string dateStr)
		{
			return DateTime.ParseExact(dateStr, MonoHttpDate.formats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces).ToLocalTime();
		}

		// Token: 0x04002298 RID: 8856
		private static readonly string rfc1123_date = "r";

		// Token: 0x04002299 RID: 8857
		private static readonly string rfc850_date = "dddd, dd-MMM-yy HH:mm:ss G\\MT";

		// Token: 0x0400229A RID: 8858
		private static readonly string asctime_date = "ddd MMM d HH:mm:ss yyyy";

		// Token: 0x0400229B RID: 8859
		private static readonly string[] formats = new string[]
		{
			MonoHttpDate.rfc1123_date,
			MonoHttpDate.rfc850_date,
			MonoHttpDate.asctime_date
		};
	}
}
