using System;
using System.Globalization;

namespace System.Web.Util
{
	// Token: 0x0200014C RID: 332
	internal sealed class TimeUtil
	{
		// Token: 0x06000EEF RID: 3823 RVA: 0x00002050 File Offset: 0x00000250
		private TimeUtil()
		{
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0002A89C File Offset: 0x00028A9C
		internal static string ToUtcTimeString(DateTime dt)
		{
			return dt.ToUniversalTime().ToString("R", DateTimeFormatInfo.InvariantInfo);
		}
	}
}
