using System;

namespace System.Threading
{
	// Token: 0x020004B4 RID: 1204
	public sealed class PreAllocatedOverlapped : IDisposable
	{
		// Token: 0x06003869 RID: 14441 RVA: 0x000CC576 File Offset: 0x000CA776
		[CLSCompliant(false)]
		public PreAllocatedOverlapped(IOCompletionCallback callback, object state, object pinData)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600386A RID: 14442 RVA: 0x00002194 File Offset: 0x00000394
		public void Dispose()
		{
		}
	}
}
