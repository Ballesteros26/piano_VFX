using System;

namespace System.IO
{
	// Token: 0x020003D1 RID: 977
	internal interface IFileWatcher
	{
		// Token: 0x06001E03 RID: 7683
		void StartDispatching(FileSystemWatcher fsw);

		// Token: 0x06001E04 RID: 7684
		void StopDispatching(FileSystemWatcher fsw);
	}
}
