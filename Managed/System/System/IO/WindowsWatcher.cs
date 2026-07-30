using System;

namespace System.IO
{
	// Token: 0x020003ED RID: 1005
	internal class WindowsWatcher : IFileWatcher
	{
		// Token: 0x06001E6F RID: 7791 RVA: 0x000020EB File Offset: 0x000002EB
		private WindowsWatcher()
		{
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x000074E4 File Offset: 0x000056E4
		public static bool GetInstance(out IFileWatcher watcher)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x000027E8 File Offset: 0x000009E8
		public void StartDispatching(FileSystemWatcher fsw)
		{
		}

		// Token: 0x06001E72 RID: 7794 RVA: 0x000027E8 File Offset: 0x000009E8
		public void StopDispatching(FileSystemWatcher fsw)
		{
		}
	}
}
