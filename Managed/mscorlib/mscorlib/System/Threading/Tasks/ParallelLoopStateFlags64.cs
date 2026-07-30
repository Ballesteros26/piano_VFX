using System;

namespace System.Threading.Tasks
{
	// Token: 0x020004E6 RID: 1254
	internal class ParallelLoopStateFlags64 : ParallelLoopStateFlags
	{
		// Token: 0x1700097C RID: 2428
		// (get) Token: 0x060039DD RID: 14813 RVA: 0x000D1963 File Offset: 0x000CFB63
		internal long LowestBreakIteration
		{
			get
			{
				if (IntPtr.Size >= 8)
				{
					return this.m_lowestBreakIteration;
				}
				return Interlocked.Read(ref this.m_lowestBreakIteration);
			}
		}

		// Token: 0x1700097D RID: 2429
		// (get) Token: 0x060039DE RID: 14814 RVA: 0x000D1980 File Offset: 0x000CFB80
		internal long? NullableLowestBreakIteration
		{
			get
			{
				if (this.m_lowestBreakIteration == 9223372036854775807L)
				{
					return null;
				}
				if (IntPtr.Size >= 8)
				{
					return new long?(this.m_lowestBreakIteration);
				}
				return new long?(Interlocked.Read(ref this.m_lowestBreakIteration));
			}
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x000D19CC File Offset: 0x000CFBCC
		internal bool ShouldExitLoop(long CallerIteration)
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != ParallelLoopStateFlags.PLS_NONE && ((loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_CANCELED)) != 0 || ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0 && CallerIteration > this.LowestBreakIteration));
		}

		// Token: 0x060039E0 RID: 14816 RVA: 0x000D1A18 File Offset: 0x000CFC18
		internal bool ShouldExitLoop()
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != ParallelLoopStateFlags.PLS_NONE && (loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED)) != 0;
		}

		// Token: 0x04001E44 RID: 7748
		internal long m_lowestBreakIteration = long.MaxValue;
	}
}
