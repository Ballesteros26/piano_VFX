using System;

namespace System.Threading.Tasks
{
	// Token: 0x020004E4 RID: 1252
	internal class ParallelLoopStateFlags
	{
		// Token: 0x17000979 RID: 2425
		// (get) Token: 0x060039D0 RID: 14800 RVA: 0x000D1798 File Offset: 0x000CF998
		internal int LoopStateFlags
		{
			get
			{
				return this.m_LoopStateFlags;
			}
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x000D17A4 File Offset: 0x000CF9A4
		internal bool AtomicLoopStateUpdate(int newState, int illegalStates)
		{
			int num = 0;
			return this.AtomicLoopStateUpdate(newState, illegalStates, ref num);
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x000D17C0 File Offset: 0x000CF9C0
		internal bool AtomicLoopStateUpdate(int newState, int illegalStates, ref int oldState)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				oldState = this.m_LoopStateFlags;
				if ((oldState & illegalStates) != 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this.m_LoopStateFlags, oldState | newState, oldState) == oldState)
				{
					return true;
				}
				spinWait.SpinOnce();
			}
			return false;
		}

		// Token: 0x060039D3 RID: 14803 RVA: 0x000D1806 File Offset: 0x000CFA06
		internal void SetExceptional()
		{
			this.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_EXCEPTIONAL, ParallelLoopStateFlags.PLS_NONE);
		}

		// Token: 0x060039D4 RID: 14804 RVA: 0x000D1819 File Offset: 0x000CFA19
		internal void Stop()
		{
			if (!this.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_STOPPED, ParallelLoopStateFlags.PLS_BROKEN))
			{
				throw new InvalidOperationException(Environment.GetResourceString("Stop was called after Break was called."));
			}
		}

		// Token: 0x060039D5 RID: 14805 RVA: 0x000D183D File Offset: 0x000CFA3D
		internal bool Cancel()
		{
			return this.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_CANCELED, ParallelLoopStateFlags.PLS_NONE);
		}

		// Token: 0x04001E3D RID: 7741
		internal static int PLS_NONE;

		// Token: 0x04001E3E RID: 7742
		internal static int PLS_EXCEPTIONAL = 1;

		// Token: 0x04001E3F RID: 7743
		internal static int PLS_BROKEN = 2;

		// Token: 0x04001E40 RID: 7744
		internal static int PLS_STOPPED = 4;

		// Token: 0x04001E41 RID: 7745
		internal static int PLS_CANCELED = 8;

		// Token: 0x04001E42 RID: 7746
		private volatile int m_LoopStateFlags = ParallelLoopStateFlags.PLS_NONE;
	}
}
