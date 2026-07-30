using System;

namespace System.IO
{
	// Token: 0x020003DF RID: 991
	internal struct MonoIOStat
	{
		// Token: 0x04001838 RID: 6200
		public FileAttributes fileAttributes;

		// Token: 0x04001839 RID: 6201
		public long Length;

		// Token: 0x0400183A RID: 6202
		public long CreationTime;

		// Token: 0x0400183B RID: 6203
		public long LastAccessTime;

		// Token: 0x0400183C RID: 6204
		public long LastWriteTime;
	}
}
