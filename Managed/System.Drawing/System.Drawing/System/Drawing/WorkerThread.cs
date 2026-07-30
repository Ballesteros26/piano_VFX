using System;
using System.Threading;

namespace System.Drawing
{
	// Token: 0x02000075 RID: 117
	internal class WorkerThread
	{
		// Token: 0x06000522 RID: 1314 RVA: 0x0000EE35 File Offset: 0x0000D035
		public WorkerThread(EventHandler frmChgHandler, AnimateEventArgs aniEvtArgs, int[] delay)
		{
			this.frameChangeHandler = frmChgHandler;
			this.animateEventArgs = aniEvtArgs;
			this.delay = delay;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0000EE54 File Offset: 0x0000D054
		public void LoopHandler()
		{
			try
			{
				int num = 0;
				for (;;)
				{
					Thread.Sleep(this.delay[num++]);
					this.frameChangeHandler(null, this.animateEventArgs);
					if (num == this.delay.Length)
					{
						num = 0;
					}
				}
			}
			catch (ThreadAbortException)
			{
				Thread.ResetAbort();
			}
		}

		// Token: 0x040003F7 RID: 1015
		private EventHandler frameChangeHandler;

		// Token: 0x040003F8 RID: 1016
		private AnimateEventArgs animateEventArgs;

		// Token: 0x040003F9 RID: 1017
		private int[] delay;
	}
}
