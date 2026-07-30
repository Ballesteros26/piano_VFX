using System;
using System.Diagnostics.Tracing;

namespace System.Collections.Concurrent
{
	// Token: 0x020009F6 RID: 2550
	[EventSource(Name = "System.Collections.Concurrent.ConcurrentCollectionsEventSource", Guid = "35167F8E-49B2-4b96-AB86-435B59336B5E")]
	internal sealed class CDSCollectionETWBCLProvider : EventSource
	{
		// Token: 0x06005E9F RID: 24223 RVA: 0x0013772C File Offset: 0x0013592C
		private CDSCollectionETWBCLProvider()
		{
		}

		// Token: 0x06005EA0 RID: 24224 RVA: 0x00137734 File Offset: 0x00135934
		[Event(1, Level = EventLevel.Warning)]
		public void ConcurrentStack_FastPushFailed(int spinCount)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(1, spinCount);
			}
		}

		// Token: 0x06005EA1 RID: 24225 RVA: 0x00137749 File Offset: 0x00135949
		[Event(2, Level = EventLevel.Warning)]
		public void ConcurrentStack_FastPopFailed(int spinCount)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(2, spinCount);
			}
		}

		// Token: 0x06005EA2 RID: 24226 RVA: 0x0013775E File Offset: 0x0013595E
		[Event(3, Level = EventLevel.Warning)]
		public void ConcurrentDictionary_AcquiringAllLocks(int numOfBuckets)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(3, numOfBuckets);
			}
		}

		// Token: 0x06005EA3 RID: 24227 RVA: 0x00137773 File Offset: 0x00135973
		[Event(4, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryTakeSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(4);
			}
		}

		// Token: 0x06005EA4 RID: 24228 RVA: 0x00137787 File Offset: 0x00135987
		[Event(5, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryPeekSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(5);
			}
		}

		// Token: 0x04002FC7 RID: 12231
		public static CDSCollectionETWBCLProvider Log = new CDSCollectionETWBCLProvider();

		// Token: 0x04002FC8 RID: 12232
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x04002FC9 RID: 12233
		private const int CONCURRENTSTACK_FASTPUSHFAILED_ID = 1;

		// Token: 0x04002FCA RID: 12234
		private const int CONCURRENTSTACK_FASTPOPFAILED_ID = 2;

		// Token: 0x04002FCB RID: 12235
		private const int CONCURRENTDICTIONARY_ACQUIRINGALLLOCKS_ID = 3;

		// Token: 0x04002FCC RID: 12236
		private const int CONCURRENTBAG_TRYTAKESTEALS_ID = 4;

		// Token: 0x04002FCD RID: 12237
		private const int CONCURRENTBAG_TRYPEEKSTEALS_ID = 5;
	}
}
