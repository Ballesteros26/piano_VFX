using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000072 RID: 114
	[AttributeUsage(AttributeTargets.Field)]
	internal class blksize_tAttribute : MapAttribute
	{
		// Token: 0x06000657 RID: 1623 RVA: 0x0000E187 File Offset: 0x0000C387
		public blksize_tAttribute()
			: base("blksize_t")
		{
		}
	}
}
