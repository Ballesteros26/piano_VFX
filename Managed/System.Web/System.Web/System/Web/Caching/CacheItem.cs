using System;

namespace System.Web.Caching
{
	// Token: 0x0200067F RID: 1663
	internal class CacheItem
	{
		// Token: 0x04002570 RID: 9584
		public object Value;

		// Token: 0x04002571 RID: 9585
		public string Key;

		// Token: 0x04002572 RID: 9586
		public CacheDependency Dependency;

		// Token: 0x04002573 RID: 9587
		public DateTime AbsoluteExpiration;

		// Token: 0x04002574 RID: 9588
		public TimeSpan SlidingExpiration;

		// Token: 0x04002575 RID: 9589
		public CacheItemPriority Priority;

		// Token: 0x04002576 RID: 9590
		public CacheItemRemovedCallback OnRemoveCallback;

		// Token: 0x04002577 RID: 9591
		public CacheItemUpdateCallback OnUpdateCallback;

		// Token: 0x04002578 RID: 9592
		public DateTime LastChange;

		// Token: 0x04002579 RID: 9593
		public long ExpiresAt;

		// Token: 0x0400257A RID: 9594
		public bool Disabled;

		// Token: 0x0400257B RID: 9595
		public bool IsTimedItem;

		// Token: 0x0400257C RID: 9596
		public int PriorityQueueIndex = -1;
	}
}
