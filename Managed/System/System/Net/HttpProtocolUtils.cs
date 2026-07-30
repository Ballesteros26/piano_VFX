using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x02000456 RID: 1110
	internal class HttpProtocolUtils
	{
		// Token: 0x060020D0 RID: 8400 RVA: 0x000020EB File Offset: 0x000002EB
		private HttpProtocolUtils()
		{
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x0007F69C File Offset: 0x0007D89C
		internal static DateTime string2date(string S)
		{
			DateTime dateTime;
			if (HttpDateParse.ParseHttpDate(S, out dateTime))
			{
				return dateTime;
			}
			throw new ProtocolViolationException(global::SR.GetString("The value of the date string in the header is invalid."));
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x0007F6C4 File Offset: 0x0007D8C4
		internal static string date2string(DateTime D)
		{
			DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();
			return D.ToUniversalTime().ToString("R", dateTimeFormatInfo);
		}
	}
}
