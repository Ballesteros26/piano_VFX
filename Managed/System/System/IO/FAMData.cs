using System;
using System.Collections;

namespace System.IO
{
	// Token: 0x020003CA RID: 970
	internal class FAMData
	{
		// Token: 0x040019FB RID: 6651
		public FileSystemWatcher FSW;

		// Token: 0x040019FC RID: 6652
		public string Directory;

		// Token: 0x040019FD RID: 6653
		public string FileMask;

		// Token: 0x040019FE RID: 6654
		public bool IncludeSubdirs;

		// Token: 0x040019FF RID: 6655
		public bool Enabled;

		// Token: 0x04001A00 RID: 6656
		public FAMRequest Request;

		// Token: 0x04001A01 RID: 6657
		public Hashtable SubDirs;
	}
}
