using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001EC RID: 492
	[VisibleToOtherModules]
	internal class SystemClock
	{
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x00023CE4 File Offset: 0x00021EE4
		public static DateTime now
		{
			get
			{
				return DateTime.Now;
			}
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x00023CFC File Offset: 0x00021EFC
		public static long ToUnixTimeMilliseconds(DateTime date)
		{
			return Convert.ToInt64((date.ToUniversalTime() - SystemClock.s_Epoch).TotalMilliseconds);
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00023D2C File Offset: 0x00021F2C
		public static long ToUnixTimeSeconds(DateTime date)
		{
			return Convert.ToInt64((date.ToUniversalTime() - SystemClock.s_Epoch).TotalSeconds);
		}

		// Token: 0x040006BF RID: 1727
		private static readonly DateTime s_Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 1);
	}
}
