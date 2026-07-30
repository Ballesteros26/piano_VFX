using System;
using System.Diagnostics;
using System.Security.Permissions;
using Unity;

namespace System.Threading.Tasks
{
	/// <summary>Enables iterations of <see cref="T:System.Threading.Tasks.Parallel" /> loops to interact with other iterations. An instance of this class is provided by the Parallel class to each loop; you can not create instances in your user code.</summary>
	// Token: 0x020004E1 RID: 1249
	[DebuggerDisplay("ShouldExitCurrentIteration = {ShouldExitCurrentIteration}")]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true, ExternalThreading = true)]
	public class ParallelLoopState
	{
		// Token: 0x060039B7 RID: 14775 RVA: 0x000D1570 File Offset: 0x000CF770
		internal ParallelLoopState(ParallelLoopStateFlags fbase)
		{
			this.m_flagsBase = fbase;
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x060039B8 RID: 14776 RVA: 0x000D157F File Offset: 0x000CF77F
		internal virtual bool InternalShouldExitCurrentIteration
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("This method is not supported."));
			}
		}

		/// <summary>Gets whether the current iteration of the loop should exit based on requests made by this or other iterations.</summary>
		/// <returns>true if the current iteration should exit; otherwise false.</returns>
		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x060039B9 RID: 14777 RVA: 0x000D1590 File Offset: 0x000CF790
		public bool ShouldExitCurrentIteration
		{
			get
			{
				return this.InternalShouldExitCurrentIteration;
			}
		}

		/// <summary>Gets whether any iteration of the loop has called <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" />.</summary>
		/// <returns>true if any iteration has stopped the loop; otherwise false.</returns>
		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x060039BA RID: 14778 RVA: 0x000D1598 File Offset: 0x000CF798
		public bool IsStopped
		{
			get
			{
				return (this.m_flagsBase.LoopStateFlags & ParallelLoopStateFlags.PLS_STOPPED) != 0;
			}
		}

		/// <summary>Gets whether any iteration of the loop has thrown an exception that went unhandled by that iteration.</summary>
		/// <returns>True if an unhandled exception was thrown; otherwise false.</returns>
		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x060039BB RID: 14779 RVA: 0x000D15AE File Offset: 0x000CF7AE
		public bool IsExceptional
		{
			get
			{
				return (this.m_flagsBase.LoopStateFlags & ParallelLoopStateFlags.PLS_EXCEPTIONAL) != 0;
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x060039BC RID: 14780 RVA: 0x000D157F File Offset: 0x000CF77F
		internal virtual long? InternalLowestBreakIteration
		{
			get
			{
				throw new NotSupportedException(Environment.GetResourceString("This method is not supported."));
			}
		}

		/// <summary>Gets the lowest iteration of the loop from which <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> was called. </summary>
		/// <returns>An integer that represents the lowest iteration from which Break was called. In the case of a <see cref="M:System.Threading.Tasks.Parallel.ForEach``1(System.Collections.Concurrent.Partitioner{``0},System.Action{``0})" /> loop, the value is based on an internally-generated index.</returns>
		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x060039BD RID: 14781 RVA: 0x000D15C4 File Offset: 0x000CF7C4
		public long? LowestBreakIteration
		{
			get
			{
				return this.InternalLowestBreakIteration;
			}
		}

		/// <summary>Communicates that the <see cref="T:System.Threading.Tasks.Parallel" /> loop should cease execution at the system's earliest convenience.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> method was previously called. <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> and <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> may not be used in combination by iterations of the same loop.</exception>
		// Token: 0x060039BE RID: 14782 RVA: 0x000D15CC File Offset: 0x000CF7CC
		public void Stop()
		{
			this.m_flagsBase.Stop();
		}

		// Token: 0x060039BF RID: 14783 RVA: 0x000D157F File Offset: 0x000CF77F
		internal virtual void InternalBreak()
		{
			throw new NotSupportedException(Environment.GetResourceString("This method is not supported."));
		}

		/// <summary>Communicates that the <see cref="T:System.Threading.Tasks.Parallel" /> loop should cease execution at the system's earliest convenience of iterations beyond the current iteration.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> method was previously called. <see cref="M:System.Threading.Tasks.ParallelLoopState.Break" /> and <see cref="M:System.Threading.Tasks.ParallelLoopState.Stop" /> may not be used in combination by iterations of the same loop.</exception>
		// Token: 0x060039C0 RID: 14784 RVA: 0x000D15D9 File Offset: 0x000CF7D9
		public void Break()
		{
			this.InternalBreak();
		}

		// Token: 0x060039C1 RID: 14785 RVA: 0x000D15E4 File Offset: 0x000CF7E4
		internal static void Break(int iteration, ParallelLoopStateFlags32 pflags)
		{
			int pls_NONE = ParallelLoopStateFlags.PLS_NONE;
			if (pflags.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_BROKEN, ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED, ref pls_NONE))
			{
				int num = pflags.m_lowestBreakIteration;
				if (iteration < num)
				{
					SpinWait spinWait = default(SpinWait);
					while (Interlocked.CompareExchange(ref pflags.m_lowestBreakIteration, iteration, num) != num)
					{
						spinWait.SpinOnce();
						num = pflags.m_lowestBreakIteration;
						if (iteration > num)
						{
							break;
						}
					}
				}
				return;
			}
			if ((pls_NONE & ParallelLoopStateFlags.PLS_STOPPED) != 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Break was called after Stop was called."));
			}
		}

		// Token: 0x060039C2 RID: 14786 RVA: 0x000D166C File Offset: 0x000CF86C
		internal static void Break(long iteration, ParallelLoopStateFlags64 pflags)
		{
			int pls_NONE = ParallelLoopStateFlags.PLS_NONE;
			if (pflags.AtomicLoopStateUpdate(ParallelLoopStateFlags.PLS_BROKEN, ParallelLoopStateFlags.PLS_STOPPED | ParallelLoopStateFlags.PLS_EXCEPTIONAL | ParallelLoopStateFlags.PLS_CANCELED, ref pls_NONE))
			{
				long num = pflags.LowestBreakIteration;
				if (iteration < num)
				{
					SpinWait spinWait = default(SpinWait);
					while (Interlocked.CompareExchange(ref pflags.m_lowestBreakIteration, iteration, num) != num)
					{
						spinWait.SpinOnce();
						num = pflags.LowestBreakIteration;
						if (iteration > num)
						{
							break;
						}
					}
				}
				return;
			}
			if ((pls_NONE & ParallelLoopStateFlags.PLS_STOPPED) != 0)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Break was called after Stop was called."));
			}
		}

		// Token: 0x060039C3 RID: 14787 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal ParallelLoopState()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001E38 RID: 7736
		private ParallelLoopStateFlags m_flagsBase;
	}
}
