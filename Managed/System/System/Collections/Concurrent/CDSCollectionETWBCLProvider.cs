using System;
using System.Diagnostics.Tracing;

namespace System.Collections.Concurrent
{
	// Token: 0x020006EC RID: 1772
	[EventSource(Name = "System.Collections.Concurrent.ConcurrentCollectionsEventSource", Guid = "35167F8E-49B2-4b96-AB86-435B59336B5E")]
	internal sealed class CDSCollectionETWBCLProvider : EventSource
	{
		// Token: 0x0600377D RID: 14205 RVA: 0x0007B9D5 File Offset: 0x00079BD5
		private CDSCollectionETWBCLProvider()
		{
		}

		// Token: 0x0600377E RID: 14206 RVA: 0x000CC5F9 File Offset: 0x000CA7F9
		[Event(1, Level = EventLevel.Warning)]
		public void ConcurrentStack_FastPushFailed(int spinCount)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(1, spinCount);
			}
		}

		// Token: 0x0600377F RID: 14207 RVA: 0x000CC60E File Offset: 0x000CA80E
		[Event(2, Level = EventLevel.Warning)]
		public void ConcurrentStack_FastPopFailed(int spinCount)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(2, spinCount);
			}
		}

		// Token: 0x06003780 RID: 14208 RVA: 0x000CC623 File Offset: 0x000CA823
		[Event(3, Level = EventLevel.Warning)]
		public void ConcurrentDictionary_AcquiringAllLocks(int numOfBuckets)
		{
			if (base.IsEnabled(EventLevel.Warning, EventKeywords.All))
			{
				base.WriteEvent(3, numOfBuckets);
			}
		}

		// Token: 0x06003781 RID: 14209 RVA: 0x000CC638 File Offset: 0x000CA838
		[Event(4, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryTakeSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(4);
			}
		}

		// Token: 0x06003782 RID: 14210 RVA: 0x000CC64C File Offset: 0x000CA84C
		[Event(5, Level = EventLevel.Verbose)]
		public void ConcurrentBag_TryPeekSteals()
		{
			if (base.IsEnabled(EventLevel.Verbose, EventKeywords.All))
			{
				base.WriteEvent(5);
			}
		}

		// Token: 0x04002C03 RID: 11267
		public static CDSCollectionETWBCLProvider Log = new CDSCollectionETWBCLProvider();

		// Token: 0x04002C04 RID: 11268
		private const EventKeywords ALL_KEYWORDS = EventKeywords.All;

		// Token: 0x04002C05 RID: 11269
		private const int CONCURRENTSTACK_FASTPUSHFAILED_ID = 1;

		// Token: 0x04002C06 RID: 11270
		private const int CONCURRENTSTACK_FASTPOPFAILED_ID = 2;

		// Token: 0x04002C07 RID: 11271
		private const int CONCURRENTDICTIONARY_ACQUIRINGALLLOCKS_ID = 3;

		// Token: 0x04002C08 RID: 11272
		private const int CONCURRENTBAG_TRYTAKESTEALS_ID = 4;

		// Token: 0x04002C09 RID: 11273
		private const int CONCURRENTBAG_TRYPEEKSTEALS_ID = 5;
	}
}
