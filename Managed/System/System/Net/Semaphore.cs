using System;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200049B RID: 1179
	internal sealed class Semaphore : WaitHandle
	{
		// Token: 0x060022D8 RID: 8920 RVA: 0x00086D8C File Offset: 0x00084F8C
		internal Semaphore(int initialCount, int maxCount)
		{
			lock (this)
			{
				int num;
				this.Handle = Semaphore.CreateSemaphore_internal(initialCount, maxCount, null, out num);
			}
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x00086DD8 File Offset: 0x00084FD8
		internal bool ReleaseSemaphore()
		{
			int num;
			return Semaphore.ReleaseSemaphore_internal(this.Handle, 1, out num);
		}
	}
}
