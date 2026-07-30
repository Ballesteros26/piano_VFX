using System;

namespace System.IO
{
	// Token: 0x020003E5 RID: 997
	internal class NullFileWatcher : IFileWatcher
	{
		// Token: 0x06001E53 RID: 7763 RVA: 0x000027E8 File Offset: 0x000009E8
		public void StartDispatching(FileSystemWatcher fsw)
		{
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x000027E8 File Offset: 0x000009E8
		public void StopDispatching(FileSystemWatcher fsw)
		{
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x00079038 File Offset: 0x00077238
		public static bool GetInstance(out IFileWatcher watcher)
		{
			if (NullFileWatcher.instance != null)
			{
				watcher = NullFileWatcher.instance;
				return true;
			}
			IFileWatcher fileWatcher;
			watcher = (fileWatcher = new NullFileWatcher());
			NullFileWatcher.instance = fileWatcher;
			return true;
		}

		// Token: 0x04001AD0 RID: 6864
		private static IFileWatcher instance;
	}
}
