using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Indicates whether an <see cref="T:System.Threading.EventWaitHandle" /> is reset automatically or manually after receiving a signal.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200046F RID: 1135
	[ComVisible(false)]
	public enum EventResetMode
	{
		/// <summary>When signaled, the <see cref="T:System.Threading.EventWaitHandle" /> resets automatically after releasing a single thread. If no threads are waiting, the <see cref="T:System.Threading.EventWaitHandle" /> remains signaled until a thread blocks, and resets after releasing the thread.</summary>
		// Token: 0x04001CAA RID: 7338
		AutoReset,
		/// <summary>When signaled, the <see cref="T:System.Threading.EventWaitHandle" /> releases all waiting threads and remains signaled until it is manually reset.</summary>
		// Token: 0x04001CAB RID: 7339
		ManualReset
	}
}
