using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x020003E3 RID: 995
	internal class KeventWatcher : IFileWatcher
	{
		// Token: 0x06001E4D RID: 7757 RVA: 0x000020EB File Offset: 0x000002EB
		private KeventWatcher()
		{
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x00078F60 File Offset: 0x00077160
		public static bool GetInstance(out IFileWatcher watcher)
		{
			if (KeventWatcher.failed)
			{
				watcher = null;
				return false;
			}
			if (KeventWatcher.instance != null)
			{
				watcher = KeventWatcher.instance;
				return true;
			}
			KeventWatcher.watches = Hashtable.Synchronized(new Hashtable());
			int num = KeventWatcher.kqueue();
			if (num == -1)
			{
				KeventWatcher.failed = true;
				watcher = null;
				return false;
			}
			KeventWatcher.close(num);
			KeventWatcher.instance = new KeventWatcher();
			watcher = KeventWatcher.instance;
			return true;
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x00078FC8 File Offset: 0x000771C8
		public void StartDispatching(FileSystemWatcher fsw)
		{
			KqueueMonitor kqueueMonitor;
			if (KeventWatcher.watches.ContainsKey(fsw))
			{
				kqueueMonitor = (KqueueMonitor)KeventWatcher.watches[fsw];
			}
			else
			{
				kqueueMonitor = new KqueueMonitor(fsw);
				KeventWatcher.watches.Add(fsw, kqueueMonitor);
			}
			kqueueMonitor.Start();
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x00079010 File Offset: 0x00077210
		public void StopDispatching(FileSystemWatcher fsw)
		{
			KqueueMonitor kqueueMonitor = (KqueueMonitor)KeventWatcher.watches[fsw];
			if (kqueueMonitor == null)
			{
				return;
			}
			kqueueMonitor.Stop();
		}

		// Token: 0x06001E51 RID: 7761
		[DllImport("libc")]
		private static extern int close(int fd);

		// Token: 0x06001E52 RID: 7762
		[DllImport("libc")]
		private static extern int kqueue();

		// Token: 0x04001AC4 RID: 6852
		private static bool failed;

		// Token: 0x04001AC5 RID: 6853
		private static KeventWatcher instance;

		// Token: 0x04001AC6 RID: 6854
		private static Hashtable watches;
	}
}
