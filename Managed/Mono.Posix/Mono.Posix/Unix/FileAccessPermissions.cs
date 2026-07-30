using System;

namespace Mono.Unix
{
	// Token: 0x02000008 RID: 8
	[Flags]
	public enum FileAccessPermissions
	{
		// Token: 0x04000035 RID: 53
		UserReadWriteExecute = 448,
		// Token: 0x04000036 RID: 54
		UserRead = 256,
		// Token: 0x04000037 RID: 55
		UserWrite = 128,
		// Token: 0x04000038 RID: 56
		UserExecute = 64,
		// Token: 0x04000039 RID: 57
		GroupReadWriteExecute = 56,
		// Token: 0x0400003A RID: 58
		GroupRead = 32,
		// Token: 0x0400003B RID: 59
		GroupWrite = 16,
		// Token: 0x0400003C RID: 60
		GroupExecute = 8,
		// Token: 0x0400003D RID: 61
		OtherReadWriteExecute = 7,
		// Token: 0x0400003E RID: 62
		OtherRead = 4,
		// Token: 0x0400003F RID: 63
		OtherWrite = 2,
		// Token: 0x04000040 RID: 64
		OtherExecute = 1,
		// Token: 0x04000041 RID: 65
		DefaultPermissions = 438,
		// Token: 0x04000042 RID: 66
		AllPermissions = 511
	}
}
