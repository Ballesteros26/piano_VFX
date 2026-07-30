using System;

namespace System.Threading.Tasks
{
	// Token: 0x020004E3 RID: 1251
	internal class ParallelLoopState64 : ParallelLoopState
	{
		// Token: 0x060039CA RID: 14794 RVA: 0x000D1744 File Offset: 0x000CF944
		internal ParallelLoopState64(ParallelLoopStateFlags64 sharedParallelStateFlags)
			: base(sharedParallelStateFlags)
		{
			this.m_sharedParallelStateFlags = sharedParallelStateFlags;
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x060039CB RID: 14795 RVA: 0x000D1754 File Offset: 0x000CF954
		// (set) Token: 0x060039CC RID: 14796 RVA: 0x000D175C File Offset: 0x000CF95C
		internal long CurrentIteration
		{
			get
			{
				return this.m_currentIteration;
			}
			set
			{
				this.m_currentIteration = value;
			}
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x060039CD RID: 14797 RVA: 0x000D1765 File Offset: 0x000CF965
		internal override bool InternalShouldExitCurrentIteration
		{
			get
			{
				return this.m_sharedParallelStateFlags.ShouldExitLoop(this.CurrentIteration);
			}
		}

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x060039CE RID: 14798 RVA: 0x000D1778 File Offset: 0x000CF978
		internal override long? InternalLowestBreakIteration
		{
			get
			{
				return this.m_sharedParallelStateFlags.NullableLowestBreakIteration;
			}
		}

		// Token: 0x060039CF RID: 14799 RVA: 0x000D1785 File Offset: 0x000CF985
		internal override void InternalBreak()
		{
			ParallelLoopState.Break(this.CurrentIteration, this.m_sharedParallelStateFlags);
		}

		// Token: 0x04001E3B RID: 7739
		private ParallelLoopStateFlags64 m_sharedParallelStateFlags;

		// Token: 0x04001E3C RID: 7740
		private long m_currentIteration;
	}
}
