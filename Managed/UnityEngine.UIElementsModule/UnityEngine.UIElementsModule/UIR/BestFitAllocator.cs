using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000245 RID: 581
	internal class BestFitAllocator
	{
		// Token: 0x0600113B RID: 4411 RVA: 0x00047B14 File Offset: 0x00045D14
		public BestFitAllocator(uint size)
		{
			this.totalSize = size;
			this.m_FirstBlock = (this.m_FirstAvailableBlock = this.m_BlockPool.Get());
			this.m_FirstAvailableBlock.end = size;
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600113C RID: 4412 RVA: 0x00047B61 File Offset: 0x00045D61
		public uint totalSize { get; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600113D RID: 4413 RVA: 0x00047B6C File Offset: 0x00045D6C
		public uint highWatermark
		{
			get
			{
				return this.m_HighWatermark;
			}
		}

		// Token: 0x0600113E RID: 4414 RVA: 0x00047B84 File Offset: 0x00045D84
		public Alloc Allocate(uint size)
		{
			BestFitAllocator.Block block = this.BestFitFindAvailableBlock(size);
			bool flag = block == null;
			Alloc alloc;
			if (flag)
			{
				alloc = default(Alloc);
			}
			else
			{
				Debug.Assert(block.size >= size);
				Debug.Assert(!block.allocated);
				bool flag2 = size != block.size;
				if (flag2)
				{
					this.SplitBlock(block, size);
				}
				Debug.Assert(block.size == size);
				bool flag3 = block.end > this.m_HighWatermark;
				if (flag3)
				{
					this.m_HighWatermark = block.end;
				}
				bool flag4 = block == this.m_FirstAvailableBlock;
				if (flag4)
				{
					this.m_FirstAvailableBlock = this.m_FirstAvailableBlock.nextAvailable;
				}
				bool flag5 = block.prevAvailable != null;
				if (flag5)
				{
					block.prevAvailable.nextAvailable = block.nextAvailable;
				}
				bool flag6 = block.nextAvailable != null;
				if (flag6)
				{
					block.nextAvailable.prevAvailable = block.prevAvailable;
				}
				block.allocated = true;
				block.prevAvailable = (block.nextAvailable = null);
				alloc = new Alloc
				{
					start = block.start,
					size = block.size,
					handle = block
				};
			}
			return alloc;
		}

		// Token: 0x0600113F RID: 4415 RVA: 0x00047CC4 File Offset: 0x00045EC4
		public void Free(Alloc alloc)
		{
			BestFitAllocator.Block block = (BestFitAllocator.Block)alloc.handle;
			bool flag = !block.allocated;
			if (flag)
			{
				Debug.Assert(false, "Severe error: UIR allocation double-free");
			}
			else
			{
				Debug.Assert(block.allocated);
				Debug.Assert(block.start == alloc.start);
				Debug.Assert(block.size == alloc.size);
				bool flag2 = block.end == this.m_HighWatermark;
				if (flag2)
				{
					bool flag3 = block.prev != null;
					if (flag3)
					{
						this.m_HighWatermark = (block.prev.allocated ? block.prev.end : block.prev.start);
					}
					else
					{
						this.m_HighWatermark = 0U;
					}
				}
				block.allocated = false;
				BestFitAllocator.Block block2 = this.m_FirstAvailableBlock;
				BestFitAllocator.Block block3 = null;
				while (block2 != null && block2.start < block.start)
				{
					block3 = block2;
					block2 = block2.nextAvailable;
				}
				bool flag4 = block3 == null;
				if (flag4)
				{
					Debug.Assert(block.prevAvailable == null);
					block.nextAvailable = this.m_FirstAvailableBlock;
					this.m_FirstAvailableBlock = block;
				}
				else
				{
					block.prevAvailable = block3;
					block.nextAvailable = block3.nextAvailable;
					block3.nextAvailable = block;
				}
				bool flag5 = block.nextAvailable != null;
				if (flag5)
				{
					block.nextAvailable.prevAvailable = block;
				}
				bool flag6 = block.prevAvailable == block.prev && block.prev != null;
				if (flag6)
				{
					block = this.CoalesceBlockWithPrevious(block);
				}
				bool flag7 = block.nextAvailable == block.next && block.next != null;
				if (flag7)
				{
					block = this.CoalesceBlockWithPrevious(block.next);
				}
			}
		}

		// Token: 0x06001140 RID: 4416 RVA: 0x00047E80 File Offset: 0x00046080
		private BestFitAllocator.Block CoalesceBlockWithPrevious(BestFitAllocator.Block block)
		{
			Debug.Assert(block.prevAvailable.end == block.start);
			Debug.Assert(block.prev.nextAvailable == block);
			BestFitAllocator.Block prev = block.prev;
			prev.next = block.next;
			bool flag = block.next != null;
			if (flag)
			{
				block.next.prev = prev;
			}
			prev.nextAvailable = block.nextAvailable;
			bool flag2 = block.nextAvailable != null;
			if (flag2)
			{
				block.nextAvailable.prevAvailable = block.prevAvailable;
			}
			prev.end = block.end;
			this.m_BlockPool.Return(block);
			return prev;
		}

		// Token: 0x06001141 RID: 4417 RVA: 0x00047F30 File Offset: 0x00046130
		internal HeapStatistics GatherStatistics()
		{
			HeapStatistics heapStatistics = default(HeapStatistics);
			for (BestFitAllocator.Block block = this.m_FirstBlock; block != null; block = block.next)
			{
				bool allocated = block.allocated;
				if (allocated)
				{
					heapStatistics.numAllocs += 1U;
					heapStatistics.allocatedSize += block.size;
				}
				else
				{
					heapStatistics.freeSize += block.size;
					heapStatistics.availableBlocksCount += 1U;
					heapStatistics.largestAvailableBlock = Math.Max(heapStatistics.largestAvailableBlock, block.size);
				}
				heapStatistics.blockCount += 1U;
			}
			heapStatistics.totalSize = this.totalSize;
			heapStatistics.highWatermark = this.m_HighWatermark;
			bool flag = heapStatistics.freeSize > 0U;
			if (flag)
			{
				heapStatistics.fragmentation = (float)((heapStatistics.freeSize - heapStatistics.largestAvailableBlock) / heapStatistics.freeSize) * 100f;
			}
			return heapStatistics;
		}

		// Token: 0x06001142 RID: 4418 RVA: 0x00048024 File Offset: 0x00046224
		private BestFitAllocator.Block BestFitFindAvailableBlock(uint size)
		{
			BestFitAllocator.Block block = this.m_FirstAvailableBlock;
			BestFitAllocator.Block block2 = null;
			uint num = uint.MaxValue;
			while (block != null)
			{
				bool flag = block.size >= size && num > block.size;
				if (flag)
				{
					block2 = block;
					num = block.size;
				}
				block = block.nextAvailable;
			}
			return block2;
		}

		// Token: 0x06001143 RID: 4419 RVA: 0x00048080 File Offset: 0x00046280
		private void SplitBlock(BestFitAllocator.Block block, uint size)
		{
			Debug.Assert(block.size > size);
			BestFitAllocator.Block block2 = this.m_BlockPool.Get();
			block2.next = block.next;
			block2.nextAvailable = block.nextAvailable;
			block2.prev = block;
			block2.prevAvailable = block;
			block2.start = block.start + size;
			block2.end = block.end;
			bool flag = block2.next != null;
			if (flag)
			{
				block2.next.prev = block2;
			}
			bool flag2 = block2.nextAvailable != null;
			if (flag2)
			{
				block2.nextAvailable.prevAvailable = block2;
			}
			block.next = block2;
			block.nextAvailable = block2;
			block.end = block2.start;
		}

		// Token: 0x04000817 RID: 2071
		private BestFitAllocator.Block m_FirstBlock;

		// Token: 0x04000818 RID: 2072
		private BestFitAllocator.Block m_FirstAvailableBlock;

		// Token: 0x04000819 RID: 2073
		private Pool<BestFitAllocator.Block> m_BlockPool = new Pool<BestFitAllocator.Block>();

		// Token: 0x0400081A RID: 2074
		private uint m_HighWatermark;

		// Token: 0x02000246 RID: 582
		private class Block : PoolItem
		{
			// Token: 0x1700049F RID: 1183
			// (get) Token: 0x06001144 RID: 4420 RVA: 0x00048138 File Offset: 0x00046338
			public uint size
			{
				get
				{
					return this.end - this.start;
				}
			}

			// Token: 0x0400081B RID: 2075
			public uint start;

			// Token: 0x0400081C RID: 2076
			public uint end;

			// Token: 0x0400081D RID: 2077
			public BestFitAllocator.Block prev;

			// Token: 0x0400081E RID: 2078
			public BestFitAllocator.Block next;

			// Token: 0x0400081F RID: 2079
			public BestFitAllocator.Block prevAvailable;

			// Token: 0x04000820 RID: 2080
			public BestFitAllocator.Block nextAvailable;

			// Token: 0x04000821 RID: 2081
			public bool allocated;
		}
	}
}
