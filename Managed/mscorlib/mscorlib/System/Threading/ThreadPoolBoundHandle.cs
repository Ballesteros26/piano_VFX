using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020004B3 RID: 1203
	public sealed class ThreadPoolBoundHandle : IDisposable
	{
		// Token: 0x06003861 RID: 14433 RVA: 0x00002111 File Offset: 0x00000311
		internal ThreadPoolBoundHandle()
		{
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06003862 RID: 14434 RVA: 0x000C8D51 File Offset: 0x000C6F51
		public SafeHandle Handle
		{
			get
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x000C8D51 File Offset: 0x000C6F51
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* AllocateNativeOverlapped(IOCompletionCallback callback, object state, object pinData)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x000C8D51 File Offset: 0x000C6F51
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* AllocateNativeOverlapped(PreAllocatedOverlapped preAllocated)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x000C8D51 File Offset: 0x000C6F51
		public static ThreadPoolBoundHandle BindHandle(SafeHandle handle)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x00002194 File Offset: 0x00000394
		public void Dispose()
		{
		}

		// Token: 0x06003867 RID: 14439 RVA: 0x000C8D51 File Offset: 0x000C6F51
		[CLSCompliant(false)]
		public unsafe void FreeNativeOverlapped(NativeOverlapped* overlapped)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003868 RID: 14440 RVA: 0x000C8D51 File Offset: 0x000C6F51
		[CLSCompliant(false)]
		public unsafe static object GetNativeOverlappedState(NativeOverlapped* overlapped)
		{
			throw new PlatformNotSupportedException();
		}
	}
}
