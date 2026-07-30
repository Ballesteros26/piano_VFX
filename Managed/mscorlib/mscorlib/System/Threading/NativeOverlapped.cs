using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Provides an explicit layout that is visible from unmanaged code and that will have the same layout as the Win32 OVERLAPPED structure with additional reserved fields at the end.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020004A9 RID: 1193
	[ComVisible(true)]
	public struct NativeOverlapped
	{
		/// <summary>Specifies a system-dependent status. Reserved for operating system use.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001D43 RID: 7491
		public IntPtr InternalLow;

		/// <summary>Specifies the length of the data transferred. Reserved for operating system use.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001D44 RID: 7492
		public IntPtr InternalHigh;

		/// <summary>Specifies a file position at which to start the transfer.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001D45 RID: 7493
		public int OffsetLow;

		/// <summary>Specifies the high word of the byte offset at which to start the transfer.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001D46 RID: 7494
		public int OffsetHigh;

		/// <summary>Specifies the handle to an event set to the signaled state when the operation is complete. The calling process must set this member either to zero or to a valid event handle before calling any overlapped functions.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04001D47 RID: 7495
		public IntPtr EventHandle;
	}
}
