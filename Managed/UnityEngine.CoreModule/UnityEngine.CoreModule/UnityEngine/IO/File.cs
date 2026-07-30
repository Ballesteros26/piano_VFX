using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.IO
{
	// Token: 0x02000391 RID: 913
	[StaticAccessor("FileAccessor", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/VirtualFileSystem/VirtualFileSystem.h")]
	[NativeConditional("ENABLE_PROFILER")]
	internal static class File
	{
		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x06001FC8 RID: 8136 RVA: 0x000362EC File Offset: 0x000344EC
		internal static ulong totalOpenCalls
		{
			get
			{
				return File.GetTotalOpenCalls();
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001FC9 RID: 8137 RVA: 0x00036304 File Offset: 0x00034504
		internal static ulong totalCloseCalls
		{
			get
			{
				return File.GetTotalCloseCalls();
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001FCA RID: 8138 RVA: 0x0003631C File Offset: 0x0003451C
		internal static ulong totalReadCalls
		{
			get
			{
				return File.GetTotalReadCalls();
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001FCB RID: 8139 RVA: 0x00036334 File Offset: 0x00034534
		internal static ulong totalWriteCalls
		{
			get
			{
				return File.GetTotalWriteCalls();
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001FCC RID: 8140 RVA: 0x0003634C File Offset: 0x0003454C
		internal static ulong totalSeekCalls
		{
			get
			{
				return File.GetTotalSeekCalls();
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001FCD RID: 8141 RVA: 0x00036364 File Offset: 0x00034564
		internal static ulong totalZeroSeekCalls
		{
			get
			{
				return File.GetTotalZeroSeekCalls();
			}
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001FCE RID: 8142 RVA: 0x0003637C File Offset: 0x0003457C
		internal static ulong totalFilesOpened
		{
			get
			{
				return File.GetTotalFilesOpened();
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001FCF RID: 8143 RVA: 0x00036394 File Offset: 0x00034594
		internal static ulong totalFilesClosed
		{
			get
			{
				return File.GetTotalFilesClosed();
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001FD0 RID: 8144 RVA: 0x000363AC File Offset: 0x000345AC
		internal static ulong totalBytesRead
		{
			get
			{
				return File.GetTotalBytesRead();
			}
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001FD1 RID: 8145 RVA: 0x000363C4 File Offset: 0x000345C4
		internal static ulong totalBytesWritten
		{
			get
			{
				return File.GetTotalBytesWritten();
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001FD3 RID: 8147 RVA: 0x000363E8 File Offset: 0x000345E8
		// (set) Token: 0x06001FD2 RID: 8146 RVA: 0x000363DB File Offset: 0x000345DB
		internal static bool recordZeroSeeks
		{
			get
			{
				return File.GetRecordZeroSeeks();
			}
			set
			{
				File.SetRecordZeroSeeks(value);
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001FD4 RID: 8148 RVA: 0x00036400 File Offset: 0x00034600
		// (set) Token: 0x06001FD5 RID: 8149 RVA: 0x00036417 File Offset: 0x00034617
		internal static ThreadIORestrictionMode MainThreadIORestrictionMode
		{
			get
			{
				return File.GetMainThreadFileIORestriction();
			}
			set
			{
				File.SetMainThreadFileIORestriction(value);
			}
		}

		// Token: 0x06001FD6 RID: 8150
		[MethodImpl(4096)]
		internal static extern void SetRecordZeroSeeks(bool enable);

		// Token: 0x06001FD7 RID: 8151
		[MethodImpl(4096)]
		internal static extern bool GetRecordZeroSeeks();

		// Token: 0x06001FD8 RID: 8152
		[MethodImpl(4096)]
		internal static extern ulong GetTotalOpenCalls();

		// Token: 0x06001FD9 RID: 8153
		[MethodImpl(4096)]
		internal static extern ulong GetTotalCloseCalls();

		// Token: 0x06001FDA RID: 8154
		[MethodImpl(4096)]
		internal static extern ulong GetTotalReadCalls();

		// Token: 0x06001FDB RID: 8155
		[MethodImpl(4096)]
		internal static extern ulong GetTotalWriteCalls();

		// Token: 0x06001FDC RID: 8156
		[MethodImpl(4096)]
		internal static extern ulong GetTotalSeekCalls();

		// Token: 0x06001FDD RID: 8157
		[MethodImpl(4096)]
		internal static extern ulong GetTotalZeroSeekCalls();

		// Token: 0x06001FDE RID: 8158
		[MethodImpl(4096)]
		internal static extern ulong GetTotalFilesOpened();

		// Token: 0x06001FDF RID: 8159
		[MethodImpl(4096)]
		internal static extern ulong GetTotalFilesClosed();

		// Token: 0x06001FE0 RID: 8160
		[MethodImpl(4096)]
		internal static extern ulong GetTotalBytesRead();

		// Token: 0x06001FE1 RID: 8161
		[MethodImpl(4096)]
		internal static extern ulong GetTotalBytesWritten();

		// Token: 0x06001FE2 RID: 8162
		[MethodImpl(4096)]
		private static extern void SetMainThreadFileIORestriction(ThreadIORestrictionMode mode);

		// Token: 0x06001FE3 RID: 8163
		[MethodImpl(4096)]
		private static extern ThreadIORestrictionMode GetMainThreadFileIORestriction();
	}
}
