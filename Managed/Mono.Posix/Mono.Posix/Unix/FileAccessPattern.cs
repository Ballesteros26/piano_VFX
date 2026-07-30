using System;

namespace Mono.Unix
{
	// Token: 0x02000007 RID: 7
	public enum FileAccessPattern
	{
		// Token: 0x0400002E RID: 46
		Normal,
		// Token: 0x0400002F RID: 47
		Sequential = 2,
		// Token: 0x04000030 RID: 48
		Random = 1,
		// Token: 0x04000031 RID: 49
		NoReuse = 5,
		// Token: 0x04000032 RID: 50
		PreLoad = 3,
		// Token: 0x04000033 RID: 51
		FlushCache
	}
}
