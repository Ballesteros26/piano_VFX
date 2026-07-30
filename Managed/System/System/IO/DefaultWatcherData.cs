using System;
using System.Collections;

namespace System.IO
{
	// Token: 0x020003C2 RID: 962
	internal class DefaultWatcherData
	{
		// Token: 0x040019DB RID: 6619
		public FileSystemWatcher FSW;

		// Token: 0x040019DC RID: 6620
		public string Directory;

		// Token: 0x040019DD RID: 6621
		public string FileMask;

		// Token: 0x040019DE RID: 6622
		public bool IncludeSubdirs;

		// Token: 0x040019DF RID: 6623
		public bool Enabled;

		// Token: 0x040019E0 RID: 6624
		public bool NoWildcards;

		// Token: 0x040019E1 RID: 6625
		public DateTime DisabledTime;

		// Token: 0x040019E2 RID: 6626
		public object FilesLock = new object();

		// Token: 0x040019E3 RID: 6627
		public Hashtable Files;
	}
}
