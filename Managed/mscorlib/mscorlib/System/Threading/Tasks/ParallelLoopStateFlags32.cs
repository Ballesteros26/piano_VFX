using System;

namespace System.Threading.Tasks
{
	// Token: 0x020004E5 RID: 1253
	internal class ParallelLoopStateFlags32 : ParallelLoopStateFlags
	{
		// Token: 0x1700097A RID: 2426
		// (get) Token: 0x060039D8 RID: 14808 RVA: 0x000D187E File Offset: 0x000CFA7E
		internal int LowestBreakIteration
		{
			get
			{
				return this.m_lowestBreakIteration;
			}
		}

		// Token: 0x1700097B RID: 2427
		// (get) Token: 0x060039D9 RID: 14809 RVA: 0x000D1888 File Offset: 0x000CFA88
		internal long? NullableLowestBreakIteration
		{
			get
			{
				if (this.m_lowestBreakIteration == 2147483647)
				{
					return null;
				}
				long num = (long)this.m_lowestBreakIteration;
				if (IntPtr.Size >= 8)
				{
					return new long?(num);
				}
				return new long?(Interlocked.Read(ref num));
			}
		}

		// Token: 0x060039DA RID: 14810 RVA: 0x000D18D4 File Offset: 0x000CFAD4
		internal bool ShouldExitLoop(int CallerIteration)
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != ParallelLoopStateFlags.PLS_NONE && ((loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_CANCELED)) != 0 || ((loopStateFlags & ParallelLoopStateFlags.PLS_BROKEN) != 0 && CallerIteration > this.LowestBreakIteration));
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x000D1920 File Offset: 0x000CFB20
		internal bool ShouldExitLoop()
		{
			int loopStateFlags = base.LoopStateFlags;
			return loopStateFlags != ParallelLoopStateFlags.PLS_NONE && (loopStateFlags & (ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED)) != 0;
		}

		// Token: 0x04001E43 RID: 7747
		internal volatile int m_lowestBreakIteration = int.MaxValue;
	}
}
