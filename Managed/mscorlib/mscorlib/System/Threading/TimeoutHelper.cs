using System;

namespace System.Threading
{
	// Token: 0x02000462 RID: 1122
	internal static class TimeoutHelper
	{
		// Token: 0x0600358A RID: 13706 RVA: 0x000C61FF File Offset: 0x000C43FF
		public static uint GetTime()
		{
			return (uint)Environment.TickCount;
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x000C6208 File Offset: 0x000C4408
		public static int UpdateTimeOut(uint startTime, int originalWaitMillisecondsTimeout)
		{
			uint num = TimeoutHelper.GetTime() - startTime;
			if (num > 2147483647U)
			{
				return 0;
			}
			int num2 = originalWaitMillisecondsTimeout - (int)num;
			if (num2 <= 0)
			{
				return 0;
			}
			return num2;
		}
	}
}
