using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace System.Web.Caching
{
	// Token: 0x02000684 RID: 1668
	internal sealed class CacheItemPriorityQueue
	{
		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x06004746 RID: 18246 RVA: 0x000C86BD File Offset: 0x000C68BD
		public int Count
		{
			get
			{
				return this.heapCount;
			}
		}

		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x06004747 RID: 18247 RVA: 0x000C86C5 File Offset: 0x000C68C5
		public int Size
		{
			get
			{
				return this.heapSize;
			}
		}

		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x06004748 RID: 18248 RVA: 0x000C86CD File Offset: 0x000C68CD
		public CacheItem[] Heap
		{
			get
			{
				return this.heap;
			}
		}

		// Token: 0x06004749 RID: 18249 RVA: 0x000C86D5 File Offset: 0x000C68D5
		public CacheItemPriorityQueue()
		{
			this.queueLock = new ReaderWriterLockSlim();
		}

		// Token: 0x0600474A RID: 18250 RVA: 0x000C86E8 File Offset: 0x000C68E8
		private void ResizeHeap(int newSize)
		{
			CacheItem[] array = this.heap;
			Array.Resize<CacheItem>(ref this.heap, newSize);
			this.heapSize = newSize;
			if (array != null)
			{
				((IList)array).Clear();
			}
		}

		// Token: 0x0600474B RID: 18251 RVA: 0x000C871C File Offset: 0x000C691C
		private CacheItem[] GetHeapWithGrow()
		{
			if (this.heap == null)
			{
				this.heap = new CacheItem[32];
				this.heapSize = 32;
				this.heapCount = 0;
				return this.heap;
			}
			if (this.heapCount >= this.heapSize)
			{
				this.ResizeHeap(this.heapSize <<= 1);
			}
			return this.heap;
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x000C8780 File Offset: 0x000C6980
		private CacheItem[] GetHeapWithShrink()
		{
			if (this.heap == null)
			{
				return null;
			}
			if (this.heapSize > 8192)
			{
				int num = this.heapSize >> 1;
				if (this.heapCount < num)
				{
					this.ResizeHeap(num + this.heapCount / 3);
				}
			}
			return this.heap;
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x000C87CC File Offset: 0x000C69CC
		public void Enqueue(CacheItem item)
		{
			if (item == null)
			{
				return;
			}
			try
			{
				this.queueLock.EnterWriteLock();
				CacheItem[] heapWithGrow = this.GetHeapWithGrow();
				heapWithGrow[this.heapCount] = item;
				if (this.heapCount == 0)
				{
					item.PriorityQueueIndex = 0;
				}
				CacheItem[] array = heapWithGrow;
				int num = this.heapCount;
				this.heapCount = num + 1;
				this.BubbleUp(array, num);
			}
			finally
			{
				this.queueLock.ExitWriteLock();
			}
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x000C8840 File Offset: 0x000C6A40
		public CacheItem Dequeue()
		{
			CacheItem cacheItem;
			try
			{
				this.queueLock.EnterWriteLock();
				CacheItem[] heapWithShrink = this.GetHeapWithShrink();
				if (heapWithShrink == null || this.heapCount == 0)
				{
					cacheItem = null;
				}
				else
				{
					CacheItem cacheItem2 = heapWithShrink[0];
					int num = this.heapCount - 1;
					this.heapCount = num;
					int num2 = num;
					heapWithShrink[0] = heapWithShrink[num2];
					heapWithShrink[num2] = null;
					if (this.heapCount > 0)
					{
						this.BubbleDown(heapWithShrink, 0);
					}
					cacheItem = cacheItem2;
				}
			}
			finally
			{
				this.queueLock.ExitWriteLock();
			}
			return cacheItem;
		}

		// Token: 0x0600474F RID: 18255 RVA: 0x000C88C0 File Offset: 0x000C6AC0
		public bool Update(CacheItem item)
		{
			if (item == null || item.PriorityQueueIndex <= 0 || item.PriorityQueueIndex >= this.heapCount - 1)
			{
				return false;
			}
			try
			{
				this.queueLock.EnterWriteLock();
				CacheItem cacheItem = this.heap[item.PriorityQueueIndex];
				if (cacheItem == null || string.Compare(cacheItem.Key, item.Key, StringComparison.Ordinal) != 0)
				{
					return false;
				}
				int priorityQueueIndex = item.PriorityQueueIndex;
				int num = this.BubbleUp(this.heap, priorityQueueIndex);
				if (num > -1 && num >= priorityQueueIndex)
				{
					this.BubbleDown(this.heap, num);
				}
			}
			finally
			{
				this.queueLock.ExitWriteLock();
			}
			return true;
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x000C896C File Offset: 0x000C6B6C
		public CacheItem Peek()
		{
			CacheItem cacheItem;
			try
			{
				this.queueLock.EnterReadLock();
				if (this.heap == null || this.heapCount == 0)
				{
					cacheItem = null;
				}
				else
				{
					cacheItem = this.heap[0];
				}
			}
			finally
			{
				this.queueLock.ExitReadLock();
			}
			return cacheItem;
		}

		// Token: 0x06004751 RID: 18257 RVA: 0x000C89C0 File Offset: 0x000C6BC0
		private int BubbleDown(CacheItem[] heap, int startIndex)
		{
			int num = startIndex;
			int num2 = startIndex + 1;
			int num3 = startIndex + 2;
			CacheItem cacheItem = heap[num];
			int num4 = ((num3 < this.heapCount && heap[num3].ExpiresAt < heap[num2].ExpiresAt) ? 2 : 1);
			for (;;)
			{
				int num5 = num;
				num2 = (num << 1) + 1;
				num3 = num2 + 1;
				if (this.heapCount > num2 && heap[num].ExpiresAt > heap[num2].ExpiresAt)
				{
					num = num2;
				}
				if (this.heapCount > num3 && heap[num].ExpiresAt > heap[num3].ExpiresAt)
				{
					num = num3;
				}
				if (num == num5)
				{
					break;
				}
				CacheItem cacheItem2 = heap[num];
				heap[num] = heap[num5];
				heap[num].PriorityQueueIndex = num;
				heap[num5] = cacheItem2;
				cacheItem2.PriorityQueueIndex = num5;
			}
			cacheItem.PriorityQueueIndex = num;
			return num;
		}

		// Token: 0x06004752 RID: 18258 RVA: 0x000C8A78 File Offset: 0x000C6C78
		private int BubbleUp(CacheItem[] heap, int startIndex)
		{
			if (this.heapCount <= 1)
			{
				return -1;
			}
			int num = this.heapCount - 1;
			if (startIndex < 0 || startIndex > num)
			{
				return -1;
			}
			int i = startIndex;
			int num2 = i - 1 >> 1;
			CacheItem cacheItem = heap[i];
			while (i > 0)
			{
				CacheItem cacheItem2 = heap[num2];
				if (heap[i].ExpiresAt >= cacheItem2.ExpiresAt)
				{
					break;
				}
				heap[i] = cacheItem2;
				cacheItem2.PriorityQueueIndex = i;
				i = num2;
				num2 = i - 1 >> 1;
			}
			heap[i] = cacheItem;
			cacheItem.PriorityQueueIndex = i;
			return i;
		}

		// Token: 0x06004753 RID: 18259 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DEBUG")]
		private void InitDebugMode()
		{
		}

		// Token: 0x06004754 RID: 18260 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DEBUG")]
		private void AddSequenceEntry(CacheItem item, EDSequenceEntryType type)
		{
		}

		// Token: 0x06004755 RID: 18261 RVA: 0x0000393A File Offset: 0x00001B3A
		[Conditional("DEBUG")]
		public void OnItemDisable(CacheItem i)
		{
		}

		// Token: 0x0400258E RID: 9614
		private const int INITIAL_HEAP_SIZE = 32;

		// Token: 0x0400258F RID: 9615
		private const int HEAP_RESIZE_THRESHOLD = 8192;

		// Token: 0x04002590 RID: 9616
		private CacheItem[] heap;

		// Token: 0x04002591 RID: 9617
		private int heapSize;

		// Token: 0x04002592 RID: 9618
		private int heapCount;

		// Token: 0x04002593 RID: 9619
		private ReaderWriterLockSlim queueLock;
	}
}
