using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020004AD RID: 1197
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class InternalThread : CriticalFinalizerObject
	{
		// Token: 0x06003821 RID: 14369
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Thread_free_internal();

		// Token: 0x06003822 RID: 14370 RVA: 0x000CBD7C File Offset: 0x000C9F7C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~InternalThread()
		{
			this.Thread_free_internal();
		}

		// Token: 0x04001D5C RID: 7516
		private int lock_thread_id;

		// Token: 0x04001D5D RID: 7517
		private IntPtr handle;

		// Token: 0x04001D5E RID: 7518
		private IntPtr native_handle;

		// Token: 0x04001D5F RID: 7519
		private IntPtr unused3;

		// Token: 0x04001D60 RID: 7520
		private IntPtr name;

		// Token: 0x04001D61 RID: 7521
		private int name_len;

		// Token: 0x04001D62 RID: 7522
		private ThreadState state;

		// Token: 0x04001D63 RID: 7523
		private object abort_exc;

		// Token: 0x04001D64 RID: 7524
		private int abort_state_handle;

		// Token: 0x04001D65 RID: 7525
		internal long thread_id;

		// Token: 0x04001D66 RID: 7526
		private IntPtr debugger_thread;

		// Token: 0x04001D67 RID: 7527
		private UIntPtr static_data;

		// Token: 0x04001D68 RID: 7528
		private IntPtr runtime_thread_info;

		// Token: 0x04001D69 RID: 7529
		private object current_appcontext;

		// Token: 0x04001D6A RID: 7530
		private object root_domain_thread;

		// Token: 0x04001D6B RID: 7531
		internal byte[] _serialized_principal;

		// Token: 0x04001D6C RID: 7532
		internal int _serialized_principal_version;

		// Token: 0x04001D6D RID: 7533
		private IntPtr appdomain_refs;

		// Token: 0x04001D6E RID: 7534
		private int interruption_requested;

		// Token: 0x04001D6F RID: 7535
		private IntPtr synch_cs;

		// Token: 0x04001D70 RID: 7536
		internal bool threadpool_thread;

		// Token: 0x04001D71 RID: 7537
		private bool thread_interrupt_requested;

		// Token: 0x04001D72 RID: 7538
		internal int stack_size;

		// Token: 0x04001D73 RID: 7539
		internal byte apartment_state;

		// Token: 0x04001D74 RID: 7540
		internal volatile int critical_region_level;

		// Token: 0x04001D75 RID: 7541
		internal int managed_id;

		// Token: 0x04001D76 RID: 7542
		private int small_id;

		// Token: 0x04001D77 RID: 7543
		private IntPtr manage_callback;

		// Token: 0x04001D78 RID: 7544
		private IntPtr unused4;

		// Token: 0x04001D79 RID: 7545
		private IntPtr flags;

		// Token: 0x04001D7A RID: 7546
		private IntPtr thread_pinning_ref;

		// Token: 0x04001D7B RID: 7547
		private IntPtr abort_protected_block_count;

		// Token: 0x04001D7C RID: 7548
		private int priority = 2;

		// Token: 0x04001D7D RID: 7549
		private IntPtr owned_mutex;

		// Token: 0x04001D7E RID: 7550
		private IntPtr suspended_event;

		// Token: 0x04001D7F RID: 7551
		private int self_suspended;

		// Token: 0x04001D80 RID: 7552
		private IntPtr unused1;

		// Token: 0x04001D81 RID: 7553
		private IntPtr unused2;

		// Token: 0x04001D82 RID: 7554
		private IntPtr last;
	}
}
