using System;

namespace Mono.Unix
{
	// Token: 0x0200000A RID: 10
	[Flags]
	public enum FileSpecialAttributes
	{
		// Token: 0x04000044 RID: 68
		SetUserId = 2048,
		// Token: 0x04000045 RID: 69
		SetGroupId = 1024,
		// Token: 0x04000046 RID: 70
		Sticky = 512
	}
}
