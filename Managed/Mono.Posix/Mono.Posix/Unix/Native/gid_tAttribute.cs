using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000074 RID: 116
	[AttributeUsage(AttributeTargets.Field)]
	internal class gid_tAttribute : MapAttribute
	{
		// Token: 0x06000659 RID: 1625 RVA: 0x0000E1A1 File Offset: 0x0000C3A1
		public gid_tAttribute()
			: base("gid_t")
		{
		}
	}
}
