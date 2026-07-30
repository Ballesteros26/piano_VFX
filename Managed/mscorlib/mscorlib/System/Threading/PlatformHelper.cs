using System;

namespace System.Threading
{
	// Token: 0x02000461 RID: 1121
	internal static class PlatformHelper
	{
		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x06003588 RID: 13704 RVA: 0x000C61B0 File Offset: 0x000C43B0
		internal static int ProcessorCount
		{
			get
			{
				int tickCount = Environment.TickCount;
				int num = PlatformHelper.s_processorCount;
				if (num == 0 || tickCount - PlatformHelper.s_lastProcessorCountRefreshTicks >= 30000)
				{
					num = (PlatformHelper.s_processorCount = Environment.ProcessorCount);
					PlatformHelper.s_lastProcessorCountRefreshTicks = tickCount;
				}
				return num;
			}
		}

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06003589 RID: 13705 RVA: 0x000C61F5 File Offset: 0x000C43F5
		internal static bool IsSingleProcessor
		{
			get
			{
				return PlatformHelper.ProcessorCount == 1;
			}
		}

		// Token: 0x04001C8A RID: 7306
		private const int PROCESSOR_COUNT_REFRESH_INTERVAL_MS = 30000;

		// Token: 0x04001C8B RID: 7307
		private static volatile int s_processorCount;

		// Token: 0x04001C8C RID: 7308
		private static volatile int s_lastProcessorCountRefreshTicks;
	}
}
