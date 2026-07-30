using System;

namespace UnityEngine.XR
{
	// Token: 0x02000011 RID: 17
	internal static class TimeConverter
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00003740 File Offset: 0x00001940
		public static DateTime now
		{
			get
			{
				return DateTime.Now;
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003758 File Offset: 0x00001958
		public static long LocalDateTimeToUnixTimeMilliseconds(DateTime date)
		{
			return Convert.ToInt64((date.ToUniversalTime() - TimeConverter.s_Epoch).TotalMilliseconds);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003788 File Offset: 0x00001988
		public static DateTime UnixTimeMillisecondsToLocalDateTime(long unixTimeInMilliseconds)
		{
			return TimeConverter.s_Epoch.AddMilliseconds((double)unixTimeInMilliseconds).ToLocalTime();
		}

		// Token: 0x0400009C RID: 156
		private static readonly DateTime s_Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 1);
	}
}
