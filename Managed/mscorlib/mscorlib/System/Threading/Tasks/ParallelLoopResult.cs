using System;

namespace System.Threading.Tasks
{
	/// <summary>Provides completion status on the execution of a <see cref="T:System.Threading.Tasks.Parallel" /> loop.</summary>
	// Token: 0x020004E7 RID: 1255
	public struct ParallelLoopResult
	{
		/// <summary>Gets whether the loop ran to completion, such that all iterations of the loop were executed and the loop didn't receive a request to end prematurely.</summary>
		/// <returns>true if the loop ran to completion; otherwise false;</returns>
		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x060039E2 RID: 14818 RVA: 0x000D1A5D File Offset: 0x000CFC5D
		public bool IsCompleted
		{
			get
			{
				return this.m_completed;
			}
		}

		/// <summary>Gets the index of the lowest iteration from which <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> was called.</summary>
		/// <returns>Returns an integer that represents the lowest iteration from which the Break statement was called.</returns>
		// Token: 0x1700097F RID: 2431
		// (get) Token: 0x060039E3 RID: 14819 RVA: 0x000D1A65 File Offset: 0x000CFC65
		public long? LowestBreakIteration
		{
			get
			{
				return this.m_lowestBreakIteration;
			}
		}

		// Token: 0x04001E45 RID: 7749
		internal bool m_completed;

		// Token: 0x04001E46 RID: 7750
		internal long? m_lowestBreakIteration;
	}
}
