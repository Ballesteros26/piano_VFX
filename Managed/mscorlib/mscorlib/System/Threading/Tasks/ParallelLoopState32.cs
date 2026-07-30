using System;

namespace System.Threading.Tasks
{
	// Token: 0x020004E2 RID: 1250
	internal class ParallelLoopState32 : ParallelLoopState
	{
		// Token: 0x060039C4 RID: 14788 RVA: 0x000D16F0 File Offset: 0x000CF8F0
		internal ParallelLoopState32(ParallelLoopStateFlags32 sharedParallelStateFlags)
			: base(sharedParallelStateFlags)
		{
			this.m_sharedParallelStateFlags = sharedParallelStateFlags;
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x060039C5 RID: 14789 RVA: 0x000D1700 File Offset: 0x000CF900
		// (set) Token: 0x060039C6 RID: 14790 RVA: 0x000D1708 File Offset: 0x000CF908
		internal int CurrentIteration
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

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x060039C7 RID: 14791 RVA: 0x000D1711 File Offset: 0x000CF911
		internal override bool InternalShouldExitCurrentIteration
		{
			get
			{
				return this.m_sharedParallelStateFlags.ShouldExitLoop(this.CurrentIteration);
			}
		}

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x060039C8 RID: 14792 RVA: 0x000D1724 File Offset: 0x000CF924
		internal override long? InternalLowestBreakIteration
		{
			get
			{
				return this.m_sharedParallelStateFlags.NullableLowestBreakIteration;
			}
		}

		// Token: 0x060039C9 RID: 14793 RVA: 0x000D1731 File Offset: 0x000CF931
		internal override void InternalBreak()
		{
			ParallelLoopState.Break(this.CurrentIteration, this.m_sharedParallelStateFlags);
		}

		// Token: 0x04001E39 RID: 7737
		private ParallelLoopStateFlags32 m_sharedParallelStateFlags;

		// Token: 0x04001E3A RID: 7738
		private int m_currentIteration;
	}
}
