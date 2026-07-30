using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000037 RID: 55
	[Map]
	[Flags]
	[CLSCompliant(false)]
	public enum DirectoryNotifyFlags
	{
		// Token: 0x040001D2 RID: 466
		DN_ACCESS = 1,
		// Token: 0x040001D3 RID: 467
		DN_MODIFY = 2,
		// Token: 0x040001D4 RID: 468
		DN_CREATE = 4,
		// Token: 0x040001D5 RID: 469
		DN_DELETE = 8,
		// Token: 0x040001D6 RID: 470
		DN_RENAME = 16,
		// Token: 0x040001D7 RID: 471
		DN_ATTRIB = 32,
		// Token: 0x040001D8 RID: 472
		DN_MULTISHOT = -2147483648
	}
}
