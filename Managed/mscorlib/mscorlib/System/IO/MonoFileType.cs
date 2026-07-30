using System;

namespace System.IO
{
	// Token: 0x020003DC RID: 988
	internal enum MonoFileType
	{
		// Token: 0x04001814 RID: 6164
		Unknown,
		// Token: 0x04001815 RID: 6165
		Disk,
		// Token: 0x04001816 RID: 6166
		Char,
		// Token: 0x04001817 RID: 6167
		Pipe,
		// Token: 0x04001818 RID: 6168
		Remote = 32768
	}
}
